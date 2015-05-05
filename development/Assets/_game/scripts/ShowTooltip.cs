using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class ShowTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	
	[SerializeField]
	private Tooltip _tooltip;

	private Zone _zone;
	private TooltipData _tooltipData;

	//===================================================
	// UNITY METHODS
	//===================================================

	/// <summary>
	/// Awake.
	/// </summary>
	void Awake () {
		_zone = gameObject.GetComponent<Zone>();
		_tooltipData = new TooltipData();
	}

	/// <summary>
	/// Start.
	/// </summary>
	void Start () {
		
	}

	//===================================================
	// PUBLIC METHODS
	//===================================================

	/// <summary>
	/// Called when the users moves the mouse over the zone.
	/// </summary>
	/// <param name="eventData">The event data.</param>
	public void OnPointerEnter( PointerEventData eventData ) {
		_tooltipData.id = _zone.ID;
		_tooltipData.population = _zone.Population;
		_tooltipData.populationIn = _zone.PopulationIn;
		_tooltipData.populationOut = _zone.PopulationOut;
		_tooltipData.infected = _zone.Infected;
		_tooltipData.infectedIn = _zone.InfectedIn;
		_tooltipData.infectedOut = _zone.InfectedOut;

		// show the info in the tooltip
		_tooltip.Show( _tooltipData );
	}

	/// <summary>
	/// Called when [pointer exit].
	/// </summary>
	/// <param name="eventData">The event data.</param>
	public void OnPointerExit( PointerEventData eventData ) {
		//_tooltip.Hide();
	}

	//===================================================
	// PRIVATE METHODS
	//===================================================



	//===================================================
	// EVENTS METHODS
	//===================================================


}
