using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GwsUtils
{
    public class DebugUtils
    {
        private static StreamWriter _writer = null;
        private static string _outputFilePath;
        private static string _outputFolder;


        public static void Cleanup()
        {
            if (_writer != null)
                _writer.Dispose();
        }

        public static void Write(string outputString, bool addDateTimePrefix = true, bool addCurrentMethodName = false, bool addCallingMethod = false, bool outputStacktrace = false)
        {
            if (string.IsNullOrEmpty(_outputFilePath))
                CreateOutputFile();

            if (_writer == null)
                return;

            StringBuilder stringBuilder = new StringBuilder();


            string prefix = "";
            if (addDateTimePrefix)
            {
                prefix = $"{DateTime.Now:HH:mm:ss.fff} - GWS - ";
                stringBuilder.Append(prefix);
            }

            if (addCurrentMethodName)
            {
                string currentMethodName = new StackTrace().GetFrame(1).GetMethod().Name;
                stringBuilder.Append($"ExecutingMethod={currentMethodName} - ");
            }

            if (addCallingMethod)
            {
                string callingMethodName = new StackTrace().GetFrame(2).GetMethod().Name;
                stringBuilder.Append($"CallingMethod={callingMethodName} - ");
            }



            stringBuilder.Append(outputString);

            if (outputStacktrace)
            {
                stringBuilder.Append($"- STACKTRACE: {new StackTrace()}");
            }

            _writer.WriteLine(stringBuilder.ToString());
        }

        public static void CreateOutputFile(bool addDateTimeTagToFileName = false)
        {
            string baseFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Cadwell", "Temp");
            if (!Directory.Exists(baseFolder))
            {
                Directory.CreateDirectory(baseFolder);
            }

            string fileTag = "";
            if (addDateTimeTagToFileName)
                fileTag = $"_{DateTime.Now:HHmmss-fff}";
            string fileName = $"DebugOutput{fileTag}.txt";

            string outputFilePath = Path.Combine(baseFolder, fileName);
            CreateOutputFile(outputFilePath);
        }

        public static void CreateOutputFile(string outputFilePath)
        {
            if (_writer != null)
            {
                _writer.Close();
                _writer.Dispose();
            }

            _writer = new StreamWriter(outputFilePath);
            _writer.AutoFlush = true;

            _outputFilePath = outputFilePath;
        }

    }
}
