using System.Collections;
using UnityEditor;
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
    [Tooltip("Place the error sprite in this slot")]
    public Sprite errorSprite;

    [Tooltip("Place the win panel in this slot")]
    [SerializeField] private GameObject winPanel;

    /// <summary>
    /// These variables are used to track displayed sprites 
    /// </summary>
    private string _currentGlowingSprite = "";
    private float _firstStopTime = 0f;
    private bool _isPaused = false;
    private Coroutine _spriteFlashCoroutine = null;
    private Coroutine _errorFlashCoroutine = null;

    private enum GameState { WaitingForRed, WaitingForGreen, Error }
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

    private string isPlayerMoving = "IsPlayerMoving";
    private string isPlayerGoingBack = "IsPlayerGoingBack";

    private string isCarMoving = "IsCarMoving";

    #endregion

    private void Start()
    {
        StartCoroutine(GlowCycle());
        winPanel.SetActive(false);
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
                        StartCoroutine(PauseAndFlash(2f));

                        Citizens.SetBool(isPlayerMoving, true);
                        Citizens.SetBool(isPlayerGoingBack, false);
                        citizensWalking.PlayCitizenSounds();
                        citizensWalking.StopCarSounds();
                    }
                    else
                    {
                        TriggerErrorState();
                    }
                    break;

                case GameState.WaitingForGreen:
                    if (_currentGlowingSprite == "Green" && (Time.time - _firstStopTime) >= 4.5f)
                    {
                        Car.SetBool(isCarMoving, true);

                        citizensWalking.PlayCarSounds();
                        citizensWalking.StopCitizenSounds();
                        StartCoroutine(PauseAndFlash(2f));

                        StartCoroutine(LoadWinPanelAfterCarAnimation(3f));
                    }
                    else if (_currentGlowingSprite == "Green" || _currentGlowingSprite == "Orange")
                    {
                        TriggerErrorState();
                    }
                    else
                    {
                        ResetProgress();
                        Citizens.SetBool(isPlayerGoingBack, true);
                        Citizens.SetBool(isPlayerMoving, false);
                        citizensWalking.StopCitizenSounds();
                    }
                    break;
            }
        }
    }

    private void TriggerErrorState()
    {
        if (_spriteFlashCoroutine != null)
            StopCoroutine(_spriteFlashCoroutine);
        _spriteFlashCoroutine = null;

        if (_errorFlashCoroutine != null)
            StopCoroutine(_errorFlashCoroutine);

        Citizens.SetBool(isPlayerGoingBack, true);
        Citizens.SetBool(isPlayerMoving, false);
        citizensWalking.StopCitizenSounds();

        _currentState = GameState.Error;
        _isPaused = true;

        _errorFlashCoroutine = StartCoroutine(ErrorFlashSequence(3f));
    }

    #region Public Functions
    public void GoToMainmenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
        // EditorApplication.isPaused = true;
    }
    #endregion

    #region Private Functions
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
    #endregion

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

    private IEnumerator ErrorFlashSequence(float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            redSprite.sprite = errorSprite;
            orangeSprite.sprite = errorSprite;
            greenSprite.sprite = errorSprite;
            yield return new WaitForSeconds(0.25f);

            redSprite.sprite = redNormal;
            orangeSprite.sprite = orangeNormal;
            greenSprite.sprite = greenNormal;
            yield return new WaitForSeconds(0.25f);

            elapsedTime += 0.5f;
        }

        ResetProgress();
        _currentState = GameState.WaitingForRed;
        _isPaused = false;
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
    private IEnumerator LoadWinPanelAfterCarAnimation(float delay)
    {
        yield return new WaitForSeconds(delay);
        winPanel.SetActive(true);
    }
    #endregion
}
