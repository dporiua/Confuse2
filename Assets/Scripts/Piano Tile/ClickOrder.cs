using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickOrder : MonoBehaviour
{
    private int endOfList;
    public List<int> tilesList = new List<int>(6);
    

// Update is called once per frame
    void Update()
    {
     if (endOfList == 7)
     {
            SceneManager.LoadScene("Level03");
     }
    }
    
    public void AButton()
    {
        if (tilesList.Count == 0)
        {
            Debug.Log("Wrong, start again");
            SceneManager.LoadScene(1);
        }
        if (tilesList.Count == 4)
        {
            tilesList.Add(1);
            endOfList++;
        }
         else
         {
             Debug.Log("Wrong, start again");
             SceneManager.LoadScene(1);
         }
    }
    
    public void BButton()
    {
        if (tilesList.Count == 0)
        {
            tilesList.Add(2);
            endOfList++;
        }
        else
        {
            Debug.Log("Wrong, start again");
            SceneManager.LoadScene(1);
        }
    }
    
    public void CButton()
    {
        if (tilesList.Count == 0)
            Debug.Log("Wrong! start again");
        if (tilesList.Count == 3)
        {
            tilesList.Add(3);
            endOfList++;
        }
        else
        {
            Debug.Log("Wrong! start again");
            SceneManager.LoadScene(1);
        }
      
    }
    
    public void DButton()
    {
        if (tilesList.Count == 0)
            Debug.Log("Wrong! start again");
        if (tilesList.Count == 1)
        {
            tilesList.Add(4);
            endOfList++;
        }
        else
        {
            Debug.Log("Wrong! start again");
            SceneManager.LoadScene(1);
        }
    }
    
    public void EButton()
    {
        if (tilesList.Count == 0)
            Debug.Log("Wrong! start again");
        if (tilesList.Count == 5)
        {
            tilesList.Add(5);
            endOfList++;
        }
        else
        {
            Debug.Log("Wrong! start again");
            SceneManager.LoadScene(1);
        }
    }
    
    public void FButton()
    {
        if (tilesList.Count == 0)
            Debug.Log("Wrong! start again");
        if (tilesList.Count == 2)
        {
            tilesList.Add(6);
            endOfList++;
        }
        else
        {
            Debug.Log("Wrong! start again");
            SceneManager.LoadScene(1);
        }
    }
    
    public void GButton()
    {
        if (tilesList.Count == 0)
            Debug.Log("Wrong! start again");
        if (tilesList.Count == 6)
        {
            tilesList.Add(7);
            endOfList++;
        }
        else
        {
            Debug.Log("Wrong! start again");
            SceneManager.LoadScene(1);
        }
    }
}
