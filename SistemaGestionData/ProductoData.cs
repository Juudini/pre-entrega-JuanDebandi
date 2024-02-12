using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestion
{
    public static class ProductoData
    {
        public static string connectionString;

        static ProductoData()
        {
            ProductoData.connectionString = @"Server=.; Database=coderhouse; Trusted_Connection=True;";
        }
    }
}
