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

	public List<float> GetBinXPosns()
	{
		List<float> xPosns = new List<float>();
		for (int i = 0; i < _numBins; i++)
		{
			xPosns.Add(_bin0Posn.x + i * _binXSpacing);
		}
		return xPosns;
	}
	public void CreateBinsForLevel(int currentLevel, PieceFactory pieceFactory)
	{
		try
		{
			float x = _bin0Posn.x;
			pieceFactory.CreatePiece(transform, 13, x, _bin0Posn.y);
			pieceFactory.CreatePiece(transform, 4, x, _bin0Posn.y + _binPieceYSpacing);
			pieceFactory.CreatePiece(transform, 0, x, _bin0Posn.y + 2 * _binPieceYSpacing);
			pieceFactory.CreatePiece(transform, 43, x, _bin0Posn.y + 3 * _binPieceYSpacing);

			x = _bin0Posn.x + _binXSpacing;
			pieceFactory.CreatePiece(transform, 55, x, _bin0Posn.y);

			pieceFactory.CreatePiece(transform, 18, x, _bin0Posn.y + _binPieceYSpacing);
			pieceFactory.CreatePiece(transform, 2, x, _bin0Posn.y + 2 * _binPieceYSpacing);
			pieceFactory.CreatePiece(transform, 21, x, _bin0Posn.y + 3 * _binPieceYSpacing);

			x = _bin0Posn.x + 2 * _binXSpacing;
			pieceFactory.CreatePiece(transform, 60, x, _bin0Posn.y);
			pieceFactory.CreatePiece(transform, 44, x, _bin0Posn.y + _binPieceYSpacing);
			pieceFactory.CreatePiece(transform, 30, x, _bin0Posn.y + 2 * _binPieceYSpacing);
			pieceFactory.CreatePiece(transform, 3, x, _bin0Posn.y + 3 * _binPieceYSpacing);

			x = _bin0Posn.x + 3 * _binXSpacing;
			pieceFactory.CreatePiece(transform, 31, x, _bin0Posn.y);
			pieceFactory.CreatePiece(transform, 42, x, _bin0Posn.y + _binPieceYSpacing);
			pieceFactory.CreatePiece(transform, 60, x, _bin0Posn.y + 2 * _binPieceYSpacing);
			pieceFactory.CreatePiece(transform, 15, x, _bin0Posn.y + 3 * _binPieceYSpacing);

			x = _bin0Posn.x + 4 * _binXSpacing;
			pieceFactory.CreatePiece(transform, 3, x, _bin0Posn.y);
			pieceFactory.CreatePiece(transform, 12, x, _bin0Posn.y + _binPieceYSpacing);
			pieceFactory.CreatePiece(transform, 49, x, _bin0Posn.y + 2 * _binPieceYSpacing);
			pieceFactory.CreatePiece(transform, 36, x, _bin0Posn.y + 3 * _binPieceYSpacing);
		}
		catch (Exception ex)
		{
			Debug.LogException(ex);
		}
	}
}
