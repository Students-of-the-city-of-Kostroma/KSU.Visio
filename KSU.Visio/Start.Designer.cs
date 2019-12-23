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
            this.canvasPB = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.canvasPB)).BeginInit();
            this.SuspendLayout();
            // 
            // canvasPB
            // 
            this.canvasPB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.canvasPB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.canvasPB.Location = new System.Drawing.Point(12, 41);
            this.canvasPB.Name = "canvasPB";
            this.canvasPB.Size = new System.Drawing.Size(920, 483);
            this.canvasPB.TabIndex = 1;
            this.canvasPB.TabStop = false;
            this.canvasPB.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.CanvasPB_MouseDoubleClick);
            this.canvasPB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CanvasPB_MouseDown);
            this.canvasPB.MouseMove += new System.Windows.Forms.MouseEventHandler(this.CanvasPB_MouseMove);
            this.canvasPB.MouseUp += new System.Windows.Forms.MouseEventHandler(this.CanvasPB_MouseUp);
            this.canvasPB.Resize += new System.EventHandler(this.canvasPB_Resize);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(47, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Пуск";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.StartB_Click);
            // 
            // Start
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 536);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.canvasPB);
            this.MinimumSize = new System.Drawing.Size(766, 574);
            this.Name = "Start";
            this.Text = "KSU.Visio";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Start_FormClosed);
            this.Load += new System.EventHandler(this.Start_Load);
            ((System.ComponentModel.ISupportInitialize)(this.canvasPB)).EndInit();
            this.ResumeLayout(false);

        }

		#endregion
        private System.Windows.Forms.PictureBox canvasPB;
        private System.Windows.Forms.Button button1;
    }
}

