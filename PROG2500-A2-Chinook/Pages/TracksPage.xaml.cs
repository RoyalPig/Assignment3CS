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
    /// Interaction logic for TracksPage.xaml
    /// </summary>
    public partial class TracksPage : Page
    {
        ChinookContext context = new ChinookContext();
        CollectionViewSource trackViewSource = new CollectionViewSource();
        public TracksPage()
        {
            InitializeComponent();

            // Tie the markup viewsource object to the code viewsource object
            trackViewSource = (CollectionViewSource)FindResource(nameof(trackViewSource));
            // Use dbcontet to tell entity framework to load data from the db
            context.Tracks.Load();
            // Set viewsource data to use data collection
            trackViewSource.Source = context.Tracks.Local.ToObservableCollection();
        }
    }
}
