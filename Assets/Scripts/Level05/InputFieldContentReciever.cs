using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
        #endregion

        public void GetInputText()
        {
            string enteredText = inputField.text;
            Debug.Log("Player typed: " + enteredText);
        }
    }
}