using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptDefinitions.Assets.Scripts.SimulationLogic
{
    // Currently the number of springs in the springboard is hard coded at 6
    // If needed, this can be re-written to have it be configurable
    public class SpringboardLogic
    {
        public List<int> Springs;

        public SpringboardLogic()
		{
            Springs = new List<int> { int.MinValue, int.MinValue , int.MinValue , int.MinValue , int.MinValue , int.MinValue };
        }

        public int FirstAvailableSpring()
		{
            return Springs.FindIndex(i => i == int.MinValue);
		}

        public bool TryMovePieceToAvailableSpring(int pieceId, int width, out int springIdx)
		{
            springIdx = FirstAvailableSpring();
            if (springIdx + width >= 6) // piece won't fit.  
                return false;

            for (int i = 0; i < width; i++)
			{
                Springs[springIdx + i] = pieceId;
			}
            return true;
		}
        
    }
}
