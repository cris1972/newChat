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
// -------------- часть 1 отправляющий клиент ------------------------  
//////////////////////////////////////////////////////////////////////

namespace udplocalsend
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            // Отправка сообщения
            UdpClient udp = new UdpClient();

            // Указываем адрес отправки сообщения
            IPAddress ipaddress = IPAddress.Parse(textBoxAddress.Text);
            IPEndPoint ipendpoint = new IPEndPoint(ipaddress, 15000);

            // Формирование оправляемого сообщения и его отправка.
            byte[] message = Encoding.Default.GetBytes(textBoxSend.Text);
            int sended = udp.Send(message, message.Length, ipendpoint);
            textBoxSend.Text = "";

            // После окончания попытки отправки закрываем UDP соединение,
            // и освобождаем занятые объектом UdpClient ресурсы.
            udp.Close();
        }

    }
}
                                              