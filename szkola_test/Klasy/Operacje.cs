using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace szkola_test.Klasy
{
	static class Operacje
	{
		static string address = ".\\SQLEXPRESS";

		static string tresc = "Data Source=" + address + ";Database=Szkola1;Integrated Security=True;";
		private static SqlConnection Polaczenie = new SqlConnection(tresc);
		private static SqlCommand Komenda;
		//private static SqlDataReader Czytnik;

		private static string zapytanieSql = "";

		public static object MessageBoxButtons { get; private set; }

		public static bool DodajUcznia(Uczen u)
		{
			bool sprawdz = true;
			try
			{
				if (Polaczenie.State == ConnectionState.Closed)
					Polaczenie.Open();
				//zapytanieSql = string.Format("insert into Uczniowie(Imie, Nazwisko, Cykl, Klasa, Instrument, Nauczyciel) values ('{0}', '{1}', {2}, {3}, {4}, {5})", u.Imie, u.Nazwisko, u.Cykl, u.Klasa, u.Instrument, u.Nauczyciel);
				zapytanieSql = string.Format("insert into Uczniowie11(Imie, Nazwisko, Cykl, Klasa, Instrument, Nauczyciel, Pesel) values ('{0}', '{1}', {2}, {3}, '{4}', '{5}', '{6}')",
					u.Imie, u.Nazwisko, u.Cykl, u.Klasa, u.Instrument.Nazwa, u.Nauczyciel.Imie + " " + u.Nauczyciel.Nazwisko, u.Pesel);
				Komenda = new SqlCommand(zapytanieSql, Polaczenie);
				Komenda.CommandText = zapytanieSql;
				Komenda.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				string byk = string.Format("Problem podczas dodawania nowego ucznia :\n{0}", ex.Message);
				MessageBox.Show(byk, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
				sprawdz = false;
			}
			finally
			{
				Polaczenie.Close();
			}
			return sprawdz;
		}
	}
}