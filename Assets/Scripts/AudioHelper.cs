using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioHelper 
{
    public static AudioSource PlayClip2D(AudioClip clip, float volume)
    {
        // create the object
        GameObject audioObject = new GameObject("Audio2D");
        AudioSource audioSource = audioObject.AddComponent<AudioSource>();

        //configure the sound object
        audioSource.clip = clip;
        audioSource.volume = volume;

        //activate
        audioSource.Play();
        Object.Destroy(audioObject, clip.length);

        //return just in case
        return audioSource;
    }
}
