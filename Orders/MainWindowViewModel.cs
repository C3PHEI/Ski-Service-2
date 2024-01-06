using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Ski_Service_2.Orders
{
    // MainWindowViewModel.cs
    public class MainWindowViewModel : ViewModelBase
    {
        private ObservableCollection<OrderModel> serviceOrders;

        public ObservableCollection<OrderModel> ServiceOrders
        {
            get { return serviceOrders; }
            set
            {
                if (serviceOrders != value)
                {
                    serviceOrders = value;
                    OnPropertyChanged(nameof(ServiceOrders));
                }
            }
        }

        public MainWindowViewModel()
        {
            LoadOrders(); // Lade die Bestellungen beim Initialisieren
        }

        private void LoadOrders()
        {
            try
            {
                // Verwende die Connection String für deine Datenbank
                using (SqlConnection connection = new SqlConnection(AppConfig.ConnectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM Bestellungen";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            ObservableCollection<OrderModel> orders = new ObservableCollection<OrderModel>();

                            while (reader.Read())
                            {
                                OrderModel order = new OrderModel
                                {
                                    BestellungsID = Convert.ToInt32(reader["BestellungsID"]),
                                    Prioritaet = reader["Prioritaet"].ToString(),
                                    Dienstleistung = reader["Service"].ToString(),
                                    Name = reader["Name"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    Telefon = reader["Telefon"].ToString(),
                                    DatumEinreichung = Convert.ToDateTime(reader["DatumEinreichung"]),
                                    Preis = Convert.ToDecimal(reader["Preis"]),
                                    StatusID = Convert.ToInt32(reader["StatusID"]),
                                    MitarbeiterID = Convert.ToInt32(reader["MitarbeiterID"])
                                };
                                orders.Add(order);
                            }
                            ServiceOrders = orders;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle Fehler entsprechend deinen Anforderungen
                MessageBox.Show($"Fehler beim Laden der Bestellungen: {ex.Message}");
            }
        }
    }
}
