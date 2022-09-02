// See https://aka.ms/new-console-template for more information

namespace CodeTest
{
    class Program
    {
        /// <summary> exit key for user to enter to close app </summary>
        public static string EXITKEY = "exit";

        static void Main(string[] args)
        {
            var _palindromeObject = new Palindrome();

            while(true)
            {
                // get user input, and check if they typed the exit key
                if(_palindromeObject.GetParagraph().IsExitKey())
                    return;

                // find the number of palindromes in both the word, and sentence lists
                var wordPalindromes = _palindromeObject.FindNumberOfWordPalindromes();
                var sentencePalindromes = _palindromeObject.FindNumberOfSentencePalindromes();
                var uniqueWordsCountList = _palindromeObject.GetUniqueWordCount();

                // print numter of palindromes in words and sentences. Also print number of distinct words
                Console.WriteLine($"{Environment.NewLine}Word Palindromes: {wordPalindromes}");
                Console.WriteLine($"Sentence Palindromes: {sentencePalindromes}");
                Console.WriteLine($"Distinct Words: {uniqueWordsCountList?.Count ?? 0}");

                // get the number of unique words, and print them as a list
                if(uniqueWordsCountList is not null && uniqueWordsCountList.Count() > 0)
                {
                    Console.WriteLine($"{Environment.NewLine}Distinct Words:");
                    foreach(var word in uniqueWordsCountList)
                        Console.WriteLine($"\t{word.Item1}: {word.Item2}");
                }

                // get a letter from user to check against list of words. 
                Console.Write($"{Environment.NewLine}Enter a letter to filter words by: ");
                var inputChar = Console.ReadLine()?.ToLower().ToCharArray()[0];
                if(inputChar is null)
                {
                    // if the given char is somehow invalid, just continue
                    Console.WriteLine("Invalid input");
                    PrintEmptyLines(3);
                    continue;
                }
                
                // check which words contain the letter given
                var filteredList = _palindromeObject.GetWordsContainingLetter(inputChar.Value);
                if(filteredList is not null && filteredList.Count() > 0)
                {
                    Console.WriteLine($"Words containing '{inputChar}':");
                    foreach(var word in filteredList)
                        Console.WriteLine($"\t{word}");
                }
                else
                {
                    // if no words contained the letter, give message
                    Console.WriteLine($"No words containing: {inputChar}");
                }

                // add spacing for next loop
                PrintEmptyLines(3);
            }
        }

        /// <summary> print a number of empty lines </summary>
        public static void PrintEmptyLines(int numLines) =>
            Console.WriteLine("".PadLeft(numLines, '\n'));
    }

    /// <summary> class for handling Palindrome work (getting input, breaking into lists, counts, etc.)
    public class Palindrome
    {
        /// <summary> user input for exiting program </summary>
        private static string EXITKEY = "exit";

        /// <summary> user input string </summary>
        private string? _userInput;

        public Palindrome()
        {
            // empty list to start
            _userInput = "";
        }

        /// <summary> get input from the user </summary>
        public Palindrome GetParagraph()
        {
            Console.Write("Enter a paragraph, or EXIT to quit: ");
            var userInput = Console.ReadLine() ?? "";
            _userInput = userInput.ToLower().Trim();
            return this;
        }

        /// <summary> check if the exit key is entered </summary>
        public bool IsExitKey() => _userInput?.Equals(EXITKEY) ?? false;

        /// <summary> get a list of words from the paragraph </summary>
        public List<string>? GetWordsList(string? paragraph) => GetSplitList('.', ' ');

        /// <summary> get a list of sentences from the paragraph </summary>
        public List<string>? GetSentencesList(string? paragraph) => GetSplitList(' ', '.');

        /// <summary> remove specified char, and split paragragh into list using given split char </summary>
        private List<string>? GetSplitList(char removeChar, char splitChar)
        {
            if (string.IsNullOrEmpty(_userInput)) return null;

            var returnList = _userInput.Replace(removeChar.ToString(), "")
                .Split(splitChar, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            return returnList.ToList();
        }

        /// <summary> Find Palindromes in the words List </summary>
        public int FindNumberOfWordPalindromes() => FindNumberOfPalendromes(GetWordsList(_userInput));

        /// <summary> Find palindromes in the sentences List </summary>
        public int FindNumberOfSentencePalindromes() => FindNumberOfPalendromes(GetSentencesList(_userInput));

        /// <summary> check how many palindromes exit in the list </summary>
        private int FindNumberOfPalendromes(List<string>? strings)
        {
            var palindromes = strings?.Where(word => word.Equals(word.Reverse())).ToList();
            return palindromes?.Count() ?? 0;
        }

        /// <summary> get a list of unique words, and how many times they appear in the paragraph </summary>
        public List<(string, int)>? GetUniqueWordCount()
        {
            var words = GetWordsList(_userInput);
            if (words is null) return null;

            var distinctCountList = new List<(string, int)>();
            foreach (var word in words.Distinct())
                distinctCountList.Add((word, words.Count(checkWord => checkWord.Equals(word))));

            return distinctCountList;
        }

        /// <summary> get a list of words containing the specified letter </summary>
        public List<string>? GetWordsContainingLetter(char letter) =>
            GetWordsList(_userInput)?.Where(word => word.Contains(letter)).ToList() ?? null;
    }

    /// <summary> extension class for reversing a string
    public static class StringExtension
    {
        public static string Reverse(this string str)
        {
            var chars = str.ToCharArray();
            Array.Reverse(chars);
            return new String(chars);
        }
    }
}