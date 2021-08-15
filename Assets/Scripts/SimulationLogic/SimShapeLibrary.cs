using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.SimulationLogic.Models;

namespace Assets.Scripts.SimulationLogic
{
	public static class SimShapeLibrary
	{
		private static List<SimShape> _pieces;
		public static List<SimShape> SimShapes
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
			_pieces = new List<SimShape>();

			// ============ Level 1 ========
			/*		...
					...
					X..			*/
			SimShapes.Add(new SimShape(0, 1, true, false, false, false, false, false, false, false, false));
			//--------------------------------------------------------------------------------------
			/*		...
					...
					XX.			*/
			SimShapes.Add(new SimShape(1, 1, true, true, false, false, false, false, false, false, false));
			//--------------------------------------------------------------------------------------
			/*		...
					...
					XXX			*/
			SimShapes.Add(new SimShape(2, 1, true, true, true, false, false, false, false, false, false));
			//--------------------------------------------------------------------------------------
			/*		...
					X..
					X..			*/
			SimShapes.Add(new SimShape(3,1,  true, false, false, true, false, false, false, false, false));
			//--------------------------------------------------------------------------------------
			/*		...
					XX.
					XX.			*/
			SimShapes.Add(new SimShape(4, 1, true, true, false, true, true, false, false, false, false));
			//--------------------------------------------------------------------------------------
			/*		X..
					X..
					X..			*/
			SimShapes.Add(new SimShape(5, 1, true, false, false, true, false, false, true, false, false));
			//--------------------------------------------------------------------------------------
			/*		XX.
					XX.
					XX.			*/
			SimShapes.Add(new SimShape(6, 1, true, true, false, true, true, false, true, true, false));
			//--------------------------------------------------------------------------------------
			/*		XXX
					XXX
					XXX			*/
			SimShapes.Add(new SimShape(7, 1, true, true, true, true, true, true, true, true, true));


			// ============ Level 2 ========
			/*		...
					X..
					XX.			*/
			SimShapes.Add(new SimShape(8, 2, true, true, false, true, false, false, false, false, false));
			//--------------------------------------------------------------------------------------
			/*		...
					.X.
					XX.			*/
			SimShapes.Add(new SimShape(9, 2, true, true, false, false, true, false, false, false, false));
			//--------------------------------------------------------------------------------------
			/*		...
					X..
					XXX			*/
			SimShapes.Add(new SimShape(10, 2,true, true, true, true, false, false, false, false, false));
			//--------------------------------------------------------------------------------------
			/*		...
					.X.
					XXX			*/
			SimShapes.Add(new SimShape(11, 2, true, true, true, false, true, false, false, false, false));
			//--------------------------------------------------------------------------------------
			/*		...
					..X
					XXX			*/
			SimShapes.Add(new SimShape(12, 2, true, true, true, false, false, true, false, false, false));
			//--------------------------------------------------------------------------------------
			/*		X..
					X.X
					XXX			*/
			SimShapes.Add(new SimShape(13, 2, true, true, true, true, false, true, true, false, false));
			//--------------------------------------------------------------------------------------
			/*		...
					X.X
					XXX			*/
			SimShapes.Add(new SimShape(14, 2, true, true, true, true, false, true, false, false, false));
			//--------------------------------------------------------------------------------------
			/*		...
					XXX
					X.X			*/
			SimShapes.Add(new SimShape(15, 2, true, false, true, true, true, true, false, false, false));


			// ============ Level 3 ========
			/*		X..
					X..
					XX.			*/
			SimShapes.Add(new SimShape(16, 3, true, true, false, true, false, false, true, false, false));
			//--------------------------------------------------------------------------------------
			/*		.X.
					.X.
					XX.			*/
			SimShapes.Add(new SimShape(17, 3,true, true, false, false, true, false, false, true, false));
			//--------------------------------------------------------------------------------------
			/*		X..
					XX.
					XX.			*/
			SimShapes.Add(new SimShape(18, 3,true, true, false, true, true, false, true, false, false));
			//--------------------------------------------------------------------------------------
			/*		.X.
					XX.
					XX.			*/
			SimShapes.Add(new SimShape(19, 3,true, true, false, true, true, false, false, true, false));
			//--------------------------------------------------------------------------------------
			/*		X..
					X..
					XXX			*/
			SimShapes.Add(new SimShape(20, 3,true, true, true, true, false, false, true, false, false));
			//--------------------------------------------------------------------------------------
			/*		..X
					..X
					XXX			*/
			SimShapes.Add(new SimShape(21, 3,true, true, true, false, false, true, false, false, true));
			//--------------------------------------------------------------------------------------
			/*		.X.
					.X.
					XXX			*/
			SimShapes.Add(new SimShape(22, 3,true, true, true, false, true, false, false, true, false));
			//--------------------------------------------------------------------------------------
			/*		XX.
					XX.
					XXX			*/
			SimShapes.Add(new SimShape(23, 3,true, true, true, true, true, false, true, true, false));

