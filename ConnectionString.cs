using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ski_Service_2
{
    public static class AppConfig
    {
        public static string ConnectionString { get; } = @"Data Source=;Initial Catalog=SkiService;Integrated Security=True";
        //Hier Ihre ConnectionString ändern nach Data Source=
        //Das können Sie so lassen - ;Initial Catalog=SkiService;Integrated Security=True";
    }
}
