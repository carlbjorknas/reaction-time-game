using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ReactionGame.Models
{
    public class Logger
    {
        private object _lock = new object();
        private string _path;

        public Logger()
        {
            _path = HttpContext.Current.Server.MapPath("~/log.txt");
        }
        public void Log(string message)
        {
            var logMessage = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "   " + message;
            lock (_lock)
            {                
                File.AppendAllLines(_path, new[] { logMessage });
            }
        }
    }
}