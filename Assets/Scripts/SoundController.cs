using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioClip pickupSound;
    public AudioClip gameoverSound;
    public AudioClip negativepickupSound;
    public AudioClip themesongSound;

    AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayPickupSound()
    {
        PlaySound(pickupSound);
    }

    public void PlayGameOverSound()
    {
        PlaySound(gameoverSound);
    }

    public void PlayNegativePickupSound()
    {
        PlaySound(negativepickupSound);
    }

    public void PlayThemeSongSound()
    {
        PlaySound(themesongSound);
    }

    void PlaySound(AudioClip _newSound)
    {
        //set the audiosources audioclip to be the passed in sound
        audioSource.clip = _newSound;
        //Play the audiosource
        audioSource.Play();
    }

    public void PlayCollisionSound(GameObject _go)
    {
        //check to see if the collided object has an Audiosource. 
        //This is a failsafe in cas we forgot to attach one to our wall
        if (_go.GetComponent<AudioSource>() != null)
        {
            //play the audio on the wall object
            _go.GetComponent<AudioSource>().Play();
        }

    }
}

