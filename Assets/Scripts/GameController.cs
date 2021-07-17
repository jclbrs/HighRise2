using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.SimulationLogic;
using Assets.Scripts.SimulationLogic.Models;
using UnityEngine;

public class GameController : MonoBehaviour
{
	public GameObject PiecePrefab;

	void Start()
	{
		// Instantiate(BlockPrefab, new Vector3(1.0f, 1.0f));
		// joe test drawing a piece

		CreatePiece();
	}

	private void CreatePiece()
	{
		// Joe continue here
		GameObject selectedPiece = Instantiate(PiecePrefab);
		selectedPiece.transform.position = new Vector3(5.0f, 5.0f);

		Piece simPiece = PieceLibrary.Pieces[13];
		Debug.Log($"PieceId: {simPiece.Id}");
		ConstructPieceShape(selectedPiece, simPiece);
		Debug.Log($"Child: {selectedPiece.transform.GetChild(0).name}");
	}

	private void ConstructPieceShape(GameObject pieceContainer, Piece simPiece)
	{
		bool[,] simShape = simPiece.Shape;
		// Activate/Deactivate the blocks for the shape
		for (int row = 0; row < 3; row++)
		{
			for (int col = 0; col < 3; col++)
			{
				GameObject currentBlock = pieceContainer.transform.Find($"Block_{col}-{row}").gameObject;
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