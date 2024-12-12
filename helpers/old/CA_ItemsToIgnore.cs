using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Runtime.Remoting.Contexts;


namespace Bank_statement_to_excel.helpers
{
    internal class CA_ItemsToIgnore
    {
        public static string ReadFile(string filePath)
        {
           
            if (File.Exists(filePath))
            {
                string fileContents = File.ReadAllText(filePath);

                MessageBox.Show(fileContents);
                
                return File.ReadAllText(filePath);
            }
            else
            {
                // Create an empty text file
                File.Create(filePath).Close();

                MessageBox.Show("New file created");
                return "File created.";
            }
        }

        public static void WriteToFile(string filePath, string userInput)
        {
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine(userInput);
                MessageBox.Show(userInput + " added to ignored");
            }
        }


    }
}
