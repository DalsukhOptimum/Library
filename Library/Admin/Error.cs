using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.Classes
{
    public class Error
    {
        //Error Function for all the other classes 
        public static void PrintingError(string ErrorMessage, string DetailMessage)
        {
            string DirectoryPath = AppDomain.CurrentDomain.BaseDirectory + "Error";
            Directory.CreateDirectory(DirectoryPath);

            string date = DateTime.Now.ToString("dd-MM-yyyy");
            using (StreamWriter sr = new StreamWriter(DirectoryPath + $"\\{date}.txt"))
            {
                sr.WriteLine("-------------------------------------");
                sr.WriteLine(DateTime.Now);
                sr.WriteLine($"{DetailMessage} and Error Is: {ErrorMessage}");
            }

        }
    }
}
