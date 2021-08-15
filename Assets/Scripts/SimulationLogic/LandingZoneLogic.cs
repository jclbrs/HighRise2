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
		private int _landingSuccessRow;

		private List<RowPieceForce> _currentRowForces = new List<RowPieceForce>();
		private List<RowPieceForce> _aboveRowForces = new List<RowPieceForce>(); // the accumulation of row forces for the rows above

		public LandingZoneLogic(int landingSuccessRow)
		{
			ClearLandingZone();
			_landingSuccessRow = landingSuccessRow;
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


		public bool MoveSpringboardPiecesToLandingZone(List<SimPiece> simPieces)
		{
			DropPiecesToRestingPosition(simPieces);
			bool isStable = CalculateStability();
			return isStable;
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
		public bool CalculateStability()
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
			// This row passed, so the "current row forces" are now the "above row forces", for the next row
			_aboveRowForces.Clear();
			_aboveRowForces.AddRange(_currentRowForces);
			_currentRowForces.Clear();
			return true;
		}

		private bool CalculatePieceOnRow(int row, ref int col)
		{
			// block is present
			int currentCol = col;
			RowPieceForce rowPieceForce = new RowPieceForce();
			rowPieceForce.StartColIdx = currentCol;
			// Find how wide the piece is on this row
			int nextCol = CalcRowForceStartAndEndIndex(row, currentCol, ref rowPieceForce);
			col = nextCol; // this is passed back to the caller by reference
			rowPieceForce.Mass = rowPieceForce.EndColIdx - rowPieceForce.StartColIdx + 1; // Each block has a weight of 1.  So a piece that is 3 wide on this row has a weight = 3

			// note: weight of a block is coincidentally the same as the width of the block, so can use it in the equation
			//       if we change that, the code will need to be adjusted
			int rowPieceWidth = rowPieceForce.EndColIdx - rowPieceForce.StartColIdx + 1;
			rowPieceForce.CtrOfMass = rowPieceForce.StartColIdx + rowPieceWidth / 2f;
			// Now find if there are accumulated pieces above that have a center of gravity on this current piece

			// Find the applicable 'above' force, if any, and apply it to this force
			RowPieceForce forceAbove = _aboveRowForces.Find(r => rowPieceForce.StartColIdx <= r.AccumulatedCtrOfMass && rowPieceForce.EndColIdx >= r.AccumulatedCtrOfMass);
			if (forceAbove != null)
			{
				rowPieceForce.MassAbove = forceAbove.AccumulatedMass;
				rowPieceForce.CtrOfMassAbove = forceAbove.AccumulatedCtrOfMass;
			}
			CalcEffectOfAboveForces(rowPieceForce);

			// === Run stability checks ====
			// Check if already on the ground
			if (row == 0)
			{
				_currentRowForces.Add(rowPieceForce); // joe is this needed????
				return true;
			}

			// Check if exactly on the border
			// note: since pieces can only be up to 3 blocks wide, this means that the piece's blocks on this row must be 2 blocks wide
			int accumulatedCtrOfMassAsInt = Convert.ToInt32(rowPieceForce.AccumulatedCtrOfMass);
			if (rowPieceForce.AccumulatedCtrOfMass == accumulatedCtrOfMassAsInt)
			{
				// yes, it is on the border.  Now determine what is below it, on left and right
				// note: because of the geometric limitations of 3 blocks max, there must be at least 1 block below it (left or right)
				int pieceIdBelowLeft = LandingZone[accumulatedCtrOfMassAsInt - 1, row - 1].PieceId;
				int pieceIdBelowRight = LandingZone[accumulatedCtrOfMassAsInt, row - 1].PieceId;
				bool hasPieceBelowLeft = (pieceIdBelowLeft > int.MinValue);
				bool hasPieceBelowRight = (pieceIdBelowRight > int.MinValue);
				if (hasPieceBelowLeft && hasPieceBelowRight) // There are piece blocks below on left and right.  
				{
					if (pieceIdBelowLeft == pieceIdBelowRight)
					{
						// The blocks on left and right are part of the same piece, so leave this current piece's center of mass as is
						// Joe, there is a potential flaw here:
						//		we are using the piece id to indicate whether it is the same piece
						//		In reality, we could have 2 different pieces with the same shape (same pieceId) next to each other
						// We should consider having unique identifiers for the pieces, and not rely solely on pieceId
						return true;
					}

					// The blocks below are from different pieces, so split this piece's center of mass as 2 separate forces, directly on center of below left and below right
					RowPieceForce splitForceOnLeft = SplitRowForceIntoTwo(ref rowPieceForce);
					_currentRowForces.Add(rowPieceForce);
					_currentRowForces.Add(splitForceOnLeft);
					return true;
				}
				else // there is only 1 block beneath this piece.  We will allow stability here, by moving the center of mass slightly within the lower block's range
				{
					float shiftCenterOfMass = (hasPieceBelowLeft ? -0.1f : 0.1f);
					rowPieceForce.AccumulatedCtrOfMass += shiftCenterOfMass;
					return true;
				}
			}


			// Check if above another piece
			int colIdxWithCtrOfMass = (int)Math.Floor(rowPieceForce.AccumulatedCtrOfMass);
			if (LandingZone[colIdxWithCtrOfMass, row - 1].PieceId > int.MinValue)
				return true;
			else // not above another piece.  This means either it is straddling 2 pieces below, or its center is not stable
			{
				// check if straddling, which can only happen if the piece is 3-blocks wide
				bool is3BlocksWide = (rowPieceForce.EndColIdx - rowPieceForce.StartColIdx + 1 == 3);
				bool hasBelowLeftPiece = (LandingZone[currentCol, row - 1].PieceId > int.MinValue);
				bool hasBelowRightPiece = (LandingZone[currentCol + 2, row - 1].PieceId > int.MinValue);
				if (is3BlocksWide && hasBelowLeftPiece && hasBelowRightPiece)
				{
					List<RowPieceForce> straddleAdjustedPieces = CalcStraddledForces(rowPieceForce);
					_currentRowForces.AddRange(straddleAdjustedPieces);
					return true;
				}
				else
				{
					return false;
				}
			}
			return false; // the code shouldn't get here, as hopefully, all cases are covered above
		}

		private List<RowPieceForce> CalcStraddledForces(RowPieceForce rowPieceForce)
		{
			RowPieceForce leftForce = new RowPieceForce();
			RowPieceForce rightForce = new RowPieceForce();

			leftForce.StartColIdx = rowPieceForce.StartColIdx;
			leftForce.EndColIdx = rowPieceForce.StartColIdx;
			rightForce.StartColIdx = rowPieceForce.EndColIdx;
			rightForce.EndColIdx = rowPieceForce.EndColIdx;
			leftForce.Mass = 1f;
			rightForce.Mass = 1f;
			leftForce.MassAbove = rowPieceForce.MassAbove / 2f;
			rightForce.MassAbove = rowPieceForce.MassAbove / 2f;
			leftForce.AccumulatedMass = leftForce.Mass + leftForce.MassAbove;
			rightForce.AccumulatedMass = rightForce.Mass + rightForce.MassAbove;
			leftForce.CtrOfMass = leftForce.StartColIdx + 0.5f;
			rightForce.CtrOfMass = rightForce.StartColIdx + 0.5f;
			leftForce.CtrOfMassAbove = leftForce.CtrOfMass;
			rightForce.CtrOfMassAbove = rightForce.CtrOfMass;
			leftForce.AccumulatedCtrOfMass = leftForce.CtrOfMass;
			rightForce.AccumulatedCtrOfMass = rightForce.CtrOfMass;
			List<RowPieceForce> straddledForces = new List<RowPieceForce>();
			straddledForces.Add(leftForce);
			straddledForces.Add(rightForce);
			return straddledForces;
		}

		private void CalcEffectOfAboveForces(RowPieceForce rowPieceForce)
		{
			RowPieceForce aboveRowForce = _aboveRowForces.Find(p => p.AccumulatedCtrOfMass >= rowPieceForce.StartColIdx && p.AccumulatedCtrOfMass <= rowPieceForce.EndColIdx + 1);
			if (aboveRowForce != null)
			{
				rowPieceForce.CtrOfMassAbove = aboveRowForce.AccumulatedCtrOfMass;
				rowPieceForce.MassAbove = aboveRowForce.AccumulatedMass;
			}
			// now calculate the total weight and center of gravity for this piece, including the effect of the pieces weighing down on it
			rowPieceForce.AccumulatedMass = rowPieceForce.Mass + rowPieceForce.MassAbove; // Joe, this could be a calculated property in RowPieceForce class
			rowPieceForce.AccumulatedCtrOfMass = (rowPieceForce.Mass * rowPieceForce.CtrOfMass + rowPieceForce.MassAbove * rowPieceForce.CtrOfMassAbove) / rowPieceForce.AccumulatedMass;
			_currentRowForces.Add(rowPieceForce);
		}

		private int CalcRowForceStartAndEndIndex(int row, int col, ref RowPieceForce rowPieceForce)
		{
			rowPieceForce.StartColIdx = col;
			int nextCol = col;
			for (int i = 0; i + col < 6; i++)
			{
				if (LandingZone[i + col, row].PieceId == LandingZone[col, row].PieceId)
					rowPieceForce.EndColIdx = i + col;
				else
					break;
			}
			nextCol = rowPieceForce.EndColIdx + 1;
			return nextCol;
		}

		private static RowPieceForce SplitRowForceIntoTwo(ref RowPieceForce rowPieceForce)
		{
			RowPieceForce splitForceOnLeft = new RowPieceForce();

			splitForceOnLeft.Mass = rowPieceForce.Mass / 2f;
			rowPieceForce.Mass /= 2f;

			splitForceOnLeft.MassAbove = rowPieceForce.MassAbove / 2f;
			rowPieceForce.MassAbove /= 2f;

			splitForceOnLeft.StartColIdx = rowPieceForce.StartColIdx;
			rowPieceForce.StartColIdx += 1;

			splitForceOnLeft.EndColIdx = rowPieceForce.StartColIdx;

			splitForceOnLeft.CtrOfMass = rowPieceForce.CtrOfMass - 0.5f; // possible since piece is on border, and a block width == 1
			rowPieceForce.CtrOfMass += 0.5f;

			splitForceOnLeft.AccumulatedMass = rowPieceForce.AccumulatedMass / 2f;
			rowPieceForce.AccumulatedMass /= 2f;

			splitForceOnLeft.AccumulatedCtrOfMass = splitForceOnLeft.CtrOfMass;
			rowPieceForce.AccumulatedCtrOfMass = rowPieceForce.CtrOfMass;

			splitForceOnLeft.CtrOfMassAbove = splitForceOnLeft.CtrOfMass;
			rowPieceForce.CtrOfMassAbove = rowPieceForce.CtrOfMass;
			return splitForceOnLeft;
		}

		public int GetHighestPieceRowIdx()
		{
			for (int zoneRow = NumRowsInLandingZone - 3; zoneRow >= 0; zoneRow--)
			{
				for (int zoneCol = 0; zoneCol < NumColsInLandingZone; zoneCol++)
				{
					if (LandingZone[zoneCol, zoneRow].PieceId > int.MinValue)
						return zoneRow;
				}
			}
			return -1;
		}

		public bool IsLevelSuccess()
		{
			return (CalculateStability() && (GetHighestPieceRowIdx() >= _landingSuccessRow));
		}
	}
}
