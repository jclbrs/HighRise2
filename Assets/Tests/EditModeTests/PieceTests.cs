using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Logic.Enums;
using Assets.Scripts.Logic.Models;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PieceTests
{
	[Test]
	public void Instantiate_DataProperlyPlaced()
	{
		Piece p = new Piece(123, 100, true, false, false, true, true, false, true, true, true);
		bool infoIsOk = (p.LevelFirstAppears == 100) && (p.Id == 123) && (p.Shape[0, 0] = true) &&
			(p.Shape[0, 0] = true) &&
			(p.Shape[0, 1] = true) &&
			(p.Shape[0, 2] = true) &&
			(p.Shape[1, 0] = true) &&
			(p.Shape[1, 1] = true) &&
			(p.Shape[1, 2] = true) &&
			(p.Shape[2, 0] = true) &&
			(p.Shape[2, 1] = true) &&
			(p.Shape[2, 2] = true);

		Assert.IsTrue(infoIsOk);
	}

	[Test]
	public void Width_Return1()
	{
		Piece p = new Piece(123, 100, true, false, false, true, false, false, true, false, false);
		Assert.AreEqual(1, p.GetWidth());
	}

	[Test]
	public void Width_Return2()
	{
		Piece p = new Piece(123, 100, true, true, false, true, false, false, true, false, false);
		Assert.AreEqual(2, p.GetWidth());
	}

	[Test]
	public void Width_Return3()
	{
		Piece p = new Piece(123, 100, true, false, false, true, false, false, true, true, true);
		Assert.AreEqual(3, p.GetWidth());
	}

	[Test]
	public void NewPiece_ExpectInactiveState()
	{
		Piece p = new Piece(123, 100, true, false, false, true, false, false, true, true, true);
		Assert.AreEqual(PieceState.NotActive, p.CurrentState);
	}

}
