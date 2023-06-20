using System;
using System.IO;
using System.Linq;

namespace LetterService
{
    interface ILetterService
    {
        void CombineTwoLetters(string inputFile1, string inputFile2, string resultFile);
    }

    class LetterService : ILetterService
    {
        public void CombineTwoLetters(string inputFile1, string inputFile2, string resultFile)
        {
            string content1 = string.IsNullOrEmpty(inputFile1) ? "" : File.ReadAllText(inputFile1);
            string content2 = string.IsNullOrEmpty(inputFile2) ? "" : File.ReadAllText(inputFile2);
            string combinedContent = content1 + Environment.NewLine + content2;
            File.WriteAllText(resultFile, combinedContent);
        }
    }

    class Program
    {
        //Set default values
        static string rootFolder = "CombinedLetters";
        static string inputFolder = Path.Combine(rootFolder, "Input");
        static string archiveFolder = Path.Combine(rootFolder, "Archive");
        static string outputFolder = Path.Combine(rootFolder, "Output");
        static string processDate = "20220125"; //DateTime.Now.ToString("yyyyMMdd");

        static void Main(string[] args)
        {
            if (!ParseParameters(args))
            {
                Console.WriteLine("Some parameters are invalid, please check and retry.");
                PrintUsage();
            }
            ILetterService letterService = new LetterService();

            //Start execution.
            try
            {
                Console.WriteLine("Start Execution.");
                Excute(inputFolder, outputFolder, letterService, processDate, archiveFolder);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Encountered error: " + ex.Message);
                Console.WriteLine("Stack: " + ex.StackTrace);
            }
            finally
            {
                Console.WriteLine("Finished Execution.");
            }
        }

        static bool ParseParameters(string[] args)
        {
            if (args.Length % 2 != 0)
            {
                Console.WriteLine("Invalid number of parameters.");
                return false;
            }

            for (int i = 0; i < args.Length; i += 2)
            {
                string option = args[i].ToLower();
                string value = args[i + 1];

                switch (option)
                {
                    case "-i":
                        if (!Directory.Exists(value)) { return false; }
                        inputFolder = value;
                        break;
                    case "-o":
                        if (!Directory.Exists(value)) { return false; }
                        outputFolder = value;
                        break;
                    case "-d":
                        if (!DateTime.TryParseExact(value, "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out _))
                        {
                            Console.WriteLine("Invalid date format. Expected format: yyyyMMdd");
                            return false;
                        }
                        break;
                    case "-r":
                        if (!Directory.Exists(value)) { return false; }
                        rootFolder = value;
                        break;
                    case "-a":
                        if (!Directory.Exists(value)) { return false; }
                        archiveFolder = value;
                        break;
                    default:
                        return false;
                }
            }
            return true;
        }

        static void PrintUsage()
        {
            Console.WriteLine("Usage: LetterService.exe -i <input path> -o <output path> -d <date> -r <root folder path> -a <archive path>");
            Console.WriteLine("-i <input path>      : Path to the input folder, will overwrite root folder's input path.");
            Console.WriteLine("-o <output path>     : Path to the output folder, will overwrite root folder's output path.");
            Console.WriteLine("-d <date>            : Date in yyyyMMdd format. Default to today if not provided.");
            Console.WriteLine("-r <root folder path>: Path to the root folder. Default to c:\\temp if not provided.");
            Console.WriteLine("-a <archive path>    : Path to the archive folder, will overwrite root folder's archive path.");
        }

        static void Excute(string sourceFolder, string outputFolder, ILetterService letterService, string processDate, string archiveFolder)
        {
            string admissionFolder = Path.Combine(sourceFolder, "Admission", processDate);
            string scholarshipFolder = Path.Combine(sourceFolder, "Scholarship", processDate);
            var letterFiles = GetAllFilesToBeProcessed(new List<string> { admissionFolder, scholarshipFolder });
            // Process admission letters
            ProcessLetters(letterFiles, outputFolder, letterService, archiveFolder);
        }

        static List<string> GetAllFilesToBeProcessed(List<string> filePaths)
        {
            List<string> result = new List<string>();
            foreach (string filePath in filePaths)
            {
                string[] files = Directory.GetFiles(filePath, "*.txt");
                foreach (string file in files)
                {
                    result.Add(file);
                }
            }
            return result;
        }

        static void ProcessLetters(List<string> filesToProcess, string outputFolder, ILetterService letterService, string archiveFolder)
        {
            var groupedLetters = GroupLettersByStudentId(filesToProcess);

            foreach (string studentId in groupedLetters.Keys)
            {
                List<string> letterFilesForStudent = groupedLetters[studentId];
                string letterPathForCombine = Path.Combine(outputFolder, $"combined-{studentId}.txt");
                CombineMultipleLetters(letterFilesForStudent, letterPathForCombine, letterService);
                ArchiveFiles(letterFilesForStudent, archiveFolder);
            }
        }

        static void ArchiveFiles(List<string> filesToMove, string destination)
        {
            foreach (string file in filesToMove)
            {
                Directory.Move(file, Path.Combine(destination, Path.GetFileName(file)));
            }
        }

        /*
         * This will run through all files provided and group the file pathes by student id.
         */
        static Dictionary<string, List<string>> GroupLettersByStudentId(List<string> letterFiles)
        {
            var combinedLetters = new Dictionary<string, List<string>>();
            foreach (string letterFile in letterFiles)
            {
                string universityID = Path.GetFileNameWithoutExtension(letterFile).Split('-')[1];

                if (!combinedLetters.ContainsKey(universityID))
                {
                    combinedLetters[universityID] = new List<string>();
                }

                combinedLetters[universityID].Add(letterFile);
            }
            return combinedLetters;
        }

        static void CombineMultipleLetters(List<string> letterFiles, string combinedLetterPath, ILetterService letterService)
        {
            // Start with the first letter file
            string firstLetterFile = letterFiles[0];
            if (!Path.Exists(combinedLetterPath)) { File.Copy(firstLetterFile, combinedLetterPath); }

            // Combine subsequent letter files
            for (int i = 1; i < letterFiles.Count; i++)
            {
                string currentLetterFile = letterFiles[i];
                letterService.CombineTwoLetters(combinedLetterPath, currentLetterFile, combinedLetterPath);
            }
        }
    }
}
