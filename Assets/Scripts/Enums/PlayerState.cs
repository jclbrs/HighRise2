﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Enums
{
	public enum PlayerState
	{
		IdleUnderBin,
		MovingToBin,
		PushingPieceToSpringboard,
		IdleBySpringboard,
		Climbing,
		WaitingForBinPiece
	}
}
