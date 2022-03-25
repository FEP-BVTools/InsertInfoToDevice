using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using System.IO.Ports;

namespace InsertInfoToDevice
{
    class SendCMDToReader
    {
        public ipass ipass = new ipass();
        public icash icash = new icash();
        public EasyCard EasyCard = new EasyCard();
        


        byte[] CardID = new byte[7];

        SerialPort RS232;
        RichTextBox richTextBox1;

        public SendCMDToReader( SerialPort rs232, RichTextBox Rich)
        {
            RS232 = rs232;
            richTextBox1 = Rich;

        }


        private byte LRCFuntion(byte[] LRCData)
        {
            int i = 0;
            byte LRCResult = 0;
            bool Leni = LRCData.Length > LRCData.Length - 3;
            foreach (byte x in LRCData)
            {
                if (LRCData.Length - 3 == i)
                {
                    break;
                }
                if (i >= 5)
                {
                    LRCResult ^= x;
                }
                i++;
            }

            return LRCResult;
        }


        public void PPR_Reset()
        {

            /*
            byte[] TM_Location_ID = { 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30 };
            byte[] TM_ID = { 0x30, 0x30 };
            byte[] TM_TXN_Date_Time = { 0x32,0x30,0x32,0x30,//年
                                        0x31,0x30,0x30,0x35,//月日
                                        0x30,0x39,0x31,0x35,0x30,0x30};//時分秒 
            byte[] TM_Serial_Number = { 0x30, 0x30, 0x30, 0x30, 0x30, 0x31 };
            byte[] TM_Agent_Number = { 0x30, 0x30, 0x30, 0x30 };
            byte[] TXN_Date_Time = { 0x33, 0x70, 0x5E, 0x5E };
            byte Location_ID =0x65;
            byte[] New_Location_ID = { 0x65, 0x00 };
            byte Service_Provider_ID = 0x23;
            byte[] New_Service_Provider_ID ={ 0x23, 0x00, 0x00 };
            byte MicroPaymentFlag = 0x80;
            byte[] OneDayQuotaMicroPayment = { 0x00, 0x00 };
            byte[] OneceQuotaMicroPayment = { 0x00, 0x00 };
            byte SAM_Slot_Control_Flag = 0x11;
            byte MifareKeySet =0x00;
            byte[] ReservedForUse = { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            byte DataTail = 0xFA;
            */
            byte[] PRR_Reset = { 0xEA, 0x04, 0x01, 0x00, 0x47, 0x80, 0x01, 0x00, 0x01, 0x40,
                            0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30,
                            0x30, 0x30,
                            0x32, 0x30, 0x32, 0x30, 0x30, 0x33, 0x30, 0x33, 0x31, 0x34, 0x35, 0x36, 0x35, 0x31,
                            0x30, 0x30, 0x30, 0x30, 0x30, 0x31,
                            0x30, 0x30, 0x30, 0x30,
                            0x33, 0x70, 0x5E, 0x5E,
                            0x65,
                            0x65, 0x00,
                            0x23,
                            0x23, 0x00, 0x00, 0x80, 0x00, 0x00, 0x00, 0x00,
                            0x11,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xFA,
                            0xEB, 0x90, 0x00 };

            int unixTime = Convert.ToInt32(DateTimeOffset.Now.ToUnixTimeSeconds() + 28800);
            byte[] timeStampArray = BitConverter.GetBytes(unixTime);
            string datestr;
            DateTime gtm = (new DateTime(1970, 1, 1)).AddSeconds(Convert.ToInt32(unixTime));
            datestr = gtm.ToString("yyyyMMddHHmmss");
            int TMTXNDateTimeIndex = 22;
            int TXN_Date_TimeIndex = 46;
            int j = 0;

            //richTextBox1.Text += BitConverter.ToString(timeStampArray) + Environment.NewLine;

            foreach (byte a in datestr)
            {
                PRR_Reset[TMTXNDateTimeIndex + j] = a;
                //richTextBox1.Text += $"{PRR_Reset[TMTXNDateTimeIndex + j]:X}"+"--" + Environment.NewLine;
                j++;
            }

            for (int i = 0; i < 4; i++)
            {
                PRR_Reset[TXN_Date_TimeIndex + i] = timeStampArray[i];
                //richTextBox1.Text += $"{timeStampArray[i]:X}" + Environment.NewLine;
            }

            PRR_Reset[PRR_Reset.Length - 3] = LRCFuntion(PRR_Reset);

            richTextBox1.Text +="PPR_Reset:"+ BitConverter.ToString(PRR_Reset) + Environment.NewLine;//輸出指令
            //richTextBox1.Text += datestr + Environment.NewLine;

            RS232.Write(PRR_Reset, 0, PRR_Reset.Length);
        }

