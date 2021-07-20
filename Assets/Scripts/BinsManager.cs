using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.SimulationLogic;
using Assets.Scripts.SimulationLogic.Models;
using ScriptDefinitions.Assets.Scripts.SimulationLogic;
using UnityEngine;

public class BinsManager : MonoBehaviour
{
	private LogicController _logicController;
	private Vector2 _bin0Posn;
	private float _binXSpacing;
	private float _binPieceYSpacing;
	private PieceFactory _pieceFactory;

	public void InitializeGameSettings(PieceFactory pieceFactory, Vector2 bin0Posn, float binXSpacing, float binPieceYSpacing)
	{
		_pieceFactory = pieceFactory;
		_bin0Posn = bin0Posn;
		_binXSpacing = binXSpacing;
		_binPieceYSpacing = binPieceYSpacing;
	}

	public void InitializeLevelSettings(LogicController logicController)
	{
		_logicController = logicController;
		CreateBinsForLevel();
	}

	public List<float> GetBinXPosns()
	{
		List<float> xPosns = new List<float>();
		for (int i = 0; i < _logicController.BinsLogic.Bins.Count; i++)
		{
			xPosns.Add(_bin0Posn.x + i * _binXSpacing);
		}
		return xPosns;
	}

	public void CreateBinsForLevel()
	{
		try
		{
			for (int binIdx = 0; binIdx < _logicController.BinsLogic.Bins.Count; binIdx++)
			{
				for (int binCell = 0; binCell < _logicController.BinsLogic.Bins[0].Count; binCell++ )
				{
					int pieceId = _logicController.BinsLogic.Bins[binIdx][binCell].Id;
					float xPosn = _bin0Posn.x + binIdx * _binXSpacing;
					float yPosn = _bin0Posn.y + binCell * _binPieceYSpacing;
					_pieceFactory.CreatePiece(transform, pieceId, xPosn, yPosn);
				}
			}


			//float x = _bin0Posn.x;
			//_pieceFactory.CreatePiece(transform, 13, x, _bin0Posn.y);
			//_pieceFactory.CreatePiece(transform, 4, x, _bin0Posn.y + _binPieceYSpacing);
			//_pieceFactory.CreatePiece(transform, 0, x, _bin0Posn.y + 2 * _binPieceYSpacing);
			//_pieceFactory.CreatePiece(transform, 43, x, _bin0Posn.y + 3 * _binPieceYSpacing);

			//x = _bin0Posn.x + _binXSpacing;
			//_pieceFactory.CreatePiece(transform, 55, x, _bin0Posn.y);

			//_pieceFactory.CreatePiece(transform, 18, x, _bin0Posn.y + _binPieceYSpacing);
			//_pieceFactory.CreatePiece(transform, 2, x, _bin0Posn.y + 2 * _binPieceYSpacing);
			//_pieceFactory.CreatePiece(transform, 21, x, _bin0Posn.y + 3 * _binPieceYSpacing);

			//x = _bin0Posn.x + 2 * _binXSpacing;
			//_pieceFactory.CreatePiece(transform, 60, x, _bin0Posn.y);
			//_pieceFactory.CreatePiece(transform, 44, x, _bin0Posn.y + _binPieceYSpacing);
			//_pieceFactory.CreatePiece(transform, 30, x, _bin0Posn.y + 2 * _binPieceYSpacing);
			//_pieceFactory.CreatePiece(transform, 3, x, _bin0Posn.y + 3 * _binPieceYSpacing);

			//x = _bin0Posn.x + 3 * _binXSpacing;
			//_pieceFactory.CreatePiece(transform, 31, x, _bin0Posn.y);
			//_pieceFactory.CreatePiece(transform, 42, x, _bin0Posn.y + _binPieceYSpacing);
			//_pieceFactory.CreatePiece(transform, 60, x, _bin0Posn.y + 2 * _binPieceYSpacing);
			//_pieceFactory.CreatePiece(transform, 15, x, _bin0Posn.y + 3 * _binPieceYSpacing);

			//x = _bin0Posn.x + 4 * _binXSpacing;
			//_pieceFactory.CreatePiece(transform, 3, x, _bin0Posn.y);
			//_pieceFactory.CreatePiece(transform, 12, x, _bin0Posn.y + _binPieceYSpacing);
			//_pieceFactory.CreatePiece(transform, 49, x, _bin0Posn.y + 2 * _binPieceYSpacing);
			//_pieceFactory.CreatePiece(transform, 36, x, _bin0Posn.y + 3 * _binPieceYSpacing);
		}
		catch (Exception ex)
		{
			Debug.LogException(ex);
		}
	}

	// Kicked off from a UnityEvent on Player Controller
	public void DropBinPiece(int binIdx)
	{
		// shorten some variables for ease of viewing here
		BinsLogic binsLgc = _logicController.BinsLogic;

		SimPiece simPiece = binsLgc.DropPieceFromBin(binIdx);
		// get the newly created logical piece at the top of the selected bin, to add it to the screen
		int newPieceId = binsLgc.Bins[binIdx][binsLgc.NumCellsPerBin - 1].Id;
		float xPosn = _bin0Posn.x + binIdx * _binXSpacing;
		float yPosn = _bin0Posn.y + binsLgc.NumCellsPerBin * _binPieceYSpacing;
		_pieceFactory.CreatePiece(transform, newPieceId, xPosn, yPosn);
		Debug.Log($"Received request to drop bin piece from bin:{binIdx}, pieceId:{simPiece.Id}");
	}
}
