using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace AIPlanner
{
    class ProbDef
    {
        #region Data Members

        public List<PlanAction> Actions = new List<PlanAction>();
        public PredicateList StartState = new PredicateList();
        public PredicateList GoalState = new PredicateList();
        // this gives actions that are definitely mutex
        public Dictionary<PlanAction, HashSet<PlanAction>> MutexActions;
        public Dictionary<int, HashSet<PlanAction>> PositiveProducers;
        public Dictionary<int, HashSet<PlanAction>> NegativeProducers;
        private ProblemParser.DataContainer mDC;

        #endregion

        public ProblemParser.DataContainer DC { get { return mDC; } }

        public ProbDef(ProblemParser.DataContainer dc)
        {
            mDC = dc;

            StartState.DC = dc;
            GoalState.DC = dc;

            GenerateAllGrounds();
        }

        #region Ground Actions Operations

        [Conditional("DEBUG")]
        private void DoSafegaurds()
        {
            foreach (PlanAction act in Actions)
            {
                Debug.Assert(!act.Effects.Positive.Overlaps(act.Preconds.Positive) &&
                    !act.Effects.Negative.Overlaps(act.Preconds.Negative));
                Debug.Assert(!act.Effects.Negative.Overlaps(act.Effects.Positive) &&
                    !act.Preconds.Positive.Overlaps(act.Preconds.Negative));
            }
        }

        public bool IsActionExecutable(PredicateList state, int actID)
        {
            return IsActionExecutable(state, this.Actions[actID]);
        }

        public bool IsActionExecutable(PredicateList state, PlanAction act)
        {
            return act.Preconds.Positive.IsSubsetOf(state.Positive);
        }

        public HashSet<PlanAction> GetExecutableActions(PredicateList state)
        {
            HashSet<PlanAction> hs = new HashSet<PlanAction>(this.Actions);
            return GetExecutableActions(state, hs);
        }

        public HashSet<PlanAction> GetExecutableActions(PredicateList state, HashSet<PlanAction> allowedActs)
        {
            HashSet<PlanAction> lst = new HashSet<PlanAction>();
            foreach (PlanAction act in allowedActs)
                if (IsActionExecutable(state, act))
                    lst.Add(act);
            return lst;
        }

        public PredicateList ExecuteAction(PredicateList currState, PlanAction act, bool remove = true, bool inplace = false)
        {
            Debug.Assert(IsActionExecutable(currState, act));
            PredicateList nextState;
            if (inplace)
                nextState = currState;
            else
                nextState = new PredicateList(currState, mDC);

            if (remove)
            {
                nextState.Positive.ExceptWith(act.Effects.Negative);
                nextState.Negative.ExceptWith(act.Effects.Positive);
            }
            nextState.Positive.UnionWith(act.Effects.Positive);
            nextState.Negative.UnionWith(act.Effects.Negative);

            return nextState;
        }

        private void Initialize()
        {
            MutexActions = new Dictionary<PlanAction, HashSet<PlanAction>>(Actions.Count);
            PositiveProducers = new Dictionary<int, HashSet<PlanAction>>();
            NegativeProducers = new Dictionary<int, HashSet<PlanAction>>();
            // Initialize action mutexes
            for (int i = 0; i < Actions.Count; i++)
            {
                PlanAction act1 = Actions[i];
                if (!MutexActions.ContainsKey(act1))
                    MutexActions.Add(act1, new HashSet<PlanAction>());
                for (int j = i + 1; j < Actions.Count; j++)
                {
                    PlanAction act2 = Actions[j];
                    if (!MutexActions.ContainsKey(act2))
                        MutexActions.Add(act2, new HashSet<PlanAction>());

                    if (PlanMutex.ActionsMutex(act1, act2))
                    {
                        MutexActions[act1].Add(act2);
                        MutexActions[act2].Add(act1);
                    }
                }
            }

        }

        public HashSet<PlanAction> GetActionsProducingPositiveEffect(int effect)
        {
            if (PositiveProducers.ContainsKey(effect))
                return PositiveProducers[effect];
            HashSet<PlanAction> ret = new HashSet<PlanAction>();

            foreach (PlanAction act in Actions)
                if (act.Effects.Positive.Contains(effect))
                    ret.Add(act);

            PositiveProducers.Add(effect, ret);
            return ret;
        }

        public HashSet<PlanAction> GetActionsProducingNegativeEffect(int effect)
        {
            if (NegativeProducers.ContainsKey(effect))
                return NegativeProducers[effect];
            HashSet<PlanAction> ret = new HashSet<PlanAction>();

            foreach (PlanAction act in Actions)
                if (act.Effects.Negative.Contains(effect))
                    ret.Add(act);

            NegativeProducers.Add(effect, ret);
            return ret;
        }

        #endregion


        #region Ground Actions Generation

        private void GenerateAllGrounds()
        {
            GenerateGroundPredicates();
            GenerateGroundActions();
            Initialize();
        }

        private void GenerateGroundPredicates()
        {
            // Only ground terms that are used will be generated.
            GenerateGroundStates(mDC.StartStates, ref StartState);
            GenerateGroundStates(mDC.GoalStates, ref GoalState);
        }

        private void GenerateGroundStates(List<ProblemParser.DataContainer.State> statList,
            ref PredicateList output)
        {
            foreach (ProblemParser.DataContainer.State stat in statList)
            {
                List<int> objectIds = new List<int>(stat.Objects.Count);
                for (int i = 0; i < stat.Objects.Count; i++)
                    objectIds.Add(mDC.ObjectID[stat.Objects[i]]);
                int predID = stat.Pred.PredID;

                int finalPredID = mDC.GetLinearIndex(predID, objectIds);

                output.Positive.Add(finalPredID);
            }

            output.DC = mDC;
        }

        private void GenerateGroundActions()
        {
            //now for each action, generate all possible argument lists and then generate an id.
            foreach (ProblemParser.Operator op in mDC.Operators)
            {
                // now for all possible arguments:
                op.ComputeHash();
                int argCount = op.ParamNames.Count;
                //TODO: Check this
                List<int> argList = Enumerable.Repeat(0, argCount).ToList<int>();
                int maxParamLinIndex = mDC.GetMaxIndexForSize(argCount);

                PlanAction.ID actID = new PlanAction.ID();
                actID.ActionName = op.Name;
                actID.ParamCount = op.ParamNames.Count;
                actID.DC = mDC;

                for (int paramLinearIndex = 0; paramLinearIndex < maxParamLinIndex; 
                    paramLinearIndex++)
                {
                    mDC.GetMultiIndex(paramLinearIndex, ref argList);
                    // now make them ground!
                    PlanAction act = GenerateSingleGroundAction(op, argList);
                    act.ActionID = actID;
                    act.ParametersIndex = paramLinearIndex;

                    Actions.Add(act);
                }
                
            }
        }

        private PlanAction GenerateSingleGroundAction(ProblemParser.Operator op, List<int> argList)
        {
            PlanAction action = new PlanAction();
            GenerateGroundActionArgs(op, op.Preconds, argList, ref action.Preconds.Positive);
            GenerateGroundActionArgs(op, op.PosEffect, argList, ref action.Effects.Positive);
            GenerateGroundActionArgs(op, op.NegEffect, argList, ref action.Effects.Negative);
            return action;
        }

        private void GenerateGroundActionArgs(ProblemParser.Operator op, 
            ProblemParser.Argument arg, List<int> argList, ref HashSet<int> groundPredicates)
        {
            List<int> idxs = new List<int>();
            for (int i = 0; i < arg.PredList.Count; i++)
            {
                int predID = arg.PredList[i].PredID;

                idxs.Clear();
                List<string> currPredParamList = arg.PredParams[i];
                // translate each predicate param to action param index.
                for (int j = 0; j < currPredParamList.Count; j++)
                    idxs.Add(op.ParamNameHash[currPredParamList[j]]);
                // set value for each predicate param according to its value in calling action.
                for (int j = 0; j < idxs.Count; j++)
                    idxs[j] = argList[idxs[j]];

                //now we have arguments for a precondition predicate, so make it ground!
                int predGroundID = mDC.GetLinearIndex(predID, idxs);

                groundPredicates.Add(predGroundID);
            }
        }

        #endregion
    }
}
