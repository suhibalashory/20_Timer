using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Game_Stream
{
	public partial class UC_Timer : U_Timer
	{

		public int T_Number { get; set; }
		public string T_Name { get; set; } 
		public Color TimeColor 
		{
				get
				{
				   return this.ForeColor;
				}
				set
				{
				    this.ForeColor = value;
				}
			}
		public string Base_Time
		{
			get
			{
				return this.Minute.ToString() + ":" +  this.HOUR.ToString();
			}
		}

		public int Minute {  get; set; }
		public int HOUR { get; set; }

		private int Scond { get; set; } = 0;
		private int Min { get; set; }  
		private int Hour { get; set; } 
		public string Timer_Count 
		{
			get
			{
				return Hour.ToString() + ":" + stFormat(Min) + ":" + stFormat(Scond);
			}
		}


		private string stFormat(int s)
		{

			if (s.ToString().Length == 1)
				return "0" + s.ToString();
			else
				return s.ToString();
		}

		public UC_Timer()
		{
			InitializeComponent();
			timer1.Interval = 1000;
		}

		// Event To return T_Number And use for Delet 
		public class ReturnTagEventArgs : EventArgs
		{
			public UC_Timer timer { get; set; }
			public ReturnTagEventArgs(UC_Timer _T)
			{ 
				this.timer = _T;
			} 
			
		}

		public event EventHandler<ReturnTagEventArgs> On_CloseTimer;
		public void AfterCloseTemir(UC_Timer _Timer)
		{
			AfterCloseTemir(new ReturnTagEventArgs(_Timer));
		}
		protected virtual void AfterCloseTemir(ReturnTagEventArgs e)
		{
			On_CloseTimer?.Invoke(this, e);
		}
		//========================================================

		private void btn_Close_Click(object sender, EventArgs e)
		{
			if(On_CloseTimer != null)
				AfterCloseTemir(this);
		}

		public void SetValues()
		{
			tb_TimerName_And_Number.Text = this.T_Name + " "+ "ط" + " " + this.T_Number.ToString();
			lab_Time_To_Done.Text = this.Base_Time + "د";

			this.Min = this.Minute;
			this.Hour = this.HOUR ;
		}
		
		private void CountStart()
		{
			if(Scond > 0)
			{
				Scond--;
			}
			else
			{
				if(Min > 0)
				{
					Scond = 59; 
					Min--;
				}
				else
				{
					if(Hour > 0)
					{
						Min = 59;
						Hour--;
					}
					
				}
			}

			lab_Time_To_Done.Text = this.Base_Time + "د";
		}

		private bool Check_If_Done()
		{
			return (this.Hour == 0) && (this.Min == 0);
		}
		
		private void timer1_Tick(object sender, EventArgs e)
		{
			if(Check_If_Done())
			{
				Console.Beep();
				lab_Time.ForeColor = Color.Red;
				rjToggleButton1.Checked = false;
				timer1.Stop();
			}
			
			CountStart();
			lab_Time.Text = Timer_Count.ToString();
		}

		private void rjToggleButton1_Click(object sender, EventArgs e)
		{
			if (rjToggleButton1.Checked)
			{
				timer1.Start();
			}
			else
			{
				timer1.Stop();
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			this.lab_Time.ForeColor = Color.White;
			timer1.Stop();

			this.HOUR = 0;
			this.Minute = 0;
			this.Scond = 0;
			this.Min = 0;
			this.Hour = 0;

			rjToggleButton1.Checked = false;

			lab_Time.Text = Timer_Count.ToString();
		}

		private bool ChekInput(string S)
		{
			if (string.IsNullOrWhiteSpace(S)) return false;

			foreach(char c in S)
			{
				if (!Char.IsNumber(c)) return false;
			}
			return true;
		}

		private void اضافةToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (ChekInput(ts_tb_Hour.Text))
			{
				this.Hour += int.Parse(ts_tb_Hour.Text);
				this.HOUR += int.Parse(ts_tb_Hour.Text);
			}

			if (ChekInput(tc_cb_Min.Text))
			{
				int Min = int.Parse(tc_cb_Min.Text);

	
					this.Min += Min;
					this.Minute += Min;
				
			}
		}


		private void اضافةToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
		{
			tc_cb_Min.SelectedIndex = 0;
		}



		private void ts_tb_Hour_MouseDown(object sender, MouseEventArgs e)
		{
			if (ts_tb_Hour.Text == "ساعات")
			{
				ts_tb_Hour.Text = "";
			}
		}

		private void ts_tb_Hour_KeyDown(object sender, KeyEventArgs e)
		{
			if (ts_tb_Hour.Text == "ساعات")
			{
				ts_tb_Hour.Text = "";
			}
		}
	}
}
