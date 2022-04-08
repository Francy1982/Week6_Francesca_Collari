using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvaAgenti
{
    //Padre
    internal abstract class Persona
    {
        public string Nome { get; set; }    
        public string Cognome { get; set; }
        public string CodiceFiscale { get; set; }


        public Persona(string nome, string cognome, string codiceFiscale)
        {
            Nome = nome;
            Cognome = cognome;
            CodiceFiscale = codiceFiscale;

        }

        public override string ToString()
        {
            return $"CF: {CodiceFiscale} - Nome: {Nome} - Cognome: {Cognome}";
        }
    }
}
