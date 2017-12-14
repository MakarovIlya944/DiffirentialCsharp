using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tao.FreeGlut;
using Tao.OpenGl;
using Tao.Platform.Windows;
using System.IO;

namespace DiffirentialCsharp
{
	public partial class Form1 : Form
	{
		public string fileans;

		List<PointSolve> Net = new List<PointSolve>();

		public Form1()
		{
			InitializeComponent();
		}

		private void button_calculate_Click(object sender, EventArgs e)
		{
			string[] s = textBox_startcondition.Text.Split(';');
			Vertex Start = new Vertex(s.Length);
			for (int i = 0; i < s.Length; i++)
				Start.v[i] = Convert.ToDouble(s[i]);
			Cauchy system = new Cauchy(Convert.ToDouble(textBox_leftborder.Text), Convert.ToDouble(textBox_rightborder.Text), Convert.ToDouble(textBox_step.Text), Start);
			string w = "";
			switch (domainUpDown1.Text)
			{
				case "Метод Эйлера":
					Net = system.Eiler(out w);
					break;
				case "Метод Рунге-Кутты":
					system.RungeKutta(out w);
					break;
				case "Метод Трапеции (прогноз-коррекция)":
					system.Trapetion(out w, Convert.ToDouble(textBox_accuracy.Text));
					break;
			}
			richTextBox1.Text = w;
		}

		private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
		{
			if (domainUpDown1.Text == "Метод Трапеции (прогноз-коррекция)")
				textBox_accuracy.Visible = true;
			else
				textBox_accuracy.Visible = false;
		}
		
		private void лабараторная1ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			label_startcondition.Text = "Начальное условие";
			label_step.Visible = true;
			button_calculate.Visible = true;
			checkBox_yvertex.Visible = true;
			domainUpDown1.Visible = true;
			buttonOpenfile.Visible = false;
			buttonCreate.Visible = false;
		}

