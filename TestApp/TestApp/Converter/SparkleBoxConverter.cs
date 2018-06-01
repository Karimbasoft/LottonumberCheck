using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestApp.Business;

namespace TestApp.Converter
{
    public class SparkleBoxConverter
    {
        public static int[] ConvertSparkleBoxToIntArray(SparkleBox tempSparkel)
        {
            int[] ia = tempSparkel.SparkleBoxNumbers.Split(' ').Select(n => Convert.ToInt32(n)).ToArray();
            return ia;
        }

        public static List<int> ConvertSparkleBoxToIntList(SparkleBox tempSparkel)
        {
            List<int> tempListOfNumbers = new List<int>();
            int[] ia = tempSparkel.SparkleBoxNumbers.Split(' ').Select(n => Convert.ToInt32(n)).ToArray();

            foreach (var item in ia)
            {
                tempListOfNumbers.Add(item);
            }

            return tempListOfNumbers;
        }
    }
}
