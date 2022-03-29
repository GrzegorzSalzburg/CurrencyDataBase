using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CurrencyDataBase
{
    internal class Program
    {

        static void Main(string[] args)
        {
            while (true)
            {
                Task.Delay(1000).Wait();
                string letter = "";
                Console.WriteLine("\nCzy chcesz wprowadzić datę? (t/n)");
                string option = Console.ReadLine();
                if (option == "t" || option == "T")
                {
                    Console.WriteLine("\nWprowadz date w formacie yyyy-mm-dd: ");
                    string date = Console.ReadLine();
                    Console.WriteLine("\nWybierz opcje\n  1.Wyswietl wszystkie dane\n  2.Wyswietl po literze\n  3.Exit\n");
                    int decision = Convert.ToInt32(Console.ReadLine());
                    if (decision == 2 || decision == 1)
                    {
                        if (decision == 2)
                        {
                            Console.WriteLine("\nWprowadz pierwsza litere waluty: ");
                            letter = Console.ReadLine().ToUpper();
                        }
                        Load(date, decision, letter);
                    }
                    else
                    {
                        break;
                    }
                }
                else break;


            }
            Console.WriteLine("Koniec");
            Console.ReadLine();
        }


        /*
        public static async void Load()
        {
            string call = "http://radoslaw.idzikowski.staff.iiar.pwr.wroc.pl/instruction/students.json";
            HttpClient client = new HttpClient();
            string response = await client.GetStringAsync(call);
            Console.WriteLine(response);
            List<Student> students = JsonConvert.DeserializeObject<List<Student>>(response);
            foreach (var s in students)
            {
                Console.WriteLine(s.studentID + "\t" + s.studentName);
            }
        }
        */


        public static async void Load(string date, int decision, string letter)
        {
            string call = $"https://openexchangerates.org/api/historical/{date}.json?app_id=5389851f961749ce881d4ceaea659942";
            HttpClient client = new HttpClient();
            string response = await client.GetStringAsync(call);
            //Console.WriteLine(response);
            var result = JsonConvert.DeserializeObject<Currency>(response);
            var context = new Currency_values();
            var counter = 0;
            if (decision == 1)
            {
                foreach (var item in result.Rates)
                {
                    context.Currencies.Add(new Currency_Base { Index = counter, currencyName = item.Key, rate = item.Value });
                    context.SaveChanges();
                    counter++;
                }
                var students = (from s in context.Currencies select s).ToList<Currency_Base>();
                foreach (var st in students)
                {
                    Console.WriteLine("ID: {0}, Currency: {1}, Rate: {2}", st.Index, st.currencyName, st.rate);
                    var remove = context.Currencies.First(x => x.ID == st.ID);
                    context.Currencies.Remove(remove);
                    context.SaveChanges();
                }
            }
            else if (decision == 2)
            {
                foreach (var item in result.Rates)
                {
                    if (item.Key.StartsWith(letter))
                    {
                        context.Currencies.Add(new Currency_Base { Index = counter, currencyName = item.Key, rate = item.Value });
                        context.SaveChanges();
                        counter++;
                    }
                }
                var students = (from s in context.Currencies where s.currencyName.StartsWith(letter) select s).ToList<Currency_Base>();
                foreach (var st in students)
                {
                    Console.WriteLine("ID: {0}, Currency: {1}, Rate: {2}", st.Index, st.currencyName, st.rate);
                    var remove = context.Currencies.First(x => x.ID == st.ID);
                    context.Currencies.Remove(remove);
                    context.SaveChanges();
                }
            }
            else
            {
                System.Environment.Exit(0);
            }
        }
    }
}
