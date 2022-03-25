using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using System.Text.RegularExpressions;


namespace InsertInfoToDevice
{
    public partial class Form1 : Form
    {
        SerialPortControl RS232_ForReader, RS232_ForBV;
        Thread Thread_ForReader, Thread_ForBV;
        private MyDelegate Delegate;

        //COMPortProgramme COMPortProgramme_Reader, COMPortProgramme_BV;
        ReaderAction ReaderAction;

        byte[] OriginalIPassID= { 0,0,0,0};
        byte[] OriginalFullIPassID = new byte[16];
        byte[] EmptyCardResponse = { 0xea, 0x02, 0x01, 0x00, 0x01, 0x01, 0x90, 0x00 };

        bool TestMode = false;

        int TypeChangeCount = 0,SendFakeEmptyCardTimes=0;

        public string[,] IPassCardsData = new string[9, 204];

        public byte[,] IPassCardsData_ByteType = new byte[9, 204];

        byte[] TempCardData = new byte[204];


        /*Command Check List*/
        byte[] ReaderResponse_IPassID = { 0xEA, 0x02, 0x01, 0x00, 0x0C };
        byte[] ReaderResponse_IPassInfo = { 0xEA, 0x05, 0x01, 0x00, 0xC5 };
        byte[] ReaderResponse_KBVI = { 0xEA, 0x05, 0x02, 0x00, 0x15 };

        byte[] BVResponse_ReadIPass = { 0xEA, 0x05, 0x01, 0x00, 0x09 };
        byte[] BVResponse_EntryTxnIPass = { 0xEA, 0x05, 0x02, 0x00, 0x5A };
        byte[] BVResponse_ExitTxnIPass = { 0xEA, 0x05, 0x03, 0x00, 0x5A };

        /*Command Check List_EasyCard*/
        public int EasyCardDataAmount = 0;
        public int EasyCardDataLen = 0;

        string[,] EasyCardsData = new string[5,400];
        public byte[,] EasyCardsData_ByteType = new byte[5, 400];

        byte[] OriginalEasyCardID = { 0, 0, 0, 0 };


        byte[] ReaderResponse_EasyCardID = { 0xEA, 0x02, 0x01, 0x00, 0x0C, 0x04 };
        byte[] ReaderResponse_EasyCardInfo = { 0xEA, 0x04, 0x01, 0x00, 0xFE };
        byte[] ReaderResponse_EasyCard_MileTrade = { 0xEA, 0x04, 0x01, 0x00, 0x7E };

        byte[] BVResponse_EasyCard_MileTrade = { 0xEA, 0x04, 0x01, 0x00, 0x67, 0x80, 0x05, 0x02, 0x01 };
        byte[] BVResponse_EasyCard_Read = { 0xEA, 0x04, 0x01, 0x00, 0x17, 0x80, 0x05, 0x01, 0x00, 0x10 };


        //額外判定指令竄改參數區
        public byte[,] ReaderCheckCMDCollect = new byte[100, 20], BVCheckCMDCollect = new byte[100, 20];
        public int[] ReaderCheckCMDCollectSize = new int[100], BVCheckCMDCollectSize = new int[100];
        public int ReaderCheckCMDCollectCounter=0,BVCheckCMDCollectCounter=0;
        
        public int[,] ReaderInsertIndex = new int[100, 50], BVInsertIndex = new int[100, 50];
        public byte[,] ReaderIndexValue = new byte[100, 50], BVIndexValue = new byte[100, 50];
        public int[] ReaderInsertindexSize = new int[100], BVInsertindexSize = new int[100];
        



        public Form1()
        {
            InitializeComponent();

            RS232_ForReader = new SerialPortControl();
            RS232_ForBV = new SerialPortControl();
            ComPort_CB1.SelectedIndex = 0;
            comboBox1.SelectedIndex = 0;
            Delegate = new MyDelegate(this);

            ReaderAction = new ReaderAction(ReaderVer_Lab, RS232_ForReader.SerialPort, this.Reader_Tx_Rich, this.Reader_Rx_Rich);

            //獲取假資料
            EasyCardDataAmount = GetCardsfileInfo(EasyCardsData,"EasyCardsData");

            /*
             * EasyCardDataLen用途說明
             * 1.將配合陣列的 0 加回來
             * 2.處理預設陣列過長問題
             */

            EasyCardDataLen = ChangeCardDataToByte(EasyCardsData, EasyCardDataAmount+1, EasyCardsData_ByteType);

            Console.WriteLine("Init Done");

        }

