using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Tooltip : MonoBehaviour {

	[SerializeField]
	private Text _zone;

	[SerializeField]
	private Text _population;

	[SerializeField]
	private Text _populationIn;

	[SerializeField]
	private Text _populationOut;

	[SerializeField]
	private Image _populationInIcon;

	[SerializeField]
	private Image _populationOutIcon;
	
	[SerializeField]
	private Text _infected;
	
	[SerializeField]
	private Text _infectedIn;

	[SerializeField]
	private Text _infectedOut;

	[SerializeField]
	private Image _infectedInIcon;

	[SerializeField]
	private Image _infectedOutIcon;

	private GameManager _gameManger;
	
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
		_gameManger = GameManager.instance;
		_gameManger.EventTitle += OnStartGame;
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
	/// Shows the specified data.
	/// </summary>
	/// <param name="data">The data.</param>
	public void Show( TooltipData data ) {
		_zone.text = data.id.ToString( "00" );
		_population.text = data.population.ToString();
		_populationIn.text = data.populationIn.ToString();
		_populationOut.text = data.populationOut.ToString();
		_infected.text = data.infected.ToString();
		_infectedIn.text = data.infectedIn.ToString();
		_infectedOut.text = data.infectedOut.ToString();

		SetArrow( data.populationIn, _populationInIcon );
		SetArrow( data.populationOut, _populationOutIcon );
		SetArrow( data.infectedIn, _infectedInIcon);
		SetArrow( data.infectedOut, _infectedOutIcon );
	}

	//===================================================
	// PRIVATE METHODS
	//===================================================

	/// <summary>
	/// Sets the arrow direction or hides it.
	/// </summary>
	/// <param name="value">The value.</param>
	/// <param name="noChangeIcon">The no change icon.</param>
	/// <param name="arrow">The arrow.</param>
	private void SetArrow( int value, Image icon ) {
		if( value > 0 ) {
			icon.gameObject.SetActive( true );
		} else {
			icon.gameObject.SetActive( false );
		}		
	}

	//===================================================
	// EVENTS METHODS
	//===================================================

	private void OnStartGame() {
		string x = "XX";
		_zone.text = x;
		_population.text = x;
		_populationIn.text = x;
		_populationOut.text = x;
		_infected.text = x;
		_infectedIn.text = x;
		_infectedOut.text = x;
	}

	private void OnEndGame() {
	}

}
