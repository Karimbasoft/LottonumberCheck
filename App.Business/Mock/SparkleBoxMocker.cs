using App.Business.LotteryTicket;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace App.Business
{
    public class SparkleBoxMocker : SerializeObject
    {
        #region Fields
        private readonly User _user;
        #endregion

        #region Propertys
        public ObservableCollection<SparkleBox> UserNumbers => _user.UserNumbers;
        #endregion

        public SparkleBoxMocker(User user)
        {
            _user = user;
        }

        public void DeleteEntryFromSparkleBox(string numbers)
        {
            SparkleBox sparkle = UserNumbers.FirstOrDefault<SparkleBox>(s => s.SparkleBoxNumbers.Equals(numbers));

            if (UserNumbers.Contains(sparkle))
            {
                UserNumbers.Remove(sparkle);
                Save();
            }
        }

        public void DeleteEntryFromSparkleBox(int id)
        {
            if (id <= UserNumbers.Count)
            {
                UserNumbers.RemoveAt(id);
                Save();
            }
        }

        public void ClearSparkleBox(SparkleBox sparkleBox)
        {
            if (UserNumbers.Any(x => x.SparkleBoxNumbers.Equals(sparkleBox.SparkleBoxNumbers)))
            {
                UserNumbers.Clear();
                Save();
            }
        }

        public void AddEntryToSparkleBoxCollection(string numbers)
        {
            if (UserNumbers.Count < 12)
            {
                UserNumbers.Add(new SparkleBox(numbers.Split(' ').Select(n => Convert.ToInt32(n)).OrderBy(i => i).ToArray()));
                Save();
            }
        }

        public void AddEntryToSparkleBoxCollection(SparkleBox sparkleBox)
        {
            if (UserNumbers.Count < 12)
            {
                UserNumbers.Add(sparkleBox);
                Save();
            }
        }

        private void Save()
        {
            SerializeUser(_user);
        }
    }
}
