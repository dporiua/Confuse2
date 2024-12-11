using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Dana
{
    /// <summary>
    /// This script recieves the text written in the input field.
    /// Attach the function into the input field object in the hierarchy.
    /// </summary>

    public class InputFieldContentReciever : MonoBehaviour
    {
        #region Variables.
        public TMP_InputField inputField;

        [SerializeField] private TMP_Text instructionText;

        private string _enteredText;
        #endregion

        private void Update()
        {
            if (_enteredText == "A Maze" || _enteredText == "A maze" || _enteredText == "a maze") 
            {
                instructionText.text = "Rotate the box. Didn't I say the solution IS a maze?";
            }
        }
        public void GetInputText()
        {
            _enteredText = inputField.text;
            Debug.Log("Player typed: " + _enteredText);
            Invoke("ClearEnteredText", 2f);
        }

        /// <summary>
        /// Helps the line drawer script edit the text.
        /// </summary>
        private void ClearEnteredText()
        {
            _enteredText = string.Empty;
        }
    }
}