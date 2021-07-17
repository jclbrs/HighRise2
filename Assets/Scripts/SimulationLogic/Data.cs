using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.SimulationLogic.Models;

namespace Assets.Scripts.SimulationLogic
{
	public static class Data
	{
		public static Dictionary<int, List<Piece>> Bins { get; set; }
		public static Piece[] Pieces { get; set; }
		public static LandingZoneCell[,] LandingZone { get; set; }

	}
}
