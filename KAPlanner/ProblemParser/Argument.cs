using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProblemParser
{
    class Argument
    {
        public List<Predicate> PredList = new List<Predicate>();
        public List<List<string>> PredParams = new List<List<string>>();

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < PredList.Count; i++)
            {
                Predicate currPred = PredList[i];
                List<string> currParams = PredParams[i];

                sb.Append("{");
                sb.Append(currPred.Name);

                if( currParams.Count != 0 )
                {
                    sb.Append("(");
                    sb.Append(currParams[0]);
                    for (int j = 1; j < currParams.Count; j++)
                    {
                        sb.Append(",");
                        sb.Append(currParams[j]);
                    }
                    sb.Append(")");
                }
                sb.Append("}");

            }
            return sb.ToString();
        }

    }
}
