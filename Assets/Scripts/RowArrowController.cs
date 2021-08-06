using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Enums;
using UnityEngine;

public class RowArrowController : MonoBehaviour
{
	private float _destYCoord;
	private float _startYCoord;

	private void Start()
	{
		_startYCoord = transform.position.y;
	}

	public void OnPiecesLanded(bool isStable, int highestRowReached)
	{
		_destYCoord = _startYCoord + (highestRowReached + 1) * 0.86f; // JOE this is messed up.  FIX!!!
		StartCoroutine(MovePointer());
	}

	public IEnumerator MovePointer() 
	{
		float elapsedTime = 0;
		float durationSeconds = 1f; // hard coding to see how it looks
		Vector3 startingPosition = new Vector3(transform.position.x, transform.position.y);
		Vector3 endingPosition = new Vector3(transform.position.x, _destYCoord);
		yield return new WaitForSeconds(1f);
		while (elapsedTime < 1f)
		{
			transform.position = Vector3.Lerp(startingPosition, endingPosition, (elapsedTime / durationSeconds));
			elapsedTime += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		transform.position = endingPosition;
	}
}
