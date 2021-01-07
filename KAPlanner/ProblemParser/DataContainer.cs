using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace ProblemParser
{
    class DataContainer
    {
        // these are defined in domain file.
        public List<Predicate> Predicates = new List<Predicate>();
        public List<Operator> Operators = new List<Operator>();
        public Dictionary<string, Predicate> PredicateHash = new Dictionary<string, Predicate>();
        public Dictionary<string, Operator> OperatorHash = new Dictionary<string, Operator>();
        //these are defined in problem file.
        public class State
        {
            public Predicate Pred;
            public List<string> Objects = new List<string>();
        }
        public List<string> Objects = new List<string>();
        public List<State> StartStates = new List<State>();
        public List<State> GoalStates = new List<State>();

        // now these are for post processing
        public Dictionary<string, int> ObjectID = new Dictionary<string, int>();

        public int GetLinearIndex(int pred, List<int> idx)
        {
            int index = 0;
            Debug.Assert(idx.Count == Predicates[pred].ParamCount);
            for (int i = 0; i < idx.Count; i++)
                index = index * Objects.Count + idx[i];

            return index + Predicates[pred].StartIndex;
        }

        public int GetMaxIndexForSize(int listSize)
        {
            return (int)Math.Pow((double)Objects.Count, (double)listSize);
        }

        public void GetMultiIndex(int linear, ref List<int> lst)
        {
            int dim = lst.Count;

            for (int i = dim - 1; i >= 0; i--)
            {
                lst[i] = linear % Objects.Count;
                linear = linear / Objects.Count;
            }
        }

        public void PostProcess()
        {
            for (int i = 0; i < Objects.Count; i++)
                ObjectID.Add(Objects[i], i);

            int startIndex = 0;
            for(int i = 0; i < Predicates.Count; i++)
            {
                Predicate pred = Predicates[i];
                pred.StartIndex = startIndex;
                pred.Size = (int)Math.Pow((double)Objects.Count, (double)pred.ParamCount);
                startIndex += pred.Size;
            }
        }

        public Predicate GetPredicateByName(string name)
        {
            Predicate ret;
            if (PredicateHash.TryGetValue(name, out ret))
                return ret;
            else
                return null;
        }

        public Operator GetOperatorByName(string name)
        {
            Operator ret;
            if (OperatorHash.TryGetValue(name, out ret))
                return ret;
            else
                return null;
        }

        public void AddPredicate(Predicate pred)
        {
            Debug.Assert(GetPredicateByName(pred.Name) == null);
            Predicates.Add(pred);
            PredicateHash.Add(pred.Name, pred);
            pred.PredID = Predicates.Count - 1;
        }

        public void AddOperator(Operator op)
        {
            Debug.Assert(GetOperatorByName(op.Name) == null);
            Operators.Add(op);
            OperatorHash.Add(op.Name, op);
        }

        public string GetObjectString(int oid)
        {
            return Objects[oid];
        }

        public string GetPredicateString(int pred)
        {
            StringBuilder sb = new StringBuilder();
            int pid = Predicates.Count - 1;
            for (int i = 0; i < Predicates.Count - 1; i++)
            {
                if (Predicates[i].StartIndex <= pred &&
                    Predicates[i + 1].StartIndex > pred)
                {
                    pid = i;
                    break;
                }
            }

            // assure it is a valid predicate
            Debug.Assert(pred <= Predicates[Predicates.Count - 1].StartIndex +
                Predicates[Predicates.Count - 1].Size);

            if (Predicates[pid].ParamCount == 0)
            {
                sb.Append(Predicates[pid].Name);
                return sb.ToString();
            }

            int linearIndex = pred - Predicates[pid].StartIndex;
            List<int> lst = Enumerable.Range(0, Predicates[pid].ParamCount).ToList();
            GetMultiIndex(linearIndex, ref lst);

            sb.Append(Predicates[pid].Name);
            sb.Append("(");
            sb.Append(GetObjectString(lst[0]));

            for (int i = 1; i < lst.Count; i++)
            {
                sb.Append(", ");
                sb.Append(GetObjectString(lst[i]));
            }
            sb.Append(")");

            return sb.ToString();
        }

#if false
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            // first print predicates
            sb.AppendLine("============   Predicates ============");
            foreach (Predicate pred in this.Predicates)
                sb.AppendLine(pred.ToString());

            sb.AppendLine("============   Operators =============");
            foreach (Operator op in this.Operators)
                sb.AppendLine(op.ToString());

            sb.AppendLine("============ Problem definition ============");
            sb.AppendLine("============ Objects=================");
            foreach (string str in Objects)
                sb.AppendLine(str);

            sb.AppendLine("============ Start State ============");
            foreach (State stat in StartStates)
            {
                sb.AppendLine(stat.ToString());
            }
            
            sb.AppendLine("============ Goal State =============");
            foreach(State stat in GoalStates)
            {
                sb.AppendLine(stat.ToString());
            }

            return sb.ToString();
        }
#endif
    }
}
