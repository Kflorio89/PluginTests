﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginTester
{
    public interface ICalculator
    {
        int Calculate(int a, int b);
        char GetSymbol();
    }
}
