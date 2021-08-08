using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingZoneController : MonoBehaviour
{
	public void SetupLevel()
	{
		foreach (Transform child in transform.Find("LandingPieces"))
			GameObject.Destroy(child.gameObject);
	}
}
