using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Assets.Scripts.Enums;
using UnityEngine;

public class SpringboardController : MonoBehaviour
{
	private Dictionary<int, PieceManager> _springboardPieces;
	private  SpringboardState _currentState;
	private float _springboardYSpeed;
	private float _springboardMoveHeight;
	private float _initialYCoord;
	private float _firstPieceSpringX;
	private float _pieceSpringXSpacing;
	private List<float> _springXCoords;

	void Start()
	{
		_currentState = SpringboardState.Idle;
		_initialYCoord = transform.position.y;
		_springboardPieces = new Dictionary<int, PieceManager>();
	}

	internal void InitializeGameSettings(float springboardYSpeed, float springboardMoveHeight, float firstPieceSpringX, float pieceSpringXSpacing)
	{
		_springboardYSpeed = springboardYSpeed;
		_springboardMoveHeight = springboardMoveHeight;
		_firstPieceSpringX = firstPieceSpringX;
		_pieceSpringXSpacing = pieceSpringXSpacing;

		_springXCoords = new List<float>();
		for (int i = 0; i < 6; i++) // hard coded, 6 springs
		{
			_springXCoords.Add(_firstPieceSpringX + i * _pieceSpringXSpacing);
		}

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
					sprungPiecesController.OnPiecesReleasedFromSpringboard(this, _springboardPieces);
					_springboardPieces = new Dictionary<int, PieceManager>();
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
		return _springXCoords;
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
		Debug.Log($"Piece dropped to springboard at {xIdx}. Springs:{sb.ToString()}");
	}

	public void OnSpringboardTriggered()
	{
		Debug.Log("Springboard triggered");
		_currentState = SpringboardState.SpringingUp;
	}
}
