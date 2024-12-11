using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarText : MonoBehaviour
{

    #region Variables.
    public TMP_InputField inputField;

    private string _enteredText;
    #endregion

    private void Update()
    {
        if (_enteredText == "raC"  || _enteredText == "Rac" && Input.GetKeyUp(KeyCode.Return))
        {
            SceneManager.LoadScene(2);
        }
    }
    public void GetInputText()
    {
        _enteredText = inputField.text;
    }
}
