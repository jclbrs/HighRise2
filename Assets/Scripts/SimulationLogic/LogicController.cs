using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.SimulationLogic;

namespace ScriptDefinitions.Assets.Scripts.SimulationLogic
{
    public class LogicController
    {
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

        /// <summary>
        /// Populates the bins for the current level, and clears the Springboard & landing zone
        /// </summary>
        /// <param name="level">Level is 1-based</param>
        public void SetupLevel(int level)
		{
            //_binsLogic.Initialize(level);
		}

        //public 
	}
}