        public void PR_Reset()
        {
            byte Xor = 0xA4;
            byte[] PR_Reset = { 0xEA, 0x04, 0x01, 0x00, 0x26, 0x84, 0x01, 0x01, 0x00, 0x20 };
            Array.Resize(ref PR_Reset, PR_Reset.Length + 35);
            PR_Reset[PR_Reset.Length - 3] = Xor;
            PR_Reset[PR_Reset.Length - 2] = 0x90;

            richTextBox1.Text += "PR_Reset:"+BitConverter.ToString(PR_Reset) + Environment.NewLine;//輸出指令

            RS232.Write(PR_Reset, 0, PR_Reset.Length);
            
        }

        public void ReaderFindCard()
        {
            byte[] FindCardCode = { 0xEA, 0x02, 0x01, 0x00, 0x01, 0x00, 0x90, 0x00 };

            richTextBox1.Text += "FindCard:"+BitConverter.ToString(FindCardCode) + Environment.NewLine;

            RS232.Write(FindCardCode, 0, FindCardCode.Length);
        }

        public void SAMCheck()
        {
            byte[] FindSAMCardCode = { 0xEA, 0x01, 0x01, 0x00, 0x01, 0x04, 0x90, 0x00 };

            richTextBox1.Text += "FindSAMCard:" + BitConverter.ToString(FindSAMCardCode) + Environment.NewLine;

            RS232.Write(FindSAMCardCode, 0, FindSAMCardCode.Length);
        }

        public void SAMCheckForTest()
        {
            byte[] FindSAMCardCode = { 0xEA, 0x01, 0x01, 0x00, 0x01, 0x08, 0x90, 0x00 };

           richTextBox1.Text += "SAMCheckForTest:" + BitConverter.ToString(FindSAMCardCode) + Environment.NewLine;

            RS232.Write(FindSAMCardCode, 0, FindSAMCardCode.Length);
        }

        public void RebootReaderAtAP()
        {
            Char[] Resetchars = { 'R', 'E', 'S', 'E', 'T' };
            byte[] CmdHead = { 0xEA, 0x01, 0x20, 0x00, 0x05 };
            byte[] CmdTail = { 0x90, 0x00 };
            int i = CmdHead.Length;
            Array.Resize(ref CmdHead, CmdHead.Length + Resetchars.Length + CmdTail.Length);
            foreach (char code in Resetchars)
            {
                CmdHead[i] = Convert.ToByte(code);
                i++;
            }
            CmdHead[CmdHead.Length - 2] = 0x90;

            richTextBox1.Text += "RebootReaderAtAP:" + BitConverter.ToString(CmdHead) + Environment.NewLine;//顯示輸出指令

            RS232.Write(CmdHead, 0, CmdHead.Length);
        }


        public void GetVersionAtAP()
        {
            byte[] GetVersion = { 0xEA, 0x01, 0x00, 0x00, 0x01, 0x00, 0x90, 0x00, };

            richTextBox1.Text += "GetVersionAtAP:" + BitConverter.ToString(GetVersion) + Environment.NewLine;//顯示輸出指令
            
            RS232.Write(GetVersion, 0, GetVersion.Length);
        }



        public void ReadCardForEasyCard()
        {
            byte[] PR_ReadForMilageBV2 = { 0xEA, 0x04, 0x01, 0x00, 0x06, 0x68, 0x13, 0x01, 0x00, 0x7B, 0x01, 0x90, 0x00 };


            RS232.Write(PR_ReadForMilageBV2, 0, PR_ReadForMilageBV2.Length);

        }

        public void WriteCardForEasyCard()
        {
            byte Xor = 0x00;
            byte[] EasyCardWriteCode = { 0xEA, 0x04, 0x01, 0x00, 0x2B, 0x68, 0x13, 0x03, 0x03, 0x24 };
            Array.Resize(ref EasyCardWriteCode, 50);
            Array.Copy(EasyCard.GetWriteStruct(), 0, EasyCardWriteCode, 10, EasyCard.GetWriteStruct().Length);
            EasyCardWriteCode[EasyCardWriteCode.Length - 4] = 0x21;
            for (int a = 5; a < EasyCardWriteCode.Length - 3; a++)
            {
                Xor ^= EasyCardWriteCode[a];
            }
            EasyCardWriteCode[EasyCardWriteCode.Length - 3] = Xor;
            EasyCardWriteCode[EasyCardWriteCode.Length - 2] = 0x90;
            RS232.Write(EasyCardWriteCode, 0, EasyCardWriteCode.Length);
        }

