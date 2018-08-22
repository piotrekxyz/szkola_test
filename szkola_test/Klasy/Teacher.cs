using System.Collections.Generic;
using System.Linq;

namespace szkola_test.Klasy
{
	class Teacher : Employee
	{
		#region properties
		public List<Subject> Subjects { get; set; }
		public Instrument Instrument { get; set; }
		#endregion
		private static string position = "nauczyciel";

		#region constructors
		public Teacher(string name, string surname, Instrument instrument = null, string pesel = null, params string[] subjects) : base(name, surname, position, pesel)
		{
			Instrument = instrument;
			Subjects = MakeSubjects(subjects);
		}

		public static List<Subject> MakeSubjects(string[] subjects)
		{
			List<Subject> listSubjectsTemp = new List<Subject>();
			subjects.ToList().ForEach(a => listSubjectsTemp.Add(new Subject(a)));
			return listSubjectsTemp;
		}
		#endregion
	}
}