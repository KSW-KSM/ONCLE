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
    /// Student_Information.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Student_Information : UserControl
    {
        string dicPath_Class;
        string File_name;
        public List<string> file_name = new List<string>(); //file_list의 키값을 저장
        public Dictionary<string, string> file_list = new Dictionary<string, string>();

        public Student_Information()
        {
            InitializeComponent();
            dicPath_Class = @"C:\Temp\EBS Support Data\Class list";
            DirectoryInfo di = new DirectoryInfo(dicPath_Class);
            try //Form1 에서 db 풀면! try 풀기
            {
                foreach (System.IO.FileInfo file in di.GetFiles())
                {
                    file_list.Add(file.Name.Substring(0, file.Name.Length - 4), file.FullName);
                    file_name.Add(file.Name.Substring(0, file.Name.Length - 4));
                    comboxlist.Items.Add(file.Name.Substring(0, file.Name.Length - 4));
                }
            }
            catch { }
        }

        private void InputTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("해당 반을 정말로 삭제하시겠습니까\"?", "삭제 메세지", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    if (file_list.ContainsKey(comboxlist.Text))
                    {
                        System.IO.File.Delete(file_list[comboxlist.Text]);
                        DirectoryInfo di = new DirectoryInfo(dicPath_Class);
                        //최적화 필요
                        comboxlist.Items.Clear();
                        student_input.Clear();
                        InputTextBox.Clear();
                        file_list.Clear();
                        foreach (System.IO.FileInfo file in di.GetFiles())
                        {
                            file_list.Add(file.Name.Substring(0, file.Name.Length - 4), file.FullName);
                            comboxlist.Items.Add(file.Name.Substring(0, file.Name.Length - 4));
                        }
                    }
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }


        private void comboxlist_DropDownClosed(object sender, EventArgs e)
        {
            if(comboxlist.Text != "")
            {
                InputTextBox.Text = comboxlist.Text;

                string text = File.ReadAllText(file_list[comboxlist.Text]);
                string[] data_lod = text.Split('\n');
                Console.WriteLine(text);
                student_input.Text = text;
                bottom_text.Text = comboxlist.Text+"을(를) 수정합니다.";
            }



        }

        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Console.WriteLine('d');
                if (InputTextBox.Text.Length > 1)
                {
                    File_name = @"C:\Temp\EBS Support data\Class list\" + InputTextBox.Text + ".txt";
                    //char[] rex_space = { '\n' };
                    //string[] student_list = student_input.Text.Split(rex_space, StringSplitOptions.RemoveEmptyEntries);
                    string student_list1 = student_input.Text;//.Trim('\n').Trim().Replace("\n", "").Split('\n');
                    string[] student_list = Regex.Split(student_list1, Environment.NewLine);
                    Console.WriteLine("count:" + String.Join(Environment.NewLine, student_list));
                    int i = 0;
                    using (StreamWriter outputFile = new StreamWriter(File_name))
                    {
                        foreach (string line in student_list)
                        {
                            
                            Console.WriteLine(i + "번째 값: " + line + "\n글자수:"+line.Length+ "\n-----------");
                            i++;
                            if (line.Length != 0)
                            {
                                Console.WriteLine("접근");


                                outputFile.WriteLine(line);


                            }

                        }
                        outputFile.Close();
                    }
                    FileInfo file = new FileInfo(File_name);
                    Console.WriteLine("접근1");
                    if (!file_list.ContainsKey(file.Name.Substring(0, file.Name.Length - 4)))
                    {
                        file_list.Add(file.Name.Substring(0, file.Name.Length - 4), file.FullName);
                        file_name.Add(file.Name.Substring(0, file.Name.Length - 4));
                        comboxlist.Items.Add(file.Name.Substring(0, file.Name.Length - 4));
                    }                             
                    comboxlist.SelectedIndex = -1;
                    InputTextBox.Clear();
                    student_input.Clear();
                    bottom_text.Text = "저장되었습니다.";
                }
                else
                {
                    bottom_text.Text = "반 이름은 2글자 이상으로 작성해주세요.";
                }
            }
            catch
            {

            }
        }

        private void student_input_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }


        private void student_input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                student_input.Text = student_input.Text + '\n';
            }
        }
    }
}
