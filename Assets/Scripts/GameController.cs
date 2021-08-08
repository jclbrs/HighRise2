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
	private SprungPiecesController _sprungPiecesController;
	private LandingZoneController _landingZoneController;
	private RowArrowController _rowArrowController;
	private EventsManager _eventsManager;
	private int _currentLevel;

	void Start()
	{
		// Get the components that are part of this game object
		_eventsManager = GetComponent<EventsManager>();
		_config = GetComponent<ConfigSettings>();
		_pieceFactory = GetComponent<PieceFactory>();

		// Get the components that are defined in ConfigSettings
		_springboardController = _config.SpringboardObject.GetComponent<SpringboardController>();
		_playerController = _config.PlayerObject.GetComponent<PlayerController>();
		_binsManager = _config.BinsObject.GetComponent<BinsManager>();
		_sprungPiecesController = _config.SprungPiecesContainer.GetComponent<SprungPiecesController>();
		_landingZoneController = _config.LandingZoneObject.GetComponent<LandingZoneController>();
		GameObject RowArrowObject = _config.RowArrowObject;
		_rowArrowController = RowArrowObject.GetComponent<RowArrowController>();

		// Get the starting level from ConfigSettings (Normally this will be 1, but can change for testing purposes)
		_currentLevel = _config.InitialLevel;

		// Initialize the various components
		_logicController = new LogicController();

		PieceFactoryDIWrapper pieceFactoryDIWrapper = new PieceFactoryDIWrapper
		{
			PiecePrefab = _config.PiecePrefab,
			DestroyingPieceParticlesPrefab = _config.DestroyingPieceParticlesPrefab,
			PieceDropFromBinYSpeed = _config.PieceDropFromBinYSpeed,
			PieceXSpeed = _config.PieceXSpeed,
			BlockWidth = _config.BlockWidth,
			EventsManager = _eventsManager,
			BlockColors = _config.BlockColors

		};
		_pieceFactory.InitializeGameSettings(pieceFactoryDIWrapper);

		_springboardController.InitializeGameSettings(_config.SpringboardYSpeed, _config.SpringboardMoveHeight);

		BinsDIWrapper binsDIWrapper = new BinsDIWrapper
		{
			PieceFactory = _pieceFactory,
			Bin0Posn = _config.Bin0Posn,
			BinXSpacing = _config.BinXSpacing,
			BinPieceYSpacing = _config.BinPieceYSpacing,
			PieceYFromBinDrop = _config.PieceYFromBinDrop
		};
		_binsManager.InitializeGameSettings(binsDIWrapper);

		SprungPiecesDIWrapper sprungPiecesDIWrapper = new SprungPiecesDIWrapper
		{
			EventsManager = _eventsManager,
			SpringOverPoints = _config.SpringOverPoints,
			FirstPieceSpringX = _config.FirstPieceSpringX,
			PieceSpringXSpacing = _config.BlockWidth,
			BlockWidth = _config.BlockWidth,
			LogicController = _logicController,
			SprungYSpeed = _config.SprungYSpeed
		};
		_sprungPiecesController.InitializeGameSettings(sprungPiecesDIWrapper);

		List<float> springboardXPosns = _sprungPiecesController.GetInitialSpringXPosns();

		PlayerDIWrapper playerDIWrapper = new PlayerDIWrapper
		{
			//springboardXPosns, _config.PlayerXSpeed, _config.PlayerYSpeed, _config.PlayerSuccesDestination, _eventsManager
			SpringboardXPosns = springboardXPosns,
			FloorY = _config.PlayerFloorY,
			XSpeed = _config.PlayerXSpeed,
			YSpeed = _config.PlayerYSpeed,
			SuccessDestination = _config.PlayerSuccesDestination,
			EventsManager = _eventsManager
		};
		_playerController.InitializeGameSettings(playerDIWrapper);

		// set up subscriptions to events
		_eventsManager.BinPieceSelected += _binsManager.OnDroppingBinPieceToPlayer;
		_eventsManager.PieceDroppedFromBin += _playerController.OnBeginMovePieceToSpringboard;
		_eventsManager.PieceDroppedToSpringboard += _springboardController.OnPieceDroppedToSpringboard;
		_eventsManager.SpringboardTriggered += _springboardController.OnSpringboardTriggered;
		//_eventsManager.PiecesLanding += _landingZoneController.OnPiecesLanding;
		_eventsManager.PiecesLanding += _rowArrowController.OnPiecesLanding;
		_eventsManager.SuccessfulLevel += _playerController.OnSuccessfulLevel;
		_eventsManager.CanStartNextLevel += OnStartNextLevel; // 

		SetupLevel(_currentLevel);
	}

	private void OnStartNextLevel()
	{
		_currentLevel++;
		Debug.Log($"Ready to start next level:{_currentLevel}");
		//_logicController
		SetupLevel(_currentLevel);
	}

	private void OnDestroy()
	{
		_eventsManager.BinPieceSelected -= _binsManager.OnDroppingBinPieceToPlayer;
		_eventsManager.PieceDroppedFromBin -= _playerController.OnBeginMovePieceToSpringboard;
		_eventsManager.PieceDroppedToSpringboard -= _springboardController.OnPieceDroppedToSpringboard;
		_eventsManager.SpringboardTriggered -= _springboardController.OnSpringboardTriggered;
		//_eventsManager.PiecesLanding -= _landingZoneController.OnPiecesLanding;
		_eventsManager.PiecesLanding -= _rowArrowController.OnPiecesLanding;
		_eventsManager.SuccessfulLevel -= _playerController.OnSuccessfulLevel;
		_eventsManager.CanStartNextLevel -= OnStartNextLevel;
	}

	// Some of these settings might be the same throughout the game, but to stay flexible, they are being populated for each level
	public void SetupLevel(int level)
	{
		_logicController.SetupLevel(_currentLevel, _config.NumBins, _config.NumCellsPerBin, _config.LandingSuccessRow);
		_binsManager.InitializeLevelSettings(_logicController);
		_landingZoneController.SetupLevel();
		_rowArrowController.InitializeLevelSettings();

		List<float> binXPosns = _binsManager.GetBinXPosns();
		_playerController.InitializeLevelSettings(_logicController, binXPosns, _config.PlayerXOffsetFromBin);


	}
}