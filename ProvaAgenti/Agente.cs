using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvaAgenti
{
    //figlio
    internal class Agente : Persona
    {
        public string? AreaGeografica { get; set; }
        public int AnnoDiInizioAttivita { get; set; }



        //costruttore overloaded
        //eredita d apersona
        
        public Agente(string nome, string cognome, string codiceFiscale, string areaGeografica, int annoInizioAttivita):base(nome,cognome,codiceFiscale)
        {
            AreaGeografica = areaGeografica;
            AnnoDiInizioAttivita = annoInizioAttivita;
       
        }

        //metodo per calcolare anni di servizio
        public int CalcolaAnniDiServizio()
        {
            
            int anniDiServizio =  DateTime.Now.Year - AnnoDiInizioAttivita;
            return anniDiServizio;

        }


        public override string ToString()
        {
            return base.ToString() + $" - Anni di servizio: {CalcolaAnniDiServizio()}";
        }
    }
}
