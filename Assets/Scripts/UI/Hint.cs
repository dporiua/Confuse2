using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Dana
{
    /// <summary>
    /// Re-usable Hint script.
    /// </summary>

    public class Hint : MonoBehaviour
    {
        #region Variables
        [SerializeField] private GameObject hintPanel;
        [SerializeField] private string hintDescription;
        [SerializeField] private TMP_Text hintTextUI;
        #endregion

        private void Start()
        {
            hintPanel.SetActive(false);
            hintTextUI.text = hintDescription;
        }

        public void HintToggler()
        {
            hintPanel.SetActive(!hintPanel.activeSelf);
        }
    }
}