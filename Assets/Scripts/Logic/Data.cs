using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Logic.Models;

namespace Assets.Scripts.Logic
{
	public static class Data
	{
		public static Dictionary<int, List<Piece>> Bins { get; set; }
		public static Piece[] Springboard;
		public static LandingZoneCell[,] LandingZone { get; set; }

	}
}
