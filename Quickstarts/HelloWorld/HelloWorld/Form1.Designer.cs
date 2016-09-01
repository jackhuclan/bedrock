namespace HelloWorld
{
    partial class Form1
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
            this.MainRegion = new Bedrock.Winform.RegionPlaceholder();
            this.SuspendLayout();
            // 
            // MainRegion
            // 
            this.MainRegion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MainRegion.Context = null;
            this.MainRegion.Location = new System.Drawing.Point(44, 32);
            this.MainRegion.Name = "MainRegion";
            this.MainRegion.RegionManager = null;
            this.MainRegion.Size = new System.Drawing.Size(365, 176);
            this.MainRegion.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 403);
            this.Controls.Add(this.MainRegion);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private Bedrock.Winform.RegionPlaceholder MainRegion;
    }
}

