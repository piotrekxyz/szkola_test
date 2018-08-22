namespace szkola_test.Klasy
{
	abstract public class Person
	{
		#region properties
		public string Name { get; set; }
		public string Surname { get; set; }
		public string Pesel { get; set; }
		#endregion

		#region constructors
		public Person(string name, string surname, string pesel = null)
		{
			Name = name;
			Surname = surname;
			Pesel = pesel;
		}
		#endregion
	}
}