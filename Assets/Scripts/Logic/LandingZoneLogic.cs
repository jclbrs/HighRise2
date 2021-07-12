using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Logic.Models;

namespace Assets.Scripts.Logic
{
	public enum PlacePieceStatus
	{
		Ok,
		BadRowArg,
		BadColArg,
		InvalidPieceId,
		AnotherPiecePresent,
		TooFarToTheRight
	}

	public class LandingZoneLogic
	{
        public LandingZoneBlock[,] LandingZoneCells; // 
        public int NumRowsInLandingZone { get; private set; } = 21;
        public int NumColsInLandingZone { get; private set; } =  5;
		private PieceLibrary _pieceLibrary;

        public LandingZoneLogic()
		{
			ClearLandingZone();
			_pieceLibrary = new PieceLibrary();
		}

		public void ClearLandingZone()
		{
			LandingZoneCells = new LandingZoneBlock[NumRowsInLandingZone, NumColsInLandingZone];
			for (int row = 0; row < NumRowsInLandingZone; row++)
			{
				for (int col = 0; col < NumColsInLandingZone; col++)
				{
					LandingZoneCells[row, col] = new LandingZoneBlock();
				}
			}
		}

		public void DropPiecesFromSpringboard(List<SpringboardPiece> pieces)
		{
			StartNewPiecesPositioning(pieces);
			DropPiecesToRestingPosition(pieces);
			CalculateStability();

			// next sequences
			// Drop pieces (loop until each gets to a collision of floor or existing piece)
			// Calculate stability
			// if stable, return success
			// If not, start tumbling
		}

		private void DropPiecesToRestingPosition(List<SpringboardPiece> pieces)
		{
			throw new NotImplementedException();
		}

		// Place all pieces from springboard to top of landing zone
		public void StartNewPiecesPositioning(List<SpringboardPiece> pieces)
		{
			PlacePieceStatus placePieceStatus;
			foreach (SpringboardPiece springboardPiece in pieces)
			{
				if (!TryPlacePiece(springboardPiece.PieceId, NumRowsInLandingZone-1 - 3, springboardPiece.Col, out placePieceStatus))
					throw new Exception($"Exception dropping piece {springboardPiece.PieceId} onto landing area at col {springboardPiece.Col}");
			}
		}

		public bool TryPlacePiece(int pieceId, int row, int col, out PlacePieceStatus placePieceStatus) 
		{
			placePieceStatus = PlacePieceStatus.Ok;

			#region Validation
			if (row >= NumRowsInLandingZone || row < 0)
			{
				placePieceStatus = PlacePieceStatus.BadRowArg;
				return false;
			}
			if (col >= NumColsInLandingZone || col < 0)
			{
				placePieceStatus = PlacePieceStatus.BadColArg;
				return false;
			}
			Piece piece = null;
			try
			{
				piece = _pieceLibrary.Pieces[pieceId];
			}
			catch (Exception)
			{
				placePieceStatus = PlacePieceStatus.InvalidPieceId;
				return false;
			}
			int width = piece.GetWidth();
			if ((col - 1 + width) >= NumColsInLandingZone)
			{
				placePieceStatus = PlacePieceStatus.TooFarToTheRight;
				return false;
			}
			#endregion

			for (int pieceRow = 0; pieceRow < 3; pieceRow++)
			{
				for (int pieceCol = 0; pieceCol < 3; pieceCol++)
				{
					if (piece.Shape[pieceRow, pieceCol] && (col + pieceCol < NumColsInLandingZone))
						LandingZoneCells[row + pieceRow, col + pieceCol].PieceId = pieceId;
				}
			}
			return true;
		}

		// Once a dropping piece gets a collision, it was added to the BuildArea (done elsewhere)
		// Then this is run to determine if the new structure is stable
		public void CalculateStability() // joe, maybe return bool. or perhaps object detailing stability details
        {
            // Start at the upper row, go through each block in the row
            // Then work down to lower rows, accumulating the various center of mass values
            for (int row = NumRowsInLandingZone - 1; row >= 0; row--)
            {
                for (int col = 0; col < NumColsInLandingZone; col++)
                {
                    if (LandingZoneCells[row, col] == null || LandingZoneCells[row, col].PieceId == 0)
                        continue;

                    // continue here
                }
            }
        }

    }
}
