using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Database;
using Android.OS;
using Android.Views;
using Android.Widget;
using SorterAndroid.Views;
using static Android.Widget.AdapterView;
using Environment = System.Environment;

namespace SorterAndroid {
	[Activity(Label = "@string/enter_name")]
	public class EnterActivity : Activity {

		private IList<string> entries;

		private EditText txtEnter;
		private Button btnAdd;
		private ImageButton btnBack, btnAccept, btnSave;
		private ListView list;
		private EnterAdapter adapter;

		protected override void OnCreate(Bundle savedInstanceState) {
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.activity_enter);
			entries = Intent.Extras.GetStringArrayList("Entries");
			// Identificación de elementos
			txtEnter = FindViewById<EditText>(Resource.Id.enter_text);
			btnAdd = FindViewById<Button>(Resource.Id.enter_add);
			btnBack = FindViewById<ImageButton>(Resource.Id.enter_btnBack);
			btnAccept = FindViewById<ImageButton>(Resource.Id.enter_btAccept);
			btnSave = FindViewById<ImageButton>(Resource.Id.enter_btSave);
			list = FindViewById<ListView>(Resource.Id.enter_list);
			// Preparación de elementos
			DisableAccept();
			DisableSave();
			// Activación de botones
			if (entries.Count >= 1)
				EnableSave();
			if (entries.Count >= 2)
				EnableAccept();
			adapter = new EnterAdapter(this, entries.ToList());
			list.Adapter = adapter;
			// Gestión de eventos
			btnAdd.Click += new EventHandler((s,e) => Add());
			btnBack.Click += new EventHandler(async (s,e) => await BackAsync());
			btnAccept.Click += new EventHandler((s,e) => Accept());
			btnSave.Click += new EventHandler(async (s,e) => await SaveAsync());
			list.ItemClick += (sender, e) => RemoveEntry(adapter.GetItem(e.Position));
}

		/// <summary>
		/// Añade el candidato introducido si cumple las características
		/// </summary>
		private void Add() {
			if (!string.IsNullOrWhiteSpace(txtEnter.Text))
				if (txtEnter.Text.Length > 25) {
					this.Alert(Resource.String.toolonginput);
					txtEnter.Text = txtEnter.Text.Substring(0, 25);
				}
				else if (entries.Contains(txtEnter.Text)) {
					this.Alert(Resource.String.invalidinput);
					txtEnter.SelectAll();
				}
				else
					AddEntry();
		}


		private void AddEntry() {
			entries.Add(txtEnter.Text);
			adapter.Add(txtEnter.Text);
			txtEnter.Text = "";
			// Activación de botones
			if (entries.Count == 1)
				EnableSave();
			if (entries.Count == 2)
				EnableAccept();
		}

		private void RemoveEntry(string entry) {
			adapter.Remove(entry);
			entries.Remove(entry);
			if (entries.Count == 0)
				DisableSave();
			if (entries.Count == 1)
				DisableAccept();
		}

		private async Task BackAsync() {
			if (entries.Any())
				if (!await this.Confirm(Resource.String.lostprogressmsg))
					return;
			Close(Result.Canceled);
		}


		private void Accept() {
			Close(Result.Ok);
		}

		private async Task SaveAsync() {
			// Introducción de nombre de salida
			string outputName = await this.EnterText(Resource.String.enteranamemsg);
			if (outputName.Equals(".lst"))
				this.Alert(Resource.String.notenoughlonginput);
			else {
				string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), outputName);
				// Comprobación de nombre no usado
				if (File.Exists(fileName))
					if (!await this.Confirm(Resource.String.overridemsg))
						return;
				// Guardado del archivo
				File.WriteAllLines(fileName, entries);
			}
		}

		private void Close(Result res) {
			Intent returnIntent = new Intent();
			returnIntent.PutStringArrayListExtra("Entries", entries);
			SetResult(res, returnIntent);
			Finish();
		}

		// Activación y desactivación de botones

		private void EnableAccept() {
			btnAccept.Enabled = true;
			btnAccept.SetImageResource(Resource.Drawable.imgAccept);
		}

		private void DisableAccept() {
			btnAccept.Enabled = false;
			btnAccept.SetImageResource(Resource.Drawable.imgAcceptDisabled);
		}

		private void EnableSave() {
			btnSave.Enabled = true;
			btnSave.SetImageResource(Resource.Drawable.imgSave);
		}

		private void DisableSave() {
			btnSave.Enabled = false;
			btnSave.SetImageResource(Resource.Drawable.imgSaveDisabled);
		}
	}

}