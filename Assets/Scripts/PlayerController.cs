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
	private LogicController _logicController;
	private EventsManager _eventsManager;
	private PieceManager _attachedPieceManager;

	private float _xSpeed;

	private PlayerState _currentState;
	private int _currentXIndex; // x-index is based on the current state. If on springboard, based on which spring.  If under bins, based on which bin
	private int _destinationXIndex;
	private float _destinationXCoord;
	private bool _isMoveRight; // -1 for left, +1 for right

	private float _pieceXWidth;

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
		HandlePlayerAction();
	}

	private void HandlePlayerAction()
	{
		switch (_currentState)
		{
			case PlayerState.IdleUnderBin:
				// no actions
				break;
			case PlayerState.MovingToBin:
				MoveToNextBin();
				break;
			case PlayerState.WaitingForBinPiece:
				// no action for the moment
				break;
			case PlayerState.PushingPieceToSpringboard:
				//Debug.Log($"Moving piece: x:{transform.position.x}, speed:{_xSpeed}, Dest:{_destinationXCoord}");
				transform.position = new Vector3(transform.position.x - _xSpeed, transform.position.y);
				if (transform.position.x <= _destinationXCoord + _pieceXWidth)
				{
					transform.position = new Vector3(_destinationXCoord + _pieceXWidth, transform.position.y); // if overshot, set to the exact desired position
					_currentState = PlayerState.HoldingPieceOnSpringboard; 
					_currentXIndex = _destinationXIndex;
				}
				break;
			case PlayerState.HoldingPieceOnSpringboard:
				// for now, no action
				break;
			case PlayerState.MovingToNextSpring:
				int direction = (_isMoveRight ? 1 : -1);
				transform.position = new Vector3(transform.position.x + direction * _xSpeed, transform.position.y);
				float destXIncludingPlayerOffset = _destinationXCoord + _pieceXWidth * _attachedPieceManager.BlockWidth;
				if (_isMoveRight)
				{
					if (transform.position.x >= destXIncludingPlayerOffset)
					{
						_currentState = PlayerState.HoldingPieceOnSpringboard; // joe maybe change to reflect that player is still holding the piece above spring
						_currentXIndex = _destinationXIndex;
					}
				}
				else
				{
					if (transform.position.x <= destXIncludingPlayerOffset)
					{
						_currentState = PlayerState.HoldingPieceOnSpringboard; // joe maybe change to reflect that player is still holding the piece above spring
						_currentXIndex = _destinationXIndex;
					}

				}

				break;
			default:
				throw new NotImplementedException($"Player State ({_currentState}) not handled yet");
		}
	}

	private void MoveToNextBin()
	{
		if (_isMoveRight)
		{
			if (transform.position.x >= _destinationXCoord)
			{
				transform.position = new Vector3(_destinationXCoord, transform.position.y); // set to exact destination
				_currentState = PlayerState.IdleUnderBin;
				_currentXIndex = _destinationXIndex;
			}
			else
				transform.position = new Vector3(transform.position.x + _xSpeed, transform.position.y);
		}
		else
		{
			if (transform.position.x <= _destinationXCoord)
			{
				transform.position = new Vector3(_destinationXCoord, transform.position.y); // set to exact destination
				_currentState = PlayerState.IdleUnderBin;
				_currentXIndex = _destinationXIndex;
			}
			else
				transform.position = new Vector3(transform.position.x - _xSpeed, transform.position.y);
		}
	}

	// Invoked by a PieceManager via Event, when piece just fell to ground from bin
	public void OnBeginMovePieceToSpringboard(PieceManager pieceManager)
	{
		_attachedPieceManager = pieceManager;
		_currentState = PlayerState.PushingPieceToSpringboard;
		PositionPieceNextToPlayer(pieceManager);

		_currentState = PlayerState.PushingPieceToSpringboard;
		int springIdx;
		if (_logicController.SpringboardLogic.TryMovePieceToAvailableSpring(pieceManager.SimPiece.Id, pieceManager.SimPiece.GetSimWidth(), out springIdx))
		{
			_destinationXIndex = springIdx;
			_destinationXCoord = _springboardXPosns[springIdx];
			_isMoveRight = false;
			pieceManager.XSpeed = _xSpeed; // have piece's speed match the player's
			pieceManager.DestinationXPosn = _destinationXCoord;
		}
		else
		{
			// joe - piece doesn't fit on springboard, so move to area where it will be sent to garbage
			throw new NotImplementedException("Need move to garbage area");
		}
		//Debug.Log($"1st avail spring:{firstAvailableSpringIdx}");
	}

	private void PositionPieceNextToPlayer(PieceManager pieceManager)
	{
		// 1st: since the piece is now under the player's control, move the game object from a Bins child object to a Player child object
		// so they will move together

		// Position the exact position of the player first (since it will be the parent)
		_pieceXWidth = pieceManager.GetXWidth();
		float playerX = pieceManager.transform.position.x + _pieceXWidth;
		transform.position = new Vector3(playerX, transform.position.y);
		pieceManager.gameObject.transform.SetParent(gameObject.transform);
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
						_destinationXIndex = _currentXIndex + 1;
						_destinationXCoord = _playerXBelowBins[_destinationXIndex];
						_isMoveRight = true;
					}
					break;
				case PlayerState.MovingToBin: // player is already moving.  Ignore the extra key entry
					break;
				case PlayerState.HoldingPieceOnSpringboard:
					//Debug.Log($"xIdx:{_currentXIndex}");
					if (_currentXIndex <= 5 - _pieceXWidth) // 5 is the hard coded index for the right-most
					{
						_currentState = PlayerState.MovingToNextSpring;
						_destinationXIndex = _currentXIndex + 1;
						_destinationXCoord = _springboardXPosns[_destinationXIndex];
						_isMoveRight = true;
					}
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
						_destinationXIndex = _currentXIndex - 1;
						_destinationXCoord = _playerXBelowBins[_destinationXIndex];
						_isMoveRight = false;
					}
					break;
				case PlayerState.MovingToBin: // player is already moving.  Ignore the extra key entry
					break;
				case PlayerState.HoldingPieceOnSpringboard:
					if (_currentXIndex > 0) 
					{
						_currentState = PlayerState.MovingToNextSpring;
						_destinationXIndex = _currentXIndex-1;
						_destinationXCoord = _springboardXPosns[_destinationXIndex];
						_isMoveRight = false;
					}
					break;
				default:
					throw new Exception($"actions for {_currentState} not written yet");
			}
		}
		if (Input.GetKeyDown("space"))
		{
			switch (_currentState)
			{
				case PlayerState.IdleUnderBin:
					_currentState = PlayerState.WaitingForBinPiece;
					_eventsManager.OnBinPieceSelected(_currentXIndex);
					break;
				case PlayerState.HoldingPieceOnSpringboard:
					_logicController.SpringboardLogic.DropPieceOntoSpringboard(_attachedPieceManager.SimPiece.Id, _attachedPieceManager.SimPiece.GetSimWidth(), _currentXIndex);
					_currentState = PlayerState.MovingToBin;
					_destinationXIndex = 0;
					_destinationXCoord = _playerXBelowBins[_destinationXIndex];
					_isMoveRight = true;
					_eventsManager.OnPieceDroppedToSpringboard(_currentXIndex,_attachedPieceManager);
					_attachedPieceManager.BeginDropUntilCollision();
					_attachedPieceManager = null;
					break;
			}
			
		}
		if (Input.GetKeyDown(KeyCode.Return)){
			Debug.Log("Return pressed");
			_logicController.SpringboardLogic.ClearSpringboard();
			_eventsManager.OnSpringboardTriggered();
		}
	}

}
