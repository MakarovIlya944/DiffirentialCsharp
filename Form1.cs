using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace DiffirentialCsharp
{
	public partial class Form1 : Form
	{
		DifferentialEquation system;

		public Form1()
		{
			InitializeComponent();
		}

		private void textBoxleftborder_TextChanged(object sender, EventArgs e)
		{

		}

		private void button_calculate_Click(object sender, EventArgs e)
		{
			system = new DifferentialEquation((float)Convert.ToDouble(textBox_step), (float)Convert.ToDouble(textBox_leftborder), (float)Convert.ToDouble(textBox_rightborder), (float)Convert.ToDouble(textBox_startcondition));
		}

		private void Form1_Load(object sender, EventArgs e)
		{

		}
	}

	public class Vertex
	{
		public float x, y, z;

		public Vertex()
		{
			x = 0; y = 0; z = 0;
		}

		public Vertex(int k)
		{
			x = 1;y = 1;z = 1;
		}

		public Vertex(Vertex a)
		{
			x = a.x;
			y = a.y;
			z = a.z;
		}

		public Vertex(int a, int b, int c)
		{
			x = a; y = b; z = c;
		}

		public Vertex(float a, float b, float c)
		{
			x = a; y = b; z = c;
		}

		public Vertex(double a, double b, double c)
		{
			x = (float)a; y = (float)b; z = (float)c;
		}

		public static Vertex operator +(Vertex a, Vertex b)
		{
			Vertex c = a;
			c.x += b.x;
			c.y += b.y;
			c.z += b.z;
			return c;
		}

		public static Vertex operator -(Vertex a, Vertex b)
		{
			Vertex c = a;
			c.x -= b.x;
			c.y -= b.y;
			c.z -= b.z;
			return c;
		}

		public static Vertex operator -(Vertex a)
		{
			Vertex c = a;
			c.x = -a.x;
			c.y = -a.y;
			c.z = -a.z;
			return c;
		}

		public static Vertex operator *(Vertex a, double k)
		{
			Vertex c = a;
			c.x *= (float)k;
			c.y *= (float)k;
			c.z *= (float)k;
			return c;
		}

		public static Vertex operator *(Vertex a, float k)
		{
			Vertex c = a;
			c.x *= k;
			c.y *= k;
			c.z *= k;
			return c;
		}

		public static Vertex operator *(Vertex a, Vertex b)
		{//не скалярное умножение
			Vertex c = new Vertex();
			c.x = b.x * a.x;
			c.y = b.y * a.y;
			c.z = b.z * a.z;
			return c;
		}

		public static Vertex operator *(Vertex a, float[] A)
		{//умножение на матрицу
			Vertex c = new Vertex();
			c.x = A[0] * a.x + A[1] * a.y + A[2] * a.z;
			c.y = A[3] * a.x + A[4] * a.y + A[5] * a.z;
			c.z = A[6] * a.x + A[7] * a.y + A[8] * a.z;
			return c;
		}

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
			b &= x <= a.x;
			b &= y <= a.y;
			b &= z <= a.z;
			return b;
		}

		public bool GreaterAllComponents(Vertex a)
		{
			bool b = true;
			b &= x >= a.x;
			b &= y >= a.y;
			b &= z >= a.z;
			return b;
		}

		public float NormaEvklid()
		{
			return (float)Math.Sqrt(x * x + y * y + z * z);
		}

		public void NormirovkaEvklid()
		{
			float Norma = this.NormaEvklid();
			x /= Norma;
			y /= Norma;
			z /= Norma;
		}
	}

	public class DifferentialEquation
	{

		//Function:X->Y
		public bool IsXVertex = false;
		public bool IsYVertex = false;
		public bool IsUniformStep = true;

		public DifferentialEquation()
		{

		}

		public DifferentialEquation(int netnum, float leftborder, float rightborder, float startcondition)
		{
			NetNum = netnum;
			float length = leftborder - leftborder;
			Step = length / NetNum;
			Net = new List<float>(NetNum);
			Net[0] = leftborder;
			for (int i = 1; i < NetNum; i++)
				Net[i] = leftborder + Step * i;
		}

		public DifferentialEquation(Vertex step, Vertex leftborder, Vertex rightborder, Vertex startcondition, int dimension)
		{
			Dimension = dimension;
			IsYVertex = true;
			IsXVertex = true;
			LeftBorderV = leftborder;
			RightBorderV = rightborder;
			StartConditionV = startcondition;
			StepV = step;
			NetV = new List<Vertex>((int)((rightborder.x - leftborder.x) / step.x) + 1);
			NetNum = 0;
			Vertex tmp = leftborder, right = rightborder - new Vertex(dimension) * 1E-3;
			while (tmp.SmallerAllComponents(right))
			{
				NetV[NetNum] = tmp;
				NetNum++;//----------------------------------------------------------------------------ошибочка
				Vertex t = leftborder + StepV * NetNum;
				tmp = t;
			}
			NetV[NetNum] = rightborder;
			NetFunctionV = new List<Vertex>(++NetNum);
		}

		public DifferentialEquation(float step, float leftborder, float rightborder, float startcondition)
		{
			LeftBorder = leftborder;
			RightBorder = rightborder;
			StartCondition = startcondition;
			Step = step;
			Net = new List<float>((int)((rightborder - leftborder) / Step) + 1);
			NetNum = 0;
			float tmp = leftborder;
			while (tmp < rightborder - 1E-3)
			{
				Net[NetNum] = tmp;
				NetNum++;
				tmp = leftborder + NetNum * Step;
			}
			Net[NetNum] = rightborder;
			NetFunction = new List<float>(++NetNum);
		}

		public void EilerV()
		{
			Vertex r;
			NetFunctionV[0] = StartConditionV;//началное условие
			for (int i = 1; i < NetNum; i++)
				NetFunctionV[i] = NetFunctionV[i - 1] + Function(NetV[i - 1], NetFunctionV[i - 1]) * StepV;
		}

		/*public void PrintNetFunctionV()
		{
			printf_s("X\t\tY\t\t||Y-Y(ac)||\n");
			for (int i(0); i < NetNum; i++)
			{
				for (int j(0); j < 3; j++)
				{
					printf_s("%E\t", NetV[i][j]);
					printf_s("%E\t", NetFunctionV[i][j]);
					printf_s("%E\n", abs(NetFunctionV[i][j] - AccurateFunction(NetV[i])[j]));
				}
				printf_s("------------------------------------------------\n");
			}
			printf_s("\n");
		}*/

		public void Eiler()
		{
			NetFunction[0] = StartCondition;//началное условие
			for (int i = 1; i < NetNum; i++)
				NetFunction[i] = NetFunction[i - 1] + Step * Function(Net[i - 1], NetFunction[i - 1]);
		}

		/*public void PrintNetFunction()
		{
			printf_s("X\t\tY\t\t||Y-Y(ac)||\n");
			for (int i(0); i < NetNum; i++)
			{
				printf_s("%E\t", Net[i]);
				printf_s("%E\t", NetFunction[i]);
				printf_s("%E\n", abs(NetFunction[i] - AccurateFunction(Net[i])));
			}
			printf_s("\n");
		}*/

		public void Trapetion(float epsilon)
		{
			float eiler, corect, step = Step;
			NetFunction.Add(StartCondition);
			Net.Add(LeftBorder);
			int i = 0;
			while (Net[i] < RightBorder + epsilon)
			{
				eiler = NetFunction[i] + step * Function(Net[i], NetFunction[i]);
				corect = NetFunction[i] + step / 2 * (Function(Net[i], NetFunction[i]) + Function(Net[i] + step, eiler));
				if (Math.Abs(corect - eiler) < epsilon)
				{
					Net.Add(Net[i] + step);
					NetFunction.Add(corect);
					i++;
				}
				else
					step /= 2;
			}

			NetNum = i;
			Step = -1;
			IsUniformStep = false;
		}

		public void RungeKutta()
		{
			float k1, k2, k3;
			NetFunction[0] = StartCondition;
			for (int i = 1; i < NetNum; i++)
			{
				k1 = Step * Function(Net[i], NetFunction[i - 1]);
				k2 = Step * Function(Net[i] + Step / 2, NetFunction[i - 1] + k1 / 2);
				k3 = Step * Function(Net[i] + Step, NetFunction[i - 1] - k1 + 2 * k2);
				NetFunction[i] = NetFunction[i - 1] + (k1 + 4 * k2 + k3) / 6;
			}
		}

		private int NetNum;
		private int Dimension = 0;

		private Vertex LeftBorderV = new Vertex(), RightBorderV = new Vertex();
		private Vertex StepV = new Vertex();
		private List<Vertex> NetV = new List<Vertex>();
		private List<Vertex> NetFunctionV = new List<Vertex>();
		private Vertex StartConditionV = new Vertex();

		private float LeftBorder, RightBorder;
		private float Step;
		private List<float> Net = new List<float>();
		private List<float> NetFunction = new List<float>();
		private float StartCondition;

		private Vertex Function(Vertex x, Vertex y)//Правая чaсть 
		{
			float[] A = new float[9] { 3, -2, -1, 3, -4, -3, 2, -4, 0 };
			return y*A;
		}

		private float Function(float x, float y)//Правая чaсть 
		{
			return y / x + x;
		}

		private Vertex AccurateFunction(Vertex x)
		{
			float[] A = new float[9] { 3, -2, -1, 3, -4, -3, 2, -4, 0 };
			return x*A;
		}

		private float AccurateFunction(float x)
		{
			return x * x;
		}
	}

}
