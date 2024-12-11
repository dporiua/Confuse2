using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEightWinCondition : MonoBehaviour
{
    private void OnMouseDown()
    {
        SceneManager.LoadScene(9);
    }
}
