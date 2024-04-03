using Microsoft.EntityFrameworkCore;
using PROG2500_A3_LINQ.Data;
using PROG2500_A3_LINQ.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace PROG2500_A3_LINQ.Pages
{
    public partial class CustomerOrdersPage : Page
    {
        private ChinookContext context = new ChinookContext();
        private CollectionViewSource customersViewSource;

        public CustomerOrdersPage()
        {
            InitializeComponent();
            customersViewSource = (CollectionViewSource)FindResource(nameof(customersViewSource));
            LoadData();
        }

        private void LoadData()
        {
            var customers = context.Customers
                .AsNoTracking()
                .Include(c => c.Invoices)
                .ThenInclude(i => i.InvoiceLines)
                .ToList();

            customersViewSource.Source = new ObservableCollection<Customer>(customers);

        }


        private void CustomerOrdersSearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                if (customersViewSource?.View == null)
                {
                    return;
                }

                var searchQuery = textBox.Text.ToLower();
                customersViewSource.View.Filter = item =>
                {
                    var customer = item as Customer;
                    if (customer != null)
                    {
                        return (customer.FirstName + " " + customer.LastName).ToLower().Contains(searchQuery);
                    }
                    return false;
                };

                customersViewSource.View.Refresh();
            }
        }
    }
}