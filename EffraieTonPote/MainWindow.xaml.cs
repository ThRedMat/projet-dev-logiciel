using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace EffraieTonPote
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		
		}

		/* Bouton play on-click */
		private void Start(object sender, RoutedEventArgs e)
		{
			/* Variable global pour les points */
			Application.Current.Properties["Points"] = 0;

			/* Affichage des éléments */
			Play.Visibility = Visibility.Hidden;
			Arrow.Visibility = Visibility.Visible;
			TextBlock0.Visibility = Visibility.Visible;

			/* Path dynamique */
			string picture = "Assets\\arrow.png";
            string path = System.IO.Path.Combine(Environment.CurrentDirectory.Replace("\\bin\\Debug\\net5.0-windows", ""), picture);

            Arrow.Source = new BitmapImage(new Uri(path));
			
			/* Determination de la direction de la flèche*/
			ArrDir();

			/* Événements déclanché par la pression d'une touche */
			KeyUp += KeyHandler;
		}

		/* Événements déclanché par la pression d'une touche */
		public void KeyHandler(object sender, KeyEventArgs e)
		{
			/* Test pour voir si les touche relaché correspondent aux bonne direction de flèches */
			int pick = (int)Application.Current.Properties["Pick"];
			int points = (int)Application.Current.Properties["Points"];

			/*Determination de la direction de la flèche*/
			ArrDir();

			if (pick == 1 && e.Key == Key.Up || pick == 2 && e.Key == Key.Down || pick == 3 && e.Key == Key.Left ||
			   pick == 4 && e.Key == Key.Right)
			{
				points++;
				Application.Current.Properties["Points"] = points;
				TextBlock0.Text = "Scores : " + points;
				TextBlock1.Background = Brushes.LightGreen;
			}

			else
			{
				KeyUp -= KeyHandler;
				TextBlock1.Background = Brushes.Red;
				TextBlock0.Visibility = Visibility.Hidden;
				Application.Current.Properties["Points"] = 0;

				string picture = "Assets\\scary.jpg";
				string path1 = System.IO.Path.Combine(Environment.CurrentDirectory.Replace("\\bin\\Debug\\net5.0-windows", ""), picture);
				Scary.Source = new BitmapImage(new Uri(path1));
				Scary.Visibility = Visibility.Visible;

				string sound = "Assets\\jumpscare.wav";
				string path0 = System.IO.Path.Combine(Environment.CurrentDirectory.Replace("\\bin\\Debug\\net5.0-windows", ""), sound);
				MediaPlayer Sound1 = new MediaPlayer();
				Sound1.Open(new Uri(path0));
				Sound1.Play();
				
				var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(7) };
				timer.Start();
				timer.Tick += (sender, args) =>
				{
					timer.Stop();
					Reset();
					Sound1.Stop();
				};
			}
		}

		/* Determination de la direction de la flèche */
		public void ArrDir()
		{
			/* Détermin pick */
			Random random = new();
			Application.Current.Properties["Pick"] = random.Next(1, 5);
			int pick = (int)Application.Current.Properties["Pick"];

			switch (pick)
			{
				case 1:
					RotateTransform rotateTransform = new(-90);
					Arrow.RenderTransform = rotateTransform;
                    Arrow.HorizontalAlignment = HorizontalAlignment.Center;
                    Arrow.VerticalAlignment = VerticalAlignment.Center;
					break;

				case 2:
					rotateTransform = new(90);
					Arrow.RenderTransform = rotateTransform;
                    Arrow.HorizontalAlignment = HorizontalAlignment.Center;
                    Arrow.VerticalAlignment = VerticalAlignment.Center;
					break;

				case 3:
					rotateTransform = new(180);
					Arrow.RenderTransform = rotateTransform;
                    Arrow.HorizontalAlignment = HorizontalAlignment.Center;
                    Arrow.VerticalAlignment = VerticalAlignment.Center;
					break;

				default:
					rotateTransform = new(0);
					Arrow.RenderTransform = rotateTransform;
                    Arrow.HorizontalAlignment = HorizontalAlignment.Center;
                    Arrow.VerticalAlignment = VerticalAlignment.Center;
					break;
			}
		}

		/* Reset */
		public void Reset()
		{
			Application.Current.Properties["Points"] = 0;

			Play.Visibility = Visibility.Visible;
			TextBlock1.Background = Brushes.LightBlue;

			Scary.Visibility = Visibility.Hidden;
			Arrow.Visibility = Visibility.Hidden;
			TextBlock0.Visibility = Visibility.Hidden;

			RotateTransform rotateTransform = new(0);
			Arrow.RenderTransform = rotateTransform;

			KeyUp -= KeyHandler;
		}
	}
}