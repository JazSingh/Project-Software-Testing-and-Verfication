namespace ST_Project
{
    partial class DungeonRPG
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
            this.diff = new System.Windows.Forms.Label();
            this.dif1 = new System.Windows.Forms.Label();
            this.dif5 = new System.Windows.Forms.Label();
            this.dif4 = new System.Windows.Forms.Label();
            this.dif3 = new System.Windows.Forms.Label();
            this.dif2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // newgame_b
            // 
            this.newgame_b.Font = new System.Drawing.Font("Lucida Sans", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.button2.Font = new System.Drawing.Font("Lucida Sans", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(548, 349);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(179, 69);
            this.button2.TabIndex = 1;
            this.button2.Text = "Load Game";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // diff
            // 
            this.diff.BackColor = System.Drawing.SystemColors.Window;
            this.diff.Font = new System.Drawing.Font("Lucida Sans", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.diff.Location = new System.Drawing.Point(357, 247);
            this.diff.Name = "diff";
            this.diff.Size = new System.Drawing.Size(135, 53);
            this.diff.TabIndex = 2;
            this.diff.Text = "Difficulty:";
            this.diff.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.diff.Visible = false;
            // 
            // dif1
            // 
            this.dif1.BackColor = System.Drawing.Color.Lime;
            this.dif1.Font = new System.Drawing.Font("Lucida Sans", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dif1.Location = new System.Drawing.Point(509, 247);
            this.dif1.Name = "dif1";
            this.dif1.Size = new System.Drawing.Size(87, 51);
            this.dif1.TabIndex = 3;
            this.dif1.Text = "1";
            this.dif1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.dif1.Visible = false;
            this.dif1.Click += new System.EventHandler(this.dif_Click);
            // 
            // dif5
            // 
            this.dif5.BackColor = System.Drawing.Color.Black;
            this.dif5.Font = new System.Drawing.Font("Lucida Sans", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dif5.ForeColor = System.Drawing.Color.White;
            this.dif5.Location = new System.Drawing.Point(881, 247);
            this.dif5.Name = "dif5";
            this.dif5.Size = new System.Drawing.Size(87, 51);
            this.dif5.TabIndex = 4;
            this.dif5.Text = "5";
            this.dif5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.dif5.Visible = false;
            this.dif5.Click += new System.EventHandler(this.dif_Click);
            // 
            // dif4
            // 
            this.dif4.BackColor = System.Drawing.Color.Red;
            this.dif4.Font = new System.Drawing.Font("Lucida Sans", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dif4.Location = new System.Drawing.Point(788, 247);
            this.dif4.Name = "dif4";
            this.dif4.Size = new System.Drawing.Size(87, 51);
            this.dif4.TabIndex = 5;
            this.dif4.Text = "4";
            this.dif4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.dif4.Visible = false;
            this.dif4.Click += new System.EventHandler(this.dif_Click);
            // 
            // dif3
            // 
            this.dif3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.dif3.Font = new System.Drawing.Font("Lucida Sans", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dif3.Location = new System.Drawing.Point(695, 247);
            this.dif3.Name = "dif3";
            this.dif3.Size = new System.Drawing.Size(87, 51);
            this.dif3.TabIndex = 6;
            this.dif3.Text = "3";
            this.dif3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.dif3.Visible = false;
            this.dif3.Click += new System.EventHandler(this.dif_Click);
            // 
            // dif2
            // 
            this.dif2.BackColor = System.Drawing.Color.Yellow;
            this.dif2.Font = new System.Drawing.Font("Lucida Sans", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dif2.Location = new System.Drawing.Point(602, 247);
            this.dif2.Name = "dif2";
            this.dif2.Size = new System.Drawing.Size(87, 51);
            this.dif2.TabIndex = 7;
            this.dif2.Text = "2";
            this.dif2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.dif2.Visible = false;
            this.dif2.Click += new System.EventHandler(this.dif_Click);
            // 
            // DungeonRPG
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.dif2);
            this.Controls.Add(this.dif3);
            this.Controls.Add(this.dif4);
            this.Controls.Add(this.dif5);
            this.Controls.Add(this.dif1);
            this.Controls.Add(this.diff);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.newgame_b);
            this.Name = "DungeonRPG";
            this.Text = "Hoofdscherm";
            this.Load += new System.EventHandler(this.DungeonRPG_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button newgame_b;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label diff;
        private System.Windows.Forms.Label dif1;
        private System.Windows.Forms.Label dif5;
        private System.Windows.Forms.Label dif4;
        private System.Windows.Forms.Label dif3;
        private System.Windows.Forms.Label dif2;
    }
}

