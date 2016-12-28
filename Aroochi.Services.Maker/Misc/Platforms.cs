using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aroochi.Services.Maker
{
    public enum Platforms : byte
    {
        Android = 1,
        Ios = 2,
        WindowsPhone = 3,
        BlackBerry = 4,
        Browser = 5,
        Windows = 6, // Desktop
        Unknow = 7
    }
}