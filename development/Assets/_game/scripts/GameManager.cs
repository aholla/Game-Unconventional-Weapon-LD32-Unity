using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {
	public static GameManager instance = null;

	public delegate void DelegateEvent();
	public event DelegateEvent EventNewDay;
	public event DelegateEvent EventTitle;
	public event DelegateEvent EventNuke;

	public int Population {
		get {
			int population = 0;
			for( int i = 0; i < _zones.Length; i++ ) {
				Zone zone = _zones[ i ];
				if( !zone.Nuked ) {
					population += zone.Population;
				}
			}
			return population; 
		}
	}

	public int Infected {
		get {
			int infected = 0;
			for( int i = 0; i < _zones.Length; i++ ) {
				Zone zone = _zones[ i ];
				infected += zone.Infected;
			}
			return infected;
		}
	}

	public int Day {
		get { return _day; }
	}

	public int TotalDays {
		get { return _totalDays; }
	}

	public Stats Stats {
		get { return _stats;  }
	}

	[SerializeField]
	private GameObject _titleOverlay;

	[SerializeField]
	private GameObject _startDayOverlay;

	[SerializeField]
	private GameObject _endGameOverlay;

	[SerializeField]
	private Zone[] _zones;

	[SerializeField]
	private AudioClip _audioMusic;

	[Range(1, 7)]
	[SerializeField]
	private int _totalDays;

	private int _day;
	private Stats _stats;
	
	//===================================================
	// UNITY METHODS
	//===================================================

	/// <summary>
	/// Awake.
	/// </summary>
	void Awake () {
		if( instance == null ) {
			instance = this;
		} else if( instance != this ) {
			Destroy( gameObject );
		}
		DontDestroyOnLoad( gameObject );

		_titleOverlay.SetActive( false );
		_startDayOverlay.SetActive( false );
		_endGameOverlay.SetActive( false );
	}

	/// <summary>
	/// Start.
	/// </summary>
	void Start () {
		_stats = new Stats();
		AudioManager.instance.PlayMusic( _audioMusic, 0.2f );
		InitGame();
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
	/// Applies the spending to all zones.
	/// </summary>
	/// <param name="spending">The spending.</param>
	public void ApplySpendingToAll( Spending spending ) {
		for( int i = 0; i < _zones.Length; i++ ) {
			Zone zone = _zones[ i ];
			zone.ApplySpending( spending );
		}
	} 

	/// <summary>
	/// Nukes the specified zone.
	/// </summary>
	/// <param name="zone">The zone.</param>
	public void Nuke( Zone zone ) {
		_stats.SaveNuked( zone.Population );
		zone.Nuke();
		if( EventNuke != null ) {
			EventNuke();
		}
	}	

	//===================================================
	// PRIVATE METHODS
	//===================================================

	/// <summary>
	/// Initializes the game.
	/// </summary>
	private void InitGame() {
		_stats.Init();
		_day = 1;

		for( int i = 0; i < _zones.Length; i++ ) {
			Zone zone = _zones[ i ];
			zone.Init();
		}

		ShowTitleOverlay();
	}

	/// <summary>
	/// Shows the title overlay.
	/// </summary>
	private void ShowTitleOverlay() {
		_startDayOverlay.SetActive( false );
		_endGameOverlay.SetActive( false );
		_titleOverlay.SetActive( true );

		if( EventTitle != null ) {
			EventTitle();
		}
	}

	/// <summary>
	/// Shows the start day overlay.
	/// </summary>
	private void ShowStartDayOverlay() {
		_titleOverlay.SetActive( false );
		_endGameOverlay.SetActive( false );
		_startDayOverlay.SetActive( true );
		StartNewDay();
	}

	/// <summary>
	/// Shows the end game overlay.
	/// </summary>
	private void ShowEndGameOverlay() {
		_titleOverlay.SetActive( false );
		_startDayOverlay.SetActive( false );
		_endGameOverlay.SetActive( true );

		EndGamePanel endGame = _endGameOverlay.GetComponent<EndGamePanel>();
		endGame.ShowStats();
	}

	/// <summary>
	/// Starts a new day.
	/// </summary>
	private void StartNewDay() {
		// if not the first day, spread the infection.
		if( _day != 1 ) {
			for( int i = 0; i < _zones.Length; i++ ) {
				Zone zone = _zones[ i ];
				zone.Spread();
			}
		}

		if( EventNewDay != null ) {
			EventNewDay();
		}

		_day += 1;
	}

	/// <summary>
	/// Saves the spending stats.
	/// </summary>
	private void SaveStats() {
		for( int i = 0; i < _zones.Length; i++ ) {
			Zone zone = _zones[ i ];

			if( !zone.Nuked ) {
				_stats.SaveSpendingEduction( zone.Spending.education );
				_stats.SaveSpendingPolice( zone.Spending.police );
				_stats.SaveSpendingVaccine( zone.Spending.research );
				_stats.SaveSpendingRemaining( zone.Spending.remaining );
			}
		}
	}

	//===================================================
	// EVENTS METHODS
	//===================================================

	/// <summary>
	/// Called when the user clicks the start button
	/// </summary>
	public void OnTitleStartClick() {
		ShowStartDayOverlay();
	}

	/// <summary>
	/// Called when [start day click].
	/// </summary>
	public void OnStartDayClick() {
		_startDayOverlay.SetActive( false );		
	}

	/// <summary>
	/// Called when [end game replay click].
	/// </summary>
	public void OnEndGameReplayClick() {
		_endGameOverlay.SetActive( false );
		InitGame();
	}

	/// <summary>
	/// Called when [end day button click].
	/// </summary>
	public void OnEndDayButtonClick() {
		SaveStats();
		if( _day <= _totalDays ) {
			ShowStartDayOverlay();
		} else {
			ShowEndGameOverlay();
		}
	}


}
