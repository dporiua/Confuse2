using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class KeyBoard : MonoBehaviour
{
    [SerializeField] Button A;
    [SerializeField] Button B;
    [SerializeField] Button C;
    [SerializeField] Button D;
    [SerializeField] Button E;
    [SerializeField] Button F;
    [SerializeField] Button G;
    [SerializeField] Button H;
    [SerializeField] Button I;
    [SerializeField] Button J;
    [SerializeField] Button K;
    [SerializeField] Button L;
    [SerializeField] Button M;
    [SerializeField] Button N;
    [SerializeField] Button O;
    [SerializeField] Button P;
    [SerializeField] Button Q;
    [SerializeField] Button R;
    [SerializeField] Button S;
    [SerializeField] Button T;
    [SerializeField] Button U;
    [SerializeField] Button V;
    [SerializeField] Button W;
    [SerializeField] Button X;
    [SerializeField] Button Y;
    [SerializeField] Button Z;
    [SerializeField] Button Escape;
    [SerializeField] Button Enter;

    public string PlayerCharacters;
    public string PuzzleSolver = "ESCAPE";
    
    private int flag = 0;

    [SerializeField] TMP_Text SolverMessage;
    [SerializeField] TMP_Text ComputerMessage;
    [SerializeField] TMP_Text PlayerCharactersDisplay;

    public void Start()
    {
        SolverMessage.text = " ";
    }
    public void KeyA()
    {
        PlayerCharacters += "A";
        PlayerCharactersDisplay.text = PlayerCharacters;
    }

    public void KeyB()
    {
        PlayerCharacters += "B";
        PlayerCharactersDisplay.text = PlayerCharacters;
    }

    public void KeyC()
    {
        PlayerCharacters += "C";
        PlayerCharactersDisplay.text = PlayerCharacters;
    }
    public void KeyD()
    {
        PlayerCharacters += "D";
        PlayerCharactersDisplay.text = PlayerCharacters;
    }

    public void KeyE()
    {
        PlayerCharacters += "E";
        PlayerCharactersDisplay.text = PlayerCharacters;
    }

    public void KeyF()
    {
        PlayerCharacters += "F";
        PlayerCharactersDisplay.text = PlayerCharacters;
    }
    public void KeyG()
    {
        PlayerCharacters += "G";
        PlayerCharactersDisplay.text = PlayerCharacters;
    }
    public void KeyH()
    {
        PlayerCharacters += "H";
        PlayerCharactersDisplay.text = PlayerCharacters;
    }
    public void KeyI()
    {
        PlayerCharacters += "I";
        PlayerCharactersDisplay.text = PlayerCharacters;
    }
    public void KeyJ()
    {
        PlayerCharacters += "J";
        PlayerCharactersDisplay.text = PlayerCharacters;
    }

    public void KeyK()
    {
        PlayerCharacters += "K";
        PlayerCharactersDisplay.text = PlayerCharacters;
    }

    public void KeyL()
    {
        PlayerCharacters += "L";
        PlayerCharactersDisplay.text = PlayerCharacters;
    }

    public void KeyM()
    {
        PlayerCharacters += "M";
        PlayerCharactersDisplay.text = PlayerCharacters;
    }

    public void KeyN()
    {
        PlayerCharacters += "N";
        PlayerCharactersDisplay.text = PlayerCharacters;
    }

    public void KeyO()
    {
        PlayerCharacters += "O";
        PlayerCharactersDisplay.text = PlayerCharacters;
    }

    public void KeyP()
    {
        PlayerCharacters += "P";
        PlayerCharactersDisplay.text = PlayerCharacters;
    }

    public void KeyQ()
    {
        PlayerCharacters += "Q";
        PlayerCharactersDisplay.text = PlayerCharacters;
    }

    public void KeyR()
    {
        PlayerCharacters += "R";
        PlayerCharactersDisplay.text = PlayerCharacters;
    }

    public void KeyS()
    {
        PlayerCharacters += "S";
        PlayerCharactersDisplay.text = PlayerCharacters;
    }

    public void KeyT()
    {
        PlayerCharacters += "T";
        PlayerCharactersDisplay.text = PlayerCharacters;
    }

    public void KeyU()
    {
        PlayerCharacters += "U";
        PlayerCharactersDisplay.text = PlayerCharacters;
    }

    public void KeyV()
    {
        PlayerCharacters += "V";
        PlayerCharactersDisplay.text = PlayerCharacters;
    }
    public void KeyW()
    {
        PlayerCharacters += "W";
        PlayerCharactersDisplay.text = PlayerCharacters;
    }

    public void KeyX()
    {
        PlayerCharacters += "X";
        PlayerCharactersDisplay.text = PlayerCharacters;
    }
    public void KeyY()
    {
        PlayerCharacters += "Y";
        PlayerCharactersDisplay.text = PlayerCharacters;
    }
    public void KeyZ()
    {
        PlayerCharacters += "Z";
        PlayerCharactersDisplay.text = PlayerCharacters;
    }

    public void KeyEnter()
    {
        if (PlayerCharacters == PuzzleSolver)
        {
            flag = 1;
            ComputerMessage.text = " ";

            //Have to write for the player to know to press escape
            SolverMessage.text = "Click the Escape Button";
  
        }
        else
        {
            PlayerCharacters = string.Empty;
        }
    }
    
    public void KeyEscape()
    {
        if (flag == 1)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene(7);
            }
        }
    }
}

