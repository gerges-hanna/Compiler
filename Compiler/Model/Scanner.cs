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
                                    ,']','/','(',')','\'','\"'};



        public DS.Queue<string> queue = new DS.Queue<string>("Empty");
        public DS.Queue<ScannerModel> queue2 = new DS.Queue<ScannerModel>(new ScannerModel());
        ArrayList arlist = new ArrayList();
        

        public int lineNo { get; set; }
        public string lexem { get; set; }
        public int lexemeNoInLine { get; set; }
        public bool matchability { get; set; }


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


        public void setProgram(String Program)
        {
            string[] splitProgram= DevelopedFunctions.splitUsingArray(Program, splitters);
            this.prepareList();
            for (int i = 0; i < splitProgram.Length; i++)
            {
                if(!splitProgram[i].Equals("") && !splitProgram[i].Equals(" "))
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
                        switch (this.myArray[i + 1])
                        {
                            case "-":
                                queue.enqueue("/-");
                                foundComment = true;
                                multiLineComment = true;
                                setScannerOutput(line, "/-", ++lexemLine, true);
                                flag = 1;
                                break;
                            default:
                                queue.enqueue("/");
                                setScannerOutput(line, "/", ++lexemLine, true);
                                break;
                        }
                        break;
                    case "-":
                        switch (this.myArray[i+1])
                        {
                            case ">":
                                queue.enqueue("->");
                                setScannerOutput(line, "->", ++lexemLine, true);
                                flag = 1;
                                break;
                            case "/":
                                queue.enqueue("-/");
                                setScannerOutput(line, "-/", ++lexemLine, true);
                                flag = 1;
                                break;
                            case "-":
                                queue.enqueue("--");
                                setScannerOutput(line, "--", ++lexemLine, true);
                                flag = 1;
                                foundComment = true;
                                break;
                            
                            default:
                                queue.enqueue("-");
                                setScannerOutput(line, "-", ++lexemLine, true);
                                break;

                        }
                        break;
                    
                    case "=":
                        if (this.myArray[i + 1].Equals("="))
                        {
                            queue.enqueue("==");
                            setScannerOutput(line, "==", ++lexemLine, true);
                            flag = 1;
                        }else
                            queue.enqueue("=");
                        setScannerOutput(line, "=", ++lexemLine, true);
                        break;
                    case "<":
                        if (this.myArray[i + 1].Equals("="))
                        {
                            queue.enqueue("<=");
                            setScannerOutput(line, "<=", ++lexemLine, true);
                            flag = 1;
                        }
                        else
                        {
                            queue.enqueue("<");
                            setScannerOutput(line, "<", ++lexemLine, true);
                        }      
                        break;
                    case ">":
                        if (this.myArray[i + 1].Equals("="))
                        {
                            queue.enqueue(">=");
                            setScannerOutput(line, ">=", ++lexemLine, true);
                            flag = 1;
                        }
                        else
                        {
                            queue.enqueue(">");
                            setScannerOutput(line, ">", ++lexemLine, true);
                        }    
                        break;
                    case "!":
                        if (this.myArray[i + 1].Equals("="))
                        {
                            queue.enqueue("!=");
                            setScannerOutput(line, "!=", ++lexemLine, true);
                            flag = 1;
                        }
                        break;
                    case "+":
                        queue.enqueue("+");
                        setScannerOutput(line, "+", ++lexemLine, true);
                        break;
                    case "*":
                        queue.enqueue("*");
                        setScannerOutput(line, "*", ++lexemLine, true);
                        break;
                    case "%":
                        queue.enqueue("%");
                        setScannerOutput(line, "%", ++lexemLine, true);
                        break;
                    case "#":
                        queue.enqueue("#");
                        setScannerOutput(line, "#", ++lexemLine, true);
                        break;
                    case "^":
                        queue.enqueue("^");
                        setScannerOutput(line, "^", ++lexemLine, true);
                        break;
                    case "@":
                        queue.enqueue("@");
                        setScannerOutput(line, "@", ++lexemLine, true);
                        break;
                    case "$":
                        queue.enqueue("$");
                        setScannerOutput(line, "$", ++lexemLine, true);
                        break;

                    case "&":
                        if (this.myArray[i + 1].Equals("&"))
                        {
                            queue.enqueue("&&");
                            setScannerOutput(line, "&&", ++lexemLine, true);
                            flag = 1;
                        }
                        break;
                    case "|":
                        if (this.myArray[i + 1].Equals("|"))
                        {
                            queue.enqueue("||");
                            setScannerOutput(line, "||", ++lexemLine, true);
                            flag = 1;
                        }
                        break;
                    case "~":
                        queue.enqueue("~");
                        setScannerOutput(line, "~", ++lexemLine, true);
                        break;
                    case "{":
                        queue.enqueue("{");
                        setScannerOutput(line, "{", ++lexemLine, true);
                        break;
                    case "}":
                        queue.enqueue("}");
                        setScannerOutput(line, "}", ++lexemLine, true);
                        break;
                    case "[":
                        queue.enqueue("[");
                        setScannerOutput(line, "[", ++lexemLine, true);
                        break;
                    case "]":
                        queue.enqueue("]");
                        setScannerOutput(line, "]", ++lexemLine, true);
                        break;
                    case "(":
                        queue.enqueue("(");
                        setScannerOutput(line, "(", ++lexemLine, true);
                        break;
                    case ")":
                        queue.enqueue(")");
                        setScannerOutput(line, ")", ++lexemLine, true);
                        break;

                    case "'":
                        queue.enqueue("'");
                        setScannerOutput(line, "'", ++lexemLine, true);
                        break;
                    case "\"":
                        queue.enqueue("\"");
                        setScannerOutput(line, "\"", ++lexemLine, true);
                        break;
                    case "Pattern":
                        queue.enqueue("Pattern");
                        setScannerOutput(line, "Pattern", ++lexemLine, true);
                        break;
                    case "DerivedFrom":
                        queue.enqueue("DerivedFrom");
                        setScannerOutput(line, "DerivedFrom", ++lexemLine, true);
                        break;
                    case "TrueFor":
                        queue.enqueue("TrueFor");
                        setScannerOutput(line, "TrueFor", ++lexemLine, true);
                        break;
                    case "Else":
                        queue.enqueue("Else");
                        setScannerOutput(line, "Else", ++lexemLine, true);
                        break;
                    case "Ity":
                        queue.enqueue("Ity");
                        setScannerOutput(line, "Ity", ++lexemLine, true);
                        break;
                    case "Sity":
                        queue.enqueue("Sity");
                        setScannerOutput(line, "Sity", ++lexemLine, true);
                        break;
                    case "Cwq":
                        queue.enqueue("Cwq");
                        setScannerOutput(line, "Cwq", ++lexemLine, true);
                        break;
                    case "CwqSequence":
                        queue.enqueue("CwqSequence");
                        setScannerOutput(line, "CwqSequence", ++lexemLine, true);
                        break;
                    case "Ifity":
                        queue.enqueue("Ifity");
                        setScannerOutput(line, "Ifity", ++lexemLine, true);
                        break;
                    case "Sifity":
                        queue.enqueue("Sifity");
                        setScannerOutput(line, "Sifity", ++lexemLine, true);
                        break;
                    case "Valueless":
                        queue.enqueue("Valueless");
                        setScannerOutput(line, "Valueless", ++lexemLine, true);
                        break;
                    case "Logical":
                        queue.enqueue("Logical");
                        setScannerOutput(line, "Logical", ++lexemLine, true);
                        break;
                    case "BreakFromThis":
                        queue.enqueue("BreakFromThis");
                        setScannerOutput(line, "BreakFromThis", ++lexemLine, true);
                        break;
                    case "Whatever":
                        queue.enqueue("Whatever");
                        setScannerOutput(line, "Whatever", ++lexemLine, true);
                        break;
                    case "Respondwith":
                        queue.enqueue("Respondwith");
                        setScannerOutput(line, "Respondwith", ++lexemLine, true);
                        break;
                    case "Srap":
                        queue.enqueue("Srap");
                        setScannerOutput(line, "Srap", ++lexemLine, true);
                        break;
                    case "Scan":
                        queue.enqueue("Scan");
                        setScannerOutput(line, "Scan", ++lexemLine, true);
                        break;
                    case "Conditionof":
                        queue.enqueue("Conditionof");
                        setScannerOutput(line, "Conditionof", ++lexemLine, true);
                        break;
                    case "Require":
                        queue.enqueue("Require");
                        setScannerOutput(line, "Require", ++lexemLine, true);
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
        private void setScannerOutput(int lineNo, string lexem, int lexemeNoInLine, bool matchability)
        {
            queue2.enqueue(new ScannerModel()
            {
                lineNo = lineNo,
                lexem = lexem,
                lexemeNoInLine = lexemeNoInLine,
                matchability = matchability
            });
        }
}
    class ScannerModel
    {
        public int lineNo { get; set; }
        public string lexem { get; set; }
        public int lexemeNoInLine { get; set; }
        public bool matchability { get; set; }

}
}


//
//
//case "Respondwith":
//case "Srap":
//case "Scan":
//case "Conditionof":
//case "Require":