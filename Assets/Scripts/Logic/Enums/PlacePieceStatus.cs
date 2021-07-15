using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Logic.Enums
{
	public enum PlacePieceStatus
	{
		Ok,
		BadRowArg,
		BadColArg,
		InvalidPieceId,
		AnotherPiecePresent,
		TooFarToTheRight
	}
}
