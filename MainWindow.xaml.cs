using System;
using System.Windows;
using Microsoft.Win32;
using System.IO;
using System.Collections.Generic;
using Bank_statement_to_excel.helpers;
using System.Data;
using CsvHelper;
using System.Globalization;
using System.Windows.Controls;

namespace Bank_statement_to_excel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {         
        public MainWindow()
        {
            InitializeComponent();          
            SetWindowSize();
          
        }


        private void SetWindowSize()
        {
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;

            double desiredWidth = screenWidth * 0.8;
            double desiredHeight = screenHeight * 0.8;

            Width = desiredWidth;
            Height = desiredHeight;
        }


        // load CSV file button clicked - any button, CA or CC
        private void BtnLoadCsvFile(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {   
                if (button.Name == "btnLoadCA")
                {
                    // create new object of LoadCsvFiles class and assign left column of the grid to it
                    LoadCsvFiles CA_loadCsvFiles = new LoadCsvFiles(dataGridView1, File1Name);
                    CA_loadCsvFiles.LoadCsvAndBindToGrid(button.Name);
                    if(dataGridView1.Items.Count > 0)
                    {
                        BtnDeleteFromCA.IsEnabled = true;
                    }
                }
                else if (button.Name == "btnLoadCC")
                {
                    LoadCsvFiles CC_loadCsvFiles = new LoadCsvFiles(dataGridView2, File2Name);
                    CC_loadCsvFiles.LoadCsvAndBindToGrid(button.Name);
                    if (dataGridView2.Items.Count > 0)
                    {
                        BtnDeleteFromCC.IsEnabled = true;
                    }
                }
                //LoadCsvAndBindToGrid(button.Name);
            }
        } 
     

        private void Btn_CA_Delete(object sender, RoutedEventArgs e)
        {
            CA_HandleBtnDelete.HandleDelete(dataGridView1);           
            BtnDeleteFromCA.IsEnabled = false; 
        }

        private void Btn_CC_Delete(object sender, RoutedEventArgs e)
        {
            CC_HandleBtnDelete.CC_btnClick(dataGridView2);           
            
            BtnDeleteFromCC.IsEnabled = false;
        }


        // Close the application
        private void ExitApp(object sender, RoutedEventArgs e)
            {
            Application.Current.Shutdown();
            }

        private void ShowIgnored(object sender, RoutedEventArgs e)
        {
            CA_ItemsToIgnore.ReadFile("CA_ignored.txt");
        }

        private void AddToIgnored(object sender, RoutedEventArgs e)
        {
            GetInputForIgnore getInput = new GetInputForIgnore();
            getInput.ShowDialog();

            // Retrieve user input from InputWindow
            string userInput = getInput.UserInput;
            if (!string.IsNullOrEmpty(userInput))
            {
                CA_ItemsToIgnore.WriteToFile("CA_ignored.txt", userInput);               
            }
            else
            {
                MessageBox.Show("No input provided.");
            }                        
        }

       
        public void BtnMergeDataGrids(object sender, RoutedEventArgs e)
        {
            if(CA_HandleBtnDelete.CA_Deleted == false || CC_HandleBtnDelete.CC_Deleted == false)
            {
                MessageBox.Show("Sort out both statements first!");
            }
            else
            {
                dataGridView2.Visibility = Visibility.Collapsed;
                Grid.SetColumnSpan(dataGridView1, 3);
                MergeFiles();
            }
            
        }
        
        private void MergeFiles()
        {
            // load both datatables saved to CSV files
            DataTable CA_dataTable = LoadCsvFiles.LoadCsvIntoDataTable("CA_dataTable.csv");
            DataTable CC_dataTable = LoadCsvFiles.LoadCsvIntoDataTable("CC_dataTable.csv");           

            foreach (DataRow row in CC_dataTable.Rows)
            {
                CA_dataTable.Rows.Add(row.ItemArray);
            }

            DataView dv = CA_dataTable.DefaultView;
            dv.Sort = "Date DESC";
            dv.Sort = "Date  ASC";
            CA_dataTable = dv.ToTable();
            dataGridView1.ItemsSource = CA_dataTable.DefaultView;



        }
        
    }
}
