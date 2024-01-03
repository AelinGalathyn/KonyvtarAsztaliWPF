using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KonyvtarAsztaliWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Statisztika statisztika;
        public MainWindow()
        {
            InitializeComponent();
            Beolvasas();
        }

        private void Beolvasas()
        {
            statisztika = new Statisztika();
            dataGrid.ItemsSource = statisztika.Konyvek;
        }

        private void btnTorles_Click(object sender, RoutedEventArgs e)
        {
            Konyv konyv = dataGrid.SelectedItem as Konyv;
            if(konyv != null)
            {
                MessageBoxResult result = MessageBox.Show("Biztos szeretné törölni a kiválasztott könyvet?", "Megerősítés", MessageBoxButton.YesNo);
                if(result == MessageBoxResult.Yes)
                {
                    if (statisztika.Torles(konyv))
                    {
                        statisztika.Konyvek.Remove(konyv);
                        MessageBox.Show("Sikeres törlés!");
                        dataGrid.Items.Refresh();
                    }
                    else
                    {
                        MessageBox.Show("Sikertelen törlés.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Törléshez előbb válasszon ki könyvet!");
            }
        }
    }
}