using System.IO;
namespace Compiler.Model
{
    public class DevelopedFunctions
    {
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
            this function takes an array of characters 
            and copies it into a new array of character and
            add to that array additional place for any new insertion

            argument must be initalized with non negative length
        */
        public static char[] copyCharAndAdd1(char []oldArray)
        {
            char []newArray = new char[oldArray.Length+1];
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


        // testing templates in c#
        public static T[] copyAndAdd1<T>(T[] oldArray)
        {
            T[] newArray = new T[oldArray.Length + 1];
            for(int i = 0; i < oldArray.Length; i++)
            {
                newArray[i] = oldArray[i];
            }
            return newArray;
        }
    }
}