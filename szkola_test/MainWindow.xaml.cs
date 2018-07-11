using System.Collections.Generic;
using System.Linq;
using System.Windows;
using szkola_test.Klasy;

namespace szkola_test
{
	public partial class MainWindow : Window
	{
		#region zaladowanie programu
		public MainWindow()
		{
			InitializeComponent();
			InicjalizujDane();
		}

		private void InicjalizujDane()
		{
			WczytajInstrumenty();
			WczytajNauczycieli();
			UstawZebyNieKlikac();
		}

		private void UstawZebyNieKlikac()
		{
			//szescioletni_cykl_rb.IsChecked = true;	// ustawić właściwość, która zmieni klasę
			imie_tb.Text = "Ja";
			nazwisko_tb.Text = "Jestem";
			instrument_cb.SelectedIndex = 0;
			nauczyciel_cb.SelectedIndex = 1;
			pesel_tb.Text = "12345678901";
			//Operacje.WrzucDoBazy("Baza", "Instrumenty0", "Nazwa", ZwrocInstrumenty().ToArray());
			//Operacje.WrzucDoBazy("Baza", "Nauczyciele0", "Imie_i_Nazwisko", ZwrocNauczycieli().ToArray());
			string[] kolumny = new string[] { "Imie", "Nazwisko", "Pesel", "Instrument", "Nauczyciel", "Klasa", "Cykl" };
			//Operacje.WrzucDoBazy("Baza", "Testowa", kolumny, ZwrocNauczycieli().ToArray());
			var instrumenty = Operacje.ListaInstrumentow;
			var uczniowie = Operacje.ListaUczniow;
			//klasa_cb.SelectedIndex = 3;
		}

		private void WczytajNauczycieli()
		{
			IList<string> nauczyciele = new List<string>() { "Paweł Janas", "Adam Nawałka", "Zbigniew Boniek", "Robert Lewandowski", "Kaczor Donald", "Szogun Total War", "Jasio Skompy" };
			//Dictionary<string, string> x = nauczyciele.Select(a => a.Split(' ').ToDictionary(a));	// jak sortować po podzieleniu po 2 wyrazie?
			nauczyciele = nauczyciele.OrderBy(a => a).ToList();
			if (nauczyciel_cb.Items.Count < 1)
			{
				foreach (var item in nauczyciele)
					nauczyciel_cb.Items.Add(item);
			}
		}

		private void WczytajInstrumenty()
		{
			IList<string> instrumenty = new List<string>() { "akordeon", "perkusja", "flet", "trąbka", "saksofon", "klarnet", "puzon", "skrzypce", "altówka", "gitara", "fortepian" };
			instrumenty = instrumenty.OrderBy(a => a).ToList();
			if (instrument_cb.Items.Count < 1)
			{
				foreach (var item in instrumenty)
					instrument_cb.Items.Add(item);
			}
		}
		#endregion

		#region metody
		private void WczytajKlasy(int ile)
		{
			klasa_cb.Items.Clear();
			for (int i = 1; i <= ile; i++)
				klasa_cb.Items.Add(i);
		}

		private IList<string> ZwrocInstrumenty()
		{
			IList<string> instrumenty = new List<string>() { "akordeon", "perkusja", "flet", "trąbka", "saksofon", "klarnet", "puzon", "skrzypce", "altówka", "gitara", "fortepian" };
			instrumenty = instrumenty.OrderBy(a => a).ToList();

			return instrumenty;
		}

		private IList<string> ZwrocNauczycieli()
		{
			IList<string> nauczyciele = new List<string>() { "Paweł Janas", "Adam Nawałka", "Zbigniew Boniek", "Robert Lewandowski", "Kaczor Donald", "Szogun Total War", "Jasio Skompy" };
			nauczyciele = nauczyciele.OrderBy(a => a).ToList();

			return nauczyciele;
		}

		private void Dodaj_bt_Click(object sender, RoutedEventArgs e)
		{

			if (!string.IsNullOrWhiteSpace(imie_tb.Text) && !string.IsNullOrWhiteSpace(nazwisko_tb.Text) && klasa_cb.SelectedIndex > -1 && instrument_cb.SelectedIndex > -1 && nauczyciel_cb.SelectedIndex > -1 && !string.IsNullOrWhiteSpace(pesel_tb.Text))
				if (Operacje.DodajUcznia(ZrobUcznia()))
					MessageBox.Show(string.Format("Dodano ucznia: \"{0} {1}\"", imie_tb.Text, nazwisko_tb.Text));
		}

		private Uczen ZrobUcznia()
		{
			string imie = imie_tb.Text;
			string nazwisko = nazwisko_tb.Text;
			int cykl = (bool)czteroletni_cykl_rb.IsChecked ? 4 : 6;
			int klasa = (int)klasa_cb.SelectedValue;
			string instrument = instrument_cb.SelectedValue.ToString();
			string[] nauczyciel = nauczyciel_cb.SelectedValue.ToString().Split(' ');
			string pesel = pesel_tb.Text;
			Instrument i = new Instrument(instrument_cb.SelectedValue.ToString(), "jakaś sekcja");
			Nauczyciel n = new Nauczyciel(nauczyciel[0], nauczyciel[1], i);
			//Nauczyciel nn = new Nauczyciel(nauczyciel[0], nauczyciel[1], "a vista", "zespół kameralny", "duet");
			Uczen u = new Uczen(imie, nazwisko, cykl, klasa, i, n, pesel);

			return u;
		}

		private void Czteroletni_cykl_rb_click(object sender, RoutedEventArgs e)
		{
			WczytajKlasy(4);
		}

		private void Szescioletni_cykl_rb_click(object sender, RoutedEventArgs e)
		{
			WczytajKlasy(6);
		}
		#endregion
	}
}
