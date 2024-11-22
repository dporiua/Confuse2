using UnityEngine;

namespace Dana
{
    /// <summary>
    /// This script is meant to draw a line on a 'paper' layer.
    /// It uses LineRenderer to render line points and delete them if
    /// they reach incorrect bounds/ player does not reach the end.
    /// </summary>

    public class LineDrawer : MonoBehaviour
    {
        #region Variables
        public LineRenderer linePrefab;
        public Material mainLineMaterial;
        public Material warningLineMaterial;
        public Transform startPoint;
        public Transform endPoint;
        public Collider mazeBounds;
        public LayerMask paperLayer;
        public LayerMask mazeWallLayer;

        [Header("Script references")]
        [SerializeField] private LineColourChanger lineColourChanger;

        private LineRenderer _currentLine;
        private Vector3 _lastPosition;
        private bool _isDrawing = false;

        private const float PointsDistance = 0.1f;

        #endregion

        private void Update()
        {
            HandleDrawingInput();
        }

        #region Private Functions
        private void HandleDrawingInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePosition = GetMousePositionOnPaper();
                if (IsLineInsideCollider(startPoint.GetComponent<Collider>(), mousePosition))
                {
                    Debug.Log("Start point clicked. Beginning drawing.");
                    StartDrawing(mousePosition);
                }
                else
                {
                    Debug.Log("Mouse not on start point.");
                }
            }


            if (_isDrawing && Input.GetMouseButton(0))
            {
                Vector3 mousePosition = GetMousePositionOnPaper();

                if (mousePosition != Vector3.zero && IsLinePointValid(mousePosition))
                {
                    if (Vector3.Distance(mousePosition, _lastPosition) > PointsDistance)
                    {
                        AddPoint(mousePosition);
                    }
                }
                else
                {
                    ResetDrawing();
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                Vector3 mousePosition = GetMousePositionOnPaper();

                if (mousePosition != Vector3.zero && IsLineInsideCollider(endPoint.GetComponent<Collider>(), mousePosition))
                {
                    FinishDrawing();
                }
                else
                {
                    ResetDrawing();
                }
            }
        }

        private void StartDrawing(Vector3 startPosition)
        {
            _isDrawing = true;
            _currentLine = Instantiate(linePrefab);
            _currentLine.material = mainLineMaterial;
            _currentLine.positionCount = 1;
            _currentLine.SetPosition(0, startPosition);
            _lastPosition = startPosition;
        }

        private void AddPoint(Vector3 position)
        {
            _currentLine.positionCount++;
            _currentLine.SetPosition(_currentLine.positionCount - 1, position);
            _lastPosition = position;
        }

        private void FinishDrawing()
        {
            _isDrawing = false;
            Debug.Log("'Player won!'"); // will add a constraint to not render a line here unless the player started from start point to finish.
        }

        private void ResetDrawing()
        {
            _isDrawing = false;
            if (_currentLine != null)
            {
                Destroy(_currentLine.gameObject);
            }
            Debug.Log("reseting the drawing.");
        }


        private bool IsLinePointValid(Vector3 point)
        {
            if (Physics.CheckSphere(point, 0.05f, mazeWallLayer))
            {
                Debug.Log("not a valid linePoint");
                return false;
            }
            return mazeBounds.bounds.Contains(point);
        }

        private bool IsLineInsideCollider(Collider collider, Vector3 linePoint)
        {
            return collider.bounds.Contains(linePoint);
        }

        #region Cam related function
        private Vector3 GetMousePositionOnPaper()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, paperLayer))
            {
                return hit.point;
            }

            Debug.Log("Raycast did not hit the paper.");
            return Vector3.zero;
        }

        #endregion
        #endregion
    }
}