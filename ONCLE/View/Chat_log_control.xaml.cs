using Microsoft.Win32;
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
    /// Chat_log_control.xaml에 대한 상호 작용 논리
    /// </summary>
    /// 

    public class Table_Header
    {
        public string key { get; set; }
    }
    public partial class Chat_log_control : UserControl
    {
        bool start_ = false;
        string filename_;
        string filepath_;
        string TEXT ="-1";
        List<string> chat;
        List<string> All_chat;
        public Chat_log_control()
        {
            InitializeComponent();
        }

        private MatchCollection REX_Func(String REX, String TEXT)
        {
            Regex reg = new Regex(REX);
            return reg.Matches(TEXT); //공백이 존재해야 하기에, data_space_sum을 사용하지 않음.
        }

        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "Text files (*.txt)|*.txt";
            openFileDialog.InitialDirectory = @"%USERPROFILE%\Downloads";
            int l = 0;
            if (openFileDialog.ShowDialog() == true)
            {
                foreach (string filename in openFileDialog.FileNames)
                {
                    start_ = false;
                    if (l != 0)
                    {
                        break;
                    }
                    l++;
                    filename_ = openFileDialog.SafeFileName;
                    filepath_ = filename;
                    datagrid_lod(filename_, filepath_);
                }
            }
        }

        private void datagrid_lod(string file, string path)
        {
            start_ = true;
            TEXT = "";
            Console.WriteLine(key.Text);
            key_table.Items.Clear();
            string key1 = @"([님이]) +\S";
            List<string> key0 = new List<string>();
            chat = new List<string>();
            All_chat = new List<string>();
            key0.Clear();
            if (File.Exists(path))//파일 존재 유무 확인
            {
                TEXT = File.ReadAllText(path); //파일 read
                Console.WriteLine(TEXT);
            }
            key0 = TEXT.Split('\n').ToList();
            Console.WriteLine(file);

            comboxlist_.Items.Clear();
            for (int i = 0; i <= key0.Count - 1; i++)
            {
                if (key0[i].LastIndexOf(":") == 2 && !chat.Contains(key0[i].Substring(key0[i].LastIndexOf(":") + 4, 3)))
                {
                    chat.Add(key0[i].Substring(key0[i].LastIndexOf(":")+4, 3));
                    comboxlist_.Items.Add(chat[i]);
                    Console.WriteLine(key0[i]);
                }
                else if(key0[i].LastIndexOf(":") != 2)
                {
                    Console.WriteLine(key0[i]);
                    All_chat.Add(key0[i]);
                }
            }

            if (key.Text != "") {
                try
                {
                    for (int i = 0; i < key0.Count; i++)
                    {
                        
                        if (key0[i].IndexOf(key.Text) != -1 && key0[i].LastIndexOf(":") != 2)
                        {
                            if (key0[i].LastIndexOf(":") < key0[i].IndexOf(key.Text))
                            {
                                key_table.Items.Add(new Table_Header { key = key0[i] });
                            }
                        }
                    }
                }
                catch { }
            }
            else
            {

                for (int i = 0; i <= key0.Count - 1; i++)
                {
                    Console.WriteLine(key0[i].IndexOf(":"));
                    if (key0[i].LastIndexOf(":") != 2)
                    {
                        key_table.Items.Add(new Table_Header { key = key0[i] });
                    }
                }
            }
        }

        private void close_button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void comboxlist_DropDownClosed(object sender, EventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void key_table_LoadingRow(object sender, DataGridRowEventArgs e)
        {

        }

        private void set_button_Click(object sender, RoutedEventArgs e)
        {
            if (start_)
            {
                datagrid_lod(filename_, filepath_);
            }
        }

        private void comboxlist_DropDownClosed_1(object sender, EventArgs e)
        {
            if (TEXT != "-1")
            {
                try
                {
                    string key1 = @"([" + comboxlist_.Text + "])+(:)+\\S+";
                    Regex reg_key = new Regex(key1);
                    var results_key = reg_key.Matches(TEXT);
                    List<string> st_log = new List<string>();
                    int i1 = 0;
                    foreach (Match m in results_key)
                    {
                        st_log.Add(m.Value);
                        Console.WriteLine("st_log: " + st_log[i1]);
                        i1++;
                    }
                    AllChat.Text = "전체 채팅 수: " + st_log.Count + "회";

                    string key2 = @"(["+comboxlist_.Text+"])+[->]+\\D+(:)?";
                    Regex reg_key2 = new Regex(key2);
                    var results_key2 = reg_key2.Matches(TEXT);
                    List<string> st_log2 = new List<string>();
                    int i2 = 0;
                    foreach (Match m in results_key2)
                    {
                        st_log2.Add(m.Value);
                        Console.WriteLine("st_log: " + st_log2[i2]);
                        i2++;
                    }

                    AllvoidChat.Text = "전체 귓속말 수: " + st_log2.Count + "회";
                    int Count = st_log.Count + st_log2.Count;
                    if (Count != 0)
                    {
                        pnt_all.Text = "수업 채팅 점유율: 전체 채팅의 " + Math.Truncate(Convert.ToDouble(Count * 100) / Convert.ToDouble(All_chat.Count)) + "%";
                    }
                }
                catch { }
            }
            
        }
    }
}
