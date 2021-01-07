using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace ProblemParser
{
    class Predicate
    {
        public string Name;
        public int ParamCount;

        public int PredID;

        public int StartIndex;
        public int Size;
        public Predicate(string str, int count)
        {
            Name = str;
            ParamCount = count;
        }

        public string GenerateName(List<string> parameters = null)
        {
            Debug.Assert(!((parameters != null) && (parameters.Count != ParamCount)));

            StringBuilder sb = new StringBuilder();
            sb.Append(Name);
            if (parameters != null && parameters.Count != 0)
            {
                sb.Append("(");
                sb.Append(parameters[0]);
                for (int i = 1; i < parameters.Count; i++)
                {
                    sb.Append(",");
                    sb.Append(parameters[i]);
                }
                sb.Append(")");
            }
            return sb.ToString();
        }

        public override string ToString()
        {
            return string.Format("Pred ({0} {1})", Name, ParamCount);
        }
    }
}
