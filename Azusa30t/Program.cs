using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {

            string fileName = @"G:\input.txt";
            Count count1 = new Count(fileName);
            if (File.Exists(fileName))
            {
                count1.count_characters();//统计字符数
                count1.count_word();//统计单词总数
                count1.count_lines();//统计有效行数
                count1.count_words();//统计单词出现次数
            }
            Console.ReadKey();
        }
    }
    class Count
    {
        string fileName1;
        string fileName2 = @"G:\output.txt";//输出的文件夹
        public Count(string n)//构造函数，对传入的文件进行赋值
        {
            fileName1 = n;
        }
        public void count_characters()//统计字符数
        {
            FileStream file1 = new FileStream(fileName1, FileMode.Open, FileAccess.Read);//打开的文件
            int count;
            count = 0;
            byte[] buf = new byte[file1.Length];//缓存区接受文件夹中的内容
            file1.Read(buf, 0, buf.Length);
            for (int a = 0; a < buf.Length; a++)//利用循环判断是否为字符
            {
                if ((int)buf[a] <= 127)
                    count++;
            }
            //File.AppendAllText(@"G:\output.txt", "characters:");//强制输入某段数据到指定文件夹
            string avaible1 = "character: ";
            string avaible2;
            avaible2 = Convert.ToString(count);
            Console.WriteLine(avaible1 + avaible2);
            avaible1 = avaible1 + avaible2;
            StreamWriter file2 = new StreamWriter(fileName2, true);//写入该文件夹中，true表示追加到问价下一行
            file2.WriteLine(avaible1);
            file2.Flush();
            file1.Close();
            file2.Close();
        }
        public void count_word()//统计单词总数
        {
            Dictionary<string, int> dictionary_1;
            List<string> listLines = new List<string>();
            StreamReader reader = new StreamReader(@"G:\input.txt");
            string line = reader.ReadLine();
            while (line != "" && line != null)
            {
                listLines.Add(line);
                line = reader.ReadLine();
            }
            string str1;
            str1 = "";
            for (int i = 0; i < listLines.Count; i++)
            {
                str1 = str1 + listLines[i];
            }
            dictionary_1 = new Dictionary<string, int>();
            string[] words = Regex.Split(str1, @"\W+");
            int counts;
            counts = 0;
            foreach (string word in words)
            {
                if (dictionary_1.ContainsKey(word))
                {
                    dictionary_1[word]++;
                }
                else
                {
                    dictionary_1[word] = 1;
                }
            }
            foreach (KeyValuePair<string, int> avaible_1 in dictionary_1)
            {
                if(avaible_1.Key.Length>=4)
                {
                    string word = avaible_1.Key;
                    int count = avaible_1.Value;
                    counts = counts + count;
                }                
                //Console.WriteLine("{0}:{1}", word, count);
            }
            string avaible_2, avaible_3;
            avaible_2 = "words: ";
            Console.WriteLine(avaible_2 + counts);
            avaible_3 = Convert.ToString(counts);
            avaible_2 = avaible_2 + avaible_3;
            StreamWriter file2 = new StreamWriter(fileName2, true);
            file2.WriteLine(avaible_2);
            file2.Flush();
            file2.Close();
        }
        public void count_lines()//统计有效行数
        {
            int count;
            count = 0;
            string[] str = File.ReadAllLines(fileName1, System.Text.Encoding.Default);
            count = str.Length;
            string avaible_1, avaible_2;
            avaible_1 = "lines: ";            
            Console.WriteLine(avaible_1+count);
            avaible_2 = Convert.ToString(count);
            avaible_1 = avaible_1 + avaible_2;
            StreamWriter file = new StreamWriter(fileName2, true);
            file.WriteLine(avaible_1);
            file.Flush();
            file.Close();
        }
        public void count_words()//统计单词出现次数
        {
            Dictionary<string, int> dictory_1 = new Dictionary<string, int>();
            dictory_1 = sortdictionary_desc(dictory_1);
            Dictionary<string, int> dictory_2 = new Dictionary<string, int>();
            int avaible_2;
            avaible_2 = 0;
            foreach(KeyValuePair<string, int> sw in dictory_1)
            {
                avaible_2++;
                dictory_2.Add(sw.Key,sw.Value);
                if (avaible_2 == 10)
                    break;
            }
            List<KeyValuePair<string, int>> myList = new List<KeyValuePair<string, int>>(dictory_2);
            myList.Sort(delegate (KeyValuePair<string, int> s1, KeyValuePair<string, int> s2)
            {
                return s1.Key.CompareTo(s2.Key);
            });
            dictory_2.Clear();
            foreach (KeyValuePair<string, int> pair in myList)
            {
                dictory_2.Add(pair.Key, pair.Value);
            }
            foreach (KeyValuePair<string, int> avaible_1 in dictory_2)
            {
                string avaible_3 ="<"+ avaible_1.Key+">";
                int avaible_4 = avaible_1.Value;
                Console.WriteLine("{0}:{1}", avaible_3, avaible_4);
                string sr = Convert.ToString(avaible_4);
                avaible_3 = avaible_3 + ": ";
                avaible_3 = avaible_3 + avaible_4;
                StreamWriter stream = new StreamWriter(fileName2, true);
                stream.WriteLine(avaible_3);
                stream.Flush();
                stream.Close();

            }           
        }
        public Dictionary<string,int> sortdictionary_desc(Dictionary<string,int> dic)//对字典中的数据按照value排序
        {
            Dictionary<string, int> dictionary_1;
            List<string> listLines = new List<string>();
            StreamReader reader = new StreamReader(@"G:\input.txt");
            string line = reader.ReadLine();
            while (line != "" && line != null)
            {
                listLines.Add(line);
                line = reader.ReadLine();
            }
            string str1;
            str1 = "";
            for (int i = 0; i < listLines.Count; i++)
            {
                str1 = str1 + listLines[i];
            }
            dictionary_1 = new Dictionary<string, int>();
            string[] words = Regex.Split(str1, @"\W+");
            foreach (string word in words)
            {
                if (dictionary_1.ContainsKey(word))
                {
                    dictionary_1[word]++;
                }
                else
                {
                    dictionary_1[word] = 1;
                }
            }
            List<KeyValuePair<string, int>> myList = new List<KeyValuePair<string, int>>(dictionary_1);
            myList.Sort(delegate (KeyValuePair<string, int> s1, KeyValuePair<string, int> s2)
            {
                return s2.Value.CompareTo(s1.Value);
            });
            dictionary_1.Clear();
            int n = 0;
            foreach (KeyValuePair<string, int> pair in myList)
            {
                if(pair.Key.Length>=4)
                {
                    n++;
                    dictionary_1.Add(pair.Key, pair.Value);
                }
                if (n == 10)
                    break;
                
            }
            return dictionary_1;
        }
     }    
}
