using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp10
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Worker currentWorker;
        private Profile[] profiles;
        private bool isManager;
        private TextBox[] textBoxes;

        public MainWindow()
        {
            InitializeComponent();
            
            AccessBox.ItemsSource = new string[2] { nameof(Consultant), nameof(Manager) };            
            TextBoxesFill();                       
            ClearTextBoxes();
            TextBoxesEnable(false, 99);
            CheckRights();
        }

        private void TextBoxesFill()
        {
            textBoxes = new TextBox[5]
            {
                LastNameValue,
                FirstNameValue,
                MiddleNameValue,
                MobilePhoneValue,
                PassportIDValue
            };
        }

        private void TextBoxesEnable(bool enable, int exceptionBox)
        {
            for (int i = 0; i < textBoxes.Length; i++)
            {
                textBoxes[i].IsEnabled = enable;
                if (i == exceptionBox && enable == false)
                {
                    textBoxes[i].IsEnabled = !enable;
                }
                
            }
        }

        private void AccessBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AccessBox.SelectedIndex == 0)
            {
                currentWorker = new Consultant();
                isManager = false;
            }
            else if (AccessBox.SelectedIndex == 1)
            {
                currentWorker = new Manager();
                isManager = true;
            }
            profiles = currentWorker.GetDataBase();
            AllDataWindow.ItemsSource = currentWorker.PrintAllData();                        
            ClearTextBoxes();
            TextBoxesEnable(false, 99);
            CheckRights();
        }

        protected void CheckRights()
        {            
            if (currentWorker != null)
            {
                RefreshBaseButton.IsEnabled = true;                
                if (currentWorker is Manager) Create_Button.IsEnabled = true;
                else Create_Button.IsEnabled = false;
            }
            else
            {
                RefreshBaseButton.IsEnabled = false;                
                Create_Button.IsEnabled = false;
            }
            ChangeButton.IsEnabled = false;
        }

        protected void ClearTextBoxes()
        {
            ChangeButton.IsEnabled = false;
            foreach (TextBox box in textBoxes)
            {
                box.Text = string.Empty;
                box.Background = Brushes.White;
            }          
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(AllDataWindow.SelectedIndex > -1)
            {
                TextBoxesEnable(isManager, 3);
                LastNameValue.Text = profiles[AllDataWindow.SelectedIndex].LastName;
                FirstNameValue.Text = profiles[AllDataWindow.SelectedIndex].FirstName;
                MiddleNameValue.Text = profiles[AllDataWindow.SelectedIndex].MiddleName;
                MobilePhoneValue.Text = profiles[AllDataWindow.SelectedIndex].MobilePhone;
                PassportIDValue.Text = HideData(profiles[AllDataWindow.SelectedIndex].PassportID);
                foreach (TextBox box in textBoxes)
                {
                    box.Background = Brushes.White;
                }
                CheckRights();
            }
            
        }

        private void SetNewData(Profile targetProfile, int numberOfLine)
        {
            currentWorker.AddProfileToDataBase(targetProfile, numberOfLine + 1);
        }

        private void Button_Click_RefreshButton(object sender, RoutedEventArgs e)
        {
            AllDataWindow.ItemsSource = currentWorker.PrintAllData();
        }   

        private void LastNameValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            LastNameValue.Background = Brushes.Yellow;
            ChangeButton.IsEnabled = true;
            if (AllDataWindow.SelectedIndex > -1)
            {
                profiles[AllDataWindow.SelectedIndex].ChangeField = "LastName";
                if (String.IsNullOrEmpty(LastNameValue.Text))
                    profiles[AllDataWindow.SelectedIndex].ChangeType = "Удалено";
                else profiles[AllDataWindow.SelectedIndex].ChangeType = "Изменено";
            }
               
        }

        private void FirstNameValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            FirstNameValue.Background = Brushes.Yellow;
            ChangeButton.IsEnabled = true;
            if (AllDataWindow.SelectedIndex > -1)
            {
                profiles[AllDataWindow.SelectedIndex].ChangeField = "FirstNAme";
                if (String.IsNullOrEmpty(FirstNameValue.Text))
                    profiles[AllDataWindow.SelectedIndex].ChangeType = "Удалено";
                else profiles[AllDataWindow.SelectedIndex].ChangeType = "Изменено";
            }
                
        }

        private void MiddleNameValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            MiddleNameValue.Background = Brushes.Yellow;
            ChangeButton.IsEnabled = true;
            if (AllDataWindow.SelectedIndex > -1)
            {
                profiles[AllDataWindow.SelectedIndex].ChangeField = "MiddleName";
                if (String.IsNullOrEmpty(MiddleNameValue.Text))
                    profiles[AllDataWindow.SelectedIndex].ChangeType = "Удалено";
                else profiles[AllDataWindow.SelectedIndex].ChangeType = "Изменено";
            }
                
        }

        private void MobilePhoneValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            MobilePhoneValue.Background = Brushes.Yellow;
            ChangeButton.IsEnabled = true;
            if (AllDataWindow.SelectedIndex > -1)
            {
                profiles[AllDataWindow.SelectedIndex].ChangeField = "MobilePhone";
                if (String.IsNullOrEmpty(MobilePhoneValue.Text))
                    ChangeButton.IsEnabled = false; 
                else
                {
                    ChangeButton.IsEnabled = true;
                    profiles[AllDataWindow.SelectedIndex].ChangeType = "Изменено";
                }
            }
        }

        private void PassportIDValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            PassportIDValue.Background = Brushes.Yellow;
            ChangeButton.IsEnabled = true;
            if (AllDataWindow.SelectedIndex > -1)
            {
                profiles[AllDataWindow.SelectedIndex].ChangeField = "PassportID";
                if (String.IsNullOrEmpty(PassportIDValue.Text))
                    profiles[AllDataWindow.SelectedIndex].ChangeType = "Удалено";
                else profiles[AllDataWindow.SelectedIndex].ChangeType = "Изменено";
            }
        }

        private void Button_Click_Change(object sender, RoutedEventArgs e)
        {
            if (AllDataWindow.SelectedIndex > -1)
            {
                profiles[AllDataWindow.SelectedIndex].LastName = LastNameValue.Text;
                profiles[AllDataWindow.SelectedIndex].FirstName = FirstNameValue.Text;
                profiles[AllDataWindow.SelectedIndex].MiddleName = MiddleNameValue.Text;
                profiles[AllDataWindow.SelectedIndex].MobilePhone = MobilePhoneValue.Text;
                if (isManager) profiles[AllDataWindow.SelectedIndex].PassportID = PassportIDValue.Text;
                profiles[AllDataWindow.SelectedIndex].ChangeDate = DateTime.Now.ToString();
                
                profiles[AllDataWindow.SelectedIndex].DataChanger = currentWorker.GetType().ToString();
                SetNewData(profiles[AllDataWindow.SelectedIndex], AllDataWindow.SelectedIndex);
                ClearTextBoxes();
                TextBoxesEnable(false, 99);
                ChangeButton.IsEnabled = false;
            }
            else if (RefreshBaseButton.IsEnabled == false) 
            {
                if (currentWorker is ICreateNewProfile)
                    (currentWorker as ICreateNewProfile).CreateProfile(CreateProfileData());
                profiles = currentWorker.GetDataBase();                
                ClearTextBoxes();
                TextBoxesEnable(false, 99);
                CheckRights();
                AllDataWindow.IsEnabled = true;
            }
        }

        private void Create_Button_Click(object sender, RoutedEventArgs e)
        {
            ClearTextBoxes();
            TextBoxesEnable(isManager, 3);
            foreach (TextBox box in textBoxes)
            {                
                box.Background = Brushes.Yellow;
            }
            ChangeButton.IsEnabled = true;
            Create_Button.IsEnabled = false;
            AllDataWindow.ItemsSource = String.Empty;
            AllDataWindow.SelectedIndex = -1;
            AllDataWindow.IsEnabled = false;
            RefreshBaseButton.IsEnabled = false;
        }

        private string CreateProfileData()
        {
            string data =
                $"{currentWorker.NumberOfLines() + 1}_" +
                $"{FirstNameValue.Text}_" +
                $"{MiddleNameValue.Text}_" +
                $"{LastNameValue.Text}_" +
                $"{MobilePhoneValue.Text}_" +
                $"{PassportIDValue.Text}_" +
                $"{DateTime.Now}_" +
                $"All Fields_" +
                $"Created_" +
                $"{currentWorker.GetType()}";
            
            return data;
        }

        private string HideData(string data)
        {
            if (!isManager)
            {
                string result = String.Empty;
                for (int i = 0; i < data.Length; i++)
                {
                    result = $"{result}*";
                }
                return result;
            }
            return data;
        }
    }
}
