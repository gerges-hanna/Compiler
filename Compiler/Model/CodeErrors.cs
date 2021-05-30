

namespace Compiler.Model
{
    class CodeErrors
    {
        
        public static ScannerModel[] rows = new ScannerModel[0];
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

                rows = DevelopedFunctions.copyAndAdd1<ScannerModel>(rows);
                rows[linesIndex] = newSM;
                linesIndex++;

                queue.dequeue();
            }
            for (int i = 0; i < rows.Length; i++)
            {
                switch (rows[i].lexem)
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
                            if (!(rows[i - 1].lexem == "Ity" || rows[i - 1].lexem == "Sity" ||
                                rows[i - 1].lexem == "Cwq" || rows[i - 1].lexem == "CwqSequence" ||
                                rows[i - 1].lexem == "Ifity" || rows[i - 1].lexem == "Sifity" ||
                                rows[i - 1].lexem == "Valueless" || rows[i - 1].lexem == "Logical"))
                            {
                                before = false;
                            }
                        }
                        if (i + 1 >= rows.Length)
                        {
                            after = false;
                        }
                        else
                        {
                            if (rows[i + 1].returnToken != "Identifier" && rows[i+1].lexem != "[")
                            {
                                after = false;
                            }
                            else
                            {
                                if (!DevelopedFunctions.isValidIdentifier(rows[i + 1].lexem) && rows[i+1].matchability)
                                {
                                    errorCounter++;
                                    rows[i + 1].matchability = false;
                                }
                            }
                        }
                        if (before)
                        {
                            rows[i].matchability = true;
                            if (!after)
                            {
                                rows[i + 1].matchability = false;
                                errorCounter++;
                            }
                        }
                        else if(!before)
                        {
                            rows[i-1].matchability = false;
                            errorCounter++;
                        }
                    break;
                    case "{":
                        curlyBrackets.push("{");
                    break;
                    case "}":
                        if (curlyBrackets.isEmpty() || curlyBrackets.peek() != "{")
                        {
                            rows[i].matchability = false;
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
                            if (rows[i - 1].returnToken != "Constant" && !DevelopedFunctions.isValidIdentifier(rows[i - 1].lexem) && rows[i-1].matchability)
                            {
                                errorCounter++;
                                rows[i - 1].matchability = false;
                            }
                        }
                        if (i + 1 >= rows.Length)
                        {
                            after = false;
                        }
                        else
                        {
                            if (rows[i + 1].returnToken != "Constant" && !DevelopedFunctions.isValidIdentifier(rows[i + 1].lexem) && rows[i+1].matchability)
                            {
                                errorCounter++;
                                rows[i + 1].matchability = false;
                            }
                        }
                        if (before)
                        {
                            if (!after)
                            {
                                errorCounter++;
                                rows[i + 1].matchability = false;
                            }
                        }
                        else
                        {
                            rows[i].matchability = false;
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
                            if (rows[i-1].returnToken == "Identifier" && !DevelopedFunctions.isValidIdentifier(rows[i - 1].lexem) && rows[i-1].matchability)
                            {
                                errorCounter++;
                                rows[i - 1].matchability = false;
                            }
                        }
                        if (i + 1 >= rows.Length)
                        {
                            after = false;
                        }
                        else
                        {
                            if (rows[i + 1].returnToken != "Quotation Mark" && rows[i+1].lexem != "{")
                            {
                                if (rows[i + 1].returnToken != "Constant" && !DevelopedFunctions.isValidIdentifier(rows[i + 1].lexem) && rows[i + 1].matchability)
                                {
                                    errorCounter++;
                                    rows[i+1].matchability = false;
                                }
                            }
                        }
                        if (before)
                        {
                            if (!after)
                            {
                                errorCounter++;
                                rows[i + 1].matchability = false;
                            }
                        }
                        else
                        {
                            errorCounter++;
                            rows[i].matchability = false;
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
                            if (!DevelopedFunctions.isValidIdentifier(rows[i - 1].lexem) && rows[i-1].matchability)
                            {
                                before = false;
                            }
                        }
                        if (i + 1 >= rows.Length)
                        {
                            after = false;
                        }
                        else
                        {
                            if (!DevelopedFunctions.isValidIdentifier(rows[i + 1].lexem) && rows[i - 1].matchability)
                            {
                                errorCounter++;
                                rows[i+1].matchability = false;
                            }
                        }
                        if (before)
                        {
                            if (!after)
                            {
                                errorCounter++;
                                rows[i + 1].matchability = false;
                            }
                        }
                        else
                        {
                            errorCounter++;
                            rows[i].matchability = false;
                        }
                    break;
                    case "Require":
                        if (i + 1 > rows.Length)
                        {
                            rows[i].matchability = false;
                            errorCounter++;
                        }
                        else
                        {
                            if (!DevelopedFunctions.isValidIdentifier(rows[i + 1].lexem))
                            {
                                rows[i].matchability = false;
                                errorCounter++;
                            }
                        }
                    break;
                    case ",":
                        before = true;
                        after = true;
                        if (i - 1 == -1)
                        {
                            before = false;
                        }
                        else
                        {
                            if (rows[i - 1].returnToken != "Constant")
                            {
                                if (!DevelopedFunctions.isValidIdentifier(rows[i - 1].lexem) && rows[i - 1].matchability)
                                {
                                    errorCounter++;
                                    rows[i - 1].matchability = false;
                                }
                            }
                        }
                        if (i + 1 >= rows.Length)
                        {
                            after = false;
                        }
                        else
                        {
                            if(rows[i+1].returnToken != "Constant")
                            {
                                if (!DevelopedFunctions.isValidIdentifier(rows[i + 1].lexem) && rows[i + 1].matchability)
                                {
                                    errorCounter++;
                                    rows[i + 1].matchability = false;
                                }
                            }
                            
                        }
                        if(!before || !after)
                        {
                            errorCounter++;
                            rows[i].matchability = false;
                        }
                    break;
                    case "[":
                        squareBrackets.push("[");
                    break;
                    case "]":
                        if (squareBrackets.peek() != "[")
                        {
                            errorCounter++;
                            rows[i].matchability = false;
                            break;
                        }
                        else
                            squareBrackets.pop();
                    break;
                }
            }
            return errorCounter;
        }
    }
}
