namespace szkola_test.Klasy
{
	class Uczen : Osoba
	{
		#region wlasciwosi
		public int Cykl { get; set; }
		public int? Klasa { get; set; }
		public Nauczyciel Nauczyciel { get; set; }
		public bool Absolwent { get; set; } = false;
		public Instrument Instrument { get; set; }
		#endregion

		public Uczen(string imie, string nazwisko, int cykl, int klasa, Instrument instrument, Nauczyciel nauczyciel, string pesel) : base(imie, nazwisko, pesel)
		{
			this.Cykl = cykl;
			this.Klasa = klasa;
			this.Nauczyciel = nauczyciel;
			this.Pesel = pesel;
			this.Instrument = instrument;
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
