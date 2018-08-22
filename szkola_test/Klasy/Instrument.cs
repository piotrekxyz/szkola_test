namespace szkola_test.Klasy
{
	class Instrument : Subject
	{
		public string Section { get; set; }

		public Instrument(string name, string section) : base(name) => Section = section;
	}
}
