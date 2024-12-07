using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Dana.Leon
{
    /// <summary>
    /// Responsible for end game logic.
    /// </summary>

    public class LevelSixWinConditionAndPanel : MonoBehaviour
    {
        #region Variables
        [SerializeField] GameObject endGameCanvas;
        #endregion

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                endGameCanvas.SetActive(true);
            }
        }

        public void GoToMainMenu()
        {
            SceneManager.LoadScene(0);
        }

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}