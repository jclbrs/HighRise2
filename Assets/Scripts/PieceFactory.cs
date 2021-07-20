using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.SimulationLogic;
using Assets.Scripts.SimulationLogic.Models;
using UnityEngine;

public class PieceFactory : MonoBehaviour
{
	private GameObject _piecePrefab;

	public void InitializeGameSettings(GameObject piecePrefab)
	{
		_piecePrefab = piecePrefab;
	}

	public void CreatePiece(Transform parentTransform, int pieceId, float x, float y)
	{
		GameObject selectedPiece = Instantiate(_piecePrefab, parentTransform);
		selectedPiece.transform.position = new Vector3(x, y);
		PieceManager newPieceManager = selectedPiece.GetComponent<PieceManager>();
		newPieceManager.yMove = 0.0f;

		SimPiece simPiece = PieceLibrary.Pieces[pieceId];
		newPieceManager.ConstructPieceShape(selectedPiece, simPiece);
	}


}
