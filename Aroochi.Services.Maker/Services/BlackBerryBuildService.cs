using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aroochi.Services.Maker;
using Aroochi.Services.Maker.Models;

namespace Aroochi.Services.Maker.Services
{
    public class BlackBerryBuildService : GlobalBuildService
    {
        public override MakeStatus Build(string outputPackageName)
        {
            Platform = Platforms.BlackBerry;
            return base.Build(outputPackageName);
        }
    }
}