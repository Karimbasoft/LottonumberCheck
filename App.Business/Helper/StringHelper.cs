﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace App.Business.Helper
{
    public static class StringHelper
    {
        public static bool CheckIfSubstringContainsEscapeSymbol(this string data)
        {
            return data.Contains("\n") ? false : true;
        }

        public static string RemoveWhitespace(this string data)
        {
            if (CheckIfSubstringContainsEscapeSymbol(data))
                data = data.Replace("\n", " ");

            return new string(data.ToCharArray()
                .Where(c => !char.IsWhiteSpace(c))
                .ToArray());
        }

        /// <summary>
        /// Enfernt leere Array-Einträge
        /// </summary>
        /// <param name="toConvertArry"></param>
        /// <returns></returns>
        public static string[] RemoveWhiteSpaceFromStringArray(this string[] toConvertArry)
        {
            foreach (var item in toConvertArry)
            {
                if (string.IsNullOrWhiteSpace(item))
                    toConvertArry = toConvertArry.Where(val => val != item).ToArray();
            }
            return toConvertArry;
        }
    }
}
