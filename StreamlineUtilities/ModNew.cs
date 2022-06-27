using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TDI.Streamline.Utilities.Networking.Mod;
using TDI.Streamline.Utilities.Publishing;

namespace StreamlinePlugin
{
    /// <summary>
    /// Example Plugin Mod
    /// </summary>
    public class ModNew : IMod, IScriptable
    {
        public GenericDictionary Vars => throw new NotImplementedException();
        GetVar _varsMethod;
        public object CurrentVars
        {
            get
            {
                return _currentVars;
            }
            
            set
            {
                _currentVars = value;
                _varsSet = true;
            }
        }
        private bool _varsSet = false;
        private object _currentVars;
        public string ModName => "NewModName";

        public ModNew()
        {
            
        }

        public void SetCallback(GetVar gv)
        {
            _varsMethod = gv;
            SetSomeVars();
        }

        public void SetSomeVars()
        {
            CurrentVars = _varsMethod();
        }
        
        public object CallMod(Msg msg)
        {
            throw new NotImplementedException();
        }

        public Errors Connect(params object[] args)
        {
            throw new NotImplementedException();
        }

        public Errors Disconnect(params object[] args)
        {
            throw new NotImplementedException();
        }

        public object ExecuteScriptCommand(params object[] args)
        {
            if (args.Length == 1)
            {
                if (args[0] is string str)
                {
                    return $"{str} Is a string";
                }
                else if (args[0] is int num)
                {
                    return $"{num} Is an int";
                }
                else if (args[0] is double dub)
                {
                    return $"{dub} Is a double";
                }
            }
            return $"Other length of params, length: {args.Length}";
        }

        public object GetCaps()
        {
            throw new NotImplementedException();
        }

        public object GetReport()
        {
            throw new NotImplementedException();
        }

        public GenericDictionary GetScriptCommandList()
        {
            throw new NotImplementedException();
        }

        public object GetVars()
        {
            return CurrentVars.ToString();
        }

        public void ProcMsg(Msg msg)
        {
            throw new NotImplementedException();
        }

        public void Release()
        {
            throw new NotImplementedException();
        }

        public Errors Reset(params object[] args)
        {
            throw new NotImplementedException();
        }
    }
}
