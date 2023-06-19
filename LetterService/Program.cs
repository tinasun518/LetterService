// See https://aka.ms/new-console-template for more information

public interface ILetterService 
{
    void CombineTwoLetters(string inputFile1, string inputFile2, string resultFile);

}

public class LetterService : ILetterService 
{

    public void CombineTwoLetters(string inputFile1, string inputFile2, string resultFile) 
    {
        Console.WriteLine("Hello, World!");

    }


}



