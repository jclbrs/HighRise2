using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.SimulationLogic;
using Assets.Scripts.SimulationLogic.Models;

namespace ScriptDefinitions.Assets.Scripts.SimulationLogic
{
    public class LogicController
    {
        public Dictionary<int, List<Piece>> Bins
		{
            get { return _binsLogic.Bins; }
		}

        private BinsLogic _binsLogic;
        private SpringboardLogic _springboardLogic;
        private LandingZoneLogic _landingZoneLogic;

        public LogicController(int level, int numBins, int numCellsPerBin)
		{
            _binsLogic = new BinsLogic(level, numBins, numCellsPerBin);
            _binsLogic.PopulateAllBins();

            _springboardLogic = new SpringboardLogic();
            _landingZoneLogic = new LandingZoneLogic();
		}

        public Piece DropPieceFromBin(int pieceId)
		{
            return _binsLogic.DropPiece(pieceId);
		}

        /// <summary>
        /// Populates the bins for the current level, and clears the Springboard & landing zone
        /// </summary>
        /// <param name="level">Level is 1-based</param>
        public void SetupLevel(int level)
		{
            //_binsLogic.Initialize(level);
		}
	}
}
