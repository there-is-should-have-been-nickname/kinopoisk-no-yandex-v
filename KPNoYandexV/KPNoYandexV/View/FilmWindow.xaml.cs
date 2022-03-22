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
    /// Логика взаимодействия для FilmWindow.xaml
    /// </summary>
    public partial class FilmWindow : Window
    {
        public FilmWindow(int FilmId)
        {
            InitializeComponent();
            var ViewModel = new FilmWindowVM(FilmId);

            DataContext = ViewModel;

            foreach (var Act in ViewModel.CurrentActors)
            {
                var Block = new Rectangle();
                Block.Width = 115;
                Block.Height = 30;
                Block.Stroke = Brushes.Gray;
                Block.RadiusX = 10;
                Block.RadiusY = 10;
                Block.StrokeThickness = 1;
                Block.HorizontalAlignment = HorizontalAlignment.Center;
                Block.VerticalAlignment = VerticalAlignment.Stretch;
                Block.Margin = new Thickness(0, 0, 20, 0);

                var Name = new TextBlock();
                Name.MaxWidth = 150;
                Name.TextWrapping = TextWrapping.Wrap;
                Name.FontFamily = new FontFamily("Consolas");
                Name.FontSize = 12;
                Name.Margin = new Thickness(-120, 15, 0, 0);
                Name.Text = Act.FirstName + " " + Act.LastName;

                ActorsStackPanel.Children.Add(Block);
                ActorsStackPanel.Children.Add(Name);


            }
        }
    }
}
