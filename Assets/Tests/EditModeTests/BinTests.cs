using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.SimulationLogic;
using Assets.Scripts.SimulationLogic.Enums;
using Assets.Scripts.SimulationLogic.Models;
using NUnit.Framework;
using ScriptDefinitions.Assets.Scripts.SimulationLogic;
using UnityEngine;
using UnityEngine.TestTools;

public class BinTests
{
    [Test]
    public void InitializeLevel2_ExpectOnlyLevel1And2ApplicablePieces()
    {
        BinsLogic binsLogic = new BinsLogic(2,5, 4);
        Assert.IsFalse(binsLogic.ApplicableSimPieces.Exists(x => x.LevelFirstAppears > 2));
    }

    [Test]
    public void PopulateBin_ExpectCorrectNumPiecesInData()
    {
        BinsLogic binsLogic = new BinsLogic(2, 5, 4);
        binsLogic.PopulateBin(2);
        Assert.AreEqual(4, binsLogic.SimBins[2].Count);
    }

    [Test]
    public void PopulateBin_ExpectCorrectPieceState()
    {
        BinsLogic binsLogic = new BinsLogic(2, 5, 4);
        binsLogic.PopulateBin(2);
        Assert.AreEqual(binsLogic.SimBins[2][0].CurrentState, PieceState.InBin);
    }

    [Test]
    public void PopulateAllBins_Expect5BinsInData()
    {
        BinsLogic binsLogic = new BinsLogic(2, 5, 4);
        binsLogic.PopulateAllBins();
        Assert.AreEqual(5, binsLogic.SimBins.Count);
    }

    [Test]
    public void DropPiece_ExpectBottomPieceRetrieved()
    {
        BinsLogic binsLogic = new BinsLogic(2, 5, 4);
        binsLogic.PopulateBin(3);
        SimPiece bottomPiece = binsLogic.SimBins[3][0];

        SimPiece droppedPiece = binsLogic.DropPieceFromBin(3);
        Assert.AreEqual(bottomPiece.Id, droppedPiece.Id);
    }

    [Test]
    public void DropPiece_Expect2ndPieceAtBottom()
    {
        BinsLogic binsLogic = new BinsLogic(2, 5, 4);
        binsLogic.PopulateBin(3);
        SimPiece secondBottomPiece = binsLogic.SimBins[3][1];

        binsLogic.DropPieceFromBin(3);
        Assert.AreEqual(secondBottomPiece.Id, binsLogic.SimBins[3][0].Id);
    }

    [Test]
    public void DropPiece_ExpectTopPieceMovedDown()
    {
        BinsLogic binsLogic = new BinsLogic(2, 5, 4);
        binsLogic.PopulateBin(3);
        SimPiece topPiece = binsLogic.SimBins[3][3];

        binsLogic.DropPieceFromBin(3);
        Assert.AreEqual(topPiece.Id, binsLogic.SimBins[3][2].Id);
    }

    [Test]
    public void DropPiece_ExpectChangedPieceState()
    {
        BinsLogic binsLogic = new BinsLogic(2, 5, 4);
        binsLogic.PopulateBin(3);

        SimPiece droppedPiece = binsLogic.DropPieceFromBin(3);
        Assert.AreEqual(PieceState.OnPlatform, droppedPiece.CurrentState);
    }
}
