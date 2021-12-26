using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaladont
{
    class Program
    {
        static string fileName = System.IO.Path.GetFullPath(@"..\..\") + Path.DirectorySeparatorChar + "reci.txt";

        /*static void Main(string[] args)
        {
            List<string> words = new List<string>();

            using (FileStream fs = File.OpenRead(fileName))
            using (StreamReader sr = new StreamReader(fs))
            {
                String line;
                while ((line = sr.ReadLine()) != null)
                    words.Add(line);
            }
            List<Stack<string>> chains = new List<Stack<string>>();
            Stack<string> chain = new Stack<string>();

            foreach (string word in words) 
            {
                List<string> newWords = new List<string>(words);
                chain.Push(word);
                FillChain(word, newWords,ref chain);
                ReversePrintChain(chain);
                chains.Add(chain);
                chain.Clear();
            }

            Logger.SaveLog();
        }*/

        private static void ReversePrintChain(Stack<string> chain) 
        {
            if (chain.Count == 0)
                return;

            Stack<string> tempStack = new Stack<string>();

            while (chain.Count() > 0)
            {
                tempStack.Push(chain.Pop());
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("[ ");
            foreach (string str in tempStack) 
            {
                sb.Append($"{str} ");
            }
            sb.Append("]");

            Logger.WriteLine(sb.ToString());
            Logger.WriteLine($"LENGTH = {tempStack.Count}");
        }

        private static void FillChain(string currWord, List<string> words, ref Stack<string> currChain) 
        {
            string nextWord = GetNextWord(currWord, words);

            if (String.IsNullOrEmpty(nextWord) || currChain.Contains(nextWord))
                return;

            currChain.Push(nextWord);
            words.Remove(nextWord);
            FillChain(nextWord, words,ref currChain);
        }

        private static string GetNextWord(string currWord, List<string> currWords) 
        {
            foreach (string word in currWords.ToList()) 
            {
                string toCompare = currWord.Substring(currWord.Length - 2);
                if (word.StartsWith(toCompare) && !word.Equals(currWord)) 
                {
                    return word;
                }    
            }

            return null;
        }
    }

    public static class Logger
    {
        public static StringBuilder LogString = new StringBuilder(276438, Int32.MaxValue);
        public static void WriteLine(string str)
        {
            Console.WriteLine(str);
            LogString.Append(str).Append(Environment.NewLine);
        }
        public static void Write(string str)
        {
            Console.Write(str);
            LogString.Append(str);

        }
        public static void SaveLog(bool Append = false, string Path = "./Log.txt")
        {
            if (LogString != null && LogString.Length > 0)
            {
                if (Append)
                {
                    using (StreamWriter file = System.IO.File.AppendText(Path))
                    {
                        file.Write(LogString.ToString());
                        file.Close();
                        file.Dispose();
                    }
                }
                else
                {
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(Path))
                    {
                        file.Write(LogString.ToString());
                        file.Close();
                        file.Dispose();
                    }
                }
            }

            LogString.Clear();
        }
    }
}
