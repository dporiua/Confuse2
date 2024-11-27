using System.Collections.Generic;
using UnityEngine;

namespace Dana
{
    /// <summary>
    /// This script is meant to draw a line if the texture pixel is not black.
    /// It uses LineRenderer to render line points and delete them if
    /// they reach the mazeWallColour or the player releases the left mouse button.
    /// </summary>

    public class LineDrawer : MonoBehaviour
    {
        #region Variables
        [SerializeField] private Texture2D mazeTexture;
        [SerializeField] private Color mazeWallColour;
        [SerializeField] private Color mazeGroundColour;
        [SerializeField] private GameObject lineRendererPrefab;
        [SerializeField] private Camera mainCamera;

        private LineRenderer _currentLine;
        private List<Vector3> _linePoints = new List<Vector3>();
        private bool _isDrawing = false;

        [Header("Script references")]
        [SerializeField] private LineColourChanger lineColourChanger;

        [Header("Raycast Settings")]
        [SerializeField] private LayerMask mazeLayerMask;

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
        /// Tells the game that the player started drawing. Instantiates line prefab 
        /// several times to form a line.
        /// </summary>
        private void StartDrawing()
        {
            _isDrawing = true;

            GameObject line = Instantiate(lineRendererPrefab, Vector3.zero, Quaternion.identity);
            _currentLine = line.GetComponent<LineRenderer>();
            _linePoints.Clear();

            _currentLine.sortingLayerName = "MazePaper";
            _currentLine.sortingOrder = 10;
        }

        /// <summary>
        /// Adds line points epending on where the mouse position and saves their positions. 
        /// Positions get cleared when line hits a wall.
        /// </summary>
        private void AddPoint()
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, mazeLayerMask))
            {
                if (hit.collider != null && hit.collider.gameObject == gameObject)
                {
                    Vector3 worldPosition = hit.point;
                    Vector2 textureCoord = WorldToTextureCoordination(worldPosition);

                    if (!IsPointOnMazeGround(textureCoord))
                    {
                        ClearCurrentLine();
                        return;
                    }

                    if (_linePoints.Count == 0 || Vector3.Distance(worldPosition, _linePoints[_linePoints.Count - 1]) > 0.1f)
                    {
                        if (CheckIntermediateCollisions(worldPosition))
                        {
                            ClearCurrentLine();
                            return;
                        }

                        _linePoints.Add(worldPosition);
                        _currentLine.positionCount = _linePoints.Count;
                        _currentLine.SetPositions(_linePoints.ToArray());
                    }
                }
            }
        }

        private bool CheckIntermediateCollisions(Vector3 newPoint)
        {
            if (_linePoints.Count == 0) return false;

            Vector3 lastPoint = _linePoints[_linePoints.Count - 1];
            int steps = 10;
            for (int i = 1; i <= steps; i++)
            {
                Vector3 intermediatePoint = Vector3.Lerp(lastPoint, newPoint, i / (float)steps);
                Vector2 textureCoord = WorldToTextureCoordination(intermediatePoint);

                if (!IsPointOnMazeGround(textureCoord) || IsLineCollidingWithMazeWall(intermediatePoint))
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsPointOnMazeGround(Vector2 textureCoord)
        {
            if (textureCoord.x >= 0 && textureCoord.y >= 0 && textureCoord.x < mazeTexture.width && textureCoord.y < mazeTexture.height)
            {
                Color pixelColour = mazeTexture.GetPixel((int)textureCoord.x, (int)textureCoord.y);
                return ColorsMatch(pixelColour, mazeGroundColour);
            }
            return false;
        }


        private bool IsLineCollidingWithMazeWall(Vector3 position)
        {
            Vector2 pixelPosition = WorldToTextureCoordination(position);
            if (pixelPosition.x >= 0 && pixelPosition.y >= 0 && pixelPosition.x < mazeTexture.width && pixelPosition.y < mazeTexture.height)
            {
                Color pixelColour = mazeTexture.GetPixel((int)pixelPosition.x, (int)pixelPosition.y);
                return ColorsMatch(pixelColour, mazeWallColour);
            }
            return false;
        }

        private bool ColorsMatch(Color a, Color b, float tolerance = 0.01f)
        {
            return Mathf.Abs(a.r - b.r) < tolerance &&
                   Mathf.Abs(a.g - b.g) < tolerance &&
                   Mathf.Abs(a.b - b.b) < tolerance;
        }

        private Vector2 WorldToTextureCoordination(Vector3 worldPosition)
        {
            worldPosition = transform.InverseTransformPoint(worldPosition);
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            Bounds bounds = spriteRenderer.bounds;
            var min = bounds.min;
            float xNormalized = (worldPosition.x - bounds.min.x) / bounds.size.x;
            float yNormalized = (worldPosition.y - bounds.min.y) / bounds.size.y;

            xNormalized = Mathf.Clamp01(xNormalized);
            yNormalized = Mathf.Clamp01(yNormalized);

            return new Vector2(xNormalized * mazeTexture.width, yNormalized * mazeTexture.height);
        }

        private void ClearCurrentLine()
        {
            if (_currentLine != null)
            {
                //Destroy(_currentLine.gameObject);
            }
            _linePoints.Clear();
            _isDrawing = false;
        }
        #endregion
    }
}
