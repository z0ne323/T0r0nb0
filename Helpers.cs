using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace T0r0nb0
{
    public static class Helpers
    {
        public static bool FileExists(string path)
        {
            return File.Exists(path);
        }

        public static async Task<List<string>> LoadWordlistAsync(string filePath)
        {
            try
            {
                // Use StreamReader to efficiently read the wordlist file
                using (StreamReader reader = new StreamReader(filePath))
                {
                    List<string> wordlist = new List<string>();
                    string line;

                    while ((line = await reader.ReadLineAsync()) != null)
                    {
                        // Null-check after assignment
                        if (!string.IsNullOrWhiteSpace(line))
                        {
                            wordlist.Add(line.Trim());
                        }
                    }

                    return wordlist;
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"[!] Wordlist file not found: {ex.Message}");
                throw; // Rethrow the exception to terminate the program
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[-] Error reading wordlist file: {ex.Message}");
                return new List<string>(); // Return an empty list for other exceptions
            }
        }

        public static string ComputeMD5Hash(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }

                return sb.ToString();
            }
        }
    }
}
