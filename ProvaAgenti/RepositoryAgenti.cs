using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvaAgenti
{
    //repository agenti
    internal class RepositoryAgenti : IRepository<Agente>
    {

        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ProvaAgenti;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
       


        
        public bool Aggiungi(Agente item)
        {
            //Tramite using e una SqLConnection mi connetto al DB
            //Creo la SqlConnection  passandole la stringa di connessione recuperata sopra
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //apro la connessione
                connection.Open();

                //Creo un comando Sql. In questo caso non è una query (select)
                
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.Text;
                //i paramtri che devo passare sono indicati con @nome, li specifico dopo
                //comando per inserire un record
                command.CommandText = "insert into dbo.Agente values(@Nome, @Cognome,@CodiceFiscale,@Areageografica,@AnnoDiInizioAttivita)";


                //----------------Command---------------------

                //Passo i miei parametri a quelli del comando
                command.Parameters.AddWithValue("@Nome", item.Nome);
                command.Parameters.AddWithValue("@Cognome", item.Cognome);
                command.Parameters.AddWithValue("@CodiceFiscale", item.CodiceFiscale);
                command.Parameters.AddWithValue("@AreaGeografica", item.AreaGeografica);
                command.Parameters.AddWithValue("@AnnoDiInizioAttivita", item.AnnoDiInizioAttivita);
                //eseguo il comando
                //restituisce il numero INT di non query eseguite 

          //Controllo

                int numRighe = command.ExecuteNonQuery();

                //----------------------------------------------

                //se il numero è uno ho eseguito un comando, cioè aggiunto l'agente
                if (numRighe == 1)
                {
                    //chiudo la connessione
                    connection.Close();
                    return true;
                }
                //se numero uguale a zero non ho eseguito a query, probabilmente ho un errore
                connection.Close();
                return false;
            }

        }

        public List<Agente> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.Text;
                //query: prelevo tutti gli agenti
                command.CommandText = "select * from Agente";

                //Leggo tutte le righe (record) della tabella
                //il tipo SqlDataReader è una specie di tabella (elenco di record)
                SqlDataReader reader = command.ExecuteReader();

                //creo una lista vuota di agenti
                List<Agente> agenti = new List<Agente>();


                //Il metodo Read() della classe SqlDataReader restituisce true se legge qualcosa
                //false se non ci sono più righe da leggere
                //si ferma quando ha letto tutto
                while (reader.Read())
                {
                    //reader[nomeColonna] restituisce il dato del record relativo a quella colonna
                    //prosegue riga per riga: la prima volta del primo record, la seconda del secondo ecc
                    //il dato è una variabile oggetto, va castato. Si può fare in fase di lettura o di passaggio al costruttore
                    var nome = (string)reader["Nome"];   //non castato
                    string cognome = (string)reader["Cognome"];    //castato
                    var codiceFiscale = (string)reader["CodiceFiscale"];
                    var areaGeografica = (string)reader["AreaGeografica"];
                    var annoDiInizioAttivita = (int)reader["AnnoDiInizioAttivita"];

                    //costruisco un audiolibro con i dati raccolti
                    //devo castare quelli che non ho già castato
                    Agente agent = new Agente(nome, cognome, codiceFiscale, areaGeografica, annoDiInizioAttivita);

                    //aggiungo l'agente alla lista creata in precedenza
                    agenti.Add(agent);
                }

                //chiudo la connessione al Db
                connection.Close();

                //restituisco la lista aggiornata
                return agenti;
            }
        }



        //lista agenti dell'area
        public List<Agente> GetByArea(string area)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.Text;
                //prelevo dal DB gli agenti dell'area che passo come parametro
                command.CommandText = "select * from dbo.Agente where AreaGeografica = @area";
                //assegno il parametro al @nome nel comando (query)
                command.Parameters.AddWithValue("@area", area);

                // leggo il risultato della query
                SqlDataReader reader = command.ExecuteReader();

                //inizializzo una lista vuota di agenti dell'area
                var agentiPerArea = new List<Agente>();

                //passo i valori della "tabella" reader a delle variabili e le casto
                while (reader.Read())
                {
                    string nome = (string)reader["Nome"];
                    string cognome = (string)reader["Cognome"];
                    var codiceFiscale = (string)reader["CodiceFiscale"];
                    var areaGeografica = (string)reader["AreaGeografica"];
                    var annoDiInizioAttivita = (int)reader["AnnoDiInizioAttivita"];

                    //riempio la ista col nuovo agente
                    Agente ag = new Agente(nome, cognome, codiceFiscale, areaGeografica, annoDiInizioAttivita);
                    agentiPerArea.Add(ag);
                }
                connection.Close();
                return agentiPerArea;
            }
        }

     
    }
}
