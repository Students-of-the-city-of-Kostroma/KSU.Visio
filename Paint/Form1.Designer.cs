namespace Paint
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.Save = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.open = new System.Windows.Forms.Button();
            this.OpenFile = new System.Windows.Forms.Button();
            this.Way = new System.Windows.Forms.Button();
            this.Start = new System.Windows.Forms.Button();
            this.Stop = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.Arrbtn = new System.Windows.Forms.Button();
            this.Peobtn = new System.Windows.Forms.Button();
            this.Diabtn = new System.Windows.Forms.Button();
            this.Retbtn = new System.Windows.Forms.Button();
            this.Inbtn = new System.Windows.Forms.Button();
            this.Aggbtn = new System.Windows.Forms.Button();
            this.Inherbtn = new System.Windows.Forms.Button();
            this.Combtn = new System.Windows.Forms.Button();
            this.Assyncbtn = new System.Windows.Forms.Button();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.PaleGreen;
            this.button1.Location = new System.Drawing.Point(6, 43);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(70, 60);
            this.button1.TabIndex = 0;
            this.button1.Text = "Цвет";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(82, 43);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(73, 60);
            this.button2.TabIndex = 1;
            this.button2.Text = "Очистить";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.toolStripLabel2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(937, 25);
            this.toolStrip1.TabIndex = 7;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.BackColor = System.Drawing.Color.GreenYellow;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(99, 22);
            this.toolStripLabel1.Text = "Толщина кисти: ";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(13, 22);
            this.toolStripLabel2.Text = "1";
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(129, 0);
            this.trackBar1.Minimum = 1;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(69, 45);
            this.trackBar1.TabIndex = 8;
            this.trackBar1.Value = 1;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // Save
            // 
            this.Save.Location = new System.Drawing.Point(285, 0);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(75, 23);
            this.Save.TabIndex = 9;
            this.Save.Text = "Сохранить";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // open
            // 
            this.open.Location = new System.Drawing.Point(204, 0);
            this.open.Name = "open";
            this.open.Size = new System.Drawing.Size(75, 23);
            this.open.TabIndex = 10;
            this.open.Text = "Открыть";
            this.open.UseVisualStyleBackColor = true;
            this.open.Click += new System.EventHandler(this.open_Click);
            // 
            // OpenFile
            // 
            this.OpenFile.Location = new System.Drawing.Point(6, 472);
            this.OpenFile.Name = "OpenFile";
            this.OpenFile.Size = new System.Drawing.Size(81, 23);
            this.OpenFile.TabIndex = 11;
            this.OpenFile.Text = "Открыть";
            this.OpenFile.UseVisualStyleBackColor = true;
            this.OpenFile.Click += new System.EventHandler(this.OpenFile_Click);
            // 
            // Way
            // 
            this.Way.Enabled = false;
            this.Way.Location = new System.Drawing.Point(93, 472);
            this.Way.Name = "Way";
            this.Way.Size = new System.Drawing.Size(81, 23);
            this.Way.TabIndex = 12;
            this.Way.Text = "Путь";
            this.Way.UseVisualStyleBackColor = true;
            this.Way.Click += new System.EventHandler(this.Way_Click);
            // 
            // Start
            // 
            this.Start.Enabled = false;
            this.Start.Location = new System.Drawing.Point(6, 501);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(81, 23);
            this.Start.TabIndex = 13;
            this.Start.Text = "Старт";
            this.Start.UseVisualStyleBackColor = true;
            this.Start.Click += new System.EventHandler(this.Start_Click);
            // 
            // Stop
            // 
            this.Stop.Enabled = false;
            this.Stop.Location = new System.Drawing.Point(93, 501);
            this.Stop.Name = "Stop";
            this.Stop.Size = new System.Drawing.Size(81, 23);
            this.Stop.TabIndex = 14;
            this.Stop.Text = "Стоп";
            this.Stop.UseVisualStyleBackColor = true;
            this.Stop.Click += new System.EventHandler(this.Stop_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(126, 407);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(126, 436);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 17;
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackgroundImage = global::Paint.Properties.Resources.straingh;
            this.pictureBox4.Location = new System.Drawing.Point(44, 158);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(38, 38);
            this.pictureBox4.TabIndex = 6;
            this.pictureBox4.TabStop = false;
            this.pictureBox4.Click += new System.EventHandler(this.pictureBox4_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::Paint.Properties.Resources.circle;
            this.pictureBox3.Location = new System.Drawing.Point(136, 159);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(38, 38);
            this.pictureBox3.TabIndex = 4;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Click += new System.EventHandler(this.pictureBox3_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Paint.Properties.Resources.line;
            this.pictureBox2.Location = new System.Drawing.Point(88, 158);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(38, 37);
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBox1.Image = global::Paint.Properties.Resources.Image1;
            this.pictureBox1.Location = new System.Drawing.Point(0, 159);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(38, 37);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.BackColor = System.Drawing.Color.DarkRed;
            this.progressBar1.ForeColor = System.Drawing.Color.Yellow;
            this.progressBar1.Location = new System.Drawing.Point(189, 501);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(300, 23);
            this.progressBar1.TabIndex = 15;
            // 
            // Arrbtn
            // 
            this.Arrbtn.Location = new System.Drawing.Point(0, 109);
            this.Arrbtn.Name = "Arrbtn";
            this.Arrbtn.Size = new System.Drawing.Size(78, 43);
            this.Arrbtn.TabIndex = 18;
            this.Arrbtn.Text = "Сообщение";
            this.Arrbtn.UseVisualStyleBackColor = true;
            this.Arrbtn.Click += new System.EventHandler(this.button3_Click);
            // 
            // Peobtn
            // 
            this.Peobtn.Location = new System.Drawing.Point(0, 202);
            this.Peobtn.Name = "Peobtn";
            this.Peobtn.Size = new System.Drawing.Size(78, 35);
            this.Peobtn.TabIndex = 19;
            this.Peobtn.Text = "Актёр";
            this.Peobtn.UseVisualStyleBackColor = true;
            this.Peobtn.Click += new System.EventHandler(this.Peobtn_Click);
            // 
            // Diabtn
            // 
            this.Diabtn.Location = new System.Drawing.Point(84, 202);
            this.Diabtn.Name = "Diabtn";
            this.Diabtn.Size = new System.Drawing.Size(90, 35);
            this.Diabtn.TabIndex = 20;
            this.Diabtn.Text = "Объект";
            this.Diabtn.UseVisualStyleBackColor = true;
            this.Diabtn.Click += new System.EventHandler(this.Diabtn_Click);
            // 
            // Retbtn
            // 
            this.Retbtn.Location = new System.Drawing.Point(0, 243);
            this.Retbtn.Name = "Retbtn";
            this.Retbtn.Size = new System.Drawing.Size(78, 71);
            this.Retbtn.TabIndex = 21;
            this.Retbtn.Text = "Результат";
            this.Retbtn.UseVisualStyleBackColor = true;
            this.Retbtn.Click += new System.EventHandler(this.Retbtn_Click);
            // 
            // Inbtn
            // 
            this.Inbtn.Location = new System.Drawing.Point(85, 243);
            this.Inbtn.Name = "Inbtn";
            this.Inbtn.Size = new System.Drawing.Size(89, 71);
            this.Inbtn.TabIndex = 22;
            this.Inbtn.Text = "Интервал";
            this.Inbtn.UseVisualStyleBackColor = true;
            this.Inbtn.Click += new System.EventHandler(this.Inbtn_Click);
            // 
            // Aggbtn
            // 
            this.Aggbtn.Location = new System.Drawing.Point(0, 320);
            this.Aggbtn.Name = "Aggbtn";
            this.Aggbtn.Size = new System.Drawing.Size(78, 59);
            this.Aggbtn.TabIndex = 23;
            this.Aggbtn.Text = "Аггрегация";
            this.Aggbtn.UseVisualStyleBackColor = true;
            this.Aggbtn.Click += new System.EventHandler(this.Aggbtn_Click);
            // 
            // Inherbtn
            // 
            this.Inherbtn.Location = new System.Drawing.Point(84, 320);
            this.Inherbtn.Name = "Inherbtn";
            this.Inherbtn.Size = new System.Drawing.Size(90, 59);
            this.Inherbtn.TabIndex = 24;
            this.Inherbtn.Text = "Наследование";
            this.Inherbtn.UseVisualStyleBackColor = true;
            this.Inherbtn.Click += new System.EventHandler(this.Instbtn_Click);
            // 
            // Combtn
            // 
            this.Combtn.Location = new System.Drawing.Point(0, 385);
            this.Combtn.Name = "Combtn";
            this.Combtn.Size = new System.Drawing.Size(78, 57);
            this.Combtn.TabIndex = 25;
            this.Combtn.Text = "Композиция";
            this.Combtn.UseVisualStyleBackColor = true;
            this.Combtn.Click += new System.EventHandler(this.Combtn_Click);
            // 
            // Assyncbtn
            // 
            this.Assyncbtn.Location = new System.Drawing.Point(85, 385);
            this.Assyncbtn.Name = "Assyncbtn";
            this.Assyncbtn.Size = new System.Drawing.Size(89, 57);
            this.Assyncbtn.TabIndex = 26;
            this.Assyncbtn.Text = "Асинхронное сообщение";
            this.Assyncbtn.UseVisualStyleBackColor = true;
            this.Assyncbtn.Click += new System.EventHandler(this.Assyncbtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(937, 536);
            this.Controls.Add(this.Assyncbtn);
            this.Controls.Add(this.Combtn);
            this.Controls.Add(this.Inherbtn);
            this.Controls.Add(this.Aggbtn);
            this.Controls.Add(this.Inbtn);
            this.Controls.Add(this.Retbtn);
            this.Controls.Add(this.Diabtn);
            this.Controls.Add(this.Peobtn);
            this.Controls.Add(this.Arrbtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.Stop);
            this.Controls.Add(this.Start);
            this.Controls.Add(this.Way);
            this.Controls.Add(this.OpenFile);
            this.Controls.Add(this.open);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button open;
        private System.Windows.Forms.Button OpenFile;
        private System.Windows.Forms.Button Way;
        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.Button Stop;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button Arrbtn;
        private System.Windows.Forms.Button Peobtn;
        private System.Windows.Forms.Button Diabtn;
        private System.Windows.Forms.Button Retbtn;
        private System.Windows.Forms.Button Inbtn;
        private System.Windows.Forms.Button Aggbtn;
        private System.Windows.Forms.Button Inherbtn;
        private System.Windows.Forms.Button Combtn;
        private System.Windows.Forms.Button Assyncbtn;
    }
}

