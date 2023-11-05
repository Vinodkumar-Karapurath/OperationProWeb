using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdminWebCore.Class
{
    public class LogMiddleware
    {
                
        public static void LogData(string _Filepath, string _Origin)
        {
            string message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
            message += Environment.NewLine;
            message += "-----------------------------------------------------------";
            message += Environment.NewLine;
            message += string.Format("Message: {0}", _Origin);
            message += Environment.NewLine;
            message += "-----------------------------------------------------------";
            message += Environment.NewLine;

           
            string filepath = _Filepath + DateTime.Today.ToString("dd-MM-yy") + ".txt";

            //if (_Filepath.Trim().Length != 0)
            //    using (StreamWriter writer = new StreamWriter(filepath, true))
            //    {
            //        writer.WriteLine(message);
            //        writer.Close();
            //    }
        }
    }
}
