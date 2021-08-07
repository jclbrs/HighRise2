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
	private EventsManager _eventsManager;

	public void InitializeGameSettings(PieceFactoryDIWrapper diWrapper)
	{
		_piecePrefab = diWrapper.PiecePrefab;
		_destroyingPieceParticlesPrefab = diWrapper.DestroyingPieceParticlesPrefab;
		_pieceDropSpeed = diWrapper.PieceDropFromBinYSpeed;
		_pieceXSpeed = diWrapper.PieceXSpeed;
		_eventsManager = diWrapper.EventsManager;
		_blockWidth = diWrapper.BlockWidth;
	}

	public GameObject CreatePiece(Transform parentTransform, int pieceId, float x, float y)
	{
		GameObject selectedPiece = Instantiate(_piecePrefab, parentTransform);
		selectedPiece.transform.localPosition = new Vector3(x, y);

		PieceManager newPieceManager = selectedPiece.GetComponent<PieceManager>();
		newPieceManager.yMove = 0.0f;
		newPieceManager.PieceDropSpeed = _pieceDropSpeed;
		newPieceManager.XSpeed = _pieceXSpeed;
		newPieceManager.Initialize(_eventsManager,_blockWidth, _destroyingPieceParticlesPrefab);

		SimPiece simPiece = SimPieceLibrary.SimPieces[pieceId];
		newPieceManager.ConstructPieceShape(selectedPiece, simPiece);
		PlayerController playerController = GameObject.Find("Player").GetComponent<PlayerController>();
		return selectedPiece;
	}


}
