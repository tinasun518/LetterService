// See https://aka.ms/new-console-template for more information

using System.Xml.Serialization;

public interface ILetterService 
{
    void CombineTwoLetters(string inputFile1, string inputFile2, string resultFile);

}

public class LetterService : ILetterService 
{

    public void CombineTwoLetters(string inputFile1, string inputFile2, string resultFile) 
    {
        string filePath = "path/to/your/newfile.txt";
        string fileContents = "This is the content to write into the file.";
        File.WriteAllText(filePath, fileContents);
    }



    public static string FindCombinedLetters(string studentId) 
    {
        string filePath = string.Format("../{0}");
        File.CreateText(filePath).Close();
        File.WriteAllText(filePath, "test");
        return filePath;
    }



     static void Main(string[] args)
    {
        Console.WriteLine("Enter your name:");
        string name = Console.ReadLine();
        string test = FindCombinedLetters(DateTime.Now.ToString("yyyyMMdd"));
        Console.WriteLine($"Hello, {name}! Welcome to your console app.");
        Console.ReadLine();
    }
}





