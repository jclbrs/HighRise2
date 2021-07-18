using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.SimulationLogic;
using Assets.Scripts.SimulationLogic.Models;
using UnityEngine;

public class PieceFactory : MonoBehaviour
{
	private GameObject _piecePrefab;

	public void InitializeSettings(GameObject piecePrefab)
	{
		_piecePrefab = piecePrefab;
	}

	public void CreatePiece(int pieceId, float x, float y)
	{
		GameObject selectedPiece = Instantiate(_piecePrefab);
		selectedPiece.transform.position = new Vector3(x, y);
		PieceManager newPieceManager = selectedPiece.GetComponent<PieceManager>();
		newPieceManager.yMove = 0.0f;

		Piece simPiece = PieceLibrary.Pieces[pieceId];
		Debug.Log($"PieceId: {simPiece.Id}");
		newPieceManager.ConstructPieceShape(selectedPiece, simPiece);
	}


}
