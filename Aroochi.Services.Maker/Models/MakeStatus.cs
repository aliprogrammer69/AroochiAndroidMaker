using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aroochi.Services.Maker.Models
{
    public class MakeStatus
    {
        public MakeStatus(Platforms platform)
        {
            Platform = platform;
            Steps = new List<MakeStep>();

        }

        public bool? Status { get { return Steps.Count != 0 && !Steps.Any(s => s.Status == false); } }

        public Platforms Platform { get; private set; }
        public IList<MakeStep> Steps { get; }

        private TimeSpan _totalExecutionTime = TimeSpan.Zero;
        public TimeSpan TotalExecutionTime
        {
            get
            {
                if (_totalExecutionTime == TimeSpan.Zero)
                    _totalExecutionTime = Steps.Count != 0 ? new TimeSpan(Steps.Sum(s => s.ExecutionTime.Ticks)) : TimeSpan.Zero;
                return _totalExecutionTime;
            }
        }
    }
}