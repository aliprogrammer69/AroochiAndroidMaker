using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aroochi.Services.Maker;
using Aroochi.Services.Maker.Models;
using Aroochi.Services.Maker.Helpers;
using System.Diagnostics;

namespace Aroochi.Services.Maker.Services
{
    public class AndroidBuildService : GlobalBuildService
    {
#if DEBUG
        private const string ApkAddress = @"D:\android-release-unsigned.apk";
        private const string KeyStoreAddress = @"D:\Developing\ASP.Net\Aroochi\KeyStore\aroochi-release-key.keystore";

#else
        private const string ApkAddress = @"platforms\android\build\outputs\apk\android-release-unsigned.apk";
        private const string KeyStoreAddress = "";
#endif
        private const string keyStorePass = "ali0920856861zahra_aroochi";
        public override MakeStatus Build(string outputPackageName)
        {
            Platform = Platforms.Android;
            MakeStatus result = base.Build(outputPackageName);
            if (result.Steps[0].Status)//If Build Successed then go for sign
            {
                MakeStep signStep = Sign();
                result.Steps.Add(signStep);
                if (signStep.Status)//if sign successed then go for zip align
                    result.Steps.Add(ZipAlign(outputPackageName));
            }
            return result;
        }

        #region Private Methods
        private MakeStep Sign()
        {
            MakeStep stepResult = new Models.MakeStep(Steps.Sign);
            string signCommand = $"jarsigner -verbose -sigalg SHA1withRSA -digestalg SHA1 -keystore {KeyStoreAddress} {ApkAddress} Aroochi";
            Process signProcess = ProcessHelper.Execute(signCommand);
            //send keystore pass to jarsigner process
            signProcess.StandardInput.WriteLine(keyStorePass);
            ProcessHelper.FillStepInfo(stepResult, signProcess);
            //Check for signing success message
            if (!stepResult.Message.Contains("jar signed."))
                stepResult.Status = false;
            return stepResult;
        }

        private MakeStep ZipAlign(string outputPackageName)
        {
            MakeStep zipAlignStep = new MakeStep(Steps.ZipAlign);
            string zipAlignCommand = $"zipalign -v 4 {ApkAddress} {outputPackageName}.apk";
            Process zipAlegnProcess = ProcessHelper.Execute(zipAlignCommand);
            ProcessHelper.FillStepInfo(zipAlignStep, zipAlegnProcess);
            if (!zipAlignStep.Message.Contains("Verification succesful"))
                zipAlignStep.Status = false;
            return zipAlignStep;
        }
        #endregion
    }
}