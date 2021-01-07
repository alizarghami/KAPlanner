using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace AIPlanner
{
    class PredicateList
    {
        #region Define a comparer for hash computations.
        public class PredicateListComparer : IEqualityComparer<PredicateList>
        {
            private static PredicateListComparer instance = new PredicateListComparer();
            private PredicateListComparer() { ;}

            public static PredicateListComparer Instance()
            {
                return instance;
            }

            public bool Equals(PredicateList left, PredicateList right)
            {
                return left.Positive.SetEquals(right.Positive) &&
                    left.Negative.SetEquals(right.Negative);
            }

            public int GetHashCode(PredicateList obj)
            {
                unchecked
                {
                    const int p = 16777619;
                    int hash = (int)2166136261;
                    foreach(int x in obj.Positive)
                        hash = (hash ^ x) * p;
                    foreach(int x in obj.Negative)
                        hash = (hash ^ x) * p;

                    hash += hash << 13;
                    hash ^= hash >> 7;
                    hash += hash << 3;
                    hash ^= hash >> 17;
                    hash += hash << 5;
                    return hash;
                }
            }
        }
        #endregion
        public System.Collections.Generic.HashSet<int> Positive;
        public System.Collections.Generic.HashSet<int> Negative;

        public ProblemParser.DataContainer DC = null;

        public PredicateList()
        {
            Positive = new System.Collections.Generic.HashSet<int>();
            Negative = new System.Collections.Generic.HashSet<int>();
        }
        public PredicateList(PredicateList other, ProblemParser.DataContainer dc)
        {
            Positive = new System.Collections.Generic.HashSet<int>(other.Positive);
            Negative = new System.Collections.Generic.HashSet<int>(other.Negative);
            DC = dc;
        }

        public bool IsSubsetOf(PredicateList other)
        {
            return Positive.IsSubsetOf(other.Positive) && Negative.IsSubsetOf(other.Negative);
        }

        public void Union(PredicateList other)
        {
            if (DC == null)
                DC = other.DC;
            Positive.UnionWith(other.Positive);
            Negative.UnionWith(other.Negative);
        }

        public void IntersectWith(PredicateList other)
        {
            Positive.IntersectWith(other.Positive);
            Negative.IntersectWith(other.Negative);
        }

        public void Clear()
        {
            Positive.Clear();
            Negative.Clear();
        }

        public override bool Equals(object obj)
        {
            return Object.ReferenceEquals(obj, this) || Positive.SetEquals(((PredicateList)obj).Positive);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hc = Positive.Count;

                foreach (int pred in Positive)
                {
                    hc = hc * 314159 + pred;
                }

                return hc;
            }
        }

        public bool Overlaps(PredicateList other)
        {
            return Positive.Overlaps(other.Positive) || Negative.Overlaps(other.Negative);
        }

        public override string ToString()
        {
            if( DC == null )
                return base.ToString();

            StringBuilder sb = new StringBuilder();

            if (Positive.Count > 0)
            {
 
                int i = 0;
                foreach (int pred in Positive)
                {
                    AddSinglePred(pred, ref sb);
                    i++;
                    if (i != Positive.Count)
                        sb.Append("\r\n");
                }
                sb.Append("\r\n");

            }

            if (Negative.Count > 0)
            {

                int i = 0;
                foreach (int pred in Negative)
                {
                    sb.Append("~");
                    AddSinglePred(pred, ref sb);
                    if (i != Negative.Count)
                        sb.Append("\r\n");
                }

            }

            return sb.ToString();
        }

        private void AddSinglePred(int pred, ref StringBuilder sb)
        {
            // Search for the right predicate candidate
            int pid = DC.Predicates.Count - 1;
            for (int i = 0; i < DC.Predicates.Count - 1; i++)
            {
                if (DC.Predicates[i].StartIndex <= pred &&
                    DC.Predicates[i + 1].StartIndex > pred)
                {
                    pid = i;
                    break;
                }
            }

            // assure it is a valid predicate
            Debug.Assert(pred <= DC.Predicates[DC.Predicates.Count - 1].StartIndex +
                DC.Predicates[DC.Predicates.Count - 1].Size);

            if (DC.Predicates[pid].ParamCount == 0)
            {
                sb.Append(DC.Predicates[pid].Name);
                return;
            }

            int linearIndex = pred - DC.Predicates[pid].StartIndex;
            List<int> lst = Enumerable.Range(0, DC.Predicates[pid].ParamCount).ToList();
            DC.GetMultiIndex(linearIndex, ref lst);

            sb.Append(DC.Predicates[pid].Name);
            sb.Append("(");
            sb.Append(DC.GetObjectString(lst[0]));

            for (int i = 1; i < lst.Count; i++)
            {
                sb.Append(", ");
                sb.Append(DC.GetObjectString(lst[i]));
            }
            sb.Append(")");
        }

    }
}
