using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using static System.Net.Mime.MediaTypeNames;

namespace Bank_statement_to_excel.helpers
{
    public class CA_DeleteColumns
    {
        public static void CA_OnClickDelete(DataGrid dataGrid)
        {       
            dataGrid.Items.Refresh();         

            changeValue(dataGrid);
        }

        private static void changeValue(DataGrid dataGrid)
        {

            // Get the DataView from the DataGrid's ItemsSource
            DataView dataView = (DataView)dataGrid.ItemsSource;

            // Create a new DataTable and copy the schema and data from the DataView
            DataTable dataTable = dataView.ToTable();


            // Determine the index of the last column
            int lastColumnIndex = dataTable.Columns.Count - 2;

            // Add the new "ca" column to the DataTable before the last column
            dataTable.Columns.Add("Account name", typeof(string)).SetOrdinal(lastColumnIndex);

            // Populate the "ca" column with "ca" values
            foreach (DataRow row in dataTable.Rows)
            {
                row["Account name"] = "ca";
            }

            dataTable.Columns.Add("Company name", typeof(string)).SetOrdinal(1);
            // Add empty columns to the new DataTable at the specified insertion index
            for (int i = 0; i < 4; i++)
            {
                DataColumn newColumn = dataTable.Columns.Add($"C{i + 1}", typeof(string));
                newColumn.SetOrdinal(3 + i);
            }       
           
            List<DataRow> rowsToRemove = new List<DataRow>();

            foreach (DataRow row in dataTable.Rows)
            {
                string stringValue = row["Amount"].ToString();
                double doubleValue;
                if (double.TryParse(stringValue, out doubleValue))
                {
                    if (doubleValue > 0)
                    {
                        rowsToRemove.Add(row);
                    }
                    else if (doubleValue < 0)
                    {
                        //Console.WriteLine(doubleValue);
                        row["Amount"] = doubleValue * -1;

                    }
                }

                else
                {
                    MessageBox.Show("Error while deleting amounts");
                }
            }

            // Remove the collected rows from the DataTable
            foreach (DataRow rowToRemove in rowsToRemove)
            {
                dataTable.Rows.Remove(rowToRemove);
            }
                  
            dataGrid.Items.Refresh();
            
            dataGrid.ItemsSource = dataTable.DefaultView;
           
            CA_RemoveListedEntries entries = new CA_RemoveListedEntries();
            
            entries.RemoveEntries(dataGrid);
        }
    }
}
