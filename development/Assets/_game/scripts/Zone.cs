using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Serialization;

public class Zone : MonoBehaviour {

	public int ID {
		get { return _id; }
	}

	public float InfectedPercent {
		get { return Mathf.Round( ( _infected / _population ) * 100.0f );  }
	}	

	public int Population {
		get { return (int) _population;	}
	}

	public int PopulationIn {
		get { return ( int ) ( _nonInfectedIn + _infectedIn); }
	}

	public int PopulationOut {
		get { return ( int ) ( _nonInfectedOut + _infectedOut ); }
	}

	public int Infected {
		get { return ( int ) _infected; }
	}

	public int InfectedIn {
		get { return ( int ) _infectedIn; }
	}

	public int InfectedOut {
		get { return ( int ) _nonInfectedOut; }
	}

	public int OutInfected {
		get { return ( int ) _infectedOut; }
	}

	public Spending Spending {
		get { return _spending; }
	}

	
	public bool Nuked { 
		get { return _isNuked; } 
	}

	[SerializeField]
	private float _population;	

	[SerializeField]
	private float _infected;

	[SerializeField]
	private Spending _spending;

	[SerializeField]
	private Zone[] _neighborZones;

	[SerializeField]
	private bool _startInfection = false;


	private int _id;
	private SpriteRenderer _infectionHighlight;
	private SpriteRenderer _nukeOverlay;
	private Color _infectionColor;

	private float _nonInfectedIn;
	private float _nonInfectedOut;
	private float _infectedIn;
	private float _infectedOut;

	private bool _isNuked = false;

	//===================================================
	// UNITY METHODS
	//===================================================

	/// <summary>
	/// Awake.
	/// </summary>
	void Awake() {
		_id = int.Parse( gameObject.name );
		_spending = new Spending();
		_infectionHighlight = transform.FindChild( "Infection Overlay" ).GetComponent<SpriteRenderer>();
		_nukeOverlay = transform.FindChild( "Nuke Overlay" ).GetComponent<SpriteRenderer>();
	}

	//===================================================
	// PUBLIC METHODS
	//===================================================

	/// <summary>
	/// Initializes this instance.
	/// </summary>
	public void Init() {
		_population = Random.Range( 80, 100 );
		_spending.Init();

		_isNuked = false;

		_infectionColor = _infectionHighlight.color;
		_infectionColor.a = 0.0f;
		_infectionHighlight.color = _infectionColor;

		_nukeOverlay.color = _infectionColor;

		_nonInfectedIn = 0.0f;
		_nonInfectedOut = 0.0f;
		_infectedIn = 0.0f;
		_infectedOut = 0.0f;

		_infected = 0.0f;

		if( _startInfection ) {
			_infected = Mathf.Round( 0.8f * _population );
			ShowInfectedColor();
		}
	}

	/// <summary>
	/// Spreads the infection if infected.
	/// </summary>
	public void Spread() {
		if( _infected > 0 ) {
			SpreadWithinZone();
		}
		SpeadToNewZone();
	}

	/// <summary>
	/// Applies the spending settings from another zone.
	/// </summary>
	/// <param name="spending">The spending.</param>
	public void ApplySpending( Spending spending ) {
		_spending.education = spending.education;
		_spending.police = spending.police;
		_spending.research = spending.research;
	}

	/// <summary>
	/// Move new "people" into the zone.
	/// </summary>
	/// <param name="nonInfected">The additions.</param>
	/// <param name="infected">The infected.</param>
	public void Migrate( float nonInfected, float infected ) {
		_nonInfectedIn = nonInfected;
		_infectedIn = infected;

		_population += _nonInfectedIn + _infectedIn;
		_infected += _infectedIn;

		ShowInfectedColor();
	}

	/// <summary>
	/// Nukes this instance.
	/// </summary>
	public void Nuke() {
		_isNuked = true;
		_population = 0;
		_infected = 0;

		Color nukeColor = _nukeOverlay.color;
		nukeColor.a = 1.0f;
		_nukeOverlay.color = nukeColor;
	}

	//===================================================
	// PRIVATE METHODS
	//===================================================

	/// <summary>
	/// Shows the color of the infected.
	/// </summary>
	private void ShowInfectedColor() {
		_infectionColor.a = InfectedPercent / 100.0f;
		_infectionHighlight.color = _infectionColor;
	}

	/// <summary>
	/// Spreads within zone.
	/// </summary>
	private void SpreadWithinZone() {
		if( !_isNuked ) {
			float infectionMultiplier = GameConstants.infectionMultiplyer;
			float educationDampener = 1.0f - _spending.education;
			float spreadCount = Mathf.Floor( _infected * ( infectionMultiplier * ( 1.0f + educationDampener ) ) );

			_infected += spreadCount;

			if( _infected > _population ) {
				_infected = _population;
			}

			ShowInfectedColor();
		}
	}

	/// <summary>
	/// Speads to new zone.
	/// </summary>
	private void SpeadToNewZone() {
		if( !_isNuked && InfectedPercent >= GameConstants.minInfectedPercent ) {

			// select the number of population that will migrate. include a percentage of the infected.
			float migrationMultiplier = GameConstants.migrationMultiplyer;// Random.Range( GameConstants.speadMin, GameConstants.speadMax );
			float policeDampener = 1.0f - _spending.police;
			float migrationCountNonInfected = Mathf.Floor( _population * ( migrationMultiplier * ( 1.0f + policeDampener ) ) );
			float migrationCountInfected = Mathf.Floor( migrationCountNonInfected * ( InfectedPercent / 100.0f) );
			
			// select only the zones that have a lower infection rate.
			List<Zone> migrationZones = new List<Zone>();
			for( int i = 0; i < _neighborZones.Length; i++ ) {
				Zone _neighbor = _neighborZones[ i ];
				if( !_neighbor.Nuked && _neighbor.InfectedPercent <= InfectedPercent + 10.0f ) {
					migrationZones.Add( _neighbor );
				}
			}

			migrationCountNonInfected -= migrationCountInfected;

			// apply values to globals.			
			_population -= migrationCountNonInfected + migrationCountInfected;
			_infected -= migrationCountInfected;
			_nonInfectedOut = migrationCountNonInfected;
			_infectedOut = migrationCountInfected;


			// distribute this migration population to the random neibouring zones.
			if( migrationZones.Count > 0 ) {

				migrationZones.Shuffle();

				//TODO: do this without the while loop.
				while( migrationCountNonInfected > 0 || migrationCountInfected > 0 ) {
					for( int j = 0; j < migrationZones.Count; j++ ) {
						Zone newZone = migrationZones[ j ];

						float newNonInfected = Mathf.Ceil( Random.Range( 0, migrationCountNonInfected ) );
						float newInfected = Mathf.Ceil( Random.Range( 0, migrationCountInfected ) );

						migrationCountNonInfected -= newNonInfected;
						migrationCountInfected -= newInfected;

						newZone.Migrate( newNonInfected, newInfected );
					}
				}
			}		
		}
	}

	//===================================================
	// EVENTS METHODS
	//===================================================



	
}
