using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.SimulationLogic;
using Assets.Scripts.SimulationLogic.Enums;
using Assets.Scripts.SimulationLogic.Models;

namespace ScriptDefinitions.Assets.Scripts.SimulationLogic
{
    public class BinsLogic
    {
		public Dictionary<int, List<SimPiece>> Bins { get; private set; }
		public List<SimPiece> ApplicablePieces { get; private set; }
		public int NumBins { get; private set; }
		public int NumCellsPerBin { get; private set; }
		private readonly int _level;
		private Random _rnd;

		/// <summary>
		/// Constructor completely sets up and populates all bins for a level
		/// </summary>
		/// <param name="level">1-index</param>
		/// <param name="numBins"></param>
		/// <param name="numCellsPerBin"></param>
		public BinsLogic(int level, int numBins, int numCellsPerBin)
		{
			NumBins = numBins;
			NumCellsPerBin = numCellsPerBin;
			_level = level;
			_rnd = new Random();
			ApplicablePieces = PieceLibrary.Pieces.Where(x => x.LevelFirstAppears <= level).ToList();
			Bins = new Dictionary<int, List<SimPiece>>();
		}

		public void PopulateAllBins()
		{
			for (int i = 0; i < NumBins; i++)
			{
				PopulateBin(i);
			}
		}

		/// <summary>
		/// Randomly fills a designated bin with pieces from the Applicable Pieces list
		/// </summary>
		/// <param name="binIdx">bin index is 0-index</param>
		public void PopulateBin(int binIdx)
		{
			if (Bins.ContainsKey(binIdx))
				Bins.Remove(binIdx); // clear the bin of any data.  Starting over with new data

			List<SimPiece> binPieces = new List<SimPiece>();
			for(int i = 0; i < NumCellsPerBin; i++)
			{
				SimPiece nextPiece = ApplicablePieces[_rnd.Next(ApplicablePieces.Count)];
				nextPiece.CurrentState = PieceState.InBin;
				binPieces.Add(nextPiece);
			}
			Bins.Add(binIdx, binPieces);
		}

		/// <summary>
		/// Gets the bottom piece at designated bin, removes it there, and adds a new one at the top
		/// </summary>
		/// <param name="binIdx"></param>
		/// <returns></returns>
		public SimPiece DropPieceFromBin(int binIdx)
		{
			SimPiece droppedPiece = Bins[binIdx][0];
			droppedPiece.CurrentState = PieceState.OnPlatform;
			Bins[binIdx].RemoveAt(0);
			Bins[binIdx].Add(ApplicablePieces[_rnd.Next(ApplicablePieces.Count)]);
			return droppedPiece;
		}

       
    }
}
