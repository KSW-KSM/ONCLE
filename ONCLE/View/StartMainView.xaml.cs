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
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Dynamic;

namespace ONCLE.View
{

    /// <summary>
    /// StartMainView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class StartMainView : UserControl
    {

        bool drag = false;
        Point start_point = new Point(0, 0); //화면 좌표
        //DataTable table = new DataTable();
        int tab_size; //탭 사이즈 저장
        String[] tab_name; //탭 이름 저장
        int index_class = 0;
        int[] table_index;

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
        List<string> Mail = new List<string>();
        List<string> open_file_nmae_list = new List<string>(); //출석부 순서
        public Dictionary<int, string> file_list = new Dictionary<int, string>();

        bool text_setting = false; //메인 폼에 텍스트가 있는지 여부

        Dictionary<string, string> student_item = new Dictionary<string, string>(); //총 학생 정보
        List<string> student_list_ch = new List<string>();
        Dictionary<string, string> Mail_item = new Dictionary<string, string>(); //총 학생 정보
        Dictionary<string, string> Mail_item_Not = new Dictionary<string, string>(); //총 학생 정보
        Dictionary<string, string> student_sort_item = new Dictionary<string, string>(); //표에 들어갈 정렬이 된 정보
        List<string> open_file_nmae_list_copy;
        Window main_window;
        bool Filetest;
        List<string> File_name = new List<string>();
        int Color_sell_count;
        tardy_button_dlog least;
        static int least_value = 80;

        int Real_Student_Count;
        int row2;

        DataGridRow row;

        string lesson_date_week;

        string save_File = @"C:\Temp\EBS Support data\RunTimeSave.txt";
        string save_File2 = @"C:\Temp\EBS Support data\RunTimeSave2.txt";
        string save_File_list = @"C:\Temp\EBS Support data\RunTimeSave_list.txt";
        string save_File_list2 = @"C:\Temp\EBS Support data\RunTimeSave_list2.txt";
        public class Table_Header
        {
            public string ID { get; set; }
            public string Name { get; set; }
            public string Time { get; set; }
        }

        public StartMainView(MainWindow w)
        {
            InitializeComponent();
            Console.WriteLine("창이 바뀜");

            FileInfo file = new FileInfo(save_File);
            if (!file.Exists)
            {
                using (StreamWriter outputFile = new StreamWriter(save_File))
                {
                    outputFile.WriteLine("Null");
                }
                file = new FileInfo(save_File);
            }
            FileInfo file2 = new FileInfo(save_File2);
            if (!file2.Exists)
            {
                using (StreamWriter outputFile = new StreamWriter(save_File2))
                {
                    outputFile.WriteLine(least_value);
                }
                file2 = new FileInfo(save_File2);
            }
            else
            {
                
                least_value = Convert.ToInt32(Run_Time_Save2_Get());
                Console.WriteLine(least_value);
            }


            student_table.Items.Clear();
            main_window = w;



            dicPath = @"C:\Temp\EBS Support Data";
            dic = new DirectoryInfo(dicPath);

            dicPath_EBS = @"C:\Temp\EBS Support Data\EBS File";
            dic_EBS = new DirectoryInfo(dicPath_EBS);

            dicPath_Class = @"C:\Temp\EBS Support Data\Class list";
            dic_Class = new DirectoryInfo(dicPath_Class);

            for (int i = 0; i < dic_EBS.GetFiles().Length; i++)
            {
                list_box.Items.Add(dic_EBS.GetFiles().GetValue(i).ToString());
            }
            //list_box.ItemsSource = File_name;

            file_drag_or_click(Run_Time_Save_Get());

        }


        public void Run_Time_Save(string file_path)
        {
            using (StreamWriter outputFile = new StreamWriter(save_File))
            {
                if (file_path.Length != 0)
                {
                    outputFile.WriteLine(file_path);
                }
            }
            FileInfo file = new FileInfo(save_File);

        }

        public void Run_Time_Save_list(string lesson_time)
        {
            using (StreamWriter outputFile = new StreamWriter(save_File_list2))
            {
                Console.WriteLine("1:"+lesson_date_week);
                outputFile.WriteLine(top_title.Text.Substring(4));
                outputFile.WriteLine(student_name[0]);
                outputFile.WriteLine(lesson_date.Text.Substring(5,2));
                outputFile.WriteLine(lesson_date.Text.Substring(8, 2));
                outputFile.WriteLine(lesson_date_week.Substring(1, 1));
            }
            FileInfo file = new FileInfo(save_File2);

            using (StreamWriter outputFile = new StreamWriter(save_File_list))
            {
                outputFile.WriteLine(lesson_time);
                
                for (int i=0; i < student_list_ch.Count; i++)
                {
                    
                    outputFile.WriteLine(student_list_ch[i]);
                }
            }
            file = new FileInfo(save_File);

        }

        public string Run_Time_Save_Get()
        {
            string text = File.ReadAllText(save_File);
            string list = text;//.Trim('\n').Trim().Replace("\n", "").Split('\n');
            string[] list2 = Regex.Split(list, Environment.NewLine);
            return list2[0].Replace('\n', ' ').Trim();
        }

        public string Run_Time_Save2_Get()
        {
            string text = File.ReadAllText(save_File2);
            string list = text;//.Trim('\n').Trim().Replace("\n", "").Split('\n');
            string[] list2 = Regex.Split(list, Environment.NewLine);
            return list2[0].Replace('\n', ' ').Trim();
        }

        private void File_lod()
        {
            class_name = new List<string>();
            class_name.Clear();
            file_data_save.Clear();
            file_list.Clear();
            DirectoryInfo di = new DirectoryInfo(dicPath_Class);
            int i = 0;
            try //Form1 에서 db 풀면! try 풀기
            {
                Console.WriteLine("Class 경로에 있는 파일수:" + di.GetFiles().Length);
                foreach (System.IO.FileInfo file in di.GetFiles())
                {
                    file_list.Add(i, file.FullName);
                    class_name.Add(file.Name.Substring(0, file.Name.Length - 4));
                    i++;
                }

            }
            catch { }
            Console.WriteLine("Class 경로에 있는 파일수:" + file_list.Count);
        }

        private void File_read(int index) //file_list size를 이용하여 만들어야한다.
        {
            file_data_save.Clear();
            open_file_nmae_list.Clear();
            string text = File.ReadAllText(file_list[index]);
            string student_list1 = text;//.Trim('\n').Trim().Replace("\n", "").Split('\n');
            string[] student_list = Regex.Split(student_list1, Environment.NewLine);
            Console.WriteLine("count:" + String.Join(Environment.NewLine, student_list));
            Dictionary<string, int> Count1 = new Dictionary<string, int>(); //메일 카운트

            int i = 0;
            int c = 0;
            foreach (string item in student_list)
            {

                Console.WriteLine(i + "번째 값: " + open_file_nmae_list.Contains(item) + "\n글자수:" + item.Length + "\n-----------");
                
                if (item.Length != 0)
                {
                    if (!Count1.ContainsKey(item))
                    {
                        Console.WriteLine("실행성공1");
                        Count1.Add(item, 0);
                    }
                    if (open_file_nmae_list.Contains(item))
                    {
                        Console.WriteLine("실행성공2");
                        Count1[item]++;
                        open_file_nmae_list.Add(item.Replace('\n', ' ').Trim()+"(" +Count1[item].ToString()+")");
                        open_file_nmae_list_copy.Add(item.Replace('\n', ' ').Trim()+"(" +Count1[item].ToString()+")");
                    }
                    else
                    {
                        open_file_nmae_list.Add(item.Replace('\n', ' ').Trim());
                        open_file_nmae_list_copy.Add(item.Replace('\n', ' ').Trim());
                    }
                    Console.WriteLine(i + open_file_nmae_list[i]);
                    i++;
                }

            }
        }

        private void student_sort()
        {
            
            File_lod();


            Real_Student_Count = student_name.Count;
            //open_file_nmae_list //해당 리스트를 인덱스로 설정하고 시간을 매칭해준다. 
            //student_item 선생님 성함이 포함되어있음
            bool class_find = false; //클래스를 찾았는지 여부.
            student_sort_item = new Dictionary<string, string>();
            Dictionary<string, int> Mail_item_stack = new Dictionary<string, int>(); //메일 카운트
            Dictionary<string, int> Mail_item_Count = new Dictionary<string, int>(); //메일 카운트
            Dictionary<string, int> Mail_item_start_index_save = new Dictionary<string, int>(); //처음에 등장한 메일의 인덱스를 기억
            open_file_nmae_list_copy = new List<string>();
            List<int> count = new List<int>();
            count.Clear();
            count.Add(0);
            index_class = 0;
            int open_file_nmae_list_count;

            int COUNT_DN = 2;
            Console.WriteLine("file수:" + file_list.Count);
            for (int i = 0; i < file_list.Count; i++) //파일 수만큼 반복을 하면서
            {
                File_read(i); //파일을 읽어온다.
                Mail_item_stack.Clear();
                Mail_item_Count.Clear();
                index_class++;
                count.Add(0);
                for (int j = 1; j < student_name.Count; j++) //student_name은 선생님 정보가 걸러져있지 않음. 그렇기에 인덱스가 1부터 시작
                {
                    Console.WriteLine(j);
                    Console.WriteLine("open_file_nmae_list.count :"+ open_file_nmae_list.Count);
                    
                    //student_item[open_file_nmae_list[i]]

                    if (open_file_nmae_list_copy.Contains(student_name[j]))
                    {
                        Console.WriteLine("실행1");
                        if (!Mail_item_Count.ContainsKey(Mail_item[student_name[j]]))
                        {
                            Console.WriteLine("실행2");
                            Mail_item_Count.Add(Mail_item[student_name[j]], 0);
                            count[i]++;
                        }
                        else
                        {
                            Console.WriteLine("실행3");
                            Mail_item_Count[Mail_item[student_name[j]]]++;
                        }
                        //Console.WriteLine("{0} : {1} 이곳에 true가 뜸", student_name[j], !open_file_nmae_list_copy.Contains(student_name[j]));
                        open_file_nmae_list_copy.Remove(student_name[j]); //지우지 않으면, 동명의 학생 존재시 무한 카운트 발생
                        if (count[i] > COUNT_DN)
                        {
                            Console.WriteLine("실행4");
                            class_find = true;
                            break;
                        }
                        
                    }
                    //Console.WriteLine("{0} : {1}", student_name[j], !open_file_nmae_list_copy.Contains(student_name[j]));
                }
                if (count[i] > COUNT_DN)
                {
                    Console.WriteLine("실행5");
                    Console.WriteLine(student_name.Count);
                    Console.WriteLine(Mail_item.Count);
                    Mail_item_stack.Clear();
                    Mail_item_Count.Clear();
                    for (int j = 0; j < open_file_nmae_list.Count; j++)
                    {
                        Console.WriteLine(open_file_nmae_list[j]);
                        if (Mail_item.ContainsKey(open_file_nmae_list[j]))
                        {
                                Console.WriteLine("처음보는 학생발견:" + open_file_nmae_list[j]);
                                Mail_item_start_index_save.Add(Mail_item[open_file_nmae_list[j]], j);
                                Mail_item_Count.Add(Mail_item[open_file_nmae_list[j]], 0);
                                Mail_item_stack.Add(Mail_item[open_file_nmae_list[j]], Convert.ToInt32(student_item[open_file_nmae_list[j]]));

                                if (student_item.ContainsKey(open_file_nmae_list[j]))
                                {
                                    Console.WriteLine("해당 학생은 등록된학생:----------------"+ open_file_nmae_list[j]+" "+student_item[open_file_nmae_list[j]]);
                                    student_sort_item.Add(open_file_nmae_list[j], student_item[open_file_nmae_list[j]]);
                                }
                                else if (open_file_nmae_list[j].Length != 0)//그냥 공백만 아니면 허용
                                {//여기서 딕셔너리 갑추가 (지각생
                                    Console.WriteLine("해당 학생은 등록안된학생:----------------" + open_file_nmae_list[j]);
                                    student_item.Add(open_file_nmae_list[j], "0");
                                    student_sort_item.Add(open_file_nmae_list[j], "0");
                                    Console.WriteLine("정상 탈출");
                                }
                        }
                        else
                        {
                            student_item.Add(open_file_nmae_list[j], "0");
                            student_sort_item.Add(open_file_nmae_list[j], "0");
                        }

                    }
                }
                else
                {
                    continue;
                }
                grade_class.Text = class_name[i];
                Console.WriteLine("{0} 브레이크 걸림 {1}에서", i, file_list[i]);
                break;
            }
            if (!class_find)
            {
                student_sort_item = student_item;
                index_class++;
                class_name.Add("정보가 없는 반");
                grade_class.Text = "정보가 없는 반";
                class_find = false;
                Console.WriteLine(index_class);
            }
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
            if (openFileDialog.ShowDialog() == true)
            {
                top_title.Text = "파일이 등록되었습니다.";
                foreach (string filename in openFileDialog.FileNames)
                {
                    file_drag_or_click(filename, openFileDialog.SafeFileName);
                }
            }
        }


        private void datagrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            // Get the DataRow corresponding to the DataGridRow that is loading.
            if (Color_sell_count < student_sort_item.Count)
            {
                Console.WriteLine("색을 바꾸러 들어옴" + e.Row.GetIndex().ToString() + "     "+ Color_sell_count + student_sort_item.Keys.ToArray()[Color_sell_count]);
                var item = e.Row.Item as ExpandoObject;
                
                if (student_sort_item.Count > Color_sell_count && e.Row.GetIndex() == Color_sell_count)
                {
                    Console.WriteLine("색바꾸기 직접 접근");
                    if ((Convert.ToDouble(0.01 * least_value) * Convert.ToDouble(play_time.TotalMinutes)) <= Convert.ToDouble(student_sort_item.Values.ToArray()[Color_sell_count]) && Convert.ToInt32(student_sort_item.Values.ToArray()[Color_sell_count]) != 0)
                    {//출석
                        e.Row.Background = new SolidColorBrush(Colors.Green);
                        student_list_ch.Add("");
                    }
                    else if (Convert.ToDouble(0.01 * least_value) * Convert.ToDouble(play_time.TotalMinutes) >= Convert.ToDouble(student_sort_item.Values.ToArray()[Color_sell_count]) && Convert.ToInt32(student_sort_item.Values.ToArray()[Color_sell_count]) != 0)
                    {//지각
                        e.Row.Background = new SolidColorBrush(Colors.Yellow);
                        student_list_ch.Add("◐");
                    }
                    else
                    {//무단
                        e.Row.Background = new SolidColorBrush(Colors.Red);
                        student_list_ch.Add("◎");
                    }
                    Color_sell_count++;
                }

            }
            Console.WriteLine(top_title.Text+ top_title.Text.Substring(0, 1));
            if (Color_sell_count == student_sort_item.Count)
            {
                Console.WriteLine(top_title.Text.Substring(0, 1));
                Run_Time_Save_list(top_title.Text.Substring(0, 1));
            }

        }

