using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Bank_statement_to_excel.helpers
{
    internal class CA_RemoveListedEntries
    {

        //get items to ignore 

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

        List<string> listToIgnore = ReadStringListFromFile("CA_ignored.txt");
        public void RemoveEntries(DataGrid dataGrid)
        {
            DataView dataView = (DataView)dataGrid.ItemsSource;
            DataTable dataTable = dataView.ToTable();

            dataGrid.Items.Refresh();
            // Rebind the DataTable to the DataGrid
            dataGrid.ItemsSource = dataTable.DefaultView;

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
                        break;
                    }
                }

                // If the row should be removed, remove it directly from the DataTable
                if (containsIgnore)
                {
                    dataTable.Rows.RemoveAt(i);
                }
            }

            foreach (var column in dataGrid.Columns.ToList())
            {
                if (column.Header.ToString() == "Type" || column.Header.ToString() == "Balance")
                {
                    dataGrid.Columns.Remove(column);
                }
            }


            dataGrid.Items.Refresh();
            // Rebind the DataTable to the DataGrid
            dataGrid.ItemsSource = dataTable.DefaultView;

            WriteDataTableToFile(dataTable, "CA_dataTable");

        }


        //write datatable to file

        private void WriteDataTableToFile(DataTable dataTable, string filePath)
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
