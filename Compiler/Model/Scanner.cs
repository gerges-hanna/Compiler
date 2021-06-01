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
        private string[] splittersString = { 
                                     "#", "^" ,"\n"," ","@","$"
                                    ,"+","*","%","&&","||","~"
                                    ,"->","--","-/","-"
                                    ,"/-","/"
                                    ,"==","="
                                    ,"<=","<"
                                    ,">=",">"
                                    ,"!="
                                    ,"{","}","[","]","(",")",","
                                    ,"\t","\'","\""
        };


        public DS.Stack<String> stack = new DS.Stack<string>("Empty");
        public DS.Queue<ScannerModel> queue = new DS.Queue<ScannerModel>(new ScannerModel());
        
        ArrayList arlist = new ArrayList();

        //if found comment don't read and set it true
        private bool foundComment = false;
        private bool multiLineComment = false;

        private string[] myArray;

        public void getLexema(String Program)
        {
            this.myArray = DevelopedFunctions.splitStringUsingSTArray(Program, splittersString);
            //This flag to skip check next index in loop
            bool cotationFlag = false;
            int line = 1;
            int lexemLine = 0;
            string lexeme="";
            for (int i = 0; i < this.myArray.Length; i++)
            {
                if (this.myArray[i].Equals("-/"))
                {
                    foundComment = false;
                    multiLineComment = false;
                }

                if (cotationFlag && (!this.myArray[i].Equals("\'") && !this.myArray[i].Equals("\"")))
                {
                    lexeme = lexeme + this.myArray[i];
                    continue;
                }
                    

                //To Skip Comment Line and don't check it 
                if (this.skipCommentLine(i))
                {
                    if (this.myArray[i].Equals("\n"))
                    {
                        line++;
                        lexemLine = 0;
                    }
                        
                    continue;
                }
                    


                switch (this.myArray[i])
                {
                    case "":
                    case " ":
                    case "\t":
                        if(cotationFlag)
                            lexeme = lexeme + this.myArray[i];
                        break;

                    case "\n":
                        line++;
                        lexemLine = 0;
                        break;

                    case "\"":
                    case "\'":
                        if (stack.peek().Equals(this.myArray[i]))
                        {
                            stack.pop();
                            cotationFlag = false;
                            setScannerModel(line, lexeme, ++lexemLine, true, "String");
                        }
                        else if (cotationFlag)
                        {
                            lexeme = lexeme + this.myArray[i];
                            continue;
                        }
                        else
                        {
                            lexeme = "";
                            stack.push(this.myArray[i]);
                            cotationFlag = true;
                        }

                        setOutput(line, this.myArray[i], ++lexemLine);
                        break;

                    case "/-":
                        lexeme = "/-";
                        foundComment = true;
                        multiLineComment = true;
                        setScannerModel(line, lexeme, ++lexemLine, true, DevelopedFunctions.getReturnToken(lexeme));
                        break;        
                    case "--":
                        lexeme = "--";
                        setScannerModel(line, lexeme, ++lexemLine, true, DevelopedFunctions.getReturnToken(lexeme));
                        foundComment = true;
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

