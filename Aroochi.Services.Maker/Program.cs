using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aroochi.Services.Maker.Models;
using Newtonsoft.Json;
using Aroochi.Services.Maker.Services;
using Aroochi.Services.Maker.Helpers;

namespace Aroochi.Services.Maker
{
    class Program
    {
        private static IBuildService _buildService = null;
        private static IList<string> platforms = new List<string>();
        static void Main(string[] args)
        {
            string packageName = string.Empty;
            var argsOptionSet = new OptionSet()
            {
                {"op|packagename=","Output Package Name",v=> packageName = v },
                {"p|platform=","Platform Name",v=> platforms.Add(v) }
            };

            argsOptionSet.Parse(args);

            //Change Current directory for Ionic Call
            Environment.CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            IList<MakeStatus> result = new List<MakeStatus>();
            for (int i = 0; i < platforms.Count; i++)
                result.Add(Make(platforms[i], packageName));
            //Return Result to caller
            Console.Out.Write(JsonConvert.SerializeObject(result));
        }

        #region Private Methods
        private static MakeStatus Make(string platformName, string outputPackageName)
        {
            //Get Platform enum
            Platforms platform = Platforms.Unknow;
            Enum.TryParse(platformName, true, out platform);

            switch (platform)
            {
                case Platforms.Android:
                    _buildService = new AndroidBuildService();
                    break;
                case Platforms.WindowsPhone:
                    _buildService = new WindowsPhoneBuildService();
                    break;
                case Platforms.BlackBerry:
                    _buildService = new BlackBerryBuildService();
                    break;
                case Platforms.Browser:
                    break;
                case Platforms.Windows:
                    break;
                case Platforms.Unknow:
                    return new MakeStatus(Platforms.Unknow);
            }

            return _buildService.Build(outputPackageName);
        }
        #endregion
    }
}