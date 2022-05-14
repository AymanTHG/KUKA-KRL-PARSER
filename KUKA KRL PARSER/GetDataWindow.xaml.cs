using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace KUKA_KRL_PARSER
{
    /// <summary>
    /// Interaktionslogik für GetDataWindow.xaml
    /// </summary>
    public partial class GetDataWindow : Window
    {

        public string PathToEMI;
        
        public static GetDataWindow _getDataWindow;
        public GetDataWindow()
        {
            InitializeComponent();
            _getDataWindow = this;
        }


        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {                         
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".png";
            dlg.Filter = "TXT Files (*.txt)|*.txt";


            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();
            PathToEMI = dlg.FileName;
            txtPath.Text = PathToEMI;           
        }

        

        private void Submit(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();

            
            String input = File.ReadAllText(PathToEMI);
            string checkforRecords = "";
            input = Regex.Replace(input, " {2,}", " ");
            input = input.Replace('.', ',');

            while(!checkforRecords.Contains("[RECORDS]"))
            {
                checkforRecords += input.Substring(0,1);
                input = input.Remove(0, 1);
            
               
            }

            input = input.Remove(0, 1);
            input = input.Remove(input.Length - 7);
       
            int i = 0, j = 0;
           
           
            float[,] values = new float[File.ReadLines(PathToEMI).Count(), 9];
            foreach (var row in input.Split('\n'))
            {
                j = 0;
                foreach(var col in row.Trim().Split(' '))
                {
                    
                        values[i, j] = float.Parse(col.Trim());
                        j++;
                    
                    
           
                }

                    i++;
                
               
            }
            
            MainWindow._mainWindow.values = values;
            Close();
        }
    }
}
