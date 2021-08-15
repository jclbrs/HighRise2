using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.SimulationLogic.Models
{
	public class StabilityResult
	{
		public bool IsStable { get; set; }
		public List<int> UnstablePieceIds { get; set; }

		public StabilityResult()
		{
			UnstablePieceIds = new List<int>();
		}
	}
}
