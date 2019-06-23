using System;
using System.Collections.Generic;

using Android.App;
using Android.Views;
using Android.Widget;

namespace SorterAndroid.Views {
	internal class EnterAdapter : ArrayAdapter<string> {

		readonly Activity context;

		public EnterAdapter(Activity context, List<string> entries) : base(context, Android.Resource.Layout.ActivityListItem, entries) {
			this.context = context;
		}

		public override View GetView(int position, View convertView, ViewGroup parent) {
			View view = context.LayoutInflater.Inflate(Android.Resource.Layout.ActivityListItem, null);
			TextView txt = view.FindViewById<TextView>(Android.Resource.Id.Text1);
			txt.Text = GetItem(position);
			txt.TextSize = 20;
			ImageView img = view.FindViewById<ImageView>(Android.Resource.Id.Icon);
			img.SetImageResource(Resource.Drawable.imgDelete);
			return view;
		}
	}
}