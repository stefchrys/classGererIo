using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace testsLogs
{
    /// <summary>
    /// Classe qui gere ecriture de fichier , creation de fichier et dossier
    /// Doc: https://msdn.microsoft.com/fr-fr/library/8bh11f1k.aspx
    /// 
    /// </summary>
    class GererIO
    {
        
        /// <summary>
        /// Crée un dossier sur le disque entré en paramètre, avec un chemin en segond paramètre
        /// puis le nom du dossier, si le dossier existe déja un mesage d'erreur apparait.
        /// Si le disque n'existe pas un message d'erreur apparait.
        /// Si le chemin n'existe pas , il le crée.
        /// Exemple : pour créer le dosier "mondossier" dans c:\chem1\chem2
        /// string[] arr = {"chem1","chem2"};
        /// GererIO.creerDossier("c",arr,"mondossier");
        /// Exemple si on ne connait pas le lecteur à l'avance:
        ///  string lecteur = Path.GetPathRoot(Environment.SystemDirectory);
        ///  string[] chemin = {"111","21"};
        ///  GererIO.creerDossier(lecteur,chemin,"A5");
        /// </summary>
        /// <param name="disk">La lettre de du disque Exemple: "C" pour C:\</param>
        /// <param name="chemin">Suite du chemin dans un tableau Exemple:pour un chemin "ch1/ch2" le tableau = {"ch1","ch2"}, 
        /// si le chemin n'existe pas il sera créer</param>
        /// <param name="newFolder">nom du nouveau dossier a crée</param>
        /// <returns>Retourne vrai si l'opération c'est bien passée</returns>
        public static bool creerDossier(string disk,string[] chemin,string newFolder)
        {
            string racine="";
            //verification des carractères dans les parametres
            bool folderValid = verifierChar(newFolder);
            foreach (string el in chemin)
            {
                bool test = verifierChar(el);
                if (test == false)
                {
                    folderValid = false;
                }
            }
            //creation du chemin avec les elments du tableau
            foreach(string el in chemin)
            {                
                racine = racine + @"\" + el;
            }
            //Si chemin mal formaté on stop.
            if (!folderValid)
            {
                Console.WriteLine("caracttères interdits");
                Console.ReadLine();
                return false;
            }
            else//si l'adresse est correctement formatée on execute la suite
            {
                //si le disque existe
                if (System.IO.Directory.Exists(disk + @":\"))
                {
                    //creation du chemin complet
                    racine = disk + @":\" + racine;
                    string folderPath = System.IO.Path.Combine(racine, newFolder);
                    //si le dossier a crée n'existe pas on le crée
                    if (!System.IO.Directory.Exists(folderPath))
                    {
                        System.IO.Directory.CreateDirectory(folderPath);
                        return true;
                    }
                    else// sinon erreur
                    {
                        Console.WriteLine("Folder \"{0}\" already exists.", folderPath);
                        Console.ReadLine();
                        return false;
                    }
                }
                else //sinon erreur
                {
                    Console.WriteLine("Le disque \"{0}\" n'existe pas.", disk);
                    Console.ReadLine();
                    return false;
                }
            }
        }
        /// <summary>
        /// Creer un doosier dans le dossier courant.
        /// surcharge.
        /// </summary>
        /// <param name="newFolder"></param>
        /// <returns>true si reussite</returns>
        public static bool creerDossier(string newFolder)
        {
            try
            {
                //calcul le chemin courant
                string currentPath = Path.GetDirectoryName(Environment.CurrentDirectory);
                //combine le nouveau dosier
                string folderPath = System.IO.Path.Combine(currentPath, newFolder);
                //et crée le dossier
                System.IO.Directory.CreateDirectory(folderPath);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                Console.ReadLine();
                return false;
            }
        }

        /// <summary>
        /// Verifie les carracteres du parametre, retourne false
        /// si les carractères interdits sont trouvés
        /// </summary>
        /// <param name="newFolder">Chaine de carractère à verifier</param>
        /// <returns>BOOL</returns>
        public static bool verifierChar(string newFolder)
        {
            bool folderValid = true;
            string goodChar = "[a-z,A-Z,0-9]";//cachemintères autorisés
            //verifier la chaine
            int i = newFolder.Length;
            for (int k = 0; k < i; k++)
            {
                if (!Regex.IsMatch(Convert.ToString(newFolder.ElementAt(k)), goodChar))
                {
                    folderValid = false;
                }
            }
            return folderValid;
        }

        /// <summary>
        /// Crée un fichier dans dans un repertoire donné en paramètre
        /// Exemple: pour créer un fichier dans le repertoire courant:
        /// string lecteur = Path.GetDirectoryName(Environment.CurrentDirectory);
        /// GererIO.creerFichier(lecteur,"test.txt");
        /// </summary>
        /// <param name="pathString">Chemin cible</param>
        /// <param name="fichier">nouveau fichier</param>
        /// <returns>Vrai si bien fonctionné</returns>
        public static bool creerFichier(string pathString,string fichier)
        {
            //creer le chemin du fichier (chemin/fichier)
            pathString = System.IO.Path.Combine(pathString, fichier);
            if (!System.IO.File.Exists(pathString))
            {
                try
                {
                    System.IO.File.Create(pathString);
                    return true;
                }catch(Exception e){
                    Console.WriteLine("{0} Exception caught.", e);
                    Console.ReadLine();
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Fichier déjà existant");
                Console.ReadLine();
                return false;
            }         
        }

        /// <summary>
        /// Ajoute,remplace une ligne de texte dans un fichier sans effacer le contenu
        /// Exemple:
        /// string lecteur = Path.GetDirectoryName(Environment.CurrentDirectory);
        /// GererIO.ajouterTexte(lecteur,"text.txt","ajouter","hello");
        /// </summary>
        /// <param name="pathString">chemin du fichier</param>
        /// <param name="fichier">nom du fichier</param>
        /// <param name="texte">texte a ecrire</param>
        /// <param name="choix">Choix de l'action:
        /// remplacerTexte: remplace tout le texte du fichier
        /// ajouterLigne : ajoute une ligne dans le fichier
        /// remplacerLignesTableau : remplace le contenu d'un fichier par  les ligne d'un tableau de string
        /// </param>
        /// <returns>vrai si cela c'est bien passé</returns>
        public static bool ecrireTexte(string pathString, string fichier,string choix,string texte=null,string[] arr=null)
        {           
            //creer le chemin du fichier (chemin/fichier)
            pathString = System.IO.Path.Combine(pathString, fichier); 
            try
            {
                bool ctrl = true;
                switch (choix)
                {
                    case "remplacerTexte":
                        System.IO.File.WriteAllText(pathString, texte); //remplace le contenu du fichier                       
                        break;
                    case "ajouterLigne":
                        using (System.IO.StreamWriter file = new System.IO.StreamWriter(pathString, true))
                        {
                            file.WriteLine(texte);//ajoute une ligne sans rien effacer                          
                        }
                        break;
                    case "remplacerLignesTableau"://remplace le contenu par les lignes du tableau
                        System.IO.File.WriteAllLines(pathString, arr);
                        break;
                    default:
                        Console.WriteLine("Action impossibles choix=ajouter ou remplacer");
                        Console.ReadLine();
                        ctrl = false;
                        break;
                }
                return ctrl;                
            }catch(Exception e){
                Console.WriteLine("{0} Exception caught.", e);
                Console.ReadLine();
                return false;
            }          
        }       
    }
}
