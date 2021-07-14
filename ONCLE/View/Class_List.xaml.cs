using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using System.Collections.ObjectModel;
using System.IO;
using System.Text.RegularExpressions;

namespace ONCLE.View
{
    /// <summary>
    /// Class_List.xaml에 대한 상호 작용 논리
    /// </summary>
    /// 
    public partial class Class_List : UserControl
    {
        public class MyClass
        {
            public string IsColor { get; set; }

            public string Time1 { get; set; }
            public string Time2 { get; set; }
            public string Time3 { get; set; }
            public string Time4 { get; set; }
            public string Time5 { get; set; }
            public string Time6 { get; set; }
            public string Time7 { get; set; }
            public string Time8 { get; set; }

            public string Time9 { get; set; }
            public string Time10 { get; set; }
            public string Time11 { get; set; }
        }

        public class Class_ListViewModel
        {
            string save_File_list = @"C:\Temp\EBS Support data\RunTimeSave_list.txt";
            string save_File_list2 = @"C:\Temp\EBS Support data\RunTimeSave_list2.txt";
            string[] list2;
            string[] list3;
            public ObservableCollection<MyClass> student_list { get; set; }

            public string Run_Time_Save_Get(int index)
            {
                string text = File.ReadAllText(save_File_list) ;
                string list = text;//.Trim('\n').Trim().Replace("\n", "").Split('\n');
                list2 = Regex.Split(list, Environment.NewLine);
                return list2[index].Replace('\n', ' ').Trim();
            }

            public Class_ListViewModel()
            {
                FileInfo file_0 = new FileInfo(save_File_list);
                Console.WriteLine(file_0.Exists);
                if (file_0.Exists)
                {
                    string text = File.ReadAllText(save_File_list);
                    string list = text;//.Trim('\n').Trim().Replace("\n", "").Split('\n');
                    list2 = Regex.Split(list, Environment.NewLine);
                    int control = 0;
                    bool con = true;
                    student_list = new ObservableCollection<MyClass>();

                    for (int i = 1; i < list2.Length; i++)
                    {
                        Console.WriteLine(control);
                        if ((((i - 1) / 5) % 2) == 0)//7
                        {
                            control++;
                            switch (Run_Time_Save_Get(0))
                            {
                                case "1":
                                    student_list.Add(new MyClass { IsColor = 1.ToString(), Time1 = (i).ToString(), Time2 = " ", Time3 = Run_Time_Save_Get(i) });
                                    break;
                                case "2":
                                    student_list.Add(new MyClass { IsColor = 1.ToString(), Time1 = (i).ToString(), Time2 = " ", Time3 = "", Time4 = Run_Time_Save_Get(i) });
                                    break;
                                case "3":
                                    student_list.Add(new MyClass { IsColor = 1.ToString(), Time1 = (i).ToString(), Time2 = " ", Time3 = " ", Time4 = " ", Time5 = Run_Time_Save_Get(i) });
                                    break;
                                case "4":
                                    student_list.Add(new MyClass { IsColor = 1.ToString(), Time1 = (i).ToString(), Time2 = " ", Time3 = " ", Time4 = " ", Time5 = "", Time6 = Run_Time_Save_Get(i) });
                                    break;
                                case "5":
                                    student_list.Add(new MyClass { IsColor = 1.ToString(), Time1 = (i).ToString(), Time2 = "", Time3 = "", Time4 = "", Time5 = "", Time6 = " ", Time7 = Run_Time_Save_Get(i) });
                                    break;
                                case "6":
                                    student_list.Add(new MyClass { IsColor = 1.ToString(), Time1 = (i).ToString(), Time2 = "", Time3 = "", Time4 = "", Time5 = "", Time6 = " ", Time7 = "", Time8 = Run_Time_Save_Get(i) });
                                    break;
                                case "7":
                                    student_list.Add(new MyClass { IsColor = 1.ToString(), Time1 = (i).ToString(), Time2 = "", Time3 = "", Time4 = "", Time5 = "", Time6 = " ", Time7 = "", Time8 = "", Time9 = Run_Time_Save_Get(i) });
                                    break;
                                case "8":
                                    student_list.Add(new MyClass { IsColor = 1.ToString(), Time1 = (i).ToString(), Time2 = "", Time3 = "", Time4 = "", Time5 = "", Time6 = " ", Time7 = "", Time8 = "", Time9 = "", Time10 = Run_Time_Save_Get(i) });
                                    break;
                            }
                        }
                        else if ((((i - 1) / 5) % 2) == 1)
                        {
                            control++;
                            switch (Run_Time_Save_Get(0))
                            {
                                case "1":
                                    student_list.Add(new MyClass { IsColor = 0.ToString(), Time1 = (i).ToString(), Time2 = " ", Time3 = Run_Time_Save_Get(i) });
                                    break;
                                case "2":
                                    student_list.Add(new MyClass { IsColor = 0.ToString(), Time1 = (i).ToString(), Time2 = " ", Time3 = "", Time4 = Run_Time_Save_Get(i) });
                                    break;
                                case "3":
                                    student_list.Add(new MyClass { IsColor = 0.ToString(), Time1 = (i).ToString(), Time2 = " ", Time3 = " ", Time4 = " ", Time5 = Run_Time_Save_Get(i) });
                                    break;
                                case "4":
                                    student_list.Add(new MyClass { IsColor = 0.ToString(), Time1 = (i).ToString(), Time2 = " ", Time3 = " ", Time4 = " ", Time5 = "", Time6 = Run_Time_Save_Get(i) });
                                    break;
                                case "5":
                                    student_list.Add(new MyClass { IsColor = 0.ToString(), Time1 = (i).ToString(), Time2 = "", Time3 = "", Time4 = "", Time5 = "", Time6 = " ", Time7 = Run_Time_Save_Get(i) });
                                    break;
                                case "6":
                                    student_list.Add(new MyClass { IsColor = 0.ToString(), Time1 = (i).ToString(), Time2 = "", Time3 = "", Time4 = "", Time5 = "", Time6 = " ", Time7 = "", Time8 = Run_Time_Save_Get(i) });
                                    break;
                                case "7":
                                    student_list.Add(new MyClass { IsColor = 0.ToString(), Time1 = (i).ToString(), Time2 = "", Time3 = "", Time4 = "", Time5 = "", Time6 = " ", Time7 = "", Time8 = "", Time9 = Run_Time_Save_Get(i) });
                                    break;
                                case "8":
                                    student_list.Add(new MyClass { IsColor = 0.ToString(), Time1 = (i).ToString(), Time2 = "", Time3 = "", Time4 = "", Time5 = "", Time6 = " ", Time7 = "", Time8 = "", Time9 = "", Time10 = Run_Time_Save_Get(i) });
                                    break;
                            }
                        }

                    }

                }
            }
        }

