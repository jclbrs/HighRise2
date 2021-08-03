using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.SimulationLogic.Models
{
	// This consists of a piece on a landing zone row, and identifies its accumulated center of gravity
	public class RowPieceForce
	{
		public int StartColIdx;
		public int EndColIdx;
		public float Mass; // each block has a weight of 1 (so if 2 blocks from a piece are on that row, then the weight is 2)
		public float CtrOfMass; // the center of mass of the portion of the piece on this row
		public float MassAbove; // The calculated weight on the center of gravity of all the pieces above, resting on the current one
		public float CtrOfMassAbove;
		public float AccumulatedMass; // includes all of the weights above, aka: = Weight + WeightAbove
		public float AccumulatedCtrOfMass; // The center of mass, including the forces from above
	}
}