			// ============ Level 4 ========
			/*		.XX
					.XX
					XXX			*/
			SimShapes.Add(new SimShape(24, 4,true, true, true, false, true, true, false, true, true));
			//--------------------------------------------------------------------------------------
			/*		X.X
					XXX
					XXX			*/
			SimShapes.Add(new SimShape(25, 4,true, true, true, true, true, true, true, false, true));
			//--------------------------------------------------------------------------------------
			/*		X.X
					X.X
					XXX			*/
			SimShapes.Add(new SimShape(26, 4,true, true, true, true, false, true, true, false, true));
			//--------------------------------------------------------------------------------------
			/*		..X
					.XX
					XXX			*/
			SimShapes.Add(new SimShape(27, 4,true, true, true, false, true, true, false, false, true));
			//--------------------------------------------------------------------------------------
			/*		X..
					XX.
					XXX			*/
			SimShapes.Add(new SimShape(28, 4,true, true, true, true, true, false, true, false, false));
			//--------------------------------------------------------------------------------------
			/*		.X.
					.XX
					XXX			*/
			SimShapes.Add(new SimShape(29, 4,true, true, true, false, true, true, false, true, false));
			//--------------------------------------------------------------------------------------
			/*		.X.
					XX.
					XXX			*/
			SimShapes.Add(new SimShape(30, 4,true, true, true, true, true, false, false, true, false));
			//--------------------------------------------------------------------------------------
			/*		.X.
					XXX
					XXX			*/
			SimShapes.Add(new SimShape(31, 4,true, true, true, true, true, true, false, true, false));

			// ============ Level 5 ========
			//--------------------------------------------------------------------------------------
			/*		...
					XX.
					.XX			*/
			SimShapes.Add(new SimShape(32, 5,false, true, true, true, true, false, false, false, false));
			//--------------------------------------------------------------------------------------
			/*		...
					XX.
					.X.			*/
			SimShapes.Add(new SimShape(33, 5, false, true, false, true, true, false, false, false, false));
			//--------------------------------------------------------------------------------------
			/*		.X.
					XX.
					.X.			*/
			SimShapes.Add(new SimShape(34, 5, false, true, false, true, true, false, false, true, false));
			//--------------------------------------------------------------------------------------
			/*		XX.
					XX.
					.X.			*/
			SimShapes.Add(new SimShape(35, 5, false, true, false, true, true, false, true, true, false));
			//--------------------------------------------------------------------------------------
			/*		XX.
					.X.
					.X.			*/
			SimShapes.Add(new SimShape(36, 5, false, true, false, false, true, false, true, true, false));
			//--------------------------------------------------------------------------------------
			/*		XX.
					.X.
					.XX			*/
			SimShapes.Add(new SimShape(37, 5, false, true, true, false, true, false, true, true, false));
			//--------------------------------------------------------------------------------------
			/*		.X.
					XX.
					.XX			*/
			SimShapes.Add(new SimShape(38, 5, false, true, true, true, true, false, false, true, false));
			//--------------------------------------------------------------------------------------
			/*		XX.
					XX.
					.XX			*/
			SimShapes.Add(new SimShape(39, 5, false, true, true, true, true, false, true, true, false));

