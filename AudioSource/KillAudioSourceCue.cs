using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillAudioSourceCue : MonoBehaviour
{
    void Start()
    {
        AudioSource As = GetComponent<AudioSource>();
        Destroy(gameObject, As.clip.length + 0.025f);
    }
}
