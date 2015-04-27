using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IcyMazeRunner.Klassen
{
    public class GameTime
    {
        //GameTime sorgt für stets gleiche geschwidnigkeit - prozessoruinabhängig
        public Stopwatch Watch;
        public TimeSpan TotalTime;
        public TimeSpan ElapsedTime;

        public GameTime()
        {
            Watch = new Stopwatch();
            TotalTime = TimeSpan.FromSeconds(0);
            ElapsedTime = TimeSpan.FromSeconds(0);
        }

        public void start()
        {
            Watch.Start();
        }

        public void stop()
        {
            Watch.Stop();
            TotalTime = TimeSpan.FromSeconds(0);
            ElapsedTime = TimeSpan.FromSeconds(0);

        }

        public void update()
        {
            ElapsedTime = Watch.Elapsed-TotalTime;
            TotalTime = Watch.Elapsed;
        }
    }
}
