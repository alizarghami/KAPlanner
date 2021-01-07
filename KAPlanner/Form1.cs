using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using ProblemParser;
using AIPlanner;

namespace KAPlanner
{
    public partial class KAPlanner : Form
    {
        private string strDomain = "";
        private string strProblem = "";
        private string strPlan = "";

        public KAPlanner()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void Hello(HashSet<int> a)
        {
            a.Add(2);
        }

        # region Main Codes !!!

        string strGoalStateTxt;
        int N = 0;

        private Dictionary<PredicateList, int> mPredsCache = new Dictionary<PredicateList, int>();

        private void StartRun()
        {
            string[] strArgs = new string[2];
            strArgs[0] = strDomain;
            strArgs[1] = strProblem;

            //Cursor.Current = Cursors.WaitCursor;
            //Application.DoEvents();

            Application.UseWaitCursor = true;

            mPredsCache.Clear();

            while (wrkPlanner.IsBusy) ;

            wrkPlanner.RunWorkerAsync(strArgs);

            btnRun.Enabled = false;
            runToolStripMenuItem.Enabled = false;
            btnCancel.Enabled = true;
            cancelToolStripMenuItem.Enabled = true;

            progressRun.Style = ProgressBarStyle.Marquee;
        }

        private void CancelRun()
        {
            wrkPlanner.CancelAsync();
            btnRun.Enabled = true;
            runToolStripMenuItem.Enabled = true;
            btnCancel.Enabled = false;
            btnCancel.Enabled = false;

            Application.UseWaitCursor = false;
            progressRun.Style = ProgressBarStyle.Blocks;
        }

        private string forwardSearch(ProbDef prob, PredicateList currState, Stack<PredicateList> visited, GraphPlan gp)
        {
            List<PlanAction> lstActions = prob.GetExecutableActions(currState).ToList();
            string strAnswer = "fail";
            List<int> scores = new List<int>();
            PredicateList bestState = null;
            PlanAction bestAction = null;

            N++;    // counter

            if (goalcheck(currState, prob.GoalState))
            {
                strAnswer = "";
                strGoalStateTxt = currState.ToString();
                N--;    // Counter
                return (strAnswer);                // Return success!!!
            }
            foreach (PredicateList state in visited)
            {
                if (stateEquality(currState, state))
                {
                    N--;    // Counter
                    return (strAnswer);               // Failure for looP !!!
                }
            }


            foreach (PlanAction act in lstActions)
            {
                if (wrkPlanner.CancellationPending)
                    return "cancel";

                PredicateList nextState = prob.ExecuteAction(currState, act);

                int heuristic;
                if (!mPredsCache.TryGetValue(nextState, out heuristic))
                {
                    gp.GenerateGraph(nextState);
                    heuristic = gp.HeuristicValue;
                    mPredsCache.Add(nextState, heuristic);
                }

                scores.Add(heuristic);
            }

            while (strAnswer.Equals("fail"))
            {
                if (scores.Min() < int.MaxValue)
                {
                    int tempind = scores.IndexOf(scores.Min());
                    scores[tempind] = int.MaxValue;
                    bestAction = lstActions[tempind];
                    bestState = prob.ExecuteAction(currState, bestAction);
                    visited.Push(currState);
                    strAnswer = forwardSearch(prob, bestState, visited, gp);
                    visited.Pop();
                }
                else
                {
                    N--;    //Counter
                    return (strAnswer); // failure !!!   Backtracking needed!
                }
            }

            strAnswer = N.ToString() + ": [" + bestAction.ToString() + "]\r\n" + strAnswer;
            N--;    // Counter
            return (strAnswer);  // Success !!!

        }


        private bool goalcheck(PredicateList currState, PredicateList goal)
        {

            HashSet<int> inter = goal.Negative;
            inter.IntersectWith(currState.Positive);

            if (goal.Positive.IsSubsetOf(currState.Positive) && inter.Count() == 0)
            {
                return (true);
            }
            else
            {
                return (false);
            }

        }


        private bool stateEquality(PredicateList state1, PredicateList state2)
        {

            if (state1.Positive.IsSubsetOf(state2.Positive) && state2.Positive.IsSubsetOf(state1.Positive))
            {
                return (true);
            }
            else
            {
                return (false);
            }
        }

        # endregion

        # region Menu Codes Here

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Text files (*.txt) |*.txt|" + " All files (*.*) |*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.Title = "Open Domain file";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                strDomain = openFileDialog1.FileName;
                txtDomain.Text = strDomain;
            }

            openFileDialog1.Filter = "Text files (*.txt) |*.txt|" + " All files (*.*) |*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.Title = "Open Problem file";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                strProblem = openFileDialog1.FileName;
                txtProblem.Text = strProblem;
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            strDomain = "";
            strProblem = "";
            txtDomain.Text = "";
            txtProblem.Text = "";
            txtPlan.Text = "";
            txtGoalS.Text = "";
            txtGoal.Text = "";
            txtStart.Text = "";
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.DefaultExt = "txt";
            saveFileDialog1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.OverwritePrompt = true;
            saveFileDialog1.Title = "Save";
            
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {

                System.IO.File.WriteAllText(saveFileDialog1.FileName, strPlan);
            }

        }

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartRun();
        }

        private void cancelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CancelRun();
        }

        # endregion

        # region Button codes

        private void btnDomain_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Text files (*.txt) |*.txt|" + " All files (*.*) |*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.Title = "Open Domain file";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                strDomain = openFileDialog1.FileName;
                txtDomain.Text = strDomain;
            }
        }

        private void btnProblem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Text files (*.txt) |*.txt|" + " All files (*.*) |*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.Title = "Open Problem file";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                strProblem = openFileDialog1.FileName;
                txtProblem.Text = strProblem;
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            StartRun();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            CancelRun();
        }

        # endregion



        private void wrkPlanner_DoWork(object sender, DoWorkEventArgs e)
        {
            string[] args = e.Argument as string[];

            try
            {
                DataContainer dc = new DataContainer();
                DomainReader domain = new DomainReader(args[0], dc);
                ProblemReader problem = new ProblemReader(args[1], dc);
                domain.Parse();
                problem.Parse();
                dc.PostProcess();

                ProbDef prob = new ProbDef(dc);

                GraphPlan gp = new GraphPlan(prob);

                string myplan = "fail";
                Stack<PredicateList> visited = new Stack<PredicateList>();

                PredicateList currState = prob.StartState;

                List<PlanAction> lstAnswer = new List<PlanAction>();

                myplan = forwardSearch(prob, currState, visited, gp);

                if (wrkPlanner.CancellationPending)
                {
                    
                    e.Cancel = true;
                    
                }

                string[] arr = new string[3];

                arr[0] = myplan;
                arr[1] = prob.StartState.ToString();
                arr[2] = prob.GoalState.ToString();
                e.Result = arr;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void wrkPlanner_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                return;
            }

            if (e.Result == null)
                return;

            string[] res = e.Result as string[];

            txtPlan.Text = res[0];
            strPlan = res[0];
            txtStart.Text = res[1];
            txtGoal.Text = res[2];
            txtGoalS.Text = strGoalStateTxt;

            btnRun.Enabled = true;
            runToolStripMenuItem.Enabled = true;
            btnCancel.Enabled = false;
            cancelToolStripMenuItem.Enabled = false;

            Application.UseWaitCursor = false;
            progressRun.Style = ProgressBarStyle.Blocks;
        }

    }
}
