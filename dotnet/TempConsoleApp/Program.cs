using System.Security.Cryptography;
using System.Text;

internal class Program
{
    /// <summary>
    /// Entry point of the application that computes an MD5 hash of user input with optional salt.
    /// </summary>
    /// <param name="args">Command-line arguments (not used in this application).</param>
    /// <remarks>
    /// This method prompts the user to enter text and an optional salt value, then computes and displays
    /// the MD5 hash of the combined input. If no input is provided, a message is displayed and the method returns.
    /// 
    /// The <see cref="ComputeHash"/> local function handles the actual hashing logic using MD5 algorithm.
    /// </remarks>
    private static void Main(string[] args)
    {
        Console.Write("Enter text to hash: ");
        string? input = Console.ReadLine();

        Console.Write("Enter salt value: ");
        string? salt = Console.ReadLine();

        if (input is null)
        {
            Console.WriteLine("No input provided.");
            return;
        }

        string hash = ComputeHash(input, salt);
        Console.WriteLine($"Hash: {hash}");

        /// <summary>
        /// Computes the MD5 hash of the given input combined with an optional salt.
        /// </summary>
        static string ComputeHash(string input, string? salt)
        {
            string combined = input + (salt ?? string.Empty);

            byte[] byts = Encoding.UTF8.GetBytes(combined);
            byte[] hash = MD5.HashData(byts);

            StringBuilder sb = new();
            foreach (byte b in hash)
            {
                sb.Append(b.ToString("x2"));
            }

            return sb.ToString();
        }
    }
}