using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PieceMover : MonoBehaviour
{
	public enum PieceState
	{
		InBin,
		DownFromBin,
		OnFloorBelowBin,
		OnPlatform,
		SpringUp,
		SpringOver,
		SpringDown,
		FreeFall
	}

	public float YIncrement = 0.1f;
	public float YPosnToSpringOver = 2.0f;
	public int PlatformPosition = 0;
	public float BlockWidth = 0.14f;
	public PieceState CurrentState;
	public Vector2 CenterOfMass;

	private Rigidbody2D _rigidbody;
	private 
	void Start()
	{
		_rigidbody = GetComponent<Rigidbody2D>();
		CurrentState = PieceState.SpringUp;
		CenterOfMass = _rigidbody.centerOfMass;
	}

	void Update()
	{

	}

	private void FixedUpdate()
	{
		CenterOfMass = _rigidbody.centerOfMass;
		switch (CurrentState)
		{
			case PieceState.SpringUp:
				if (transform.position.y < YPosnToSpringOver)
					transform.position = new Vector2(transform.position.x, transform.position.y + YIncrement);
				else
					CurrentState = PieceState.SpringOver;
				break;
			case PieceState.SpringOver:
				if (transform.position.x > BlockWidth * PlatformPosition)
					transform.position = new Vector2(transform.position.x - 0.05f, transform.position.y);
				else
				{
					transform.position = new Vector2(BlockWidth * PlatformPosition, transform.position.y);
					CurrentState = PieceState.SpringDown;
				}
				break;
			case PieceState.SpringDown:
				transform.position = new Vector2(transform.position.x, transform.position.y - YIncrement);
				break;
			case PieceState.FreeFall:
				// no action
				break;
			default:
				throw new Exception("Current state not defined yet");
		};
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		Debug.Log("Collision");
		_rigidbody.bodyType = RigidbodyType2D.Dynamic;
		CurrentState = PieceState.FreeFall;
	}
}
