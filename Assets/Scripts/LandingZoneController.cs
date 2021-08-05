using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingZoneController : MonoBehaviour
{
    public void OnPiecesLanded(bool isStable, int highestRowIdx)
	{
		Debug.Log($"Stable:{isStable}, highestRow:{highestRowIdx}");
	}
}
