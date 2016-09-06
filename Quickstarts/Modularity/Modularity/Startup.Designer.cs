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
            this.ModuleA = new Modularity.ModuleControl();
            this.ModuleB = new Modularity.ModuleControl();
            this.ModuleC = new Modularity.ModuleControl();
            this.ModuleD = new Modularity.ModuleControl();
            this.ModuleE = new Modularity.ModuleControl();
            this.ModuleF = new Modularity.ModuleControl();
            this.TraceTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // ModuleA
            // 
            this.ModuleA.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.ModuleA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ModuleA.Label2Text = " ";
            this.ModuleA.Location = new System.Drawing.Point(165, 12);
            this.ModuleA.ModuleName = "ModuleA";
            this.ModuleA.Name = "ModuleA";
            this.ModuleA.Size = new System.Drawing.Size(133, 110);
            this.ModuleA.TabIndex = 0;
            // 
            // ModuleB
            // 
            this.ModuleB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ModuleB.Label2Text = "Click to load";
            this.ModuleB.Location = new System.Drawing.Point(329, 12);
            this.ModuleB.ModuleName = "ModuleB";
            this.ModuleB.Name = "ModuleB";
            this.ModuleB.Size = new System.Drawing.Size(133, 110);
            this.ModuleB.TabIndex = 1;
            this.ModuleB.Load += new System.EventHandler(this.ModuleB_Load);
            // 
            // ModuleC
            // 
            this.ModuleC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ModuleC.Label2Text = "Click to load";
            this.ModuleC.Location = new System.Drawing.Point(487, 12);
            this.ModuleC.ModuleName = "ModuleC";
            this.ModuleC.Name = "ModuleC";
            this.ModuleC.Size = new System.Drawing.Size(133, 110);
            this.ModuleC.TabIndex = 2;
            this.ModuleC.Load += new System.EventHandler(this.ModuleC_Load);
            // 
            // ModuleD
            // 
            this.ModuleD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.ModuleD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ModuleD.Label2Text = null;
            this.ModuleD.Location = new System.Drawing.Point(165, 137);
            this.ModuleD.ModuleName = "ModuleD";
            this.ModuleD.Name = "ModuleD";
            this.ModuleD.Size = new System.Drawing.Size(133, 110);
            this.ModuleD.TabIndex = 3;
            // 
            // ModuleE
            // 
            this.ModuleE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ModuleE.Label2Text = "Click to load";
            this.ModuleE.Location = new System.Drawing.Point(329, 137);
            this.ModuleE.ModuleName = "ModuleE";
            this.ModuleE.Name = "ModuleE";
            this.ModuleE.Size = new System.Drawing.Size(133, 110);
            this.ModuleE.TabIndex = 4;
            this.ModuleE.Load += new System.EventHandler(this.ModuleE_Load);
            // 
            // ModuleF
            // 
            this.ModuleF.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ModuleF.Label2Text = "Click to load";
            this.ModuleF.Location = new System.Drawing.Point(487, 137);
            this.ModuleF.ModuleName = "ModuleF";
            this.ModuleF.Name = "ModuleF";
            this.ModuleF.Size = new System.Drawing.Size(133, 110);
            this.ModuleF.TabIndex = 5;
            this.ModuleF.Load += new System.EventHandler(this.ModuleF_Load);
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
            this.Controls.Add(this.ModuleF);
            this.Controls.Add(this.ModuleE);
            this.Controls.Add(this.ModuleD);
            this.Controls.Add(this.ModuleC);
            this.Controls.Add(this.ModuleB);
            this.Controls.Add(this.ModuleA);
            this.Name = "Startup";
            this.Text = "Startup";
            this.Load += new System.EventHandler(this.Startup_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ModuleControl ModuleA;
        private ModuleControl ModuleB;
        private ModuleControl ModuleC;
        private ModuleControl ModuleD;
        private ModuleControl ModuleE;
        private ModuleControl ModuleF;
        private System.Windows.Forms.TextBox TraceTextBox;
    }
}