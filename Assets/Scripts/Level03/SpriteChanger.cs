using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChanger : MonoBehaviour
{
    #region Variables.
    public Sprite clockHeadSprite;
    private SpriteRenderer spriteRenderer;
    #endregion

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.Log("No Sprite renderer on this game object " + gameObject.name);
        }
    }

    void OnMouseDown()
    {
        if (spriteRenderer is not null && spriteRenderer.sprite.name != "ThisWillNeverNotBeTrueLol")
        {
            spriteRenderer.sprite = clockHeadSprite;
        }
    }
}