        /*
         * ReaderSpace
         */

        private void ComPort_CB_DropDown(object sender, EventArgs e) 
        {
            ReaderCOMStatus_Lab.Text = "未連接";
            ComPort_CB1.Items.Clear();
            ComPort_CB1.Items.Add("選擇ComPort");
            ComPort_CB1.Items.AddRange(SerialPort.GetPortNames());

            if (comboBox1.SelectedIndex != 0)
            {
                ComPort_CB1.Items.Remove(comboBox1.Text);
            }

        }
        

        private void ComPort_CB_TextChanged(object sender, EventArgs e)
        {
            if (RS232_ForReader.IsOpen())
            {
                if (ComPort_CB1.Text == "")
                {
                    ComPort_CB1.SelectedItem = RS232_ForReader.PortName();
                }
                else if (ComPort_CB1.SelectedIndex == 0)
                {
                    RS232_ForReader.Close();
                    ReaderRecTimeOutCounter.Enabled = false;
                }
                else if (ComPort_CB1.Text != RS232_ForReader.PortName())
                {
                    RS232_ForReader.Close();
                    ReaderRecTimeOutCounter.Enabled = false;
                    Thread.Sleep(1000);
                    RS232_ForReader.PortName(ComPort_CB1.Text);
                    RS232_ForReader.Open();
                    ReaderCOMStatus_Lab.Text = "已連接";
                    Thread_ForReader = new Thread(new ThreadStart(Reader));
                    Thread_ForReader.Start();

                    //RS232_ForReader.BaudRate(57600);
                    ReaderRecTimeOutCounter.Enabled = true;
                }
            }
            else
            {
                if (ComPort_CB1.Text == "")
                {
                    ComPort_CB1.SelectedIndex = 0;
                }
                else if (ComPort_CB1.SelectedIndex != 0 && ComPort_CB1.Text != "")
                {
                    RS232_ForReader.PortName(ComPort_CB1.Text);
                    RS232_ForReader.Open();
                    Thread_ForReader = new Thread(new ThreadStart(Reader));
                    Thread_ForReader.Start();

                    //RS232_ForReader.BaudRate(57600);
                    ReaderRecTimeOutCounter.Enabled = true;
                }
            }
        }



        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

            Reader_Tx_Rich.SelectionStart = Reader_Tx_Rich.TextLength;
            Reader_Tx_Rich.ScrollToCaret();
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {
            Reader_Rx_Rich.SelectionStart = Reader_Rx_Rich.TextLength;
            Reader_Rx_Rich.ScrollToCaret();
        }

