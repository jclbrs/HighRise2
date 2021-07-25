using System;
using UnityEngine;

public class EventsManager : MonoBehaviour
{
	public event Action<int> BinPieceSelected;
	public event Action<PieceManager> PieceDroppedFromBin;

	public void OnBinPieceSelected(int binIdx)
	{
		Debug.Log($"EventsManager: Bin piece selected, binIdx={binIdx}");
		BinPieceSelected?.Invoke(binIdx);
	}

	public void OnPieceDroppedFromBin(PieceManager pieceManager)
	{
		PieceDroppedFromBin?.Invoke(pieceManager);
	}


}
