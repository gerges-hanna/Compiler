﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Model
{
    class Run
    {
        static void Main(string[] args)
        {
            /*Token[] keywords = DevelopedFunctions.loadKeywords("../../keywords.txt");
            foreach(Token t in keywords)
            {
                Console.WriteLine(t);
            }*/

            /*string s = "kansdknsad";
            Console.WriteLine(DevelopedFunctions.subString(s, 99, 99));*/

            //char[] g = { '$', '^' };
            //string []s = DevelopedFunctions.splitUsingArray("Gerges$Hanna^", g);

            //for(int i = 0; i < s.Length; i++)
            //{
            //    Console.WriteLine(s[i]);
            //}

            Console.WriteLine("This is main method from Model package, " +
                "change it if this is not your method" +
                "Right click on Compiler project and properties" +
                "and change the startup object");

            //"/-q's'qsd\"sfwe\"fw==efdvfdre-/\n" +
            String test1 = "/-q's'qsd\"sfwe\"fw==efdvfdre-/\n" +
                "--This is main == function\n" +
                "Ity#decr'e'ase(){\n" +
                "Ity#3num=5^\n" +
                "Whatever (counter<num)\n" +
                "reg3=reg3-1^} }\n";
            String test2 =
                "/- it's comment so any thing here (*-*) not appear in lexema -/\n" +
                "Ity#testVar=9^\n" +
                "Sity#calculateFunction(Ity#index){\n" +
                "	--it's make index increase an it's comment too\n" +
                "	Whatever(index<testVar){\n" +
                "		index=index+1^\n" +
                "}\n" +
                "}";

            Console.WriteLine(test2);


            Model.Scanner scanner = new Scanner();
            scanner.setProgram(test2);
            scanner.getLexema();
            scanner.queue.display();
            Console.WriteLine("LineNo\tLexem\tLexemeNoInLine\tMarchability\tReturnToken");
            while (!scanner.queue2.isEmpty())
            {
                Console.WriteLine(scanner.queue2.peekFront().lineNo + "\t\t" + scanner.queue2.peekFront().lexem + "\t\t" + scanner.queue2.peekFront().lexemeNoInLine + "\t\t\t\t" + scanner.queue2.peekFront().matchability);
                scanner.queue2.dequeue();
            } 

            //DS.Stack<String> obj = new DS.Stack<String>("Empty");
            //obj.push("as");
            //obj.push("s");
            //obj.push("sa");
            //obj.push("w");

            //// print Stack elements
            //obj.display();

            //// print Top element of Stack
            //Console.Write("\nTop element is {0}\n", obj.peek());

            //// Delete top element of Stack
            //obj.pop();
            //obj.pop();

            //// print Stack elements
            //obj.display();

            //// print Top element of Stack
            //Console.Write("\nTop element is {0}\n", obj.peek());

            //obj.pop();
            //obj.pop();

            //// print Stack elements
            //obj.display();

            //// print Top element of Stack
            //Console.Write("\nTop element is {0}\n", obj.peek());

            /*DS.Queue<String> queue = new DS.Queue<String>("Empty");
            queue.enqueue("as");
            queue.dequeue();
            queue.enqueue("ef");
            queue.enqueue("ghn");
            queue.enqueue("mh");
            queue.display();
            Console.WriteLine(queue.peekFront());
            Console.WriteLine(queue.peekRear());*/




        }
    }
}
