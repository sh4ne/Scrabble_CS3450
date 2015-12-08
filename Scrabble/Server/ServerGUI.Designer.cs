namespace Scrabble.Server
{
    partial class ServerGUI
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
            this.comBox = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gameList = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comLabel = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.printgames = new System.Windows.Forms.Button();
            this.toClient = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // comBox
            // 
            this.comBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.comBox.Cursor = System.Windows.Forms.Cursors.AppStarting;
            this.comBox.Location = new System.Drawing.Point(304, 83);
            this.comBox.Multiline = true;
            this.comBox.Name = "comBox";
            this.comBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.comBox.Size = new System.Drawing.Size(315, 465);
            this.comBox.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(631, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // gameList
            // 
            this.gameList.FormattingEnabled = true;
            this.gameList.Location = new System.Drawing.Point(13, 83);
            this.gameList.Name = "gameList";
            this.gameList.Size = new System.Drawing.Size(257, 69);
            this.gameList.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Running Games";
            // 
            // comLabel
            // 
            this.comLabel.AutoSize = true;
            this.comLabel.Location = new System.Drawing.Point(304, 67);
            this.comLabel.Name = "comLabel";
            this.comLabel.Size = new System.Drawing.Size(84, 13);
            this.comLabel.TabIndex = 4;
            this.comLabel.Text = "Communications";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(15, 158);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(118, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Create New World";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // printgames
            // 
            this.printgames.Location = new System.Drawing.Point(149, 158);
            this.printgames.Name = "printgames";
            this.printgames.Size = new System.Drawing.Size(75, 23);
            this.printgames.TabIndex = 6;
            this.printgames.Text = "Print Games";
            this.printgames.UseVisualStyleBackColor = true;
            this.printgames.Click += new System.EventHandler(this.printgames_Click);
            // 
            // toClient
            // 
            this.toClient.Location = new System.Drawing.Point(149, 199);
            this.toClient.Name = "toClient";
            this.toClient.Size = new System.Drawing.Size(75, 28);
            this.toClient.TabIndex = 7;
            this.toClient.Text = "To Client";
            this.toClient.UseVisualStyleBackColor = true;
            this.toClient.Click += new System.EventHandler(this.toClient_Click);
            // 
            // ServerGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(631, 570);
            this.Controls.Add(this.toClient);
            this.Controls.Add(this.printgames);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gameList);
            this.Controls.Add(this.comBox);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ServerGUI";
            this.Text = "ServerGUI";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ServerGUI_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox comBox;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ListBox gameList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label comLabel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button printgames;
        private System.Windows.Forms.Button toClient;
    }
}