using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using szkola_test.Klasy;

namespace szkola_test
{
	public partial class MainWindow : Window
	{
		#region variables
		List<CheckBox> checkBoxSubjectsList = new List<CheckBox>();
		List<Subject> subjects;
		List<Employee> employees;
		List<Teacher> teachers;
		List<Instrument> instruments;
		List<Student> students;
		#endregion

		#region load program
		public MainWindow()
		{
			InitializeComponent();
			InitializeData();
		}

		private void InitializeData()
		{
			LoadInstruments();
			LoadTeachers();
			LoadEmployees();
			LoadSubjects();
			Set_To_Dont_Click();
			MakeCheckBoxesOfSubjects();
			ShowSubjects();
			LoadStudents();
		}

		

		private void Set_To_Dont_Click()
		{
			name_student_tb.Text = "Ktoś";
			surname_student_tb.Text = "jakiś";
			instrument_student_cb.SelectedIndex = 0;
			instrument_teacher_cb.SelectedIndex = 0;
			teacher_cb.SelectedIndex = 1;
			pesel_student_tb.Text = "22345678903";
			string[] columns = new string[] { "Imie", "Nazwisko" };
			sixYearCycle_rb.IsChecked = true;
			LoadClasses(6);
			name_teacher_tb.Text = "Znany";
			surname_teacher_tb.Text = "Belfer";
			pesel_teacher_tb.Text = "22345678903";
		}

		private void LoadStudents()
		{
			students = Operations.StudentsList;
		}

		private void LoadSubjects()
		{
			listOfSubjects_listBox.Items.Clear();
			subjects = Operations.SubjectsList;
			subjects.ForEach(a => listOfSubjects_listBox.Items.Add(a.Name));
		}

		private void LoadEmployees()
		{
			listOfEmployees_listBox.Items.Clear();
			employees = Operations.EmployeesList;
			employees.ForEach(a => listOfEmployees_listBox.Items.Add(a.Name + " " + a.Surname));
		}

		private void LoadTeachers()
		{
			teacher_cb.Items.Clear();
			teachers = Operations.TeachersList;
			teachers.ForEach(a => teacher_cb.Items.Add(a.Name + " " + a.Surname));
		}

		private void LoadInstruments()
		{
			instruments = Operations.InstrumentsList;
			instruments.ForEach(a => instrument_student_cb.Items.Add(a.Name));
			instruments.ForEach(a => instrument_teacher_cb.Items.Add(a.Name));
			instruments.ForEach(a => listOfInstruments_listBox.Items.Add(a.Name));
		}

		private void LoadClasses(int count)
		{
			class_cb.Items.Clear();
			var classes = Extensions.MakeClasses(count);
			classes.ForEach(a => class_cb.Items.Add(a));
			class_cb.SelectedIndex = 0;
		}
		#endregion

		#region methods
		private void Add_student_bt_Click(object sender, RoutedEventArgs e)
		{
			if (Operations.AddStudent(MakeStudent()))
				Extensions.LogDebug(string.Format("Dodano ucznia: \"{0} {1}\"", name_student_tb.Text, surname_student_tb.Text));
		}

		private Student MakeStudent()
		{
			if (string.IsNullOrWhiteSpace(name_student_tb.Text) || string.IsNullOrWhiteSpace(surname_student_tb.Text) || class_cb.SelectedIndex < 0 || instrument_student_cb.SelectedIndex < 0 || teacher_cb.SelectedIndex < 0 || string.IsNullOrWhiteSpace(pesel_student_tb.Text))
				return null;

			int cycle = (bool)fourYearCycle_rb.IsChecked ? 4 : 6;
			int _class = (int)class_cb.SelectedValue;
			string[] teacher_text = teacher_cb.SelectedValue.ToString().Split(' ');
			Instrument instrument = new Instrument(instrument_student_cb.SelectedValue.ToString(), "jakaś sekcja");
			Teacher teacher = new Teacher(teacher_text[0], teacher_text[1], instrument);
			Student student = new Student(name_student_tb.Text, surname_student_tb.Text, cycle, _class, instrument, teacher, pesel_student_tb.Text);

			return student;
		}

