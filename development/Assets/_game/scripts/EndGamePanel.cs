using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndGamePanel : MonoBehaviour {

	[SerializeField]
	private Text _education;

	[SerializeField]
	private Text _police;

	[SerializeField]
	private Text _vaccine;

	[SerializeField]
	private Text _remaining;

	[SerializeField]
	private Text _population;

	[SerializeField]
	private Text _efficiency;

	[SerializeField]
	private Text _infected;

	[SerializeField]
	private Text _cured;

	[SerializeField]
	private Text _nuked;

	[SerializeField]
	private Text _survivors;
	
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

	/// <summary>
	/// Shows the stats.
	/// </summary>
	public void ShowStats() {
		GameManager gameManager = GameManager.instance;

		float multi = 100.0f;

		_education.text = "$" + ( gameManager.Stats.Education * multi ).ToString();
		_police.text = "$" + (gameManager.Stats.Police * multi ).ToString();
		_vaccine.text = "$" + (gameManager.Stats.Vaccine * multi ).ToString();
		_remaining.text = "$" + (gameManager.Stats.Remainign * multi ).ToString();

		_population.text = gameManager.Population.ToString();
		_infected.text = gameManager.Infected.ToString();
		_efficiency.text = gameManager.Stats.Efficiency.ToString() + "%";
		_cured.text = gameManager.Stats.Cured.ToString();
		_nuked.text = gameManager.Stats.Nuked.ToString();
		_survivors.text = gameManager.Stats.Survivors.ToString();
	}

	//===================================================
	// PRIVATE METHODS
	//===================================================



	//===================================================
	// EVENTS METHODS
	//===================================================


}
