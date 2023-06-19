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
            string content2 = File.ReadAllText(inputFile2);
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

            string rootFolder = "C:\\Users\\kelma\\source\\repos\\CombinedLetters";
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
                    string[] letterFiles = Directory.GetFiles(targetFolderPath, "*.txt");

                    foreach (string letterFile in letterFiles)
                    {
                        string universityID = Path.GetFileNameWithoutExtension(letterFile).Substring(10);
                        string combinedLetterPath = Path.Combine(outputFolder, $"combined-{universityID}.txt");

                        // Check if any existing combined letter for the same university ID exists
                        if (File.Exists(combinedLetterPath))
                        {
                            // Read the existing combined letter content
                            string existingCombinedContent = File.ReadAllText(combinedLetterPath);

                            // Read the current letter content
                            string currentLetterContent = File.ReadAllText(letterFile);

                            // Combine the current letter content with the existing combined letter content
                            string combinedContent = existingCombinedContent + Environment.NewLine + currentLetterContent;

                            // Update the combined letter file with the new content
                            File.WriteAllText(combinedLetterPath, combinedContent);
                        }
                        else
                        {
                            // No existing combined letter, create a new one
                            letterService.CombineTwoLetters(letterFile, string.Empty, combinedLetterPath);
                        }

                    }

                }
            }
        }
    }

    // ...
    // Rest of the code
}
