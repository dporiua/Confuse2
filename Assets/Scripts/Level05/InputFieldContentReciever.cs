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

        [SerializeField] private TMP_Text gameDesc;

        private string _enteredText;
        #endregion

        private void Update()
        {
            if (_enteredText == "A Maze" || _enteredText == "A maze") 
            {
                gameDesc.text = "No silly, Read your news!";
            }
        }
        public void GetInputText()
        {
            _enteredText = inputField.text;
            Debug.Log("Player typed: " + _enteredText);
        }
    }
}