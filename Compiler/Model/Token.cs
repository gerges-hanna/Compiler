﻿namespace Compiler.Model
{
    public class Token
    {
        private string _lexeme;
        private string _returnToken;
        public Token(string lexeme = "", string returnToken = "")
        {
            this._lexeme = lexeme;
            this._returnToken = returnToken;
        }

        public string getLexeme() => _lexeme;
        public string getReturnToken() => _returnToken;

        public void setLexeme(string lex)
        {
            _lexeme = lex;
        }

        public void setReturnToken(string RT)
        {
            _returnToken = RT;
        }

        public bool isLexemesAreEqual(Token a)
        {
            return a.getLexeme() == this._lexeme;
        }

        public bool isReturnTokensAreEqual(Token a)
        {
            return a.getReturnToken() == this._returnToken;
        }

        public static Token[] copyAndAdd1(Token []oldTokens)
        {
            Token[] returnValue = new Token[oldTokens.Length + 1];
            for (int i = 0; i < oldTokens.Length; i++)
            {
                returnValue[i] = oldTokens[i];
            }
            return returnValue;
        }
        public override string ToString()
        {
            return this._lexeme + " " + this._returnToken;
        }
    }
}
