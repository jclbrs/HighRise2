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
	public PieceState CurrentState;

	private Rigidbody2D _rigidbody;
	private 
	void Start()
	{
		_rigidbody = GetComponent<Rigidbody2D>();
		CurrentState = PieceState.SpringUp;
	}

	void Update()
	{

	}

	private void FixedUpdate()
	{
		switch (CurrentState)
		{
			case PieceState.SpringUp:
				if (transform.position.y < YPosnToSpringOver)
					transform.position = new Vector2(transform.position.x, transform.position.y + YIncrement);
				else
					CurrentState = PieceState.SpringOver;
				break;
			case PieceState.SpringOver:
				if (transform.position.x > 0.4f * PlatformPosition)
					transform.position = new Vector2(transform.position.x - 0.05f, transform.position.y);
				else
				{
					transform.position = new Vector2(0.4f * PlatformPosition, transform.position.y);
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
