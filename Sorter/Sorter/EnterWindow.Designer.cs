using System.Windows.Forms;

namespace Sorter {
	partial class EnterWindow {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EnterWindow));
			this.pnSouth = new System.Windows.Forms.FlowLayoutPanel();
			this.btOk = new System.Windows.Forms.Button();
			this.btCancel = new System.Windows.Forms.Button();
			this.pnNorth = new System.Windows.Forms.FlowLayoutPanel();
			this.txtEnter = new System.Windows.Forms.TextBox();
			this.btAdd = new System.Windows.Forms.Button();
			this.pnCenter = new System.Windows.Forms.Panel();
			this.pnSouth.SuspendLayout();
			this.pnNorth.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnSouth
			// 
			this.pnSouth.AutoSize = true;
			this.pnSouth.Controls.Add(this.btOk);
			this.pnSouth.Controls.Add(this.btCancel);
			this.pnSouth.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnSouth.Location = new System.Drawing.Point(0, 325);
			this.pnSouth.Name = "pnSouth";
			this.pnSouth.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.pnSouth.Size = new System.Drawing.Size(384, 36);
			this.pnSouth.TabIndex = 0;
			// 
			// btOk
			// 
			this.btOk.BackColor = System.Drawing.Color.Blue;
			this.btOk.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btOk.ForeColor = System.Drawing.Color.White;
			this.btOk.Location = new System.Drawing.Point(306, 3);
			this.btOk.Name = "btOk";
			this.btOk.Size = new System.Drawing.Size(75, 30);
			this.btOk.TabIndex = 0;
			this.btOk.Text = "OK";
			this.btOk.UseVisualStyleBackColor = false;
			this.btOk.Click += new System.EventHandler(this.BtOk_Click);
			// 
			// btCancel
			// 
			this.btCancel.BackColor = System.Drawing.Color.OrangeRed;
			this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btCancel.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btCancel.ForeColor = System.Drawing.Color.White;
			this.btCancel.Location = new System.Drawing.Point(225, 3);
			this.btCancel.Name = "btCancel";
			this.btCancel.Size = new System.Drawing.Size(75, 30);
			this.btCancel.TabIndex = 1;
			this.btCancel.Text = "Cancelar";
			this.btCancel.UseVisualStyleBackColor = false;
			this.btCancel.Click += new System.EventHandler(this.BtCancel_Click);
			// 
			// pnNorth
			// 
			this.pnNorth.AutoSize = true;
			this.pnNorth.Controls.Add(this.txtEnter);
			this.pnNorth.Controls.Add(this.btAdd);
			this.pnNorth.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnNorth.Location = new System.Drawing.Point(0, 0);
			this.pnNorth.Name = "pnNorth";
			this.pnNorth.Size = new System.Drawing.Size(384, 36);
			this.pnNorth.TabIndex = 1;
			// 
			// txtEnter
			// 
			this.txtEnter.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.txtEnter.Location = new System.Drawing.Point(3, 8);
			this.txtEnter.Name = "txtEnter";
			this.txtEnter.Size = new System.Drawing.Size(250, 20);
			this.txtEnter.TabIndex = 0;
			this.txtEnter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtEnter_KeyDown);
			// 
			// btAdd
			// 
			this.btAdd.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btAdd.Location = new System.Drawing.Point(259, 3);
			this.btAdd.Name = "btAdd";
			this.btAdd.Size = new System.Drawing.Size(75, 30);
			this.btAdd.TabIndex = 1;
			this.btAdd.Text = "Añadir";
			this.btAdd.UseVisualStyleBackColor = true;
			this.btAdd.Click += new System.EventHandler(this.BtAdd_Click);
			// 
			// pnCenter
			// 
			this.pnCenter.AutoScroll = true;
			this.pnCenter.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnCenter.Location = new System.Drawing.Point(0, 36);
			this.pnCenter.Name = "pnCenter";
			this.pnCenter.Size = new System.Drawing.Size(384, 289);
			this.pnCenter.TabIndex = 2;
			// 
			// EnterWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.CancelButton = this.btCancel;
			this.ClientSize = new System.Drawing.Size(384, 361);
			this.Controls.Add(this.pnCenter);
			this.Controls.Add(this.pnNorth);
			this.Controls.Add(this.pnSouth);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(400, 200);
			this.Name = "EnterWindow";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "MatchAndRank: Participantes";
			this.pnSouth.ResumeLayout(false);
			this.pnNorth.ResumeLayout(false);
			this.pnNorth.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.FlowLayoutPanel pnSouth;
		private System.Windows.Forms.Button btOk;
		private System.Windows.Forms.Button btCancel;
		private System.Windows.Forms.FlowLayoutPanel pnNorth;
		private System.Windows.Forms.TextBox txtEnter;
		private System.Windows.Forms.Button btAdd;
		private System.Windows.Forms.Panel pnCenter;
	}
}