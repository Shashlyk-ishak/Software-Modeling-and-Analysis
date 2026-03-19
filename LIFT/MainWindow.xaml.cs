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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

/*
ИНФОРМАЦИЯ О ПРОГРАММЕ

Данный проект представляет из себя симуляцию работы лифта

На левой части экрана представлены кнопки этажей по нажатию которых кабина лифта перемещается на их уровень
По середине расположена кабина лифта
Справа расположены две кнопки "Открыть двери" и "Закрыть двери", они закрывают и открывают двери кабины лифта соответственно (реализовано посредством смены изображений)

Ограничения учтенные в программе:
1) Лифт не может начать движение с открытыми дверьми
2) Во время движения кнопки Открытия и Закрытия дверей не доступны
3) При попытке переместиться на этаж на котором вы уже находитесь высвечивается соответсвующее сообщение
4) При попытке Закрыть двери, если они уже закрыты, или Открыть двери, если они открыты, высветиться предупреждение.
 */





namespace LIFT
{

    public partial class MainWindow : Window
    {
        //Переменные для работы с методами
        private bool IsClosed = true;
        private const double floor_height = -130;
        private double currentY = 0;

        public MainWindow()
        {
            InitializeComponent();
            DoorsCheck(); //Проверка закрытия дверей на старте программы

        }

        public bool DoorsCheck() //Метод проверки дверей на их состояние
        {
            if (IsClosed == false)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void Floor_1_Click(object sender, RoutedEventArgs e) //Метод отправки лифта на 1 этаж
        {
            if (!IsClosed)
            {
                MessageBox.Show("Закройте двери перед поездкой!!!");
            }
            else
            {
                Change_floor(0);
            }


        }

        private void Floor_2_Click(object sender, RoutedEventArgs e) //Метод отправки лифта на 2 этаж
        {
            if (!IsClosed)
            {
                MessageBox.Show("Закройте двери перед поездкой!!!");
            }
            else
            {
                Change_floor(floor_height);


            }
        }

        private void Floor_3_Click(object sender, RoutedEventArgs e) //Метод отправки лифта на 3 этаж
        {
            if (!IsClosed)
            {
                MessageBox.Show("Закройте двери перед поездкой!!!");
            }
            else
            {
                Change_floor(floor_height * 2);
            }
        }

        private void Open_cabin_Click(object sender, RoutedEventArgs e) //Метод открытия кабины по кнопке
        {
            string current_doors_state = null;
            if (Cabin_image.Source is BitmapImage bitmapImage)
            {
                current_doors_state = bitmapImage.UriSource?.ToString();
            }
            string opened_doors_path = "Img/Opened_doors.png";


            if (current_doors_state == opened_doors_path)
            {
                MessageBox.Show("Двери уже открыты!");
            }
            else
            {
                Cabin_image.Source = new BitmapImage(new Uri("Img/Opened_doors.png", UriKind.Relative));
                IsClosed = false;
            }
        }

        private void Close_cabin_Click(object sender, RoutedEventArgs e) //Метод закрытия кабины по кнопке
        {
            string current_doors_state = null;
            if (Cabin_image.Source is BitmapImage bitmapImage)
            {
                current_doors_state = bitmapImage.UriSource?.ToString();
            }
            string closed_doors_path = "Img/Closed_doors.png";


            if (current_doors_state == closed_doors_path)
            {
                MessageBox.Show("Двери уже закрыты!");
            }
            else
            {
                Cabin_image.Source = new BitmapImage(new Uri("Img/Closed_doors.png", UriKind.Relative));
                IsClosed = true;
            }
        }

        public void Change_floor(double position) //Метод смены этажа (с анимацией)
        {
            if (position == currentY)
            {
                MessageBox.Show("Вы уже находитесь на этом этаже!");
            }
            Open_cabin.IsEnabled = false;
            Close_cabin.IsEnabled = false;
            DoubleAnimation animation = new DoubleAnimation();
            animation.To = position; 
            animation.Duration = TimeSpan.FromSeconds(2);
            animation.EasingFunction = new QuadraticEase();

            animation.Completed += (s, e) =>
            {
                Open_cabin.IsEnabled = true;
                Close_cabin.IsEnabled = true;
            };

            CabinTransform.BeginAnimation(TranslateTransform.YProperty, animation);
            currentY = position;
        }
    }
}
