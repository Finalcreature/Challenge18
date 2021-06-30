using UnityEngine.Audio;
using UnityEngine;


[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 0.75f;
    [Range(0f, 1f)]
    public float volumeVariance = .1f; // random variance for sound not repeating exactly the same

    [Range(0.1f, 3f)]
    public float pitch = 1f;
    [Range(0f, 1f)]
    public float pitchVariance = .1f; // random variance for sound not repeating exactly the same

    public float spatialBlend = 1f; //0-1 value which 1 makes it full 3d sound and 0 makes 2d sound

    public bool loop = false;


    [HideInInspector]
    public AudioSource source;
}