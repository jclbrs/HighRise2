using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Events;

namespace Assets.Scripts
{
	[Serializable]
	public class PlayerWantsBinPieceEvent : UnityEvent<int>
	{
	}

	[Serializable]
	public class PieceBinDropLandedEvent: UnityEvent<PieceManager>
	{
	}
}
