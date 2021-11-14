using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicGame : MonoBehaviour
{
    // Reference to Audio Source component
    private AudioSource audioSrc;

    // Music volume variable that will be modified
    // by dragging slider knob

    private float musicVolume = MusicControler.instance.musicVolume;


    // Use this for initialization
    void Start()
    {

        // Assign Audio Source component to control it
        audioSrc = GetComponent<AudioSource>();
        musicVolume = MusicControler.instance.musicVolume;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(musicVolume);
        // Setting volume option of Audio Source to be equal to musicVolume
        audioSrc.volume = musicVolume;
    }

    
}