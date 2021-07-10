using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
namespace С2000М
{
    class Program
    {
        static void Main(string[] args)
        {
            String ФайлЧтение;
            int СмещениеРазделов;
            string ФайлЗапись;

            // Проверка аргументов

            if (args.Length == 0)
            {
                Console.WriteLine("Не верно указаны данные. Путь до файла, множитель, Конечный путь");
            }
            else if (File.Exists(args[0]) == false)
            {
                Console.WriteLine("Не найден исходный файл");
            }
            else
            {

                ФайлЧтение = args[0];
                СмещениеРазделов = Convert.ToInt32(args[1]);
                ФайлЗапись = args[2];

                // Регистрация Кодировки WIN1251
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                // Загрузка файла для работы
                List<string> СчитанныйФайл = new List<string>(File.ReadAllLines(args[0], System.Text.Encoding.GetEncoding(1251)));
                for (int Счётчик1 = 0; Счётчик1 <= СчитанныйФайл.Count - 1; Счётчик1++)
                {
                    if (СчитанныйФайл[Счётчик1].IndexOf("Раздел: ") != -1 & СчитанныйФайл[Счётчик1].Split("Нет").Length == 1)
                    {
                        int КонечныйНомерРаздела = System.Convert.ToInt32(СчитанныйФайл[Счётчик1].Split("Раздел: ")[1].Split(',')[0]) + (СмещениеРазделов);
                        if (СчитанныйФайл[Счётчик1].Split("Раздел: ")[1].Split(',').Length >= 2)
                        {
                            СчитанныйФайл[Счётчик1] = СчитанныйФайл[Счётчик1].Split("Раздел: ")[0] + "Раздел: " + КонечныйНомерРаздела + ',' + СчитанныйФайл[Счётчик1].Split("Раздел: ")[1].Split(',')[1];
                        }
                        else
                        {
                            СчитанныйФайл[Счётчик1] = СчитанныйФайл[Счётчик1].Split("Раздел: ")[0] + "Раздел: " + КонечныйНомерРаздела;
                        }
                    }
                }
                File.WriteAllLines(ФайлЗапись, СчитанныйФайл, System.Text.Encoding.GetEncoding(1251));
            }
        }
    }
}
