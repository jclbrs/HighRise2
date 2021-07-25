using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Enums;
using Assets.Scripts.SimulationLogic;
using Assets.Scripts.SimulationLogic.Models;
using ScriptDefinitions.Assets.Scripts.SimulationLogic;
using UnityEngine;
using static PlayerController;

public class BinsManager : MonoBehaviour
{
	[SerializeField] private PlayerController _playerController;
	private LogicController _logicController;
	private Vector2 _bin0Posn;
	private float _binXSpacing;
	private float _binPieceYSpacing;
	private float _yDropPointToPlayer;
	private PieceFactory _pieceFactory;
	private Dictionary<int, List<GameObject>> _bins;  // maintain a reference to the actual game objects in the bin

	public void InitializeGameSettings(PieceFactory pieceFactory, Vector2 bin0Posn, float binXSpacing, float binPieceYSpacing, float yDropPointToPlayer)
	{
		_pieceFactory = pieceFactory;
		_bin0Posn = bin0Posn;
		_binXSpacing = binXSpacing;
		_binPieceYSpacing = binPieceYSpacing;
		_yDropPointToPlayer = yDropPointToPlayer;
		_bins = new Dictionary<int, List<GameObject>>();
		//_playerController.BinPieceDropRequested += DropBinPieceToPlayer(int binIdx);
	}

	public void InitializeLevelSettings(LogicController logicController)
	{
		_logicController = logicController;
		CreateBinsForLevel();
	}

	public List<float> GetBinXPosns()
	{
		List<float> xPosns = new List<float>();
		for (int i = 0; i < _logicController.BinsLogic.SimBins.Count; i++)
		{
			xPosns.Add(_bin0Posn.x + i * _binXSpacing);
		}
		return xPosns;
	}

	public void CreateBinsForLevel()
	{
		try
		{
			for (int binIdx = 0; binIdx < _logicController.BinsLogic.SimBins.Count; binIdx++)
			{
				List<GameObject> piecesInBin = new List<GameObject>();
				for (int binCell = 0; binCell < _logicController.BinsLogic.SimBins[0].Count; binCell++ )
				{
					int pieceId = _logicController.BinsLogic.SimBins[binIdx][binCell].Id;
					float xPosn = _bin0Posn.x + binIdx * _binXSpacing;
					float yPosn = _bin0Posn.y + binCell * _binPieceYSpacing;
					GameObject piece = _pieceFactory.CreatePiece(transform, pieceId, xPosn, yPosn);
					piece.GetComponent<PieceManager>().CurrentState = PieceState.InBin;
					piecesInBin.Add(piece);
				}
				_bins[binIdx] = piecesInBin;
			}
		}
		catch (Exception ex)
		{
			Debug.LogException(ex);
		}
	}

	// Kicked off from a UnityEvent on Player Controller
	public void OnDroppingBinPieceToPlayer(int binIdx)
	{
		BinsLogic binsLgc = _logicController.BinsLogic;
		SimPiece simPiece = binsLgc.DropPieceFromBin(binIdx);
		Debug.Log($"Received request to drop bin piece from bin:{binIdx}, pieceId:{simPiece.Id}");

		// Drop the selected piece
		PieceManager pieceManager = _bins[binIdx][0].GetComponent<PieceManager>();
		pieceManager.CurrentState = PieceState.DroppingFromBinToPlayer;
		pieceManager.BeginDropFromBinToPlayer(_yDropPointToPlayer);

		// get the newly created logical piece at the top of the selected bin, to add it to the screen
		int newPieceId = binsLgc.SimBins[binIdx][binsLgc.NumCellsPerBin - 1].Id;
		float xPosn = _bin0Posn.x + binIdx * _binXSpacing;
		float yPosn = _bin0Posn.y + binsLgc.NumCellsPerBin * _binPieceYSpacing;
		GameObject addedPiece = _pieceFactory.CreatePiece(transform, newPieceId, xPosn, yPosn);
		pieceManager = addedPiece.GetComponent<PieceManager>();
		pieceManager.BeginDropToYPosnInBin(_bin0Posn.y + (binsLgc.NumCellsPerBin - 1) * _binPieceYSpacing);

		// Move the remaining bin pieces down to the next position
		for (int i = 1; i < binsLgc.NumCellsPerBin; i++)
		{
			pieceManager = _bins[binIdx][i].GetComponent<PieceManager>();
			yPosn = _bin0Posn.y + (i - 1) * _binPieceYSpacing;
			pieceManager.BeginDropToYPosnInBin(yPosn);
		}
	}
}
