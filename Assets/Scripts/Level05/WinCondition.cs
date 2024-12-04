using UnityEngine;
using UnityEngine.SceneManagement;

namespace Dana
{
    /// <summary>
    /// Win condition is declared here. Currently
    /// the script simply loads the next scene.
    /// </summary>

    public class WinCondition : MonoBehaviour
    {
        public void OnMouseClick()
        { 
            SceneManager.LoadScene("Level06");
        }
    }
}