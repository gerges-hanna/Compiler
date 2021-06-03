using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Model
{
    class ScannerOutput
    {
        private ScannerModel[] rows;
        private int totalNumberOfErrors = 0;
        private string mainCode;
        private string[] filesIncluded;
        public ScannerOutput(string mainCode)
        {
            rows = new ScannerModel[0];
            this.mainCode = mainCode;
            totalNumberOfErrors = 0;
            filesIncluded = new string[0];
        }

        public int CompileAll()
        {
            recursiveScanning(mainCode, 0);
            return totalNumberOfErrors;
        }
        private void recursiveScanning(string fileName, int stackTree = 0)
        {
            Scanner sc = new Scanner();
            try
            {
                if (stackTree != 0)
                {
                    StreamReader st = new StreamReader(fileName);
                    string refreshedString = "";
                    string OriginalString = st.ReadToEnd();
                    for (int i = 0; i < OriginalString.Length; i++)
                    {
                        if (OriginalString[i] != '\r')
                        {
                            refreshedString += OriginalString[i].ToString();
                        }
                    }
                    sc.getLexema(refreshedString);
                }
                else
                    sc.getLexema(fileName);
                totalNumberOfErrors += CodeErrors.getNumberOfErrors(sc.queue);
                ScannerModel[] tmpRows = CodeErrors.rows;
                for(int i = 0; i < tmpRows.Length; i++)
                {
                    this.rows = DevelopedFunctions.copyAndAdd1<ScannerModel>(this.rows);
                    this.rows[this.rows.Length - 1] = tmpRows[i];
                    if (tmpRows[i].returnToken == "Inclusion" || tmpRows[i].lexem == "Require")
                    {
                        if (i + 1 < tmpRows.Length && !DevelopedFunctions.isStringThere(filesIncluded, tmpRows[i + 1].lexem))
                        {
                            filesIncluded = DevelopedFunctions.copyAndAdd1<string>(filesIncluded);
                            filesIncluded[filesIncluded.Length - 1] = tmpRows[i + 1].lexem;

                            this.rows = DevelopedFunctions.copyAndAdd1<ScannerModel>(this.rows);
                            this.rows[this.rows.Length - 1] = tmpRows[i + 1];

                            this.rows = DevelopedFunctions.copyAndAdd1<ScannerModel>(this.rows);
                            this.rows[this.rows.Length - 1] = getRowSeperator(tmpRows[i + 1].lexem, true);
                            recursiveScanning(tmpRows[i + 1].lexem, stackTree + 1);

                            this.rows = DevelopedFunctions.copyAndAdd1<ScannerModel>(this.rows);
                            this.rows[this.rows.Length - 1] = getRowSeperator(tmpRows[i + 1].lexem, false);
                            
                            i++;
                        }
                    }
                }
            }
            catch(FileNotFoundException fnfe)
            {
                Console.WriteLine(fileName + " not found");
                return;
            }
        }
        private ScannerModel getRowSeperator(string fileName, bool start)
        {
            ScannerModel reval = new ScannerModel();
            reval.lineNo = 0;
            reval.lexemeNoInLine = 0;
            reval.lexem = "";
            reval.returnToken = (start? "start of: ": "end of: ") + fileName;
            return reval;
        }

        public ScannerModel[] getRows()
        {
            return this.rows;
        }
    }
}
