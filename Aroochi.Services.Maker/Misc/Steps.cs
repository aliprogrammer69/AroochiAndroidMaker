using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aroochi.Services.Maker
{
    public enum Steps : byte
    {
        Build = 1,
        Sign = 2,
        ZipAlign = 3
    };
}