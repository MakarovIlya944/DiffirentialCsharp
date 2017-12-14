namespace DiffirentialCsharp
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
			this.textBox_startcondition = new System.Windows.Forms.TextBox();
			this.Instruments = new System.Windows.Forms.Panel();
			this.buttonCreate = new System.Windows.Forms.Button();
			this.buttonOpenfile = new System.Windows.Forms.Button();
			this.textBox_accuracy = new System.Windows.Forms.TextBox();
			this.domainUpDown1 = new System.Windows.Forms.DomainUpDown();
			this.button_calculate = new System.Windows.Forms.Button();
			this.checkBox_yvertex = new System.Windows.Forms.CheckBox();
			this.textBox_step = new System.Windows.Forms.TextBox();
			this.textBox_rightborder = new System.Windows.Forms.TextBox();
			this.textBox_leftborder = new System.Windows.Forms.TextBox();
			this.label_step = new System.Windows.Forms.Label();
			this.label_startcondition = new System.Windows.Forms.Label();
			this.label_rightborder = new System.Windows.Forms.Label();
			this.label_leftborder = new System.Windows.Forms.Label();
			this.label_Instruments = new System.Windows.Forms.Label();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.выборЛабыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.лабараторная1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.лабараторная2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.Instruments.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// textBox_startcondition
			// 
			this.textBox_startcondition.Location = new System.Drawing.Point(15, 66);
			this.textBox_startcondition.Name = "textBox_startcondition";
			this.textBox_startcondition.Size = new System.Drawing.Size(200, 20);
			this.textBox_startcondition.TabIndex = 0;
			// 
			// Instruments
			// 
			this.Instruments.BackColor = System.Drawing.SystemColors.ControlDark;
			this.Instruments.Controls.Add(this.buttonCreate);
			this.Instruments.Controls.Add(this.buttonOpenfile);
			this.Instruments.Controls.Add(this.textBox_accuracy);
			this.Instruments.Controls.Add(this.domainUpDown1);
			this.Instruments.Controls.Add(this.button_calculate);
			this.Instruments.Controls.Add(this.checkBox_yvertex);
			this.Instruments.Controls.Add(this.textBox_step);
			this.Instruments.Controls.Add(this.textBox_rightborder);
			this.Instruments.Controls.Add(this.textBox_leftborder);
			this.Instruments.Controls.Add(this.textBox_startcondition);
			this.Instruments.Controls.Add(this.label_step);
			this.Instruments.Controls.Add(this.label_startcondition);
			this.Instruments.Controls.Add(this.label_rightborder);
			this.Instruments.Controls.Add(this.label_leftborder);
			this.Instruments.Controls.Add(this.label_Instruments);
			this.Instruments.Location = new System.Drawing.Point(12, 27);
			this.Instruments.Name = "Instruments";
			this.Instruments.Size = new System.Drawing.Size(400, 422);
			this.Instruments.TabIndex = 1;
			// 
			// buttonCreate
			// 
			this.buttonCreate.Location = new System.Drawing.Point(278, 390);
			this.buttonCreate.Name = "buttonCreate";
			this.buttonCreate.Size = new System.Drawing.Size(95, 23);
			this.buttonCreate.TabIndex = 10;
			this.buttonCreate.Text = "Создать файл";
			this.buttonCreate.UseVisualStyleBackColor = true;
			this.buttonCreate.Click += new System.EventHandler(this.buttonCreate_Click);
			// 
			// buttonOpenfile
			// 
			this.buttonOpenfile.Location = new System.Drawing.Point(146, 390);
			this.buttonOpenfile.Name = "buttonOpenfile";
			this.buttonOpenfile.Size = new System.Drawing.Size(117, 23);
			this.buttonOpenfile.TabIndex = 9;
			this.buttonOpenfile.Text = "Открыть файл";
			this.buttonOpenfile.UseVisualStyleBackColor = true;
			this.buttonOpenfile.Visible = false;
			this.buttonOpenfile.Click += new System.EventHandler(this.buttonOpenfile_Click);
			// 
			// textBox_accuracy
			// 
			this.textBox_accuracy.Location = new System.Drawing.Point(253, 296);
			this.textBox_accuracy.Name = "textBox_accuracy";
			this.textBox_accuracy.Size = new System.Drawing.Size(120, 20);
			this.textBox_accuracy.TabIndex = 8;
			this.textBox_accuracy.Visible = false;
			// 
			// domainUpDown1
			// 
			this.domainUpDown1.Items.Add("Метод Рунге-Кутты");
			this.domainUpDown1.Items.Add("Метод Трапеции (прогноз-коррекция)");
			this.domainUpDown1.Items.Add("Метод Эйлера");
			this.domainUpDown1.Location = new System.Drawing.Point(253, 269);
			this.domainUpDown1.MinimumSize = new System.Drawing.Size(120, 0);
			this.domainUpDown1.Name = "domainUpDown1";
			this.domainUpDown1.Size = new System.Drawing.Size(120, 20);
			this.domainUpDown1.TabIndex = 7;
			this.domainUpDown1.Text = "Метод Эйлера";
			this.domainUpDown1.SelectedItemChanged += new System.EventHandler(this.domainUpDown1_SelectedItemChanged);
			// 
			// button_calculate
			// 
			this.button_calculate.Location = new System.Drawing.Point(165, 390);
			this.button_calculate.Name = "button_calculate";
			this.button_calculate.Size = new System.Drawing.Size(73, 23);
			this.button_calculate.TabIndex = 6;
			this.button_calculate.Text = "Вычислить";
			this.button_calculate.UseVisualStyleBackColor = true;
			this.button_calculate.Click += new System.EventHandler(this.button_calculate_Click);
			// 
			// checkBox_yvertex
			// 
			this.checkBox_yvertex.AutoSize = true;
			this.checkBox_yvertex.Location = new System.Drawing.Point(267, 66);
			this.checkBox_yvertex.Name = "checkBox_yvertex";
			this.checkBox_yvertex.Size = new System.Drawing.Size(76, 17);
			this.checkBox_yvertex.TabIndex = 5;
			this.checkBox_yvertex.Text = "y - Вектор";
			this.checkBox_yvertex.UseVisualStyleBackColor = true;
			// 
			// textBox_step
			// 
			this.textBox_step.Location = new System.Drawing.Point(15, 216);
			this.textBox_step.Name = "textBox_step";
			this.textBox_step.Size = new System.Drawing.Size(200, 20);
			this.textBox_step.TabIndex = 0;
			this.textBox_step.Text = "0,01";
			// 
			// textBox_rightborder
			// 
			this.textBox_rightborder.Location = new System.Drawing.Point(15, 166);
			this.textBox_rightborder.Name = "textBox_rightborder";
			this.textBox_rightborder.Size = new System.Drawing.Size(200, 20);
			this.textBox_rightborder.TabIndex = 0;
			this.textBox_rightborder.Text = "1";
			// 
			// textBox_leftborder
			// 
			this.textBox_leftborder.Location = new System.Drawing.Point(15, 116);
			this.textBox_leftborder.Name = "textBox_leftborder";
			this.textBox_leftborder.Size = new System.Drawing.Size(200, 20);
			this.textBox_leftborder.TabIndex = 0;
			this.textBox_leftborder.Text = "0";
			// 
			// label_step
			// 
			this.label_step.AutoSize = true;
			this.label_step.Location = new System.Drawing.Point(15, 196);
			this.label_step.Name = "label_step";
			this.label_step.Size = new System.Drawing.Size(27, 13);
			this.label_step.TabIndex = 4;
			this.label_step.Text = "Шаг";
			// 
			// label_startcondition
			// 
			this.label_startcondition.AutoSize = true;
			this.label_startcondition.Location = new System.Drawing.Point(15, 46);
			this.label_startcondition.Name = "label_startcondition";
			this.label_startcondition.Size = new System.Drawing.Size(106, 13);
			this.label_startcondition.TabIndex = 3;
			this.label_startcondition.Text = "Начальное условие";
			// 
			// label_rightborder
			// 
			this.label_rightborder.AutoSize = true;
			this.label_rightborder.Location = new System.Drawing.Point(15, 146);
			this.label_rightborder.Name = "label_rightborder";
			this.label_rightborder.Size = new System.Drawing.Size(89, 13);
			this.label_rightborder.TabIndex = 2;
			this.label_rightborder.Text = "Правая граница";
			// 
			// label_leftborder
			// 
			this.label_leftborder.AutoSize = true;
			this.label_leftborder.Location = new System.Drawing.Point(15, 96);
			this.label_leftborder.Name = "label_leftborder";
			this.label_leftborder.Size = new System.Drawing.Size(77, 13);
			this.label_leftborder.TabIndex = 1;
			this.label_leftborder.Text = "Лева граница";
			// 
			// label_Instruments
			// 
			this.label_Instruments.AutoSize = true;
			this.label_Instruments.BackColor = System.Drawing.SystemColors.ControlLight;
			this.label_Instruments.Location = new System.Drawing.Point(162, 16);
			this.label_Instruments.Name = "label_Instruments";
			this.label_Instruments.Size = new System.Drawing.Size(76, 13);
			this.label_Instruments.TabIndex = 0;
			this.label_Instruments.Text = "Инструменты";
			// 
			// richTextBox1
			// 
			this.richTextBox1.Location = new System.Drawing.Point(418, 52);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.Size = new System.Drawing.Size(301, 397);
			this.richTextBox1.TabIndex = 3;
			this.richTextBox1.Text = "";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(539, 22);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(59, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "Результат";
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			this.openFileDialog1.Filter = "Тектовый файл| *.txt";
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.выборЛабыToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(728, 24);
			this.menuStrip1.TabIndex = 5;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// выборЛабыToolStripMenuItem
			// 
			this.выборЛабыToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.лабараторная1ToolStripMenuItem,
            this.лабараторная2ToolStripMenuItem});
			this.выборЛабыToolStripMenuItem.Name = "выборЛабыToolStripMenuItem";
			this.выборЛабыToolStripMenuItem.Size = new System.Drawing.Size(88, 20);
			this.выборЛабыToolStripMenuItem.Text = "Выбор лабы";
			// 
			// лабараторная1ToolStripMenuItem
			// 
			this.лабараторная1ToolStripMenuItem.Name = "лабараторная1ToolStripMenuItem";
			this.лабараторная1ToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
			this.лабараторная1ToolStripMenuItem.Text = "Лабараторная 1";
			this.лабараторная1ToolStripMenuItem.Click += new System.EventHandler(this.лабараторная1ToolStripMenuItem_Click);
			// 
			// лабараторная2ToolStripMenuItem
			// 
			this.лабараторная2ToolStripMenuItem.Name = "лабараторная2ToolStripMenuItem";
			this.лабараторная2ToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
			this.лабараторная2ToolStripMenuItem.Text = "Лабараторная 2";
			this.лабараторная2ToolStripMenuItem.Click += new System.EventHandler(this.лабараторная2ToolStripMenuItem_Click);
			// 
			// saveFileDialog1
			// 
			this.saveFileDialog1.Filter = "Тектовый файл| *.txt";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(728, 461);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.richTextBox1);
			this.Controls.Add(this.Instruments);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "Form1";
			this.Text = "Дифференциальные уравнения";
			this.Instruments.ResumeLayout(false);
			this.Instruments.PerformLayout();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox textBox_startcondition;
		private System.Windows.Forms.Panel Instruments;
		private System.Windows.Forms.Label label_Instruments;
		private System.Windows.Forms.Label label_step;
		private System.Windows.Forms.Label label_startcondition;
		private System.Windows.Forms.Label label_rightborder;
		private System.Windows.Forms.Label label_leftborder;
		private System.Windows.Forms.TextBox textBox_leftborder;
		private System.Windows.Forms.TextBox textBox_step;
		private System.Windows.Forms.TextBox textBox_rightborder;
		private System.Windows.Forms.Button button_calculate;
		private System.Windows.Forms.DomainUpDown domainUpDown1;
		private System.Windows.Forms.RichTextBox richTextBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBox_accuracy;
		private System.Windows.Forms.CheckBox checkBox_yvertex;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem выборЛабыToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem лабараторная1ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem лабараторная2ToolStripMenuItem;
		private System.Windows.Forms.Button buttonOpenfile;
		private System.Windows.Forms.Button buttonCreate;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
	}
}

