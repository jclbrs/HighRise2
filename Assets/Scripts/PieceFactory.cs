using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.SimulationLogic;
using Assets.Scripts.SimulationLogic.Models;
using UnityEngine;

public class PieceFactory : MonoBehaviour
{
	private GameObject _piecePrefab;
	private float _pieceDropSpeed;

	public void InitializeGameSettings(GameObject piecePrefab, float pieceDropSpeed)
	{
		_piecePrefab = piecePrefab;
		_pieceDropSpeed = pieceDropSpeed;
	}

	public GameObject CreatePiece(Transform parentTransform, int pieceId, float x, float y)
	{
		GameObject selectedPiece = Instantiate(_piecePrefab, parentTransform);
		selectedPiece.transform.position = new Vector3(x, y);
		PieceManager newPieceManager = selectedPiece.GetComponent<PieceManager>();
		newPieceManager.yMove = 0.0f;
		newPieceManager.PieceDropSpeed = _pieceDropSpeed;

		SimPiece simPiece = SimPieceLibrary.SimPieces[pieceId];
		newPieceManager.ConstructPieceShape(selectedPiece, simPiece);
		return selectedPiece;
	}


}
