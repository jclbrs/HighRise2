using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.SimulationLogic;
using Assets.Scripts.SimulationLogic.Enums;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class CalcStabilityTests
{
    [Test]
    public void PlaceSimplestPiece_expectStable()
    {
        LandingZoneLogic zone = new LandingZoneLogic();
        PlacePieceStatus status;
        zone.TryPlacePiece(0, 0, 0, out status);

        bool isStable = zone.CalculateStability();
        Assert.IsTrue(isStable);
    }

    [Test]
    public void Place2PiecesAdjacent_expectStable()
    {
        LandingZoneLogic zone = new LandingZoneLogic();
        PlacePieceStatus status;
        zone.TryPlacePiece(0, 0, 0, out status);
        zone.TryPlacePiece(3, 0, 1, out status);

        bool isStable = zone.CalculateStability();
        Assert.IsTrue(isStable);
    }

    [Test]
    public void Place2PiecesSeparateButSameRow_expectStable()
    {
        LandingZoneLogic zone = new LandingZoneLogic();
        PlacePieceStatus status;
        zone.TryPlacePiece(0, 0, 0, out status);
        zone.TryPlacePiece(3, 0, 2, out status);

        bool isStable = zone.CalculateStability();
        Assert.IsTrue(isStable);
    }

    [Test]
    public void PlaceSimplePieceAboveAnother_expectStable()
    {
        LandingZoneLogic zone = new LandingZoneLogic();
        PlacePieceStatus status;
        zone.TryPlacePiece(0, 0, 0, out status);
        zone.TryPlacePiece(0, 1, 0, out status);

        bool isStable = zone.CalculateStability();
        Assert.IsTrue(isStable);
    }

    [Test]
    public void Place2BlockPieceAbove1Block_expectStable() // This is a case where the center of gravity is right on the edge of the piece below it
    {
        LandingZoneLogic zone = new LandingZoneLogic();
        PlacePieceStatus status;
        zone.TryPlacePiece(0, 0, 0, out status);
        zone.TryPlacePiece(1, 1, 0, out status);

        bool isStable = zone.CalculateStability();
        Assert.IsTrue(isStable);
    }

    [Test]
    public void Place3BlockPieceAbove2Separated1Blocks_expectStable() // This is a case where the center of gravity is is above a gap below it, but the edges should support it
    {
        LandingZoneLogic zone = new LandingZoneLogic();
        PlacePieceStatus status;
        zone.TryPlacePiece(0, 0, 0, out status);
        zone.TryPlacePiece(0, 0, 2, out status);
        zone.TryPlacePiece(2, 1, 0, out status);

        bool isStable = zone.CalculateStability();
        Assert.IsTrue(isStable);
    }

    [Test]
    public void JoeReminderToAddMoreComplexTests()
	{
        Assert.IsTrue(false);
	}
}
