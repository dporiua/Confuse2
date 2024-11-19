using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text timer;
    [SerializeField] private GameObject endGamePanel;

    private float countdown;
    private float numberDown = 20;
    private const float _initialCountdown = 500;

    // Start is called before the first frame update
    void Start()
    {
        endGamePanel.SetActive(false);
        countdown = _initialCountdown;
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= numberDown * Time.deltaTime;

        if (countdown < 10 && countdown > 6)
        {
            countdown = _initialCountdown;
        }

        int _minutes = Mathf.FloorToInt(countdown / 60);
        int _seconds = Mathf.FloorToInt(countdown % 60);
        timer.text = string.Format("{0:00}:{1:00}", _minutes, _seconds);

        if (countdown <= 0)
        {
            endGamePanel.SetActive(true);
        }
    }

    public void ReduceTime()
    {
        countdown -= 100;
        if (countdown < 0) countdown = 0;
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}