using CsvHelper.Configuration;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Bank_statement_to_excel.helpers
{
    internal class WriteCSVFile
    {
        public static void WriteDataTableToFile(DataTable dataTable, string filePath)
        {



            // Create a StringBuilder to construct the file content
            StreamWriter writer = new StreamWriter(filePath);
            using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)))

            {
                // Write CSV header
                foreach (DataColumn column in dataTable.Columns)
                {
                    csv.WriteField(column.ColumnName);
                }
                csv.NextRecord();

                // Write CSV records
                foreach (DataRow row in dataTable.Rows)
                {
                    for (int i = 0; i < dataTable.Columns.Count; i++)
                    {
                        csv.WriteField(row[i]);
                    }
                    csv.NextRecord();
                }
            }
            writer.Close();
            MessageBox.Show("file created");
        }
    }
}
