namespace Alkuaine_testi;

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Tervetuloa Alkuaine-testiin!");
            Console.WriteLine("------------------------------------------------------------------------------");
            Console.WriteLine("1. Aloita testi");
            Console.WriteLine("2. Tarkastele tuloksia");
            Console.WriteLine("3. Poistu testistä.");

            string? input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.Clear();
                    // Peli-metodi
                    break;

                case "2":
                    Console.Clear();
                    // Tulosten tarkastelu-metodi
                    break;

                case "3":
                    Console.Clear();
                    return;

                default:
                    Console.Clear();
                    Console.WriteLine("Virheellinen valinta, yritä uudelleen.");
                    break;

            }
        }

    }
}
