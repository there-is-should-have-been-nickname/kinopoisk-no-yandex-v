﻿using KPNoYandexV.Data;
using KPNoYandexV.Model;
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
    /// Логика взаимодействия для Films.xaml
    /// </summary>
    public partial class Films : Page
    {
        public Films()
        {
            InitializeComponent();

            List<Film> Films = new List<Film>();
            using (var db = new KPNoYandexVContext())
            {
                Films = db.Films.ToList();
            }

            foreach (var Film in Films)
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
                Name.Text = Film.Name;

                var Number = new TextBlock();
                Number.MaxWidth = 200;
                Number.TextWrapping = TextWrapping.Wrap;
                Number.FontFamily = new FontFamily("Consolas");
                Number.FontSize = 16;
                Number.Margin = new Thickness(-60, -55, 0, 0);
                Number.Text = Film.Id.ToString();


                FilmsStackPanel.Children.Add(Block);
                FilmsStackPanel.Children.Add(Name);
                FilmsStackPanel.Children.Add(Number);

            }
        }
    }
}