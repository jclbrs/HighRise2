using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.SimulationLogic.Models;

namespace ScriptDefinitions.Assets.Scripts.SimulationLogic
{
    // Currently the number of springs in the springboard is hard coded at 6
    // If needed, this can be re-written to have it be configurable
    public class SpringboardLogic
    {
        public List<int> Springs;

        public SpringboardLogic()
		{
			InitializeSprings();
		}

		private void InitializeSprings()
		{
			Springs = new List<int> { int.MinValue, int.MinValue, int.MinValue, int.MinValue, int.MinValue, int.MinValue };
		}

		public int FirstAvailableSpring()
		{
            return Springs.FindLastIndex(i => i > int.MinValue) + 1;
		}

        public bool TryMovePieceToAvailableSpring(int pieceId, int width, out int springIdx)
		{
            springIdx = FirstAvailableSpring();
            if (springIdx + width > 6) // piece won't fit.  
                return false;

            return true;
		}

        public void DropPieceOntoSpringboard(SimPiece simPiece) // pieceId, int width, int springIdx)
		{
            for (int i = 0; i < simPiece.GetSimWidth(); i++)
            {
                Springs[simPiece.SpringboardColumn + i] = simPiece.Id;
            }

        }

		public void ClearSpringboard()
		{
            InitializeSprings();
		}
	}
}
