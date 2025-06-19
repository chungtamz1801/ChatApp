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
        List<byte[]> keyAES = new List<byte[]>();
        List<string> clients = new List<string>();
        List<int> index = new List<int>();
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

        private void send_btn_Click(object sender, EventArgs e)
        {
            try
            {
                if (Friend_IP.Text == String.Empty || Friend_Port.Text == String.Empty)
                {
                    MessageBox.Show("Thông tin kết nối không hợp lệ");
                }
                else if (message_tbx.Text == "Key")
                {
                    for (int i = 0; i < keyAES.Count; i++)
                    {
                        message_lst.Items.Add($"System: Key:{BitConverter.ToString(keyAES[i])}, Client:{clients[i]}, Index:{index[i]}");
                    }
                }
                else
                {
                    //find key
                    epRemote = new IPEndPoint(IPAddress.Parse(Friend_IP.Text), Convert.ToInt32(Friend_Port.Text));
                    IPEndPoint remote = epRemote as IPEndPoint;
                    IPEndPoint local = epLocal as IPEndPoint;
                    int id = clients.IndexOf(remote.ToString());
                    if (id == -1) 
                    {
                        MessageBox.Show("Bạn chưa có key của địa chỉ này!");
                    }
                    else
                    {
                        byte[] key = keyAES[id];
                        //send message
                        byte[] sendingMessage = Default.InsertArrayInFirst(10 * index[clients.IndexOf(remote.ToString())], AES.Encrypt(message_tbx.Text, key, new byte[16]));
                        socket.SendTo(sendingMessage, epRemote);

                        message_lst.Items.Add("Me: " + message_tbx.Text);
                    }
                }
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
            IPEndPoint remote = epRemote as IPEndPoint;
            if (clients.Contains(remote.ToString()))
            {
                MessageBox.Show("Bạn đã có AES Key rồi");
            }
            else
            {
                long n, en, de;
                RSAT.GenerateKeys(out n, out en, out de);
                publicKeyRSA = new long[] { en, n };
                privateKeyRSA = new long[] { de, n };
                message_lst.Items.Add($"System: Private Key là ({de},{n})");
                message_lst.Items.Add($"System: Public Key là ({en},{n})");
                string data = Default.TrasferLongArrayToString(publicKeyRSA);
                IPEndPoint ep = epLocal as IPEndPoint;
                data = data + "/" + ep.ToString() + "/";
                ASCIIEncoding aEncoding = new ASCIIEncoding();
                byte[] temp = aEncoding.GetBytes(data);
                byte[] sendingData = Default.InsertArrayInFirst(1 + keyAES.Count * 10, temp);
                socket.SendTo(sendingData, epRemote);
            }
        }

        private void MessageCallBack(IAsyncResult aResult)
        {
            try
            {
                byte[] receivedData = new byte[16*storage+1];
                receivedData = (byte[])aResult.AsyncState;
                if (receivedData[0] % 10 == 0)
                {
                    byte[] key = keyAES[receivedData[0]/10];
                    byte[] bytes = Default.DeleteArrayInFirst(receivedData.Take(17).ToArray());
                    string receivedMessage = AES.Decrypt(bytes, key, new byte[16]);
                    message_lst.Items.Add($"{clients[receivedData[0] / 10]}: {receivedMessage}");
                }
                else if(receivedData[0] % 10 == 1)
                {
                    byte[] bytes = Default.DeleteArrayInFirst(receivedData);
                    ASCIIEncoding aEncoding = new ASCIIEncoding();
                    string temp = aEncoding.GetString(bytes);
                    string[] list = temp.Split('/');
                    string s = list[1];
                    temp = list[0];
                    DialogResult dialogResult = MessageBox.Show(s+" want to connect!", "Request", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        long[] pubKey = Default.TrasferStringToLongArray(temp);
                        message_lst.Items.Add($"System: Public Key nhận được là ({pubKey[0]},{pubKey[1]})");
                        //Gen key AES
                        byte[] key = new byte[16];
                        using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
                        {
                            rng.GetBytes(key);
                        }
                        keyAES.Add(key);
                        clients.Add(s);
                        index.Add(receivedData[0]/10);
                        message_lst.Items.Add($"System: Key AES là {BitConverter.ToString(key)}");
                        //Ma hoa key AES bang public key RSA
                        string sendingMessage = RSAT.Encrypt(key, pubKey[0], pubKey[1]);
                        message_lst.Items.Add($"System: Key AES sau khi mã hóa bằng RSA là {sendingMessage}");
                        byte[] data = aEncoding.GetBytes(sendingMessage);
                        byte[] sendingData = Default.InsertArrayInFirst(2+keyAES.Count*10, data);
                        IPEndPoint ep = new IPEndPoint(IPAddress.Parse(s.Split(':')[0]), Convert.ToInt32(s.Split(':')[1]));
                        socket.SendTo(sendingData, ep);
                    }
                    else if(dialogResult == DialogResult.No)
                    {
                        byte[] response = new byte[] {3};
                        epRemote = new IPEndPoint(IPAddress.Parse(s.Split(':')[0]), Convert.ToInt32(s.Split(':')[1]));
                        socket.SendTo(response, epRemote);
                    }
                    
                }
                else if(receivedData[0] % 10 == 2)
                {
                    //Decode RSA
                    byte[] bytes = Default.DeleteArrayInFirst(receivedData);
                    ASCIIEncoding aEncoding = new ASCIIEncoding();
                    string temp = aEncoding.GetString(bytes);
                    message_lst.Items.Add($"System: Key AES mã hóa nhận được là {temp}");
                    byte[] key = RSAT.Decrypt(temp, privateKeyRSA[0], privateKeyRSA[1]);
                    keyAES.Add(key);
                    IPEndPoint remote = epRemote as IPEndPoint;
                    clients.Add(remote.ToString());
                    index.Add(receivedData[0] / 10 -1);
                    message_lst.Items.Add($"System: Key AES sau khi giải mã là {BitConverter.ToString(key)}");
                    //
                }
                else if (receivedData[0] == 3)
                {
                    message_lst.Items.Add($"System: Request was denied");
                }

                //Convert byte[] to string
                buffer = new byte[16*storage+1];
                socket.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref epTemp, new AsyncCallback(MessageCallBack), buffer);
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
