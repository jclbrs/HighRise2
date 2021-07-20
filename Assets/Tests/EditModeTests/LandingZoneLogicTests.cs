using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.SimulationLogic;
using Assets.Scripts.SimulationLogic.Enums;
using Assets.Scripts.SimulationLogic.Models;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class LandingZoneLogicTests
{
    [Test]
    public void Initialize_FirstRowZoneCellsShouldBeEmpty()
    {
        LandingZoneLogic zone = new LandingZoneLogic();
        bool isEmpty = true;
        for (int i = 0; i < zone.NumColsInLandingZone; i++)
		{
            if (zone.LandingZone[0, i].PieceId != int.MinValue)
                isEmpty = false;
		}
        Assert.IsTrue(isEmpty);
    }

	[Test]
	public void PlaceSimplePieceOn1stRow_ExpectProperPositioning()
	{
        LandingZoneLogic zone = new LandingZoneLogic();
        PlacePieceStatus status;
        zone.TryPlacePiece(1, 0, 0, out status);
        Assert.AreEqual(zone.LandingZone[0, 0].PieceId, 1);
	}

    [Test]
    public void PlaceComplexPieceOnR2C2_ExpectProperPositioning()
    {
        LandingZoneLogic zone = new LandingZoneLogic();
        PlacePieceStatus status;
        zone.TryPlacePiece(26, 2, 2, out status); // piece 26 is a tall U shape

        bool isValid = 
            (zone.LandingZone[2, 2].PieceId == 26) &&
            (zone.LandingZone[3, 2].PieceId == 26) &&
            (zone.LandingZone[4, 2].PieceId == 26) &&
            (zone.LandingZone[2, 3].PieceId == 26) &&
            (zone.LandingZone[3, 3].PieceId == int.MinValue) &&
            (zone.LandingZone[4, 3].PieceId == 26) &&
            (zone.LandingZone[2, 4].PieceId == 26) &&
            (zone.LandingZone[3, 4].PieceId == int.MinValue) &&
            (zone.LandingZone[4, 4].PieceId == 26);

        Assert.IsTrue(isValid);
    }

    [Test]
    public void TryPlacePieceTooFarToRight_ExpectErrorStatus()
    {
        LandingZoneLogic zone = new LandingZoneLogic();
        PlacePieceStatus status;
        bool result = zone.TryPlacePiece(26, 2, 3,out status); // piece 26 is a tall U shape, width of 3
        Assert.IsFalse(result);
        Assert.AreEqual(PlacePieceStatus.TooFarToTheRight, status);
    }

    [Test]
    public void TryPlacePieceBadCol_ExpectErrorStatus()
    {
        LandingZoneLogic zone = new LandingZoneLogic();
        PlacePieceStatus status;
        bool result = zone.TryPlacePiece(26, 2, 5, out status); // piece 26 is a tall U shape, width of 3
        Assert.IsFalse(result);
        Assert.AreEqual(PlacePieceStatus.BadColArg, status);
    }

    [Test]
    public void TryPlacePieceBadRow_ExpectErrorStatus()
    {
        LandingZoneLogic zone = new LandingZoneLogic();
        PlacePieceStatus status;
        bool result = zone.TryPlacePiece(26, 50, 2, out status); // piece 26 is a tall U shape, width of 3
        Assert.IsFalse(result);
        Assert.AreEqual(PlacePieceStatus.BadRowArg, status);
    }

    [Test]
    public void StartNewPiecesPositioning_1SmallPiece_ExpectSuccess()
	{
        SimPiece testPiece = PieceLibrary.Pieces[1];
        testPiece.SpringboardColumn = 0;
        List<SimPiece> pieces = new List<SimPiece>
        {
            testPiece
        };
        LandingZoneLogic zone = new LandingZoneLogic();
        zone.StartNewPiecesPositioning(pieces);
        Assert.AreEqual(zone.LandingZone[zone.NumRowsInLandingZone-1 - 3, 0].PieceId, 1);
    }
}
