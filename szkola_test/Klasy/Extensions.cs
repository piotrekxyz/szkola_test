using System;
using System.Collections.Generic;
using System.Windows;

namespace szkola_test.Klasy
{
	static class Extensions
	{
		#region variables
		private static string address = ".\\SQLEXPRESS";
		//private static string database = "master";		// tu dac master, na wypadek gdyby jakaś baza nie istniała
		private static string database = "Szkola1";      // tu dac master, na wypadek gdyby jakaś baza nie istniała
		private static string content = string.Format("Data Source={0}; Database={1}; Integrated Security=True;", address, database); // to raz nadpisane w programie już zostaje takie
		#endregion

		public static List<int> MakeClasses(int countClasses)
		{
			List<int> classes = new List<int>();
			for (int i = 1; i <= countClasses; i++)
				classes.Add(i);
			return classes;
		}

		public static string MakeTeachersSubjectsList(List<Subject> subjects)
		{
			string result = "";
			if (subjects.Capacity < 1)
				return null;
			foreach (var item in subjects)
				result += item.Name + ";";
			return result;
		}

		public static void LogError(string text, Exception ex)
		{
			MessageBox.Show(string.Format(text + "{0}", ex.Message), "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
		}

		public static void LogDebug(string text)
		{
			MessageBox.Show(text, "Info", MessageBoxButton.OK, MessageBoxImage.Information);
		}

		public static bool AddToDatabase(string tableName, string[] columnsNames, string database = "Szkola1", params string[][] itemsToInsert)
		{//string querySql = string.Format("if not exists(select * from sys.tables where name = '{0}') create table {0}(Id smallint primary key identity(1, 1),{1} varchar(30) not null unique)", tableName, columnsNames[0]);
			bool check = true;
			List<string> inserts = new List<string>();

			foreach (var item in itemsToInsert)
				inserts.Add(MakeInsertString(tableName, columnsNames, item));

			foreach (var item in inserts)
					check = Operations.InsertToDataBase(item, database);

			return check;
		}

		static private string MakeInsertString(string tableName, string[] columnsNames, string[] itemsToInsert)
		{
			if (itemsToInsert.Length < columnsNames.Length)
				return null;

			var query = MakeInsertInto(tableName, columnsNames);
			var right_bracket = ")";

			for (int i = 0; i < columnsNames.Length; i++)
			{
				query += itemsToInsert[i] + "'";
				if (i < columnsNames.Length - 1)
					query += ",'";
			}
			return query + right_bracket;
		}

		static private string MakeInsertInto(string tableName, string[] columnsNames)
		{
			var insert_into = "insert into " + tableName;
			var left_bracket = "(";
			var values = ") values ('";

			for (int i = 0; i < columnsNames.Length; i++)
			{
				left_bracket += columnsNames[i];
				if (i < columnsNames.Length - 1)
					left_bracket += ",";
			}

			string result = insert_into + left_bracket + values;
			return result;
		}

		//static public string[][] SplitStringsInTabAndReturn(string[] data)
		//{
		//	string[][] result = new string[data.Length][];
		//	for (int i = 0; i < data.Length; i++)
		//		result[i] = data[i].Split(' ');
		//	return result;
		//}

		//public static IList<string> ReturnInstruments()
		//{
		//	IList<string> instruments = new List<string>() { "akordeon", "perkusja", "flet", "trąbka", "saksofon", "klarnet", "puzon", "skrzypce", "altówka", "gitara", "fortepian" };
		//	instruments = instruments.OrderBy(a => a).ToList();
		//	return instruments;
		//}

		//public static IList<string> ReturnTeachers()
		//{
		//	IList<string> teachers = new List<string>() { "Paweł Janas", "Adam Nawałka", "Zbigniew Boniek", "Robert Lewandowski", "Kaczor Donald", "Szogun TotalWar", "Jasio Skompy" };
		//	teachers = teachers.OrderBy(a => a).ToList();
		//	return teachers;
		//}
	}
}
