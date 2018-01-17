/*
 * Created by SharpDevelop.
 * User: Owner
 * Date: 3/19/2015
 * Time: 6:05 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace ProgChooser
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.mainFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.statusLabel = new System.Windows.Forms.Label();
			this.mainFlowLayoutPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// mainFlowLayoutPanel
			// 
			this.mainFlowLayoutPanel.AutoSize = true;
			this.mainFlowLayoutPanel.Controls.Add(this.statusLabel);
			this.mainFlowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.mainFlowLayoutPanel.Location = new System.Drawing.Point(0, 0);
			this.mainFlowLayoutPanel.Name = "mainFlowLayoutPanel";
			this.mainFlowLayoutPanel.Size = new System.Drawing.Size(196, 128);
			this.mainFlowLayoutPanel.TabIndex = 0;
			// 
			// statusLabel
			// 
			this.statusLabel.AutoSize = true;
			this.statusLabel.Location = new System.Drawing.Point(3, 0);
			this.statusLabel.Name = "statusLabel";
			this.statusLabel.Size = new System.Drawing.Size(0, 13);
			this.statusLabel.TabIndex = 0;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.ClientSize = new System.Drawing.Size(284, 261);
			this.Controls.Add(this.mainFlowLayoutPanel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Which editor?";
			this.Load += new System.EventHandler(this.MainFormLoad);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainFormKeyDown);
			this.mainFlowLayoutPanel.ResumeLayout(false);
			this.mainFlowLayoutPanel.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.Label statusLabel;
		private System.Windows.Forms.FlowLayoutPanel mainFlowLayoutPanel;
		
	}
}
