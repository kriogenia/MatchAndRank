using System;
using System.Windows.Forms;

namespace Sorter {
	partial class MainWindow {
		/// <summary>
		/// Variable del diseñador necesaria.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Limpiar los recursos que se estén usando.
		/// </summary>
		/// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Código generado por el Diseñador de Windows Forms

		/// <summary>
		/// Método necesario para admitir el Diseñador. No se puede modificar
		/// el contenido de este método con el editor de código.
		/// </summary>
		private void InitializeComponent() {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
			this.pnBase = new System.Windows.Forms.TableLayoutPanel();
			this.btLeft = new System.Windows.Forms.Button();
			this.btRight = new System.Windows.Forms.Button();
			this.pnBase.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnBase
			// 
			this.pnBase.ColumnCount = 2;
			this.pnBase.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.pnBase.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.pnBase.Controls.Add(this.btLeft, 0, 0);
			this.pnBase.Controls.Add(this.btRight, 1, 0);
			this.pnBase.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnBase.Location = new System.Drawing.Point(0, 0);
			this.pnBase.Name = "pnBase";
			this.pnBase.RowCount = 1;
			this.pnBase.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.pnBase.Size = new System.Drawing.Size(384, 161);
			this.pnBase.TabIndex = 0;
			// 
			// btLeft
			// 
			this.btLeft.BackColor = System.Drawing.Color.OrangeRed;
			this.btLeft.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btLeft.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btLeft.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btLeft.ForeColor = System.Drawing.Color.White;
			this.btLeft.Image = ((System.Drawing.Image)(resources.GetObject("btLeft.Image")));
			this.btLeft.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btLeft.Location = new System.Drawing.Point(3, 3);
			this.btLeft.Name = "btLeft";
			this.btLeft.Size = new System.Drawing.Size(186, 155);
			this.btLeft.TabIndex = 0;
			this.btLeft.Text = "btLeft";
			this.btLeft.UseVisualStyleBackColor = false;
			// 
			// btRight
			// 
			this.btRight.BackColor = System.Drawing.Color.Blue;
			this.btRight.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btRight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btRight.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btRight.ForeColor = System.Drawing.Color.White;
			this.btRight.Image = ((System.Drawing.Image)(resources.GetObject("btRight.Image")));
			this.btRight.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btRight.Location = new System.Drawing.Point(195, 3);
			this.btRight.Name = "btRight";
			this.btRight.Size = new System.Drawing.Size(186, 155);
			this.btRight.TabIndex = 1;
			this.btRight.Text = "btRight";
			this.btRight.UseVisualStyleBackColor = false;
			// 
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(384, 161);
			this.Controls.Add(this.pnBase);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.MinimumSize = new System.Drawing.Size(400, 200);
			this.Name = "MainWindow";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "MatchAndRank";
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Main_KeyUp);
			this.FormClosing += new FormClosingEventHandler(Main_Closing);
			this.pnBase.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel pnBase;
		private System.Windows.Forms.Button btLeft;
		private System.Windows.Forms.Button btRight;
	}
}

