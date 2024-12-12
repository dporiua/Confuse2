using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DoorLockInputFeedback : MonoBehaviour
{
    public TMP_InputField inputField;

    public void WriteLetter(string letter)
    {
        inputField.text += letter;
    }
}
