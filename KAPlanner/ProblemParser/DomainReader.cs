using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace ProblemParser
{
    class DomainReader : FileParser
    {
        public DomainReader(string filename, DataContainer data)
        {
            OpenFile(filename);
            Data = data;
        }

        public override void Parse()
        {
            string line;
            bool predsInitialized = false;
            while ((line = ReadNewLine(false)) != null)
            {
                // assume a StrInt input for either predicates or operators
                string type;
                int count;

                ParseStrInt(line, out type, out count);

                if (type == "predicates")
                {
                    ParsePredicates(count);
                    predsInitialized = true;
                }
                else if (type == "operators")
                {
                    if (!predsInitialized)
                        throw new InvalidOperationException("Cannot parse Operators if Predicates are not initialized yet");
                    ParseOperators(count);
                }
                else
                    throw new InvalidOperationException("Unknown method type");
            }

            mFile.Close();
        }

        private void ParsePredicates(int predCount)
        {
            string name;
            int numArgs;
            for (int predNum = 0; predNum < predCount; predNum++)
            {
                ParseStrInt(ReadNewLine(), out name, out numArgs);
                Data.AddPredicate(new Predicate(name, numArgs));
            }
        }

        private void ParseOperators(int opCount)
        {
            for (int opNum = 0; opNum < opCount; opNum++)
            {
                Operator op = new Operator();
                string type;
                int count;

                op.Name = ReadNewLine();

                ParseStrInt(ReadNewLine(), out type, out count);
                // assume it is parameters
                Debug.Assert(type == "parameters");
                for (int i = 0; i < count; i++)
                    op.ParamNames.Add(ReadNewLine());

                ParseStrInt(ReadNewLine(), out type, out count);
                Debug.Assert(type == "preconditions");
                ReadSetOfPreds(ref op.Preconds, count);

                ParseStrInt(ReadNewLine(), out type, out count);
                Debug.Assert(type == "add-effects");
                ReadSetOfPreds(ref op.PosEffect, count);

                ParseStrInt(ReadNewLine(), out type, out count);
                Debug.Assert(type == "delete-effects");
                ReadSetOfPreds(ref op.NegEffect, count);

                Data.AddOperator(op);
            }
        }

        private void ReadPredicate(out Predicate pred, out List<string> predParams)
        {
            string precName = ReadNewLine();
            pred = Data.GetPredicateByName(precName);
            Debug.Assert(pred != null);
            predParams = new List<string>();
            for (int j = 0; j < pred.ParamCount; j++)
                predParams.Add(ReadNewLine());
        }

        private void ReadSetOfPreds(ref Argument arg, int count)
        {
            Predicate pred;
            List<string> lst;

            for (int i = 0; i < count; i++)
            {
                ReadPredicate(out pred, out lst);
                arg.PredList.Add(pred);
                arg.PredParams.Add(lst);
            }
        }

    }
}
