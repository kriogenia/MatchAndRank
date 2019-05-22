using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Sorter {
	public partial class RankingWindow : Form {

		/// <summary>
		/// Constructor de la ventana de rankings
		/// </summary>
		/// <param name="ranking">Ranking a mostrar</param>
		public RankingWindow(string[] ranking) {
			InitializeComponent();
			BuildRanking(ranking);
		}

		/// <summary>
		/// Genera las labels que mostrarán el ránking
		/// </summary>
		/// <param name="ranking"></param>
		private void BuildRanking(string[] ranking) {
			SuspendLayout();
			int pos = 1;
			var lbs = ranking.Select(x => new RankingLabel(x, pos++));
			foreach (var lb in lbs)
				Controls.Add(lb);
			ResumeLayout(false);
			PerformLayout();
		}
	}

	/// <summary>
	/// Versión modificada de las labels adaptada para el diseño
	/// </summary>
	class RankingLabel : Label {

		public RankingLabel(string text, int n) : base() {
			Anchor = AnchorStyles.Top;
			AutoSize = true;
			Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
			ForeColor = (n%2 == 1) ? Color.Blue : Color.OrangeRed;
			Location = new Point(13, 13 + 25 * (n-1));
			Name = "lb" + text;
			Size = new Size(86, 22);
			TabIndex = 1;
			Text = n + ". " + text;
		}
	}
}
