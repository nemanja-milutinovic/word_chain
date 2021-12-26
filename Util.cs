using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaladont
{
    public static class Util
    {
        public static void AddAll<T>(this Stack<T> stack1, Stack<T> stack2)
        {
            T[] arr = new T[stack2.Count];
            stack2.CopyTo(arr, 0);

            for (int i = arr.Length - 1; i >= 0; i--)
            {
                stack1.Push(arr[i]);
            }
        }

        public static void PrintStack(Stack<Node> stack)
        {
            if (stack.Count == 0)
                return;

            StringBuilder sb = new StringBuilder();
            sb.Append("[ ");
            foreach (Node n in stack)
            {
                sb.Append($"{n.Word} ");
            }
            sb.Append("]");

            Logger.WriteLine(sb.ToString());
            Logger.WriteLine($"LENGTH = {stack.Count}");
        }

        public static Stack<T> Clone<T>(this Stack<T> original)
        {
            var arr = new T[original.Count];
            original.CopyTo(arr, 0);
            Array.Reverse(arr);
            return new Stack<T>(arr);
        }

        public static HashSet<T> ToHashSet<T>(
        this IEnumerable<T> source,
        IEqualityComparer<T> comparer = null)
        {
            return new HashSet<T>(source, comparer);
        }

        public static bool AreAllVisited(HashSet<Node> nodes, HashSet<Node> visited)
        {
            foreach (Node n in nodes)
            {
                if (!visited.Contains(n))
                    return false;
            }

            return true;
        }
    }

    public static class Logger
    {
        public static StringBuilder LogString = new StringBuilder(276438, Int32.MaxValue);
        public static int logNum = 1;
        public static string Path = "./Log.txt";

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
        public static void SaveLog(bool Append = false)
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

            if (new FileInfo(Path).Length > 500000000)
            {
                Path = $"./Log{logNum++}.txt";
            }
        }
    }

    
}
