using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartDayPanel : MonoBehaviour {

	[SerializeField]
	private Text _dayText;

	[SerializeField]
	private Text _descText;

	[SerializeField]
	private string[]  _dayCopy;

	private GameManager _gameManager;

	//===================================================
	// UNITY METHODS
	//===================================================

	/// <summary>
	/// Awake.
	/// </summary>
	void Awake () {
		_gameManager = GameManager.instance;
		_gameManager.EventNewDay += OnNewDay;
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

	private void OnNewDay() {
		int day = _gameManager.Day;
		_dayText.text = "DAY " + day.ToString() + "/" + _gameManager.TotalDays.ToString();
		_descText.text = _dayCopy[ day -1];
	}

}
