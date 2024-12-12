using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Bank_statement_to_excel.helpers
{
    internal class MergeDataGrids
    {
        public static void Merge(DataGrid data1)
        {
            DataView dataView = (DataView)data1.ItemsSource;
            DataTable dataTable = dataView.ToTable();

            data1.Items.Refresh();
            // Rebind the DataTable to the DataGrid
            data1.ItemsSource = dataTable.DefaultView;

            foreach (DataRow row in dataTable.Rows)
            {
                Console.WriteLine(row);
            }      
        }
    }
}
