using Microsoft.EntityFrameworkCore;
using PROG2500_A3_LINQ.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PROG2500_A3_LINQ.Pages
{
    /// <summary>
    /// Interaction logic for ArtistsPage.xaml
    /// </summary>
    public partial class ArtistsPage : Page
    {
        ChinookContext context = new ChinookContext();
        CollectionViewSource artistViewSource = new CollectionViewSource();
        public ArtistsPage()
        {
            InitializeComponent();
            // Tie the markup viewsource object to the code viewsource object
            artistViewSource = (CollectionViewSource)FindResource(nameof(artistViewSource));
            // Use dbcontet to tell entity framework to load data from the db
            context.Artists.Load();
            // Set viewsource data to use data collection
            artistViewSource.Source = context.Artists.Local.ToObservableCollection();
        }
    }
}
