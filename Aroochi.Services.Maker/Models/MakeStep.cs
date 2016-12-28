using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aroochi.Services.Maker.Models
{
    public class MakeStep
    {
        public MakeStep(Steps name)
        {
            Name = name;
            Status = true;
        }

        public Steps Name { get; private set; }
        public bool Status { get; set; }
        public string Message { get; set; }
        public TimeSpan ExecutionTime { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
