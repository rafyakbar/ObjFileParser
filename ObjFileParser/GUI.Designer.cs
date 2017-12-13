namespace ObjFileParser
{
    partial class GUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GUI));
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.browseButton = new System.Windows.Forms.Button();
            this.infoTextBox = new System.Windows.Forms.TextBox();
            this.glNormal3f = new System.Windows.Forms.CheckBox();
            this.glTexCoord3f = new System.Windows.Forms.CheckBox();
            this.parseButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 70);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(423, 23);
            this.progressBar.TabIndex = 5;
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(361, 12);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(75, 23);
            this.browseButton.TabIndex = 4;
            this.browseButton.Text = "Pilih file .obj";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // infoTextBox
            // 
            this.infoTextBox.Location = new System.Drawing.Point(13, 14);
            this.infoTextBox.Name = "infoTextBox";
            this.infoTextBox.Size = new System.Drawing.Size(350, 20);
            this.infoTextBox.TabIndex = 3;
            // 
            // glNormal3f
            // 
            this.glNormal3f.AutoSize = true;
            this.glNormal3f.Checked = true;
            this.glNormal3f.CheckState = System.Windows.Forms.CheckState.Checked;
            this.glNormal3f.Location = new System.Drawing.Point(99, 45);
            this.glNormal3f.Name = "glNormal3f";
            this.glNormal3f.Size = new System.Drawing.Size(67, 17);
            this.glNormal3f.TabIndex = 6;
            this.glNormal3f.Text = "glNormal";
            this.glNormal3f.UseVisualStyleBackColor = true;
            // 
            // glTexCoord3f
            // 
            this.glTexCoord3f.AutoSize = true;
            this.glTexCoord3f.Checked = true;
            this.glTexCoord3f.CheckState = System.Windows.Forms.CheckState.Checked;
            this.glTexCoord3f.Location = new System.Drawing.Point(13, 45);
            this.glTexCoord3f.Name = "glTexCoord3f";
            this.glTexCoord3f.Size = new System.Drawing.Size(80, 17);
            this.glTexCoord3f.TabIndex = 7;
            this.glTexCoord3f.Text = "glTexCoord";
            this.glTexCoord3f.UseVisualStyleBackColor = true;
            // 
            // parseButton
            // 
            this.parseButton.Location = new System.Drawing.Point(361, 41);
            this.parseButton.Name = "parseButton";
            this.parseButton.Size = new System.Drawing.Size(75, 23);
            this.parseButton.TabIndex = 8;
            this.parseButton.Text = "Parse";
            this.parseButton.UseVisualStyleBackColor = true;
            this.parseButton.Click += new System.EventHandler(this.parseButton_Click);
            // 
            // GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(448, 104);
            this.Controls.Add(this.parseButton);
            this.Controls.Add(this.glTexCoord3f);
            this.Controls.Add(this.glNormal3f);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.infoTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "GUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = ".obj file parser (by Rafy AA & Respita YR)";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.TextBox infoTextBox;
        private System.Windows.Forms.CheckBox glNormal3f;
        private System.Windows.Forms.CheckBox glTexCoord3f;
        private System.Windows.Forms.Button parseButton;
    }
}

