using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.SimulationLogic;
using Assets.Scripts.SimulationLogic.Models;
using ScriptDefinitions.Assets.Scripts.SimulationLogic;
using UnityEngine;

public class GameController : MonoBehaviour
{
	private ConfigSettings _config;
	private SpringboardController _springboardController;
	private PieceFactory _pieceFactory;
	private PlayerController _playerController;
	private BinsManager _binsManager;
	private LogicController _logicController;
	private EventsManager _eventsManager;
	private int _currentLevel;

	void Start()
	{
		// Inject the elements from ConfigSettings
		_config = GetComponent<ConfigSettings>();
		_currentLevel = _config.InitialLevel;
		_eventsManager = GetComponent<EventsManager>();
		_springboardController = _config.SpringboardObject.GetComponent<SpringboardController>();
		_playerController = _config.PlayerObject.GetComponent<PlayerController>();
		_pieceFactory = GetComponent<PieceFactory>();
		_binsManager = _config.BinsObject.GetComponent<BinsManager>();

		// Initialize the various components
		_logicController = new LogicController(_currentLevel, _config.NumBins, _config.NumCellsPerBin);
		_pieceFactory.InitializeGameSettings(_config.PiecePrefab, _config.PieceDropSpeed, _config.PieceXSpeed, _config.BlockWidth, _eventsManager);
		List<float> springboardXPosns = _springboardController.GetSpringboardXPosns();
		_playerController.InitializeGameSettings(springboardXPosns,_config.PieceXSpeed, _eventsManager);
		_binsManager.InitializeGameSettings(_pieceFactory, _config.Bin0Posn, _config.BinXSpacing, _config.BinPieceYSpacing, _config.PieceYFromBinDrop);
		_springboardController.InitializeGameSettings(_config.SpringboardYSpeed, _config.SpringboardMoveHeight);

		// set up subscriptions to events
		_eventsManager.BinPieceSelected += _binsManager.OnDroppingBinPieceToPlayer;
		_eventsManager.PieceDroppedFromBin += _playerController.OnBeginMovePieceToSpringboard;
		_eventsManager.PieceDroppedToSpringboard += _springboardController.OnPieceDroppedToSpringboard;
		_eventsManager.SpringboardTriggered += _springboardController.OnSpringboardTriggered;

		SetupLevel(_currentLevel);
	}

	private void OnDestroy()
	{
		_eventsManager.BinPieceSelected -= _binsManager.OnDroppingBinPieceToPlayer;
		_eventsManager.PieceDroppedFromBin -= _playerController.OnBeginMovePieceToSpringboard;
		_eventsManager.PieceDroppedToSpringboard -= _springboardController.OnPieceDroppedToSpringboard;
		_eventsManager.SpringboardTriggered -= _springboardController.OnSpringboardTriggered;
	}

	// Some of these settings might be the same throughout the game, but to stay flexible, they are being populated for each level
	public void SetupLevel(int level)
	{
		// Set up x positions that player can move to/from
		_logicController.SetupLevel(level);
		_binsManager.InitializeLevelSettings(_logicController);

		List<float> binXPosns = _binsManager.GetBinXPosns();
		_playerController.InitializeLevelSettings(_logicController, binXPosns, _config.PlayerXOffsetFromBin);


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