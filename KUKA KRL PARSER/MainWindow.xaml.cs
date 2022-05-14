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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace KUKA_KRL_PARSER
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow _mainWindow;
        public float[,] values;
        public int amountOfCommands = 1;
        string pathToEMI;
       
       public string srcStartText = "&ACCESS RVP" + "\n" +
                            "&REL 97" + "\n" +
                            @"&PARAM TEMPLATE = C:\KRC\Roboter\Template\vorgabe" + "\n" +
                            "&PARAM EDITMASK = *" + "\n" +
                            "DEF emil_dbf_1( )" + "\n" +
                            ";FOLD INI" + "\n" +
                            ";FOLD BASISTECH INI" + "\n" +
                            "GLOBAL INTERRUPT DECL 3 WHEN $STOPMESS==TRUE DO IR_STOPM ( )" + "\n" +
                            "INTERRUPT ON 3 " + "\n" +
                            "BAS (#INITMOV,0 )" + "\n" +
                            ";ENDFOLD (BASISTECH INI)" + "\n" +
                            ";FOLD SPOTTECH INI" + "\n" +
                            "USERSPOT(#INIT)" + "\n" +
                            ";ENDFOLD (SPOTTECH INI)" + "\n" +
                            ";FOLD GRIPPERTECH INI" + "\n" +
                            "USER_GRP(0,DUMMY,DUMMY,GDEFAULT)" + "\n" +
                            ";ENDFOLD (GRIPPERTECH INI)" +  "\n" +
                            ";FOLD USER INI" + "\n" +
                            " ;ENDFOLD (USER INI)" + "\n" +
                            ";ENDFOLD (INI)" + "\n" +
                            ";FOLD PTP HOME  Vel= 100 % DEFAULT;%{PE}%MKUKATPBASIS,%CMOVE,%VPTP,%P 1:PTP, 2:HOME, 3:, 5:100, 7:DEFAULT" + "\n" +
                            "$BWDSTART = FALSE" + "\n" +
                            "PDAT_ACT=PDEFAULT" + "\n" +
                            "FDAT_ACT=FHOME" + "\n" +
                            "BAS (#PTP_PARAMS,100 )" + "\n" +
                            "$H_POS=XHOME" + "\n" +
                            "PTP  XHOME" + "\n" +
                            ";ENDFOLD" + "\n";

        public string datStartText = 
                              "&ACCESS RVP" + "\n" +
                              "&REL 97" + "\n" +
                              @"&PARAM TEMPLATE = C:\KRC\Roboter\Template\vorgabe" + "\n" +
                              "&PARAM EDITMASK = *" + "\n" +
                              "DEFDAT  EMIL_DBF_1" + "\n" +
                              ";FOLD EXTERNAL DECLARATIONS;%{PE}%MKUKATPBASIS,%CEXT,%VCOMMON,%P" + "\n" +
                              ";FOLD GRIPPERTECH EXT" + "\n" +
                              "EXT  H50 (INT  :IN,INT  :IN,INT  :IN,GRP_TYP  :IN )" + "\n" +
                              ";ENDFOLD (GRIPPERTECH EXT)" + "\n" +
                              ";FOLD SPOTTECH EXT" + "\n" +
                              "EXT  USERSPOT (S_COMMAND  :IN,SPOT_TYPE  :IN )" + "\n" +
                              ";ENDFOLD (SPOTTECH EXT)" + "\n" +
                              ";FOLD BASISTECH EXT;%{PE}%MKUKATPBASIS,%CEXT,%VEXT,%P" + "\n" +
                              "EXT  BAS (BAS_COMMAND  :IN,REAL  :IN )" + "\n" +
                              "DECL INT SUCCESS" + "\n" +
                              ";ENDFOLD (BASISTECH EXT)" + "\n" +
                              ";FOLD USER EXT;%{E}%MKUKATPUSER,%CEXT,%VEXT,%P" + "\n" +
                              ";ENDFOLD (USER EXT)" + "\n" +
                              ";ENDFOLD (EXTERNAL DECLARATIONS)" + "\n" +
                              "DECL BASIS_SUGG_T LAST_BASIS={POINT1[] \"P12\",POINT2[] \"P12\", CP_PARAMS[] \"CPDAT6\", PTP_PARAMS[] \"PDAT6\", CONT[] \"C_PTP\", CP_VEL[] \"2\", PTP_VEL[] \"100\", SPL_NAME[] \"S0\"}" + "\n";

        string saveStartSRC;
        string saveStartDAT;
        bool isSRC = true;
        public MainWindow()
        {
            InitializeComponent();
            Console.WriteLine(srcStartText);
            pathToEMI = GetDataWindow._getDataWindow.PathToEMI;
            Console.WriteLine(pathToEMI);
            _mainWindow = this;
            saveStartSRC = srcStartText;
            saveStartDAT = datStartText;
            KRL_CODE.Text = srcStartText;
            
        }

        private void Add_Command(object sender, RoutedEventArgs e)
        {
            AddCommandWindow addWindow = new AddCommandWindow();
            addWindow.Show();
        }

        private void Generate_Command(object sender, RoutedEventArgs e)
        {
            string path = "";

            System.Windows.Forms.FolderBrowserDialog openFileDlg = new System.Windows.Forms.FolderBrowserDialog();
            var result = openFileDlg.ShowDialog();
            if (result.ToString() != string.Empty)
            {
                path = openFileDlg.SelectedPath;

                
            }

            if(path != string.Empty)
            {
                string pathSRC = path + "/src.txt";
                string pathDAT = path + "/dat.txt";


                if(isSRC)
                {
                    srcStartText = KRL_CODE.Text;
                }
                else
                {
                    datStartText = KRL_CODE.Text;
                }

                File.WriteAllText(pathSRC, srcStartText);
                File.WriteAllText(pathDAT, datStartText);
            }
            



        }

       

        private void Redo_Command(object sender, RoutedEventArgs e)
        {
            srcStartText = saveStartSRC;
            datStartText = saveStartDAT;
            KRL_CODE.Text = saveStartSRC;
            amountOfCommands = 1;
        }

        private void showSRC(object sender, RoutedEventArgs e)
        {
            datStartText = KRL_CODE.Text;
            KRL_CODE.Text = srcStartText;
           isSRC = true;
           
        }

        private void showDAT(object sender, RoutedEventArgs e)
        {
            srcStartText = KRL_CODE.Text;
            KRL_CODE.Text = datStartText;

            isSRC = false;
         
        }

        /*private void KRL_CODE_TextChanged(object sender, TextChangedEventArgs e)
        {          
                if (isSRC)
                {
                    srcStartText = KRL_CODE.Text;
                    Console.WriteLine("hey1");
                }
                else
                {
                    datStartText = KRL_CODE.Text;
                    Console.WriteLine("hey2");
                }
        
        }*/
    }
}
