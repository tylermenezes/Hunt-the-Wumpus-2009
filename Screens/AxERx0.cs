using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HuntTheWumpus.Screens {
	class AxERx0 : Form {
		private Label loadScreenLabel;
		public Timer timeShowLoadScreen;
		private System.ComponentModel.IContainer components;
		private Label byline;
		private Timer changeLoadMessage;
		private PictureBox axerLogo;

		public AxERx0() {
			this.InitializeComponent();
		}

		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			this.axerLogo = new System.Windows.Forms.PictureBox();
			this.loadScreenLabel = new System.Windows.Forms.Label();
			this.timeShowLoadScreen = new System.Windows.Forms.Timer(this.components);
			this.byline = new System.Windows.Forms.Label();
			this.changeLoadMessage = new System.Windows.Forms.Timer(this.components);
			((System.ComponentModel.ISupportInitialize)(this.axerLogo)).BeginInit();
			this.SuspendLayout();
			// 
			// axerLogo
			// 
			this.axerLogo.BackColor = System.Drawing.Color.Transparent;
			this.axerLogo.Image = global::HuntTheWumpus.Properties.Resources.axerxo_Logo_White;
			this.axerLogo.Location = new System.Drawing.Point(76, 69);
			this.axerLogo.Name = "axerLogo";
			this.axerLogo.Size = new System.Drawing.Size(450, 139);
			this.axerLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.axerLogo.TabIndex = 0;
			this.axerLogo.TabStop = false;
			// 
			// loadScreenLabel
			// 
			this.loadScreenLabel.BackColor = System.Drawing.Color.Transparent;
			this.loadScreenLabel.ForeColor = System.Drawing.Color.White;
			this.loadScreenLabel.Location = new System.Drawing.Point(12, 278);
			this.loadScreenLabel.Name = "loadScreenLabel";
			this.loadScreenLabel.Size = new System.Drawing.Size(576, 13);
			this.loadScreenLabel.TabIndex = 1;
			this.loadScreenLabel.Text = Random.loadingString + "...";
			this.loadScreenLabel.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			// 
			// timeShowLoadScreen
			// 
			this.timeShowLoadScreen.Enabled = true;
			this.timeShowLoadScreen.Interval = 5000;
			this.timeShowLoadScreen.Tick += new System.EventHandler(this.timeShowLoadScreen_Tick);
			// 
			// byline
			// 
			this.byline.AutoSize = true;
			this.byline.BackColor = System.Drawing.Color.Transparent;
			this.byline.ForeColor = System.Drawing.Color.White;
			this.byline.Location = new System.Drawing.Point(73, 53);
			this.byline.Name = "byline";
			this.byline.Size = new System.Drawing.Size(96, 13);
			this.byline.TabIndex = 2;
			this.byline.Text = "1337 h4xx0ring by:";
			// 
			// changeLoadMessage
			// 
			this.changeLoadMessage.Enabled = true;
			this.changeLoadMessage.Interval = 1000;
			this.changeLoadMessage.Tick += new System.EventHandler(this.changeLoadMessage_Tick);
			// 
			// AxERx0
			// 
			this.BackColor = System.Drawing.Color.Black;
			this.BackgroundImage = global::HuntTheWumpus.Properties.Resources.dotted_bg1;
			this.ClientSize = new System.Drawing.Size(600, 300);
			this.Controls.Add(this.byline);
			this.Controls.Add(this.loadScreenLabel);
			this.Controls.Add(this.axerLogo);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "AxERx0";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			((System.ComponentModel.ISupportInitialize)(this.axerLogo)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		private void timeShowLoadScreen_Tick( object sender, EventArgs e ) {
			this.Close();
		}

		private void changeLoadMessage_Tick( object sender, EventArgs e ) {
			loadScreenLabel.Text = Random.loadingString + "...";
		}
	}
}
