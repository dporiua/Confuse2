using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupsOrderOfLayerChanger : MonoBehaviour
{
    #region Variables.
    private SpriteRenderer spriteRenderer;
    #endregion

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogError(gameObject.name + " does not have a SpriteRenderer");
        }
    }

    void Update()
    {
        float currentYPosition = transform.position.y;

        if (currentYPosition > 1)
        {
            spriteRenderer.sortingOrder = 4;
        }
        else
        {
            spriteRenderer.sortingOrder = 6;
        }
    }
}
