using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace AIPlanner
{
    class GraphPlan
    {
        #region Data type definitions
        public enum GraphSearchState
        {
            Unknown,
            ContinueSearch,
            GoalReachable,
            NoPossibleSolutions
        }

        class SMutex
        {
            public Dictionary<int, HashSet<int>> PosPos = new Dictionary<int,HashSet<int>>();
            //public Dictionary<int, HashSet<int>> PosNeg = new Dictionary<int,HashSet<int>>();
            //public Dictionary<int, HashSet<int>> NegPos = new Dictionary<int,HashSet<int>>();
            //public Dictionary<int, HashSet<int>> NegNeg = new Dictionary<int,HashSet<int>>();

            public SMutex() { ;}
            public SMutex(PredicateList pred)
            {
            }

            public void Clear()
            {
                PosPos.Clear();
            }

            public bool AddPredicate(PredicateList pred)
            {
                throw new NotImplementedException();
            }

            public bool PreconditionsMutex(PredicateList p1, PredicateList p2)
            {
                //NOTE: Only positive preconditions
                return CheckPrec(p1, p2) || CheckPrec(p2, p1);
            }

            private bool CheckPrec(PredicateList p1, PredicateList p2)
            {
                HashSet<int> hs;
                foreach (int p in p1.Positive)
                    if (PosPos.TryGetValue(p, out hs))
                        if (hs.Overlaps(p2.Positive))
                            return true;
                return false;
            }

            public void AddPair(int st1, int st2)
            {
                AddSingle(st1, st2);
                AddSingle(st2, st1);
            }

            private void AddSingle(int s1, int s2)
            {
                HashSet<int> ret;
                if (PosPos.TryGetValue(s1, out ret))
                {
                    ret.Add(s2);
                }
                else
                {
                    ret = new HashSet<int>();
                    ret.Add(s2);
                    PosPos.Add(s1, ret);
                }
            }

            public ProblemParser.DataContainer DC = null;

            public override string ToString()
            {
                if (DC == null)
                    return base.ToString();

                StringBuilder sb = new StringBuilder();
                PredicateList tmp = new PredicateList();
                tmp.DC = DC;

                HashSet<Tuple<int, int>> allMutexes = new HashSet<Tuple<int, int>>();

                foreach (int st1 in PosPos.Keys)
                {
                    HashSet<int> mu = PosPos[st1];
                    foreach (int st2 in mu)
                    {
                        Tuple<int, int> norm = new Tuple<int, int>(st1, st2), 
                            rev = new Tuple<int, int>(st2, st1);

                        if (!(allMutexes.Contains(norm) || allMutexes.Contains(rev)))
                            allMutexes.Add(norm);
                    }
                }

                foreach (Tuple<int, int> mutex in allMutexes)
                {
                    sb.Append("{");
                    sb.Append(DC.GetPredicateString(mutex.Item1));
                    sb.Append(", ");
                    sb.Append(DC.GetPredicateString(mutex.Item2));
                    sb.Append("}");
                }

                return sb.ToString();
            }
        }

        class AMutex
        {
            public Dictionary<PlanAction, HashSet<PlanAction>> mutex = new Dictionary<PlanAction, HashSet<PlanAction>>();
            public ProblemParser.DataContainer DC = null;

            public void Clear()
            {
                mutex.Clear();
            }

            public bool AreActionsMutex(PlanAction act1, PlanAction act2)
            {
                HashSet<PlanAction> hs;
                if (mutex.TryGetValue(act1, out hs))
                {
                    return hs.Contains(act2);
                }
                else
                {
                    // what if mutexes dont exists?
                    // BUG
                    throw new Exception("(GraphPlan.AMutex.AreActionsMutex) BUG");
                }
            }

            public void AddPair(PlanAction act1, PlanAction act2)
            {
                AddTo(act1, act2);
                AddTo(act2, act1);
            }

            private void AddTo(PlanAction act1, PlanAction act2)
            {
                HashSet<PlanAction> hs;
                if (mutex.TryGetValue(act1, out hs))
                {
                    hs.Add(act2);
                }
                else
                {
                    hs = new HashSet<PlanAction>();
                    hs.Add(act2);
                    mutex.Add(act1, hs);
                }
            }

            public override string ToString()
            {
                if (DC == null)
                    return base.ToString();

                HashSet<Tuple<PlanAction, PlanAction>> allMutexes = 
                    new HashSet<Tuple<PlanAction, PlanAction>>();
                StringBuilder sb = new StringBuilder();
                sb.Append("{");
                foreach(PlanAction a1 in mutex.Keys)
                    foreach (PlanAction a2 in mutex[a1])
                    {
                        Tuple<PlanAction, PlanAction> nrm = new Tuple<PlanAction, PlanAction>(a1, a2),
                            rev = new Tuple<PlanAction, PlanAction>(a2, a1);

                        if (!(allMutexes.Contains(nrm) || allMutexes.Contains(rev)))
                        {
                            allMutexes.Add(nrm);
                            sb.Append("{");
                            sb.Append(a1.ToString());
                            sb.Append(", ");
                            sb.Append(a2.ToString());
                            sb.Append("}");
                        }
                    }

                sb.Append("}");
                return sb.ToString();
            }
        }

        #endregion

        #region Data members

        private readonly ProbDef mProbDef;
        private HashSet<PlanAction> PrevActionLayer;
        private HashSet<PlanAction> CurrActionLayer;
        private PredicateList PrevStates;
        private PredicateList CurrStates;
        
        private HashSet<PlanAction> RemainingActions = new HashSet<PlanAction>();
        private HashSet<PlanAction> ExecutableActions;

        public GraphSearchState SearchState { get; private set; }
        public int NumLevelsExpanded { get; private set; }

        private SMutex StateMutexes = new SMutex();
        private SMutex PrevStateMutexes = new SMutex();
        private AMutex ActionMutexes = new AMutex();

//        private PredicateList NewPreds = new PredicateList();
//        private HashSet<PlanAction> NewActions = new HashSet<PlanAction>();

        #endregion

        #region Methods

        public GraphPlan(ProbDef pd)
        {
            mProbDef = pd;
            //Reset();
        }

        public int HeuristicValue
        {
            get
            {
                if (SearchState == GraphSearchState.GoalReachable)
                    return NumLevelsExpanded;
                else
                    return int.MaxValue;
            }
        }

        HashSet<int> U = new HashSet<int>();
        HashSet<int> PU = new HashSet<int>();

        public void Reset()
        {
            PrevStates =  new PredicateList();
            CurrStates = new PredicateList();
            PrevStates.DC = mProbDef.DC;
            CurrStates.DC = mProbDef.DC;
            PrevActionLayer = new HashSet<PlanAction>();
            CurrActionLayer = new HashSet<PlanAction>();

            RemainingActions.Clear();
            RemainingActions.UnionWith(mProbDef.Actions);
            NumLevelsExpanded = 0;
            SearchState = GraphSearchState.ContinueSearch;

            StateMutexes.Clear();
            PrevStateMutexes.Clear();
            StateMutexes.DC = mProbDef.DC;
            PrevStateMutexes.DC = mProbDef.DC;

            ActionMutexes.Clear();
            ActionMutexes.DC = mProbDef.DC;

            //NewActions.Clear();
            //NewPreds.Clear();
            //NewPreds.DC = mProbDef.DC;
            U.Clear();
            PU.Clear();
        }

        public void GenerateGraph(PredicateList startState)
        {
            Reset();
            CurrStates.Union(startState);

            CheckGoal();
            while (SearchState == GraphSearchState.ContinueSearch)
            {
                GenerateNextLevelStates();
                CheckGoal();
            }
        }

        void GenerateNextLevelStates()
        {
            PrevStates.Clear();
            PrevStates.Union(CurrStates);

            foreach (PlanAction act in ExecutableActions)
            {
                // do execute the action
                CurrStates = mProbDef.ExecuteAction(CurrStates, act, false, true);
            }

            //NewPreds.Clear();
            //NewPreds.Union(CurrStates);
            //NewPreds.IntersectWith(PrevStates);

            // now for the added predicates compute mutexes between themselves and previous predicates
            // but first, compute action mutexes. these are between executable actions and previousely
            // executed actions.

            //TODO:Think about prevactionlayer
            GenerateActionMutexes();
            GenerateStateMutexes();

            NumLevelsExpanded++;
        }

        private void GenerateActionMutexes()
        {
            //TODO: NOTE FOR OPTIMIZATION:
            // change this code to 
            // 1- only calc for actions that are newly added
            // 2- only for those whose preconds are no longer mutex
            ActionMutexes.Clear();
            PlanAction[] actions = CurrActionLayer.ToArray();

            for(int i = 0; i < actions.Length; i++)
                for (int j = i + 1; j < actions.Length; j++)
                {
                    if (mProbDef.MutexActions[actions[i]].Contains(actions[j]) ||
                        StateMutexes.PreconditionsMutex(actions[i].Preconds, actions[j].Preconds))
                    {
                        // add two actions as mutex
                        ActionMutexes.AddPair(actions[i], actions[j]);
                    }
                }
        }

        private void GenerateStateMutexes()
        {
            int[] posProp = CurrStates.Positive.ToArray();

            Swap(ref PrevStateMutexes, ref StateMutexes);
            StateMutexes.Clear();

            PU.Clear();
            PU.UnionWith(U);
            U.Clear();

            for(int i = 0; i < posProp.Length; i++)
                for (int j = i + 1; j < posProp.Length; j++)
                {
                    // Each action leading to these props should be mutex
                    int prop1 = posProp[i], prop2 = posProp[j];
                    HashSet<PlanAction> actSet1 = new HashSet<PlanAction>(mProbDef.GetActionsProducingPositiveEffect(prop1));
                    HashSet<PlanAction> actSet2 = new HashSet<PlanAction>(mProbDef.GetActionsProducingPositiveEffect(prop2));

                    bool prop1noop = PrevStates.Positive.Contains(prop1);
                    bool prop2noop = PrevStates.Positive.Contains(prop2);

                    // get only those that are executed up until now
                    actSet1.IntersectWith(CurrActionLayer);
                    actSet2.IntersectWith(CurrActionLayer);

                    //TODO: Check if this is true.
                    // if there exists an action that produces both preds then they are not mutex
                    if (actSet1.Overlaps(actSet2))
                        break;

                    // Check if there exists two actions that are not mutex and can generate both states.
                    bool allMutex = true;

                    // If there was no action generating the prop (prop exists from start state)
                    // then they should be considered mutex if the noop operation are mutex
                    // but this cannot happen unless we have faulty start state. 
                    if (actSet1.Count != 0 && actSet2.Count != 0)
                    {
                        foreach (PlanAction act1 in actSet1)
                        {
                            if (allMutex == false)
                                break;
                            foreach (PlanAction act2 in actSet2)
                            {
                                if (!ActionMutexes.AreActionsMutex(act1, act2))
                                {
                                    allMutex = false;
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        //allMutex = false;
                    }

                    if (allMutex)
                    {
                        if (prop1noop && prop2noop)
                        {
                            // First check if both noops can be non-mutex.
                            // Can only be non-mutex if the they were non mutex in prev state
                            HashSet<int> hs;
                            if (!(PrevStateMutexes.PosPos.TryGetValue(prop1, out hs) && hs.Contains(prop2)))
                                allMutex = false;
                        }

                        if (allMutex)
                            if (prop1noop)
                                allMutex = NoopActionMutexCheck(prop1, actSet2);
                            else if (prop2noop)
                                allMutex = NoopActionMutexCheck(prop2, actSet1);
                    }

                    if (allMutex)
                    {
                        Debug.WriteLine("Mutex {" + mProbDef.DC.GetPredicateString(prop1) + ", " +
                            mProbDef.DC.GetPredicateString(prop2));

                        if (mProbDef.GoalState.Positive.Contains(prop1) && mProbDef.GoalState.Positive.Contains(prop2))
                        {
                            U.Add(prop1);
                            U.Add(prop2);
                        }
                        StateMutexes.AddPair(prop1, prop2);
                    }
                }
        }

        private bool NoopActionMutexCheck(int currPropNoop, HashSet<PlanAction> otherActs)
        {
            // Check if noop of currPropNoop is mutex to any action creating prop2.
            // This can be the case if in prev state, prop1 is not mutex to any precond
            // of any actions that can result in prop2; or if an action negates the proposition.
            HashSet<int> hs;
            if (PrevStateMutexes.PosPos.TryGetValue(currPropNoop, out hs))
            {
                foreach (PlanAction act in otherActs)
                {
                    if (!hs.Overlaps(act.Preconds.Positive))
                        return false;
                }
            }

            foreach (PlanAction act in otherActs)
            {
                // Check if action negates the proposition; if not, then there is an action
                // which is not mutex to this noop.
                if (!act.Effects.Negative.Contains(currPropNoop))
                    return false;
            }

            return true;
        }

        private bool AreGoalsMutex()
        {
            // goals are positive
            foreach (int prop in mProbDef.GoalState.Positive)
            {
                HashSet<int> hs;
                if (StateMutexes.PosPos.TryGetValue(prop, out hs) && hs.Overlaps(mProbDef.GoalState.Positive))
                    return true;
            }
            return false;
        }

        private void CheckGoal()
        {
            bool bGoalFound = mProbDef.GoalState.IsSubsetOf(CurrStates);
            ExecutableActions = mProbDef.GetExecutableActions(CurrStates, RemainingActions);
            RemainingActions.ExceptWith(ExecutableActions);

            PrevActionLayer.Clear();
            PrevActionLayer.UnionWith(CurrActionLayer);
            CurrActionLayer.UnionWith(ExecutableActions);

            if (bGoalFound)
                if (AreGoalsMutex())
                    if( U.SetEquals(PU) && U.Count != 0)
                        SearchState = GraphSearchState.NoPossibleSolutions;
                    else
                        SearchState = GraphSearchState.ContinueSearch;
                else
                    SearchState = GraphSearchState.GoalReachable;
            else
                if (ExecutableActions.Count == 0)
                    SearchState = GraphSearchState.NoPossibleSolutions;
                else
                    SearchState = GraphSearchState.ContinueSearch;
        }

        static void Swap<T>(ref T lhs, ref T rhs)
        {
            T temp;
            temp = lhs;
            lhs = rhs;
            rhs = temp;
        }

        #endregion
    }
}