        private void file_drag_or_click(String FilePath, String Filenmae = null, DragEventArgs e = null)
        {
            if (FilePath != "Null")
            {
                student_list_ch.Clear();
                Run_Time_Save(FilePath);
                string TEXT = "";
                const string REX_NAME = @"\/+[가-힣]{2,4}";
                const string REX_TIME = @"([0-5][0-9]):([0-5][0-9]):([0-5][0-9])";
                const string REX_PLAY_TIME = @"(19[7-9][0-9]|20\d{2})-(0[0-9]|1[0-2])-(0[1-9]|[1-2][0-9]|3[0-1]) ([0-5][0-9]):([0-5][0-9])";
                const string REX_EMAIL = @"[a-zA-Z0-9!#$%&'*+=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?";
                const string REX_LESSON = @"([0-9]+[가-힣]{2,4})+[ \t]+[\S{0, }]*"; //얘는 공백이 있어야함.
                const string REX_DATE = @"(19[7-9][0-9]|20\d{2})-(0[0-9]|1[0-2])-(0[1-9]|[1-2][0-9]|3[0-1])";
                const string REX_DATE_WEEK = @"\((.)\)";
                String[] path;

                student_table.Items.Clear();

                if (File.Exists(FilePath))//파일 존재 유무 확인
                {
                    text_setting = true;
                    TEXT = File.ReadAllText(FilePath); //파일 read
                    Console.WriteLine(TEXT);
                }
                try
                {
                    text_setting = true;
                    student_name.Clear();
                    student_time_start.Clear();
                    student_time_stop.Clear();
                    Mail.Clear();
                    Mail_item.Clear();


                    //정규식 작성 사전단계--
                    char[] rex_space = { ' ' };
                    string[] data_space = TEXT.Split(rex_space, StringSplitOptions.RemoveEmptyEntries);
                    string data_space_sum = "";


                    for (int i = 0; i < data_space.Length; i++)
                    {
                        data_space_sum += data_space[i];
                    }
                    //-----------------------------------

                    //수업정보 추출
                    var results_lesson = REX_Func(REX_LESSON, TEXT); //공백이 존재해야 하기에, data_space_sum을 사용하지 않음.
                    string save_lesson = "";

                    foreach (Match m in results_lesson)
                    {
                        save_lesson = m.Value;
                    }
                    //Console.WriteLine("save_lesson: "+ save_lesson);
                    top_title.Text = save_lesson;

                    Regex reg_Mail = new Regex(REX_EMAIL);
                    var results_Mail = reg_Mail.Matches(TEXT);
                    int i1 = 0;
                    foreach (Match m in results_Mail)
                    {
                        Mail.Add(m.Value);
                        Console.WriteLine("Mail: " + Mail[i1]);
                        i1++;
                    }


                    //수업날짜 추출
                    var results_date = REX_Func(REX_DATE, TEXT);
                    string save_date = "";

                    foreach (Match m in results_date)
                    {
                        save_date = m.Value;
                    }
                    //Console.WriteLine("save_date: "+ save_date);
                    lesson_date.Text = save_date;

                    var results_date_WEEK = REX_Func(REX_DATE_WEEK, TEXT);
                    string save_date_WEEK = "";

                    foreach (Match m in results_date_WEEK)
                    {
                        save_date_WEEK = m.Value;
                    }
                    Console.WriteLine(save_date_WEEK);
                    lesson_date_week = save_date_WEEK;





                    //이름 추출
                    Regex reg = new Regex(REX_NAME);
                    var results = REX_Func(REX_NAME, data_space_sum);
                    char[] rex_nmae_sl = { '/' };
                    int j = 0;
                    //Console.WriteLine("save_student_count: "+ results.Count);
                    foreach (Match m in results)
                    {
                        student_name.Add(m.Value.Trim().Replace("/", ""));
                        Console.WriteLine("save_student: " + student_name[j]);
                        j++;

                    }

                    try
                    {
                        Filetest = true;
                        teacher_name.Text = student_name[0];

                    }
                    catch
                    {
                        Filetest = false;
                        top_title.Text = "잘못된 파일입니다.";
                    }

                    //시간 추출
                    var results_time = REX_Func(REX_TIME, data_space_sum);
                    Console.WriteLine(results_time.Count);
                    int k = 0;
                    int c = 1;
                    time_index = 0;
                    time_list.Clear();
                    student_item.Clear();
                    student_sort_item.Clear();

                    Console.WriteLine(Mail.Count + "  ,   " + student_name.Count);

                    Dictionary<string, int> Count2 = new Dictionary<string, int>(); //메일 카운트

                    foreach (Match m in results_time)
                    {
                        //GroupCollection name = m.Groups; 
                        //Console.WriteLine("{0}={1}=", m[0].Index, m[1].Value.Length);
                        Console.WriteLine(m);
                        if (k % 2 == 0)
                        {
                            if (k != 0) //선생님 시간을 걸러냄
                            {
                                student_time_start.Add(Convert.ToDateTime(m.Value));
                            }
                        }
                        else if (k % 2 == 1)
                        {
                            if (k != 1) //선생님 시간을 걸러냄
                            {
                                if (!Count2.ContainsKey(student_name[c]))
                                {
                                    Count2.Add(student_name[c], 0);
                                }
                                student_time_stop.Add(Convert.ToDateTime(m.Value));

                                TimeSpan dateDiff = student_time_stop[time_index] - student_time_start[time_index]; //여기부터
                                                                                                                    //Console.WriteLine("{0}-{1}", student_time_start[time_index], student_time_stop[time_index]);
                                time = Convert.ToInt32(dateDiff.TotalMinutes);//여기까지 함수로 정의 (나중에)
                                time_list.Add(time.ToString());
                                if (student_item.ContainsKey(student_name[c]) && !Mail_item.ContainsValue(Mail[c + 1]))//메일이 등록되어 있지않고, 동성의 학생이 존재할경우
                                {
                                    Console.WriteLine("실행1");
                                    Count2[student_name[c]]++;
                                    student_item.Add(student_name[c] + "(" + Count2[student_name[c]].ToString() + ")", time.ToString());
                                    Mail_item.Add(student_name[c] + "(" + Count2[student_name[c]].ToString() + ")", Mail[c + 1]);
                                }//()동명학생 sort리스트 네임 변수처리해주기
                                else if (student_item.ContainsKey(student_name[c]) && Mail_item.ContainsValue(Mail[c + 1])) //메일이 등록되어 있고, 동성의 학생이 존재할경우
                                {
                                    Console.WriteLine("실행2");
                                    student_item[student_name[c]] = (Convert.ToInt32(student_item[student_name[c]]) + time).ToString();
                                }
                                else
                                {
                                    Console.WriteLine("실행3");
                                    student_item.Add(student_name[c], time.ToString());
                                    Mail_item.Add(student_name[c], Mail[c + 1]);
                                }

                                Console.WriteLine("현재 student 값" + student_item[student_name[c]] + student_name[c] + Mail[c + 1]);
                                time_index++;
                                c++;
                            }
                        }

                        k++;

                    }

                    //Console.WriteLine("정상");
                    //정렬
                    //student_sort();
                    //Console.WriteLine("3구역 size", student_sort_item.Count);
                    /*
                    for (int i = 0; i < student_sort_item.Count; i++)
                    {
                        //TimeSpan dateDiff = student_time_stop[i] - student_time_start[i]; //여기부터
                        //Console.WriteLine("{0}-{1}", student_time_start[i], student_time_stop[i]);
                        //time = Convert.ToInt32(dateDiff.TotalMinutes);//여기까지 함수로 정의 (나중에)
                        if (Convert.ToInt32(student_sort_item.Values.ToArray()[i]) < 10)
                        {
                            dataGridView1.Rows.Add(dataGridView1.Rows.Count + 1, student_sort_item.Keys.ToArray()[i], "0" + student_sort_item.Values.ToArray()[i] + "분");
                        }
                        else
                        {
                            dataGridView1.Rows.Add(dataGridView1.Rows.Count + 1, student_sort_item.Keys.ToArray()[i], student_sort_item.Values.ToArray()[i] + "분");
                        }
                    }*/
                    //Console.WriteLine(student_item[student_name[i + 1]].ToString());




                    Regex reg_time_ti = new Regex(REX_PLAY_TIME);
                    var results_time_ti = REX_Func(REX_PLAY_TIME, TEXT);


                    int l = 0;
                    foreach (Match m in results_time_ti)
                    {

                        if (l == 0)
                        {
                            play_time1 = Convert.ToDateTime(m.Value);
                        }
                        else
                        {
                            play_time2 = Convert.ToDateTime(m.Value);
                        }

                        l++;
                    }
                    play_time = play_time2 - play_time1;
                    Console.WriteLine(play_time1);
                    Console.WriteLine(play_time2);
                    Console.WriteLine(play_time);
                    lesson_time.Text = play_time.TotalMinutes.ToString() + "분";
                    //string[] name = Regex.Replace(data_space_sum, rex_name, "");
                    //string[] data_lod = new string[];
                    //tabControl1.TabPages[0].Text = class_name[index_class - 1];

                    student_sort();
                    Color_sell_count = 0;
                    for (int i = 0; i <= student_sort_item.Count; i++)
                    {

                        student_table.Items.Add(new Table_Header { ID = (i + 1).ToString(), Name = student_sort_item.Keys.ToArray()[i], Time = student_sort_item.Values.ToArray()[i] + "분" });
                        //Table_headers.Add(new Table_Header { ID = (i+1).ToString(), Name = student_sort_item.Keys.ToArray()[i], Time = student_sort_item.Values.ToArray()[i] + "분" });
                        //student_table.bind

                    }

                    if (Filetest)
                    {
                        string dest_file = System.IO.Path.Combine(dicPath_EBS, lesson_date.Text + " " + top_title.Text + ".txt");
                        //Console.WriteLine();
                        System.IO.File.Copy(FilePath, dest_file, true);
                        list_box.Items.Insert(0, lesson_date.Text + " " + top_title.Text + ".txt");
                        //list_box.Items.Add(lesson_date.Text + " " + top_title.Text + ".txt");
                        list_box.Items.SortDescriptions.Add( //정렬
                        new System.ComponentModel.SortDescription("",
                        System.ComponentModel.ListSortDirection.Ascending));
                        list_box.SelectedValue = lesson_date.Text + " " + top_title.Text + ".txt";
                    }
                }

                catch
                {


                }
            }

        }

        private void btn_dilog_click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("처음 값: " + least_value);
            least = new tardy_button_dlog(this.sub_grid, least_value);
            sub_grid.Children.Add(least);
            //if(least.return_p_sent )
        }

        private void student_table_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void student_table_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void student_table_Drop(object sender, DragEventArgs e)
        {
            
        }

        private void student_table_PreviewDrop(object sender, DragEventArgs e)
        {
            Console.WriteLine(e.Data);
        }

        private void close_button_Click(object sender, RoutedEventArgs e)
        {
            least_value = Convert.ToInt32(Run_Time_Save2_Get());
            student_list_ch.Clear();
            Console.WriteLine(least_value);
            file_drag_or_click(Run_Time_Save_Get());
        }

        private void ListBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(1);
        }
    }

}
