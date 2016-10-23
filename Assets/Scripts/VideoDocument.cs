using System;
using UnityEngine;

[RequireComponent (typeof(AudioSource))]
[RequireComponent(typeof(Renderer))]
public class VideoDocument : Document
{
    public override void Open()
    {
        MovieTexture movie = (MovieTexture)GetComponent<Renderer>().material.mainTexture;
        AudioSource source = GetComponent<AudioSource>();
        if (movie.isPlaying)
        {
            source.Stop();
            movie.Stop();
        }
        else {
            movie.Play();
            source.Play();
        }
    }
}
