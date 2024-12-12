using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Bank_statement_to_excel.helpers
{
    internal class CC_SortColumns
    {
        //public static void CC_btnClick(DataGrid dataGrid)
        //{
        //    //MessageBox.Show("button clicked");
        //    dataGrid.Items.Refresh();

        //    AddColumns(dataGrid);

        }

        //private static void AddColumns(DataGrid dataGrid)
        //{
        //    // Get the DataView from the DataGrid's ItemsSource
        //    DataView dataView = (DataView)dataGrid.ItemsSource;

        //    // Create a new DataTable and copy the schema and data from the DataView
        //    DataTable dataTable = dataView.ToTable();

        //    // Add empty columns to the new DataTable at the specified insertion index
        //    for (int i = 0; i < 5; i++)
        //    {
        //        DataColumn newColumn = dataTable.Columns.Add($"C{i + 1}", typeof(string));
        //        newColumn.SetOrdinal(2 + i);
        //    }

        //    foreach (DataRow row in dataTable.Rows)
        //    {
        //        string stringValue = row["Paid In"].ToString();
        //        double doubleValue;
        //        if (double.TryParse(stringValue, out doubleValue))
        //        {
        //            if (doubleValue > 0)
        //            {
        //                row["Paid Out"] = -1 * doubleValue;
        //            }
                 
        //        }
        //    }

        //    dataGrid.Items.Refresh();
        //    // Rebind the DataTable to the DataGrid
        //    dataGrid.ItemsSource = dataTable.DefaultView;

        //    foreach (var column in dataGrid.Columns.ToList())
        //    {
        //        if (column.Header.ToString() == "Paid In" )
        //        {
        //            dataGrid.Columns.Remove(column);
        //        }
        //    }

        //    dataGrid.Items.Refresh();

        //    dataGrid.ItemsSource = dataTable.DefaultView;



        //    MessageBox.Show("button clcked again");
           
        //}
  //  }
}