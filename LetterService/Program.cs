using System;
using System.IO;
using System.Linq;

namespace ConsoleApp
{
    interface ILetterService
    {
        void CombineTwoLetters(string inputFile1, string inputFile2, string resultFile);
    }

    class LetterService : ILetterService
    {
        public void CombineTwoLetters(string inputFile1, string inputFile2, string resultFile)
        {
            // Combine the contents of inputFile1 and inputFile2 into resultFile
            string content1 = File.ReadAllText(inputFile1);
            string content2 = string.Empty;

            if (!string.IsNullOrEmpty(inputFile2))
            {
                content2 = File.ReadAllText(inputFile2);
            }

            string combinedContent = content1 + Environment.NewLine + content2;
            File.WriteAllText(resultFile, combinedContent);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // ...
            // Define folder paths and other variables

            // Create an instance of the LetterService
            ILetterService letterService = new LetterService();

            string rootFolder = "C:\\Users\\kelma\\source\\repos\\LetterService\\CombinedLetters";
            string inputFolder = Path.Combine(rootFolder, "Input");
            string archiveFolder = Path.Combine(rootFolder, "Archive");
            string outputFolder = Path.Combine(rootFolder, "Output");

            // Create dated folder path based on the current date
            string currentDateFolder = Path.Combine(inputFolder, ""); // DateTime.Today.ToString("yyyyMMdd")



            // Call the ProcessLetters method
            ProcessLetters(currentDateFolder, outputFolder, letterService);

            // ...
            // Rest of the code
        }

        static void ProcessLetters(string sourceFolder, string outputFolder, ILetterService letterService)
        {
            string admissionFolder = Path.Combine(sourceFolder, "Admission");
            string scholarshipFolder = Path.Combine(sourceFolder, "Scholarship");
            string processDate = "20220125";
            // Process admission letters
            ProcessLettersInFolder(admissionFolder, outputFolder, processDate, letterService);

            // Process scholarship letters
            ProcessLettersInFolder(scholarshipFolder, outputFolder, processDate, letterService);
        }

        static void ProcessLettersInFolder(string folderPath, string outputFolder, string targetDate, ILetterService letterService)
        {
            if (Directory.Exists(folderPath))
            {
                string targetFolderPath = Path.Combine(folderPath, targetDate);

                if (Directory.Exists(targetFolderPath))
                {
                    var combinedLetters = new Dictionary<string, List<string>>();

                    string[] letterFiles = Directory.GetFiles(targetFolderPath, "*.txt");

                    foreach (string letterFile in letterFiles)
                    {
                        string universityID = Path.GetFileNameWithoutExtension(letterFile).Substring(10);

                        if (!combinedLetters.ContainsKey(universityID))
                        {
                            combinedLetters[universityID] = new List<string>();
                        }

                        combinedLetters[universityID].Add(letterFile);
                    }

                    foreach (var kvp in combinedLetters)
                    {
                        string universityID = kvp.Key;
                        List<string> letterFilesForUniversity = kvp.Value;

                        string combinedLetterPath = Path.Combine(outputFolder, $"combined-{universityID}.txt");

                        if (letterFilesForUniversity.Count == 1)
                        {
                            // Only one letter file, no need to combine
                            File.Copy(letterFilesForUniversity[0], combinedLetterPath);
                        }
                        else
                        {
                            // Combine multiple letter files
                            CombineMultipleLetters(letterFilesForUniversity, combinedLetterPath, letterService);
                        }
                    }
                }
            }
        }

        static void CombineMultipleLetters(List<string> letterFiles, string combinedLetterPath, ILetterService letterService)
        {
            // Sort the letter files to ensure consistent ordering
            letterFiles.Sort();

            // Start with the first letter file
            string firstLetterFile = letterFiles[0];
            File.Copy(firstLetterFile, combinedLetterPath);

            // Combine subsequent letter files
            for (int i = 1; i < letterFiles.Count; i++)
            {
                string currentLetterFile = letterFiles[i];
                letterService.CombineTwoLetters(combinedLetterPath, currentLetterFile, combinedLetterPath);
            }
        
    }
}

    // ...
    // Rest of the code
}
