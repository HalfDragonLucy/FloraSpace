namespace FloraSpace.GUI
{
    partial class GUI
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            flowLayoutPanel1 = new FlowLayoutPanel();
            BtnTempFiles = new Button();
            statusStrip1 = new StatusStrip();
            CoreProgress = new ToolStripProgressBar();
            VersionLabel = new ToolStripStatusLabel();
            BtnUpdateProgram = new ToolStripStatusLabel();
            toolTip1 = new ToolTip(components);
            flowLayoutPanel1.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AccessibleRole = AccessibleRole.None;
            flowLayoutPanel1.Controls.Add(BtnTempFiles);
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.Location = new Point(0, 0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(537, 169);
            flowLayoutPanel1.TabIndex = 2;
            // 
            // BtnTempFiles
            // 
            BtnTempFiles.AccessibleDescription = "Click this button to initiate the process of clearing temporary files in Windows, which can free up disk space and improve system performance.";
            BtnTempFiles.AccessibleName = "Button: Clear Temporary Files";
            BtnTempFiles.AccessibleRole = AccessibleRole.ButtonMenu;
            BtnTempFiles.AutoEllipsis = true;
            BtnTempFiles.BackgroundImageLayout = ImageLayout.None;
            BtnTempFiles.Cursor = Cursors.Hand;
            BtnTempFiles.FlatAppearance.BorderColor = Color.Black;
            BtnTempFiles.FlatStyle = FlatStyle.Flat;
            BtnTempFiles.Location = new Point(3, 3);
            BtnTempFiles.Name = "BtnTempFiles";
            BtnTempFiles.Size = new Size(149, 37);
            BtnTempFiles.TabIndex = 0;
            BtnTempFiles.Text = "Temporary Files";
            toolTip1.SetToolTip(BtnTempFiles, "Click this button to initiate the process of clearing temporary files in Windows, which can free up disk space and improve system performance.");
            BtnTempFiles.UseVisualStyleBackColor = true;
            BtnTempFiles.Click += BtnTempFiles_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.AccessibleRole = AccessibleRole.None;
            statusStrip1.Items.AddRange(new ToolStripItem[] { CoreProgress, VersionLabel, BtnUpdateProgram });
            statusStrip1.Location = new Point(0, 169);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(537, 22);
            statusStrip1.SizingGrip = false;
            statusStrip1.TabIndex = 3;
            statusStrip1.Text = "statusStrip1";
            // 
            // CoreProgress
            // 
            CoreProgress.AccessibleDescription = "This progress bar visually represents the current progress of tasks or operations within the program. It provides important feedback on the status of ongoing work.";
            CoreProgress.AccessibleName = "ProgressBar: Show Progress";
            CoreProgress.AccessibleRole = AccessibleRole.ProgressBar;
            CoreProgress.AutoToolTip = true;
            CoreProgress.Name = "CoreProgress";
            CoreProgress.Size = new Size(100, 16);
            CoreProgress.Style = ProgressBarStyle.Continuous;
            CoreProgress.ToolTipText = "This progress bar visually represents the current progress of tasks or operations within the program. It provides important feedback on the status of ongoing work.";
            // 
            // VersionLabel
            // 
            VersionLabel.AccessibleDescription = "This label displays the current version of the program, which is important for tracking software updates and compatibility.";
            VersionLabel.AccessibleName = "Label: Program Version";
            VersionLabel.AccessibleRole = AccessibleRole.Text;
            VersionLabel.AutoToolTip = true;
            VersionLabel.Name = "VersionLabel";
            VersionLabel.Size = new Size(56, 17);
            VersionLabel.Text = "Version: ?";
            VersionLabel.ToolTipText = "This label displays the current version of the program, which is important for tracking software updates and compatibility.";
            // 
            // BtnUpdateProgram
            // 
            BtnUpdateProgram.AccessibleDescription = "Click this button to upgrade to the latest version of this program. (requires internet)";
            BtnUpdateProgram.AccessibleName = "Button: Update Program";
            BtnUpdateProgram.AccessibleRole = AccessibleRole.ButtonMenu;
            BtnUpdateProgram.AutoToolTip = true;
            BtnUpdateProgram.BackgroundImageLayout = ImageLayout.None;
            BtnUpdateProgram.DisplayStyle = ToolStripItemDisplayStyle.Image;
            BtnUpdateProgram.Image = Properties.Resources.UpdateScript;
            BtnUpdateProgram.Name = "BtnUpdateProgram";
            BtnUpdateProgram.Size = new Size(16, 17);
            BtnUpdateProgram.ToolTipText = "Click this button to upgrade to the latest version of this program. (requires internet)";
            BtnUpdateProgram.Click += BtnUpdateProgram_Click;
            // 
            // GUI
            // 
            AccessibleRole = AccessibleRole.None;
            AutoScaleMode = AutoScaleMode.None;
            BackgroundImageLayout = ImageLayout.None;
            ClientSize = new Size(537, 191);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(statusStrip1);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "GUI";
            SizeGripStyle = SizeGripStyle.Show;
            Text = "FloraSpace: GUI";
            flowLayoutPanel1.ResumeLayout(false);
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FlowLayoutPanel flowLayoutPanel1;
        private Button BtnTempFiles;
        private StatusStrip statusStrip1;
        private ToolStripProgressBar CoreProgress;
        private ToolStripStatusLabel VersionLabel;
        private ToolTip toolTip1;
        private ToolStripStatusLabel BtnUpdateProgram;
    }
}