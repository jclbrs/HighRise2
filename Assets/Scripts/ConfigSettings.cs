using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class to hold all configurations. Will be read by GameController, and handed out to the various needed controllers
/// Each contoller will, in turn, place these variables into public variables, which can be tweaked during runtime in Unity for testing purposes
/// </summary>
public class ConfigSettings : MonoBehaviour
{
    public int InitialLevel; // used to test later levels

    [Header("==== Piece related ====")]
    public GameObject PiecePrefab;
    public GameObject DestroyingPieceParticlesPrefab;
    public float BlockWidth; // A block is one of the 3x3 sections that belong to a piece
    public float PieceXSpeed;  // player speed will be the same, so the move to springboard together

    [Header("==== Bin related ====")]
    public GameObject BinsObject;
    public int NumBins;
    public int NumCellsPerBin;
    public Vector2 Bin0Posn;
    public float BinXSpacing;
    public float BinPieceYSpacing;
    public float PieceDropFromBinYSpeed;
    public float PieceYFromBinDrop; // The y-coord that the player stops at when being dropped from the bin down to the player (not at ground, until dropping onto spring

    [Header("==== Player related ====")]
    public GameObject PlayerObject;
    public float PlayerXOffsetFromBin;
    public float PlayerXSpeed;
    public float PlayerYSpeed;
    public Vector2 PlayerSuccesDestination;

    [Header("==== Springboard related ====")]
    public GameObject SpringboardObject;
    public GameObject SprungPiecesContainer;
    public float SprungYSpeed;
    public float SpringboardYSpeed;
    public float SpringboardMoveHeight;
    public float FirstPieceSpringX;  // the x-coordinate to drop the first piece (on the far left of the springboard)
    public List<Vector2> SpringOverPoints;

    [Header("==== Landing Zone related ====")]
    public GameObject LandingZoneObject;
    public GameObject RowArrowObject;
    public int LandingSuccessRow;
}
