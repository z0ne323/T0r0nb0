using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace T0r0nb0
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = "ABSOLUTE_PATH_OF_YOUR_WORDLIST_FILE";
            string hashedPassword = "5f4dcc3b5aa765d61d8327deb882cf99"; // MD5 hash for "password"

            List<string> wordlist = new List<string>();

            try
            {
                // Use the LoadWordlistAsync function from Helpers module
                wordlist = Helpers.LoadWordlistAsync(filePath).Result;
                Console.WriteLine($"[+] Wordlist loaded successfully with {wordlist.Count} words.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[-] Error loading wordlist: {ex.Message}");
                return;
            }

            bool passwordFound = false;

            // Perform dictionary attack using the loaded wordlist
            foreach (string word in wordlist)
            {
                if (CrackPasswordWithDictionary(word, hashedPassword))
                {
                    Console.WriteLine($"[+] Password cracked: {word}");
                    passwordFound = true;
                    break; // Exit the loop once the password is found
                }
            }

            // If the loop completes and the password is not found
            if (!passwordFound)
            {
                Console.WriteLine("[-] Password not found in the wordlist.");
            }
        }

        static bool CrackPasswordWithDictionary(string word, string hashedPassword)
        {
            string hash = Helpers.ComputeMD5Hash(word);

            return hash == hashedPassword;
        }
    }
}
