using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.SimulationLogic;
using Assets.Scripts.SimulationLogic.Models;
using UnityEngine;

public class PieceFactory : MonoBehaviour
{
	private GameObject _piecePrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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

	//private void ConstructPieceShape(GameObject pieceContainer, Piece simPiece)
	//{
	//	bool[,] simShape = simPiece.Shape;
	//	GameObject currentBlock;
	//	int row = int.MinValue;
	//	int col = int.MinValue;
	//	// Activate/Deactivate the blocks for the shape
	//	try
	//	{
	//		for (row = 0; row < 3; row++)
	//		{
	//			for (col = 0; col < 3; col++)
	//			{
	//				currentBlock = pieceContainer.transform.Find($"Block_{col}-{row}").gameObject;
	//				currentBlock.SetActive(simShape[col, row]);

	//				// Activate/Deactivate the borders, based on its neighbors
	//				if (currentBlock.activeSelf)
	//				{
	//					if (row < 2 && simShape[col, row + 1]) // could have piece above it
	//						currentBlock.transform.Find("Border_top").gameObject.SetActive(false);
	//					if (row > 0 && simShape[col, row - 1]) // could have piece below it
	//						currentBlock.transform.Find("Border_btm").gameObject.SetActive(false);
	//					if (col < 2 && simShape[col + 1, row]) // could have piece to the right
	//						currentBlock.transform.Find("Border_rgt").gameObject.SetActive(false);
	//					if (col > 0 && simShape[col - 1, row]) // could have piece to the left
	//						currentBlock.transform.Find("Border_lft").gameObject.SetActive(false);
	//				}

	//			}
	//		}
	//	}
	//	catch (Exception ex)
	//	{
	//		Debug.LogError($"Error building pieceid {simPiece.Id} at row:{row}, col:{col}");
	//	}
	//}

	// Update is called once per frame

}
