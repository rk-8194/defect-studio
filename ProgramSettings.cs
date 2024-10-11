using System;
namespace DefectStudio
{
    public static class ProgramSettings
    {
        #region Program Modes
        /// <summary>
        /// Debug mode prints verbose details to the console.
        /// </summary>
        public static bool isDebugMode = false;

        /// <summary>
        /// Dry run mode does not perform cell manipulations but instead creates 'dummy' files to test
        /// input/ouput.
        /// </summary>
        public static bool isDryMode = false;
        #endregion
    }
}

