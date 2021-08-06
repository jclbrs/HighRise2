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

	public PieceState CurrentState;
	public  SimPiece SimPiece { get; private set; }
	public float DestinationYPosn;
	public float XSpeed;
	public float DestinationXPosn;
	public float BlockWidth;
	private GameObject _destroyingPieceParticlesPrefab;
	private EventsManager _eventsManager;

	public void Initialize(EventsManager eventsManager, float blockWidth, GameObject destroyingPieceParticlesPrefab)
	{
		_eventsManager = eventsManager;
		BlockWidth = blockWidth;
		_destroyingPieceParticlesPrefab = destroyingPieceParticlesPrefab;
	}

	void Update()
	{
		transform.position = new Vector3(transform.position.x, transform.position.y + yMove);
		switch (CurrentState)
		{
			case PieceState.InBin:
				// no action
				break;
			case PieceState.DroppingToNextBinCell:
				if (transform.position.y <= DestinationYPosn)
				{
					CurrentState = PieceState.InBin;
					yMove = 0f;
				}
				break;
			case PieceState.DroppingFromBinToPlayer:
				if (transform.position.y <= DestinationYPosn)
				{
					CurrentState = PieceState.MovingToSpringboard;
					yMove = 0f;
					_eventsManager.OnPieceDroppedFromBin(this);
				}
				break;
			case PieceState.MovingToSpringboard:
				if (transform.position.x <= DestinationXPosn)
					CurrentState = PieceState.OnSpringboard;
				break;
			case PieceState.OnSpringboard:
				// not addressed yet
				break;
			case PieceState.DroppingInLandingZone:
				// no special actions currently
				break;
			case PieceState.LandedOnLandingZone:
				// no special actions
				break;
			case PieceState.IntoGarbage:
				// no actions, as a coroutine is in progress to destroy the piece
				break;
			default:
				Debug.LogError($"Piece state '{CurrentState}' action not defined");
				break;
		}
	}

	// This is called when a child border object collision is triggered, and it sends a message upward to here
	public void CollisionDetected(string blockName)
	{
		//Debug.Log($"Piece {SimPiece.Id} notified of collision by block:{blockName}");
		if (CurrentState == PieceState.DroppingInLandingZone)
			CurrentState = PieceState.LandedOnLandingZone;
		yMove = 0f;
	}

	public void BeginDropInLandingZoneUntilCollision()
	{
		CurrentState = PieceState.DroppingInLandingZone;
		yMove = PieceDropSpeed;
	}

	public void BeginDropToSpringboardUntilCollision()
	{
		CurrentState = PieceState.MovingToSpringboard;
		yMove = PieceDropSpeed;
	}

	public void BeginDropToYPosnInBin(float yPosn)
	{
		CurrentState = PieceState.DroppingToNextBinCell;
		DestinationYPosn = yPosn;
		yMove = PieceDropSpeed;
		//Debug.Log($"pc{SimPiece.Id} from {transform.position.y} to {yPosn}");
	}

	public void BeginDropFromBinToPlayer(float destinationYPosn)
	{
		CurrentState = PieceState.DroppingFromBinToPlayer;
		DestinationYPosn = destinationYPosn;
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

	public float GetXWidth()
	{
		return SimPiece.GetSimWidth() * BlockWidth;
	}

	public void TossInGarbage()
	{
		CurrentState = PieceState.IntoGarbage;
		StartCoroutine(DestroyPiece());
	}

	private IEnumerator DestroyPiece()
	{
		GameObject particleEffect = Instantiate(_destroyingPieceParticlesPrefab, transform);
		yield return new WaitForSeconds(3f);
		Destroy(gameObject);
	}
}
