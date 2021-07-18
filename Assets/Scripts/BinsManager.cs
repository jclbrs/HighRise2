using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinsManager : MonoBehaviour
{
    private int _numBins;
	private Vector2 _bin0Posn;
	private float _binXSpacing;
	private float _binPieceYSpacing;

	public void InitializeSettings(int numBins, Vector2 bin0Posn, float binXSpacing, float binPieceYSpacing)
	{
        _numBins = numBins;
		_bin0Posn = bin0Posn;
		_binXSpacing = binXSpacing;
		_binPieceYSpacing = binPieceYSpacing;
	}

	public void CreateBinsForLevel(int currentLevel, PieceFactory pieceFactory)
	{
		try
		{
			float x = _bin0Posn.x;
			pieceFactory.CreatePiece(13, x, _bin0Posn.y);
			pieceFactory.CreatePiece(4, x, _bin0Posn.y + _binPieceYSpacing);
			pieceFactory.CreatePiece(0, x, _bin0Posn.y + 2 * _binPieceYSpacing);
			pieceFactory.CreatePiece(43, x, _bin0Posn.y + 3 * _binPieceYSpacing);

			x = _bin0Posn.x + _binXSpacing;
			pieceFactory.CreatePiece(55, x, _bin0Posn.y);

			pieceFactory.CreatePiece(18, x, _bin0Posn.y + _binPieceYSpacing);
			pieceFactory.CreatePiece(2, x, _bin0Posn.y + 2 * _binPieceYSpacing);
			pieceFactory.CreatePiece(21, x, _bin0Posn.y + 3 * _binPieceYSpacing);

			x = _bin0Posn.x + 2 * _binXSpacing;
			pieceFactory.CreatePiece(60, x, _bin0Posn.y);
			pieceFactory.CreatePiece(44, x, _bin0Posn.y + _binPieceYSpacing);
			pieceFactory.CreatePiece(30, x, _bin0Posn.y + 2 * _binPieceYSpacing);
			pieceFactory.CreatePiece(3, x, _bin0Posn.y + 3 * _binPieceYSpacing);

			x = _bin0Posn.x + 3 * _binXSpacing;
			pieceFactory.CreatePiece(31, x, _bin0Posn.y);
			pieceFactory.CreatePiece(42, x, _bin0Posn.y + _binPieceYSpacing);
			pieceFactory.CreatePiece(60, x, _bin0Posn.y + 2 * _binPieceYSpacing);
			pieceFactory.CreatePiece(15, x, _bin0Posn.y + 3 * _binPieceYSpacing);

			x = _bin0Posn.x + 4 * _binXSpacing;
			pieceFactory.CreatePiece(3, x, _bin0Posn.y);
			pieceFactory.CreatePiece(12, x, _bin0Posn.y + _binPieceYSpacing);
			pieceFactory.CreatePiece(49, x, _bin0Posn.y + 2 * _binPieceYSpacing);
			pieceFactory.CreatePiece(36, x, _bin0Posn.y + 3 * _binPieceYSpacing);
		}
		catch (Exception ex)
		{
			Debug.LogException(ex);
		}
	}
}
