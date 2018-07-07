namespace szkola_test.Klasy
{
	class Uczen : Osoba
	{
		public int Cykl { get; set; }   // zastanowić się czy nie lepiej cyfrą
		public int? Klasa { get; set; }
		public Nauczyciel Nauczyciel { get; set; }
		bool Absolwent { get; set; } = false;

		public Uczen(string imie, string nazwisko, int cykl, int klasa, Instrument instrument, Nauczyciel nauczyciel, string pesel) : base(imie, nazwisko, instrument, pesel)
		{
			this.Cykl = cykl;
			this.Klasa = klasa;
			this.Nauczyciel = nauczyciel;
			this.Pesel = pesel;
		}

		void PodniesKlase()     // jakoś we właściwości?
		{
			if (Cykl == Klasa)
			{
				Absolwent = true;
				Klasa = null;
			}
			else
				Klasa++;
		}
	}
}
