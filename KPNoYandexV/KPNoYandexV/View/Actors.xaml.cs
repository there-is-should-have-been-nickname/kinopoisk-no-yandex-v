using KPNoYandexV.Data;
using KPNoYandexV.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KPNoYandexV.View
{
    /// <summary>
    /// Логика взаимодействия для Actors.xaml
    /// </summary>
    public partial class Actors : Page
    {
        public Actors()
        {
            InitializeComponent();

            var ViewModel = new ActorsPageVM();
            DataContext = ViewModel;
            foreach (var Actor in ViewModel.Actors)
            {
                var Block = new Rectangle();
                Block.Width = 300;
                Block.Height = 50;
                Block.Stroke = Brushes.Gray;
                Block.RadiusX = 10;
                Block.RadiusY = 10;
                Block.StrokeThickness = 1;
                Block.HorizontalAlignment = HorizontalAlignment.Center;
                Block.VerticalAlignment = VerticalAlignment.Stretch;
                Block.Margin = new Thickness(0, 0, 0, 20);

                var Name = new TextBlock();
                Name.MaxWidth = 150;
                Name.TextWrapping = TextWrapping.Wrap;
                Name.FontFamily = new FontFamily("Consolas");
                Name.FontSize = 16;
                Name.Margin = new Thickness(-80, -55, 0, 0);
                Name.Text = Actor.FirstName + " " + Actor.LastName;

                var Number = new TextBlock();
                Number.MaxWidth = 200;
                Number.TextWrapping = TextWrapping.Wrap;
                Number.FontFamily = new FontFamily("Consolas");
                Number.FontSize = 16;
                Number.Margin = new Thickness(-70, -55, 0, 0);
                Number.Text = Actor.Id.ToString();


                ActorsStackPanel.Children.Add(Block);
                ActorsStackPanel.Children.Add(Name);
                ActorsStackPanel.Children.Add(Number);

            }
        }
    }
}
