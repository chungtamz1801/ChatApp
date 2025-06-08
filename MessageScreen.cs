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
        EndPoint epRemote,epLocal;
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
            //gen key for RSA
            long p = 61;
            long q = 53;
            long n, en, de;
            RSAT.GenerateKeys(p, q,out n,out en,out de);
            message_lst.Items.Add("System:" + en);
            message_lst.Items.Add("System:" + de);
            //get user IP
            Friend_IP.Text = Default.GetLocalIP();
        }

        private void connect_btn_Click(object sender, EventArgs e)
        {
            ///binding socket
            epLocal = new IPEndPoint(IPAddress.Parse(ip), port);
            socket.Bind(epLocal);
            //connect to remote IP  
            epRemote = new IPEndPoint(IPAddress.Parse(Friend_IP.Text), Convert.ToInt32(Friend_Port.Text));
            socket.Connect(epRemote);
            //Listening the specific port
            buffer = new byte[16*storage+1];
            socket.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref epRemote, new AsyncCallback(MessageCallBack), buffer);
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
                socket.Send(sendingMessage);

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
            long  p = 61;
            long  q = 53;
            long  n, en, de;
            RSAT.GenerateKeys(p, q, out n, out en, out de);
            publicKeyRSA = new long[]{ en, n };
            privateKeyRSA = new long[] { de, n };
            string data = Default.TrasferLongArrayToString(publicKeyRSA);
            ASCIIEncoding aEncoding = new ASCIIEncoding();
            byte[] temp = aEncoding.GetBytes(data);
            byte[] sendingData = new byte[temp.Length+1];
            Array.Copy(temp,0, sendingData,1,temp.Length);
            sendingData[0] = 1;
            socket.Send(sendingData);
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
                    long[] pubKey = Default.TrasferStringToLongArray(temp);
                    //Gen key AES
                    keyAES = new byte[16];
                    using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
                    {
                        rng.GetBytes(keyAES);
                    }
                    message_lst.Items.Add("System1: " + BitConverter.ToString(keyAES));
                    //Ma hoa key AES bang public key RSA
                    string sendingMessage = RSAT.Encrypt(keyAES, pubKey[0], pubKey[1]);
                    message_lst.Items.Add("System2: " + sendingMessage);
                    byte[] data = aEncoding.GetBytes(sendingMessage);
                    byte[] sendingData = new byte[data.Length+1];
                    Array.Copy(data, 0, sendingData, 1, data.Length);
                    sendingData[0] = 2;
                    socket.Send(sendingData);
                }
                else if(receivedData[0] == 2)
                {
                    //Decode RSA
                    byte[] bytes = new byte[16*storage];
                    Array.Copy(receivedData, 1, bytes, 0, 16*storage);
                    ASCIIEncoding aEncoding = new ASCIIEncoding();
                    string temp = aEncoding.GetString(bytes);
                    message_lst.Items.Add("System3: " + temp);
                    keyAES = RSAT.Decrypt(temp, privateKeyRSA[0], privateKeyRSA[1]);
                    message_lst.Items.Add("System4: " + BitConverter.ToString(keyAES));
                    //
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
