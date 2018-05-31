using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace TestApp.Business
{
    public class SparkleBoxMocker : SerializeObject
    {
        #region Propertys
        public ObservableCollection<SparkleBox> UserNumbers { get; set; }
        #endregion

        public void DeleteEntryFromSparkleBox(string numbers)
        {
            SparkleBox sparkle = UserNumbers.FirstOrDefault<SparkleBox>(s => s.SparkleBoxNumbers.Equals(numbers));

            if (UserNumbers.Contains(sparkle))
            {
                UserNumbers.Remove(sparkle);
                SerializeSparkleBox(UserNumbers);
            }
        }

        public void DeleteEntryFromSparkleBox(int id)
        {
            if (id <= UserNumbers.Count)
            {
                UserNumbers.RemoveAt(id);
                SerializeSparkleBox(UserNumbers);
            }
        }

        public void AddEntryToSparkleBoxCollection(string numbers)
        {
            if (UserNumbers.Count < 12)
            {
                UserNumbers.Add(new SparkleBox(numbers.Split(' ').Select(n => Convert.ToInt32(n)).ToArray()));
                SerializeSparkleBox(UserNumbers);
            }
        }

        public void AddEntryToSparkleBoxCollection(SparkleBox sparkleBox)
        {
            if (UserNumbers.Count < 12)
            {
                UserNumbers.Add(sparkleBox);
                SerializeSparkleBox(UserNumbers);
            }
        }
    }
}
