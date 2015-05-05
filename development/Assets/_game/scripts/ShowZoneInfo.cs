using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ShowZoneInfo : MonoBehaviour, IPointerClickHandler {

	[SerializeField]
	private ZoneInfoPanel _zoneInfoPanel;

	private Zone _zone;
	
	//===================================================
	// UNITY METHODS
	//===================================================

	/// <summary>
	/// Awake.
	/// </summary>
	void Awake () {
		_zone = gameObject.GetComponent<Zone>();
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
	/// Called when [pointer click].
	/// </summary>
	/// <param name="eventData">The event data.</param>
	public void OnPointerClick( PointerEventData eventData ) {
		if( !_zone.Nuked ) {
			_zoneInfoPanel.Show( _zone );
		}
	}

	//===================================================
	// PRIVATE METHODS
	//===================================================


	//===================================================
	// EVENTS METHODS
	//===================================================


	
}
