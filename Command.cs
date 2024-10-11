using DefectStudio.IO;
using System;
using System.IO;

namespace DefectStudio.Library 
{
    public static partial class Command 
    {
        public static void RunTasks()
        {
            DebugMessage.New(DebugMessageType.Normal, "Running tasks...");

            if (File.Exists("TASKS"))
            {
                string[] tasks = File.ReadAllLines("TASKS");
                foreach (string task in tasks) { TaskManager.AddTask(task); }

                TaskManager.ExecuteNextTask();
            }
            else DebugMessage.New(DebugMessageType.Error, "No task file was found. Cannot run Defect Studio.");
        }

        public static void ReadFile(string[] args)
        {
            if (File.Exists(args[0].Trim())) {
                DebugMessage.New(DebugMessageType.Normal, "Reading file: " + args[0]);
                FileReader.Read(args[0].Trim());
            }
            else {
                DebugMessage.New(DebugMessageType.Error, "File not found.");
            }
        }
    }
}


