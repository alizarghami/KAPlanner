using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProblemParser
{
    class Operator
    {
        public string Name;
        public List<string> ParamNames = new List<string>();

        public Dictionary<string, int> ParamNameHash = null;

        public Argument Preconds = new Argument();
        public Argument PosEffect = new Argument();
        public Argument NegEffect = new Argument();


        public void ComputeHash()
        {
            if (ParamNameHash != null)
                return;
            ParamNameHash = new Dictionary<string,int>();
            for (int i = 0; i < ParamNames.Count; i++)
                ParamNameHash.Add(ParamNames[i], i);
        }

        private void AddArg(Argument arg, Predicate pred, List<string> args)
        {
            arg.PredList.Add(pred);
            arg.PredParams.Add(args);
        }

        public void AddPrecondition(Predicate pred, List<string> args)
        {
            AddArg(Preconds, pred, args);
        }

        public void AddPosEffect(Predicate pred, List<string> args)
        {
            AddArg(PosEffect, pred, args);
        }

        public void AddNegEffect(Predicate pred, List<string> args)
        {
            AddArg(NegEffect, pred, args);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Format("Operator {0} :", Name));
            sb.AppendLine("Parameters:");
            foreach (string str in this.ParamNames)
            {
                sb.AppendLine(string.Format("\t {0}", str));
            }

            sb.AppendLine("Preconditions:");
            sb.AppendLine(Preconds.ToString());
            sb.AppendLine("Positive effects:");
            sb.AppendLine(PosEffect.ToString());
            sb.AppendLine("Negative effects:");
            sb.AppendLine(NegEffect.ToString());

            return sb.ToString();
        }

    }
}
