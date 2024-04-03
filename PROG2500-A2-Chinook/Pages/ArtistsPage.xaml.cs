using Microsoft.EntityFrameworkCore;
using PROG2500_A3_LINQ.Data;
using PROG2500_A3_LINQ.Models;
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
    public partial class ArtistsPage : Page
    {
        ChinookContext context = new ChinookContext();
        CollectionViewSource artistViewSource;

        public ArtistsPage()
        {
            InitializeComponent();
            artistViewSource = (CollectionViewSource)FindResource(nameof(artistViewSource));
            context.Artists.Load();
            artistViewSource.Source = context.Artists.Local.ToObservableCollection();
        }

        private void ArtistSearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (artistViewSource?.View == null)
            {
                return; // Exit if the view is not ready.
            }

            string searchText = ArtistSearchTextBox.Text.ToLower();
            artistViewSource.View.Filter = item =>
            {
                if (string.IsNullOrEmpty(searchText))
                    return true; // If the search text is empty, show all artists.

                if (item is Artist artist)
                {
                    return artist.Name.ToLower().Contains(searchText);
                }
                return false;
            };
            artistViewSource.View.Refresh();
        }

    }
}
