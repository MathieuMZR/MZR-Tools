using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

public class AudioManagerTool : MonoBehaviour
{
    //AudioCue Prefab (Simple GameObject with AudioSource Component on.
    public GameObject audioCue;

    public enum SfxType
    {
        Type1,
        Type2,
        Type3,
        //...
    }

    //Optional.
    public AudioMixer audioMixer;

    public AudioClip[] type1, type2, type3;

    public static AudioManagerTool Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    /// <summary>
    /// Basic function to spawn a sound everywhere and anytime.
    /// </summary>
    /// <param name="type">Type of sound.</param>
    /// <param name="randomPitch">Set random pitch or not.</param>
    /// <param name="pitch">Set the base pitch. If random, set the pitch between 1-x and 1+x.</param>
    /// <param name="index">Get the sound at index.</param>
    /// <param name="volume">Set the sound volume.</param>
    /// <param name="isLooping">Set the sound looping.</param>
    /// <param name="stereoPan">Set the sound to left or right audio output.</param>
    /// /// <param name="delayBeforePlay">Set the delay before the sound plays.</param>
    /// <param name="spatialBlend">Set the sound in 2D or 3D space, </param>
    /// <param name="min3DDistance">Set the minimum sound distance to be heard.</param>
    /// <param name="max3DDistance">Set the maximum sound distance to be heard.</param>
    /// <param name="audioMixerGroup">Set the sound's audio mixer group.</param>
    public void SpawnAudio(SfxType type, bool randomPitch, float pitch = 1, int index = 0, float volume = 1f,
        bool isLooping = false, float stereoPan = 0, float delayBeforePlay = 0, float spatialBlend = 0, float min3DDistance = 1f, 
        float max3DDistance = 500f, AudioMixerGroup audioMixerGroup = null)
    {
        //Create new sound and add stock audio source in local variable.
        GameObject newSound = Instantiate(audioCue, transform.position, Quaternion.identity);
        AudioSource newSoundAs = newSound.GetComponent<AudioSource>();

        //Set the sound's audio mixer group.
        newSoundAs.outputAudioMixerGroup = audioMixerGroup;
        
        //Security.
        newSoundAs.playOnAwake = false;

        //Switch the sound's type and pick the given index.
        //Modify here to add more types if you need.
        switch (type)
        {
            case SfxType.Type1:
                newSoundAs.clip = type1[index];
                break;
            case SfxType.Type2:
                newSoundAs.clip = type2[index];
                break;
            case SfxType.Type3:
                newSoundAs.clip = type3[index];
                break;
        }

        //Set the sound's volume.
        newSoundAs.volume = volume;

        //Set sound's pitch.
        if (randomPitch)
        {
            newSoundAs.pitch = Random.Range(1 - pitch, 1 + pitch);
        }
        else
        {
            newSoundAs.pitch = pitch;
        }

        //Set the sound looping statement.
        newSoundAs.loop = isLooping;

        //Set the sound output side.
        newSoundAs.panStereo = stereoPan;

        //Set the sound spatial function.
        newSoundAs.spatialBlend = spatialBlend;

        //Set the sound hearing distances.
        newSoundAs.minDistance = min3DDistance;
        newSoundAs.maxDistance = max3DDistance;
        
        //Play the sound.
        newSoundAs.PlayDelayed(delayBeforePlay);
    }
}

