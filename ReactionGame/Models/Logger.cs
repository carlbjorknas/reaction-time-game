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
        public void Log(string message)
        {
            var logMessage = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "   " + message;
            lock (_lock)
            {                
                File.AppendAllLines(HttpContext.Current.Server.MapPath("~/log.txt"), new[] { logMessage });
            }
        }
    }
}