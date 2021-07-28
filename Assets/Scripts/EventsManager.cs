using System;
using UnityEngine;

public class EventsManager : MonoBehaviour
{
	public event Action<int> BinPieceSelected;
	public event Action<PieceManager> PieceDroppedFromBin;
	public event Action<int, PieceManager> PieceDroppedToSpringboard;
	public event Action SpringboardTriggered;

	public void OnBinPieceSelected(int binIdx)
	{
		Debug.Log($"EventsManager: Bin piece selected, binIdx={binIdx}");
		BinPieceSelected?.Invoke(binIdx);
	}

	public void OnPieceDroppedFromBin(PieceManager pieceManager)
	{
		PieceDroppedFromBin?.Invoke(pieceManager);
	}

	public void OnPieceDroppedToSpringboard(int xIdx, PieceManager pieceManager)
	{
		PieceDroppedToSpringboard?.Invoke(xIdx, pieceManager);
	}

	public void OnSpringboardTriggered()
	{
		SpringboardTriggered?.Invoke();
	}




}