        private void Reader()
        {
            while (RS232_ForReader.IsOpen())
            {
                if (RS232_ForReader.GetReceivedLength() > 5)
                {
                    byte[] Reader_Recieved = RS232_ForReader.GetReceived();

                    if (Reader_Recieved[Reader_Recieved.Length - 2] == 0x90 && Reader_Recieved[Reader_Recieved.Length - 1] == 0x00)
                    {
                        /*
                         * 給BV資料竄改位置
                         */
                        /*---------------------------------------------------------------------------------------------------------------*/
                        if (TestMode == true)
                        {                           
                            int CMDCase = CheckCMDFuction(ReaderCheckCMDCollect, ReaderCheckCMDCollectCounter, ReaderCheckCMDCollectSize, Reader_Recieved);

                            Delegate.UpdateUI("[Tx]" + BitConverter.ToString(Reader_Recieved) + Environment.NewLine, Reader_Tx_Rich);

                            switch (CMDCase)
                            {
                                case 0:
                                    Delegate.UpdateUI("Is Case 0!!!" + Environment.NewLine, Reader_Tx_Rich);
                                    //InsertCMDFuc(Reader_Recieved,"Reader")
                                    break;
                                case 1:
                                    Delegate.UpdateUI("Is Case 1!!!" + Environment.NewLine, Reader_Tx_Rich);
                                    break;
                                case 2:
                                    Delegate.UpdateUI("Is Case 2!!!" + Environment.NewLine, Reader_Tx_Rich);
                                    break;
                                case 3:
                                    Delegate.UpdateUI("Is Case 3!!!" + Environment.NewLine, Reader_Tx_Rich);
                                    break;
                                default:
                                    break;

                            }


                            if (SendFakeEmptyCardTimes == 0)
                            {


                                //重置卡片
                                if (TypeChangeCount > EasyCardDataAmount)
                                {
                                    TypeChangeCount = 0;
                                }


                                if (IsTargetCMD(ReaderResponse_EasyCardID, Reader_Recieved) == true)
                                {
                                    Delegate.UpdateUI("EasyCardID:" + Environment.NewLine, Reader_Tx_Rich);
                                    Delegate.UpdateUI("[Tx]" + BitConverter.ToString(Reader_Recieved) + Environment.NewLine, Reader_Tx_Rich);
                                    //保存 悠遊卡ID
                                    Array.Copy(Reader_Recieved, 6, OriginalEasyCardID,0, 4);
                                    Console.WriteLine(OriginalEasyCardID);
                                    //置換為假卡ID
                                    for (int i = 0; i < 4; i++)
                                    {
                                        Reader_Recieved[6 + i] = EasyCardsData_ByteType[TypeChangeCount, 6 + i];
                                    }
                                    Delegate.UpdateUI("FackEasyCardID:\n[Rx]" + BitConverter.ToString(Reader_Recieved) + Environment.NewLine, BV_Rx_Rich);
                                }


                                //讀卡內容修改
                                if (IsTargetCMD(ReaderResponse_EasyCardInfo, Reader_Recieved) == true)
                                {
                                    Delegate.UpdateUI("EasyCardInfo:" + Environment.NewLine, Reader_Tx_Rich);
                                    Delegate.UpdateUI("[Tx]" + BitConverter.ToString(Reader_Recieved) + Environment.NewLine, Reader_Tx_Rich);
                                    //回應假資料
                                    for (int i = 0; i < EasyCardDataLen; i++)
                                    {
                                        Reader_Recieved[i] = EasyCardsData_ByteType[TypeChangeCount, i];
                                    }
                                    LRCFuntion(Reader_Recieved, 5);
                                    Delegate.UpdateUI("FackEasyCardInfo:\n[Rx]" + BitConverter.ToString(Reader_Recieved) + Environment.NewLine, BV_Rx_Rich);                                    

                                    if (TypeChangeCount > EasyCardDataAmount)
                                    {
                                        TypeChangeCount = 0;
                                    }
                                }

                                if (IsTargetCMD(ReaderResponse_EasyCard_MileTrade, Reader_Recieved) == true)
                                {
                                    Delegate.UpdateUI("EasyCard_MileTrade:\n[Rx]" + BitConverter.ToString(Reader_Recieved) + Environment.NewLine, BV_Rx_Rich);
                                    TypeChangeCount++;
                                    SendFakeEmptyCardTimes = 3;
                                }

                            }
                            else
                            {
                                Reader_Recieved = EmptyCardResponse;
                                SendFakeEmptyCardTimes--;
                            }

                        }

                        /*---------------------------------------------------------------------------------------------------------------*/
                        else
                        {
                            Console.WriteLine("Reader => PC:" + BitConverter.ToString(Reader_Recieved));
                            Delegate.UpdateUI("[Tx]" + BitConverter.ToString(Reader_Recieved) + Environment.NewLine, Reader_Tx_Rich);
                            Delegate.UpdateUI("[Rx]" + BitConverter.ToString(Reader_Recieved) + Environment.NewLine, BV_Rx_Rich);

                            SendFakeEmptyCardTimes = 0;
                            TypeChangeCount = 0;
                        }
                        
                        RS232_ForBV.Write(Reader_Recieved);
                        RS232_ForReader.ClearQueue();
                        ReaderRecTimeOutCounter.Enabled = false;
                    }
                }
                Thread.Sleep(5);
            }
        }

