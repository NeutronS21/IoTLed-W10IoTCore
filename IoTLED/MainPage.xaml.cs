using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Devices.Gpio;
using System.Threading.Tasks;



namespace IoTLED
{
    /// <summary>
    /// Blank page with 3 buttons to control a led, on, off and strobe. 
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private const int LED_PIN = 18;
        private bool LedSpento = true;
        private GpioPin pin;
        private GpioPinValue pinValue;

        public MainPage()
        {
            this.InitializeComponent();
            InitGPIO();
        }

        private void InitGPIO()
        {
            var gpio = GpioController.GetDefault();
            pin = gpio.OpenPin(LED_PIN);
            pinValue = GpioPinValue.Low;
            pin.Write(pinValue);
            pin.SetDriveMode(GpioPinDriveMode.Output);
            this.Status.Text = "Status: GPIO Inizializzato, aspetto i comandi.";
        }

        private void LedOn_Click(object sender, RoutedEventArgs e)
        {
            this.Status.Text = "Status: Led Acceso";
            pin.Write(GpioPinValue.High);
            
        }

        public void LedOff_Click(object sender, RoutedEventArgs e)
        {
            this.Status.Text = "Status: Led Spento";
            pin.Write(GpioPinValue.Low);
            LedSpento = false;
        }

        private void Lampeggio_Click(object sender, RoutedEventArgs e)
        {
            this.Status.Text = "Status: Lampeggio";
            while (LedSpento)
            {
                pin.Write(GpioPinValue.High);
                Task.Delay(5000);
                pin.Write(GpioPinValue.Low);
            }
        }
    }
}