        public class Top_Bottom
        {
            public string Time1 { get; set; }
            public string Time2 { get; set; }
            public string Time3 { get; set; }
            public string Time4 { get; set; }
            public string Time5 { get; set; }
            public string Time6 { get; set; }
            public string Time7 { get; set; }
            public string Time8 { get; set; }

            public string Time9 { get; set; }
        }

        string save_File_list2 = @"C:\Temp\EBS Support data\RunTimeSave_list2.txt";
        string[] list3;
        public string Run_Time_Save_Get2(int index)
        {
            string text = File.ReadAllText(save_File_list2);
            string list = text;//.Trim('\n').Trim().Replace("\n", "").Split('\n');
            list3 = Regex.Split(list, Environment.NewLine);
            return list3[index].Replace('\n', ' ').Trim();
        }

        Class_ListViewModel VM = new Class_ListViewModel();
        public Class_List()
        {

            InitializeComponent();
            FileInfo file_0 = new FileInfo(save_File_list2);
            Console.WriteLine(file_0.Exists);
            if (file_0.Exists)
            {
                DataContext = VM;

                month.Text = Run_Time_Save_Get2(2) + "   월";
                Work.Text = Run_Time_Save_Get2(3) + "    일";
                Day_Week.Text = Run_Time_Save_Get2(4);
                switch (VM.Run_Time_Save_Get(0))
                {
                    case "1":
                        Time1.Text = Run_Time_Save_Get2(0);
                        break;
                    case "2":
                        Time2.Text = Run_Time_Save_Get2(0);
                        break;
                    case "3":
                        Time3.Text = Run_Time_Save_Get2(0);
                        break;
                    case "4":
                        Time4.Text = Run_Time_Save_Get2(0);
                        break;
                    case "5":
                        Time5.Text = Run_Time_Save_Get2(0);
                        break;
                    case "6":
                        Time6.Text = Run_Time_Save_Get2(0);
                        break;
                    case "7":
                        Time7.Text = Run_Time_Save_Get2(0);
                        break;
                    case "8":
                        Time8.Text = Run_Time_Save_Get2(0);
                        break;
                }
                switch (VM.Run_Time_Save_Get(0))
                {
                    case "1":
                        Time1_.Text = Run_Time_Save_Get2(1);
                        break;
                    case "2":
                        Time2_.Text = Run_Time_Save_Get2(1);
                        break;
                    case "3":
                        Time3_.Text = Run_Time_Save_Get2(1);
                        break;
                    case "4":
                        Time4_.Text = Run_Time_Save_Get2(1);
                        break;
                    case "5":
                        Time5_.Text = Run_Time_Save_Get2(1);
                        break;
                    case "6":
                        Time6_.Text = Run_Time_Save_Get2(1);
                        break;
                    case "7":
                        Time7_.Text = Run_Time_Save_Get2(1);
                        break;
                    case "8":
                        Time8_.Text = Run_Time_Save_Get2(1);
                        break;
                }
            }
        }



        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DataGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
