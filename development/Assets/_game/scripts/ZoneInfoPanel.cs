using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ZoneInfoPanel : MonoBehaviour {

	[SerializeField]
	private Text _textZone;

	[SerializeField]
	private Text _textPopulation;
	
	[SerializeField]
	private Text _textInfected;

	[SerializeField]
	private Text _textBudget;

	[SerializeField]
	private Slider _sliderEducation;

	[SerializeField]
	private Slider _sliderPolice;

	[SerializeField]
	private Slider _sliderReaserch;

	[SerializeField]
	private Toggle _toggleApplyToAll;

	[SerializeField]
	private Toggle _nukeToggle;

	[SerializeField]
	private Image _nukeOverlay;

	[SerializeField]
	private Text _buttonApplyText;

	[SerializeField]
	private int _whenNukeAvailable;

	private Zone _zone;
	private float _budgetAvail;

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

	/// <summary>
	/// OnDisable.
	/// </summary>
	void OnDisable() {
		_toggleApplyToAll.isOn = false;
		_nukeToggle.isOn = false;
	}

	//===================================================
	// PUBLIC METHODS
	//===================================================

	/// <summary>
	/// Shows the info panel when clicked.
	/// </summary>
	/// <param name="zone">The zone.</param>
	public void Show( Zone zone ) {
		_zone = zone;
		_textZone.text = "ZONE:" + _zone.ID.ToString("00");
		_textPopulation.text = _zone.Population.ToString();
		_textInfected.text = _zone.Infected.ToString();

		_budgetAvail = _zone.Population * GameConstants.budget;	

		UpdateSliders();
		UpdateBudget();
		CheckIfNukeAvailable();

		this.gameObject.SetActive( true );
	}

	/// <summary>
	/// Hides the panel.
	/// </summary>
	public void Hide() {
		this.gameObject.SetActive( false );
	}


	/// <summary>
	/// Adjusts the education slider.
	/// </summary>
	/// <param name="value">The value.</param>
	public void AdjustSliderEducation( float value ) {
		_zone.Spending.education = value;

		while( _zone.Spending.education + _zone.Spending.police + _zone.Spending.research > 1.0f ) {
			if( _zone.Spending.police > _zone.Spending.research ) {
				_zone.Spending.police -= 0.1f;
			} else {
				_zone.Spending.research -= 0.1f;
			}
		}
		UpdateSliders();
		UpdateBudget();
	}

	/// <summary>
	/// Adjusts the  police slider.
	/// </summary>
	/// <param name="value">The value.</param>
	public void AdjustSliderPolice( float value ) {
		_zone.Spending.police = value;
		while( _zone.Spending.education + _zone.Spending.police + _zone.Spending.research > 1.0f ) {
			if( _zone.Spending.education > _zone.Spending.research ) {
				_zone.Spending.education -= 0.1f;
			} else {
				_zone.Spending.research -= 0.1f;
			}
		}
		UpdateSliders();
		UpdateBudget();
	}

	/// <summary>
	/// Adjusts the research slider.
	/// </summary>
	/// <param name="value">The value.</param>
	public void AdjustSliderResearch( float value ) {
		_zone.Spending.research = value;
		while( _zone.Spending.education + _zone.Spending.police + _zone.Spending.research > 1.0f ) {
			if( _zone.Spending.education > _zone.Spending.police ) {
				_zone.Spending.education -= 0.1f;
			} else {
				_zone.Spending.police -= 0.1f;
			}
		}
		UpdateSliders();
		UpdateBudget();
	}

	/// <summary>
	/// Toggles the nuke.
	/// </summary>
	public void ToggleNuke() {
		bool isNukeEnabled = _nukeToggle.isOn;

		if( isNukeEnabled ) {
			_buttonApplyText.text = "NUKE";
		} else {
			_buttonApplyText.text = "APPLY";
		}

		_nukeOverlay.gameObject.SetActive( isNukeEnabled );
	}

	//===================================================
	// PRIVATE METHODS
	//===================================================

	/// <summary>
	/// Updates the sliders values.
	/// </summary>
	private void UpdateSliders() {
		_sliderEducation.value = _zone.Spending.education;
		_sliderPolice.value = _zone.Spending.police;
		_sliderReaserch.value = _zone.Spending.research;
	}

	/// <summary>
	/// Updates the budget.
	/// </summary>
	private void UpdateBudget() {
		float education = _zone.Spending.education * _budgetAvail;
		float police = _zone.Spending.police * _budgetAvail;
		float vaccine = _zone.Spending.research * _budgetAvail;

		float budget = Mathf.Round( _budgetAvail - ( education  + police + vaccine ) );
		_zone.Spending.remaining = budget;
		_textBudget.text = "$" + budget.ToString();
	}

	/// <summary>
	/// Checks if nuke available.
	/// </summary>
	private void CheckIfNukeAvailable() {
		bool isNukeAvailable;
		if( GameManager.instance.Day > _whenNukeAvailable ) {
			isNukeAvailable = true;
		} else {
			isNukeAvailable = false;
		}

		_nukeToggle.gameObject.SetActive( isNukeAvailable );
	}

	//===================================================
	// EVENTS METHODS
	//===================================================

	/// <summary>
	/// Called when [close click].
	/// </summary>
	public void OnCloseClick() {
		Hide();
	}

	/// <summary>
	/// Called when [apply click].
	/// </summary>
	public void OnApplyClick() {
		if( !_nukeToggle.isOn ) {
			if( _toggleApplyToAll.isOn ) {
				GameManager.instance.ApplySpendingToAll( _zone.Spending );
			}
		} else {
			GameManager.instance.Nuke( _zone );
		}
		OnCloseClick();
	}

}
