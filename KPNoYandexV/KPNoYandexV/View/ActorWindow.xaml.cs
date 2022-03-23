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
    /// Логика взаимодействия для ActorWindow.xaml
    /// </summary>
    public partial class ActorWindow : Window
    {
        public ActorWindow(int ActorId)
        {
            InitializeComponent();

            var ViewModel = new ActorWindowVM(ActorId);
            DataContext = ViewModel;

            foreach (var Film in ViewModel.CurrentFilms)
            {
                var TemplateButton = new ControlTemplate(typeof(Button));
                var Bord = new FrameworkElementFactory(typeof(Border));
                Bord.SetValue(Border.WidthProperty, 115.0);
                Bord.SetValue(Border.HeightProperty, 30.0);
                Bord.SetValue(Border.BorderBrushProperty, Brushes.Gray);
                Bord.SetValue(Border.BorderThicknessProperty, new Thickness(1, 1, 1, 1));
                Bord.SetValue(Border.CornerRadiusProperty, new CornerRadius(10.0));
                Bord.SetValue(Border.BackgroundProperty, Brushes.Transparent);
                TemplateButton.VisualTree = Bord;
                var ContentPresent = new FrameworkElementFactory(typeof(ContentPresenter));
                Bord.AppendChild(ContentPresent);


                var Btn = new Button();
                Btn.Template = TemplateButton;
                Btn.Margin = new Thickness(10, 15, 0, 0);

                Btn.HorizontalContentAlignment = HorizontalAlignment.Center;
                Btn.VerticalContentAlignment = VerticalAlignment.Center;
                Btn.Content = Film.Name;
                Btn.Cursor = Cursors.Hand;
                Btn.Command = ViewModel.FilmOpenClick;
                Btn.CommandParameter = Film.Id;

                FilmsStackPanel.Children.Add(Btn);


            }
        }
    }
}
