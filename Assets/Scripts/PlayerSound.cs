using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    public AudioClip jumpSfx;
    public AudioClip timeFreezeSfx;

    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlayJump()
    {
        source.PlayOneShot(jumpSfx);
    }
    public void StartTimeFreeze()
    {
        source.clip = timeFreezeSfx;
        source.Play();
    }
    public void StopTimeFreeze()
    {
        source.Stop();
    }
}
