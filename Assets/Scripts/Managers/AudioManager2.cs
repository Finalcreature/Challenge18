using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

/// <summary>
/// AudioManager Singelton used to create and control Audioclips,sources and Listener
/// </summary>
public class AudioManager2 : MonoBehaviour
{
	public static AudioManager2 instance;
	public Sound[] sounds;
	private AudioSource mysource;
	private Sound BGM;


	private void Awake()
	{
		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}

		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;
			s.source.volume = s.volume;
			s.source.pitch = s.pitch;
			s.source.spatialBlend = s.spatialBlend;
		}
	}
	private void Start()
	{

	}

	/// <summary>
	/// Plays the sound name AudioClip in a set Sounds Array if possible;
	/// </summary>
	/// <param name="sound">Sound wanted Name</param>
	public void Play(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

		s.source.Play();
	}
	/// <summary>
	/// Play sound at Location
	/// </summary>
	/// <param name="sound"> Sound name</param>
	/// <param name="location">Vector3 Location</param>
	public void PlayAtLocation(string sound, Vector3 location)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}
		AudioSource.PlayClipAtPoint(s.clip, location);
	}

	/// <summary>
	/// Searches for sound name in main sounds pool and returns if exists.
	/// </summary>
	/// <param name="name">Sound name</param>
	/// <returns>Sound var with Name name </returns>
	public Sound GiveSound(string name)
	{
		Sound s = Array.Find(sounds, item => item.name == name);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return null;
		}
		return s;
	}
}