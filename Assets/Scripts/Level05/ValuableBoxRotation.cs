using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dana
{
    /// <summary>
    /// Rotates the object this script is attached to around using the
    /// left mouse button.
    /// </summary>

    public class ValuableBoxRotation : MonoBehaviour
    {
        #region Variables
        [Tooltip("Change the speed of object rotation by changing the value. The higher the number, the faster the rotation.")]
        [SerializeField] private float rotationSpeed = 30f;

        private bool isDragging = false;
        private Vector3 lastMousePosition;
        #endregion

        private void Update()
        {
            HandleMouseInput();
        }

        #region Private Functions.
        private void HandleMouseInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.collider.gameObject == gameObject) 
                    {
                        isDragging = true;
                        lastMousePosition = Input.mousePosition;
                    }
                }
            }

            if (isDragging && Input.GetMouseButton(0))
            {
                RotateObject();
            }

            if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
            }
        }

        private void RotateObject()
        {
            Vector3 currentMousePosition = Input.mousePosition;
            Vector3 deltaMousePosition = currentMousePosition - lastMousePosition;

            float rotationX = -deltaMousePosition.y * rotationSpeed * Time.deltaTime;
            float rotationY = deltaMousePosition.x * rotationSpeed * Time.deltaTime;

            transform.Rotate(rotationX, rotationY, 0, Space.World); 

            lastMousePosition = currentMousePosition; 
        }
        #endregion
    }
}