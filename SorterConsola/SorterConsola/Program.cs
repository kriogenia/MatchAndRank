using System;

namespace Sorter {
	class Program {

		const string FILE = "Entrada.txt";                                                              // Nombre del fichero con los contendientes por defecto

		static void Main(string[] args) {
			try {
				Sorter sorter = (args.Length < 1) ? new Sorter(FILE) : new Sorter(args[0]);
				sorter.Play();
				sorter.Show();
			}
			catch (Exception e) {
				Console.WriteLine(@"Fichero de entrada no encontrado. \n" +  e.Message);
			}
		}
	}
}
