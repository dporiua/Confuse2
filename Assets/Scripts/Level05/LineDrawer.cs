using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dana
{
    /// <summary>
    /// This script is meant to draw a line based on colliders.
    /// It uses LineRenderer to render line points and deletes the line
    /// Changes material colour after a short delay as well. 
    /// </summary>
 
    public class LineDrawer : MonoBehaviour
    {
        #region Variables
        [Tooltip("Assign any object which contains a line renderer component into this slot.")]
        [SerializeField] private GameObject lineRendererPrefab;
        [Tooltip("Assign the camera that is used to render the maze view here.")]
        [SerializeField] private Camera mainCamera;

        [Header("Layer Masks")]
        [Tooltip("Assign a layer related to maze ground into this slot, this will help the script with identifying places where the line can be drawn.")]
        [SerializeField] private LayerMask groundLayerMask;
        [Tooltip("Assign a layer related to maze wall into this slot, this will help the script identify places where the pen can't go beyond.")]
        [SerializeField] private LayerMask wallLayerMask;

        [Header("Line Settings")]
        [Tooltip("Delay time before the line is cleared.")]
        [SerializeField] private float clearDelay = 0.6f;
        [Tooltip("Assign a colour from the colour wheel to represent line error colour")]
        [SerializeField] private Color incorrectLineColour = Color.red;

        [Header("Start and End Points")]
        [Tooltip("Assign a Start empty GameObject with a collider.")]
        [SerializeField] private GameObject startGameObject;
        [Tooltip("Assign a End empty GameObject with a collider.")]
        [SerializeField] private GameObject endGameObject;

        private List<LineRenderer> activeLines = new List<LineRenderer>();
        private LineRenderer _currentLine;
        private List<Vector3> _linePoints = new List<Vector3>();
        private bool _isDrawing = false;
        private bool _startedFromStart = false;

        #endregion

        private void Update()
        {
            HandleDrawingInput();
        }


        #region Public Functions
        /// <summary>
        /// Destroys all active lines, ensuring no lines stay in the scene.
        /// </summary>
        public void ClearAllLines()
        {
            foreach (var line in activeLines)
            {
                if (line != null)
                {
                    Destroy(line.gameObject);
                }
            }
            activeLines.Clear();
        }
        #endregion

        #region Private Functions
        /// <summary>
        /// Handles user input for starting, continuing, and ending line drawing.
        /// </summary>
        private void HandleDrawingInput()
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            bool isGroundHit = Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayerMask);

            if (Input.GetMouseButtonDown(0) && isGroundHit)
            {
                if (IsStartPoint(hit))
                {
                    Debug.Log("It has hit start.");
                    _startedFromStart = true;
                    StartDrawing(hit.point);
                }
            }

            if (_isDrawing && Input.GetMouseButton(0) && isGroundHit)
            {
                AddPoint(hit.point);
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (IsEndPoint())
                {
                    _isDrawing = false;
                }
                else
                {
                    FinishLine();
                }
            }
        }

        /// <summary>
        /// Starts a new line when the player begins drawing.
        /// </summary>
        private void StartDrawing(Vector3 startPoint)
        {
            _isDrawing = true;

            GameObject line = Instantiate(lineRendererPrefab, startPoint, Quaternion.identity);
            _currentLine = line.GetComponent<LineRenderer>();
            _linePoints.Clear();

            _currentLine.sortingLayerName = "MazePaper";
            _currentLine.sortingOrder = 10;

            _currentLine.transform.SetParent(transform, true);
            activeLines.Add(_currentLine);
        }

        /// <summary>
        /// Adds points to the line while the player is drawing.
        /// </summary>
        private void AddPoint(Vector3 worldPosition)
        {
            if (_linePoints.Count == 0 || Vector3.Distance(worldPosition, _linePoints[_linePoints.Count - 1]) > 0.1f)
            {
                if (CheckWallCollision(worldPosition))
                {
                    FinishLine();
                    return;
                }

                _linePoints.Add(worldPosition);
                _currentLine.positionCount = _linePoints.Count;
                _currentLine.SetPositions(_linePoints.ToArray());
            }
        }

        /// Prepares the line to be deleted by marking it as a finished line which did not follow the game rules to solving a maze.
        private void FinishLine()
        {
            if (_currentLine != null)
            {
                StartCoroutine(ClearLineWithDelay(_currentLine));
                _currentLine = null;
                _linePoints.Clear();
                _isDrawing = false;
                _startedFromStart = false;
            }
        }

        /// <summary>
        /// Clears the line after a delay while changing its color to signify line deleting feedback.
        /// </summary>
        private IEnumerator ClearLineWithDelay(LineRenderer line)
        {
            if (line != null)
            {
                line.material.color = incorrectLineColour;

                yield return new WaitForSeconds(clearDelay);

                if (line != null)
                {
                    activeLines.Remove(line);
                    Destroy(line.gameObject);
                }
            }
        }

        #region Bools
        /// <summary>
        /// Checks if drawing's position overlaps with any wall colliders.
        /// </summary>
        private bool CheckWallCollision(Vector3 position)
        {
            Collider[] colliders = Physics.OverlapSphere(position, 0.05f, wallLayerMask);
            return colliders.Length > 0;
        }

        private bool IsStartPoint(RaycastHit hit)
        {
            return hit.collider != null && hit.collider.gameObject == startGameObject;
        }

        private bool IsEndPoint()
        {
            if (!_isDrawing || !_startedFromStart) return false;

            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayerMask))
            {
                return hit.collider != null && hit.collider.gameObject == endGameObject;
            }
            return false;
        }
        #endregion
        #endregion
    }
}
