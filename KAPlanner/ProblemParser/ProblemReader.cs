using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace ProblemParser
{
    class ProblemReader : FileParser
    {
        public ProblemReader(string filename, DataContainer data)
        {
            OpenFile(filename);
            Data = data;
        }

        public override void Parse()
        {
            string line;

            while ((line = ReadNewLine(false)) != null)
            {
                string type;
                int count;

                ParseStrInt(line, out type, out count);

                if (type == "objects")
                {
                    for (int i = 0; i < count; i++)
                        Data.Objects.Add(ReadNewLine());
                }
                else if (type == "initial-state")
                {
                    ParseState(ref Data.StartStates, count);
                }
                else if (type == "goals")
                {
                    ParseState(ref Data.GoalStates, count);
                }
                else
                    throw new InvalidOperationException("Unknown method type");

            }

            mFile.Close();
        }
        private void ParseState(ref List<DataContainer.State> statList, int count)
        {
            for (int i = 0; i < count; i++)
            {
                Predicate pred = Data.GetPredicateByName(ReadNewLine());
                Debug.Assert(pred != null);
                DataContainer.State stat = new DataContainer.State();
                stat.Pred = pred;
                for (int j = 0; j < pred.ParamCount; j++)
                {
                    stat.Objects.Add(ReadNewLine());
                }
                statList.Add(stat);
            }
        }

    }
}
