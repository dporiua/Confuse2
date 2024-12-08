using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrafficLightLogic : MonoBehaviour
{
    #region Variables.
    [Header("Sprite Renderers")]
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
    [Tooltip("Place the glowing state of the Green traffic light here")]
    public Sprite greenGlowing;

    /// <summary>
    /// These variables are used to track displayed sprites 
    /// </summary>
    private string _currentGlowingSprite = "";
    private float _firstStopTime = 0f;
    private bool _isPaused = false;
    private Coroutine _spriteFlashCoroutine = null;

    private enum GameState { WaitingForRed, WaitingForGreen }
    private GameState _currentState = GameState.WaitingForRed;

    /// <summary>
    /// Script for Audio Source
    /// </summary>
    [SerializeField] CitizensWalking citizensWalking;

    /// <summary>
    /// Variables for Animator
    /// </summary>
    [SerializeField] private Animator Citizens;
    [SerializeField] private Animator Car;

    private string Citizensmoving = "CitizensMoving";
    private string Citizensreset = "CitizensPositionReset";

    private string Carmoving = "CarsMoving";
    private string Carsgoingback = "CarsGoingBack";
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

            switch (_currentState)
            {
                case GameState.WaitingForRed:
                    if (_currentGlowingSprite == "Red")
                    {
                        _currentState = GameState.WaitingForGreen;
                        _firstStopTime = Time.time;
                        StartCoroutine(PauseAndFlash(4f));
                        Citizens.Play(Citizensmoving, 0, 5.0f);
                        citizensWalking.PlayCitizenSounds();
                        citizensWalking.StopCarSounds();
                    }
                    else
                    {
                        ResetProgress();
                        citizensWalking.StopCitizenSounds();
                    }
                    break;

                case GameState.WaitingForGreen:
                    if (_currentGlowingSprite == "Green")
                    {
                        float timeSinceRed = Time.time - _firstStopTime;
                        if (timeSinceRed >= 3f)
                        {
                            citizensWalking.PlayCarSounds();
                            Car.Play(Carmoving, 0, 0.0f);
                            citizensWalking.StopCitizenSounds();
                            SceneManager.LoadScene("Level05");
                        }
                        else
                        {
                            ResetProgress();
                            Citizens.Play(Citizensreset, 0, 5.0f);
                            citizensWalking.StopCitizenSounds();
                            Car.Play(Carsgoingback, 0, 0.0f);
                        }
                    }
                    else
                    {
                        ResetProgress();
                        citizensWalking.StopCitizenSounds();
                    }
                    break;
            }

        }
    }

    #region Private Functions.
    private void ResetProgress()
    {
        _currentState = GameState.WaitingForRed;
        _firstStopTime = 0f;
    }

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
