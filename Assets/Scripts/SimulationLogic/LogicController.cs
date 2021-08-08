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
        public BinsLogic BinsLogic { get; private set; }
        public SpringboardLogic SpringboardLogic { get; private set; }
        public LandingZoneLogic LandingZoneLogic;
        public int CurrentLevel { get; private set; }

        public LogicController()
		{
            BinsLogic = new BinsLogic();
        }

        /// <summary>
        /// Populates the bins for the current level, and clears the Springboard & landing zone
        /// </summary>
        /// <param name="level">Level is 1-based</param>
        public void SetupLevel(int level, int numBins, int numCellsPerBin, int landingSuccessRow)
		{
            CurrentLevel = level;
            BinsLogic.SetupLevel(level, numBins, numCellsPerBin);
            BinsLogic.PopulateAllBins();

            SpringboardLogic = new SpringboardLogic();
            LandingZoneLogic = new LandingZoneLogic(landingSuccessRow);
        }
	}
}
