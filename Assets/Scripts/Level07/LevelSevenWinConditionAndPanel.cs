using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Dana.Leon
{
    /// <summary>
    /// Responsible for end game logic.
    /// </summary>

    public class LevelSevenWinConditionAndPanel : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene(8);
            }
        }
    }
}