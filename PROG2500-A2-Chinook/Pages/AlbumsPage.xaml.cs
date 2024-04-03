using Microsoft.EntityFrameworkCore;
using PROG2500_A3_LINQ.Data;
using PROG2500_A3_LINQ.Models;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace PROG2500_A3_LINQ.Pages
{
    public partial class AlbumsPage : Page
    {
        ChinookContext context = new ChinookContext();
        CollectionViewSource albumViewSource;

        public AlbumsPage()
        {
            InitializeComponent();
            albumViewSource = (CollectionViewSource)FindResource(nameof(albumViewSource));
            context.Albums.Load();
            albumViewSource.Source = context.Albums.Local.ToObservableCollection();
        }

        private void AlbumSearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (albumViewSource?.View == null)
            {
                return; // Exit if the view is not ready.
            }

            string searchText = AlbumSearchTextBox.Text.ToLower();
            albumViewSource.View.Filter = item =>
            {
                if (string.IsNullOrEmpty(searchText))
                    return true; // If the search text is empty, show all albums.

                if (item is Album album)
                {
                    return album.Title.ToLower().Contains(searchText);
                }
                return false;
            };
            albumViewSource.View.Refresh();
        }
    }
}
