using DefectStudio.IO;
using DefectStudio.Library;
using System;
using System.Collections.Generic;

namespace DefectStudio
{
    /// <summary>
    /// In Defect Studio, a command is the first word written on each line in the input script.
    /// All subsequent words are arguments.
    /// The command dictionary links these keywords to a function.
    /// </summary>
    public static class CommandDictionary
    {
        internal static Dictionary<string, dynamic> _dictionary = new Dictionary<string, dynamic>() {
            { "READ", new Action<string[]>(Command.ReadFile) },
            { "VACANCY_CLUSTER", new Action<string[]>(Command.GenerateVacancyCluster) }
        };
    }
}


