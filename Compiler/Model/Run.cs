using System;
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

            char[] g = { '$', '^' };
            string []s = DevelopedFunctions.splitUsingArray("Gerges$Hanna^", g);

            for(int i = 0; i < s.Length; i++)
            {
                Console.WriteLine(s[i]);
            }

            Console.WriteLine("This is main method from Model package, " +
                "change it if this is not your method" +
                "Right click on Compiler project and properties" +
                "and change the startup object");


            String test1 ="/-qsqsdsfwefw==efdvfdre-/\n"+
                "--This is main function\n" +
                "Ity#decrease(){\n" +
                "Ity#3num=5^\n" +
                "Whatever (counter<num)\n"+
                "reg3=reg3-1^} }\n";

            Console.WriteLine(test1);


            Model.Scanner scanner = new Scanner();
            scanner.setProgram(test1);
            scanner.getLexema();
            scanner.queue.display();
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
