using KPNoYandexV.ViewModel;
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
using System.Windows.Shapes;

namespace KPNoYandexV.View
{
    /// <summary>
    /// Логика взаимодействия для UpdateActorWindow.xaml
    /// </summary>
    public partial class UpdateActorWindow : Window
    {
        public UpdateActorWindow(int ActorId)
        {
            InitializeComponent();
            var ViewModel = new UpdateActorWindowVM(ActorId, this);
            DataContext = ViewModel;
        }
    }
}
