using System;
using System.Diagnostics;
using System.IO;

namespace RerunApp
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2 || string.IsNullOrEmpty(args[0]) || string.IsNullOrEmpty(args[1]))
            {
                Console.WriteLine(@"Rerun Process
Usage: RERUNPROCESS.EXE [EXECUTABLENAME] [PATH] 
");
                return;
            }
            
            if (!IsProcessOpen(args[0].ToLower().Replace(".exe", "")))
            {
                StartProcess(args);
            }
            else
            {
                Console.WriteLine("{0} Process Is Already Running", args[0]);
            }
        }

        private static void StartProcess(string[] args)
        {
            Process p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.FileName = Path.Combine(args[1], args[0]);
            try
            {
                p.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Rerunning {0}\n{1}", Path.Combine(args[1], args[0]), ex.Message);
            }
        }

        private static bool IsProcessOpen(string name)
        {
            foreach (Process clsProcess in Process.GetProcesses())
            {
                if (clsProcess.ProcessName.ToLower().Contains(name))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
