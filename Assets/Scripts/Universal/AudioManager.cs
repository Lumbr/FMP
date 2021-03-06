﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource[] sfx;
    public AudioSource[] musicTracks;
    public int currentTrack;
    public bool musicCanPlay;
    public float volume;
    public float maxVol;
    List<int> tracksToPlay = new List<int> { };
    bool done = true;
    void Update()
    {
        foreach (AudioSource sf in sfx) { sf.volume = volume * maxVol; }
        foreach (AudioSource song in musicTracks) { song.volume = volume * maxVol; };
        if (musicCanPlay)
        {
            if (!musicTracks[currentTrack].isPlaying)
            {
                if(tracksToPlay != null &&tracksToPlay.Count >= 1&& !done)
                {
                    currentTrack = tracksToPlay[Random.Range(0, tracksToPlay.Count - 1)];
                    done = true;
                }
                musicTracks[currentTrack].Play();
            }
        }
        else
        {
            musicTracks[currentTrack].Stop();
        }
    }
    public void SwitchTrack(int newTrack)
    {
        foreach (AudioSource track in musicTracks) track.Stop();
        currentTrack = newTrack;
        tracksToPlay.Clear();
        musicTracks[currentTrack].Play();
        done = false;
    }
    public void SwitchTrack(List<int> newTracks)
    {
        foreach(AudioSource track in musicTracks) track.Stop();
        tracksToPlay = newTracks;
        done = false;
    }
}
