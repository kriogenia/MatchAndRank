using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace SorterAndroid {

	[Activity(Label = "LoadActivity")]
	public class LoadActivity : ListActivity {

		private IList<string> saves;

		protected override void OnCreate(Bundle bundle) {
			base.OnCreate(bundle);
			saves = Intent.Extras.GetStringArrayList("Saves");
			ListView.ChoiceMode = ChoiceMode.Single;
			ListAdapter = new LoadAdapter(this, saves.Select(x => x.Substring(1, x.Length - 5)).ToList());
		}

		protected override void OnListItemClick(ListView l, View v, int position, long id) {
			Intent returnIntent = new Intent();
			returnIntent.PutExtra("Selected", saves[position]);
			SetResult(Result.Ok, returnIntent);
			Finish();
		}
	}

	internal class LoadAdapter : BaseAdapter<string> {

		readonly Activity context;
		readonly List<string> saves;

		public LoadAdapter(Activity context, List<string> saves) : base() {
			this.context = context;
			this.saves = saves;
		}

		public override long GetItemId(int position) => position;
		public override string this[int position] => saves[position];
		public override int Count => saves.Count;

		public override View GetView(int position, View convertView, ViewGroup parent) {
			var save = saves[position];
			View view = convertView;
			if (view == null)
				view = context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, null);
			view.FindViewById<TextView>(Android.Resource.Id.Text1).Text = save;
			return view;
		}
	}
}
