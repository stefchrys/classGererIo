using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace testsLogs
{
    class Program
    {
        static void Main(string[] args)
        {
            //exemple 1:
            
            string lecteur = Path.GetDirectoryName(Environment.CurrentDirectory);
            string[] lines = { "First ", "Second ", "Third " };
            GererIO.ecrireTexte(lecteur,"text.txt","remplacerLignesTableau",null,lines);
            foreach(string el in lines ){
            GererIO.ecrireTexte(lecteur, "text.txt", "ajouterLigne",el, null);
            }
            
        }
    }
}
