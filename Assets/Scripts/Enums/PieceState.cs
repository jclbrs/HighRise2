using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Enums
{
	public enum PieceState
	{
		InBin,
		DroppingToNextBinCell,
		DroppingFromBinToPlayer,
		MovingToSpringboard,
		OnSpringboard,
		MovingUpFromSpringboard,
		MovingDownToLandingZone,
		LandedOnLandingZone,
		DroppingInLandingZone,
		IntoGarbage,
		FallingUnstable,
		DroppingFromPlayerToSpring,
		OnSpring
	}
}
