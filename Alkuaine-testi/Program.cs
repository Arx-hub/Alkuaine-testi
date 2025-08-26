using System.Linq;

namespace Alkuaine_testi;

class Program
{
    static void Main(string[] args)
    {
        SaveManager saveManager = new SaveManager();
        var lines = File.ReadAllLines("alkuaineet.txt"); // Luetaan alkuaineet tiedostosta taulukkoon
        List<string> elements = lines.ToList();          // Muutetaan taulukko listaksi

        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("Tervetuloa alkuainepeliin! Haluatko pelata (p) vai tarkastella tuloksia (t)?");
            string choice = Console.ReadLine()?.ToLower() ?? ""; // Kysytään käyttäjältä syöte, muutetaan se
                                                                 // pieniksi kirjaimiksi, mikäli syöte on
            if (string.IsNullOrEmpty(choice))                    // null, korvataan se tyhjällä merkkijonolla
            {
                Console.WriteLine("Valinta ei voi olla tyhjä.");
                return;
            }

            switch (choice)
            {
                case "p":
                    List<string> answers = new List<string>(); // Luodaan lista vastauksia varten
                    int correct = 0;                           // Laskuri oikeille vastauksille
                    int incorrect = 0;                         // Laskuri väärille vastauksille

                    Console.WriteLine("Syötä viisi ensimmäisestä 20 alkuaineesta yksi kerrallaan.");
                    for (int i = 0; i < 5; i++)
                    {
                        string answer = Console.ReadLine()?.ToLower() ?? "";
                        if (!answers.Contains(answer)) // Tarkastetaan että vastaus ei ole jo listassa
                        {
                            answers.Add(answer);
                        }
                        else
                        {
                            incorrect++;
                            return;
                        }
                        /* else
                        {
                            Console.WriteLine("Et voi antaa samaa vastausta useammin kuin kerran.");
                            i--;
                            return;
                        } */
                    }

                    foreach (string answer in answers) // Tarkastetaan vastaukset ja lisätään
                    {                                  // laskuriin oikeat ja väärät
                        if (elements.Contains(answer))
                        {
                            correct++;
                        }
                        else
                        {
                            incorrect++;
                        }
                    }

                    Console.WriteLine($"Sait {correct} oikein, ja {incorrect} väärin.");

                    // Tulosten tallentamisen koodi
                    saveManager.Save(correct, incorrect);

                    exit = true;
                    break;
                case "t": // Tulosten tarkastelun koodi
                    saveManager.Load();
                    exit = true;
                    break;
                default:
                    Console.WriteLine($"{choice} ei ole hyväksytty valinta, yritä uudestaan.");
                    break;
            }
        }
    }
}
