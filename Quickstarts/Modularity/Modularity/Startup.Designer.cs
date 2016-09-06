namespace Modularity
{
    partial class Startup
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
            this.moduleControl1 = new Modularity.ModuleControl();
            this.moduleControl2 = new Modularity.ModuleControl();
            this.moduleControl3 = new Modularity.ModuleControl();
            this.moduleControl4 = new Modularity.ModuleControl();
            this.moduleControl5 = new Modularity.ModuleControl();
            this.moduleControl6 = new Modularity.ModuleControl();
            this.TraceTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // moduleControl1
            // 
            this.moduleControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.moduleControl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.moduleControl1.Label2Text = " ";
            this.moduleControl1.Location = new System.Drawing.Point(165, 12);
            this.moduleControl1.ModuleName = "ModuleA";
            this.moduleControl1.Name = "moduleControl1";
            this.moduleControl1.Size = new System.Drawing.Size(133, 110);
            this.moduleControl1.TabIndex = 0;
            // 
            // moduleControl2
            // 
            this.moduleControl2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.moduleControl2.Label2Text = null;
            this.moduleControl2.Location = new System.Drawing.Point(329, 12);
            this.moduleControl2.ModuleName = "ModuleB";
            this.moduleControl2.Name = "moduleControl2";
            this.moduleControl2.Size = new System.Drawing.Size(133, 110);
            this.moduleControl2.TabIndex = 1;
            // 
            // moduleControl3
            // 
            this.moduleControl3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.moduleControl3.Label2Text = null;
            this.moduleControl3.Location = new System.Drawing.Point(487, 12);
            this.moduleControl3.ModuleName = "ModuleC";
            this.moduleControl3.Name = "moduleControl3";
            this.moduleControl3.Size = new System.Drawing.Size(133, 110);
            this.moduleControl3.TabIndex = 2;
            // 
            // moduleControl4
            // 
            this.moduleControl4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.moduleControl4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.moduleControl4.Label2Text = null;
            this.moduleControl4.Location = new System.Drawing.Point(165, 137);
            this.moduleControl4.ModuleName = "ModuleD";
            this.moduleControl4.Name = "moduleControl4";
            this.moduleControl4.Size = new System.Drawing.Size(133, 110);
            this.moduleControl4.TabIndex = 3;
            // 
            // moduleControl5
            // 
            this.moduleControl5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.moduleControl5.Label2Text = null;
            this.moduleControl5.Location = new System.Drawing.Point(329, 137);
            this.moduleControl5.ModuleName = "ModuleE";
            this.moduleControl5.Name = "moduleControl5";
            this.moduleControl5.Size = new System.Drawing.Size(133, 110);
            this.moduleControl5.TabIndex = 4;
            // 
            // moduleControl6
            // 
            this.moduleControl6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.moduleControl6.Label2Text = null;
            this.moduleControl6.Location = new System.Drawing.Point(487, 137);
            this.moduleControl6.ModuleName = "ModuleF";
            this.moduleControl6.Name = "moduleControl6";
            this.moduleControl6.Size = new System.Drawing.Size(133, 110);
            this.moduleControl6.TabIndex = 5;
            // 
            // TraceTextBox
            // 
            this.TraceTextBox.Location = new System.Drawing.Point(29, 257);
            this.TraceTextBox.Multiline = true;
            this.TraceTextBox.Name = "TraceTextBox";
            this.TraceTextBox.Size = new System.Drawing.Size(591, 191);
            this.TraceTextBox.TabIndex = 6;
            // 
            // Startup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(641, 452);
            this.Controls.Add(this.TraceTextBox);
            this.Controls.Add(this.moduleControl6);
            this.Controls.Add(this.moduleControl5);
            this.Controls.Add(this.moduleControl4);
            this.Controls.Add(this.moduleControl3);
            this.Controls.Add(this.moduleControl2);
            this.Controls.Add(this.moduleControl1);
            this.Name = "Startup";
            this.Text = "Startup";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ModuleControl moduleControl1;
        private ModuleControl moduleControl2;
        private ModuleControl moduleControl3;
        private ModuleControl moduleControl4;
        private ModuleControl moduleControl5;
        private ModuleControl moduleControl6;
        private System.Windows.Forms.TextBox TraceTextBox;
    }
}