using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Environment = System.Environment;

namespace SorterAndroid {
	[Activity(Label = "RankingActivity")]
	public class RankingActivity : Activity {

		IEnumerable<string> ranking;

		private ImageButton btnBack,btnSave;
		private ListView list;

		protected override void OnCreate(Bundle savedInstanceState) {
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.activity_ranking);
			int pos = 1;
			ranking = Intent.Extras.GetStringArrayList("Ranking").Select(x => pos++ + ". " + x);
			btnBack = FindViewById<ImageButton>(Resource.Id.rnk_btnBack);
			btnSave = FindViewById<ImageButton>(Resource.Id.rnk_btSave);
			btnBack.Click += new EventHandler((s, e) => Close(Result.Canceled));
			btnSave.Click += new EventHandler(async (s, e) => await SaveAsync());
			list = FindViewById<ListView>(Resource.Id.rnk_list);
			list.Adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, ranking.ToList());
		}

		private async Task SaveAsync() {
			// Introducción de nombre de salida
			string outputName = await this.EnterText(Resource.String.enteranamemsg);
			if (outputName.Equals(".rnk"))
				this.Alert(Resource.String.notenoughlonginput);
			else {
				string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), outputName);
				// Comprobación de nombre no usado
				if (File.Exists(fileName))
					if (!await this.Confirm(Resource.String.overridemsg))
						return;
				// Guardado del archivo
				File.WriteAllLines(fileName, ranking);
			}
		}

		private void Close(Result res) {
			Intent returnIntent = new Intent();
			SetResult(res, returnIntent);
			Finish();
		}
	}
}