using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using ChatApp.Secutiry;
using System.Security.Cryptography;

namespace ChatApp
{
    public partial class MessageScreen : Form
    {
        Socket socket;
        EndPoint epRemote,epLocal,epTemp;
        byte[] buffer;
        byte[] keyAES;
        long[] publicKeyRSA = new long[2];
        long[] privateKeyRSA = new long[2];
        int storage = 10;

        public string ip { get; set; }
        public int port { get; set; }

        public MessageScreen()
        {
            InitializeComponent();
        }

        private void MessageScreen_Load(object sender, EventArgs e)
        {
            //set up socket
            socket = new Socket(AddressFamily.InterNetwork,SocketType.Dgram, ProtocolType.Udp);
            socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            //get user IP
            Information_IP.Text = ip;
            Information_Port.Text = port.ToString();
            Friend_IP.Text = Default.GetLocalIP();
            epLocal = new IPEndPoint(IPAddress.Parse(ip), port);
            socket.Bind(epLocal);
            epTemp =new IPEndPoint(IPAddress.Any, 0);
            buffer = new byte[16 * storage + 1];
            socket.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref epTemp, new AsyncCallback(MessageCallBack), buffer);
        }

        private void connect_btn_Click(object sender, EventArgs e)
        {
            //connect to remote IP  
            //epRemote = new IPEndPoint(IPAddress.Parse(Friend_IP.Text), Convert.ToInt32(Friend_Port.Text));
        }

        private void send_btn_Click(object sender, EventArgs e)
        {
            //ASCIIEncoding aEncoding = new ASCIIEncoding();
            try
            {
                byte[] sendingMessage = new byte[16 * storage + 1];
                Array.Copy(AES.Encrypt(message_tbx.Text, keyAES, new byte[16]), 0, sendingMessage, 1, 16);
                //send message
                sendingMessage[0] = 0;
                socket.SendTo(sendingMessage,epRemote);

                message_lst.Items.Add("Me: " + message_tbx.Text);

                message_tbx.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void key_btn_Click(object sender, EventArgs e)
        {
            epRemote = new IPEndPoint(IPAddress.Parse(Friend_IP.Text), Convert.ToInt32(Friend_Port.Text));
            long n, en, de;
            RSAT.GenerateKeys(out n, out en, out de);
            publicKeyRSA = new long[]{ en, n };
            privateKeyRSA = new long[] { de, n };
            message_lst.Items.Add($"System: Private Key là ({de},{n})");
            message_lst.Items.Add($"System: Public Key là ({en},{n})");
            string data = Default.TrasferLongArrayToString(publicKeyRSA);
            IPEndPoint ep = epLocal as IPEndPoint; 
            data = data + "/" + ep.ToString();
            ASCIIEncoding aEncoding = new ASCIIEncoding();
            byte[] temp = aEncoding.GetBytes(data);
            byte[] sendingData = new byte[temp.Length+1];
            Array.Copy(temp,0, sendingData,1,temp.Length);
            sendingData[0] = 1;
            socket.SendTo(sendingData,epRemote);
        }

        private void MessageCallBack(IAsyncResult aResult)
        {
            try
            {
                byte[] receivedData = new byte[16*storage+1];
                receivedData = (byte[])aResult.AsyncState;
                if (receivedData[0] == 0)
                {
                    byte[] bytes = new byte[16];
                    Array.Copy(receivedData, 1 , bytes, 0, bytes.Length);
                    string receivedMessage = AES.Decrypt(bytes, keyAES, new byte[16]);
                    message_lst.Items.Add("Friend: " + receivedMessage);
                }
                else if(receivedData[0] == 1)
                {
                    byte[] bytes = new byte[16*storage];
                    Array.Copy(receivedData, 1, bytes, 0, 16*storage);
                    ASCIIEncoding aEncoding = new ASCIIEncoding();
                    string temp = aEncoding.GetString(bytes);
                    string[] list = temp.Split('/');
                    temp = list[0];
                    long[] pubKey = Default.TrasferStringToLongArray(temp);
                    message_lst.Items.Add($"System: Public Key nhận được là ({pubKey[0]},{pubKey[1]})");
                    //Gen key AES
                    keyAES = new byte[16];
                    using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
                    {
                        rng.GetBytes(keyAES);
                    }
                    message_lst.Items.Add($"System: Key AES là {BitConverter.ToString(keyAES)}");
                    //Ma hoa key AES bang public key RSA
                    string sendingMessage = RSAT.Encrypt(keyAES, pubKey[0], pubKey[1]);
                    message_lst.Items.Add($"System: Key AES sau khi mã hóa bằng RSA là {sendingMessage}");
                    byte[] data = aEncoding.GetBytes(sendingMessage);
                    byte[] sendingData = new byte[data.Length+1];
                    Array.Copy(data, 0, sendingData, 1, data.Length);
                    string s = list[1] + " want to connect!";
                    DialogResult dialogResult = MessageBox.Show(s, "Request", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        sendingData[0] = 2;
                        epRemote = new IPEndPoint(IPAddress.Parse(list[1].Split(':')[0]), Convert.ToInt32(list[1].Split(':')[1]));
                        socket.SendTo(sendingData, epRemote);
                    }
                    else if(dialogResult == DialogResult.No)
                    {
                        sendingData = new byte[] {3};
                        epRemote = new IPEndPoint(IPAddress.Parse(list[1].Split(':')[0]), Convert.ToInt32(list[1].Split(':')[1]));
                        socket.SendTo(sendingData, epRemote);
                    }
                    
                }
                else if(receivedData[0] == 2)
                {
                    //Decode RSA
                    byte[] bytes = new byte[16*storage];
                    Array.Copy(receivedData, 1, bytes, 0, 16*storage);
                    ASCIIEncoding aEncoding = new ASCIIEncoding();
                    string temp = aEncoding.GetString(bytes);
                    message_lst.Items.Add($"System: Key AES mã hóa nhận được là {temp}");
                    keyAES = RSAT.Decrypt(temp, privateKeyRSA[0], privateKeyRSA[1]);
                    message_lst.Items.Add($"System: Key AES sau khi giải mã là {BitConverter.ToString(keyAES)}");
                    //
                }
                else if (receivedData[0] == 3)
                {
                    message_lst.Items.Add($"System: Request was denied");
                }

                //Convert byte[] to string
                buffer = new byte[16*storage+1];
                socket.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref epRemote, new AsyncCallback(MessageCallBack), buffer);
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
