using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCache
{
    private readonly string PATH = "Audio/";

    private AudioClip[] clips;
    private Dictionary<string, AudioClip> links = new Dictionary<string, AudioClip>();

    public AudioCache Load()
    {
        clips = Resources.LoadAll<AudioClip>(PATH);
        foreach (AudioClip clip in clips)
        {
            links.Add(clip.name, clip);
        }
        return this;
    }

    public AudioClip Get(string name)
    {
        return links[name];
    }
}

