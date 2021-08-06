using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingZoneController : MonoBehaviour
{
 //   public void OnPiecesLanding(bool isStable, int highestRowIdx)
	//{
	//	Debug.Log($"Stable:{isStable}, highestRow:{highestRowIdx}");
	//	if (!isStable) {
	//		StartCoroutine("StartUnstablePieceActions");
	//	}
	//}

	//private IEnumerable StartUnstablePieceActions()
	//{
	//	Transform landingPiecesContainer = transform.Find("LandingPieces");
	//	yield return new WaitForSeconds(2f);
	//	int pieceCount = landingPiecesContainer.childCount;
	//	for (int i = 0; i < pieceCount; i++)
	//	{
	//		PieceManager pieceManager = landingPiecesContainer.GetChild(i).GetComponent<PieceManager>();
	//		pieceManager.StartUnstableAction();
	//	}
	//}
}
