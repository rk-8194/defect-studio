using System;

namespace DefectStudio
{
    public enum DebugMessageType { Normal, Warning, Error }

    public static class DebugMessage
    {
        public static void New(DebugMessageType debugMessageType, string message)
        {
            switch (debugMessageType)
            {
                case (DebugMessageType.Warning):
                    Console.WriteLine("|!|\tWarning!\t\t" + message); break;
                case (DebugMessageType.Error):
                    Console.WriteLine("!!!\tERROR!\t\t" + message); break;
                default:
                    Console.WriteLine("\t\t\t" + message); break;
            }
        }
    }
}

