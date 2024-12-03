using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizensWalking : MonoBehaviour
{
    [SerializeField] AudioClip WalkingSound;
    [SerializeField] AudioClip CarSound;
    [SerializeField] AudioSource audioSource;

    public void PlayCitizenSounds()
    {
        audioSource.clip = WalkingSound;
        audioSource.Play();
    }

    public void StopCitizenSounds()
    {
        audioSource.Stop();
    }

    public void PlayCarSounds()
    {
        audioSource.clip = CarSound;
        audioSource.Play();
    }

    public void StopCarSounds()
    {
        audioSource.Stop();
    }
}
