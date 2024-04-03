using Microsoft.EntityFrameworkCore;
using PROG2500_A3_LINQ.Data;
using PROG2500_A3_LINQ.Models;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace PROG2500_A3_LINQ.Pages
{
    public partial class TracksPage : Page
    {
        ChinookContext context = new ChinookContext();
        CollectionViewSource trackViewSource;

        public TracksPage()
        {
            InitializeComponent();
            trackViewSource = (CollectionViewSource)FindResource(nameof(trackViewSource));
            context.Tracks.Load();
            trackViewSource.Source = context.Tracks.Local.ToObservableCollection();
        }

        private void TrackSearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (trackViewSource?.View == null)
            {
                return; // Exit the method if trackViewSource is not initialized.
            }

            string searchText = TrackSearchTextBox.Text.ToLower();
            trackViewSource.View.Filter = item =>
            {
                var track = item as Track;
                return track != null && track.Name.ToLower().Contains(searchText);
            };
            trackViewSource.View.Refresh();

        }

    }
}
