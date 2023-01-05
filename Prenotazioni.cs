using System;
using System.Collections.Generic;
using MySqlConnector;

namespace PrenotazioneCamere
{
    public class Prenotazione
    {
        public int Numero { get; set; }
        public DateTime Data { get; set; }
        public int Anno { get; set; }
        public int NumeroCamera { get; set; }
        public int Progressivo { get; set; }
        public DateTime Dal { get; set; }
        public DateTime Al { get; set; }
        public string CodFiscaleCliente { get; set; }
        public decimal Caparra { get; set; }
        public decimal Tariffa { get; set; }

    }
    public class Cliente
    {
        public string CodFiscale { get; set; }
        public string Cognome { get; set; }
        public string Nome { get; set; }
        public string Citta { get; set; }
        public string Provincia { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Cellulare { get; set; }
    }
    public class GestionePrenotazioni
    {
        private readonly string connectionString;

        public GestionePrenotazioni(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void RegistraCliente(Cliente cliente)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO Clienti (CodFiscale, Cognome, Nome, Citta, Provincia, Email, Telefono, Cellulare) VALUES (@codFiscale, @cognome, @nome, @citta, @provincia, @email, @telefono, @cellulare)";
                    cmd.Parameters.AddWithValue("@codFiscale", cliente.CodFiscale);
                    cmd.Parameters.AddWithValue("@cognome", cliente.Cognome);
                    cmd.Parameters.AddWithValue("@nome", cliente.Nome);
                    cmd.Parameters.AddWithValue("@citta", cliente.Citta);
                    cmd.Parameters.AddWithValue("@provincia", cliente.Provincia);
                    cmd.Parameters.AddWithValue("@email", cliente.Email);
                    cmd.Parameters.AddWithValue("@telefono", cliente.Telefono);
                    cmd.Parameters.AddWithValue("@cellulare", cliente.Cellulare);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void InserisciPrenotazione(Prenotazione prenotazione)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO camere (NumeroCamera,Descrizione,Tipologia) VALUES (@numero,@descrizione,@tipologia)";
                    cmd.CommandText = "INSERT INTO Prenotazioni (Numero, Data, Anno, NumeroCamera, Progressivo, Dal, Al, CodFiscaleCliente, Caparra, Tariffa) VALUES (@numero, @data, @anno, @numeroCamera, @progressivo, @dal, @al, @codFiscaleCliente, @caparra, @tariffa)";
                    cmd.Parameters.AddWithValue("@numero", prenotazione.Numero);
                    cmd.Parameters.AddWithValue("@data", prenotazione.Data);
                    cmd.Parameters.AddWithValue("@anno", prenotazione.Anno);
                    cmd.Parameters.AddWithValue("@numeroCamera", prenotazione.NumeroCamera);
                    cmd.Parameters.AddWithValue("@progressivo", prenotazione.Progressivo);
                    cmd.Parameters.AddWithValue("@dal", prenotazione.Dal);
                    cmd.Parameters.AddWithValue("@al", prenotazione.Al);
                    cmd.Parameters.AddWithValue("@codFiscaleCliente", prenotazione.CodFiscaleCliente);
                    cmd.Parameters.AddWithValue("@caparra", prenotazione.Caparra);
                    cmd.Parameters.AddWithValue("@tariffa", prenotazione.Tariffa);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public Prenotazione CercaPrenotazionePerNumero(int numero)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT Numero, Data, Anno, NumeroCamera, Progressivo, Dal, Al, CodFiscaleCliente, Caparra, Tariffa FROM Prenotazioni WHERE Numero = @numero";
                    cmd.Parameters.AddWithValue("@numero", numero);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Prenotazione
                            {
                                Numero = reader.GetInt32(0),
                                Data = reader.GetDateTime(1),
                                Anno = reader.GetInt32(2),
                                NumeroCamera = reader.GetInt32(3),
                                Progressivo = reader.GetInt32(4),
                                Dal = reader.GetDateTime(5),
                                Al = reader.GetDateTime(6),
                                CodFiscaleCliente = reader.GetString(7),
                                Caparra = reader.GetDecimal(8),
                                Tariffa = reader.GetDecimal(9)
                            };
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        public List<Prenotazione> CercaPrenotazioniPerData(DateTime data)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT Numero, Data, Anno, NumeroCamera, Progressivo, Dal, Al, CodFiscaleCliente, Caparra, Tariffa FROM Prenotazioni WHERE Data = @data";
                    cmd.Parameters.AddWithValue("@data", data);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Prenotazione> prenotazioni = new List<Prenotazione>();
                        while (reader.Read())
                        {
                            prenotazioni.Add(new Prenotazione
                            {
                                Numero = reader.GetInt32(0),
                                Data = reader.GetDateTime(1),
                                Anno = reader.GetInt32(2),
                                NumeroCamera = reader.GetInt32(3),
                                Progressivo = reader.GetInt32(4),
                                Dal = reader.GetDateTime(5),
                                Al = reader.GetDateTime(6),
                                CodFiscaleCliente = reader.GetString(7),
                                Caparra = reader.GetDecimal(8),
                                Tariffa = reader.GetDecimal(9)
                            });
                        }
                        return prenotazioni;
                    }
                }
            }
        }

        public void StampaSchedaRiassuntiva(Prenotazione prenotazione)
        {
            Console.WriteLine("Numero: " + prenotazione.Numero);
            Console.WriteLine("Caparra: " + prenotazione.Caparra.ToString("F2"));
            Console.WriteLine("Tariffa: " + prenotazione.Tariffa.ToString("F2"));
            Console.WriteLine("Totale: " + (prenotazione.Caparra + prenotazione.Tariffa).ToString("F2"));
            Console.WriteLine("Saldo finale da pagare: " + (prenotazione.Tariffa - prenotazione.Caparra).ToString("F2"));
        }
    }
}

