using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using Assets.Scripts.Enums;
using Assets.Scripts.SimulationLogic.Models;
using UnityEngine;

public class PieceManager : MonoBehaviour
{
	public float yMove = 0;
	public float PieceDropSpeed;
	public PieceBinDropLandedEvent LandedFromBinEvent;

	public PieceState CurrentState;
	public  SimPiece SimPiece { get; private set; }
	private float _destinationYPosn;
	public float XSpeed;


	void Update()
	{
		transform.position = new Vector3(transform.position.x, transform.position.y + yMove);
		switch (CurrentState)
		{
			case PieceState.InBin:
				// no action
				break;
			case PieceState.DroppingToNextBinCell:
				if (transform.position.y <= _destinationYPosn)
				{
					CurrentState = PieceState.InBin;
					yMove = 0f;
				}
				break;
			case PieceState.DroppingFromBinToPlayer:
				if (transform.position.y <= _destinationYPosn)
				{
					CurrentState = PieceState.MovingToSpringboard;
					yMove = 0f;
					LandedFromBinEvent.Invoke(this);
				}
				break;
			case PieceState.MovingToSpringboard:
				// joe need to get the first available spring x-coord
				transform.position = new Vector3(transform.position.x - XSpeed, transform.position.y);
				break;
			default:
				Debug.LogError($"Piece state '{CurrentState}' action not defined");
				break;
		}
	}

	// This is called when a child border object collision is triggered, and it sends a message upward to here
	public void CollisionDetected()
	{ // joe this may all be obsolete, as we're dropping piece from bin to a specific y-coord, not collision
		Debug.Log("Piece notified of collision");
		yMove = 0f;
		if (CurrentState == PieceState.DroppingFromBinToPlayer)
		{
			CurrentState = PieceState.MovingToSpringboard;
			LandedFromBinEvent.Invoke(this);
		}

		
	}

	public void BeginDropUntilCollision()
	{
		Debug.Log($"PieceDropSpeed:{PieceDropSpeed}");
		yMove = PieceDropSpeed;
	}

	public void BeginDropToYPosnInBin(float yPosn)
	{
		CurrentState = PieceState.DroppingToNextBinCell;
		_destinationYPosn = yPosn;
		yMove = PieceDropSpeed;
	}

	public void BeginDropFromBinToPlayer(float destinationYPosn)
	{
		CurrentState = PieceState.DroppingFromBinToPlayer;
		_destinationYPosn = destinationYPosn;
		yMove = PieceDropSpeed;
	}

	public void ConstructPieceShape(GameObject pieceContainer, SimPiece simPiece)
	{
		SimPiece = simPiece;
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

}
