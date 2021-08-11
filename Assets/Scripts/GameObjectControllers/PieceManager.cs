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
	public SimPiece SimPiece { get; private set; }
	public float DestinationYPosn;
	public float XSpeed;
	public float DestinationXPosn;
	public float BlockWidth;
	private GameObject _destroyingPieceParticlesPrefab;
	private EventsManager _eventsManager;
	private bool _isLandingZoneStable;
	private float _rotationSpeed;
	private static System.Random _randomizer;

	public void Initialize(EventsManager eventsManager, float blockWidth, GameObject destroyingPieceParticlesPrefab)
	{
		_eventsManager = eventsManager;
		BlockWidth = blockWidth;
		_destroyingPieceParticlesPrefab = destroyingPieceParticlesPrefab;
		_randomizer = new System.Random();
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
			case PieceState.DroppingFromPlayerToSpring:
				if (transform.position.y <= DestinationYPosn)
				{
					transform.position = new Vector3(transform.position.x, DestinationYPosn);
					CurrentState = PieceState.OnSpring;
					yMove = 0f;
				}
				break;
			case PieceState.OnSpring:
				break;
			case PieceState.DroppingInLandingZone:
				if (transform.position.y <= DestinationYPosn)
				{
					transform.position = new Vector3(transform.position.x, DestinationYPosn);
					CurrentState = PieceState.LandedOnLandingZone;
					yMove = 0f;
					if (!_isLandingZoneStable)
					{
						Component[] pieceManagersInLandingZone = transform.parent.GetComponentsInChildren(typeof(PieceManager));
						foreach (PieceManager pieceManagerInLandingZone in pieceManagersInLandingZone)
						{
							pieceManagerInLandingZone.CurrentState = PieceState.FallingUnstable;
							pieceManagerInLandingZone.StartUnstableAction();
						}
					}
				}
				DrawDownRay();
				break;
			case PieceState.LandedOnLandingZone:

				// no special actions
				break;
			case PieceState.IntoGarbage:
				// no actions, as a coroutine is in progress to destroy the piece
				break;
			case PieceState.FallingUnstable:
				transform.Rotate(Vector3.forward * (_rotationSpeed * Time.deltaTime));
				if (transform.position.y < -50f)
					Destroy(gameObject);
				break;
			default:
				Debug.LogError($"Piece state '{CurrentState}' action not defined");
				break;
		}
	}

	// This is called when a child border object collision is triggered, and it sends a message upward to here
	public void CollisionDetected(Collider2D collider)
	{
		Debug.Log("Collision");
		switch (CurrentState)
		{
			case PieceState.FallingUnstable: // ignore collisions if falling due to instability
				break;
			//case PieceState.DroppingInLandingZone:
				//CurrentState = PieceState.LandedOnLandingZone;
				//yMove = 0f;
				//if (!_isLandingZoneStable)
				//{
				//	Component[] pieceManagersInLandingZone = transform.parent.GetComponentsInChildren(typeof(PieceManager));
				//	foreach (PieceManager pieceManagerInLandingZone in pieceManagersInLandingZone)
				//	{
				//		pieceManagerInLandingZone.CurrentState = PieceState.FallingUnstable;
				//		pieceManagerInLandingZone.StartUnstableAction();
				//	}
				//}
				break;
			default:
				yMove = 0f;
				break;
		}
	}

	private void DrawDownRay()
	{
		// joe testing
		Collider2D[] colliders = GetComponentsInChildren<Collider2D>();
		//Debug.Log($"Found {colliders.Length} colliders");
		foreach (Collider2D collider in colliders)
		{
			//Debug.Log($"Name:{collider.transform.name}");
			if (collider.transform.name == "Border_btm")
			{
				Vector2 rayStartCoord = new Vector2(collider.transform.position.x, collider.transform.position.y - 0.5f);
				RaycastHit2D hit = Physics2D.Raycast(rayStartCoord, Vector2.down);
				//Debug.Log($"Hit:{hit.point.x}/{hit.point.y}, distance:{hit.distance}");
				Debug.DrawRay(rayStartCoord, Vector2.down * hit.distance, Color.green);
				//float yCollision = hit.point.y;
				//Debug.Log($"Piece {SimPiece.Id} yCollision:{yCollision}");
			}
		}
		// joe end testing

	}

	public void BeginDropInLandingZone(bool isLandingZoneStable)
	{
		CurrentState = PieceState.DroppingInLandingZone;
		_isLandingZoneStable = isLandingZoneStable;
		yMove = PieceDropSpeed;
		CalcYDropCollision();
	}

	private void CalcYDropCollision()
	{
		Collider2D[] colliders = GetComponentsInChildren<Collider2D>();
		List<float> potentialYDestCoords = new List<float>();
		foreach (Collider2D collider in colliders)
		{
			if (collider.transform.name == "Border_btm")
			{
				Vector2 rayStartCoord = new Vector2(collider.transform.position.x, collider.transform.position.y - 0.5f);
				RaycastHit2D hit = Physics2D.Raycast(rayStartCoord, Vector2.down);
				potentialYDestCoords.Add(hit.point.y);
				Debug.DrawRay(rayStartCoord, Vector2.down * hit.distance, Color.green);
			}
		}

		// JOE THIS IS MESSED UP.  FIX!!!
		// The highest y-Coord is the 1st collision
		potentialYDestCoords.Sort();
		potentialYDestCoords.Reverse();
		DestinationYPosn = potentialYDestCoords[0];
	}

	public void BeginDropToSpringUntilCollision()
	{
		CurrentState = PieceState.DroppingFromPlayerToSpring;
		CalcYDropCollision();
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
						SpriteRenderer renderer = currentBlock.GetComponent<SpriteRenderer>();
						//renderer.color = Color.red;
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

	public void StartUnstableAction()
	{
		_rotationSpeed = _randomizer.Next(-150, 150);
		yMove = PieceDropSpeed / 4f;  // slow down the crashes
		Component[] allChildColliders = gameObject.GetComponentsInChildren(typeof(BoxCollider2D));
		foreach (BoxCollider2D collider in allChildColliders)
		{
			collider.enabled = false;
		}
	}
}
