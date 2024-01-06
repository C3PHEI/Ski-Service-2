using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ski_Service_2
{
    public static class AppConfig
    {
        public static string ConnectionString { get; } = @"Data Source=DESKTOP-2MEVUSI\SQLEXPRESS01;Initial Catalog=SkiService;Integrated Security=True";
    }
}
