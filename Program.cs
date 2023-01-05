using System;
using System.Collections.Generic;
using MySqlConnector;

namespace PrenotazioneCamere
{
    public class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "SERVER=localhost;" + "DATABASE=prenotazionicamere;" + "UID=root;" + "PASSWORD='';";
            GestionePrenotazioni gestore = new GestionePrenotazioni(connectionString);
            

            Console.WriteLine("1. Registra nuovo cliente");
            Console.WriteLine("2. Carica nuova prenotazione");
            Console.WriteLine("3. Cerca prenotazione per numero");
            Console.WriteLine("4. Cerca prenotazioni per data");
            Console.WriteLine("5. Stampa scheda riassuntiva dei costi");
            Console.WriteLine("Scegli un'opzione:");

            string scelta = Console.ReadLine();
            int opzione = 0;
            if (int.TryParse(scelta, out opzione))
            {
                switch (opzione)
                {
                    case 1:
                        // Registra un nuovo cliente
                        Console.WriteLine("Inserisci il codice fiscale:");
                        string codFiscale = Console.ReadLine();
                        Console.WriteLine("Inserisci il cognome:");
                        string cognome = Console.ReadLine();
                        Console.WriteLine("Inserisci il nome:");
                        string nome = Console.ReadLine();
                        Console.WriteLine("Inserisci la città:");
                        string citta = Console.ReadLine();
                        Console.WriteLine("Inserisci la provincia:");
                        string provincia = Console.ReadLine();
                        Console.WriteLine("Inserisci l'email:");
                        string email = Console.ReadLine();
                        Console.WriteLine("Inserisci il telefono:");
                        string telefono = Console.ReadLine();
                        Console.WriteLine("Inserisci il cellulare:");
                        string cellulare = Console.ReadLine();
                        Cliente nuovoCliente = new Cliente
                        {
                            CodFiscale = codFiscale,
                            Cognome = cognome,
                            Nome = nome,
                            Citta = citta,
                            Provincia = provincia,
                            Email = email,
                            Telefono = telefono,
                            Cellulare = cellulare
                        };
                        gestore.RegistraCliente(nuovoCliente);
                        break;
                    case 2:
                        // Carica una nuova prenotazione
                        Console.WriteLine("Inserisci la data della prenotazione (gg/mm/aaaa):");
                        DateTime dataPrenotazione = DateTime.Parse(Console.ReadLine());
                        Console.WriteLine("Inserisci l'anno della prenotazione:");
                        int annoPrenotazione = int.Parse(Console.ReadLine());
                        Console.WriteLine("Inserisci il numero della camera:");
                        int numeroCamera = int.Parse(Console.ReadLine());
                        Console.WriteLine("Inserisci il numero progressivo:");
                        int numeroProgressivo = int.Parse(Console.ReadLine());
                        Console.WriteLine("Inserisci la data di inizio soggiorno (gg/mm/aaaa):");
                        DateTime dataInizioSoggiorno = DateTime.Parse(Console.ReadLine());
                        Console.WriteLine("Inserisci la data di fine soggiorno (gg/mm/aaaa):");
                        DateTime dataFineSoggiorno = DateTime.Parse(Console.ReadLine());
                        Console.WriteLine("Inserisci il codice fiscale del cliente:");
                        string codFiscaleCliente = Console.ReadLine();
                        Console.WriteLine("Inserisci la caparra:");
                        decimal caparra = decimal.Parse(Console.ReadLine());
                        Console.WriteLine("Inserisci la tariffa:");
                        decimal tariffa = decimal.Parse(Console.ReadLine());
                        
                        Prenotazione nuovaPrenotazione = new Prenotazione
                        {
                            Data = dataPrenotazione,
                            Anno = annoPrenotazione,
                            NumeroCamera = numeroCamera,
                            Progressivo = numeroProgressivo,
                            Dal = dataInizioSoggiorno,
                            Al = dataFineSoggiorno,
                            CodFiscaleCliente = codFiscaleCliente,
                            Caparra = caparra,
                            Tariffa = tariffa
                        };
                        gestore.InserisciPrenotazione(nuovaPrenotazione);
                        break;
                    case 3:
                        // Cerca la prenotazione per numero
                        Console.WriteLine("Inserisci il numero della prenotazione:");
                        int numeroPrenotazione = int.Parse(Console.ReadLine());
                        Prenotazione prenotazioneTrovata = gestore.CercaPrenotazionePerNumero(numeroPrenotazione);
                        if (prenotazioneTrovata != null)
                        {
                            Console.WriteLine("Numero: " + prenotazioneTrovata.Numero);
                            Console.WriteLine("Data: " + prenotazioneTrovata.Data.ToShortDateString());
                            Console.WriteLine("Anno: " + prenotazioneTrovata.Anno);
                            Console.WriteLine("Numero camera: " + prenotazioneTrovata.NumeroCamera);
                            Console.WriteLine("Progressivo: " + prenotazioneTrovata.Progressivo);
                            Console.WriteLine("Dal: " + prenotazioneTrovata.Dal.ToShortDateString());
                            Console.WriteLine("Al: " + prenotazioneTrovata.Al.ToShortDateString());
                            Console.WriteLine("Codice fiscale cliente: " + prenotazioneTrovata.CodFiscaleCliente);
                            Console.WriteLine("Caparra: " + prenotazioneTrovata.Caparra.ToString("F2"));
                            Console.WriteLine("Tariffa: " + prenotazioneTrovata.Tariffa.ToString("F2"));
                        }
                        break;
                    case 4:
                        // Cerca le prenotazioni per data
                        Console.WriteLine("Inserisci la data delle prenotazioni da cercare (gg/mm/aaaa):");
                        DateTime dataCercaPrenotazioni = DateTime.Parse(Console.ReadLine());
                        List<Prenotazione> prenotazioniTrovate = gestore.CercaPrenotazioniPerData(dataCercaPrenotazioni);
                        if (prenotazioniTrovate.Count > 0)
                        {
                            Console.WriteLine("Prenotazioni trovate:");
                            foreach (Prenotazione prenotazione in prenotazioniTrovate)
                            {
                                Console.WriteLine("Numero: " + prenotazione.Numero);
                                Console.WriteLine("Data: " + prenotazione.Data.ToShortDateString());
                                Console.WriteLine("Anno: " + prenotazione.Anno);
                                Console.WriteLine("Numero camera: " + prenotazione.NumeroCamera);
                                Console.WriteLine("Progressivo: " + prenotazione.Progressivo);
                                Console.WriteLine("Dal: " + prenotazione.Dal.ToShortDateString());
                                Console.WriteLine("Al: " + prenotazione.Al.ToShortDateString());
                                Console.WriteLine("Codice fiscale cliente: " + prenotazione.CodFiscaleCliente);
                                Console.WriteLine("Caparra: " + prenotazione.Caparra.ToString("F2"));
                                Console.WriteLine("Tariffa: " + prenotazione.Tariffa.ToString("F2"));
                            }
                        }
                        else
                        {
                            Console.WriteLine("Nessuna prenotazione trovata per la data specificata.");
                        }
                        break;
                    case 5:
                        // Stampa una scheda riassuntiva dei costi per la prenotazione
                        Console.WriteLine("Inserisci il numero della prenotazione per cui stampare la scheda riassuntiva dei costi:");
                        int numeroPrenotazioneSchedaRiassuntiva = int.Parse(Console.ReadLine());
                        Prenotazione prenotazioneSchedaRiassuntiva = gestore.CercaPrenotazionePerNumero(numeroPrenotazioneSchedaRiassuntiva);
                        if (prenotazioneSchedaRiassuntiva != null)
                        {
                            gestore.StampaSchedaRiassuntiva(prenotazioneSchedaRiassuntiva);
                        }
                        else
                        {
                            Console.WriteLine("Nessuna prenotazione trovata per il numero specificato.");
                        }
                        break;
                    default:
                        Console.WriteLine("Opzione non valida. Riprova.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Input non valido. Riprova.");
            }
        }
    }
}