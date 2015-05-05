using UnityEngine;
using System;

public class Stats {

	public float Education {
		get { return Mathf.Round(_education); }
	}

	public float Police {
		get { return  Mathf.Round(_police); }
	}

	public float Vaccine {
		get { return Mathf.Round(_vaccine); }
	}

	public float Remainign {
		get { return Mathf.Round(_remaining); }
	}

	public float Efficiency {
		get {
			float efficiency = GameConstants.vaccineStart;
			efficiency += GameConstants.vaccineInc * _vaccine;
			return Mathf.Round(efficiency);
		}
	}

	public float Cured {
		get {
			return Mathf.Round( GameManager.instance.Infected * (Efficiency/100.0f) );
		}
	}

	public float Nuked {
		get {
			return Mathf.Round( _nuked );
		}
	}

	public float Survivors {
		get {
			return Mathf.Round( GameManager.instance.Population - ( GameManager.instance.Infected - Cured ) );
		}
	}

	private float _education;
	private float _police;
	private float _vaccine;
	private float _remaining;
	private float _nuked;

	//===================================================
	// PUBLIC METHODS
	//===================================================

	/// <summary>
	/// Initializes this instance.
	/// </summary>
	public void Init() {
		_education = 0.0f;
		_police = 0.0f;
		_vaccine = 0.0f;
		_remaining = 0.0f;
		_nuked = 0.0f;
	}

	/// <summary>
	/// Saves the spending eduction.
	/// </summary>
	/// <param name="value">The value.</param>
	public void SaveSpendingEduction( float value ) {
		_education += value;
	}

	/// <summary>
	/// Saves the spending police.
	/// </summary>
	/// <param name="value">The value.</param>
	public void SaveSpendingPolice( float value ) {
		_police += value;
	}

	/// <summary>
	/// Saves the spending vaccine.
	/// </summary>
	/// <param name="value">The value.</param>
	public void SaveSpendingVaccine( float value ) {
		_vaccine += value;

	}

	/// <summary>
	/// Saves the spending remaining.
	/// </summary>
	/// <param name="value">The value.</param>
	public void SaveSpendingRemaining( float value ) {
		_remaining += value;
	}

	/// <summary>
	/// Saves the nuked.
	/// </summary>
	/// <param name="value">The value.</param>
	public void SaveNuked( float value ) {
		_nuked += value;
	}
}

