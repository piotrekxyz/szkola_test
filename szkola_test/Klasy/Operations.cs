using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Linq;

namespace szkola_test.Klasy
{
	static class Operations
	{
		#region variables
		private static string address = ".\\SQLEXPRESS";
		private static string database = "Szkola1";
		private static string content = string.Format("Data Source={0}; Database={1}; Integrated Security=True;", address, database); // to raz nadpisane w programie już zostaje takie
		private static SqlConnection connection;
		private static SqlCommand command;
		private static string querySql = "";
		private static SqlDataReader reader;
		#endregion

		#region properties
		public static List<Student> StudentsList
		{
			get
			{
				var listOfStudents = new List<Student>();
				try
				{
					querySql = "select * from Uczniowie";
					reader = QueryToDataBase(querySql);
					if (reader.HasRows)
					{
						while (reader.Read())
						{
							Instrument instrument = new Instrument(reader["Instrument"].ToString(), null);
							string[] name_and_surname_of_teacher = reader["Nauczyciel"].ToString().Split(' ');
							listOfStudents.Add(new Student(reader["Imie"].ToString(), reader["Nazwisko"].ToString(), Convert.ToInt16(reader["Cykl"]), Convert.ToInt16(reader["Klasa"]),
								instrument, new Teacher(name_and_surname_of_teacher[0], name_and_surname_of_teacher[1], instrument), reader["Pesel"].ToString()));
						}
						reader.Close();
					}
				}
				catch (Exception ex)
				{
					Extensions.LogError("Problem podczas pobierania listy uczniów:\n", ex);
				}
				finally
				{
					connection.Close();
					if (reader != null)
					{
						reader.Dispose();
						reader = null;
					}
				}
				return listOfStudents.OrderBy(a => a.Surname).ThenBy(a => a.Name).ToList();
			}
		}

