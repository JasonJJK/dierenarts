using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;

namespace _15_1_FirmaBruinsma
{
    class Globaal
    {
        public static OleDbConnection Connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;
                    data source = ..\\..\\..\\Dierenartsonline.accdb ; Persist Security Info=False;");
    }
}
