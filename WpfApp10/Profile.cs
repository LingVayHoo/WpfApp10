using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp10
{
    public class Profile
    {
        public string ID { get; set; }
        public string firstName;
        public string middleName;
        public string lastName;
        private string mobilePhone;
        private string passportID;
        private string changeDate;
        private string changedField;
        private string changeType;
        private string dataChanger;
        public string FirstName
        {
            get { return firstName; }

            set
            {
                firstName = value;              
            }
        }

        public string MiddleName
        {
            get { return middleName; }
            set
            {
                middleName = value;               
            }
        }

        public string LastName
        {
            get { return lastName; } 
            set
            {
                lastName = value;
            }
        }

        public string MobilePhone
        {
            get { return mobilePhone; }
            set
            {
                mobilePhone = value;
            }
        }

        public string PassportID
        {
            get { return passportID; }
            set
            {
                passportID = value;
            }
        }

        public string ChangeField
        {
            get { return changedField; }
            set { changedField = value; }
        }

        public string ChangeDate
        {
            get { return changeDate; }
            set { changeDate = value; }
        }

        public string ChangeType 
        {
            get { return changeType; }
            set
            {
                changeType = value;
            }
        }

        public string DataChanger
        {
            get { return dataChanger; }
            set
            {
                dataChanger = value;
            }
        }

        /// <summary>
        /// Конструктор для создания профиля из string
        /// </summary>
        /// <param name="Data"></param>
        public Profile(string Data)
        {
            string[] splitedData = Data.Split('_');
            ID = splitedData[0];
            FirstName = splitedData[1];
            MiddleName = splitedData[2];
            LastName = splitedData[3];
            MobilePhone = splitedData[4];
            PassportID = splitedData[5];
            ChangeDate = splitedData[6];
            ChangeField = splitedData[7];
            ChangeType = splitedData[8];
            DataChanger = splitedData[9];
        }
        
        ///// <summary>
        ///// Метод вывода данных с возможностью скрытия поля PassportID
        ///// </summary>
        ///// <param name="showPassportID"></param>
        ///// <returns></returns>
        //public string PrintData(bool showPassportID)
        //{
        //    string temp;
        //    temp = PassportIDGet(showPassportID);
        //    string line =
        //        $"{ID}_" +
        //        $"{FirstName}_" +
        //        $"{MiddleName}_" +
        //        $"{LastName}_" +
        //        $"{mobilePhone}_" +
        //        $"{temp}_" +
        //        $"{ChangeDate}_" +
        //        $"{ChangeField}_" +
        //        $"{ChangeType}_" +
        //        $"{DataChanger}";
        //    return line;
        //}

        //public string PassportIDGet(bool isManager)
        //{
        //    if (isManager)
        //    {
        //        return passportID;
        //    }
        //    else
        //    {
        //        return HideData(passportID);
        //    }
        //}

        
    }
}
