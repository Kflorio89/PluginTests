using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PluginTester
{
    public class CalculatorHostProvider
    {
        private static List<CalculatorHost> m_calculators;

        public static List<CalculatorHost> Calculators
        {
            get
            {
                if (null == m_calculators)
                    Reload();

                return m_calculators;
            }
        }

        public static void Reload()
        {
            if (null == m_calculators)
                m_calculators = new List<CalculatorHost>();
            else
                m_calculators.Clear();
            
            m_calculators.Add(new CalculatorHost()); // load the default
            List<Assembly> plugInAssemblies = LoadPlugInAssemblies();
            List<ICalculator> plugIns = GetPlugIns(plugInAssemblies);

            foreach (ICalculator calc in plugIns)
            {
                m_calculators.Add(new CalculatorHost(calc));
            }
        }
    }
}
