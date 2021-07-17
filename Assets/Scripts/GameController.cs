using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.SimulationLogic;
using Assets.Scripts.SimulationLogic.Models;
using UnityEngine;

public class GameController : MonoBehaviour
{
	public GameObject PiecePrefab;
	public Vector2 Bin0Position;
	public float BinXSpacing;
	public float BinPieceYSpacing;

	void Start()
	{
		try
		{
			float x = Bin0Position.x;
			CreatePiece(13, x, Bin0Position.y);
			CreatePiece(4, x, Bin0Position.y + BinPieceYSpacing);
			CreatePiece(0, x, Bin0Position.y + 2 * BinPieceYSpacing);
			CreatePiece(43, x, Bin0Position.y + 3 * BinPieceYSpacing);

			x = Bin0Position.x + BinXSpacing;
			//CreatePiece(55, x, Bin0Position.y); // joe, this piece fails.  Find out why
			CreatePiece(17, x, Bin0Position.y); // this too .  ????

			CreatePiece(18, x, Bin0Position.y + BinPieceYSpacing);
			CreatePiece(2, x, Bin0Position.y + 2 * BinPieceYSpacing);
			CreatePiece(21, x, Bin0Position.y + 3 * BinPieceYSpacing);

			x = Bin0Position.x + 2 * BinXSpacing;
			CreatePiece(60, x, Bin0Position.y);
			CreatePiece(44, x, Bin0Position.y + BinPieceYSpacing);
			CreatePiece(30, x, Bin0Position.y + 2 * BinPieceYSpacing);
			CreatePiece(3, x, Bin0Position.y + 3 * BinPieceYSpacing);

			x = Bin0Position.x + 3 * BinXSpacing;
			CreatePiece(31, x, Bin0Position.y);
			CreatePiece(42, x, Bin0Position.y + BinPieceYSpacing);
			CreatePiece(60, x, Bin0Position.y + 2 * BinPieceYSpacing);
			CreatePiece(15, x, Bin0Position.y + 3 * BinPieceYSpacing);
		}
		catch (Exception ex)
		{
			Debug.LogException(ex);
		}
	}

	private void CreatePiece(int pieceId, float x, float y)
	{
		GameObject selectedPiece = Instantiate(PiecePrefab);
		selectedPiece.transform.position = new Vector3(x, y);
		PieceManager pieceManager = selectedPiece.GetComponent<PieceManager>();
		pieceManager.yMove = 0.0f;

		Piece simPiece = PieceLibrary.Pieces[pieceId];
		Debug.Log($"PieceId: {simPiece.Id}");
		ConstructPieceShape(selectedPiece, simPiece);
	}

	private void ConstructPieceShape(GameObject pieceContainer, Piece simPiece)
	{
		bool[,] simShape = simPiece.Shape;
		GameObject currentBlock;
		int row = int.MinValue;
		int col = int.MinValue;
		// Activate/Deactivate the blocks for the shape
		try
		{
			for (row = 0; row < 3; row++)
			{
				for (col = 0; col < 3; col++)
				{
					currentBlock = pieceContainer.transform.Find($"Block_{col}-{row}").gameObject;
					currentBlock.SetActive(simShape[col, row]);

					// Activate/Deactivate the borders, based on its neighbors
					if (currentBlock.activeSelf)
					{
						if (row < 2 && simShape[col, row + 1]) // could have piece above it
							currentBlock.transform.Find("Border_top").gameObject.SetActive(false);
						if (row > 0 && simShape[col, row - 1]) // could have piece below it
							currentBlock.transform.Find("Border_btm").gameObject.SetActive(false);
						if (col < 2 && simShape[col + 1, row]) // could have piece to the right
							currentBlock.transform.Find("Border_rgt").gameObject.SetActive(false);
						if (col > 0 && simShape[col - 1, row]) // could have piece to the left
							currentBlock.transform.Find("Border_lft").gameObject.SetActive(false);
					}

				}
			}
		}
		catch (Exception ex)
		{
			Debug.LogError($"Error building pieceid {simPiece.Id} at row:{row}, col:{col}");
		}

	}

	// Update is called once per frame
	void Update()
	{

	}
}

/*
 So you know how you have to be careful with container objects because if you mess with them in one place while adding or removing in another place, you get all kinds of fun errors?
And in realtime games, that's a serious concern and frequent problem.
So... copy/pasting some code from this Poker game I'm writing for Gameboard...
        public void NewPlayerJoined(PokerPlayerSceneObject inPlayerObject)
        {
            lock(PlayerValues)
            {
                PokerPlayerValues playerValues = new PokerPlayerValues()
                {
                    playerObject = inPlayerObject,
                };

                PlayerValues.Add(playerValues);
            }
        }
The player joins, I put them in a local object that maintains game specific data, then store them in the PlayerValues list.
Which I've wrapped in a Lock statement.
This prevents ANYTHING else from touching PlayerValues until it's unlocked.
And C# essentially queues it up, so it will wait until it's unlocked before doing other things with it.
So you don't lose anything, and you don't have to worry about sequence breaking the code and hitting errors.

*/