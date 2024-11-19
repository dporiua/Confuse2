using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PianoTile : MonoBehaviour
{
    public GameObject[] pianoTiles;

    // private string tiles;

    [SerializeField] private Material clicked;

    [SerializeField] private AudioClip pianoClip;

    [SerializeField] private GameObject clipLocation;
    // Start is called before the first frame update
    void Start()
    {
        FindTiles();
        StartCoroutine(DelayAction(3f));
    }

    IEnumerator DelayAction(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        pianoTiles[1].GetComponent<Renderer>().materials[1] = clicked;
        AudioSource.PlayClipAtPoint(pianoClip, clipLocation.transform.position, 0.7f);
        // tiles = "B";
        yield return new WaitForSeconds(delayTime);
        pianoTiles[3].GetComponent<Renderer>().materials[1] = clicked;
        AudioSource.PlayClipAtPoint(pianoClip, clipLocation.transform.position, 0.7f);
        // tiles = "D";
        yield return new WaitForSeconds(delayTime);
        pianoTiles[5].GetComponent<Renderer>().materials[1] = clicked;
        AudioSource.PlayClipAtPoint(pianoClip, clipLocation.transform.position, 0.7f);
        // tiles = "F";
        yield return new WaitForSeconds(delayTime);
        pianoTiles[2].GetComponent<Renderer>().materials[1] = clicked;
        AudioSource.PlayClipAtPoint(pianoClip, clipLocation.transform.position, 0.7f);
        // tiles = "C";
        yield return new WaitForSeconds(delayTime);
        pianoTiles[0].GetComponent<Renderer>().materials[1] = clicked;
        AudioSource.PlayClipAtPoint(pianoClip, clipLocation.transform.position, 0.7f);
        // tiles = "A";
        yield return new WaitForSeconds(delayTime);
        pianoTiles[4].GetComponent<Renderer>().materials[1] = clicked;
        AudioSource.PlayClipAtPoint(pianoClip, clipLocation.transform.position, 0.7f);
        // tiles = "E";
        yield return new WaitForSeconds(delayTime);
        pianoTiles[6].GetComponent<Renderer>().materials[1] = clicked;
        AudioSource.PlayClipAtPoint(pianoClip, clipLocation.transform.position, 0.7f);
        // tiles = "G";
    }

    void FindTiles()
    {
        if (pianoTiles[0] = GameObject.Find("A tile"))
            Debug.Log("Found A tile");
        if (pianoTiles[1] = GameObject.Find("B tile"))
            Debug.Log("Found B tile");
        if (pianoTiles[2] = GameObject.Find("C tile"))
            Debug.Log("Found C tile");
        if (pianoTiles[3] = GameObject.Find("D tile"))
            Debug.Log("Found D tile");
        if (pianoTiles[4] = GameObject.Find("E tile"))
            Debug.Log("Found E tile");
        if (pianoTiles[5] = GameObject.Find("F tile"))
            Debug.Log("Found F tile");
        if (pianoTiles[6] = GameObject.Find("G tile"))
            Debug.Log("Found G tile");
    }

}
