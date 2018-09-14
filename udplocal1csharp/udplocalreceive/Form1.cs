using System;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Threading;

//////////////////////////////////////////////////////////////////////
// ----------------- www.interestprograms.ru -------------------------
// -------------- исходные коды программ и игр -----------------------
// ------- ОТПРАВКА И ПРИЕМ СООБЩЕНИЙ ПО ПРОТОКОЛУ UDP --------------- 
// -------------- часть1 извлечение сообщений ------------------------
//////////////////////////////////////////////////////////////////////

namespace udplocalreceive
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void buttonReceive_Click(object sender, EventArgs e)
        {
            // Запускаем отдельный поток для асинхронной работы приложения
            // во время приема сообщений
            stopReceive = false;
            rec = new Thread(new ThreadStart(Receive));
            rec.Start();
        }

        Thread rec = null;
        UdpClient udp = new UdpClient(15000);
        bool stopReceive = false;

        // Функция извлекающая пришедшие сообщения
        // работающая в отдельном потоке.
        void Receive()
        {
            try
            {
                while (true)
                {

                    IPEndPoint ipendpoint = null;
                    byte[] message = udp.Receive(ref ipendpoint);
                    ShowMessage(Encoding.Default.GetString(message));

                    // Если дана команда остановить поток, останавливаем бесконечный цикл.
                    if (stopReceive == true) break;
                }  
            }
            catch(Exception e)
            {
                //MessageBox.Show(e.Message);
            }
        }
        

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopReceive();
        }

        // Функция безопасной остановки дополнительного потока
        void StopReceive()
        {
            stopReceive = true; 
            if(udp != null) udp.Close();
            if(rec != null) rec.Join();
        }


        // Блок кода предоставляющий безопасный доступ к членам класса из разных потоков
        delegate void ShowMessageCallback(string message);
        void ShowMessage(string message)
        {
            if (textBoxReceive.InvokeRequired)
            {
                ShowMessageCallback dt = new ShowMessageCallback(ShowMessage);
                Invoke(dt, new object[] { message });
            }
            else
            {
                textBoxReceive.Text = message;
            }
        }

    }
}
                                              