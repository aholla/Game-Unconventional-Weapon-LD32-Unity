using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class ButtonSounds : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {


	[SerializeField]
	private AudioClip _audioClick;

	[SerializeField]
	private AudioClip _audioOver;

	//===================================================
	// UNITY METHODS
	//===================================================

	/// <summary>
	/// Awake.
	/// </summary>
	void Awake () {
		
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



	//===================================================
	// PRIVATE METHODS
	//===================================================



	//===================================================
	// EVENTS METHODS
	//===================================================

	public void OnPointerEnter( PointerEventData eventData ) {
		AudioManager.instance.PlaySFX( _audioOver );
	}

	public void OnPointerExit( PointerEventData eventData ) {
		//_tooltip.Hide();
	}

	public void OnPointerClick( PointerEventData eventData ) {
		AudioManager.instance.PlaySFX( _audioClick );
	}
}