		private Teacher MakeTeacher()
		{
			List<string> teacherSubjects = new List<string>();

			foreach (CheckBox item in subject_sp.Children)
				if (item.IsChecked.Value)
					teacherSubjects.Add(item.Content.ToString());
			Instrument instrument = instruments.FirstOrDefault(a => a.Name == instrument_teacher_cb.SelectedValue.ToString());
			Teacher teacher = new Teacher(name_teacher_tb.Text, surname_teacher_tb.Text, instrument, pesel_teacher_tb.Text, teacherSubjects.ToArray());
			return teacher;
		}

		private void FourYearCycle_rb_click(object sender, RoutedEventArgs e)
		{
			LoadClasses(4);
		}

		private void SixYearCycle_rb_click(object sender, RoutedEventArgs e)
		{
			LoadClasses(6);
		}
		#endregion

		private void Add_teacher_bt_Click(object sender, RoutedEventArgs e)
		{
			if (Operations.AddTeacher(MakeTeacher()))
				Extensions.LogDebug(string.Format("Dodano nauczyciela: \"{0} {1}\"", name_teacher_tb.Text, surname_teacher_tb.Text));
		}

		private void Add_instrument_bt_Click(object sender, RoutedEventArgs e)
		{

		}

		void MakeCheckBoxesOfSubjects()
		{
			if (subjects.Count < 1)
				return;
			CheckBox checkBox;
			int margin_top = 3, margin = 2;
			List<CheckBox> localCheckBoxList = new List<CheckBox>();

			foreach (var item in subjects)
			{
				checkBox = new CheckBox
				{
					Name = item.Name.Replace(' ', '_'),
					Content = item.Name,
					Margin = new Thickness(margin, margin_top, margin, margin)
				};
				localCheckBoxList.Add(checkBox);
			}
			checkBoxSubjectsList = localCheckBoxList;
		}

		void ShowSubjects()
		{
			if (checkBoxSubjectsList.Count < 1)
				return;
			checkBoxSubjectsList.ForEach(cb => subject_sp.Children.Add(cb));
		}

		private void Add_subject_bt_Click(object sender, RoutedEventArgs e)
		{
			Operations.AddSubject(new Subject(subject_name_tb.Text, subject_description_tb.Text));
			LoadSubjects();
		}

		private void Add_employee_bt_Click(object sender, RoutedEventArgs e)
		{
			Operations.AddEmployee(new Employee(employee_name_tb.Text, employee_surname_tb.Text, employee_position_tb.Text, employee_pesel_tb.Text));
			LoadEmployees();
		}

		private void ChangedClickSubjects(object sender, SelectionChangedEventArgs e)
		{
			if (listOfSubjects_listBox.SelectedIndex == -1)
				return; // wyczyść
			string item = listOfSubjects_listBox.SelectedItem.ToString();
			Subject subject = subjects.FirstOrDefault(x => x.Name == item);
			if (subject != null)
			{
				subject_name_tb.Text = subject.Name;
				subject_description_tb.Text = subject.Description;
			}
		}

		private void ChangedClickEmployees(object sender, SelectionChangedEventArgs e)
		{
			if (listOfEmployees_listBox.SelectedIndex == -1)
				return;
			var names = listOfEmployees_listBox.SelectedItem.ToString().Split(' ');
			Employee employee = employees.FirstOrDefault(x => x.Name == names[0] && x.Surname == names[1] );
			if (employee != null)
			{
				employee_name_tb.Text = employee.Name;
				employee_surname_tb.Text = employee.Surname;
				employee_position_tb.Text = employee.Position;
				employee_pesel_tb.Text = employee.Pesel;
			}
		}

		private void ChangedClickInstrument(object sender, SelectionChangedEventArgs e)
		{
			if (listOfInstruments_listBox.SelectedIndex == -1)
				return;
			string item = listOfInstruments_listBox.SelectedItem.ToString();
			Instrument instrument = instruments.FirstOrDefault(x => x.Name == item);
			if (instrument != null)
				instrument_name_tb.Text = instrument.Name;
		}
	}
}
