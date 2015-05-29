namespace ST_Project
{
    partial class Hoofdscherm
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
            this.newgame_b = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.cb_logging = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // newgame_b
            // 
            this.newgame_b.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newgame_b.Location = new System.Drawing.Point(548, 123);
            this.newgame_b.Name = "newgame_b";
            this.newgame_b.Size = new System.Drawing.Size(179, 69);
            this.newgame_b.TabIndex = 0;
            this.newgame_b.Text = "New Game";
            this.newgame_b.UseVisualStyleBackColor = true;
            this.newgame_b.Click += new System.EventHandler(this.newgame_b_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(548, 349);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(179, 69);
            this.button2.TabIndex = 1;
            this.button2.Text = "Load Game";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(885, 40);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(152, 50);
            this.button1.TabIndex = 9;
            this.button1.Text = "Highscores";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cb_logging
            // 
            this.cb_logging.AutoSize = true;
            this.cb_logging.Location = new System.Drawing.Point(325, 146);
            this.cb_logging.Name = "cb_logging";
            this.cb_logging.Size = new System.Drawing.Size(60, 17);
            this.cb_logging.TabIndex = 10;
            this.cb_logging.Text = "logging";
            this.cb_logging.UseVisualStyleBackColor = true;
            this.cb_logging.CheckedChanged += new System.EventHandler(this.cb_logging_CheckedChanged);
            // 
            // Hoofdscherm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(962, 526);
            this.Controls.Add(this.cb_logging);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.newgame_b);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Hoofdscherm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hoofdscherm";
            this.Load += new System.EventHandler(this.DungeonRPG_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private System.Windows.Forms.Button newgame_b;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox cb_logging;
    }
}

