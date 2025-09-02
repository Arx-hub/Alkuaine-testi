using System.Linq;

namespace Alkuaine_testi;

class Program
{
    static void Main(string[] args)
    {
        SaveManager saveManager = new SaveManager();
        var lines = File.ReadAllLines("alkuaineet.txt"); // Luetaan alkuaineet tiedostosta taulukkoon
        List<string> elements = lines.ToList();          // Muutetaan taulukko listaksi

        Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("******************************");
            Console.WriteLine("* Tervetuloa alkuainepeliin! *");
            Console.WriteLine("******************************");
            Console.ResetColor();
            Console.WriteLine("Paina 'enter' jatkaaksesi");
            Console.ReadLine();
            Console.Clear();


        bool exit = false;
        while (!exit)
        {
           
            
            Console.WriteLine("Haluatko pelata (p), tarkastella tuloksia (t), poistua pelistä (x)?");
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

                    Console.WriteLine("Valitse vaikeustaso: helppo (h), keksi (k), vaikea(v), extreme(e)");
                    string level = Console.ReadLine()?.ToLower() ?? "k"; //Select difficulty if no difficulty is selected keksi difficulty is chosen by default

                    

                    int questionsToAsk = level == "h" ? 3 : level == "k" ? 5 : level == "v" ? 10 : 20;//Diferent ranges depending on the selected difficutly 20 being the extreme difficutly
                    Console.WriteLine($"Syötä {questionsToAsk} ensimmäisestä 20 alkuaineesta yksi kerrallaan.");

                                             
                    // Console.WriteLine("Syötä viisi ensimmäisestä 20 alkuaineesta yksi kerrallaan.");
                    for (int i = 0; i < questionsToAsk; i++)
                    {
                        string answer = Console.ReadLine()?.ToLower() ?? "";
                        if (!answers.Contains(answer)) // Tarkastetaan että vastaus ei ole jo listassa
                        {
                            answers.Add(answer);
                        }
                        else
                        {
                            incorrect++;
                            continue;
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
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("*********************************");
                    Console.WriteLine($"* Sait {correct} oikein, ja {incorrect} väärin. *");
                    Console.WriteLine("*********************************");
                    Console.ResetColor();

                    // Tulosten tallentamisen koodi
                    saveManager.Save(correct, incorrect);

                    
                    break;

                case "t": // Tulosten tarkastelun koodi
                    {
                        // Kutsutaan Load-metodia, joka lukee kaikki tulokset ja laskee keskiarvon.
                        saveManager.Load();

                        break;
                    }

                case "x": // Poistutaan pelistä.
                    {
                        Console.Clear();

                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.Red;
                        
                        Console.WriteLine("***************************");
                        Console.WriteLine("* Kiitoksia pelaamisesta! *");
                        Console.WriteLine("***************************");
                        Console.ResetColor();
                        exit = true;
                        break;
                    }
                default:
                    Console.WriteLine($"{choice} ei ole hyväksytty valinta, yritä uudestaan.");
                    break;
            }
        }
    }
}
