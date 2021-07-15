using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Logic;
using Assets.Scripts.Logic.Enums;
using Assets.Scripts.Logic.Models;

namespace ScriptDefinitions.Assets.Scripts.Logic
{
    public class BinsLogic
    {
		public List<Piece> ApplicablePieces { get; private set; }
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
			Data.Bins = new Dictionary<int, List<Piece>>();
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
			if (Data.Bins.ContainsKey(binIdx))
				Data.Bins.Remove(binIdx); // clear the bin of any data.  Starting over with new data

			List<Piece> binPieces = new List<Piece>();
			for(int i = 0; i < NumCellsPerBin; i++)
			{
				Piece nextPiece = ApplicablePieces[_rnd.Next(ApplicablePieces.Count)];
				nextPiece.CurrentState = PieceState.InBin;
				binPieces.Add(nextPiece);
			}
			Data.Bins.Add(binIdx, binPieces);
		}

		/// <summary>
		/// Gets the bottom piece at designated bin, removes it there, and adds a new one at the top
		/// </summary>
		/// <param name="binIdx"></param>
		/// <returns></returns>
		public Piece DropPiece(int binIdx)
		{
			Piece droppedPiece = Data.Bins[binIdx][0];
			droppedPiece.CurrentState = PieceState.OnPlatform;
			Data.Bins[binIdx].RemoveAt(0);
			Data.Bins[binIdx].Add(ApplicablePieces[_rnd.Next(ApplicablePieces.Count)]);
			return droppedPiece;
		}

       
    }
}
