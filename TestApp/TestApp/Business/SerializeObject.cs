using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;

namespace TestApp.Business
{
    public class SerializeObject
    {

        #region Fields
        private string _pathToPersonalFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        #endregion

        #region Propertys
        public string PathToJSON
        {
            get
            {
                return Path.Combine(_pathToPersonalFolder, "LottoNumbers.json");
            }
        }
        #endregion

        public void SerializeSparkleBox(SparkleBox obj)
        {
            string json = JsonConvert.SerializeObject(obj);

            using (var file = File.Open(PathToJSON, FileMode.Create, FileAccess.Write))
            using (var strm = new StreamWriter(file))
            {
                strm.Write(json);
            }
        }


        /// <summary>
        /// Serialisiert die SparkleBox
        /// </summary>
        /// <param name="objList"></param>
        public void SerializeSparkleBox(ObservableCollection<SparkleBox> objList)
        {
            string json = JsonConvert.SerializeObject(objList);

            using (var file = File.Open(PathToJSON, FileMode.Create, FileAccess.Write))
            using (var strm = new StreamWriter(file))
            {
                strm.Write(json);
            }
        }

        /// <summary>
        /// Deserialisiert die SparkleBox ObservableCollection
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<SparkleBox> DeserializeSparkleBoxList()
        {
            List<SparkleBox> temp = new List<SparkleBox>();

            if (CheckIfFileExist(PathToJSON))
            {
                using (StreamReader r = new StreamReader(PathToJSON))
                {
                    string json = r.ReadToEnd();
                    temp = JsonConvert.DeserializeObject<List<SparkleBox>>(json);
                }
            }

            return new ObservableCollection<SparkleBox>(temp);
        }

        #region Methods
        /// <summary>
        /// Check if json-file exist
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private bool CheckIfFileExist(string path)
        {
            bool checker = false;

            if (File.Exists(path))
            {
                checker = true;
            }
            return checker;
        }
        #endregion
    }
}
