using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Windows.Forms;
using System.IO;

namespace InsertInfoToDevice
{

    public class ipass
    {
        private KRTC_Read ipassRead;
        private KRTC_Write ipassWrite;
        public int Len = 0;


        //=====================================================================
        public void initialization()
        {
            ipassRead.csn = new byte[16];
            ipassRead.evValue = new byte[4];
            ipassRead.bevValue = new byte[4];
            ipassRead.syncValue = new byte[4];
            ipassRead.persionalID = new byte[6];
            ipassRead.speActivationTime = new byte[4];
            ipassRead.speExipreTime = new byte[4];
            ipassRead.speResetTime = new byte[4];
            ipassRead.speRouteID = new byte[2];
            ipassRead.speUseNo = new byte[2];
            ipassRead.preBUSRoute = new byte[2];
            ipassRead.preBUSTime = new byte[4];
            ipassRead.preBUSNo = new byte[2];
            ipassRead.speHUsedNo = new byte[2];
            ipassRead.cardTxnsNumber = new byte[2];
            ipassRead.preGenTime = new byte[4];
            ipassRead.preTxnsValue = new byte[2];
            ipassRead.prePostValue = new byte[2];
            ipassRead.preTxnsMachine = new byte[4];
            ipassRead.trfID = new byte[2];
            ipassRead.traExpireTime = new byte[4];
            ipassRead.personalTime = new byte[4];
            ipassRead.trfGenTime = new byte[4];
            ipassRead.trfRouteID = new byte[2];
            ipassRead.trfTxnsMachine = new byte[2];
            ipassRead.MaaSTransport = new byte[4];
            ipassRead.MaaSStartDate = new byte[4];
            ipassRead.MaaSEndDate = new byte[4];

            ipassRead.MaaSStartEndStation1.StartStation = new byte[2];
            ipassRead.MaaSStartEndStation1.EndStation = new byte[2];
            ipassRead.MaaSStartEndStation1.RouteID = new byte[2];
            ipassRead.MaaSStartEndStation1.Used.UsedValue = new byte[2];

            ipassRead.MaaSStartEndStation2.StartStation = new byte[2];
            ipassRead.MaaSStartEndStation2.EndStation = new byte[2];
            ipassRead.MaaSStartEndStation2.RouteID = new byte[2];
            ipassRead.MaaSStartEndStation2.Used.UsedValue = new byte[2];

            ipassRead.MaaSStartEndStation3.StartStation = new byte[2];
            ipassRead.MaaSStartEndStation3.EndStation = new byte[2];
            ipassRead.MaaSStartEndStation3.RouteID = new byte[2];
            ipassRead.MaaSStartEndStation3.Used.UsedValue = new byte[2];

            ipassRead.MaaSStartEndStation4.StartStation = new byte[2];
            ipassRead.MaaSStartEndStation4.EndStation = new byte[2];
            ipassRead.MaaSStartEndStation4.RouteID = new byte[2];
            ipassRead.MaaSStartEndStation4.Used.UsedValue = new byte[2];

            ipassRead.CardNumber = new byte[16];
            ipassRead.EnterpriseCode = new byte[2];
            ipassRead.CardTransactionSerialNumber = new byte[2];
            ipassRead.CardExpireTime = new byte[2];


            //=====================================================================
            ipassWrite.csn = new byte[16];
            ipassWrite.txnsValue = new byte[2];
            ipassWrite.txnsTime = new byte[4];
            ipassWrite.txnsMachine = new byte[2];
            ipassWrite.routeID = new byte[2];
            ipassWrite.speHUsedNo = new byte[2];
            ipassWrite.speResetTime = new byte[4];
            ipassWrite.trfID = new byte[2];
            ipassWrite.traExpireTime = new byte[4];
            ipassWrite.welExpireTime = new byte[4];
            ipassWrite.speUseNo = new byte[2];

            ipassWrite.MaaSStartEndStation1.StartStation = new byte[2];
            ipassWrite.MaaSStartEndStation1.EndStation = new byte[2];
            ipassWrite.MaaSStartEndStation1.RouteID = new byte[2];
            ipassWrite.MaaSStartEndStation1.Used.UsedValue = new byte[2];

            ipassWrite.MaaSStartEndStation2.StartStation = new byte[2];
            ipassWrite.MaaSStartEndStation2.EndStation = new byte[2];
            ipassWrite.MaaSStartEndStation2.RouteID = new byte[2];
            ipassWrite.MaaSStartEndStation2.Used.UsedValue = new byte[2];

            ipassWrite.MaaSStartEndStation3.StartStation = new byte[2];
            ipassWrite.MaaSStartEndStation3.EndStation = new byte[2];
            ipassWrite.MaaSStartEndStation3.RouteID = new byte[2];
            ipassWrite.MaaSStartEndStation3.Used.UsedValue = new byte[2];

            ipassWrite.MaaSStartEndStation4.StartStation = new byte[2];
            ipassWrite.MaaSStartEndStation4.EndStation = new byte[2];
            ipassWrite.MaaSStartEndStation4.RouteID = new byte[2];
            ipassWrite.MaaSStartEndStation4.Used.UsedValue = new byte[2];
        }
        public void BytesToReadStruct(byte[] Recieved)//比文件中總byte數多1
        {
            Array.Copy(Recieved, 5, ipassRead.csn, 0, 16);
            Array.Copy(Recieved, 21, ipassRead.evValue, 0, 4);
            Array.Copy(Recieved, 25, ipassRead.bevValue, 0, 4);
            Array.Copy(Recieved, 29, ipassRead.syncValue, 0, 4);
            Array.Copy(Recieved, 33, ipassRead.persionalID, 0, 6);
            ipassRead.cardType = Recieved[39];
            ipassRead.speProviderType = Recieved[40];
            ipassRead.speProviderID = Recieved[41];
            Array.Copy(Recieved, 42, ipassRead.speActivationTime, 0, 4);
            Array.Copy(Recieved, 46, ipassRead.speExipreTime, 0, 4);
            Array.Copy(Recieved, 50, ipassRead.speResetTime, 0, 4);
            ipassRead.spestStation = Recieved[54];
            ipassRead.speedStation = Recieved[55];
            Array.Copy(Recieved, 56, ipassRead.speRouteID, 0, 2);
            Array.Copy(Recieved, 58, ipassRead.speUseNo, 0, 2);
            ipassRead.busConveyancer = Recieved[60];
            ipassRead.preBUSAreaCode = Recieved[61];
            ipassRead.preBUSCompID = Recieved[62];
            Array.Copy(Recieved, 63, ipassRead.preBUSRoute, 0, 2);
            ipassRead.preBUSStation = Recieved[65];
            Array.Copy(Recieved, 66, ipassRead.preBUSTime, 0, 4);
            ipassRead.preBUSStatus = Recieved[70];
            ipassRead.preTravelType = Recieved[71];
            Array.Copy(Recieved, 72, ipassRead.preBUSNo, 0, 2);
            Array.Copy(Recieved, 74, ipassRead.speHUsedNo, 0, 2);
            Array.Copy(Recieved, 76, ipassRead.cardTxnsNumber, 0, 2);
            Array.Copy(Recieved, 78, ipassRead.preGenTime, 0, 4);
            ipassRead.preGenClass = Recieved[82];
            Array.Copy(Recieved, 83, ipassRead.preTxnsValue, 0, 2);
            Array.Copy(Recieved, 85, ipassRead.prePostValue, 0, 2);
            ipassRead.preGenSystem = Recieved[87];
            ipassRead.preTxnsLocaion = Recieved[88];
            Array.Copy(Recieved, 89, ipassRead.preTxnsMachine, 0, 4);
            ipassRead.preSale = Recieved[93];
            ipassRead.syncStatus = Recieved[94];
            Array.Copy(Recieved, 95, ipassRead.trfID, 0, 2);
            ipassRead.transferFlag = Recieved[97];
            ipassRead.travelDay = Recieved[98];
            Array.Copy(Recieved, 99, ipassRead.traExpireTime, 0, 4);
            Array.Copy(Recieved, 103, ipassRead.personalTime, 0, 4);
            Array.Copy(Recieved, 107, ipassRead.trfGenTime, 0, 4);
            ipassRead.trfspeCurrentSystemID = Recieved[111];
            ipassRead.trfprePreviousSystemID = Recieved[112];
            ipassRead.trfTxnsType = Recieved[113];
            ipassRead.trfCompID = Recieved[114];
            ipassRead.trfTxnsLocation = Recieved[115];
            Array.Copy(Recieved, 116, ipassRead.trfRouteID, 0, 2);
            Array.Copy(Recieved, 118, ipassRead.trfTxnsMachine, 0, 2);
            ipassRead.regFlag = Recieved[120];
            ipassRead.cardStatus = Recieved[121];
            ipassRead.identifyRegStudentCard = Recieved[122];
            ipassRead.monthlyCompID = Recieved[123];
            if (Len > 127)
                ipassRead.evenRideBit = Recieved[124];
            if (Len > 128)
            {
                ipassRead.MaaSCard = Recieved[125];
                ipassRead.MappingType = Recieved[126];
                ipassRead.MaaSAreaCode = Recieved[127];
                Array.Copy(Recieved, 128, ipassRead.MaaSTransport, 0, 4);
                ipassRead.MaaSPeriodCode = Recieved[132];
                ipassRead.MaaSPeriod = Recieved[133];
                Array.Copy(Recieved, 134, ipassRead.MaaSStartDate, 0, 4);
                Array.Copy(Recieved, 138, ipassRead.MaaSEndDate, 0, 4);

                ipassRead.MaaSStartEndStation1.TransportSetting = Recieved[142];
                Array.Copy(Recieved, 143, ipassRead.MaaSStartEndStation1.StartStation, 0, 2);
                Array.Copy(Recieved, 145, ipassRead.MaaSStartEndStation1.EndStation, 0, 2);
                Array.Copy(Recieved, 147, ipassRead.MaaSStartEndStation1.RouteID, 0, 2);
                ipassRead.MaaSStartEndStation1.Used.MaxUsage = Recieved[149];
                ipassRead.MaaSStartEndStation1.Used.RemainingUse = Recieved[150];
                Array.Copy(Recieved, 151, ipassRead.MaaSStartEndStation1.Used.UsedValue, 0, 2);

                ipassRead.MaaSStartEndStation2.TransportSetting = Recieved[153];
                Array.Copy(Recieved, 154, ipassRead.MaaSStartEndStation2.StartStation, 0, 2);
                Array.Copy(Recieved, 156, ipassRead.MaaSStartEndStation2.EndStation, 0, 2);
                Array.Copy(Recieved, 158, ipassRead.MaaSStartEndStation2.RouteID, 0, 2);
                ipassRead.MaaSStartEndStation2.Used.MaxUsage = Recieved[160];
                ipassRead.MaaSStartEndStation2.Used.RemainingUse = Recieved[161];
                Array.Copy(Recieved, 162, ipassRead.MaaSStartEndStation2.Used.UsedValue, 0, 2);

                ipassRead.MaaSStartEndStation3.TransportSetting = Recieved[164];
                Array.Copy(Recieved, 165, ipassRead.MaaSStartEndStation3.StartStation, 0, 2);
                Array.Copy(Recieved, 167, ipassRead.MaaSStartEndStation3.EndStation, 0, 2);
                Array.Copy(Recieved, 169, ipassRead.MaaSStartEndStation3.RouteID, 0, 2);
                ipassRead.MaaSStartEndStation3.Used.MaxUsage = Recieved[171];
                ipassRead.MaaSStartEndStation3.Used.RemainingUse = Recieved[172];
                Array.Copy(Recieved, 173, ipassRead.MaaSStartEndStation3.Used.UsedValue, 0, 2);

                ipassRead.MaaSStartEndStation4.TransportSetting = Recieved[175];
                Array.Copy(Recieved, 176, ipassRead.MaaSStartEndStation4.StartStation, 0, 2);
                Array.Copy(Recieved, 178, ipassRead.MaaSStartEndStation4.EndStation, 0, 2);
                Array.Copy(Recieved, 180, ipassRead.MaaSStartEndStation4.RouteID, 0, 2);
                ipassRead.MaaSStartEndStation4.Used.MaxUsage = Recieved[182];
                ipassRead.MaaSStartEndStation4.Used.RemainingUse = Recieved[183];
                Array.Copy(Recieved, 184, ipassRead.MaaSStartEndStation4.Used.UsedValue, 0, 2);

                Array.Copy(Recieved, 186, ipassRead.CardNumber, 0, 15);//應該為16


            }
        }
        public byte[] GetWriteStruct()//須修改
        {
            var MemStream = new MemoryStream();
            var Binwriter = new BinaryWriter(MemStream);
            int unixTime = Convert.ToInt32(DateTimeOffset.Now.ToUnixTimeSeconds() + 28800);

            ipassWrite.txnsTime = BitConverter.GetBytes(unixTime);
            ipassWrite.csn = ipassRead.csn;//16
            ipassWrite.txnsSystem = ipassRead.preGenSystem;
            ipassWrite.txnsLocation = ipassRead.preTxnsLocaion;
            Array.Copy(ipassRead.preTxnsMachine, 2, ipassWrite.txnsMachine, 0, 2);
            ipassWrite.compID = ipassRead.preBUSCompID;
            ipassWrite.routeID = ipassRead.preBUSRoute;
            ipassWrite.travelType = 0x01;
            ipassWrite.speHUsedNo = ipassRead.speHUsedNo;
            ipassWrite.speResetTime = ipassRead.speResetTime;
            ipassWrite.areaCode = ipassRead.preBUSAreaCode;
            ipassWrite.trfID = ipassRead.trfID;
            ipassWrite.travelType = 0x01;
            if (ipassRead.preBUSStatus == 0x00)
            {
                ipassWrite.txnsType = 0x00;
            }
            else
            {
                ipassWrite.txnsType = 0x10;
            }
            Binwriter.Write(ipassWrite.csn);
            Binwriter.Write(ipassWrite.txnsValue);
            Binwriter.Write(ipassWrite.txnsTime);
            Binwriter.Write(ipassWrite.txnsType);
            Binwriter.Write(ipassWrite.txnsSystem);
            Binwriter.Write(ipassWrite.txnsLocation);
            Binwriter.Write(ipassWrite.txnsMachine);
            Binwriter.Write(ipassWrite.compID);
            Binwriter.Write(ipassWrite.routeID);
            Binwriter.Write(ipassWrite.travelType);
            Binwriter.Write(ipassWrite.speHUsedNo);
            Binwriter.Write(ipassWrite.speResetTime);
            Binwriter.Write(ipassWrite.permitFlag);
            Binwriter.Write(ipassWrite.areaCode);
            Binwriter.Write(ipassWrite.trfID);
            Binwriter.Write(ipassWrite.txnspreSale);
            Binwriter.Write(ipassWrite.traExpireTime);
            Binwriter.Write(ipassWrite.welExpireTime);
            Binwriter.Write(ipassWrite.speUseNo);
            Binwriter.Write(ipassWrite.incrEvenRideBit);

            if (Len > 128) //須修改
            {
                /*
                ipassWrite.MaaSStartEndStation1 = ipassRead.MaaSStartEndStation1;
                ipassWrite.MaaSStartEndStation2 = ipassRead.MaaSStartEndStation2;
                ipassWrite.MaaSStartEndStation3 = ipassRead.MaaSStartEndStation3;
                ipassWrite.MaaSStartEndStation4 = ipassRead.MaaSStartEndStation4;
                //MaaSStartEndStation1
                Binwriter.Write(ipassWrite.MaaSStartEndStation1.TransportSetting);
                Binwriter.Write(ipassWrite.MaaSStartEndStation1.StartStation);
                Binwriter.Write(ipassWrite.MaaSStartEndStation1.EndStation);
                Binwriter.Write(ipassWrite.MaaSStartEndStation1.RouteID);
                Binwriter.Write(ipassWrite.MaaSStartEndStation1.Used.MaxUsage);
                Binwriter.Write(ipassWrite.MaaSStartEndStation1.Used.RemainingUse);
                Binwriter.Write(ipassWrite.MaaSStartEndStation1.Used.UsedValue);
                //MaaSStartEndStation2
                Binwriter.Write(ipassWrite.MaaSStartEndStation2.TransportSetting);
                Binwriter.Write(ipassWrite.MaaSStartEndStation2.StartStation);
                Binwriter.Write(ipassWrite.MaaSStartEndStation2.EndStation);
                Binwriter.Write(ipassWrite.MaaSStartEndStation2.RouteID);
                Binwriter.Write(ipassWrite.MaaSStartEndStation2.Used.MaxUsage);
                Binwriter.Write(ipassWrite.MaaSStartEndStation2.Used.RemainingUse);
                Binwriter.Write(ipassWrite.MaaSStartEndStation2.Used.UsedValue);
                //MaaSStartEndStation3
                Binwriter.Write(ipassWrite.MaaSStartEndStation3.TransportSetting);
                Binwriter.Write(ipassWrite.MaaSStartEndStation3.StartStation);
                Binwriter.Write(ipassWrite.MaaSStartEndStation3.EndStation);
                Binwriter.Write(ipassWrite.MaaSStartEndStation3.RouteID);
                Binwriter.Write(ipassWrite.MaaSStartEndStation3.Used.MaxUsage);
                Binwriter.Write(ipassWrite.MaaSStartEndStation3.Used.RemainingUse);
                Binwriter.Write(ipassWrite.MaaSStartEndStation3.Used.UsedValue);
                //MaaSStartEndStation4
                Binwriter.Write(ipassWrite.MaaSStartEndStation4.TransportSetting);
                Binwriter.Write(ipassWrite.MaaSStartEndStation4.StartStation);
                Binwriter.Write(ipassWrite.MaaSStartEndStation4.EndStation);
                Binwriter.Write(ipassWrite.MaaSStartEndStation4.RouteID);
                Binwriter.Write(ipassWrite.MaaSStartEndStation4.Used.MaxUsage);
                Binwriter.Write(ipassWrite.MaaSStartEndStation4.Used.RemainingUse);
                Binwriter.Write(ipassWrite.MaaSStartEndStation4.Used.UsedValue);
                */
            }


            return MemStream.ToArray();
        }
        public byte Get_preBUSStatus()
        {
            return ipassRead.preBUSStatus;
        }
        public int Get_evValue()
        {
            return BitConverter.ToInt32(ipassRead.evValue, 0);
        }
        
