using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Enums;
using UnityEngine;

public class SprungPiecesController : MonoBehaviour
{
	SprungPiecesState _currentState;
	private int _destSpingOverIndex;
	private List<Vector2> _springOverPoints;
	private Vector2 _initialPosition;

	private void Start()
	{
		_currentState = SprungPiecesState.Idle;
	}

	internal void InitializeGameSettings(List<Vector2> springOverPoints)
	{
		_springOverPoints = springOverPoints;
		_initialPosition = transform.position;
	}

	public void OnPiecesReleasedFromSpringboard()
	{
		Debug.Log("Sprung pieces released");
		_currentState = SprungPiecesState.SpringingUp;
		_destSpingOverIndex = 0;
	}

	private void Update()
	{
		if (_currentState == SprungPiecesState.SpringingUp)
		{
			if (_destSpingOverIndex < _springOverPoints.Count)
			{
				Vector2 destination = new Vector2(_initialPosition.x + _springOverPoints[_destSpingOverIndex].x, _initialPosition.y + _springOverPoints[_destSpingOverIndex].y);
				transform.position = Vector2.MoveTowards(transform.position, destination, 10f * Time.deltaTime); // joe fix the hard coded speed setting (should be from config)
				if (Vector2.Distance(transform.position, destination) < 0.005) {
					Debug.Log($"curr idx:{_destSpingOverIndex}. destX/Y:{destination.x}/{destination.y}, yPnt:{_springOverPoints[_destSpingOverIndex].y}");
					_destSpingOverIndex = _destSpingOverIndex + 1;
				}
				//new Vector3(transform.position.x, transform.position.y + 0.05f); // joe hard coded for testing
			} else
			{
				_currentState = SprungPiecesState.Idle;
			}
		}
	}
}
