using UnityEngine;
using UnityEngine.SceneManagement;

public class ArrowRotator : MonoBehaviour
{
    #region Variables
    [SerializeField] private Material redMaterial;
    [SerializeField] private Material greenMaterial;
    [SerializeField] private Renderer _renderer;

    private bool _isDragging = false;
    private Vector3 _initialMousePos;
    #endregion

    void Start()
    {
        if (_renderer != null && redMaterial != null)
        {
            _renderer.material = redMaterial;
        }
    }

    void OnMouseDown()
    {
        _initialMousePos = Input.mousePosition;
        _isDragging = true;
    }

    void OnMouseDrag()
    {
        if (_isDragging)
        {
            Vector3 currentMousePosition = Input.mousePosition;
            float horizontalRotation = currentMousePosition.x - _initialMousePos.x;

            transform.Rotate(0, horizontalRotation * 0.15f, 0, Space.World);
            _initialMousePos = currentMousePosition;

            UpdateMaterial();
        }
    }

    void OnMouseUp()
    {
        _isDragging = false;

        float yRotation = transform.eulerAngles.y;
        if (yRotation > 170 && yRotation < 190)
        {
            LoadNextScene();
        }
    }

    #region Private Functions.
    private void LoadNextScene()
    {
        SceneManager.LoadScene(7);
    }

    private void UpdateMaterial()
    {
        if (_renderer != null)
        {
            float yRotation = transform.eulerAngles.y;

            if (yRotation > 170 && yRotation < 190)
            {
                _renderer.material = greenMaterial;
            }
            else
            {
                _renderer.material = redMaterial;
            }
        }
    }
    #endregion
}
