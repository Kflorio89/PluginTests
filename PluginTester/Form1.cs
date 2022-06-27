using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using TDI.Streamline.Utilities.Networking.Mod;

namespace PluginTester
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private static List<Assembly> LoadPlugInAssemblies(string path)
        {
            //DirectoryInfo dInfo = new DirectoryInfo(Path.Combine(Environment.CurrentDirectory, "Plugins"));
            DirectoryInfo dInfo = new DirectoryInfo(path);
            FileInfo[] files = dInfo.GetFiles("*.dll");

            List<Assembly> plugInAssemblyList = new List<Assembly>();

            if (null != files)
            {
                foreach (FileInfo file in files)
                {
                    plugInAssemblyList.Add(Assembly.LoadFile(file.FullName));
                }
            }
            return plugInAssemblyList;
        }

        private static List<IScriptable> GetPlugIns(List<Assembly> assemblies)
        {
            List<Type> availableTypes = new List<Type>();

            foreach (Assembly currentAssembly in assemblies)
            {
                availableTypes.AddRange(currentAssembly.GetTypes());
            }

            // get a list of objects that implement the ICalculator interface AND 
            // have the CalculationPlugInAttribute
            List<Type> calculatorList = availableTypes.FindAll(delegate (Type t)
            {
                List<Type> interfaceTypes = new List<Type>(t.GetInterfaces());
                //object[] arr = t.GetCustomAttributes(typeof(CalculationPlugInAttribute), true);
                //return !(arr == null || arr.Length == 0) && interfaceTypes.Contains(typeof(IScriptable));
                return interfaceTypes.Contains(typeof(IScriptable));
            });

            // convert the list of Objects to an instantiated list of ICalculators
            return calculatorList.ConvertAll<IScriptable>(delegate (Type t)
            {
                return Activator.CreateInstance(t) as IScriptable;
            });
        }

        // Streamline instantiated mod
        public class Mod
        {
            private IScriptable _scriptMod;
            private IMod _modVersion;
            private object _vars = "ThisIsMyVars";
            public TDI.Streamline.Utilities.Networking.Mod.GetVar _varsMethod;
            public Mod(IScriptable scriptable)
            {
                _scriptMod = scriptable;
                _varsMethod = GetVars;
                scriptable.SetCallback(_varsMethod);
            }

            public string ExecuteCommand()
            {
                _modVersion = _scriptMod as IMod;
                string name = _modVersion.ModName;
                return _scriptMod.ExecuteScriptCommand(name) as string;
            }

            public object GetVars()
            {
                return _vars;
            }
        }

        private void BtnLoad_Click(object sender, EventArgs e)
        {
            string path = @"C:\Users\kflor\source\repos\PluginTester\StreamlineUtilities\bin\Debug";

            List<Assembly> assList = LoadPlugInAssemblies(path);
            List<IScriptable> scriptList = GetPlugIns(assList);
            List<Mod> modList = new List<Mod>();
            foreach (IScriptable scr in scriptList)
            {
                modList.Add(new Mod(scr));
            }

            try
            {
                Thread.Sleep(1000);
                string returnVal = modList[0].GetVars() as string;
                txtOutput.AppendText(returnVal + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
    }
}