        public void ReadCardForIPass()
        {
            byte Xor = 0x00;
            byte[] ipassreadCode = { 0xEA, 0x05, 0x01, 0x00, 0x09, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x90, 0x00 };
            int unixTime = Convert.ToInt32(DateTimeOffset.Now.ToUnixTimeSeconds() + 28800);
            Array.Copy(CardID, 0, ipassreadCode, 5, 4);
            Array.Copy(BitConverter.GetBytes(unixTime), 0, ipassreadCode, 9, 4);
            for (int a = 5; a < 13; a++)
            {
                Xor ^= ipassreadCode[a];
            }
            ipassreadCode[ipassreadCode.Length - 3] = Xor;
            RS232.Write(ipassreadCode, 0, ipassreadCode.Length);

        }

        public void WriteCardForIPass()
        {
            byte Xor = 0x00;
            byte[] ipasswriteCode = { 0xEA, 0x05, 0x00, 0x00, 0x00 };
            if (ipass.Len <= 128)//無Maas段
            {
                Array.Resize(ref ipasswriteCode, 61);
                ipasswriteCode[4] = 0x36;
            }
            else
            {
                Array.Resize(ref ipasswriteCode, 105);
                ipasswriteCode[4] = 0x62;
            }

            if (ipass.Get_preBUSStatus() == 0x00)//目前卡片讀卡結果為上或下車狀態
            {
                ipasswriteCode[2] = 0x02;
            }
            else
            {
                ipasswriteCode[2] = 0x03;
            }

            Array.Copy(ipass.GetWriteStruct(), 0, ipasswriteCode, 5, ipass.GetWriteStruct().Length);

            //LRC
            for (int i = 5; i < ipasswriteCode.Length - 3; i++)
            {
                Xor ^= ipasswriteCode[i];
            }

            ipasswriteCode[ipasswriteCode.Length - 3] = Xor;
            ipasswriteCode[ipasswriteCode.Length - 2] = 0x90;

            //richTextBox1.Text += "長度:" + ipasswriteCode.Length + ";" + BitConverter.ToString(ipasswriteCode) + Environment.NewLine;//輸出指令
            RS232.Write(ipasswriteCode, 0, ipasswriteCode.Length);
        }

        public void WriteCardForIPass_Gen2()
        {
            byte Xor = 0x00;
            byte[] ipasswriteCode = { 0xEA, 0x05, 0x00, 0x00, 0x5A };
            Array.Resize(ref ipasswriteCode, 97);
            if (ipass.Get_preBUSStatus() == 0x00)//目前卡片讀卡結果為上或下車狀態
            {
                ipasswriteCode[2] = 0x02;
            }
            else
            {
                ipasswriteCode[2] = 0x03;
            }

            Array.Copy(ipass.GetWriteStruct(), 0, ipasswriteCode, 5, ipass.GetWriteStruct().Length);

            //LRC
            for (int i = 5; i < ipasswriteCode.Length - 3; i++)
            {
                Xor ^= ipasswriteCode[i];
            }

            ipasswriteCode[ipasswriteCode.Length - 3] = Xor;
            ipasswriteCode[ipasswriteCode.Length - 2] = 0x90;

            //richTextBox1.Text += "長度:" + ipasswriteCode.Length + ";" + BitConverter.ToString(ipasswriteCode) + Environment.NewLine;//輸出指令
            RS232.Write(ipasswriteCode, 0, ipasswriteCode.Length);
        }

        public void ReadCardForICash()
        {
            byte Xor = 0x00;
            byte[] icashreadCode = { 0xEA, 0x06, 0x01, 0x00, 0x0D, 0x07 };
            int unixTime = Convert.ToInt32(DateTimeOffset.Now.ToUnixTimeSeconds() + 28800);
            Array.Resize(ref icashreadCode, 20);
            Array.Copy(CardID, 0, icashreadCode, 6, 7);
            Array.Copy(BitConverter.GetBytes(unixTime), 0, icashreadCode, 13, 4);
            for (int a = 5; a < 17; a++)
            {
                Xor ^= icashreadCode[a];
            }
            icashreadCode[icashreadCode.Length - 3] = Xor;
            icashreadCode[icashreadCode.Length - 2] = 0x90;
            RS232.Write(icashreadCode, 0, icashreadCode.Length);
        }


        public void WriteCardForICash()
        {
            byte Xor = 0x00;
            byte[] icashwriteCode = { 0xEA, 0x06, 0x02, 0x00, 0x2F };
            Array.Resize(ref icashwriteCode, 54);
            Array.Copy(icash.GetWriteStruct(), 0, icashwriteCode, 5, icash.GetWriteStruct().Length);
            for (int a = 5; a < 51; a++)
            {
                Xor ^= icashwriteCode[a];
            }
            icashwriteCode[icashwriteCode.Length - 3] = Xor;
            icashwriteCode[icashwriteCode.Length - 2] = 0x90;
            RS232.Write(icashwriteCode, 0, icashwriteCode.Length);
        }
    }
}
