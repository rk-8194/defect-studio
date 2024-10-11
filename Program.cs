using System;
using DefectStudio.Library;

namespace DefectStudio
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Has debug mode been enabled?
            if(args.Contains<string>("-debug")) ProgramSettings.isDebugMode = true;

            // Has dry run mode been enabled?
            if(args.Contains("-dryrun")) ProgramSettings.isDryMode = true;

            Command.RunTasks();
        }
    }
}