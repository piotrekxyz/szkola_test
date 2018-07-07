namespace szkola_test.Klasy
{
	class Osoba
	{
		public string Imie { get; set; }
		public string Nazwisko { get; set; }
		public Instrument Instrument { get; set; }
		public string Pesel { get; set; }

		public Osoba(string imie, string nazwisko)
		{
			this.Imie = imie;
			this.Nazwisko = nazwisko;
		}

		public Osoba(string imie, string nazwisko, string pesel)
		{
			this.Imie = imie;
			this.Nazwisko = nazwisko;
			this.Pesel = pesel;
		}

		//public Osoba(string imie, string nazwisko, Instrument instrument) : base() => this.Instrument = instrument;
		public Osoba(string imie, string nazwisko, Instrument instrument) : this(imie, nazwisko) => this.Instrument = instrument;
		public Osoba(string imie, string nazwisko, Instrument instrument, string pesel) : this(imie, nazwisko) => this.Instrument = instrument;

		public Osoba(string imie, string nazwisko, string nazwa, string sekcja) : this(imie, nazwisko) => this.Instrument = new Instrument(nazwa, sekcja);
		public Osoba(string imie, string nazwisko, string nazwa, string sekcja, string pesel) : this(imie, nazwisko, pesel) => this.Instrument = new Instrument(nazwa, sekcja);

	}
}
