using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace CPopeWebsite
{
    public class AutoSave : IDisposable
    {
        public event Action autoSave;
        private Timer timer;

        public AutoSave(double interval = 5000)
        {
            timer = new Timer(interval);
            timer.Elapsed += TriggerEvent;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        private void TriggerEvent(object sender, ElapsedEventArgs e)
        {
            autoSave?.Invoke();
        }

        public void Dispose()
        {
            timer.Stop();
            timer.Dispose();
        }
    }
}
