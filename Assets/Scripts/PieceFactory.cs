using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.SimulationLogic;
using Assets.Scripts.SimulationLogic.Models;
using UnityEngine;

public class PieceFactory : MonoBehaviour
{
	private GameObject _piecePrefab;
	private float _pieceDropSpeed;
	private float _pieceXSpeed;
	private EventsManager _eventsManager;

	public void InitializeGameSettings(GameObject piecePrefab, float pieceDropSpeed, float pieceXSpeed, EventsManager eventsManager)
	{
		_piecePrefab = piecePrefab;
		_pieceDropSpeed = pieceDropSpeed;
		_pieceXSpeed = pieceXSpeed;
		_eventsManager = eventsManager;
	}

	public GameObject CreatePiece(Transform parentTransform, int pieceId, float x, float y)
	{
		GameObject selectedPiece = Instantiate(_piecePrefab, parentTransform);
		selectedPiece.transform.position = new Vector3(x, y);
		PieceManager newPieceManager = selectedPiece.GetComponent<PieceManager>();
		newPieceManager.yMove = 0.0f;
		newPieceManager.PieceDropSpeed = _pieceDropSpeed;
		newPieceManager.XSpeed = _pieceXSpeed;
		newPieceManager.Initialize(_eventsManager);

		SimPiece simPiece = SimPieceLibrary.SimPieces[pieceId];
		newPieceManager.ConstructPieceShape(selectedPiece, simPiece);
		PlayerController playerController = GameObject.Find("Player").GetComponent<PlayerController>();
		return selectedPiece;
	}


}
