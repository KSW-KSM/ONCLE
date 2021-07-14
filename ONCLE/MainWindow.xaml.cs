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
using Microsoft.Win32;
using System.Text.RegularExpressions;

namespace ONCLE
{

    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> class_name;
        string dicPath;
        DirectoryInfo dic;

        string dicPath_EBS;
        DirectoryInfo dic_EBS;

        string dicPath_Class;
        DirectoryInfo dic_Class;

        List<string> student_name = new List<string>();
        List<DateTime> student_time_start = new List<DateTime>();
        List<DateTime> student_time_stop = new List<DateTime>();


        DateTime play_time1;
        DateTime play_time2;
        TimeSpan play_time;

        int time;
        List<string> time_list = new List<string>();
        int time_index;

        List<string> file_data_save = new List<string>();
        List<string> open_file_nmae_list = new List<string>(); //출석부 순서
        public Dictionary<int, string> file_list = new Dictionary<int, string>();

        bool text_setting = false; //메인 폼에 텍스트가 있는지 여부

        Dictionary<string, string> student_item = new Dictionary<string, string>(); //총 학생 정보
        Dictionary<string, string> student_sort_item = new Dictionary<string, string>(); //표에 들어갈 정렬이 된 정보

        public MainWindow()
        {
            InitializeComponent();
            Directory_create();
            Main.Content = new View.StartMainView(this);

            
        }

        private void Directory_create()
        {
            dicPath = @"C:\Temp\EBS Support Data";
            dic = new DirectoryInfo(dicPath);
            
            if (dic.Exists == false)
            { // 폴더가 있습니다.
                dic.Create();
            }

            dicPath_EBS = @"C:\Temp\EBS Support Data\EBS File";
            dic_EBS = new DirectoryInfo(dicPath_EBS);

            if (dic_EBS.Exists == false)
            { // 폴더가 있습니다.
                dic_EBS.Create();
            }

            dicPath_Class = @"C:\Temp\EBS Support Data\Class list";
            dic_Class = new DirectoryInfo(dicPath_Class);

            if (dic_Class.Exists == false)
            { // 폴더가 있습니다.
                dic_Class.Create();
            }

        }

        private MatchCollection REX_Func(String REX, String TEXT)
        {
            Regex reg = new Regex(REX);
            return reg.Matches(TEXT); //공백이 존재해야 하기에, data_space_sum을 사용하지 않음.
        }

        private void close_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void WindowMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Class_Edit_Button_Click3(object sender, RoutedEventArgs e)
        {

            Main.Content = new View.Class_List();

            
        }

        private void Class_Edit_Button_Click4(object sender, RoutedEventArgs e)
        {

            Main.Content = new View.Chat_log_control();


        }

        private void Student_Information_Click2(object sender, RoutedEventArgs e)
        {

            Main.Content = new View.Student_Information();

        }

        private void Main_Start_Click1(object sender, RoutedEventArgs e)
        {

           Main.Content = new View.StartMainView(this);
                
        }

    }
}
