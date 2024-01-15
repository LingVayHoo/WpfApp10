using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace WpfApp10
{
    public class Consultant: Worker
    {
        /// <summary>
        /// Метод изменения поля Profile
        /// </summary>
        /// <param name="profile"></param>
        /// <param name="fieldName"></param>
        /// <param name="newValue"></param>
        public override void SetField(Profile profile, string fieldName, string newValue)
        {
            switch (fieldName)
            {
                case nameof(Profile.MobilePhone):
                    profile.MobilePhone = newValue;
                    break;
                default:
                    break;
            }
        }

        public override string[] PrintAllData()
        {
            _needTohide = true;
            return base.PrintAllData();
        }

    }
}

