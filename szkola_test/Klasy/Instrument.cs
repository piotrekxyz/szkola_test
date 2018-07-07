namespace szkola_test.Klasy
{
	class Instrument : Przedmiot
	{
		public string Sekcja { get; set; }

		public Instrument(string nazwa, string sekcja) : base(nazwa) => this.Sekcja = sekcja;
	}
}
