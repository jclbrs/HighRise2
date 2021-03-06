using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceFactoryDIWrapper
{
	public GameObject PiecePrefab;
	public GameObject DestroyingPieceParticlesPrefab;
	public float PieceDropFromBinYSpeed;
	public float PieceXSpeed;
	public float BlockWidth;
	public float BlockHeight;
	public EventsManager EventsManager;
	public List<Color> BlockColors;
}
