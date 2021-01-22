namespace DualNumbersWindowsForms
{
    partial class DualzahlenRechner
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
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.textBoxSummand1 = new System.Windows.Forms.TextBox();
            this.textBoxSummand2 = new System.Windows.Forms.TextBox();
            this.textBoxSumme = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.textBoxSignBit = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "-",
            "+"});
            this.comboBox1.Location = new System.Drawing.Point(65, 127);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 24);
            this.comboBox1.TabIndex = 14;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // textBoxSummand1
            // 
            this.textBoxSummand1.Location = new System.Drawing.Point(195, 87);
            this.textBoxSummand1.Name = "textBoxSummand1";
            this.textBoxSummand1.Size = new System.Drawing.Size(209, 22);
            this.textBoxSummand1.TabIndex = 0;
            // 
            // textBoxSummand2
            // 
            this.textBoxSummand2.Location = new System.Drawing.Point(195, 127);
            this.textBoxSummand2.Name = "textBoxSummand2";
            this.textBoxSummand2.Size = new System.Drawing.Size(209, 22);
            this.textBoxSummand2.TabIndex = 1;
            // 
            // textBoxSumme
            // 
            this.textBoxSumme.Location = new System.Drawing.Point(195, 182);
            this.textBoxSumme.Name = "textBoxSumme";
            this.textBoxSumme.ReadOnly = true;
            this.textBoxSumme.Size = new System.Drawing.Size(209, 22);
            this.textBoxSumme.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(192, 152);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(216, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "__________________________";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label5.Location = new System.Drawing.Point(197, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(207, 20);
            this.label5.TabIndex = 10;
            this.label5.Text = "Bitte zwei 8Bit Dualzahlen";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label6.Location = new System.Drawing.Point(328, 52);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 20);
            this.label6.TabIndex = 11;
            this.label6.Text = "eingeben";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(195, 222);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(209, 40);
            this.button1.TabIndex = 15;
            this.button1.Text = "Berechne";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBoxSignBit
            // 
            this.textBoxSignBit.Location = new System.Drawing.Point(166, 182);
            this.textBoxSignBit.Name = "textBoxSignBit";
            this.textBoxSignBit.ReadOnly = true;
            this.textBoxSignBit.Size = new System.Drawing.Size(23, 22);
            this.textBoxSignBit.TabIndex = 16;
            // 
            // DualzahlenRechner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(585, 450);
            this.Controls.Add(this.textBoxSignBit);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxSumme);
            this.Controls.Add(this.textBoxSummand2);
            this.Controls.Add(this.textBoxSummand1);
            this.Name = "DualzahlenRechner";
            this.Text = "DualzahlenRechner";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxSummand1;
        private System.Windows.Forms.TextBox textBoxSummand2;
        private System.Windows.Forms.TextBox textBoxSumme;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox textBoxSignBit;
    }
}

