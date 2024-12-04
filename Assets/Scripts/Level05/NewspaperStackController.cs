using UnityEngine;

public class NewspaperStackController : MonoBehaviour
{
    public Texture[] newspaperTextures; // Newspaper texture array
    public Renderer newspaperRenderer;
    private int currentIndex = 0; // Tracks the current newspaper index

    void Start()
    {
        // Set the initial newspaper texture
        if (newspaperTextures.Length > 0)
        {
            newspaperRenderer.material.mainTexture = newspaperTextures[currentIndex];
        }
    }

    void Update()
    {
        // Detect mouse click
        if (Input.GetMouseButtonDown(0)) 
        {
            // Check if the click hits the newspaper
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // Check if the clicked object is this newspaper stack
                if (hit.transform == transform)
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
    }
}
