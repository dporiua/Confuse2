using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrafficLightLogic : MonoBehaviour
{
    #region Variables.
    [Header("Sprite renderers.")]
    [Tooltip("Drag the Red traffic sprite object in scene into this space.")]
    public SpriteRenderer redSprite;
    [Tooltip("Drag the Orange traffic sprite object in scene into this space.")]
    public SpriteRenderer orangeSprite;
    [Tooltip("Drag the Green traffic sprite object in scene into this space.")]
    public SpriteRenderer greenSprite;

    [Header("Sprites")]
    [Tooltip("Place the regular state of the Red traffic light here")]
    public Sprite redNormal;
    [Tooltip("Place the glowing state of the Red traffic light here")]
    public Sprite redGlowing;
    [Tooltip("Place the regular state of the Orange traffic light here")]
    public Sprite orangeNormal;
    [Tooltip("Place the glowing state of the Orange traffic light here")]
    public Sprite orangeGlowing;
    [Tooltip("Place the regular state of the Green traffic light here")]
    public Sprite greenNormal;
    [Tooltip("Place the glowing state of the Red traffic light here")]
    public Sprite greenGlowing;

    /// <summary>
    /// These variables are used to track displayed sprites 
    /// </summary>
    private string _currentGlowingSprite = "";
    private string _lastStoppedSprite = "";
    private float _firstStopTime = 0f;
    private bool _isPaused = false;
    private Coroutine _spriteFlashCoroutine = null;
    #endregion

    private void Start()
    {
        StartCoroutine(GlowCycle());
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !_isPaused)
        {
            Debug.Log("Stopped on: " + _currentGlowingSprite);

            if (_currentGlowingSprite == "Red")
            {
                _lastStoppedSprite = "Red";
                _firstStopTime = Time.time;
            }
            else if (_currentGlowingSprite == "Green" && _lastStoppedSprite == "Red")
            {
                if (Time.time - _firstStopTime >= 6f)
                {
                    SceneManager.LoadScene("Level05");
                }
            }

            StartCoroutine(PauseAndFlash(4f));
        }
    }

    #region Private Functions.
    
    private void SetGlow(SpriteRenderer sprite, Sprite normal, Sprite glowing, string spriteName)
    {
        redSprite.sprite = redNormal;
        orangeSprite.sprite = orangeNormal;
        greenSprite.sprite = greenNormal;

        sprite.sprite = glowing;

        _currentGlowingSprite = spriteName;

    }

    #region IEnumerators
    private IEnumerator GlowCycle()
    {
        while (true)
        {
            SetGlow(redSprite, redNormal, redGlowing, "Red");
            yield return WaitForUnpaused(1f);

            SetGlow(orangeSprite, orangeNormal, orangeGlowing, "Orange");
            yield return WaitForUnpaused(1f);

            SetGlow(greenSprite, greenNormal, greenGlowing, "Green");
            yield return WaitForUnpaused(1f);
        }
    }

    private IEnumerator PauseAndFlash(float duration)
    {
        _isPaused = true;

        if (_spriteFlashCoroutine != null)
            StopCoroutine(_spriteFlashCoroutine);
        _spriteFlashCoroutine = StartCoroutine(FlashCurrentSprite());

        yield return new WaitForSeconds(duration);

        if (_spriteFlashCoroutine != null)
            StopCoroutine(_spriteFlashCoroutine);
        _spriteFlashCoroutine = null;

        _isPaused = false;
    }

    private IEnumerator FlashCurrentSprite()
    {
        SpriteRenderer targetSprite = null;
        Sprite normalSprite = null;
        Sprite glowingSprite = null;

        if (_currentGlowingSprite == "Red")
        {
            targetSprite = redSprite;
            normalSprite = redNormal;
            glowingSprite = redGlowing;
        }
        else if (_currentGlowingSprite == "Orange")
        {
            targetSprite = orangeSprite;
            normalSprite = orangeNormal;
            glowingSprite = orangeGlowing;
        }
        else if (_currentGlowingSprite == "Green")
        {
            targetSprite = greenSprite;
            normalSprite = greenNormal;
            glowingSprite = greenGlowing;
        }

        while (_isPaused)
        {
            if (targetSprite != null)
            {
                targetSprite.sprite = normalSprite;
                yield return new WaitForSeconds(0.25f);

                targetSprite.sprite = glowingSprite;
                yield return new WaitForSeconds(0.25f);
            }
        }
    }

    private IEnumerator WaitForUnpaused(float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            if (!_isPaused)
                elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
    #endregion
    #endregion
}
