using System;

namespace szkola_test.Klasy
{
	class Subject
	{
		public string Name { get; set; }
		public string Description { get; set; }

		public Subject(string name) => this.Name = name;
		public Subject(string name, string description) : this(name) { this.Description = description; }
	}
}
