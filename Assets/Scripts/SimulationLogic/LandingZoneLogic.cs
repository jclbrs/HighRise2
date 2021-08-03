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
		public int NumColsInLandingZone { get; private set; } = 6;

		private List<RowPieceForce> _currentRowForces = new List<RowPieceForce>();
		private List<RowPieceForce> _aboveRowForces = new List<RowPieceForce>(); // the accumulation of row forces for the rows above

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


		public void MoveSpringboardPiecesToLandingZone(List<SimPiece> simPieces)
		{
			DropPiecesToRestingPosition(simPieces);
			CalculateStability();
		}

		private void DropPiecesToRestingPosition(List<SimPiece> pieces)
		{
			foreach (SimPiece piece in pieces)
			{
				int landingRow = FindLandingPosition(piece.SpringboardColumn, piece.Id);
				PlacePieceStatus placePieceStatus;
				if (!TryPlacePiece(piece.Id, landingRow, piece.SpringboardColumn, out placePieceStatus))
				{
					throw new Exception($"Could not place piece in landing zone: {placePieceStatus}");
				}
			}
		}

		// Place all pieces from springboard to top of landing zone
		public void StartNewPiecesPositioning(List<SimPiece> sprungPieces)
		{
			PlacePieceStatus placePieceStatus;
			foreach (SimPiece piece in sprungPieces)
			{
				if (!TryPlacePiece(piece.Id, NumRowsInLandingZone - 1 - 3, piece.SpringboardColumn, out placePieceStatus))
					throw new Exception($"Exception dropping piece {piece.Id} onto landing area at col {piece.SpringboardColumn}");
			}
		}

		public int FindLandingPosition(int zoneIdx, int pieceId)
		{
			SimPiece piece = SimPieceLibrary.SimPieces[pieceId];
			// start just above the top of landing zone, and work down
			for (int zoneRow = NumRowsInLandingZone - 3; zoneRow > 0; zoneRow--)
			{ // joe spelling this out in detail for now, to understand what is needed, then we can improve the algorithm
			  // piece col 0
				if (piece.Shape[0, 0])
				{
					//if (LandingZone[zoneIdx, 10].PieceId > int.MinValue)
					if (LandingZone[zoneIdx, zoneRow - 1].PieceId > int.MinValue) // found another piece just below
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
					if (LandingZone[zoneIdx + 1, zoneRow - 1].PieceId > int.MinValue)
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
					if (piece.Shape[pieceCol, pieceRow] && (col + pieceCol < NumColsInLandingZone))
						LandingZone[col + pieceCol, row + pieceRow].PieceId = pieceId;
				}
			}
			return true;
		}

		// Once a dropping piece gets a collision, it was added to the BuildArea (done elsewhere)
		// Then this is run to determine if the new structure is stable
		public bool CalculateStability() // joe, maybe return bool. or perhaps object detailing stability details
		{
			bool isStable = false;

			// Start at the upper row, go through each block in the row
			// Then work down to lower rows, accumulating the various center of mass values
			for (int row = NumRowsInLandingZone - 1; row >= 0; row--)
			{
				isStable = CalculateRow(row);
				if (!isStable)
					return false;
			}
			return true;
		}

		private bool CalculateRow(int row)
		{
			int col = 0;
			while (col < 6)
			{
				if (LandingZone[col, row].PieceId == int.MinValue) // no piece block in this landing zone cell
					col++;
				else
				{
					if (!CalculatePieceOnRow(row, ref col)) // the col reference will return the next column to evaluate.  The value depends on the width of the piece
						return false;
				}
			}
			return true;
		}

		private bool CalculatePieceOnRow(int row, ref int col)
		{
			// block is present
			RowPieceForce rowPieceForce = new RowPieceForce();
			rowPieceForce.StartColIdx = col;
			// Find how wide the piece is on this row
			rowPieceForce.EndColIdx = col;
			for (int i = 0; i + col < 5; i++)
			{
				if (LandingZone[i + col, row].PieceId == LandingZone[col, row].PieceId)
					rowPieceForce.EndColIdx = i + col;
				else
					break;
			}
			col = rowPieceForce.EndColIdx + 1;
			rowPieceForce.Weight = rowPieceForce.EndColIdx - rowPieceForce.StartColIdx + 1; // Each block has a weight of 1.  So a piece that is 3 wide on this row has a weight = 3

			// note: weight of a block is coincidentally the same as the width of the block, so can use it in the equation
			//       if we change that, the code will need to be adjusted
			int rowPieceWidth = rowPieceForce.EndColIdx - rowPieceForce.StartColIdx + 1;
			rowPieceForce.CtrOfGrav = rowPieceForce.StartColIdx + rowPieceWidth/2f;
			// Now find if there are accumulated pieces above that have a center of gravity on this current piece
			RowPieceForce aboveRowForce = _aboveRowForces.Find(p => p.CtrOfGrav >= rowPieceForce.StartColIdx && p.CtrOfGrav <= rowPieceForce.EndColIdx);
			if (aboveRowForce != null)
			{
				rowPieceForce.CtrOfGravAbove = aboveRowForce.AccumulatedCtrOfGrav;
				rowPieceForce.WeightAbove = aboveRowForce.AccumulatedWeight;
			}
			// now calculate the total weight and center of gravity for this piece, including the effect of the pieces weighing down on it
			rowPieceForce.AccumulatedWeight = rowPieceForce.Weight + rowPieceForce.WeightAbove; // Joe, this could be a calculated property in RowPieceForce class
			rowPieceForce.AccumulatedCtrOfGrav =  (rowPieceForce.Weight * rowPieceForce.CtrOfGrav + rowPieceForce.WeightAbove * rowPieceForce.CtrOfGravAbove) / rowPieceForce.AccumulatedWeight;
			// We're done with the above forces on this piece, so it can be removed
			_aboveRowForces.Remove(aboveRowForce);
			_currentRowForces.Add(rowPieceForce);
			// Run stability checks
			if (row == 0) // on the ground
				return true;
			// Check if above another piece
			// (joe will need to consider if exactly between 2 lower pieces, will need to decide whether to split into two forces)
			int colIdxWithCtrOfGrav = (int)Math.Floor(rowPieceForce.AccumulatedCtrOfGrav);
			if (LandingZone[colIdxWithCtrOfGrav, row-1].PieceId == int.MinValue)
			{
				// there is no piece below, need to consider 2 cases:
				// 1. straddling - in that case split the piece into 2 pieceRowForces and split the weight and adjust the CtrOfGravities to the center of each lower piece
				// 2. right on the edge of one piece.  In that case, shift it to just within the lower piece
				// Joe, but for now, just return false
				return false;
			}

			return true;
		}
	}
}
