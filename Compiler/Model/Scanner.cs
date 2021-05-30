using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Model
{
    class Scanner
    {
        private char[] splitters = { '#', '^' ,'=','\n',' '
                                    ,'@','$','+','-','*','/'
                                    ,'%','&','|','~','=','<'
                                    ,'>','!','-','{','}','['
                                    ,']','/','(',')',',','\t','\'','\"'};



        public DS.Queue<ScannerModel> queue = new DS.Queue<ScannerModel>(new ScannerModel());
        ArrayList arlist = new ArrayList();
        

        //if found comment don't read and set it true
        private bool foundComment = false;
        private bool multiLineComment = false;

        /*
         To Make Array list 
        1- first use prepareList
        2- second make loop and used addToList(Enter your woed here)
        3- you will find your array in this.myArray
         */
        private int counter;
        private string[] myArray;
        private void prepareList()
        {
            this.myArray = new string[0];
            this.counter = 0;
        }
        private void addToList(String addString)
        {
            this.myArray = DevelopedFunctions.copyAndAdd1<string>(this.myArray);
            this.myArray[this.counter] = addString;
            this.counter++;

            
        }
        /******************************************/

        public void setProgram(String Program)
        {
            string[] splitProgram= DevelopedFunctions.splitUsingArray(Program, splitters);
            this.prepareList();
            for (int i = 0; i < splitProgram.Length; i++)
            {
                if(!splitProgram[i].Equals("") && !splitProgram[i].Equals(" ") && !splitProgram[i].Equals("\t"))
                {
                    this.addToList(splitProgram[i]);
                }
            }
        }
        public string[] getProgram()
        {
            return this.myArray;
        }

        public void getLexema()
        {
            //This flag to skip check next index in loop
            int flag = 0;

            int line = 1;
            int lexemLine = 0;
            string lexeme;
            for (int i = 0; i < this.myArray.Length; i++)
            {
                if (flag == 1)
                {
                    flag = 0;
                    continue;
                }

                //To Skip Comment Line and don't check it 
                if (this.skipCommentLine(i))
                {
                    continue;
                }

                switch (this.myArray[i])
                {
                    case "\n":
                        line++;
                        lexemLine = 0;
                        break;
                    case "/":
                        if (!isEndOfArray(myArray.Length, i + 1) && this.myArray[i + 1].Equals("-"))
                        {
                            lexeme = "/-";
                            foundComment = true;
                            multiLineComment = true;
                            setScannerModel(line, lexeme, ++lexemLine, true, DevelopedFunctions.getReturnToken(lexeme));
                            flag = 1;
                        }
                        else
                        {
                            lexeme = "/";
                            setScannerModel(line, lexeme, ++lexemLine, true, DevelopedFunctions.getReturnToken(lexeme));
                        }
                        break;
                    case "-":
                        if (!isEndOfArray(myArray.Length, i + 1) && this.myArray[i + 1].Equals(">"))
                        {
                            lexeme = "->";
                            setScannerModel(line, lexeme, ++lexemLine, true, DevelopedFunctions.getReturnToken(lexeme));
                            flag = 1;
                        }
                        else if (!isEndOfArray(myArray.Length, i + 1) && this.myArray[i + 1].Equals("/"))
                        {
                            lexeme = "-/";
                            setScannerModel(line, lexeme, ++lexemLine, true, DevelopedFunctions.getReturnToken(lexeme));
                            flag = 1;
                        }
                        else if (!isEndOfArray(myArray.Length, i + 1) && this.myArray[i + 1].Equals("-"))
                        {
                            lexeme = "--";
                            setScannerModel(line, lexeme, ++lexemLine, true, DevelopedFunctions.getReturnToken(lexeme));
                            flag = 1;
                            foundComment = true;
                        }
                        else
                        {
                            lexeme = "-";
                            setScannerModel(line, lexeme, ++lexemLine, true, DevelopedFunctions.getReturnToken(lexeme));
                        }
                        break;
                        
                    
                    case "=":
                        if (!isEndOfArray(myArray.Length, i + 1) && this.myArray[i + 1].Equals("="))
                        {
                            lexeme = "==";
                            setScannerModel(line, lexeme, ++lexemLine, true, DevelopedFunctions.getReturnToken(lexeme));
                            flag = 1;
                        }else
                        {
                            lexeme = "=";
                            setScannerModel(line, lexeme, ++lexemLine, true, DevelopedFunctions.getReturnToken(lexeme));
                        }
                        break;
                    case "<":
                        if (!isEndOfArray(myArray.Length, i + 1) && this.myArray[i + 1].Equals("="))
                        {
                            lexeme = "<=";
                            setScannerModel(line, lexeme, ++lexemLine, true, DevelopedFunctions.getReturnToken(lexeme));
                            flag = 1;
                        }
                        else
                        {
                            lexeme = "<";
                            setScannerModel(line, lexeme, ++lexemLine, true, DevelopedFunctions.getReturnToken(lexeme));
                        }      
                        break;
                    case ">":
                        if (!isEndOfArray(myArray.Length,i+1) && this.myArray[i + 1].Equals("="))
                        {
                            lexeme = ">=";
                            setScannerModel(line, lexeme, ++lexemLine, true, DevelopedFunctions.getReturnToken(lexeme));
                            flag = 1;
                        }
                        else
                        {
                            lexeme = ">";
                            setScannerModel(line, lexeme, ++lexemLine, true, DevelopedFunctions.getReturnToken(lexeme));
                        }    
                        break;
                    case "!":
                        if (!isEndOfArray(myArray.Length, i + 1) && this.myArray[i + 1].Equals("="))
                        {
                            lexeme = "!=";
                            setScannerModel(line, lexeme, ++lexemLine, true, DevelopedFunctions.getReturnToken(lexeme));
                            flag = 1;
                        }
                        break;
                    case "&":
                        if (!isEndOfArray(myArray.Length, i + 1) && this.myArray[i + 1].Equals("&"))
                        {
                            lexeme = "&&";
                            setScannerModel(line, lexeme, ++lexemLine, true, DevelopedFunctions.getReturnToken(lexeme));
                            flag = 1;
                        }
                        else
                        {
                            lexeme = "&";
                            setScannerModel(line, lexeme, ++lexemLine, true, DevelopedFunctions.getReturnToken(lexeme));
                        }
                        break;
                    case "|":
                        if (!isEndOfArray(myArray.Length, i + 1) && this.myArray[i + 1].Equals("|"))
                        {
                            lexeme = "||";
                            setScannerModel(line, lexeme, ++lexemLine, true, DevelopedFunctions.getReturnToken(lexeme));
                            flag = 1;
                        }
                        else
                        {
                            lexeme = "|";
                            setScannerModel(line, lexeme, ++lexemLine, true, DevelopedFunctions.getReturnToken(lexeme));
                        }
                        break;

                    default:
                        setOutput(line, this.myArray[i], ++lexemLine);
                        break;
                }

            }
        }
        private bool skipCommentLine(int i)
        {
            //To Skip Comment Line and don't check it 
            if (foundComment == true)
            {
                if (multiLineComment == true)
                {
                    if (this.myArray[i].Equals("-") && this.myArray[i + 1].Equals("/"))
                    {
                        foundComment = false;
                        multiLineComment = false;
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    if (this.myArray[i].Equals("\n"))
                    {
                        foundComment = false;
                        return false;
                    } 
                    return true;
                }

            }
            return false;
        }
        
        private void setOutput(int lineNo, string lexem, int lexemeNoInLine)
        {
            string returnToken = DevelopedFunctions.getReturnToken(lexem);
            if (returnToken != null)
            {
                setScannerModel(lineNo, lexem, lexemeNoInLine, true, returnToken);
            }
            else
            {
                if (DevelopedFunctions.isNumber(lexem))
                    setScannerModel(lineNo, lexem, lexemeNoInLine, true, "Constant");
                else
                    setScannerModel(lineNo, lexem, lexemeNoInLine, true, "Identifier");

            }

        }
        private bool isEndOfArray(int length,int i)
        {
            return i == length ? true : false;
        }
        private void setScannerModel(int lineNo, string lexem, int lexemeNoInLine, bool matchability,string returnToken)
        {
            queue.enqueue(new ScannerModel()
            {
                lineNo = lineNo,
                lexem = lexem,
                lexemeNoInLine = lexemeNoInLine,
                matchability = matchability,
                returnToken=returnToken

            });
        }

}
    class ScannerModel
    {
        
        public int lineNo { get; set; }
        public string lexem { get; set; }
        public int lexemeNoInLine { get; set; }
        public bool matchability { get; set; }
        public string returnToken { get; set; }

    }
}

