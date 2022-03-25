using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using System.IO.Ports;

namespace InsertInfoToDevice
{
    class ReaderAction
    {
        int REC_count = 7;
        bool REC = false;
        public int ReaderFramewareVersion = 0;


        SendCMDToReader Commander1;
        Queue<byte> RecievedQ = new Queue<byte>();

        SerialPort RS232;
        Label VersionLabel;
        RichTextBox richTextBox1, richTextBox2;


        public ReaderAction(Label versionLabel, SerialPort rs232, RichTextBox rich1, RichTextBox rich2)
        {
            RS232 = rs232;
            richTextBox1 = rich1;
            richTextBox2 = rich2;

            VersionLabel = versionLabel;
            ipass.initialization();
            icash.initialization();
            EasyCard.initialization();
            Commander1 = new SendCMDToReader(RS232, richTextBox1);

        }


        public ipass ipass = new ipass();
        public icash icash = new icash();
        public EasyCard EasyCard = new EasyCard();


        public string FindCard()
        {
            Commander1.ReaderFindCard();
            DataRecieved();
            if (REC == true)
            {
                return CheckCardType();
            }
            return CheckCardType();
        }

        public bool GetReader()
        {
            Commander1.GetVersionAtAP();
            DataRecieved();

            if (REC == true)
            {
                byte[] Recieved = RecievedQ.ToArray();
                richTextBox2.Text += "Ver:" + BitConverter.ToString(Recieved) + Environment.NewLine;

                if (ShowVersion() == true)
                {
                    return true;
                }
            }
            return false;

        }

        private string CheckCardType()
        {
            byte[] Recieved = RecievedQ.ToArray();
            byte[] CardID = new byte[7];


            RecievedQ.Clear();
            string CardType = "";


            if (Recieved.Length == 8 || Recieved.Length == 19)
            {
                switch (Recieved[5])
                {
                    case 0x01:  //無卡
                        {

                            CardType = "None";
                            break;
                        }
                    case 0x02:  //多卡重疊
                        {
                            CardType = "多卡重疊";
                            break;
                        }
                    case 0x00:  //未知
                        {
                            CardType = "未知";
                            break;
                        }
                    case 0x03:  //遠鑫卡
                        {
                            CardType = "遠鑫卡";
                            break;
                        }
                    case 0x04:  //悠遊卡
                        {
                            CardType = "悠遊卡";
                            break;
                        }
                    case 0x05:  //一卡通
                        {
                            CardType = "一卡通";
                            break;
                        }
                    case 0x06:  //愛金卡
                        {
                            CardType = "愛金卡";
                            break;
                        }
                    default:
                        {
                            CardType = "NoneCard";
                            break;
                        }
                }
            }
            REC = false;
            return CardType;
        }


        private string IPassCardAction(byte[] Recieved, byte[] CardID)
        {
            string ReaderStatus="";

            if (BitConverter.ToString(Recieved, 6, 4) != BitConverter.ToString(CardID, 0, 4))//防重複卡片
            {
                Array.Copy(Recieved, 6, CardID, 0, 4);
                Commander1.ReadCardForIPass();
                DataRecieved();

                if (REC == true)
                {
                    if (ReaderFramewareVersion == 1)
                    {
                        if (RecievedQ.Count == 128 || RecievedQ.Count == 127)
                        {
                            ipass.Len = RecievedQ.Count;
                            ipass.BytesToReadStruct(RecievedQ.ToArray());
                            RecievedQ.Clear();
                        }
                        else
                        {
                            ReaderStatus += "(讀卡失敗)";
                            Array.Clear(CardID, 0, CardID.Length);
                        }
                    }
                    else if (ReaderFramewareVersion == 2 || ReaderFramewareVersion == 3)
                    {
                        if (RecievedQ.Count == 204)
                        {
                            ipass.Len = RecievedQ.Count;
                            ipass.BytesToReadStruct(RecievedQ.ToArray());
                            RecievedQ.Clear();                           
                        }
                        else
                        {
                            ReaderStatus = "(讀卡失敗)";
                            Array.Clear(CardID, 0, CardID.Length);
                        }
                    }
                    else
                    {
                        //richTextBox1.Text += "卡機類型錯誤" + Environment.NewLine;
                    }
                    REC = false;
                                     
                }

            }
            else
            {
                //DoNoting
            }
            return ReaderStatus;
        }



        private bool ShowVersion()
        {
            byte[] VersionTitle = new byte[3];

            byte[] Recieved = RecievedQ.ToArray();
            RecievedQ.Clear();

            int i = 0, j = 0;

            try
            {
                if (Recieved[1] == 0x01 && Recieved[2] == 0x00)
                {
                    /*獲取前3版本字*/
                    foreach (byte words in Recieved)
                    {
                        if (i > 4 && i < 8)
                        {
                            VersionLabel.Text += Convert.ToChar(words);
                            VersionTitle[j] = words;
                            j++;
                        }
                        else if (i >= 8 && i < Recieved.Length - 2)
                        {
                            VersionLabel.Text += Convert.ToChar(words);
                        }
                        i++;
                    }

                    ReaderFramewareVersion = VersionCheck(VersionTitle, Recieved);

                    return true;
                }
                else
                {
                    REC = false;
                    //VersionLabel.Text = "版本讀取失敗";
                    return false;
                }
            }
            catch
            {
                REC = false;
                //VersionLabel.Text = "版本讀取失敗";
                return false;
            }


        }
        private int VersionCheck(byte[] VersionTitle, byte[] VersionInfo)
        {
            string ReaderApVersion;
            int Gen;

            byte[] F2D = { Convert.ToByte('F'), Convert.ToByte('2'), Convert.ToByte('D') };
            ReaderApVersion = Encoding.ASCII.GetString(VersionInfo, 5, VersionInfo.Length - 7);

            if (Convert.ToChar(VersionTitle[0]) == 'F' && Convert.ToChar(VersionTitle[1]) == '1' && Convert.ToChar(VersionTitle[2]) == 'M')
            {
                Gen = 1;

            }
            else if (Convert.ToChar(VersionTitle[0]) == 'B' && Convert.ToChar(VersionTitle[1]) == '1' && Convert.ToChar(VersionTitle[2]) == 'M')
            {
                Gen = 2;
            }
            else if (VersionTitle[0] == F2D[0] && VersionTitle[1] == F2D[1] && VersionTitle[2] == F2D[2])
            {
                Gen = 2;
            }
            else if (Convert.ToChar(VersionTitle[0]) == 'T' && Convert.ToChar(VersionTitle[1]) == 'S' && Convert.ToChar(VersionTitle[2]) == '2')
            {
                Gen = 3;
            }
            else
            {
                Gen = 0;

            }
            return Gen;
        }
        private void DataRecieved()
        {
            while (RS232.BytesToRead > 0)
            {
                RecievedQ.Enqueue(Convert.ToByte(RS232.ReadByte()));
                if (RecievedQ.Count == 4)
                {
                    REC_count += 256 * RecievedQ.Last();
                }
                if (RecievedQ.Count == 5)
                {
                    REC_count += RecievedQ.Last();
                }
                if (REC_count == RecievedQ.Count)
                {
                    REC_count = 7;
                    REC = true;
                }
            }
        }

    }
}
