using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Logic.Models;

namespace Assets.Scripts.Logic
{
	public static class PieceLibrary
	{
		private static List<Piece> _pieces;
		public static List<Piece> Pieces
		{
			get
			{
				if (_pieces == null)
					Initialize();
				
				return _pieces;
			}
		}

		private static void Initialize()
		{
			_pieces = new List<Piece>();

			// ============ Level 1: YELLOW (or maybe white) ========
			/*		...
					...
					X..			*/
			Pieces.Add(new Piece(0, 1, true, false, false, false, false, false, false, false, false));
			//--------------------------------------------------------------------------------------
			/*		...
					...
					XX.			*/
			Pieces.Add(new Piece(1, 1, true, true, false, false, false, false, false, false, false));
			//--------------------------------------------------------------------------------------
			/*		...
					...
					XXX			*/
			Pieces.Add(new Piece(2, 1, true, true, true, false, false, false, false, false, false));
			//--------------------------------------------------------------------------------------
			/*		...
					X..
					X..			*/
			Pieces.Add(new Piece(3,1,  true, false, false, true, false, false, false, false, false));
			//--------------------------------------------------------------------------------------
			/*		...
					XX.
					XX.			*/
			Pieces.Add(new Piece(4, 1, true, true, false, true, true, false, false, false, false));
			//--------------------------------------------------------------------------------------
			/*		X..
					X..
					X..			*/
			Pieces.Add(new Piece(5, 1, true, false, false, true, false, false, true, false, false));
			//--------------------------------------------------------------------------------------
			/*		XX.
					XX.
					XX.			*/
			Pieces.Add(new Piece(6, 1, true, true, false, true, true, false, true, true, false));
			//--------------------------------------------------------------------------------------
			/*		XXX
					XXX
					XXX			*/
			Pieces.Add(new Piece(7, 1, true, true, true, true, true, true, true, true, true));


			// ============ Level 2: RED ========
			/*		...
					X..
					XX.			*/
			Pieces.Add(new Piece(8, 2, true, true, false, true, false, false, false, false, false));
			//--------------------------------------------------------------------------------------
			/*		...
					.X.
					XX.			*/
			Pieces.Add(new Piece(9, 2, true, true, false, false, true, false, false, false, false));
			//--------------------------------------------------------------------------------------
			/*		...
					X..
					XXX			*/
			Pieces.Add(new Piece(10, 2,true, true, true, true, false, false, false, false, false));
			//--------------------------------------------------------------------------------------
			/*		...
					.X.
					XXX			*/
			Pieces.Add(new Piece(11, 2, true, true, true, false, true, false, false, false, false));
			//--------------------------------------------------------------------------------------
			/*		...
					..X
					XXX			*/
			Pieces.Add(new Piece(12, 2, true, true, true, false, false, true, false, false, false));
			//--------------------------------------------------------------------------------------
			/*		X..
					X.X
					XXX			*/
			Pieces.Add(new Piece(13, 2, true, true, true, true, false, true, true, false, false));
			//--------------------------------------------------------------------------------------
			/*		...
					X.X
					XXX			*/
			Pieces.Add(new Piece(14, 2, true, true, true, true, false, true, false, false, false));
			//--------------------------------------------------------------------------------------
			/*		...
					XXX
					X.X			*/
			Pieces.Add(new Piece(15, 2, true, false, true, true, true, true, false, false, false));


			// ============ Level 3: GREEN ========
			/*		X..
					X..
					XX.			*/
			Pieces.Add(new Piece(16, 3, true, true, false, true, false, false, true, false, false));
			//--------------------------------------------------------------------------------------
			/*		.X.
					.X.
					XX.			*/
			Pieces.Add(new Piece(17, 3,true, true, false, false, true, false, false, true, false));
			//--------------------------------------------------------------------------------------
			/*		X..
					XX.
					XX.			*/
			Pieces.Add(new Piece(18, 3,true, true, false, true, true, false, true, false, false));
			//--------------------------------------------------------------------------------------
			/*		.X.
					XX.
					XX.			*/
			Pieces.Add(new Piece(19, 3,true, true, false, true, true, false, false, true, false));
			//--------------------------------------------------------------------------------------
			/*		X..
					X..
					XXX			*/
			Pieces.Add(new Piece(20, 3,true, true, true, true, false, false, true, false, false));
			//--------------------------------------------------------------------------------------
			/*		..X
					..X
					XXX			*/
			Pieces.Add(new Piece(21, 3,true, true, true, false, false, true, false, false, true));
			//--------------------------------------------------------------------------------------
			/*		.X.
					.X.
					XXX			*/
			Pieces.Add(new Piece(22, 3,true, true, true, false, true, false, false, true, false));
			//--------------------------------------------------------------------------------------
			/*		XX.
					XX.
					XXX			*/
			Pieces.Add(new Piece(23, 3,true, true, true, true, true, false, true, true, false));

			// ============ Level 4: BLUE ========
			/*		.XX
					.XX
					XXX			*/
			Pieces.Add(new Piece(24, 4,true, true, true, false, true, true, false, true, true));
			//--------------------------------------------------------------------------------------
			/*		X.X
					XXX
					XXX			*/
			Pieces.Add(new Piece(25, 4,true, true, true, true, true, true, true, false, true));
			//--------------------------------------------------------------------------------------
			/*		X.X
					X.X
					XXX			*/
			Pieces.Add(new Piece(26, 4,true, true, true, true, false, true, true, false, true));
			//--------------------------------------------------------------------------------------
			/*		..X
					.XX
					XXX			*/
			Pieces.Add(new Piece(27, 4,true, true, true, false, true, true, false, false, true));
			//--------------------------------------------------------------------------------------
			/*		X..
					XX.
					XXX			*/
			Pieces.Add(new Piece(28, 4,true, true, true, true, true, false, true, false, false));
			//--------------------------------------------------------------------------------------
			/*		.X.
					.XX
					XXX			*/
			Pieces.Add(new Piece(29, 4,true, true, true, false, true, true, false, true, false));
			//--------------------------------------------------------------------------------------
			/*		.X.
					XX.
					XXX			*/
			Pieces.Add(new Piece(30, 4,true, true, true, true, true, false, false, true, false));
			//--------------------------------------------------------------------------------------
			/*		.X.
					XXX
					XXX			*/
			Pieces.Add(new Piece(31, 4,true, true, true, true, true, true, false, true, false));

			// ============ MORE PIECES - COLOR TBD ========


			// ============ Level 5 ========
			//--------------------------------------------------------------------------------------
			/*		...
					XX.
					.XX			*/
			Pieces.Add(new Piece(32, 5,false, true, true, true, true, false, false, false, false));
			//--------------------------------------------------------------------------------------
			/*		...
					XX.
					.X.			*/
			Pieces.Add(new Piece(33, 5, false, true, false, true, true, false, false, false, false));
			//--------------------------------------------------------------------------------------
			/*		.X.
					XX.
					.X.			*/
			Pieces.Add(new Piece(34, 5, false, true, false, true, true, false, false, true, false));
			//--------------------------------------------------------------------------------------
			/*		XX.
					XX.
					.X.			*/
			Pieces.Add(new Piece(35, 5, false, true, false, true, true, false, true, true, false));
			//--------------------------------------------------------------------------------------
			/*		XX.
					.X.
					.X.			*/
			Pieces.Add(new Piece(36, 5, false, true, false, false, true, false, true, true, false));
			//--------------------------------------------------------------------------------------
			/*		XX.
					.X.
					.XX			*/
			Pieces.Add(new Piece(37, 5, false, true, true, false, true, false, true, true, false));
			//--------------------------------------------------------------------------------------
			/*		.X.
					XX.
					.XX			*/
			Pieces.Add(new Piece(38, 5, false, true, true, true, true, false, false, true, false));
			//--------------------------------------------------------------------------------------
			/*		XX.
					XX.
					.XX			*/
			Pieces.Add(new Piece(39, 5, false, true, true, true, true, false, true, true, false));

			// ============ Level 6 ========
			//--------------------------------------------------------------------------------------
			/*		XX.
					.XX
					.XX			*/
			Pieces.Add(new Piece(40, 6, false, true, true, false, true, true, true, true, false));
			//--------------------------------------------------------------------------------------
			/*		X..
					XXX
					.XX			*/
			Pieces.Add(new Piece(41, 6, false, true, true, true, true, true, true, false, false));
			//--------------------------------------------------------------------------------------
			/*		...
					.XX
					XX.			*/
			Pieces.Add(new Piece(42, 6, true, true, false, false, true, true, false, false, false));
			//--------------------------------------------------------------------------------------
			/*		...
					XX.
					X..			*/
			Pieces.Add(new Piece(43, 6, true, false, false, true, true, false, false, false, false));
			//--------------------------------------------------------------------------------------
			/*		X..
					XX.
					X..			*/
			Pieces.Add(new Piece(44, 6, true, false, false, true, true, false, true, false, false));
			//--------------------------------------------------------------------------------------
			/*		XX.
					XX.
					X..			*/
			Pieces.Add(new Piece(45, 6, true, false, false, true, true, false, true, true, false));
			//--------------------------------------------------------------------------------------
			/*		XX.
					X..
					X..			*/
			Pieces.Add(new Piece(46, 6, true, false, false, true, false, false, true, true, false));
			//--------------------------------------------------------------------------------------
			/*		.XX
					.X.
					XX.			*/
			Pieces.Add(new Piece(47, 6, true, true, false, false, true, false, false, true, true));

			// ============ Level 7 ========
			//--------------------------------------------------------------------------------------
			/*		.X.
					.XX
					XX.			*/
			Pieces.Add(new Piece(48, 7, true, true, false, false, true, true, false, true, false));
			//--------------------------------------------------------------------------------------
			/*		.XX
					XX.
					.X.			*/
			Pieces.Add(new Piece(49, 7, false, true, false, true, true, false, false, true, true));
			//--------------------------------------------------------------------------------------
			/*		XX.
					XXX
					.X.			*/
			Pieces.Add(new Piece(50, 7, false, true, false, true, true, true, true, true, false));
			//--------------------------------------------------------------------------------------
			/*		X..
					XXX
					.X.			*/
			Pieces.Add(new Piece(51, 7, false, true, false, true, true, true, true, false, false));
			//--------------------------------------------------------------------------------------
			/*		XX.
					.XX
					.X.			*/
			Pieces.Add(new Piece(52, 7, false, true, false, false, true, true, true, true, false));
			//--------------------------------------------------------------------------------------
			/*		.XX
					XXX
					.X.			*/
			Pieces.Add(new Piece(53, 7, false, true, false, true, true, true, false, true, true));
			//--------------------------------------------------------------------------------------
			/*		..X
					XXX
					.X.			*/
			Pieces.Add(new Piece(54, 7, false, true, false, true, true, true, false, false, true));
			//--------------------------------------------------------------------------------------
			/*		.XX
					.XX
					XX.			*/
			Pieces.Add(new Piece(55, 7, true, true, false, false, true, true, false, true, true));

			// ============ Level 8 ========
			//--------------------------------------------------------------------------------------
			/*		.XX
					XX.
					XX.			*/
			Pieces.Add(new Piece(56, 8, true, true, false, true, true, false, false, true, true));
			//--------------------------------------------------------------------------------------
			/*		..X
					XX.
					XX.			*/
			Pieces.Add(new Piece(57, 8, true, true, false, true, true, false, false, false, true));
			//--------------------------------------------------------------------------------------
			/*		XXX
					.XX
					.X.			*/
			Pieces.Add(new Piece(58, 8, false, true, false, false, true, true, true, true, true));
			//--------------------------------------------------------------------------------------
			/*		XXX
					XX.
					.X.			*/
			Pieces.Add(new Piece(59, 8, false, true, false, true, true, false, true, true, true));
			//--------------------------------------------------------------------------------------
			/*		...
					XXX
					.X.			*/
			Pieces.Add(new Piece(60, 8, false, true, false, true, true, true, false, false, false));
			//--------------------------------------------------------------------------------------
			/*		XXX
					.X.
					.X.			*/
			Pieces.Add(new Piece(61, 8, false, true, false, false, true, false, true, true, true));
			//--------------------------------------------------------------------------------------


		}
	}
}
