using System.IO;
using System;
namespace Compiler.Model
{
    public class DevelopedFunctions
    {
        public static Token[] tokens;
        /*
            this function split a string into multiple substring
            based on the seperator

            pre: string s must be initalized and not empty
                 string seperator must be initalized and not empty

            return array of strings
        */
        public static string [] splitString(string s, string seperator)
        {
            string []splittedStrings = new string[0];
            int splitIndex = 0; // indexer for the splittedStrings array

            char []charBuffer = new char[0];
            int charIndex = 0; // index for the charBuffer array

            // scan every character and compare it with 
            // the seperator, if the seperator was found
            // then push the scanned characters to the
            // splittedStrings array and clear the
            // charBuffer
            for(int i = 0; i < s.Length; i++)
            {
                int count = 0;
                for(int f = 0; f < seperator.Length; f++)
                {
                    int tmpIndex = i;
                    if(f < s.Length && s[tmpIndex] == seperator[f])
                    {
                        tmpIndex++;
                        count++;
                    }
                }
                if(count == seperator.Length && charBuffer.Length > 0)
                {
                    splittedStrings = copyAndAdd1<string>(splittedStrings);
                    splittedStrings[splitIndex] = new string(charBuffer);
                    charBuffer = new char[0];
                    charIndex = 0;
                    splitIndex++;
                    i += seperator.Length-1;
                }
                else
                {
                    charBuffer = copyAndAdd1<char>(charBuffer);
                    charBuffer[charIndex] = s[i];
                    charIndex++;
                }
            }
            // if the charBuffer not empty 
            // then push what's inside it in splittedStrings
            if(charBuffer.Length > 0)
            {
                splittedStrings = copyAndAdd1<string>(splittedStrings);
                splittedStrings[splitIndex] = new string(charBuffer);
            }

            // return the splitted string;
            return splittedStrings;

        }
	/*
            This function checks if a string is valid 
            to be a mathod or variable name
            
            returns true if the string is valid name
            and false otherwise
            
            string id must be initalized
        */
	public static bool isValidIdentifier(string id)
	{
	    int count = 0;
	    for(int i = 0; i < id.Length; i++)
	    {
		if(i == 0 && isDigit(id[i]))
		{
		    return false;
		}
		for(int f = 0; f < 26; f++)
		{
		    if(id[i] == 'a' + f || id[i] == 'A' + f || id[i] == '_' || isDigit(id[i]))
		    {
			count++;
			break;
		    }
		}
	    }
	    if(count == id.Length)
		return true;
	    else
		return false;
	}
        /*
            This function returns a substring from a certain index(startIndex)
            with a certain length
            string s must be initalized and not empty
            startIndex must be initalized and less than string length
            length must be initalized and less than string s length

         */
        public static string subString(string s, int startIndex, int length)
        {
            char[] charBuffer = new char[0];
            int bufferIndex = 0;
            int index = startIndex;
            while (index < s.Length && index < startIndex+length)
            {
                charBuffer = copyAndAdd1<char>(charBuffer);
                charBuffer[bufferIndex] = s[index];
                index++;
                bufferIndex++;
            }
            string newSubString = "";
            for(index = 0; index < charBuffer.Length; index++)
            {
                newSubString += charBuffer[index].ToString();
            }
            return newSubString;
        }
        /*
            this function takes an array of strings 
            and copies it into a new array of strings and
            add to that array additional place for any new insertion

            oldArray must be initalized with non negative length
        */
        public static string[] copyStringAndAdd1(string []oldArray)
        {
            string []newArray = new string[oldArray.Length+1];
            for(int i = 0; i < oldArray.Length; i++)
            {
                newArray[i] = oldArray[i];
            }
            return newArray;
        }
        /*
            this function loads the keywords from a text file
            to an array of tokens
            
            the filePath must be initalized non empty string and
            it is prefered to be absolute path
        */
        public static Token [] loadKeywords(string filePath)
        {
            Token[] keywords = new Token[0];

            StreamReader reader = new StreamReader(filePath);
            int index = 0;
            while (true)
            {
                // read next line
                string line = reader.ReadLine();

                // if that line is null, we break from the loop
                // because we reached the end of the file
                if (line == null)
                    break;
                // split the line 
                string[] splittedLine = splitString(line, "\t");

                // create an instance of token class
                // and push it back in the array
                // by creating a new array with the same values
                // and adding aditional place to that new array to
                // push back the new token
                Token t = new Token(splittedLine[0], splittedLine[1]);
                keywords = copyAndAdd1<Token>(keywords);
                keywords[index] = t;
                index++;
            }
            // close the stream reader
            reader.Close();

            // return loaded keywords
            return keywords;
        }
        /*
            this function takes an array of a certain class
            and copies it into a new array of that class and
            add to that array additional place for any new insertion

            argument must be initalized with non negative length
        */
        public static T[] copyAndAdd1<T>(T[] oldArray)
        {
            T[] newArray = new T[oldArray.Length + 1];
            for(int i = 0; i < oldArray.Length; i++)
            {
                newArray[i] = oldArray[i];
            }
            return newArray;
        }
        /*
            this function splits a string by a collection of delimiters
            every delimiter must be one character
            
            returns every splitted string and the seperator after it
            in an array of strings
            
            string s must be initalized and not empty
            char[] chars must be initalized with seperators
        */
        public static string[] splitUsingArray(string s, char []chars)
        {
            string[] splittedStrings = new string[0];
            int splittedIndex = 0;
            char[] charBuffer = new char[0];
            int bufferIndex = 0;
            for(int i = 0; i < s.Length; i++)
            {
                bool foundSeperator = false;
                for(int f = 0; f < chars.Length; f++)
                {
                    if(s[i] == chars[f])
                    {
                        foundSeperator = true;

                        splittedStrings = copyAndAdd1<string>(splittedStrings);
                        splittedStrings[splittedIndex] = new string(charBuffer);
                        splittedIndex++;

                        charBuffer = new char[0];
                        bufferIndex = 0;

                        splittedStrings = copyAndAdd1<string>(splittedStrings);
                        splittedStrings[splittedIndex] = chars[f].ToString();
                        splittedIndex++;
                        break;
                    }
                }
                if(!foundSeperator)
                {
                    charBuffer = copyAndAdd1<char>(charBuffer);
                    charBuffer[bufferIndex] = s[i];
                    bufferIndex++;
                }
            }
            if(charBuffer.Length > 0)
            {
                splittedStrings = copyAndAdd1<string>(splittedStrings);
                splittedStrings[splittedIndex] = new string(charBuffer);
                splittedIndex++;
            }
            return splittedStrings;
        }
        /*
            This functions tells if a string
            only consist of digits or digits and digits after a dot

            return true if a string only contains digits or digits
            and only one dot and after it also digits
            return false otherwise

            string s must be initialized and not empty
	    */
        public static bool isNumber(string s)
        {
            int count = 0;
            bool foundDot = false;
            for(int i = 0; i < s.Length; i++)
            {
                if(isDigit(s[i]))
                {
                    count++;
                }
                else if(s[i] == '.' && i+1 < s.Length && isDigit(s[i+1]) && foundDot == false)
                {
                    foundDot = true;
                    count++;
                }
                else if(s[i] == '.' && foundDot == true)
                    return false;
                else
                    return false;
            }
            if(count == s.Length)
            {
               return true;
            }
            else
                return false;
        }

        /*
            check if a character is a digit
            returns true if the character is digit
            and false otherwise
        */
        private static bool isDigit(char c)
        {
            for(int i = 0; i < 10; i++)
            {
                if(c == '0' + i)
                {
                    return true;
                }
            }
            return false;
        }
        /*
            this function takes a lexeme and returns
            what's it equivalent in English language

            string lexeme must be initalized
        */
        public static string getReturnToken(string lexeme)
        {
            if(tokens == null)
            {
                tokens = loadKeywords("../../keywords.txt");
            }
            for(int i = 0; i < tokens.Length; i++)
            {
                if(tokens[i].getLexeme() == lexeme)
                {
                    return tokens[i].getReturnToken();
                }
            }
            return null;
        }
    }
}
