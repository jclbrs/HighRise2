using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.SimulationLogic.Models;

namespace Assets.Scripts.SimulationLogic
{
	public static class PieceLibrary
	{
		private static List<SimPiece> _pieces;
		public static List<SimPiece> Pieces
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
			_pieces = new List<SimPiece>();

			// ============ Level 1: YELLOW (or maybe white) ========
			/*		...
					...
					X..			*/
			Pieces.Add(new SimPiece(0, 1, true, false, false, false, false, false, false, false, false));
			//--------------------------------------------------------------------------------------
			/*		...
					...
					XX.			*/
			Pieces.Add(new SimPiece(1, 1, true, true, false, false, false, false, false, false, false));
			//--------------------------------------------------------------------------------------
			/*		...
					...
					XXX			*/
			Pieces.Add(new SimPiece(2, 1, true, true, true, false, false, false, false, false, false));
			//--------------------------------------------------------------------------------------
			/*		...
					X..
					X..			*/
			Pieces.Add(new SimPiece(3,1,  true, false, false, true, false, false, false, false, false));
			//--------------------------------------------------------------------------------------
			/*		...
					XX.
					XX.			*/
			Pieces.Add(new SimPiece(4, 1, true, true, false, true, true, false, false, false, false));
			//--------------------------------------------------------------------------------------
			/*		X..
					X..
					X..			*/
			Pieces.Add(new SimPiece(5, 1, true, false, false, true, false, false, true, false, false));
			//--------------------------------------------------------------------------------------
			/*		XX.
					XX.
					XX.			*/
			Pieces.Add(new SimPiece(6, 1, true, true, false, true, true, false, true, true, false));
			//--------------------------------------------------------------------------------------
			/*		XXX
					XXX
					XXX			*/
			Pieces.Add(new SimPiece(7, 1, true, true, true, true, true, true, true, true, true));


			// ============ Level 2: RED ========
			/*		...
					X..
					XX.			*/
			Pieces.Add(new SimPiece(8, 2, true, true, false, true, false, false, false, false, false));
			//--------------------------------------------------------------------------------------
			/*		...
					.X.
					XX.			*/
			Pieces.Add(new SimPiece(9, 2, true, true, false, false, true, false, false, false, false));
			//--------------------------------------------------------------------------------------
			/*		...
					X..
					XXX			*/
			Pieces.Add(new SimPiece(10, 2,true, true, true, true, false, false, false, false, false));
			//--------------------------------------------------------------------------------------
			/*		...
					.X.
					XXX			*/
			Pieces.Add(new SimPiece(11, 2, true, true, true, false, true, false, false, false, false));
			//--------------------------------------------------------------------------------------
			/*		...
					..X
					XXX			*/
			Pieces.Add(new SimPiece(12, 2, true, true, true, false, false, true, false, false, false));
			//--------------------------------------------------------------------------------------
			/*		X..
					X.X
					XXX			*/
			Pieces.Add(new SimPiece(13, 2, true, true, true, true, false, true, true, false, false));
			//--------------------------------------------------------------------------------------
			/*		...
					X.X
					XXX			*/
			Pieces.Add(new SimPiece(14, 2, true, true, true, true, false, true, false, false, false));
			//--------------------------------------------------------------------------------------
			/*		...
					XXX
					X.X			*/
			Pieces.Add(new SimPiece(15, 2, true, false, true, true, true, true, false, false, false));


			// ============ Level 3: GREEN ========
			/*		X..
					X..
					XX.			*/
			Pieces.Add(new SimPiece(16, 3, true, true, false, true, false, false, true, false, false));
			//--------------------------------------------------------------------------------------
			/*		.X.
					.X.
					XX.			*/
			Pieces.Add(new SimPiece(17, 3,true, true, false, false, true, false, false, true, false));
			//--------------------------------------------------------------------------------------
			/*		X..
					XX.
					XX.			*/
			Pieces.Add(new SimPiece(18, 3,true, true, false, true, true, false, true, false, false));
			//--------------------------------------------------------------------------------------
			/*		.X.
					XX.
					XX.			*/
			Pieces.Add(new SimPiece(19, 3,true, true, false, true, true, false, false, true, false));
			//--------------------------------------------------------------------------------------
			/*		X..
					X..
					XXX			*/
			Pieces.Add(new SimPiece(20, 3,true, true, true, true, false, false, true, false, false));
			//--------------------------------------------------------------------------------------
			/*		..X
					..X
					XXX			*/
			Pieces.Add(new SimPiece(21, 3,true, true, true, false, false, true, false, false, true));
			//--------------------------------------------------------------------------------------
			/*		.X.
					.X.
					XXX			*/
			Pieces.Add(new SimPiece(22, 3,true, true, true, false, true, false, false, true, false));
			//--------------------------------------------------------------------------------------
			/*		XX.
					XX.
					XXX			*/
			Pieces.Add(new SimPiece(23, 3,true, true, true, true, true, false, true, true, false));

