using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Bank_statement_to_excel.helpers
{
    internal class CA_HandleBtnDelete
    {
        public static bool CA_Deleted = false;
        public static void HandleDelete(DataGrid dataGrid)
        {

            DataView dataView = (DataView)dataGrid.ItemsSource;
            DataTable dataTable = dataView.ToTable();
           
            // delete unwanted columns
            dataTable.Columns.Remove("Type");
            //if (dataTable.Columns.Contains("Balance")) // Check if column with name "1" exists
            //{
            //    // Remove the column
            //    dataTable.Columns.Remove("Balance");
            //}

            dataTable.Columns.Add("Company name", typeof(string)).SetOrdinal(1);

            dataTable.Columns.Add("Category", typeof(string)).SetOrdinal(3);


            dataTable.Columns.Add("Account name", typeof(string)).SetOrdinal(4);

            // Populate the "ca" column with "ca" values
            foreach (DataRow row in dataTable.Rows)
            {
                row["Account name"] = "ca";
            }

            // Determine the index of the last column
            int lastColumnIndex = dataTable.Columns.Count - 1;

            // Add the new "ca" column to the DataTable before the last column
            dataTable.Columns.Add("Grand", typeof(string)).SetOrdinal(5);

            dataTable.Columns.Add("CashNet", typeof(string)).SetOrdinal(6);
            dataTable.Columns.Add("CashVAT", typeof(string)).SetOrdinal(7);
            dataTable.Columns.Add("CCNet", typeof(string)).SetOrdinal(8);
            dataTable.Columns.Add("CCVAT", typeof(string)).SetOrdinal(9);
            dataTable.AcceptChanges();
            // Rebind the DataTable to the DataGrid
            dataGrid.ItemsSource = dataTable.DefaultView;
            dataGrid.Items.Refresh();

            RemoveAmounts(dataTable);

            // Rebind the DataTable to the DataGrid
            dataGrid.ItemsSource = dataTable.DefaultView;
            dataGrid.Items.Refresh();
        }

        private static void RemoveAmounts(DataTable dataTable)
        {
            // Iterate over the DataTable rows in reverse order to safely remove rows       

            int totalRows = 0;
            int positiveAmounts = 0;
            int negativeAmounts = 0;

            for (int i = dataTable.Rows.Count - 1; i >= 0; i--)
            {
                // Parse the string to a decimal number
                decimal amount;
                if (decimal.TryParse(dataTable.Rows[i]["Amount"].ToString(), out amount))
                {
                    if (amount > 0)
                    {
                        // Delete the row if amount > 0
                        dataTable.Rows[i].Delete();
                        positiveAmounts++;
                    }
                    else if (amount < 0)
                    {
                        negativeAmounts++;
                        dataTable.Rows[i]["Amount"] = amount * -1;
                    }
                }
                totalRows++;
            }

            // Accept changes to permanently delete rows
            dataTable.AcceptChanges();
            MessageBox.Show("Total rows: " + totalRows + "\nTotal positive: " + positiveAmounts + "\nTotal negative: " + negativeAmounts);

            DeleteIgnored(dataTable);

        }

        static List<string> ReadStringListFromFile(string filePath)
        {
            List<string> stringList = new List<string>();

            try
            {
                // Read all lines from the file
                string[] lines = File.ReadAllLines(filePath);

                // Add each line as a separate string to the list
                foreach (string line in lines)
                {
                    stringList.Add(line);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading file: {ex.Message}");
                // Handle the exception
            }

            return stringList;
        }
        private static void DeleteIgnored(DataTable dataTable)
        {
            int ignored = 0;
            List<string> listToIgnore = ReadStringListFromFile("CA_ignored.txt");

            // Iterate over the DataTable rows in reverse order to safely remove rows
            for (int i = dataTable.Rows.Count - 1; i >= 0; i--)
            {
                DataRow row = dataTable.Rows[i];
                string stringValue = row["Description"].ToString().ToLower();

                bool containsIgnore = false;
                foreach (string item in listToIgnore)
                {
                    if (stringValue.Contains(item.ToLower()))
                    {
                        containsIgnore = true;
                        ignored++;
                        break;
                    }
                }

                // If the row should be removed, remove it directly from the DataTable
                if (containsIgnore)
                {
                    dataTable.Rows.RemoveAt(i);
                }

            } MessageBox.Show("deleted ignored " + ignored);

            CA_Deleted = true;
            WriteCSVFile.WriteDataTableToFile(dataTable, "CA_dataTable.csv");
            //AddColumns(dataTable);
        }

        private static void WriteDataTableToFile(DataTable dataTable, string filePath)
        {
            // Create a StringBuilder to construct the file content
            using (var sw = new StreamWriter(filePath))
            {
                // Write header
                foreach (DataColumn column in dataTable.Columns)
                {
                    sw.Write(column.ColumnName);
                    sw.Write(",");
                }
                sw.WriteLine();

                // Write rows
                foreach (DataRow row in dataTable.Rows)
                {
                    foreach (var item in row.ItemArray)
                    {
                        sw.Write(item);
                        sw.Write(",");
                    }
                    sw.WriteLine();
                }
            }
            MessageBox.Show("file created");
        }

    }
}
