using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FBS.Domain.Repository
{
    public interface IHelper
    {
        void ExecCommand(string sql);
        void ExecScriptFile(string sqlScript);
    }
    
}
