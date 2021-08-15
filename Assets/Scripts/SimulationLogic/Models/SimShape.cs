using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.SimulationLogic.Enums;

namespace Assets.Scripts.SimulationLogic.Models
{
    public class SimShape
    {
        public int Id { get; private set; }
        public bool[,] Blocks { get; private set; }
        public int LevelFirstAppears { get; private set; }
        public PieceState CurrentState { get; set; }
        public int SpringboardColumn { get; set; } // this will be set once the piece is set on the springboard, and used in the landing zone
                                                   // it refers to the bottom left of the piece, even if the piece is '7' shaped, and nothing solid there
        public int SpringboardRow { get; set; } // this will be set where the piece is set after dropping into the landing zone

        public SimShape(SimShape sp): this(sp.Id, sp.LevelFirstAppears, sp.Blocks[0, 0], sp.Blocks[1, 0], sp.Blocks[2, 0], sp.Blocks[0, 1], sp.Blocks[1, 1], sp.Blocks[2, 1], sp.Blocks[0, 2], sp.Blocks[1, 2], sp.Blocks[2, 2])
		{}

		public SimShape(int id, int levelFirstAppears, bool c0r0, bool c1r0, bool c2r0, bool c0r1, bool c1r1, bool c2r1, bool c0r2, bool c1r2, bool c2r2)
        {
            Id = id;
            LevelFirstAppears = levelFirstAppears;
            CurrentState = PieceState.NotActive;
            SpringboardColumn = int.MinValue;
            SpringboardRow = int.MinValue; 
            Blocks = new bool[3, 3];
            // defines the shape of the piece with 9 blocks in a 3x3 array
            Blocks[0, 0] = c0r0; // bottom left
            Blocks[1, 0] = c1r0;
            Blocks[2, 0] = c2r0;
            Blocks[0, 1] = c0r1;
            Blocks[1, 1] = c1r1;
            Blocks[2, 1] = c2r1;
            Blocks[0, 2] = c0r2;
            Blocks[1, 2] = c1r2;
            Blocks[2, 2] = c2r2; // upper right
        }

        public int GetSimWidth()
		{
            int width = 1;
            if (Blocks[2, 0] || Blocks[2, 1] || Blocks[2, 2])
                width = 3;
            else if (Blocks[1, 0] || Blocks[1, 1] || Blocks[1,2])
                width = 2;

            return width;
		}

        public int FirstSolidRow(int col)
		{
            for (int row = 0; row < 3; row++)
			{
                if (Blocks[row, col])
                    return row;
			}
            return int.MinValue;
		}
    }

}
