using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Logic.Models
{
    public class Piece
    {
        public int Id { get; private set; }
        public bool[,] Shape { get; private set; }
        public int LevelFirstAppears { get; private set; }

        public Piece(int id, int levelFirstAppears, bool r0c0, bool r0c1, bool r0c2, bool r1c0, bool r1c1, bool r1c2, bool r2c0, bool r2c1, bool r2c2)
        {
            Id = id;
            LevelFirstAppears = levelFirstAppears;
            Shape = new bool[3, 3];
            // defines the shape of the piece with 9 blocks in a 3x3 array
            Shape[0, 0] = r0c0; // bottom left
            Shape[0, 1] = r0c1;
            Shape[0, 2] = r0c2;
            Shape[1, 0] = r1c0;
            Shape[1, 1] = r1c1;
            Shape[1, 2] = r1c2;
            Shape[2, 0] = r2c0;
            Shape[2, 1] = r2c1;
            Shape[2, 2] = r2c2; // upper right
        }

        public int GetWidth()
		{
            int width = 1;
            if (Shape[0, 2] || Shape[1, 2] || Shape[2, 2])
                width = 3;
            else if (Shape[0, 1] || Shape[1, 2] || Shape[2, 1])
                width = 2;

            return width;
		}
    }

}
