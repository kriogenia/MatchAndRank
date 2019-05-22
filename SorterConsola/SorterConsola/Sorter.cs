using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Sorter {
	class Sorter {

		const int EMPTY = -1;
		const int MIRROR = -2;

		public static readonly char[] OPTIONS = { 'a', 'A', 'b', 'B' };

		public Entry[] entries;																			// Lista de contendientes
		public int[][] previousResults;                                                                 // Matriz de resultados almacenada
		public int matches;																				// Contador de enfrentamientos

		/// <summary>
		/// Crea un objeto de tipo sorter inicializando sus variables
		/// </summary>
		public Sorter(string file) {
			int n = 0;
			entries = File.ReadAllLines(file).Select(x => new Entry(x, n++)).ToArray();
			previousResults = new int[n].Select(x => new int[n].Select(y => EMPTY).ToArray()).ToArray();
			for (int i = 0; i < n; i++) previousResults[i][i] = MIRROR;
			matches = 1;
		}

		/// <summary>
		/// Ejecuta los enfrentamientos
		/// </summary>
		public void Play() {
			bool lastIt = false;
			// Ejecutamos los enfrentamientos para obtener los resultados
			while (!lastIt) {
				lastIt = true;
				foreach (Entry a in entries) {
					// Si a ya ha sido emparejado en esta ronda se lo salta
					if (a.Clashed)
						continue;
					a.Clashed = true;
					Entry b = FindOpponent(a);
					// Si no le ha encontrado un oponente válido se lo salta
					if (b == null)
						continue;
					// Si lo ha encontrado procesa el enfrentamiento
					lastIt = false;
					Match(a, b);
				}
				// Reseteamos los contendientes a no emparejados para la siguiente ronda
				Parallel.ForEach(entries, entry => entry.Clashed = false);
			}
		}

		/// <summary>
		/// Imprimos el resultado definitivo
		/// </summary>
		public void Show() {
			int pos = 1;
			Console.WriteLine(entries.OrderBy(x => -x.Score).Aggregate("", (s, x) => s += pos++ + " - " + x.Name + System.Environment.NewLine));
			Console.Read();
		}

		/// <summary>
		/// Busca un oponente para el contendiente pedido, a ser posible no enfrentado antes, de no haber ninguno posible devuelve EMPTY
		/// </summary>
		/// <param name="a">Contendiente para el que buscar rival</param>
		/// <returns></returns>
		private Entry FindOpponent(Entry a) {
			// Filtra los candidatos con la misma puntuación que no hayan sido enfrentados en esta ronda aún
			var candidates = entries.AsParallel().Where(b => !b.Clashed).Where(b => b.Score == a.Score);
			// Si la lista está vacía no hay candidato válido
			if (!candidates.Any())
				return null;
			// Intenta obtener un rival al que no se haya enfrentado
			var optimum = candidates.AsParallel().Where(b => previousResults[a.Position][b.Position] == EMPTY);
			if (optimum.Any())
				return optimum.First();
			// Si solo hay candidatos ya enfrentados devuelve uno aleatorio
			return candidates.ElementAt(new Random().Next(candidates.Count()));
		}
		
		/// <summary>
		/// Enfrenta dos contendientes, sea por primera vez o repitiendo el enfrentamiento
		/// </summary>
		/// <param name="a">Primer contendiente</param>
		/// <param name="b">Segundo contendiente</param>
		private void Match(Entry a, Entry b) {
			// Colocamos el nodo rival como visitado
			b.Clashed = true;
			// Si los dos contendientes no se han enfrentado realiza el enfrentamiento
			if (previousResults[a.Position][b.Position] == EMPTY)
				NewMatch(a, b);
			if (previousResults[a.Position][b.Position] == a.Position)
				ChangeScore(a, b);
			else
				ChangeScore(b, a);
		}

		/// <summary>
		/// Enfrenta dos contendientes por primera vez
		/// </summary>
		/// <param name="a">Primer contendiente</param>
		/// <param name="b">Segundo contendiente</param>
		private void NewMatch(Entry a, Entry b) {
			// Imprime el encuentro
			Console.WriteLine("Enfrentamiento {0}", matches++);
			Console.WriteLine("[A] {0} v. [B] {1}", a.Name,b.Name);
			char input;
			while (!IsValidOption(out input))
				Console.WriteLine("Introduce una opción válida");
			// Computa el resultado
			ProcessResult(input, a, b);
			Console.WriteLine("--------------------------------");
		}

		/// <summary>
		/// Lee una línea por consola y comprueba que sea una opción de voto válida
		/// </summary>
		/// <param name="input">Caracter leído</param>
		/// <returns></returns>
		private bool IsValidOption(out char input) {
			string read = Console.ReadLine();
			try {
				input = read.ElementAt(0);
				return OPTIONS.Where(o => o == read.ElementAt(0)).Any();	}
			catch (Exception) {
				input = ' ';
				return false;
			}
		}

		/// <summary>
		/// Selecciona el ganador y almacena el resultado
		/// </summary>
		/// <param name="input">Selector del vencedor</param>
		/// <param name="a">Contendiente</param>
		/// <param name="b">Contendiente</param>
		private void ProcessResult(char input, Entry a, Entry b) {
			switch (input) {
				case 'a':
				case 'A':
					SaveResult(a, b);
					break;
				case 'b':
				case 'B':
					SaveResult(b, a);
					break;
			}
			
		}

		/// <summary>
		/// Almacena el resultado del enfrentamiento
		/// </summary>
		/// <param name="winner">Contendiente vencedor</param>
		/// <param name="loser">Contendiente perdedor</param>
		private void SaveResult(Entry winner, Entry loser) {
			previousResults[winner.Position][loser.Position] = winner.Position;
			previousResults[loser.Position][winner.Position] = winner.Position;
		}

		/// <summary>
		/// Modifica las puntuaciones de los dos contendientes
		/// </summary>
		/// <param name="winner">Contendiente vencedor</param>
		/// <param name="loser">Contendiente perdedor</param>
		private void ChangeScore(Entry winner, Entry loser) {
			winner.Score++;
			loser.Score--;
		}
	}

	/// <summary>
	/// Clase representativa de los contendientes
	/// </summary>
	class Entry {
		public String Name { get; }                                     // Nombre del contendiente
		public int Position { get; }									// Posición del contendiente
		public int Score { get; set; }                                  // Puntuación del contendiente
		public bool Clashed { get; set; }								// Boolean que guarda si el contendiente ha sido ya visitado en esta iteración o no

		/// <summary>
		/// Constructor del contendiente
		/// </summary>
		/// <param name="name">Nombre del contendiente</param>
		public Entry(string name, int pos) {
			Name = name;
			Position = pos;
			Score = 0;
			Clashed = false;
		}
	}
}
