using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Enums;
using Assets.Scripts.SimulationLogic.Models;
using ScriptDefinitions.Assets.Scripts.SimulationLogic;
using UnityEngine;


// This component is attached to a game object that holds the dropped pieces, and takes them on the path up and over to the left.
// After that, it unattaches the pieces to let them fall on their own in the landing zone, and it goes back to the springboard
// Imagine it is like an amusement park ride where the guests (pieces) go in for the ride, and are dropped off at the end of the ride
public class SprungPiecesController : MonoBehaviour
{
	SprungPiecesState _currentState;
	private int _destSpingOverIndex;
	private List<Vector2> _springOverPathPoints;
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

	internal void InitializeGameSettings(List<Vector2> springOverPathPoints, float firstPieceSpringX, float pieceSpringXSpacing, LogicController logicController, float ySpeed)
	{
		_springOverPathPoints = springOverPathPoints;
		_initialPosition = transform.position;
		_firstPieceSpringX = firstPieceSpringX;
		_pieceSpringXSpacing = pieceSpringXSpacing;
		_logicController = logicController;
		_ySpeed = ySpeed;

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
		Debug.Log("Sprung pieces released");
		_currentState = SprungPiecesState.SpringingUp;
		_sprungPieces = sprungPieces;
		_springboardController = springboardController;
		_destSpingOverIndex = 0;
	}

	private void Update()
	{
		if (_currentState == SprungPiecesState.SpringingUp)
		{
			if (_destSpingOverIndex < _springOverPathPoints.Count)
			{
				Vector2 destination = new Vector2(_initialPosition.x + _springOverPathPoints[_destSpingOverIndex].x, _initialPosition.y + _springOverPathPoints[_destSpingOverIndex].y);
				transform.position = Vector2.MoveTowards(transform.position, destination, _ySpeed * Time.deltaTime); // joe fix the hard coded speed setting (should be from config)
				if (Vector2.Distance(transform.position, destination) < 0.005) {
					Debug.Log($"curr idx:{_destSpingOverIndex}. destX/Y:{destination.x}/{destination.y}, yPnt:{_springOverPathPoints[_destSpingOverIndex].y}");
					_destSpingOverIndex = _destSpingOverIndex + 1;
				}
				//new Vector3(transform.position.x, transform.position.y + 0.05f); // joe hard coded for testing
			} else
			{
				_currentState = SprungPiecesState.Idle;
				List<SimPiece> simPieces = new List<SimPiece>();
				foreach(PieceManager pieceMgr in _sprungPieces.Values)
				{
					simPieces.Add(pieceMgr.SimPiece);
					pieceMgr.transform.SetParent(null);
					pieceMgr.BeginDropUntilCollision();
				}
				_logicController.LandingZoneLogic.MoveSpringboardPiecesToLandingZone(simPieces);
				transform.SetParent(_springboardController.transform);
				transform.localPosition = Vector3.zero;
			}
		}
	}
}
