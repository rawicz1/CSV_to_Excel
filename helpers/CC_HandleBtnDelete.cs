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
    internal class CC_HandleBtnDelete
    {
        public static bool CC_Deleted = false;
        public static void CC_btnClick(DataGrid dataGrid)
        {          
            dataGrid.Items.Refresh();

            AddColumns(dataGrid);
        }

        private static void AddColumns(DataGrid dataGrid)
        {
            // Get the DataView from the DataGrid's ItemsSource
            DataView dataView = (DataView)dataGrid.ItemsSource;

            // Create a new DataTable and copy the schema and data from the DataView
            DataTable dataTable = dataView.ToTable();


            dataTable.Columns.Add("Company name", typeof(string)).SetOrdinal(1);

            dataTable.Columns.Add("Category", typeof(string)).SetOrdinal(3);


            dataTable.Columns.Add("Account name", typeof(string)).SetOrdinal(4);


            // Populate the "cc" column with "cc" values
            foreach (DataRow row in dataTable.Rows)
            {
                row["Account name"] = "cc";
            }

            if (dataTable.Columns.Contains("Paid Out"))
            {
                dataTable.Columns["Paid Out"].ColumnName = "CCNet";
            }

            foreach (DataRow row in dataTable.Rows)
            {
                string stringValue = row["Paid In"].ToString();
                double doubleValue;
                if (double.TryParse(stringValue, out doubleValue))
                {
                    if (doubleValue > 0)
                    {
                        row["CCNet"] = -1 * doubleValue;
                    }
                }
            }

            dataTable.Columns.Add("Grand", typeof(string)).SetOrdinal(5);
            dataTable.Columns.Add("CashNet", typeof(string)).SetOrdinal(6);
            dataTable.Columns.Add("CashVAT", typeof(string)).SetOrdinal(7);

            if (dataTable.Columns.Contains("Paid In")) // Check if column with name "1" exists
            {
                // Remove the column
                dataTable.Columns.Remove("Paid In");
            }

            dataGrid.Items.Refresh();
            // Rebind the DataTable to the DataGrid
            dataGrid.ItemsSource = dataTable.DefaultView;

            foreach (var column in dataGrid.Columns.ToList())
            {
                if (column.Header.ToString() == "Paid In")
                {
                    dataGrid.Columns.Remove(column);
                }
            }

            dataTable.AcceptChanges();
            dataGrid.Items.Refresh();
            dataGrid.ItemsSource = dataTable.DefaultView;            
            CC_Deleted = true;
            WriteCSVFile.WriteDataTableToFile(dataTable, "CC_dataTable.csv");

        }
    }
}