		private void лабараторная2ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			textBox_accuracy.Visible = false;
			textBox_startcondition.Visible = false;
			label_startcondition.Text = "Значение y(0,5) = ";
			button_calculate.Visible = false;
			checkBox_yvertex.Visible = false;
			domainUpDown1.Visible = false;
			buttonOpenfile.Visible = true;
			buttonCreate.Visible = true;
		}

		private void buttonOpenfile_Click(object sender, EventArgs e)
		{
			richTextBox1.Text = "";
			DialogResult res = openFileDialog1.ShowDialog();
			// если файл выбран - и возвращен результат OK 
			if (res == DialogResult.OK)
			{
				string url = openFileDialog1.FileName;
				StreamReader F = new StreamReader(url, Encoding.Default);

				string s = "";

				DiagEq sys = new DiagEq();
				sys.Init(F);
				if (sys.Forward())
					sys.Reverse(out s);

				richTextBox1.Text += s;

				sys.GetString_01(out s, Convert.ToDouble(textBox_step.Text));
				string path = Directory.GetCurrentDirectory() + @"\test6.csv";
				StreamWriter T = File.AppendText(path);
				T.WriteLine(s);

				T.Close();
				F.Close();
			}
		}

		private void buttonCreate_Click(object sender, EventArgs e)
		{
			string S;

			double h = Convert.ToDouble(textBox_step.Text);
			double l = Convert.ToDouble(textBox_leftborder.Text);
			double r = Convert.ToDouble(textBox_rightborder.Text);

			CreateMatrix(l, r, h, out S);

			if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
				return;
			// получаем выбранный файл
			string filename = saveFileDialog1.FileName;
			fileans = filename;
			// сохраняем текст в файл
			System.IO.File.WriteAllText(filename, S);
		}

		private void CreateMatrix(double l, double r, double h, out string s)
		{
			s = "";
			int n = (int)Math.Ceiling((r - l) / h);
			s += (n + 1).ToString() + Environment.NewLine;
			for (int i = 1; i <= n; i++)
				s += (1 + (l + i * h) * (l + i * h) * h / 2d).ToString() + " ";
			s += Environment.NewLine;
			s += (-1 - 1d / h).ToString() + " ";
			for (int i = 1; i <= n; i++)
				s += (-2 - 2 * h * h / (l + i * h) / (l + i * h)).ToString() + " ";
			s += Environment.NewLine;
			s += (1d / h).ToString() + " ";
			for (int i = 1; i < n; i++)
				s += (1 - (l + i * h) * (l + i * h) * h / 2d).ToString() + " ";
			s += Environment.NewLine;
			s += (6 - h / 2d).ToString() + " ";
			for (int i = 1; i < n; i++)
				s += (h*h).ToString() + " ";
			s += (h * h - 1).ToString();
		}
		
	}

	public struct PointSolve
	{
		public double x;
		public Vertex y;
		public PointSolve(double X, Vertex Y)
		{
			x = X;
			y = Y;
		}
		public static bool operator <(PointSolve a, PointSolve b)
		{
			return a.y.x < b.y.x;
		}
		public static bool operator >(PointSolve a, PointSolve b)
		{
			return a.y.x > b.y.x;
		}
	}

	public class Vertex
	{
		static public int Dimension;
		public double[] v;

		public double x { get { return v[0]; } }
		public double y { get { return v[1]; } }
		public double z { get { return v[2]; } }

		static Vertex()
		{
			Dimension = 3;
		}

		public Vertex()
		{
			v = new double[Dimension];
			for (int i = 0; i < Dimension; i++)
				v[i] = 0;
		}

		public Vertex(Vertex a)
		{
			v = new double[Dimension];
			for (int i = 0; i < Dimension; i++)
				v[i] = a.v[i];
		}

		public Vertex(int k)
		{
			Dimension = k;
			v = new double[Dimension];
			for (int i = 0; i < Dimension; i++)
				v[i] = 1;
		}

		public Vertex(int[] a)
		{
			v = new double[Dimension];
			for (int i = 0; i < Dimension; i++)
				v[i] = a[i];
		}

		public Vertex(float[] a)
		{
			v = new double[Dimension];
			for (int i = 0; i < Dimension; i++)
				v[i] = a[i];
		}

		public Vertex(double[] a)
		{
			v = new double[Dimension];
			for (int i = 0; i < Dimension; i++)
				v[i] = a[i];
		}

		public static Vertex operator +(Vertex a, Vertex b)
		{
			Vertex c = new Vertex(a);
			for (int i = 0; i < Dimension; i++)
				c.v[i] += b.v[i];
			return c;
		}

		public static Vertex operator -(Vertex a, Vertex b)
		{
			Vertex c = new Vertex(a);
			for (int i = 0; i < Dimension; i++)
				c.v[i] -= b.v[i];
			return c;
		}

		public static Vertex operator -(Vertex a)
		{
			Vertex c = new Vertex();
			for (int i = 0; i < Dimension; i++)
				c.v[i] = -a.v[i];
			return c;
		}

		public static Vertex operator *(Vertex a, double k)
		{
			Vertex c = new Vertex(a);
			for (int i = 0; i < Dimension; i++)
				c.v[i] *= k;
			return c;
		}

		public static Vertex operator *(double k, Vertex a)
		{
			Vertex c = new Vertex(a);
			for (int i = 0; i < Dimension; i++)
				c.v[i] *= k;
			return c;
		}

		public static Vertex operator *(Vertex a, float k)
		{
			Vertex c = new Vertex(a);
			for (int i = 0; i < Dimension; i++)
				c.v[i] *= k;
			return c;
		}

		public static Vertex operator *(Vertex a, Vertex b)
		{//не скалярное умножение
			Vertex c = new Vertex(a);
			for (int i = 0; i < Dimension; i++)
				c.v[i] *= b.v[i];
			return c;
		}

		/*public static Vertex operator *(Vertex a, float[] A)
		{//умножение на матрицу
			Vertex c = new Vertex();
			c.v = A[0] * a.v + A[1] * a.y + A[2] * a.z;
			c.y = A[3] * a.v + A[4] * a.y + A[5] * a.z;
			c.z = A[6] * a.v + A[7] * a.y + A[8] * a.z;
			return c;
		}*/

		public static bool operator <(Vertex a, Vertex b)
		{
			return a.NormaEvklid() < b.NormaEvklid();
		}

		public static bool operator >(Vertex a, Vertex b)
		{
			return a.NormaEvklid() > b.NormaEvklid();
		}

		public bool SmallerAllComponents(Vertex a)
		{
			bool b = true;
			for (int i = 0; i < Dimension; i++)
				b &= v[i] < a.v[i];
			return b;
		}

		public bool GreaterAllComponents(Vertex a)
		{
			bool b = true;
			for (int i = 0; i < Dimension; i++)
				b &= v[i] > a.v[i];
			return b;
		}

		public double NormaEvklid()
		{
			if (Dimension != 1)
			{
				double sum = 0;
				for (int i = 0; i < Dimension; i++)
					sum += v[i] * v[i];
				return Math.Sqrt(Math.Abs(sum));
			}
			return Math.Abs(x);
		}

		public void NormirovkaEvklid()
		{
			double Norma = 1 / this.NormaEvklid();
			for (int i = 0; i < Dimension; i++)
				v[i] *= Norma;
		}
	}

	public abstract class DiffurEq
	{
		public double Left, Right, Step;
		public Vertex Start;
		public double Eps = 1E-5;
		public abstract Vertex Exact(double x);
		public double[] Points = new double[12] { 0, 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1, 1 };
		public Vertex RightPart(double x, Vertex y)
		{
			Vertex a = new Vertex(y);
			//a.v[0] = y.x / x + x;
			a.v[0] = -1000 * y.x + 1000 * y.y + 1000 * y.z;
			a.v[1] = -999 * y.y;
			a.v[2] = y.z;
			return a;
		}
	}

	public class Cauchy : DiffurEq
	{
		public Cauchy(double left, double right, double step, Vertex start)
		{
			Left = left;
			Right = right;
			Step = step;
			Start = start;
		}
		public void NewCauchy(double left, double right, double step, Vertex start)
		{
			Left = left;
			Right = right;
			Step = step;
			Start = start;
		}
		public override Vertex Exact(double x)
		{//точное значение функции
			Vertex a = new Vertex();
			//a.v[0] = x * x;
			a.v[0] = 1000 * Math.Exp(-999 * x) + 1000 / 1001 * Math.Exp(x) - (999 + 1000 / 1001) * Math.Exp(-1000 * x);
			a.v[1] = Math.Exp(-999 * x);
			a.v[2] = Math.Exp(x);
			return a;
		}
		public List<PointSolve> Eiler(out string S)
		{
			List<PointSolve> ans = new List<PointSolve>();
			//вычисление
			Vertex eiler = Start;
			double x = Left;
			string s = "";
			S = "";
			string path = Directory.GetCurrentDirectory() + @"\Eiler.csv";
			StreamWriter F = File.AppendText(path);
			F.WriteLine("x;y");
			int i = 1, k = 0;
			while (x < Right + Eps)
			{
				ans.Add(new PointSolve(x, eiler));
				for (int j = 0; j < Vertex.Dimension; j++)
				{
					double tmp = (Exact(x) - eiler).NormaEvklid();
					if (Vertex.Dimension == 1)
						tmp = Math.Abs(Exact(x).x - eiler.x);
					s = string.Format("{0:0.00000E+0}        {1:0.00000E+0}        {2:0.00000E+0}\n", x, eiler.v[j], tmp);
					//вывод в файл
					if (Math.Abs(x - Points[k]) < 1E-2 * Step)
						F.WriteLine("{0:0.00000000000000E+0};{1:0.00000000000000E+0}", x, eiler.v[j]);
					//вывод в приложении
					S += s;
					//вывод изображения
				}
				if (Math.Abs(x - Points[k]) < 1E-2 * Step)
					k++;
				s = "---------------------------------------------------------------\n";
				//F.Write(s);
				S += s;
				eiler = eiler + Step * RightPart(x, eiler);
				x = Left + i * Step;
				i++;
			}
			F.WriteLine("\n");
			F.Close();
			return ans;
		}

		public void RungeKutta(out string S)
		{
			Vertex k1, k2, k3;
			Vertex runge = Start;
			double x = Left;
			string s;
			S = "";
			string path = Directory.GetCurrentDirectory() + @"\RungeKutta.csv";
			StreamWriter F = File.AppendText(path);
			F.WriteLine("x;y;||y-Y||");
			int i = 1, k = 0;
			while (x < Right + Eps)
			{
				for (int j = 0; j < Vertex.Dimension; j++)
				{
					double tmp = (Exact(x) - runge).NormaEvklid();
					if (Vertex.Dimension == 1)
						tmp = Math.Abs(Exact(x).x - runge.x);
					s = string.Format("{0:0.00000E+0}        {1:0.00000E+0}        {2:0.00000E+0}\n", x, runge.v[j], tmp);
					//вывод в файл
					if (Math.Abs(x - Points[k]) < 1E-2 * Step)
						F.WriteLine("{0:0.00000000000000E+0};{1:0.00000000000000E+0}", x, runge.v[j]);

					//вывод в приложении
					S += s;
					//вывод изображения
				}
				s = "---------------------------------------------------------------\n";
				if (Math.Abs(x - Points[k]) < 1E-2 * Step)
					k++;
				S += s;
				k1 = Step * RightPart(x, runge);
				k2 = Step * RightPart(x + Step / 2, runge + k1 * 0.5);
				k3 = Step * RightPart(x + Step, runge - k1 + 2 * k2);
				runge = runge + (k1 + 4 * k2 + k3) * (1.0 / 6);
				x = Left + i * Step;
				i++;
			}
			F.WriteLine("\n");

			F.Close();
		}

		public void Trapetion(out string S, double accuracy)
		{
			Vertex eiler = Start, correct = Start, prev = Start;
			double x = Left, step = Step;
			string s = "";
			S = "";
			string path = Directory.GetCurrentDirectory() + @"\Trapetion.csv";
			string path1 = Directory.GetCurrentDirectory() + @"\Trapetion_dots.csv";
			StreamWriter F = File.AppendText(path);
			StreamWriter F1 = File.AppendText(path1);
			F.WriteLine("x;y;||y-Y||");
			for (int j = 0; j < Vertex.Dimension; j++)
			{
				F.WriteLine("{0:0.00000000000000E+0};{1:0.00000000000000E+0};", x, Start.v[j]);
				F1.WriteLine("{0:0.00000000000000E+0};{1:0.00000000000000E+0};", x, Start.v[j]);
				S += string.Format("{0:0.00000E+0}        {1:0.00000E+0}        {2:0.00000E+0}\n", x, correct.v[j], 0);
			}
			int i = 1, k = 1;
			while (x < Right + Eps)
			{
				eiler = prev + step * RightPart(x, prev);
				correct = prev + (step / 2) * (RightPart(x, prev) + RightPart(x + step, eiler));
				if ((correct - eiler).NormaEvklid() > accuracy)
					step /= 2;
				//else if ((correct - eiler).NormaEvklid() < accuracy * 1E-2)
				//	step *= 2;
				else
				{
					x = Left + i * step;
					prev = correct;
					i++;
					double tmp = (Exact(x) - correct).NormaEvklid();
					s = string.Format("{0:0.00000E+0}        {1:0.00000E+0}        {2:0.00000E+0}\n", x, correct.v[0], tmp);
					//вывод в файл
					//F1.WriteLine("{0:0.00000000000000E+0};{1:0.00000000000000E+0}", x, correct.v[0]);
					if (Math.Abs(x - Points[k]) < 1E-2 * step)
						F.WriteLine("{0:0.00000000000000E+0};{1:0.00000000000000E+0};{2:0.00000000000000E+0}", x, correct.v[0], tmp);
					//вывод в приложении
					//S += s;
					//вывод изображения
					for (int j = 1; j < Vertex.Dimension; j++)
					{
						s = string.Format("{0:0.00000E+0}        {1:0.00000E+0}\n", x, correct.v[j], tmp);
						//вывод в файл
						//F1.WriteLine("{0:0.00000000000000E+0};{1:0.00000000000000E+0}", x, correct.v[j]);
						if (Math.Abs(x - Points[k]) < 1E-2 * step)
							F.WriteLine("{0:0.00000000000000E+0};{1:0.00000000000000E+0}", x, correct.v[j]);
						//вывод в приложении
						//S += s;
						//вывод изображения
					}
					s = "---------------------------------------------------------------\n";
					if (Math.Abs(x - Points[k]) < 1E-2 * step)
						k++;
					//S += s;
				}
			}
			S += "done";
			F.WriteLine("\n");
			F.Close();
			F1.Close();
		}
	}

	public class DiagEq
	{
		private int Dimension;
		private double[,] a;
		private double[] r;

		public void Init(StreamReader F)
		{
			try
			{
				string s = F.ReadLine();
				Dimension = Convert.ToInt16(s);

				a = new double[3, Dimension];
				s = F.ReadLine();//снизу вверх по диагоналям считываем
				string[] S = s.Split();
				for (int i = 1; i < Dimension; i++)
					a[0, i] = Convert.ToDouble(S[i - 1]);
				a[0, 0] = 0;


				s = F.ReadLine();
				S = s.Split();
				for (int i = 0; i < Dimension; i++)
					a[1, i] = Convert.ToDouble(S[i]);

				s = F.ReadLine();
				S = s.Split();
				for (int i = 0; i < Dimension - 1; i++)
					a[2, i] = Convert.ToDouble(S[i]);
				a[2, Dimension - 1] = 0;


				r = new double[Dimension];
				s = F.ReadLine();
				S = s.Split();
				for (int i = 0; i < Dimension; i++)
					r[i] = Convert.ToDouble(S[i]);
			}
			catch (FormatException)
			{
				MessageBox.Show("Ошибка типа!");
			}
		}

		public bool Forward()
		{
			//2 - беты 1 - лямбда
			a[2, 0] /= -a[1, 0];
			a[1, 0] = r[0] / a[1, 0];
			double tmp;
			for (int i = 1; i < Dimension; i++)
			{
				tmp = a[1, i] + a[0, i] * a[2, i - 1];
				if (Math.Abs(tmp) < 1E-20)
				{
					MessageBox.Show("Система не корректна");
					return false;
				}
				a[2, i] /= -tmp;
				if (Math.Abs(a[2, i]) > 1)
				{
					MessageBox.Show("Система не устойчива");
					return false;
				}
				a[1, i] = (r[i] - a[0, i] * a[1, i - 1]) / tmp;
			}
			return true;
		}

		public void Reverse(out string S)
		{
			//2 - беты 1 - лямбда
			//ответ в 0
			a[0, Dimension - 1] = a[1, Dimension - 1];//х н = лямбда н 
			S = "";
			S += a[0, Dimension - 1].ToString() + '\n';
			for (int i = Dimension - 2; i >= 0; i--)
			{
				a[0, i] = a[2, i] * a[0, i + 1] + a[1, i];
				S = a[0, i].ToString() + '\n' + S;
			}
			S += '\n' + Math.Abs(-0.5 * Math.Cos(0.5) - a[0, 0]).ToString();
		}

		public void GetString_01(out string S, double h)
		{
			//[0.5,1]
			S = "h;y\n";
			S += "0;" + a[0, 0].ToString() + '\n';
			double l = 0.5;
			double[] H = { 0.55, 0.6,0.65,0.7,0.75, 0.8, 0.85, 0.9,0.95, 1, 1 };
			for (int i = 1, k=0; k < 11 && i < Dimension; i++)
			{
				double x = l + i * h;
				if (H[k] < x + 1E-8)
				{
					S += x.ToString() + ";" + a[0, i].ToString() + '\n';
					k++;
				}
			}
		}
	}
}