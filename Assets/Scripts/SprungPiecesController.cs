using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Enums;
using UnityEngine;

public class SprungPiecesController : MonoBehaviour
{
	SprungPiecesState _currentState;

	private void Start()
	{
		_currentState = SprungPiecesState.Idle;	
	}
	public void OnPiecesReleasedFromSpringboard()
	{
		Debug.Log("Sprung pieces released");
		_currentState = SprungPiecesState.SpringingUp;
	}

	private void Update()
	{
		transform.position = new Vector3(transform.position.x, transform.position.y + 0.05f); // joe hard coded for testing
	}
}
