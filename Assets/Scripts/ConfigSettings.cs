using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class to hold all configurations. Will be read by GameController, and handed out to the various needed controllers
/// Each contoller will, in turn, place these variables into public variables, which can be tweaked during runtime in Unity for testing purposes
/// </summary>
public class ConfigSettings : MonoBehaviour
{
    public GameObject PiecePrefab;
    public GameObject PlayerObject;
    public GameObject SpringboardObject;
    public GameObject BinsObject;

    public int NumBins;
    public Vector2 Bin0Posn;
    public float BinXSpacing;
    public float BinPieceYSpacing;
    public float PlayerXOffsetFromBin;
    public float PlayerSpeed;
}
