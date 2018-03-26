using System;

class Program
{
    static void Main(string[] args)
    {
        ReadFromFile read = new ReadFromFile();
        read.hola();
    }
}
class ReadFromFile
{
    public void hola()
    {
        // Example #2
        // Read each line of the file into a string array. Each element
        // of the array is one line of the file.
        string[] lines = System.IO.File.ReadAllLines(@"C:\Users\camil\OneDrive\Documents\TEC\Análisis\ElPoeta\poems.txt");

        // Display the file contents by using a foreach loop.
        System.Console.WriteLine("Contents of WriteLines2.txt = ");
        foreach (string line in lines)
        {
            // Use a tab to indent each line of the file.
            string[] palabras = line.Trim().Split(new[] { "at the it a an" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string palabra in palabras)
            {
                string[] a = palabra.Split(' ');
                foreach (string has in a)
                {
                    Console.WriteLine("\t" + has);

                }
            }
        }
        // Keep the console window open in debug mode.
        Console.WriteLine("Press any key to exit.");
        System.Console.ReadKey();
    }
}
