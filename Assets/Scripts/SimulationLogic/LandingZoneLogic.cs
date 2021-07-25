using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.SimulationLogic.Enums;
using Assets.Scripts.SimulationLogic.Models;

namespace Assets.Scripts.SimulationLogic
{
	public class LandingZoneLogic
	{
		public LandingZoneCell[,] LandingZone { get; set; }
		public int NumRowsInLandingZone { get; private set; } = 21;
        public int NumColsInLandingZone { get; private set; } =  5;

        public LandingZoneLogic()
		{
			ClearLandingZone();
		}

		public void ClearLandingZone()
		{
			LandingZone = new LandingZoneCell[NumRowsInLandingZone, NumColsInLandingZone];
			for (int row = 0; row < NumRowsInLandingZone; row++)
			{
				for (int col = 0; col < NumColsInLandingZone; col++)
				{
					LandingZone[row, col] = new LandingZoneCell();
				}
			}
		}

		public void DropPiecesFromSpringboard(List<SimPiece> pieces)
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

		private void DropPiecesToRestingPosition(List<SimPiece> pieces)
		{
			foreach (SimPiece piece in pieces)
			{
				for (int pieceCol = 0; pieceCol < piece.GetSimWidth(); pieceCol++)
				{
					// joe continue here
				}
			}
			// joe continue here
		}

		// Place all pieces from springboard to top of landing zone
		public void StartNewPiecesPositioning(List<SimPiece> pieces)
		{
			PlacePieceStatus placePieceStatus;
			foreach (SimPiece piece in pieces)
			{
				if (!TryPlacePiece(piece.Id, NumRowsInLandingZone-1 - 3, piece.SpringboardColumn, out placePieceStatus))
					throw new Exception($"Exception dropping piece {piece.Id} onto landing area at col {piece.SpringboardColumn}");
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
			SimPiece piece = null;
			try
			{
				piece = SimPieceLibrary.SimPieces[pieceId];
			}
			catch (Exception)
			{
				placePieceStatus = PlacePieceStatus.InvalidPieceId;
				return false;
			}
			int width = piece.GetSimWidth();
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
						LandingZone[row + pieceRow, col + pieceCol].PieceId = pieceId;
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
                    if (LandingZone[row, col] == null || LandingZone[row, col].PieceId == 0)
                        continue;

                    // continue here
                }
            }
        }

    }
}