			// ============ Level 6 ========
			//--------------------------------------------------------------------------------------
			/*		XX.
					.XX
					.XX			*/
			SimShapes.Add(new SimShape(40, 6, false, true, true, false, true, true, true, true, false));
			//--------------------------------------------------------------------------------------
			/*		X..
					XXX
					.XX			*/
			SimShapes.Add(new SimShape(41, 6, false, true, true, true, true, true, true, false, false));
			//--------------------------------------------------------------------------------------
			/*		...
					.XX
					XX.			*/
			SimShapes.Add(new SimShape(42, 6, true, true, false, false, true, true, false, false, false));
			//--------------------------------------------------------------------------------------
			/*		...
					XX.
					X..			*/
			SimShapes.Add(new SimShape(43, 6, true, false, false, true, true, false, false, false, false));
			//--------------------------------------------------------------------------------------
			/*		X..
					XX.
					X..			*/
			SimShapes.Add(new SimShape(44, 6, true, false, false, true, true, false, true, false, false));
			//--------------------------------------------------------------------------------------
			/*		XX.
					XX.
					X..			*/
			SimShapes.Add(new SimShape(45, 6, true, false, false, true, true, false, true, true, false));
			//--------------------------------------------------------------------------------------
			/*		XX.
					X..
					X..			*/
			SimShapes.Add(new SimShape(46, 6, true, false, false, true, false, false, true, true, false));
			//--------------------------------------------------------------------------------------
			/*		.XX
					.X.
					XX.			*/
			SimShapes.Add(new SimShape(47, 6, true, true, false, false, true, false, false, true, true));

			// ============ Level 7 ========
			//--------------------------------------------------------------------------------------
			/*		.X.
					.XX
					XX.			*/
			SimShapes.Add(new SimShape(48, 7, true, true, false, false, true, true, false, true, false));
			//--------------------------------------------------------------------------------------
			/*		.XX
					XX.
					.X.			*/
			SimShapes.Add(new SimShape(49, 7, false, true, false, true, true, false, false, true, true));
			//--------------------------------------------------------------------------------------
			/*		XX.
					XXX
					.X.			*/
			SimShapes.Add(new SimShape(50, 7, false, true, false, true, true, true, true, true, false));
			//--------------------------------------------------------------------------------------
			/*		X..
					XXX
					.X.			*/
			SimShapes.Add(new SimShape(51, 7, false, true, false, true, true, true, true, false, false));
			//--------------------------------------------------------------------------------------
			/*		XX.
					.XX
					.X.			*/
			SimShapes.Add(new SimShape(52, 7, false, true, false, false, true, true, true, true, false));
			//--------------------------------------------------------------------------------------
			/*		.XX
					XXX
					.X.			*/
			SimShapes.Add(new SimShape(53, 7, false, true, false, true, true, true, false, true, true));
			//--------------------------------------------------------------------------------------
			/*		..X
					XXX
					.X.			*/
			SimShapes.Add(new SimShape(54, 7, false, true, false, true, true, true, false, false, true));
			//--------------------------------------------------------------------------------------
			/*		.XX
					.XX
					XX.			*/
			SimShapes.Add(new SimShape(55, 7, true, true, false, false, true, true, false, true, true));

			// ============ Level 8 ========
			//--------------------------------------------------------------------------------------
			/*		.XX
					XX.
					XX.			*/
			SimShapes.Add(new SimShape(56, 8, true, true, false, true, true, false, false, true, true));
			//--------------------------------------------------------------------------------------
			/*		..X
					XX.
					XX.			*/
			SimShapes.Add(new SimShape(57, 8, true, true, false, true, true, false, false, false, true));
			//--------------------------------------------------------------------------------------
			/*		XXX
					.XX
					.X.			*/
			SimShapes.Add(new SimShape(58, 8, false, true, false, false, true, true, true, true, true));
			//--------------------------------------------------------------------------------------
			/*		XXX
					XX.
					.X.			*/
			SimShapes.Add(new SimShape(59, 8, false, true, false, true, true, false, true, true, true));
			//--------------------------------------------------------------------------------------
			/*		...
					XXX
					.X.			*/
			SimShapes.Add(new SimShape(60, 8, false, true, false, true, true, true, false, false, false));
			//--------------------------------------------------------------------------------------
			/*		XXX
					.X.
					.X.			*/
			SimShapes.Add(new SimShape(61, 8, false, true, false, false, true, false, true, true, true));
			//--------------------------------------------------------------------------------------


		}
	}
}
