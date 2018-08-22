namespace szkola_test.Klasy
{
	class Student : Person
	{
		#region properties
		public int Cycle { get; set; }
		public int? _Class { get; set; }
		public Teacher _Teacher { get; set; }
		public bool Graduate { get; set; } = false;
		public Instrument Instrument { get; set; }
		#endregion

		public Student(string name, string surname, int cycle, int _class, Instrument instrument, Teacher teacher, string pesel) : base(name, surname, pesel)
		{
			Cycle = cycle;
			_Class = _class;
			_Teacher = teacher;
			Pesel = pesel;
			Instrument = instrument;
		}

		void RaiseClass()     // jakoś we właściwości?
		{
			if (Cycle == _Class)
			{
				Graduate = true;
				_Class = null;
			}
			else
				_Class++;
		}
	}
}
