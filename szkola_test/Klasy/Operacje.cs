using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace szkola_test.Klasy
{
	static class Operacje
	{
		#region zmienne
		private static string address = ".\\SQLEXPRESS";
		//private static string database = "master";		// tu dac master, na wypadek gdyby jakaś baza nie istniała
		private static string database = "Szkola1";      // tu dac master, na wypadek gdyby jakaś baza nie istniała
		private static string tresc = string.Format("Data Source={0}; Database={1}; Integrated Security=True;", address, database); // to raz nadpisane w programie już zostaje takie
		private static SqlConnection polaczenie;
		private static SqlCommand komenda;
		private static string zapytanieSql = "";
		private static SqlDataReader Czytnik;
		#endregion

		#region wlasciwosci
		public static IList<Uczen> ListaUczniow
		{
			get
			{
				var lista = new List<Uczen>();
				try
				{
					database = "Baza";   // wstawić domyślną bazę, która istnieje
					tresc = string.Format("Data Source={0}; Database={1}; Integrated Security=True;", address, database);
					polaczenie = new SqlConnection(tresc);
					if (polaczenie.State == ConnectionState.Closed)
						polaczenie.Open();

					zapytanieSql = string.Format("select * from Uczniowie");
					komenda = new SqlCommand(zapytanieSql, polaczenie);
					Czytnik = komenda.ExecuteReader();

					if (Czytnik.HasRows)
					{
						while (Czytnik.Read())
						{
							Instrument i = new Instrument(Czytnik["Instrument"].ToString(), null);
							string[] imie_i_nazwisko_nauczyciela = Czytnik["Nauczyciel"].ToString().Split(' ');
							lista.Add(new Uczen(Czytnik["Imie"].ToString(), Czytnik["Nazwisko"].ToString(), Convert.ToInt16(Czytnik["Cykl"]), Convert.ToInt16(Czytnik["Klasa"]),
								i,	new Nauczyciel(imie_i_nazwisko_nauczyciela[0], imie_i_nazwisko_nauczyciela[1], i), Czytnik["Pesel"].ToString()));
						}
						Czytnik.Close();
					}
				}
				catch (Exception ex)
				{
					string byk = string.Format("Problem podczas pobierania listy uczniów :\n{0}", ex.Message);
					MessageBox.Show(byk, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
				}
				finally
				{
					polaczenie.Close();

					if (Czytnik != null)    // można w using i tego nie trzeba
					{
						Czytnik.Dispose();
						Czytnik = null;
					}
				}
				return lista;
			}
		}

		public static IList<Instrument> ListaInstrumentow
		{
			get
			{
				var lista = new List<Instrument>();
				try
				{
					database = "Baza";   // wstawić domyślną bazę, która istnieje
					tresc = string.Format("Data Source={0}; Database={1}; Integrated Security=True;", address, database);
					polaczenie = new SqlConnection(tresc);
					if (polaczenie.State == ConnectionState.Closed)
						polaczenie.Open();

					zapytanieSql = string.Format("select * from Instrumenty0");
					komenda = new SqlCommand(zapytanieSql, polaczenie);
					Czytnik = komenda.ExecuteReader();

					if (Czytnik.HasRows)
					{
						while (Czytnik.Read())
						{
							lista.Add(new Instrument(Czytnik["Nazwa"].ToString(), null));
						}
						Czytnik.Close();
					}
				}
				catch (Exception ex)
				{
					string byk = string.Format("Problem podczas pobierania listy instrumentów :\n{0}", ex.Message);
					MessageBox.Show(byk, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
				}
				finally
				{
					polaczenie.Close();

					if (Czytnik != null)    // można w using i tego nie trzeba
					{
						Czytnik.Dispose();
						Czytnik = null;
					}
				}
				return lista;
			}
		}
		#endregion

		#region metody
		public static bool DodajUcznia(Uczen u)
		{
			//database = "Szkola1";   // wstawić domyślną bazę, która istnieje
			database = "Baza";   // wstawić domyślną bazę, która istnieje
			tresc = string.Format("Data Source={0}; Database={1}; Integrated Security=True;", address, database);
			polaczenie = new SqlConnection(tresc);
			bool sprawdz = true;
			try
			{
				if (polaczenie.State == ConnectionState.Closed)
					polaczenie.Open();

				zapytanieSql = string.Format("insert into Uczniowie(Imie, Nazwisko, Cykl, Klasa, Instrument, Nauczyciel, Pesel) values ('{0}', '{1}', {2}, {3}, '{4}', '{5}', '{6}')",
					u.Imie, u.Nazwisko, u.Cykl, u.Klasa, u.Instrument.Nazwa, u.Nauczyciel.Imie + " " + u.Nauczyciel.Nazwisko, u.Pesel);
				komenda = new SqlCommand(zapytanieSql, polaczenie);
				komenda.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				string byk = string.Format("Problem podczas dodawania nowego ucznia :\n{0}", ex.Message);
				MessageBox.Show(byk, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
				sprawdz = false;
			}
			finally
			{
				polaczenie.Close();
			}
			return sprawdz;
		}

		public static bool WrzucDoBazy(string nazwaBazy, string nazwaTabeli, string nazwaKolumny, string[] elementyDoWstawienia)
		{
			bool sprawdz = true;
			try
			{
				//zapytanieSql = string.Format("if not exists(select * from sys.databases where name='{0}') create database {0}", database);
				database = nazwaBazy;   // wstawić domyślną bazę, która istnieje
				tresc = string.Format("Data Source={0}; Database={1}; Integrated Security=True;", address, database);
				polaczenie = new SqlConnection(tresc);
				if (polaczenie.State == ConnectionState.Closed)
					polaczenie.Open();

				zapytanieSql = string.Format("if not exists(select * from sys.tables where name = '{0}') create table {0}(Id smallint primary key identity(1, 1),{1} varchar(30) not null unique)", nazwaTabeli, nazwaKolumny);
				komenda = new SqlCommand(zapytanieSql, polaczenie);
				WykonujNaBazie(zapytanieSql, komenda);

				foreach (var item in elementyDoWstawienia)
				{
					zapytanieSql = string.Format("insert into {0} ({1}) values ('{2}')", nazwaTabeli, nazwaKolumny, item);
					WykonujNaBazie(zapytanieSql, komenda);
				}
			}
			catch (Exception ex)
			{
				string byk = string.Format("Problem podczas dodawania do bazy:\n{0}", ex.Message);
				MessageBox.Show(byk, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
				sprawdz = false;
			}
			finally
			{
				polaczenie.Close();
			}

			return sprawdz;
		}

		public static bool WrzucDoBazy(string nazwaBazy, string nazwaTabeli, string[] nazwyKolumn, params string[] elementyDoWstawienia)
		{
			string cos = ZrobInsertStringa(nazwaTabeli, nazwyKolumn, elementyDoWstawienia);
			bool sprawdz = true;
			//try
			//{
			//	//zapytanieSql = string.Format("if not exists(select * from sys.databases where name='{0}') create database {0}", database);
			//	database = nazwaBazy;   // wstawić domyślną bazę, która istnieje
			//	tresc = string.Format("Data Source={0}; Database={1}; Integrated Security=True;", address, database);
			//	polaczenie = new SqlConnection(tresc);
			//	if (polaczenie.State == ConnectionState.Closed)
			//		polaczenie.Open();

			//	//zapytanieSql = string.Format("if not exists(select * from sys.tables where name = '{0}') create table {0}(Id smallint primary key identity(1, 1),{1} varchar(30) not null unique)",
			//	nazwaTabeli, nazwaKolumny);
			//	komenda = new SqlCommand(zapytanieSql, polaczenie);
			//	WykonujNaBazie(zapytanieSql, komenda);

			//	foreach (var item in elementyDoWstawienia)
			//	{

			//		zapytanieSql = string.Format("insert into {0} ({1}) values ('{2}')", nazwaTabeli, nazwaKolumny, item);
			//		WykonujNaBazie(zapytanieSql, komenda);
			//	}
			//}
			//catch (Exception ex)
			//{
			//	string byk = string.Format("Problem podczas dodawania do bazy:\n{0}", ex.Message);
			//	MessageBox.Show(byk, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
			//	sprawdz = false;
			//}
			//finally
			//{
			//	polaczenie.Close();
			//}

			return sprawdz;
		}

		static private string ZrobInsertStringa(string nazwaTabeli, string[] nazwyKolumn, string[] elementyDoWstawienia)
		{
			if (elementyDoWstawienia.Length < nazwyKolumn.Length)
				return null;
			if (nazwyKolumn.Length == 1)
				return string.Format("insert into {0} ({1}) values ('{1}')", nazwaTabeli, nazwyKolumn[0]);

			var s0 = string.Format("insert into {0} ", nazwaTabeli);
			var s1 = "(";
			var s2 = ") values ('";
			var s3 = ")";

			int licznik = 0;
			var s = string.Format("({0}) values ('{0}')", nazwyKolumn[0]);
			foreach (var item in nazwyKolumn)
			{
				s1 += nazwyKolumn[licznik];
				s2 += elementyDoWstawienia[licznik] + "'";
				if (licznik < nazwyKolumn.Length - 1)
				{
					s1 += ",";
					s2 += ",'";
				}
				licznik++;
			}
			var wynik = s0 + s1 + s2 + s3;
			return wynik;
		}

		private static bool WykonujNaBazie(string zapytanie, SqlCommand komenda)
		{
			komenda.CommandText = zapytanie;
			komenda.ExecuteNonQuery();

			return true;
		}
		#endregion
	}
}