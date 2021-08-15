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
		public Dictionary<int, List<SimShape>> SimBins { get; private set; }
		public List<SimShape> ApplicableSimShapes { get; private set; }
		public int NumBins { get; private set; }
		public int NumCellsPerBin { get; private set; }
		private int _level;
		private static Random _rnd;

		/// <summary>
		/// Constructor completely sets up and populates all bins for a level
		/// </summary>
		/// <param name="level">1-index</param>
		/// <param name="numBins"></param>
		/// <param name="numCellsPerBin"></param>
		public BinsLogic()
		{
			_rnd = new Random();
		}

		public void SetupLevel(int level, int numBins, int numCellsPerBin)
		{
			NumBins = numBins;
			NumCellsPerBin = numCellsPerBin;
			_level = level;
			ApplicableSimShapes = SimShapeLibrary.SimShapes.Where(x => x.LevelFirstAppears <= level).ToList();
			SimBins = new Dictionary<int, List<SimShape>>();

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
			if (SimBins.ContainsKey(binIdx))
				SimBins.Remove(binIdx); // clear the bin of any data.  Starting over with new data

			List<SimShape> binPieces = new List<SimShape>();

			 //bool joeTempPieceToggle = true;

			for (int i = 0; i < NumCellsPerBin; i++)
			{
				SimShape nextPiece = ApplicableSimShapes[_rnd.Next(ApplicableSimShapes.Count)];
				// start joe testing to just look at 2 specific pieces
				//int joeTestId = (joeTempPieceToggle ? 0 : 2);
				//joeTempPieceToggle = !joeTempPieceToggle;
				//SimShape nextPiece = SimShapeLibrary.SimShapes.Find(x => x.Id == joeTestId);
				// end Joe testing
				nextPiece.CurrentState = PieceState.InBin;
				binPieces.Add(nextPiece);
			}
			SimBins.Add(binIdx, binPieces);
		}

		/// <summary>
		/// Gets the bottom piece at designated bin, removes it there, and adds a new one at the top
		/// </summary>
		/// <param name="binIdx"></param>
		/// <returns></returns>
		public SimShape DropPieceFromBin(int binIdx)
		{
			SimShape droppedPiece = SimBins[binIdx][0];
			droppedPiece.CurrentState = PieceState.OnPlatform;
			SimBins[binIdx].RemoveAt(0);
			SimBins[binIdx].Add(ApplicableSimShapes[_rnd.Next(ApplicableSimShapes.Count)]);
			return droppedPiece;
		}

       
    }
}
