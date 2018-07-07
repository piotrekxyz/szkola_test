using System.Collections.Generic;

namespace szkola_test.Klasy
{
	class Nauczyciel : Osoba
	{
		public IList<Przedmiot> Przedmioty { get; set; }

		public Nauczyciel(string imie, string nazwisko, Instrument instrument) : base(imie, nazwisko, instrument) { }


		public Nauczyciel(string imie, string nazwisko, List<Przedmiot> przedmioty) : base(imie, nazwisko) => this.Przedmioty = przedmioty;


		public Nauczyciel(string imie, string nazwisko, Instrument instrument, List<Przedmiot> przedmioty) : base(imie, nazwisko, instrument) => this.Przedmioty = przedmioty;
	}
}
