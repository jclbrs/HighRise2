using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using Assets.Scripts.Enums;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerWantsBinPieceEvent BinPieceDropRequested;

    private List<float> _playerXBelowBins;
    private List<float> _springboardXPosns;
    private PlayerState _currentState;
    private int _currentXIndex;
    private float _destinationXCoord;
    private float _speed;
    private bool _isMoveRight; // -1 for left, +1 for right

	private void Start()
	{
	}

	void Update()
    {
        HandleInput();

        if (_currentState == PlayerState.MovingToBin)
		{
            if (_isMoveRight)
            {
                if (transform.position.x >= _destinationXCoord)
                {
                    _currentState = PlayerState.IdleUnderBin;
                    _currentXIndex++;
                }
                else
                    transform.position = new Vector3(transform.position.x + _speed, transform.position.y);
            } else
			{
                if (transform.position.x <= _destinationXCoord)
                {
                    _currentState = PlayerState.IdleUnderBin;
                    _currentXIndex--;
                }
                else
                    transform.position = new Vector3(transform.position.x - _speed, transform.position.y);
            }
        }
    }

	public void InitializeSettings(List<float> binXPosns, float xOffSetFromBin, List<float> springboardXPosns, float playerSpeed)
	{
        _springboardXPosns = springboardXPosns;
        _playerXBelowBins = new List<float>();
        foreach (float binXPosn in binXPosns)
            _playerXBelowBins.Add(binXPosn + xOffSetFromBin);
        _currentState = PlayerState.IdleUnderBin;
        _currentXIndex = 0;
        transform.position = new Vector3(_playerXBelowBins[0], transform.position.y);
        _speed = playerSpeed;
	}

    private void HandleInput()
    {
        if (Input.GetKeyDown("right"))
        {
            switch (_currentState)
            {
                case PlayerState.IdleUnderBin:
                    if (_currentXIndex < _playerXBelowBins.Count-1)
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
            BinPieceDropRequested.Invoke(_currentXIndex);
        }
    }
}
