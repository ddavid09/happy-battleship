using System;
using System.Threading.Tasks;

namespace HappyBattleship.web
{
    public interface ISimulation
    {
        void Start();
        void Stop();
        void Pause();
        bool IsRunning { get; }
        bool IsFinished { get; }
        int TurnsInterval { get; set; }

        event EventHandler<TurnEventArgs> AfterTurn;

        event EventHandler<SimInitialisedEventArgs> Initialised;

    }
}