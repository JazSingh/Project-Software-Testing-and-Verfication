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
            this.Score_label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Score_label
            // 
            this.Score_label.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.Score_label.Font = new System.Drawing.Font("Lucida Sans", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Score_label.Location = new System.Drawing.Point(66, 301);
            this.Score_label.Name = "Score_label";
            this.Score_label.Size = new System.Drawing.Size(93, 35);
            this.Score_label.TabIndex = 0;
            this.Score_label.Text = "Score:";
            this.Score_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Gamescherm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.Score_label);
            this.Name = "Gamescherm";
            this.Text = "Gamescherm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label Score_label;
    }
}