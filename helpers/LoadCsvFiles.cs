using CsvHelper;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Bank_statement_to_excel.helpers
{
    internal class LoadCsvFiles
    {

        private DataGrid _dataGrid;
        private TextBlock _textBlock;
        public LoadCsvFiles(DataGrid dataGrid, TextBlock textBlock)
            {
            _dataGrid = dataGrid;
            _textBlock = textBlock;
            }

        public void LoadCsvAndBindToGrid(string buttonName)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV Files (*.csv)|*.csv|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;

           

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                DataTable dataTable = LoadCsvIntoDataTable(filePath);

                // Sort the DataTable by the "Date" column using a custom comparer
                if (dataTable.Columns.Contains("Date"))
                {
                    DataView dv = dataTable.DefaultView;
                    dv.Sort = "Date ASC";
                    dataTable = dv.ToTable();
                }

                // Bind the loaded DataTable to the corresponding DataGrid
                if (buttonName == "btnLoadCA")
                {
                    _textBlock.Text = $"Loaded file: {Path.GetFileName(filePath)}";                   
                    _dataGrid.ItemsSource = dataTable.DefaultView;
                    //BtnDeleteFromCA.IsEnabled = true;
                }
                else if (buttonName == "btnLoadCC")
                {
                    _textBlock.Text = $"Loaded file: {Path.GetFileName(filePath)}";
                    _dataGrid.ItemsSource = dataTable.DefaultView;
                    //BtnDeleteFromCC.IsEnabled = true;
                }
            }
        }

        public static DataTable LoadCsvIntoDataTable(string filePath)
        {
            DataTable dataTable = new DataTable();

            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();
                foreach (string header in csv.HeaderRecord)
                {
                    dataTable.Columns.Add(header);
                }

                while (csv.Read())
                {
                    DataRow row = dataTable.NewRow();
                    try
                    {
                        foreach (DataColumn column in dataTable.Columns)
                        {
                            row[column.ColumnName] = csv.GetField(column.DataType, column.ColumnName);
                        }
                        dataTable.Rows.Add(row);
                    }
                    catch (CsvHelper.MissingFieldException)
                    {
                        MessageBox.Show("error loading data");
                    }
                    
                }
            }

            return dataTable;
        }

    }
}
