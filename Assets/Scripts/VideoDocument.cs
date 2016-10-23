using System;
using UnityEngine;

public class VideoDocument : Document
{

    public override void Open()
    {
        MovieTexture movie = (MovieTexture)GetComponent<Renderer>().material.mainTexture;
        if (movie.isPlaying)
            movie.Stop();
        else
            movie.Play();
    }
}
