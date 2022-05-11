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


namespace KUKA_KRL_PARSER
{
    /// <summary>
    /// Interaktionslogik für AddCommandWindow.xaml
    /// </summary>
    public partial class AddCommandWindow : Window
    {
       
        

        public AddCommandWindow()
        {
            InitializeComponent();
        }

        private void Submit(object sender, RoutedEventArgs e)
        {
           
            switch(CommandBox.Text)
            {
                case "Lin":
                    string cont = "";
                    if(ContBox.Text == "Yes")
                    {
                        cont = "CONT";
                    }
                    
                   
                    for (int i = 0; i < Int32.Parse(AmountBox.Text); i++)
                    {


                        string Lin = ";FOLD LIN P"+ MainWindow._mainWindow.amountOfCommands.ToString() + " " + cont +" Vel=" + VelocityBox.Text + "m/s" + " CPDAT"+ MainWindow._mainWindow.amountOfCommands.ToString() + " Tool[8]:Kelle Base[0];%{PE}%R" + "\n" +
                                     "5.5.28,%MKUKATPBASIS,%CMOVE,%VLIN,%P 1:LIN, 2:P " + MainWindow._mainWindow.amountOfCommands.ToString() + ", 3:, 5:" + VelocityBox.Text + ", 7:CPDAT" + MainWindow._mainWindow.amountOfCommands.ToString() + "\n" +
                                     "$BWDSTART=FALSE" + "\n" +
                                     "LDAT_ACT=LCPDAT" + MainWindow._mainWindow.amountOfCommands.ToString() + "\n" +
                                     "FDAT_ACT=FP" + MainWindow._mainWindow.amountOfCommands.ToString() + "\n" +
                                     "BAS(#CP_PARAMS," + VelocityBox.Text + ")" + "\n" +
                                     "LIN XP" + MainWindow._mainWindow.amountOfCommands.ToString() + "\n" +
                                     ";ENDFOLD" + "\n";
                        
                        MainWindow._mainWindow.srcStartText += Lin;
                        MainWindow._mainWindow.KRL_CODE.Text = MainWindow._mainWindow.srcStartText;

                        string datLin = "DECL E6POS XP" + MainWindow._mainWindow.amountOfCommands.ToString()
                            + "={X "+ MainWindow._mainWindow.values[MainWindow._mainWindow.amountOfCommands,1].ToString().Replace(',','.') 
                            + ",Y " + MainWindow._mainWindow.values[MainWindow._mainWindow.amountOfCommands, 2].ToString().Replace(',', '.')
                            + ",Z "+ MainWindow._mainWindow.values[MainWindow._mainWindow.amountOfCommands, 3].ToString().Replace(',', '.')
                            + ",A "+ MainWindow._mainWindow.values[MainWindow._mainWindow.amountOfCommands, 4].ToString().Replace(',', '.')
                            + ",B " + MainWindow._mainWindow.values[MainWindow._mainWindow.amountOfCommands, 5].ToString().Replace(',', '.')
                            + ",C "+ MainWindow._mainWindow.values[MainWindow._mainWindow.amountOfCommands, 6].ToString().Replace(',', '.')
                            + ",S "+ MainWindow._mainWindow.values[MainWindow._mainWindow.amountOfCommands, 7].ToString().Replace(',', '.')
                            + ",T "+ MainWindow._mainWindow.values[MainWindow._mainWindow.amountOfCommands, 8].ToString().Replace(',', '.') + "\n"
                            + ",E1 0.0,E2 0.0,E3 0.0,E4 0.0,E5 0.0,E6 0.0}" + "\n" +
                            "DECL FDAT FP" + MainWindow._mainWindow.amountOfCommands.ToString() + "={TOOL_NO 8,BASE_NO 0,IPO_FRAME #BASE,POINT2[] \" \",TQ_STATE FALSE}" + "\n" +
                            "DECL LDAT LCPDAT" + MainWindow._mainWindow.amountOfCommands.ToString() + "={VEL " + VelocityDat.Text + ",ACC " + BeschleunigungDat.Text + ",APO_DIST " + APODISTDat.Text+",APO_FAC 50.0,ORI_TYP #VAR,CIRC_TYP #BASE,JERK_FAC 50.0}" + "\n";
                        MainWindow._mainWindow.datStartText += datLin;
                        MainWindow._mainWindow.amountOfCommands++;

                        
                      
                    }
                    break;

                case "PTP":

                    string cont2 = "";
                   
                    if (ContBox.Text == "Yes")
                    {
                        cont2 = "CONT";
                    }
                   

                        for (int i = 0; i < Int32.Parse(AmountBox.Text); i++)
                        {
                            Console.WriteLine(VelocityBox.Text);   
                            int velocity = (int)(float.Parse(VelocityBox.Text)/1000);

                            Console.WriteLine(velocity);

                            string PTP = ";FOLD PTP P" + MainWindow._mainWindow.amountOfCommands.ToString() + " " + cont2 + " Vel=" + velocity.ToString() + "% "  + "PDAT" + MainWindow._mainWindow.amountOfCommands.ToString() + " Tool[8]:Kelle Base[0];%{PE}%R" + "\n" +
                                         "5.5.28,%MKUKATPBASIS,%CMOVE,%VPTP,%P 1:PTP, 2:P " + MainWindow._mainWindow.amountOfCommands.ToString() + ", 3:, 5:" + velocity.ToString() + ", 7:PDAT" + MainWindow._mainWindow.amountOfCommands.ToString() + "\n" +
                                         "$BWDSTART=FALSE" + "\n" +
                                         "PDAT_ACT=PPDAT" + MainWindow._mainWindow.amountOfCommands.ToString() + "\n" +
                                         "FDAT_ACT=FP" + MainWindow._mainWindow.amountOfCommands.ToString() + "\n" +
                                         "BAS(#CP_PARAMS," + velocity.ToString() + ")" + "\n" +
                                         "PTP XP" + MainWindow._mainWindow.amountOfCommands.ToString() + "\n" +
                                         ";ENDFOLD" + "\n";

                            MainWindow._mainWindow.srcStartText += PTP;
                            MainWindow._mainWindow.KRL_CODE.Text = MainWindow._mainWindow.srcStartText;

                            string datPTP = "DECL E6POS XP" + MainWindow._mainWindow.amountOfCommands.ToString()
                                + "={X " + MainWindow._mainWindow.values[MainWindow._mainWindow.amountOfCommands, 1].ToString().Replace(',', '.')
                                + ",Y " + MainWindow._mainWindow.values[MainWindow._mainWindow.amountOfCommands, 2].ToString().Replace(',', '.')
                                + ",Z " + MainWindow._mainWindow.values[MainWindow._mainWindow.amountOfCommands, 3].ToString().Replace(',', '.')
                                + ",A " + MainWindow._mainWindow.values[MainWindow._mainWindow.amountOfCommands, 4].ToString().Replace(',', '.')
                                + ",B " + MainWindow._mainWindow.values[MainWindow._mainWindow.amountOfCommands, 5].ToString().Replace(',', '.')
                                + ",C " + MainWindow._mainWindow.values[MainWindow._mainWindow.amountOfCommands, 6].ToString().Replace(',', '.')
                                + ",S " + MainWindow._mainWindow.values[MainWindow._mainWindow.amountOfCommands, 7].ToString().Replace(',', '.')
                                + ",T " + MainWindow._mainWindow.values[MainWindow._mainWindow.amountOfCommands, 8].ToString().Replace(',', '.') + "\n"
                                + ",E1 0.0,E2 0.0,E3 0.0,E4 0.0,E5 0.0,E6 0.0}" + "\n" +
                                "DECL FDAT FP" + MainWindow._mainWindow.amountOfCommands.ToString() + "={TOOL_NO 8,BASE_NO 0,IPO_FRAME #BASE,POINT2[] \" \",TQ_STATE FALSE}" + "\n" +
                                "DECL PDAT PPDAT" + MainWindow._mainWindow.amountOfCommands.ToString() + "={VEL"+ VelocityDat.Text +",ACC "+ BeschleunigungDat.Text+",APO_DIST " + APODISTDat.Text + ",APO_MODE #CPTP}" + "\n";
                            MainWindow._mainWindow.datStartText += datPTP;
                            MainWindow._mainWindow.amountOfCommands++;

                           
                    }

                    break;
            }
            
            Close();
        }

