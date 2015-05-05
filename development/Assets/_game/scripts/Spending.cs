using UnityEngine;
using System.Collections;

[System.Serializable]
public class Spending  {

	public float police = 0.0f;
	public float education = 0.0f;
	public float research = 0.0f;
	public float remaining = 0.0f;

	private float _budget = 1.0f;
	private float _remainingBudget = 0.0f;

	//===================================================
	// UNITY METHODS
	//===================================================


	//===================================================
	// PUBLIC METHODS
	//===================================================

	public void Init() {
		_remainingBudget = _budget;
		SetPolice( Random.Range( 0.0f, _remainingBudget ) );
		SetEducation( Random.Range( 0.0f, _remainingBudget ) );
		SetResearch( _remainingBudget );
		remaining = _remainingBudget;
	}

	//===================================================
	// PRIVATE METHODS
	//===================================================

	private void SetPolice( float value ) {
		police = value;
		_remainingBudget -= value;
	}

	private void SetEducation( float value ) {
		education = value;
		_remainingBudget -= value;
	}

	private void SetResearch( float value ) {
		research = value;
		_remainingBudget -= value;
	}

	//===================================================
	// EVENTS METHODS
	//===================================================



}
