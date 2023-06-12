using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Role_Management
{
    public class EvenOdd
    {

        // Back to Menu
        public void BackToMenu()
        {
            Console.Write("\n[i] TEKAN ENTER UNTUK KEMBALI KE MENU");
            Console.ReadLine();
            Menu();
        }
        // Menu
       
        public void Menu()
        {
            Console.Clear();
            Console.WriteLine(" ");
            Console.WriteLine("==============================");
            Console.WriteLine("      MENU GANJIL GENAP");
            Console.WriteLine("------------------------------");
            Console.WriteLine("1. Cek Ganjil/Genap");
            Console.WriteLine("2. Print Ganjil/Genap (dengan limit)");
            Console.WriteLine("3. Logout");
            Console.WriteLine("4. Exit");
            Console.WriteLine("------------------------------");
            Console.WriteLine(" ");

            Console.Write("Pilihan: ");
            string menu = Console.ReadLine();

            if (!string.IsNullOrEmpty(menu) && int.TryParse(menu, out int num))
            {
                switch (Convert.ToInt32(menu))
                {
                    case 1:
                        EvenOddCheckMenu();
                        break;

                    case 2:
                        PrintEvenOddMenu();
                        break;

                    case 3:
                        Program.Login();
                        break;

                    case 4:
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Pilihan Menu Tidak Valid!");
                        BackToMenu();
                        break;
                }
            }
            else
            {
                Console.WriteLine("Pilihan Menu Tidak Valid!");
                BackToMenu();
            }
        }

        // Print Even or Odd Number
        public void PrintEvenOddMenu()
        {
            Console.Write("Pilih (Ganjil/Genap): ");
            string type = Console.ReadLine().ToLower();


            if (!string.IsNullOrEmpty(type) && (type == "ganjil" || type == "genap"))
            {

                Console.Write("Masukkan Limit: ");
                string limit = Console.ReadLine();
                if (!string.IsNullOrEmpty(limit) && int.TryParse(limit, out int num))
                {
                    if (Convert.ToInt32(limit) > 0)
                    {
                        PrintEvenOdd(Convert.ToInt32(limit), type);
                    }
                    else
                    {
                        Console.Write("Limit harus lebih dari 0 (Nol)");
                        BackToMenu();
                    }
                }
                else
                {
                    Console.Write("Limit harus diisi dan berupa angka");
                    BackToMenu();
                }
            }
            else
            {
                Console.Write("Harap pilih Ganjil atau Genap!");
                BackToMenu();
            }


        }

        public void PrintEvenOdd(int limit, string choice)
        {
            string result = "";
            int modulus = 0;
            if (choice.ToLower() == "ganjil")
            {
                modulus = 1;
            }
            else if (choice.ToLower() == "genap")
            {
                modulus = 0;
            }
            else
            {
                Console.WriteLine("Harap pilih Ganjil atau Genap!");
                BackToMenu();
            }

            for (int i = 1; i <= limit; i++)
            {
                if (i % 2 == modulus)
                {
                    result += i.ToString();
                    if (i != limit)
                    {
                        result += ", ";
                    }
                }
            }

            Console.WriteLine("Print bilangan 1 - " + limit + ": ");
            Console.WriteLine(result);
            BackToMenu();
        }

        // Check Even or Odd Number
        public void EvenOddCheckMenu()
        {
            Console.Write("Masukkan bilangan yang ingin dicek: ");
            string number = Console.ReadLine();
            if (!string.IsNullOrEmpty(number) && int.TryParse(number, out int num))
            {
                if (Convert.ToInt32(number) > 0)
                {
                    string result = EvenOddCheck(Convert.ToInt32(number));
                    Console.WriteLine(result);
                    BackToMenu();
                }
                else
                {
                    Console.WriteLine("Angka harus lebih dari 0 (Nol)");
                    BackToMenu();
                }
            }
            else
            {
                Console.Write("Masukan Bukan Angka!");
                BackToMenu();
            }
        }
        public string EvenOddCheck(int input)
        {
            string result = "Ganjil";
            if (input % 2 == 0)
            {
                result = "Genap";
            }
            return result;
        }
    }
}
