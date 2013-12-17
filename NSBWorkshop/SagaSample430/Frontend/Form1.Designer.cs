namespace Frontend
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
            this.btnEmployeeApproval = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbEmployeeName = new System.Windows.Forms.TextBox();
            this.tbAmount = new System.Windows.Forms.TextBox();
            this.btnDirectManagerApproval = new System.Windows.Forms.Button();
            this.VPApproval = new System.Windows.Forms.Button();
            this.btnFinanaceApproval = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnEmployeeApproval
            // 
            this.btnEmployeeApproval.Location = new System.Drawing.Point(212, 12);
            this.btnEmployeeApproval.Name = "btnEmployeeApproval";
            this.btnEmployeeApproval.Size = new System.Drawing.Size(77, 50);
            this.btnEmployeeApproval.TabIndex = 0;
            this.btnEmployeeApproval.Text = "Request Approval";
            this.btnEmployeeApproval.UseVisualStyleBackColor = true;
            this.btnEmployeeApproval.Click += new System.EventHandler(this.btnEmployeeApproval_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Employee Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Amount";
            // 
            // tbEmployeeName
            // 
            this.tbEmployeeName.Location = new System.Drawing.Point(106, 16);
            this.tbEmployeeName.Name = "tbEmployeeName";
            this.tbEmployeeName.Size = new System.Drawing.Size(100, 20);
            this.tbEmployeeName.TabIndex = 3;
            this.tbEmployeeName.Text = "Ohad";
            // 
            // tbAmount
            // 
            this.tbAmount.Location = new System.Drawing.Point(106, 42);
            this.tbAmount.Name = "tbAmount";
            this.tbAmount.Size = new System.Drawing.Size(100, 20);
            this.tbAmount.TabIndex = 4;
            this.tbAmount.Text = "100";
            // 
            // btnDirectManagerApproval
            // 
            this.btnDirectManagerApproval.Location = new System.Drawing.Point(153, 101);
            this.btnDirectManagerApproval.Name = "btnDirectManagerApproval";
            this.btnDirectManagerApproval.Size = new System.Drawing.Size(136, 26);
            this.btnDirectManagerApproval.TabIndex = 5;
            this.btnDirectManagerApproval.Text = "Direct Manager Approval";
            this.btnDirectManagerApproval.UseVisualStyleBackColor = true;
            this.btnDirectManagerApproval.Click += new System.EventHandler(this.btnDirectManagerApproval_Click);
            // 
            // VPApproval
            // 
            this.VPApproval.Location = new System.Drawing.Point(153, 133);
            this.VPApproval.Name = "VPApproval";
            this.VPApproval.Size = new System.Drawing.Size(136, 26);
            this.VPApproval.TabIndex = 6;
            this.VPApproval.Text = "VP Approval";
            this.VPApproval.UseVisualStyleBackColor = true;
            this.VPApproval.Click += new System.EventHandler(this.VPApproval_Click);
            // 
            // btnFinanaceApproval
            // 
            this.btnFinanaceApproval.Location = new System.Drawing.Point(153, 165);
            this.btnFinanaceApproval.Name = "btnFinanaceApproval";
            this.btnFinanaceApproval.Size = new System.Drawing.Size(136, 26);
            this.btnFinanaceApproval.TabIndex = 7;
            this.btnFinanaceApproval.Text = "Finance Approval";
            this.btnFinanaceApproval.UseVisualStyleBackColor = true;
            this.btnFinanaceApproval.Click += new System.EventHandler(this.btnFinanaceApproval_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "label3";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(368, 251);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnFinanaceApproval);
            this.Controls.Add(this.VPApproval);
            this.Controls.Add(this.btnDirectManagerApproval);
            this.Controls.Add(this.tbAmount);
            this.Controls.Add(this.tbEmployeeName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnEmployeeApproval);
            this.Name = "Form1";
            this.Text = "Approval Request Form";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnEmployeeApproval;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbEmployeeName;
        private System.Windows.Forms.TextBox tbAmount;
        private System.Windows.Forms.Button btnDirectManagerApproval;
        private System.Windows.Forms.Button VPApproval;
        private System.Windows.Forms.Button btnFinanaceApproval;
        private System.Windows.Forms.Label label3;
    }
}

