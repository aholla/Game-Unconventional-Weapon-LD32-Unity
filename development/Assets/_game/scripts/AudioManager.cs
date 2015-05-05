using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {
	public static AudioManager instance = null;
	
	[SerializeField]
	private AudioSource _sfxSource;

	[SerializeField]
	private AudioSource _musicSource;

	//===================================================
	// UNITY METHODS
	//===================================================

	void Awake() {
		if( instance == null ) {
			instance = this;
		} else if( instance != this ) {
			Destroy( gameObject );
		}
		DontDestroyOnLoad( gameObject );
	}

	/// <summary>
	/// Start.
	/// </summary>
	void Start () {
		
	}
	
	/// <summary>
	/// Update.
	/// </summary>
	void Update () {
		
	}

	//===================================================
	// PUBLIC METHODS
	//===================================================


	/// <summary>
	/// Plays the SFX.
	/// </summary>
	/// <param name="clip">The clip.</param>
	/// <param name="volume">The volume.</param>
	public void PlaySFX( AudioClip clip, float volume = 1.0f ) {
		_sfxSource.clip = clip;
		_sfxSource.Play();
	}

	/// <summary>
	/// Plays the music.
	/// </summary>
	/// <param name="clip">The clip.</param>
	/// <param name="volume">The volume.</param>
	public void PlayMusic( AudioClip clip, float volume = 1.0f ) {
		_musicSource.clip = clip;
		_musicSource.loop = true;
		_musicSource.volume = volume;
		_musicSource.Play();
	}

	//===================================================
	// PRIVATE METHODS
	//===================================================



	//===================================================
	// EVENTS METHODS
	//===================================================



}
