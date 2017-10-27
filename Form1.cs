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
		List<PointSolve> Net = new List<PointSolve>();
		//Для OpenGl
		public int cube_ = 0, flour_ = 0, sphere_ = 0;
		public double time = 0;
		public Point WindowWH, Mouse;
		public Vertex eye = new Vertex(new double[3] { 0.0, 0.0, 0.0 });
		public Vertex center = new Vertex(new double[3] { 0.0, 0.0, 0.0 }), center_ = new Vertex(new double[3] { 0.0, 0.0, 0.0 });

		public double fi = 60, psi = 40, fi_ = 60, psi_ = 40, r = 70;
		public Point mousexy = new Point();

		public float param = 50.0f;

		public Form1()
		{
			InitializeComponent();
			OpenGLWindow.InitializeContexts();
		}

		private void OpenGLWindow_Load(object sender, EventArgs e)
		{
			//----------------------------OpenGL-------------------------------
			Glut.glutInit();
			Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_DOUBLE | Glut.GLUT_DEPTH);

			// цвет отчистки окна 
			Gl.glClearColor(0, 0, 0, 1);

			// установка порта вывода в соответствии с размерами элемента OpenGLWindow 
			Gl.glViewport(0, 0, OpenGLWindow.Width, OpenGLWindow.Height);

			// настройка проекции 
			Gl.glMatrixMode(Gl.GL_PROJECTION);
			Gl.glLoadIdentity();
			Glu.gluPerspective(45, (float)OpenGLWindow.Width / (float)OpenGLWindow.Height, 0.1, 200);

			// модельно видовые преобразования
			Gl.glMatrixMode(Gl.GL_MODELVIEW);
			Gl.glLoadIdentity();

			// настройка параметров OpenGL для визуализации 
			Gl.glEnable(Gl.GL_DEPTH_TEST);
			Gl.glShadeModel(Gl.GL_SMOOTH);

			// освещение
			Gl.glEnable(Gl.GL_LIGHTING);
			Gl.glEnable(Gl.GL_LIGHT0);
			Gl.glEnable(Gl.GL_LIGHT1);

			// смещивание
			Gl.glEnable(Gl.GL_BLEND);
			Gl.glBlendFunc(Gl.GL_SRC_ALPHA, Gl.GL_ONE_MINUS_SRC_ALPHA);

			// точка
			Gl.glEnable(Gl.GL_POINT_SMOOTH);
			//-----------------------------OpenGL------------------------------

		}

		private void button_calculate_Click(object sender, EventArgs e)
		{
			panel1.Visible = false;
			OpenGLWindow.Visible = false;
			if (checkBox_yvertex.Checked)
				OpenGLWindow.Visible = true;
			else
				panel1.Visible = true;


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

		private void panel1_Paint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			DrawNet(e);
			DrawFunction2D(e);
		}

		private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
		{
			if (domainUpDown1.Text == "Метод Трапеции (прогноз-коррекция)")
				textBox_accuracy.Visible = true;
			else
				textBox_accuracy.Visible = false;
		}

		private void DrawFunction2D(PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			int num = Net.Count;
			double min, max;
			MaxMinPointSolve(out min, out max);
			float kx = (float)(panel1.Width - 5) / num, ky = (float)((panel1.Height - 5) / (max - min));
			for (int i = 0; i < num - 1; i++)
			{
				Pen p = new Pen(Color.FromArgb(0, (int)(255 * ((float)(i + 1) / num)), (int)(255 * ((float)(i + 1) / num))));
				g.DrawLine(p, kx * i + 5, panel1.Height - ky * (float)Net[i].y.v[0], kx * (i + 1) + 5, panel1.Height - ky * (float)Net[i + 1].y.v[0]);
			}
		}

		private void лабараторная1ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			textBox_accuracy.Visible = true;
			textBox_leftborder.Visible = true;
			textBox_rightborder.Visible = true;
			textBox_startcondition.Visible = true;
			textBox_step.Visible = true;
			label_leftborder.Visible = true;
			label_rightborder.Visible = true;
			label_startcondition.Visible = true;
			label_step.Visible = true;
			buttonOpenfile.Visible = false;
		}

		private void лабараторная2ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			textBox_accuracy.Visible = false;
			textBox_leftborder.Visible = false;
			textBox_rightborder.Visible = false;
			textBox_startcondition.Visible = false;
			textBox_step.Visible = false;
			label_leftborder.Visible = false;
			label_rightborder.Visible = false;
			label_startcondition.Visible = false;
			label_step.Visible = false;
			buttonOpenfile.Visible = true;
		}

		private void buttonOpenfile_Click(object sender, EventArgs e)
		{
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

				F.Close();
			}
		}

		private void MaxMinPointSolve(out double min, out double max)
		{
			min = Net[0].y.x; max = min;
			foreach (var tmp in Net)
			{
				var t = tmp.y.x;
				if (t < min)
					min = t;
				if (t > max)
					max = t;
			}
		}

		private void DrawNet(PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			int nx = 0, ny = 0;
			float dx = 20, dy = 20;
			float line = 0;
			while (panel1.Width > nx * dx)
			{
				g.DrawLine(Pens.Black, line + nx * dx, 0, line + nx * dx, panel1.Height);
				nx++;
			}
			line = panel1.Height - 1;
			while (line > ny * dy)
			{
				g.DrawLine(Pens.Black, 0, line - ny * dy, panel1.Width, line - ny * dy);
				ny++;
			}
		}

		//OpenGL

		private void timer1_Tick(object sender, EventArgs e)
		{


			DrawFuncion();

			time += timer1.Interval / 1000.0;
		}

		private void Init()
		{
			flour_ = Gl.glGenLists(1);
			Gl.glNewList(flour_, Gl.GL_COMPILE);
			Flour();
			Gl.glEndList();

			sphere_ = Gl.glGenLists(1);
			Gl.glNewList(sphere_, Gl.GL_COMPILE);
			Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_AMBIENT_AND_DIFFUSE, new float[4] { 0.8f, 0.8f, 1, 0.4f });
			Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_SPECULAR, new float[4] { 1, 1, 1, 1 });
			Gl.glMaterialf(Gl.GL_FRONT, Gl.GL_SHININESS, 60);
			Glut.glutSolidSphere(2, 32, 32);
			Gl.glEndList();
		}

		private void DrawFuncion()
		{
			Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
			Gl.glLoadIdentity();
			Glu.gluLookAt(eye.x, eye.y, eye.z, center.x, center.y, center.z, 0, 1, 0);

			Light();
			Lamp(new Vertex(new double[3] { 0, 20, 0 }), new Vertex(new double[3] { 0, -1, 0 }));

			Gl.glPushMatrix();
			Gl.glTranslated(0, -2, 0);
			Gl.glCallList(flour_);
			Gl.glPopMatrix();

			Gl.glFlush();
			OpenGLWindow.Invalidate();
		}

		//Свет

		private void Lamp(Vertex position, Vertex vector)
		{
			vector.NormirovkaEvklid();
			Gl.glPushMatrix();
			Gl.glTranslated(position.x, position.y, position.z);
			Gl.glDisable(Gl.GL_LIGHTING);
			Gl.glColor3d(0, 0, 0);
			Glut.glutWireCube(1);
			Gl.glEnable(Gl.GL_LIGHTING);

			float[] light1_positiopn = { (float)position.x, (float)position.y, (float)position.z, 1 };
			float[] light1_direction = { (float)vector.x, (float)vector.y, (float)vector.z };
			float[] light1_ambient = { param / 100.0f, param / 100.0f, param / 100.0f, 1 };

			Gl.glLightfv(Gl.GL_LIGHT1, Gl.GL_AMBIENT, light1_ambient);
			Gl.glLightfv(Gl.GL_LIGHT1, Gl.GL_POSITION, light1_positiopn);
			Gl.glLightfv(Gl.GL_LIGHT1, Gl.GL_DIFFUSE, light1_ambient);
			Gl.glLightfv(Gl.GL_LIGHT1, Gl.GL_SPECULAR, new float[] { 0, 0.6f, 0.1f, 0 });

			Gl.glLightf(Gl.GL_LIGHT1, Gl.GL_SPOT_CUTOFF, 0.9f);
			Gl.glLightfv(Gl.GL_LIGHT1, Gl.GL_SPOT_DIRECTION, light1_direction);
			Gl.glLightf(Gl.GL_LIGHT1, Gl.GL_SPOT_EXPONENT, 0.8f);

			Gl.glPopMatrix();
		}

		private void Light()
		{
			float[] light_position = { 0, 2, 0, 0 };
			float[] light_ambient = { 0.1f, 0.1f, 0.1f, 1 };
			float[] white_light = { 1, 1, 1, 1 };

			Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_AMBIENT, light_ambient);
			Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_POSITION, light_position);
			Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_DIFFUSE, light_ambient);
			Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_SPECULAR, white_light);
		}

		//Поверхность

		private void Flour()
		{
			Gl.glBegin(Gl.GL_TRIANGLES);
			Gl.glColor3d(0.1, 0.4, 0.1);
			Gl.glMaterialf(Gl.GL_FRONT, Gl.GL_SHININESS, 0);
			Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_SPECULAR, new float[] { 0, 0, 0, 1 });
			Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_AMBIENT_AND_DIFFUSE, new float[] { 0.1f, 0.4f, 0.1f, 1 });
			int a = 100;
			float k = 0.2f, end = (float)a / k * 2;
			for (int i = 0; i < end; i++)
				for (int j = 0; j < end; j++)
				{
					Gl.glNormal3d(0, 1, 0);
					Gl.glVertex3f(i * k - a, 0, j * k - a);
					Gl.glVertex3f((1 + i) * k - a, 0, j * k - a);
					Gl.glVertex3f((1 + i) * k - a, 0, (1 + j) * k - a);
					Gl.glVertex3f(i * k - a, 0, j * k - a);
					Gl.glVertex3f(i * k - a, 0, (1 + j) * k - a);
					Gl.glVertex3f((1 + i) * k - a, 0, (1 + j) * k - a);
				}
			Gl.glEnd();
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
			a.v[0] = -100 * y.x + 100 * y.y + 100 * y.z;
			a.v[1] = -99 * y.y;
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
			Vertex a = new Vertex(Vertex.Dimension);
			//a.v[0] = x * x;
			a.v[0] = 1;
			a.v[1] = x;
			a.v[2] = x * x;
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
			string s;
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
			}
			int i = 1, k = 1;
			while (x < Right + Eps)
			{
				eiler = prev + step * RightPart(x, prev);
				correct = prev + (step / 2) * (RightPart(x, prev) + RightPart(x + step, eiler));
				if ((correct - eiler).NormaEvklid() < accuracy)
				{
					x = Left + i * step;
					prev = correct;
					i++;
					for (int j = 0; j < Vertex.Dimension; j++)
					{
						double tmp = (Exact(x) - correct).NormaEvklid();
						s = string.Format("{0:0.00000E+0}        {1:0.00000E+0}        {2:0.00000E+0}\n", x, correct.v[j], tmp);
						//вывод в файл
						F1.WriteLine("{0:0.00000000000000E+0};{1:0.00000000000000E+0}", x, correct.v[j]);
						if (Math.Abs(x - Points[k]) < 1E-2 * step)
							F.WriteLine("{0:0.00000000000000E+0};{1:0.00000000000000E+0}", x, correct.v[j]);
						//вывод в приложении
						S += s;
						//вывод изображения
					}
					s = "---------------------------------------------------------------\n";
					if (Math.Abs(x - Points[k]) < 1E-2 * step)
						k++;
					S += s;

				}
				else
					step /= 2;
			}
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
			try {
				string s = F.ReadLine();
				Dimension = Convert.ToInt16(s);

				a = new double[3, Dimension];
				s = F.ReadLine();//снизу вверх по диагоналям считываем
				string[] S = s.Split();
				for (int i = 1; i < Dimension; i++)
					a[0, i] = Convert.ToDouble(S[i-1]);
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
			catch(FormatException)
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
			for (int i = 1;i<Dimension;i++)
			{
				tmp = a[1, i] + a[0, i] * a[2, i - 1];
				if(Math.Abs(tmp) < 1E-20)
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
				S += a[0, i].ToString() + '\n';
			}
			S = S.Reverse().ToString();
		}
	}
}