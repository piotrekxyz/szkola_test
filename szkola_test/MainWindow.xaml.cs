using System.Collections.Generic;
using System.Linq;
using System.Windows;
using szkola_test.Klasy;

namespace szkola_test
{
	public partial class MainWindow : Window
	{
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
			//klasa_cb.SelectedIndex = 3;
		}

		private void WczytajKlasy(int ile)
		{
			klasa_cb.Items.Clear();
			for (int i = 1; i <= ile; i++)
				klasa_cb.Items.Add(i);
		}

		private void WczytajNauczycieli()
		{
			IList<string> nauczyciele = new List<string>() { "Paweł Janas", "Adam Nawałka", "Zbigniew Boniek", "Robert Lewandowski", "Kaczor Donald", "Szogun Total War" };
			//Dictionary<string, string> x = nauczyciele.Select(a => a.Split(' ').ToDictionary(a));	// dowiedzieć się jak sortować po podzieleniu po 2 wyrazie
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

		private IList<string> ZwrocInstrumenty()
		{
			IList<string> instrumenty = new List<string>() { "akordeon", "perkusja", "flet", "trąbka", "saksofon", "klarnet", "puzon", "skrzypce", "altówka", "gitara", "fortepian" };
			instrumenty = instrumenty.OrderBy(a => a).ToList();
			string s = "";

			foreach (var item in instrumenty)
				s = item;

			return instrumenty;
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
	}
}
