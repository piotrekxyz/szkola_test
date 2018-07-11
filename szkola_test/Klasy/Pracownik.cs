using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace szkola_test.Klasy
{
	class Pracownik : Osoba
	{
		public string Stanowisko { get; set; }

		public Pracownik(string imie, string nazwisko, string stanowisko) : base(imie, nazwisko) => this.Stanowisko = stanowisko;
	}
}
