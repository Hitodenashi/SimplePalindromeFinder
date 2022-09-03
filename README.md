# SimplePalindromeFinder
simple console app to find palindrome words, and sentences in a paragraph given by the user.


## Purpose of program
The purpose of this program, is to take in text input from the user, and find any palindrome words, and sentences withing the given text.


## Implementation
This app is written as a console app, in C#, and uses LINQ functionality to filter lists of words and sentences.


### Program - Class
The main entry point class for the program. Contains the Main() entry point method.

This class first creates an instance of the Palindrome object. This object is then used to get an input from the user, and test for the exit key. If the exit key is given, then the program exits, otherwise, the program then proceeds to check for any palindromes using the functionality within the Palindrome object.

The number of word, and sentence, palindromes are stored first. Then, a list of unique words, and the word counts, is stored. Next, the number of each is then presented to the user.

Then, the app checks the list of unique words, testing for null, or empty, list. If this list of words is empty or null, then the step is skipped, otherwise, the app then presents a list of the words present in the paragraph, along with the number of times each word appears.

The next step, is to prompt the user for a letter. This letter will be used to filter the list of words to only the words that contain the given letter. The input is first tested to see if the user gave a valid letter, if the input is invalid, the user is given a message, and then the remaining iteration of the program is skipped.

If the user gave a valid letter to search, then the palindrome object is then used to test the list of words in the paragraph. This is done by calling the GetWordsContainingLetter on the palindrome object, with the given letter as the parameter. If a valid list of words is returned, then list of words is presented to the user. If an invalid, or empty, list is returned, then a message is given to the user.

Finally, the console is paused, and waiting for user input, before clearing the screen to continuing the next iteration.


### Palindrome - Class
Object that contains the functionality needed to take in user input, and create lists of words, and sentences, that can be used to search for any palindromes. This includes a private variable to hold the user input, as well as a private static variable to hold the text needed for the user to exit the program. 

GetParagraph() - Function that prompts the user for text input, and then performs a ToLower(), and Trim(), function to create an expected format for text to be processed. Finally, this returns the object instance itself, in order to chain an IsExitKey() function call off of.

IsExitKey() - Function that tests user input against the EXITKEY variable, in order to determine if the program should exit.

GetWordsList() / GetSentencesList() - These Functions call the GetSplitList() function, using differing parameters, in order to create a list of words, or sentences, to be used for testing if palindromes exist.

GetSplitList() - This Function First test if the user input is valid. If an invalid input is determined, then the function exits, returning null. If the user input is valid, the function replaces the removeChar parameter with an empty char, and then splits the resulting string by the given delimiter (splitChar parameter). Finally, this removes any whitespace, or empty entries, and returns the resulting list of words/sentences.

FindNumberOfWordPalindromes() / FindNumberOfSentencePalindromes() - Functions that call the FindNumberOfPalindromes() function, using either GetWordsList(), or GetSentencesList(), as the parameter.

FindNumberOfPalindromes() - This function Checks the given list of strings, for palindromes. This iterates though the list of strings, and compares each string to it's reversed counterpart. Finally, this returns the number of palindromes found in the list of strings.

GetUniqueWordCount() - This function returns a list of words, and the number of times that word appears in the user input. First, a list of words in the user input is obtained, then check for null. A distinct list of words is then checked, and a count for each word time the word appears is checked. The words, and number of times they appear, is then stored in a list, and returned. 

GetWordsContainingLetter() - This function is used to find any words that contain a specified letter. First, a list of words is checked, and each word is checked for whether they contain the given letter. Each word that contains the given letter, is then added to a list, and the resulting list of words, is returned.


### StringExtention - Static Class
StringExtension is a static class containing a string extention method for reversing a given string.

Reverse() - Extension method that takes the given string, and first converts it to a char array. The array is then reversed, and a new string instance is created using the reversed char array. Finally, the new string instance is created.