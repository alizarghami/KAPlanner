using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace ProblemParser
{
    abstract class FileParser
    {
        protected StreamReader mFile = null;
        protected char[] TokenSeperator = new char[] { ':' };
        protected DataContainer Data;

        protected void OpenFile(string filename)
        {
            if (mFile != null)
                mFile.Close();
            mFile = new StreamReader(filename);
        }

        protected string ReadNewLine(bool dothrow = true)
        {
            string s;
            while ((s = mFile.ReadLine()) != null)
            {
                s = s.Trim().ToLower();
                if (s == "")
                    continue;
                else
                    return s;
            }

            if (dothrow)
                throw new InvalidDataException("Unexpected end of file");
            else
                return null;
        }

        protected void ParseStrInt(string line, out string str, out int val)
        {
            string[] tokens = line.Split(TokenSeperator);
            if (tokens.Length != 2)
                throw new InvalidDataException("Invalid StrInt");

            str = tokens[0].Trim();
            val = int.Parse(tokens[1]);
        }

        public abstract void Parse();

    }
}
