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
            Console.WriteLine("This is main method from Model package, " +
                "change it if this is not your method" +
                "Right click on Compiler project and properties" +
                "and change the startup object");
        }
    }
}
