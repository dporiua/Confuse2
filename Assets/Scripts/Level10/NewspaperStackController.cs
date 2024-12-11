using UnityEngine;
using UnityEngine.SceneManagement;
public class NewspaperStackController : MonoBehaviour
{
    public Texture[] newspaperTextures; // Array of textures for the newspapers
    public Renderer newspaperRenderer; // Renderer of the top newspaper
    public int winningPageIndex; // Index of the newspaper with the special word
    public GameObject winningWordCollider; // Collider positioned over the winning word
    private int currentIndex = 0; // Tracks the current newspaper index

    void Start()
    {
        // Set the initial newspaper texture
        if (newspaperTextures.Length > 0)
        {
            newspaperRenderer.material.mainTexture = newspaperTextures[currentIndex];
            Debug.Log($"Initial Texture: {newspaperTextures[currentIndex].name}");
        }
        else
        {
            Debug.LogWarning("No textures assigned to the newspaper stack!");
        }

        // Ensure the winning word collider is initially disabled
        if (winningWordCollider != null)
        {
            winningWordCollider.SetActive(false);
        }
    }

    void Update()
    {
        // Detect mouse click
        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            // Check if the click hits the newspaper or the winning word collider
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // Check if the clicked object is the winning word
                if (winningWordCollider != null && hit.transform == winningWordCollider.transform)
                {
                    WinLevel();
                }
                // Check if the clicked object is this newspaper stack
                else if (hit.transform == transform)
                {
                    CycleNewspaper();
                }
            }
        }
    }

    private void CycleNewspaper()
    {
        // Cycle to the next newspaper
        currentIndex = (currentIndex + 1) % newspaperTextures.Length;
        newspaperRenderer.material.mainTexture = newspaperTextures[currentIndex];
        Debug.Log($"Texture Changed: {newspaperTextures[currentIndex].name}");

        // Toggle the winning word collider visibility based on the page
        if (currentIndex == winningPageIndex)
        {
            if (winningWordCollider != null)
            {
                winningWordCollider.SetActive(true);
            }
        }
        else
        {
            if (winningWordCollider != null)
            {
                winningWordCollider.SetActive(false);
            }
        }
    }

    private void WinLevel()
    {
        SceneManager.LoadScene(11);
    }
}
