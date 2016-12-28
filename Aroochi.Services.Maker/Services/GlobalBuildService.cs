using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aroochi.Services.Maker.Models;
using System.Diagnostics;
using Aroochi.Services.Maker.Helpers;

namespace Aroochi.Services.Maker.Services
{
    public abstract class GlobalBuildService : IBuildService
    {
        #region Properties
        protected Platforms Platform { get; set; }
        protected string OutputPackageName { get; private set; }
        #endregion

        public virtual MakeStatus Build(string outputPackageName)
        {
            OutputPackageName = outputPackageName;
            MakeStatus result = new MakeStatus(Platform);
            result.Steps.Add(Release());
            return result;
        }

        #region Private Methods
        private MakeStep Release()
        {
            Process process = ProcessHelper.Execute($"ionic build --release {Enum.GetName(typeof(Platforms), Platform)}");
            //Read Standard OutPut
            MakeStep stepResult = new MakeStep(Steps.Build);
            ProcessHelper.FillStepInfo(stepResult, process);//Fill Process Detials to Step
            return stepResult;
        }
        #endregion
    }
}