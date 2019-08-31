namespace KSU.Visio
{
    partial class Start
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.objectsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.canvasPB = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.canvasPB)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.objectsPanel);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 512);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Панель объектов";
            // 
            // objectsPanel
            // 
            this.objectsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objectsPanel.Location = new System.Drawing.Point(3, 16);
            this.objectsPanel.Name = "objectsPanel";
            this.objectsPanel.Size = new System.Drawing.Size(194, 493);
            this.objectsPanel.TabIndex = 0;
            // 
            // canvasPB
            // 
            this.canvasPB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.canvasPB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.canvasPB.Location = new System.Drawing.Point(215, 12);
            this.canvasPB.Name = "canvasPB";
            this.canvasPB.Size = new System.Drawing.Size(626, 512);
            this.canvasPB.TabIndex = 1;
            this.canvasPB.TabStop = false;
            this.canvasPB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.canvasPB_MouseDown);
            this.canvasPB.MouseMove += new System.Windows.Forms.MouseEventHandler(this.canvasPB_MouseMove);
            this.canvasPB.MouseUp += new System.Windows.Forms.MouseEventHandler(this.canvasPB_MouseUp);
            // 
            // Start
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(853, 536);
            this.Controls.Add(this.canvasPB);
            this.Controls.Add(this.groupBox1);
            this.MinimumSize = new System.Drawing.Size(766, 574);
            this.Name = "Start";
            this.Text = "KSU.Visio";
            this.Load += new System.EventHandler(this.Start_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.canvasPB)).EndInit();
            this.ResumeLayout(false);

        }

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.FlowLayoutPanel objectsPanel;
        private System.Windows.Forms.PictureBox canvasPB;
    }
}

