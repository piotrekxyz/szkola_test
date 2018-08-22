namespace szkola_test.Klasy
{
	class Employee : Person
	{
		public string Position { get; set; }

		public Employee(string name, string surname, string position, string pesel = null) : base(name, surname, pesel) => Position = position;
	}
}
