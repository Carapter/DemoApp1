using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DemoApp.Helpers
{
    public class ValidationHelper
    {
        /// <summary>
        /// Проверка ИНН
        /// </summary>
        /// <param name="inn">ИНН</param>
        /// <returns></returns>
        public static bool IsValidInn(string? inn)
        {
            if (string.IsNullOrWhiteSpace(inn)) return false;
            return Regex.IsMatch(inn, @"^\d{10}$|^\d{12}$");
        }

        public static bool IsValidEmail(string? email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        public static bool IsValidPhoneNumber(string? phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) return false;
            return Regex.IsMatch(phone, @"^\+?[78]\s?\(?\d{3}\)?\s?\d{3}[-\s]?\d{2}[-\s]?\d{2}$");
        }

        public static bool IsValidRating(int? rating)
        {
            return rating is >= 0 and <= 100;
        }
    }

}