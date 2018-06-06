using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class AudioManager : MonoBehaviour {

	public AudioClip sound;

	private Button button { get { return GetComponent<Button> (); } }
	private AudioSource source { get { return GetComponent<AudioSource> (); } }

	void Start()
	{
		gameObject.AddComponent<AudioSource> ();
		source.clip = sound;
		source.playOnAwake = false;

		button.onClick.AddListener (() => PlaySound ());
	}
	void PlaySound()
	{
		AudioSource[] audios = FindObjectsOfType (typeof(AudioSource)) as AudioSource[];
		foreach (AudioSource aud in audios)
		if (aud.isPlaying)
			aud.Stop ();
		source.PlayOneShot (sound);
	}

}
