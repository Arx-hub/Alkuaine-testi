using Newtonsoft.Json;

namespace Alkuaine_testi
{

    class SaveManager
    {
        private readonly string currentDirectory; // ohjelman sijainti
        private readonly string savedResultDirectory; // tuloskansioiden sijainti
        public SaveManager()
        {
            currentDirectory = Directory.GetCurrentDirectory();
            savedResultDirectory = Path.Combine(currentDirectory, "savedResults");
        }

        public void Save(double correct, double incorrect)
        {
            double total = correct + incorrect;
            double correctPercentage = (correct / total) * 100;
            string folderName = DateTime.Now.ToString("dd-MM-yyyy");
            bool doesFolderExist = Directory.Exists(Path.Combine(savedResultDirectory, folderName)); // onko tänään jo luotu kansio?

            if (!doesFolderExist)   // jos ei, luodaan kansio ja tallennetaan tulos uuteen Json-tiedostoon
            {
                Directory.CreateDirectory(Path.Combine(savedResultDirectory, folderName));
                string filePath = Path.Combine(savedResultDirectory, folderName, "tulokset.json");
                string json = JsonConvert.SerializeObject(new List<double> { correctPercentage }, Formatting.Indented);
                File.WriteAllText(filePath, json);
            }
            else    // jos on, puretaan tiedosto listaksi, lisätään tulos, ja kirjoitetaan uudestaan Jsoniksi
            {
                string fileContent = File.ReadAllText(Path.Combine(savedResultDirectory, folderName, "tulokset.json"));
                List<double> percentages = JsonConvert.DeserializeObject<List<double>>(fileContent);
                percentages.Add(correctPercentage);
                string updatedContent = JsonConvert.SerializeObject(percentages, Formatting.Indented);
                File.WriteAllText(Path.Combine(savedResultDirectory, folderName, "tulokset.json"), updatedContent);
            }
        }

        public void Load()
        {
            var saveFiles = FindSavedResults(savedResultDirectory); // haetaan olemassaolevat tulokset
            List<double> savedPercentages = new List<double>();
            double totalOfAllPercentages = 0;
            foreach (var file in saveFiles)
            {
                string content = File.ReadAllText(file);
                List<double> percentages = JsonConvert.DeserializeObject<List<double>>(content) ?? new List<double>();
                foreach (var percentage in percentages)
                {
                    savedPercentages.Add(percentage);
                    totalOfAllPercentages += percentage;
                }
            }
            if (savedPercentages.Count == 0) Console.WriteLine("Ei tallennettuja tuloksia!");
            else
            {
                double averageResult = totalOfAllPercentages / savedPercentages.Count();
                Console.WriteLine($"{savedPercentages.Count()} tallennettua tulosta löydetty!");
                Console.WriteLine($"Tallennettujen tulosten keskiarvo:{averageResult}%");
            }

        }

        IEnumerable<string> FindSavedResults(string folderName) // etsii tuloskansiosta kaikki Json-tiedostot ja palauttaa ne listana
        {
            List<string> saveFiles = new List<string>();
            var foundSaves = Directory.EnumerateFiles(folderName, "*", SearchOption.AllDirectories);
            foreach (var file in foundSaves)
            {
                if (Path.GetFileName(file) == "tulokset.json") saveFiles.Add(file);
            }
            return saveFiles;
        }
    }
}