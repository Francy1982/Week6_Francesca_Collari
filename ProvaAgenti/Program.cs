// See https://aka.ms/new-console-template for more information

using ProvaAgenti;



//1) a, b, e, g, 
//2) b, d  
//3) c

RepositoryAgenti repositoryAgenti = new RepositoryAgenti();

bool continua = true; 
Console.WriteLine("\nBenvenuto in caserma");

while (continua)
{
    Console.WriteLine("\nScegli cosa vuoi fare: \n");
    Console.WriteLine("***************Menu*************");
    Console.WriteLine("1. Mostra tutti gli agenti");
    Console.WriteLine("2. Mostrare gli agenti assegnati a una data area geografica");
    Console.WriteLine("3. Mostrare agenti con anni di servizio maggiori o uguali a una certa quantità");
    Console.WriteLine("4. Inserire un nuovo agente (se non presente nel db)");
    Console.WriteLine("0. Esci");

    Console.WriteLine("Scegli cosa fare: ");

    int scelta;
    while (!(int.TryParse(Console.ReadLine(), out scelta) && scelta >= 0 && scelta < 5))
        Console.WriteLine("Inserire un numero intero valido");
   
    switch (scelta)
    {
        case 0:
            //esci
            continua = false;
            break;
        case 1:
            //Mostrare tutti agenti
            VisualizzaAgenti();
            break;
        case 2:
            //Mostra agenti per area
            CercaAgentePerArea();
            break;
        case 3:
            //agenti con anni servizio >=tot
            CercaAgentiPiuAnziani();
            break;
        case 4:
            //inserire agente non presente
            AggiungiAgente();
            break;

    }


    
}

Agente CreaNuovoAgente()
{
    Console.WriteLine("Inserisci i dati del nuovo agente:");
    Console.Write("Nome: ");
    string nome = Console.ReadLine();   
    Console.Write("Cognome: ");
    string cognome = Console.ReadLine();
    Console.Write("Codice fiscale: ");
    string codiceFiscale = Console.ReadLine();
    Console.Write("Area geografica: ");
    string areaGeografica = Console.ReadLine();

    int annoInizioAttivita; ;
    Console.Write("Anno inizio attività: ");
    while (!int.TryParse(Console.ReadLine(), out annoInizioAttivita))
    {
        Console.WriteLine("Formato errato e/o codice isbn già presente. Riprova. Riprova");
    }

    //creo nuovo agente coi dati passati
    Agente nuovoAgente = new Agente(nome, cognome, codiceFiscale, areaGeografica, annoInizioAttivita);

    return nuovoAgente;
}

void CercaAgentiPiuAnziani()
{
    Console.WriteLine("Inserisci gli anni minimi di servizio degli agenti cercati:");

    int anniDiServizio;
    while (!int.TryParse(Console.ReadLine(), out anniDiServizio))
        Console.WriteLine("Digita un numero intero");

    //lista agenti 
    var agenti = repositoryAgenti.GetAll();

    //creo lista per agenti con anni>=tot
    List<Agente> agentiAnziani = new List<Agente>();

    foreach(var agent in agenti)
    {
        if(agent.CalcolaAnniDiServizio() >= anniDiServizio)
        {
            agentiAnziani.Add(agent);
        }
    }
    if (agentiAnziani.Count == 0)
        Console.WriteLine($"Non ci sono agenti con più di {anniDiServizio} anni di servizio ");
    else
    {
        foreach (var a in agentiAnziani)
            Console.WriteLine(a.ToString());
    }
}

void CercaAgentePerArea()
{
        Console.WriteLine("Inserisci l'area:");
        string areaCercata = Console.ReadLine();

        //cerco gli agenti assegnati a quell'area e li metto in una lista
        List<Agente> agenti = repositoryAgenti.GetByArea(areaCercata);

        Console.WriteLine("Ecco gli agenti assegnati all'area scelta:");

        //Stampo la lista degli agenti
        foreach (Agente item in agenti)
        {
            Console.WriteLine(item);
        }
    
}

void VisualizzaAgenti()
{
    Console.WriteLine("Elenco agenti in servizio:");

    //prelevo tutti gli agenti
    var agenti = repositoryAgenti.GetAll();

    //stampo i dati con ToString
    foreach (var agent in agenti)
        Console.WriteLine(agent);

}

void AggiungiAgente()
{
    VisualizzaAgenti();
    //creo un nuovo agente 
    Agente nuovoAgente = CreaNuovoAgente();

    //prendo la lista degli agenti
    var agenti = repositoryAgenti.GetAll();

    //verifico che non esista un altro agente con lo stesso codice fiscale
    foreach (var item in agenti)
    {
        if (item.CodiceFiscale == nuovoAgente.CodiceFiscale)
            Console.WriteLine("Agente già presente nel database");
        else
        {

            //aggiungo  verifico se l'aggiunta è andata a buon fine
            bool risultato = repositoryAgenti.Aggiungi(nuovoAgente);
            if (risultato)
            {
                Console.WriteLine("Agente aggiunto");
            }
            else
                Console.WriteLine("Operazione non riuscita.");
            break;
        }
    }
}