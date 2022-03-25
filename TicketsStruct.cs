using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsertInfoToDevice
{
    struct UsedAmount       //一卡通Maas StartEndStation使用者結構體
    {
        public byte MaxUsage; // 使用次數上限   MAX 255
        public byte RemainingUse; // 剩餘使用次數   MAX 255
        public byte[] UsedValue; // 累積使用金額   MAX 65535
    }
    struct StartEndStation  //一卡通Maas StartEndStation結構體
    {
        public byte TransportSetting;   // 起訖站交通運具 MAX 255, 若無使用則填入0x00
        public byte[] StartStation;       // 起站 MAX 1023, 若無使用則填入0x00, 0x00
        public byte[] EndStation;         // 訖站 MAX 1023, 若無使用則填入0x00, 0x00
        public byte[] RouteID;            // 路線編號, 若無使用則填入0x00, 0x00
        public UsedAmount Used; // 使用次數 or 累積使用金額, 若無使用則填入0x00, 0x00
    }
    struct KRTC_Read     //一卡通讀卡結構體
    {
        public byte[] csn;           // 卡號，16 bytes
        public byte[] evValue;        // 主要電子票值4
        public byte[] bevValue;       // 備份電子票值4
        public byte[] syncValue;      // 同步後電子票值4
        public byte[] persionalID;        // 身份證字號6
        public byte cardType;          // 公車端票卡種類，1 byte
        public byte speProviderType;       // 特種票識別身分，1 byte
        public byte speProviderID;     // 特種票識別單位，1 byte
        public byte[] speActivationTime;  // 特種票識別起始日，4 bytes(年-2bytes, 月-1byte, 日-1 byte)
        public byte[] speExipreTime;      // 特種票識別有效日，4 bytes(年-2bytes, 月-1byte, 日-1 byte)
        public byte[] speResetTime;       // 特種票重置日期，4 bytes(年-2bytes, 月-1byte, 日-1 byte)
        public byte spestStation;      // 特種票起站代碼，1 byte
        public byte speedStation;      // 特種票迄站代碼，1 byte
        public byte[] speRouteID;     // 特種票路線編號，2 bytes
        public byte[] speUseNo;       // 特種票使用限次，2 bytes
        public byte busConveyancer;        // 公車業者代碼，1 byte，2012.05.14_2.0.04 新增
        public byte preBUSAreaCode;        // 上次公車端區碼，1 byte
        public byte preBUSCompID;      // 上次公車端交易運輸業者，1 byte
        public byte[] preBUSRoute;        // 上次公車端交易路線，2 byte
        public byte preBUSStation;     // 上次公車端交易站號，1 byte
        public byte[] preBUSTime;     // 上次公車端交易時間(UNIX Time)，4 bytes
        public byte preBUSStatus;      // 上次公車端搭乘狀態，0x00-一般(已下車), 0x01-乘車中(已上車)
        public byte preTravelType;     // 上次搭乘類型，1 byte(0x01-錢包搭乘, 0x02-特種票搭乘等等)
        public byte[] preBUSNo;       // 上次公車端交易驗票機編號，2 bytes
        public byte[] speHUsedNo;     // 特種票已使用次數，2 bytes
        public byte[] cardTxnsNumber;     // 卡片交易序號，2 byte，S3B0#1 ，2012.07.07_V2.0.01  新增
        public byte[] preGenTime;     // 上次交易時間(UNIX Time)，4 bytes，S4B0#2
        public byte preGenClass;       // 上次交易類別，1 byte，S4B0#3
        public byte[] preTxnsValue;       // 上次交易票值/票點，2 byte，S4B0#4 ，2012.07.07_V2.0.01  新增
        public byte[] prePostValue;       // 上次交易後票值/票點，2 byte，S4B0#5 ，2012.07.07_V2.0.01  新增
        public byte preGenSystem;      // 上次交易系統編號，1 byte，S4B0#6
        public byte preTxnsLocaion;        // 上次交易地點編號，1 byte，S4B0#7 ，2012.07.07_V2.0.01  新增
        public byte[] preTxnsMachine;     // 上次交易機器編號，4 byte，S4B0#8 ，2012.07.07_V2.0.01  新增
        public byte preSale;           // 計程預收金額，1 byte
        public byte syncStatus;        // 同步狀態，0-無須同步，1-表主蓋備，2-表備蓋主
        public byte[] trfID;          // 轉乘ID 2 byte (Little Endian)
        public byte transferFlag;      // 轉換旗標，1 byte，0表無須轉換，1表A3洗點，2表C3洗卡洗點，3表C4洗卡洗點，4表C5洗卡//2013.09.05_V2.2.0.0
        public byte travelDay;         // 旅遊卡天數，2013.09.05_V2.2.0.0
        public byte[] traExpireTime;      // 旅遊卡有效日，2013.09.05_V2.2.0.0
        public byte[] personalTime;       // 個人化有效日，2013.09.05_V2.2.0.0

        // 2012.05.14 新增_v2.0.04_項目六：新增段次交易所需欄位資料
        // *****START*****
        public byte[] trfGenTime;     // (轉乘識別群組#1)交易時間(UNIX Time)，4 bytes，S3B0#3(byte3~6)
        public byte trfspeCurrentSystemID; // (轉乘識別群組#2)本次系統代碼，1 bytes，S3B0#4(byte7)
        public byte trfprePreviousSystemID;    // (轉乘識別群組#3)前次系統代碼，1 bytes，S3B0#5(byte8)
        public byte trfTxnsType;       // (轉乘識別群組#4)交易類別，1 bytes，S3B0#6(byte9)
        public byte trfCompID;         // (轉乘識別群組#5)交易業者編號，1 bytes，S3B0#7(byte10)
        public byte trfTxnsLocation;       // (轉乘識別群組#6)交易地點編號，1 bytes，S3B0#8(byte11)
        public byte[] trfRouteID;     // (轉乘識別群組#7)交易路線編號，2 bytes，S3B0#8(byte12~13)
        public byte[] trfTxnsMachine;     // (轉乘識別群組#8)交易設備編號，2 bytes，S3B0#9(byte14~15)
                                          // *****END*****

        // 2.2.1.7 <AC01> 新增數位學生證與記名學生卡識別資訊
        public byte regFlag;                   // 記名旗標 (S1B1B14)
        public byte cardStatus;                // 卡片狀態 (S1B0B14)
        public byte identifyRegStudentCard;    // 記名學生卡識別 (見一覽表B 7.2 學生卡綜合判斷說明)
                                               // 0x00表示非記名卡，
                                               // 0x01不記名學生卡已記名(記名學生卡)
                                               // 0x02數位學生證(記名學生卡)

        // 2.2.1.7 <AD01> 新增讀出定期票業者代碼
        public byte monthlyCompID;             // 定期票業者代碼 (S8B0B13)
                                               // 2.2.2.2 Add `evenRideBit' for Taoyuan Citizen Card.
        public byte evenRideBit;

        public byte MaaSCard;      // MaaS卡種類
        public byte MappingType;   // MaaS 0x01
        public byte MaaSAreaCode;      // 區域代碼
        public byte[] MaaSTransport;  // 交通運具
        public byte MaaSPeriodCode;    // 票種旗標
        public byte MaaSPeriod;        // 天數/時數 MAX 255
        public byte[] MaaSStartDate;  // 起始時間 UNIX Time (Little Endian)
        public byte[] MaaSEndDate;    // 結束時間 UNIX Time (Little Endian)
        public StartEndStation MaaSStartEndStation1, MaaSStartEndStation2, MaaSStartEndStation3, MaaSStartEndStation4;// 起迄站資訊
                                                                                                                      //2018.02.13_V2.2.2.4
        public byte[] CardNumber; // 外觀卡號, EV1才會填入
                                  //2020.02.14_V2.2.2.9
        public byte[] EnterpriseCode;// 企業代碼  (Little Endian)
        public byte CardVersion; // 票卡版本
        public byte[] CardTransactionSerialNumber; //票卡交易序號  (Little Endian)
        public byte SixRecordIndex; // 六筆寫入指標
        public byte PersonalType; // 個人身分別
        public byte[] CardExpireTime; //  票卡有效日期 UNIX Time (Little Endian)
        public byte BankID; // 銀行代碼

    }
    struct KRTC_Write    //一卡通寫卡結構體
    {
        public byte[] csn;   // 卡號，16 bytes，如S0B0#1,2
        public byte[] txnsValue;   // 交易金額，2 bytes
        public byte[] txnsTime;   // 交易時間(UNIX Time)，4 bytes
        public byte txnsType;   // 交易類別，1 byte，如S4B0#3
        public byte txnsSystem;   // 交易系統編號，1 byte，如S4B0#6
        public byte txnsLocation;   // 交易地點編號，1 byte，如S4B0#7
        public byte[] txnsMachine;   // 交易機器編號，2 bytes，如S4B0#8
        public byte compID;   // 運輸業者編號，1 byte，目前有14家業者
        public byte[] routeID;   // 交易路線編號，2 bytes，由業者自訂
        public byte travelType;   // 搭乘類型，1 bytes (0x01-錢包搭乘、0x02-特種票搭乘等)
        public byte[] speHUsedNo;   // 特種票已使用次數，2 byte，若無使用則填入0x00, 0x00
        public byte[] speResetTime;   // 特種票重置日期，4 bytes(年-2bytes, 月-1byte, 日-1 byte)
        public byte permitFlag;   // 特許旗標
        public byte areaCode;   // 區碼
        public byte[] trfID;     //轉乘ID 2 byte (Little Endian)
        public byte txnspreSale;   // 計程預收金額，1 byte //2012.05.14 新增_v2.0.04_項目五
        public byte[] traExpireTime;   // 暢遊卡有效日，4 bytes(UNIX Time)，若無使用則寫0，若有使用則填入有效日期, 2013.09.05_V2.2.0.0
        public byte[] welExpireTime;   // 社福卡有效日，4 bytes(UNIX Time)，若無使用則寫0，若有使用則填入有效日期, 2013.09.05_V2.2.0.0
        public byte[] speUseNo;   // 2015.11.26 新增點數上限更新

        // 2.2.2.2 Add `evenRideBit' for Taoyuan Citizen Card.
        public byte incrEvenRideBit; // Refer to `EvenRideBitOptions' below.
        public StartEndStation MaaSStartEndStation1, MaaSStartEndStation2, MaaSStartEndStation3, MaaSStartEndStation4;
    }
    struct ICASH_Record  //愛金卡讀卡結構體
    {
        public byte[] Card_No;              //icasH2.0卡號 (BCD) 8bytes 5
        public byte[] Card_Balance;         //票卡卡片額度 4bytes int 13
        public byte[] Card_TSN;             //票卡交易序號 4bytes uint 17
        public byte Card_Type;            //卡片種類 1byte 21
        public byte[] Card_Expiration_Date; //卡片有效日期 4bytes 22
        public byte Kind_of_the_ticket;   //票卡種類 1byte 26
        public byte Area_Code;            //區碼 1byte 27
        public byte[] Points_of_Card;       //票卡點數(敬老、愛陪) 2bytes ushort 28
        public byte[] PersonalID;           //身分證字號 10bytes 30
        public byte ID_Code;              //身分識別碼 1byte 40
        public byte Card_Issuer;          //發卡單位 1byte 41
        public byte[] Begin_Date;           //啟用日期 4bytes uint 42
        public byte[] Expiration_Date;      //身份有效日期 4bytes uint 46
        public byte[] Reset_Dat;            //重置日期 4bytes uint 50
        public byte[] Limit_Counts;         //使用上限 (BCD) 4bytes 54
        public byte LastTransferCode;     //前次轉乘代碼 1byte 58
        public byte CurrentTransferCode;  //本次轉乘代碼 1byte 59
        public byte[] TransferTime;         //轉乘日期 4bytes uint 60
        public byte TransferSysID;        //轉乘交易系統編號 1byte 64
        public byte TransferSpID;         //轉乘業者代碼 1byte 65
        public byte TransferTxnType;      //轉乘交易類別 1byte 66
        public byte[] TransferDiscount;     //轉乘優惠金額 2bytes ushort 67
        public byte[] TransferStationID;    //轉乘場站代碼 2bytes ushort 69
        public byte[] TransferDeviceID;     //轉乘設備編號 4bytes uint 71
                                            //===========================================================================================
        public byte ZoneTxnSysID;         //前次段次交易系統編號 1byte 75
        public byte ZoneCode;             //前次段碼 1byte 76
        public byte[] ZoneTxnTime;          //前次段次交易時間 4bytes uint 77
        public byte[] ZoneRouteNo;          //前次段次路線編號 2bytes ushort 81
        public byte[] ZoneTxnCTSN;          //前次段次票卡交易序號 4bytes uint 83
        public byte[] ZoneTxnAmt;           //前次段次交易金額 2bytes ushort 87
        public byte ZoneEntryStatus;      //前次上下車狀態(0x01 -> 上車,0x00 -> 下車) 1byte 89
        public byte ZoneTxnType;          //前次段次交易類別 1byte 90
        public byte ZoneDirection;        //前次往返程註記(0x01 -> 去程,0x02 -> 返程,0x00 -> 循環) 1byte 91
        public byte ZoneSpID;             //前次段次交易業者代號 1byte 92
        public byte[] ZoneStationID;        //前次段次交易場站代碼 2byte 93
        public byte[] ZoneDeviceID;         //前次段次設備編號 4bytes uint 95
                                            //===========================================================================================
        public byte MileTxnSysID;         //前次里程交易系統編號 1byte 99
        public byte[] MileTxnTime;          //前次里程交易時間 4bytes uint 100
        public byte[] MileRouteNo;          //前次里程路線編號 2bytes ushort 104
        public byte[] MileTxnCTSN;          //前次里程票卡交易序號 4bytes uint 106
        public byte[] MileTxnAmt;           //前次里程交易金額 2bytes ushort 110
        public byte MileEntryStatus;      //前次上下車狀態(0x01 -> 上車,0x00 -> 下車) 1byte 112
        public byte MileTxnType;          //前次里程交易類別 1byte 113
        public byte MileTxnMode;          //前次里程交易模式 1byte 114
        public byte MileDirection;        //前次往返程註記(0x01 -> 去程,0x02 -> 返程,0x00 -> 循環) 1byte 115
        public byte MileSpID;             //前次里程交易業者代號 1byte 116
        public byte[] MileStationID;        //前次里程交易場站代碼 2bytes ushort 117
        public byte[] MileDeviceID;         //前次里程設備編號 4bytes uint 119
        public byte[] Receivables;          //上車站到終點站票價 2bytes ushort 123
        public byte RideCounts;           //搭乘次數 1byte 125
        public byte[] RideDate;             //搭乘日期 4bytes uint 126-129
    }
    struct ICASH_Txns_Mile   //愛金卡寫卡結構體
    {
        public byte Trans_Mode;               //交易模式 1byte
        public byte Trans_Type;               //交易類別 1byte
        public byte[] DateTime;                  //交易時間 4bytes uint
        public byte[] Amount;                  //交易金額/點數 2bytes ushort
        public byte[] Receivables;             //上車站到終點站票價 2bytes ushort
        public byte Direction;                //行駛方向 1byte
        public byte Trans_Status;             //交易狀態 1byte
        public byte[] RouteNo;                 //路線編號 2bytes ushort
        public byte[] Current_Station;         //上/下車站別 2bytes ushort
        public byte TransferGroupCode_Before; //前次轉乘群組碼 1byte
        public byte TransferGroupCode;        //本次轉乘群組碼 1byte
        public byte[] TransferDiscount;        //轉乘優惠金額 2bytes ushort
        public byte SysID;                    //交易系統編號 1byte
        public byte CompanyID;                //交易業者編號 1byte
        public byte[] DeviceID;                  //設備編號 4bytes uint
        public byte[] Vehicles_type;           //乘車類別 2bytes ushort
        public byte[] Channel_Type;          //通路識別代碼 3bytes
        public byte[] SocialPntUsed;           //累積已使用社福優惠點數 2bytes ushort
        public byte[] SocialDiscount;          //社福優惠金額 2bytes ushort
        public byte[] SocialResetDate;           //社福優惠點數重置日期 4bytes uint
        public byte RideCounts;               //搭乘次數 1byte
        public byte[] RideDate;                  //搭乘日期 4bytes uint
        public byte[] TradePoint;              //交易點數 2bytes ushort
    }
    struct DS_CSC_READ_FOR_MILAGE_BV_7B   //悠遊卡讀卡結構體
    {
        public byte[] cmd_manufacture_serial_number;             //卡號 uint32 4bytes 5
        public byte cid_issuer_code;                           //發卡公司 uchar 1byte 9
        public byte[] cid_begin_time;                            //票卡起始日期 uint32 4bytes 10(unix)
        public byte[] cid_expiry_time;                           //票卡到期日期 uint32 4bytes 14(unix)
        public byte cid_status;                                //票卡狀態 uchar 1byte 18
        public byte gsp_autopay_flag;                          //自動加值授權認證旗標 uchar 1byte 19
        public byte[] gsp_autopay_value;                         //自動加值金額 ushort 2bytes 20
        public byte[] gsp_max_ev;                                //票卡最大額度上限 ushort 2bytes 22
        public byte[] gsp_max_deduct_value;                      //票卡最大扣款金額 ushort 2bytes 24
        public byte gsp_personal_profile;                      //票卡身份別 uchar 1byte 26
        public byte[] gsp_profile_expiry_date;                   //身份到期日 ushort 2bytes 27(DOS)
        public byte gsp_area_code;                             //區碼 uchar 1byte 29
        public byte gsp_bank_code;                             //銀行代碼 uchar 1byte 30
        public byte area_auth_flag;                            //地區認證旗標 uchar 1byte 31
        public byte special_ticket_type;                       //特殊票票別 uchar 1byte 32
        public byte[] cpd_social_security_code;                  //身份證號碼 uchar 6bytes 33
        public byte[] cpd_deposit;                               //押金 ushort 2bytes 39
        public byte[] ev;                                        //電子錢包額度(餘額) short 2bytes 41
        public byte[] tsd_transaction_sequence_number;           //交易序號 ushort 2bytes 43
        public byte[] tsd_loyalty_points;                        //忠誠累積點數 ushort 2bytes 45
        public byte[] tsd_add_value_accumulated_points;          //加值累積點數 ushort 2bytes 47

        public byte urt_transaction_sequence_number_LSB;       //轉乘交易序號末碼 uchar 1byte 49
        public byte[] urt_transfer_group_code_new;               //新轉乘群組代碼 uchar 2bytes 50
        public byte[] urt_transaction_date_and_time;             //轉乘交易時間 uint32 4bytes 52(unix)
        public byte urt_transaction_type;                      //轉乘方式 char 1byte 56
        public byte[] urt_transfer_discount;                     //轉乘金額 ushort 2bytes 57
        public byte[] urt_ev_afetr_transfer;                     //轉乘後票卡餘額 short 2bytes 59
        public byte urt_transfer_group_code;                   //轉乘群組代碼 uchar 1byte 61
        public byte urt_transaction_location_code;             //轉乘交易場站代碼 uchar 1byte 62
        public byte[] urt_transaction_equipment_id;              //轉乘交易設備編號 uchar 4byte 63

        public byte busfix_fare_product_company;               //特種票交易公司代碼 uchar 1byte 67
        public byte busfix_fare_product_kind;                  //特種票分類 uchar 1byte 68
        public byte busfix_fare_product_type;                  //特種票票種 uchar 1byte 69
        public byte[] busfix_first_possible_utilization_date;    //特種票起始日 ushort 2bytes 70(DOS)
        public byte[] busfix_last_possible_utilization_date;     //特種票到期日 ushort 2bytes 72(DOS)
        public byte busfix_number_of_use;                      //特種票可使用次數 uchar 1byte 74
        public byte busfix_duration_of_use;                    //特種票期限 uchar 1bytes 75
        public byte busfix_authorized_lines;                   //特種票可用路線代碼 uchar 1byte 76
        public byte busfix_authorized_groups;                  //特種票可用路線群組 uchar 1byte 77
        public byte busfix_stop1_number;                       //特種票起迄站代碼 1 uchar 1byte 78
        public byte busfix_stop2_number;                       //特種票起迄站代碼 2 uchar 1byte 79
        public byte[] busfix_vip_points;                         //VIP 票累積儲值點數 ushort 2bytes 80

        public byte busvar_current_used_number;                //特種票已用次數 uchar 1byte 82
        public byte[] busvar_date_of_first_transaction;          //特種票首次交易日期 ushort 2bytes 83(DOS)
        public byte busvar_milage_forbiddance_flag;            //里程計費系統禁用旗標 uchar 1byte 85
        public byte busvar_special_permission;                 //特許權 uchar 1byte 86
        public byte busvar_travel_to_or_from;                  //往返程註記 uchar 1byte 87
        public byte busvar_get_on_or_off;                      //上下車狀態 uchar 1byte 88
        public byte busvar_company_number;                     //交易公司代碼 uchar 1byte 89
        public byte[] busvar_device_serial_number;               //設備次編號 ushort 2bytes 90
        public byte busvar_line_number;                        //路線代碼 uchar 1byte 92
        public byte[] busvar_transaction_date_and_time;          //交易時間 uint32 4bytes 93(unix)
        public byte busvar_stop_number;                        //站牌代碼 uchar 1byte 97
        public byte[] busvar_value_of_transaction;               //交易金額 ushort 2bytes 98
        public byte busvar_travel_mode;                        //搭乘模式 uchar 1byte 100
        public byte busvar_vip_accumulated_points1;            //搭乘次數旗標 &VIP 票累積已用點數 uchar 1byte 101
        public byte busvar_vip_accumulated_points2;            //VIP 票累積已用點數_2 uchar 1byte 102
        public byte busvar_accumulated_free_rides;             //累積優惠點數 uchar 1byte 103
        public byte[] busvar_free_transaction_date_and_time;     //優惠點數交易時間 uint32 4bytes 104(unix)
        public byte[] busvar_accumulated_free_rides2;            //累積優惠點數 2 short 2bytes 108
        public byte[] busvar_free_transaction_date_and_time2;    //累積優惠點數 2 交易日期 char 2bytes 110(DOS)

        public byte var_transaction_number_lsb;                //交易序號末碼 uchar 1byte 112
        public byte[] var_transaction_date_time;                 //加值時間 uint32 4bytes 113(unix)
        public byte var_transaction_type;                      //加值方式代碼 uchar 1byte 117
        public byte[] var_value_of_transaction;                  //加值金額 short 2bytes 118
        public byte[] var_ev_afetr_transaction;                  //加值後餘額 short 2bytes 120
        public byte var_operator_code;                         //交易公司代碼 uchar 1byte 122
        public byte var_transaction_location_code;             //交易場站代碼 uchar 1byte 123
        public byte[] var_transaction_device_id;                 //交易設備編號 uchar 4bytes 124
    }
    struct DS_CSC_MILAGE_DEDUCT_IN_V3    //悠遊卡寫卡結構體
    {
        public byte message_type;                      //訊息代碼 uchar 1byte
        public byte[] utr_transaction_date_time;         //交易時間 uint 4bytes
        public byte[] cmd_manufacture_serial_number;     //卡號 byte 4bytes
        public byte[] deducted_value;                    //扣值金額 ushort 2bytes

        public byte[] urt_transfer_date_time;            //轉乘交易時間 uint 4bytes
        public byte[] urt_transfer_discount;             //轉乘優惠金額 ushort 2bytes
        public byte urt_transfer_group_code;           //轉乘群組代碼 uchar 1byte
        public byte[] urt_transfer_group_code_new;       //新轉乘群組代碼 uchar 2bytes

        public byte busvar_current_used_number;        //特種票已用次數 uchar 1byte
        public byte[] busvar_date_of_first_transaction;  //特種票首次交易日期 ushort 2bytes
        public byte busvar_milage_forbiddance_flag;    //里程計費系統禁用旗標 uchar 1byte
        public byte busvar_special_permission;         //特許權 uchar 1byte
        public byte busvar_travel_to_or_from;          //往返程註記 uchar 1byte
        public byte busvar_get_on_or_off;              //上下車狀態 uchar 1byte
        public byte busvar_line1_number;               //路線代碼 uchar 1byte
        public byte[] busvar_line2_number;               //路線代碼2 uchar 2bytes
        public byte busvar_stop_number;                //站牌代碼 uchar 1byte
        public byte busvar_travel_mode;                //搭乘模式 uchar 1byte
        public byte busvar_transaction_type;           //交易方式代碼 uchar 1byte
        public byte busvar_vip_accumulated_points1;    //當日搭乘次數 &VIP 票累積已用點數1 uchar 1byte
        public byte busvar_vip_accumulated_points2;    //VIP 票累積已用點數2 uchar 1byte
        public byte busvar_remaining_rides;            //優惠剩餘使用次數 uchar 1byte
    }

}
