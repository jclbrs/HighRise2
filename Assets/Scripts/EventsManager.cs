using System;
using UnityEngine;

public class EventsManager : MonoBehaviour
{
	public event Action<int> BinPieceSelected;
	public event Action<PieceManager> PieceDroppedFromBin;
	public event Action<int, PieceManager> PieceDroppedToSpringboard;
	public event Action SpringboardTriggered;
	public event Action<bool,int> PiecesLanded;

	public void OnBinPieceSelected(int binIdx)
	{
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

	public void OnPiecesLanded(bool isStable, int highestRowIdx)
	{
		PiecesLanded?.Invoke(isStable, highestRowIdx);
	}


}
