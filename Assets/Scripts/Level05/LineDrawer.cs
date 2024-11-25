using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Dana
{
    /// <summary>
    /// This script is meant to draw a line if the texture pixel is not black.
    /// It uses LineRenderer to render line points and delete them if
    /// they reach touch mazeWallColour which represents the colour of the maze's wall.
    /// </summary>

    public class LineDrawer : MonoBehaviour
    {
        #region Variables
        [SerializeField] private Texture2D mazeTexture;
        [SerializeField] private Color mazeWallColour = Color.black;
        [SerializeField] private Color mazeGroundColour = Color.white;
        [SerializeField] private GameObject lineRendererPrefab;
        [SerializeField] private Camera mainCamera;

        private LineRenderer _currentLine;
        private List<Vector3> _linePoints = new List<Vector3>();
        private bool _isDrawing = false;

        [Header("Script references")]
        [SerializeField] private LineColourChanger lineColourChanger;

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
                _isDrawing = false;
                // FinishTheDrawing();
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
            Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;

            Vector2 textureCoord = WorldToTextureCoordination(mousePosition);

            if (_linePoints.Count == 0 || Vector3.Distance(mousePosition, _linePoints[_linePoints.Count - 1]) > 0.1f)
            {
                _linePoints.Add(mousePosition);
                _currentLine.positionCount = _linePoints.Count;
                _currentLine.SetPositions(_linePoints.ToArray());

                if (IsLineCollidingWithMazeWall(mousePosition))
                {
                    ClearCurrentLine();
                }
            }
        }



        /// <summary>
        /// Reads the texture's pixels to determine whether the line hit the maze wall or not.
        /// </summary>
        private bool ColorsMatch(Color a, Color b, float tolerance = 0.01f)
        {
            return Mathf.Abs(a.r - b.r) < tolerance &&
                   Mathf.Abs(a.g - b.g) < tolerance &&
                   Mathf.Abs(a.b - b.b) < tolerance;
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


        private void FinishTheDrawing()
        {

        }

        private Vector2 WorldToTextureCoordination(Vector3 worldPosition)
        {
            Vector3 spritePosition = transform.position;
            Vector2 localPosition = new Vector2((worldPosition.x - spritePosition.x) / transform.localScale.x + 0.5f, (worldPosition.y - spritePosition.y) / transform.localScale.y + 0.5f);
            return new Vector2(localPosition.x * mazeTexture.width, localPosition.y * mazeTexture.height);
        }

        private void ClearCurrentLine()
        {
            if (_currentLine != null)
            {
                Destroy(_currentLine.gameObject);
            }
            _linePoints.Clear();
            _isDrawing = false;
        }
        #endregion
    }
}