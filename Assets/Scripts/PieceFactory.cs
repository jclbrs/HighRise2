using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.SimulationLogic;
using Assets.Scripts.SimulationLogic.Models;
using UnityEngine;

public class PieceFactory : MonoBehaviour
{
	private GameObject _piecePrefab;
	private GameObject _destroyingPieceParticlesPrefab;
	private float _pieceDropSpeed;
	private float _pieceXSpeed;
	private float _blockWidth;
	private float _blockHeight;
	private List<Color> _blockColors;
	private EventsManager _eventsManager;

	public void InitializeGameSettings(PieceFactoryDIWrapper diWrapper)
	{
		_piecePrefab = diWrapper.PiecePrefab;
		_destroyingPieceParticlesPrefab = diWrapper.DestroyingPieceParticlesPrefab;
		_pieceDropSpeed = diWrapper.PieceDropFromBinYSpeed;
		_pieceXSpeed = diWrapper.PieceXSpeed;
		_eventsManager = diWrapper.EventsManager;
		_blockWidth = diWrapper.BlockWidth;
		_blockHeight = diWrapper.BlockHeight;
		_blockColors = diWrapper.BlockColors;
	}

	public GameObject CreatePiece(Transform parentTransform, int pieceId, float x, float y)
	{
		GameObject selectedPiece = Instantiate(_piecePrefab, parentTransform);
		selectedPiece.transform.localPosition = new Vector3(x, y);

		PieceManager newPieceManager = selectedPiece.GetComponent<PieceManager>();
		newPieceManager.yMove = 0.0f;
		newPieceManager.PieceDropSpeed = _pieceDropSpeed;
		newPieceManager.XSpeed = _pieceXSpeed;
		newPieceManager.Initialize(_eventsManager,_blockWidth, _blockHeight, _destroyingPieceParticlesPrefab);


		SimPiece simPiece = new SimPiece(SimPieceLibrary.SimPieces[pieceId]);
		newPieceManager.ConstructPieceShape(selectedPiece, simPiece);

		Color pieceColor = CalculatePieceColor(newPieceManager.SimPiece.Id);
		SpriteRenderer[] renderers = newPieceManager.gameObject.GetComponentsInChildren<SpriteRenderer>();
		foreach (SpriteRenderer renderer in renderers)
		{
			if (renderer.transform.name.ToLower().Contains("block"))
				renderer.color = pieceColor;
		}

		PlayerController playerController = GameObject.Find("Player").GetComponent<PlayerController>();
		return selectedPiece;
	}

	// This assumes there are 8 different pieces per level, and the color changes for each associated level piece
	private Color CalculatePieceColor(int pieceId)
	{
		int colorIdx = (int)(pieceId / 8);
		Color levelColor = _blockColors[colorIdx];
		return levelColor;
	}
}
