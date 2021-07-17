using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.SimulationLogic.Enums
{
	public enum PieceState 
	{
		NotActive,
		InBin,
		OnPlatform,
		OnSpringboard,
		InAir,
		LandingZoneDropping,
		LandingZoneLanded,
		UnstableFalling
	}
}
