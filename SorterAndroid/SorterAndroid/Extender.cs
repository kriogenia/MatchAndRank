using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace SorterAndroid {
	public static class Extender {

		/// <summary>
		/// Imprime mensajes de error
		/// </summary>
		/// <param name="msgId">Mensaje de error</param>
		public static void Alert(this Context context, int msgId) {
			AlertDialog.Builder dialog = new AlertDialog.Builder(context);
			dialog.SetNeutralButton(Resource.String.ok, delegate { dialog.Dispose(); });
			dialog.SetTitle(Resource.String.errortitle);
			dialog.SetMessage(msgId);
			AlertDialog alert = dialog.Create();
			alert.Show();
		}

		/// <summary>
		/// Saca un mensaje con opciones de sí y no al usuario
		/// </summary>
		/// <param name="msgId">Mensaje del diálogo</param>
		/// <returns></returns>
		public static Task<bool> Confirm(this Context context, int msgId) {
			var tcs = new TaskCompletionSource<bool>();
			AlertDialog.Builder dialog = new AlertDialog.Builder(context);
			dialog.SetTitle(Resource.String.confirmtitle);
			dialog.SetMessage(msgId);
			dialog.SetPositiveButton(Resource.String.yes, delegate { tcs.TrySetResult(true); });
			dialog.SetNegativeButton(Resource.String.no, delegate { tcs.TrySetResult(false); });
			AlertDialog alert = dialog.Create();
			alert.Show();
			return tcs.Task;
		}

		/// <summary>
		/// Saca un mensaje pidiendo al usuario la entrada por teclado de una cadena de texto
		/// </summary>
		/// <param name="msgId">Mensaje del diálogo</param>
		/// <returns></returns>
		public static Task<string> EnterText(this Context context, int msgId) {
			var tcs = new TaskCompletionSource<string>();
			EditText et = new EditText(context);
			AlertDialog.Builder ad = new AlertDialog.Builder(context);
			ad.SetTitle(Resource.String.askfortitle);
			ad.SetMessage(msgId);
			ad.SetView(et);
			ad.SetNeutralButton(Resource.String.ok, delegate { tcs.TrySetResult(et.Text + ".lst"); });
			ad.Show();
			return tcs.Task;
		}
	}
}