		public static List<Teacher> TeachersList
		{
			get
			{
				var listOfTeachers = new List<Teacher>();
				try
				{
					querySql = string.Format("select * from Nauczyciele");
					reader = QueryToDataBase(querySql);
					if (reader.HasRows)
					{
						while (reader.Read())
							listOfTeachers.Add(new Teacher(reader["Imie"].ToString(), reader["Nazwisko"].ToString()));
						reader.Close();
					}
				}
				catch (Exception ex)
				{
					string error = string.Format("Problem podczas pobierania listy nauczycieli :\n{0}", ex.Message);
					MessageBox.Show(error, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
				}
				finally
				{
					connection.Close();
					if (reader != null)
					{
						reader.Dispose();
						reader = null;
					}
				}
				return listOfTeachers.OrderBy(a => a.Surname).ThenBy(a => a.Name).ToList();
			}
		}

		public static List<Instrument> InstrumentsList
		{
			get
			{
				var listOfInstruments = new List<Instrument>();
				try
				{
					querySql = string.Format("select * from Instrumenty");
					reader = QueryToDataBase(querySql);
					if (reader.HasRows)
					{
						while (reader.Read())
							listOfInstruments.Add(new Instrument(reader["Nazwa"].ToString(), null));
						reader.Close();
					}
				}
				catch (Exception ex)
				{
					Extensions.LogError("Problem podczas pobierania listy instrumentów:\n", ex);
				}
				finally
				{
					connection.Close();
					if (reader != null)
					{
						reader.Dispose();
						reader = null;
					}
				}
				return listOfInstruments.OrderBy(a => a.Name).ToList();
			}
		}

		public static List<Subject> SubjectsList
		{
			get
			{
				var listOfSubjects = new List<Subject>();
				try
				{
					querySql = string.Format("select * from Przedmioty");
					reader = QueryToDataBase(querySql);
					if (reader.HasRows)
					{
						while (reader.Read())
							listOfSubjects.Add(new Subject(reader["Nazwa"].ToString(), reader["Opis"].ToString()));
						reader.Close();
					}
				}
				catch (Exception ex)
				{
					Extensions.LogError("Problem podczas pobierania listy przedmiotów:\n", ex);
				}
				finally
				{
					connection.Close();
					if (reader != null)
					{
						reader.Dispose();
						reader = null;
					}
				}
				return listOfSubjects.OrderBy(a => a.Name).ToList();
			}
		}

		public static List<Employee> EmployeesList
		{
			get
			{
				var listOfEmployees = new List<Employee>();
				try
				{
					querySql = string.Format("select * from Pracownicy");
					reader = QueryToDataBase(querySql);
					if (reader.HasRows)
					{
						while (reader.Read())
							listOfEmployees.Add(new Employee(reader["Imie"].ToString(), reader["Nazwisko"].ToString(), reader["Stanowisko"].ToString(), reader["Pesel"].ToString()));
						reader.Close();
					}
				}
				catch (Exception ex)
				{
					Extensions.LogError("Problem podczas pobierania listy pracowników:\n", ex);
				}
				finally
				{
					connection.Close();
					if (reader != null)
					{
						reader.Dispose();
						reader = null;
					}
				}
				return listOfEmployees.OrderBy(a => a.Surname).ThenBy(a => a.Name).ToList();
			}
		}
		#endregion

		#region methods
		public static bool AddStudent(Student student)
		{
			if (student is null)
				return false;
			bool check = true;
			try
			{
				querySql = string.Format("insert into Uczniowie(Imie, Nazwisko, Cykl, Klasa, Instrument, Nauczyciel, Pesel) values ('{0}', '{1}', {2}, {3}, '{4}', '{5}', '{6}')",
					student.Name, student.Surname, student.Cycle, student._Class, student.Instrument.Name, student._Teacher.Name + " " + student._Teacher.Surname, student.Pesel);
				check = InsertToDataBase(querySql);
			}
			catch (Exception ex)
			{
				Extensions.LogError("Problem podczas dodawania nowego ucznia:\n", ex);
				check = false;
			}
			return check;
		}


		public static bool AddTeacher(Teacher teacher)
		{
			if (teacher is null)
				return false;
			bool check = true;
			try
			{
				querySql = string.Format("insert into Nauczyciele(Imie, Nazwisko, Instrument, Pesel, Przedmioty) values ('{0}', '{1}', '{2}', '{3}', '{4}')",
					teacher.Name, teacher.Surname, teacher.Instrument.Name, teacher.Pesel, Extensions.MakeTeachersSubjectsList(teacher.Subjects));
				check = InsertToDataBase(querySql);
			}
			catch (Exception ex)
			{
				Extensions.LogError("Problem podczas dodawania nowego ucznia:\n", ex);
				check = false;
			}
			return check;
		}

		public static bool AddEmployee(Employee employee)
		{
			bool check = true;
			try
			{
				querySql = string.Format("insert into Pracownik(Imie, Nazwisko, Stanowisko, Pesel) values ('{0}', '{1}', '{2}', {3})",
					employee.Name, employee.Surname, employee.Position, employee.Pesel);
				check = InsertToDataBase(querySql);
			}
			catch (Exception ex)
			{
				Extensions.LogError("Problem podczas dodawania nowego pracownika:\n", ex);
				check = false;
			}
			return check;
		}

		public static bool AddSubject(Subject subject)
		{
			//if (string.IsNullOrEmpty(subject.Description) || string.IsNullOrWhiteSpace(subject.Description))
			//	subject = new Subject(subject.Name);
			//database = "Szkola1";   // wstawić domyślną bazę, która istnieje
			database = "Szkola1";   // wstawić domyślną bazę, która istnieje
			content = string.Format("Data Source={0}; Database={1}; Integrated Security=True;", address, database);
			connection = new SqlConnection(content);
			bool check = true;
			try
			{
				if (connection.State == ConnectionState.Closed)
					connection.Open();

				//querySql = string.Format("select count(Nazwa) from Przedmioty where Nazwa = {0}", subject.Name);
				//querySql = string.Format("select * from Przedmioty where Nazwa = {0}", subject.Name);
				////querySql = string.Format("select * from Przedmioty", subject.Name);
				//command = new SqlCommand(querySql, connection);
				////var result = Convert.ToInt32(command.ExecuteScalar());
				////var result2 = command.ExecuteNonQuery();
				//var czytnik = command.ExecuteReader();
				//if (czytnik.HasRows)
				//{
				//	MessageBox.Show(string.Format("Przedmiot {0} już istnieje w bazie", subject.Name), "Błąd", MessageBoxButton.OK, MessageBoxImage.Information);
				//	return false;
				//}
				//if (result2 > 0)
				//{
				//	MessageBox.Show(string.Format("Przedmiot {0} już istnieje w bazie", subject.Name), "Błąd", MessageBoxButton.OK, MessageBoxImage.Information);
				//	return false;
				//}

				querySql = string.Format("insert into Przedmioty(Nazwa, Opis) values ('{0}', '{1}')",
					subject.Name, subject.Description);
				command = new SqlCommand(querySql, connection);
				command.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				Extensions.LogError("Problem podczas dodawania nowego przedmiotu:\n", ex);
				check = false;
			}
			finally
			{
				connection.Close();
			}
			return check;
		}

		public static bool InsertToDataBase(string text, string database = "Szkola1")
		{
			content = string.Format("Data Source={0}; Database={1}; Integrated Security=True;", address, database);
			connection = new SqlConnection(content);
			bool check = true;
			try
			{
				if (connection.State == ConnectionState.Closed)
					connection.Open();
				command = new SqlCommand(text, connection);
				command.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				Extensions.LogError("Problem podczas wstawiania do bazy:\n", ex);
				check = false;
			}
			finally
			{
				connection.Close();
			}
			return check;
		}

		public static SqlDataReader QueryToDataBase(string query, string database = "Szkola1")
		{
			content = string.Format("Data Source={0}; Database={1}; Integrated Security=True;", address, database);
			connection = new SqlConnection(content);
			if (connection.State == ConnectionState.Closed)
				connection.Open();
			command = new SqlCommand(query, connection);
			reader = command.ExecuteReader();

			return reader;
		}
		#endregion
	}
}