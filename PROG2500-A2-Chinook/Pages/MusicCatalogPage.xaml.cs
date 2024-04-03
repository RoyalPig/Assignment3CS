using Microsoft.EntityFrameworkCore;
using PROG2500_A3_LINQ.Data;
using PROG2500_A3_LINQ.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class MusicCatalogPage : Page
    {
        private ChinookContext context = new ChinookContext();
        private CollectionViewSource musicCatalogViewSource;

        public MusicCatalogPage()
        {
            InitializeComponent();
            musicCatalogViewSource = (CollectionViewSource)FindResource(nameof(musicCatalogViewSource));
            LoadData();
        }

        private void LoadData()
        {
            var artistsWithAlbums = context.Artists
                .Include(a => a.Albums)
                .ThenInclude(al => al.Tracks)
                .OrderBy(a => a.Name)
                .ToList();

            var groupedArtists = artistsWithAlbums
                .GroupBy(a => a.Name[0].ToString().ToUpper())
                .OrderBy(g => g.Key)
                .Select(g => new Grouping<string, Artist>(g.Key, g.ToList()))
                .ToList();

            musicCatalogViewSource.Source = new ObservableCollection<Grouping<string, Artist>>(groupedArtists);
            DataContext = this;
        }
        private void MusicCatalogSearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            string searchText = MusicCatalogSearchTextBox.Text.ToLower();
            musicCatalogViewSource.View.Filter = item =>
            {
                var group = item as Grouping<string, Artist>;
                if (group != null)
                {
                    // Adjust this logic if you want to search within albums and tracks as well.
                    return group.Any(artist => artist.Name.ToLower().Contains(searchText));
                }
                return false;
            };

            musicCatalogViewSource.View.Refresh();
        }

    }

    public class Grouping<TKey, TElement> : ObservableCollection<TElement>
    {
        public TKey Key { get; private set; }
        public int Count => this.Items.Count; 


        public Grouping(TKey key, IEnumerable<TElement> items) : base(items)
        {
            Key = key;
        }
    }
}