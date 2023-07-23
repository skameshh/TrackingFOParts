
namespace TrackingFOParts
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
            this.btnLocalOutlook = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnLocalOutlook
            // 
            this.btnLocalOutlook.Location = new System.Drawing.Point(305, 12);
            this.btnLocalOutlook.Name = "btnLocalOutlook";
            this.btnLocalOutlook.Size = new System.Drawing.Size(303, 84);
            this.btnLocalOutlook.TabIndex = 1;
            this.btnLocalOutlook.Text = "Local outlook";
            this.btnLocalOutlook.UseVisualStyleBackColor = true;
            this.btnLocalOutlook.Click += new System.EventHandler(this.btnLocalOutlook_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1018, 552);
            this.Controls.Add(this.btnLocalOutlook);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnLocalOutlook;
    }
}

