

namespace Compiler.Model
{
    class CodeErrors
    {
        public static ScannerModel[] lines = new ScannerModel[0];
        public static int getNumberOfErrors(DS.Queue<ScannerModel> queue)
        {
            DS.Stack<string> curlyBrackets = new DS.Stack<string>("error");
            DS.Stack<string> squareBrackets = new DS.Stack<string>("error");
            int errorCounter = 0;
            int linesIndex = 0;
            while (!queue.isEmpty())
            {
                ScannerModel newSM = new ScannerModel();
                newSM.lineNo = queue.peekFront().lineNo;
                newSM.lexem = queue.peekFront().lexem;
                newSM.returnToken = queue.peekFront().returnToken;
                newSM.lexemeNoInLine = queue.peekFront().lexemeNoInLine;
                newSM.matchability = queue.peekFront().matchability;

                lines = DevelopedFunctions.copyAndAdd1<ScannerModel>(lines);
                lines[linesIndex] = newSM;
                linesIndex++;

                queue.dequeue();
            }
            for (int i = 0; i < lines.Length; i++)
            {
                switch (lines[i].lexem)
                {
                    case "#":
                        bool before = true;
                        bool after = true;
                        if (i - 1 == -1)
                        {
                            before = false;
                        }
                        else
                        {
                            if (!(lines[i - 1].lexem == "Ity" || lines[i - 1].lexem == "Sity" ||
                                lines[i - 1].lexem == "Cwq" || lines[i - 1].lexem == "CwqSequence" ||
                                lines[i - 1].lexem == "Ifity" || lines[i - 1].lexem == "Sifity" ||
                                lines[i - 1].lexem == "Valueless" || lines[i - 1].lexem == "Logical"))
                            {
                                before = false;
                            }
                        }
                        if (i + 1 >= lines.Length)
                        {
                            after = false;
                        }
                        else
                        {
                            if (lines[i + 1].returnToken != "Identifier")
                            {
                                after = false;
                            }
                            else
                            {
                                if (!DevelopedFunctions.isValidIdentifier(lines[i + 1].lexem))
                                {
                                    after = false;
                                }
                            }
                        }
                        if (before)
                        {
                            lines[i].matchability = true;
                            if (!after)
                            {
                                lines[i + 1].matchability = false;
                                errorCounter++;
                            }
                        }
                        else
                        {
                            lines[i].matchability = false;
                            errorCounter++;
                        }
                        break;
                    case "{":
                        if (i - 1 == -1)
                        {
                            errorCounter++;
                            lines[i].matchability = false;
                        }
                        else
                        {
                            if (lines[i - 1].lexem != "Pattern" || lines[i - 1].lexem != "Else" ||
                                !DevelopedFunctions.isValidIdentifier(lines[i - 1].lexem))
                            {
                                errorCounter++;
                                lines[i].matchability = false;
                            }
                        }
                        curlyBrackets.push("{");
                        break;
                    case "}":
                        if (curlyBrackets.isEmpty() || curlyBrackets.peek() != "{")
                        {
                            lines[i].matchability = false;
                            errorCounter++;
                        }
                        else if (curlyBrackets.peek() == "{")
                            curlyBrackets.pop();
                        break;
                    case "/":
                    case "*":
                    case "-":
                    case "+":
                    case "==":
                    case "<":
                    case ">":
                    case "!=":
                    case "<=":
                    case ">=":
                        before = true;
                        after = true;
                        if (i - 1 == -1)
                        {
                            before = false;
                        }
                        else
                        {
                            if (lines[i - 1].returnToken != "Constant" && !DevelopedFunctions.isValidIdentifier(lines[i - 1].lexem))
                            {
                                before = false;
                            }
                        }
                        if (i + 1 >= lines.Length)
                        {
                            after = false;
                        }
                        else
                        {
                            if (lines[i + 1].returnToken != "Constant" && !DevelopedFunctions.isValidIdentifier(lines[i + 1].lexem))
                            {
                                after = false;
                            }
                        }
                        if (before)
                        {
                            if (!after)
                            {
                                errorCounter++;
                                lines[i + 1].matchability = false;
                            }
                        }
                        else
                        {
                            lines[i].matchability = false;
                            errorCounter++;
                        }
                        break;
                    case "=":
                        before = true;
                        after = true;
                        if (i - 1 == -1)
                        {
                            before = false;
                        }
                        else
                        {
                            if (!DevelopedFunctions.isValidIdentifier(lines[i - 1].lexem))
                            {
                                before = false;
                            }
                        }
                        if (i + 1 >= lines.Length)
                        {
                            after = false;
                        }
                        else
                        {
                            if (lines[i + 1].returnToken != "Constant" && !DevelopedFunctions.isValidIdentifier(lines[i + 1].lexem))
                            {
                                after = false;
                            }
                            
                        }
                        if (before)
                        {
                            if (!after)
                            {
                                errorCounter++;
                                lines[i + 1].matchability = false;
                            }
                        }
                        else
                        {
                            errorCounter++;
                            lines[i].matchability = false;
                        }
                        break;
                    case "->":
                        before = true;
                        after = true;
                        if (i - 1 == -1)
                        {
                            before = false;
                        }
                        else
                        {
                            if (!DevelopedFunctions.isValidIdentifier(lines[i - 1].lexem))
                            {
                                before = false;
                            }
                        }
                        if (i + 1 >= lines.Length)
                        {
                            after = false;
                        }
                        else
                        {
                            if (!DevelopedFunctions.isValidIdentifier(lines[i + 1].lexem))
                            {
                                after = false;
                            }
                        }
                        if (before)
                        {
                            if (!after)
                            {
                                errorCounter++;
                                lines[i + 1].matchability = false;
                            }
                        }
                        else
                        {
                            errorCounter++;
                            lines[i].matchability = false;
                        }
                        break;
                    case "Require":
                        if (i + 1 > lines.Length)
                        {
                            lines[i].matchability = false;
                            errorCounter++;
                        }
                        else
                        {
                            if (!DevelopedFunctions.isValidIdentifier(lines[i + 1].lexem))
                            {
                                lines[i].matchability = false;
                                errorCounter++;
                            }
                        }
                        break;
                    case ",":
                        before = true;
                        after = true;
                        if (i - 1 == lines.Length)
                        {
                            before = false;
                        }
                        else
                        {
                            if (lines[i - 1].returnToken != "Identifier")
                            {
                                before = false;
                            }
                        }
                        if (i + 1 >= lines.Length)
                        {
                            after = false;
                        }
                        else
                        {
                            if (!DevelopedFunctions.isValidIdentifier(lines[i + 1].lexem))
                            {
                                after = false;
                            }
                        }
                        if (before)
                        {
                            if (!after)
                            {
                                errorCounter++;
                                lines[i + 1].matchability = false;
                            }
                        }
                        else
                        {
                            errorCounter++;
                            lines[i].matchability = false;
                        }
                        break;
                    case "[":
                        before = false;
                        after = false;
                        if (i - 1 == -1)
                        {
                            before = false;
                        }
                        else
                        {
                            if (!DevelopedFunctions.isValidIdentifier(lines[i - 1].lexem))
                            {
                                before = false;
                            }
                        }
                        if (i + 1 == lines.Length)
                        {
                            after = false;
                        }
                        if (lines[i + 1].returnToken != "Constant" || !DevelopedFunctions.isValidIdentifier(lines[i + 1].lexem))
                        {
                            after = false;
                        }
                        if (before && !after)
                        {
                            errorCounter++;
                            lines[i + 1].matchability = false;
                        }
                        else
                        {
                            errorCounter++;
                            lines[i].matchability = false;
                        }
                        squareBrackets.push("[");
                        break;
                    case "]":
                        if (squareBrackets.peek() != "[")
                        {
                            errorCounter++;
                            lines[i].matchability = false;
                            break;
                        }
                        else
                            squareBrackets.pop();
                        if (i - 1 != -1 && (!DevelopedFunctions.isValidIdentifier(lines[i - 1].lexem) || lines[i - 1].returnToken != "Constant"))
                        {
                            errorCounter++;
                            lines[i].matchability = false;
                        }
                        break;
                }
            }
            return errorCounter;
        }
    }
}
