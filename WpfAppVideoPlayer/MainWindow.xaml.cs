using Microsoft.Win32;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfAppVideoPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            mediaElementVideo.LoadedBehavior = MediaState.Manual;       // Necessary for the buttons to function properly.
        }

        private void buttonMute_Click(object sender, RoutedEventArgs e)
        {
            if (sliderVolume.Value > 0)
            {
                sliderVolume.Value = 0;
            }
            else
            {
                sliderVolume.Value = 50;
            }
        }

        private void buttonPlay_Click(object sender, RoutedEventArgs e)
        {
            sliderDuration.Value = 0;
            mediaElementVideo.Play();
            buttonPlay.IsEnabled = false;
            buttonPause.IsEnabled = true;
        }

        private void buttonPause_Click(object sender, RoutedEventArgs e)
        {
            if (buttonPause.IsEnabled)
            {
                buttonPause.IsEnabled = false;
            }
            mediaElementVideo.Pause();
            buttonPlay.IsEnabled = true;
        }

        private void buttonStop_Click(object sender, RoutedEventArgs e)
        {
            mediaElementVideo.Stop();
            sliderDuration.Value = 0;
            buttonPlay.IsEnabled = true;
            buttonPause.IsEnabled = false;
        }

        private void buttonOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Supported|*.avi;*.mp4;*.wmv;*.mpeg";
            if (openFile.ShowDialog() == true)
            {
                mediaElementVideo.Source = new Uri(openFile.FileName);
                labelNoVideoSelected.Visibility = Visibility.Collapsed;
                buttonMute.IsEnabled = true;
                buttonPause.IsEnabled = true;
                buttonStop.IsEnabled = true;
                sliderDuration.IsEnabled = true;
                sliderVolume.IsEnabled = true;
                mediaElementVideo.Play();
                mediaElementVideo.Volume = 50;
                sliderVolume.Value = mediaElementVideo.Volume;
            }
        }

        private void sliderVolume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mediaElementVideo.Volume = sliderVolume.Value;
        }

        private void mediaElementVideo_MediaOpened(object sender, RoutedEventArgs e)
        {
            sliderDuration.Maximum = mediaElementVideo.NaturalDuration.TimeSpan.TotalSeconds;   // Total duration
        }

        private void sliderDuration_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            mediaElementVideo.Position = TimeSpan.FromSeconds(sliderDuration.Value);
        }

        private void mediaElementVideo_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (buttonPlay.IsEnabled == false)
            {
                mediaElementVideo.Pause();
                buttonPlay.IsEnabled = true;
                buttonPause.IsEnabled = false;
            }
            else
            {
                mediaElementVideo.Play();
                buttonPlay.IsEnabled = false;
                buttonPause.IsEnabled = true;
            }
        }
    }
}