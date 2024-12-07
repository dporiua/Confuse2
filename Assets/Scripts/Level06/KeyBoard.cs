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
    
    [SerializeField] TMP_Text SolverMessage;
    [SerializeField] TMP_Text PlayerCharactersDisplay;

    public void Start()
    {
        //PlayerCharactersDisplay.text = ("Type the Code:" );
        SolverMessage.text = " ";
    }
    public void KeyA()
    {
        PlayerCharacters += "A";
        PlayerCharactersDisplay.text = ("Type the Code:" + PlayerCharacters);
    }

    public void KeyB()
    {
        PlayerCharacters += "B";
        PlayerCharactersDisplay.text = ("Type the Code:" + PlayerCharacters);
    }

    public void KeyC()
    {
        PlayerCharacters += "C";
        PlayerCharactersDisplay.text = ("Type the Code:" + PlayerCharacters);
    }
    public void KeyD()
    {
        PlayerCharacters += "D";
        PlayerCharactersDisplay.text = ("Type the Code:" + PlayerCharacters);
    }

    public void KeyE()
    {
        PlayerCharacters += "E";
        PlayerCharactersDisplay.text = ("Type the Code:" + PlayerCharacters);
    }

    public void KeyF()
    {
        PlayerCharacters += "F";
        PlayerCharactersDisplay.text = ("Type the Code:" + PlayerCharacters);
    }
    public void KeyG()
    {
        PlayerCharacters += "G";
        PlayerCharactersDisplay.text = ("Type the Code:" + PlayerCharacters);
    }
    public void KeyH()
    {
        PlayerCharacters += "H";
        PlayerCharactersDisplay.text = ("Type the Code:" + PlayerCharacters);
    }
    public void KeyI()
    {
        PlayerCharacters += "I";
        PlayerCharactersDisplay.text = ("Type the Code:" + PlayerCharacters);
    }
    public void KeyJ()
    {
        PlayerCharacters += "J";
        PlayerCharactersDisplay.text = ("Type the Code:" + PlayerCharacters);
    }

    public void KeyK()
    {
        PlayerCharacters += "K";
        PlayerCharactersDisplay.text = ("Type the Code:" + PlayerCharacters);
    }

    public void KeyL()
    {
        PlayerCharacters += "L";
        PlayerCharactersDisplay.text = ("Type the Code:" + PlayerCharacters);
    }

    public void KeyM()
    {
        PlayerCharacters += "M";
        PlayerCharactersDisplay.text = ("Type the Code:" + PlayerCharacters);
    }

    public void KeyN()
    {
        PlayerCharacters += "N";
        PlayerCharactersDisplay.text = ("Type the Code:" + PlayerCharacters);
    }

    public void KeyO()
    {
        PlayerCharacters += "O";
        PlayerCharactersDisplay.text = ("Type the Code:" + PlayerCharacters);
    }

    public void KeyP()
    {
        PlayerCharacters += "P";
        PlayerCharactersDisplay.text = ("Type the Code:" + PlayerCharacters);
    }

    public void KeyQ()
    {
        PlayerCharacters += "Q";
        PlayerCharactersDisplay.text = ("Type the Code:" + PlayerCharacters);
    }

    public void KeyR()
    {
        PlayerCharacters += "R";
        PlayerCharactersDisplay.text = ("Type the Code:" + PlayerCharacters);
    }

    public void KeyS()
    {
        PlayerCharacters += "S";
        PlayerCharactersDisplay.text = ("Type the Code:" + PlayerCharacters);
    }

    public void KeyT()
    {
        PlayerCharacters += "T";
        PlayerCharactersDisplay.text = ("Type the Code:" + PlayerCharacters);
    }

    public void KeyU()
    {
        PlayerCharacters += "U";
        PlayerCharactersDisplay.text = ("Type the Code:" + PlayerCharacters);
    }

    public void KeyV()
    {
        PlayerCharacters += "V";
        PlayerCharactersDisplay.text = ("Type the Code:" + PlayerCharacters);
    }
    public void KeyW()
    {
        PlayerCharacters += "W";
        PlayerCharactersDisplay.text = ("Type the Code:" + PlayerCharacters);
    }

    public void KeyX()
    {
        PlayerCharacters += "X";
        PlayerCharactersDisplay.text = ("Type the Code:" + PlayerCharacters);
    }
    public void KeyY()
    {
        PlayerCharacters += "Y";
        PlayerCharactersDisplay.text = ("Type the Code:" + PlayerCharacters);
    }
    public void KeyZ()
    {
        PlayerCharacters += "Z";
        PlayerCharactersDisplay.text = ("Type the Code:" + PlayerCharacters);
    }

    public void KeyEnter()
    {
        if (PlayerCharacters == PuzzleSolver)
        {
            PlayerCharactersDisplay.text = " ";

            //Have to write for the player to know to press escape
            SolverMessage.text = "Escape the Game";
  
        }
        else
        {
            PlayerCharacters = string.Empty;
        }
    }
    
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(7);
        }
    }
}