			// ============ Level 4: BLUE ========
			/*		.XX
					.XX
					XXX			*/
			Pieces.Add(new SimPiece(24, 4,true, true, true, false, true, true, false, true, true));
			//--------------------------------------------------------------------------------------
			/*		X.X
					XXX
					XXX			*/
			Pieces.Add(new SimPiece(25, 4,true, true, true, true, true, true, true, false, true));
			//--------------------------------------------------------------------------------------
			/*		X.X
					X.X
					XXX			*/
			Pieces.Add(new SimPiece(26, 4,true, true, true, true, false, true, true, false, true));
			//--------------------------------------------------------------------------------------
			/*		..X
					.XX
					XXX			*/
			Pieces.Add(new SimPiece(27, 4,true, true, true, false, true, true, false, false, true));
			//--------------------------------------------------------------------------------------
			/*		X..
					XX.
					XXX			*/
			Pieces.Add(new SimPiece(28, 4,true, true, true, true, true, false, true, false, false));
			//--------------------------------------------------------------------------------------
			/*		.X.
					.XX
					XXX			*/
			Pieces.Add(new SimPiece(29, 4,true, true, true, false, true, true, false, true, false));
			//--------------------------------------------------------------------------------------
			/*		.X.
					XX.
					XXX			*/
			Pieces.Add(new SimPiece(30, 4,true, true, true, true, true, false, false, true, false));
			//--------------------------------------------------------------------------------------
			/*		.X.
					XXX
					XXX			*/
			Pieces.Add(new SimPiece(31, 4,true, true, true, true, true, true, false, true, false));

			// ============ MORE PIECES - COLOR TBD ========


			// ============ Level 5 ========
			//--------------------------------------------------------------------------------------
			/*		...
					XX.
					.XX			*/
			Pieces.Add(new SimPiece(32, 5,false, true, true, true, true, false, false, false, false));
			//--------------------------------------------------------------------------------------
			/*		...
					XX.
					.X.			*/
			Pieces.Add(new SimPiece(33, 5, false, true, false, true, true, false, false, false, false));
			//--------------------------------------------------------------------------------------
			/*		.X.
					XX.
					.X.			*/
			Pieces.Add(new SimPiece(34, 5, false, true, false, true, true, false, false, true, false));
			//--------------------------------------------------------------------------------------
			/*		XX.
					XX.
					.X.			*/
			Pieces.Add(new SimPiece(35, 5, false, true, false, true, true, false, true, true, false));
			//--------------------------------------------------------------------------------------
			/*		XX.
					.X.
					.X.			*/
			Pieces.Add(new SimPiece(36, 5, false, true, false, false, true, false, true, true, false));
			//--------------------------------------------------------------------------------------
			/*		XX.
					.X.
					.XX			*/
			Pieces.Add(new SimPiece(37, 5, false, true, true, false, true, false, true, true, false));
			//--------------------------------------------------------------------------------------
			/*		.X.
					XX.
					.XX			*/
			Pieces.Add(new SimPiece(38, 5, false, true, true, true, true, false, false, true, false));
			//--------------------------------------------------------------------------------------
			/*		XX.
					XX.
					.XX			*/
			Pieces.Add(new SimPiece(39, 5, false, true, true, true, true, false, true, true, false));

