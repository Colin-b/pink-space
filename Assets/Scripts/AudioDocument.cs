using System;
using UnityEngine;

public class AudioDocument : Document
{

    public override void Open()
    {
        AudioSource source = GetComponent<AudioSource>();
        if (source.isPlaying)
            source.Stop();
        else
            source.Play();
    }
}
