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
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            mediaVideo.LoadedBehavior = MediaState.Manual;
            sliderVolume.Value = 50;
            mediaVideo.Volume = sliderVolume.Value / 100;   // É necessário usar valores decimais para alterar o volume aqui, que vai de 0 a 1.
            if (mediaVideo.Source is null)
            {
                sliderVolume.IsEnabled = false;
                buttonMute.IsEnabled = false;
                buttonPause.IsEnabled = false;
                buttonPlay.IsEnabled = false;
                buttonStop.IsEnabled = false;
                sliderVideo.IsEnabled = false;
            }
        }

        private void buttonMute_Click(object sender, RoutedEventArgs e)
        {
            sliderVolume.Value = 0;
        }

        private void sliderVolume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mediaVideo.Volume = sliderVolume.Value / 100;   // É necessário usar valores decimais para alterar o volume aqui, que vai de 0 a 1.
        }

        private void buttonPlay_Click(object sender, RoutedEventArgs e)
        {
            mediaVideo.Play();
            buttonPlay.IsEnabled = false;
            buttonStop.IsEnabled = true;
            buttonPause.IsEnabled = true;
        }

        private void buttonPause_Click(object sender, RoutedEventArgs e)
        {
            if (mediaVideo.Position > TimeSpan.FromMilliseconds(0) || buttonPlay.IsEnabled)
            {
                mediaVideo.Pause();
                buttonPlay.IsEnabled = true;
            }
        }

        private void buttonStop_Click(object sender, RoutedEventArgs e)
        {
            mediaVideo.Position = TimeSpan.FromSeconds(0);  // Também pode ser usado como nova instância seguidos de três params: new TimeSpan(horas, minutos, segundos);
            mediaVideo.Stop();
            buttonPlay.IsEnabled = true;
            buttonStop.IsEnabled = false;
            buttonPause.IsEnabled = false;
            sliderVideo.Value = 0;
        }

        private void buttonOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.InitialDirectory = "C:\\";
            openFile.Filter = "Vídeos|*.wmv;*.avi;*.mp4;*.mpeg;*.mov";
            //openFile.ShowDialog();
            if (openFile.ShowDialog() == true)
            {
                string videoName = openFile.FileName;
                try
                {
                    // Você precisa criar uma nova instância de mídia do tipo Uri para carregar a fonte quando ela não é carregada junto ao programa.
                    mediaVideo.Source = new Uri(videoName);
                    if (mediaVideo.NaturalDuration.HasTimeSpan)
                    {
                        TimeSpan totalVideo = mediaVideo.NaturalDuration.TimeSpan;  // Armazena a duração total do vídeo.
                        sliderVideo.Maximum = totalVideo.TotalSeconds;
                    }

                    double altura = mediaVideo.NaturalVideoHeight;
                    double largura = mediaVideo.NaturalVideoWidth;
                    this.Width = Math.Max(640,largura);
                    this.Height = Math.Max(480,altura);

                    sliderVolume.IsEnabled = true;
                    buttonMute.IsEnabled = true;
                    buttonPause.IsEnabled = true;
                    buttonPlay.IsEnabled = false;
                    buttonStop.IsEnabled = true;
                    sliderVideo.IsEnabled = true;
                    labelNoVideo.Visibility = Visibility.Collapsed;
                    labelNoVideo.IsEnabled = false;
                    mediaVideo.Play();
                }
                catch (NotSupportedException)
                {
                    MessageBox.Show("Arquivo não suportado.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao carregar o vídeo: "+ex.Message);
                }
                openFile.InitialDirectory = videoName;
            }
        }

        private void mediaVideo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!labelNoVideo.IsEnabled)
            {
                if (buttonPlay.IsEnabled)
                {
                    mediaVideo.Play();
                    buttonPlay.IsEnabled = false;
                }
                else
                {
                    mediaVideo.Pause();
                    buttonPlay.IsEnabled = true;
                }
            }
        }

        private void sliderVideo_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //sliderVideo.Maximum =
            mediaVideo.Position = TimeSpan.FromSeconds(sliderVideo.Value);
        }
    }
}