using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Loggers.Interfaces
{
    public interface ILogManager
    {
        void Error(Exception ex, string message, string prefix = "");
        void Error(string message, string prefix = "");
        void Info(string message, string prefix = "");
        void Warn(string message, string prefix = "");
        void Debug(string message, string prefix = "");
    }
}
