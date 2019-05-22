using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sorter {
	public partial class MainWindow : Form {

		// Variables globales
		private Sorter s;                                                               // Gestor de los enfrentamientos
		private bool wait;																// Semáforo para controlar la entrada de votos
		private Match currentMatch;														// Enfrentamiento en ejecución
		private EventHandler leftEvent;													// Evento de carga de archivos
		private EventHandler rightEvent;												// Evento de entrada de datos manual

		/// <summary>
		/// Constructor de la clase, prepara e inicia todo el programa
		/// </summary>
		public MainWindow() {
			InitializeComponent();
			PrepareStartButtons();														// Estado de inicio
		}

		/// <summary>
		/// Genera los enfrentamientos según vayan llegando resultados, al final muestra el ránking
		/// </summary>
		public void Play() {
			ChangeToPlayEvents();														// Estado de juego
			foreach (var match in s.Play())												// Proceso de enfrentamientos
				PlayMatch(match);
			PrepareEndButtons();														// Estado final
		}

		/// <summary>
		/// Prepara la primera vista de la ventana con la opción de la carga de archivo o de introducción manual del mismo
		/// </summary>
		private void PrepareStartButtons() {
			// Botón izquierdo
			btLeft.Text = "Cargar desde fichero";
			leftEvent = new EventHandler(BtLeft_Click_Load);
			btLeft.Click += leftEvent;
			// Botón derecho
			btRight.Text = "Introducir manualmente";
			rightEvent = new EventHandler(BtRight_Click_Enter);
			btRight.Click += rightEvent;
		}

		/// <summary>
		/// Modifica el evento asociado a los botones de la aplicación
		/// </summary>
		private void ChangeToPlayEvents() {
			DisableButtons();
			// Botón izquierdo
			btLeft.Click -= leftEvent;
			leftEvent = new EventHandler(BtLeft_Click_Vote);
			btLeft.Click += leftEvent;
			// Botón derecho
			btRight.Click -= rightEvent;
			rightEvent = new EventHandler(BtRight_Click_Vote); ;
			btRight.Click += rightEvent;
		}

		/// <summary>
		/// Prepara la primera vista de la ventana con la opción de la carga de archivo o de introducción manual del mismo
		/// </summary>
		private void PrepareEndButtons() {
			DisableButtons();
			// Botón izquierdo
			btLeft.Invoke((MethodInvoker)delegate { btLeft.Text = "Mostrar ranking"; });
			btLeft.Click -= leftEvent;
			leftEvent = new EventHandler(BtLeft_Click_Ranking);
			btLeft.Click += leftEvent;
			// Botón derecho
			btRight.Invoke((MethodInvoker)delegate { btRight.Text = "Volver al inicio"; });
			btRight.Click -= rightEvent;
			rightEvent = new EventHandler(BtRight_Click_Restart); ;
			btRight.Click += rightEvent;
			EnableButtons();
			// Salida por pantalla del ránking automática
			ShowRanking();
		}

		/// <summary>
		/// Imprime los dos nuevos contendientes en pantalla y espera al resultado
		/// </summary>
		/// <param name="match">Entradas a enfrentar</param>
		private void PlayMatch(Match match) {
			// Preparación de la votación
			currentMatch = match;
			btLeft.Invoke((MethodInvoker)delegate { btLeft.Text = match.Left.Name; });
			btRight.Invoke((MethodInvoker)delegate { btRight.Text = match.Right.Name; });
			// Espera y proceso del voto
			EnableButtons();
			Wait();
			DisableButtons();
		}

		/// <summary>
		/// Genera la ventana con el ránking definitivo
		/// </summary>
		private void ShowRanking() => new RankingWindow(s.GetRanking()).ShowDialog();

		/// <summary>
		/// Reactiva los botones una vez han sido reconfigurados por completo
		/// </summary>
		private void EnableButtons() {
			btLeft.Invoke((MethodInvoker)delegate { btLeft.Enabled = true; });
			btRight.Invoke((MethodInvoker)delegate { btRight.Enabled = true; });
		}

		/// <summary>
		/// Desactiva los botones para evitar clicks problemáticos
		/// </summary>
		private void DisableButtons() {
			btLeft.Invoke((MethodInvoker)delegate { btLeft.Enabled = false; });
			btRight.Invoke((MethodInvoker)delegate { btRight.Enabled = false; });
		}

		/// <summary>
		/// Genera un bucle de espera hasta que se vote una opción
		/// </summary>
		private void Wait() {
			wait = true;
			while (wait) { }
			wait = true;
		}

		/// <summary>
		/// Lanza la señal de salida del bucle de Wait
		/// </summary>
		private void Resume() {
			wait = false;
		}

		// Eventos

		/// <summary>
		/// Gestiona el evento de click en el panel izquierdo para cargar los datos desde un archivo
		/// </summary>
		/// <param name="sender">Objeto que invoca el evento</param>
		/// <param name="e">Evento</param>
		private async void BtLeft_Click_Load(object sender, System.EventArgs e) {
			// Abre un diálogo para la elección de un fichero y obtiene su dirección
			using (OpenFileDialog openFileDialog = new OpenFileDialog()) {
				openFileDialog.InitialDirectory = "c:\\";
				openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
				openFileDialog.FilterIndex = 2;
				openFileDialog.RestoreDirectory = true;
				if (openFileDialog.ShowDialog() == DialogResult.OK) {
					try {
						s = new Sorter(openFileDialog.FileName);
							await Task.Run(() => Play());			}
					catch (Exception) {
						MessageBox.Show("Candidatos inválidos");
					}
				}
			}
		}

		/// <summary>
		/// Gestiona el evento de click en el panel derecho para cargar los datos manualmente
		/// </summary>
		/// <param name="sender">Objeto que invoca el evento</param>
		/// <param name="e">Evento</param>
		private async void BtRight_Click_Enter(object sender, System.EventArgs e) {
			// Inicio de ventana de entrada de datos, de parámetro la lista de entradas para poder inicialziar el sorter
			IList<string> entries = new List<string>();
			if (new EnterWindow(entries).ShowDialog() == DialogResult.OK) {
				s = new Sorter(entries.ToArray());
				await Task.Run(() => Play());
			}
		}

		/// <summary>
		/// Recibe el voto en el botón izquierdo y lo procesa
		/// </summary>
		/// <param name="sender">Objeto que invoca el evento</param>
		/// <param name="e">Evento</param>
		private void BtLeft_Click_Vote(object sender, System.EventArgs e) {
			s.ProcessVote(currentMatch.Left, currentMatch.Right);
			Resume();
		}

		/// <summary>
		/// Recibe el voto en el botón derecho y lo procesa
		/// </summary>
		/// <param name="sender">Objeto que invoca el evento</param>
		/// <param name="e">Evento</param>
		private void BtRight_Click_Vote(object sender, System.EventArgs e) {
			s.ProcessVote(currentMatch.Right, currentMatch.Left);
			Resume();
		}

		/// <summary>
		/// Gestiona el evento de mostrar el ránking al final si es pedido de nuevo
		/// </summary>
		/// <param name="sender">Objeto que invoca el evento</param>
		/// <param name="e">Evento</param>
		private void BtLeft_Click_Ranking(object sender, System.EventArgs e) => ShowRanking();

		/// <summary>
		/// Gestiona el evento de volver al inicio del programa
		/// </summary>
		/// <param name="sender">Objeto que invoca el evento</param>
		/// <param name="e">Evento</param>
		private void BtRight_Click_Restart(object sender, System.EventArgs e) {
			if (MessageBox.Show("¿Estás seguro?", "Reiniciar", MessageBoxButtons.YesNo) == DialogResult.Yes) {
				btLeft.Click -= leftEvent;
				btRight.Click -= rightEvent;
				PrepareStartButtons();
			}
		}

		/// <summary>
		/// Recoge la pulsación de las flechas y activa los botones consecuentes
		/// </summary>
		/// <param name="sender">Objeto que recoge el evento</param>
		/// <param name="e">Evento</param>
		private void Main_KeyUp(object sender, KeyEventArgs e) {
			switch (e.KeyCode) {
				case Keys.Left:
					btLeft.PerformClick();
					break;
				case Keys.Right:
					btRight.PerformClick();
					break;
				default:
					break;
			}
		}

		/// <summary>
		/// Levanta una advertencia para prevenir el cierre
		/// </summary>
		/// <param name="sender">Objeto que recoge el evento</param>
		/// <param name="e">Evento</param>
		private void Main_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			if (MessageBox.Show("¿Estás seguro de que desear salir", "Cerrar", MessageBoxButtons.YesNo) == DialogResult.No)
				e.Cancel = true;
		}
	}

}
