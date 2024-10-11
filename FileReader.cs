using System;

namespace DefectStudio.IO
{
    public static class FileReader
    {
        internal static Dictionary<string, dynamic> format = new Dictionary<string, dynamic>() {
            { "XYZ", new Action(StructureManager.LoadXYZ) },
            { "VASP", new Action(StructureManager.LoadVASP) }
        };

        public static void Read(string path)
        {
            // Read the text file at the path relative to the current working directory
            // of defect studio.
            string[] contents = File.ReadAllLines(path);        // .. read
            TaskManager.SetWorkContents([.. contents]);         // .. set the current work file in TaskManager.

            if (path.Equals("POSCAR"))
            {
                format["VASP"].DynamicInvoke();
            }

            DebugMessage.New(DebugMessageType.Normal, "Successfully read file: " + path);
        }

        /// <summary>
        /// Gets the format of the file which has just been read. In most cases, this can easily
        /// be obtained from the file extension. In some special cases, such as VASP POSCAR files,
        /// a manual search of the file will be needed to determine the format.
        /// </summary>
        /// <param name="contents"></param>
        private static void GetUnknownFormat(string[] contents) { }
    }
}

