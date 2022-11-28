using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSound : MonoBehaviour
{
    public AudioClip AudioClip;
    private AudioSource _audio;

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        _audio.PlayOneShot(AudioClip);
    }


    private void OnDisable()
    {
        _audio.Stop();
    }
   
}
