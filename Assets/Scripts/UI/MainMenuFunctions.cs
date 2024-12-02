using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuFunctions : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameObject playerButton;
    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private GameObject mainMenuPanel;
    #endregion

    public void PlayButton()
    {
        SceneManager.LoadScene("Level01");
    }

    public void ShowPlayButton()
    {
        playerButton.SetActive(true);
    }

    public void ToggleCreditsPanel()
    {
        creditsPanel.SetActive(!creditsPanel.activeSelf);
        mainMenuPanel.SetActive(!mainMenuPanel.activeSelf);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
