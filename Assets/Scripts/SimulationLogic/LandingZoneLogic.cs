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
		public int NumRowsInLandingZone { get; private set; } = 24; // Actually only 21 used. the 3 remaining are to place the test piece in a starting position for calculations
        public int NumColsInLandingZone { get; private set; } =  6; 

        public LandingZoneLogic()
		{
			ClearLandingZone();
		}

		public void ClearLandingZone()
		{
			LandingZone = new LandingZoneCell[NumColsInLandingZone, NumRowsInLandingZone];
			for (int col = 0; col < NumColsInLandingZone; col++)
			{
				for (int row = 0; row < NumRowsInLandingZone; row++)
				{
					LandingZone[col, row] = new LandingZoneCell();
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
		public void StartNewPiecesPositioning(List<SimPiece> sprungPieces)
		{
			PlacePieceStatus placePieceStatus;
			foreach (SimPiece piece in sprungPieces)
			{
				if (!TryPlacePiece(piece.Id, NumRowsInLandingZone-1 - 3, piece.SpringboardColumn, out placePieceStatus))
					throw new Exception($"Exception dropping piece {piece.Id} onto landing area at col {piece.SpringboardColumn}");
			}
		}

		public int FindLandingPosition(int zoneIdx, int pieceId)
		{
			SimPiece piece = SimPieceLibrary.SimPieces[pieceId];
			// start just above the top of landing zone, and work down
			for (int zoneRow = NumRowsInLandingZone-3; zoneRow > 0; zoneRow--)
			{ // joe spelling this out in detail for now, to understand what is needed, then we can improve the algorithm
				// piece col 0
				if (piece.Shape[0, 0])
				{
					//if (LandingZone[zoneIdx, 10].PieceId > int.MinValue)
						if (LandingZone[zoneIdx,zoneRow-1].PieceId > int.MinValue) // found another piece just below
						return zoneRow;
				}
				if (piece.Shape[0, 1])
				{
					if (LandingZone[zoneIdx, zoneRow].PieceId > int.MinValue) 
						return zoneRow;
				}
				if (piece.Shape[0, 2])
				{
					if (LandingZone[zoneIdx, zoneRow + 1].PieceId > int.MinValue)
						return zoneRow;
				}

				if (piece.Shape[1, 0])
				{
					if (LandingZone[zoneIdx+1, zoneRow - 1].PieceId > int.MinValue)
						return zoneRow;
				}
				if (piece.Shape[1, 1])
				{
					if (LandingZone[zoneIdx + 1, zoneRow].PieceId > int.MinValue)
						return zoneRow;
				}
				if (piece.Shape[1, 2])
				{
					if (LandingZone[zoneIdx + 1, zoneRow + 1].PieceId > int.MinValue)
						return zoneRow;
				}
				if (piece.Shape[2, 0])
				{
					if (LandingZone[zoneIdx + 2, zoneRow - 1].PieceId > int.MinValue)
						return zoneRow;
				}
				if (piece.Shape[2, 1])
				{
					if (LandingZone[zoneIdx + 2, zoneRow].PieceId > int.MinValue)
						return zoneRow;
				}
				if (piece.Shape[2, 2])
				{
					if (LandingZone[zoneIdx + 2, zoneRow + 1].PieceId > int.MinValue)
						return zoneRow;
				}
			}
			return 0;
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