        private void ReaderRecTimeOutCounter_Tick(object sender, EventArgs e)
        {
            Reader_Rx_Rich.Text += "Time_Out==>ReaderBaudRatChange\n";
            if (RS232_ForReader.BaudRate() == 115200)
            {
                RS232_ForReader.BaudRate(57600);
            }
            else
            {
                RS232_ForReader.BaudRate(115200);
            }
        }

        /*
        * BV Space
        */

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            if (RS232_ForBV.IsOpen())
            {
                if (comboBox1.Text == "")
                {
                    comboBox1.SelectedItem = RS232_ForBV.PortName();
                }
                else if (comboBox1.SelectedIndex == 0)
                {
                    RS232_ForBV.Close();
                    BVRecTimeOutCounter.Enabled = false;
                }
                else if (comboBox1.Text != RS232_ForBV.PortName())
                {
                    RS232_ForBV.Close();
                    BVRecTimeOutCounter.Enabled = false;
                    Thread.Sleep(1000);
                    RS232_ForBV.PortName(comboBox1.Text);
                    RS232_ForBV.Open();

                    BVCOMStatus_Lab.Text = "已連接";
                    Thread_ForBV = new Thread(new ThreadStart(BV));
                    Thread_ForBV.Start();

                    //
                    BVRecTimeOutCounter.Enabled = true;
                }
            }
            else
            {
                if (comboBox1.Text == "")
                {
                    comboBox1.SelectedIndex = 0;
                }
                else if (comboBox1.SelectedIndex != 0 && comboBox1.Text != "")
                {
                    RS232_ForBV.PortName(comboBox1.Text);
                    RS232_ForBV.Open();
                    Thread_ForBV = new Thread(new ThreadStart(BV));
                    Thread_ForBV.Start();


                    BVRecTimeOutCounter.Enabled = true;
                }
            }
        }
        private void BV()
        {
            byte[] BV_Recieved;
            while (RS232_ForBV.IsOpen())
            {
                if (RS232_ForBV.GetReceivedLength() > 5)
                {
                    BV_Recieved = RS232_ForBV.GetReceived();
                    if (BV_Recieved[BV_Recieved.Length - 2] == 0x90 && BV_Recieved[BV_Recieved.Length - 1] == 0x00)
                    {
                        
                        /*給Reader 竄改位置*/
                        if (TestMode == true)
                        {
                            if (IsTargetCMD(BVResponse_EasyCard_Read, BV_Recieved) == true)
                            {
                                Delegate.UpdateUI("EasyCard_Read:" + Environment.NewLine, BV_Tx_Rich);
                                Delegate.UpdateUI("[Tx]" + BitConverter.ToString(BV_Recieved) + Environment.NewLine, BV_Tx_Rich);
                                //EasyCard 讀卡不需要ID
                            }

                            if (IsTargetCMD(BVResponse_EasyCard_MileTrade, BV_Recieved) == true)
                            {
                                Delegate.UpdateUI("EasyCard_MileTrade:" + Environment.NewLine, BV_Tx_Rich);
                                Delegate.UpdateUI("[Tx]" + BitConverter.ToString(BV_Recieved) + Environment.NewLine, BV_Tx_Rich);
                            }


                        }
                        /*------------------------------------------------------------------------------------------------*/
                        else
                        {
                            Console.WriteLine("BV => PC:" + BitConverter.ToString(BV_Recieved));
                            Delegate.UpdateUI("[Tx]" + BitConverter.ToString(BV_Recieved) + Environment.NewLine, BV_Tx_Rich);
                            Delegate.UpdateUI("[Rx]" + BitConverter.ToString(BV_Recieved) + Environment.NewLine, Reader_Rx_Rich);
                        }


                        RS232_ForReader.Write(BV_Recieved);
                        //保存變更模擬卡片參考資料


                        RS232_ForBV.ClearQueue();
                        BVRecTimeOutCounter.Enabled = false;
                    }
                }
                Thread.Sleep(5);
            }           
        }



