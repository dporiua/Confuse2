using System.Collections.Generic;
using UnityEngine;

namespace Dana
{
    /// <summary>
    /// This script is meant to draw a line based on colliders.
    /// It uses LineRenderer to render line points and deletes the line
    /// if it collides with a maze wall or the player releases the left mouse button.
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

        private LineRenderer _currentLine;
        private List<Vector3> _linePoints = new List<Vector3>();
        private bool _isDrawing = false;

        #endregion

        private void Update()
        {
            HandleDrawingInput();
        }

        #region Private Functions
        /// <summary>
        /// Handles user input for starting, continuing, and ending line drawing.
        /// </summary>
        private void HandleDrawingInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartDrawing();
            }

            if (_isDrawing && Input.GetMouseButton(0))
            {
                AddPoint();
            }

            if (Input.GetMouseButtonUp(0))
            {
                ClearCurrentLine();
            }
        }

        /// <summary>
        /// Starts a new line when the player begins drawing.
        /// </summary>
        private void StartDrawing()
        {
            _isDrawing = true;

            GameObject line = Instantiate(lineRendererPrefab, transform.position, transform.rotation);
            _currentLine = line.GetComponent<LineRenderer>();
            _linePoints.Clear();

            _currentLine.sortingLayerName = "MazePaper";
            _currentLine.sortingOrder = 10;

            _currentLine.transform.SetParent(transform, true);
        }

        /// <summary>
        /// Adds points to the line while the player is drawing.
        /// </summary>
        private void AddPoint()
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            {
                if (IsGround(hit.collider))
                {
                    Vector3 worldPosition = hit.point;

                    if (_linePoints.Count == 0 || Vector3.Distance(worldPosition, _linePoints[_linePoints.Count - 1]) > 0.1f)
                    {
                        if (CheckWallCollision(worldPosition))
                        {
                            ClearCurrentLine();
                            return;
                        }

                        _linePoints.Add(worldPosition);
                        _currentLine.positionCount = _linePoints.Count;
                        _currentLine.SetPositions(_linePoints.ToArray());
                    }
                }
                else if (IsWall(hit.collider))
                {
                    ClearCurrentLine();
                }
            }
        }

        /// <summary>
        /// Clears current line when leaving mouse button or hitting a wall.
        /// </summary>
        private void ClearCurrentLine()
        {
            if (_currentLine != null)
            {
                Destroy(_currentLine.gameObject);
            }
            _linePoints.Clear();
            _isDrawing = false;
        }

        #region Bools.
        /// <summary>
        /// Checks if drawing's postion overlaps with any wall colliders.
        /// </summary>
        private bool CheckWallCollision(Vector3 position)
        {
            Collider[] colliders = Physics.OverlapSphere(position, 0.05f, wallLayerMask);
            return colliders.Length > 0;
        }

        /// <summary>
        /// Checks if a collider belongs to the ground layer.
        /// </summary>
        private bool IsGround(Collider collider)
        {
            return collider.gameObject.layer == LayerMask.NameToLayer("MazeGround");
        }

        /// <summary>
        /// Checks if a collider belongs to the wall layer.
        /// </summary>
        private bool IsWall(Collider collider)
        {
            return collider.gameObject.layer == LayerMask.NameToLayer("MazeWall");
        }
        #endregion
        #endregion
    }
}