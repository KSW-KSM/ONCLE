using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace ONCLE.View
{
    /// <summary>
    /// tardy_button_dlog.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class tardy_button_dlog : UserControl
    {
        string save_File = @"C:\Temp\EBS Support data\RunTimeSave2.txt";

        public int return_p_sent;
        Grid User;
        public tardy_button_dlog(Grid U, int back_value)
        {

            InitializeComponent();
            p_sent.Text = Run_Time_Save_Get();
            User = U;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("버튼 클릭시의: "+p_sent.Text);
            if (p_sent.Text == "")
            {
                p_sent.Background = new SolidColorBrush(Colors.Red);
            }
            else
            {
                return_p_sent = Convert.ToInt32(p_sent.Text);
                if (Convert.ToInt32(p_sent.Text) >= 0)
                {

                    if (User != null)
                        Run_Time_Save(return_p_sent);
                        User.Children.Remove(this);

                }
                else
                {
                    p_sent.Background = new SolidColorBrush(Colors.Red);
                }

            }
        }

        public void Run_Time_Save(int pnt)
        {
            using (StreamWriter outputFile = new StreamWriter(save_File))
            {
                if (pnt >= 0)
                {
                    outputFile.WriteLine(pnt.ToString());
                }
                else
                {
                    outputFile.WriteLine("80");
                }
            }
            FileInfo file = new FileInfo(save_File);
        }




        public string Run_Time_Save_Get()
        {
            string text = File.ReadAllText(save_File);
            string list = text;//.Trim('\n').Trim().Replace("\n", "").Split('\n');
            string[] list2 = Regex.Split(list, Environment.NewLine);
            Console.WriteLine();
            return list2[0].Replace('\n', ' ').Trim();

        }


        private void p_sent_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
