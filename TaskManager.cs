using System;

namespace DefectStudio
{
    public static class TaskManager
    {
        public static Queue<string> Tasks { get => tasks; set => tasks = value; }
        private static Queue<string> tasks = new Queue<string>();

        #region Work File
        public static List<string> WorkContents { get => workContents; }        

        private static List<string> workContents = [];
        #endregion

        /// <summary>
        /// Sets the contents of the current working file.
        /// </summary>
        /// <param name="contents"></param>
        public static void SetWorkContents(List<string> contents) {
            foreach (var item in contents) { workContents.Add(item); }
        }

        /// <summary>
        /// Adds a task to the queue.
        /// </summary>
        /// <param name="task"></param>
        public static void AddTask(string task) { 
            tasks.Enqueue(task);
            DebugMessage.New(DebugMessageType.Normal, "Added task: " + task);
        }
        
        /// <summary>
        /// Executes the next task in the queue.
        /// </summary>
        public static void ExecuteNextTask()
        {
            // Get the next task in the queue.
            string[] task = Tasks.Dequeue().Split(" ");

            string command = task[0];
            string[] args;
            if (task.Length > 1)
                args = task.Skip(1).ToArray();
            else
                args = new string[0]; // Initialize as an empty array

            DebugMessage.New(DebugMessageType.Normal, $"> Executing task: " +
                $"\n\t\t\t\tCommand:\t{command} " +
                $"\n\t\t\t\tArguments:\t{string.Join(", ", args)}");
            CommandDictionary._dictionary[command].DynamicInvoke(new object[] { args });
        }
    }
}

