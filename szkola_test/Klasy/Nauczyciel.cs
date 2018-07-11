using System.Collections.Generic;
using System.Linq;

namespace szkola_test.Klasy
{
	class Nauczyciel : Pracownik
	{
		#region wlasciwosci
		public IList<Przedmiot> Przedmioty { get; set; }
		public Instrument Instrument { get; set; }
		#endregion
		private static string stanowisko = "nauczyciel";

		#region konstruktory
		public Nauczyciel(string imie, string nazwisko, Instrument instrument) : base(imie, nazwisko, stanowisko) { this.Instrument = instrument; }
		public Nauczyciel(string imie, string nazwisko, params string[] przedmioty) : base(imie, nazwisko, stanowisko)
		{
			this.Przedmioty = przedmioty.Select(a => new Przedmiot(a)).ToList();
		}
		public Nauczyciel(string imie, string nazwisko, Instrument instrument, params string[] przedmioty) : base(imie, nazwisko, stanowisko)
		{
			this.Instrument = instrument;
			this.Przedmioty = przedmioty.Select(a => new Przedmiot(a)).ToList();
		}
		#endregion
	}
}