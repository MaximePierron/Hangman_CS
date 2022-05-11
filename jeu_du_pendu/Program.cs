using System;
using System.Collections.Generic;
using System.IO;
using jeu_du_pendu;

namespace jeu_du_pendu
{
    class Program
    {
        static void AfficherMot(string mot, List<char> lettres)
        {
            int taille_mot = mot.Length;
            for(int i = 0; i < taille_mot; i++)
            {
                if (lettres.Contains(mot[i])){
                    Console.Write(mot[i] + " ");
                }
                else
                {
                    Console.Write("_ ");
                
                }
                
            }
            Console.WriteLine();
        }

        static bool motDevine(string mot, List<char> lettres)
        {
            foreach(char lettre in lettres)
            {
                mot = mot.Replace(lettre.ToString(), "");
            }
            if (mot == "")
            {
                return true;
            }
            return false;
        }
        static char DemandeUneLettre()
        {
            Console.WriteLine("Proposez une lettre");
            string lettre = Console.ReadLine();
            while (true)
            {
                try
                {
                    char lettre_char = char.Parse(lettre);
                    return char.ToUpper(lettre_char);
                }
                catch
                {
                    Console.WriteLine("Veuillez rentrer un seul caractère");
                    return DemandeUneLettre();
                }
            }

        }
        static void DevinerMot(string mot)
        {
            List<char> lettres = new List<char>();
            List<char> lettresExclues = new List<char>();
            const int NB_VIES = 6;
            int vies = NB_VIES;
            while (vies>0)
            {
                Console.WriteLine(Ascii.Pendu[NB_VIES - vies]);
                AfficherMot(mot, lettres);
                char lettre = DemandeUneLettre();
                Console.Clear();
                if (mot.Contains(lettre))
                {
                    if (!lettres.Contains(lettre))
                    {
                        Console.WriteLine("Cette lettre est présente dans le mot.");
                        lettres.Add(lettre);
                        if (motDevine(mot, lettres)) break;
                    } else
                    {
                        Console.WriteLine("Vous avez déjà proposé cette lettre !");
                    }
                } else
                {
                    if (!lettresExclues.Contains(lettre))
                    {
                        lettresExclues.Add(lettre);
                        vies--;
                    }
                    else
                    {
                        Console.WriteLine("Vous avez déjà proposé cette lettre !");
                    }
                }
                Console.WriteLine($"Il vous reste {vies} " + ((vies == 1) ? "vie" : "vies"));
                Console.WriteLine(((lettresExclues.Count !=0) ? "Le mot ne contient pas " + ((lettresExclues.Count == 1) ? "la lettre : " : "les lettres : ") + String.Join(",", lettresExclues): ""));
            }
            Console.WriteLine(Ascii.Pendu[NB_VIES - vies]);
            if (vies == 0)
            {
                Console.WriteLine("Vous avez perdu ! le mot était " + mot);
            } else
            {
                Console.WriteLine("Bravo, vous avez trouvé !");
            }
            
        }

        static string[] ChargerLesMots(string nomFichier)
        {
            try
            {
                return File.ReadAllLines(nomFichier);
            } catch (Exception ex)
            {
                Console.WriteLine("Erreur de lecture du fichier : " + nomFichier + ex.Message);
            }
            return null;
        }

        static void Main(string[] args)
        {
            Random rnd = new Random();
            var mots = ChargerLesMots("mots.txt");
            int indexMot = rnd.Next(mots.Length);
            if(mots != null)
            {
                do
                {
                    Console.Clear();
                    string mot = mots[indexMot].Trim().ToUpper();
                    DevinerMot(mot);
                    Console.WriteLine("Voulez-vous rejouer ? O ou N");
                } while(Console.ReadLine().ToLower() == "o");
                Console.WriteLine("Merci d'avoir joué !");

            }
            else
            {
                Console.WriteLine("La liste de mots est vide !");
            }

        }
    }
}
