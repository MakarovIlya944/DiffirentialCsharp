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
			this.checkBox_yvertex = new System.Windows.Forms.CheckBox();
			this.checkBox_xvertex = new System.Windows.Forms.CheckBox();
			this.textBox_step = new System.Windows.Forms.TextBox();
			this.textBox_rightborder = new System.Windows.Forms.TextBox();
			this.textBoxleftborder = new System.Windows.Forms.TextBox();
			this.label_step = new System.Windows.Forms.Label();
			this.label_startcondition = new System.Windows.Forms.Label();
			this.label_rightborder = new System.Windows.Forms.Label();
			this.label_leftborder = new System.Windows.Forms.Label();
			this.label_Instruments = new System.Windows.Forms.Label();
			this.button_calculate = new System.Windows.Forms.Button();
			this.Instruments.SuspendLayout();
			this.SuspendLayout();
			// 
			// textBox_startcondition
			// 
			this.textBox_startcondition.Location = new System.Drawing.Point(15, 60);
			this.textBox_startcondition.Name = "textBox_startcondition";
			this.textBox_startcondition.Size = new System.Drawing.Size(200, 20);
			this.textBox_startcondition.TabIndex = 0;
			// 
			// Instruments
			// 
			this.Instruments.BackColor = System.Drawing.SystemColors.ControlDark;
			this.Instruments.Controls.Add(this.button_calculate);
			this.Instruments.Controls.Add(this.checkBox_yvertex);
			this.Instruments.Controls.Add(this.checkBox_xvertex);
			this.Instruments.Controls.Add(this.textBox_step);
			this.Instruments.Controls.Add(this.textBox_rightborder);
			this.Instruments.Controls.Add(this.textBoxleftborder);
			this.Instruments.Controls.Add(this.textBox_startcondition);
			this.Instruments.Controls.Add(this.label_step);
			this.Instruments.Controls.Add(this.label_startcondition);
			this.Instruments.Controls.Add(this.label_rightborder);
			this.Instruments.Controls.Add(this.label_leftborder);
			this.Instruments.Controls.Add(this.label_Instruments);
			this.Instruments.Location = new System.Drawing.Point(12, 12);
			this.Instruments.Name = "Instruments";
			this.Instruments.Size = new System.Drawing.Size(400, 437);
			this.Instruments.TabIndex = 1;
			// 
			// checkBox_yvertex
			// 
			this.checkBox_yvertex.AutoSize = true;
			this.checkBox_yvertex.Location = new System.Drawing.Point(268, 110);
			this.checkBox_yvertex.Name = "checkBox_yvertex";
			this.checkBox_yvertex.Size = new System.Drawing.Size(76, 17);
			this.checkBox_yvertex.TabIndex = 5;
			this.checkBox_yvertex.Text = "y - Вектор";
			this.checkBox_yvertex.UseVisualStyleBackColor = true;
			// 
			// checkBox_xvertex
			// 
			this.checkBox_xvertex.AutoSize = true;
			this.checkBox_xvertex.Location = new System.Drawing.Point(268, 63);
			this.checkBox_xvertex.Name = "checkBox_xvertex";
			this.checkBox_xvertex.Size = new System.Drawing.Size(76, 17);
			this.checkBox_xvertex.TabIndex = 5;
			this.checkBox_xvertex.Text = "x - Вектор";
			this.checkBox_xvertex.UseVisualStyleBackColor = true;
			// 
			// textBox_step
			// 
			this.textBox_step.Location = new System.Drawing.Point(15, 210);
			this.textBox_step.Name = "textBox_step";
			this.textBox_step.Size = new System.Drawing.Size(200, 20);
			this.textBox_step.TabIndex = 0;
			this.textBox_step.TextChanged += new System.EventHandler(this.textBoxleftborder_TextChanged);
			// 
			// textBox_rightborder
			// 
			this.textBox_rightborder.Location = new System.Drawing.Point(15, 160);
			this.textBox_rightborder.Name = "textBox_rightborder";
			this.textBox_rightborder.Size = new System.Drawing.Size(200, 20);
			this.textBox_rightborder.TabIndex = 0;
			this.textBox_rightborder.TextChanged += new System.EventHandler(this.textBoxleftborder_TextChanged);
			// 
			// textBoxleftborder
			// 
			this.textBoxleftborder.Location = new System.Drawing.Point(15, 110);
			this.textBoxleftborder.Name = "textBoxleftborder";
			this.textBoxleftborder.Size = new System.Drawing.Size(200, 20);
			this.textBoxleftborder.TabIndex = 0;
			this.textBoxleftborder.TextChanged += new System.EventHandler(this.textBoxleftborder_TextChanged);
			// 
			// label_step
			// 
			this.label_step.AutoSize = true;
			this.label_step.Location = new System.Drawing.Point(15, 190);
			this.label_step.Name = "label_step";
			this.label_step.Size = new System.Drawing.Size(27, 13);
			this.label_step.TabIndex = 4;
			this.label_step.Text = "Шаг";
			// 
			// label_startcondition
			// 
			this.label_startcondition.AutoSize = true;
			this.label_startcondition.Location = new System.Drawing.Point(15, 40);
			this.label_startcondition.Name = "label_startcondition";
			this.label_startcondition.Size = new System.Drawing.Size(106, 13);
			this.label_startcondition.TabIndex = 3;
			this.label_startcondition.Text = "Начальное условие";
			// 
			// label_rightborder
			// 
			this.label_rightborder.AutoSize = true;
			this.label_rightborder.Location = new System.Drawing.Point(15, 140);
			this.label_rightborder.Name = "label_rightborder";
			this.label_rightborder.Size = new System.Drawing.Size(89, 13);
			this.label_rightborder.TabIndex = 2;
			this.label_rightborder.Text = "Правая граница";
			// 
			// label_leftborder
			// 
			this.label_leftborder.AutoSize = true;
			this.label_leftborder.Location = new System.Drawing.Point(15, 90);
			this.label_leftborder.Name = "label_leftborder";
			this.label_leftborder.Size = new System.Drawing.Size(77, 13);
			this.label_leftborder.TabIndex = 1;
			this.label_leftborder.Text = "Лева граница";
			// 
			// label_Instruments
			// 
			this.label_Instruments.AutoSize = true;
			this.label_Instruments.BackColor = System.Drawing.SystemColors.ControlLight;
			this.label_Instruments.Location = new System.Drawing.Point(162, 10);
			this.label_Instruments.Name = "label_Instruments";
			this.label_Instruments.Size = new System.Drawing.Size(76, 13);
			this.label_Instruments.TabIndex = 0;
			this.label_Instruments.Text = "Инструменты";
			// 
			// button_calculate
			// 
			this.button_calculate.Location = new System.Drawing.Point(165, 411);
			this.button_calculate.Name = "button_calculate";
			this.button_calculate.Size = new System.Drawing.Size(73, 23);
			this.button_calculate.TabIndex = 6;
			this.button_calculate.Text = "Вычислить";
			this.button_calculate.UseVisualStyleBackColor = true;
			this.button_calculate.Click += new System.EventHandler(this.button_calculate_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(984, 461);
			this.Controls.Add(this.Instruments);
			this.Name = "Form1";
			this.Text = "Form1";
			this.Instruments.ResumeLayout(false);
			this.Instruments.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TextBox textBox_startcondition;
		private System.Windows.Forms.Panel Instruments;
		private System.Windows.Forms.Label label_Instruments;
		private System.Windows.Forms.Label label_step;
		private System.Windows.Forms.Label label_startcondition;
		private System.Windows.Forms.Label label_rightborder;
		private System.Windows.Forms.Label label_leftborder;
		private System.Windows.Forms.TextBox textBoxleftborder;
		private System.Windows.Forms.CheckBox checkBox_yvertex;
		private System.Windows.Forms.CheckBox checkBox_xvertex;
		private System.Windows.Forms.TextBox textBox_step;
		private System.Windows.Forms.TextBox textBox_rightborder;
		private System.Windows.Forms.Button button_calculate;
	}
}

