using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentificationNumberLibrary
{
    public class NumberClass
    {
        /// <summary>
        /// метод формирует уникальный идентификационный номер каждому человеку, родившемуся в Румынии
        /// </summary>
        /// <param name="surname">Фамилия</param>
        /// <param name="gender">Пол</param>
        /// <param name="dateBirthday">Дата рождения</param>
        /// <param name="residentValue">Является ли резидентом</param>
        /// <param name="townCode">Код города</param>
        /// <returns></returns>
        public static string GetRomaniaNumber(string surname, string gender, DateTime dateBirthday, bool residentValue, string townName)
        {
            string identificationNumber = "";
            //формирование первой цифры кода
            int year = dateBirthday.Year;
            if (year >= 1900 & year <=1949)
            {
                identificationNumber = identificationNumber + "1";
            }
            if (year >= 1950 & year <= 1999)
            {
                identificationNumber = identificationNumber + "2";
            }
            if (year >= 1800 & year <= 1849)
            {
                identificationNumber = identificationNumber + "3";
            }
            if (year >= 1850 & year <= 1899)
            {
                identificationNumber = identificationNumber + "4";
            }
            if (year >= 2000 & year <= 2049)
            {
                identificationNumber = identificationNumber + "5";
            }
            if (year >= 2050 & year <= 2099)
            {
                identificationNumber = identificationNumber + "6";
            }
            //если человек не резидент Румынии, ставится рандомное число от 7 до 9
            if (residentValue == false)
            {
                Random rnd = new Random();
                int value = rnd.Next(7, 10);
                identificationNumber = identificationNumber + value;
            }
            //Следующие 2 цифры - последние 2 цифры года рождения
            string yearString = Convert.ToString(year);
            identificationNumber = identificationNumber + yearString.Substring(2, 2);
            //Следующие 2 цифры - месяц рождения
            int month = dateBirthday.Month;
            string monthString = Convert.ToString(month);
            if (month < 10)
            {
                monthString = "0" + monthString;
            }
            identificationNumber = identificationNumber + monthString;
            //Следующие 2 цифры - дата рождения
            int day = dateBirthday.Day;
            string dayString = Convert.ToString(day);
            if (day<10)
            {
                dayString = "0" + dayString;
            }
            identificationNumber = identificationNumber + dayString;
            //Следующие 2 цифры - код города
            identificationNumber = identificationNumber + Convert.ToString(GetTownCode(townName));
            //Следующая цифра - пол человека
            identificationNumber = identificationNumber + gender;
            //Следующие 2 цифры - код из таблицы ASCII для первой буквы фамилии человека, введенной латинскими буквами
            //identificationNumber = identificationNumber + Convert.ToString(Convert.ToInt32(surname.Substring(0,1)));
            Console.WriteLine(Convert.ToString(Convert.ToInt32(surname.Substring(0, 1))));

            return identificationNumber;
        }
        /// <summary>
        /// метод находит код выбранного города
        /// </summary>
        /// <param name="townName">выбранный город</param>
        /// <returns>код города</returns>
        public static int GetTownCode(string townName)
        {
            int townCode = 1;
            string path = @"../../../Roman.csv";
            string[] readText = File.ReadAllLines(path);
            foreach (string s in readText)
            {
                string[] arrayText = s.Split(';');
                if (arrayText[2] == townName)
                {
                    townCode = Convert.ToInt32(arrayText[0]);
                }
            }
            return townCode;
        }
    }
}
