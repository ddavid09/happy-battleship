using System;
using System.Threading.Tasks;

namespace HappyBattleship.Library
{
    public interface ISimulation
    {
        void Init();
        void Start();
        void Stop();
        void Pause();
        bool IsRunning { get; }
        bool IsFinished { get; }
        int TurnsInterval { get; set; }

        event EventHandler<TurnEventArgs> AfterTurn;

        event EventHandler<SimInitialisedEventArgs> Initialised;

        event EventHandler Finished;

    }
}