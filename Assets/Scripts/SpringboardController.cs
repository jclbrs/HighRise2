using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Enums;
using UnityEngine;

public class SpringboardController : MonoBehaviour
{
	private Dictionary<int, PieceManager> _springboardPieces;
	private  SpringboardState _currentState;
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

					SprungPiecesController sprungPiecesController = gameObject.transform.Find("SprungPiecesContainer").GetComponent<SprungPiecesController>();
					sprungPiecesController.transform.SetParent(null);
					sprungPiecesController.OnPiecesReleasedFromSpringboard();
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

	public List<float> GetSpringboardXPosns()
	{
		List<float> xPosns = new List<float>();
		xPosns.Add(transform.Find("Springs/Spring0").position.x);
		xPosns.Add(transform.Find("Springs/Spring1").position.x);
		xPosns.Add(transform.Find("Springs/Spring2").position.x);
		xPosns.Add(transform.Find("Springs/Spring3").position.x);
		xPosns.Add(transform.Find("Springs/Spring4").position.x);
		xPosns.Add(transform.Find("Springs/Spring5").position.x);
		return xPosns;
	}

	public void OnPieceDroppedToSpringboard(int xIdx, PieceManager pieceManager)
	{
		Debug.Log($"Piece dropped to springboard");
		pieceManager.gameObject.transform.SetParent(gameObject.transform.Find("SprungPiecesContainer"));
		_springboardPieces.Add(xIdx, pieceManager);
	}

	public void OnSpringboardTriggered()
	{
		Debug.Log("Springboard triggered");
		_currentState = SpringboardState.SpringingUp;
	}
}
