using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Sorter {
	public partial class EnterWindow : Form {

		readonly IList<string> entries;											// Dirección de la lista de candidatos a rellenar

		/// <summary>
		/// Constructor de la ventana de introducción de candidatos
		/// </summary>
		/// <param name="entries"></param>
		public EnterWindow(IList<string> entries) {
			this.entries = entries;
			InitializeComponent();
			ActiveControl = txtEnter;
		}

		/// <summary>
		/// Añade el candidato introducido si cumple las características
		/// </summary>
		private void Add() {
			if (!string.IsNullOrWhiteSpace(txtEnter.Text))
				if (entries.Contains(txtEnter.Text))
					MessageBox.Show("Candidato ya añadido");
				else {
					AddEntry(txtEnter.Text);
					txtEnter.Text = "";
					ActiveControl = txtEnter;
				}
		}

		/// <summary>
		/// Añade los candidatos a la lista y a la ventana
		/// </summary>
		/// <param name="entry">Candidato a añadir</param>
		private void AddEntry(string entry) {
			entries.Add(entry);
			pnCenter.Controls.Add(new RankingLabel(entry, entries.Count));
		}

		// Eventos

		/// <summary>
		/// Gestiona el evento de uso del botón de Añadir
		/// </summary>
		/// <param name="sender">Objeto generador del evento</param>
		/// <param name="e">Evento</param>
		private void BtAdd_Click(object sender, EventArgs e) => Add();

		/// <summary>
		/// Gestiona el evento de click en el botón de cancelar
		/// </summary>
		/// <param name="sender">Objeto generador del evento</param>
		/// <param name="e">Evento</param>
		private void BtCancel_Click(object sender, EventArgs e) => DialogResult = DialogResult.No;

		/// <summary>
		/// Gestiona el evento de click en el botón de Ok
		/// </summary>
		/// <param name="sender">Objeto generador del evento</param>
		/// <param name="e">Evento</param>
		private void BtOk_Click(object sender, EventArgs e) {
			if (entries.Count < 2)
				MessageBox.Show("Debes añadir mínimo dos candidatos para la ejecución");
			else {
				DialogResult = DialogResult.OK;
				this.Close();
			}
		}

		/// <summary>
		/// Gestiona el evento de uso del Enter en la text box para añadir el elemento
		/// </summary>
		/// <param name="sender">Objeto generador del evento</param>
		/// <param name="e">Evento</param>
		private void TxtEnter_KeyDown(object sender, KeyEventArgs e) {
			if (e.KeyCode == Keys.Enter) {
				Add();
				e.SuppressKeyPress = true;
			}
		}
	}
}
