/*
 * Created by SharpDevelop.
 * User: Jake Gustafson
 * Date: 3/19/2015
 * Time: 6:05 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;

namespace ProgChooser
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		//public static ArrayList progButtons=new ArrayList();
		public static ArrayList progLabels=new ArrayList();
		public static string[] args=null;
		public static string participle="(before initializing)";
		public MainForm(string[] set_args)
		{
			args=set_args;
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
		}
		void runProgram(int index) {
			string Name="";
			foreach (Label thisLabel in progLabels) {
				if (index.ToString()==(string)thisLabel.Tag) {
					Name=thisLabel.Text;
				}
			}
			statusLabel.Text="Loading "+Name+"...";
//			string result=ProgChooser.launchProgram(index);
//			if (result=null) Application.Exit();
//			else statusLabel.Text=result;
			bool result=ProgChooser.launchProgram(index);
			if (result) Application.Exit();
			else statusLabel.Text="Could not launch program: "+ProgChooser.getError();
		}
		
		void MainFormLoad(object sender, EventArgs e)
		{
			participle="during MainFormLoad";
			try {
				mainFlowLayoutPanel.Location = new Point(0,0);
				participle="loading args";
				string result=ProgChooser.load(args);
				participle="getting choices list";
				if (result!=null) MessageBox.Show(result);
				ArrayList thisAL=ProgChooser.showChoicesToArrayList();
				int index=0;
				
				if (thisAL!=null) {
					participle="showing choices";
					foreach (string thisString in thisAL) { //for (int index=0; index<4; index++) {
						participle="processing choice:"+((thisString!=null)?("\""+thisString+"\""):"null");
		//				Button newButton=new Button();
		//				this.mainFlowLayoutPanel.Controls.Add(newButton);
		//				newButton.Location = new System.Drawing.Point(3, 3);
		//				newButton.Name = "button"+progButtons.Count.ToString();
		//				newButton.Size = new System.Drawing.Size(75, 23);
		//				newButton.TabIndex = 0;
		//				newButton.Text = "button"+progButtons.Count.ToString();
		//				newButton.UseVisualStyleBackColor = true;
		//				newButton.Click += new System.EventHandler(this.AnyProgramButtonClick);
		//				progButtons.Add(newButton);
						Label newLabel = new System.Windows.Forms.Label();
						this.mainFlowLayoutPanel.Controls.Add(newLabel);
						newLabel.Location = new System.Drawing.Point(3, 0);
						newLabel.Name = "programLabel"+index.ToString();
						newLabel.Tag=index.ToString();
						newLabel.Size = new System.Drawing.Size(100, 23);
						newLabel.TabIndex = 0;
						newLabel.Text=thisString; //"item "+index.ToString();
						newLabel.AutoSize=true;
						newLabel.Click += new System.EventHandler(this.AnyProgramLabelClick);
						progLabels.Add(newLabel);
						index++;
					}
				}
				else {
					participle="showing program options since no args";
					string thisString = "There was no file sent to ProgChooser. The purpose of the program is to send files to a program you choose using one keystroke.";
					if (args.Length>1) {
						string extAndQuotesIfPresent_string = "no";
						string temp_extAndQuotesIfPresent_string = ProgChooser.getExtensionString();
						if (!string.IsNullOrEmpty(temp_extAndQuotesIfPresent_string)) {
							extAndQuotesIfPresent_string = "\""+temp_extAndQuotesIfPresent_string+"\"";
						}
						thisString = "ProgChooser has not been setup for files with "+extAndQuotesIfPresent_string+" extension.";
					}
					else {
						this.Text="ProgChooser";
					}
					Label newLabel = new System.Windows.Forms.Label();
					this.mainFlowLayoutPanel.Controls.Add(newLabel);
					newLabel.Location = new System.Drawing.Point(3, 0);
					newLabel.Name = "programLabel"+index.ToString();
					newLabel.Tag=index.ToString();
					newLabel.Size = new System.Drawing.Size(100, 23);
					newLabel.TabIndex = 0;
					newLabel.Text=thisString; //"item "+index.ToString();
					newLabel.AutoSize=true;
					//progLabels.Add(newLabel);
					Console.Error.WriteLine("Got null list of programs");
				}
			}
			catch (Exception exn) {
				Console.Error.WriteLine(("Could not finish "+participle+" ").TrimEnd()+" in MainFormLoad for the following reason: "+exn.ToString());
			}
		}
		void AnyProgramButtonClick(object sender, System.EventArgs e)
		{
			Button thisControl = (Button)sender;
			statusLabel.Text=thisControl.Text;
			runProgram(int.Parse((string)thisControl.Tag));
		}
		void AnyProgramLabelClick(object sender, System.EventArgs e)
		{
			Label thisControl = (Label)sender;
			statusLabel.Text=thisControl.Text;
			runProgram(int.Parse((string)thisControl.Tag));
		}
		void AnyMenuItemClick(object sender, EventArgs e)
		{
			ToolStripMenuItem thisTSMI= (ToolStripMenuItem)sender;
			statusLabel.Text=thisTSMI.Text;
		}
		
		void MenuStripItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			
		}
		
		void FirstToolStripMenuItemClick(object sender, EventArgs e)
		{
			statusLabel.Text="item 1";
		}
		void MainFormKeyDown(object sender, KeyEventArgs e)
		{
			int oemOffset=172;
			if (e.KeyCode==Keys.F1 || e.KeyValue-oemOffset==1 || e.KeyCode==Keys.D1) {
				runProgram(1);
			}
			else if (e.KeyCode==Keys.F2 || e.KeyValue-oemOffset==2 || e.KeyCode==Keys.D2) {
				runProgram(2);
			}
			else if (e.KeyCode==Keys.F3 || e.KeyValue-oemOffset==3 || e.KeyCode==Keys.D3) {
				runProgram(3);
			}
			else if (e.KeyCode==Keys.F4 || e.KeyValue-oemOffset==4 || e.KeyCode==Keys.D4) {
				runProgram(4);
			}
			else if (e.KeyCode==Keys.F5 || e.KeyValue-oemOffset==5 || e.KeyCode==Keys.D5) {
				runProgram(5);
			}
			else if (e.KeyCode==Keys.F6 || e.KeyValue-oemOffset==6 || e.KeyCode==Keys.D6) {
				runProgram(6);
			}
			else if (e.KeyCode==Keys.F7 || e.KeyValue-oemOffset==7 || e.KeyCode==Keys.D7) {
				runProgram(7);
			}
			else if (e.KeyCode==Keys.F8 || e.KeyValue-oemOffset==8 || e.KeyCode==Keys.D8) {
				runProgram(8);
			}
			else if (e.KeyCode==Keys.F9 || e.KeyValue-oemOffset==9 || e.KeyCode==Keys.D9) {
				runProgram(9);
			}
			else if (e.KeyCode==Keys.F10 || e.KeyValue-oemOffset==10 || e.KeyCode==Keys.D0) {
				runProgram(10);
			}
			else {
				//statusLabel.Text="Unused key "+e.KeyCode.ToString();
			}
		}
	}
}