        public void ConvertDoubleByte(double doubleVal)
        {
            byte byteVal = 0;

            // Double to byte conversion can overflow.
            try
            {
                byteVal = System.Convert.ToByte(doubleVal);
                System.Console.WriteLine("{0} as a byte is: {1}.",
                    doubleVal, byteVal);
            }
            catch (System.OverflowException)
            {
                System.Console.WriteLine(
                    "Overflow in double-to-byte conversion.");
            }

            // Byte to double conversion cannot overflow.
            doubleVal = System.Convert.ToDouble(byteVal);
            System.Console.WriteLine("{0} as a double is: {1}.",
                byteVal, doubleVal);
        }


    }
    public class icash
    {
        private ICASH_Record IcashRead;
        private ICASH_Txns_Mile IcashWrite;
       
        //=====================================================================
        public void initialization()
        {
            IcashRead.Card_No = new byte[8];
            IcashRead.Card_Balance = new byte[4];
            IcashRead.Card_TSN = new byte[4];
            IcashRead.Card_Expiration_Date = new byte[4];
            IcashRead.Points_of_Card = new byte[2];
            IcashRead.PersonalID = new byte[10];
            IcashRead.Begin_Date = new byte[4];
            IcashRead.Expiration_Date = new byte[4];
            IcashRead.Reset_Dat = new byte[4];
            IcashRead.Limit_Counts = new byte[4];
            IcashRead.TransferTime = new byte[4];
            IcashRead.TransferDiscount = new byte[2];
            IcashRead.TransferStationID = new byte[2];
            IcashRead.TransferDeviceID = new byte[4];
            IcashRead.ZoneTxnTime = new byte[4];
            IcashRead.ZoneRouteNo = new byte[2];
            IcashRead.ZoneTxnCTSN = new byte[4];
            IcashRead.ZoneTxnAmt = new byte[2];
            IcashRead.ZoneStationID = new byte[2];
            IcashRead.ZoneDeviceID = new byte[4];
            IcashRead.MileTxnTime = new byte[4];
            IcashRead.MileRouteNo = new byte[2];
            IcashRead.MileTxnCTSN = new byte[4];
            IcashRead.MileTxnAmt = new byte[2];
            IcashRead.MileStationID = new byte[2];
            IcashRead.MileDeviceID = new byte[4];
            IcashRead.Receivables = new byte[2];
            IcashRead.RideDate = new byte[4];
            //=====================================================================
            IcashWrite.DateTime = new byte[4];
            IcashWrite.Amount = new byte[2];
            IcashWrite.Receivables = new byte[2];
            IcashWrite.RouteNo = new byte[2];
            IcashWrite.Current_Station = new byte[2];
            IcashWrite.TransferDiscount = new byte[2];
            IcashWrite.DeviceID = new byte[4];
            IcashWrite.Vehicles_type = new byte[2];
            IcashWrite.Channel_Type = new byte[3];
            IcashWrite.SocialPntUsed = new byte[2];
            IcashWrite.SocialDiscount = new byte[2];
            IcashWrite.SocialResetDate = new byte[4];
            IcashWrite.RideDate = new byte[4];
            IcashWrite.TradePoint = new byte[2];
        }
        public void BytesToReadStruct(byte[] Recieved)
        {
            Array.Copy(Recieved, 5, IcashRead.Card_No, 0, 8);
            Array.Copy(Recieved, 13, IcashRead.Card_Balance, 0, 4);
            Array.Copy(Recieved, 17, IcashRead.Card_TSN, 0, 4);
            IcashRead.Card_Type = Recieved[21];
            Array.Copy(Recieved, 22, IcashRead.Card_Expiration_Date, 0, 4);
            IcashRead.Kind_of_the_ticket = Recieved[26];
            IcashRead.Area_Code = Recieved[27];
            Array.Copy(Recieved, 28, IcashRead.Points_of_Card, 0, 2);
            Array.Copy(Recieved, 30, IcashRead.PersonalID, 0, 10);
            IcashRead.ID_Code = Recieved[40];
            IcashRead.Card_Issuer = Recieved[41];
            Array.Copy(Recieved, 42, IcashRead.Begin_Date, 0, 4);
            Array.Copy(Recieved, 46, IcashRead.Expiration_Date, 0, 4);
            Array.Copy(Recieved, 50, IcashRead.Reset_Dat, 0, 4);
            Array.Copy(Recieved, 54, IcashRead.Limit_Counts, 0, 4);
            IcashRead.LastTransferCode = Recieved[58];
            IcashRead.CurrentTransferCode = Recieved[59];
            Array.Copy(Recieved, 60, IcashRead.TransferTime, 0, 4);
            IcashRead.TransferSysID = Recieved[64];
            IcashRead.TransferSpID = Recieved[65];
            IcashRead.TransferTxnType = Recieved[66];
            Array.Copy(Recieved, 67, IcashRead.TransferDiscount, 0, 2);
            Array.Copy(Recieved, 69, IcashRead.TransferStationID, 0, 2);
            Array.Copy(Recieved, 71, IcashRead.TransferDeviceID, 0, 4);
            IcashRead.ZoneTxnSysID = Recieved[75];
            IcashRead.ZoneCode = Recieved[76];
            Array.Copy(Recieved, 77, IcashRead.ZoneTxnTime, 0, 4);
            Array.Copy(Recieved, 81, IcashRead.ZoneRouteNo, 0, 2);
            Array.Copy(Recieved, 83, IcashRead.ZoneTxnCTSN, 0, 4);
            Array.Copy(Recieved, 87, IcashRead.ZoneTxnAmt, 0, 2);
            IcashRead.ZoneEntryStatus = Recieved[89];
            IcashRead.ZoneTxnType = Recieved[90];
            IcashRead.ZoneDirection = Recieved[91];
            IcashRead.ZoneSpID = Recieved[92];
            Array.Copy(Recieved, 93, IcashRead.ZoneStationID, 0, 2);
            Array.Copy(Recieved, 95, IcashRead.ZoneDeviceID, 0, 4);
            IcashRead.MileTxnSysID = Recieved[99];
            Array.Copy(Recieved, 100, IcashRead.MileTxnTime, 0, 4);
            Array.Copy(Recieved, 104, IcashRead.MileRouteNo, 0, 2);
            Array.Copy(Recieved, 106, IcashRead.MileTxnCTSN, 0, 4);
            Array.Copy(Recieved, 110, IcashRead.MileTxnAmt, 0, 2);
            IcashRead.MileEntryStatus = Recieved[112];
            IcashRead.MileTxnType = Recieved[113];
            IcashRead.MileTxnMode = Recieved[114];
            IcashRead.MileDirection = Recieved[115];
            IcashRead.MileSpID = Recieved[116];
            Array.Copy(Recieved, 117, IcashRead.MileStationID, 0, 2);
            Array.Copy(Recieved, 119, IcashRead.MileDeviceID, 0, 4);
            Array.Copy(Recieved, 123, IcashRead.Receivables, 0, 2);
            IcashRead.RideCounts = Recieved[125];
            Array.Copy(Recieved, 126, IcashRead.RideDate, 0, 4);
        }
        public byte[] GetWriteStruct()
        {
            var MemStream = new MemoryStream();
            var Binwriter = new BinaryWriter(MemStream);
            int unixTime = Convert.ToInt32(DateTimeOffset.Now.ToUnixTimeSeconds() + 28800);
            IcashWrite.Trans_Mode = 0x11;
            IcashWrite.DateTime = BitConverter.GetBytes(unixTime);
            IcashWrite.Direction = 0x01;
            IcashWrite.Channel_Type[0] = 0x42;
            IcashWrite.Channel_Type[1] = 0x55;
            IcashWrite.Channel_Type[2] = 0x53;
            IcashWrite.RouteNo = IcashRead.MileRouteNo;
            IcashWrite.Current_Station = IcashRead.MileStationID;
            IcashWrite.TransferGroupCode_Before = IcashRead.LastTransferCode;
            IcashWrite.TransferGroupCode = IcashRead.CurrentTransferCode;
            IcashWrite.SysID = IcashRead.MileTxnSysID;
            IcashWrite.CompanyID = IcashRead.MileSpID;
            IcashWrite.DeviceID = IcashRead.MileDeviceID;
            IcashWrite.RideCounts = IcashRead.RideCounts;
            IcashWrite.RideDate = IcashRead.RideDate;
            if (IcashRead.MileEntryStatus == 0x00)
            {
                IcashWrite.Trans_Type = 0x00;
                IcashWrite.Trans_Status = 0x01;
            }
            else
            {

                IcashWrite.Trans_Type = 0x10;
                IcashWrite.Trans_Status = 0x00;
            }
            Binwriter.Write(IcashWrite.Trans_Mode);
            Binwriter.Write(IcashWrite.Trans_Type);
            Binwriter.Write(IcashWrite.DateTime);
            Binwriter.Write(IcashWrite.Amount);
            Binwriter.Write(IcashWrite.Receivables);
            Binwriter.Write(IcashWrite.Direction);
            Binwriter.Write(IcashWrite.Trans_Status);
            Binwriter.Write(IcashWrite.RouteNo);
            Binwriter.Write(IcashWrite.Current_Station);
            Binwriter.Write(IcashWrite.TransferGroupCode_Before);
            Binwriter.Write(IcashWrite.TransferGroupCode);
            Binwriter.Write(IcashWrite.TransferDiscount);
            Binwriter.Write(IcashWrite.SysID);
            Binwriter.Write(IcashWrite.CompanyID);
            Binwriter.Write(IcashWrite.DeviceID);
            Binwriter.Write(IcashWrite.Vehicles_type);
            Binwriter.Write(IcashWrite.Channel_Type);
            Binwriter.Write(IcashWrite.SocialPntUsed);
            Binwriter.Write(IcashWrite.SocialDiscount);
            Binwriter.Write(IcashWrite.SocialResetDate);
            Binwriter.Write(IcashWrite.RideCounts);
            Binwriter.Write(IcashWrite.RideDate);
            Binwriter.Write(IcashWrite.TradePoint);
            return MemStream.ToArray();
        }
        public int Get_Card_Balance()
        {
            return BitConverter.ToInt32(IcashRead.Card_Balance, 0);
        }
        public byte Get_Trans_Type()
        {
            return IcashWrite.Trans_Type;
        }
       
    }
    public class EasyCard
    {
        private DS_CSC_READ_FOR_MILAGE_BV_7B EasyCardRead;
        private DS_CSC_MILAGE_DEDUCT_IN_V3 EasyCardWrite;
        //=====================================================================
        public void initialization()
        {
            EasyCardRead.cmd_manufacture_serial_number = new byte[4];
            EasyCardRead.cid_begin_time = new byte[4];
            EasyCardRead.cid_expiry_time = new byte[4];
            EasyCardRead.gsp_autopay_value = new byte[2];
            EasyCardRead.gsp_max_ev = new byte[2];
            EasyCardRead.gsp_max_deduct_value = new byte[2];
            EasyCardRead.gsp_profile_expiry_date = new byte[2];
            EasyCardRead.cpd_social_security_code = new byte[6];
            EasyCardRead.cpd_deposit = new byte[2];
            EasyCardRead.ev = new byte[2];
            EasyCardRead.tsd_transaction_sequence_number = new byte[2];
            EasyCardRead.tsd_loyalty_points = new byte[2];
            EasyCardRead.tsd_add_value_accumulated_points = new byte[2];
            EasyCardRead.urt_transfer_group_code_new = new byte[2];
            EasyCardRead.urt_transaction_date_and_time = new byte[4];
            EasyCardRead.urt_transfer_discount = new byte[2];
            EasyCardRead.urt_ev_afetr_transfer = new byte[2];
            EasyCardRead.urt_transaction_equipment_id = new byte[4];
            EasyCardRead.busfix_first_possible_utilization_date = new byte[2];
            EasyCardRead.busfix_last_possible_utilization_date = new byte[2];
            EasyCardRead.busfix_vip_points = new byte[2];
            EasyCardRead.busvar_date_of_first_transaction = new byte[2];
            EasyCardRead.busvar_device_serial_number = new byte[2];
            EasyCardRead.busvar_transaction_date_and_time = new byte[4];
            EasyCardRead.busvar_value_of_transaction = new byte[2];
            EasyCardRead.busvar_free_transaction_date_and_time = new byte[4];
            EasyCardRead.busvar_accumulated_free_rides2 = new byte[2];
            EasyCardRead.busvar_free_transaction_date_and_time2 = new byte[2];
            EasyCardRead.var_transaction_date_time = new byte[4];
            EasyCardRead.var_value_of_transaction = new byte[2];
            EasyCardRead.var_ev_afetr_transaction = new byte[2];
            EasyCardRead.var_transaction_device_id = new byte[4];
            //===========================================
            EasyCardWrite.utr_transaction_date_time = new byte[4];
            EasyCardWrite.cmd_manufacture_serial_number = new byte[4];
            EasyCardWrite.deducted_value = new byte[2];
            EasyCardWrite.urt_transfer_date_time = new byte[4];
            EasyCardWrite.urt_transfer_discount = new byte[2];
            EasyCardWrite.urt_transfer_group_code_new = new byte[2];
            EasyCardWrite.busvar_date_of_first_transaction = new byte[2];
            EasyCardWrite.busvar_line2_number = new byte[2];
        }
        public void BytesToReadStruct(byte[] Recieved)
        {
            Array.Copy(Recieved, 5, EasyCardRead.cmd_manufacture_serial_number, 0, 4);
            EasyCardRead.cid_issuer_code = Recieved[9];
            Array.Copy(Recieved, 10, EasyCardRead.cid_begin_time, 0, 4);
            Array.Copy(Recieved, 14, EasyCardRead.cid_expiry_time, 0, 4);
            EasyCardRead.cid_status = Recieved[18];
            EasyCardRead.gsp_autopay_flag = Recieved[19];
            Array.Copy(Recieved, 20, EasyCardRead.gsp_autopay_value, 0, 2);
            Array.Copy(Recieved, 22, EasyCardRead.gsp_max_ev, 0, 2);
            Array.Copy(Recieved, 24, EasyCardRead.gsp_max_deduct_value, 0, 2);
            EasyCardRead.gsp_personal_profile = Recieved[26];
            Array.Copy(Recieved, 27, EasyCardRead.gsp_profile_expiry_date, 0, 2);
            EasyCardRead.gsp_area_code = Recieved[29];
            EasyCardRead.gsp_bank_code = Recieved[30];
            EasyCardRead.area_auth_flag = Recieved[31];
            EasyCardRead.special_ticket_type = Recieved[32];
            Array.Copy(Recieved, 33, EasyCardRead.cpd_social_security_code, 0, 6);
            Array.Copy(Recieved, 39, EasyCardRead.cpd_deposit, 0, 2);
            Array.Copy(Recieved, 41, EasyCardRead.ev, 0, 2);
            Array.Copy(Recieved, 43, EasyCardRead.tsd_transaction_sequence_number, 0, 2);
            Array.Copy(Recieved, 45, EasyCardRead.tsd_loyalty_points, 0, 2);
            Array.Copy(Recieved, 47, EasyCardRead.tsd_add_value_accumulated_points, 0, 2);
            EasyCardRead.urt_transaction_sequence_number_LSB = Recieved[49];
            Array.Copy(Recieved, 50, EasyCardRead.urt_transfer_group_code_new, 0, 2);
            Array.Copy(Recieved, 52, EasyCardRead.urt_transaction_date_and_time, 0, 4);
            EasyCardRead.urt_transaction_type = Recieved[56];
            Array.Copy(Recieved, 57, EasyCardRead.urt_transfer_discount, 0, 2);
            Array.Copy(Recieved, 59, EasyCardRead.urt_ev_afetr_transfer, 0, 2);
            EasyCardRead.urt_transfer_group_code = Recieved[61];
            EasyCardRead.urt_transaction_location_code = Recieved[62];
            Array.Copy(Recieved, 63, EasyCardRead.urt_transaction_equipment_id, 0, 4);
            EasyCardRead.busfix_fare_product_company = Recieved[67];
            EasyCardRead.busfix_fare_product_kind = Recieved[68];
            EasyCardRead.busfix_fare_product_type = Recieved[69];
            Array.Copy(Recieved, 70, EasyCardRead.busfix_first_possible_utilization_date, 0, 2);
            Array.Copy(Recieved, 72, EasyCardRead.busfix_last_possible_utilization_date, 0, 2);
            EasyCardRead.busfix_number_of_use = Recieved[74];
            EasyCardRead.busfix_duration_of_use = Recieved[75];
            EasyCardRead.busfix_authorized_lines = Recieved[76];
            EasyCardRead.busfix_authorized_groups = Recieved[77];
            EasyCardRead.busfix_stop1_number = Recieved[78];
            EasyCardRead.busfix_stop2_number = Recieved[79];
            Array.Copy(Recieved, 80, EasyCardRead.busfix_vip_points, 0, 2);
            EasyCardRead.busvar_current_used_number = Recieved[82];
            Array.Copy(Recieved, 83, EasyCardRead.busvar_date_of_first_transaction, 0, 2);
            EasyCardRead.busvar_milage_forbiddance_flag = Recieved[85];
            EasyCardRead.busvar_special_permission = Recieved[86];
            EasyCardRead.busvar_travel_to_or_from = Recieved[87];
            EasyCardRead.busvar_get_on_or_off = Recieved[88];
            EasyCardRead.busvar_company_number = Recieved[89];
            Array.Copy(Recieved, 90, EasyCardRead.busvar_device_serial_number, 0, 2);
            EasyCardRead.busvar_line_number = Recieved[92];
            Array.Copy(Recieved, 93, EasyCardRead.busvar_transaction_date_and_time, 0, 4);
            EasyCardRead.busvar_stop_number = Recieved[97];
            Array.Copy(Recieved, 98, EasyCardRead.busvar_value_of_transaction, 0, 2);
            EasyCardRead.busvar_travel_mode = Recieved[100];
            EasyCardRead.busvar_vip_accumulated_points1 = Recieved[101];
            EasyCardRead.busvar_vip_accumulated_points2 = Recieved[102];
            EasyCardRead.busvar_accumulated_free_rides = Recieved[103];
            Array.Copy(Recieved, 104, EasyCardRead.busvar_free_transaction_date_and_time, 0, 4);
            Array.Copy(Recieved, 108, EasyCardRead.busvar_accumulated_free_rides2, 0, 2);
            Array.Copy(Recieved, 110, EasyCardRead.busvar_free_transaction_date_and_time2, 0, 2);
            EasyCardRead.var_transaction_number_lsb = Recieved[112];
            Array.Copy(Recieved, 113, EasyCardRead.var_transaction_date_time, 0, 4);
            EasyCardRead.var_transaction_type = Recieved[117];
            Array.Copy(Recieved, 118, EasyCardRead.var_value_of_transaction, 0, 2);
            Array.Copy(Recieved, 120, EasyCardRead.var_ev_afetr_transaction, 0, 2);
            EasyCardRead.var_operator_code = Recieved[122];
            EasyCardRead.var_transaction_location_code = Recieved[123];
            Array.Copy(Recieved, 124, EasyCardRead.var_transaction_device_id, 0, 4);
        }
        public byte[] GetWriteStruct()
        {
            var MemStream = new MemoryStream();
            var Binwriter = new BinaryWriter(MemStream);
            int unixTime = Convert.ToInt32(DateTimeOffset.Now.ToUnixTimeSeconds() + 28800);
            EasyCardWrite.message_type = 0x01;
            EasyCardWrite.utr_transaction_date_time = BitConverter.GetBytes(unixTime);
            EasyCardWrite.cmd_manufacture_serial_number = EasyCardRead.cmd_manufacture_serial_number;
            EasyCardWrite.urt_transfer_date_time = EasyCardRead.urt_transaction_date_and_time;
            EasyCardWrite.urt_transfer_discount = EasyCardRead.urt_transfer_discount;
            EasyCardWrite.urt_transfer_group_code = EasyCardRead.urt_transfer_group_code;
            EasyCardWrite.urt_transfer_group_code_new = EasyCardRead.urt_transfer_group_code_new;
            EasyCardWrite.busvar_current_used_number = EasyCardRead.busvar_current_used_number;
            EasyCardWrite.busvar_date_of_first_transaction = EasyCardRead.busvar_date_of_first_transaction;
            EasyCardWrite.busvar_travel_to_or_from = EasyCardRead.busvar_travel_to_or_from;
            EasyCardWrite.busvar_line1_number = EasyCardRead.busvar_line_number;
            EasyCardWrite.busvar_line2_number[0] = EasyCardRead.busvar_line_number;
            EasyCardWrite.busvar_stop_number = EasyCardRead.busvar_stop_number;
            EasyCardWrite.busvar_travel_mode = 0x01;
            EasyCardWrite.busvar_vip_accumulated_points1 = EasyCardRead.busvar_vip_accumulated_points1;
            EasyCardWrite.busvar_vip_accumulated_points2 = EasyCardRead.busvar_vip_accumulated_points2;
            if (EasyCardRead.busvar_get_on_or_off == 0x00)
            {
                EasyCardWrite.busvar_get_on_or_off = 0x15;
                EasyCardWrite.busvar_transaction_type = 0x00;
            }
            else
            {
                EasyCardWrite.busvar_get_on_or_off = 0x14;
                EasyCardWrite.busvar_transaction_type = 0x10;
            }
            Binwriter.Write(EasyCardWrite.message_type);
            Binwriter.Write(EasyCardWrite.utr_transaction_date_time);
            Binwriter.Write(EasyCardWrite.cmd_manufacture_serial_number);
            Binwriter.Write(EasyCardWrite.deducted_value);
            Binwriter.Write(EasyCardWrite.urt_transfer_date_time);
            Binwriter.Write(EasyCardWrite.urt_transfer_discount);
            Binwriter.Write(EasyCardWrite.urt_transfer_group_code);
            Binwriter.Write(EasyCardWrite.urt_transfer_group_code_new);
            Binwriter.Write(EasyCardWrite.busvar_current_used_number);
            Binwriter.Write(EasyCardWrite.busvar_date_of_first_transaction);
            Binwriter.Write(EasyCardWrite.busvar_milage_forbiddance_flag);
            Binwriter.Write(EasyCardWrite.busvar_special_permission);
            Binwriter.Write(EasyCardWrite.busvar_travel_to_or_from);
            Binwriter.Write(EasyCardWrite.busvar_get_on_or_off);
            Binwriter.Write(EasyCardWrite.busvar_line1_number);
            Binwriter.Write(EasyCardWrite.busvar_line2_number);
            Binwriter.Write(EasyCardWrite.busvar_stop_number);
            Binwriter.Write(EasyCardWrite.busvar_travel_mode);
            Binwriter.Write(EasyCardWrite.busvar_transaction_type);
            Binwriter.Write(EasyCardWrite.busvar_vip_accumulated_points1);
            Binwriter.Write(EasyCardWrite.busvar_vip_accumulated_points2);
            Binwriter.Write(EasyCardWrite.busvar_remaining_rides);
            return MemStream.ToArray();
        }
        public int Get_ev()
        {
            return BitConverter.ToInt16(EasyCardRead.ev, 0);
        }


    }
}
