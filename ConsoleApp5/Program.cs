using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Security.AccessControl;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace ConsoleApp5
{
    class Program
    {
        class Menu
        {
            private string[] _term;
            public int SelectTerm { get; private set; }
            public ConsoleColor ForegroundColor = ConsoleColor.Blue;
            public ConsoleColor BackgroundColor = ConsoleColor.Black;
            public ConsoleColor SelectForegroundColor = ConsoleColor.Black;
            public ConsoleColor SelectBackgroundColor = ConsoleColor.Blue;

            public Menu(string[] term)
            {
                _term = term;
                SelectTerm = 0;
                ShowMenu();
            }

            public void SelectUp()
            {
                SelectTerm--;
                if (SelectTerm < 0)
                    SelectTerm = _term.Length - 1;
                ShowMenu();
            }

            public void SelectDown()
            {
                SelectTerm++;
                if (SelectTerm >= _term.Length)
                    SelectTerm = 0;
                ShowMenu();
            }
            public void HideMenu()
            {
                Console.Clear();
            }

            public void ShowMenu()
            {
                Console.Clear();
                Console.ForegroundColor = this.ForegroundColor;
                Console.BackgroundColor = this.BackgroundColor;

                for (int i = 0; i < _term.Length; i++)
                {
                    if (i == SelectTerm)
                    {
                        Console.ForegroundColor = this.SelectForegroundColor;
                        Console.BackgroundColor = this.SelectBackgroundColor;
                        Console.WriteLine(_term[i]);
                        Console.ForegroundColor = this.ForegroundColor;
                        Console.BackgroundColor = this.BackgroundColor;
                    }
                    else Console.WriteLine(_term[i]);

                }
            }
        }

        struct Country
        {
            public string Name, Capital, Continent;
            public uint People, Square;
        }

        static void Main(string[] args)
        {

            Note n = new Note();
            string way = Environment.CurrentDirectory;
  
            if (File.Exists(way + "\\password.txt") == false)
            {
                FileStream p = File.Create(way + "\\password.txt");
                p.Close();
            }

            bool next1 = false;
            next1 = n.Password(next1);

            if (next1 == false)
            {
                Environment.Exit(0);
            }

            if (File.Exists(way + "\\db.txt") == false)
            {
                FileStream f = File.Create(way + "\\db.txt");
                f.Close();
            }

            FileStream fstream = File.OpenRead(way + "\\db.txt");
            StreamReader reader = new StreamReader(fstream);
            string steam;
            int N = 0;

            while ((steam = reader.ReadLine()) != null)
            {
                N++;
            }
            n.Start(N);

            reader.Close();
            fstream.Close();

            Console.Title = "zagolovok";
        back1:
            Menu m = new Menu(new string[] { "Добавить запись в базу", "Просмотр Базы Данных", "Сортировать", "Изменить", "Удалить", "Допольнительные функции", "Изменить/Создать пароль", "Справка", "Выход" });
            ConsoleKeyInfo info;
            bool flag = true;
            do
            {
                info = Console.ReadKey();
                switch (info.Key)
                {
                    case ConsoleKey.DownArrow:
                        m.SelectDown();
                        break;
                    case ConsoleKey.UpArrow:
                        m.SelectUp();
                        break;
                    case ConsoleKey.Enter:
                        m.HideMenu();
                        switch (m.SelectTerm)
                        {
                            case 0:
                                n.FillMain(N);
                                N++;
                                break;
                            case 1:
                                n.Seen(N);
                                break;
                            case 2:
                                Menu s = new Menu(new string[] { "Сортировать по названию", "Сортировать по населению", "Сортировать по площади", "Назад" });
                                ConsoleKeyInfo info2;
                                bool flag2 = true;
                                do
                                {
                                    info2 = Console.ReadKey();
                                    switch (info2.Key)
                                    {
                                        case ConsoleKey.DownArrow:
                                            s.SelectDown();
                                            break;
                                        case ConsoleKey.UpArrow:
                                            s.SelectUp();
                                            break;
                                        case ConsoleKey.Enter:
                                            s.HideMenu();
                                            switch (s.SelectTerm)
                                            {
                                                case 0:
                                                    n.Sort(N);
                                                    n.Seen(N);
                                                    break;
                                                case 1:
                                                    n.PeopleSort(N);
                                                    n.Seen(N);
                                                    break;
                                                case 2:
                                                    n.SquareSort(N);
                                                    n.Seen(N);
                                                    break;
                                                case 3:
                                                    goto back1;
                                            }
                                            s.ShowMenu();
                                            break;
                                    }

                                } while (flag2);
                                break;
                            case 3:
                                bool next = false;
                                next = n.Password(next);
                                
                                   
                                if (next == true)
                                {
                                    Menu ch = new Menu(new string[] { "Изменить название", "Изменить столицу", "Изменить континент", "Изменить численность", "Изменить площадь", "Назад" });
                                    ConsoleKeyInfo infochange;
                                    bool flagchange = true;
                                    do
                                    {
                                        infochange = Console.ReadKey();
                                        switch (infochange.Key)
                                        {
                                            case ConsoleKey.DownArrow:
                                                ch.SelectDown();
                                                break;
                                            case ConsoleKey.UpArrow:
                                                ch.SelectUp();
                                                break;
                                            case ConsoleKey.Enter:
                                                ch.HideMenu();
                                                switch (ch.SelectTerm)
                                                {
                                                    case 0:
                                                        n.Seen(N);
                                                        int ChangePer = 0;
                                                        n.Change(N, ChangePer);
                                                        break;
                                                    case 1:
                                                        n.Seen(N);
                                                        ChangePer = 1;
                                                        n.Change(N, ChangePer);
                                                        break;
                                                    case 2:
                                                        n.Seen(N);
                                                        ChangePer = 2;
                                                        n.Change(N, ChangePer);
                                                        break;
                                                    case 3:
                                                        n.Seen(N);
                                                        ChangePer = 3;
                                                        n.Change(N, ChangePer);
                                                        break;
                                                    case 4:
                                                        n.Seen(N);
                                                        ChangePer = 4;
                                                        n.Change(N, ChangePer);
                                                        break;
                                                    case 5:
                                                        goto back1;
                                                }
                                                ch.ShowMenu();
                                                break;
                                        }

                                    } while (flagchange);
                                }
                                break;

                            case 4:
                                n.Seen(N);
                                n.Delete(N);
                                N--;
                                n.WriteFile(N);
                                n.Seen(N);
                                break;
                            case 5:
                                Menu m2 = new Menu(new string[] { "Удалить, если численность населения меньше заданого", "Поиск по названию столицы", "Поиск занимающей площади", "Назад" });
                                ConsoleKeyInfo info1;
                                bool flag1 = true;
                                do
                                {
                                    info1 = Console.ReadKey();
                                    switch (info1.Key)
                                    {
                                        case ConsoleKey.DownArrow:
                                            m2.SelectDown();
                                            break;
                                        case ConsoleKey.UpArrow:
                                            m2.SelectUp();
                                            break;
                                        case ConsoleKey.Enter:
                                            m2.HideMenu();
                                            switch (m2.SelectTerm)
                                            {
                                                case 0:
                                                    n.Seen(N);
                                                    Console.WriteLine("Введите численность населения");
                                                    uint DeleteWriter = uint.Parse(Console.ReadLine());
                                                    int J = n.newN(N, DeleteWriter);
                                                    n.DeleteBase(N, DeleteWriter, J);
                                                    N = J;
                                                    n.WriteFile(N);
                                                    n.Seen(N);
                                                    break;
                                                case 1:
                                                    n.Seen(N);
                                                    n.SearchCapital(N);
                                                    break;
                                                case 2:
                                                    n.Seen(N);
                                                    n.SearchSquare(N);
                                                    break;
                                                case 3:
                                                    goto back1;
                                            }
                                            m2.ShowMenu();
                                            break;
                                    }

                                } while (flag1);
                                break;

                            case 6:
                                n.CreatePassword();
                                break;
                            case 7:
                                string WayHelp = Environment.CurrentDirectory;
                                if (File.Exists(WayHelp + "\\Help.txt") == true)
                                {
                                    System.Diagnostics.Process.Start(WayHelp + "\\Help.txt");

                                }
                                break;
                            case 8:
                                Environment.Exit(0);
                                break;
                        }
                        m.ShowMenu();
                        break;

                }

            } while (flag);
        }
        class Note
        {
            
            private static int N = 100;
            Country[] count = new Country[N];
            public void Start(int N)
            {
                string way = Environment.CurrentDirectory;
                FileStream fstream = File.OpenRead(way + "\\db.txt");
                StreamReader reader = new StreamReader(fstream);
                string steam;
                int a, i, c;
                a = 0;
                i = 0;
                c = 0;
                while ((steam = reader.ReadLine()) != null)
                {
                    string temp;
                    temp = null;
                    string stroct;
                    stroct = steam;
                    for (int f = c; f < stroct.Length; f++)
                    {
                        if (stroct[f] == ':')
                        {
                            c = f + 1;

                            if (a == 0)
                            {
                                count[i].Name = temp;

                            }

                            if (a == 1)
                            {
                                count[i].Capital = temp;

                            }

                            if (a == 2)
                            {
                                count[i].Continent = temp;

                            }

                            if (a == 3)
                            {
                                count[i].People = uint.Parse(temp);

                            }

                            if (a == 4)
                            {
                                count[i].Square = uint.Parse(temp);
                                a = 0;
                                i++;
                                c = 0;
                            }
                            else
                            {
                                a++;
                            }
                            temp = null;
                        }
                        if (stroct[f] != ':')
                        {
                            temp += stroct[f];
                        }

                    }


                }
                reader.Close();
                fstream.Close();

            }

            public Country[] SearchCapital(int N)
            {
                string capital;
                Console.WriteLine("Введите столицу государства");
                capital = Console.ReadLine();
                for (int i = 0; i < N; i++)
                {
                    if (count[i].Capital == capital)
                    {
                        Console.WriteLine($" {i + 1}\t{count[i].Name}\t{count[i].Capital}\t{count[i].Continent}\t{count[i].People}\t{count[i].Square}");
                    }
                }
                Console.ReadLine();


                return count;
            }

            public Country[] SearchSquare(int N)
            {
                uint square;
                Console.WriteLine("Введите площадь государства");
                square = uint.Parse(Console.ReadLine());
                for (int i = 0; i < N; i++)
                {
                    if (count[i].Square == square)
                    {
                        Console.WriteLine($" {i + 1}\t{count[i].Name}\t{count[i].Capital}\t{count[i].Continent}\t{count[i].People}\t{count[i].Square}");
                    }
                }
                Console.ReadLine();

                return count;
            }

            public Country[] Sort(int N)
            {
                Country TempCountry;
                string stroct;
                string stroct1;
                int d = 0;

                for (int i = d; i < N; i++)
                {
                    stroct = count[i].Name;
                    for (int j = d; j < N; j++)
                    {
                        stroct1 = count[j].Name;
                        for (int h = 0; h < stroct.Length; h++)
                        {
                            if (stroct[h] > stroct1[h])
                            {
                                TempCountry = count[i];
                                count[i] = count[j];
                                count[j] = TempCountry;
                                stroct = count[i].Name;
                                break;
                            }

                            if (stroct[h] < stroct1[h])
                            {
                                break;
                            }
                        }
                    }
                    d = d + 1;
                }

                return count;
            }

            public Country[] PeopleSort(int N)
            {
                Country Temp;

                int num = 0;

                for (int i = 0; i < N; i++)
                {
                    for (int j = num; j < N; j++)
                    {
                        if (count[i].People < count[j].People)
                        {
                            Temp = count[i];
                            count[i] = count[j];
                            count[j] = Temp;
                        }
                    }
                    num++;
                }

                return count;
            }

            public Country[] SquareSort(int N)
            {
                Country Temp2;

                int square = 0;

                for (int i = 0; i < N; i++)
                {
                    for (int j = square; j < N; j++)
                    {
                        if (count[i].Square < count[j].Square)
                        {
                            Temp2 = count[i];
                            count[i] = count[j];
                            count[j] = Temp2;
                        }
                    }
                    square++;
                }


                return count;
            }

            public void Seen(int N)
            {
                Console.WriteLine("№ \tСтрана|Столица|Континент|Население|Площадь");
                for (int i = 0; i < N; i++)
                {
                    Console.WriteLine($" {i + 1}\t{count[i].Name}\t{count[i].Capital}\t{count[i].Continent}\t{count[i].People}\t{count[i].Square}");
                }
                Console.ReadLine();
            }

            public string EncriptedReadLine()
            {
                string password = "";
                ConsoleKeyInfo k;
                while ((k = Console.ReadKey(true)).Key != ConsoleKey.Enter)
                {
                    if (k.Key == ConsoleKey.Backspace)
                    {
                        if (password.Length > 0)
                        {
                            password = password.Remove(password.Length - 1);
                            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                            Console.Write(" ");
                            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                        }
                    }
                    else if (k.KeyChar != 0)
                    {
                        password += k.KeyChar;
                        Console.Write("*");
                    }
                }
                Console.WriteLine();
                return password;
            }

            public Country[] FillMain(int N)
            {
                count = Add(count, N);
                Console.WriteLine("Введите название государства");
                count[N].Name = Console.ReadLine();
                Console.WriteLine("Введите столицу государства");
                count[N].Capital = Console.ReadLine();
                Console.WriteLine("Введите континент государства");
                count[N].Continent = Console.ReadLine();

                string People = "";
                int intProv = 0;
                while (intProv == 0)
                {
                    Console.WriteLine("Введите население государтсва");
                    People = Console.ReadLine();
                    try
                    {
                        intProv = Convert.ToInt32(People);
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Введите число");
                    }


                }
                count[N].People = uint.Parse(People);


                string Square = "";
                intProv = 0;
                while (intProv == 0)
                {
                    Console.WriteLine("Введите площадь государства");
                    Square = Console.ReadLine();
                    try
                    {
                        intProv = Convert.ToInt32(Square);
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Введите число");
                    }


                }
                count[N].Square = uint.Parse(Square);
                N++;
                WriteFile(N);

                return count;
            }

            public void WriteFile(int N)
            {
                string way = Environment.CurrentDirectory;
                FileStream file = new FileStream(way + "\\db.txt", FileMode.Create);

                StreamWriter writer = new StreamWriter(file);

                for (int i = 0; i < N; i++)
                {
                    writer.Write("{0}:{1}:{2}:{3}:{4}:\n", count[i].Name, count[i].Capital, count[i].Continent, count[i].People, count[i].Square);
                }
                writer.Close();
                file.Close();

            }

            public Country[] Add(Country[] count, int N)
            {

                Country[] temp = new Country[N + 1];

                for (int i = 0; i < N; i++)
                {
                    temp[i] = count[i];
                }

                count = temp;

                return count;
            }

            public Country[] Change(int N, int ChangePer)
            {

                Console.WriteLine("Выберите номер строки");
                int i = int.Parse(Console.ReadLine()) - 1;

                Console.WriteLine($" {i + 1}\t{count[i].Name}\t{count[i].Capital}\t{count[i].Continent}\t{count[i].People}\t{count[i].Square}");

                switch (ChangePer)
                {
                    case 0:
                        Console.WriteLine("Введите новое название государства");
                        count[i].Name = Console.ReadLine();
                        break;
                    case 1:
                        Console.WriteLine("Введите новую столицу государства");
                        count[i].Capital = Console.ReadLine();
                        break;
                    case 2:
                        Console.WriteLine("Введите новый континент государтсва");
                        count[i].Continent = Console.ReadLine();
                        break;
                    case 3:
                        Console.WriteLine("Введите новое население государтсва");
                        count[i].People = uint.Parse(Console.ReadLine());
                        break;
                    case 4:
                        Console.WriteLine("Введите новую площадь государства");
                        count[i].Square = uint.Parse(Console.ReadLine());
                        break;
                }
                WriteFile(N);
                Console.WriteLine("\n");
                Seen(N);
                return count;
            }

            public Country[] Delete(int N)
            {
                Console.WriteLine("Выберите какую удалить строку");
                int i = int.Parse(Console.ReadLine()) - 1;
                Console.WriteLine($" {i + 1}\t{count[i].Name}\t{count[i].Capital}\t{count[i].Continent}\t{count[i].People}\t{count[i].Square}");
                Console.ReadLine();

                Country[] temp = new Country[N - 1];
                int k = 0;
                int DeleteStr = i;
                for (i = 0; i < N; i++)
                {
                    if (DeleteStr != i)
                    {
                        temp[k] = count[i];
                        k++;
                    }


                }

                count = temp;
                return count;
            }

            public Country[] DeleteBase(int N, uint DeleteWriter, int J)
            {
                Country[] temp = new Country[J];
                int p = 0;
                for (int i = 0; i < N; i++)
                {
                    if (DeleteWriter < count[i].People)
                    {
                        temp[p] = count[i];
                        p++;
                    }
                }
                count = temp;
                return count;
            }

            public int newN(int N, uint DeleteWriter)
            {
                int J = 0;
                for (int i = 0; i < N; i++)
                {
                    if (DeleteWriter < count[i].People)
                    {
                        J++;
                    }

                }
                return J;
            }

            public bool Password(bool next)
            {
                string way = Environment.CurrentDirectory;
                FileStream fstream = File.OpenRead(way + "\\password.txt");
                StreamReader readerProv = new StreamReader(fstream);
                string steam = readerProv.ReadLine();
                readerProv.Close();
                fstream.Close();
                

                if (steam != null)
                {
                    string password;
                    Console.WriteLine("Введите пароль");
                    password = EncriptedReadLine();
                    
                    FileStream FstreamPassword = new FileStream(way + "\\password.txt", FileMode.Open, FileAccess.Read);
                    DESCryptoServiceProvider cryptic = new DESCryptoServiceProvider();
                    cryptic.Key = ASCIIEncoding.ASCII.GetBytes("ABCDEFGH");
                    cryptic.IV = ASCIIEncoding.ASCII.GetBytes("ABCDEFGH");
                    CryptoStream crStream = new CryptoStream(FstreamPassword, cryptic.CreateDecryptor(), CryptoStreamMode.Read);
                    StreamReader reader1 = new StreamReader(crStream);
                    string PassSave = reader1.ReadToEnd();
                    reader1.Close();


                    for (int i = 0; i < 5; i++)
                    {
                        if (password != PassSave)
                        {
                            Console.WriteLine("Неверный пароль");
                            Console.WriteLine("Введите пароль");
                            password = EncriptedReadLine();

                        }
                        if (password == PassSave)
                        {
                            next = true;
                            break;
                        }
                    }

                    FstreamPassword.Close();
                }
                else
                {
                    next = true;
                }
                

                return next;
             
            }

            public void CreatePassword()
            {
                
                string Confirm;
                string Create;
                string Pass;
                string steam;

                string way = Environment.CurrentDirectory;
                FileStream fstream = File.OpenRead(way + "\\password.txt");
                StreamReader reader = new StreamReader(fstream);
                steam = reader.ReadLine();
                reader.Close();

               
                if (steam == null)
                {
                    Console.WriteLine("Введите новый пароль");
                    Confirm = EncriptedReadLine();

                    Console.WriteLine("Потвердите пароль");
                    Create = EncriptedReadLine();


                    if (Create != Confirm)
                    {
                        Console.WriteLine("Пароли не совпадают");
                        Console.ReadLine();
                    }
                    else
                    {
                        string srcStr = Create;
                        FileStream filepassword = new FileStream(way + "\\password.txt", FileMode.Create);
                        DESCryptoServiceProvider cryptic1 = new DESCryptoServiceProvider();

                        cryptic1.Key = ASCIIEncoding.ASCII.GetBytes("ABCDEFGH");
                        cryptic1.IV = ASCIIEncoding.ASCII.GetBytes("ABCDEFGH");
                        CryptoStream crStream1 = new CryptoStream(filepassword, cryptic1.CreateEncryptor(), CryptoStreamMode.Write);
                        byte[] data = ASCIIEncoding.ASCII.GetBytes(srcStr);
                        crStream1.Write(data, 0, data.Length);
                        crStream1.Close();
                        filepassword.Close();
                    }
                }
                else
                {
                    FileStream FstreamPassword = new FileStream(way + "\\password.txt", FileMode.Open, FileAccess.Read);
                    DESCryptoServiceProvider Fcryptic = new DESCryptoServiceProvider();
                    Fcryptic.Key = ASCIIEncoding.ASCII.GetBytes("ABCDEFGH");
                    Fcryptic.IV = ASCIIEncoding.ASCII.GetBytes("ABCDEFGH");
                    CryptoStream FcrStream = new CryptoStream(FstreamPassword, Fcryptic.CreateDecryptor(), CryptoStreamMode.Read);
                    StreamReader SavePass = new StreamReader(FcrStream);
                    Pass = SavePass.ReadToEnd();
                    SavePass.Close();

                    string Password;
                    Console.WriteLine("Введите пароль");
                    Password = EncriptedReadLine();

                    for (int i = 0; i < 3; i++)
                    {

                        if (Password != Pass)
                        {
                            Console.WriteLine("Неверный пароль");
                            Console.WriteLine("Введите пароль");
                            Password = EncriptedReadLine();
                        }

                        else
                        {
                            Console.WriteLine("Введите новый пароль");
                            string ChangePassword = EncriptedReadLine();
                            Console.WriteLine("Потведите пароль");
                            string Change = EncriptedReadLine();
                            
                           

                            if (Change != ChangePassword)
                            {
                                Console.WriteLine("Пароли не совпадают");
                                Console.ReadLine();
                                break;
                            }
                            else
                            {
                                string srcStr = Change;
                                FileStream changepassword = new FileStream(way + "\\password.txt", FileMode.Create);
                                DESCryptoServiceProvider Wcryptic = new DESCryptoServiceProvider();
                                Wcryptic.Key = ASCIIEncoding.ASCII.GetBytes("ABCDEFGH");
                                Wcryptic.IV = ASCIIEncoding.ASCII.GetBytes("ABCDEFGH");
                                CryptoStream WcrStream = new CryptoStream(changepassword, Wcryptic.CreateEncryptor(), CryptoStreamMode.Write);
                                byte[] data = ASCIIEncoding.ASCII.GetBytes(srcStr);
                                WcrStream.Write(data, 0, data.Length);
                                WcrStream.Close();
                                changepassword.Close();
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}
