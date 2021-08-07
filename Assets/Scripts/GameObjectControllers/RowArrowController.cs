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

	public void OnPiecesLanding(bool isStable, int highestRowReached)
	{
		if (isStable)
			_destYCoord = _startYCoord + (highestRowReached + 1) * 0.86f; // The hard coded value here is (approx) the screen height of a block
		else
			_destYCoord = _startYCoord;
		StartCoroutine(MovePointer());
	}

	public IEnumerator MovePointer() 
	{
		float elapsedTime = 0;
		float durationSeconds = 1f; 
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
