using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.SimulationLogic.Enums;
using Assets.Scripts.SimulationLogic.Models;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PieceTests
{
	[Test]
	public void Instantiate_DataProperlyPlaced()
	{
		SimShape p = new SimShape(123, 100, true, false, false, true, true, false, true, true, true);
		bool infoIsOk = (p.LevelFirstAppears == 100) && (p.Id == 123) && (p.Blocks[0, 0] = true) &&
			(p.Blocks[0, 0] = true) &&
			(p.Blocks[0, 1] = true) &&
			(p.Blocks[0, 2] = true) &&
			(p.Blocks[1, 0] = true) &&
			(p.Blocks[1, 1] = true) &&
			(p.Blocks[1, 2] = true) &&
			(p.Blocks[2, 0] = true) &&
			(p.Blocks[2, 1] = true) &&
			(p.Blocks[2, 2] = true);

		Assert.IsTrue(infoIsOk);
	}

	[Test]
	public void Width_Return1()
	{
		SimShape p = new SimShape(123, 100, true, false, false, true, false, false, true, false, false);
		Assert.AreEqual(1, p.GetSimWidth());
	}

	[Test]
	public void Width_Return2()
	{
		SimShape p = new SimShape(123, 100, true, true, false, true, false, false, true, false, false);
		Assert.AreEqual(2, p.GetSimWidth());
	}

	[Test]
	public void Width_Return3()
	{
		SimShape p = new SimShape(123, 100, true, false, false, true, false, false, true, true, true);
		Assert.AreEqual(3, p.GetSimWidth());
	}

	[Test]
	public void NewPiece_ExpectInactiveState()
	{
		SimShape p = new SimShape(123, 100, true, false, false, true, false, false, true, true, true);
		Assert.AreEqual(PieceState.NotActive, p.CurrentState);
	}

}
