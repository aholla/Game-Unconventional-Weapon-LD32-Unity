using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Hud : MonoBehaviour {
	
	[SerializeField]
	private Text _day;

	[SerializeField]
	private Text _population;

	[SerializeField]
	private Text _infected;

	private GameManager _gameManager;
	
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
		_gameManager = GameManager.instance;	
		_gameManager.EventNewDay += OnNewDay;
		_gameManager.EventTitle += OnTitle;
		_gameManager.EventNuke += OnNewDay;		
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

	/// <summary>
	/// Called when GameManager distatches a new day.
	/// </summary>
	private void OnNewDay() {
		_day.text = _gameManager.Day.ToString() + "/" + _gameManager.TotalDays.ToString();
		_population.text = _gameManager.Population.ToString();
		_infected.text = _gameManager.Infected.ToString();
	}

	/// <summary>
	/// Called when title is shown.
	/// </summary>
	private void OnTitle() {
		_day.text = "XX/XX";
		_population.text = "XXXX";
		_infected.text = "XXXX";
	}

}
