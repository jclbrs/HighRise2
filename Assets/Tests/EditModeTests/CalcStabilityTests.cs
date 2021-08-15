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
        LandingZoneLogic zone = new LandingZoneLogic(18);
        PlacePieceStatus status;
        zone.TryPlacePiece(1,0, 0, 0, out status);

        bool isStable = zone.CalculateStability().IsStable;
        Assert.IsTrue(isStable);
    }

    [Test]
    public void Place2SamePiecesAdjacent_expectStable()
    {
        LandingZoneLogic zone = new LandingZoneLogic(18);
        PlacePieceStatus status;
        zone.TryPlacePiece(1,0, 0, 0, out status);
        zone.TryPlacePiece(2, 0, 0, 1, out status);

        bool isStable = zone.CalculateStability().IsStable;
        Assert.IsTrue(isStable);
    }

    [Test]
    public void Place2DifferentPiecesAdjacent_expectStable()
    {
        LandingZoneLogic zone = new LandingZoneLogic(18);
        PlacePieceStatus status;
        zone.TryPlacePiece(1, 0, 0, 0, out status);
        zone.TryPlacePiece(2, 3, 0, 1, out status);

        bool isStable = zone.CalculateStability().IsStable;
        Assert.IsTrue(isStable);
    }

    [Test]
    public void Place2SamePiecesSeparateButSameRow_expectStable()
    {
        LandingZoneLogic zone = new LandingZoneLogic(18);
        PlacePieceStatus status;
        zone.TryPlacePiece(1, 0, 0, 0, out status);
        zone.TryPlacePiece(2, 0, 0, 2, out status);

        bool isStable = zone.CalculateStability().IsStable;
        Assert.IsTrue(isStable);
    }

    [Test]
    public void Place2DifferentPiecesSeparateButSameRow_expectStable()
    {
        LandingZoneLogic zone = new LandingZoneLogic(18);
        PlacePieceStatus status;
        zone.TryPlacePiece(1, 0, 0, 0, out status);
        zone.TryPlacePiece(2, 3, 0, 2, out status);

        bool isStable = zone.CalculateStability().IsStable;
        Assert.IsTrue(isStable);
    }

    [Test]
    public void PlaceSimplePieceAboveAnother_expectStable()
    {
        LandingZoneLogic zone = new LandingZoneLogic(18);
        PlacePieceStatus status;
        zone.TryPlacePiece(1, 0, 0, 0, out status);
        zone.TryPlacePiece(2, 0, 1, 0, out status);

        bool isStable = zone.CalculateStability().IsStable;
        Assert.IsTrue(isStable);
    }

    [Test]
    public void Place2BlockPieceAbove1Block_expectStable() // This is a case where the center of gravity is right on the edge of the piece below it
    {
        LandingZoneLogic zone = new LandingZoneLogic(18);
        PlacePieceStatus status;
        zone.TryPlacePiece(1, 0, 0, 0, out status);
        zone.TryPlacePiece(2, 1, 1, 0, out status);

        bool isStable = zone.CalculateStability().IsStable;
        Assert.IsTrue(isStable);
    }

    [Test]
    public void Place2BlockPieceAbove1BlockInMiddleCol_expectStable() // This is a case where the center of gravity is right on the edge of the piece below it
    {
        LandingZoneLogic zone = new LandingZoneLogic(18);
        PlacePieceStatus status;
        zone.TryPlacePiece(1, 0, 0, 3, out status);
        zone.TryPlacePiece(2, 1, 1, 2, out status);

        bool isStable = zone.CalculateStability().IsStable;
        Assert.IsTrue(isStable);
    }

    [Test]
    public void Place2BlockPieceAbove1BlockOnFarRight_expectStable() // This is a case where the center of gravity is right on the edge of the piece below it
    {
        LandingZoneLogic zone = new LandingZoneLogic(18);
        PlacePieceStatus status;
        zone.TryPlacePiece(1, 0, 0, 5, out status);
        zone.TryPlacePiece(2, 1, 1, 4, out status);

        bool isStable = zone.CalculateStability().IsStable;
        Assert.IsTrue(isStable);
    }

    [Test]
    public void Place3BlockPieceAbove2Separated1Blocks_expectStable() // This is a case where the center of gravity is is above a gap below it, but the edges should support it
    {
        LandingZoneLogic zone = new LandingZoneLogic(18);
        PlacePieceStatus status;
        zone.TryPlacePiece(1, 0, 0, 0, out status);
        zone.TryPlacePiece(2,0, 0, 2, out status);
        zone.TryPlacePiece(3,2, 1, 0, out status);

        bool isStable = zone.CalculateStability().IsStable;
        Assert.IsTrue(isStable);
    }

    [Test]
    public void Stack1_2_3WideOffset_expectUnstable()
    {
        LandingZoneLogic zone = new LandingZoneLogic(18);
        PlacePieceStatus status;
        zone.TryPlacePiece(1,0, 0, 0, out status);
        zone.TryPlacePiece(2,1, 1, 1, out status);
        zone.TryPlacePiece(3,2, 2, 2, out status);

        bool isStable = zone.CalculateStability().IsStable;
        Assert.IsFalse(isStable);
    }

    [Test]
    public void Stack1_3_3TallOffset_expectUnstable()
    {
        LandingZoneLogic zone = new LandingZoneLogic(18);
        PlacePieceStatus status;
        zone.TryPlacePiece(1,0, 0, 0, out status);
        zone.TryPlacePiece(2,6, 1, 0, out status);
        zone.TryPlacePiece(3,6, 4, 1, out status);

        bool isStable = zone.CalculateStability().IsStable;
        Assert.IsFalse(isStable);
    }

    [Test]
    public void UShapeUnder7Shape_expectStable()
    {
        LandingZoneLogic zone = new LandingZoneLogic(18);
        PlacePieceStatus status;
        zone.TryPlacePiece(1,14, 0, 0, out status); // U-shape
        zone.TryPlacePiece(2,33, 1, 0, out status); // 7-shape

        bool isStable = zone.CalculateStability().IsStable;
        Assert.IsTrue(isStable);
    }
}
