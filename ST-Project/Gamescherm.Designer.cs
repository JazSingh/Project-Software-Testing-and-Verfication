namespace ST_Project
{
    partial class Gamescherm
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
            this.potions_lbl = new System.Windows.Forms.Label();
            this.scrolls_lbl = new System.Windows.Forms.Label();
            this.crystal_lbl = new System.Windows.Forms.Label();
            this.NRpotions = new System.Windows.Forms.Label();
            this.NRcrystals = new System.Windows.Forms.Label();
            this.NRscrolls = new System.Windows.Forms.Label();
            this.Health_lbl = new System.Windows.Forms.Label();
            this.NRhealth = new System.Windows.Forms.Label();
            this.Fight_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // potions_lbl
            // 
            this.potions_lbl.AutoSize = true;
            this.potions_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.potions_lbl.Location = new System.Drawing.Point(118, 513);
            this.potions_lbl.Name = "potions_lbl";
            this.potions_lbl.Size = new System.Drawing.Size(145, 24);
            this.potions_lbl.TabIndex = 0;
            this.potions_lbl.Text = "Health Potions";
            // 
            // scrolls_lbl
            // 
            this.scrolls_lbl.AutoSize = true;
            this.scrolls_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scrolls_lbl.Location = new System.Drawing.Point(495, 513);
            this.scrolls_lbl.Name = "scrolls_lbl";
            this.scrolls_lbl.Size = new System.Drawing.Size(135, 24);
            this.scrolls_lbl.TabIndex = 1;
            this.scrolls_lbl.Text = "Magic Scrolls";
            // 
            // crystal_lbl
            // 
            this.crystal_lbl.AutoSize = true;
            this.crystal_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.crystal_lbl.Location = new System.Drawing.Point(309, 513);
            this.crystal_lbl.Name = "crystal_lbl";
            this.crystal_lbl.Size = new System.Drawing.Size(135, 24);
            this.crystal_lbl.TabIndex = 2;
            this.crystal_lbl.Text = "Time Crystals";
            // 
            // NRpotions
            // 
            this.NRpotions.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NRpotions.Location = new System.Drawing.Point(118, 556);
            this.NRpotions.Name = "NRpotions";
            this.NRpotions.Size = new System.Drawing.Size(152, 22);
            this.NRpotions.TabIndex = 3;
            this.NRpotions.Text = "0";
            this.NRpotions.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // NRcrystals
            // 
            this.NRcrystals.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NRcrystals.Location = new System.Drawing.Point(309, 556);
            this.NRcrystals.Name = "NRcrystals";
            this.NRcrystals.Size = new System.Drawing.Size(152, 22);
            this.NRcrystals.TabIndex = 4;
            this.NRcrystals.Text = "0";
            this.NRcrystals.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // NRscrolls
            // 
            this.NRscrolls.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NRscrolls.Location = new System.Drawing.Point(495, 556);
            this.NRscrolls.Name = "NRscrolls";
            this.NRscrolls.Size = new System.Drawing.Size(152, 22);
            this.NRscrolls.TabIndex = 5;
            this.NRscrolls.Text = "0";
            this.NRscrolls.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Health_lbl
            // 
            this.Health_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Health_lbl.Location = new System.Drawing.Point(116, 420);
            this.Health_lbl.Name = "Health_lbl";
            this.Health_lbl.Size = new System.Drawing.Size(76, 33);
            this.Health_lbl.TabIndex = 6;
            this.Health_lbl.Text = "HP:";
            // 
            // NRhealth
            // 
            this.NRhealth.AutoSize = true;
            this.NRhealth.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NRhealth.Location = new System.Drawing.Point(231, 420);
            this.NRhealth.Name = "NRhealth";
            this.NRhealth.Size = new System.Drawing.Size(32, 33);
            this.NRhealth.TabIndex = 7;
            this.NRhealth.Text = "0";
            // 
            // Fight_button
            // 
            this.Fight_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Fight_button.Location = new System.Drawing.Point(831, 487);
            this.Fight_button.Name = "Fight_button";
            this.Fight_button.Size = new System.Drawing.Size(180, 74);
            this.Fight_button.TabIndex = 8;
            this.Fight_button.Text = "Fight!";
            this.Fight_button.UseVisualStyleBackColor = true;
            this.Fight_button.Visible = false;
            // 
            // Gamescherm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.Fight_button);
            this.Controls.Add(this.NRhealth);
            this.Controls.Add(this.Health_lbl);
            this.Controls.Add(this.NRscrolls);
            this.Controls.Add(this.NRcrystals);
            this.Controls.Add(this.NRpotions);
            this.Controls.Add(this.crystal_lbl);
            this.Controls.Add(this.scrolls_lbl);
            this.Controls.Add(this.potions_lbl);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Gamescherm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gamescherm";
            this.Load += new System.EventHandler(this.Gamescherm_Load);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.CheckMove);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label potions_lbl;
        private System.Windows.Forms.Label scrolls_lbl;
        private System.Windows.Forms.Label crystal_lbl;
        private System.Windows.Forms.Label NRpotions;
        private System.Windows.Forms.Label NRcrystals;
        private System.Windows.Forms.Label NRscrolls;
        private System.Windows.Forms.Label Health_lbl;
        private System.Windows.Forms.Label NRhealth;
        private System.Windows.Forms.Button Fight_button;

    }
}