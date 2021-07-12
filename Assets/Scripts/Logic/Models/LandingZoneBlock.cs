using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Logic.Models
{
    public class LandingZoneBlock
    {
        // Each block has a logical width of 1.0f (for calculation purposes)

        // If pieceId == 0, then there is no piece in that block
        // Otherwise, there is a piece, and it has an individual mass of 1
        public int PieceId { get; set; }

        // The AccumulatedXCenterOfMass is the result of all of the pieces above weighing down.
        // If their center of mass is within this block's width, the the Accumulated XCenterOfMass is the calculation of the new Mass Center
        public float AccumulatedXCenterOfMass { get; set; }

        public LandingZoneBlock()
		{
            PieceId = int.MinValue;
            AccumulatedXCenterOfMass = 0.0f;
		}
    }

}
