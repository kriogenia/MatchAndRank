using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorter {
	class Sorter {

		// Constantes de resultados
		const int EMPTY = -1;
		const int MIRROR = -2;

		// Variables globales
		public Entry[] entries;																			// Lista de contendientes
		public int[][] previousResults;                                                                 // Matriz de resultados almacenada
		public int matches;                                                                             // Contador de enfrentamientos

		/// <summary>
		/// Crea un objeto de tipo sorter a partir de una lista de candidatos
		/// </summary>
		public Sorter(string[] entries) => Initialize(entries);

		/// <summary>
		/// Crea un objeto de tipo sorter a partir de un archivo con una lista de candidatos
		/// </summary>
		public Sorter(string file) => Initialize(File.ReadAllLines(file));

		/// <summary>
		/// Inicializa las variables del objeto
		/// </summary>
		/// <param name="entries"></param>
		private void Initialize(string[] entries) {
			int n = 0;
			this.entries = entries.Select(x => new Entry(x, n++)).ToArray();
			previousResults = new int[n].Select(x => new int[n].Select(y => EMPTY).ToArray()).ToArray();
			for (int i = 0; i < n; i++) previousResults[i][i] = MIRROR;
			matches = 1;
		}

		/// <summary>
		/// Ejecuta los enfrentamientos
		/// </summary>
		public IEnumerable<Match> Play() {
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
					// Si lo ha encontrado colocamos el nodo rival como visitado
					b.Clashed = true;
					// Si el enfrentamiento nunca se ha dado, devolvemos el enfrentamiento y procesamos su resultado
					if (previousResults[a.Position][b.Position] == EMPTY)
						yield return new Match(a, b);
					entries[previousResults[a.Position][b.Position]].Score++;
					// Si hubo enfrentamiento no es la última ronda
					lastIt = false;
				}
				// Reseteamos los contendientes a no emparejados para la siguiente ronda
				Parallel.ForEach(entries, entry => entry.Clashed = false);
			}
		}

		/// <summary>
		/// Almacena el resultado del enfrentamiento
		/// </summary>
		/// <param name="winner">Contendiente vencedor</param>
		/// <param name="loser">Contendiente perdedor</param>
		public void ProcessVote(Entry winner, Entry loser) {
			previousResults[winner.Position][loser.Position] = winner.Position;
			previousResults[loser.Position][winner.Position] = winner.Position;
		}

		/// <summary>
		/// Imprimos el resultado definitivo
		/// </summary>
		public string[] GetRanking() => entries.OrderBy(x => -x.Score).Select(e => e.Name).ToArray();

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

	/// <summary>
	/// Clase representación de los enfrentamientos para su comunicación a la interfaz
	/// </summary>
	class Match {
		public Entry Left { get; }
		public Entry Right { get; }

		/// <summary>
		/// Constructor del enfrentamiento
		/// </summary>
		/// <param name="left">Contendiente de la izquierda</param>
		/// <param name="right">Contendiente de la derecha</param>
		public Match(Entry left, Entry right) {
			Left = left;
			Right = right;
		}
	}
}
