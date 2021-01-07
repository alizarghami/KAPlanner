using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIPlanner
{
    class PlanAction
    {
        public PredicateList Effects = new PredicateList();
        public PredicateList Preconds = new PredicateList();
        //public int ID;

        public class ID
        {
            public string ActionName;
            public int ParamCount;
            public ProblemParser.DataContainer DC;
        }

        private PlanAction.ID internalID = null;
        public PlanAction.ID ActionID
        {
            get
            {
                return internalID;
            }
            set
            {
                internalID = value;
                Effects.DC = internalID.DC;
                Preconds.DC = internalID.DC;
            }
        }
        
        public int ParametersIndex;

        public override string ToString()
        {
            if (ActionID == null)
                return base.ToString();

            string actName = this.ActionID.ActionName;

            if (this.ActionID.ParamCount == 0)
                return actName;

            StringBuilder sb = new StringBuilder();
            int actParamIndex = this.ParametersIndex;
            List<int> lstParams = Enumerable.Range(0, this.ActionID.ParamCount).ToList();

            ActionID.DC.GetMultiIndex(actParamIndex, ref lstParams);

            // now build action string
            sb.Append(actName);
            //sb.Append("(");
            sb.Append(" ");
            sb.Append(ActionID.DC.Objects[lstParams[0]]);

            for (int i = 1; i < lstParams.Count; i++)
            {
                //sb.Append(", ");
                sb.Append(" ");
                sb.Append(ActionID.DC.GetObjectString(lstParams[i]));
            }
            //sb.Append(")");

            return sb.ToString();

        }
    }
}
