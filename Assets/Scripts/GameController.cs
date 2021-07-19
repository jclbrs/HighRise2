using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.SimulationLogic;
using Assets.Scripts.SimulationLogic.Models;
using UnityEngine;

public class GameController : MonoBehaviour
{
	private ConfigSettings _config;
	private BinsManager _binsManager;
	private PieceFactory _pieceFactory;
	private GameObject _playerObject;
	private GameObject _springboardObject;
	private int _currentLevel;

	void Start()
	{
		_currentLevel = 1;
		_config = GetComponent<ConfigSettings>();

		// The Piece Factory doesn't need to be it's own game object, so it is also another component
		_pieceFactory = GetComponent<PieceFactory>();
		_pieceFactory.InitializeSettings(_config.PiecePrefab);

		// Player and springboard do not change throughout the game, so the controller is accessing them directly as game objects
		_playerObject = _config.PlayerObject;
		_springboardObject = _config.SpringboardObject;

		
		_binsManager = _config.BinsObject.GetComponent<BinsManager>();
		_binsManager.InitializeSettings(_config.NumBins, _config.Bin0Posn, _config.BinXSpacing, _config.BinPieceYSpacing);

		// Set up x positions that player can move to/from
		List<float> binXPosns = _binsManager.GetBinXPosns();
		List<float> springboardXPosns = _springboardObject.GetComponent<SpringboardController>().GetSpringboardXPosns();
		_playerObject.GetComponent<PlayerController>().InitializeSettings(binXPosns, _config.PlayerXOffsetFromBin, springboardXPosns, _config.PlayerSpeed);

		_binsManager.CreateBinsForLevel(_currentLevel, _pieceFactory);
	}
}

/*
 So you know how you have to be careful with container objects because if you mess with them in one place while adding or removing in another place, you get all kinds of fun errors?
And in realtime games, that's a serious concern and frequent problem.
So... copy/pasting some code from this Poker game I'm writing for Gameboard...
        public void NewPlayerJoined(PokerPlayerSceneObject inPlayerObject)
        {
            lock(PlayerValues)
            {
                PokerPlayerValues playerValues = new PokerPlayerValues()
                {
                    playerObject = inPlayerObject,
                };

                PlayerValues.Add(playerValues);
            }
        }
The player joins, I put them in a local object that maintains game specific data, then store them in the PlayerValues list.
Which I've wrapped in a Lock statement.
This prevents ANYTHING else from touching PlayerValues until it's unlocked.
And C# essentially queues it up, so it will wait until it's unlocked before doing other things with it.
So you don't lose anything, and you don't have to worry about sequence breaking the code and hitting errors.

*/