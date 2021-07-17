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
        LandingZoneLogic landingZoneLogic = new LandingZoneLogic();
        bool isEmpty = true;
        for (int i = 0; i < landingZoneLogic.NumColsInLandingZone; i++)
		{
            if (Data.LandingZone[0, i].PieceId != int.MinValue)
                isEmpty = false;
		}
        Assert.IsTrue(isEmpty);
    }

	[Test]
	public void PlaceSimplePieceOn1stRow_ExpectProperPositioning()
	{
        LandingZoneLogic landingZone = new LandingZoneLogic();
        PlacePieceStatus status;
        landingZone.TryPlacePiece(1, 0, 0, out status);
        Assert.AreEqual(Data.LandingZone[0, 0].PieceId, 1);
	}

    [Test]
    public void PlaceComplexPieceOnR2C2_ExpectProperPositioning()
    {
        LandingZoneLogic zone = new LandingZoneLogic();
        PlacePieceStatus status;
        zone.TryPlacePiece(26, 2, 2, out status); // piece 26 is a tall U shape

        bool isValid = 
            (Data.LandingZone[2, 2].PieceId == 26) &&
            (Data.LandingZone[2, 3].PieceId == 26) &&
            (Data.LandingZone[2, 4].PieceId == 26) &&
            (Data.LandingZone[3, 2].PieceId == 26) &&
            (Data.LandingZone[3, 3].PieceId == int.MinValue) &&
            (Data.LandingZone[3, 4].PieceId == 26) &&
            (Data.LandingZone[4, 2].PieceId == 26) &&
            (Data.LandingZone[4, 3].PieceId == int.MinValue) &&
            (Data.LandingZone[4, 4].PieceId == 26);

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
        Piece testPiece = PieceLibrary.Pieces[1];
        testPiece.SpringboardColumn = 0;
        List<Piece> pieces = new List<Piece>
        {
            testPiece
        };
        LandingZoneLogic zone = new LandingZoneLogic();
        zone.StartNewPiecesPositioning(pieces);
        Assert.AreEqual(Data.LandingZone[zone.NumRowsInLandingZone-1 - 3, 0].PieceId, 1);
    }
}
