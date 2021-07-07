using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace
{
	public enum PieceState
	{
		InBin,
		DroppingFromBin,
		OnBinFloor,
		OnPlatform,
		SpringUp,
		SpringOver,
		SpringDown,
		FreeFall
	}
}
