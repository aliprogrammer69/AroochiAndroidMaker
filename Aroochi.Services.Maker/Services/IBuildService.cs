using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aroochi.Services.Maker.Models;

namespace Aroochi.Services.Maker.Services
{
    public interface IBuildService
    {
        MakeStatus Build(string outputPakcgeName);
    }
}
