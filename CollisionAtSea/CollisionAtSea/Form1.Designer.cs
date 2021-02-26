namespace CollisionAtSea
{
    partial class CollisionAtSea
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
            this.buttonCollision = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonDefaultWindow = new System.Windows.Forms.Button();
            this.richTextBoxPoints = new System.Windows.Forms.RichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDownMinDistance = new System.Windows.Forms.NumericUpDown();
            this.richTextBoxShowCollisionResults = new System.Windows.Forms.RichTextBox();
            this.dataGridViewCollision = new System.Windows.Forms.DataGridView();
            this.Speed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.plotViewCollision = new OxyPlot.WindowsForms.PlotView();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinDistance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCollision)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonCollision
            // 
            this.buttonCollision.Location = new System.Drawing.Point(15, 587);
            this.buttonCollision.Name = "buttonCollision";
            this.buttonCollision.Size = new System.Drawing.Size(147, 43);
            this.buttonCollision.TabIndex = 0;
            this.buttonCollision.Text = "Calculate";
            this.buttonCollision.UseVisualStyleBackColor = true;
            this.buttonCollision.Click += new System.EventHandler(this.buttonCollision_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 17);
            this.label3.TabIndex = 7;
            this.label3.Text = "Start/End";
            // 
            // buttonDefaultWindow
            // 
            this.buttonDefaultWindow.Location = new System.Drawing.Point(1083, 643);
            this.buttonDefaultWindow.Name = "buttonDefaultWindow";
            this.buttonDefaultWindow.Size = new System.Drawing.Size(169, 28);
            this.buttonDefaultWindow.TabIndex = 10;
            this.buttonDefaultWindow.Text = "Clear";
            this.buttonDefaultWindow.UseVisualStyleBackColor = true;
            this.buttonDefaultWindow.Click += new System.EventHandler(this.buttonDefaultWindow_Click);
            // 
            // richTextBoxPoints
            // 
            this.richTextBoxPoints.Location = new System.Drawing.Point(12, 50);
            this.richTextBoxPoints.Name = "richTextBoxPoints";
            this.richTextBoxPoints.ReadOnly = true;
            this.richTextBoxPoints.Size = new System.Drawing.Size(253, 160);
            this.richTextBoxPoints.TabIndex = 11;
            this.richTextBoxPoints.Text = "";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 513);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(142, 17);
            this.label4.TabIndex = 13;
            this.label4.Text = "Minimal distance in m";
            // 
            // numericUpDownMinDistance
            // 
            this.numericUpDownMinDistance.Cursor = System.Windows.Forms.Cursors.Default;
            this.numericUpDownMinDistance.DecimalPlaces = 1;
            this.numericUpDownMinDistance.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownMinDistance.Location = new System.Drawing.Point(12, 533);
            this.numericUpDownMinDistance.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDownMinDistance.Name = "numericUpDownMinDistance";
            this.numericUpDownMinDistance.Size = new System.Drawing.Size(99, 22);
            this.numericUpDownMinDistance.TabIndex = 12;
            this.numericUpDownMinDistance.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // richTextBoxShowCollisionResults
            // 
            this.richTextBoxShowCollisionResults.Location = new System.Drawing.Point(1283, 30);
            this.richTextBoxShowCollisionResults.Name = "richTextBoxShowCollisionResults";
            this.richTextBoxShowCollisionResults.ReadOnly = true;
            this.richTextBoxShowCollisionResults.Size = new System.Drawing.Size(453, 252);
            this.richTextBoxShowCollisionResults.TabIndex = 14;
            this.richTextBoxShowCollisionResults.Text = "";
            // 
            // dataGridViewCollision
            // 
            this.dataGridViewCollision.AllowUserToAddRows = false;
            this.dataGridViewCollision.AllowUserToDeleteRows = false;
            this.dataGridViewCollision.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCollision.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Speed});
            this.dataGridViewCollision.Location = new System.Drawing.Point(12, 241);
            this.dataGridViewCollision.Name = "dataGridViewCollision";
            this.dataGridViewCollision.RowHeadersWidth = 88;
            this.dataGridViewCollision.RowTemplate.Height = 24;
            this.dataGridViewCollision.Size = new System.Drawing.Size(253, 232);
            this.dataGridViewCollision.TabIndex = 15;
            this.dataGridViewCollision.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridViewCollision_CellValidating);
            this.dataGridViewCollision.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridViewCollision_RowsAdded);
            // 
            // Speed
            // 
            this.Speed.HeaderText = "Speed in m/s";
            this.Speed.Name = "Speed";
            // 
            // plotViewCollision
            // 
            this.plotViewCollision.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.plotViewCollision.Location = new System.Drawing.Point(302, 30);
            this.plotViewCollision.Name = "plotViewCollision";
            this.plotViewCollision.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotViewCollision.Size = new System.Drawing.Size(950, 600);
            this.plotViewCollision.TabIndex = 16;
            this.plotViewCollision.Text = "plotView1";
            this.plotViewCollision.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotViewCollision.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotViewCollision.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            this.plotViewCollision.MouseClick += new System.Windows.Forms.MouseEventHandler(this.plotViewCollision_MouseClick);
            // 
            // CollisionAtSea
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(1784, 727);
            this.Controls.Add(this.plotViewCollision);
            this.Controls.Add(this.dataGridViewCollision);
            this.Controls.Add(this.richTextBoxShowCollisionResults);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numericUpDownMinDistance);
            this.Controls.Add(this.richTextBoxPoints);
            this.Controls.Add(this.buttonDefaultWindow);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonCollision);
            this.Name = "CollisionAtSea";
            this.Text = "Collision At Sea";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinDistance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCollision)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCollision;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonDefaultWindow;
        private System.Windows.Forms.RichTextBox richTextBoxPoints;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDownMinDistance;
        private System.Windows.Forms.RichTextBox richTextBoxShowCollisionResults;
        private System.Windows.Forms.DataGridView dataGridViewCollision;
        private OxyPlot.WindowsForms.PlotView plotViewCollision;
        private System.Windows.Forms.DataGridViewTextBoxColumn Speed;
    }
}

