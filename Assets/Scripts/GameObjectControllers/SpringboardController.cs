using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Assets.Scripts.Enums;
using UnityEngine;

// The Springboard and springboardController do not actually hold the drop pieces.
// That is the job of the SprungPiecesContainer/SprunPiecesController.
// This class' responsibility is to make the springs go up and down, and the attached SprungPiecesContainer with it
public class SpringboardController : MonoBehaviour
{
	private Dictionary<int, PieceManager> _springboardPieces;
	private SpringboardState _currentState;
	private float _springboardYSpeed;
	private float _springboardMoveHeight;
	private float _initialYCoord;

	void Start()
	{
		_currentState = SpringboardState.Idle;
		_initialYCoord = transform.position.y;
		_springboardPieces = new Dictionary<int, PieceManager>();
	}

	internal void InitializeGameSettings(float springboardYSpeed, float springboardMoveHeight)
	{
		_springboardYSpeed = springboardYSpeed;
		_springboardMoveHeight = springboardMoveHeight;
	}

	void Update()
	{
		switch (_currentState)
		{
			case SpringboardState.Idle:
				break;
			case SpringboardState.SpringingUp:
				transform.position = new Vector3(transform.position.x, transform.position.y + _springboardYSpeed);
				if (transform.position.y >= _initialYCoord + _springboardMoveHeight)
				{
					_currentState = SpringboardState.SpringingDown;
					Transform sprungPiecesContainer = gameObject.transform.Find("SprungPiecesContainer");
					if (sprungPiecesContainer != null) // make sure sprungPiecesContainer is attached, and not currently detached and moving over to landing zone
					{
						SprungPiecesController sprungPiecesController = sprungPiecesContainer.GetComponent<SprungPiecesController>();
						sprungPiecesController.transform.SetParent(null);
						sprungPiecesController.OnPiecesReleasedFromSpringboard(this, _springboardPieces);
						_springboardPieces = new Dictionary<int, PieceManager>();
					}
				}

				break;
			case SpringboardState.SpringingDown:
				transform.position = new Vector3(transform.position.x, transform.position.y - _springboardYSpeed);
				if (transform.position.y <= _initialYCoord)
				{
					transform.position = new Vector3(transform.position.x, _initialYCoord);
					_currentState = SpringboardState.Idle;
				}
				break;
		}
	}

	public void OnPieceDroppedToSpringboard(int xIdx, PieceManager pieceManager)
	{
		pieceManager.gameObject.transform.SetParent(gameObject.transform.Find("SprungPiecesContainer"));
		_springboardPieces.Add(xIdx, pieceManager);
		List<int> keys = new List<int>(_springboardPieces.Keys);
		StringBuilder sb = new StringBuilder();
		foreach (int key in keys)
		{
			sb.Append($"{key.ToString()},");
		}
		//Debug.Log($"Piece dropped to springboard at {xIdx}. Springs:{sb.ToString()}");
	}

	public void OnSpringboardTriggered()
	{
		//Debug.Log("Springboard triggered");
		_currentState = SpringboardState.SpringingUp;
	}
}
