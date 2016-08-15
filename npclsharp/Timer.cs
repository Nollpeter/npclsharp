using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortableClassLibrary_NP
{
    public interface ITimer
    {
        void Stop();
        void Start();
        event EventHandler Tick;
        
    }
    public class Timer : ITimer
    {
        //private bool _continueTicking;
        private Double interval;
        private System.Threading.Timer timer;
        public Double Interval
        {
            get { return interval; }
            set { interval = value; if (timer != null) timer.Change(0, (Int32)Interval); }
        }
        public Timer()
        {
            interval = 10;
            
            
        }
        public event EventHandler Tick;
        public void Stop()
        {
           // throw new NotImplementedException();
            timer.Dispose();
        }

        public void Start()
        {
            timer = new System.Threading.Timer(internalTick, this,0,(Int32)Interval);
            //System.Threading.Tasks.Task.WaitAny(null, TimeSpan.FromMilliseconds(interval));
        }

        private void internalTick(Object state)
        {
            if (Tick != null)
                Tick(this, new EventArgs());
        }
        
    }
}
