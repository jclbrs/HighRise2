using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Logic.Models;

namespace Assets.Scripts.Logic
{
	public class PieceLibrary
	{
		public Dictionary<int, Piece> Pieces { get; private set; }

		public PieceLibrary()
		{
			Pieces = new Dictionary<int, Piece>();

			// ============ Level 1: YELLOW (or maybe white) ========
			/*		...
					...
					X..			*/
			Pieces.Add(0,new Piece(0, 1, true, false, false, false, false, false, false, false, false));
			//--------------------------------------------------------------------------------------
			/*		...
					...
					XX.			*/
			Pieces.Add(1, new Piece(1, 1, true, true, false, false, false, false, false, false, false));
			//--------------------------------------------------------------------------------------
			/*		...
					...
					XXX			*/
			Pieces.Add(2, new Piece(2, 1, true, true, true, false, false, false, false, false, false));
			//--------------------------------------------------------------------------------------
			/*		...
					X..
					X..			*/
			Pieces.Add(3, new Piece(3,1,  true, false, false, true, false, false, false, false, false));
			//--------------------------------------------------------------------------------------
			/*		...
					XX.
					XX.			*/
			Pieces.Add(4, new Piece(4, 1, true, true, false, true, true, false, false, false, false));
			//--------------------------------------------------------------------------------------
			/*		X..
					X..
					X..			*/
			Pieces.Add(5, new Piece(5, 1, true, false, false, true, false, false, true, false, false));
			//--------------------------------------------------------------------------------------
			/*		XX.
					XX.
					XX.			*/
			Pieces.Add(6, new Piece(6, 1, true, true, false, true, true, false, true, true, false));
			//--------------------------------------------------------------------------------------
			/*		XXX
					XXX
					XXX			*/
			Pieces.Add(7, new Piece(7, 1, true, true, true, true, true, true, true, true, true));


			// ============ Level 2: RED ========
			/*		...
					X..
					XX.			*/
			Pieces.Add(8, new Piece(8, 2, true, true, false, true, false, false, false, false, false));
			//--------------------------------------------------------------------------------------
			/*		...
					.X.
					XX.			*/
			Pieces.Add(9, new Piece(9, 2, true, true, false, false, true, false, false, false, false));
			//--------------------------------------------------------------------------------------
			/*		...
					X..
					XXX			*/
			Pieces.Add(10, new Piece(10, 2,true, true, true, true, false, false, false, false, false));
			//--------------------------------------------------------------------------------------
			/*		...
					.X.
					XXX			*/
			Pieces.Add(11, new Piece(11, 2, true, true, true, false, true, false, false, false, false));
			//--------------------------------------------------------------------------------------
			/*		...
					..X
					XXX			*/
			Pieces.Add(12, new Piece(12, 2, true, true, true, false, false, true, false, false, false));
			//--------------------------------------------------------------------------------------
			/*		X..
					X.X
					XXX			*/
			Pieces.Add(13, new Piece(13, 2, true, true, true, true, false, true, true, false, false));
			//--------------------------------------------------------------------------------------
			/*		...
					X.X
					XXX			*/
			Pieces.Add(14, new Piece(14, 2, true, true, true, true, false, true, false, false, false));
			//--------------------------------------------------------------------------------------
			/*		...
					XXX
					X.X			*/
			Pieces.Add(15, new Piece(15, 2, true, false, true, true, true, true, false, false, false));


			// ============ Level 3: GREEN ========
			/*		X..
					X..
					XX.			*/
			Pieces.Add(16, new Piece(16, 3, true, true, false, true, false, false, true, false, false));
			//--------------------------------------------------------------------------------------
			/*		.X.
					.X.
					XX.			*/
			Pieces.Add(17, new Piece(17, 3,true, true, false, false, true, false, false, true, false));
			//--------------------------------------------------------------------------------------
			/*		X..
					XX.
					XX.			*/
			Pieces.Add(18, new Piece(18, 3,true, true, false, true, true, false, true, false, false));
			//--------------------------------------------------------------------------------------
			/*		.X.
					XX.
					XX.			*/
			Pieces.Add(19, new Piece(19, 3,true, true, false, true, true, false, false, true, false));
			//--------------------------------------------------------------------------------------
			/*		X..
					X..
					XXX			*/
			Pieces.Add(20, new Piece(20, 3,true, true, true, true, false, false, true, false, false));
			//--------------------------------------------------------------------------------------
			/*		..X
					..X
					XXX			*/
			Pieces.Add(21, new Piece(21, 3,true, true, true, false, false, true, false, false, true));
			//--------------------------------------------------------------------------------------
			/*		.X.
					.X.
					XXX			*/
			Pieces.Add(22, new Piece(22, 3,true, true, true, false, true, false, false, true, false));
			//--------------------------------------------------------------------------------------
			/*		XX.
					XX.
					XXX			*/
			Pieces.Add(23, new Piece(23, 3,true, true, true, true, true, false, true, true, false));

			// ============ Level 4: BLUE ========
			/*		.XX
					.XX
					XXX			*/
			Pieces.Add(24, new Piece(24, 4,true, true, true, false, true, true, false, true, true));
			//--------------------------------------------------------------------------------------
			/*		X.X
					XXX
					XXX			*/
			Pieces.Add(25, new Piece(25, 4,true, true, true, true, true, true, true, false, true));
			//--------------------------------------------------------------------------------------
			/*		X.X
					X.X
					XXX			*/
			Pieces.Add(26, new Piece(26, 4,true, true, true, true, false, true, true, false, true));
			//--------------------------------------------------------------------------------------
			/*		..X
					.XX
					XXX			*/
			Pieces.Add(27, new Piece(27, 4,true, true, true, false, true, true, false, false, true));
			//--------------------------------------------------------------------------------------
			/*		X..
					XX.
					XXX			*/
			Pieces.Add(28, new Piece(28, 4,true, true, true, true, true, false, true, false, false));
			//--------------------------------------------------------------------------------------
			/*		.X.
					.XX
					XXX			*/
			Pieces.Add(29, new Piece(29, 4,true, true, true, false, true, true, false, true, false));
			//--------------------------------------------------------------------------------------
			/*		.X.
					XX.
					XXX			*/
			Pieces.Add(30, new Piece(30, 4,true, true, true, true, true, false, false, true, false));
			//--------------------------------------------------------------------------------------
			/*		.X.
					XXX
					XXX			*/
			Pieces.Add(31, new Piece(31, 4,true, true, true, true, true, true, false, true, false));

			// ============ MORE PIECES - COLOR TBD ========


			// ============ Level 5 ========
			//--------------------------------------------------------------------------------------
			/*		...
					XX.
					.XX			*/
			Pieces.Add(32, new Piece(32, 5,false, true, true, true, true, false, false, false, false));
			//--------------------------------------------------------------------------------------
			/*		...
					XX.
					.X.			*/
			Pieces.Add(33, new Piece(33, 5, false, true, false, true, true, false, false, false, false));
			//--------------------------------------------------------------------------------------
			/*		.X.
					XX.
					.X.			*/
			Pieces.Add(34, new Piece(34, 5, false, true, false, true, true, false, false, true, false));
			//--------------------------------------------------------------------------------------
			/*		XX.
					XX.
					.X.			*/
			Pieces.Add(35, new Piece(35, 5, false, true, false, true, true, false, true, true, false));
			//--------------------------------------------------------------------------------------
			/*		XX.
					.X.
					.X.			*/
			Pieces.Add(36, new Piece(36, 5, false, true, false, false, true, false, true, true, false));
			//--------------------------------------------------------------------------------------
			/*		XX.
					.X.
					.XX			*/
			Pieces.Add(37, new Piece(37, 5, false, true, true, false, true, false, true, true, false));
			//--------------------------------------------------------------------------------------
			/*		.X.
					XX.
					.XX			*/
			Pieces.Add(38, new Piece(38, 5, false, true, true, true, true, false, false, true, false));
			//--------------------------------------------------------------------------------------
			/*		XX.
					XX.
					.XX			*/
			Pieces.Add(39, new Piece(39, 5, false, true, true, true, true, false, true, true, false));

			// ============ Level 6 ========
			//--------------------------------------------------------------------------------------
			/*		XX.
					.XX
					.XX			*/
			Pieces.Add(40, new Piece(40, 6, false, true, true, false, true, true, true, true, false));
			//--------------------------------------------------------------------------------------
			/*		X..
					XXX
					.XX			*/
			Pieces.Add(41, new Piece(41, 6, false, true, true, true, true, true, true, false, false));
			//--------------------------------------------------------------------------------------
			/*		...
					.XX
					XX.			*/
			Pieces.Add(42, new Piece(42, 6, true, true, false, false, true, true, false, false, false));
			//--------------------------------------------------------------------------------------
			/*		...
					XX.
					X..			*/
			Pieces.Add(43, new Piece(43, 6, true, false, false, true, true, false, false, false, false));
			//--------------------------------------------------------------------------------------
			/*		X..
					XX.
					X..			*/
			Pieces.Add(44, new Piece(44, 6, true, false, false, true, true, false, true, false, false));
			//--------------------------------------------------------------------------------------
			/*		XX.
					XX.
					X..			*/
			Pieces.Add(45, new Piece(45, 6, true, false, false, true, true, false, true, true, false));
			//--------------------------------------------------------------------------------------
			/*		XX.
					X..
					X..			*/
			Pieces.Add(46, new Piece(46, 6, true, false, false, true, false, false, true, true, false));
			//--------------------------------------------------------------------------------------
			/*		.XX
					.X.
					XX.			*/
			Pieces.Add(47, new Piece(47, 6, true, true, false, false, true, false, false, true, true));

			// ============ Level 7 ========
			//--------------------------------------------------------------------------------------
			/*		.X.
					.XX
					XX.			*/
			Pieces.Add(48, new Piece(48, 7, true, true, false, false, true, true, false, true, false));
			//--------------------------------------------------------------------------------------
			/*		.XX
					XX.
					.X.			*/
			Pieces.Add(49, new Piece(49, 7, false, true, false, true, true, false, false, true, true));
			//--------------------------------------------------------------------------------------
			/*		XX.
					XXX
					.X.			*/
			Pieces.Add(50, new Piece(50, 7, false, true, false, true, true, true, true, true, false));
			//--------------------------------------------------------------------------------------
			/*		X..
					XXX
					.X.			*/
			Pieces.Add(51, new Piece(51, 7, false, true, false, true, true, true, true, false, false));
			//--------------------------------------------------------------------------------------
			/*		XX.
					.XX
					.X.			*/
			Pieces.Add(52, new Piece(52, 7, false, true, false, false, true, true, true, true, false));
			//--------------------------------------------------------------------------------------
			/*		.XX
					XXX
					.X.			*/
			Pieces.Add(53, new Piece(53, 7, false, true, false, true, true, true, false, true, true));
			//--------------------------------------------------------------------------------------
			/*		..X
					XXX
					.X.			*/
			Pieces.Add(54, new Piece(54, 7, false, true, false, true, true, true, false, false, true));
			//--------------------------------------------------------------------------------------
			/*		.XX
					.XX
					XX.			*/
			Pieces.Add(55, new Piece(55, 7, true, true, false, false, true, true, false, true, true));

			// ============ Level 8 ========
			//--------------------------------------------------------------------------------------
			/*		.XX
					XX.
					XX.			*/
			Pieces.Add(56, new Piece(56, 8, true, true, false, true, true, false, false, true, true));
			//--------------------------------------------------------------------------------------
			/*		..X
					XX.
					XX.			*/
			Pieces.Add(57, new Piece(57, 8, true, true, false, true, true, false, false, false, true));
			//--------------------------------------------------------------------------------------
			/*		XXX
					.XX
					.X.			*/
			Pieces.Add(58, new Piece(58, 8, false, true, false, false, true, true, true, true, true));
			//--------------------------------------------------------------------------------------
			/*		XXX
					XX.
					.X.			*/
			Pieces.Add(59, new Piece(59, 8, false, true, false, true, true, false, true, true, true));
			//--------------------------------------------------------------------------------------
			/*		...
					XXX
					.X.			*/
			Pieces.Add(60, new Piece(60, 8, false, true, false, true, true, true, false, false, false));
			//--------------------------------------------------------------------------------------
			/*		XXX
					.X.
					.X.			*/
			Pieces.Add(61, new Piece(61, 8, false, true, false, false, true, false, true, true, true));
			//--------------------------------------------------------------------------------------


		}
	}
}
