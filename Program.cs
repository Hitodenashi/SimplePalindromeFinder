// See https://aka.ms/new-console-template for more information

namespace CodeTest
{
    class Program
    {
        /// <summary> exit key for user to enter to close app </summary>
        public static string EXITKEY = "exit";

        static void Main(string[] args)
        {
            // variable for user input
            var input = "";
            while(true)
            {
                // get user input, and check if they typed the exit key
                input = GetParagraph();
                if(input.Equals(EXITKEY))
                    return;
                
                // separate the paragraph into a list of words, and sentences (with spaces, and periods removed)
                var wordsList = GetWordsList(input);
                var sentencesList = GetSentencesList(input);

                // check if either list is empty
                if(wordsList is null || sentencesList is null) continue;

                // find the number of palindromes in both the word, and sentence lists
                var wordPalindromes = FindNumberOfPalendromes(wordsList);
                var sentencePalindromes = FindNumberOfPalendromes(sentencesList);

                // print numter of palindromes in words and sentences. Also print number of distinct words
                Console.WriteLine($"{Environment.NewLine}Word Palindromes: {wordPalindromes}");
                Console.WriteLine($"Sentence Palindromes: {sentencePalindromes}");
                Console.WriteLine($"Distinct Words: {GetUniqueWordCount(wordsList)?.Count ?? 0}");

                // get the number of unique words, and print them as a list
                var uniqueWords = GetUniqueWordCount(wordsList);
                if(uniqueWords is not null && uniqueWords.Count() > 0)
                {
                    Console.WriteLine($"{Environment.NewLine}Distinct Words:");
                    foreach(var word in uniqueWords)
                        Console.WriteLine($"\t{word.Item1}: {word.Item2}");
                }

                // get a letter from user to check against list of words. 
                Console.Write($"{Environment.NewLine}Enter a letter to filter words by: ");
                var inputChar = Console.ReadLine()?.ToCharArray()[0];
                if(inputChar is null)
                {
                    // if the given char is somehow invalid, just continue
                    Console.WriteLine("Invalid input");
                    PrintEmptyLines(3);
                    continue;
                }
                
                // check which words contain the letter given
                var filteredList = GetWordsContainingLetter(wordsList, inputChar.Value);
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

        /// <summary> get input from the user </summary>
        public static string GetParagraph()
        {
            Console.Write("Enter a paragraph, or EXIT to quit: ");
            var userInput = Console.ReadLine() ?? "";
            //var charList = Array.FindAll<char>(userInput.ToCharArray(), letter => char.IsLetter(letter));
            return userInput.ToLower().Trim();
        }

        /// <summary> get a list of words from the paragraph </summary>
        public static List<string>? GetWordsList(string paragraph) => GetSplitList(paragraph, '.', ' ');

        /// <summary> get a list of sentences from the paragraph </summary>
        public static List<string>? GetSentencesList(string paragraph) => GetSplitList(paragraph, ' ', '.');

        /// <summary> remove specified char, and split paragragh into list using given split char </summary>
        public static List<string>? GetSplitList(string paragraph, char removeChar, char splitChar)
        {
            if (string.IsNullOrEmpty(paragraph)) return null;

            var returnList = paragraph.ToLower().Replace(removeChar.ToString(), "").Split(splitChar).ToList();
            returnList = returnList.Where(word => !string.IsNullOrEmpty(word)).ToList();
            return returnList;
        }

        /// <summary> check how many palindromes exit in the list </summary>
        public static int FindNumberOfPalendromes(List<string>? strings)
        {
            var palindromes = strings?.Where(word => word.Equals(word.Reverse())).ToList();
            return palindromes?.Count() ?? 0;
        }

        /// <summary> get a list of unique words, and how many times they appear in the paragraph </summary>
        public static List<(string, int)>? GetUniqueWordCount(List<string>? words)
        {
            if (words is null) return null;

            var distinctCountList = new List<(string, int)>();
            foreach (var word in words.Distinct())
                distinctCountList.Add((word, words.Count(checkWord => checkWord.Equals(word))));

            return distinctCountList;
        }

        /// <summary> get a list of words containing the specified letter </summary>
        public static List<string>? GetWordsContainingLetter(List<string> wordList, char letter) =>
            wordList.Where(word => word.Contains(letter)).ToList();
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