        private void BV_Rx_Rich_TextChanged_1(object sender, EventArgs e)
        {

            BV_Rx_Rich.SelectionStart = Reader_Tx_Rich.TextLength;
            BV_Rx_Rich.ScrollToCaret();
        }



        private void BV_Rx_Rich_TextChanged(object sender, EventArgs e)
        {
            BV_Tx_Rich.SelectionStart = BV_Tx_Rich.TextLength;
            BV_Tx_Rich.ScrollToCaret();
        }

        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            BVCOMStatus_Lab.Text = "未連接";
            comboBox1.Items.Clear();
            comboBox1.Items.Add("選擇ComPort");
            comboBox1.Items.AddRange(SerialPort.GetPortNames());
            if (ComPort_CB1.SelectedIndex != 0)
            {
                comboBox1.Items.Remove(ComPort_CB1.Text);
            }
        }


        private void BVRecTimeOutCounter_Tick(object sender, EventArgs e)
        {
            BV_Tx_Rich.Text += "Time_Out==>BVBaudRatChange\n";
            if (RS232_ForBV.BaudRate() == 115200)
            {
                RS232_ForBV.BaudRate(57600);
            }
            else
            {
                RS232_ForBV.BaudRate(115200);
            }
        }

        /*
         * Other 
         */
        public void InsertCMDFuc(byte[] RecieveData,String InsertTargetType, int InsertIndexItem)
        {
            int[] InsertindexSize;
            int[,] InsertIndex;
            byte[,] InsertValue;

            if (InsertTargetType == "Reader")
            {
                InsertindexSize=ReaderInsertindexSize;
                InsertIndex=ReaderInsertIndex;
                InsertValue=ReaderIndexValue;
            }
            else 
            {
                InsertindexSize = BVInsertindexSize;
                InsertIndex = BVInsertIndex;
                InsertValue = BVIndexValue;
            }



            int []  InsertIndexArray= { 0};
            byte[]  InsertValueArray= { 0};

            Array.Resize(ref InsertIndexArray, InsertindexSize[InsertIndexItem]);
            Array.Resize(ref InsertValueArray, InsertindexSize[InsertIndexItem]);

            for (int i = 0; i < InsertindexSize[InsertIndexItem]; i++)
            {
                InsertIndexArray[i] = InsertIndex[InsertIndexItem, i];
                InsertValueArray[i] = InsertValue[InsertIndexItem, i];
            }

            for (int i = 0; i < InsertindexSize[InsertIndexItem]; i++)
            {
                RecieveData[InsertIndexArray[i]] = InsertValue[InsertIndexItem, i];
            }
        }




        public int CheckCMDFuction(byte[,] CheckCMDCollect, int CheckCMDCollectCounter,int[] CheckCMDCollectSize,byte[] RecieveData)
        {
            int CMDCase=999;
            int JudgeLen = 0;//用於校正較高精度指令

            if (CheckCMDCollectCounter > 0)
            {
                
                for (int CheckCMDCollectItem = 0; CheckCMDCollectItem < CheckCMDCollectCounter; CheckCMDCollectItem++)
                {
                    int CMDCheckSum = 0;                   

                    for (int CheckCMDByte = 0; CheckCMDByte < CheckCMDCollectSize[CheckCMDCollectItem]; CheckCMDByte++)
                    {
                        if (RecieveData[CheckCMDByte] == CheckCMDCollect[CheckCMDCollectItem, CheckCMDByte])
                        {
                            CMDCheckSum++;
                        }

                    }

                    if (CMDCheckSum == CheckCMDCollectSize[CheckCMDCollectItem])
                    {
                        

                        if (CMDCase == 999)
                        {
                            JudgeLen = CheckCMDCollectSize[CheckCMDCollectItem];
                            CMDCase = CheckCMDCollectItem;
                        }
                        else if(CheckCMDCollectSize[CheckCMDCollectItem]> JudgeLen)
                        {
                            CMDCase = CheckCMDCollectItem;
                        }

                        
                    }

                }
                return CMDCase;
            }

            return CMDCase;
            

        }



        public int ChangeCardDataToByte(string[,] CardData,int CardAmount,byte[,] TargetCardData_byte)
        {
            int CardDatalen = 0;

            while (CardData[0, CardDatalen] !=null)
            {
                CardDatalen++;                
            }

            for (int CardDataindex = 0; CardDataindex < CardAmount; CardDataindex++)
            {
                for (int i = 0; i < CardDatalen; i++)
                {
                    TargetCardData_byte[CardDataindex, i] = Convert.ToByte(CardData[CardDataindex, i]);
                }
            }
            return CardDatalen;

        }

        public void CardDataEdit(byte[,] CardData,int CardDataIndex,byte[] ReciveData)
        {

            byte[] NeedLRCData=new byte[196];
            /*
             * IPassDataChange             
             */

            CardData[CardDataIndex, 62] = ReciveData[32];

            CardData[CardDataIndex, 63] = ReciveData[33];
            CardData[CardDataIndex, 64] = ReciveData[34];

            CardData[CardDataIndex, 65] = ReciveData[29];

            CardData[CardDataIndex, 66] = ReciveData[23];
            CardData[CardDataIndex, 67] = ReciveData[24];
            CardData[CardDataIndex, 68] = ReciveData[25];
            CardData[CardDataIndex, 69] = ReciveData[26];

            /*
            if (CardData[CardDataIndex, 70] == 0)
            {
                CardData[CardDataIndex, 70] = 1;
            }
            else
            {
                CardData[CardDataIndex, 70] = 0;
            }
            */

            CardData[CardDataIndex, 72] = ReciveData[30];
            CardData[CardDataIndex, 73] = ReciveData[31];

            CardData[CardDataIndex, 77] = CardData[CardDataIndex, 77]++;

            CardData[CardDataIndex, 78] = ReciveData[23];
            CardData[CardDataIndex, 79] = ReciveData[24];
            CardData[CardDataIndex, 80] = ReciveData[25];
            CardData[CardDataIndex, 81] = ReciveData[26];

            CardData[CardDataIndex, 82] = ReciveData[27];

            CardData[CardDataIndex, 83] = ReciveData[21];
            CardData[CardDataIndex, 84] = ReciveData[22];

            
            

            if (CardData[CardDataIndex, 93] == 0)
            {
                CardData[CardDataIndex, 93] = 0x01;//預收金額
            }
            else
            {
                CardData[CardDataIndex, 93] = 0x00;
            }
            

            CardData[CardDataIndex, 107] = ReciveData[23];
            CardData[CardDataIndex, 108] = ReciveData[24];
            CardData[CardDataIndex, 109] = ReciveData[25];
            CardData[CardDataIndex, 110] = ReciveData[26];

            CardData[CardDataIndex, 113] = ReciveData[27];

            CardData[CardDataIndex, 115] = ReciveData[29];

            byte LRCResult = 0x00;
            int DataStartindex=5, DataEndindex = 200;
            for (int i = DataStartindex; i < DataEndindex; i++)
            {
                LRCResult ^= CardData[CardDataIndex,i];

            }
            CardData[CardDataIndex,201] = LRCResult;

        }


        public bool IsTargetCMD(byte[] TargetCMD, byte[] ReciveData)
        {
            int x = 0;
            if (ReciveData.Length >= TargetCMD.Length)
            {
                for (int i = 0; i < TargetCMD.Length; i++)
                {
                    if (ReciveData[i] == TargetCMD[i])
                    {
                        x++;
                    }

                }
                if (x == TargetCMD.Length)
                {
                    return true;
                }
            }


            return false;
        }


        public int GetCardsfileInfo(string[,] Data,string FileName)
        {
            int ReaderLineCount = 0;
            try
            {
                string path = $"CardsInfo\\{FileName}.csv";
                using (StreamReader sr = File.OpenText(path))
                {
                    string line;
                    // Read and display lines from the file until the end of
                    // the file is reached.
                    while ((line = sr.ReadLine()) != null)
                    {

                        string[] sArray = Regex.Split(line, ",");

                        for (int i = 0; i < sArray.Length; i++)
                        {
                            Data[ReaderLineCount, i] = sArray[i];
                        }

                        ReaderLineCount++;
                    }
                    return ReaderLineCount-1;//陣列由0開始算
                }
            }
            catch
            {
                MessageBox.Show(this, "卡片資料讀取失敗", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return 0;
        }



        private void Clear_Click(object sender, EventArgs e)
        {
            BV_Rx_Rich.Text = "";
            BV_Tx_Rich.Text = "";
            Reader_Tx_Rich.Text = "";
            Reader_Rx_Rich.Text = "";
        }
        private void TypeChangeCountPlus_Click(object sender, EventArgs e)
        {
            TypeChangeCount++; 
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                Thread_ForReader.Abort();
                Thread_ForBV.Abort();
            }
            catch
            { 
            
            }
        }

        public void ChangeToOriginalCardID(byte[] ReciveData)
        {
            byte Xor = 0x00;

            Array.Copy(OriginalIPassID, 0, ReciveData, 5, OriginalIPassID.Length);//身分證欄位
            for (int a = 5; a < 13; a++)
            {
                Xor ^= ReciveData[a];
            }
            ReciveData[ReciveData.Length - 3] = Xor;
        }

        private void FakeBVCMD_btn_Click(object sender, EventArgs e)
        {
            byte[] TestCMD = StringToByteArrayFuc(FakeBVCMD_TextBox.Text);

            InsertCMDFuc(TestCMD, "Reader", 0);
            
            RS232_ForReader.Write(TestCMD);
        }

        public void ChangeToOriginalFullCardID(byte[] ReciveData)
        {
            Array.Copy(OriginalFullIPassID, 0, ReciveData, 5, OriginalFullIPassID.Length);//身分證欄位
            LRCFuntion(ReciveData, 5);
        }

        private void SendCMDToBV_btn_Click(object sender, EventArgs e)
        {
            byte[] TestCMD = StringToByteArrayFuc(FakeBVCMD_TextBox.Text);
            RS232_ForBV.Write(TestCMD);
        }

        private void ModeChange_btn_Click(object sender, EventArgs e)
        {
            if (TestMode == false)
            {
                TestMode = true;
                ModeChange_btn.Text = "Normal Mode";
            }
            else
            {
                TestMode = false;
                ModeChange_btn.Text = "Test Mode";
            }
            
        }

        public byte[] StringToByteArrayFuc(string TargetString)
        {
            byte[] TargetsByteArray= {0};

            if (TargetString != "")
            {
                string[] sArray = Regex.Split(TargetString, ",");
                Array.Resize(ref TargetsByteArray, sArray.Length);

                for (int i = 0; i < sArray.Length; i++)
                {
                    TargetsByteArray[i] = Convert.ToByte(Int32.Parse(sArray[i], System.Globalization.NumberStyles.HexNumber));
                }
            }

            return TargetsByteArray;
        }

        public int[] StringToIntArrayFuc(string TargetString)
        {
            int[] TargetsIntArray = { 0 };

            if (TargetString != "")
            {
                string[] sArray = Regex.Split(TargetString, ",");
                Array.Resize(ref TargetsIntArray, sArray.Length);

                for (int i = 0; i < sArray.Length; i++)
                {
                    TargetsIntArray[i] = Int32.Parse(sArray[i]);
                }
            }

            return TargetsIntArray;
        }

        private void Vaule_textBox_KeyPress(object sender, KeyPressEventArgs e)
        {            
            if (e.KeyChar == 13)
            {
                byte[] CheckCMD, ReplaceValue;
                int[] InsertIdex;
                //擷取參數資料
                CheckCMD=StringToByteArrayFuc(CMDHeader.Text);
                InsertIdex = StringToIntArrayFuc(Insert_textBox.Text);
                ReplaceValue= StringToByteArrayFuc(Value_textBox.Text);


                //顯示目標指令
                richTextBox1.Text+= BitConverter.ToString(CheckCMD);
                
                if (CMDTarget.SelectedIndex == 0)
                {
                    richTextBox1.Text += "已加入ReaderCheckCMDCollect\n";
                    ReaderCheckCMDCollectSize[ReaderCheckCMDCollectCounter] = CheckCMD.Length;
                    ReaderInsertindexSize[ReaderCheckCMDCollectCounter] = InsertIdex.Length;

                    //將目標指令塞入目標指令集
                    for (int i = 0; i < CheckCMD.Length; i++)
                    {
                        ReaderCheckCMDCollect[ReaderCheckCMDCollectCounter, i] = CheckCMD[i];
                    }
                    for (int i = 0; i < InsertIdex.Length; i++)
                    {
                        ReaderInsertIndex[ReaderCheckCMDCollectCounter, i] = InsertIdex[i];
                        ReaderIndexValue[ReaderCheckCMDCollectCounter, i] = ReplaceValue[i];
                    }

                    ReaderCheckCMDCollectCounter++;
                }

                else
                {
                    richTextBox1.Text += "已加入BVCheckCMDCollect\n";
                    BVCheckCMDCollectSize[BVCheckCMDCollectCounter] = CheckCMD.Length;
                    BVInsertindexSize[BVCheckCMDCollectCounter] = InsertIdex.Length;
                    
                    //將目標指令塞入目標指令集
                    for (int i = 0; i < CheckCMD.Length; i++)
                    {
                        BVCheckCMDCollect[BVCheckCMDCollectCounter, i] = CheckCMD[i];
                    }

                    for (int i = 0; i < InsertIdex.Length; i++)
                    {
                        BVInsertIndex[BVCheckCMDCollectCounter, i] = InsertIdex[i];
                        BVIndexValue[BVCheckCMDCollectCounter, i] = ReplaceValue[i];
                    }

                    BVCheckCMDCollectCounter++;
                }
            }
        }

        public void LRCFuntion(byte[] ReciveData,int DataStartindex)
        {
            byte LRCResult = 0x00;
            int DataEndindex = ReciveData.Length - 3;
            for (int i = DataStartindex; i < DataEndindex; i++)
            {
                LRCResult ^= ReciveData[i];

            }
            ReciveData[ReciveData.Length - 3] = LRCResult;
        }


        private void CreatCardData(byte[] CardData)
        {
            String TestDataFileName = "CardsData.csv";
            using (StreamWriter sw = new StreamWriter(TestDataFileName, true, System.Text.Encoding.Default))
            {
                sw.WriteLine("CardData:\n");
                foreach (byte data in CardData)
                {                  
                    sw.Write(data.ToString()+",");
                }
            }           
        }

        private void CreatCardEditData(byte[,] CardData,int CardDataindex)
        {
            String TestDataFileName = "IPassCardData.csv";
            using (StreamWriter sw = new StreamWriter(TestDataFileName, true, System.Text.Encoding.Default))
            {
                
                    for (int i = 0; i < 204; i++)
                    {
                        sw.Write(CardData[CardDataindex, i].ToString() + ",");
                    }               
                    sw.WriteLine("");
            }
        }

    }


    public class MyDelegate
    {
        private Form Form;
        private delegate void UpdateUIText(string Str, Control ctl);
        private delegate void ChangeEnable(ComboBox comboBox, bool enablebool);
        private delegate void ChangeSelectedIndex(ComboBox comboBox);

        public MyDelegate(Form Form)
        {
            this.Form = Form;
        }
        public void UpdateUI(string str, Control ctl)
        {
            try
            {
                if (Form.InvokeRequired)
                {
                    UpdateUIText Update = new UpdateUIText(UpdateUI);
                    Form.Invoke(Update, str, ctl);
                }
                else
                {
                    ctl.Text += str;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void UpdateUI(ComboBox comboBox, bool enablebool)
        {
            try
            {
                if (Form.InvokeRequired)
                {
                    ChangeEnable Update = new ChangeEnable(UpdateUI);
                    Form.Invoke(Update, comboBox, enablebool);
                }
                else
                {
                    comboBox.Enabled = enablebool;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void UpdateUI(ComboBox comboBox)
        {
            try
            {
                if (Form.InvokeRequired)
                {
                    ChangeSelectedIndex Update = new ChangeSelectedIndex(UpdateUI);
                    Form.Invoke(Update, comboBox);
                }
                else
                {
                    comboBox.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }


}