			// ============ Level 6 ========
			//--------------------------------------------------------------------------------------
			/*		XX.
					.XX
					.XX			*/
			Pieces.Add(new SimPiece(40, 6, false, true, true, false, true, true, true, true, false));
			//--------------------------------------------------------------------------------------
			/*		X..
					XXX
					.XX			*/
			Pieces.Add(new SimPiece(41, 6, false, true, true, true, true, true, true, false, false));
			//--------------------------------------------------------------------------------------
			/*		...
					.XX
					XX.			*/
			Pieces.Add(new SimPiece(42, 6, true, true, false, false, true, true, false, false, false));
			//--------------------------------------------------------------------------------------
			/*		...
					XX.
					X..			*/
			Pieces.Add(new SimPiece(43, 6, true, false, false, true, true, false, false, false, false));
			//--------------------------------------------------------------------------------------
			/*		X..
					XX.
					X..			*/
			Pieces.Add(new SimPiece(44, 6, true, false, false, true, true, false, true, false, false));
			//--------------------------------------------------------------------------------------
			/*		XX.
					XX.
					X..			*/
			Pieces.Add(new SimPiece(45, 6, true, false, false, true, true, false, true, true, false));
			//--------------------------------------------------------------------------------------
			/*		XX.
					X..
					X..			*/
			Pieces.Add(new SimPiece(46, 6, true, false, false, true, false, false, true, true, false));
			//--------------------------------------------------------------------------------------
			/*		.XX
					.X.
					XX.			*/
			Pieces.Add(new SimPiece(47, 6, true, true, false, false, true, false, false, true, true));

			// ============ Level 7 ========
			//--------------------------------------------------------------------------------------
			/*		.X.
					.XX
					XX.			*/
			Pieces.Add(new SimPiece(48, 7, true, true, false, false, true, true, false, true, false));
			//--------------------------------------------------------------------------------------
			/*		.XX
					XX.
					.X.			*/
			Pieces.Add(new SimPiece(49, 7, false, true, false, true, true, false, false, true, true));
			//--------------------------------------------------------------------------------------
			/*		XX.
					XXX
					.X.			*/
			Pieces.Add(new SimPiece(50, 7, false, true, false, true, true, true, true, true, false));
			//--------------------------------------------------------------------------------------
			/*		X..
					XXX
					.X.			*/
			Pieces.Add(new SimPiece(51, 7, false, true, false, true, true, true, true, false, false));
			//--------------------------------------------------------------------------------------
			/*		XX.
					.XX
					.X.			*/
			Pieces.Add(new SimPiece(52, 7, false, true, false, false, true, true, true, true, false));
			//--------------------------------------------------------------------------------------
			/*		.XX
					XXX
					.X.			*/
			Pieces.Add(new SimPiece(53, 7, false, true, false, true, true, true, false, true, true));
			//--------------------------------------------------------------------------------------
			/*		..X
					XXX
					.X.			*/
			Pieces.Add(new SimPiece(54, 7, false, true, false, true, true, true, false, false, true));
			//--------------------------------------------------------------------------------------
			/*		.XX
					.XX
					XX.			*/
			Pieces.Add(new SimPiece(55, 7, true, true, false, false, true, true, false, true, true));

			// ============ Level 8 ========
			//--------------------------------------------------------------------------------------
			/*		.XX
					XX.
					XX.			*/
			Pieces.Add(new SimPiece(56, 8, true, true, false, true, true, false, false, true, true));
			//--------------------------------------------------------------------------------------
			/*		..X
					XX.
					XX.			*/
			Pieces.Add(new SimPiece(57, 8, true, true, false, true, true, false, false, false, true));
			//--------------------------------------------------------------------------------------
			/*		XXX
					.XX
					.X.			*/
			Pieces.Add(new SimPiece(58, 8, false, true, false, false, true, true, true, true, true));
			//--------------------------------------------------------------------------------------
			/*		XXX
					XX.
					.X.			*/
			Pieces.Add(new SimPiece(59, 8, false, true, false, true, true, false, true, true, true));
			//--------------------------------------------------------------------------------------
			/*		...
					XXX
					.X.			*/
			Pieces.Add(new SimPiece(60, 8, false, true, false, true, true, true, false, false, false));
			//--------------------------------------------------------------------------------------
			/*		XXX
					.X.
					.X.			*/
			Pieces.Add(new SimPiece(61, 8, false, true, false, false, true, false, true, true, true));
			//--------------------------------------------------------------------------------------


		}
	}
}
