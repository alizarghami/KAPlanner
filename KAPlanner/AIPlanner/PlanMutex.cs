using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIPlanner
{
    class PlanMutex
    {
        //TODO: Correct these
        public HashSet<int> SignMutexes = new HashSet<int>();
        public Dictionary<int, HashSet<int>> OtherMutexes = new Dictionary<int, HashSet<int>>();

        public static bool InconsistentEffects(PlanAction act1, PlanAction act2)
        {
            // an effect of one negates an effect of other
            //TODO: Debug this
            return (act1.Effects.Positive.Overlaps(act2.Effects.Negative) ||
                act1.Effects.Negative.Overlaps(act2.Effects.Positive));
        }

        public static bool Interference(PlanAction act1, PlanAction act2)
        {
            // one deletes a precondition of the other
            //TODO: Debug this
            return (act1.Effects.Positive.Overlaps(act2.Preconds.Negative) ||
                act1.Effects.Negative.Overlaps(act2.Preconds.Positive) ||
                act2.Effects.Positive.Overlaps(act1.Preconds.Negative) ||
                act2.Effects.Negative.Overlaps(act1.Preconds.Positive));
        }

/*        public static bool CompetingNeeds(PlanAction act1, PlanAction act2)
        {
            // having mutually exclusive precondition
            //TODO: Debug this
            return (act1.Preconds.Positive.Overlaps(act2.Preconds.Positive) ||
                act1.Preconds.Negative.Overlaps(act2.Preconds.Negative));
        }
        */
        public static bool ActionsMutex(PlanAction act1, PlanAction act2)
        {
            return (InconsistentEffects(act1, act2) || Interference(act1, act2));
        }

        //public static bool InconsistentSupport(

    }
}
