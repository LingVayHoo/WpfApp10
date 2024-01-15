using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp10
{
    public abstract class Worker
    {
        protected string _fileName = "DataBase.txt";
        protected bool _needTohide = false;

        /// <summary>
        /// Получение базы данных из файла
        /// </summary>
        public Profile[] GetDataBase()
        {
            if (IsFileExists())
            {
                Profile[] DataBase = new Profile[NumberOfLines()];
                Console.WriteLine(DataBase.Length);
                using (StreamReader sr = new StreamReader(_fileName, Encoding.UTF8))
                {
                    string line;
                    int i = 0;

                    while (!String.IsNullOrEmpty((line = sr.ReadLine())))
                    {
                        DataBase[i] = new Profile(line);
                        i++;
                    }
                }
                return DataBase;
            }
            else
            {
                return null;
            }
        }

        protected string PassportIDAccess(Profile profile)
        {
            string result = HideData(true, profile.PassportID);
            return result;
        }

        /// <summary>
        /// Метод для проверки существования файла
        /// </summary>
        /// <returns></returns>
        protected bool IsFileExists()
        {
            FileInfo file = new FileInfo(@_fileName);
            return file.Exists;
        }

        /// <summary>
        /// Метод для определения ID
        /// </summary>
        /// <returns></returns>
        public int NumberOfLines()
        {
            int count = 0;
            if (IsFileExists())
            {
                using (StreamReader sr = new StreamReader(_fileName, Encoding.UTF8))
                {
                    string line;
                    while (!String.IsNullOrEmpty((line = sr.ReadLine())))
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        /// <summary>
        /// Вывод всех профилей
        /// </summary>
        /// <param name="ShowPassportID"></param>
        /// <returns></returns>
        public virtual string[] PrintAllData()
        {
            Profile[] DataBase = GetDataBase();
            if (DataBase != null)
            {
                string[] allProfiles = new string[DataBase.Length];
                for (int i = 0; i < DataBase.Length; i++)
                {
                    string stringProfile = ConvertProfileToString(DataBase[i], _needTohide);
                    allProfiles[i] = stringProfile;
                }
                return allProfiles;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Метод конвертации класса Profile в string
        /// </summary>
        /// <param name="targetProfile"></param>
        /// <returns></returns>
        protected virtual string ConvertProfileToString(Profile targetProfile, bool needToHide)
        {
            string temp;
            temp = HideData(needToHide, targetProfile.PassportID);
            string line =
                $"{targetProfile.ID}_" +
                $"{targetProfile.FirstName}_" +
                $"{targetProfile.MiddleName}_" +
                $"{targetProfile.LastName}_" +
                $"{targetProfile.MobilePhone}_" +
                $"{temp}_" +
                $"{targetProfile.ChangeDate}_" +
                $"{targetProfile.ChangeField}_" +
                $"{targetProfile.ChangeType}_" +
                $"{targetProfile.DataChanger}";
            return line;
        }

        protected string HideData(bool isNeedHide, string data)
        {
            if (isNeedHide)
            {
                string result = String.Empty;
                for (int i = 0; i < data.Length; i++)
                {
                    result = $"{result}*";
                }
                return result;
            }
            else 
            { 
                return data; 
            }
        }

        /// <summary>
        /// Метод записи в файл
        /// </summary>
        /// <param name="dataBase"></param>
        protected void SetDatabaseToStorage(Profile[] dataBase)
        {
            using (StreamWriter sw = new StreamWriter(_fileName, false, Encoding.UTF8))
            {
                foreach (var profile in dataBase)
                {
                    string line = ConvertProfileToString(profile, false);
                    sw.WriteLine(line);
                }
            }
        }

        protected void AddDataToStorage(Profile profile)
        {
            using (StreamWriter sw = new StreamWriter(_fileName, true, Encoding.UTF8))
            {
                string line = ConvertProfileToString(profile, false);
                sw.WriteLine(line);
            }
        }

        /// <summary>
        /// Метод внесения изменений в базу данных
        /// </summary>
        /// <param name="targetProfile">Профиль, в котором есть изменения</param>
        /// <param name="id">ID записи</param>
        public void AddProfileToDataBase(Profile targetProfile, int id)
        {
            Profile[] dataBase = GetDataBase();
            targetProfile.ID = id.ToString();

            for (int i = 0; i < dataBase.Length; i++)
            {
                if (i + 1 == id) dataBase[i] = targetProfile;
            }
            SetDatabaseToStorage(dataBase);

        }

        /// <summary>
        /// Метод изменения поля в профиле
        /// </summary>
        /// <param name="profile">Профиль, содержащий целевое поле</param>
        /// <param name="fieldName">Название поля</param>
        /// <param name="newValue">Новое значение поля</param>
        public abstract void SetField(Profile profile, string fieldName, string newValue);
    }

    

}
