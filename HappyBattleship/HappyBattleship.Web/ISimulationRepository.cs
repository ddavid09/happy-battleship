using HappyBattleship.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HappyBattleship.Web
{
    public interface ISimulationRepository
    {
        List<HappyBattleshipWebSimulation> Simulations { get; set; }
    }
}
