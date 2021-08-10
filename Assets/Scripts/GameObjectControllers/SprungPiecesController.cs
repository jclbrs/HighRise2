using System.Collections;
using System.Collections.Generic;
using System.Text;
using Assets.Scripts.Enums;
using Assets.Scripts.SimulationLogic.Models;
using ScriptDefinitions.Assets.Scripts.SimulationLogic;
using UnityEngine;


// This component is attached to a game object that holds the dropped pieces, and takes them on the path up and over to the left.
// After that, it unattaches the pieces to let them fall on their own in the landing zone, and it goes back to the springboard
// Imagine it is like an amusement park ride where the guests (pieces) go in for the ride, and are dropped off at the end of the ride
public class SprungPiecesController : MonoBehaviour
{
	EventsManager _eventsManager;
	SprungPiecesState _currentState;
	private int _destSpingOverArchingIndex;
	private List<Vector2> _springOverArchingPathPoints;
	private Vector2 _initialPosition;
	private Dictionary<int, PieceManager> _sprungPieces;
	private SpringboardController _springboardController;
	private float _firstPieceSpringX;
	private float _pieceSpringXSpacing;
	private List<float> _springXCoords;
	private LogicController _logicController;
	private float _ySpeed;

	private void Start()
	{
		_currentState = SprungPiecesState.Idle;
	}

	internal void InitializeGameSettings(SprungPiecesDIWrapper wrapper)
	{
		_eventsManager = wrapper.EventsManager;
		_springOverArchingPathPoints = wrapper.SpringOverPoints;
		_firstPieceSpringX = wrapper.FirstPieceSpringX;
		_pieceSpringXSpacing = wrapper.PieceSpringXSpacing;
		_logicController = wrapper.LogicController;
		_ySpeed = wrapper.SprungYSpeed;
		_initialPosition = transform.position;

		_springXCoords = new List<float>();
		for (int i = 0; i < 6; i++) // hard coded, 6 springs
		{
			_springXCoords.Add(_firstPieceSpringX + i * _pieceSpringXSpacing);
		}
	}

	public List<float> GetInitialSpringXPosns()
	{
		return _springXCoords;
	}

	public void OnPiecesReleasedFromSpringboard(SpringboardController springboardController, Dictionary<int, PieceManager> sprungPieces)
	{
		// joe debugging
		StringBuilder sb = new StringBuilder();
		foreach(KeyValuePair<int, PieceManager> kvp in sprungPieces)
		{
			sb.Append($"{kvp.Key}-{kvp.Value.SimPiece.SpringboardColumn} ...");
		}
		//Debug.Log($"Sprung pieces released: {sb.ToString()}");
		// end joe debugging
		_currentState = SprungPiecesState.SpringingUp;
		_sprungPieces = sprungPieces;
		_springboardController = springboardController;
		_destSpingOverArchingIndex = 0;
	}

	private void Update()
	{
		if (_currentState != SprungPiecesState.SpringingUp)
			return;

		if (_destSpingOverArchingIndex < _springOverArchingPathPoints.Count) // These are configured positions on the screen that the pieces pass to move over to landing zone
		{
			// move to the next coordinate to simulate a parabolic action up and over from springboard area to landing zone area
			Vector2 destination = new Vector2(_initialPosition.x + _springOverArchingPathPoints[_destSpingOverArchingIndex].x, _initialPosition.y + _springOverArchingPathPoints[_destSpingOverArchingIndex].y);
			transform.position = Vector2.MoveTowards(transform.position, destination, _ySpeed * Time.deltaTime); // joe fix the hard coded speed setting (should be from config)
			if (Vector2.Distance(transform.position, destination) < 0.005)
				_destSpingOverArchingIndex = _destSpingOverArchingIndex + 1;
		}
		else
		{
			// pieces (still children of the SprungPiecesController) are now at the top of the landing zone, and ready to drop
			_currentState = SprungPiecesState.Idle;
			List<SimPiece> simPieces = new List<SimPiece>();
			foreach (PieceManager pieceMgr in _sprungPieces.Values)
				simPieces.Add(pieceMgr.SimPiece);

			// calculate newly added pieces to the simulation, and determine if stable
			bool isStable = _logicController.LandingZoneLogic.MoveSpringboardPiecesToLandingZone(simPieces);
			//Debug.Log($"SprungPiecesController. Stable:{isStable}");

			// move all piece out of SprungPiecesController, and have them now be children of LandingZone
			foreach (PieceManager pieceMgr in _sprungPieces.Values)
			{
				pieceMgr.BeginDropInLandingZone(isStable);
				GameObject landingPiecesGO = GameObject.Find("/LandingZone/LandingPieces");
				pieceMgr.transform.SetParent(landingPiecesGO.transform);
			}

			int highestLandingRowIdx = _logicController.LandingZoneLogic.GetHighestPieceRowIdx();
			_eventsManager.OnPiecesLanding(isStable, highestLandingRowIdx);  // The row arrow should be a listener

			// The SprungPiecesController is done transporting the pieces over to LandingZone, so now bring it back as a child of Springboard
			transform.SetParent(_springboardController.transform);
			transform.localPosition = Vector3.zero;

			// If this is not stable, clear out the simulation landingZone to start over
			if (isStable)
			{
				if (_logicController.LandingZoneLogic.IsLevelSuccess())
				{ 
					Debug.Log("LEVEL SUCCESS");
					_eventsManager.OnSuccessfulLevel();
				}
			}
			else
				_logicController.LandingZoneLogic.ClearLandingZone();
			
		}
	}
}