        private bool handle = true;
        private void ComboBox_DropDownClosed(object sender, EventArgs e)
        {
            if (handle) Handle();
            handle = true;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            handle = !cmb.IsDropDownOpen;
            Handle();
        }

        private void Handle()
        {
            switch (CommandBox.SelectedItem.ToString().Split(new string[] { ": " }, StringSplitOptions.None).Last())
            {
                case "Lin":
                    //Handle for the first combobox
                    
                    part_Scrollbar.Maximum = 2;
                    part_Scrollbar.Minimum = 0.001;
                    part_Scrollbar.SmallChange = 0.001;
                    part_Scrollbar3.Minimum = 0.1;
                    part_Scrollbar3.Maximum = 2.0;
                    part_Scrollbar3.SmallChange = 0.1;
                    part_Scrollbar4.Minimum = 1.0;
                    part_Scrollbar4.Maximum = 100.0;
                    part_Scrollbar4.SmallChange = 1.0;
                    part_Scrollbar5.Minimum = 0.0;
                    part_Scrollbar5.Maximum = 300.0;
                    part_Scrollbar5.SmallChange = 0.1;
                 
                    break;
                case "PTP":
                    part_Scrollbar.Maximum = 100;
                    part_Scrollbar.Minimum = 1;
                    part_Scrollbar.SmallChange = 1;
                    part_Scrollbar3.Minimum = 1;
                    part_Scrollbar3.Maximum = 100;
                    part_Scrollbar3.SmallChange = 1;
                    part_Scrollbar4.Minimum = 1;
                    part_Scrollbar4.Maximum = 100;
                    part_Scrollbar4.SmallChange = 1;
                    part_Scrollbar5.Minimum = 0;
                    part_Scrollbar5.Maximum = 100;
                    part_Scrollbar5.SmallChange = 1;

                    break;
                

            }
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {

            Close();
        }
    }
}
