using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dana
{
    public class LineColourChanger : MonoBehaviour
    {
        #region Enums.
        #region Default Line Material
        public enum DefaultLineMaterial
        {
            Black,
            Blue,
            Orange,
            Purple
        }
        #endregion

        #region ErrorLineMaterial
        public enum ErrorLineMaterial
        {
            Red,
            Green
        }
        #endregion
        #endregion

        #region Variables
        [Header("Materials")]
        public List<Material> defaultMaterials = new List<Material>(4);
        public List<Material> errorMaterials = new List<Material>(2);
        public LineRenderer lineRenderer;

        [Header("Material Selector")]
        public DefaultLineMaterial selectedDefaultMaterial;
        public ErrorLineMaterial selectedErrorMaterial;

        private bool isInErrorState = false; 
        #endregion

        void Start()
        {
            if (lineRenderer == null)
            {
                lineRenderer = GetComponent<LineRenderer>();
            }

            UpdateMaterial();
        }

        void Update()
        {
            // UpdateMaterial(); was calling it here just to try it out!! 
        }


        #region Public Functions
        /// <summary>
        /// Sets the state to error mode, this will be used later in the line renderer script.
        /// </summary>
        public void SetErrorState(bool isInError)
        {
            isInErrorState = isInError;
            UpdateMaterial();
        }
        #endregion

        #region Private Functions
        private void UpdateMaterial()
        {
            if (lineRenderer == null) return;

            if (isInErrorState)
            {
                ApplyMaterial(errorMaterials, (int)selectedErrorMaterial);
            }
            else if (!isInErrorState)
            {
                ApplyMaterial(defaultMaterials, (int)selectedDefaultMaterial);
            }
            else
            {
                Debug.LogWarning("Is in neither states");
            }
        }

        private void ApplyMaterial(List<Material> materialsList, int index)
        {
            if (index < materialsList.Count)
            {
                lineRenderer.material = materialsList[index];
            }
            else
            {
                Debug.LogWarning("Material isn't in list range");
            }
        }

        #endregion
    }
}
