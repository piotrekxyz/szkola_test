namespace szkola_test.Klasy
{
	abstract public class Osoba
	{
		#region wlasciwosci
		public string Imie { get; set; }
		public string Nazwisko { get; set; }
		public string Pesel { get; set; }
		#endregion

		#region konstruktory
		public Osoba(string imie, string nazwisko)
		{
			this.Imie = imie;
			this.Nazwisko = nazwisko;
		}

		public Osoba(string imie, string nazwisko, string pesel) : this(imie, nazwisko) { this.Pesel = pesel; }
		#endregion
	}
}