using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelOneWinCondition : MonoBehaviour
{
    private void OnMouseDown()
    {
        SceneManager.LoadScene("Level02");
    }
}
