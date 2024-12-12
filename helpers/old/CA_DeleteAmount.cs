using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Bank_statement_to_excel.helpers
{
    internal class CA_DeleteAmount
    {

        public static void DeleteAmounts(DataTable dataTable)
        {

           

            // Create a list to collect rows that need to be removed
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
                    //else if (doubleValue < 0)
                    //{
                    //    Console.WriteLine(doubleValue);
                    //    row["Amount"] = doubleValue * -1;

                    //}
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


            

        }

    }
}
