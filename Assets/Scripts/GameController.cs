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
	private int _currentLevel;

	void Start()
	{
		_currentLevel = 1;
		_config = GetComponent<ConfigSettings>();

		_binsManager = GetComponent<BinsManager>();
		_binsManager.InitializeSettings(_config.NumBins, _config.Bin0Posn, _config.BinXSpacing, _config.BinPieceYSpacing);

		_pieceFactory = GetComponent<PieceFactory>();
		_pieceFactory.InitializeSettings(_config.PiecePrefab);

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