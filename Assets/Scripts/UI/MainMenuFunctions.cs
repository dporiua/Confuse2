using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuFunctions : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameObject playerButton;
    #endregion

    public void PlayButton()
    {
        SceneManager.LoadScene("Level01");
    }

    public void ShowPlayButton()
    {
        playerButton.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
