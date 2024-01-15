using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp10
{
    public class Manager: Worker, ICreateNewProfile
    {     
        public override void SetField(Profile profile, string fieldName, string newValue)
        {
            switch (fieldName)
            {
                case nameof(Profile.FirstName):
                    profile.FirstName = newValue;
                    break;
                case nameof(Profile.MiddleName): 
                    profile.MiddleName = newValue;
                    break;
                case nameof(Profile.LastName): 
                    profile.LastName = newValue;
                    break;
                case nameof(Profile.MobilePhone):
                    profile.MobilePhone = newValue;
                    break;
                case nameof(Profile.PassportID):
                    profile.PassportID = newValue;
                    break;
                default:
                    break;
            }
        }

        public void CreateProfile(string data)
        {
            Profile profile = new Profile(data);
            AddDataToStorage(profile);
        }
    }
}
