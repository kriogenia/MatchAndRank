using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System;
using Android.Content;
using System.Collections.Generic;
using System.Linq;
using Plugin.FilePicker;
using System.IO;
using Environment = System.Environment;

namespace SorterAndroid
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
		// Identificadores de actividades
		public const int ENTER_RESULT = 1;
		public const int LOAD_RESULT = 2;

		// Elementos clave
		Button btT, btB;
		EventHandler ehT, ehB;

		// Manejador de los enfrentamientos
		Sorter s;

		// Métodos de funcionamiento de la actividad

		protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
			// Referencia a objetos creados
			btT = FindViewById<Button>(Resource.Id.btT);
			btB = FindViewById<Button>(Resource.Id.btB);
			// Instanciamento inicial
			InstanceFirst();
		}

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults) {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data) {
			// Comprobamos que petición estamos atendiendo
			if (requestCode == ENTER_RESULT)
				if (resultCode == Result.Ok)
					InstanceFirstMatch(data);
			if (requestCode == LOAD_RESULT)
				if (resultCode == Result.Ok)
					LoadEntriesFromFile(data);
		}

		// Métodos de instanciamiento de los objetos

		/// <summary>
		/// Prepara los objetos para la visualización inicial
		/// </summary>
		private void InstanceFirst() {
			btT.Text = GetString(Resource.String.start);
			btB.Text = GetString(Resource.String.load);
			ehT = new EventHandler(StartEnterActivity);
			btT.Click += ehT;
			ehB = new EventHandler(LoadFile);
			btB.Click += ehB;
		}

		/// <summary>
		/// Prepara los botones para la primera votación
		/// </summary>
		/// <param name="data">Información de la actividad de entrada de contendientes</param>
		private void InstanceFirstMatch(Intent data) {
			var entries = data.Extras.GetStringArrayList("Entries");
			s = new Sorter(entries.ToArray());
			var next = s.GetNextMatch();
			btT.Text = next.Left.Name;
			btB.Text = next.Right.Name;
			// Intercambio de acciones de los botones
			btT.Click -= ehT;
			ehT = new EventHandler(VoteTop);
			btT.Click += ehT;
			btB.Click -= ehB;
			ehB = new EventHandler(VoteBot);
			btB.Click += ehB;
		}

		/// <summary>
		/// Prepara los botones para la siguiente votación
		/// </summary>
		/// <param name="data">Información de la actividad de entrada de contendientes</param>
		private void InstanceNextMatch() {
			var next = s.GetNextMatch();
			if (next == null)
				InstanceRanking();
			else {
				btT.Text = next.Left.Name;
				btB.Text = next.Right.Name;
			}
		}

		/// <summary>
		/// Lanza el ránking con los resultados
		/// </summary>
		private void InstanceRanking() {
			btT.Text = GetString(Resource.String.ranking);
			btB.Text = GetString(Resource.String.restart);
			// Intercambio de acciones de los botones
			btT.Click -= ehT;
			ehT = new EventHandler(ShowRanking);
			btT.Click += ehT;
			btB.Click -= ehB;
			ehB = new EventHandler(VoteBot);
			btB.Click += ehB;
		}


		// Carga de datos

		/// <summary>
		/// Carga los datos del fichero elegido y abre la ventana de edición de participantes
		/// </summary>
		/// <param name="data">Información del fichero elegido</param>
		private void LoadEntriesFromFile(Intent data) {
			// Sacamos el archivo seleccionado
			string chosenSave = data.Extras.GetString("Selected");
			var save = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)).Where(x => x.Contains(chosenSave)).First();
			// Leemos las entradas guardadas
			var entries = File.ReadAllLines(save);
			// Invocamos la ventana de introducción de entradas
			Intent intent = new Intent(this, typeof(EnterActivity));
			intent.PutStringArrayListExtra("Entries", entries);
			StartActivityForResult(intent, ENTER_RESULT);
		}


		// Métodos de evento de los objetos

		/// <summary>
		/// Abre la actividad de introducción de contendientes
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void StartEnterActivity(object sender, EventArgs e) {
			var entries = new List<string>();
			Intent intent = new Intent(this, typeof(EnterActivity));
			intent.PutStringArrayListExtra("Entries", entries);
			StartActivityForResult(intent, ENTER_RESULT);
		}

		/// <summary>
		/// Carga las listas guardadas y las envía a una ventana para que el usuario seleccione una de ellas
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LoadFile(object sender, EventArgs e) {
			var saves = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)).Where(x => x.Contains(".lst")); ;
			// Abrir ventana donde mostrar las listas guardadas para la selección
			Intent intent = new Intent(this, typeof(LoadActivity));
			intent.PutStringArrayListExtra("Saves", saves.Select(x => x.Replace(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "")).ToList());
			StartActivityForResult(intent, LOAD_RESULT);
		}

		/// <summary>
		/// Procesa el voto del contendiente superior
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void VoteTop(object sender, EventArgs e) {
			s.ProcessVote(s.GetNextMatch().Left, s.GetNextMatch().Right);
			InstanceNextMatch();
		}

		// <summary>
		/// Procesa el voto del contendiente inferior
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void VoteBot(object sender, EventArgs e) {
			s.ProcessVote(s.GetNextMatch().Right, s.GetNextMatch().Left);
			InstanceNextMatch();
		}

		/// <summary>
		/// Abre la actividad de introducción de contendientes
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ShowRanking(object sender, EventArgs e) {
			Intent intent = new Intent(this, typeof(RankingActivity));
			intent.PutStringArrayListExtra("Ranking", s.GetRanking());
			StartActivityForResult(intent, 0);
		}

	}
}