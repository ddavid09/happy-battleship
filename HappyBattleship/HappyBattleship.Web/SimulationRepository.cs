using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HappyBattleship.Web
{
    public class SimulationRepository : ISimulationRepository
    {
        public List<HappyBattleshipWebSimulation> Simulations { get; set; } = new List<HappyBattleshipWebSimulation>();
    }
}
