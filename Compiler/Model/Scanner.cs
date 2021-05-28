using System;
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
                                    ,']','/','(',')'};
        
        public DS.Queue<string> queue = new DS.Queue<string>("Empty");

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
            
            for (int i = 0; i < this.myArray.Length; i++)
            {
                if (flag == 1)
                {
                    flag = 0;
                    continue;
                }
                

                switch (this.myArray[i])
                {
                    case "/":
                        switch (this.myArray[i + 1])
                        {
                            case "-":
                                queue.enqueue("/-");
                                flag = 1;
                                break;
                            default:
                                queue.enqueue("/");
                                break;
                        }
                        break;
                    case "-":
                        switch (this.myArray[i+1])
                        {
                            case ">":
                                queue.enqueue("->");
                                flag = 1;
                                break;
                            case "/":
                                queue.enqueue("-/");
                                flag = 1;
                                break;
                            case "-":
                                queue.enqueue("--");
                                flag = 1;
                                break;
                            
                            default:
                                queue.enqueue("-");
                                break;

                        }
                        break;
                    
                    case "=":
                        if (this.myArray[i + 1].Equals("="))
                        {
                            queue.enqueue("==");
                            flag = 1;
                        }else
                            queue.enqueue("=");
                        break;
                    case "<":
                        if (this.myArray[i + 1].Equals("="))
                        {
                            queue.enqueue("<=");
                            flag = 1;
                        }
                        else
                            queue.enqueue("<");
                        break;
                    case ">":
                        if (this.myArray[i + 1].Equals("="))
                        {
                            queue.enqueue(">=");
                            flag = 1;
                        }
                        else
                            queue.enqueue(">");
                        break;
                    case "!":
                        if (this.myArray[i + 1].Equals("="))
                        {
                            queue.enqueue("!=");
                            flag = 1;
                        }
                        break;
                }

            }
        }
}
}


//case "Pattern":
//case "DerivedFrom":
//case "TrueFor":
//case "Else":
//case "Ity":
//case "Sity":
//case "Cwq":
//case "CwqSequence":
//case "Ifity":
//case "Sifity":
//case "Valueless":
//case "Logical":
//case "BreakFromThis":
//case "Whatever":
//case "Respondwith":
//case "Srap":
//case "Scan":
//case "Conditionof":
//case "Require":