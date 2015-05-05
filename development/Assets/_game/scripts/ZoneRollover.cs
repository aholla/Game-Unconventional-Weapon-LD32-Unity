using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class ZoneRollover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {

	[SerializeField]
	private float _rolloverDuration = 0.1f;

	[SerializeField]
	private AudioClip _audioClick;

	[SerializeField]
	private AudioClip _audioOver;

	private SpriteRenderer _rollover;
	private Zone _zone;
	
	//===================================================
	// UNITY METHODS
	//===================================================

	/// <summary>
	/// Awake.
	/// </summary>
	void Awake () {
		_rollover = transform.FindChild( "Rollover" ).GetComponent<SpriteRenderer>();
		_zone = gameObject.GetComponent<Zone>();

		FadeOut( true );
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

	/// <summary>
	/// Called when [pointer enter].
	/// </summary>
	/// <param name="eventData">The event data.</param>
	public void OnPointerEnter( PointerEventData eventData ) {
		if( !_zone.Nuked ) {
			FadeIn();
			AudioManager.instance.PlaySFX( _audioOver );
		}
	}

	/// <summary>
	/// Called when [pointer exit].
	/// </summary>
	/// <param name="eventData">The event data.</param>
	public void OnPointerExit( PointerEventData eventData ) {
		if( !_zone.Nuked ) {
			FadeOut();
		}
	}

	/// <summary>
	/// Called when [pointer click].
	/// </summary>
	/// <param name="eventData">The event data.</param>
	public void OnPointerClick( PointerEventData eventData ) {
		AudioManager.instance.PlaySFX( _audioClick );
	}

	//===================================================
	// PUBLIC METHODS
	//===================================================



	//===================================================
	// PRIVATE METHODS
	//===================================================

	/// <summary>
	/// Fades in.
	/// </summary>
	private void FadeIn() {
		LeanTween.alpha( _rollover.gameObject, 1f, _rolloverDuration );
	}

	/// <summary>
	/// Fades out.
	/// </summary>
	/// <param name="instant">if set to <c>true</c> [instant].</param>
	private void FadeOut( bool instant = false ) {
		float duration = _rolloverDuration;
		if( instant ) {
			duration = 0.001f;
		}
		LeanTween.alpha( _rollover.gameObject, 0f, duration );
	}

	//===================================================
	// EVENTS METHODS
	//===================================================



}
