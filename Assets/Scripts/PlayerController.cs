using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using Assets.Scripts.Enums;
using ScriptDefinitions.Assets.Scripts.SimulationLogic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public bool IsInitialized = false;

	private List<float> _playerXBelowBins;
	private List<float> _springboardXPosns;
	private PlayerState _currentState;
	private int _currentXIndex;
	private float _destinationXCoord;
	private float _xSpeed;
	private bool _isMoveRight; // -1 for left, +1 for right
	private LogicController _logicController;
	private EventsManager _eventsManager;

	public void InitializeGameSettings(List<float> springboardXPosns, float playerXSpeed, EventsManager eventsManager)
	{
		_springboardXPosns = springboardXPosns;
		_xSpeed = playerXSpeed;
		_eventsManager = eventsManager;
	}

	public void InitializeLevelSettings(LogicController logicController, List<float> binXPosns, float xOffSetFromBin)
	{
		_logicController = logicController;
		_playerXBelowBins = new List<float>();
		foreach (float binXPosn in binXPosns)
			_playerXBelowBins.Add(binXPosn + xOffSetFromBin);
		_currentState = PlayerState.IdleUnderBin;
		_currentXIndex = 0;
		transform.position = new Vector3(_playerXBelowBins[0], transform.position.y);
	}

	private void Start()
	{
	}

	void Update()
	{
		HandleInput();

		switch (_currentState)
		{
			case PlayerState.IdleUnderBin:
				// no actions
				break;
			case PlayerState.MovingToBin:
				BeginMoveToNextBin();
				break;
			case PlayerState.WaitingForBinPiece:
				// no action for the moment
				break;
			case PlayerState.PushingPieceToSpringboard:
				Debug.Log($"Moving piece: x:{transform.position.x}, speed:{_xSpeed}, Dest:{_destinationXCoord}");
				transform.position = new Vector3(transform.position.x - _xSpeed, transform.position.y);
				if (transform.position.x <= _destinationXCoord)
				{
					_currentState = PlayerState.IdleBySpringboard; // joe maybe change to reflect that player is still holding the piece above spring

				}
				break;
			case PlayerState.IdleBySpringboard:
				// for now, no action
				break;
			default:
				throw new NotImplementedException($"Player State ({_currentState}) not handled yet");
		}
	}

	private void CheckIfArrivedAtSpring()
	{
		throw new NotImplementedException();
	}

	private void BeginMoveToNextBin()
	{
		if (_isMoveRight)
		{
			if (transform.position.x >= _destinationXCoord)
			{
				_currentState = PlayerState.IdleUnderBin;
				_currentXIndex++;
			}
			else
				transform.position = new Vector3(transform.position.x + _xSpeed, transform.position.y);
		}
		else
		{
			if (transform.position.x <= _destinationXCoord)
			{
				_currentState = PlayerState.IdleUnderBin;
				_currentXIndex--;
			}
			else
				transform.position = new Vector3(transform.position.x - _xSpeed, transform.position.y);
		}
	}

	// Invoked by a PieceManager via UnityEvent, when piece just fell to ground from bin
	public void OnBeginMovePieceToSpringboard(PieceManager pieceManager)
	{
		Debug.Log("Player: BeginMovingPieceToSpringboard triggered");
		_currentState = PlayerState.PushingPieceToSpringboard;
		int springIdx;
		if (_logicController.SpringboardLogic.TryMovePieceToAvailableSpring(pieceManager.SimPiece.Id, pieceManager.SimPiece.GetWidth(), out springIdx))
		{
			_destinationXCoord = _springboardXPosns[springIdx];
			_isMoveRight = false;
		}
		else
		{
			// joe move to area where piece will be sent to garbage
			throw new NotImplementedException("Need move to garbage area");
		}
		//Debug.Log($"1st avail spring:{firstAvailableSpringIdx}");
	}

	private void HandleInput()
	{
		if (Input.GetKeyDown("right"))
		{
			switch (_currentState)
			{
				case PlayerState.IdleUnderBin:
					if (_currentXIndex < _playerXBelowBins.Count - 1)
					{
						_currentState = PlayerState.MovingToBin;
						_destinationXCoord = _playerXBelowBins[_currentXIndex + 1];
						_isMoveRight = true;
					}
					break;
				case PlayerState.MovingToBin: // player is already moving.  Ignore the extra key entry
					break;
				default:
					throw new Exception($"actions for {_currentState} not written yet");
			}
		}
		if (Input.GetKeyDown("left"))
		{
			switch (_currentState)
			{
				case PlayerState.IdleUnderBin:
					if (_currentXIndex > 0)
					{
						_currentState = PlayerState.MovingToBin;
						_destinationXCoord = _playerXBelowBins[_currentXIndex - 1];
						_isMoveRight = false;
					}
					break;
				case PlayerState.MovingToBin: // player is already moving.  Ignore the extra key entry
					break;
				default:
					throw new Exception($"actions for {_currentState} not written yet");
			}
		}
		if (Input.GetKeyDown("space") && _currentState == PlayerState.IdleUnderBin)
		{
			_currentState = PlayerState.WaitingForBinPiece;
			_eventsManager.OnBinPieceSelected(_currentXIndex);
			//BinPieceDropRequested.Invoke(_currentXIndex);
		}
	}

}
