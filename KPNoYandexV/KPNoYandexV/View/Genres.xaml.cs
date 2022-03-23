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
    /// Логика взаимодействия для Genres.xaml
    /// </summary>
    public partial class Genres : Page
    {
        public Genres()
        {
            InitializeComponent();

            List<Model.Genre> Genres = new List<Model.Genre>();
            using (var db = new KPNoYandexVContext())
            {
                Genres = db.Genres.ToList();
            }

            foreach (var Genre in Genres)
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
                Name.MaxWidth = 200;
                Name.TextWrapping = TextWrapping.Wrap;
                Name.FontFamily = new FontFamily("Consolas");
                Name.FontSize = 16;
                Name.Margin = new Thickness(0, -55, 0, 0);
                Name.Text = Genre.Name;

                var Number = new TextBlock();
                Number.MaxWidth = 200;
                Number.TextWrapping = TextWrapping.Wrap;
                Number.FontFamily = new FontFamily("Consolas");
                Number.FontSize = 16;
                Number.Margin = new Thickness(-60, -55, 0, 0);
                Number.Text = Genre.Id.ToString();

                var Btn = new Button();
                Btn.FontFamily = new FontFamily("Consolas");
                Btn.FontSize = 14;
                Btn.Content = "Open";
                Btn.Width = 60;
                Btn.Height = 30;
                Btn.Cursor = Cursors.Hand;
                Btn.Margin = new Thickness(200, -90, 0, 0);
                Btn.Command = new GenresPageVM().GenreOpenClick;
                Btn.CommandParameter = Genre.Id;


                GenresStackPanel.Children.Add(Block);
                GenresStackPanel.Children.Add(Name);
                GenresStackPanel.Children.Add(Number);
                GenresStackPanel.Children.Add(Btn);

            }
        }
    }
}
