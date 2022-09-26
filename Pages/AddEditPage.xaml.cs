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
using ConnectDBSQL.Classes;

namespace ConnectDBSQL.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage : Page
    {        
        private Person _person = new Person();//новое поле, которое будет хранить в себе экземпляр пользователя
        public AddEditPage(Person selectedPerson)// объект selectedPerson
        {
            InitializeComponent();            
            if(selectedPerson != null)
                _person = selectedPerson;
            DataContext = _person; // Созданный контекст
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder error = new StringBuilder();//Объект ошибка

            if (string.IsNullOrWhiteSpace(_person.FirstName))
                error.AppendLine("Укажите имя");
            if(string.IsNullOrWhiteSpace(_person.LastName))
                error.AppendLine("Укажите фамилию");
            if (string.IsNullOrWhiteSpace(_person.Age.ToString()))
                error.AppendLine("Укажите возраст");
            if(error.Length > 0)
            {
                MessageBox.Show(error.ToString());
                return;
            }
            //Если пользователь новый
            if(_person.ID == 0)
                GokkeeEntities.GetGokkee().Person.Add(_person);//Добавить его 
            try
            {
                GokkeeEntities.GetGokkee().SaveChanges(); //Сохранить изменения
                MessageBox.Show("Данные сохранены");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
