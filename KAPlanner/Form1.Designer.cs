namespace KAPlanner
{
    partial class KAPlanner
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KAPlanner));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.actionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cancelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtDomain = new System.Windows.Forms.TextBox();
            this.txtProblem = new System.Windows.Forms.TextBox();
            this.btnDomain = new System.Windows.Forms.Button();
            this.btnProblem = new System.Windows.Forms.Button();
            this.lblDomain = new System.Windows.Forms.Label();
            this.lblProblem = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnRun = new System.Windows.Forms.Button();
            this.txtStart = new System.Windows.Forms.TextBox();
            this.txtGoalS = new System.Windows.Forms.TextBox();
            this.txtGoal = new System.Windows.Forms.TextBox();
            this.txtPlan = new System.Windows.Forms.TextBox();
            this.lblStart = new System.Windows.Forms.Label();
            this.lblGoal = new System.Windows.Forms.Label();
            this.lblGoals = new System.Windows.Forms.Label();
            this.lblPlan = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.wrkPlanner = new System.ComponentModel.BackgroundWorker();
            this.btnCancel = new System.Windows.Forms.Button();
            this.progressRun = new System.Windows.Forms.ProgressBar();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.actionToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(719, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.toolStripSeparator,
            this.saveToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripMenuItem.Image")));
            this.newToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.newToolStripMenuItem.Text = "&New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripMenuItem.Image")));
            this.openToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(143, 6);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripMenuItem.Image")));
            this.saveToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(143, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // actionToolStripMenuItem
            // 
            this.actionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runToolStripMenuItem,
            this.cancelToolStripMenuItem});
            this.actionToolStripMenuItem.Name = "actionToolStripMenuItem";
            this.actionToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.actionToolStripMenuItem.Text = "Action";
            // 
            // runToolStripMenuItem
            // 
            this.runToolStripMenuItem.Name = "runToolStripMenuItem";
            this.runToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.runToolStripMenuItem.Text = "Run";
            this.runToolStripMenuItem.Click += new System.EventHandler(this.runToolStripMenuItem_Click);
            // 
            // cancelToolStripMenuItem
            // 
            this.cancelToolStripMenuItem.Enabled = false;
            this.cancelToolStripMenuItem.Name = "cancelToolStripMenuItem";
            this.cancelToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.cancelToolStripMenuItem.Text = "Cancel";
            this.cancelToolStripMenuItem.Click += new System.EventHandler(this.cancelToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.aboutToolStripMenuItem.Text = "&About...";
            // 
            // txtDomain
            // 
            this.txtDomain.Location = new System.Drawing.Point(12, 66);
            this.txtDomain.Name = "txtDomain";
            this.txtDomain.Size = new System.Drawing.Size(423, 20);
            this.txtDomain.TabIndex = 1;
            // 
            // txtProblem
            // 
            this.txtProblem.Location = new System.Drawing.Point(12, 117);
            this.txtProblem.Name = "txtProblem";
            this.txtProblem.Size = new System.Drawing.Size(423, 20);
            this.txtProblem.TabIndex = 2;
            // 
            // btnDomain
            // 
            this.btnDomain.Location = new System.Drawing.Point(462, 63);
            this.btnDomain.Name = "btnDomain";
            this.btnDomain.Size = new System.Drawing.Size(75, 23);
            this.btnDomain.TabIndex = 3;
            this.btnDomain.Text = "Browse";
            this.btnDomain.UseVisualStyleBackColor = true;
            this.btnDomain.Click += new System.EventHandler(this.btnDomain_Click);
            // 
            // btnProblem
            // 
            this.btnProblem.Location = new System.Drawing.Point(462, 114);
            this.btnProblem.Name = "btnProblem";
            this.btnProblem.Size = new System.Drawing.Size(75, 23);
            this.btnProblem.TabIndex = 4;
            this.btnProblem.Text = "Browse";
            this.btnProblem.UseVisualStyleBackColor = true;
            this.btnProblem.Click += new System.EventHandler(this.btnProblem_Click);
            // 
            // lblDomain
            // 
            this.lblDomain.AutoSize = true;
            this.lblDomain.Location = new System.Drawing.Point(12, 50);
            this.lblDomain.Name = "lblDomain";
            this.lblDomain.Size = new System.Drawing.Size(104, 13);
            this.lblDomain.TabIndex = 5;
            this.lblDomain.Text = "Domain specifier file:";
            // 
            // lblProblem
            // 
            this.lblProblem.AutoSize = true;
            this.lblProblem.Location = new System.Drawing.Point(12, 101);
            this.lblProblem.Name = "lblProblem";
            this.lblProblem.Size = new System.Drawing.Size(106, 13);
            this.lblProblem.TabIndex = 6;
            this.lblProblem.Text = "Problem specifier file:";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(605, 63);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(75, 23);
            this.btnRun.TabIndex = 7;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // txtStart
            // 
            this.txtStart.Location = new System.Drawing.Point(38, 195);
            this.txtStart.Multiline = true;
            this.txtStart.Name = "txtStart";
            this.txtStart.ReadOnly = true;
            this.txtStart.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtStart.Size = new System.Drawing.Size(100, 130);
            this.txtStart.TabIndex = 8;
            // 
            // txtGoalS
            // 
            this.txtGoalS.Location = new System.Drawing.Point(398, 195);
            this.txtGoalS.Multiline = true;
            this.txtGoalS.Name = "txtGoalS";
            this.txtGoalS.ReadOnly = true;
            this.txtGoalS.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtGoalS.Size = new System.Drawing.Size(100, 130);
            this.txtGoalS.TabIndex = 9;
            // 
            // txtGoal
            // 
            this.txtGoal.Location = new System.Drawing.Point(210, 195);
            this.txtGoal.Multiline = true;
            this.txtGoal.Name = "txtGoal";
            this.txtGoal.ReadOnly = true;
            this.txtGoal.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtGoal.Size = new System.Drawing.Size(100, 130);
            this.txtGoal.TabIndex = 10;
            // 
            // txtPlan
            // 
            this.txtPlan.Location = new System.Drawing.Point(580, 195);
            this.txtPlan.Multiline = true;
            this.txtPlan.Name = "txtPlan";
            this.txtPlan.ReadOnly = true;
            this.txtPlan.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtPlan.Size = new System.Drawing.Size(100, 130);
            this.txtPlan.TabIndex = 12;
            // 
            // lblStart
            // 
            this.lblStart.AutoSize = true;
            this.lblStart.Location = new System.Drawing.Point(35, 179);
            this.lblStart.Name = "lblStart";
            this.lblStart.Size = new System.Drawing.Size(58, 13);
            this.lblStart.TabIndex = 13;
            this.lblStart.Text = "Start state:";
            // 
            // lblGoal
            // 
            this.lblGoal.AutoSize = true;
            this.lblGoal.Location = new System.Drawing.Point(207, 179);
            this.lblGoal.Name = "lblGoal";
            this.lblGoal.Size = new System.Drawing.Size(84, 13);
            this.lblGoal.TabIndex = 14;
            this.lblGoal.Text = "Goal Conditions:";
            // 
            // lblGoals
            // 
            this.lblGoals.AutoSize = true;
            this.lblGoals.Location = new System.Drawing.Point(395, 179);
            this.lblGoals.Name = "lblGoals";
            this.lblGoals.Size = new System.Drawing.Size(58, 13);
            this.lblGoals.TabIndex = 15;
            this.lblGoals.Text = "Goal state:";
            // 
            // lblPlan
            // 
            this.lblPlan.AutoSize = true;
            this.lblPlan.Location = new System.Drawing.Point(577, 179);
            this.lblPlan.Name = "lblPlan";
            this.lblPlan.Size = new System.Drawing.Size(31, 13);
            this.lblPlan.TabIndex = 16;
            this.lblPlan.Text = "Plan:";
            // 
            // wrkPlanner
            // 
            this.wrkPlanner.WorkerSupportsCancellation = true;
            this.wrkPlanner.DoWork += new System.ComponentModel.DoWorkEventHandler(this.wrkPlanner_DoWork);
            this.wrkPlanner.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.wrkPlanner_RunWorkerCompleted);
            // 
            // btnCancel
            // 
            this.btnCancel.Enabled = false;
            this.btnCancel.Location = new System.Drawing.Point(605, 91);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 17;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // progressRun
            // 
            this.progressRun.Enabled = false;
            this.progressRun.Location = new System.Drawing.Point(580, 120);
            this.progressRun.Name = "progressRun";
            this.progressRun.Size = new System.Drawing.Size(132, 23);
            this.progressRun.TabIndex = 18;
            // 
            // KAPlanner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(719, 356);
            this.Controls.Add(this.progressRun);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblPlan);
            this.Controls.Add(this.lblGoals);
            this.Controls.Add(this.lblGoal);
            this.Controls.Add(this.lblStart);
            this.Controls.Add(this.txtPlan);
            this.Controls.Add(this.txtGoal);
            this.Controls.Add(this.txtGoalS);
            this.Controls.Add(this.txtStart);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.lblProblem);
            this.Controls.Add(this.lblDomain);
            this.Controls.Add(this.btnProblem);
            this.Controls.Add(this.btnDomain);
            this.Controls.Add(this.txtProblem);
            this.Controls.Add(this.txtDomain);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "KAPlanner";
            this.Text = "KA Planner";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.TextBox txtDomain;
        private System.Windows.Forms.TextBox txtProblem;
        private System.Windows.Forms.Button btnDomain;
        private System.Windows.Forms.Button btnProblem;
        private System.Windows.Forms.Label lblDomain;
        private System.Windows.Forms.Label lblProblem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.TextBox txtStart;
        private System.Windows.Forms.TextBox txtGoalS;
        private System.Windows.Forms.TextBox txtGoal;
        private System.Windows.Forms.TextBox txtPlan;
        private System.Windows.Forms.Label lblStart;
        private System.Windows.Forms.Label lblGoal;
        private System.Windows.Forms.Label lblGoals;
        private System.Windows.Forms.Label lblPlan;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem actionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker wrkPlanner;
        private System.Windows.Forms.ToolStripMenuItem cancelToolStripMenuItem;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ProgressBar progressRun;
    }
}

