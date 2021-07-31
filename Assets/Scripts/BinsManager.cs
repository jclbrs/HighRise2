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
				for (int binCell = 0; binCell < _logicController.BinsLogic.SimBins[0].Count; binCell++)
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
			// Testing
			//PieceManager pc0 = _bins[0][0].GetComponent<PieceManager>();
			//float x0 = pc0.transform.position.x;
			//float y0 = pc0.transform.position.y;
			//PieceManager pc1 = _bins[0][1].GetComponent<PieceManager>();
			//float x1 = pc1.transform.position.x;
			//float y1 = pc1.transform.position.y;
			//PieceManager pc2 = _bins[0][2].GetComponent<PieceManager>();
			//float x2 = pc2.transform.position.x;
			//float y2 = pc2.transform.position.y;
			//PieceManager pc3 = _bins[0][3].GetComponent<PieceManager>();
			//float x3 = pc3.transform.position.x;
			//float y3 = pc3.transform.position.y;
			//int id0 = pc0.SimPiece.Id;
			//int id1 = pc1.SimPiece.Id;
			//int id2 = pc2.SimPiece.Id;
			//int id3 = pc3.SimPiece.Id;
			//Debug.Log($"Created: {id0}-({x0}/{y0}) .. {id1}-({x1}/{y1}) .. {id2}-({x2}/{y2}) .. {id3}-({x3}/{y3})");
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


		// testing before
		//PieceManager pc0 = _bins[0][0].GetComponent<PieceManager>();
		//float x0 = pc0.transform.position.x;
		//float y0 = pc0.transform.position.y;
		//PieceManager pc1 = _bins[0][1].GetComponent<PieceManager>();
		//float x1 = pc1.transform.position.x;
		//float y1 = pc1.transform.position.y;
		//PieceManager pc2 = _bins[0][2].GetComponent<PieceManager>();
		//float x2 = pc2.transform.position.x;
		//float y2 = pc2.transform.position.y;
		//PieceManager pc3 = _bins[0][3].GetComponent<PieceManager>();
		//float x3 = pc3.transform.position.x;
		//float y3 = pc3.transform.position.y;
		//int id0 = pc0.SimPiece.Id;
		//int id1 = pc1.SimPiece.Id;
		//int id2 = pc2.SimPiece.Id;
		//int id3 = pc3.SimPiece.Id;
		//Debug.Log($"b4: {id0}-({x0}/{y0}) .. {id1}-({x1}/{y1}) .. {id2}-({x2}/{y2}) .. {id3}-({x3}/{y3})");

		// Move the remaining bin pieces down to the next position
		float yPosn;
		for (int i = 1; i < binsLgc.NumCellsPerBin; i++)
		{
			pieceManager = _bins[binIdx][i].GetComponent<PieceManager>();
			yPosn = _bin0Posn.y + (i - 1) * _binPieceYSpacing;
			pieceManager.BeginDropToYPosnInBin(yPosn);

		}
		for (int i = 1; i < binsLgc.NumCellsPerBin; i++)
		{
			_bins[binIdx][i - 1] = _bins[binIdx][i];
		}

		// get the newly created logical piece at the top of the selected bin, to add it to the screen
		int newPieceId = binsLgc.SimBins[binIdx][binsLgc.NumCellsPerBin - 1].Id;
		float xPosn = _bin0Posn.x + binIdx * _binXSpacing;
		yPosn = _bin0Posn.y + binsLgc.NumCellsPerBin * _binPieceYSpacing;
		GameObject addedPiece = _pieceFactory.CreatePiece(transform, newPieceId, xPosn, yPosn);
		pieceManager = addedPiece.GetComponent<PieceManager>();
		pieceManager.BeginDropToYPosnInBin(_bin0Posn.y + (binsLgc.NumCellsPerBin - 1) * _binPieceYSpacing);
		_bins[binIdx][binsLgc.NumCellsPerBin - 1] = pieceManager.gameObject;

		// testing after
		//pc0 = _bins[0][0].GetComponent<PieceManager>();
		//x0 = pc0.transform.position.x;
		//y0 = pc0.transform.position.y;
		//pc1 = _bins[0][1].GetComponent<PieceManager>();
		//x1 = pc1.transform.position.x;
		//y1 = pc1.transform.position.y;
		//pc2 = _bins[0][2].GetComponent<PieceManager>();
		//x2 = pc2.transform.position.x;
		//y2 = pc2.transform.position.y;
		//pc3 = _bins[0][3].GetComponent<PieceManager>();
		//x3 = pc3.transform.position.x;
		//y3 = pc3.transform.position.y;
		//id0 = pc0.SimPiece.Id;
		//id1 = pc1.SimPiece.Id;
		//id2 = pc2.SimPiece.Id;
		//id3 = pc3.SimPiece.Id;
		//Debug.Log($"after: {id0}-({x0}/{y0}) .. {id1}-({x1}/{y1}) .. {id2}-({x2}/{y2}) .. {id3}-({x3}/{y3})");
	}
}
