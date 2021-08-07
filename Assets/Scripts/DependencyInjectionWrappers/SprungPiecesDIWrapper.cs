using System.Collections;
using System.Collections.Generic;
using ScriptDefinitions.Assets.Scripts.SimulationLogic;
using UnityEngine;

public class SprungPiecesDIWrapper
{
	public EventsManager EventsManager;
	public List<Vector2> SpringOverPoints;
	public float FirstPieceSpringX;
	public float PieceSpringXSpacing;
	public float BlockWidth;
	public LogicController LogicController;
	public float SprungYSpeed;
}
