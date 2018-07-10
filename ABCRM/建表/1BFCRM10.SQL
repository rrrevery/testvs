/*==============================================================*/
/* DBMS name:      ORACLE Version 9i                            */
/* Created on:     2018-04-11 18:25:33                          */
/*==============================================================*/



-- Type package declaration
create or replace package PDTypes  
as
    TYPE ref_cursor IS REF CURSOR;
end;
/

-- Integrity package declaration
create or replace package IntegrityPackage AS
 procedure InitNestLevel;
 function GetNestLevel return number;
 procedure NextNestLevel;
 procedure PreviousNestLevel;
 end IntegrityPackage;
/

-- Integrity package definition
create or replace package body IntegrityPackage AS
 NestLevel number;

-- Procedure to initialize the trigger nest level
 procedure InitNestLevel is
 begin
 NestLevel := 0;
 end;


-- Function to return the trigger nest level
 function GetNestLevel return number is
 begin
 if NestLevel is null then
     NestLevel := 0;
 end if;
 return(NestLevel);
 end;

-- Procedure to increase the trigger nest level
 procedure NextNestLevel is
 begin
 if NestLevel is null then
     NestLevel := 0;
 end if;
 NestLevel := NestLevel + 1;
 end;

-- Procedure to decrease the trigger nest level
 procedure PreviousNestLevel is
 begin
 NestLevel := NestLevel - 1;
 end;

 end IntegrityPackage;
/


drop view BFCRM10.HYK_GRXX
/

create sequence BFCRM10.S_BFLOG
/

create sequence BFCRM10.S_HYFX_XF
/

create sequence BFCRM10.S_HYKKBKHDMX
/

create sequence BFCRM10.S_HYK_HYFWCLJL
/

create sequence BFCRM10.S_HYK_JEZCLJL
/

create sequence BFCRM10.S_HYK_YHQCLJL
/

create sequence BFCRM10.S_LMKDRLOG
/

create sequence BFCRM10.S_QYDEF
/

create sequence BFCRM10.S_QYDEF_MD
/

create sequence BFCRM10.S_SHCXHD
/

/*==============================================================*/
/* Table: ACDPD                                                 */
/*==============================================================*/
create table BFCRM10.ACDPD  (
   RQ                   DATE                            not null,
   PERSON_ID            NUMBER(10)                      not null,
   HJCS                 NUMBER(10),
   HJSJ                 NUMBER(10),
   GJSJ                 DATE,
   JTSJ                 DATE,
   constraint PK_ACDPD primary key (RQ)
)
/

/*==============================================================*/
/* Table: APP_GGTP                                              */
/*==============================================================*/
create table BFCRM10.APP_GGTP  (
   JLBH                 NUMBER(10)                      not null,
   TPWZ                 NUMBER(10)                      not null,
   GGBH                 NUMBER(5)                       not null,
   YLSJHM               VARCHAR2(100),
   STATUS               NUMBER(5)                       not null,
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16),
   DJSJ                 DATE                            not null,
   SHR                  NUMBER(10),
   SHRMC                VARCHAR2(16),
   SHSJ                 DATE,
   QDR                  NUMBER(10),
   QDRMC                VARCHAR2(16),
   QDSJ                 DATE,
   ZZRQ                 DATE,
   ZZR                  NUMBER(10),
   ZZRMC                VARCHAR2(16),
   constraint PK_APP_GGTP primary key (JLBH)
)
/

comment on column BFCRM10.APP_GGTP.TPWZ is
'1首页顶部轮播
2首页中部轮播
3兑换页轮播
4首页大图
5首页小图一
6首页小图二
7我的页推荐
8首页底部动态图
9首页滚动文字
10兑换页小图
11兑换页长图

对于非轮播图片，启动时自动终止以前同类型的广告
轮播图片则需要手工终止'
/

/*==============================================================*/
/* Table: APP_GGTPDY                                            */
/*==============================================================*/
create table BFCRM10.APP_GGTPDY  (
   JLBH                 NUMBER(10)                      not null,
   TITLE                VARCHAR2(30),
   PIC                  VARCHAR2(100),
   URLTYPE              NUMBER(5),
   URL                  VARCHAR2(100),
   YT                   NUMBER(5),
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16),
   DJSJ                 DATE                            not null,
   constraint PK_APP_GGTPDY primary key (JLBH)
)
/

comment on column BFCRM10.APP_GGTPDY.YT is
'1轮播图
2广告大图
3广告小图
4广告长图
5滚动文字'
/

/*==============================================================*/
/* Table: BANK_FQXEDEF                                          */
/*==============================================================*/
create table BFCRM10.BANK_FQXEDEF  (
   CXID                 NUMBER(10)                      not null,
   YHQID                NUMBER(5)                       not null,
   BANK_ID              NUMBER(5)                       not null,
   MDID                 NUMBER(10)                      not null,
   FQXE                 NUMBER(14,2),
   FQZXE                NUMBER(14,2),
   FQZXE_CARD           NUMBER(14,2),
   MAX_FQCS             NUMBER(10),
   FQXE_DAY             NUMBER(14,2),
   FQXE_CARD_DAY        NUMBER(14,2),
   MAX_FQCS_DAY         NUMBER(10),
   constraint PK_BANK_FQXEDEF primary key (CXID, YHQID, BANK_ID, MDID)
)
/

/*==============================================================*/
/* Table: BFLOG                                                 */
/*==============================================================*/
create table BFCRM10.BFLOG  (
   ID                   NUMBER(10)                      not null,
   MSGID                NUMBER(10)                      not null,
   CZNR                 VARCHAR2(100)                   not null,
   CZSJ1                DATE                            not null,
   CZSJ2                DATE,
   CZSJS                NUMBER(10),
   CZY                  NUMBER(10)                      not null,
   CZYMC                VARCHAR2(16),
   SYJQ                 VARCHAR2(40),
   BZ                   VARCHAR2(100),
   IPADD                VARCHAR2(40),
   CLLX                 NUMBER(10),
   HANDLE               NUMBER(10),
   constraint PK_BFLOG primary key (ID)
)
/

/*==============================================================*/
/* Index: BFLOG_CZSJ1                                           */
/*==============================================================*/
create index BFCRM10.BFLOG_CZSJ1 on BFCRM10.BFLOG (
   CZSJ1 ASC
)
/

/*==============================================================*/
/* Table: BHZT                                                  */
/*==============================================================*/
create table BFCRM10.BHZT  (
   TBLNAME              VARCHAR2(15)                    not null,
   REC_NUM              NUMBER(10)                      not null,
   constraint PK_BHZT primary key (TBLNAME)
)
/

/*==============================================================*/
/* Table: BMSP                                                  */
/*==============================================================*/
create table BFCRM10.BMSP  (
   SHBMID               NUMBER(10)                      not null,
   SHSPID               NUMBER(10)                      not null,
   constraint PK_BMSP primary key (SHBMID, SHSPID)
)
/

/*==============================================================*/
/* Table: BMSP_JFDY                                             */
/*==============================================================*/
create table BFCRM10.BMSP_JFDY  (
   SHBMID               NUMBER(10)                      not null,
   HYKTYPE              NUMBER(5)                       not null,
   SHSPID               NUMBER(10)                      not null,
   YXJ                  NUMBER(5)                       not null,
   SD                   RAW(96)                         not null,
   FZ                   FLOAT(50)                       not null,
   JFDYDBH              NUMBER(10)                      not null,
   constraint PK_BMSP_JFDY primary key (SHBMID, HYKTYPE, SHSPID, YXJ)
)
/

/*==============================================================*/
/* Index: BMSPJFDY_JLBH                                         */
/*==============================================================*/
create index BFCRM10.BMSPJFDY_JLBH on BFCRM10.BMSP_JFDY (
   JFDYDBH ASC
)
/

/*==============================================================*/
/* Table: BMSP_ZKDY                                             */
/*==============================================================*/
create table BFCRM10.BMSP_ZKDY  (
   SHBMID               NUMBER(10)                      not null,
   HYKTYPE              NUMBER(5)                       not null,
   SHSPID               NUMBER(10)                      not null,
   YXJ                  NUMBER(5)                       not null,
   SD                   RAW(96)                         not null,
   ZKL                  FLOAT(50)                       not null,
   ZKDYDBH              NUMBER(10)                      not null,
   constraint PK_BMSP_ZKDY primary key (SHBMID, HYKTYPE, SHSPID, YXJ)
)
/

/*==============================================================*/
/* Index: BMSPZKDY_JLBH                                         */
/*==============================================================*/
create index BFCRM10.BMSPZKDY_JLBH on BFCRM10.BMSP_ZKDY (
   ZKDYDBH ASC
)
/

/*==============================================================*/
/* Table: CARDLOG_ICK                                           */
/*==============================================================*/
create table BFCRM10.CARDLOG_ICK  (
   KH                   VARCHAR2(20)                    not null,
   CLLX                 NUMBER(5)                       not null,
   NM                   VARCHAR2(20),
   PSWD                 VARCHAR2(40),
   XKSJ                 DATE,
   constraint PK_CARDLOG_ICK primary key (KH, CLLX)
)
/

/*==============================================================*/
/* Table: CARDLQJL                                              */
/*==============================================================*/
create table BFCRM10.CARDLQJL  (
   JLBH                 NUMBER(10)                      not null,
   BGDDDM_BC            VARCHAR2(10)                    not null,
   BGDDDM_BR            VARCHAR2(10)                    not null,
   HYKKH1               VARCHAR2(30),
   HYKKH2               VARCHAR2(30),
   HYKSL                NUMBER(5)                       not null,
   LQR                  NUMBER(10),
   LQRMC                VARCHAR2(16),
   BJ_CZK               NUMBER(1)                       not null
      constraint CKC_BJ_CZK_CARDLQJL check (BJ_CZK in (0,1)),
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16)                    not null,
   DJSJ                 DATE                            not null,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(16),
   ZXRQ                 DATE,
   STATUS               NUMBER(5)                       not null,
   DJLX                 NUMBER(5)                       not null,
   QDRQ                 DATE,
   QDRMC                VARCHAR2(16),
   QDR                  NUMBER(10),
   SQDJH                NUMBER(10),
   MDID                 NUMBER(10),
   constraint PK_CARDLQJL primary key (JLBH)
)
/

/*==============================================================*/
/* Table: CARDLQJLITEM                                          */
/*==============================================================*/
create table BFCRM10.CARDLQJLITEM  (
   JLBH                 NUMBER(10)                      not null,
   HM                   VARCHAR2(30)                    not null,
   HYKTYPE              NUMBER(10)                      not null,
   JE                   NUMBER(14,2)                   default 0,
   LQRMC                VARCHAR2(16),
   constraint PK_CARDLQJLITEM primary key (JLBH, HM)
)
/

/*==============================================================*/
/* Table: CARDLQJLKDITEM                                        */
/*==============================================================*/
create table BFCRM10.CARDLQJLKDITEM  (
   JLBH                 NUMBER(10)                      not null,
   CZKHM_BEGIN          VARCHAR2(30)                    not null,
   CZKHM_END            VARCHAR2(30)                    not null,
   HYKTYPE              NUMBER(10)                      not null,
   SKSL                 NUMBER(10),
   MZJE                 NUMBER(14,2),
   constraint PK_CARDLQJLKDITEM primary key (JLBH, CZKHM_BEGIN)
)
/

/*==============================================================*/
/* Table: CARDLQSQJL                                            */
/*==============================================================*/
create table BFCRM10.CARDLQSQJL  (
   JLBH                 NUMBER(10)                      not null,
   BGDDDM_BR            VARCHAR2(10)                    not null,
   HYKSL                NUMBER(10)                      not null,
   BJ_CZK               NUMBER(5)                       not null,
   STATUS               NUMBER(5)                       not null,
   DJLX                 NUMBER(5)                       not null,
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(10)                    not null,
   DJSJ                 DATE                            not null,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(16),
   ZXRQ                 DATE,
   BZ                   VARCHAR2(50),
   LQDH                 NUMBER(10),
   LQR                  NUMBER(10),
   LQRMC                VARCHAR2(16),
   LQRQ                 DATE,
   constraint PK_CARDLQSQJL primary key (JLBH)
)
/

/*==============================================================*/
/* Table: CARDLQSQJLITEM                                        */
/*==============================================================*/
create table BFCRM10.CARDLQSQJLITEM  (
   JLBH                 NUMBER(10)                      not null,
   HYKTYPE              NUMBER(10)                      not null,
   HYKSL                NUMBER(10)                      not null,
   constraint PK_CARDLQSQJLITEM primary key (JLBH, HYKTYPE)
)
/

/*==============================================================*/
/* Table: CARD_YETZD                                            */
/*==============================================================*/
create table BFCRM10.CARD_YETZD  (
   JLBH                 NUMBER(10)                      not null,
   HYKTYPE              NUMBER(5)                       not null,
   BGDDDM               VARCHAR2(20)                    not null,
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16)                    not null,
   DJSJ                 DATE                            not null,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(16),
   ZXRQ                 DATE,
   constraint PK_CARD_YETZD primary key (JLBH)
)
/

/*==============================================================*/
/* Table: CARD_YETZDITEM                                        */
/*==============================================================*/
create table BFCRM10.CARD_YETZDITEM  (
   JLBH                 NUMBER(10)                      not null,
   HM                   VARCHAR2(16)                    not null,
   QCYE_OLD             NUMBER(14,2),
   TZJE                 NUMBER(14,2)                   default 0,
   QCYE_NEW             NUMBER(14,2),
   constraint PK_CARD_YETZDITEM primary key (JLBH, HM)
)
/

/*==============================================================*/
/* Table: CRMBSKJL                                              */
/*==============================================================*/
create table BFCRM10.CRMBSKJL  (
   JLBH                 NUMBER(10)                      not null,
   MDID                 NUMBER(10)                      not null,
   SKTNO                VARCHAR2(6)                     not null,
   XPH                  NUMBER(10)                      not null,
   HYID                 NUMBER(10)                      not null,
   XFJE                 NUMBER(14,2),
   XFRQ                 DATE,
   JF                   FLOAT(50),
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16)                    not null,
   DJSJ                 DATE                            not null,
   CMPNAME              VARCHAR2(50),
   constraint PK_CRMBSKJL primary key (JLBH)
)
/

/*==============================================================*/
/* Table: CRMCLIENT                                             */
/*==============================================================*/
create table BFCRM10.CRMCLIENT  (
   IPADDR               VARCHAR2(30)                    not null,
   HTTPOK               NUMBER(5),
   WSOK                 NUMBER(5),
   WCFOK                NUMBER(5),
   constraint PK_CRMCLIENT primary key (IPADDR)
)
/

/*==============================================================*/
/* Table: CRMQZJCFG                                             */
/*==============================================================*/
create table BFCRM10.CRMQZJCFG  (
   QYID                 NUMBER(10)                      not null,
   QYMC                 VARCHAR2(40)                    not null,
   CRMIP                VARCHAR2(30)                    not null,
   CRMPORT              NUMBER(5)                      default 0 not null,
   CRMURL               VARCHAR2(60)                    not null,
   constraint PK_CRMQZJCFG primary key (QYID)
)
/

/*==============================================================*/
/* Table: CRMTHMJFJL                                            */
/*==============================================================*/
create table BFCRM10.CRMTHMJFJL  (
   JLBH                 NUMBER(10)                      not null,
   CZDD                 VARCHAR2(10),
   HYID                 NUMBER(10)                      not null,
   YWCLJF               FLOAT(50)                       not null,
   YNLJJF               FLOAT(50)                       not null,
   YYHQJE               NUMBER(14,2)                    not null,
   YCZKYE               NUMBER(14,2)                    not null,
   THJF                 FLOAT(50)                       not null,
   YMJJF                FLOAT(50)                       not null,
   YMNJF                FLOAT(50)                       not null,
   YFJE_J               NUMBER(14,2)                    not null,
   YFJE_N               NUMBER(14,2)                    not null,
   SMJJF                FLOAT(50)                       not null,
   SMNJF                FLOAT(50)                       not null,
   SFJE_J               NUMBER(14,2)                    not null,
   SFJE_N               NUMBER(14,2)                    not null,
   XJ                   NUMBER(14,2),
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16)                    not null,
   DJSJ                 DATE                            not null,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(16),
   ZXRQ                 DATE,
   BZ                   VARCHAR2(50),
   MDID                 NUMBER(10),
   SKTNO                VARCHAR2(6),
   JZRQ                 DATE,
   SKYDM                VARCHAR2(8),
   DJLX                 NUMBER(5)                      default 0,
   SKYMC                VARCHAR2(16),
   JLBH_OLD             NUMBER(10),
   constraint PK_CRMTHMJFJL primary key (JLBH)
)
/

/*==============================================================*/
/* Index: CRMTHMJFJL_HYID                                       */
/*==============================================================*/
create index BFCRM10.CRMTHMJFJL_HYID on BFCRM10.CRMTHMJFJL (
   HYID ASC
)
/

/*==============================================================*/
/* Index: CRMTHMJF_JLBH_OLD                                     */
/*==============================================================*/
create index CRMTHMJF_JLBH_OLD on BFCRM10.CRMTHMJFJL (
   JLBH_OLD ASC
)
/

/*==============================================================*/
/* Table: CRMTHMJFJLITEM                                        */
/*==============================================================*/
create table BFCRM10.CRMTHMJFJLITEM  (
   JLBH                 NUMBER(10)                      not null,
   HYID                 NUMBER(10)                      not null,
   YYHQJE               NUMBER(14,2),
   YCZKYE               NUMBER(14,2),
   KYHQJE               NUMBER(14,2),
   KCZKYE               NUMBER(14,2),
   YHQJLBH              NUMBER(10),
   constraint PK_CRMTHMJFJLITEM primary key (JLBH, HYID)
)
/

/*==============================================================*/
/* Index: CRMTHMJFJLITEM_HYID                                   */
/*==============================================================*/
create index BFCRM10.CRMTHMJFJLITEM_HYID on BFCRM10.CRMTHMJFJLITEM (
   HYID ASC
)
/

/*==============================================================*/
/* Table: CRMTZCLJL                                             */
/*==============================================================*/
create table BFCRM10.CRMTZCLJL  (
   JLBH                 NUMBER(10)                      not null,
   MDID                 NUMBER(10)                      not null,
   ZY                   VARCHAR2(40),
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16)                    not null,
   DJSJ                 DATE                            not null,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(16),
   ZXRQ                 DATE,
   CLLX                 NUMBER(5),
   constraint PK_CRMTZCLJL primary key (JLBH)
)
/

/*==============================================================*/
/* Table: CRMTZCLJLITEM                                         */
/*==============================================================*/
create table BFCRM10.CRMTZCLJLITEM  (
   JLBH                 NUMBER(10)                      not null,
   JYID                 NUMBER(9,0),
   HYID                 NUMBER(10)                      not null,
   SKTNO                VARCHAR2(6),
   SKYDM                VARCHAR2(8),
   SKYMC                VARCHAR2(16),
   YWSKJE               NUMBER(14,2),
   KXFJE                NUMBER(14,2),
   TZJE                 NUMBER(14,2),
   YHQID                NUMBER(10),
   JSRQ                 DATE,
   MDFWDM               VARCHAR2(10),
   XFRQ                 DATE,
   constraint PK_CRMTZCLJLITEM primary key (JLBH, HYID)
)
/

/*==============================================================*/
/* Table: CRMUSER                                               */
/*==============================================================*/
create table BFCRM10.CRMUSER  (
   USER_DM              VARCHAR2(10)                    not null,
   USER_PSW             VARCHAR2(20),
   HTTPOK               NUMBER(1)                      default 0 not null,
   WSOK                 NUMBER(1)                      default 0 not null,
   WCFOK                NUMBER(1)                      default 0 not null,
   constraint PK_CRMUSER primary key (USER_DM)
)
/

/*==============================================================*/
/* Table: CRMUSERQX                                             */
/*==============================================================*/
create table BFCRM10.CRMUSERQX  (
   USER_DM              VARCHAR2(10)                    not null,
   GNDM                 VARCHAR2(4)                     not null,
   constraint PK_CRMUSERQX primary key (USER_DM, GNDM)
)
/

/*==============================================================*/
/* Table: CRM_POS_CZYZT                                         */
/*==============================================================*/
create table BFCRM10.CRM_POS_CZYZT  (
   CURR_SKY             NUMBER(10)                      not null,
   MDID                 NUMBER(10),
   SKTNO                VARCHAR2(6),
   SYSJ                 DATE,
   IPDZ                 VARCHAR2(20),
   CXJB                 NUMBER(10),
   constraint PK_CRM_POS_CZYZT primary key (CURR_SKY)
)
/

/*==============================================================*/
/* Table: CRM_XFDZ                                              */
/*==============================================================*/
create table BFCRM10.CRM_XFDZ  (
   MDID                 NUMBER(10)                      not null,
   SKTNO                VARCHAR2(6)                     not null,
   JLBH                 NUMBER(10)                      not null,
   SKYDM                VARCHAR2(8)                     not null,
   JZRQ                 DATE                            not null,
   DZSJ                 DATE                            not null,
   CRM_CZK              NUMBER(14,2)                   default 0,
   CRM_YHQ              NUMBER(14,2)                   default 0,
   CRM_HSK              NUMBER(14,2)                   default 0,
   JXC_CZK              NUMBER(14,2)                   default 0,
   JXC_YHQ              NUMBER(14,2)                   default 0,
   JXC_HSK              NUMBER(14,2)                   default 0,
   constraint PK_CRM_XFDZ primary key (MDID, SKTNO, JLBH)
)
/

/*==============================================================*/
/* Index: CRM_XFDZ_IDX01                                        */
/*==============================================================*/
create index BFCRM10.CRM_XFDZ_IDX01 on BFCRM10.CRM_XFDZ (
   JZRQ ASC,
   SKTNO ASC
)
/

/*==============================================================*/
/* Index: CRM_XFDZ_IDX02                                        */
/*==============================================================*/
create index BFCRM10.CRM_XFDZ_IDX02 on BFCRM10.CRM_XFDZ (
   JZRQ ASC,
   SKYDM ASC
)
/

/*==============================================================*/
/* Table: CR_XSYHHYQS_R                                         */
/*==============================================================*/
create table BFCRM10.CR_XSYHHYQS_R  (
   RQ                   DATE                            not null,
   CHANNEL              NUMBER(5)                       not null,
   FS                   NUMBER(10),
   HY                   NUMBER(10),
   SY                   NUMBER(10),
   SYHY                 NUMBER(10),
   CM                   NUMBER(10),
   CMHY                 NUMBER(10),
   LS                   NUMBER(10),
   LSHY                 NUMBER(10),
   constraint PK_CR_XSYHHYQS_R primary key (RQ, CHANNEL)
)
/

/*==============================================================*/
/* Table: CR_XZHYSL                                             */
/*==============================================================*/
create table BFCRM10.CR_XZHYSL  (
   RQ                   DATE                            not null,
   HOUR                 NUMBER(10)                      not null,
   ZSL                  NUMBER(10),
   XX                   NUMBER(10),
   WX                   NUMBER(10),
   WXBD                 NUMBER(10),
   WXFS                 NUMBER(10),
   APP                  NUMBER(10),
   APPBD                NUMBER(10),
   APPXZ                NUMBER(10),
   constraint PK_CR_XZHYSL primary key (RQ, HOUR)
)
/

/*==============================================================*/
/* Table: CR_XZHYSL_R                                           */
/*==============================================================*/
create table BFCRM10.CR_XZHYSL_R  (
   RQ                   DATE                            not null,
   ZSL                  NUMBER(10),
   HYZSL                NUMBER(10),
   XX                   NUMBER(10),
   WX                   NUMBER(10),
   WXBD                 NUMBER(10),
   WXFS                 NUMBER(10),
   APP                  NUMBER(10),
   APPBD                NUMBER(10),
   APPXZ                NUMBER(10),
   constraint PK_CR_XZHYSL_R primary key (RQ)
)
/

/*==============================================================*/
/* Table: CR_XZHYSL_W                                           */
/*==============================================================*/
create table BFCRM10.CR_XZHYSL_W  (
   YEAR                 NUMBER(10)                      not null,
   WEEK                 NUMBER(10)                      not null,
   KSRQ                 DATE,
   JSRQ                 DATE,
   ZSL                  NUMBER(10),
   HYZSL                NUMBER(10),
   XX                   NUMBER(10),
   WX                   NUMBER(10),
   WXBD                 NUMBER(10),
   WXFS                 NUMBER(10),
   APP                  NUMBER(10),
   APPBD                NUMBER(10),
   APPXZ                NUMBER(10),
   constraint PK_CR_XZHYSL_W primary key (YEAR, WEEK)
)
/

/*==============================================================*/
/* Table: CR_XZHYSL_Y                                           */
/*==============================================================*/
create table BFCRM10.CR_XZHYSL_Y  (
   YEAR                 NUMBER(10)                      not null,
   MONTH                NUMBER(10)                      not null,
   KSRQ                 DATE,
   JSRQ                 DATE,
   ZSL                  NUMBER(10),
   HYZSL                NUMBER(10),
   XX                   NUMBER(10),
   WX                   NUMBER(10),
   WXBD                 NUMBER(10),
   WXFS                 NUMBER(10),
   APP                  NUMBER(10),
   APPBD                NUMBER(10),
   APPXZ                NUMBER(10),
   constraint PK_CR_XZHYSL_Y primary key (YEAR, MONTH)
)
/

/*==============================================================*/
/* Table: CR_YHAH_R                                             */
/*==============================================================*/
create table BFCRM10.CR_YHAH_R  (
   RQ                   DATE                            not null,
   CHANNEL              NUMBER(5)                       not null,
   AHID                 NUMBER(10)                      not null,
   SL                   NUMBER(10),
   constraint PK_CR_YHAH_R primary key (RQ, CHANNEL, AHID)
)
/

/*==============================================================*/
/* Table: CR_YHHX_R                                             */
/*==============================================================*/
create table BFCRM10.CR_YHHX_R  (
   RQ                   DATE                            not null,
   CHANNEL              NUMBER(5)                       not null,
   XBID                 NUMBER(10)                      not null,
   NLID                 NUMBER(10)                      not null,
   ZYID                 NUMBER(10)                      not null,
   XLID                 NUMBER(10)                      not null,
   QYID                 NUMBER(10)                      not null,
   GKJZ                 NUMBER(10)                      not null,
   CAR                  NUMBER(10)                      not null,
   MARRY                NUMBER(10)                      not null,
   CHILD                NUMBER(10)                      not null,
   SL                   NUMBER(10),
   constraint PK_CR_YHHX_R primary key (RQ, CHANNEL, XBID, NLID, ZYID, XLID, QYID, GKJZ, CAR, MARRY, CHILD)
)
/

/*==============================================================*/
/* Table: CR_ZDFX_R                                             */
/*==============================================================*/
create table BFCRM10.CR_ZDFX_R  (
   RQ                   DATE                            not null,
   CHANNEL              NUMBER(5)                       not null,
   DEVICEBRAND          VARCHAR2(30)                    not null,
   DEVICEMODEL          VARCHAR2(30)                    not null,
   MOBILEOS             VARCHAR2(30)                    not null,
   CARRIER              VARCHAR2(30)                    not null,
   NETMODE              VARCHAR2(30)                    not null,
   SBS                  NUMBER(10),
   SYCS                 NUMBER(10),
   XYHS                 NUMBER(10),
   constraint PK_CR_ZDFX_R primary key (RQ, CHANNEL, DEVICEBRAND, DEVICEMODEL, MOBILEOS, CARRIER, NETMODE)
)
/

/*==============================================================*/
/* Table: CXHDDEF                                               */
/*==============================================================*/
create sequence BFCRM10.S_CXHDDEF
/
create table BFCRM10.CXHDDEF  (
   CXID                 NUMBER(10)                      not null,
   SHDM                 VARCHAR2(4)                     not null,
   CXHDBH               NUMBER(10)                      not null,
   CXZT                 VARCHAR2(40),
   CXNR                 VARCHAR2(4000),
   NIAN                 NUMBER(5)                       not null,
   CXQS                 NUMBER(5)                       not null,
   KSSJ                 DATE                            not null,
   JSSJ                 DATE                            not null,
   SCSJ                 DATE                            not null,
   TM                   NUMBER(10),
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16)                    not null,
   DJSJ                 DATE                            not null,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(16),
   ZXRQ                 DATE,
   constraint PK_CXHDDEF primary key (CXID)
)
/

/*==============================================================*/
/* Table: CXHDMBJZ_SPFTRJL                                      */
/*==============================================================*/
create table BFCRM10.CXHDMBJZ_SPFTRJL  (
   RQ                   DATE                            not null,
   SHDM                 VARCHAR2(4)                     not null,
   MDID                 NUMBER(10)                      not null,
   BMDM                 VARCHAR2(20)                    not null,
   SHSPID               NUMBER(10)                      not null,
   CXID                 NUMBER(10)                      not null,
   MBJZGZID             NUMBER(10)                      not null,
   XFLJMJFS             NUMBER(5)                       not null,
   ZXSJE                NUMBER(14,2)                   default 0,
   ZXSJE_MBJZ           NUMBER(14,2)                   default 0,
   ZMBJZJE              NUMBER(14,2)                   default 0,
   constraint PK_CXHDMBJZ_SPFTRJL primary key (RQ, SHDM, CXID, SHSPID, BMDM, MBJZGZID, XFLJMJFS, MDID)
)
/

/*==============================================================*/
/* Index: CXHDMBJZ_SHSPID                                       */
/*==============================================================*/
create index BFCRM10.CXHDMBJZ_SHSPID on BFCRM10.CXHDMBJZ_SPFTRJL (
   SHSPID ASC
)
/

/*==============================================================*/
/* Index: CXHDMBJZ_SHBM                                         */
/*==============================================================*/
create index BFCRM10.CXHDMBJZ_SHBM on BFCRM10.CXHDMBJZ_SPFTRJL (
   SHDM ASC,
   BMDM ASC
)
/

/*==============================================================*/
/* Table: CXHD_FQXEDEF                                          */
/*==============================================================*/
create table BFCRM10.CXHD_FQXEDEF  (
   CXID                 NUMBER(10)                      not null,
   YHQID                NUMBER(5)                       not null,
   MDID                 NUMBER(10)                      not null,
   FQZXE                NUMBER(14,2)                   default 0,
   FQXE_DAY             NUMBER(14,2)                   default 0,
   FQZCS                NUMBER(10),
   constraint PK_CXHD_FQXEDEF primary key (CXID, YHQID, MDID)
)
/

/*==============================================================*/
/* Table: CXHD_FQXEDYGZD                                        */
/*==============================================================*/
create table BFCRM10.CXHD_FQXEDYGZD  (
   JLBH                 NUMBER(10)                      not null,
   CXID                 NUMBER(10)                      not null,
   MDID                 NUMBER(5)                       not null,
   YHQID                NUMBER(5)                       not null,
   DJR                  NUMBER(10)                      not null,
   DJRMC                varchar2(16)                    not null,
   DJSJ                 DATE                            not null,
   ZXR                  NUMBER(10),
   ZXRMC                varchar2(16),
   ZXRQ                 DATE,
   QDR                  NUMBER(10),
   QDRMC                varchar2(16),
   QDSJ                 DATE,
   STATUS               NUMBER(5)                       not null,
   ZZRQ                 DATE,
   ZZR                  NUMBER(10),
   ZZRMC                varchar2(16),
   FQZXE                NUMBER(14,2)                   default 0 not null,
   FQXE_DAY             NUMBER(14,2),
   constraint PK_CXHD_FQXEDYGZD primary key (JLBH)
)
/

/*==============================================================*/
/* Table: CXHD_FQXETZ                                           */
/*==============================================================*/
create table BFCRM10.CXHD_FQXETZ  (
   JLBH                 NUMBER(10)                      not null,
   YJLBH                NUMBER(10)                      not null,
   CXID                 NUMBER(10)                      not null,
   YHQID                NUMBER(10)                      not null,
   MDID                 NUMBER(10)                      not null,
   FQZXE                NUMBER(14,2)                    not null,
   TZXE                 NUMBER(14,2)                    not null,
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16)                    not null,
   DJSJ                 DATE                            not null,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(16),
   ZXRQ                 DATE,
   FQXE_DAY             NUMBER(14,2),
   TZXE_DAY             NUMBER(14,2),
   constraint PK_CXHD_FQXETZ primary key (JLBH)
)
/

/*==============================================================*/
/* Table: CXHD_FQZJE                                            */
/*==============================================================*/
create table BFCRM10.CXHD_FQZJE  (
   CXID                 NUMBER(10)                      not null,
   YHQID                NUMBER(5)                       not null,
   MDID                 NUMBER(10)                      not null,
   JE                   NUMBER(14,2)                   default 0,
   CS                   NUMBER(10),
   constraint PK_CXHD_FQZJE primary key (CXID, YHQID, MDID)
)
/

/*==============================================================*/
/* Table: CXHD_ZSLPSL_DAY                                       */
/*==============================================================*/
create table BFCRM10.CXHD_ZSLPSL_DAY  (
   CXID                 NUMBER(10)                      not null,
   MDID                 NUMBER(10)                      not null,
   XFRQ                 VARCHAR2(8)                     not null,
   LPDM                 VARCHAR2(6)                     not null,
   SL                   NUMBER(10)                     default 0,
   constraint PK_CXHD_ZSLPSL_DAY primary key (CXID, MDID, XFRQ, LPDM)
)
/

/*==============================================================*/
/* Table: CXMBJZDGZSJ                                           */
/*==============================================================*/
create table BFCRM10.CXMBJZDGZSJ  (
   JLBH                 NUMBER(10)                      not null,
   INX                  NUMBER(5)                       not null,
   SHBMDM               VARCHAR2(10)                    not null,
   SHSPID               NUMBER(10)                      not null,
   GZBH                 NUMBER(5),
   MBJZGZID             NUMBER(10)                      not null,
   constraint PK_CXMBJZDGZSJ primary key (JLBH, INX, SHBMDM, SHSPID)
)
/

/*==============================================================*/
/* Table: CXMBJZDYD                                             */
/*==============================================================*/
create table BFCRM10.CXMBJZDYD  (
   JLBH                 NUMBER(10)                      not null,
   CXID                 NUMBER(10)                      not null,
   SHDM                 VARCHAR2(4)                     not null,
   SHBMID               NUMBER(10)                      not null,
   SHBMDM               VARCHAR2(20)                    not null,
   YXCLBJ               NUMBER(5)                      default 0 not null,
   RQ1                  DATE,
   RQ2                  DATE,
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16)                    not null,
   DJSJ                 DATE                            not null,
   SHR                  NUMBER(10),
   SHRMC                VARCHAR2(16),
   SHSJ                 DATE,
   QDR                  NUMBER(10),
   QDRMC                VARCHAR2(16),
   QDSJ                 DATE,
   STATUS               NUMBER(5)                       not null,
   ZZRQ                 DATE,
   ZZR                  NUMBER(10),
   ZZRMC                VARCHAR2(16),
   XFLJMJFS             NUMBER(5)                      default 0 not null,
   MJJQ                 NUMBER(1),
   constraint PK_CXMBJZDYD primary key (JLBH)
)
/

/*==============================================================*/
/* Table: CXMBJZDYD_FD                                          */
/*==============================================================*/
create table BFCRM10.CXMBJZDYD_FD  (
   JLBH                 NUMBER(10)                      not null,
   INX                  NUMBER(5)                       not null,
   KSRQ                 DATE                            not null,
   JSRQ                 DATE                            not null,
   SD                   RAW(48)                         not null,
   constraint PK_CXMBJZDYD_FD primary key (JLBH, INX)
)
/

/*==============================================================*/
/* Table: CXMBJZDYD_GZITEM                                      */
/*==============================================================*/
create table BFCRM10.CXMBJZDYD_GZITEM  (
   JLBH                 NUMBER(10)                      not null,
   INX                  NUMBER(5)                       not null,
   GZBH                 NUMBER(5)                       not null,
   SJLX                 NUMBER(5)                       not null,
   SJNR                 NUMBER(10)                      not null,
   XH                   NUMBER(10),
   constraint PK_CXMBJZDYD_GZITEM primary key (JLBH, INX, GZBH, SJLX, SJNR)
)
/

comment on column BFCRM10.CXMBJZDYD_GZITEM.SJLX is
'1 部门
2 合同
3 分类
4 品牌'
/

/*==============================================================*/
/* Table: CXMBJZDYD_GZSD                                        */
/*==============================================================*/
create table BFCRM10.CXMBJZDYD_GZSD  (
   JLBH                 NUMBER(10)                      not null,
   INX                  NUMBER(5)                       not null,
   GZBH                 NUMBER(5)                       not null,
   CLFS_BM              NUMBER(5)                      default 0 not null,
   CLFS_SPFL            NUMBER(5)                      default 0 not null,
   CLFS_SPSB            NUMBER(5)                      default 0 not null,
   CLFS_HT              NUMBER(5)                      default 0 not null,
   BJ_CJ                NUMBER(1)                       not null
      constraint CKC_BJ_CJ_CXMBJZDY check (BJ_CJ in (0,1)),
   MBJZGZID             NUMBER(10)                      not null,
   CLFS_SP              NUMBER(5)                      default 0 not null,
   CLFS_HYZ             NUMBER(5)                      default 0,
   constraint PK_CXMBJZDYD_GZSD primary key (JLBH, INX, GZBH)
)
/

comment on column BFCRM10.CXMBJZDYD_GZSD.CLFS_BM is
'1 等于
0 不处理
-1 不等于'
/

/*==============================================================*/
/* Table: CXMBJZDYD_SP                                          */
/*==============================================================*/
create table BFCRM10.CXMBJZDYD_SP  (
   JLBH                 NUMBER(10)                      not null,
   INX                  NUMBER(5)                       not null,
   SHSPID               NUMBER(10)                      not null,
   MBJZGZID             NUMBER(10)                      not null,
   XH                   NUMBER(10),
   BJ_CJ                NUMBER(5),
   constraint PK_CXMBJZDYD_SP primary key (JLBH, INX, SHSPID)
)
/

/*==============================================================*/
/* Table: CXMBJZFD_SJ                                           */
/*==============================================================*/
create table BFCRM10.CXMBJZFD_SJ  (
   JLBH                 NUMBER(10)                      not null,
   INX                  NUMBER(5)                       not null,
   DAY_WEEK             NUMBER(5)                       not null,
   MIN_DAY1             NUMBER(5)                       not null,
   MIN_DAY2             NUMBER(5)                       not null,
   constraint PK_CXMBJZFD_SJ primary key (JLBH, INX, DAY_WEEK, MIN_DAY1)
)
/

/*==============================================================*/
/* Table: CXZLP_XZLPSLDEF                                       */
/*==============================================================*/
create table BFCRM10.CXZLP_XZLPSLDEF  (
   CXID                 NUMBER(10)                      not null,
   MDID                 NUMBER(10)                      not null,
   LPDM                 VARCHAR2(6)                     not null,
   LPSL_DAY             NUMBER(10)                     default 0,
   constraint PK_CXZLP_XZLPSLDEF primary key (CXID, MDID, LPDM)
)
/

/*==============================================================*/
/* Table: CZK_FXMD                                              */
/*==============================================================*/
create table BFCRM10.CZK_FXMD  (
   HYKTYPE              NUMBER(5)                       not null,
   FXMDID               NUMBER(5)                       not null,
   MDID                 NUMBER(5)                       not null,
   constraint PK_CZK_FXMD primary key (HYKTYPE, FXMDID, MDID)
)
/

/*==============================================================*/
/* Table: CZK_JKD                                               */
/*==============================================================*/
create table BFCRM10.CZK_JKD  (
   JLBH                 NUMBER(10)                      not null,
   RQ                   DATE                            not null,
   RYID                 NUMBER(10)                      not null,
   RYMC                 VARCHAR2(20)                    not null,
   JKJE                 NUMBER(14,2),
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16)                    not null,
   DJSJ                 DATE                            not null,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(16),
   ZXRQ                 DATE,
   CZDD                 VARCHAR2(10),
   constraint PK_CZK_JKD primary key (JLBH)
)
/

/*==============================================================*/
/* Table: CZK_JKDMX                                             */
/*==============================================================*/
create table BFCRM10.CZK_JKDMX  (
   JLBH                 NUMBER(10)                      not null,
   ZFFSID               NUMBER(10)                      not null,
   JE                   NUMBER(14,2)                    not null,
   constraint PK_CZK_JKDMX primary key (JLBH, ZFFSID)
)
/

/*==============================================================*/
/* Table: CZK_KHDA                                              */
/*==============================================================*/
create table BFCRM10.CZK_KHDA  (
   KHID                 NUMBER(10)                      not null,
   KHDM                 VARCHAR2(10)                    not null,
   KHMC                 VARCHAR2(60),
   KHXZ                 VARCHAR2(20),
   KHDZ                 VARCHAR2(60),
   KHWZ                 VARCHAR2(30),
   KHKHH                VARCHAR2(30),
   LXRXM                VARCHAR2(16),
   LXRDH                VARCHAR2(30),
   LXRSJ                VARCHAR2(30),
   LXRTXDZ              VARCHAR2(60),
   LXRYB                VARCHAR2(6),
   LXREMAIL             VARCHAR2(30),
   LXRZJ                NUMBER(10),
   LXRZJHM              VARCHAR2(30),
   BZ                   VARCHAR2(100),
   PYM                  VARCHAR2(10),
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16)                    not null,
   DJSJ                 DATE                            not null,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(16),
   ZXRQ                 DATE,
   BXH                  VARCHAR2(20),
   NBBMBH               NUMBER(10),
   MDID                 NUMBER(10),
   constraint PK_CZK_KHDA primary key (KHID)
)
/

/*==============================================================*/
/* Index: INDEX_CZK_KHDA_KHMC                                   */
/*==============================================================*/
create unique index BFCRM10.INDEX_CZK_KHDA_KHMC on BFCRM10.CZK_KHDA (
   KHMC ASC
)
/

/*==============================================================*/
/* Table: CZK_MD                                                */
/*==============================================================*/
create table BFCRM10.CZK_MD  (
   MDID                 NUMBER(5)                       not null,
   HYKTYPE              NUMBER(5)                       not null,
   constraint PK_CZK_MD primary key (MDID, HYKTYPE)
)
/

/*==============================================================*/
/* Table: CZK_MZ                                                */
/*==============================================================*/
create table BFCRM10.CZK_MZ  (
   CODE                 NUMBER(10)                      not null,
   JE                   NUMBER(14,2)                    not null,
   constraint PK_CZK_MZ primary key (CODE)
)
/

/*==============================================================*/
/* Table: CZK_QKGZJL                                            */
/*==============================================================*/
create table BFCRM10.CZK_QKGZJL  (
   JLBH                 NUMBER(10)                      not null,
   FKJE                 NUMBER(14,2)                    not null,
   QDSL                 NUMBER(10),
   ZY                   VARCHAR2(40),
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16)                    not null,
   DJSJ                 DATE                            not null,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(16),
   ZXRQ                 DATE,
   constraint PK_CZK_QKGZJL primary key (JLBH)
)
/

/*==============================================================*/
/* Table: CZK_QKGZJLMX                                          */
/*==============================================================*/
create table BFCRM10.CZK_QKGZJLMX  (
   JLBH                 NUMBER(10)                      not null,
   CZKHM                VARCHAR2(30)                    not null,
   SKJLBH               NUMBER(10)                      not null,
   QCYE                 NUMBER(14,2)                    not null,
   HYID                 NUMBER(10)                      not null,
   constraint PK_CZK_QKGZJLMX primary key (JLBH, CZKHM)
)
/

/*==============================================================*/
/* Table: CZK_RHZ                                               */
/*==============================================================*/
create table BFCRM10.CZK_RHZ  (
   RQ                   DATE                            not null,
   HYID                 NUMBER(10)                      not null,
   SQYE                 NUMBER(14,2)                   default 0,
   JKJE                 NUMBER(14,2)                   default 0,
   CKJE                 NUMBER(14,2)                   default 0,
   QKJE                 NUMBER(14,2)                   default 0,
   XFJE                 NUMBER(14,2)                   default 0,
   ZFJE                 NUMBER(14,2)                   default 0,
   QMYE                 NUMBER(14,2)                   default 0,
   constraint PK_CZK_RHZ primary key (RQ, HYID)
)
/

/*==============================================================*/
/* Table: CZK_XFBM                                              */
/*==============================================================*/
create table BFCRM10.CZK_XFBM  (
   HYID                 NUMBER(10)                      not null,
   KSRQ                 DATE                            not null,
   JSRQ                 DATE                            not null,
   SHBMID               NUMBER(10)                      not null,
   SHDM                 VARCHAR2(6)                     not null,
   DJR                  NUMBER(10),
   DJRMC                VARCHAR2(20),
   DJRQ                 DATE,
   constraint PK_CZK_XFBM primary key (HYID, KSRQ, JSRQ, SHBMID)
)
/

/*==============================================================*/
/* Table: CZK_XFMX_HZ                                           */
/*==============================================================*/
create table BFCRM10.CZK_XFMX_HZ  (
   RQ                   DATE                            not null,
   HYID                 NUMBER(10)                      not null,
   SHSPID               NUMBER(10)                      not null,
   KHID                 NUMBER(10),
   SHSPFLID             NUMBER(10),
   SHSBID               NUMBER(10),
   CZK_XFJE             NUMBER(14,2)                   default 0,
   constraint PK_CZK_XFMX_HZ primary key (RQ, HYID, SHSPID)
)
/

/*==============================================================*/
/* Table: CZK_YEQLTZD                                           */
/*==============================================================*/
create table BFCRM10.CZK_YEQLTZD  (
   JLBH                 NUMBER(10)                      not null,
   HYKTYPE              NUMBER(10)                      not null,
   TZSL                 NUMBER(10),
   TZJE                 NUMBER(14,2),
   ZY                   VARCHAR2(40),
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16)                    not null,
   DJSJ                 DATE                            not null,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(16),
   ZXRQ                 DATE,
   CZDD                 VARCHAR2(10),
   constraint PK_CZK_YEQLTZD primary key (JLBH)
)
/

/*==============================================================*/
/* Table: CZK_YEQLTZD_ITEM                                      */
/*==============================================================*/
create table BFCRM10.CZK_YEQLTZD_ITEM  (
   JLBH                 NUMBER(10)                      not null,
   HYID                 NUMBER(10)                      not null,
   YYE                  NUMBER(14,2),
   TZJE                 NUMBER(14,2),
   constraint PK_CZK_YEQLTZD_ITEM primary key (JLBH, HYID)
)
/

/*==============================================================*/
/* Table: CZYGROUP_BGDDQX                                       */
/*==============================================================*/
create table BFCRM10.CZYGROUP_BGDDQX  (
   ID                   NUMBER(5)                       not null,
   BGDDDM               VARCHAR2(20)                    not null,
   constraint PK_CZYGROUP_BGDDQX primary key (ID, BGDDDM)
)
/

/*==============================================================*/
/* Table: CZYGROUP_BMCZQX                                       */
/*==============================================================*/
create table BFCRM10.CZYGROUP_BMCZQX  (
   ID                   NUMBER(5)                       not null,
   SHDM                 VARCHAR2(4)                     not null,
   BMDM                 VARCHAR2(10)                    not null,
   DM                   VARCHAR2(14),
   constraint PK_CZYGROUP_BMCZQX primary key (ID, SHDM, BMDM)
)
/

/*==============================================================*/
/* Table: CZYGROUP_FXDWQX                                       */
/*==============================================================*/
create table BFCRM10.CZYGROUP_FXDWQX  (
   ID                   NUMBER(5)                       not null,
   FXDWDM               VARCHAR2(10)                    not null,
   constraint PK_CZYGROUP_FXDWQX primary key (ID, FXDWDM)
)
/

/*==============================================================*/
/* Table: CZYGROUP_HYLXQX                                       */
/*==============================================================*/
create table BFCRM10.CZYGROUP_HYLXQX  (
   ID                   NUMBER(5)                       not null,
   HYKTYPE              NUMBER(5)                       not null,
   constraint PK_CZYGROUP_HYLXQX primary key (ID, HYKTYPE)
)
/

/*==============================================================*/
/* Table: CZYGROUP_MDQX                                         */
/*==============================================================*/
create table BFCRM10.CZYGROUP_MDQX  (
   ID                   NUMBER(10)                      not null,
   MDID                 NUMBER(10)                      not null,
   constraint PK_CZYGROUP_MDQX primary key (ID, MDID)
)
/

/*==============================================================*/
/* Table: DEF_CZY_CRMPOS_QX                                     */
/*==============================================================*/
create table BFCRM10.DEF_CZY_CRMPOS_QX  (
   PERSON_ID            NUMBER(10)                      not null,
   BJ_CX                NUMBER(5)                      default 0 not null,
   BJ_JY                NUMBER(5)                      default 0 not null,
   BJ_CZ                NUMBER(5)                      default 0 not null,
   constraint PK_DEF_CZY_CRMPOS_QX primary key (PERSON_ID)
)
/

/*==============================================================*/
/* Table: DEF_CZY_SKT                                           */
/*==============================================================*/
create table BFCRM10.DEF_CZY_SKT  (
   PERSON_ID            NUMBER(10)                      not null,
   MDID                 NUMBER(10)                      not null,
   SKTNO                VARCHAR2(6)                     not null,
   constraint PK_DEF_CZY_SKT primary key (PERSON_ID, MDID, SKTNO)
)
/

/*==============================================================*/
/* Table: DEF_QTKGLY                                            */
/*==============================================================*/
create table BFCRM10.DEF_QTKGLY  (
   MDID                 NUMBER(10)                      not null,
   PERSON_ID            NUMBER(10)                      not null,
   CZDD                 VARCHAR2(10),
   constraint PK_DEF_QTKGLY primary key (MDID)
)
/

/*==============================================================*/
/* Table: DEF_SKGLY                                             */
/*==============================================================*/
create table BFCRM10.DEF_SKGLY  (
   PERSON_ID            NUMBER(10)                      not null,
   PASSWORD             VARCHAR2(30)                    not null,
   constraint PK_DEF_SKGLY primary key (PERSON_ID)
)
/

/*==============================================================*/
/* Table: DGXX                                                  */
/*==============================================================*/
create table BFCRM10.DGXX  (
   JLBH                 NUMBER(10)                      not null,
   TITLE                varchar2(40),
   KSRQ                 DATE,
   JSRQ                 DATE,
   DJR                  NUMBER(10),
   DJSJ                 DATE,
   DJRMC                varchar2(16),
   TEXTCONT             BLOB,
   MDID                 NUMBER(10),
   TYPE                 NUMBER(10),
   constraint PK_DGXX primary key (JLBH)
)
/

/*==============================================================*/
/* Table: DJZT                                                  */
/*==============================================================*/
create table BFCRM10.DJZT  (
   MSGID                NUMBER(10)                      not null,
   JLBH                 NUMBER(10)                      not null,
   DISPLAY_JLBH         VARCHAR2(10)                    not null,
   constraint PK_DJZT primary key (MSGID, JLBH)
)
/

/*==============================================================*/
/* Table: DMBDSJ                                                */
/*==============================================================*/
create table BFCRM10.DMBDSJ  (
   TBLNAME              VARCHAR2(40)                    not null,
   GXSJ                 DATE                            not null,
   constraint PK_DMBDSJ primary key (TBLNAME)
)
/

/*==============================================================*/
/* Table: DYCS                                                  */
/*==============================================================*/
create table BFCRM10.DYCS  (
   JLBH                 NUMBER(10)                      not null,
   NUM                  NUMBER(10),
   constraint PK_DYCS primary key (JLBH)
)
/

/*==============================================================*/
/* Table: FQXEDYGZD                                             */
/*==============================================================*/
create table BFCRM10.FQXEDYGZD  (
   JLBH                 NUMBER(10)                      not null,
   CXID                 NUMBER(10)                      not null,
   DJLX                 NUMBER(5)                       not null,
   MDID                 NUMBER(5)                       not null,
   YHQID                NUMBER(5)                       not null,
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(20)                    not null,
   DJSJ                 DATE                            not null,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(20),
   ZXRQ                 DATE,
   QDR                  NUMBER(10),
   QDRMC                VARCHAR2(20),
   QDSJ                 DATE,
   STATUS               NUMBER(5)                       not null,
   ZZRQ                 DATE,
   ZZR                  NUMBER(10),
   ZZRMC                VARCHAR2(20),
   FQZXE                NUMBER(14,2)                   default 0 not null,
   constraint PK_FQXEDYGZD primary key (JLBH)
)
/

/*==============================================================*/
/* Table: FQXEDYGZD_ITEM                                        */
/*==============================================================*/
create table BFCRM10.FQXEDYGZD_ITEM  (
   JLBH                 NUMBER(10)                      not null,
   SJNR                 NUMBER(10)                      not null,
   FQXE                 NUMBER(14,2),
   FQZXE                NUMBER(14,2),
   MAX_FQCS             NUMBER(10),
   FQZXE_CARD           NUMBER(14,2),
   FQXE_DAY             NUMBER(14,2),
   FQXE_CARD_DAY        NUMBER(14,2),
   MAX_FQCS_DAY         NUMBER(10),
   constraint PK_FQXEDYGZD_ITEM primary key (JLBH, SJNR)
)
/

/*==============================================================*/
/* Table: FTPCONFIG                                             */
/*==============================================================*/
create table BFCRM10.FTPCONFIG  (
   ID                   NUMBER(10)                      not null,
   DIR                  VARCHAR2(60),
   PSWD                 VARCHAR2(60),
   FIRST_DIR            VARCHAR2(60),
   IP_NET               VARCHAR2(60),
   IP_PUB               VARCHAR2(60),
   constraint PK_FTPCONFIG primary key (ID)
)
/

/*==============================================================*/
/* Table: FTPLIST                                               */
/*==============================================================*/
create table BFCRM10.FTPLIST  (
   FTP                  VARCHAR2(20)                    not null,
   USERNAME             VARCHAR2(20),
   PASSWORD             VARCHAR2(20),
   BJ                   NUMBER(5),
   LASTTIME             DATE,
   constraint PK_FTPLIST primary key (FTP)
)
/

/*==============================================================*/
/* Table: FWNRDEF                                               */
/*==============================================================*/
create table BFCRM10.FWNRDEF  (
   FWNRID               NUMBER(5)                       not null,
   FWNRMC               VARCHAR2(60),
   BJ_TY                NUMBER(5)                      default 0,
   DW                   VARCHAR2(10),
   ZDFWSL               FLOAT(50),
   BJ_XZFWSL            NUMBER(5),
   constraint PK_FWNRDEF primary key (FWNRID)
)
/

/*==============================================================*/
/* Table: FXDWDEF                                               */
/*==============================================================*/
create sequence BFCRM10.S_FXDWDEF
/
create table BFCRM10.FXDWDEF  (
   FXDWID               NUMBER(10)                      not null,
   FXDWDM               VARCHAR2(10),
   FXDWMC               VARCHAR2(20)                    not null,
   HMCD                 NUMBER(10),
   KHQDM                VARCHAR2(10),
   KHHZM                VARCHAR2(10),
   BJ_TY                NUMBER(5)                      default 0,
   MJBJ                 NUMBER(5),
   TM                   NUMBER(10),
   constraint PK_FXDWDEF primary key (FXDWID)
)
/

/*==============================================================*/
/* Table: GHSDEF                                                */
/*==============================================================*/
create table BFCRM10.GHSDEF  (
   GHSID                NUMBER(10)                      not null,
   GHSMC                VARCHAR2(30),
   GHSDZ                VARCHAR2(50),
   DHHM                 VARCHAR2(30),
   ZYXM                 VARCHAR2(30),
   BJ_TY                NUMBER(5),
   constraint PK_GHSDEF primary key (GHSID)
)
/

/*==============================================================*/
/* Table: GW_SHDY                                               */
/*==============================================================*/
create table BFCRM10.GW_SHDY  (
   SHID                 NUMBER(10)                      not null,
   SHMC                 VARCHAR2(30)                    not null,
   constraint PK_GW_SHDY primary key (SHID)
)
/

comment on table BFCRM10.GW_SHDY is
'购物中心商铺定义'
/

/*==============================================================*/
/* Table: GW_SHJFGZ                                             */
/*==============================================================*/
create table BFCRM10.GW_SHJFGZ  (
   JLBH                 NUMBER(10)                      not null,
   SHID                 NUMBER(10)                      not null,
   HYKTYPE              NUMBER(10)                      not null,
   JE                   NUMBER(14,2),
   JF                   NUMBER(14,2)                    not null,
   JF_SRR               NUMBER(14,2)                    not null,
   JF_SRY               NUMBER(14,2)                    not null,
   DJR                  NUMBER(10),
   DJRMC                VARCHAR2(30),
   DJSJ                 DATE,
   constraint PK_GW_SHJFGZ primary key (JLBH)
)
/

comment on table BFCRM10.GW_SHJFGZ is
'购物中心版的简单商户积分规则定义'
/

/*==============================================================*/
/* Table: GW_SHJFJL                                             */
/*==============================================================*/
create table BFCRM10.GW_SHJFJL  (
   JLBH                 NUMBER(10)                      not null,
   SHID                 NUMBER(10),
   HYID                 NUMBER(10),
   JFGZBH               NUMBER(10),
   IMG                  VARCHAR2(100),
   PUBLICID             NUMBER(10),
   UNIONID              VARCHAR2(70),
   OPENID               VARCHAR2(70),
   XPH                  VARCHAR2(30),
   XFSJ                 DATE,
   JE                   NUMBER(14,2),
   JF                   NUMBER(14,2),
   STATUS               NUMBER(5),
   DJR                  NUMBER(10),
   DJRMC                VARCHAR2(30),
   DJSJ                 DATE,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(30),
   ZXRQ                 DATE,
   ZY                   VARCHAR2(100),
   constraint PK_GW_SHJFJL primary key (JLBH)
)
/

comment on table BFCRM10.GW_SHJFJL is
'购物中心商户手工积分记录'
/

/*==============================================================*/
/* Table: GXSHXXDY                                              */
/*==============================================================*/
create sequence BFCRM10.S_GXSHXXDY
/
create table BFCRM10.GXSHXXDY  (
   GXSHDM               VARCHAR2(6)                     not null,
   GXSHMC               VARCHAR2(30),
   TM                   NUMBER(10),
   constraint PK_GXSHXXDY primary key (GXSHDM)
)
/

/*==============================================================*/
/* Table: GYSXX                                                 */
/*==============================================================*/
create table BFCRM10.GYSXX  (
   GYSDM                VARCHAR2(20)                    not null,
   GYSMC                VARCHAR2(60)                    not null,
   LXDH                 VARCHAR2(20),
   DZ                   VARCHAR2(60),
   STATUS               NUMBER(5)                       not null,
   BJ_ZYCS              NUMBER(5),
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(20)                    not null,
   DJSJ                 DATE,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(20),
   ZXRQ                 DATE,
   constraint PK_GYSXX primary key (GYSDM)
)
/

/*==============================================================*/
/* Table: GYS_LPDHD                                             */
/*==============================================================*/
create table BFCRM10.GYS_LPDHD  (
   JLBH                 NUMBER(10)                      not null,
   BGDDDM               VARCHAR2(20)                    not null,
   GYSDM                VARCHAR2(20)                    not null,
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16)                    not null,
   DJSJ                 DATE                            not null,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(16),
   ZXRQ                 DATE,
   STATUS               NUMBER(5)                       not null,
   JHDBH                NUMBER(10),
   ZZR                  NUMBER(10),
   ZZRMC                VARCHAR2(16),
   ZZSJ                 DATE,
   constraint PK_GYS_LPDHD primary key (JLBH)
)
/

/*==============================================================*/
/* Table: GYS_LPDHDITEM                                         */
/*==============================================================*/
create table BFCRM10.GYS_LPDHDITEM  (
   JLBH                 NUMBER(10)                      not null,
   LPID                 NUMBER(10)                      not null,
   LPJJ                 NUMBER(14,2)                    not null,
   LPDJ                 NUMBER(14,2)                    not null,
   DHSL                 NUMBER(14,4)                    not null,
   constraint PK_GYS_LPDHDITEM primary key (JLBH, LPID)
)
/

/*==============================================================*/
/* Table: GYS_LPDHD_SQDMX                                       */
/*==============================================================*/
create table BFCRM10.GYS_LPDHD_SQDMX  (
   JLBH                 NUMBER(10)                      not null,
   SQDJH                NUMBER(10)                      not null,
   constraint PK_GYS_LPDHD_SQDMX primary key (JLBH, SQDJH)
)
/

/*==============================================================*/
/* Table: GYS_LPSQD                                             */
/*==============================================================*/
create table BFCRM10.GYS_LPSQD  (
   JLBH                 NUMBER(10)                      not null,
   SQDD                 VARCHAR2(20)                    not null,
   BGDDDM               VARCHAR2(20)                    not null,
   SQRID                NUMBER(10)                      not null,
   GYSDM                VARCHAR2(20)                    not null,
   SHDM                 VARCHAR2(10)                    not null,
   CXHDBH               NUMBER(10)                      not null,
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16)                    not null,
   DJSJ                 DATE                            not null,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(16),
   ZXRQ                 DATE,
   STATUS               NUMBER(5)                       not null,
   BJ_PS                NUMBER(5)                       not null,
   DHDBH                NUMBER(10),
   DCDBH                NUMBER(10),
   ZZR                  NUMBER(10),
   ZZRMC                VARCHAR2(16),
   ZZSJ                 DATE,
   constraint PK_GYS_LPSQD primary key (JLBH)
)
/

/*==============================================================*/
/* Table: GYS_LPSQDITEM                                         */
/*==============================================================*/
create table BFCRM10.GYS_LPSQDITEM  (
   JLBH                 NUMBER(10)                      not null,
   LPID                 NUMBER(10)                      not null,
   LPJJ                 NUMBER(14,2)                    not null,
   LPDJ                 NUMBER(14,2)                    not null,
   SQSL                 NUMBER(14,4)                    not null,
   constraint PK_GYS_LPSQDITEM primary key (JLBH, LPID)
)
/

/*==============================================================*/
/* Table: GZSJ_HYFQ                                             */
/*==============================================================*/
create table BFCRM10.GZSJ_HYFQ  (
   YHQID                NUMBER(10)                      not null,
   SHDM                 VARCHAR2(4)                     not null,
   SHBMDM               VARCHAR2(10)                    not null,
   SHSPID               NUMBER(10)                      not null,
   INX                  NUMBER(5)                       not null,
   RQ1                  DATE                            not null,
   RQ2                  DATE                            not null,
   CXSD                 RAW(96)                         not null,
   JLBH                 NUMBER(10)                      not null,
   FQGZID               NUMBER(10)                      not null,
   constraint PK_GZSJ_HYFQ primary key (YHQID, SHDM, SHBMDM, SHSPID, INX)
)
/

comment on column BFCRM10.GZSJ_HYFQ.CXSD is
'每一个字节表示30分钟区域，开始时间为0点
字节中第2位（bit1)星期一、bit2表示星期二...
bit7表示星期天，第一位(bit0)不使用
'
/

/*==============================================================*/
/* Table: GZSJ_HYJF                                             */
/*==============================================================*/
create table BFCRM10.GZSJ_HYJF  (
   HYKTYPE              NUMBER(10)                      not null,
   SHDM                 VARCHAR2(4)                     not null,
   SHBMDM               VARCHAR2(10)                    not null,
   SHSPID               NUMBER(10)                      not null,
   INX                  NUMBER(5)                       not null,
   RQ1                  DATE                            not null,
   RQ2                  DATE                            not null,
   CXSD                 RAW(96)                         not null,
   JLBH                 NUMBER(10)                      not null,
   FZ                   FLOAT(50)                       not null,
   constraint PK_GZSJ_HYJF primary key (HYKTYPE, SHDM, SHBMDM, SHSPID, INX)
)
/

comment on column BFCRM10.GZSJ_HYJF.CXSD is
'每一个字节表示30分钟区域，开始时间为0点
字节中第2位（bit1)星期一、bit2表示星期二...
bit7表示星期天，第一位(bit0)不使用
'
/

/*==============================================================*/
/* Index: GZSJ_CXMBJZ_JLBH                                      */
/*==============================================================*/
create index BFCRM10.GZSJ_CXMBJZ_JLBH on BFCRM10.GZSJ_HYJF (
   JLBH ASC
)
/

/*==============================================================*/
/* Table: GZSJ_HYYQ                                             */
/*==============================================================*/
create table BFCRM10.GZSJ_HYYQ  (
   YHQID                NUMBER(10)                      not null,
   SHDM                 VARCHAR2(4)                     not null,
   SHBMDM               VARCHAR2(10)                    not null,
   SHSPID               NUMBER(10)                      not null,
   INX                  NUMBER(5)                       not null,
   RQ1                  DATE                            not null,
   RQ2                  DATE                            not null,
   CXSD                 RAW(96)                         not null,
   JLBH                 NUMBER(10)                      not null,
   YQGZID               NUMBER(10)                      not null,
   constraint PK_GZSJ_HYYQ primary key (YHQID, SHDM, SHBMDM, SHSPID, INX)
)
/

comment on column BFCRM10.GZSJ_HYYQ.CXSD is
'每一个字节表示30分钟区域，开始时间为0点
字节中第2位（bit1)星期一、bit2表示星期二...
bit7表示星期天，第一位(bit0)不使用
'
/

/*==============================================================*/
/* Table: GZSJ_HYZKD                                            */
/*==============================================================*/
create table BFCRM10.GZSJ_HYZKD  (
   HYKTYPE              NUMBER(10)                      not null,
   SHDM                 VARCHAR2(4)                     not null,
   SHBMDM               VARCHAR2(10)                    not null,
   SHSPID               NUMBER(10)                      not null,
   INX                  NUMBER(5)                       not null,
   RQ1                  DATE                            not null,
   RQ2                  DATE                            not null,
   CXSD                 RAW(96)                         not null,
   JLBH                 NUMBER(10)                      not null,
   ZKL                  FLOAT(50)                       not null,
   constraint PK_GZSJ_HYZKD primary key (HYKTYPE, SHDM, SHBMDM, SHSPID, INX)
)
/

comment on column BFCRM10.GZSJ_HYZKD.CXSD is
'每一个字节表示30分钟区域，开始时间为0点
字节中第2位（bit1)星期一、bit2表示星期二...
bit7表示星期天，第一位(bit0)不使用
'
/

/*==============================================================*/
/* Index: GZSJ_HYZKD_JLBH                                       */
/*==============================================================*/
create index BFCRM10.GZSJ_HYZKD_JLBH on BFCRM10.GZSJ_HYZKD (
   JLBH ASC
)
/

/*==============================================================*/
/* Table: GZSJ_MBJZ                                             */
/*==============================================================*/
create table BFCRM10.GZSJ_MBJZ  (
   SHDM                 VARCHAR2(4)                     not null,
   SHBMDM               VARCHAR2(10)                    not null,
   SHSPID               NUMBER(10)                      not null,
   INX                  NUMBER(5)                       not null,
   RQ1                  DATE                            not null,
   RQ2                  DATE                            not null,
   CXSD                 RAW(48)                         not null,
   JLBH                 NUMBER(10)                      not null,
   MBJZGZID             NUMBER(10)                      not null,
   constraint PK_GZSJ_MBJZ primary key (SHDM, SHBMDM, SHSPID, INX)
)
/

comment on column BFCRM10.GZSJ_MBJZ.CXSD is
'每一个字节表示30分钟区域，开始时间为0点
字节中第2位（bit1)星期一、bit2表示星期二...
bit7表示星期天，第一位(bit0)不使用
'
/

/*==============================================================*/
/* Table: GZSJ_YHQSY                                            */
/*==============================================================*/
create table BFCRM10.GZSJ_YHQSY  (
   YHQID                NUMBER(5)                       not null,
   SHDM                 VARCHAR2(4)                     not null,
   SHBMID               NUMBER(10)                      not null,
   SHSPID               NUMBER(10)                      not null,
   YXJ                  NUMBER(5)                       not null,
   SD                   RAW(96)                         not null,
   SJBJ                 NUMBER(1)                       not null,
   YHQSYDBH             NUMBER(10)                      not null,
   constraint PK_GZSJ_YHQSY primary key (YHQID, SHDM, SHBMID, SHSPID, YXJ)
)
/

/*==============================================================*/
/* Table: GZXFJFR_CRM                                           */
/*==============================================================*/
create sequence BFCRM10.S_GZXFJFR_CRM
/
create table BFCRM10.GZXFJFR_CRM  (
   JZRQ                 DATE                            not null,
   LX                   NUMBER(5)                       not null,
   FDBH                 NUMBER(10)                      not null,
   DEPTID               VARCHAR2(10)                    not null,
   HTH                  VARCHAR2(10)                    not null,
   VIPTYPE              NUMBER(5)                       not null,
   JE                   NUMBER(14,2),
   WLDW                 VARCHAR2(6),
   JF                   FLOAT(50),
   YEARMONTH            NUMBER(10),
   JSD_JLB              NUMBER(10),
   JSBJ                 NUMBER(10),
   TM                   NUMBER(10),
   constraint PK_GZXFJFR_CRM primary key (JZRQ, FDBH, DEPTID, HTH, VIPTYPE, LX)
)
/

/*==============================================================*/
/* Table: HT_HYK_CZCLJL                                         */
/*==============================================================*/
create table BFCRM10.HT_HYK_CZCLJL  (
   JLBH                 NUMBER(10)                      not null,
   JYID                 NUMBER(10)                      not null,
   JYLX                 NUMBER(10)                     default 0 not null,
   HYID                 NUMBER(10)                      not null,
   MDID                 NUMBER(10)                      not null,
   SKTNO                VARCHAR2(6)                     not null,
   CZJE                 NUMBER(14,2)                   default 0,
   JYRQ                 DATE                            not null,
   JZRQ                 DATE,
   ZY                   VARCHAR2(40),
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16)                    not null,
   DJSJ                 DATE                            not null,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(16),
   ZXRQ                 DATE,
   constraint PK_HT_HYK_CZCLJL primary key (JLBH)
)
/

/*==============================================================*/
/* Table: HYDJSQJL                                              */
/*==============================================================*/
create table BFCRM10.HYDJSQJL  (
   HYID                 NUMBER(10)                      not null,
   YXQ                  DATE                            not null,
   HYKTYPE              NUMBER(10),
   HYKTYPE_NEW          NUMBER(10),
   BQJF                 FLOAT(50),
   SJRQ                 DATE,
   QDJE                 NUMBER(14,2),
   SJQDJE               NUMBER(14,2),
   BJ_SJ                NUMBER(5),
   STATUS               NUMBER(5),
   constraint PK_HYDJSQJL primary key (HYID, YXQ)
)
/

/*==============================================================*/
/* Index: "INX_HYK_DJSQITEM_HYIDab"                             */
/*==============================================================*/
create index BFCRM10."INX_HYK_DJSQITEM_HYIDab" on BFCRM10.HYDJSQJL (
   HYID ASC
)
/

/*==============================================================*/
/* Table: HYFXNLD_XF                                            */
/*==============================================================*/
create table BFCRM10.HYFXNLD_XF  (
   ID                   NUMBER(10)                      not null,
   RQ                   DATE                            not null,
   HYKTYPE              NUMBER(5)                       not null,
   FL                   VARCHAR2(30)                    not null,
   HYSL                 NUMBER(10)                      not null,
   XFHYSL               NUMBER(10)                      not null,
   XFCS                 NUMBER(10)                      not null,
   XFJE                 NUMBER(14,2)                    not null,
   ZXJE                 NUMBER(14,2)                    not null,
   ZDJE                 NUMBER(14,2)                    not null,
   XFJE_BY              NUMBER(14,2)                    not null,
   XFJE_BN              NUMBER(14,2)                    not null,
   constraint PK_HYFXNLD_XF primary key (RQ, HYKTYPE, FL)
)
/

/*==============================================================*/
/* Index: HYXFFX_NLD_ID                                         */
/*==============================================================*/
create unique index BFCRM10.HYXFFX_NLD_ID on BFCRM10.HYFXNLD_XF (
   ID ASC
)
/

/*==============================================================*/
/* Table: HYFXQY_XF                                             */
/*==============================================================*/
create table BFCRM10.HYFXQY_XF  (
   ID                   NUMBER(10)                      not null,
   RQ                   DATE                            not null,
   HYKTYPE              NUMBER(5)                       not null,
   QYID                 NUMBER(10)                      not null,
   HYSL                 NUMBER(10)                      not null,
   XFHYSL               NUMBER(10)                      not null,
   XFCS                 NUMBER(10)                      not null,
   XFJE                 NUMBER(14,2)                    not null,
   ZXJE                 NUMBER(14,2)                    not null,
   ZDJE                 NUMBER(14,2)                    not null,
   XFJE_BY              NUMBER(14,2)                    not null,
   XFJE_BN              NUMBER(14,2)                    not null,
   constraint PK_HYFXQY_XF primary key (RQ, QYID, HYKTYPE)
)
/

/*==============================================================*/
/* Index: HYXFFX_QY_ID                                          */
/*==============================================================*/
create unique index BFCRM10.HYXFFX_QY_ID on BFCRM10.HYFXQY_XF (
   ID ASC
)
/

/*==============================================================*/
/* Table: HYFXSR_XF                                             */
/*==============================================================*/
create table BFCRM10.HYFXSR_XF  (
   ID                   NUMBER(10)                      not null,
   RQ                   DATE                            not null,
   HYKTYPE              NUMBER(5)                       not null,
   FL                   VARCHAR2(30)                    not null,
   HYSL                 NUMBER(10)                      not null,
   XFHYSL               NUMBER(10)                      not null,
   XFCS                 NUMBER(10)                      not null,
   XFJE                 NUMBER(14,2)                    not null,
   ZXJE                 NUMBER(14,2)                    not null,
   ZDJE                 NUMBER(14,2)                    not null,
   XFJE_BY              NUMBER(14,2)                    not null,
   XFJE_BN              NUMBER(14,2)                    not null,
   constraint PK_HYFXSR_XF primary key (RQ, HYKTYPE, FL)
)
/

/*==============================================================*/
/* Index: HYXFFX_SR_ID                                          */
/*==============================================================*/
create unique index BFCRM10.HYXFFX_SR_ID on BFCRM10.HYFXSR_XF (
   ID ASC
)
/

/*==============================================================*/
/* Table: HYFXZY_XF                                             */
/*==============================================================*/
create table BFCRM10.HYFXZY_XF  (
   ID                   NUMBER(10)                      not null,
   RQ                   DATE                            not null,
   HYKTYPE              NUMBER(5)                       not null,
   FL                   VARCHAR2(30)                    not null,
   HYSL                 NUMBER(10)                      not null,
   XFHYSL               NUMBER(10)                      not null,
   XFCS                 NUMBER(10)                      not null,
   XFJE                 NUMBER(14,2)                    not null,
   ZXJE                 NUMBER(14,2)                    not null,
   ZDJE                 NUMBER(14,2)                    not null,
   XFJE_BY              NUMBER(14,2)                    not null,
   XFJE_BN              NUMBER(14,2)                    not null,
   constraint PK_HYFXZY_XF primary key (RQ, HYKTYPE, FL)
)
/

/*==============================================================*/
/* Index: HYXFFX_ZY_ID                                          */
/*==============================================================*/
create unique index BFCRM10.HYXFFX_ZY_ID on BFCRM10.HYFXZY_XF (
   ID ASC
)
/

/*==============================================================*/
/* Table: HYFX_FLXF                                             */
/*==============================================================*/
create table BFCRM10.HYFX_FLXF  (
   ID                   NUMBER(10)                      not null,
   SPFLID               NUMBER(10)                      not null,
   XFJE                 NUMBER(14,2)                    not null,
   constraint PK_HYFX_FLXF primary key (ID, SPFLID)
)
/

/*==============================================================*/
/* Table: HYFX_JGDXF                                            */
/*==============================================================*/
create table BFCRM10.HYFX_JGDXF  (
   ID                   NUMBER(10)                      not null,
   JGDID                NUMBER(5)                       not null,
   XFJE                 NUMBER(14,2)                    not null,
   constraint PK_HYFX_JGDXF primary key (ID, JGDID)
)
/

/*==============================================================*/
/* Table: HYFX_SBXF                                             */
/*==============================================================*/
create table BFCRM10.HYFX_SBXF  (
   ID                   NUMBER(10)                      not null,
   SBID                 NUMBER(10)                      not null,
   XFJE                 NUMBER(14,2)                    not null,
   constraint PK_HYFX_SBXF primary key (ID, SBID)
)
/

/*==============================================================*/
/* Table: HYFX_SJDXF                                            */
/*==============================================================*/
create table BFCRM10.HYFX_SJDXF  (
   ID                   NUMBER(10)                      not null,
   SD                   NUMBER(5)                       not null,
   XFCS                 NUMBER(10)                      not null,
   XFJE                 NUMBER(14,2)                    not null,
   constraint PK_HYFX_SJDXF primary key (ID, SD)
)
/

/*==============================================================*/
/* Table: HYFX_XF                                               */
/*==============================================================*/
create table BFCRM10.HYFX_XF  (
   ID                   NUMBER(9)                       not null,
   HYSXLX               NUMBER(5)                       not null,
   RQ                   DATE                            not null,
   HYKTYPE              NUMBER(5)                       not null,
   HYSXID               NUMBER(10)                      not null,
   HYSL                 NUMBER(10)                      not null,
   XFHYSL               NUMBER(10)                      not null,
   XFCS                 NUMBER(10)                      not null,
   XFJE                 NUMBER(14,2)                    not null,
   ZXJE                 NUMBER(14,2)                    not null,
   ZDJE                 NUMBER(14,2)                    not null,
   XFJE_BY              NUMBER(14,2)                    not null,
   XFJE_BN              NUMBER(14,2)                    not null,
   constraint PK_HYFX_XF primary key (HYSXLX, RQ, HYKTYPE, HYSXID)
)
/

/*==============================================================*/
/* Index: HYXFFX_ID                                             */
/*==============================================================*/
create unique index BFCRM10.HYXFFX_ID on BFCRM10.HYFX_XF (
   ID ASC
)
/

/*==============================================================*/
/* Table: HYHFJL                                                */
/*==============================================================*/
create table BFCRM10.HYHFJL  (
   JLBH                 NUMBER(10)                      not null,
   HYID                 NUMBER(10),
   HFNR                 varchar2(100),
   HFJG                 NUMBER(10),
   ZGPS                 varchar2(100),
   DJR                  NUMBER(10)                      not null,
   DJRMC                varchar2(16)                    not null,
   DJSJ                 DATE                            not null,
   ZXR                  NUMBER(10),
   ZXRMC                varchar2(16),
   ZXRQ                 DATE,
   constraint PK_HYHFJL primary key (JLBH)
)
/

/*==============================================================*/
/* Table: HYID_CZK_RBB                                          */
/*==============================================================*/
create table BFCRM10.HYID_CZK_RBB  (
   YEARMONTH            NUMBER(10)                      not null,
   HYID                 NUMBER(10)                      not null,
   SQYE                 NUMBER(14,2)                   default 0 not null,
   JKJE                 NUMBER(14,2)                   default 0 not null,
   CKJE                 NUMBER(14,2)                   default 0 not null,
   QKJE                 NUMBER(14,2)                   default 0 not null,
   BKJE                 NUMBER(14,2)                   default 0,
   XFJE                 NUMBER(14,2)                   default 0 not null,
   TKJE                 NUMBER(14,2)                   default 0,
   TZJE                 NUMBER(14,2)                   default 0,
   QMYE                 NUMBER(14,2)                   default 0 not null,
   constraint PK_HYID_CZK_RBB primary key (YEARMONTH, HYID)
)
/

/*==============================================================*/
/* Table: HYJFDYD                                               */
/*==============================================================*/
create table BFCRM10.HYJFDYD  (
   JFDYDBH              NUMBER(10)                      not null,
   HYKTYPE              NUMBER(5)                       not null,
   SHDM                 VARCHAR2(4)                     not null,
   SHBMID               NUMBER(10)                      not null,
   SHBMDM               VARCHAR2(20)                    not null,
   RQ1                  DATE,
   RQ2                  DATE,
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16)                    not null,
   DJSJ                 DATE                            not null,
   SHR                  NUMBER(10),
   SHRMC                VARCHAR2(16),
   SHSJ                 DATE,
   QDR                  NUMBER(10),
   QDRMC                VARCHAR2(16),
   QDSJ                 DATE,
   STATUS               NUMBER(5)                       not null,
   DJLX                 NUMBER(5)                      default 0 not null,
   ZZRQ                 DATE,
   ZZR                  NUMBER(10),
   ZZRMC                VARCHAR2(16),
   constraint PK_HYJFDYD primary key (JFDYDBH)
)
/

/*==============================================================*/
/* Table: HYJFDYDJ                                              */
/*==============================================================*/
create table BFCRM10.HYJFDYDJ  (
   HYKTYPE              NUMBER(5)                       not null,
   SHDM                 VARCHAR2(4)                     not null,
   SHBMDM               VARCHAR2(20)                    not null,
   JFDYDBH              NUMBER(10)                      not null,
   OLD_JFDYDBH          NUMBER(10),
   NEW_JFDYDBH          NUMBER(10),
   constraint PK_HYJFDYDJ primary key (HYKTYPE, SHDM, SHBMDM)
)
/

/*==============================================================*/
/* Table: HYJFDYD_BM                                            */
/*==============================================================*/
create table BFCRM10.HYJFDYD_BM  (
   JFDYDBH              NUMBER(10)                      not null,
   INX                  NUMBER(5)                       not null,
   SHBMID               NUMBER(10)                      not null,
   FZ                   FLOAT(50)                       not null,
   constraint PK_HYJFDYD_BM primary key (JFDYDBH, INX, SHBMID)
)
/

/*==============================================================*/
/* Table: HYJFDYD_FD                                            */
/*==============================================================*/
create table BFCRM10.HYJFDYD_FD  (
   JFDYDBH              NUMBER(10)                      not null,
   INX                  NUMBER(5)                       not null,
   SD                   RAW(96)                         not null,
   YXJ                  NUMBER(5)                       not null,
   CLSX                 VARCHAR2(10)                    not null,
   constraint PK_HYJFDYD_FD primary key (JFDYDBH, INX)
)
/

/*==============================================================*/
/* Table: HYJFDYD_HT                                            */
/*==============================================================*/
create table BFCRM10.HYJFDYD_HT  (
   JFDYDBH              NUMBER(10)                      not null,
   INX                  NUMBER(5)                       not null,
   SHHTID               NUMBER(10)                      not null,
   FZ                   FLOAT(50)                       not null,
   constraint PK_HYJFDYD_HT primary key (JFDYDBH, INX, SHHTID)
)
/

/*==============================================================*/
/* Table: HYJFDYD_SB                                            */
/*==============================================================*/
create table BFCRM10.HYJFDYD_SB  (
   JFDYDBH              NUMBER(10)                      not null,
   INX                  NUMBER(5)                       not null,
   SHSBID               NUMBER(10)                      not null,
   FZ                   FLOAT(50)                       not null,
   constraint PK_HYJFDYD_SB primary key (JFDYDBH, INX, SHSBID)
)
/

/*==============================================================*/
/* Table: HYJFDYD_SP                                            */
/*==============================================================*/
create table BFCRM10.HYJFDYD_SP  (
   JFDYDBH              NUMBER(10)                      not null,
   INX                  NUMBER(5)                       not null,
   SHSPID               NUMBER(10)                      not null,
   FZ                   FLOAT(50)                       not null,
   constraint PK_HYJFDYD_SP primary key (JFDYDBH, INX, SHSPID)
)
/

/*==============================================================*/
/* Table: HYJFDYD_SPFL                                          */
/*==============================================================*/
create table BFCRM10.HYJFDYD_SPFL  (
   JFDYDBH              NUMBER(10)                      not null,
   INX                  NUMBER(5)                       not null,
   SHSPFLID             NUMBER(10)                      not null,
   FZ                   FLOAT(50)                       not null,
   constraint PK_HYJFDYD_SPFL primary key (JFDYDBH, INX, SHSPFLID)
)
/

/*==============================================================*/
/* Table: HYKCARD                                               */
/*==============================================================*/
create table BFCRM10.HYKCARD  (
   CZKHM                VARCHAR2(30)                    not null,
   CDNR                 VARCHAR2(60)                    not null,
   HYKTYPE              NUMBER(5),
   QCYE                 NUMBER(14,2)                    not null,
   YXTZJE               NUMBER(14,2)                    not null,
   PDJE                 NUMBER(14,2)                   default 0 not null,
   BGDDDM               VARCHAR2(20),
   BGR                  NUMBER(10)                      not null,
   JKFS                 NUMBER(5)                      default 0 not null,
   SKJLBH               NUMBER(10),
   STATUS               NUMBER(5),
   YXQ                  DATE,
   XKRQ                 DATE,
   BJ_YK                NUMBER(5)                      default 0,
   HSK_JLBH             NUMBER(10),
   MDID                 NUMBER(10),
   FXDWID               NUMBER(10),
   YZM                  VARCHAR2(20),
   constraint PK_HYKCARD primary key (CZKHM)
)
/

/*==============================================================*/
/* Index: HYKCARD_CDNR                                          */
/*==============================================================*/
create unique index BFCRM10.HYKCARD_CDNR on BFCRM10.HYKCARD (
   CDNR ASC
)
/

/*==============================================================*/
/* Index: HYKCARD_BGDDDM_QCYE                                   */
/*==============================================================*/
create index BFCRM10.HYKCARD_BGDDDM_QCYE on BFCRM10.HYKCARD (
   HYKTYPE ASC,
   QCYE ASC,
   BGDDDM ASC,
   BGR ASC
)
/

/*==============================================================*/
/* Index: HYKCARD_BGDDDM                                        */
/*==============================================================*/
create index BFCRM10.HYKCARD_BGDDDM on BFCRM10.HYKCARD (
   HYKTYPE ASC,
   BGDDDM ASC,
   BGR ASC
)
/

/*==============================================================*/
/* Index: HYKCARD_SKJLBH                                        */
/*==============================================================*/
create index BFCRM10.HYKCARD_SKJLBH on BFCRM10.HYKCARD (
   SKJLBH ASC
)
/

/*==============================================================*/
/* Table: HYKCARD_BAK                                           */
/*==============================================================*/
create table BFCRM10.HYKCARD_BAK  (
   CZKHM                VARCHAR2(30)                    not null,
   CDNR                 VARCHAR2(60)                    not null,
   HYKTYPE              NUMBER(5),
   QCYE                 NUMBER(14,2)                    not null,
   YXTZJE               NUMBER(14,2)                    not null,
   PDJE                 NUMBER(14,2)                   default 0 not null,
   BGDDDM               VARCHAR2(20),
   BGR                  NUMBER(10)                      not null,
   JKFS                 NUMBER(5)                      default 0 not null,
   SKJLBH               NUMBER(10),
   STATUS               NUMBER(5),
   YXQ                  DATE,
   XKRQ                 DATE,
   BJ_YK                NUMBER(5),
   HSK_JLBH             NUMBER(10),
   MDID                 NUMBER(10),
   FXDWID               NUMBER(10),
   YZM                  NUMBER(10),
   constraint PK_HYKCARD_BAK primary key (CZKHM)
)
/

/*==============================================================*/
/* Index: HYKCARDBAK_CDNR                                       */
/*==============================================================*/
create unique index BFCRM10.HYKCARDBAK_CDNR on BFCRM10.HYKCARD_BAK (
   CDNR ASC
)
/

/*==============================================================*/
/* Table: HYKDEF                                                */
/*==============================================================*/
create sequence BFCRM10.S_HYKDEF
/
create table BFCRM10.HYKDEF  (
   HYKTYPE              NUMBER(5)                       not null,
   HYKKZID              NUMBER(5)                       not null,
   HYKNAME              VARCHAR2(20)                    not null,
   FXFS                 NUMBER(5)                       not null,
   HMCD                 NUMBER(5)                       not null
      constraint CKC_HMCD_HYKDEF check (HMCD >= 1),
   KHQDM                VARCHAR2(4),
   KHHZM                VARCHAR2(4),
   FFDX                 NUMBER(5)                      default 0 not null,
   YXQCD                VARCHAR2(4),
   BJ_PSW               NUMBER(1)                       not null
      constraint CKC_BJ_PSW_HYKDEF check (BJ_PSW in (0,1)),
   BJ_XSJL              NUMBER(1)                       not null
      constraint CKC_BJ_XSJL_HYKDEF check (BJ_XSJL in (0,1)),
   BJ_JF                NUMBER(1)                       not null
      constraint CKC_BJ_JF_HYKDEF check (BJ_JF in (0,1)),
   BJ_YHQZH             NUMBER(1)                       not null
      constraint CKC_BJ_YHQZH_HYKDEF check (BJ_YHQZH in (0,1)),
   BJ_CZZH              NUMBER(1)                       not null
      constraint CKC_BJ_CZZH_HYKDEF check (BJ_CZZH in (0,1)),
   BJ_CZK               NUMBER(1)                       not null
      constraint CKC_BJ_CZK_HYKDEF check (BJ_CZK in (0,1)),
   YHFS                 NUMBER(5)                       not null
      constraint CKC_YHFS_HYKDEF check (YHFS in (0,1,2)),
   FKQDXFJE             NUMBER(14,2),
   KXBJ                 NUMBER(5)                      default 1 not null
      constraint CKC_KXBJ_HYKDEF check (KXBJ in (1,0)),
   TKBJ                 NUMBER(5)                      default 1 not null
      constraint CKC_TKBJ_HYKDEF check (TKBJ in (1,0)),
   ZFBJ                 NUMBER(5)                      default 1 not null
      constraint CKC_ZFBJ_HYKDEF check (ZFBJ in (1,0)),
   HYKJCID              NUMBER(5),
   TM                   NUMBER(10),
   KFJE                 NUMBER(14,2),
   BJ_CDNRJM            NUMBER(5)                      default 1,
   CDJZ                 NUMBER(5)                      default 0 not null,
   BJ_CZYHQ             NUMBER(5)                      default 0 not null,
   BJ_XTZK              NUMBER(5)                      default 1 not null,
   FS_YXQ               NUMBER(5)                      default 0,
   BJ_QZYK              NUMBER(5)                      default 0 not null,
   BJ_CX                NUMBER(5)                      default 0,
   BJ_ZQHY              NUMBER(5)                      default 0 not null,
   BJ_TH                NUMBER(5)                      default 0,
   BJ_FPGL              NUMBER(5)                      default 0,
   BJ_XK                NUMBER(5)                      default 0,
   BJ_TS                NUMBER(5)                      default 0,
   BJ_FSK               NUMBER(5),
   FS_SYMD              NUMBER(5),
   JFCLFWFS             NUMBER(5)                      default 0
      constraint CKC_JFCLFWFS_HYKDEF check (JFCLFWFS is null or (JFCLFWFS between 0 and 2)),
   JFXX                 FLOAT(50)                      default 0,
   BJ_YZM               NUMBER(5)                      default 0,
   THBJ                 NUMBER(5)                      default 0,
   BJ_ZERO              NUMBER(5)                      default 0,
   BJ_CK                NUMBER(5)                      default 0,
   BJ_QK                NUMBER(5)                      default 0,
   BJ_JEZC              NUMBER(5)                      default 0,
   BJ_LMZK              NUMBER(5)                      default 0,
   SJJZQ                VARCHAR2(5),
   BJ_WX                NUMBER(1)                      default 0,
   constraint PK_HYKDEF primary key (HYKTYPE)
)
/

comment on column BFCRM10.HYKDEF.FXFS is
'0: 自发行卡
1: 外部卡'
/

comment on column BFCRM10.HYKDEF.YHFS is
'0: 无优惠
1: 按会员折扣率销售
2: 按会员价销售'
/

/*==============================================================*/
/* Table: HYKDEF_WBK                                            */
/*==============================================================*/
create table BFCRM10.HYKDEF_WBK  (
   HYKTYPE              NUMBER(5)                       not null,
   BJKSWZ               NUMBER(5)                       not null,
   BJNR                 VARCHAR2(8)                     not null,
   KHKSWZ               NUMBER(5)                       not null,
   KHCD                 NUMBER(5)                       not null,
   DJID                 NUMBER(10),
   constraint PK_HYKDEF_WBK primary key (HYKTYPE, BJKSWZ, BJNR)
)
/

/*==============================================================*/
/* Table: HYKDJDEF                                              */
/*==============================================================*/
create table BFCRM10.HYKDJDEF  (
   HYKKZID              NUMBER(5)                       not null,
   HYKJCID              NUMBER(5)                       not null,
   HYKJCNAME            VARCHAR2(20),
   constraint PK_HYKDJDEF primary key (HYKKZID, HYKJCID)
)
/

/*==============================================================*/
/* Table: HYKFQDGZSJ                                            */
/*==============================================================*/
create table BFCRM10.HYKFQDGZSJ  (
   JLBH                 NUMBER(10)                      not null,
   INX                  NUMBER(5)                       not null,
   SHBMDM               VARCHAR2(10)                    not null,
   SHSPID               NUMBER(10)                      not null,
   GZBH                 NUMBER(5),
   FQGZID               NUMBER(10)                      not null,
   constraint PK_HYKFQDGZSJ primary key (JLBH, INX, SHBMDM, SHSPID)
)
/

/*==============================================================*/
/* Table: HYKFQDYD                                              */
/*==============================================================*/
create table BFCRM10.HYKFQDYD  (
   JLBH                 NUMBER(10)                      not null,
   DJLX                 NUMBER(5)                      default 0 not null,
   YHQID                NUMBER(5)                       not null,
   CXID                 NUMBER(10)                      not null,
   SHDM                 VARCHAR2(4)                     not null,
   SHBMID               NUMBER(10)                      not null,
   SHBMDM               VARCHAR2(20)                    not null,
   YXCLBJ               NUMBER(5)                      default 0 not null,
   RQ1                  DATE,
   RQ2                  DATE,
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16)                    not null,
   DJSJ                 DATE                            not null,
   SHR                  NUMBER(10),
   SHRMC                VARCHAR2(16),
   SHSJ                 DATE,
   QDR                  NUMBER(10),
   QDRMC                VARCHAR2(16),
   QDSJ                 DATE,
   STATUS               NUMBER(5)                       not null,
   ZZRQ                 DATE,
   ZZR                  NUMBER(10),
   ZZRMC                VARCHAR2(16),
   XFLJFQFS             NUMBER(5)                      default 0 not null,
   SHZFFS               NUMBER(10),
   BJ_HTFQ              NUMBER(2),
   LX                   NUMBER(5),
   constraint PK_HYKFQDYD primary key (JLBH)
)
/

/*==============================================================*/
/* Table: HYKFQDYD_FD                                           */
/*==============================================================*/
create table BFCRM10.HYKFQDYD_FD  (
   JLBH                 NUMBER(10)                      not null,
   INX                  NUMBER(5)                       not null,
   SD                   RAW(48)                         not null,
   constraint PK_HYKFQDYD_FD primary key (JLBH, INX)
)
/

/*==============================================================*/
/* Table: HYKFQDYD_GZITEM                                       */
/*==============================================================*/
create table BFCRM10.HYKFQDYD_GZITEM  (
   JLBH                 NUMBER(10)                      not null,
   INX                  NUMBER(5)                       not null,
   GZBH                 NUMBER(5)                       not null,
   SJLX                 NUMBER(5)                       not null,
   SJNR                 NUMBER(10)                      not null,
   XH                   NUMBER(10),
   constraint PK_HYKFQDYD_GZITEM primary key (JLBH, INX, GZBH, SJLX, SJNR)
)
/

comment on column BFCRM10.HYKFQDYD_GZITEM.SJLX is
'1 部门
2 合同
3 分类
4 品牌'
/

/*==============================================================*/
/* Table: HYKFQDYD_GZSD                                         */
/*==============================================================*/
create table BFCRM10.HYKFQDYD_GZSD  (
   JLBH                 NUMBER(10)                      not null,
   INX                  NUMBER(5)                       not null,
   GZBH                 NUMBER(5)                       not null,
   CLFS_BM              NUMBER(5)                      default 0 not null,
   CLFS_SPFL            NUMBER(5)                      default 0 not null,
   CLFS_SPSB            NUMBER(5)                      default 0 not null,
   CLFS_HT              NUMBER(5)                      default 0 not null,
   BJ_CJ                NUMBER(1)                       not null,
   FQGZID               NUMBER(10)                      not null,
   CLFS_SP              NUMBER(5)                      default 0 not null,
   CLFS_ZFFS            NUMBER(5)                      default 0 not null,
   CLFS_BANK            NUMBER(5)                      default 0 not null,
   CLFS_KH              NUMBER(5)                      default 0 not null,
   CLFS_HYZ             NUMBER(5)                      default 0 not null,
   CLFS_HYKLX           NUMBER(5),
   CLFS_FXDW            NUMBER(5),
   constraint PK_HYKFQDYD_GZSD primary key (JLBH, INX, GZBH)
)
/

comment on column BFCRM10.HYKFQDYD_GZSD.CLFS_BM is
'1 等于
0 不处理
-1 不等于'
/

/*==============================================================*/
/* Table: HYKFQDYD_SP                                           */
/*==============================================================*/
create table BFCRM10.HYKFQDYD_SP  (
   JLBH                 NUMBER(10)                      not null,
   INX                  NUMBER(5)                       not null,
   SHSPID               NUMBER(10)                      not null,
   FQGZID               NUMBER(10)                      not null,
   XH                   NUMBER(10),
   BJ_CJ                NUMBER(5),
   constraint PK_HYKFQDYD_SP primary key (JLBH, INX, SHSPID)
)
/

/*==============================================================*/
/* Table: HYKFQFD_SJ                                            */
/*==============================================================*/
create table BFCRM10.HYKFQFD_SJ  (
   JLBH                 NUMBER(10)                      not null,
   INX                  NUMBER(5)                       not null,
   DAY_WEEK             NUMBER(5)                       not null,
   MIN_DAY1             NUMBER(5)                       not null,
   MIN_DAY2             NUMBER(5)                       not null,
   constraint PK_HYKFQFD_SJ primary key (JLBH, INX, DAY_WEEK, MIN_DAY1)
)
/

/*==============================================================*/
/* Table: HYKFXDWXGJL                                           */
/*==============================================================*/
create table BFCRM10.HYKFXDWXGJL  (
   JLBH                 NUMBER(10)                      not null,
   FXDWDM_OLD           VARCHAR2(10),
   FXDWMC_OLD           VARCHAR2(30),
   HMCD_OLD             NUMBER(10),
   KHQDM_OLD            VARCHAR2(10),
   KHHZM_OLD            VARCHAR2(10),
   FXDWID               NUMBER(10),
   FXDWDM               VARCHAR2(10),
   FXDWMC               VARCHAR2(30),
   HMCD                 NUMBER(10),
   KHQDM                VARCHAR2(10),
   KHHZM                VARCHAR2(10),
   XGR                  NUMBER(10)                      not null,
   XGRMC                VARCHAR(20)                     not null,
   XGSJ                 DATE                            not null,
   constraint PK_HYKFXDWXGJL primary key (JLBH)
)
/

/*==============================================================*/
/* Table: HYKJCZZJJGZ                                           */
/*==============================================================*/
create table BFCRM10.HYKJCZZJJGZ  (
   HYKTYPE_OLD          NUMBER(5)                       not null,
   GZLX                 NUMBER(5)                       not null,
   HYKTYPE_NEW          NUMBER(5),
   XFJE                 NUMBER(14,2),
   constraint PK_HYKJCZZJJGZ primary key (HYKTYPE_OLD, GZLX)
)
/

/*==============================================================*/
/* Table: HYKJFDGZSJ                                            */
/*==============================================================*/
create table BFCRM10.HYKJFDGZSJ  (
   JLBH                 NUMBER(10)                      not null,
   INX                  NUMBER(5)                       not null,
   SHBMDM               VARCHAR2(10)                    not null,
   SHSPID               NUMBER(10)                      not null,
   GZBH                 NUMBER(5),
   FZ                   FLOAT(50)                       not null,
   constraint PK_HYKJFDGZSJ primary key (JLBH, INX, SHBMDM, SHSPID)
)
/

/*==============================================================*/
/* Table: HYKJFDYD                                              */
/*==============================================================*/
create table BFCRM10.HYKJFDYD  (
   JLBH                 NUMBER(10)                      not null,
   HYKTYPE              NUMBER(5),
   SHDM                 VARCHAR2(4),
   SHBMID               NUMBER(10),
   SHBMDM               VARCHAR2(20)                    not null,
   YXCLBJ               NUMBER(5)                      default 0 not null,
   RQ1                  DATE,
   RQ2                  DATE,
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16)                    not null,
   DJSJ                 DATE                            not null,
   SHR                  NUMBER(10),
   SHRMC                VARCHAR2(16),
   SHSJ                 DATE,
   QDR                  NUMBER(10),
   QDRMC                VARCHAR2(16),
   QDSJ                 DATE,
   STATUS               NUMBER(5)                       not null,
   ZZRQ                 DATE,
   ZZR                  NUMBER(10),
   ZZRMC                VARCHAR2(16),
   QDZKL                float(50)                      default 0 not null,
   BJ_JFBS              NUMBER(5)                      default 1,
   constraint PK_HYKJFDYD primary key (JLBH)
)
/

/*==============================================================*/
/* Table: HYKJFDYD_FD                                           */
/*==============================================================*/
create table BFCRM10.HYKJFDYD_FD  (
   JLBH                 NUMBER(10)                      not null,
   INX                  NUMBER(5)                       not null,
   KSRQ                 DATE                            not null,
   JSRQ                 DATE                            not null,
   SD                   RAW(48)                         not null,
   constraint PK_HYKJFDYD_FD primary key (JLBH, INX)
)
/

/*==============================================================*/
/* Table: HYKJFDYD_GZITEM                                       */
/*==============================================================*/
create table BFCRM10.HYKJFDYD_GZITEM  (
   JLBH                 NUMBER(10)                      not null,
   INX                  NUMBER(5)                       not null,
   GZBH                 NUMBER(5)                       not null,
   SJLX                 NUMBER(5)                       not null,
   SJNR                 NUMBER(10)                      not null,
   XH                   NUMBER(10),
   constraint PK_HYKJFDYD_GZITEM primary key (JLBH, INX, GZBH, SJLX, SJNR)
)
/

comment on column BFCRM10.HYKJFDYD_GZITEM.SJLX is
'1 部门
2 合同
3 分类
4 品牌'
/

/*==============================================================*/
/* Table: HYKJFDYD_GZSD                                         */
/*==============================================================*/
create table BFCRM10.HYKJFDYD_GZSD  (
   JLBH                 NUMBER(10)                      not null,
   INX                  NUMBER(5)                       not null,
   GZBH                 NUMBER(5)                       not null,
   CLFS_BM              NUMBER(5)                      default 0 not null,
   CLFS_SPFL            NUMBER(5)                      default 0 not null,
   CLFS_SPSB            NUMBER(5)                      default 0 not null,
   CLFS_HT              NUMBER(5)                      default 0 not null,
   BJ_CJ                NUMBER(1)                       not null,
   CLFS_SP              number(5)                      default 0 not null,
   ZKL_BEGIN            FLOAT(50)                      default 0 not null,
   ZKL_END              FLOAT(50)                      default 100 not null,
   FZ                   FLOAT(50),
   BS                   NUMBER(5),
   FTBL                 NUMBER(8,4),
   JFFBGZ               NUMBER(10),
   CLFS_HYZ             NUMBER(5),
   CLFS_HYKLX           NUMBER(5),
   CLFS_FXDW            NUMBER(5),
   CLDX                 VARCHAR2(50),
   constraint PK_HYKJFDYD_GZSD primary key (JLBH, INX, GZBH)
)
/

/*==============================================================*/
/* Table: HYKJFDYD_KLX                                          */
/*==============================================================*/
create table BFCRM10.HYKJFDYD_KLX  (
   JLBH                 NUMBER(10)                      not null,
   HYKTYPE              NUMBER(5)                       not null,
   constraint PK_HYKJFDYD_KLX primary key (JLBH, HYKTYPE)
)
/

/*==============================================================*/
/* Table: HYKJFDYD_SP                                           */
/*==============================================================*/
create table BFCRM10.HYKJFDYD_SP  (
   JLBH                 NUMBER(10)                      not null,
   INX                  NUMBER(5)                       not null,
   SHSPID               NUMBER(10)                      not null,
   FZ                   FLOAT(50)                       not null,
   XH                   NUMBER(10),
   BJ_CJ                NUMBER(5),
   BS                   NUMBER(5),
   FTBL                 NUMBER(8,4),
   JFFBGZ               NUMBER(10),
   constraint PK_HYKJFDYD_SP primary key (JLBH, INX, SHSPID)
)
/

/*==============================================================*/
/* Table: HYKJFFD_SJ                                            */
/*==============================================================*/
create table BFCRM10.HYKJFFD_SJ  (
   JLBH                 NUMBER(10)                      not null,
   INX                  NUMBER(5)                       not null,
   DAY_WEEK             NUMBER(5)                       not null,
   MIN_DAY1             NUMBER(5)                       not null,
   MIN_DAY2             NUMBER(5)                       not null,
   constraint PK_HYKJFFD_SJ primary key (JLBH, INX, DAY_WEEK, MIN_DAY1)
)
/

/*==============================================================*/
/* Table: HYKJKJL                                               */
/*==============================================================*/
create table BFCRM10.HYKJKJL  (
   JLBH                 NUMBER(10)                      not null,
   JKFS                 NUMBER(5)                      default 0 not null,
   HYKTYPE              NUMBER(5)                       not null,
   BGDDDM               VARCHAR2(20),
   ZY                   VARCHAR2(40),
   CZKHM_BEGIN          VARCHAR2(30)                    not null,
   CZKHM_END            VARCHAR2(30)                    not null,
   JKSL                 NUMBER(10)                      not null
      constraint CKC_JKSL_HYKJKJL check (JKSL between 0 and 500000),
   QCYE                 NUMBER(14,2)                   default 0 not null,
   YXTZJE               NUMBER(14,2)                   default 0 not null,
   JKWJCS               NUMBER(5)                      default 0 not null,
   BGR                  NUMBER(10)                      not null,
   BGRMC                VARCHAR2(16)                    not null,
   PDJE                 NUMBER(14,2)                   default 0 not null,
   BJ_CZK               NUMBER(1)                       not null
      constraint CKC_BJ_CZK_HYKJKJL check (BJ_CZK in (0,1)),
   DJSJ                 DATE                            not null,
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16)                    not null,
   ZXRQ                 DATE,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(16),
   STATUS               NUMBER(5)                       not null,
   DJLX                 NUMBER(5)                      default 0,
   YXQ                  DATE,
   XKSL                 NUMBER(10)                     default 0,
   FXDWID               NUMBER(10),
   QYID                 NUMBER(10),
   MDID                 NUMBER(10),
   constraint PK_HYKJKJL primary key (JLBH)
)
/

/*==============================================================*/
/* Table: HYKJKJLITEM                                           */
/*==============================================================*/
create table BFCRM10.HYKJKJLITEM  (
   JLBH                 NUMBER(10)                      not null,
   CZKHM                VARCHAR2(30)                    not null,
   CDNR                 VARCHAR2(60)                    not null,
   JE                   NUMBER(14,2)                   default 0,
   BJ_ZK                NUMBER(5)                      default 0,
   XKRQ                 DATE,
   YZM                  VARCHAR2(20),
   constraint PK_HYKJKJLITEM primary key (JLBH, CZKHM)
)
/

/*==============================================================*/
/* Index: HYKJKJLITEM_CDNR                                      */
/*==============================================================*/
create unique index BFCRM10.HYKJKJLITEM_CDNR on BFCRM10.HYKJKJLITEM (
   CDNR ASC
)
/

/*==============================================================*/
/* Index: HYKJKJLITEM_CZKHM                                     */
/*==============================================================*/
create unique index BFCRM10.HYKJKJLITEM_CZKHM on BFCRM10.HYKJKJLITEM (
   CZKHM ASC
)
/

/*==============================================================*/
/* Table: HYKJKJL_KBKXH                                         */
/*==============================================================*/
create table BFCRM10.HYKJKJL_KBKXH  (
   JLBH                 NUMBER(10)                      not null,
   XH                   NUMBER(10)                      not null,
   SL                   NUMBER(10)                      not null,
   constraint PK_HYKJKJL_KBKXH primary key (JLBH, XH)
)
/

/*==============================================================*/
/* Table: HYKJKSQD                                              */
/*==============================================================*/
create table BFCRM10.HYKJKSQD  (
   JLBH                 NUMBER(10)                      not null,
   HYKTYPE              NUMBER(5)                       not null,
   QCYE                 NUMBER(14,2)                    not null,
   SL                   NUMBER(10)                      not null,
   BGDDDM               VARCHAR2(20)                    not null,
   SQR                  NUMBER(10)                      not null,
   BZ                   VARCHAR2(30),
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16)                    not null,
   DJSJ                 DATE                            not null,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(16),
   ZXRQ                 DATE,
   constraint PK_HYKJKSQD primary key (JLBH)
)
/

/*==============================================================*/
/* Table: HYKKBKHDMX                                            */
/*==============================================================*/
create table BFCRM10.HYKKBKHDMX  (
   XH                   NUMBER(10)                     default 0 not null,
   HYKTYPE              NUMBER(5)                       not null,
   QCYE                 NUMBER(14,2)                   default 0 not null,
   CZKHM_BEGIN          VARCHAR2(30)                    not null,
   CZKHM_END            VARCHAR2(30)                    not null,
   QDM                  VARCHAR2(10),
   HZM                  VARCHAR2(10),
   SL                   NUMBER(10)                     default 0,
   JSKH_JK              VARCHAR2(30),
   constraint PK_HYKKBKHDMX primary key (XH)
)
/

/*==============================================================*/
/* Index: HYKKBKHDMX_KS                                         */
/*==============================================================*/
create unique index HYKKBKHDMX_KS on BFCRM10.HYKKBKHDMX (
   CZKHM_BEGIN ASC
)
/

/*==============================================================*/
/* Index: HYKKBKHDMX_JS                                         */
/*==============================================================*/
create unique index HYKKBKHDMX_JS on BFCRM10.HYKKBKHDMX (
   CZKHM_END ASC
)
/

/*==============================================================*/
/* Index: HYKKBKHDMX_KLX                                        */
/*==============================================================*/
create index HYKKBKHDMX_KLX on BFCRM10.HYKKBKHDMX (
   HYKTYPE ASC,
   QCYE ASC
)
/

/*==============================================================*/
/* Table: HYKKBKJL                                              */
/*==============================================================*/
create table BFCRM10.HYKKBKJL  (
   JLBH                 NUMBER(10)                      not null,
   HYKTYPE              NUMBER(5)                       not null,
   SL                   NUMBER(10)                      not null,
   QCYE                 NUMBER(14,2)                   default 0 not null,
   KSKH                 VARCHAR2(30)                    not null,
   JSKH                 VARCHAR2(30)                    not null,
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16)                    not null,
   DJSJ                 DATE                            not null,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(16),
   ZXRQ                 DATE,
   QDM                  VARCHAR2(10),
   HZM                  VARCHAR2(10),
   constraint PK_HYKKBKJL primary key (JLBH)
)
/

/*==============================================================*/
/* Table: HYKKFPJL                                              */
/*==============================================================*/
create table BFCRM10.HYKKFPJL  (
   JLBH                 NUMBER(10)                      not null,
   HYID                 NUMBER(10)                      not null,
   SKTNO                VARCHAR2(6)                     not null,
   MDID                 NUMBER(10)                      not null,
   KPSJ                 DATE                            not null,
   JE                   NUMBER(14,2)                   default 0 not null,
   SKYDM                VARCHAR2(10),
   constraint PK_HYKKFPJL primary key (JLBH)
)
/

/*==============================================================*/
/* Table: HYKKZDEF                                              */
/*==============================================================*/
create sequence BFCRM10.S_HYKKZDEF
/
create table BFCRM10.HYKKZDEF  (
   HYKKZID              NUMBER(5)                       not null,
   HYKKZNAME            VARCHAR2(20)                    not null,
   FXFS                 NUMBER(5)                       not null,
   HMCD                 NUMBER(5)                       not null
      constraint CKC_HMCD_HYKKZDEF check (HMCD >= 1),
   KHQDM                VARCHAR2(4),
   KHHZM                VARCHAR2(4),
   FFDX                 NUMBER(5)                      default 0 not null,
   YXQCD                VARCHAR2(4),
   BJ_PSW               NUMBER(1)                       not null
      constraint CKC_BJ_PSW_HYKKZDEF check (BJ_PSW in (0,1)),
   BJ_XSJL              NUMBER(1)                       not null
      constraint CKC_BJ_XSJL_HYKKZDEF check (BJ_XSJL in (0,1)),
   BJ_JF                NUMBER(1)                       not null
      constraint CKC_BJ_JF_HYKKZDEF check (BJ_JF in (0,1)),
   BJ_YHQZH             NUMBER(1)                       not null
      constraint CKC_BJ_YHQZH_HYKKZDEF check (BJ_YHQZH in (0,1)),
   BJ_CZZH              NUMBER(1)                       not null
      constraint CKC_BJ_CZZH_HYKKZDEF check (BJ_CZZH in (0,1)),
   BJ_CZK               NUMBER(1)                       not null
      constraint CKC_BJ_CZK_HYKKZDEF check (BJ_CZK in (0,1)),
   YHFS                 NUMBER(5)                       not null
      constraint CKC_YHFS_HYKKZDEF check (YHFS in (0,1,2)),
   FKQDXFJE             NUMBER(14,2),
   KXBJ                 NUMBER(5)                      default 1 not null
      constraint CKC_KXBJ_HYKKZDEF check (KXBJ in (1,0)),
   TKBJ                 NUMBER(5)                      default 1 not null
      constraint CKC_TKBJ_HYKKZDEF check (TKBJ in (1,0)),
   ZFBJ                 NUMBER(5)                      default 1 not null
      constraint CKC_ZFBJ_HYKKZDEF check (ZFBJ in (1,0)),
   TM                   NUMBER(10),
   BJ_CDNRJM            NUMBER(5)                      default 1,
   CDJZ                 NUMBER(5)                      default 0,
   BJ_CZYHQ             NUMBER(5)                      default 0 not null,
   BJ_XTZK              NUMBER(5)                      default 1 not null,
   FS_YXQ               NUMBER(5)                      default 0 not null,
   BJ_QZYK              NUMBER(5)                      default 0 not null,
   BJ_ZQHY              NUMBER(5)                      default 0 not null,
   BJ_XFSJ              NUMBER(5),
   constraint PK_HYKKZDEF primary key (HYKKZID)
)
/

comment on column BFCRM10.HYKKZDEF.FXFS is
'0: 自发行卡
1: 外部卡'
/

comment on column BFCRM10.HYKKZDEF.YHFS is
'0: 无优惠
1: 按会员折扣率销售
2: 按会员价销售'
/

/*==============================================================*/
/* Table: HYKSBTJ                                               */
/*==============================================================*/
create table BFCRM10.HYKSBTJ  (
   HYKTYPE              NUMBER(5)                       not null,
   SHDM                 VARCHAR2(4)                     not null,
   DRXFJE               NUMBER(14,2)                    not null,
   LJXFJE               NUMBER(14,2)                    not null,
   LJSJ                 NUMBER(5)                       not null,
   BJ_SBMD              NUMBER(5)                       not null,
   constraint PK_HYKSBTJ primary key (HYKTYPE, SHDM, BJ_SBMD)
)
/

/*==============================================================*/
/* Table: HYKSBXPJL                                             */
/*==============================================================*/
create table BFCRM10.HYKSBXPJL  (
   HYID                 NUMBER(10)                      not null,
   DJR                  NUMBER(10),
   DJRMC                VARCHAR2(16),
   DJSJ                 DATE,
   ZXSJ                 DATE,
   BJ_SBMD              NUMBER(5),
   BJ_DRLJ              NUMBER(5),
   constraint PK_HYKSBXPJL primary key (HYID)
)
/

/*==============================================================*/
/* Table: HYKSBXPMX                                             */
/*==============================================================*/
create sequence BFCRM10.S_HYKSBXPMX
/
create table BFCRM10.HYKSBXPMX  (
   HYID                 NUMBER(10)                      not null,
   MDID                 NUMBER(10)                      not null,
   SKTNO                VARCHAR2(6)                     not null,
   JLBH                 NUMBER(10)                      not null,
   XFRQ                 DATE                            not null,
   XFJE                 NUMBER(14,2)                    not null,
   BJ_YZ                NUMBER(5)                      default 0,
   TM                   NUMBER(10),
   constraint PK_HYKSBXPMX primary key (HYID, MDID, SKTNO, JLBH)
)
/

/*==============================================================*/
/* Index: HYKSBXPMX_MDID_SKT_JLBH                               */
/*==============================================================*/
create unique index BFCRM10.HYKSBXPMX_MDID_SKT_JLBH on BFCRM10.HYKSBXPMX (
   JLBH ASC,
   MDID ASC,
   SKTNO ASC
)
/

/*==============================================================*/
/* Table: HYKYQDGZSJ                                            */
/*==============================================================*/
create table BFCRM10.HYKYQDGZSJ  (
   JLBH                 NUMBER(10)                      not null,
   INX                  NUMBER(5)                       not null,
   SHBMDM               VARCHAR2(10)                    not null,
   SHSPID               NUMBER(10)                      not null,
   GZBH                 NUMBER(5),
   YQGZID               NUMBER(10)                      not null,
   constraint PK_HYKYQDGZSJ primary key (JLBH, INX, SHBMDM, SHSPID)
)
/

/*==============================================================*/
/* Table: HYKYQDYD                                              */
/*==============================================================*/
create table BFCRM10.HYKYQDYD  (
   JLBH                 NUMBER(10)                      not null,
   YHQID                NUMBER(5)                       not null,
   CXID                 NUMBER(10)                      not null,
   SHDM                 VARCHAR2(4)                     not null,
   SHBMID               NUMBER(10)                      not null,
   SHBMDM               VARCHAR2(20)                    not null,
   YXCLBJ               NUMBER(5)                      default 0 not null,
   RQ1                  DATE,
   RQ2                  DATE,
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16)                    not null,
   DJSJ                 DATE                            not null,
   SHR                  NUMBER(10),
   SHRMC                VARCHAR2(16),
   SHSJ                 DATE,
   QDR                  NUMBER(10),
   QDRMC                VARCHAR2(16),
   QDSJ                 DATE,
   STATUS               NUMBER(5)                       not null,
   ZZRQ                 DATE,
   ZZR                  NUMBER(10),
   ZZRMC                VARCHAR2(16),
   BJ_FQ                NUMBER(5)                      default 0 not null,
   DJLX                 NUMBER(5)                      default 0,
   LX                   NUMBER(5),
   constraint PK_HYKYQDYD primary key (JLBH)
)
/

/*==============================================================*/
/* Table: HYKYQDYD_FD                                           */
/*==============================================================*/
create table BFCRM10.HYKYQDYD_FD  (
   JLBH                 NUMBER(10)                      not null,
   INX                  NUMBER(5)                       not null,
   SD                   RAW(48)                         not null,
   constraint PK_HYKYQDYD_FD primary key (JLBH, INX)
)
/

/*==============================================================*/
/* Table: HYKYQDYD_GZITEM                                       */
/*==============================================================*/
create table BFCRM10.HYKYQDYD_GZITEM  (
   JLBH                 NUMBER(10)                      not null,
   INX                  NUMBER(5)                       not null,
   GZBH                 NUMBER(5)                       not null,
   SJLX                 NUMBER(5)                       not null,
   SJNR                 NUMBER(10)                      not null,
   XH                   NUMBER(10),
   constraint PK_HYKYQDYD_GZITEM primary key (JLBH, INX, GZBH, SJLX, SJNR)
)
/

comment on column BFCRM10.HYKYQDYD_GZITEM.SJLX is
'1 部门
2 合同
3 分类
4 品牌'
/

/*==============================================================*/
/* Table: HYKYQDYD_GZSD                                         */
/*==============================================================*/
create table BFCRM10.HYKYQDYD_GZSD  (
   JLBH                 NUMBER(10)                      not null,
   INX                  NUMBER(5)                       not null,
   GZBH                 NUMBER(5)                       not null,
   CLFS_BM              NUMBER(5)                      default 0 not null,
   CLFS_SPFL            NUMBER(5)                      default 0 not null,
   CLFS_SPSB            NUMBER(5)                      default 0 not null,
   CLFS_HT              NUMBER(5)                      default 0 not null,
   BJ_CJ                NUMBER(1)                       not null,
   YQGZID               NUMBER(10)                      not null,
   CLFS_SP              NUMBER(5)                      default 0 not null,
   CLFS_HYZ             NUMBER(5)                      default 0,
   CLFS_HYKLX           NUMBER(5),
   CLFS_FXDW            NUMBER(5),
   constraint PK_HYKYQDYD_GZSD primary key (JLBH, INX, GZBH)
)
/

comment on column BFCRM10.HYKYQDYD_GZSD.CLFS_BM is
'1 等于
0 不处理
-1 不等于'
/

/*==============================================================*/
/* Table: HYKYQDYD_SP                                           */
/*==============================================================*/
create table BFCRM10.HYKYQDYD_SP  (
   JLBH                 NUMBER(10)                      not null,
   INX                  NUMBER(5)                       not null,
   SHSPID               NUMBER(10)                      not null,
   YQGZID               NUMBER(10)                      not null,
   XH                   NUMBER(10),
   BJ_CJ                NUMBER(5),
   constraint PK_HYKYQDYD_SP primary key (JLBH, INX, SHSPID)
)
/

/*==============================================================*/
/* Table: HYKYQFD_SJ                                            */
/*==============================================================*/
create table BFCRM10.HYKYQFD_SJ  (
   JLBH                 NUMBER(10)                      not null,
   INX                  NUMBER(5)                       not null,
   DAY_WEEK             NUMBER(5)                       not null,
   MIN_DAY1             NUMBER(5)                       not null,
   MIN_DAY2             NUMBER(5)                       not null,
   constraint PK_HYKYQFD_SJ primary key (JLBH, INX, DAY_WEEK, MIN_DAY1)
)
/

/*==============================================================*/
/* Table: HYKZKDGZSJ                                            */
/*==============================================================*/
create table BFCRM10.HYKZKDGZSJ  (
   JLBH                 NUMBER(10)                      not null,
   INX                  NUMBER(5)                       not null,
   SHBMDM               VARCHAR2(10)                    not null,
   SHSPID               NUMBER(10)                      not null,
   GZBH                 NUMBER(5),
   ZKL                  FLOAT(50)                       not null,
   constraint PK_HYKZKDGZSJ primary key (JLBH, INX, SHBMDM, SHSPID)
)
/

/*==============================================================*/
/* Table: HYKZKDYD                                              */
/*==============================================================*/
create table BFCRM10.HYKZKDYD  (
   JLBH                 NUMBER(10)                      not null,
   HYKTYPE              NUMBER(5),
   SHDM                 VARCHAR2(4),
   SHBMID               NUMBER(10),
   SHBMDM               VARCHAR2(20)                    not null,
   YXCLBJ               NUMBER(5)                      default 0 not null,
   RQ1                  DATE,
   RQ2                  DATE,
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16)                    not null,
   DJSJ                 DATE                            not null,
   SHR                  NUMBER(10),
   SHRMC                VARCHAR2(16),
   SHSJ                 DATE,
   QDR                  NUMBER(10),
   QDRMC                VARCHAR2(16),
   QDSJ                 DATE,
   STATUS               NUMBER(5)                       not null,
   ZZRQ                 DATE,
   ZZR                  NUMBER(10),
   ZZRMC                VARCHAR2(16),
   ZKFS                 NUMBER(5)                      default 0,
   QDZKL                FLOAT(50)                      default 0,
   BJ_TD                NUMBER(5)                      default 1,
   constraint PK_HYKZKDYD primary key (JLBH)
)
/

/*==============================================================*/
/* Table: HYKZKDYD_FD                                           */
/*==============================================================*/
create table BFCRM10.HYKZKDYD_FD  (
   JLBH                 NUMBER(10)                      not null,
   INX                  NUMBER(5)                       not null,
   JSRQ                 DATE                            not null,
   KSRQ                 DATE                            not null,
   SD                   RAW(48)                         not null,
   constraint PK_HYKZKDYD_FD primary key (JLBH, INX)
)
/

/*==============================================================*/
/* Table: HYKZKDYD_GZITEM                                       */
/*==============================================================*/
create table BFCRM10.HYKZKDYD_GZITEM  (
   JLBH                 NUMBER(10)                      not null,
   INX                  NUMBER(5)                       not null,
   GZBH                 NUMBER(5)                       not null,
   SJLX                 NUMBER(5)                       not null,
   SJNR                 NUMBER(10)                      not null,
   XH                   NUMBER(10),
   constraint PK_HYKZKDYD_GZITEM primary key (JLBH, INX, GZBH, SJLX, SJNR)
)
/

comment on column BFCRM10.HYKZKDYD_GZITEM.SJLX is
'1 部门
2 合同
3 分类
4 品牌'
/

/*==============================================================*/
/* Table: HYKZKDYD_GZSD                                         */
/*==============================================================*/
create table BFCRM10.HYKZKDYD_GZSD  (
   JLBH                 NUMBER(10)                      not null,
   INX                  NUMBER(5)                       not null,
   GZBH                 NUMBER(5)                       not null,
   CLFS_BM              NUMBER(5)                      default 0 not null,
   CLFS_SPFL            NUMBER(5)                      default 0 not null,
   CLFS_SPSB            NUMBER(5)                      default 0 not null,
   CLFS_HT              NUMBER(5)                      default 0 not null,
   BJ_CJ                NUMBER(1)                       not null,
   ZKL                  FLOAT(50),
   ZSZKL                FLOAT(50)                      default 1,
   CLFS_JD              NUMBER(5)                      default 0
      constraint CKC_CLFS_JD_HYKZKDYD_GZSD check (CLFS_JD is null or (CLFS_JD between 0 and 2)),
   ZKL_BEGIN            FLOAT(50),
   ZKL_END              FLOAT(50),
   CLFS_SP              NUMBER(5)                      default 0,
   CLFS_HYZ             NUMBER(5),
   CLFS_HYKLX           NUMBER(5),
   CLFS_FXDW            NUMBER(5),
   constraint PK_HYKZKDYD_GZSD primary key (JLBH, INX, GZBH)
)
/

/*==============================================================*/
/* Table: HYKZKDYD_KLX                                          */
/*==============================================================*/
create table BFCRM10.HYKZKDYD_KLX  (
   JLBH                 NUMBER(10)                      not null,
   HYKTYPE              NUMBER(5)                       not null,
   constraint PK_HYKZKDYD_KLX primary key (JLBH, HYKTYPE)
)
/

/*==============================================================*/
/* Table: HYKZKDYD_SP                                           */
/*==============================================================*/
create table BFCRM10.HYKZKDYD_SP  (
   JLBH                 NUMBER(10)                      not null,
   INX                  NUMBER(5)                       not null,
   SHSPID               NUMBER(10)                      not null,
   ZKL                  FLOAT(50)                      default 1,
   ZSZKL                FLOAT(50),
   XH                   NUMBER(10),
   BJ_CJ                NUMBER(5),
   CLFS_JD              NUMBER(5)                      default 0
      constraint CKC_CLFS_JD_HYKZKDYD_SP check (CLFS_JD is null or (CLFS_JD between 0 and 2)),
   constraint PK_HYKZKDYD_SP primary key (JLBH, INX, SHSPID)
)
/

/*==============================================================*/
/* Table: HYKZKFD_SJ                                            */
/*==============================================================*/
create table BFCRM10.HYKZKFD_SJ  (
   JLBH                 NUMBER(10)                      not null,
   INX                  NUMBER(5)                       not null,
   DAY_WEEK             NUMBER(5)                       not null,
   MIN_DAY1             NUMBER(5)                       not null,
   MIN_DAY2             NUMBER(5)                       not null,
   constraint PK_HYKZKFD_SJ primary key (JLBH, INX, DAY_WEEK, MIN_DAY1)
)
/

/*==============================================================*/
/* Table: HYK_BGDD                                              */
/*==============================================================*/
create table BFCRM10.HYK_BGDD  (
   BGDDDM               VARCHAR2(20)                    not null,
   BGDDMC               VARCHAR2(20)                    not null,
   ZK_BJ                NUMBER(5)                       not null
      constraint CKC_ZK_BJ_HYK_BGDD check (ZK_BJ in (0,1)),
   XS_BJ                NUMBER(5)                       not null
      constraint CKC_XS_BJ_HYK_BGDD check (XS_BJ in (0,1)),
   MJBJ                 NUMBER(5),
   MDID                 NUMBER(10),
   BJ_PSZX              NUMBER(5),
   TM                   NUMBER(10),
   TY_BJ                NUMBER(5),
   WEB_INX              NUMBER(10),
   BJ_CW                NUMBER(5),
   constraint PK_HYK_BGDD primary key (BGDDDM)
)
/

/*==============================================================*/
/* Table: HYK_BNLJJF_RBB                                        */
/*==============================================================*/
create table BFCRM10.HYK_BNLJJF_RBB  (
   RQ                   DATE                            not null,
   HYKTYPE              NUMBER(5)                       not null,
   SQBNLJJF             FLOAT(50),
   BNLJJFBD_GHKLX       FLOAT(50),
   BNLJJFBD_BDD         FLOAT(50),
   BNLJJFBD_TZD         FLOAT(50),
   BNLJJFBD_SJ          FLOAT(50),
   BNLJJFBD_JJ          FLOAT(50),
   BNLJJFBD_QLSJLSJ     FLOAT(50),
   BNLJJFBD_YXQYC       FLOAT(50),
   BNLJJFBD_XM          FLOAT(50),
   BNLJJFBD_FL_CZ       FLOAT(50),
   BNLJJFBD_LKHXK       FLOAT(50),
   BNLJJFBD_LKBXK       FLOAT(50),
   BNLJJFBD_QTXF        FLOAT(50),
   BNLJJFBD_YQMJF       FLOAT(50),
   BNLJJFBD_JCZZ        FLOAT(50),
   BNLJJFBD_QJF         FLOAT(50),
   BNLJJFBD_JFHG        FLOAT(50),
   BNLJJFBD_JFZC        FLOAT(50),
   QMBNLJJF             FLOAT(50),
   YEARMONTH            NUMBER(10)                      not null,
   constraint PK_HYK_BNLJJF_RBB primary key (RQ, HYKTYPE)
)
/

/*==============================================================*/
/* Table: HYK_BQJF_RBB                                          */
/*==============================================================*/
create table BFCRM10.HYK_BQJF_RBB  (
   RQ                   DATE                            not null,
   HYKTYPE              NUMBER(5)                       not null,
   SQBQJF               FLOAT(50),
   BQJFBD_GHKLX         FLOAT(50),
   BQJFBD_BDD           FLOAT(50),
   BQJFBD_TZD           FLOAT(50),
   BQJFBD_SJ            FLOAT(50),
   BQJFBD_JJ            FLOAT(50),
   BQJFBD_QLSJLSJ       FLOAT(50),
   BQJFBD_YXQYC         FLOAT(50),
   BQJFBD_XM            FLOAT(50),
   BQJFBD_FL_CZ         FLOAT(50),
   BQJFBD_LKHXK         FLOAT(50),
   BQJFBD_LKBXK         FLOAT(50),
   BQJFBD_QTXF          FLOAT(50),
   BQJFBD_YQMJF         FLOAT(50),
   BQJFBD_JCZZ          FLOAT(50),
   BQJFBD_QJF           FLOAT(50),
   BQJFBD_JFHG          FLOAT(50),
   BQJFBD_JFZC          FLOAT(50),
   QMBQJF               FLOAT(50),
   YEARMONTH            NUMBER(10)                      not null,
   constraint PK_HYK_BQJF_RBB primary key (RQ, HYKTYPE)
)
/

/*==============================================================*/
/* Table: HYK_CHILD_JL                                          */
/*==============================================================*/
create sequence BFCRM10.S_HYK_CHILD_JL
/
create table BFCRM10.HYK_CHILD_JL  (
   HYID                 NUMBER(10)                      not null,
   HYK_NO               VARCHAR2(60)                    not null,
   HY_NAME              VARCHAR2(20),
   CDNR                 VARCHAR2(60)                    not null,
   JKRQ                 DATE                            not null,
   ZHXFRQ               DATE,
   BJ_PSW               NUMBER(5)                      default 0 not null,
   PASSWORD             VARCHAR2(8),
   DJSJ                 DATE                            not null,
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16),
   STATUS               NUMBER(5)                       not null,
   TM                   NUMBER(10),
   FFSX                 NUMBER(5)                      default 0,
   OLD_HYKNO            VARCHAR2(16),
   YBGDD                VARCHAR2(10),
   KHID                 NUMBER(10),
   MDID                 NUMBER(10),
   FXDW                 NUMBER(5),
   CDNR_OLD             VARCHAR2(60),
   YZM                  VARCHAR2(10),
   constraint PK_HYK_CHILD_JL primary key (HYID, HYK_NO)
)
/

/*==============================================================*/
/* Table: HYK_CXFHGZ                                            */
/*==============================================================*/
create table BFCRM10.HYK_CXFHGZ  (
   JLBH                 NUMBER(10)                      not null,
   SHDM                 VARCHAR2(4),
   HYKTYPE              NUMBER(10)                      not null,
   SYJSRQ               DATE,
   YHQYXTS              NUMBER(10),
   KSRQ                 DATE,
   JSRQ                 DATE,
   FHGZFS               NUMBER(5),
   STATUS               NUMBER(10),
   ZY                   VARCHAR2(50),
   DJR                  NUMBER(10),
   DJRMC                VARCHAR2(16),
   DJSJ                 DATE,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(16),
   ZXRQ                 DATE,
   ZZR                  NUMBER(10),
   ZZRMC                VARCHAR2(16),
   ZZRQ                 DATE,
   constraint PK_HYK_CXFHGZ primary key (JLBH)
)
/

/*==============================================================*/
/* Table: HYK_CXFHGZFSMX                                        */
/*==============================================================*/
create table BFCRM10.HYK_CXFHGZFSMX  (
   JLBH                 NUMBER(10)                      not null,
   GZH                  NUMBER(5)                       not null,
   CYFS                 NUMBER(5)                       not null,
   CXFHGZID             NUMBER(10),
   constraint PK_HYK_CXFHGZFSMX primary key (JLBH, GZH, CYFS)
)
/

/*==============================================================*/
/* Table: HYK_CXFHGZMX                                          */
/*==============================================================*/
create table BFCRM10.HYK_CXFHGZMX  (
   JLBH                 NUMBER(10)                      not null,
   GZH                  NUMBER(5)                       not null,
   XFKSRQ               DATE,
   XFJSRQ               DATE,
   HBFS                 NUMBER(5),
   FHJE                 NUMBER(14,2),
   NR                   VARCHAR2(70),
   constraint PK_HYK_CXFHGZMX primary key (JLBH, GZH)
)
/

/*==============================================================*/
/* Table: HYK_CXFHGZNRMX                                        */
/*==============================================================*/
create table BFCRM10.HYK_CXFHGZNRMX  (
   JLBH                 NUMBER(10)                      not null,
   GZH                  NUMBER(5)                       not null,
   XXLX                 NUMBER(5)                       not null,
   XH                   NUMBER(10)                      not null,
   XXNR                 NUMBER(10),
   constraint PK_HYK_CXFHGZNRMX primary key (JLBH, GZH, XXLX, XH)
)
/

/*==============================================================*/
/* Table: HYK_CZKSKJL                                           */
/*==============================================================*/
create table BFCRM10.HYK_CZKSKJL  (
   JLBH                 NUMBER(10)                      not null,
   FS                   NUMBER(5)                       not null,
   SKSL                 NUMBER(10)                      not null,
   YSZE                 NUMBER(14,2)                   default 0 not null,
   ZKL                  NUMBER(16,4)                   default 1 not null,
   ZKJE                 NUMBER(14,2)                   default 0 not null,
   SSJE                 NUMBER(14,2)                   default 0 not null,
   YXQ                  DATE                            not null,
   BGDDDM               VARCHAR2(20)                    not null,
   ZY                   VARCHAR2(40),
   TK_FLAG              NUMBER(5)                      default 0 not null
      constraint CKC_TK_FLAG_HYK_CZKS check (TK_FLAG in (0,1)),
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16)                    not null,
   DJSJ                 DATE                            not null,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(16),
   ZXRQ                 DATE,
   STATUS               NUMBER(5)                      default 0 not null
      constraint CKC_STATUS_HYK_CZKS check (STATUS in (0,1,2)),
   QDSJ                 DATE,
   DZKFJE               NUMBER(14,2),
   KFJE                 NUMBER(14,2),
   ZSJE                 NUMBER(14,2),
   KHID                 NUMBER(10),
   LXR                  VARCHAR2(16),
   SJZSJE               NUMBER(14,2),
   TKJLBH               NUMBER(10),
   YWY                  NUMBER(10),
   TJSJ                 DATE,
   XGR                  NUMBER(10),
   XGSJ                 DATE,
   XGRMC                VARCHAR2(16),
   ZDZF                 FLOAT(50),
   BJ_RJSH              NUMBER(5)                      default 0,
   ZSJF                 NUMBER(16,4),
   SJZSJF               NUMBER(16,4),
   MDID                 NUMBER(10),
   SXSJ                 DATE,
   constraint PK_HYK_CZKSKJL primary key (JLBH)
)
/

/*==============================================================*/
/* Index: INDEX_HYK_CZKSKJL_ZXRQ                                */
/*==============================================================*/
create index BFCRM10.INDEX_HYK_CZKSKJL_ZXRQ on BFCRM10.HYK_CZKSKJL (
   ZXRQ ASC
)
/

/*==============================================================*/
/* Table: HYK_CZKSKJLITEM                                       */
/*==============================================================*/
create table BFCRM10.HYK_CZKSKJLITEM  (
   JLBH                 NUMBER(10)                      not null,
   CZKHM                VARCHAR2(30)                    not null,
   QCYE                 NUMBER(14,2)                    not null,
   YXTZJE               NUMBER(14,2)                    not null,
   PDJE                 NUMBER(14,2)                   default 0 not null,
   HYID                 NUMBER(10),
   KFJE                 NUMBER(14,2),
   YXQ                  DATE,
   BJ_QD                NUMBER(5),
   HYKTYPE              NUMBER(5),
   constraint PK_HYK_CZKSKJLITEM primary key (JLBH, CZKHM)
)
/

/*==============================================================*/
/* Table: HYK_CZKSKJLITEM_DELETE                                */
/*==============================================================*/
create table BFCRM10.HYK_CZKSKJLITEM_DELETE  (
   JLBH                 NUMBER(10)                      not null,
   CZKHM                VARCHAR2(30)                    not null,
   QCYE                 NUMBER(14,2)                    not null,
   YXTZJE               NUMBER(14,2)                    not null,
   PDJE                 NUMBER(14,2)                   default 0 not null,
   HYID                 NUMBER(10),
   KFJE                 NUMBER(14,2),
   YXQ                  DATE,
   BJ_QD                NUMBER(5),
   constraint PK_HYK_CZKSKJLITEM_DELETE primary key (JLBH, CZKHM)
)
/

/*==============================================================*/
/* Table: HYK_CZKSKJLKDITEM                                     */
/*==============================================================*/
create table BFCRM10.HYK_CZKSKJLKDITEM  (
   JLBH                 NUMBER(10)                      not null,
   CZKHM_BEGIN          VARCHAR2(30)                    not null,
   CZKHM_END            VARCHAR2(30)                    not null,
   SKSL                 NUMBER(10),
   MZJE                 NUMBER(14,2),
   HYKTYPE              NUMBER(10),
   YXQ                  DATE,
   constraint PK_HYK_CZKSKJLKDITEM primary key (JLBH, CZKHM_BEGIN)
)
/

/*==============================================================*/
/* Table: HYK_CZKSKJLKDITEM_DELETE                              */
/*==============================================================*/
create table BFCRM10.HYK_CZKSKJLKDITEM_DELETE  (
   JLBH                 NUMBER(10)                      not null,
   CZKHM_BEGIN          VARCHAR2(30)                    not null,
   CZKHM_END            VARCHAR2(30)                    not null,
   SKSL                 NUMBER(10),
   MZJE                 NUMBER(14,2),
   constraint PK_HYK_CZKSKJLKDITEM_DELETE primary key (JLBH, CZKHM_BEGIN)
)
/

/*==============================================================*/
/* Table: HYK_CZKSKJLSKMX                                       */
/*==============================================================*/
create table BFCRM10.HYK_CZKSKJLSKMX  (
   JLBH                 NUMBER(10)                      not null,
   ZFFSID               NUMBER(10)                      not null,
   JE                   NUMBER(14,2)                    not null,
   FKRQ                 DATE,
   constraint PK_HYK_CZKSKJLSKMX primary key (JLBH, ZFFSID)
)
/

/*==============================================================*/
/* Table: HYK_CZKSKJLSKMX_DELETE                                */
/*==============================================================*/
create table BFCRM10.HYK_CZKSKJLSKMX_DELETE  (
   JLBH                 NUMBER(10)                      not null,
   ZFFSID               NUMBER(10)                      not null,
   JE                   NUMBER(14,2)                    not null,
   FKRQ                 DATE,
   constraint PK_HYK_CZKSKJLSKMX_DELETE primary key (JLBH, ZFFSID)
)
/

/*==============================================================*/
/* Table: HYK_CZKSKJL_DELETE                                    */
/*==============================================================*/
create table BFCRM10.HYK_CZKSKJL_DELETE  (
   JLBH                 NUMBER(10)                      not null,
   FS                   NUMBER(5)                       not null,
   SKSL                 NUMBER(10)                      not null,
   YSZE                 NUMBER(14,2)                   default 0 not null,
   ZKL                  NUMBER(16,4)                   default 1 not null,
   ZKJE                 NUMBER(14,2)                   default 0 not null,
   SSJE                 NUMBER(14,2)                   default 0 not null,
   YXQ                  DATE                            not null,
   BGDDDM               VARCHAR2(20)                    not null,
   HYKTYPE              NUMBER(10),
   ZY                   VARCHAR2(40),
   TK_FLAG              NUMBER(5)                      default 0 not null,
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16)                    not null,
   DJSJ                 DATE                            not null,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(16),
   ZXRQ                 DATE,
   STATUS               NUMBER(5)                      default 0 not null,
   QDSJ                 DATE,
   DZKFJE               NUMBER(14,2),
   KFJE                 NUMBER(14,2),
   ZSJE                 NUMBER(14,2),
   KHID                 NUMBER(10),
   LXR                  VARCHAR2(16),
   SJZSJE               NUMBER(14,2),
   TKJLBH               NUMBER(10),
   YWY                  NUMBER(10),
   FXMD                 NUMBER(10),
   BJ_SJF               NUMBER(5)                      default 0,
   ZDZF                 FLOAT,
   SJZF                 FLOAT,
   TJSJ                 DATE,
   XGR                  NUMBER(10),
   XGSJ                 DATE,
   XGRMC                VARCHAR2(16),
   constraint PK_HYK_CZKSKJL_DELETE primary key (JLBH)
)
/

/*==============================================================*/
/* Table: HYK_CZKSKSKZPMX                                       */
/*==============================================================*/
create table BFCRM10.HYK_CZKSKSKZPMX  (
   JLBH                 NUMBER(10)                      not null,
   ZFFSID               NUMBER(10)                      not null,
   ZPHM                 VARCHAR2(30)                    not null,
   JE                   NUMBER(14,2)                    not null,
   YHID                 NUMBER(5),
   DW                   VARCHAR2(60),
   constraint PK_HYK_CZKSKSKZPMX primary key (JLBH, ZFFSID, ZPHM)
)
/

/*==============================================================*/
/* Table: HYK_CZKSKSKZPMX_DELETE                                */
/*==============================================================*/
create table BFCRM10.HYK_CZKSKSKZPMX_DELETE  (
   JLBH                 NUMBER(10)                      not null,
   ZFFSID               NUMBER(10)                      not null,
   ZPHM                 VARCHAR2(30)                    not null,
   JE                   NUMBER(14,2)                    not null,
   YHID                 NUMBER(5),
   DW                   VARCHAR2(60),
   constraint PK_HYK_CZKSKSKZPMX_DELETE primary key (JLBH, ZFFSID, ZPHM)
)
/

/*==============================================================*/
/* Table: HYK_CZK_CKJL                                          */
/*==============================================================*/
create table BFCRM10.HYK_CZK_CKJL  (
   CZJPJ_JLBH           NUMBER(10)                      not null,
   HYID                 NUMBER(10)                      not null,
   YJE                  NUMBER(14,2)                    not null,
   CKJE                 NUMBER(14,2)                    not null,
   ZY                   VARCHAR2(100),
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16)                    not null,
   DJSJ                 DATE                            not null,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(16),
   ZXRQ                 DATE,
   CZDD                 VARCHAR2(10),
   MDID                 NUMBER(10),
   SKTNO                VARCHAR2(6),
   SKYDM                VARCHAR2(8),
   BJ_SQ                NUMBER(5)                      default 0,
   HYKNO                VARCHAR2(20),
   BJ_CHILD             NUMBER(5),
   ZFFSID               NUMBER(10),
   SKYMC                VARCHAR2(20),
   SQRYID               NUMBER(10),
   constraint PK_HYK_CZK_CKJL primary key (CZJPJ_JLBH)
)
/

/*==============================================================*/
/* Index: INDEX_CZK_CKJL_ZXRQ                                   */
/*==============================================================*/
create index BFCRM10.INDEX_CZK_CKJL_ZXRQ on BFCRM10.HYK_CZK_CKJL (
   ZXRQ ASC
)
/

/*==============================================================*/
/* Table: HYK_CZK_CKJL_ZQITEM                                   */
/*==============================================================*/
create table BFCRM10.HYK_CZK_CKJL_ZQITEM  (
   JLBH                 NUMBER(10)                      not null,
   HYID                 NUMBER(10)                      not null,
   YHQID                NUMBER(5)                       not null,
   MDFWDM               VARCHAR2(10)                    not null,
   JSRQ                 DATE                            not null,
   JE                   NUMBER(14,2)                   default 0 not null,
   constraint PK_HYK_CZK_CKJL_ZQITEM primary key (JLBH, HYID, YHQID, MDFWDM, JSRQ)
)
/

/*==============================================================*/
/* Table: HYK_CZK_CKSJFGZ                                       */
/*==============================================================*/
create table BFCRM10.HYK_CZK_CKSJFGZ  (
   CKJE_BEGIN           NUMBER(14,2)                   default 0 not null,
   CKJE_END             NUMBER(14,2)                   default 0,
   ZSBL                 NUMBER(16,4),
   ZSJF                 NUMBER(16,4),
   constraint PK_HYK_CZK_CKSJFGZ primary key (CKJE_BEGIN)
)
/

/*==============================================================*/
/* Table: HYK_CZK_CKZQGZ                                        */
/*==============================================================*/
create table BFCRM10.HYK_CZK_CKZQGZ  (
   JLBH                 NUMBER(10)                      not null,
   MDID                 NUMBER(10)                      not null,
   HYKTYPE              NUMBER(5)                       not null,
   KSRQ                 DATE                            not null,
   JSRQ                 DATE                            not null,
   YHQID                NUMBER(5)                       not null,
   YXQTS                NUMBER(10),
   STATUS               NUMBER(5),
   DJSJ                 DATE                            not null,
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16)                    not null,
   ZXRQ                 DATE,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(16),
   ZZR                  NUMBER(10),
   ZZRMC                VARCHAR2(16),
   ZZRQ                 DATE,
   ZY                   VARCHAR2(40),
   constraint PK_HYK_CZK_CKZQGZ primary key (JLBH, HYKTYPE, MDID)
)
/

/*==============================================================*/
/* Table: HYK_CZK_CKZQGZITEM                                    */
/*==============================================================*/
create table BFCRM10.HYK_CZK_CKZQGZITEM  (
   JLBH                 NUMBER(10)                      not null,
   GZBH                 NUMBER(10)                      not null,
   CKJE_BEGIN           NUMBER(14,2)                   default 0 not null,
   CKJE_END             NUMBER(14,2)                   default 0 not null,
   ZSBL                 FLOAT(50),
   constraint PK_HYK_CZK_CKZQGZITEM primary key (JLBH, GZBH)
)
/

/*==============================================================*/
/* Table: HYK_CZK_CK_SKJL                                       */
/*==============================================================*/
create table BFCRM10.HYK_CZK_CK_SKJL  (
   JLBH                 NUMBER(10)                      not null,
   ZFFSID               NUMBER(10)                      not null,
   JE                   NUMBER(14,2)                    not null,
   FKRQ                 DATE,
   constraint PK_HYK_CZK_CK_SKJL primary key (JLBH, ZFFSID)
)
/

/*==============================================================*/
/* Table: HYK_CZK_FKZFITEM                                      */
/*==============================================================*/
create table BFCRM10.HYK_CZK_FKZFITEM  (
   JLBH                 NUMBER(10)                      not null,
   HYK_NO               VARCHAR(30)                     not null,
   JF                   FLOAT,
   HYID                 NUMBER(10),
   HYKTYPE              NUMBER(5),
   constraint PK_HYK_CZK_FKZFITEM primary key (JLBH, HYK_NO)
)
/

/*==============================================================*/
/* Table: HYK_CZK_FKZFITEM_DELETE                               */
/*==============================================================*/
create table BFCRM10.HYK_CZK_FKZFITEM_DELETE  (
   JLBH                 NUMBER(10)                      not null,
   HYK_NO               VARCHAR(30)                     not null,
   JF                   FLOAT,
   HYID                 NUMBER(10),
   HYKTYPE              NUMBER(5),
   constraint PK_HYK_CZK_FKZFITEM_DELETE primary key (JLBH, HYK_NO)
)
/

/*==============================================================*/
/* Table: HYK_CZK_JEZ_ZC                                        */
/*==============================================================*/
create table BFCRM10.HYK_CZK_JEZ_ZC  (
   CZJPJ_JLBH           NUMBER(10)                      not null,
   HYID_ZR              NUMBER(10),
   ZY                   VARCHAR2(40),
   ZRJE                 NUMBER(14,2)                    not null,
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16)                    not null,
   DJSJ                 DATE                            not null,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(16),
   ZXRQ                 DATE,
   CZDD                 VARCHAR2(10),
   MDID                 NUMBER(10),
   BJ_CHILD             NUMBER(5),
   HYKNO                VARCHAR2(20),
   constraint PK_CZK_ZC primary key (CZJPJ_JLBH)
)
/

/*==============================================================*/
/* Index: HYK_CZK_JEZ_ZC_HYID                                   */
/*==============================================================*/
create index BFCRM10.HYK_CZK_JEZ_ZC_HYID on BFCRM10.HYK_CZK_JEZ_ZC (
   HYID_ZR ASC
)
/

/*==============================================================*/
/* Index: INDEX_HYK_CZK_JEZ_ZC_ZXRQ                             */
/*==============================================================*/
create index BFCRM10.INDEX_HYK_CZK_JEZ_ZC_ZXRQ on BFCRM10.HYK_CZK_JEZ_ZC (
   ZXRQ ASC
)
/

/*==============================================================*/
/* Table: HYK_CZK_JZE_ZCITEM                                    */
/*==============================================================*/
create table BFCRM10.HYK_CZK_JZE_ZCITEM  (
   CZJPJ_JLBH           NUMBER(10)                      not null,
   HYID_ZC              NUMBER(10)                      not null,
   ZCJE                 NUMBER(14,2)                    not null,
   BJ_CHILD             NUMBER(5),
   HYKNO                VARCHAR2(30),
   constraint PK_HYK_CZK_JZE_ZCITEM primary key (CZJPJ_JLBH, HYID_ZC)
)
/

/*==============================================================*/
/* Index: HYK_CZK_JZE_ZCITEM_HYID                               */
/*==============================================================*/
create index BFCRM10.HYK_CZK_JZE_ZCITEM_HYID on BFCRM10.HYK_CZK_JZE_ZCITEM (
   HYID_ZC ASC
)
/

/*==============================================================*/
/* Table: HYK_CZK_KX                                            */
/*==============================================================*/
create table BFCRM10.HYK_CZK_KX  (
   CZJPJ_JLBH           NUMBER(10)                      not null,
   VIPID                NUMBER(10),
   HYKTYPE              NUMBER(5),
   ZY                   VARCHAR2(40),
   JF                   FLOAT(50)                       not null,
   JE                   NUMBER(14,2)                    not null,
   JLJE                 NUMBER(14,2)                    not null,
   LX                   NUMBER(1)                       not null
      constraint CKC_LX_HYK_CZK_ check (LX in (0,1)),
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16)                    not null,
   DJSJ                 DATE                            not null,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(16),
   ZXRQ                 DATE,
   CZDD                 VARCHAR2(10),
   HYKNO                VARCHAR2(20),
   BJ_CHILD             NUMBER(5),
   YHQJE                NUMBER(14,2),
   constraint PK_HYK_CZK_KX primary key (CZJPJ_JLBH)
)
/

/*==============================================================*/
/* Table: HYK_CZK_PLCKJL                                        */
/*==============================================================*/
create table BFCRM10.HYK_CZK_PLCKJL  (
   JLBH                 NUMBER(10)                      not null,
   HYKTYPE              NUMBER(10)                      not null,
   KHID                 NUMBER(10),
   CKJE                 NUMBER(14,2),
   YXZQJE               NUMBER(14,2),
   SJZQJE               NUMBER(14,2),
   CKSL                 NUMBER(10)                      not null,
   ZY                   VARCHAR2(40),
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16)                    not null,
   DJSJ                 DATE                            not null,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(16),
   ZXRQ                 DATE,
   STATUS               NUMBER(5)                      default 0 not null
      constraint CKC_STATUS_HYK_CZK_ check (STATUS in (0,1,2)),
   CZDD                 VARCHAR2(10),
   MDFWDM               VARCHAR2(10),
   BJ_NB                NUMBER(5),
   constraint PK_HYK_CZK_PLCKJL primary key (JLBH)
)
/

/*==============================================================*/
/* Index: INDEX_HYK_CZK_PLCKJL_ZXRQ                             */
/*==============================================================*/
create index BFCRM10.INDEX_HYK_CZK_PLCKJL_ZXRQ on BFCRM10.HYK_CZK_PLCKJL (
   ZXRQ ASC
)
/

/*==============================================================*/
/* Table: HYK_CZK_PLCKJLITEM                                    */
/*==============================================================*/
create table BFCRM10.HYK_CZK_PLCKJLITEM  (
   CZJPJ_JLBH           NUMBER(10)                      not null,
   HYID                 NUMBER(10)                      not null,
   YJE                  NUMBER(14,2)                    not null,
   CKJE                 NUMBER(14,2)                    not null,
   CZKHM                VARCHAR2(16),
   BZ                   VARCHAR2(40),
   constraint PK_HYK_CZK_PLCKJLITEM primary key (CZJPJ_JLBH, HYID)
)
/

/*==============================================================*/
/* Table: HYK_CZK_PLCKKDITEM                                    */
/*==============================================================*/
create table BFCRM10.HYK_CZK_PLCKKDITEM  (
   JLBH                 NUMBER(10)                      not null,
   CZKHM_BEGIN          VARCHAR2(16)                    not null,
   CZKHM_END            VARCHAR2(16)                    not null,
   CKSL                 NUMBER(10),
   CKJE                 NUMBER(14,2),
   constraint PK_HYK_CZK_PLCKKDITEM primary key (JLBH, CZKHM_BEGIN)
)
/

/*==============================================================*/
/* Table: HYK_CZK_PLCKSKJL                                      */
/*==============================================================*/
create table BFCRM10.HYK_CZK_PLCKSKJL  (
   JLBH                 NUMBER(10)                      not null,
   ZFFSID               NUMBER(10)                      not null,
   JE                   NUMBER(14,2)                    not null,
   FKRQ                 DATE,
   constraint PK_HYK_CZK_PLCKSKJL primary key (JLBH, ZFFSID)
)
/

/*==============================================================*/
/* Table: HYK_CZK_PLCKZKITEM                                    */
/*==============================================================*/
create table BFCRM10.HYK_CZK_PLCKZKITEM  (
   JLBH                 NUMBER(10)                      not null,
   HYK_NO               VARCHAR2(16)                    not null,
   YHQLX                NUMBER(10),
   ZKJE                 NUMBER(14,2),
   YXQTS                NUMBER(10),
   constraint PK_HYK_CZK_PLCKZKITEM primary key (JLBH, HYK_NO)
)
/

/*==============================================================*/
/* Table: HYK_CZK_PLCKZPMX                                      */
/*==============================================================*/
create table BFCRM10.HYK_CZK_PLCKZPMX  (
   JLBH                 NUMBER(10)                      not null,
   ZFFSID               NUMBER(10)                      not null,
   ZPHM                 VARCHAR2(30)                    not null,
   JE                   NUMBER(14,2)                    not null,
   YHID                 NUMBER(5),
   DW                   VARCHAR2(60),
   constraint PK_HYK_CZK_PLCKZPMX primary key (JLBH, ZFFSID, ZPHM)
)
/

/*==============================================================*/
/* Table: HYK_CZK_PLQKJL                                        */
/*==============================================================*/
create table BFCRM10.HYK_CZK_PLQKJL  (
   JLBH                 NUMBER(10)                      not null,
   HYKTYPE              NUMBER(10)                      not null,
   KHID                 NUMBER(10),
   QKJE                 NUMBER(14,2),
   QKSL                 NUMBER(10)                      not null,
   ZY                   VARCHAR2(40),
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16)                    not null,
   DJSJ                 DATE                            not null,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(16),
   ZXRQ                 DATE,
   STATUS               NUMBER(5)                      default 0 not null,
   CZDD                 VARCHAR2(10),
   constraint PK_HYK_CZK_PLQKJL primary key (JLBH)
)
/

/*==============================================================*/
/* Index: INX_HYK_CZK_PLQKJL_ZXRQ                               */
/*==============================================================*/
create index BFCRM10.INX_HYK_CZK_PLQKJL_ZXRQ on BFCRM10.HYK_CZK_PLQKJL (
   ZXRQ ASC
)
/

/*==============================================================*/
/* Table: HYK_CZK_PLQKJLITEM                                    */
/*==============================================================*/
create table BFCRM10.HYK_CZK_PLQKJLITEM  (
   CZJPJ_JLBH           NUMBER(10)                      not null,
   HYID                 NUMBER(10)                      not null,
   YJE                  NUMBER(14,2)                    not null,
   QKJE                 NUMBER(14,2)                    not null,
   CZKHM                VARCHAR2(16),
   constraint PK_HYK_CZK_PLQKJLITEM primary key (CZJPJ_JLBH, HYID)
)
/

/*==============================================================*/
/* Table: HYK_CZK_PLQKKDITEM                                    */
/*==============================================================*/
create table BFCRM10.HYK_CZK_PLQKKDITEM  (
   JLBH                 NUMBER(10)                      not null,
   CZKHM_BEGIN          VARCHAR2(16)                    not null,
   CZKHM_END            VARCHAR2(16)                    not null,
   QKSL                 NUMBER(10),
   QKJE                 NUMBER(14,2),
   constraint PK_HYK_CZK_PLQKKDITEM primary key (JLBH, CZKHM_BEGIN)
)
/

/*==============================================================*/
/* Table: HYK_CZK_PLQKSKJL                                      */
/*==============================================================*/
create table BFCRM10.HYK_CZK_PLQKSKJL  (
   JLBH                 NUMBER(10)                      not null,
   ZFFSID               NUMBER(10)                      not null,
   JE                   NUMBER(14,2)                    not null,
   FKRQ                 DATE,
   constraint PK_HYK_CZK_PLQKSKJL primary key (JLBH, ZFFSID)
)
/

/*==============================================================*/
/* Table: HYK_CZK_PLQKZPMX                                      */
/*==============================================================*/
create table BFCRM10.HYK_CZK_PLQKZPMX  (
   JLBH                 NUMBER(10)                      not null,
   ZFFSID               NUMBER(10)                      not null,
   ZPHM                 VARCHAR2(30)                    not null,
   JE                   NUMBER(14,2)                    not null,
   YHID                 NUMBER(5),
   DW                   VARCHAR2(60),
   constraint PK_HYK_CZK_PLQKZPMX primary key (JLBH, ZFFSID, ZPHM)
)
/

/*==============================================================*/
/* Table: HYK_CZK_QKJL                                          */
/*==============================================================*/
create table BFCRM10.HYK_CZK_QKJL  (
   CZJPJ_JLBH           NUMBER(10)                      not null,
   HYID                 NUMBER(10),
   ZY                   VARCHAR2(100),
   YJE                  NUMBER(14,2)                    not null,
   QKJE                 NUMBER(14,2)                    not null,
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16)                    not null,
   DJSJ                 DATE                            not null,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(16),
   ZXRQ                 DATE,
   CZDD                 VARCHAR2(10),
   MDID                 NUMBER(10),
   BJ_CHILD             NUMBER(5),
   HYKNO                VARCHAR2(20),
   constraint PK_HYK_CZK_QKJL primary key (CZJPJ_JLBH)
)
/

/*==============================================================*/
/* Index: HYK_CZK_QKJL_HYID                                     */
/*==============================================================*/
create index BFCRM10.HYK_CZK_QKJL_HYID on BFCRM10.HYK_CZK_QKJL (
   HYID ASC
)
/

/*==============================================================*/
/* Index: INDEX_HYK_CZK_QKJL_ZXRQ                               */
/*==============================================================*/
create index BFCRM10.INDEX_HYK_CZK_QKJL_ZXRQ on BFCRM10.HYK_CZK_QKJL (
   ZXRQ ASC
)
/

/*==============================================================*/
/* Table: HYK_CZK_QK_SKJL                                       */
/*==============================================================*/
create table BFCRM10.HYK_CZK_QK_SKJL  (
   JLBH                 NUMBER(10)                      not null,
   ZFFSID               NUMBER(10)                      not null,
   JE                   NUMBER(14,2)                    not null,
   FKRQ                 DATE,
   constraint PK_HYK_CZK_QK_SKJL primary key (JLBH, ZFFSID)
)
/

/*==============================================================*/
/* Table: HYK_CZK_RBB                                           */
/*==============================================================*/
create sequence BFCRM10.S_HYK_CZK_RBB
/
create table BFCRM10.HYK_CZK_RBB  (
   RQ                   DATE                            not null,
   HYKTYPE              NUMBER(5)                       not null,
   MDID                 NUMBER(10)                      not null,
   SQYE                 NUMBER(14,2)                   default 0 not null,
   JKJE                 NUMBER(14,2)                   default 0 not null,
   CKJE                 NUMBER(14,2)                   default 0 not null,
   QKJE                 NUMBER(14,2)                   default 0 not null,
   XFJE                 NUMBER(14,2)                   default 0 not null,
   TKJE                 NUMBER(14,2)                   default 0,
   TZJE                 NUMBER(14,2)                   default 0,
   QMYE                 NUMBER(14,2)                   default 0 not null,
   BKJE                 NUMBER(14,2)                   default 0,
   TM                   NUMBER(10),
   constraint PK_HYK_CZK_RBB primary key (RQ, HYKTYPE, MDID)
)
/

/*==============================================================*/
/* Table: HYK_CZK_WK                                            */
/*==============================================================*/
create table BFCRM10.HYK_CZK_WK  (
   CZJPJ_JLBH           NUMBER(10)                      not null,
   HYKTYPE              NUMBER(5),
   HYID                 NUMBER(10),
   HYKHM_OLD            VARCHAR2(30)                    not null,
   HYKHM_NEW            VARCHAR2(30)                    not null,
   ZY                   VARCHAR2(40),
   JF                   FLOAT(50)                       not null,
   JE                   NUMBER(14,2)                    not null,
   GBF                  NUMBER(14,2),
   DJSJ                 DATE                            not null,
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16)                    not null,
   BGDDDM               VARCHAR2(20),
   QCYE                 NUMBER(14,2),
   BJ_CZK               NUMBER(5)                      default 0,
   BJ_CHILD             NUMBER(5),
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(20),
   ZXRQ                 DATE,
   HKYY                 NUMBER(10),
   constraint PK_HYK_CZK_WK primary key (CZJPJ_JLBH)
)
/

/*==============================================================*/
/* Table: HYK_CZK_YHQ_CKJL                                      */
/*==============================================================*/
create table BFCRM10.HYK_CZK_YHQ_CKJL  (
   CZJPJ_JLBH           NUMBER(10)                      not null,
   HYID                 NUMBER(10)                      not null,
   YHQID                NUMBER(10)                      not null,
   JSRQ                 DATE                            not null,
   MDFWDM               VARCHAR2(10)                    not null,
   CXID                 NUMBER(10)                      not null,
   YJE                  NUMBER(14,2)                    not null,
   CKJE                 NUMBER(14,2)                    not null,
   ZY                   VARCHAR2(100),
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16)                    not null,
   DJSJ                 DATE                            not null,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(16),
   ZXRQ                 DATE,
   CZDD                 VARCHAR2(10)                    not null,
   HYKNO                VARCHAR2(36),
   BJ_CHILD             NUMBER(5),
   DJLX                 NUMBER(5),
   DJJLBH               NUMBER(10),
   SKJE                 NUMBER(14,2),
   MDID                 NUMBER(10),
   SKTNO                VARCHAR2(20),
   SKYDM                VARCHAR2(20),
   XPH                  NUMBER(10),
   JLBH_MAIN            NUMBER(10),
   constraint PK_HYK_CZK_YHQ_CKJL primary key (CZJPJ_JLBH)
)
/

/*==============================================================*/
/* Index: HYKCZKYHQCKJL_HYID                                    */
/*==============================================================*/
create index BFCRM10.HYKCZKYHQCKJL_HYID on BFCRM10.HYK_CZK_YHQ_CKJL (
   HYID ASC
)
/

/*==============================================================*/
/* Index: HYKCZKYHQCKJL_DJJLBH                                  */
/*==============================================================*/
create index HYKCZKYHQCKJL_DJJLBH on BFCRM10.HYK_CZK_YHQ_CKJL (
   DJJLBH ASC
)
/

/*==============================================================*/
/* Table: HYK_CZK_YHQ_QKJL                                      */
/*==============================================================*/
create table BFCRM10.HYK_CZK_YHQ_QKJL  (
   CZJPJ_JLBH           NUMBER(10)                      not null,
   HYID                 NUMBER(10)                      not null,
   YHQID                NUMBER(10)                      not null,
   JSRQ                 DATE                            not null,
   MDFWDM               VARCHAR2(10)                    not null,
   CXID                 NUMBER(10)                      not null,
   YJE                  NUMBER(14,2)                    not null,
   QKJE                 NUMBER(14,2)                    not null,
   ZY                   VARCHAR2(100),
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16)                    not null,
   DJSJ                 DATE                            not null,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(16),
   ZXRQ                 DATE,
   CZDD                 VARCHAR2(10),
   BJ_CHILD             NUMBER(5)                      default 0,
   HYKNO                VARCHAR2(20),
   DJJLBH               NUMBER(10),
   constraint PK_HYK_CZK_YHQ_QKJL primary key (CZJPJ_JLBH)
)
/

/*==============================================================*/
/* Index: INX_HYKCZKYHQQKJL_HYID                                */
/*==============================================================*/
create index BFCRM10.INX_HYKCZKYHQQKJL_HYID on BFCRM10.HYK_CZK_YHQ_QKJL (
   HYID ASC
)
/

/*==============================================================*/
/* Index: HYKCZKYHQQKJL_DJJLBH                                  */
/*==============================================================*/
create index HYKCZKYHQQKJL_DJJLBH on BFCRM10.HYK_CZK_YHQ_QKJL (
   DJJLBH ASC
)
/

/*==============================================================*/
/* Table: HYK_CZK_YHQ_ZC                                        */
/*==============================================================*/
create table BFCRM10.HYK_CZK_YHQ_ZC  (
   CZJPJ_JLBH           NUMBER(10)                      not null,
   HYID_ZR              NUMBER(10),
   ZY                   VARCHAR2(40),
   ZRJE                 NUMBER(14,2)                    not null,
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16)                    not null,
   DJSJ                 DATE                            not null,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(16),
   ZXRQ                 DATE,
   CZDD                 VARCHAR2(10),
   BJ_CHILD             NUMBER(5)                      default 0,
   HYKNO                VARCHAR2(20),
   constraint PK_HYK_CZK_YHQ_ZC primary key (CZJPJ_JLBH)
)
/

/*==============================================================*/
/* Index: HYK_CZK_YHQ_ZC_HYID                                   */
/*==============================================================*/
create index BFCRM10.HYK_CZK_YHQ_ZC_HYID on BFCRM10.HYK_CZK_YHQ_ZC (
   HYID_ZR ASC
)
/

/*==============================================================*/
/* Table: HYK_CZK_YHQ_ZCITEM                                    */
/*==============================================================*/
create table BFCRM10.HYK_CZK_YHQ_ZCITEM  (
   CZJPJ_JLBH           NUMBER(10)                      not null,
   HYID_ZC              NUMBER(10)                      not null,
   YHQID                NUMBER(5)                       not null,
   JSRQ                 DATE                            not null,
   MDFWDM               VARCHAR2(10)                    not null,
   CXID                 NUMBER(10)                      not null,
   ZCJE                 NUMBER(14,2)                    not null,
   BJ_CHILD             NUMBER(5),
   HYKNO                VARCHAR2(20),
   YYE                  NUMBER(14,2),
   constraint PK_HYK_CZK_YHQ_ZCITEM primary key (CZJPJ_JLBH, HYID_ZC, YHQID, JSRQ, MDFWDM)
)
/

/*==============================================================*/
/* Index: HYK_CZK_YHQ_ZCITEM_HYID                               */
/*==============================================================*/
create index BFCRM10.HYK_CZK_YHQ_ZCITEM_HYID on BFCRM10.HYK_CZK_YHQ_ZCITEM (
   HYID_ZC ASC
)
/

/*==============================================================*/
/* Table: HYK_CZK_ZJFITEM                                       */
/*==============================================================*/
create table BFCRM10.HYK_CZK_ZJFITEM  (
   JLBH                 NUMBER(10)                      not null,
   HYK_NO               VARCHAR2(16)                    not null,
   ZSJF                 NUMBER(16,4),
   constraint PK_HYK_CZK_ZJFITEM primary key (JLBH, HYK_NO)
)
/

/*==============================================================*/
/* Table: HYK_CZK_ZKGZ                                          */
/*==============================================================*/
create table BFCRM10.HYK_CZK_ZKGZ  (
   XSJE_BEGIN           NUMBER(14,2)                   default 0 not null,
   XSJE_END             NUMBER(14,2)                   default 0,
   ZSBL                 FLOAT(50),
   YXQTS                NUMBER(10),
   YHQTYPE              NUMBER(5),
   constraint PK_HYK_CZK_ZKGZ primary key (XSJE_BEGIN)
)
/

/*==============================================================*/
/* Table: HYK_CZK_ZKITEM                                        */
/*==============================================================*/
create table BFCRM10.HYK_CZK_ZKITEM  (
   JLBH                 NUMBER(10)                      not null,
   HYK_NO               VARCHAR2(16)                    not null,
   YHQLX                NUMBER(10),
   ZKJE                 NUMBER(14,2),
   YXQTS                NUMBER(10),
   constraint PK_HYK_CZK_ZKITEM primary key (JLBH, HYK_NO)
)
/

/*==============================================================*/
/* Table: HYK_CZK_ZKITEM_DELETE                                 */
/*==============================================================*/
create table BFCRM10.HYK_CZK_ZKITEM_DELETE  (
   JLBH                 NUMBER(10)                      not null,
   HYK_NO               VARCHAR2(16)                    not null,
   YHQLX                NUMBER(10),
   ZKJE                 NUMBER(14,2),
   YXQTS                NUMBER(10),
   constraint PK_HYK_CZK_ZKITEM_DELETE primary key (JLBH, HYK_NO)
)
/

/*==============================================================*/
/* Table: HYK_CZYQX                                             */
/*==============================================================*/
create table BFCRM10.HYK_CZYQX  (
   RYID                 NUMBER(10)                      not null,
   HYKTYPE              NUMBER(5)                       not null,
   constraint PK_HYK_CZYQX primary key (RYID, HYKTYPE)
)
/

/*==============================================================*/
/* Table: HYK_DJSQGZ                                            */
/*==============================================================*/
create table BFCRM10.HYK_DJSQGZ  (
   HYKTYPE              NUMBER(5)                       not null,
   HYKTYPE_NEW          NUMBER(5)                       not null,
   XFSJ                 NUMBER(5)                       not null,
   QDJF                 FLOAT(50)                      default 0 not null,
   BJ_SJ                NUMBER(5)                       not null,
   DRXFJE               NUMBER(14,2),
   SJYJJE               NUMBER(14,2),
   SJQDJE               NUMBER(14,2),
   BLRQ                 DATE,
   MDID                 NUMBER(5),
   BJ_XFJE              NUMBER(5),
   constraint PK_HYK_DJSQGZ primary key (HYKTYPE_NEW, HYKTYPE, XFSJ)
)
/

/*==============================================================*/
/* Table: HYK_DWXX                                              */
/*==============================================================*/
create table BFCRM10.HYK_DWXX  (
   HYID                 NUMBER(10)                      not null,
   DWQC                 VARCHAR2(50),
   QYID                 NUMBER(10),
   TXDZ                 VARCHAR2(50),
   YZBM                 VARCHAR2(6),
   JBRDZ                VARCHAR2(50),
   LXDH                 VARCHAR2(30),
   DWXZ                 VARCHAR2(30),
   HYGS                 VARCHAR2(30),
   NCZ                  VARCHAR2(10),
   NYYE                 VARCHAR2(10),
   NBGF                 VARCHAR2(10),
   constraint PK_HYK_DWXX primary key (HYID)
)
/

/*==============================================================*/
/* Table: HYK_FSSKFSGG                                          */
/*==============================================================*/
create table BFCRM10.HYK_FSSKFSGG  (
   JLBH                 NUMBER(10)                      not null,
   SKJLBH               NUMBER(10)                      not null,
   ZFFSID_OLD           NUMBER(10)                      not null,
   ZFFSID_NEW           NUMBER(10)                      not null,
   JE                   NUMBER(14,2),
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16)                    not null,
   DJSJ                 DATE                            not null,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(16),
   ZXRQ                 DATE,
   ZY                   VARCHAR2(30),
   constraint PK_HYK_FSSKFSGG primary key (JLBH)
)
/

/*==============================================================*/
/* Table: HYK_FSSKFSGG_ZP                                       */
/*==============================================================*/
create table BFCRM10.HYK_FSSKFSGG_ZP  (
   JLBH                 NUMBER(10)                      not null,
   ZFFSID               NUMBER(10)                      not null,
   ZPHM                 VARCHAR2(30)                    not null,
   JE                   NUMBER(14,2)                    not null,
   YHID                 NUMBER(5),
   DW                   VARCHAR2(60),
   constraint PK_HYK_FSSKFSGG_ZP primary key (JLBH, ZFFSID, ZPHM)
)
/

/*==============================================================*/
/* Table: HYK_FWYCJL                                            */
/*==============================================================*/
create table BFCRM10.HYK_FWYCJL  (
   JLBH                 NUMBER(10)                      not null,
   FWNRID               NUMBER(5)                       not null,
   JSRQ                 DATE,
   BZ                   VARCHAR2(30),
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16)                    not null,
   DJSJ                 DATE                            not null,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(16),
   ZXRQ                 DATE,
   constraint PK_HYK_FWYCJL primary key (JLBH)
)
/

/*==============================================================*/
/* Table: HYK_FWYCJLMX                                          */
/*==============================================================*/
create table BFCRM10.HYK_FWYCJLMX  (
   JLBH                 NUMBER(10)                      not null,
   HYID                 NUMBER(10)                      not null,
   FWSL                 FLOAT(50),
   constraint PK_HYK_FWYCJLMX primary key (JLBH, HYID)
)
/

/*==============================================================*/
/* Table: HYK_FWZH                                              */
/*==============================================================*/
create table BFCRM10.HYK_FWZH  (
   HYID                 NUMBER(10)                      not null,
   FWNRID               NUMBER(5)                       not null,
   JSRQ                 DATE                            not null,
   FWSL                 FLOAT(50)                      default 0,
   constraint PK_HYK_FWZH primary key (HYID, FWNRID, JSRQ)
)
/

/*==============================================================*/
/* Index: HYK_FWZH_HYID_FWID                                    */
/*==============================================================*/
create index BFCRM10.HYK_FWZH_HYID_FWID on BFCRM10.HYK_FWZH (
   HYID ASC,
   FWNRID ASC
)
/

/*==============================================================*/
/* Table: HYK_GHKLX                                             */
/*==============================================================*/
create table BFCRM10.HYK_GHKLX  (
   JLBH                 NUMBER(10)                      not null,
   HYKTYPE_NEW          NUMBER(5),
   HYID                 NUMBER(10),
   HYKHM_NEW            VARCHAR2(30)                    not null,
   HYKTYPE_OLD          NUMBER(5),
   HYKHM_OLD            VARCHAR2(30)                    not null,
   ZY                   VARCHAR2(40),
   JF                   FLOAT(50)                       not null,
   JE                   NUMBER(14,2)                    not null,
   DJSJ                 DATE                            not null,
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16)                    not null,
   ZXRQ                 DATE,
   ZXRMC                VARCHAR2(16),
   ZXR                  NUMBER(10),
   BGDDDM               VARCHAR2(20),
   QCYE                 NUMBER(14,2),
   BJ_CZK               NUMBER(5)                      default 0,
   BDJF                 FLOAT(50),
   FFSX                 NUMBER(5),
   constraint PK_HYK_GHKLX primary key (JLBH)
)
/

/*==============================================================*/
/* Index: GHKLX_HYID_DJSJ                                       */
/*==============================================================*/
create index BFCRM10.GHKLX_HYID_DJSJ on BFCRM10.HYK_GHKLX (
   DJSJ ASC,
   HYID ASC
)
/

/*==============================================================*/
/* Table: HYK_GHKLX_YHQZH                                       */
/*==============================================================*/
create table BFCRM10.HYK_GHKLX_YHQZH  (
   JLBH                 NUMBER(10)                      not null,
   HYID                 NUMBER(10)                      not null,
   YHQID                NUMBER(5)                       not null,
   MDFWDM               VARCHAR2(10)                    not null,
   TZJE                 NUMBER(14,2)                   default 0,
   constraint PK_HYK_GHKLX_YHQZH primary key (JLBH, YHQID, MDFWDM, HYID)
)
/

/*==============================================================*/
/* Index: HYK_GHKLX_YHQZH_HYID                                  */
/*==============================================================*/
create index BFCRM10.HYK_GHKLX_YHQZH_HYID on BFCRM10.HYK_GHKLX_YHQZH (
   HYID ASC
)
/

/*==============================================================*/
/* Table: HYK_GKDA                                              */
/*==============================================================*/
create sequence BFCRM10.S_HYK_GKDA
/
create table BFCRM10.HYK_GKDA  (
   GKID                 NUMBER(10)                      not null,
   SEX                  NUMBER(5),
   CSRQ                 DATE,
   SFZBH                VARCHAR2(20),
   JTZZ                 VARCHAR2(100),
   GZDW                 VARCHAR2(100),
   PASSWORD             VARCHAR2(8),
   BZ                   VARCHAR2(40),
   ZW                   VARCHAR2(20),
   E_MAIL               VARCHAR2(100),
   TXDZ                 VARCHAR2(100),
   YZBM                 VARCHAR2(40),
   PHONE                VARCHAR2(20),
   FAX                  VARCHAR2(20),
   SJHM                 VARCHAR2(20),
   BGDH                 VARCHAR2(20),
   ZJLXSJ               VARCHAR2(20),
   HYZK                 VARCHAR2(10),
   YJFLDZ               VARCHAR2(20),
   QYID                 NUMBER(10),
   ZYID                 NUMBER(10),
   JTSRID               NUMBER(10),
   XLID                 NUMBER(10),
   ZSCSJID              NUMBER(10),
   JTGJID               NUMBER(10),
   JTCYID               NUMBER(10),
   ZJLXID               NUMBER(10),
   YYAH                 VARCHAR2(30),
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16),
   DJSJ                 DATE                            not null,
   HYK_NO_OLD           VARCHAR2(16),
   CANSMS               NUMBER(5),
   GXR                  NUMBER(10),
   GXRMC                VARCHAR2(16),
   GXSJ                 DATE,
   CXXX                 VARCHAR2(30),
   CPH                  VARCHAR2(10),
   TM                   NUMBER(10),
   XXFS                 VARCHAR2(30),
   LX_SMZQ              NUMBER(5),
   BJ_CLD               NUMBER(5)                      default 0,
   HYSQID               NUMBER(10),
   BJ_TSHY              NUMBER(5),
   LMID                 NUMBER(5),
   QYMC                 VARCHAR2(30),
   QYXZ                 VARCHAR2(30),
   CZH                  VARCHAR2(30),
   GKNC                 VARCHAR2(30),
   MZID                 NUMBER(5),
   JHJNR                DATE,
   GK_NAME              VARCHAR2(30),
   CANVAS               VARCHAR2(200),
   KHJLRYID             NUMBER(10),
   PPXQ                 VARCHAR2(30),
   PPHY                 VARCHAR2(30),
   JHBJ                 NUMBER(5),
   QCPPID               NUMBER(10),
   BJ_YZSJHM            NUMBER(5),
   BJ_YZZJLX            NUMBER(5),
   QQ                   VARCHAR2(30),
   WX                   VARCHAR2(30),
   WB                   VARCHAR2(30),
   XQ                   VARCHAR2(30),
   TJRYID               NUMBER(10),
   ROAD                 VARCHAR2(30),
   HOUSENUM             VARCHAR2(30),
   SX                   VARCHAR2(10),
   XZ                   VARCHAR2(10),
   SW                   VARCHAR2(30),
   CM                   VARCHAR2(30),
   XQID                 NUMBER(10),
   SQID                 NUMBER(10),
   IMAGEURL             VARCHAR2(100),
   constraint PK_HYK_GKDA primary key (GKID)
)
/

/*==============================================================*/
/* Index: INX_GKDA_SFZHM                                        */
/*==============================================================*/
create index BFCRM10.INX_GKDA_SFZHM on BFCRM10.HYK_GKDA (
   SFZBH ASC,
   ZJLXID ASC
)
/

/*==============================================================*/
/* Index: INX_GKDA_TM                                           */
/*==============================================================*/
create index BFCRM10.INX_GKDA_TM on BFCRM10.HYK_GKDA (
   TM ASC
)
/

/*==============================================================*/
/* Table: HYK_GKDA_CHANGE                                       */
/*==============================================================*/
create table BFCRM10.HYK_GKDA_CHANGE  (
   GKID                 NUMBER(10)                      not null,
   JLBH                 NUMBER(10)                      not null,
   OLDVAL               VARCHAR2(100),
   NEWVAL               VARCHAR2(100),
   DJSJ                 DATE,
   DJR                  NUMBER(10),
   DJRMC                VARCHAR2(30),
   VALNAME              VARCHAR2(30),
   constraint PK_HYK_GKDA_CHANGE primary key (GKID, JLBH)
)
/

/*==============================================================*/
/* Table: HYK_GKDA_JTXX                                         */
/*==============================================================*/
create table BFCRM10.HYK_GKDA_JTXX  (
   GKID                 NUMBER(10)                      not null,
   JTXM                 NUMBER(10)                      not null,
   JTGX                 NUMBER(10),
   JTNL                 NUMBER(10),
   JTSR                 DATE,
   JTXB                 NUMBER(5),
   constraint PK_HYK_GKDA_JTXX primary key (GKID, JTXM)
)
/

/*==============================================================*/
/* Table: HYK_GRXX_CHILD_JL                                     */
/*==============================================================*/
create sequence BFCRM10.S_HYK_GRXX_CHILD_JL
/
create table BFCRM10.HYK_GRXX_CHILD_JL  (
   HYID                 NUMBER(10)                      not null,
   HYK_NO               VARCHAR2(30)                    not null,
   SEX                  NUMBER(5),
   CSRQ                 DATE,
   SFZBH                VARCHAR2(20),
   JTZZ                 VARCHAR2(40),
   GZDW                 VARCHAR2(100),
   BZ                   VARCHAR2(40),
   E_MAIL               VARCHAR2(40),
   TXDZ                 VARCHAR2(100),
   YZBM                 VARCHAR2(6),
   PHONE                VARCHAR2(20),
   FAX                  VARCHAR2(20),
   SJHM                 VARCHAR2(20),
   BGDH                 VARCHAR2(20),
   QYID                 NUMBER(10),
   ZYID                 NUMBER(10),
   JTSRID               NUMBER(10),
   XLID                 NUMBER(10),
   ZSCSJID              NUMBER(10),
   JTGJID               NUMBER(10),
   JTCYID               NUMBER(10),
   ZJLXID               NUMBER(10),
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16),
   DJSJ                 DATE                            not null,
   HYK_NO_OLD           VARCHAR2(16),
   CANSMS               NUMBER(5),
   GXR                  NUMBER(10),
   GXRMC                VARCHAR2(16),
   GXSJ                 DATE,
   CXXX                 VARCHAR2(30),
   CPH                  VARCHAR2(10),
   TM                   NUMBER(10),
   XXFS                 VARCHAR2(30),
   BJ_CLD               NUMBER(5)                      default 0,
   HYSQID               NUMBER(10),
   LMID                 NUMBER(5),
   constraint PK_HYK_GRXX_CHILD_JL primary key (HYID, HYK_NO)
)
/

/*==============================================================*/
/* Index: INX_SFZHM_CHILD                                       */
/*==============================================================*/
create index BFCRM10.INX_SFZHM_CHILD on BFCRM10.HYK_GRXX_CHILD_JL (
   SFZBH ASC,
   ZJLXID ASC
)
/

/*==============================================================*/
/* Table: HYK_GRXX_NR                                           */
/*==============================================================*/
create table BFCRM10.HYK_GRXX_NR  (
   HYID                 NUMBER(10)                      not null,
   XMID                 NUMBER(10)                      not null,
   constraint PK_HYK_GRXX_NR primary key (HYID, XMID)
)
/

/*==============================================================*/
/* Table: HYK_HTFQJL                                            */
/*==============================================================*/
create table BFCRM10.HYK_HTFQJL  (
   JLBH                 NUMBER(10)                      not null,
   MDID                 NUMBER(10),
   XFJE                 NUMBER(14,2),
   XFJE_FQ              NUMBER(14,2),
   FQJE                 NUMBER(14,2),
   STATUS               NUMBER(5),
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(20)                    not null,
   DJSJ                 DATE                            not null,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(20),
   ZXRQ                 DATE,
   constraint PK_HYK_HTFQJL primary key (JLBH)
)
/

/*==============================================================*/
/* Table: HYK_HTFQJL_DELETE                                     */
/*==============================================================*/
create table BFCRM10.HYK_HTFQJL_DELETE  (
   JLBH                 NUMBER(10)                      not null,
   MDID                 NUMBER(10),
   XFJE                 NUMBER(14,2),
   XFJE_FQ              NUMBER(14,2),
   FQJE                 NUMBER(14,2),
   STATUS               NUMBER(5),
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16)                    not null,
   DJSJ                 DATE                            not null,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(16),
   ZXRQ                 DATE,
   constraint PK_HYK_HTFQJL_DELETE primary key (JLBH)
)
/

/*==============================================================*/
/* Table: HYK_HTFQJL_FQ                                         */
/*==============================================================*/
create table BFCRM10.HYK_HTFQJL_FQ  (
   JLBH                 NUMBER(10)                      not null,
   YHQID                NUMBER(10)                      not null,
   FQJE                 NUMBER(14,2),
   constraint PK_HYK_HTFQJL_FQ primary key (JLBH, YHQID)
)
/

/*==============================================================*/
/* Table: HYK_HTFQJL_FQ_DELETE                                  */
/*==============================================================*/
create table BFCRM10.HYK_HTFQJL_FQ_DELETE  (
   JLBH                 NUMBER(10)                      not null,
   YHQID                NUMBER(10)                      not null,
   FQJE                 NUMBER(14,2),
   constraint PK_HYK_HTFQJL_FQ_DELETE primary key (JLBH, YHQID)
)
/

/*==============================================================*/
/* Table: HYK_HTFQJL_XPMX                                       */
/*==============================================================*/
create table BFCRM10.HYK_HTFQJL_XPMX  (
   JLBH                 NUMBER(10)                      not null,
   XFJLID               NUMBER(10)                      not null,
   SKTNO                VARCHAR2(6),
   XPH                  NUMBER(10),
   XH                   NUMBER(10),
   constraint PK_HYK_HTFQJL_XPMX primary key (JLBH, XFJLID)
)
/

/*==============================================================*/
/* Table: HYK_HTFQJL_XPMX_DELETE                                */
/*==============================================================*/
create table BFCRM10.HYK_HTFQJL_XPMX_DELETE  (
   JLBH                 NUMBER(10)                      not null,
   XFJLID               NUMBER(10)                      not null,
   SKTNO                VARCHAR2(6),
   XPH                  NUMBER(10),
   XH                   NUMBER(10),
   constraint PK_HYK_HTFQJL_XPMX_DELETE primary key (JLBH, XFJLID)
)
/

/*==============================================================*/
/* Table: HYK_HTYHQSQMX                                         */
/*==============================================================*/
create table BFCRM10.HYK_HTYHQSQMX  (
   JLBH                 NUMBER(10)                      not null,
   YHQID                NUMBER(10)                     default 0 not null,
   JE                   NUMBER(14,2)                   default 0 not null,
   FFSL                 NUMBER(10),
   constraint PK_HYK_HTYHQSQMX primary key (JLBH, YHQID, JE)
)
/

/*==============================================================*/
/* Table: HYK_HYBJ                                              */
/*==============================================================*/
create table BFCRM10.HYK_HYBJ  (
   HYID                 NUMBER(10)                      not null,
   BJID                 NUMBER(10),
   constraint PK_HYK_HYBJ primary key (HYID)
)
/

/*==============================================================*/
/* Table: HYK_HYBQ                                              */
/*==============================================================*/
create table BFCRM10.HYK_HYBQ  (
   HYID                 NUMBER(10)                      not null,
   LABELXMID            NUMBER(10),
   LABEL_VALUEID        NUMBER(10),
   YXQ                  DATE,
   BJ_TRANS             NUMBER(5),
   QZ                   NUMBER(14,2),
   LABELID              NUMBER(10),
   constraint PK_HYK_HYBQ primary key (HYID)
)
/

/*==============================================================*/
/* Table: HYK_HYFWCLJL                                          */
/*==============================================================*/
create table BFCRM10.HYK_HYFWCLJL  (
   FWBH                 NUMBER(6)                       not null,
   HYID                 NUMBER(10)                      not null,
   FWNRID               NUMBER(5)                       not null,
   JSRQ                 DATE,
   CLSJ                 DATE                            not null,
   CLLX                 NUMBER(10)                      not null,
   JLBH                 NUMBER(10),
   JFSL                 FLOAT(50)                      default 0,
   DFSL                 FLOAT(50)                      default 0,
   SYSL                 FLOAT(50)                      default 0,
   ZY                   VARCHAR2(20),
   constraint PK_HYK_HYFWCLJL primary key (FWBH)
)
/

/*==============================================================*/
/* Table: HYK_HYGRP                                             */
/*==============================================================*/
create table BFCRM10.HYK_HYGRP  (
   GRPID                NUMBER(10)                      not null,
   GRPMC                VARCHAR2(30)                    not null,
   GRPMS                VARCHAR2(100),
   GRPYT                NUMBER(10),
   KSSJ                 DATE,
   JSSJ                 DATE,
   DJR                  NUMBER(10),
   DJRMC                VARCHAR2(30),
   DJSJ                 DATE,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(30),
   ZXRQ                 DATE,
   ZZR                  NUMBER(10),
   ZZRMC                VARCHAR2(30),
   ZZRQ                 DATE,
   STATUS               NUMBER(5),
   SRFS                 NUMBER(5),
   GZFS                 NUMBER(5),
   GXZQ                 NUMBER(5),
   HYZLXID              NUMBER(10),
   JB                   NUMBER(5),
   YXQ                  DATE,
   BJ_DJT               NUMBER(5),
   BJ_YXXG              NUMBER(5),
   XGR                  NUMBER(10),
   XGRQ                 DATE,
   XGRMC                VARCHAR2(16),
   MDID                 NUMBER(10),
   BJ_WXHY              NUMBER(5),
   constraint PK_HYK_HYGRP primary key (GRPID)
)
/

/*==============================================================*/
/* Table: HYK_HYGRP_GZMX                                        */
/*==============================================================*/
create table BFCRM10.HYK_HYGRP_GZMX  (
   GRPID                NUMBER(10)                      not null,
   GZBH                 NUMBER(10)                      not null,
   SJLX                 NUMBER(10)                      not null,
   SJNR                 VARCHAR2(20)                    not null,
   SJNR2                VARCHAR2(20),
   constraint PK_HYK_HYGRP_GZMX primary key (GRPID, GZBH, SJLX, SJNR)
)
/

/*==============================================================*/
/* Table: HYK_HYQYDY                                            */
/*==============================================================*/
create table BFCRM10.HYK_HYQYDY  (
   QYID                 NUMBER(10)                      not null,
   QYDM                 VARCHAR2(10)                    not null,
   QYMC                 VARCHAR2(40)                    not null,
   YZBM                 VARCHAR2(6),
   BJ_MJ                NUMBER(5),
   constraint PK_HYK_HYQYDY primary key (QYID)
)
/

/*==============================================================*/
/* Index: HYQYDY_YZBM                                           */
/*==============================================================*/
create index BFCRM10.HYQYDY_YZBM on BFCRM10.HYK_HYQYDY (
   YZBM ASC
)
/

/*==============================================================*/
/* Index: HYQYDY_QYDM                                           */
/*==============================================================*/
create unique index BFCRM10.HYQYDY_QYDM on BFCRM10.HYK_HYQYDY (
   QYDM ASC
)
/

/*==============================================================*/
/* Table: HYK_HYTJGZ                                            */
/*==============================================================*/
create table BFCRM10.HYK_HYTJGZ  (
   JLBH                 NUMBER(10)                      not null,
   KSRQ                 DATE                            not null,
   JSRQ                 DATE                            not null,
   JLFS                 NUMBER(5)                       not null,
   XFJE                 NUMBER(14,2),
   XFCS                 NUMERIC(10),
   XFJF                 FLOAT,
   YHQID                NUMBER(10),
   YXQTS                NUMERIC(10),
   STATUS               NUMBER(5),
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16)                    not null,
   JDSJ                 DATE                            not null,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(16),
   ZXRQ                 DATE,
   ZZR                  NUMBER(10),
   ZZRMC                VARCHAR2(16),
   ZZRQ                 DATE,
   constraint PK_HYK_HYTJGZ primary key (JLBH)
)
/

/*==============================================================*/
/* Table: HYK_HYTJGZHYZ                                         */
/*==============================================================*/
create table BFCRM10.HYK_HYTJGZHYZ  (
   JLBH                 NUMBER(10)                      not null,
   GRPID                NUMBER(10)                      not null,
   constraint PK_HYK_HYTJGZHYZ primary key (JLBH, GRPID)
)
/

/*==============================================================*/
/* Table: HYK_HYTJGZITEM                                        */
/*==============================================================*/
create table BFCRM10.HYK_HYTJGZITEM  (
   JLBH                 NUMBER(10)                      not null,
   YXRS                 NUMBER(10)                      not null,
   JLSL                 NUMBER(14,2),
   constraint PK_HYK_HYTJGZITEM primary key (JLBH, YXRS)
)
/

/*==============================================================*/
/* Table: HYK_HYXMGZ                                            */
/*==============================================================*/
create table BFCRM10.HYK_HYXMGZ  (
   HYKTYPE              NUMBER(5)                       not null,
   XMJF_SX              FLOAT(50)                       not null,
   XFJE_JC              NUMBER(14,2)                    not null,
   XMSJ_ZD              NUMBER(5)                       not null,
   SXF_JC               NUMBER(14,2),
   constraint PK_HYK_HYXMGZ primary key (HYKTYPE)
)
/

/*==============================================================*/
/* Table: HYK_HYXMJL                                            */
/*==============================================================*/
create table BFCRM10.HYK_HYXMJL  (
   JLBH                 NUMBER(10)                      not null,
   HYKTYPE              NUMBER(5)                       not null,
   HYID                 NUMBER(10)                      not null,
   BJ_XM                NUMBER(5)                       not null,
   YXQ_NEW              DATE,
   YXQ_OLD              DATE                            not null,
   SXF                  NUMBER(14,2),
   HFFS                 NUMBER(10),
   ZY                   VARCHAR2(100),
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16)                    not null,
   DJSJ                 DATE                            not null,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(16),
   ZXRQ                 DATE,
   constraint PK_HYK_HYXMJL primary key (JLBH)
)
/

/*==============================================================*/
/* Table: HYK_HYXMJL_XFMX                                       */
/*==============================================================*/
create table BFCRM10.HYK_HYXMJL_XFMX  (
   JLBH                 NUMBER(10)                      not null,
   MDID                 NUMBER(10)                      not null,
   SKTNO                VARCHAR2(6)                     not null,
   XFXPH                NUMBER(10)                      not null,
   XFJE                 NUMBER(14,2),
   XFRQ                 DATE,
   constraint PK_HYK_HYXMJL_XFMX primary key (JLBH, MDID, SKTNO, XFXPH)
)
/

/*==============================================================*/
/* Table: HYK_HYXX                                              */
/*==============================================================*/
create sequence BFCRM10.S_HYK_HYXX
/
create table BFCRM10.HYK_HYXX  (
   HYID                 NUMBER(10)                      not null,
   HYKTYPE              NUMBER(5)                       not null,
   HYK_NO               VARCHAR2(60)                    not null,
   HY_NAME              VARCHAR2(20),
   CDNR                 VARCHAR2(80),
   JKRQ                 DATE                            not null,
   YXQ                  DATE,
   ZHXFRQ               DATE,
   BJ_PSW               NUMBER(5)                      default 0 not null,
   PASSWORD             VARCHAR2(8),
   DJSJ                 DATE                            not null,
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16),
   STATUS               NUMBER(5)                       not null,
   TM                   NUMBER(10),
   JGID                 NUMBER(5),
   FFSX                 NUMBER(5)                      default 0,
   OLD_HYKNO            VARCHAR2(16),
   YBGDD                VARCHAR2(10),
   KHID                 NUMBER(10),
   MDID                 NUMBER(10),
   BJ_PARENT            NUMBER(5),
   FXDW                 NUMBER(10),
   KFRYID               NUMBER(10),
   KFRYNAME             VARCHAR2(20),
   CDNR_OLD             VARCHAR2(40),
   KFRYID_WG            NUMBER(5),
   XYDJID               NUMBER(5),
   YZM                  VARCHAR2(20),
   ZHDLSJ               DATE,
   SJJRQ                DATE,
   GKID                 NUMBER(10),
   OPENID               VARCHAR2(60),
   MAINHYID             NUMBER(10),
   constraint PK_HYK_HYXX primary key (HYID)
)
/

/*==============================================================*/
/* Index: VIPXX_VIPNO                                           */
/*==============================================================*/
create unique index BFCRM10.VIPXX_VIPNO on BFCRM10.HYK_HYXX (
   HYK_NO ASC
)
/

/*==============================================================*/
/* Index: VIPXX_CDNR                                            */
/*==============================================================*/
create unique index BFCRM10.VIPXX_CDNR on BFCRM10.HYK_HYXX (
   CDNR ASC
)
/

/*==============================================================*/
/* Index: VIPXX_TM                                              */
/*==============================================================*/
create index BFCRM10.VIPXX_TM on BFCRM10.HYK_HYXX (
   TM ASC
)
/

/*==============================================================*/
/* Index: INDEX_HYK_HYXX_YXQ                                    */
/*==============================================================*/
create index BFCRM10.INDEX_HYK_HYXX_YXQ on BFCRM10.HYK_HYXX (
   YXQ ASC
)
/

/*==============================================================*/
/* Index: INDEX_HYKTYPE                                         */
/*==============================================================*/
create index BFCRM10.INDEX_HYKTYPE on BFCRM10.HYK_HYXX (
   HYKTYPE ASC
)
/

/*==============================================================*/
/* Index: INDEX_STATUS                                          */
/*==============================================================*/
create index BFCRM10.INDEX_STATUS on BFCRM10.HYK_HYXX (
   STATUS ASC
)
/

/*==============================================================*/
/* Index: "Inx_HYXX_GKID"                                       */
/*==============================================================*/
create index BFCRM10."Inx_HYXX_GKID" on BFCRM10.HYK_HYXX (
   GKID ASC
)
/

/*==============================================================*/
/* Table: HYK_HYXX_ZERO                                         */
/*==============================================================*/
create sequence BFCRM10.S_HYK_HYXX_ZERO
/
create table BFCRM10.HYK_HYXX_ZERO  (
   HYID                 NUMBER(10)                      not null,
   HYKTYPE              NUMBER(5)                       not null,
   HYK_NO               VARCHAR2(60)                    not null,
   HY_NAME              VARCHAR2(20),
   CDNR                 VARCHAR2(60),
   JKRQ                 DATE                            not null,
   YXQ                  DATE,
   ZHXFRQ               DATE,
   BJ_PSW               NUMBER(5)                      default 0 not null,
   PASSWORD             VARCHAR2(8),
   DJSJ                 DATE                            not null,
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16),
   STATUS               NUMBER(5)                       not null,
   TM                   NUMBER(10),
   JGID                 NUMBER(5),
   FFSX                 NUMBER(5)                      default 0,
   OLD_HYKNO            VARCHAR2(16),
   YBGDD                VARCHAR2(10),
   KHID                 NUMBER(10),
   MDID                 NUMBER(10),
   BJ_PARENT            NUMBER(5),
   FXDW                 NUMBER(5),
   KFRYID               NUMBER(10),
   KFRYNAME             VARCHAR2(20),
   CDNR_OLD             VARCHAR2(40),
   KFRYID_WG            NUMBER(5),
   XYDJID               NUMBER(5),
   YZM                  VARCHAR2(20),
   constraint PK_HYK_HYXX_ZERO primary key (HYID)
)
/

/*==============================================================*/
/* Index: VIPXX_VIPNO_ZERO                                      */
/*==============================================================*/
create index BFCRM10.VIPXX_VIPNO_ZERO on BFCRM10.HYK_HYXX_ZERO (
   HYK_NO ASC
)
/

/*==============================================================*/
/* Index: VIPXX_CDNR_ZERO                                       */
/*==============================================================*/
create unique index BFCRM10.VIPXX_CDNR_ZERO on BFCRM10.HYK_HYXX_ZERO (
   CDNR ASC
)
/

/*==============================================================*/
/* Index: VIPXX_ZERO_TM                                         */
/*==============================================================*/
create index BFCRM10.VIPXX_ZERO_TM on BFCRM10.HYK_HYXX_ZERO (
   TM ASC
)
/

/*==============================================================*/
/* Table: HYK_JEZCLJL                                           */
/*==============================================================*/
create sequence BFCRM10.S_HYK_JEZCLJL
/
create sequence BFCRM10.S_JEZCLJL_JYBH
/
create table BFCRM10.HYK_JEZCLJL  (
   JYBH                 NUMBER(9)                       not null,
   HYID                 NUMBER(10)                      not null,
   CLSJ                 DATE                            not null,
   CLLX                 NUMBER(5)                       not null,
   JLBH                 NUMBER(10),
   MDID                 NUMBER(10),
   SKTNO                VARCHAR2(6),
   ZY                   VARCHAR2(100),
   JFJE                 NUMBER(14,2)                   default 0 not null
      constraint CKC_JFJE_HYK_JEZC check (JFJE between -200000 and 200000),
   DFJE                 NUMBER(14,2)                   default 0 not null
      constraint CKC_DFJE_HYK_JEZC check (DFJE between -200000 and 200000),
   YE                   NUMBER(14,2)                   default 0 not null,
   JZRQ                 DATE,
   CRMJZRQ              DATE,
   SKYDM                VARCHAR2(8),
   SKYMC                VARCHAR2(16),
   JYID                 FLOAT(50),
   TM                   NUMBER(10),
   constraint PK_HYK_JEZCLJL primary key (JYBH)
)
/

/*==============================================================*/
/* Index: CZK_CLJL_RQ                                           */
/*==============================================================*/
create index BFCRM10.CZK_CLJL_RQ on BFCRM10.HYK_JEZCLJL (
   CLSJ ASC
)
/

/*==============================================================*/
/* Index: JEZCLJL_HYID                                          */
/*==============================================================*/
create index BFCRM10.JEZCLJL_HYID on BFCRM10.HYK_JEZCLJL (
   HYID ASC
)
/

/*==============================================================*/
/* Index: CZK_XFJL                                              */
/*==============================================================*/
create index BFCRM10.CZK_XFJL on BFCRM10.HYK_JEZCLJL (
   MDID ASC,
   SKTNO ASC,
   JLBH ASC
)
/

/*==============================================================*/
/* Index: JEZCLJL_JZRQ                                          */
/*==============================================================*/
create index BFCRM10.JEZCLJL_JZRQ on BFCRM10.HYK_JEZCLJL (
   MDID ASC,
   SKTNO ASC,
   CRMJZRQ ASC
)
/

/*==============================================================*/
/* Index: JEZCLJL_MD_SKT                                        */
/*==============================================================*/
create index BFCRM10.JEZCLJL_MD_SKT on BFCRM10.HYK_JEZCLJL (
   CLSJ ASC,
   MDID ASC,
   SKTNO ASC
)
/

/*==============================================================*/
/* Index: INDEX_JEZCLJL_TM                                      */
/*==============================================================*/
create index BFCRM10.INDEX_JEZCLJL_TM on BFCRM10.HYK_JEZCLJL (
   TM ASC
)
/

/*==============================================================*/
/* Table: HYK_JEZCLJL_ZERO                                      */
/*==============================================================*/
create sequence BFCRM10.S_HYK_JEZCLJL_ZERO
/
create table BFCRM10.HYK_JEZCLJL_ZERO  (
   JYBH                 NUMBER(9)                       not null,
   HYID                 NUMBER(10)                      not null,
   CLSJ                 DATE                            not null,
   CLLX                 NUMBER(5)                       not null,
   JLBH                 NUMBER(10),
   MDID                 NUMBER(10),
   SKTNO                VARCHAR2(6),
   ZY                   VARCHAR2(100),
   JFJE                 NUMBER(14,2)                    not null,
   DFJE                 NUMBER(14,2)                    not null,
   YE                   NUMBER(14,2)                   default 0 not null,
   JZRQ                 DATE,
   CRMJZRQ              DATE,
   SKYDM                VARCHAR2(8),
   SKYMC                VARCHAR2(16),
   JYID                 FLOAT(50),
   TM                   NUMBER(10),
   constraint PK_HYK_JEZCLJL_ZERO primary key (JYBH)
)
/

/*==============================================================*/
/* Index: CZK_CLJL_ZERO_RQ                                      */
/*==============================================================*/
create index BFCRM10.CZK_CLJL_ZERO_RQ on BFCRM10.HYK_JEZCLJL_ZERO (
   CLSJ ASC
)
/

/*==============================================================*/
/* Index: JEZCLJL_ZERO_HYID                                     */
/*==============================================================*/
create index BFCRM10.JEZCLJL_ZERO_HYID on BFCRM10.HYK_JEZCLJL_ZERO (
   HYID ASC
)
/

/*==============================================================*/
/* Index: CZK_XFJL_ZERO                                         */
/*==============================================================*/
create index BFCRM10.CZK_XFJL_ZERO on BFCRM10.HYK_JEZCLJL_ZERO (
   MDID ASC,
   SKTNO ASC,
   JLBH ASC
)
/

/*==============================================================*/
/* Index: JEZCLJL_ZERO_JZRQ                                     */
/*==============================================================*/
create index BFCRM10.JEZCLJL_ZERO_JZRQ on BFCRM10.HYK_JEZCLJL_ZERO (
   MDID ASC,
   SKTNO ASC,
   CRMJZRQ ASC
)
/

/*==============================================================*/
/* Index: JEZCLJL_ZERO_MD_SKT                                   */
/*==============================================================*/
create index BFCRM10.JEZCLJL_ZERO_MD_SKT on BFCRM10.HYK_JEZCLJL_ZERO (
   CLSJ ASC,
   MDID ASC,
   SKTNO ASC
)
/

/*==============================================================*/
/* Index: INDEX_JEZCLJL_ZERO_TM                                 */
/*==============================================================*/
create index BFCRM10.INDEX_JEZCLJL_ZERO_TM on BFCRM10.HYK_JEZCLJL_ZERO (
   TM ASC
)
/

/*==============================================================*/
/* Table: HYK_JEZH                                              */
/*==============================================================*/
create sequence BFCRM10.S_HYK_JEZH;
create table BFCRM10.HYK_JEZH  (
   HYID                 NUMBER(10)                      not null,
   QCYE                 NUMBER(14,2)                   default 0 not null,
   YE                   NUMBER(14,2)                   default 0 not null
      constraint CKC_YE_HYK_JEZH check (YE between 0 and 500000),
   YXTZJE               NUMBER(14,2)                   default 0 not null,
   TM                   NUMBER(10)                      not null,
   PDJE                 NUMBER(14,2)                   default 0 not null,
   JYDJJE               NUMBER(14,2)                   default 0
      constraint CKC_JYDJJE_HYK_JEZH check (JYDJJE is null or (JYDJJE >= 0)),
   FPJE                 NUMBER(14,2)                   default 0,
   constraint PK_HYK_JEZH primary key (HYID)
)
rowdependencies
/

/*==============================================================*/
/* Table: HYK_JEZH_ZERO                                         */
/*==============================================================*/
create sequence BFCRM10.S_HYK_JEZH_ZERO;
create table BFCRM10.HYK_JEZH_ZERO  (
   HYID                 NUMBER(10)                      not null,
   QCYE                 NUMBER(14,2)                   default 0 not null,
   YE                   NUMBER(14,2)                    not null,
   YXTZJE               NUMBER(14,2)                   default 0 not null,
   TM                   NUMBER(10)                      not null,
   PDJE                 NUMBER(14,2)                   default 0 not null,
   JYDJJE               NUMBER(14,2)                   default 0,
   FPJE                 NUMBER(14,2)                   default 0,
   BDSJ                 DATE,
   DAC                  RAW(8)
)
rowdependencies
/

/*==============================================================*/
/* Table: HYK_JEZ_PLZCITEM                                      */
/*==============================================================*/
create table BFCRM10.HYK_JEZ_PLZCITEM  (
   JLBH                 NUMBER(10)                      not null,
   HYID_ZC              NUMBER(10)                      not null,
   ZCJE                 NUMBER(14,2)                    not null,
   constraint PK_HYK_JEZ_PLZCITEM primary key (JLBH, HYID_ZC)
)
/

/*==============================================================*/
/* Table: HYK_JEZ_PLZCJL                                        */
/*==============================================================*/
create table BFCRM10.HYK_JEZ_PLZCJL  (
   JLBH                 NUMBER(10)                      not null,
   HYKTYPE              NUMBER(10)                      not null,
   ZCJE                 NUMBER(14,2)                    not null,
   ZRJE                 NUMBER(14,2)                    not null,
   ZCSL                 NUMBER(10)                      not null,
   ZRSL                 NUMBER(10)                      not null,
   CZDD                 VARCHAR2(10),
   JLBH_PLQK            NUMBER(10),
   JLBH_PLCK            NUMBER(10),
   ZY                   VARCHAR2(40),
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16)                    not null,
   DJSJ                 DATE                            not null,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(16),
   ZXRQ                 DATE,
   constraint PK_HYK_JEZ_PLZCJL primary key (JLBH)
)
/

/*==============================================================*/
/* Table: HYK_JEZ_PLZRITEM                                      */
/*==============================================================*/
create table BFCRM10.HYK_JEZ_PLZRITEM  (
   JLBH                 NUMBER(10)                      not null,
   HYID_ZR              NUMBER(10)                      not null,
   ZRJE                 NUMBER(14,2)                    not null,
   constraint PK_HYK_JEZ_PLZRITEM primary key (JLBH, HYID_ZR)
)
/

/*==============================================================*/
/* Table: HYK_JEZ_XFRBB                                         */
/*==============================================================*/
create sequence BFCRM10.S_HYK_JEZ_XFRBB
/
create table BFCRM10.HYK_JEZ_XFRBB  (
   RQ                   DATE                            not null,
   HYKTYPE              NUMBER(5)                       not null,
   MDID                 NUMBER(5)                       not null,
   CZMD                 NUMBER(5)                       not null,
   SKTNO                VARCHAR2(6)                     not null,
   XFJE                 NUMBER(14,2)                   default 0 not null,
   TKJE                 NUMBER(14,2)                   default 0,
   TZJE                 NUMBER(14,2)                   default 0,
   SKJE                 NUMBER(14,2)                   default 0,
   CKJE                 NUMBER(14,2)                   default 0,
   QKJE                 NUMBER(14,2)                   default 0,
   BKJE                 NUMBER(14,2)                   default 0,
   YEARMONTH            NUMBER(10),
   TM                   NUMBER(10),
   constraint PK_HYK_JEZ_XFRBB primary key (RQ, HYKTYPE, MDID, CZMD, SKTNO)
)
/

/*==============================================================*/
/* Table: HYK_JFBDD                                             */
/*==============================================================*/
create table BFCRM10.HYK_JFBDD  (
   JLBH                 NUMBER(10)                      not null,
   CZDD                 VARCHAR2(10),
   DJLX                 NUMBER(5),
   ZY                   VARCHAR2(40),
   BJ_CLWCLJF           NUMBER(5)                      default 0,
   BJ_CLBQJF            NUMBER(5)                      default 0,
   BJ_CLBNLJJF          NUMBER(5)                      default 0,
   BJ_CLLJJF            NUMBER(5)                      default 0,
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16)                    not null,
   DJSJ                 DATE                            not null,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(16),
   ZXRQ                 DATE,
   constraint PK_HYK_JFBDD primary key (JLBH)
)
/

/*==============================================================*/
/* Table: HYK_JFBDDITEM                                         */
/*==============================================================*/
create table BFCRM10.HYK_JFBDDITEM  (
   JLBH                 NUMBER(10)                      not null,
   HYID                 NUMBER(10)                      not null,
   HYK_NO               VARCHAR2(30),
   HYKTYPE              NUMBER(5),
   BJ_CHILD             NUMBER(5),
   WCLJF_OLD            NUMBER(16,4),
   TZJF                 NUMBER(16,4),
   TZJE                 NUMBER(14,2),
   constraint PK_HYK_JFBDDITEM primary key (JLBH, HYID)
)
/

/*==============================================================*/
/* Table: HYK_JFBDDITEM_XP                                      */
/*==============================================================*/
create table BFCRM10.HYK_JFBDDITEM_XP  (
   JLBH                 NUMBER(10)                      not null,
   XPH                  NUMBER(10)                      not null,
   SKTNO                VARCHAR2(16)                    not null,
   MDID                 NUMBER(10)                      not null,
   constraint PK_HYK_JFBDDITEM_XP primary key (JLBH, XPH, SKTNO, MDID)
)
/

/*==============================================================*/
/* Table: HYK_JFBDJL                                            */
/*==============================================================*/
create sequence BFCRM10.S_HYK_JFBDJL
/
create table BFCRM10.HYK_JFBDJL  (
   HYID                 NUMBER(10)                      not null,
   RQ                   DATE                            not null,
   YEARMONTH            NUMBER(10)                      not null,
   WCLJFBD              FLOAT(50)                       not null,
   BQJFBD               FLOAT(50)                       not null,
   WCLJF                FLOAT(50)                       not null,
   BQJF                 FLOAT(50)                       not null,
   TM                   NUMBER(10),
   BNLJJFBD             FLOAT(50)                      default 0,
   BNLJJF               FLOAT(50)                      default 0,
   constraint PK_HYK_JFBDJL primary key (HYID, RQ)
)
/

/*==============================================================*/
/* Index: HYKJFBDJL_NY                                          */
/*==============================================================*/
create index BFCRM10.HYKJFBDJL_NY on BFCRM10.HYK_JFBDJL (
   YEARMONTH ASC,
   HYID ASC
)
/

/*==============================================================*/
/* Table: HYK_JFBDJLMX                                          */
/*==============================================================*/
create table BFCRM10.HYK_JFBDJLMX  (
   JYBH                 NUMBER(10)                      not null,
   CZMD                 NUMBER(10)                      not null,
   SKTNO                VARCHAR2(10),
   JLBH                 NUMBER(10),
   HYID                 NUMBER(10)                      not null,
   CLSJ                 DATE                            not null,
   CLLX                 NUMBER(5)                       not null
      constraint CKC_CLLX_HYK_JFBD check (CLLX >= 31),
   ZY                   VARCHAR2(40),
   WCLJFBD              NUMBER(16,4)                   default 0 not null,
   WCLJF                NUMBER(16,4)                   default 0 not null,
   CRMJZRQ              DATE,
   CZYDM                VARCHAR2(20),
   CZYMC                VARCHAR2(20),
   BQJFBD               NUMBER(16,4),
   BQJF                 NUMBER(16,4),
   XFJEBD               NUMBER(16,4),
   XFJE                 NUMBER(16,4),
   constraint PK_HYK_JFBDJLMX primary key (JYBH)
)
/

/*==============================================================*/
/* Table: HYK_JFBDJLMX_MD                                       */
/*==============================================================*/
create table BFCRM10.HYK_JFBDJLMX_MD  (
   JYBH                 NUMBER(10)                      not null,
   MDID                 NUMBER(10)                      not null,
   WCLJFBD              NUMBER(16,4)                   default 0 not null,
   WCLJF                NUMBER(16,4)                   default 0 not null,
   XFJE                 NUMBER(16,4),
   XFJEBD               NUMBER(16,4),
   BQJF                 NUMBER(16,4),
   BQJFBD               NUMBER(16,4),
   constraint PK_HYK_JFBDJLMX_MD primary key (JYBH, MDID)
)
/

/*==============================================================*/
/* Table: HYK_JFCLGZ                                            */
/*==============================================================*/
create table BFCRM10.HYK_JFCLGZ  (
   HYKTYPE              NUMBER(5)                       not null,
   CLRC                 NUMBER(5)                       not null,
   KSRQ                 DATE                            not null,
   CLFW                 NUMBER(5)                       not null,
   QZQD                 VARCHAR2(4),
   JSRQ                 DATE,
   YXBJSCL_BJ           NUMBER(5)                       not null
      constraint CKC_YXBJSCL_BJ_HYK_JFCL check (YXBJSCL_BJ in (0,1,2,3,4)),
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16),
   DJSJ                 DATE                            not null,
   STATUS               NUMBER(5)                       not null,
   SYJSRQ               DATE,
   constraint PK_HYK_JFCLGZ primary key (HYKTYPE)
)
/

/*==============================================================*/
/* Table: HYK_JFCLGZITEM                                        */
/*==============================================================*/
create table BFCRM10.HYK_JFCLGZITEM  (
   HYKTYPE              NUMBER(5)                       not null,
   XH                   NUMBER(5)                       not null,
   JFXX                 FLOAT(50)                      default 0 not null,
   JFSX                 FLOAT(50)                       not null,
   HBFS                 NUMBER(5)                       not null
      constraint CKC_HBFS_HYK_JFCL check (HBFS in (0,1,2)),
   JF_JE                NUMBER(14,2)                   default 0 not null,
   CONTENT              VARCHAR2(100),
   constraint PK_HYK_JFCLGZITEM primary key (HYKTYPE, XH)
)
/

/*==============================================================*/
/* Table: HYK_JFCLGZITEMLP                                      */
/*==============================================================*/
create table BFCRM10.HYK_JFCLGZITEMLP  (
   HYKTYPE              NUMBER(5)                       not null,
   XH                   NUMBER(5)                       not null,
   LPDM                 VARCHAR2(6)                     not null,
   SL                   FLOAT(50)                      default 0 not null,
   constraint PK_HYK_JFCLGZITEMLP primary key (HYKTYPE, XH, LPDM)
)
/

/*==============================================================*/
/* Table: HYK_JFDHBL                                            */
/*==============================================================*/
create table BFCRM10.HYK_JFDHBL  (
   JLBH                 NUMBER(10)                      not null,
   KSRQ                 DATE                            not null,
   JSRQ                 DATE                            not null,
   HYKTYPE              NUMBER(10)                      not null,
   DHJF                 FLOAT,
   DHJE                 NUMBER(14,2),
   DJR                  NUMBER(10),
   DJRMC                VARCHAR2(16),
   DJSJ                 DATE,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(16),
   ZXRQ                 DATE,
   ZZSJ                 DATE,
   ZZR                  NUMBER(10),
   ZZRMC                VARCHAR2(16),
   QDJF                 number(16,4)                   default 0,
   MDID                 NUMBER(10),
   STATUS               NUMBER(5),
   CXID                 NUMBER(10),
   constraint PK_HYK_JFDHBL primary key (JLBH)
)
/

/*==============================================================*/
/* Table: HYK_JFFHGZMX                                          */
/*==============================================================*/
create table BFCRM10.HYK_JFFHGZMX  (
   JLBH                 NUMBER(10)                      not null,
   GZH                  NUMBER(5)                       not null,
   XFKSRQ               DATE,
   XFJSRQ               DATE,
   ZFXX                 FLOAT(50),
   ZFSX                 FLOAT(50),
   JSZF                 FLOAT(50),
   PPFXX                FLOAT(50),
   PPFSX                FLOAT(50),
   JSPPF                FLOAT(50),
   HBFS                 NUMBER(5),
   FLJE                 NUMBER(14,2),
   CONTENT              VARCHAR2(100),
   constraint PK_HYK_JFFHGZMX primary key (JLBH, GZH)
)
/

/*==============================================================*/
/* Table: HYK_JFFHLPBFJL                                        */
/*==============================================================*/
create table BFCRM10.HYK_JFFHLPBFJL  (
   JLBH                 NUMBER(10)                      not null,
   BGDDDM               VARCHAR2(20)                    not null,
   BZ                   VARCHAR2(30),
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16)                    not null,
   DJSJ                 DATE                            not null,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(16),
   ZXRQ                 DATE,
   constraint PK_HYK_JFFHLPBFJL primary key (JLBH)
)
/

/*==============================================================*/
/* Table: HYK_JFFHLPBFJLITEM                                    */
/*==============================================================*/
create table BFCRM10.HYK_JFFHLPBFJLITEM  (
   JLBH                 NUMBER(10)                      not null,
   LPID                 NUMBER(10)                      not null,
   LPDM                 VARCHAR2(6),
   LPMC                 VARCHAR2(30),
   BFSL                 NUMBER(14,4)                   default 0 not null,
   LPDJ                 NUMBER(14,2),
   LPJF                 NUMBER(14,4),
   constraint PK_HYK_JFFHLPBFJLITEM primary key (JLBH, LPID)
)
/

/*==============================================================*/
/* Table: HYK_JFFHLPJHD                                         */
/*==============================================================*/
create table BFCRM10.HYK_JFFHLPJHD  (
   JLBH                 NUMBER(10)                      not null,
   GHSID                NUMBER(10),
   BGDDDM               VARCHAR2(20)                    not null,
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16)                    not null,
   DJSJ                 DATE                            not null,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(16),
   ZXRQ                 DATE,
   ZSL                  FLOAT(50)                      default 0 not null,
   ZJE                  NUMBER(14,2),
   JJJE                 NUMBER(14,2),
   BZ                   VARCHAR2(30),
   DJLX                 NUMBER(5),
   DHDBH                NUMBER(10),
   BJ_WDD               NUMBER(5),
   THYY                 VARCHAR2(50),
   constraint PK_HYK_JFFHLPJHD primary key (JLBH)
)
/

/*==============================================================*/
/* Table: HYK_JFFHLPJHDMX                                       */
/*==============================================================*/
create table BFCRM10.HYK_JFFHLPJHDMX  (
   JLBH                 NUMBER(10)                      not null,
   LPID                 NUMBER(10)                      not null,
   JHSL                 NUMBER(14,4)                   default 0 not null,
   LPDJ                 NUMBER(14,2),
   LPJJ                 NUMBER(14,2),
   DHSL                 NUMBER(14,4),
   KCSL                 NUMBER(14,4),
   LPJF                 NUMBER(14,4),
   LPDM                 VARCHAR2(10),
   constraint PK_HYK_JFFHLPJHDMX primary key (JLBH, LPID)
)
/

/*==============================================================*/
/* Table: HYK_JFFHLPJXC                                         */
/*==============================================================*/
create table BFCRM10.HYK_JFFHLPJXC  (
   RQ                   DATE                            not null,
   MDID                 NUMBER(10)                      not null,
   LPID                 NUMBER(10)                      not null,
   QCSL                 NUMBER(14,2)                   default 0 not null,
   JHSL                 NUMBER(14,2)                   default 0 not null,
   BRSL                 NUMBER(14,2)                   default 0 not null,
   BCSL                 NUMBER(14,2)                   default 0 not null,
   FFSL                 NUMBER(14,2)                   default 0 not null,
   ZFSL                 NUMBER(14,2)                   default 0 not null,
   SYSL                 NUMBER(14,2)                   default 0 not null,
   THSL                 NUMBER(14,2)                   default 0 not null,
   ZTSL                 NUMBER(14,2)                   default 0 not null,
   JCSL                 NUMBER(14,2)                   default 0 not null,
   constraint PK_HYK_JFFHLPJXC primary key (RQ, MDID, LPID)
)
/

/*==============================================================*/
/* Table: HYK_JFFHLPKC                                          */
/*==============================================================*/
create table BFCRM10.HYK_JFFHLPKC  (
   LPID                 NUMBER(10)                      not null,
   BGDDDM               VARCHAR2(20)                    not null,
   KCSL                 NUMBER(16,4)                   default 0
      constraint CKC_KCSL_HYK_JFFH check (KCSL is null or (KCSL >= 0)),
   constraint PK_HYK_JFFHLPKC primary key (LPID, BGDDDM)
)
/

/*==============================================================*/
/* Table: HYK_JFFHLPXX                                          */
/*==============================================================*/
create table BFCRM10.HYK_JFFHLPXX  (
   LPID                 NUMBER(10),
   LPDM                 VARCHAR2(6)                     not null,
   LPMC                 VARCHAR2(30)                    not null,
   LPGG                 VARCHAR2(20)                    not null,
   LPDJ                 NUMBER(14,2)                    not null,
   LPJJ                 NUMBER(14,2),
   LPJF                 NUMBER(16,4),
   BJ_DEL               NUMBER(5),
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(20)                    not null,
   DJSJ                 DATE                            not null,
   XGR                  NUMBER(10),
   XGRMC                VARCHAR2(20),
   XGSJ                 DATE,
   BJ_WKC               NUMBER(5)                      default 0,
   GJBM                 VARCHAR2(20),
   ZSJF                 NUMBER(16,4),
   LPFLID               NUMBER(10),
   LPLX                 NUMBER(10)                     default 0,
   constraint PK_HYK_JFFHLPXX primary key (LPDM)
)
/

/*==============================================================*/
/* Table: HYK_JFFLGZ                                            */
/*==============================================================*/
create table BFCRM10.HYK_JFFLGZ  (
   JLBH                 NUMBER(10)                      not null,
   SHDM                 VARCHAR2(6)                     not null,
   HYKTYPE              NUMBER(10)                      not null,
   HBFS                 NUMBER(5)                       not null,
   CLRC                 NUMBER(10),
   CLFS                 NUMBER(5),
   XSWS                 NUMBER(5),
   KSRQ                 DATE,
   JSRQ                 DATE,
   YHQID                NUMBER(10),
   YHQJSRQ              DATE,
   YHQSL                NUMBER(10),
   YHQDW                NUMBER(10),
   STATUS               NUMBER(10),
   DJR                  NUMBER(10),
   DJRMC                VARCHAR2(16),
   DJSJ                 DATE,
   BJ_KCDYJF            NUMBER(10),
   ZXDW                 NUMBER(16,4),
   GZMC                 VARCHAR2(30),
   BJ_XZ                NUMBER(5),
   MDID                 NUMBER(10),
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(16),
   ZXRQ                 DATE,
   ZZRQ                 DATE,
   ZZR                  NUMBER(10),
   ZZRMC                VARCHAR2(16),
   constraint PK_HYK_JFFLGZ primary key (JLBH)
)
/

/*==============================================================*/
/* Table: HYK_JFFLGZITEM                                        */
/*==============================================================*/
create table BFCRM10.HYK_JFFLGZITEM  (
   JLBH                 NUMBER(10)                      not null,
   XH                   NUMBER(5)                       not null,
   JFXX                 NUMBER(16,4)                    not null,
   JFSX                 NUMBER(16,4)                    not null,
   FLBL                 NUMBER(14,2)                    not null,
   constraint PK_HYK_JFFLGZITEM primary key (JLBH, XH)
)
/

/*==============================================================*/
/* Table: HYK_JFFLJL                                            */
/*==============================================================*/
create table BFCRM10.HYK_JFFLJL  (
   JLBH                 NUMBER(10)                      not null,
   HBFS                 NUMBER(5),
   CZJLBH               NUMBER(10),
   DJR                  NUMBER(10),
   DJRMC                VARCHAR2(16),
   DJSJ                 DATE,
   SHR                  NUMBER(10),
   SHRMC                VARCHAR2(16),
   SHRQ                 DATE,
   CZDD                 VARCHAR2(20),
   ZY                   VARCHAR2(40),
   CLRQ                 DATE,
   CLJF                 FLOAT(50),
   FLJE                 NUMBER(14,2),
   DYCS                 NUMBER(10),
   DJLX                 NUMBER(10),
   WCLJF_OLD            FLOAT(50),
   SJFLJE               NUMBER(14,2),
   KYXFJE               NUMBER(14,2),
   CLIENT_IP_ADDRESS    VARCHAR2(40),
   HYID                 NUMBER(10),
   MDID                 NUMBER(10),
   YHQID                NUMBER(10),
   YHQJSRQ              DATE,
   FQGZID               NUMBER(10),
   constraint PK_HYK_JFFLJL primary key (JLBH)
)
/

/*==============================================================*/
/* Table: HYK_JFFLJLITEM                                        */
/*==============================================================*/
create table BFCRM10.HYK_JFFLJLITEM  (
   JLBH                 NUMBER(10)                      not null,
   HYID                 NUMBER(10)                      not null,
   FLGZBH               NUMBER(10)                      not null,
   SHDM                 VARCHAR2(10),
   YHQID                NUMBER(10),
   WCLXFJE              NUMBER(14,2)                   default 0,
   CLJF                 FLOAT(50),
   FLJE                 NUMBER(14,2),
   YHQJSRQ              DATE,
   WCLJF_OLD            FLOAT(50),
   CLJE                 NUMBER(14,2),
   LPJSJE               NUMBER(14,2),
   LJJSJE               NUMBER(14,2),
   YXXFJE               NUMBER(14,2),
   LJXFJE               NUMBER(14,2),
   HYK_NO               VARCHAR2(30),
   BJ_CHILD             NUMBER(5),
   constraint PK_HYK_JFFLJLITEM primary key (JLBH, HYID, FLGZBH)
)
/

/*==============================================================*/
/* Table: HYK_JFFLJL_LP                                         */
/*==============================================================*/
create table BFCRM10.HYK_JFFLJL_LP  (
   JLBH                 NUMBER(10)                      not null,
   BGDDDM               VARCHAR2(20)                    not null,
   LPID                 NUMBER(10)                      not null,
   SL                   NUMBER(16,4)                   default 0,
   LPDJ                 NUMBER(14,2),
   LPJF                 NUMBER(16,4),
   LPJJ                 NUMBER(14,2),
   LPJE                 NUMBER(14,2),
   GZID                 NUMBER(10),
   CXHDBH               NUMBER(10),
   RQ                   VARCHAR2(20),
   ZSJF                 NUMBER(16,4),
   JSJE                 NUMBER(14,2),
   KCSL                 NUMBER(14,2),
   constraint PK_HYK_JFFLJL_LP primary key (JLBH, BGDDDM, LPID)
)
/

/*==============================================================*/
/* Table: HYK_JFFLJL_LP_ITEM                                    */
/*==============================================================*/
create table BFCRM10.HYK_JFFLJL_LP_ITEM  (
   JLBH                 NUMBER(10)                      not null,
   CXHDBH               NUMBER(10)                      not null,
   GZID                 NUMBER(10)                      not null,
   LJFS                 NUMBER(5)                       not null,
   BMDM                 VARCHAR2(10)                    not null,
   RQ                   VARCHAR2(20)                    not null,
   ZXFJE                NUMBER(14,2)                   default 0 not null,
   SYXFJE               NUMBER(14,2)                   default 0 not null,
   FQJE                 NUMBER(14,2)                   default 0 not null,
   LPGRP                NUMBER(10),
   XFJLID               NUMBER(10),
   constraint PK_HYK_JFFLJL_LP_ITEM primary key (JLBH, CXHDBH, GZID, LJFS, BMDM, RQ)
)
/

/*==============================================================*/
/* Table: HYK_JFFLJL_MD                                         */
/*==============================================================*/
create table BFCRM10.HYK_JFFLJL_MD  (
   JLBH                 NUMBER(10)                      not null,
   HYID                 NUMBER(10),
   MDID                 NUMBER(10)                      not null,
   CLJF                 FLOAT(50),
   WCLXFJE              NUMBER(14,2),
   WCLJF_OLD            FLOAT(50),
   constraint PK_HYK_JFFLJL_MD primary key (JLBH, MDID)
)
/

/*==============================================================*/
/* Table: HYK_JFFLJL_SP                                         */
/*==============================================================*/
create table BFCRM10.HYK_JFFLJL_SP  (
   XFJLID               NUMBER(10)                     default 0 not null,
   INX                  NUMBER(5)                       not null,
   BMDM                 VARCHAR2(20)                    not null,
   SPDM                 VARCHAR2(20)                    not null,
   SHSPID               NUMBER(10),
   JSZLD                NUMBER(10),
   CXHDBH               NUMBER(10)                      not null,
   LJFS                 NUMBER(5)                       not null,
   GZID                 NUMBER(10)                      not null,
   XSSL                 NUMBER(16,4)                    not null,
   XSJE                 NUMBER(14,2)                    not null,
   XSJE_FQ              NUMBER(14,2)                    not null,
   FQJE                 NUMBER(14,2)                    not null,
   FQJE_SJ              NUMBER(14,2)                   default 0 not null,
   constraint PK_HYK_JFFLJL_SP primary key (XFJLID, INX)
)
/

/*==============================================================*/
/* Table: HYK_JFFLJL_XFJL                                       */
/*==============================================================*/
create table BFCRM10.HYK_JFFLJL_XFJL  (
   JLBH                 NUMBER(10)                      not null,
   MDID                 NUMBER(10)                      not null,
   SKTNO                VARCHAR2(10)                    not null,
   XPH                  NUMBER(10)                      not null,
   YXJE                 NUMBER(14,2),
   XFJE                 NUMBER(14,2),
   XFJLID               NUMBER(10),
   XFSJ                 DATE,
   HYID                 NUMBER(10),
   HYK_NO               VARCHAR2(30),
   constraint PK_HYK_JFFLJL_XFJL primary key (JLBH, MDID, SKTNO, XPH)
)
/

/*==============================================================*/
/* Table: HYK_JFFLJL_ZQ                                         */
/*==============================================================*/
create table BFCRM10.HYK_JFFLJL_ZQ  (
   JLBH                 NUMBER(10)                      not null,
   HYID                 NUMBER(10)                      not null,
   YHQCODE              VARCHAR2(30)                    not null,
   YHQID                NUMBER(10)                      not null,
   YHQMZ                NUMBER(14,2)                    not null,
   ENDDATE              DATE                            not null,
   CXID                 NUMBER(10)                      not null,
   MDID_FQ              NUMBER(10),
   MDID_YQ              NUMBER(10),
   YQSJ                 DATE,
   FQSJ                 DATE,
   STATUS               NUMBER(5)                       not null,
   constraint PK_HYK_JFFLJL_ZQ primary key (JLBH, HYID, YHQCODE)
)
/

/*==============================================================*/
/* Table: HYK_JFLXDEF                                           */
/*==============================================================*/
create sequence BFCRM10.S_HYK_JFLXDEF
/
create table BFCRM10.HYK_JFLXDEF  (
   JFLX                 NUMBER(10)                      not null,
   constraint PK_HYK_JFLXDEF primary key (JFLX)
)
/

comment on table BFCRM10.HYK_JFLXDEF is
'此表为系统内部表，不需要客户设定，不可以随意增减，只做数据记录'
/

/*==============================================================*/
/* Table: HYK_JFLXZH                                            */
/*==============================================================*/
create sequence BFCRM10.S_HYK_JFLXZH
/
create table BFCRM10.HYK_JFLXZH  (
   HYID                 NUMBER(10)                      not null,
   JFLX                 NUMBER(10)                      not null,
   YEAR                 NUMBER(10)                      not null,
   "Column_6"           CHAR(10),
   "Column_5"           CHAR(10),
   "Column_4"           CHAR(10),
   "Column_3"           CHAR(10),
   JF                   NUMBER(16,4)                    not null,
   constraint PK_HYK_JFLXZH primary key (HYID, JFLX, YEAR)
)
/

/*==============================================================*/
/* Table: HYK_JFRHZ                                             */
/*==============================================================*/
create table BFCRM10.HYK_JFRHZ  (
   RQ                   DATE                            not null,
   HYKTYPE              NUMBER(5)                       not null,
   FXMD                 NUMBER(10)                      not null,
   WCLJF                FLOAT(50),
   LJJF                 FLOAT(50),
   SL                   NUMBER(10),
   constraint PK_HYK_JFRHZ primary key (RQ, HYKTYPE, FXMD)
)
/

/*==============================================================*/
/* Table: HYK_JFRHZMD                                           */
/*==============================================================*/
create table BFCRM10.HYK_JFRHZMD  (
   RQ                   DATE                            not null,
   HYKTYPE              NUMBER(5)                       not null,
   FXMD                 NUMBER(10)                      not null,
   JCMD                 NUMBER(10)                      not null,
   WCLJF                FLOAT(50),
   LJJF                 FLOAT(50),
   constraint PK_HYK_JFRHZMD primary key (RQ, HYKTYPE, FXMD, JCMD)
)
/

/*==============================================================*/
/* Table: HYK_JFTZJL                                            */
/*==============================================================*/
create table BFCRM10.HYK_JFTZJL  (
   JLBH                 NUMBER(10)                      not null,
   MDID                 NUMBER(10),
   SKTNO                VARCHAR2(6),
   XSJYBH               NUMBER(10),
   HYID                 NUMBER(10),
   XSRQ                 DATE,
   TZJF                 FLOAT(50),
   ZY                   VARCHAR2(40),
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16)                    not null,
   DJSJ                 DATE                            not null,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(16),
   ZXRQ                 DATE,
   HYKNO                VARCHAR2(36),
   BJ_CHILD             NUMBER(5),
   constraint PK_HYK_JFTZJL primary key (JLBH)
)
/

/*==============================================================*/
/* Index: JFTZ_RQ                                               */
/*==============================================================*/
create index BFCRM10.JFTZ_RQ on BFCRM10.HYK_JFTZJL (
   ZXRQ ASC
)
/

/*==============================================================*/
/* Index: JFTZ_HYID                                             */
/*==============================================================*/
create index BFCRM10.JFTZ_HYID on BFCRM10.HYK_JFTZJL (
   HYID ASC
)
/

/*==============================================================*/
/* Table: HYK_JFTZJLITEM                                        */
/*==============================================================*/
create table BFCRM10.HYK_JFTZJLITEM  (
   JLBH                 NUMBER(10)                      not null,
   SHSPID               NUMBER(10)                      not null,
   SPDM                 VARCHAR2(13)                    not null,
   XSBM                 VARCHAR2(10)                    not null,
   JF_OLD               FLOAT(50)                       not null,
   TZJF                 FLOAT(50),
   HTH                  VARCHAR2(30),
   constraint PK_HYK_JFTZJLITEM primary key (JLBH, SHSPID, XSBM)
)
/

/*==============================================================*/
/* Table: HYK_JFYBB_MD                                          */
/*==============================================================*/
create table BFCRM10.HYK_JFYBB_MD  (
   YEARMONTH            NUMBER(10)                      not null,
   MDID                 NUMBER(10)                      not null,
   LX                   NUMBER(5)                       not null,
   XZJF                 FLOAT(50)                      default 0,
   LJJF                 FLOAT(50)                      default 0,
   DHJF                 FLOAT(50)                      default 0,
   constraint PK_HYK_JFYBB_MD primary key (YEARMONTH, MDID, LX)
)
/

/*==============================================================*/
/* Table: HYK_JFYCGZ                                            */
/*==============================================================*/
create table BFCRM10.HYK_JFYCGZ  (
   GZID                 NUMBER(5)                       not null,
   HYKTYPE              NUMBER(5)                       not null,
   GZMC                 VARCHAR2(50)                    not null,
   QSJF                 FLOAT(50)                       not null,
   ZZJF                 FLOAT(50)                       not null,
   constraint PK_HYK_JFYCGZ primary key (HYKTYPE, GZID)
)
/

/*==============================================================*/
/* Table: HYK_JFYEAR                                            */
/*==============================================================*/
create sequence BFCRM10.S_HYK_JFYEAR
/
create table BFCRM10.HYK_JFYEAR  (
   ND                   NUMBER(10)                      not null,
   HYID                 NUMBER(10)                      not null,
   WCLJF                FLOAT(50)                       not null,
   XFJE                 NUMBER(14,2)                   default 0 not null,
   ZKJE                 NUMBER(14,2)                    not null,
   BQJF                 FLOAT(50)                      default 0,
   LJJF                 FLOAT(50)                      default 0,
   LJXFJE               NUMBER(14,2)                   default 0,
   LJZKJE               NUMBER(14,2)                   default 0,
   XFCS                 NUMBER(10)                     default 0,
   THCS                 NUMBER(10)                     default 0,
   TM                   NUMBER(10),
   BNLJJF               FLOAT(50)                      default 0,
   LJXFJE_JF            NUMBER(14,2),
   constraint PK_HYK_JFYEAR primary key (ND, HYID)
)
/

/*==============================================================*/
/* Table: HYK_JFZCJL                                            */
/*==============================================================*/
create table BFCRM10.HYK_JFZCJL  (
   JLBH                 NUMBER(10)                      not null,
   HYID_ZR              NUMBER(10)                      not null,
   ZRJF                 FLOAT(50)                       not null,
   CZDD                 VARCHAR2(10)                    not null,
   ZY                   VARCHAR2(40),
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16)                    not null,
   DJSJ                 DATE                            not null,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(16),
   ZXRQ                 DATE,
   HYKTYPE              NUMBER(10),
   constraint PK_HYK_JFZCJL primary key (JLBH)
)
/

/*==============================================================*/
/* Index: HYK_JFZC_HYID                                         */
/*==============================================================*/
create index BFCRM10.HYK_JFZC_HYID on BFCRM10.HYK_JFZCJL (
   HYID_ZR ASC
)
/

/*==============================================================*/
/* Table: HYK_JFZCJLITEM                                        */
/*==============================================================*/
create table BFCRM10.HYK_JFZCJLITEM  (
   JLBH                 NUMBER(10)                      not null,
   HYID_ZC              NUMBER(10)                      not null,
   ZCJF                 FLOAT(50)                       not null,
   constraint PK_HYK_JFZCJLITEM primary key (JLBH, HYID_ZC)
)
/

/*==============================================================*/
/* Index: HYK_JFZCITEM_HYID                                     */
/*==============================================================*/
create index BFCRM10.HYK_JFZCITEM_HYID on BFCRM10.HYK_JFZCJLITEM (
   HYID_ZC ASC
)
/

/*==============================================================*/
/* Table: HYK_JFZH                                              */
/*==============================================================*/
create sequence BFCRM10.S_HYK_JFZH
/
create table BFCRM10.HYK_JFZH  (
   HYID                 NUMBER(10)                      not null,
   WCLJF                NUMBER(16,4)                    not null,
   WCLXFJE              NUMBER(14,2),
   BQJF                 NUMBER(16,4)                   default 0,
   XFJE                 NUMBER(14,2)                   default 0 not null,
   BNLJJF               NUMBER(16,4)                   default 0,
   BNXFJE               NUMBER(14,2),
   ZKJE                 NUMBER(14,2)                    not null,
   LJJF                 NUMBER(16,4)                   default 0,
   LJXFJE               NUMBER(14,2)                   default 0,
   LJZKJE               NUMBER(14,2)                   default 0,
   XFCS                 NUMBER(10)                     default 0,
   THCS                 NUMBER(10)                     default 0,
   TM                   NUMBER(10),
   LJXFJE_JF            NUMBER(14,2),
   JCJF                 NUMBER(16,4)                   default 0,
   WCLJF_TS             NUMBER(16,4),
   constraint PK_HYK_JFZH primary key (HYID)
)
/

/*==============================================================*/
/* Index: INDEX_HYK_JFZH_TM                                     */
/*==============================================================*/
create index BFCRM10.INDEX_HYK_JFZH_TM on BFCRM10.HYK_JFZH (
   TM ASC
)
/

/*==============================================================*/
/* Index: INDEX_HYK_JFZH_BQJF                                   */
/*==============================================================*/
create index BFCRM10.INDEX_HYK_JFZH_BQJF on BFCRM10.HYK_JFZH (
   BQJF ASC
)
/

/*==============================================================*/
/* Table: HYK_JFZHMONTH                                         */
/*==============================================================*/
create table BFCRM10.HYK_JFZHMONTH  (
   HYID                 NUMBER(10)                      not null,
   YEARMONTH            NUMBER(10)                      not null,
   MXFJE                NUMBER(14,2)                   default 0 not null,
   MZKJE                NUMBER(14,2)                   default 0 not null,
   MCLJF                FLOAT(50)                      default 0 not null,
   MWCLJF               FLOAT(50)                      default 0 not null,
   JFCLRQ               DATE                            not null,
   constraint PK_HYK_JFZHMONTH primary key (HYID, YEARMONTH)
)
/

/*==============================================================*/
/* Table: HYK_JF_DHBL                                           */
/*==============================================================*/
create table BFCRM10.HYK_JF_DHBL  (
   HYKTYPE              NUMBER(10)                      not null,
   DHJF                 NUMBER(14,2),
   DHJE                 NUMBER(14,2),
   QDJF                 NUMBER(16,4)                   default 0,
   constraint PK_HYK_JF_DHBL primary key (HYKTYPE)
)
/

/*==============================================================*/
/* Table: HYK_JGDJ                                              */
/*==============================================================*/
create table BFCRM10.HYK_JGDJ  (
   JGID                 NUMBER(5)                       not null,
   JGMC                 VARCHAR2(20)                    not null,
   constraint PK_HYK_JGDJ primary key (JGID)
)
/

/*==============================================================*/
/* Table: HYK_JGDJBDJL                                          */
/*==============================================================*/
create table BFCRM10.HYK_JGDJBDJL  (
   JLBH                 NUMBER(10)                      not null,
   RQ                   DATE                            not null,
   HYKTYPE              NUMBER(5)                       not null,
   JGID                 NUMBER(5)                       not null,
   FS                   NUMBER(5)                       not null,
   QDMTXFCS             NUMBER(10)                     default 0 not null,
   ZZMTXFCS             NUMBER(10)                     default 0 not null,
   QDMTTHCS             NUMBER(10)                     default 0 not null,
   ZZMTTHCS             NUMBER(10)                     default 0 not null,
   constraint PK_HYK_JGDJBDJL primary key (JLBH)
)
/

/*==============================================================*/
/* Index: "Idx_HYK_JGDJBDJL_RQ"                                 */
/*==============================================================*/
create index BFCRM10."Idx_HYK_JGDJBDJL_RQ" on BFCRM10.HYK_JGDJBDJL (
   RQ ASC
)
/

/*==============================================================*/
/* Index: "Idx_HYK_JGDJBDJL_TYPE"                               */
/*==============================================================*/
create index BFCRM10."Idx_HYK_JGDJBDJL_TYPE" on BFCRM10.HYK_JGDJBDJL (
   HYKTYPE ASC
)
/

/*==============================================================*/
/* Index: "Idx_HYK_JGDJBDJL_JGID"                               */
/*==============================================================*/
create index BFCRM10."Idx_HYK_JGDJBDJL_JGID" on BFCRM10.HYK_JGDJBDJL (
   JGID ASC
)
/

/*==============================================================*/
/* Table: HYK_JGDJBDJLITEM                                      */
/*==============================================================*/
create table BFCRM10.HYK_JGDJBDJLITEM  (
   JLBH                 NUMBER(10)                      not null,
   HYID                 NUMBER(10)                      not null,
   JGID_OLD             NUMBER(5)                       not null,
   JGID_NEW             NUMBER(5)                       not null,
   constraint PK_HYK_JGDJBDJLITEM primary key (JLBH, HYID)
)
/

/*==============================================================*/
/* Table: HYK_JGDJGZ                                            */
/*==============================================================*/
create table BFCRM10.HYK_JGDJGZ  (
   HYKTYPE              NUMBER(5)                       not null,
   JGID                 NUMBER(5)                       not null,
   FS                   NUMBER(5)                       not null,
   QDMTXFCS             NUMBER(10)                     default 0 not null,
   ZZMTXFCS             NUMBER(10)                     default 0 not null,
   QDMTTHCS             NUMBER(10)                     default 0 not null,
   ZZMTTHCS             NUMBER(10)                     default 0 not null,
   constraint PK_HYK_JGDJGZ primary key (HYKTYPE, JGID)
)
/

/*==============================================================*/
/* Table: HYK_JYCL                                              */
/*==============================================================*/
create table BFCRM10.HYK_JYCL  (
   JYID                 NUMBER(10)                      not null,
   XFJLID               NUMBER(10),
   MDID                 NUMBER(10)                      not null,
   SKTNO                VARCHAR2(6)                     not null,
   JLBH                 NUMBER(10)                      not null,
   SKYDM                VARCHAR2(8),
   JZRQ                 DATE,
   JYLX                 NUMBER(5)                       not null,
   JYJE                 NUMBER(14,2)                    not null,
   HYID_TO              NUMBER(10),
   ZBSJ                 DATE                            not null,
   TJSJ                 DATE,
   QXSJ                 DATE,
   JYZT                 NUMBER(5)                       not null,
   BJ_YHQ               NUMBER(5)                      default 0 not null,
   BJ_CZK               NUMBER(5)                      default 0 not null,
   CXID                 NUMBER(10),
   YJYID                NUMBER(10)                     default 0,
   XFQYID               NUMBER(10),
   constraint PK_HYK_JYCL primary key (JYID)
)
/

/*==============================================================*/
/* Index: HYK_JYCL_XFJLID                                       */
/*==============================================================*/
create index BFCRM10.HYK_JYCL_XFJLID on BFCRM10.HYK_JYCL (
   XFJLID ASC
)
/

/*==============================================================*/
/* Index: HYK_JYCL_MX                                           */
/*==============================================================*/
create index BFCRM10.HYK_JYCL_MX on BFCRM10.HYK_JYCL (
   XFJLID ASC,
   JYZT ASC,
   BJ_CZK ASC
)
/

/*==============================================================*/
/* Index: HYK_JYCL_JLBH                                         */
/*==============================================================*/
create index HYK_JYCL_JLBH on BFCRM10.HYK_JYCL (
   MDID ASC,
   SKTNO ASC,
   JLBH ASC
)
/

/*==============================================================*/
/* Table: HYK_JYCLITEM_CZK                                      */
/*==============================================================*/
create table BFCRM10.HYK_JYCLITEM_CZK  (
   JYID                 NUMBER(10)                      not null,
   HYID                 NUMBER(10)                      not null,
   JE                   NUMBER(14,2)                    not null,
   BJ_HSK               NUMBER(5)                      default 0 not null,
   constraint PK_HYK_JYCLITEM_CZK primary key (JYID, HYID)
)
/

/*==============================================================*/
/* Table: HYK_JYCLITEM_YHQ                                      */
/*==============================================================*/
create table BFCRM10.HYK_JYCLITEM_YHQ  (
   JYID                 NUMBER(10)                      not null,
   HYID                 NUMBER(10)                      not null,
   YHQID                NUMBER(5)                       not null,
   CXID                 NUMBER(10)                     default 0 not null,
   JSRQ                 DATE                            not null,
   JE                   NUMBER(14,2)                    not null,
   MDFWDM               VARCHAR2(10)                    not null,
   JSRQ_FQ              DATE,
   DHJF                 NUMBER(16,4)                   default 0,
   JF                   NUMBER(16,4)                   default 0,
   DHJE                 NUMBER(14,2),
   constraint PK_HYK_JYCLITEM_YHQ primary key (JYID, HYID, YHQID, CXID, JSRQ)
)
/

/*==============================================================*/
/* Index: YHQJY_HYID                                            */
/*==============================================================*/
create index BFCRM10.YHQJY_HYID on BFCRM10.HYK_JYCLITEM_YHQ (
   HYID ASC
)
/

/*==============================================================*/
/* Table: HYK_JYCLITEM_YHQDM                                    */
/*==============================================================*/
create table BFCRM10.HYK_JYCLITEM_YHQDM  (
   JYID                 NUMBER(10)                      not null,
   YHQCODE              VARCHAR2(20)                    not null,
   YHQID                NUMBER(5)                       not null,
   YHQMZ                NUMBER(14,2)                   default 0 not null,
   CXID                 NUMBER(10),
   constraint PK_HYK_JYCLITEM_YHQDM primary key (JYID, YHQCODE)
)
/

/*==============================================================*/
/* Table: HYK_JYCL_ZBZT                                         */
/*==============================================================*/
create table BFCRM10.HYK_JYCL_ZBZT  (
   JYID                 NUMBER(10)                      not null,
   ZBSJ                 DATE                            not null,
   constraint PK_HYK_JYCL_ZBZT primary key (JYID)
)
/

/*==============================================================*/
/* Table: HYK_KCCZKBGZ                                          */
/*==============================================================*/
create sequence BFCRM10.S_HYK_KCCZKBGZ
/
create table BFCRM10.HYK_KCCZKBGZ  (
   RQ                   DATE                            not null,
   BGDDDM               VARCHAR2(20)                    not null,
   HYKTYPE              NUMBER(5)                       not null,
   MZJE                 NUMBER(14,2)                   default 0 not null,
   QCSL                 NUMBER(10)                     default 0,
   QCJE                 NUMBER(14,2)                   default 0,
   JKSL                 NUMBER(10)                     default 0,
   JKJE                 NUMBER(14,2)                   default 0,
   XKSL                 NUMBER(10)                     default 0,
   XKJE                 NUMBER(14,2)                   default 0,
   BRSL                 NUMBER(10)                     default 0,
   BRJE                 NUMBER(14,2)                   default 0,
   BCJE                 NUMBER(14,2)                   default 0,
   BCSL                 NUMBER(10)                     default 0,
   HKSL                 NUMBER(10)                     default 0,
   HKJE                 NUMBER(14,2)                   default 0,
   TZSL                 NUMBER(10)                     default 0,
   TZJE                 NUMBER(14,2)                   default 0,
   FSSL                 NUMBER(10)                     default 0,
   FSJE                 NUMBER(14,2)                   default 0,
   FSTSSL               NUMBER(10)                     default 0,
   FSTSJE               NUMBER(14,2)                   default 0,
   XFTSSL               NUMBER(10)                     default 0,
   XFTSJE               NUMBER(14,2)                   default 0,
   ZFSL                 NUMBER(10)                     default 0,
   ZFJE                 NUMBER(14,2)                   default 0,
   JCSL                 NUMBER(10)                     default 0,
   JCJE                 NUMBER(14,2)                   default 0,
   YEARMONTH            NUMBER(10),
   TM                   NUMBER(10),
   constraint PK_HYK_KCCZKBGZ primary key (RQ, BGDDDM, HYKTYPE, MZJE)
)
/

/*==============================================================*/
/* Table: HYK_KCKZFJL                                           */
/*==============================================================*/
create table BFCRM10.HYK_KCKZFJL  (
   JLBH                 NUMBER(10)                      not null,
   BGDDDM               VARCHAR2(20)                    not null,
   HYKTYPE              NUMBER(5),
   ZFKSL                NUMBER(10)                     default 0 not null,
   ZFKJE                NUMBER(14,2)                   default 0 not null,
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16)                    not null,
   DJSJ                 DATE                            not null,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(16),
   ZXRQ                 DATE,
   ZFKYY                VARCHAR2(40),
   BJ_HF                NUMBER(5)                      default 0,
   MDID                 NUMBER(10),
   constraint PK_HYK_KCKZFJL primary key (JLBH)
)
/

/*==============================================================*/
/* Table: HYK_KCKZFJLITEM                                       */
/*==============================================================*/
create table BFCRM10.HYK_KCKZFJLITEM  (
   JLBH                 NUMBER(10)                      not null,
   CZKHM                VARCHAR2(16)                    not null,
   QCYE                 NUMBER(14,2)                   default 0 not null,
   constraint PK_HYK_KCKZFJLITEM primary key (JLBH, CZKHM)
)
/

/*==============================================================*/
/* Table: HYK_KFDYJL                                            */
/*==============================================================*/
create table BFCRM10.HYK_KFDYJL  (
   JLBH                 NUMBER(10)                      not null,
   KFRYID               NUMBER(10)                      not null,
   BZ                   varchar2(30),
   DJR                  NUMBER(10)                      not null,
   DJRMC                varchar2(30)                    not null,
   DJSJ                 DATE                            not null,
   ZXR                  NUMBER(10),
   ZXRMC                varchar2(16),
   ZXRQ                 DATE,
   constraint PK_HYK_KFDYJL primary key (JLBH)
)
/

/*==============================================================*/
/* Table: HYK_KFDYJLITEM                                        */
/*==============================================================*/
create table BFCRM10.HYK_KFDYJLITEM  (
   JLBH                 NUMBER(10)                      not null,
   HYID                 NUMBER(10)                      not null,
   constraint PK_HYK_KFDYJLITEM primary key (JLBH, HYID)
)
/

/*==============================================================*/
/* Table: HYK_KFZDEF                                            */
/*==============================================================*/
create table BFCRM10.HYK_KFZDEF  (
   KFZID                NUMBER(10)                      not null,
   KFZMC                varchar2(30)                    not null,
   RYID                 NUMBER(10)                      not null,
   BZ                   varchar2(50),
   constraint PK_HYK_KFZDEF primary key (KFZID)
)
/

/*==============================================================*/
/* Table: HYK_KFZDEFITEM                                        */
/*==============================================================*/
create table BFCRM10.HYK_KFZDEFITEM  (
   KFZID                NUMBER(10)                      not null,
   RYID                 NUMBER(10)                      not null,
   BJ_LZ                NUMBER(10)                     default 0 not null,
   constraint PK_HYK_KFZDEFITEM primary key (KFZID, RYID)
)
/

/*==============================================================*/
/* Table: HYK_KHQTDEF                                           */
/*==============================================================*/
create table BFCRM10.HYK_KHQTDEF  (
   QTBH                 NUMBER(10)                      not null,
   QTMC                 varchar2(30)                    not null,
   QTLXID               NUMBER(10)                      not null,
   RYID                 NUMBER(10)                      not null,
   QTRS                 NUMBER(10)                      not null,
   CJR                  NUMBER(10)                      not null,
   CJRMC                varchar2(16)                    not null,
   CJSJ                 DATE                            not null,
   BZ                   varchar2(50),
   BJ_TY                NUMBER(10)                      not null,
   constraint PK_HYK_KHQTDEF primary key (QTBH)
)
/

/*==============================================================*/
/* Table: HYK_KHQTDEFITEM                                       */
/*==============================================================*/
create table BFCRM10.HYK_KHQTDEFITEM  (
   QTBH                 NUMBER(10)                      not null,
   HYID                 NUMBER(10)                      not null,
   constraint PK_HYK_KHQTDEFITEM primary key (QTBH, HYID)
)
/

/*==============================================================*/
/* Table: HYK_KHQTLXDEF                                         */
/*==============================================================*/
create table BFCRM10.HYK_KHQTLXDEF  (
   LXID                 NUMBER(10)                      not null,
   LXMC                 varchar2(30)                    not null,
   BZ                   varchar2(50),
   constraint PK_HYK_KHQTLXDEF primary key (LXID)
)
/

/*==============================================================*/
/* Table: HYK_LDZQHDDEF                                         */
/*==============================================================*/
create table BFCRM10.HYK_LDZQHDDEF  (
   JLBH                 NUMBER(10)                      not null,
   CXZT                 VARCHAR2(50)                    not null,
   CXNR                 VARCHAR2(200),
   constraint PK_HYK_LDZQHDDEF primary key (JLBH)
)
/

/*==============================================================*/
/* Table: HYK_LDZQJL                                            */
/*==============================================================*/
create table BFCRM10.HYK_LDZQJL  (
   JLBH                 NUMBER(10)                      not null,
   HYID                 NUMBER(10),
   YHQID                NUMBER(10),
   JSRQ                 DATE,
   MDFWDM               VARCHAR2(10),
   YJE                  NUMBER(14,2)                    not null,
   CKJE                 NUMBER(14,2)                    not null,
   CZDD                 VARCHAR2(10)                    not null,
   ZY                   VARCHAR2(40),
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16)                    not null,
   DJSJ                 DATE                            not null,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(16),
   ZXRQ                 DATE,
   MDID                 NUMBER(10)                      not null,
   HYKNO                VARCHAR2(30),
   BJ_CHILD             NUMBER(5)                      default 0,
   JLBH_GZ              NUMBER(10),
   CXHDBH               NUMBER(10)                      not null,
   JLBH_CK              NUMBER(10)                      not null,
   constraint PK_HYK_LDZQJL primary key (JLBH)
)
/

/*==============================================================*/
/* Index: HYK_LDZQJL_HYID                                       */
/*==============================================================*/
create index BFCRM10.HYK_LDZQJL_HYID on BFCRM10.HYK_LDZQJL (
   HYID ASC
)
/

/*==============================================================*/
/* Table: HYK_LMKDRJL                                           */
/*==============================================================*/
create table BFCRM10.HYK_LMKDRJL  (
   JLBH                 NUMBER(10)                      not null,
   DJR                  NUMBER(10)                      not null,
   DJRMC                varchar2(16)                    not null,
   DJSJ                 DATE                            not null,
   ZXR                  NUMBER(10),
   ZXRMC                varchar2(16),
   ZXRQ                 DATE,
   STATUS               NUMBER(10),
   ZY                   varchar2(30),
   constraint PK_HYK_LMKDRJL primary key (JLBH)
)
/

/*==============================================================*/
/* Table: HYK_LMKDRJLITEM                                       */
/*==============================================================*/
create table BFCRM10.HYK_LMKDRJLITEM  (
   JLBH                 NUMBER(10)                      not null,
   HYK_NO               varchar2(30)                    not null,
   HYKTYPE              NUMBER(5)                       not null,
   HY_NAME              varchar2(20),
   SEX                  NUMBER(5),
   ZJHM                 varchar2(30),
   LXDH                 varchar2(15),
   TXDZ                 varchar2(50),
   BZ                   NUMBER(5),
   HYID                 NUMBER(10),
   BGDD                 varchar2(10)                    not null,
   JKRQ                 DATE                            not null,
   YXQ                  DATE                            not null,
   ZXR                  NUMBER(10),
   ZXRMC                varchar2(16),
   ZXRQ                 DATE,
   STATUS               NUMBER(5),
   BJ_BK                NUMBER(5)                      default 0,
   XLID                 NUMBER(5),
   ZYID                 NUMBER(5),
   SJHM                 varchar2(20),
   CLLX                 NUMBER(5),
   FXDW                 NUMBER(10),
   constraint PK_HYK_LMKDRJLITEM primary key (JLBH, HYK_NO, YXQ)
)
/

/*==============================================================*/
/* Index: HYK_LMKDRJL_HYID                                      */
/*==============================================================*/
create index HYK_LMKDRJL_HYID on BFCRM10.HYK_LMKDRJLITEM (
   HYID ASC
)
/

/*==============================================================*/
/* Index: HYK_LMKDRJL_HYK_NO                                    */
/*==============================================================*/
create index HYK_LMKDRJL_HYK_NO on BFCRM10.HYK_LMKDRJLITEM (
   HYK_NO ASC
)
/

/*==============================================================*/
/* Index: HYK_LMKDRJLITEM_ZJHM                                  */
/*==============================================================*/
create index HYK_LMKDRJLITEM_ZJHM on BFCRM10.HYK_LMKDRJLITEM (
   ZJHM ASC
)
/

/*==============================================================*/
/* Table: HYK_LPFFGZ                                            */
/*==============================================================*/
create table BFCRM10.HYK_LPFFGZ  (
   JLBH                 NUMBER(10)                      not null,
   GZMC                 VARCHAR2(30),
   GZLX                 NUMBER(10),
   KSRQ                 DATE,
   JSRQ                 DATE,
   HYKTYPE              NUMBER(10),
   BJ_DC                NUMBER(5),
   BJ_SR                NUMBER(5),
   BJ_LJ                NUMBER(5),
   BJ_BK                NUMBER(5),
   BJ_SL                NUMBER(5),
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(30)                    not null,
   DJSJ                 DATE                            not null,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(30),
   ZXRQ                 DATE,
   CXHDBH               NUMBER(10),
   BJ_TY                NUMBER(5),
   YHQID                NUMBER(10),
   CXID                 NUMBER(10),
   ZZR                  NUMBER(10),
   ZZRMC                VARCHAR2(30),
   ZZSJ                 DATE,
   constraint PK_HYK_LPFFGZ primary key (JLBH)
)
/

/*==============================================================*/
/* Table: HYK_LPFFGZ_LP                                         */
/*==============================================================*/
create table BFCRM10.HYK_LPFFGZ_LP  (
   JLBH                 NUMBER(10)                      not null,
   LPGRP                NUMBER(10)                      not null,
   LPID                 NUMBER(10)                      not null,
   LPJF                 FLOAT(50),
   LPJE                 NUMBER(14,2),
   constraint PK_HYK_LPFFGZ_LP primary key (JLBH, LPGRP, LPID)
)
/

/*==============================================================*/
/* Table: HYK_LPFFGZ_MD                                         */
/*==============================================================*/
create table BFCRM10.HYK_LPFFGZ_MD  (
   JLBH                 NUMBER(10)                      not null,
   MDID                 NUMBER(10)                      not null,
   constraint PK_HYK_LPFFGZ_MD primary key (JLBH, MDID)
)
/

/*==============================================================*/
/* Table: HYK_LPFFGZ_XFJE                                       */
/*==============================================================*/
create table BFCRM10.HYK_LPFFGZ_XFJE  (
   JLBH                 NUMBER(10)                      not null,
   XX                   NUMBER(14,2)                    not null,
   SX                   NUMBER(14,2),
   LPGRP                NUMBER(10),
   constraint PK_HYK_LPFFGZ_XFJE primary key (JLBH, XX)
)
/

/*==============================================================*/
/* Table: HYK_LPKCBDJL                                          */
/*==============================================================*/
create table BFCRM10.HYK_LPKCBDJL  (
   JYBH                 NUMBER(10)                      not null,
   LPID                 NUMBER(10),
   BGDDDM               VARCHAR2(20),
   CLSJ                 DATE,
   CLLX                 NUMBER(5),
   JLBH                 NUMBER(10),
   BDSL                 NUMBER(16,4),
   KCSL                 NUMBER(16,4),
   ZY                   VARCHAR2(20),
   CRMJZRQ              DATE,
   CZYDM                VARCHAR2(20),
   CZYMC                VARCHAR2(30),
   constraint PK_HYK_LPKCBDJL primary key (JYBH)
)
/

/*==============================================================*/
/* Index: INXLPIDBGDD                                           */
/*==============================================================*/
create index BFCRM10.INXLPIDBGDD on BFCRM10.HYK_LPKCBDJL (
   LPID ASC,
   BGDDDM ASC
)
/

/*==============================================================*/
/* Index: INXCRMJZRQ                                            */
/*==============================================================*/
create index BFCRM10.INXCRMJZRQ on BFCRM10.HYK_LPKCBDJL (
   CRMJZRQ ASC
)
/

/*==============================================================*/
/* Table: HYK_LPLQJL                                            */
/*==============================================================*/
create table BFCRM10.HYK_LPLQJL  (
   JLBH                 NUMBER(10)                      not null,
   BGDDDM_BC            VARCHAR2(10)                    not null,
   BGDDDM_BR            VARCHAR2(10)                    not null,
   ZSL                  FLOAT(50),
   ZJE                  NUMBER(14,2),
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16)                    not null,
   DJSJ                 DATE                            not null,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(16),
   ZXRQ                 DATE,
   LQR                  NUMBER(10),
   LQRMC                VARCHAR2(16),
   LQSJ                 DATE,
   STATUS               NUMBER(5)                       not null,
   BZ                   VARCHAR2(30),
   constraint PK_HYK_LPLQJL primary key (JLBH)
)
/

/*==============================================================*/
/* Table: HYK_LPLQJLITEM                                        */
/*==============================================================*/
create table BFCRM10.HYK_LPLQJLITEM  (
   JLBH                 NUMBER(10)                      not null,
   LPID                 NUMBER(10)                      not null,
   LQSL                 NUMBER(14,4)                   default 0,
   LPDJ                 NUMBER(14,2),
   LPDM                 VARCHAR2(10),
   constraint PK_HYK_LPLQJLITEM primary key (JLBH, LPID)
)
/

/*==============================================================*/
/* Table: HYK_LPLQJL_SQDMX                                      */
/*==============================================================*/
create table BFCRM10.HYK_LPLQJL_SQDMX  (
   JLBH                 NUMBER(10)                      not null,
   SQDJH                NUMBER(10)                      not null,
   constraint PK_HYK_LPLQJL_SQDMX primary key (JLBH, SQDJH)
)
/

/*==============================================================*/
/* Table: HYK_LPPDJL                                            */
/*==============================================================*/
create table BFCRM10.HYK_LPPDJL  (
   JLBH                 NUMBER(10)                      not null,
   BGDDDM               VARCHAR2(20)                    not null,
   BZ                   VARCHAR2(30),
   PDRQ                 DATE                            not null,
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16)                    not null,
   DJSJ                 DATE                            not null,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(16),
   ZXRQ                 DATE,
   constraint PK_HYK_LPPDJL primary key (JLBH)
)
/

/*==============================================================*/
/* Table: HYK_LPPDJLITEM                                        */
/*==============================================================*/
create table BFCRM10.HYK_LPPDJLITEM  (
   JLBH                 NUMBER(10)                      not null,
   LPID                 NUMBER(10)                      not null,
   LPDM                 VARCHAR2(6),
   LPMC                 VARCHAR2(30),
   LPDJ                 NUMBER(14,2),
   LPJF                 NUMBER(14,4),
   PDKC                 NUMBER(14,4)                   default 0 not null,
   PKSL                 NUMBER(14,4)                    not null,
   LPKC                 NUMBER(14,4)                    not null,
   constraint PK_HYK_LPPDJLITEM primary key (JLBH, LPID)
)
/

/*==============================================================*/
/* Table: HYK_MDJF                                              */
/*==============================================================*/
create sequence BFCRM10.S_HYK_MDJF
/
create table BFCRM10.HYK_MDJF  (
   HYID                 NUMBER(10)                      not null,
   MDID                 NUMBER(10)                      not null,
   WCLJF                NUMBER(16,4)                   default 0,
   WCLXFJE              NUMBER(14,2),
   BQJF                 NUMBER(16,4)                   default 0,
   XFJE                 NUMBER(14,2)                    not null,
   BNLJJF               NUMBER(16,4),
   BNXFJE               NUMBER(14,2),
   ZKJE                 NUMBER(14,2)                   default 0,
   LJJF                 NUMBER(16,4)                   default 0,
   LJXFJE               NUMBER(14,2)                   default 0,
   LJZKJE               NUMBER(14,2)                   default 0,
   XFCS                 NUMBER(10)                     default 0,
   THCS                 NUMBER(10)                     default 0,
   TM                   NUMBER(10),
   LJXFJE_JF            NUMBER(14,2),
   JCJF                 NUMBER(16,4)                   default 0,
   WCLJF_TS             NUMBER(16,4),
   constraint PK_HYK_MDJF primary key (HYID, MDID)
)
/

/*==============================================================*/
/* Table: HYK_MDJFYEAR                                          */
/*==============================================================*/
create sequence BFCRM10.S_HYK_MDJFYEAR
/
create table BFCRM10.HYK_MDJFYEAR  (
   ND                   NUMBER(10)                      not null,
   HYID                 NUMBER(10)                      not null,
   MDID                 NUMBER(10)                      not null,
   WCLJF                FLOAT(50)                      default 0,
   XFJE                 FLOAT(50)                       not null,
   ZKJE                 NUMBER(14,2)                   default 0,
   BQJF                 FLOAT(50)                      default 0,
   LJJF                 FLOAT(50)                      default 0,
   LJXFJE               NUMBER(14,2)                   default 0,
   LJZKJE               NUMBER(14,2)                   default 0,
   XFCS                 NUMBER(10)                     default 0,
   THCS                 NUMBER(10)                     default 0,
   TM                   NUMBER(10),
   BNLJJF               FLOAT(50),
   constraint PK_HYK_MDJFYEAR primary key (ND, HYID, MDID)
)
/

/*==============================================================*/
/* Table: HYK_MDZKFS                                            */
/*==============================================================*/
create table BFCRM10.HYK_MDZKFS  (
   HYKTYPE              NUMBER(10)                      not null,
   MDID                 NUMBER(10)                      not null,
   ZKFS                 NUMBER(10),
   constraint PK_HYK_MDZKFS primary key (HYKTYPE, MDID)
)
/

/*==============================================================*/
/* Table: HYK_MMXGJL                                            */
/*==============================================================*/
create table BFCRM10.HYK_MMXGJL  (
   JLBH                 NUMBER(10)                      not null,
   HYID                 NUMBER(10)                      not null,
   PSW_OLD              VARCHAR2(30),
   PSW_NEW              VARCHAR2(30),
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16)                    not null,
   DJSJ                 DATE                            not null,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(16),
   ZXRQ                 DATE,
   TYPE                 NUMBER(10),
   BGDDDM               VARCHAR2(20),
   constraint PK_HYK_MMXGJL primary key (JLBH)
)
/

/*==============================================================*/
/* Table: HYK_NFJL                                              */
/*==============================================================*/
create table BFCRM10.HYK_NFJL  (
   JLBH                 NUMBER(10)                      not null,
   CZDD                 VARCHAR2(20)                    not null,
   MDID                 NUMBER(10),
   SKTNO                VARCHAR2(20),
   XPH                  NUMBER(10),
   FXDWID               NUMBER(10),
   HYID                 NUMBER(10)                      not null,
   HYKTYPE              NUMBER(10)                      not null,
   YWYID                NUMBER(10),
   NF                   NUMBER(14,2),
   YYXQ                 DATE,
   XYXQ                 DATE,
   DJLX                 NUMBER(10),
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(10)                    not null,
   DJSJ                 DATE                            not null,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(16),
   ZXRQ                 DATE,
   BZ                   VARCHAR2(50),
   constraint PK_HYK_NFJL primary key (JLBH)
)
/

/*==============================================================*/
/* Table: HYK_NFJL_ZFFS                                         */
/*==============================================================*/
create table BFCRM10.HYK_NFJL_ZFFS  (
   JLBH                 NUMBER(10)                      not null,
   ZFFSID               NUMBER(10)                      not null,
   JE                   NUMBER(14,2)                    not null,
   BZ                   VARCHAR2(20),
   constraint PK_HYK_NFJL_ZFFS primary key (JLBH, ZFFSID)
)
/

/*==============================================================*/
/* Table: HYK_QTJFDHJL                                          */
/*==============================================================*/
create table BFCRM10.HYK_QTJFDHJL  (
   JLBH                 NUMBER(10)                      not null,
   HYID                 NUMBER(10),
   JYID                 NUMBER(10),
   DHJE                 NUMBER(14,2),
   YHQID                NUMBER(10),
   WCLJFBD              FLOAT,
   CLSJ                 DATE,
   MDID                 NUMBER(10),
   SKTNO                VARCHAR(6),
   XPH                  NUMBER(10),
   ZY                   VARCHAR(50),
   constraint PK_HYK_QTJFDHJL primary key (JLBH)
)
/

/*==============================================================*/
/* Table: HYK_QTJFDHJL_ITEM                                     */
/*==============================================================*/
create table BFCRM10.HYK_QTJFDHJL_ITEM  (
   JLBH                 NUMBER(10)                      not null,
   MDID                 NUMBER(10)                      not null,
   DHJF                 FLOAT,
   constraint PK_HYK_QTJFDHJL_ITEM primary key (JLBH, MDID)
)
/

/*==============================================================*/
/* Table: HYK_QZCXHD                                            */
/*==============================================================*/
create table BFCRM10.HYK_QZCXHD  (
   JLBH                 NUMBER(10)                      not null,
   CXID                 NUMBER(10)                      not null,
   MDID                 NUMBER(10),
   HDZT                 VARCHAR2(20),
   KSRQ                 DATE,
   JSRQ                 DATE,
   DJR                  NUMBER(10),
   DJRMC                VARCHAR2(20),
   DJSJ                 DATE,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(10),
   ZXRQ                 DATE,
   STATUS               NUMBER(5),
   ZZR                  NUMBER(10),
   ZZRMC                VARCHAR2(20),
   ZZRQ                 DATE,
   constraint PK_HYK_QZCXHD primary key (JLBH)
)
/

/*==============================================================*/
/* Table: HYK_QZCXHD_ITEM                                       */
/*==============================================================*/
create table BFCRM10.HYK_QZCXHD_ITEM  (
   JLBH                 NUMBER(10)                      not null,
   QZLXID               NUMBER(10)                      not null,
   JFBL                 FLOAT,
   ZFBS                 FLOAT,
   constraint PK_HYK_QZCXHD_ITEM primary key (JLBH, QZLXID)
)
/

/*==============================================================*/
/* Table: HYK_QZLXDEF                                           */
/*==============================================================*/
create table BFCRM10.HYK_QZLXDEF  (
   JLBH                 number(10)                      not null,
   QZLXMC               varchar(20),
   QZCYRS               number(10),
   STATUS               NUMBER(5),
   constraint PK_HYK_QZLXDEF primary key (JLBH)
)
/

/*==============================================================*/
/* Table: HYK_QZXX                                              */
/*==============================================================*/
create table BFCRM10.HYK_QZXX  (
   JLBH                 NUMBER(10)                      not null,
   QZMC                 VARCHAR2(20),
   QZLXID               NUMBER(10),
   QZCYRS               NUMBER(5),
   DJR                  Number(10),
   DJRMC                VARCHAR2(20),
   DJSJ                 DATE,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(20),
   ZXRQ                 DATE,
   MDID                 NUMBER(10),
   constraint PK_HYK_QZXX primary key (JLBH)
)
/

/*==============================================================*/
/* Table: HYK_QZXX_BMPLITEM                                     */
/*==============================================================*/
create table BFCRM10.HYK_QZXX_BMPLITEM  (
   JLBH                 NUMBER(10)                      not null,
   HYID                 NUMBER(10)                      not null,
   SJLX                 NUMBER(5)                       not null,
   SJNR                 NUMBER(20)                      not null,
   constraint PK_HYK_QZXX_BMPLITEM primary key (JLBH, HYID, SJLX, SJNR)
)
/

/*==============================================================*/
/* Table: HYK_QZXX_CYXXITEM                                     */
/*==============================================================*/
create table BFCRM10.HYK_QZXX_CYXXITEM  (
   JLBH                 NUMBER(10)                      not null,
   HYID                 NUMBER(10)                      not null,
   constraint PK_HYK_QZXX_CYXXITEM primary key (JLBH, HYID)
)
/

/*==============================================================*/
/* Table: HYK_SBTJLSJL                                          */
/*==============================================================*/
create table BFCRM10.HYK_SBTJLSJL  (
   JLBH                 NUMBER(10)                      not null,
   HYKTYPE              NUMBER(5)                       not null,
   SHDM                 VARCHAR2(4)                     not null,
   DRXFJE               NUMBER(14,2)                    not null,
   LJXFJE               NUMBER(14,2)                    not null,
   LJSJ                 NUMBER(5)                       not null,
   XGR                  NUMBER(10)                      not null,
   XGRMC                VARCHAR2(16)                    not null,
   XGSJ                 DATE                            not null,
   STATUS               NUMBER(5),
   BJ_SBMD              NUMBER(5),
   constraint PK_HYK_SBTJLSJL primary key (JLBH)
)
/

/*==============================================================*/
/* Table: HYK_SJJL                                              */
/*==============================================================*/
create table BFCRM10.HYK_SJJL  (
   JLBH                 NUMBER(10)                      not null,
   HYID                 NUMBER(10)                      not null,
   HYKTYPE_OLD          NUMBER(5)                       not null,
   HYKTYPE_NEW          NUMBER(5)                       not null,
   HYKHM_OLD            VARCHAR2(30)                    not null,
   HYKHM_NEW            VARCHAR2(30)                    not null,
   WCLJF_OLD            FLOAT(50),
   BQJF_OLD             FLOAT(50),
   SJJF                 FLOAT(50),
   ZY                   VARCHAR2(50),
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16)                    not null,
   DJSJ                 DATE                            not null,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(16),
   ZXRQ                 DATE,
   BGDDDM               VARCHAR2(20),
   BJ_SJ                NUMBER(5),
   YJKRQ                DATE,
   JE                   NUMBER(14,2)                   default 0,
   CZDD                 VARCHAR2(10),
   BJ_XFJE              NUMBER(5),
   BQXFJE_OLD           NUMBER(14,2),
   constraint PK_HYK_SJJL primary key (JLBH)
)
/

/*==============================================================*/
/* Index: INX_HYK_SJJL_ZXRQ                                     */
/*==============================================================*/
create index BFCRM10.INX_HYK_SJJL_ZXRQ on BFCRM10.HYK_SJJL (
   ZXRQ ASC
)
/

/*==============================================================*/
/* Table: HYK_SJJL_YHQZH                                        */
/*==============================================================*/
create table BFCRM10.HYK_SJJL_YHQZH  (
   JLBH                 NUMBER(10)                      not null,
   HYID                 NUMBER(10)                      not null,
   YHQID                NUMBER(5)                       not null,
   MDFWDM               VARCHAR2(10)                    not null,
   TZJE                 NUMBER(14,2)                   default 0,
   constraint PK_HYK_SJJL_YHQZH primary key (JLBH, YHQID, MDFWDM, HYID)
)
/

/*==============================================================*/
/* Index: HYK_SJJL_YHQZH_HYID                                   */
/*==============================================================*/
create index BFCRM10.HYK_SJJL_YHQZH_HYID on BFCRM10.HYK_SJJL_YHQZH (
   HYID ASC
)
/

/*==============================================================*/
/* Table: HYK_SMZTBDJL                                          */
/*==============================================================*/
create table BFCRM10.HYK_SMZTBDJL  (
   JLBH                 NUMBER(10)                      not null,
   RQ                   DATE                            not null,
   HYKTYPE              NUMBER(5)                       not null,
   FS                   NUMBER(5)                       not null,
   QDZJXFTS             NUMBER(5)                      default 0 not null,
   ZZZJXFTS             NUMBER(5)                      default 0 not null,
   QDLJXFJE             NUMBER(14,2)                   default 0 not null,
   ZZLJXFJE             NUMBER(14,2)                   default 0 not null,
   QDCZYE               NUMBER(14,2)                   default 0 not null,
   ZZCZYE               NUMBER(14,2)                   default 0 not null,
   constraint PK_HYK_SMZTBDJL primary key (JLBH)
)
/

/*==============================================================*/
/* Index: "Idx_HYK_SMZTBDJL_TYPE"                               */
/*==============================================================*/
create index BFCRM10."Idx_HYK_SMZTBDJL_TYPE" on BFCRM10.HYK_SMZTBDJL (
   HYKTYPE ASC
)
/

/*==============================================================*/
/* Index: "Idx_HYK_SMZTBDJL_RQ"                                 */
/*==============================================================*/
create index BFCRM10."Idx_HYK_SMZTBDJL_RQ" on BFCRM10.HYK_SMZTBDJL (
   RQ ASC
)
/

/*==============================================================*/
/* Table: HYK_SMZTBDJLITEM                                      */
/*==============================================================*/
create table BFCRM10.HYK_SMZTBDJLITEM  (
   JLBH                 NUMBER(10)                      not null,
   HYID                 NUMBER(10)                      not null,
   STATUS_OLD           NUMBER(5)                       not null,
   constraint PK_HYK_SMZTBDJLITEM primary key (JLBH, HYID)
)
/

/*==============================================================*/
/* Index: "Idx_HYK_SMZTBDJLITEM_HYID"                           */
/*==============================================================*/
create index BFCRM10."Idx_HYK_SMZTBDJLITEM_HYID" on BFCRM10.HYK_SMZTBDJLITEM (
   HYID ASC
)
/

/*==============================================================*/
/* Table: HYK_SMZTGZ                                            */
/*==============================================================*/
create table BFCRM10.HYK_SMZTGZ  (
   HYKTYPE              NUMBER(5)                       not null,
   FS                   NUMBER(5)                       not null,
   QDZJXFTS             NUMBER(5)                      default 0 not null,
   ZZZJXFTS             NUMBER(5)                      default 0 not null,
   QDLJXFJE             NUMBER(14,2)                   default 0 not null,
   ZZLJXFJE             NUMBER(14,2)                   default 0 not null,
   QDCZYE               NUMBER(14,2)                   default 0 not null,
   ZZCZYE               NUMBER(14,2)                   default 0 not null,
   constraint PK_HYK_SMZTGZ primary key (HYKTYPE)
)
/

/*==============================================================*/
/* Table: HYK_SQGZLSJL                                          */
/*==============================================================*/
create table BFCRM10.HYK_SQGZLSJL  (
   JLBH                 NUMBER(10)                      not null,
   HYKTYPE              NUMBER(5)                       not null,
   HYKTYPE_NEW          NUMBER(5)                       not null,
   XFSJ                 NUMBER(5)                       not null,
   QDJF                 FLOAT(50)                       not null,
   BJ_SJ                NUMBER(5)                       not null,
   XGR                  NUMBER(10)                      not null,
   XGRMC                VARCHAR2(16)                    not null,
   XGSJ                 DATE                            not null,
   STATUS               NUMBER(5)                      default 0,
   DRXFJE               NUMBER(14,2),
   MDID                 NUMBER(10),
   SJYJJE               NUMBER(14,2),
   SJQDJE               NUMBER(14,2),
   BJ_XFJE              NUMBER(5),
   constraint PK_HYK_SQGZLSJL primary key (JLBH)
)
/

/*==============================================================*/
/* Table: HYK_THKQCE                                            */
/*==============================================================*/
create table BFCRM10.HYK_THKQCE  (
   HYID                 NUMBER(10)                      not null,
   CXID                 NUMBER(10)                      not null,
   YHQID                NUMBER(10)                      not null,
   MDFWDM               VARCHAR2(10)                    not null,
   KQCE                 NUMBER(14,2)                   default 0 not null
      constraint CKC_KQCE_HYK_THKQ check (KQCE >= 0),
   constraint PK_HYK_THKQCE primary key (HYID, CXID, YHQID, MDFWDM)
)
/

/*==============================================================*/
/* Table: HYK_THMJFGZDY                                         */
/*==============================================================*/
create table BFCRM10.HYK_THMJFGZDY  (
   ID                   NUMBER(10)                      not null,
   JF_J                 FLOAT(50),
   JF_N                 FLOAT(50),
   BJ_TY                NUMBER(5)                      default 0,
   HYKTYPE              NUMBER(5),
   constraint PK_HYK_THMJFGZDY primary key (ID)
)
/

/*==============================================================*/
/* Table: HYK_TK                                                */
/*==============================================================*/
create table BFCRM10.HYK_TK  (
   JLBH                 NUMBER(10)                      not null,
   HYKTYPE              NUMBER(5),
   ZY                   VARCHAR2(40),
   TKSL                 NUMBER(5)                       not null,
   JE                   NUMBER(14,2)                    not null,
   TKJE                 NUMBER(14,2)                    not null,
   TKFS                 NUMBER(5)                      default 0 not null,
   SKTNO                VARCHAR2(6),
   XSXPH                NUMBER(10),
   SKYDM                VARCHAR2(8),
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16)                    not null,
   DJSJ                 DATE                            not null,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(16),
   ZXRQ                 DATE,
   MDID                 NUMBER(10),
   BGR                  NUMBER(5),
   BGRMC                VARCHAR2(16),
   BGDDDM               VARCHAR2(20),
   LYR                  NUMBER(5),
   LYRMC                VARCHAR2(16),
   FXDWID               NUMBER(5),
   YXQ_NEW              DATE,
   KFJE                 NUMBER(14,2),
   KFJE_Y               NUMBER(14,2),
   KFJE_T               NUMBER(14,2),
   constraint PK_HYK_TK primary key (JLBH)
)
/

/*==============================================================*/
/* Table: HYK_TK_ITEM                                           */
/*==============================================================*/
create table BFCRM10.HYK_TK_ITEM  (
   JLBH                 NUMBER(10)                      not null,
   HYID                 NUMBER(10)                      not null,
   HYK_NO               VARCHAR2(30),
   CZKYE_OLD            NUMBER(14,2),
   YHQYE_OLD            NUMBER(14,2),
   JE                   NUMBER(14,2)                    not null,
   ZKL                  FLOAT(50)                      default 1 not null,
   TKJE                 NUMBER(14,2)                   default 0 not null,
   KFJE                 NUMBER(14,2)                   default 0,
   QCJE                 NUMBER(14,2),
   INX                  NUMBER(10)                      not null,
   BJ_CHILD             NUMBER(5),
   WCLJF_OLD            FLOAT(50),
   constraint PK_HYK_TK_ITEM primary key (JLBH, HYID)
)
/

/*==============================================================*/
/* Table: HYK_VIPCL_SJ                                          */
/*==============================================================*/
create table BFCRM10.HYK_VIPCL_SJ  (
   JLBH                 NUMBER(10)                      not null,
   HYKTYPE              NUMBER(5),
   VIPTYPE              NUMBER(5)                       not null,
   BZ                   VARCHAR2(100),
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16),
   DJSJ                 DATE                            not null,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(16),
   ZXRQ                 DATE,
   constraint PK_HYK_VIPCL_SJ primary key (JLBH)
)
/

/*==============================================================*/
/* Table: HYK_VIPCL_SJITEM                                      */
/*==============================================================*/
create table BFCRM10.HYK_VIPCL_SJITEM  (
   JLBH                 NUMBER(10)                      not null,
   VIPID                NUMBER(10)                      not null,
   VIPTYPE              NUMBER(5)                       not null,
   VIP_NO               VARCHAR2(12)                    not null,
   VIPTYPE_NEW          NUMBER(5),
   VIP_NO_NEW           VARCHAR2(12),
   LJXFJE               NUMBER(14,2),
   LJJF                 FLOAT(50),
   WCLJF                FLOAT(50),
   SYJF                 FLOAT(50)                      default 0,
   constraint PK_HYK_VIPCL_SJITEM primary key (JLBH, VIPID)
)
/

/*==============================================================*/
/* Index: VIPCLJLITEM_VIPID                                     */
/*==============================================================*/
create index BFCRM10.VIPCLJLITEM_VIPID on BFCRM10.HYK_VIPCL_SJITEM (
   VIPID ASC
)
/

/*==============================================================*/
/* Index: VIPCLJLITEM_VIPNO1                                    */
/*==============================================================*/
create index BFCRM10.VIPCLJLITEM_VIPNO1 on BFCRM10.HYK_VIPCL_SJITEM (
   VIPTYPE ASC,
   VIP_NO ASC
)
/

/*==============================================================*/
/* Index: VIPCLJLITEM_VIPNO2                                    */
/*==============================================================*/
create index BFCRM10.VIPCLJLITEM_VIPNO2 on BFCRM10.HYK_VIPCL_SJITEM (
   VIPTYPE_NEW ASC,
   VIP_NO_NEW ASC
)
/

/*==============================================================*/
/* Table: HYK_VIPJLCLJL                                         */
/*==============================================================*/
create table BFCRM10.HYK_VIPJLCLJL  (
   JLBH                 NUMBER(10)                      not null,
   HYID                 NUMBER(10)                      not null,
   HYKTYPE              NUMBER(5),
   FLJE                 NUMBER(14,2),
   LJXFJE               NUMBER(14,2)                   default 0,
   CLJF                 FLOAT(50)                      default 0,
   CLRQ                 DATE                            not null,
   CZJLBH               NUMBER(10),
   DJR                  NUMBER(10),
   DJRMC                VARCHAR2(16),
   DJSJ                 DATE,
   SHR                  NUMBER(10),
   SHRMC                VARCHAR2(16),
   SHRQ                 DATE,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(16),
   ZXRQ                 DATE,
   SYJSRQ               DATE,
   MDID                 NUMBER(10),
   YHQID                NUMBER(10),
   BJ_KCZ               NUMBER(5),
   FWFS                 NUMBER(5),
   YXBJSCL_BJ           NUMBER(5),
   ND                   NUMBER(5),
   HBFS                 NUMBER(5),
   SKTNO                VARCHAR2(10),
   constraint PK_HYK_VIPJLCLJL primary key (JLBH)
)
/

/*==============================================================*/
/* Index: VIPJFCLJL_CLRQ                                        */
/*==============================================================*/
create index BFCRM10.VIPJFCLJL_CLRQ on BFCRM10.HYK_VIPJLCLJL (
   CLRQ ASC
)
/

/*==============================================================*/
/* Table: HYK_VIPJLCLJLLP                                       */
/*==============================================================*/
create table BFCRM10.HYK_VIPJLCLJLLP  (
   JLBH                 NUMBER(10)                      not null,
   BGDDDM               VARCHAR2(20)                    not null,
   LPDM                 VARCHAR2(6)                     not null,
   SL                   FLOAT(50)                      default 0 not null,
   constraint PK_HYK_VIPJLCLJLLP primary key (JLBH, BGDDDM, LPDM)
)
/

/*==============================================================*/
/* Table: HYK_VIPJLCLJLMX                                       */
/*==============================================================*/
create table BFCRM10.HYK_VIPJLCLJLMX  (
   JLBH                 NUMBER(10)                      not null,
   XH                   NUMBER(5)                       not null,
   JF_QC                FLOAT(50)                       not null,
   JF_CL                FLOAT(50)                       not null,
   JLJE                 NUMBER(14,2),
   CONTENT              VARCHAR2(100)                   not null,
   HBFS                 NUMBER(5),
   CS                   NUMBER(10),
   constraint PK_HYK_VIPJLCLJLMX primary key (JLBH, XH)
)
/

/*==============================================================*/
/* Table: HYK_VIPJLCLJL_CYFS                                    */
/*==============================================================*/
create table BFCRM10.HYK_VIPJLCLJL_CYFS  (
   JLBH                 NUMBER(10)                      not null,
   DJBH                 NUMBER(10)                      not null,
   GZH                  NUMBER(5)                       not null,
   XFKSRQ               DATE,
   XFJSRQ               DATE,
   constraint PK_HYK_VIPJLCLJL_CYFS primary key (JLBH, DJBH, GZH)
)
/

/*==============================================================*/
/* Table: HYK_VIPJLCLJL_MD                                      */
/*==============================================================*/
create table BFCRM10.HYK_VIPJLCLJL_MD  (
   JLBH                 NUMBER(10)                      not null,
   MDID                 NUMBER(10)                      not null,
   LJXFJE               FLOAT(50)                       not null,
   ZKJE                 NUMBER(14,2),
   CLJF                 FLOAT(50),
   constraint PK_HYK_VIPJLCLJL_MD primary key (JLBH, MDID)
)
/

/*==============================================================*/
/* Table: HYK_VIPJLCLJL_XFMX                                    */
/*==============================================================*/
create table BFCRM10.HYK_VIPJLCLJL_XFMX  (
   JLBH                 NUMBER(10)                      not null,
   CYFS                 NUMBER(5)                       not null,
   RQ                   DATE                            not null,
   MDID                 NUMBER(10)                      not null,
   SKTNO                VARCHAR2(6)                     not null,
   XSXPH                NUMBER(10)                      not null,
   SHSPID               NUMBER(10)                      not null,
   XFJF                 FLOAT(50),
   constraint PK_HYK_VIPJLCLJL_XFMX primary key (JLBH, CYFS, RQ, MDID, SKTNO, XSXPH, SHSPID)
)
/

/*==============================================================*/
/* Table: HYK_WCLJF_RBB                                         */
/*==============================================================*/
create sequence BFCRM10.S_HYK_WCLJF_RBB
/
create table BFCRM10.HYK_WCLJF_RBB  (
   RQ                   DATE                            not null,
   HYKTYPE              NUMBER(5)                       not null,
   SQWCLJF              NUMBER(16,4),
   WCLJFBD_GHKLX        NUMBER(16,4),
   WCLJFBD_BDD          NUMBER(16,4),
   WCLJFBD_TZD          NUMBER(16,4),
   WCLJFBD_SJ           NUMBER(16,4),
   WCLJFBD_JJ           NUMBER(16,4),
   WCLJFBD_QLSJLSJ      NUMBER(16,4),
   WCLJFBD_YXQYC        NUMBER(16,4),
   WCLJFBD_XM           NUMBER(16,4),
   WCLJFBD_FL_CZ        NUMBER(16,4),
   WCLJFBD_LKHXK        NUMBER(16,4),
   WCLJFBD_LKBXK        NUMBER(16,4),
   WCLJFBD_QTXF         NUMBER(16,4),
   WCLJFBD_YQMJF        NUMBER(16,4),
   WCLJFBD_JCZZ         NUMBER(16,4),
   WCLJFBD_QJF          NUMBER(16,4),
   WCLJFBD_JFHG         NUMBER(16,4),
   WCLJFBD_JFZC         NUMBER(16,4),
   WCLJFBD_JFQL         NUMBER(16,4)                   default 0,
   WCLJFBD_CZKSJF       NUMBER(16,4)                   default 0,
   WCLJFBD_WEB          NUMBER(16,4)                   default 0,
   WCLJFBD_JFDX         NUMBER(16,4)                   default 0,
   QMWCLJF              NUMBER(16,4),
   YEARMONTH            NUMBER(10)                      not null,
   TM                   NUMBER(10),
   constraint PK_HYK_WCLJF_RBB primary key (RQ, HYKTYPE)
)
/

/*==============================================================*/
/* Table: HYK_XFJFBS                                            */
/*==============================================================*/
create table BFCRM10.HYK_XFJFBS  (
   XFJLID               NUMBER(10)                      not null,
   GZID                 NUMBER(10)                      not null,
   XFJE_JF              NUMBER(14,2)                   default 0,
   BS                   NUMBER(8,2)                    default 0,
   JF                   NUMBER(14,4)                   default 0,
   XFJE_JF_OLD          NUMBER(14,2)                   default 0,
   JF_OLD               NUMBER(14,4)                   default 0,
   constraint PK_HYK_XFJFBS primary key (XFJLID, GZID)
)
/

/*==============================================================*/
/* Table: HYK_XFJL                                              */
/*==============================================================*/
create sequence BFCRM10.S_HYK_XFJL
/
create table BFCRM10.HYK_XFJL  (
   XFJLID               NUMBER(10)                      not null,
   SHDM                 VARCHAR2(4)                     not null,
   MDID                 NUMBER(10)                      not null,
   SKTNO                VARCHAR2(6)                     not null,
   JLBH                 NUMBER(10)                      not null,
   DJLX                 NUMBER(5)                       not null,
   XFJLID_OLD           NUMBER(10),
   HYID                 NUMBER(10),
   HYID_FQ              NUMBER(10),
   HYID_TQ              NUMBER(10),
   SKYDM                VARCHAR2(8),
   XFSJ                 DATE,
   JZRQ                 DATE,
   CRMJZRQ              DATE,
   SCSJ                 DATE                            not null,
   JE                   NUMBER(14,2)                   default 0 not null,
   ZK                   NUMBER(14,2)                   default 0 not null,
   ZK_HY                NUMBER(14,2)                   default 0 not null,
   CZJE                 NUMBER(14,2)                   default 0 not null,
   JF                   FLOAT(50)                      default 0 not null,
   BJ_HTBSK             NUMBER(5)                      default 0 not null,
   STATUS               NUMBER(5)                      default 0 not null,
   BJ_WZSP              NUMBER(5)                      default 0 not null,
   BJ_WZZFFS            NUMBER(5)                      default 0 not null,
   XFRQ_FQ              DATE,
   JFBS                 FLOAT(50)                      default 1 not null,
   TDJFDYDBH            NUMBER(10),
   BSFS                 NUMBER(10),
   HYKNO                VARCHAR2(20),
   HYKNO_FQ             VARCHAR2(20),
   FXDW                 NUMBER(10),
   TM                   NUMBER(10),
   READOVER             NUMBER(5)                      default 0,
   READOVERTIME         DATE,
   XSSL                 NUMBER(16,4)                   default 0,
   BJ_CHILD             NUMBER(5),
   PGRYID               NUMBER(10),
   VIPTYPE              NUMBER(5),
   BJ_SL                NUMBER(5),
   constraint PK_HYK_XFJL primary key (XFJLID)
)
/

/*==============================================================*/
/* Index: HYK_XFJL_MAIN                                         */
/*==============================================================*/
create unique index BFCRM10.HYK_XFJL_MAIN on BFCRM10.HYK_XFJL (
   MDID ASC,
   SKTNO ASC,
   JLBH ASC
)
/

/*==============================================================*/
/* Index: HYK_XFJL_HYJF                                         */
/*==============================================================*/
create index BFCRM10.HYK_XFJL_HYJF on BFCRM10.HYK_XFJL (
   HYID ASC,
   MDID ASC,
   XFSJ ASC
)
/

/*==============================================================*/
/* Index: HYK_XFJL_HYFQ                                         */
/*==============================================================*/
create index BFCRM10.HYK_XFJL_HYFQ on BFCRM10.HYK_XFJL (
   HYID_FQ ASC,
   MDID ASC,
   XFSJ ASC
)
/

/*==============================================================*/
/* Index: HYK_XFJL_MX                                           */
/*==============================================================*/
create index BFCRM10.HYK_XFJL_MX on BFCRM10.HYK_XFJL (
   XFJLID ASC,
   XFSJ ASC,
   STATUS ASC
)
/

/*==============================================================*/
/* Index: HYK_XFJL_XFSJ                                         */
/*==============================================================*/
create index BFCRM10.HYK_XFJL_XFSJ on BFCRM10.HYK_XFJL (
   XFSJ ASC
)
/

/*==============================================================*/
/* Index: INX_HYK_XFJL_HYID_XFSJ                                */
/*==============================================================*/
create index BFCRM10.INX_HYK_XFJL_HYID_XFSJ on BFCRM10.HYK_XFJL (
   HYID ASC,
   XFSJ ASC
)
/

/*==============================================================*/
/* Table: HYK_XFJL_FQ                                           */
/*==============================================================*/
create table BFCRM10.HYK_XFJL_FQ  (
   XFJLID               NUMBER(10)                      not null,
   CXID                 NUMBER(10)                      not null,
   YHQID                NUMBER(5)                       not null,
   YHQFFGZID            NUMBER(10)                      not null,
   XFLJFQFS             NUMBER(5)                       not null,
   BMDM                 VARCHAR2(10)                    not null,
   JSRQ                 DATE                            not null,
   ZXFJE_OLD            NUMBER(14,2)                   default 0 not null,
   SYXFJE_OLD           NUMBER(14,2)                   default 0 not null,
   FQJE_OLD             NUMBER(14,2)                   default 0 not null,
   ZXFJE                NUMBER(14,2)                   default 0 not null,
   SYXFJE               NUMBER(14,2)                   default 0 not null,
   FQJE                 NUMBER(14,2)                   default 0 not null,
   THJE                 NUMBER(14,2)                   default 0 not null,
   KQJE                 NUMBER(14,2)                   default 0 not null,
   HYID_ZQ              NUMBER(10),
   DJLX                 NUMBER(5)                      default 0,
   FQJE_WX              NUMBER(14,2)                   default 0 not null,
   constraint PK_HYK_XFJL_FQ primary key (XFJLID, CXID, YHQID, YHQFFGZID, XFLJFQFS, BMDM, JSRQ)
)
/

/*==============================================================*/
/* Table: HYK_XFJL_FQDM                                         */
/*==============================================================*/
create table BFCRM10.HYK_XFJL_FQDM  (
   XFJLID               NUMBER(10)                      not null,
   YHQCODE              VARCHAR2(20)                    not null,
   YHQID                NUMBER(5)                       not null,
   YHQMZ                NUMBER(14,2)                   default 0 not null,
   CXID                 NUMBER(10),
   constraint PK_HYK_XFJL_FQDM primary key (XFJLID, YHQCODE)
)
/

/*==============================================================*/
/* Table: HYK_XFJL_FQ_ITEM                                      */
/*==============================================================*/
create table BFCRM10.HYK_XFJL_FQ_ITEM  (
   JLBH                 NUMBER(10)                      not null,
   YHQID                NUMBER(5)                       not null,
   CXID                 NUMBER(10)                      not null,
   YHQFFGZID            NUMBER(10)                      not null,
   XFLJFQFS             NUMBER(5)                       not null,
   BMDM                 VARCHAR2(10)                    not null,
   ZXFJE                NUMBER(14,2)                   default 0 not null,
   SYXFJE               NUMBER(14,2)                   default 0 not null,
   FQJE                 NUMBER(14,2)                   default 0 not null,
   constraint PK_HYK_XFJL_FQ_ITEM primary key (JLBH, CXID, YHQID, YHQFFGZID, XFLJFQFS, BMDM)
)
/

/*==============================================================*/
/* Table: HYK_XFJL_FQ_ITEM_DELETE                               */
/*==============================================================*/
create table BFCRM10.HYK_XFJL_FQ_ITEM_DELETE  (
   JLBH                 NUMBER(10)                      not null,
   CXID                 NUMBER(10)                      not null,
   YHQID                NUMBER(5)                       not null,
   YHQFFGZID            NUMBER(10)                      not null,
   XFLJFQFS             NUMBER(5)                       not null,
   BMDM                 VARCHAR2(10)                    not null,
   ZXFJE                NUMBER(14,2)                   default 0,
   SYXFJE               NUMBER(14,2)                   default 0,
   FQJE                 NUMBER(14,2)                   default 0,
   constraint PK_HYK_XFJL_FQ_ITEM_DELETE primary key (JLBH, CXID, YHQID, YHQFFGZID, XFLJFQFS, BMDM)
)
/

/*==============================================================*/
/* Table: HYK_XFJL_FQ_ZQITEM                                    */
/*==============================================================*/
create table BFCRM10.HYK_XFJL_FQ_ZQITEM  (
   XFJLID               NUMBER(10)                      not null,
   CXHDBH               NUMBER(10)                      not null,
   YHQID                NUMBER(5)                       not null,
   HYID_ZQ              NUMBER(10)                      not null,
   constraint PK_HYK_XFJL_FQ_ZQITEM primary key (XFJLID, CXHDBH, YHQID, HYID_ZQ)
)
/

/*==============================================================*/
/* Table: HYK_XFJL_ID                                           */
/*==============================================================*/
create table BFCRM10.HYK_XFJL_ID  (
   XFJLID               NUMBER(10)                      not null,
   constraint PK_HYK_XFJL_ID primary key (XFJLID)
)
/

/*==============================================================*/
/* Table: HYK_XFJL_SP                                           */
/*==============================================================*/
create sequence BFCRM10.S_HYK_XFJL_SP
/
create table BFCRM10.HYK_XFJL_SP  (
   XFJLID               NUMBER(10)                     default 0 not null,
   INX                  NUMBER(5)                       not null,
   SHSPID               NUMBER(10),
   BMDM                 VARCHAR2(20)                    not null,
   SPDM                 VARCHAR2(13)                    not null,
   XSSL                 NUMBER(16,4)                   default 0 not null,
   XSJE                 NUMBER(14,2)                    not null,
   ZKJE                 NUMBER(14,2)                    not null,
   ZKJE_HY              NUMBER(14,2)                    not null,
   THJE                 NUMBER(14,2)                   default 0 not null,
   BJ_JF                NUMBER(5)                       not null,
   XSJE_JF              NUMBER(14,2)                   default 0 not null,
   XSJE_FQ              NUMBER(14,2)                   default 0 not null,
   JFDYDBH              NUMBER(10),
   JF                   FLOAT(50)                      default 0 not null,
   JFJS                 NUMBER(16,4)                   default 0 not null,
   ZKL                  NUMBER(16,4)                   default 1,
   ZKDYDBH              NUMBER(10),
   HTH                  VARCHAR2(10),
   TM                   NUMBER(10),
   BJ_JFBS              NUMBER(5),
   BJ_BCJHD             NUMBER(5)                      default 0,
   BS                   FLOAT(50),
   XSJE_SYJE            NUMBER(14,2),
   FTBL                 NUMBER(8,4),
   JFFBGZ               NUMBER(10),
   THJE_JF              NUMBER(14,2),
   THJF                 NUMBER(16,4),
   INX_OLD              NUMBER(5),
   BJ_BCJMJ             NUMBER(5)                      default 0,
   BJ_BCJYQ             NUMBER(5)                      default 0,
   constraint PK_HYK_XFJL_SP primary key (XFJLID, INX)
)
/

/*==============================================================*/
/* Index: HYK_XFJL_XFSP                                         */
/*==============================================================*/
create index BFCRM10.HYK_XFJL_XFSP on BFCRM10.HYK_XFJL_SP (
   XFJLID ASC,
   SHSPID ASC
)
/

/*==============================================================*/
/* Table: HYK_XFJL_SP_FQ                                        */
/*==============================================================*/
create table BFCRM10.HYK_XFJL_SP_FQ  (
   XFJLID               NUMBER(10)                     default 0 not null,
   INX                  NUMBER(5)                       not null,
   YHQID                NUMBER(5)                       not null,
   BMDM                 VARCHAR2(20)                    not null,
   SPDM                 VARCHAR2(13)                    not null,
   SHSPID               NUMBER(10),
   YHQFFDBH             NUMBER(10),
   CXID                 NUMBER(10)                      not null,
   XFLJFQFS             NUMBER(5)                       not null,
   YHQFFGZID            NUMBER(10)                      not null,
   XSSL                 NUMBER(16,4)                    not null,
   XSJE                 NUMBER(14,2)                    not null,
   XSJE_FQ              NUMBER(14,2)                    not null,
   FQJE                 NUMBER(14,2)                    not null,
   FQJE_SJ              NUMBER(14,2)                   default 0 not null,
   SHZFFS               NUMBER(10),
   DJLX                 NUMBER(5)                      default 5,
   constraint PK_HYK_XFJL_SP_FQ primary key (XFJLID, INX, YHQID)
)
/

/*==============================================================*/
/* Index: HYK_XFJL_SPFQ1                                        */
/*==============================================================*/
create index BFCRM10.HYK_XFJL_SPFQ1 on BFCRM10.HYK_XFJL_SP_FQ (
   XFJLID ASC,
   YHQID ASC,
   SHSPID ASC
)
/

/*==============================================================*/
/* Index: INDEX_XFJLID                                          */
/*==============================================================*/
create index BFCRM10.INDEX_XFJLID on BFCRM10.HYK_XFJL_SP_FQ (
   XFJLID ASC,
   YHQID ASC,
   YHQFFGZID ASC
)
/

/*==============================================================*/
/* Table: HYK_XFJL_SP_FQ_DELETE                                 */
/*==============================================================*/
create table BFCRM10.HYK_XFJL_SP_FQ_DELETE  (
   XFJLID               NUMBER(10)                      not null,
   INX                  NUMBER(5)                       not null,
   YHQID                NUMBER(5)                       not null,
   BMDM                 VARCHAR2(20)                    not null,
   SPDM                 VARCHAR2(30)                    not null,
   SHSPID               NUMBER(10),
   YHQFFDBH             NUMBER(10),
   CXID                 NUMBER(10)                      not null,
   XFLJFQFS             NUMBER(5)                       not null,
   YHQFFGZID            NUMBER(10)                      not null,
   XSSL                 NUMBER(16,4)                    not null,
   XSJE                 NUMBER(14,2)                    not null,
   XSJE_FQ              NUMBER(14,2)                    not null,
   FQJE                 NUMBER(14,2)                    not null,
   FQJE_SJ              NUMBER(14,2)                   default 0,
   SHZFFS               NUMBER(10),
   CLLX                 NUMBER(5)                      default 0,
   constraint PK_HYK_XFJL_SP_FQ_DELETE primary key (XFJLID, INX, YHQID)
)
/

/*==============================================================*/
/* Index: HYK_XFJL_SP_FQ_DELETE_YHQID                           */
/*==============================================================*/
create index BFCRM10.HYK_XFJL_SP_FQ_DELETE_YHQID on BFCRM10.HYK_XFJL_SP_FQ_DELETE (
   XFJLID ASC,
   YHQID ASC,
   SHSPID ASC
)
/

/*==============================================================*/
/* Index: HYK_XFJL_SP_FQ_DELETE_XFJLID                          */
/*==============================================================*/
create index BFCRM10.HYK_XFJL_SP_FQ_DELETE_XFJLID on BFCRM10.HYK_XFJL_SP_FQ_DELETE (
   XFJLID ASC,
   YHQID ASC,
   YHQFFGZID ASC
)
/

/*==============================================================*/
/* Table: HYK_XFJL_SP_MBJZ                                      */
/*==============================================================*/
create table BFCRM10.HYK_XFJL_SP_MBJZ  (
   XFJLID               NUMBER(10)                      not null,
   INX                  NUMBER(5)                       not null,
   BMDM                 VARCHAR2(20)                    not null,
   SPDM                 VARCHAR2(13)                    not null,
   SHSPID               NUMBER(10),
   MBJZDBH              NUMBER(10),
   CXID                 NUMBER(10)                      not null,
   XFLJMJFS             NUMBER(5)                       not null,
   MBJZGZID             NUMBER(10)                      not null,
   XSSL                 NUMBER(16,4)                    not null,
   XSJE                 NUMBER(14,2)                    not null,
   XSJE_MBJZ            NUMBER(14,2)                    not null,
   MBJZJE               NUMBER(14,2)                   default 0 not null,
   SHSBID               NUMBER(10),
   SHHTID               NUMBER(10),
   GHSDM                VARCHAR2(10),
   MJJQ                 NUMBER(1),
   constraint PK_HYK_XFJL_SP_MBJZ primary key (XFJLID, INX)
)
/

/*==============================================================*/
/* Table: HYK_XFJL_SP_YQFT                                      */
/*==============================================================*/
create table BFCRM10.HYK_XFJL_SP_YQFT  (
   XFJLID               NUMBER(10)                     default 0 not null,
   INX                  NUMBER(5)                       not null,
   YHQID                NUMBER(5)                       not null,
   BMDM                 VARCHAR2(20)                    not null,
   SPDM                 VARCHAR2(13)                    not null,
   SHSPID               NUMBER(10)                      not null,
   YHQSYDBH             NUMBER(10),
   CXID                 NUMBER(10),
   YHQSYGZID            NUMBER(10),
   XSJE                 NUMBER(14,2)                    not null,
   XSJE_YQ              NUMBER(14,2)                    not null,
   YQJE                 NUMBER(14,2)                    not null,
   BJ_FQ                NUMBER(5)                       not null,
   constraint PK_HYK_XFJL_SP_YQFT primary key (XFJLID, INX, YHQID)
)
/

/*==============================================================*/
/* Index: HYK_XFJL_SPYQFT                                       */
/*==============================================================*/
create index BFCRM10.HYK_XFJL_SPYQFT on BFCRM10.HYK_XFJL_SP_YQFT (
   XFJLID ASC,
   SHSPID ASC,
   YHQID ASC
)
/

/*==============================================================*/
/* Table: HYK_XFJL_SP_YQJC                                      */
/*==============================================================*/
create table BFCRM10.HYK_XFJL_SP_YQJC  (
   XFJLID               NUMBER(10)                     default 0 not null,
   YHQID                NUMBER(5)                       not null,
   INX                  NUMBER(5)                       not null,
   SPDM                 VARCHAR2(13)                    not null,
   BMDM                 VARCHAR2(20)                    not null,
   SHSPID               NUMBER(10),
   YHQSYDBH             NUMBER(10),
   CXID                 NUMBER(10),
   YHQSYGZID            NUMBER(10),
   XSJE                 NUMBER(14,2)                    not null,
   XSJE_YQ              NUMBER(14,2)                    not null,
   BJ_FQ                NUMBER(5)                       not null,
   constraint PK_HYK_XFJL_SP_YQJC primary key (XFJLID, YHQID, INX)
)
/

/*==============================================================*/
/* Table: HYK_XFJL_SP_ZFFS                                      */
/*==============================================================*/
create table BFCRM10.HYK_XFJL_SP_ZFFS  (
   XFJLID               NUMBER(10)                     default 0 not null,
   INX                  NUMBER(5)                       not null,
   SHZFFSID             NUMBER(10)                      not null,
   JE                   NUMBER(14,2)                   default 0 not null,
   BJ_JF                NUMBER(5)                       not null,
   BJ_FQ                NUMBER(5)                       not null,
   YHQID                NUMBER(10),
   JFBL                 FLOAT(50),
   SYJE                 NUMBER(14,2),
   constraint PK_HYK_XFJL_SP_ZFFS primary key (XFJLID, INX, SHZFFSID)
)
/

/*==============================================================*/
/* Table: HYK_XFJL_SP_ZFFS_FQ                                   */
/*==============================================================*/
create table BFCRM10.HYK_XFJL_SP_ZFFS_FQ  (
   XFJLID               NUMBER(10)                      not null,
   SPINX                NUMBER(5)                       not null,
   INX                  NUMBER(5)                       not null,
   SHZFFSID             NUMBER(5)                       not null,
   BANK_ZFINX           NUMBER(5)                      default 0 not null,
   BANK_ID              NUMBER(5)                      default -1 not null,
   BANK_KH              VARCHAR2(20)                   default ' ' not null,
   YHQID                NUMBER(5)                       not null,
   SHSPID               NUMBER(10)                      not null,
   SPDM                 VARCHAR2(13)                    not null,
   BMDM                 VARCHAR2(10)                    not null,
   XSJE                 NUMBER(14,2)                   default 0 not null,
   CXID                 NUMBER(10)                      not null,
   YHQFFDBH             NUMBER(10)                      not null,
   YHQFFGZID            NUMBER(10)                      not null,
   XFLJFQFS             NUMBER(5)                      default 0 not null,
   constraint PK_HYK_XFJL_SP_ZFFS_FQ primary key (XFJLID, SPINX, INX)
)
/

/*==============================================================*/
/* Table: HYK_XFJL_SP_ZFFS_MBJZ                                 */
/*==============================================================*/
create table BFCRM10.HYK_XFJL_SP_ZFFS_MBJZ  (
   XFJLID               NUMBER(10)                     default 0 not null,
   INX                  NUMBER(5)                       not null,
   SHZFFSID             NUMBER(10)                      not null,
   JE                   NUMBER(14,2)                   default 0 not null,
   BJ_JF                NUMBER(5)                       not null,
   BJ_FQ                NUMBER(5)                       not null,
   YHQID                NUMBER(10),
   constraint PK_HYK_XFJL_SP_ZFFS_MBJZ primary key (XFJLID, INX, SHZFFSID)
)
/

/*==============================================================*/
/* Table: HYK_XFJL_YHKZF                                        */
/*==============================================================*/
create table BFCRM10.HYK_XFJL_YHKZF  (
   XFJLID               NUMBER(10)                      not null,
   INX                  NUMBER(10)                      not null,
   BANK_ID              NUMBER(5)                       not null,
   BANK_KH              VARCHAR2(20)                    not null,
   SHZFFSID             NUMBER(10)                      not null,
   JE                   NUMBER(14,2)                    not null,
   constraint PK_HYK_XFJL_YHKZF primary key (XFJLID, INX)
)
/

/*==============================================================*/
/* Table: HYK_XFJL_ZFFS                                         */
/*==============================================================*/
create table BFCRM10.HYK_XFJL_ZFFS  (
   XFJLID               NUMBER(10)                      not null,
   INX                  NUMBER(5)                       not null,
   ZFFSID               NUMBER(10),
   ZFFSDM               VARCHAR2(10)                    not null,
   JE                   NUMBER(14,2)                    not null,
   BJ_JF                NUMBER(5)                       not null,
   BJ_FQ                NUMBER(5)                       not null,
   YHQID                NUMBER(5),
   JFBL                 FLOAT(50),
   constraint PK_HYK_XFJL_ZFFS primary key (XFJLID, INX)
)
/

/*==============================================================*/
/* Index: HYK_XFJL_XFZFFS                                       */
/*==============================================================*/
create index BFCRM10.HYK_XFJL_XFZFFS on BFCRM10.HYK_XFJL_ZFFS (
   XFJLID ASC,
   ZFFSID ASC
)
/

/*==============================================================*/
/* Table: HYK_XFJL_ZFFS_FQ                                      */
/*==============================================================*/
create table BFCRM10.HYK_XFJL_ZFFS_FQ  (
   XFJLID               NUMBER(10)                      not null,
   CXID                 NUMBER(10)                      not null,
   YHQID                NUMBER(5)                       not null,
   YHQFFGZID            NUMBER(10)                      not null,
   XFLJFQFS             NUMBER(5)                      default 0 not null,
   SHZFFSID             NUMBER(5)                       not null,
   BANK_ZFINX           NUMBER(5)                      default 0 not null,
   JSRQ                 DATE                            not null,
   BANK_ID              NUMBER(5)                      default -1 not null,
   BANK_KH              VARCHAR2(20)                   default ' ' not null,
   ZXFJE                NUMBER(14,2)                   default 0 not null,
   SYXFJE               NUMBER(14,2)                   default 0 not null,
   FQJE                 NUMBER(14,2)                   default 0 not null,
   FQJE_SJ              NUMBER(14,2)                   default 0 not null,
   constraint PK_HYK_XFJL_ZFFS_FQ primary key (XFJLID, CXID, YHQID, YHQFFGZID, XFLJFQFS, SHZFFSID, BANK_ZFINX, JSRQ)
)
/

/*==============================================================*/
/* Table: HYK_XFJL_ZFFS_ZSLP                                    */
/*==============================================================*/
create table BFCRM10.HYK_XFJL_ZFFS_ZSLP  (
   XFJLID               NUMBER(10)                      not null,
   CXID                 NUMBER(10)                      not null,
   YHQID                NUMBER(5)                       not null,
   YHQFFGZID            NUMBER(10)                      not null,
   XFLJFQFS             NUMBER(5)                      default 0 not null,
   SHZFFSID             NUMBER(5)                       not null,
   BANK_ZFINX           NUMBER(5)                      default 0 not null,
   LPDM                 VARCHAR2(10)                    not null,
   BANK_ID              NUMBER(5)                      default -1 not null,
   BANK_KH              VARCHAR2(20)                    not null,
   LPSL                 NUMBER(5)                      default 0 not null,
   constraint PK_HYK_XFJL_ZFFS_ZSLP primary key (XFJLID, CXID, YHQID, YHQFFGZID, XFLJFQFS, SHZFFSID, BANK_ZFINX, LPDM)
)
/

/*==============================================================*/
/* Table: HYK_XFJL_ZSLP                                         */
/*==============================================================*/
create table BFCRM10.HYK_XFJL_ZSLP  (
   XFJLID               NUMBER(10)                      not null,
   CXID                 NUMBER(10)                      not null,
   YHQID                NUMBER(5)                       not null,
   YHQFFGZID            NUMBER(10)                      not null,
   XFLJFQFS             NUMBER(5)                       not null,
   BMDM                 VARCHAR2(10)                    not null,
   LPDM                 VARCHAR2(10)                    not null,
   LPSL                 NUMBER(5)                      default 0 not null,
   constraint PK_HYK_XFJL_ZSLP primary key (XFJLID, CXID, YHQID, YHQFFGZID, XFLJFQFS, BMDM)
)
/

/*==============================================================*/
/* Table: HYK_XFLJDFQ                                           */
/*==============================================================*/
create table BFCRM10.HYK_XFLJDFQ  (
   HYID                 NUMBER(10)                      not null,
   MDFWDM               VARCHAR2(10)                    not null,
   CXID                 NUMBER(10)                      not null,
   INX_XFRQ             NUMBER(5)                       not null,
   YHQID                NUMBER(5)                       not null,
   YHQFFGZID            NUMBER(10)                      not null,
   ZXFJE                NUMBER(14,2)                   default 0 not null
      constraint CKC_ZXFJE_HYK_XFLJ check (ZXFJE >= 0),
   SYXFJE               NUMBER(14,2)                   default 0 not null
      constraint CKC_SYXFJE_HYK_XFLJ check (SYXFJE >= 0),
   FQJE                 NUMBER(14,2)                   default 0 not null
      constraint CKC_FQJE_HYK_XFLJ check (FQJE >= 0),
   XFRQ                 DATE,
   constraint PK_HYK_XFLJDFQ primary key (HYID, MDFWDM, CXID, INX_XFRQ, YHQID, YHQFFGZID)
)
/

/*==============================================================*/
/* Table: HYK_XFMX                                              */
/*==============================================================*/
create sequence BFCRM10.S_HYK_XFMX
/
create table BFCRM10.HYK_XFMX  (
   RQ                   DATE                            not null,
   MDID                 NUMBER(10)                      not null,
   SKTNO                VARCHAR2(6)                     not null,
   JLBH                 NUMBER(10)                      not null,
   SHSPID               NUMBER(10)                      not null,
   DEPTID               VARCHAR2(10)                    not null,
   HYID                 NUMBER(10)                      not null,
   HYKTYPE              NUMBER(5)                       not null,
   JYSJ                 DATE                            not null,
   SHHTID               NUMBER(10),
   SHSPFLID             NUMBER(10),
   SHSBID               NUMBER(10),
   XSSL                 NUMBER(16,4)                   default 0,
   XSJE                 NUMBER(14,2)                   default 0 not null,
   ZKJE                 NUMBER(14,2)                   default 0 not null,
   ZKJE_HY              NUMBER(14,2),
   JF                   FLOAT(50)                       not null,
   CLJF_ZF              FLOAT(50),
   CLJF_PP              FLOAT(50),
   CLJF_GHS             FLOAT(50),
   YEARMONTH            NUMBER(10),
   TM                   NUMBER(10),
   HTH                  VARCHAR2(20),
   SHBMID               NUMBER(10),
   constraint PK_HYK_XFMX primary key (RQ, MDID, SKTNO, JLBH, SHSPID, DEPTID, HYID)
)
/

/*==============================================================*/
/* Table: HYK_XFYBB                                             */
/*==============================================================*/
create table BFCRM10.HYK_XFYBB  (
   YEARMONTH            NUMBER(10)                      not null,
   MDID                 NUMBER(10)                      not null,
   HYKTYPE              NUMBER(10)                      not null,
   XZHYSL               NUMBER(10),
   LJHYSL               NUMBER(10),
   XFHYSL               NUMBER(10),
   XFJE                 NUMBER(14,2),
   XFJE_SN              FLOAT(50),
   XFJE_WB              FLOAT(50),
   XFJE_JXC             NUMBER(14,2),
   XFJE_JXC_SN          FLOAT(50),
   constraint PK_HYK_XFYBB primary key (YEARMONTH, MDID, HYKTYPE)
)
/

/*==============================================================*/
/* Table: HYK_XGMMJL                                            */
/*==============================================================*/
create table BFCRM10.HYK_XGMMJL  (
   JLBH                 NUMBER(10)                      not null,
   HYID                 NUMBER(10)                      not null,
   PSWD_OLD             VARCHAR2(30)                    not null,
   PSWD_NEW             VARCHAR2(30)                    not null,
   XGSJ                 DATE                            not null,
   XGR                  NUMBER(10),
   XGRMC                VARCHAR2(16),
   BZ                   VARCHAR2(30),
   SKTNO                VARCHAR2(6),
   MDID                 NUMBER(10),
   SKYDM                VARCHAR2(10),
   constraint PK_HYK_XGMMJL primary key (JLBH)
)
/

/*==============================================================*/
/* Table: HYK_XMGZLSJL                                          */
/*==============================================================*/
create table BFCRM10.HYK_XMGZLSJL  (
   JLBH                 NUMBER(10)                      not null,
   HYKTYPE              NUMBER(5)                       not null,
   XMJF_SX              FLOAT(50)                       not null,
   XFJE_JC              NUMBER(14,2)                    not null,
   SXF_JC               NUMBER(14,2),
   XMSJ_ZD              NUMBER(5)                       not null,
   XGR                  NUMBER(10)                      not null,
   XGRMC                VARCHAR2(16)                    not null,
   XGSJ                 DATE                            not null,
   constraint PK_HYK_XMGZLSJL primary key (JLBH)
)
/

/*==============================================================*/
/* Table: HYK_XSFWDEF                                           */
/*==============================================================*/
create table BFCRM10.HYK_XSFWDEF  (
   HYKTYPE              NUMBER(5)                       not null,
   FWNRID               NUMBER(5)                       not null,
   FWSL                 FLOAT(50),
   constraint PK_HYK_XSFWDEF primary key (HYKTYPE, FWNRID)
)
/

/*==============================================================*/
/* Table: HYK_XSFWJL                                            */
/*==============================================================*/
create table BFCRM10.HYK_XSFWJL  (
   JLBH                 NUMBER(10)                      not null,
   HYID                 NUMBER(10)                      not null,
   FWNRID               NUMBER(5)                       not null,
   FWSL                 FLOAT(50),
   BZ                   VARCHAR2(30),
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16)                    not null,
   DJSJ                 DATE                            not null,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(16),
   ZXRQ                 DATE,
   constraint PK_HYK_XSFWJL primary key (JLBH)
)
/

/*==============================================================*/
/* Table: HYK_YEBD                                              */
/*==============================================================*/
create table BFCRM10.HYK_YEBD  (
   HYID                 NUMBER(10)                      not null,
   BDSJ                 DATE                            not null,
   DAC                  RAW(8)                          not null,
   constraint PK_HYK_YEBD primary key (HYID)
)
/

/*==============================================================*/
/* Table: HYK_YHQBD                                             */
/*==============================================================*/
create table BFCRM10.HYK_YHQBD  (
   HYID                 NUMBER(10)                      not null,
   YHQID                NUMBER(5)                       not null,
   CXID                 NUMBER(10)                     default 0 not null,
   JSRQ                 DATE                            not null,
   MDFWDM               VARCHAR2(10)                    not null,
   BDSJ                 DATE                            not null,
   DAC                  RAW(8)                          not null,
   constraint PK_HYK_YHQBD primary key (HYID, YHQID, CXID, JSRQ, MDFWDM)
)
/

/*==============================================================*/
/* Table: HYK_YHQCLJL                                           */
/*==============================================================*/
create sequence BFCRM10.S_HYK_YHQCLJL
/
create sequence BFCRM10.S_YHQCLJL_JYBH
/
create table BFCRM10.HYK_YHQCLJL  (
   JYBH                 NUMBER(9)                       not null,
   HYID                 NUMBER(10)                      not null,
   CLSJ                 DATE                            not null,
   CLLX                 NUMBER(5)                       not null,
   JLBH                 NUMBER(10),
   YHQID                NUMBER(5)                       not null,
   CXID                 NUMBER(10)                     default 0 not null,
   JSRQ                 DATE                            not null,
   MDFWDM               VARCHAR2(10)                    not null,
   MDID                 NUMBER(10),
   SKTNO                VARCHAR2(6),
   ZY                   VARCHAR2(40),
   JFJE                 NUMBER(14,2)                   default 0 not null
      constraint CKC_JFJE_HYK_YHQC check (JFJE between -200000 and 200000),
   DFJE                 NUMBER(14,2)                   default 0 not null
      constraint CKC_DFJE_HYK_YHQC check (DFJE between -200000 and 200000),
   YE                   NUMBER(14,2)                   default 0 not null,
   JYID                 NUMBER(9,0),
   JZRQ                 DATE,
   CRMJZRQ              DATE,
   SKYDM                VARCHAR2(8),
   SKYMC                VARCHAR2(16),
   TM                   NUMBER(10),
   constraint PK_HYK_YHQCLJL primary key (JYBH)
)
/

/*==============================================================*/
/* Index: YHQCLJL_HYID                                          */
/*==============================================================*/
create index BFCRM10.YHQCLJL_HYID on BFCRM10.HYK_YHQCLJL (
   HYID ASC
)
/

/*==============================================================*/
/* Index: YHQCLJL_YHQ                                           */
/*==============================================================*/
create index BFCRM10.YHQCLJL_YHQ on BFCRM10.HYK_YHQCLJL (
   YHQID ASC,
   JSRQ ASC,
   MDFWDM ASC,
   CLLX ASC
)
/

/*==============================================================*/
/* Index: YHQCLJL_JZRQ                                          */
/*==============================================================*/
create index BFCRM10.YHQCLJL_JZRQ on BFCRM10.HYK_YHQCLJL (
   MDID ASC,
   SKTNO ASC,
   CRMJZRQ ASC
)
/

/*==============================================================*/
/* Table: HYK_YHQZH                                             */
/*==============================================================*/
create sequence BFCRM10.S_HYK_YHQZH
/
create table BFCRM10.HYK_YHQZH  (
   HYID                 NUMBER(10)                      not null,
   YHQID                NUMBER(5)                       not null,
   CXID                 NUMBER(10)                     default 0 not null,
   JSRQ                 DATE                            not null,
   MDFWDM               VARCHAR2(10)                    not null,
   JE                   NUMBER(14,2)                   default 0 not null
      constraint CKC_JE_HYK_YHQZ check (JE between 0 and 500000),
   JYDJJE               NUMBER(14,2)                   default 0
      constraint CKC_JYDJJE_HYK_YHQZ check (JYDJJE is null or (JYDJJE >= 0)),
   TM                   NUMBER(10),
   constraint PK_HYK_YHQZH primary key (HYID, YHQID, JSRQ, MDFWDM, CXID)
)
/

/*==============================================================*/
/* Table: HYK_YHQ_DZB                                           */
/*==============================================================*/
create table BFCRM10.HYK_YHQ_DZB  (
   HYKTYPE              NUMBER(10)                      not null,
   YHQID                NUMBER(10),
   constraint PK_HYK_YHQ_DZB primary key (HYKTYPE)
)
/

/*==============================================================*/
/* Table: HYK_YHQ_PLCKJL                                        */
/*==============================================================*/
create table BFCRM10.HYK_YHQ_PLCKJL  (
   JLBH                 NUMBER(10)                      not null,
   HYKTYPE              NUMBER(5)                       not null,
   KHID                 NUMBER(10),
   YHQID                NUMBER(10)                      not null,
   JSRQ                 DATE                            not null,
   CXID                 NUMBER(10)                      not null,
   ZY                   VARCHAR2(40),
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16)                    not null,
   DJSJ                 DATE                            not null,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(16),
   ZXRQ                 DATE,
   CZDD                 VARCHAR2(10),
   MDFWDM               VARCHAR2(10)                    not null,
   BJ_CHILD             NUMBER(5),
   DJLX                 NUMBER(5),
   constraint PK_HYK_YHQ_PLCKJL primary key (JLBH)
)
/

/*==============================================================*/
/* Table: HYK_YHQ_PLCKJLITEM                                    */
/*==============================================================*/
create table BFCRM10.HYK_YHQ_PLCKJLITEM  (
   JLBH                 NUMBER(10)                      not null,
   HYID                 NUMBER(10)                      not null,
   CKJE                 NUMBER(14,2)                    not null,
   BZ                   VARCHAR2(40),
   BJ_CHILD             NUMBER(5),
   HYKNO                VARCHAR2(20),
   INX                  NUMBER(10),
   HYKTYPE              NUMBER(10),
   constraint PK_HYK_YHQ_PLCKJLITEM primary key (JLBH, HYID)
)
/

/*==============================================================*/
/* Index: INX_HYK_CZKCKJL_HYID                                  */
/*==============================================================*/
create index BFCRM10.INX_HYK_CZKCKJL_HYID on BFCRM10.HYK_YHQ_PLCKJLITEM (
   HYID ASC
)
/

/*==============================================================*/
/* Table: HYK_YHQ_QTCLRBB                                       */
/*==============================================================*/
create table BFCRM10.HYK_YHQ_QTCLRBB  (
   RQ                   DATE                            not null,
   HYKTYPE              NUMBER(5)                       not null,
   YHQID                NUMBER(5)                       not null,
   MDID                 NUMBER(5)                       not null,
   MDFWDM               VARCHAR2(10)                    not null,
   SKTNO                VARCHAR2(6)                     not null,
   QTFQJE               NUMBER(14,2)                   default 0 not null,
   BKJE                 NUMBER(14,2),
   QKJE                 NUMBER(14,2),
   YEARMONTH            NUMBER(10),
   constraint PK_HYK_YHQ_QTCLRBB primary key (RQ, HYKTYPE, YHQID, MDID, MDFWDM, SKTNO)
)
/

/*==============================================================*/
/* Table: HYK_YHQ_RBB                                           */
/*==============================================================*/
create sequence BFCRM10.S_HYK_YHQ_RBB
/
create table BFCRM10.HYK_YHQ_RBB  (
   RQ                   DATE                            not null,
   HYKTYPE              NUMBER(5)                       not null,
   YHQID                NUMBER(5)                       not null,
   MDFWDM               VARCHAR2(10)                    not null,
   SQYE                 NUMBER(14,2)                   default 0 not null,
   CKJE                 NUMBER(14,2)                   default 0 not null,
   QKJE                 NUMBER(14,2),
   BKJE                 NUMBER(14,2),
   XFJE                 NUMBER(14,2)                   default 0 not null,
   TZJE                 NUMBER(14,2)                   default 0,
   QMYE                 NUMBER(14,2)                   default 0 not null,
   TM                   NUMBER(10),
   constraint PK_HYK_YHQ_RBB primary key (RQ, HYKTYPE, YHQID, MDFWDM)
)
/

/*==============================================================*/
/* Table: HYK_YHQ_RHZ                                           */
/*==============================================================*/
create table BFCRM10.HYK_YHQ_RHZ  (
   RQ                   DATE                            not null,
   HYID                 NUMBER(10)                      not null,
   YHQID                NUMBER(5)                       not null,
   SQYE                 NUMBER(14,2)                   default 0 not null,
   CKJE                 NUMBER(14,2)                   default 0 not null,
   QKJE                 NUMBER(14,2),
   BKJE                 NUMBER(14,2),
   XFJE                 NUMBER(14,2)                   default 0 not null,
   QMYE                 NUMBER(14,2)                   default 0 not null,
   TZJE                 NUMBER(14,2)                   default 0,
   constraint PK_HYK_YHQ_RHZ primary key (RQ, HYID, YHQID)
)
/

/*==============================================================*/
/* Table: HYK_YHQ_XFRBB                                         */
/*==============================================================*/
create sequence BFCRM10.S_HYK_YHQ_XFRBB
/
create table BFCRM10.HYK_YHQ_XFRBB  (
   RQ                   DATE                            not null,
   HYKTYPE              NUMBER(5)                       not null,
   YHQID                NUMBER(5)                       not null,
   MDFWDM               VARCHAR2(10)                    not null,
   MDID                 NUMBER(5)                       not null,
   SKTNO                VARCHAR2(6)                     not null,
   XFJE                 NUMBER(14,2)                   default 0 not null,
   TZJE                 NUMBER(14,2)                   default 0,
   YEARMONTH            NUMBER(10),
   TM                   NUMBER(10),
   constraint PK_HYK_YHQ_XFRBB primary key (RQ, HYKTYPE, YHQID, MDFWDM, MDID, SKTNO)
)
/

/*==============================================================*/
/* Table: HYK_YXQBDJL                                           */
/*==============================================================*/
create table BFCRM10.HYK_YXQBDJL  (
   CZJPJ_JLBH           NUMBER(10)                      not null,
   HYKTYPE              NUMBER(5),
   ZY                   VARCHAR2(40),
   XYXQ                 DATE                            not null,
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16)                    not null,
   DJSJ                 DATE                            not null,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(16),
   ZXRQ                 DATE,
   CZDD                 VARCHAR2(10),
   constraint PK_HYK_YXQBDJL primary key (CZJPJ_JLBH)
)
/

/*==============================================================*/
/* Table: HYK_YXQBDJLITEM                                       */
/*==============================================================*/
create table BFCRM10.HYK_YXQBDJLITEM  (
   CZJPJ_JLBH           NUMBER(10)                      not null,
   HYID                 NUMBER(10)                      not null,
   YYXQ                 DATE,
   HYKNO                VARCHAR2(20),
   BJ_CHILD             NUMBER(5),
   constraint PK_HYK_YXQBDJLITEM primary key (CZJPJ_JLBH, HYID)
)
/

/*==============================================================*/
/* Table: HYK_YXQYCGZ                                           */
/*==============================================================*/
create table BFCRM10.HYK_YXQYCGZ  (
   HYKTYPE              NUMBER(10)                      not null,
   QDJF                 FLOAT(50),
   QDXFJE               NUMBER(14,2)                    not null,
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16)                    not null,
   DJSJ                 DATE                            not null,
   YXQCD                VARCHAR2(4)                     not null,
   constraint PK_HYK_YXQYCGZ primary key (HYKTYPE)
)
/

/*==============================================================*/
/* Table: HYK_ZFJL                                              */
/*==============================================================*/
create table BFCRM10.HYK_ZFJL  (
   JLBH                 NUMBER(10)                      not null,
   HYKTYPE              NUMBER(5),
   ZY                   VARCHAR2(40),
   JE                   NUMBER(14,2)                   default 0 not null,
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16)                    not null,
   DJSJ                 DATE                            not null,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(16),
   ZXRQ                 DATE,
   BJ_HF                NUMBER(5),
   BGDDDM               VARCHAR2(20),
   constraint PK_HYK_ZFJL primary key (JLBH)
)
/

/*==============================================================*/
/* Table: HYK_ZFJLITEM                                          */
/*==============================================================*/
create table BFCRM10.HYK_ZFJLITEM  (
   JLBH                 NUMBER(10)                      not null,
   HYID                 NUMBER(10)                      not null,
   LJXFJE               NUMBER(14,2),
   LJJF                 FLOAT(50),
   WCLJF                FLOAT(50),
   YE                   NUMBER(14,2)                   default 0,
   JLJYE                NUMBER(14,2)                   default 0,
   HYKNO                VARCHAR2(20),
   BJ_CHILD             NUMBER(10),
   constraint PK_HYK_ZFJLITEM primary key (JLBH, HYID)
)
/

/*==============================================================*/
/* Index: HYK_ZFJLITEM_HYID                                     */
/*==============================================================*/
create index BFCRM10.HYK_ZFJLITEM_HYID on BFCRM10.HYK_ZFJLITEM (
   HYID ASC
)
/

/*==============================================================*/
/* Table: HYK_ZKXXJL                                            */
/*==============================================================*/
create sequence BFCRM10.S_HYK_ZKXXJL
/
create table BFCRM10.HYK_ZKXXJL  (
   XH                   NUMBER(9)                       not null,
   HYK_NO               VARCHAR2(16)                    not null,
   RQ                   DATE                            not null,
   HYID                 NUMBER(10),
   CLLX                 NUMBER(10),
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(16),
   CZDD                 VARCHAR2(10),
   constraint PK_HYK_ZKXXJL primary key (XH)
)
/

comment on column BFCRM10.HYK_ZKXXJL.CLLX is
'0 制卡，1验卡，2补磁'
/

/*==============================================================*/
/* Index: HYK_NO                                                */
/*==============================================================*/
create index BFCRM10.HYK_NO on BFCRM10.HYK_ZKXXJL (
   HYK_NO ASC
)
/

/*==============================================================*/
/* Table: HYK_ZQBDJL                                            */
/*==============================================================*/
create table BFCRM10.HYK_ZQBDJL  (
   JLBH                 NUMBER(10)                      not null,
   XFJLID               NUMBER(10)                      not null,
   HYID                 NUMBER(10)                      not null,
   DYSJ                 DATE                            not null,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(16),
   constraint PK_HYK_ZQBDJL primary key (JLBH)
)
/

/*==============================================================*/
/* Table: HYK_ZQFFJL                                            */
/*==============================================================*/
create table BFCRM10.HYK_ZQFFJL  (
   JLBH                 NUMBER(10)                      not null,
   HYKTYPE              NUMBER(10)                      not null,
   YHQID                NUMBER(5)                       not null,
   JSRQ                 DATE                            not null,
   MDFWDM               VARCHAR2(10)                    not null,
   DZJE                 NUMBER(14,2)                    not null,
   SL                   NUMBER(10)                      not null,
   CZDD                 VARCHAR2(10),
   ZY                   VARCHAR2(50),
   DJR                  NUMBER(5),
   DJRMC                VARCHAR2(16),
   DJSJ                 DATE,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(16),
   ZXRQ                 DATE,
   constraint PK_HYK_ZQFFJL primary key (JLBH)
)
/

/*==============================================================*/
/* Table: HYK_ZQFFJLITEM                                        */
/*==============================================================*/
create table BFCRM10.HYK_ZQFFJLITEM  (
   JLBH                 NUMBER(10)                      not null,
   HYID                 NUMBER(10)                      not null,
   HYK_NO               VARCHAR2(20)                    not null,
   JE                   NUMBER(14,2),
   DYSJ                 DATE,
   constraint PK_HYK_ZQFFJLITEM primary key (JLBH, HYID)
)
/

/*==============================================================*/
/* Table: HYK_ZRJL                                              */
/*==============================================================*/
create table BFCRM10.HYK_ZRJL  (
   JLBH                 NUMBER(10)                      not null,
   CZDD                 VARCHAR2(20)                    not null,
   GKID_OLD             NUMBER(10),
   GKID_NEW             NUMBER(10),
   HYID_OLD             NUMBER(10),
   HYID_NEW             NUMBER(10),
   HYKTYPE              NUMBER(10),
   SFZBH                VARCHAR2(20),
   SJHM                 VARCHAR2(20),
   HY_NAME              VARCHAR2(20),
   DJLX                 NUMBER(10),
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(10)                    not null,
   DJSJ                 DATE                            not null,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(16),
   ZXRQ                 DATE,
   BZ                   VARCHAR2(50),
   constraint PK_HYK_ZRJL primary key (JLBH)
)
/

/*==============================================================*/
/* Table: HYK_ZTBDJL                                            */
/*==============================================================*/
create table BFCRM10.HYK_ZTBDJL  (
   JLBH                 NUMBER(10)                      not null,
   NEW_STATUS           NUMBER(5)                      default 0 not null,
   ZY                   VARCHAR2(40),
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16)                    not null,
   DJSJ                 DATE                            not null,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(16),
   ZXRQ                 DATE,
   GBF                  NUMBER(14,2),
   BGDDDM               VARCHAR2(20),
   constraint PK_HYK_ZTBDJL primary key (JLBH)
)
/

/*==============================================================*/
/* Table: HYK_ZTBDJLITEM                                        */
/*==============================================================*/
create table BFCRM10.HYK_ZTBDJLITEM  (
   JLBH                 NUMBER(10)                      not null,
   HYID                 NUMBER(10)                      not null,
   OLD_STATUS           NUMBER(5)                      default 0,
   HYKNO                VARCHAR2(20),
   BJ_CHILD             NUMBER(5),
   constraint PK_HYK_ZTBDJLITEM primary key (JLBH, HYID)
)
/

/*==============================================================*/
/* Index: HYK_ZTBDJLITEM_HYID                                   */
/*==============================================================*/
create index BFCRM10.HYK_ZTBDJLITEM_HYID on BFCRM10.HYK_ZTBDJLITEM (
   HYID ASC
)
/

/*==============================================================*/
/* Table: HYK_ZTBGGZ                                            */
/*==============================================================*/
create table BFCRM10.HYK_ZTBGGZ  (
   HYKTYPE              NUMBER(10)                      not null,
   WSYSJ                NUMBER(5)                       not null,
   GZLX                 NUMBER(5)                       not null,
   constraint PK_HYK_ZTBGGZ primary key (HYKTYPE, GZLX)
)
/

/*==============================================================*/
/* Table: HYK_ZZJCCLJL                                          */
/*==============================================================*/
create table BFCRM10.HYK_ZZJCCLJL  (
   JLBH                 NUMBER(10)                      not null,
   HYID                 NUMBER(10)                      not null,
   JLBH_JJ              NUMBER(10),
   BJ_XFPZ              NUMBER(5),
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16)                    not null,
   DJSJ                 DATE                            not null,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(16),
   ZXRQ                 DATE,
   ZY                   VARCHAR2(50),
   constraint PK_HYK_ZZJCCLJL primary key (JLBH)
)
/

/*==============================================================*/
/* Index: HYK_ZZJCCLJL_HYID                                     */
/*==============================================================*/
create index BFCRM10.HYK_ZZJCCLJL_HYID on BFCRM10.HYK_ZZJCCLJL (
   HYID ASC
)
/

/*==============================================================*/
/* Table: HYK_ZZJCCLJLITEM                                      */
/*==============================================================*/
create table BFCRM10.HYK_ZZJCCLJLITEM  (
   JLBH                 NUMBER(10)                      not null,
   MDID                 NUMBER(10)                      not null,
   XPH                  NUMBER(10)                      not null,
   SKTNO                VARCHAR2(6)                     not null,
   XSJE                 NUMBER(14,2),
   XSRQ                 DATE,
   constraint PK_HYK_ZZJCCLJLITEM primary key (JLBH, MDID, XPH, SKTNO)
)
/

/*==============================================================*/
/* Table: HYNLDDY                                               */
/*==============================================================*/
create table BFCRM10.HYNLDDY  (
   NLDID                NUMBER(5)                       not null,
   NLDMC                VARCHAR2(30)                    not null,
   NL1                  NUMBER(5)                       not null,
   NL2                  NUMBER(5)                       not null,
   constraint PK_HYNLDDY primary key (NLDID)
)
/

/*==============================================================*/
/* Table: HYSQDEF_NEW                                           */
/*==============================================================*/
create table BFCRM10.HYSQDEF_NEW  (
   HYSQID               NUMBER(10)                      not null,
   HYSQMC               VARCHAR2(30)                    not null,
   BZ                   VARCHAR2(50),
   constraint PK_HYSQDEF_NEW primary key (HYSQID)
)
/

/*==============================================================*/
/* Table: HYTDJFDYD                                             */
/*==============================================================*/
create table BFCRM10.HYTDJFDYD  (
   JLBH                 NUMBER(10)                      not null,
   DJLX                 NUMBER(5)                      default 0 not null,
   KSRQ                 DATE                            not null,
   JSRQ                 DATE                            not null,
   MDID                 NUMBER(10)                      not null,
   STATUS               NUMBER(5),
   DJR                  NUMBER(10),
   DJRMC                VARCHAR2(16),
   DJSJ                 DATE,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(16),
   ZXRQ                 DATE,
   ZZR                  NUMBER(10),
   ZZRMC                VARCHAR2(16),
   ZZRQ                 DATE,
   BJ_BQJFBS            NUMBER(5),
   constraint PK_HYTDJFDYD primary key (JLBH)
)
/

/*==============================================================*/
/* Index: INDEX_HYTDJFDYD_RQ                                    */
/*==============================================================*/
create index INDEX_HYTDJFDYD_RQ on BFCRM10.HYTDJFDYD (
   KSRQ ASC,
   JSRQ ASC
)
/

/*==============================================================*/
/* Table: HYTDJFDYD_GZITEM                                      */
/*==============================================================*/
create table BFCRM10.HYTDJFDYD_GZITEM  (
   JLBH                 NUMBER(10)                      not null,
   GZBH                 NUMBER(10)                      not null,
   SJLX                 NUMBER(5)                       not null,
   SJNR                 VARCHAR2(20)                    not null,
   constraint PK_HYTDJFDYD_GZITEM primary key (JLBH, GZBH, SJLX, SJNR)
)
/

/*==============================================================*/
/* Table: HYTDJFDYD_GZSD                                        */
/*==============================================================*/
create table BFCRM10.HYTDJFDYD_GZSD  (
   JLBH                 NUMBER(10)                      not null,
   GZBH                 NUMBER(10)                      not null,
   JFBS                 FLOAT(50),
   BSFS                 NUMBER(5),
   GRPID                NUMBER(10),
   CLFS_SRDY            NUMBER(5),
   constraint PK_HYTDJFDYD_GZSD primary key (JLBH, GZBH)
)
/

/*==============================================================*/
/* Table: HYTHJL_KQ                                             */
/*==============================================================*/
create table BFCRM10.HYTHJL_KQ  (
   XFJLID               NUMBER(10)                      not null,
   CXID                 NUMBER(10)                      not null,
   YHQID                NUMBER(5)                       not null,
   YHQFFGZID            NUMBER(10)                      not null,
   XFLJFQFS             NUMBER(5)                       not null,
   BMDM                 VARCHAR2(10)                    not null,
   JSRQ                 DATE                            not null,
   ZXFJE_OLD            NUMBER(14,2)                    not null,
   SYXFJE_OLD           NUMBER(14,2)                    not null,
   FQJE_OLD             NUMBER(14,2)                    not null,
   SYXFJE_NEW           NUMBER(14,2)                    not null,
   THJE                 NUMBER(14,2)                    not null,
   KQJE                 NUMBER(14,2)                    not null,
   KQJE_SJ              NUMBER(14,2)                   default 0 not null,
   MDFWDM_YQ            VARCHAR2(10),
   MDFWDM_FQ            VARCHAR2(10),
   KQJE_SJ1             NUMBER(14,2),
   DJLX                 NUMBER(5)                      default 0,
   constraint PK_HYTHJL_KQ primary key (XFJLID, CXID, YHQID, YHQFFGZID, XFLJFQFS, BMDM, JSRQ)
)
/

/*==============================================================*/
/* Table: HYTHJL_TQ                                             */
/*==============================================================*/
create table BFCRM10.HYTHJL_TQ  (
   XFJLID               NUMBER(10)                      not null,
   HYID                 NUMBER(10)                      not null,
   YHQID                NUMBER(5)                       not null,
   CXID                 NUMBER(10)                     default 0 not null,
   JSRQ                 DATE                            not null,
   TQJE                 NUMBER(14,2)                    not null,
   MDFWDM               VARCHAR2(10),
   JF                   NUMBER(16,4)                   default 0,
   DHJF                 NUMBER(16,4),
   DHJE                 NUMBER(14,2),
   constraint PK_HYTHJL_TQ primary key (XFJLID, HYID, YHQID, CXID, JSRQ)
)
/

/*==============================================================*/
/* Table: HYTH_DJFQ_SPFTR                                       */
/*==============================================================*/
create table BFCRM10.HYTH_DJFQ_SPFTR  (
   XFJLID               NUMBER(10)                      not null,
   INX                  NUMBER(5)                       not null,
   YHQID                NUMBER(5)                       not null,
   XFJLID_OLD           NUMBER(10)                      not null,
   SHSPID               NUMBER(10),
   BMDM                 VARCHAR2(20)                    not null,
   CXID                 NUMBER(10)                      not null,
   YHQFFGZID            NUMBER(10)                      not null,
   XSJE_FQ              NUMBER(14,2)                    not null,
   FQJE                 NUMBER(14,2)                    not null,
   FQJE_SJ              NUMBER(14,2)                    not null,
   SHDM                 VARCHAR2(4)                     not null,
   MDID                 NUMBER(10)                      not null,
   HYID                 NUMBER(10)                      not null,
   CRMJZRQ              DATE                            not null,
   constraint PK_HYTH_DJFQ_SPFTR primary key (XFJLID, INX, YHQID, XFJLID_OLD)
)
/

/*==============================================================*/
/* Table: HYTSJF                                                */
/*==============================================================*/
create table BFCRM10.HYTSJF  (
   ID                   NUMBER(10)                      not null,
   RQ1                  DATE,
   RQ2                  DATE,
   JFBS                 FLOAT(50),
   constraint PK_HYTSJF primary key (ID)
)
/

/*==============================================================*/
/* Table: HYWHY                                                 */
/*==============================================================*/
create table BFCRM10.HYWHY  (
   ID                   NUMBER(5)                       not null,
   NR                   VARCHAR2(80)                    not null,
   SQLSTR               VARCHAR2(255),
   constraint PK_HYWHY primary key (ID)
)
/

/*==============================================================*/
/* Table: HYXFJL                                                */
/*==============================================================*/
create sequence BFCRM10.S_HYXFJL
/
create table BFCRM10.HYXFJL  (
   XFJLID               NUMBER(10)                      not null,
   SHDM                 VARCHAR2(4)                     not null,
   MDID                 NUMBER(10)                      not null,
   SKTNO                VARCHAR2(6)                     not null,
   JLBH                 NUMBER(10)                      not null,
   DJLX                 NUMBER(5)                       not null,
   XFJLID_OLD           NUMBER(10),
   HYID                 NUMBER(10),
   HYID_FQ              NUMBER(10),
   HYID_TQ              NUMBER(10),
   SKYDM                VARCHAR2(8),
   XFSJ                 DATE                            not null,
   JZRQ                 DATE,
   CRMJZRQ              DATE,
   SCSJ                 DATE,
   XSSL                 NUMBER(16,4)                   default 0,
   JE                   NUMBER(14,2)                   default 0 not null,
   ZK                   NUMBER(14,2)                   default 0 not null,
   ZK_HY                NUMBER(14,2)                    not null,
   JFDYDBH              NUMBER(10),
   CZJE                 NUMBER(14,2)                    not null,
   JF                   FLOAT(50)                       not null,
   BJ_HTBSK             NUMBER(5)                      default 0 not null,
   XFRQ_FQ              DATE,
   JFBS                 FLOAT(50)                      default 1 not null,
   TDJFDYDBH            NUMBER(10),
   BSFS                 NUMBER(10),
   HYKNO                VARCHAR2(20),
   HYKNO_FQ             VARCHAR2(20),
   PGRYID               NUMBER(10),
   FXDW                 NUMBER(10),
   BJ_CHILD             NUMBER(5),
   VIPTYPE              NUMBER(5),
   XSJL                 NUMBER(16,4),
   BJ_SL                NUMBER(5),
   TM                   NUMBER(10),
   constraint PK_HYXFJL primary key (XFJLID)
)
/

/*==============================================================*/
/* Index: HYXFJL_MAIN                                           */
/*==============================================================*/
create unique index BFCRM10.HYXFJL_MAIN on BFCRM10.HYXFJL (
   MDID ASC,
   SKTNO ASC,
   JLBH ASC
)
/

/*==============================================================*/
/* Index: HYXFJL_HYJF                                           */
/*==============================================================*/
create index BFCRM10.HYXFJL_HYJF on BFCRM10.HYXFJL (
   HYID ASC,
   MDID ASC,
   XFSJ ASC
)
/

/*==============================================================*/
/* Index: HYXFJL_HYFQ                                           */
/*==============================================================*/
create index BFCRM10.HYXFJL_HYFQ on BFCRM10.HYXFJL (
   HYID_FQ ASC,
   MDID ASC,
   XFSJ ASC
)
/

/*==============================================================*/
/* Index: HYXFJL_XFSJ                                           */
/*==============================================================*/
create index HYXFJL_XFSJ on BFCRM10.HYXFJL (
   XFSJ ASC
)
/

/*==============================================================*/
/* Index: HYXFJL_JZRQ                                           */
/*==============================================================*/
create index HYXFJL_JZRQ on BFCRM10.HYXFJL (
   CRMJZRQ ASC
)
/

/*==============================================================*/
/* Table: HYXFJL_BSK                                            */
/*==============================================================*/
create table BFCRM10.HYXFJL_BSK  (
   XFJLID               NUMBER(10)                      not null,
   BJ_JF                NUMBER(5)                       not null,
   BJ_FQ                NUMBER(5)                       not null,
   CRMJZRQ_OLD          DATE                            not null,
   CRMJZRQ_JF           DATE,
   CRMJZRQ_FQ           DATE,
   constraint PK_HYXFJL_BSK primary key (XFJLID)
)
/

/*==============================================================*/
/* Table: HYXFJL_FQ                                             */
/*==============================================================*/
create table BFCRM10.HYXFJL_FQ  (
   XFJLID               NUMBER(10)                      not null,
   CXID                 NUMBER(10)                      not null,
   YHQID                NUMBER(5)                       not null,
   YHQFFGZID            NUMBER(10)                      not null,
   XFLJFQFS             NUMBER(5)                       not null,
   BMDM                 VARCHAR2(10)                    not null,
   JSRQ                 DATE                            not null,
   ZXFJE_OLD            NUMBER(14,2)                   default 0 not null,
   SYXFJE_OLD           NUMBER(14,2)                   default 0 not null,
   FQJE_OLD             NUMBER(14,2)                   default 0 not null,
   ZXFJE                NUMBER(14,2)                   default 0 not null,
   SYXFJE               NUMBER(14,2)                   default 0 not null,
   FQJE                 NUMBER(14,2)                   default 0 not null,
   THJE                 NUMBER(14,2)                   default 0 not null,
   KQJE                 NUMBER(14,2)                   default 0 not null,
   HYID_ZQ              NUMBER(10),
   DJLX                 NUMBER(5)                      default 0,
   FQJE_WX              NUMBER(14,2)                   default 0 not null,
   constraint PK_HYXFJL_FQ primary key (XFJLID, CXID, YHQID, YHQFFGZID, XFLJFQFS, BMDM, JSRQ)
)
/

/*==============================================================*/
/* Table: HYXFJL_FQDM                                           */
/*==============================================================*/
create table BFCRM10.HYXFJL_FQDM  (
   XFJLID               NUMBER(10)                      not null,
   YHQCODE              VARCHAR2(20)                    not null,
   YHQID                NUMBER(5)                       not null,
   YHQMZ                NUMBER(14,2)                   default 0 not null,
   CXID                 NUMBER(10),
   constraint PK_HYXFJL_FQDM primary key (XFJLID, YHQCODE)
)
/

/*==============================================================*/
/* Table: HYXFJL_FQ_ZQITEM                                      */
/*==============================================================*/
create table BFCRM10.HYXFJL_FQ_ZQITEM  (
   XFJLID               NUMBER(10)                      not null,
   CXHDBH               NUMBER(10)                      not null,
   YHQID                NUMBER(5)                       not null,
   HYID_ZQ              NUMBER(10)                      not null,
   constraint PK_HYXFJL_FQ_ZQITEM primary key (XFJLID, CXHDBH, YHQID, HYID_ZQ)
)
/

/*==============================================================*/
/* Table: HYXFJL_SP                                             */
/*==============================================================*/
create sequence BFCRM10.S_HYXFJL_SP
/
create table BFCRM10.HYXFJL_SP  (
   XFJLID               NUMBER(10)                      not null,
   INX                  NUMBER(5)                       not null,
   SHSPID               NUMBER(10),
   BMDM                 VARCHAR2(10),
   SPDM                 VARCHAR2(13)                    not null,
   XSSL                 NUMBER(16,4)                   default 0,
   XSJE                 NUMBER(14,2)                    not null,
   ZKJE                 NUMBER(14,2)                    not null,
   ZKJE_HY              NUMBER(14,2)                    not null,
   THJE                 NUMBER(14,2)                   default 0 not null,
   BJ_JF                NUMBER(5)                       not null,
   XSJE_JF              NUMBER(14,2)                   default 0 not null,
   XSJE_FQ              NUMBER(14,2)                   default 0 not null,
   JFDYDBH              NUMBER(10),
   JF                   FLOAT(50)                      default 0 not null,
   JFJS                 NUMBER(16,4)                   default 0 not null,
   ZKDYDBH              NUMBER(10),
   ZKL                  NUMBER(16,4)                   default 1,
   HTH                  VARCHAR2(10),
   TM                   NUMBER(10),
   BJ_JFBS              NUMBER(5),
   BJ_BCJHD             NUMBER(5)                      default 0,
   BS                   FLOAT(50),
   XSJE_SYJE            NUMBER(14,2),
   FTBL                 NUMBER(8,4),
   JFFBGZ               NUMBER(10),
   THJE_JF              NUMBER(14,2),
   THJF                 NUMBER(16,4),
   INX_OLD              NUMBER(5),
   BJ_BCJYQ             NUMBER(5)                      default 0,
   BJ_BCJMJ             NUMBER(5)                      default 0,
   constraint PK_HYXFJL_SP primary key (XFJLID, INX)
)
/

/*==============================================================*/
/* Index: HYXFJL_XFSP                                           */
/*==============================================================*/
create index BFCRM10.HYXFJL_XFSP on BFCRM10.HYXFJL_SP (
   XFJLID ASC,
   SHSPID ASC
)
/

/*==============================================================*/
/* Table: HYXFJL_SP_FQ                                          */
/*==============================================================*/
create table BFCRM10.HYXFJL_SP_FQ  (
   XFJLID               NUMBER(10)                     default 0 not null,
   INX                  NUMBER(5)                       not null,
   YHQID                NUMBER(5)                       not null,
   BMDM                 VARCHAR2(20)                    not null,
   SPDM                 VARCHAR2(13)                    not null,
   SHSPID               NUMBER(10),
   YHQFFDBH             NUMBER(10),
   CXID                 NUMBER(10)                      not null,
   XFLJFQFS             NUMBER(5)                       not null,
   YHQFFGZID            NUMBER(10)                      not null,
   XSSL                 NUMBER(16,4)                    not null,
   XSJE                 NUMBER(14,2)                    not null,
   XSJE_FQ              NUMBER(14,2)                    not null,
   FQJE                 NUMBER(14,2)                    not null,
   FQJE_SJ              NUMBER(14,2)                   default 0 not null,
   SHZFFS               NUMBER(10),
   DJLX                 NUMBER(5),
   constraint PK_HYXFJL_SP_FQ primary key (XFJLID, INX, YHQID)
)
/

/*==============================================================*/
/* Index: INX_HYXFJL_SP_FQ                                      */
/*==============================================================*/
create index BFCRM10.INX_HYXFJL_SP_FQ on BFCRM10.HYXFJL_SP_FQ (
   XFJLID ASC,
   YHQID ASC,
   SHSPID ASC
)
/

/*==============================================================*/
/* Table: HYXFJL_SP_MBJZ                                        */
/*==============================================================*/
create table BFCRM10.HYXFJL_SP_MBJZ  (
   XFJLID               NUMBER(10)                      not null,
   INX                  NUMBER(5)                       not null,
   BMDM                 VARCHAR2(20)                    not null,
   SPDM                 VARCHAR2(13)                    not null,
   SHSPID               NUMBER(10),
   MBJZDBH              NUMBER(10),
   CXID                 NUMBER(10)                      not null,
   XFLJMJFS             NUMBER(5)                       not null,
   MBJZGZID             NUMBER(10)                      not null,
   XSSL                 NUMBER(16,4)                    not null,
   XSJE                 NUMBER(14,2)                    not null,
   XSJE_MBJZ            NUMBER(14,2)                    not null,
   MBJZJE               NUMBER(14,2)                   default 0 not null,
   SHSBID               NUMBER(10),
   SHHTID               NUMBER(10),
   GHSDM                VARCHAR2(10),
   MJJQ                 NUMBER(1),
   constraint PK_HYXFJL_SP_MBJZ primary key (XFJLID, INX)
)
/

/*==============================================================*/
/* Table: HYXFJL_SP_YQFT                                        */
/*==============================================================*/
create table BFCRM10.HYXFJL_SP_YQFT  (
   XFJLID               NUMBER(10)                     default 0 not null,
   INX                  NUMBER(5)                       not null,
   YHQID                NUMBER(5)                       not null,
   BMDM                 VARCHAR2(20)                    not null,
   SPDM                 VARCHAR2(13)                    not null,
   SHSPID               NUMBER(10)                      not null,
   YHQSYDBH             NUMBER(10),
   CXID                 NUMBER(10),
   YHQSYGZID            NUMBER(10),
   XSJE                 NUMBER(14,2)                    not null,
   XSJE_YQ              NUMBER(14,2)                    not null,
   YQJE                 NUMBER(14,2)                    not null,
   BJ_FQ                NUMBER(5)                       not null,
   constraint PK_HYXFJL_SP_YQFT primary key (XFJLID, INX, YHQID)
)
/

/*==============================================================*/
/* Index: HYXFJL_SPYQFT                                         */
/*==============================================================*/
create index BFCRM10.HYXFJL_SPYQFT on BFCRM10.HYXFJL_SP_YQFT (
   XFJLID ASC,
   SHSPID ASC,
   YHQID ASC
)
/

/*==============================================================*/
/* Table: HYXFJL_SP_ZFFS                                        */
/*==============================================================*/
create table BFCRM10.HYXFJL_SP_ZFFS  (
   XFJLID               NUMBER(10)                     default 0 not null,
   INX                  NUMBER(5)                       not null,
   SHZFFSID             NUMBER(10)                      not null,
   JE                   NUMBER(14,2)                   default 0 not null,
   BJ_JF                NUMBER(5)                       not null,
   BJ_FQ                NUMBER(5)                       not null,
   YHQID                NUMBER(10),
   JFBL                 FLOAT(50),
   SYJE                 NUMBER(14,2),
   constraint PK_HYXFJL_SP_ZFFS primary key (XFJLID, INX, SHZFFSID)
)
/

/*==============================================================*/
/* Table: HYXFJL_SP_ZFFS_FQ                                     */
/*==============================================================*/
create table BFCRM10.HYXFJL_SP_ZFFS_FQ  (
   XFJLID               NUMBER(10)                      not null,
   SPINX                NUMBER(5)                       not null,
   INX                  NUMBER(5)                       not null,
   SHZFFSID             NUMBER(5)                       not null,
   BANK_ZFINX           NUMBER(5)                      default 0 not null,
   BANK_ID              NUMBER(5)                      default -1 not null,
   BANK_KH              VARCHAR2(20)                   default ' ' not null,
   YHQID                NUMBER(5)                       not null,
   SHSPID               NUMBER(10)                      not null,
   SPDM                 VARCHAR2(13)                    not null,
   BMDM                 VARCHAR2(10)                    not null,
   XSJE                 NUMBER(14,2)                   default 0 not null,
   CXID                 NUMBER(10)                      not null,
   YHQFFDBH             NUMBER(10)                      not null,
   YHQFFGZID            NUMBER(10)                      not null,
   XFLJFQFS             NUMBER(5)                      default 0 not null,
   constraint PK_HYXFJL_SP_ZFFS_FQ primary key (XFJLID, SPINX, INX)
)
/

/*==============================================================*/
/* Table: HYXFJL_SP_ZFFS_MBJZ                                   */
/*==============================================================*/
create table BFCRM10.HYXFJL_SP_ZFFS_MBJZ  (
   XFJLID               NUMBER(10)                     default 0 not null,
   INX                  NUMBER(5)                       not null,
   SHZFFSID             NUMBER(10)                      not null,
   JE                   NUMBER(14,2)                   default 0 not null,
   BJ_JF                NUMBER(5)                       not null,
   BJ_FQ                NUMBER(5)                       not null,
   YHQID                NUMBER(10),
   constraint PK_HYXFJL_SP_ZFFS_MBJZ primary key (XFJLID, INX)
)
/

/*==============================================================*/
/* Table: HYXFJL_YHKZF                                          */
/*==============================================================*/
create table BFCRM10.HYXFJL_YHKZF  (
   XFJLID               NUMBER(10)                      not null,
   INX                  NUMBER(10)                      not null,
   BANK_ID              NUMBER(5)                       not null,
   BANK_KH              VARCHAR2(20)                    not null,
   SHZFFSID             NUMBER(10)                      not null,
   JE                   NUMBER(14,2)                    not null,
   constraint PK_HYXFJL_YHKZF primary key (XFJLID, BANK_KH)
)
/

/*==============================================================*/
/* Table: HYXFJL_ZFFS                                           */
/*==============================================================*/
create table BFCRM10.HYXFJL_ZFFS  (
   XFJLID               NUMBER(10)                      not null,
   INX                  NUMBER(5)                       not null,
   ZFFSID               NUMBER(10),
   ZFFSDM               VARCHAR2(10)                    not null,
   JE                   NUMBER(14,2)                    not null,
   BJ_JF                NUMBER(5)                       not null,
   BJ_FQ                NUMBER(5)                       not null,
   YHQID                NUMBER(5),
   JFBL                 FLOAT(50),
   constraint PK_HYXFJL_ZFFS primary key (XFJLID, INX)
)
/

/*==============================================================*/
/* Index: HYXFJL_ZFFS                                           */
/*==============================================================*/
create index BFCRM10.HYXFJL_ZFFS on BFCRM10.HYXFJL_ZFFS (
   XFJLID ASC,
   ZFFSID ASC
)
/

/*==============================================================*/
/* Table: HYXFJL_ZFFS_FQ                                        */
/*==============================================================*/
create table BFCRM10.HYXFJL_ZFFS_FQ  (
   XFJLID               NUMBER(10)                      not null,
   CXID                 NUMBER(10)                      not null,
   YHQID                NUMBER(5)                       not null,
   YHQFFGZID            NUMBER(10)                      not null,
   XFLJFQFS             NUMBER(5)                      default 0 not null,
   SHZFFSID             NUMBER(5)                       not null,
   BANK_ZFINX           NUMBER(5)                      default 0 not null,
   JSRQ                 DATE                            not null,
   BANK_ID              NUMBER(5)                      default -1 not null,
   BANK_KH              VARCHAR2(20)                   default ' ' not null,
   ZXFJE                NUMBER(14,2)                   default 0 not null,
   SYXFJE               NUMBER(14,2)                   default 0 not null,
   FQJE                 NUMBER(14,2)                   default 0 not null,
   FQJE_SJ              NUMBER(14,2)                   default 0 not null,
   constraint PK_HYXFJL_ZFFS_FQ primary key (XFJLID, CXID, YHQID, YHQFFGZID, XFLJFQFS, SHZFFSID, BANK_ZFINX, JSRQ)
)
/

/*==============================================================*/
/* Table: HYXFJL_ZFFS_ZSLP                                      */
/*==============================================================*/
create table BFCRM10.HYXFJL_ZFFS_ZSLP  (
   XFJLID               NUMBER(10)                      not null,
   CXID                 NUMBER(10)                      not null,
   YHQID                NUMBER(5)                       not null,
   YHQFFGZID            NUMBER(10)                      not null,
   XFLJFQFS             NUMBER(5)                      default 0 not null,
   SHZFFSID             NUMBER(5)                       not null,
   BANK_ZFINX           NUMBER(5)                      default 0 not null,
   LPDM                 VARCHAR2(10)                    not null,
   BANK_ID              NUMBER(5)                      default -1 not null,
   BANK_KH              VARCHAR2(20)                    not null,
   LPSL                 NUMBER(5)                      default 0 not null,
   constraint PK_HYXFJL_ZFFS_ZSLP primary key (XFJLID, CXID, YHQID, YHQFFGZID, XFLJFQFS, SHZFFSID, BANK_ZFINX, LPDM)
)
/

/*==============================================================*/
/* Table: HYXFJL_ZSLP                                           */
/*==============================================================*/
create table BFCRM10.HYXFJL_ZSLP  (
   XFJLID               NUMBER(10)                      not null,
   CXID                 NUMBER(10)                      not null,
   YHQID                NUMBER(5)                       not null,
   YHQFFGZID            NUMBER(10)                      not null,
   XFLJFQFS             NUMBER(5)                       not null,
   BMDM                 VARCHAR2(10)                    not null,
   LPDM                 VARCHAR2(10)                    not null,
   LPSL                 NUMBER(5)                      default 0 not null,
   constraint PK_HYXFJL_ZSLP primary key (XFJLID, CXID, YHQID, YHQFFGZID, XFLJFQFS, BMDM)
)
/

/*==============================================================*/
/* Table: HYXLKHGH                                              */
/*==============================================================*/
create table BFCRM10.HYXLKHGH  (
   JLBH                 NUMBER(10)                      not null,
   HYKTYPE              NUMBER(10),
   HYK_NO               VARCHAR2(30),
   HYID                 NUMBER(10),
   BGDDDM               VARCHAR2(20),
   JF                   FLOAT(50),
   DJR                  NUMBER(10),
   DJRMC                VARCHAR2(16),
   DJSJ                 DATE,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(16),
   ZXRQ                 DATE,
   BJ_BK                NUMBER(10),
   constraint PK_HYXLKHGH primary key (JLBH)
)
/

/*==============================================================*/
/* Index: HYXLKHGH_HYID                                         */
/*==============================================================*/
create index BFCRM10.HYXLKHGH_HYID on BFCRM10.HYXLKHGH (
   HYID ASC
)
/

/*==============================================================*/
/* Table: HYXLKHGHGZ                                            */
/*==============================================================*/
create table BFCRM10.HYXLKHGHGZ  (
   HYKTYPE              NUMBER(5)                       not null,
   JFQD                 FLOAT(50),
   JFZD                 FLOAT(50),
   SJKJJF               FLOAT(50),
   constraint PK_HYXLKHGHGZ primary key (HYKTYPE)
)
/

/*==============================================================*/
/* Table: HYXLKHGH_ITEM                                         */
/*==============================================================*/
create table BFCRM10.HYXLKHGH_ITEM  (
   JLBH                 NUMBER(10)                      not null,
   FDBH                 NUMBER(10)                      not null,
   VIP_NO               VARCHAR2(8)                     not null,
   VIP_NAME             VARCHAR2(20),
   SEX                  NUMBER(5),
   SFZBH                VARCHAR2(18),
   PHONE                VARCHAR2(18),
   POINT                FLOAT(50),
   LJPOINT              FLOAT(50),
   constraint PK_HYXLKHGH_ITEM primary key (JLBH, FDBH, VIP_NO)
)
/

/*==============================================================*/
/* Table: HYXXXMDEF                                             */
/*==============================================================*/
create sequence BFCRM10.S_HYXXXMDEF
/
create table BFCRM10.HYXXXMDEF  (
   XMID                 NUMBER(10)                      not null,
   XMLX                 NUMBER(5)                       not null,
   SXH                  NUMBER(5)                       not null,
   NR                   VARCHAR2(30)                    not null,
   TM                   NUMBER(10),
   constraint PK_HYXXXMDEF primary key (XMID)
)
/

/*==============================================================*/
/* Index: HYXXXMDEF_LX                                          */
/*==============================================================*/
create index BFCRM10.HYXXXMDEF_LX on BFCRM10.HYXXXMDEF (
   XMLX ASC,
   SXH ASC
)
/

/*==============================================================*/
/* Table: HYZKDYD                                               */
/*==============================================================*/
create table BFCRM10.HYZKDYD  (
   ZKDYDBH              NUMBER(10)                      not null,
   HYKTYPE              NUMBER(5),
   SHDM                 VARCHAR2(4),
   SHBMID               NUMBER(10),
   SHBMDM               VARCHAR2(20)                    not null,
   RQ1                  DATE,
   RQ2                  DATE,
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16)                    not null,
   DJSJ                 DATE                            not null,
   SHR                  NUMBER(10),
   SHRMC                VARCHAR2(16),
   SHSJ                 DATE,
   QDR                  NUMBER(10),
   QDRMC                VARCHAR2(16),
   QDSJ                 DATE,
   STATUS               NUMBER(5)                       not null,
   DJLX                 NUMBER(5)                       not null,
   ZZRQ                 DATE,
   ZZR                  NUMBER(10),
   ZZRMC                VARCHAR2(16),
   YXCLBJ               NUMBER(5)                      default 0 not null,
   constraint PK_HYZKDYD primary key (ZKDYDBH)
)
/

/*==============================================================*/
/* Table: HYZKDYDJ                                              */
/*==============================================================*/
create table BFCRM10.HYZKDYDJ  (
   HYKTYPE              NUMBER(5)                       not null,
   SHDM                 VARCHAR2(4)                     not null,
   SHBMDM               VARCHAR2(20)                    not null,
   ZKDYDBH              NUMBER(10)                      not null,
   OLD_ZKDYDBH          NUMBER(10),
   NEW_ZKDYDBH          NUMBER(10),
   constraint PK_HYZKDYDJ primary key (HYKTYPE, SHDM, SHBMDM)
)
/

/*==============================================================*/
/* Table: HYZKDYD_BM                                            */
/*==============================================================*/
create table BFCRM10.HYZKDYD_BM  (
   ZKDYDBH              NUMBER(10)                      not null,
   INX                  NUMBER(5)                       not null,
   SHBMID               NUMBER(10)                      not null,
   ZKL                  FLOAT(50)                       not null,
   constraint PK_HYZKDYD_BM primary key (ZKDYDBH, INX, SHBMID)
)
/

/*==============================================================*/
/* Table: HYZKDYD_FD                                            */
/*==============================================================*/
create table BFCRM10.HYZKDYD_FD  (
   ZKDYDBH              NUMBER(10)                      not null,
   INX                  NUMBER(5)                       not null,
   SD                   RAW(96)                         not null,
   YXJ                  NUMBER(5)                       not null,
   CLSX                 VARCHAR2(10)                    not null,
   constraint PK_HYZKDYD_FD primary key (ZKDYDBH, INX)
)
/

/*==============================================================*/
/* Table: HYZKDYD_HT                                            */
/*==============================================================*/
create table BFCRM10.HYZKDYD_HT  (
   ZKDYDBH              NUMBER(10)                      not null,
   INX                  NUMBER(5)                       not null,
   SHHTID               NUMBER(10)                      not null,
   ZKL                  FLOAT(50)                       not null,
   constraint PK_HYZKDYD_HT primary key (ZKDYDBH, INX, SHHTID)
)
/

/*==============================================================*/
/* Table: HYZKDYD_SB                                            */
/*==============================================================*/
create table BFCRM10.HYZKDYD_SB  (
   ZKDYDBH              NUMBER(10)                      not null,
   INX                  NUMBER(5)                       not null,
   SHSBID               NUMBER(10)                      not null,
   ZKL                  FLOAT(50)                       not null,
   constraint PK_HYZKDYD_SB primary key (ZKDYDBH, INX, SHSBID)
)
/

/*==============================================================*/
/* Table: HYZKDYD_SP                                            */
/*==============================================================*/
create table BFCRM10.HYZKDYD_SP  (
   ZKDYDBH              NUMBER(10)                      not null,
   INX                  NUMBER(5)                       not null,
   SHSPID               NUMBER(10)                      not null,
   ZKL                  FLOAT(50)                       not null,
   constraint PK_HYZKDYD_SP primary key (ZKDYDBH, INX, SHSPID)
)
/

/*==============================================================*/
/* Table: HYZKDYD_SPFL                                          */
/*==============================================================*/
create table BFCRM10.HYZKDYD_SPFL  (
   ZKDYDBH              NUMBER(10)                      not null,
   INX                  NUMBER(5)                       not null,
   SHSPFLID             NUMBER(10)                      not null,
   ZKL                  FLOAT(50)                       not null,
   constraint PK_HYZKDYD_SPFL primary key (ZKDYDBH, INX, SHSPFLID)
)
/

/*==============================================================*/
/* Table: HYZLXDY                                               */
/*==============================================================*/
create table BFCRM10.HYZLXDY  (
   HYZLXID              NUMBER(10)                      not null,
   HYZLXDM              VARCHAR2(20)                    not null,
   HYZLXMC              VARCHAR2(30),
   BJ_MJ                NUMBER(5),
   constraint PK_HYZLXDY primary key (HYZLXID)
)
/

/*==============================================================*/
/* Table: JFBSGZ                                                */
/*==============================================================*/
create table BFCRM10.JFBSGZ  (
   GZID                 NUMBER(10)                      not null,
   GZMC                 VARCHAR2(50)                    not null,
   constraint PK_JFBSGZ primary key (GZID)
)
/

/*==============================================================*/
/* Table: JFBSGZITEM                                            */
/*==============================================================*/
create table BFCRM10.JFBSGZITEM  (
   GZID                 NUMBER(10)                      not null,
   XSJE                 NUMBER(14,2)                    not null,
   BS                   NUMBER(14,2)                   default 1 not null,
   constraint PK_JFBSGZITEM primary key (GZID, XSJE)
)
/

/*==============================================================*/
/* Table: JFCXFHGZ                                              */
/*==============================================================*/
create table BFCRM10.JFCXFHGZ  (
   CXFHGZID             NUMBER(10)                      not null,
   CXFHGZMC             VARCHAR2(50)                    not null,
   BJ_JFJS              NUMBER(5),
   JFXX                 FLOAT(50),
   JFSX                 FLOAT(50),
   constraint PK_JFCXFHGZ primary key (CXFHGZID)
)
/

/*==============================================================*/
/* Table: JSZLD                                                 */
/*==============================================================*/
create table BFCRM10.JSZLD  (
   JLBH                 NUMBER(10)                      not null,
   CXHDBH               NUMBER(10)                      not null,
   SHDM                 VARCHAR2(6)                     not null,
   SHBMID               NUMBER(10)                      not null,
   SHBMDM               VARCHAR2(20)                    not null,
   YXCLBJ               NUMBER(5)                      default 0 not null,
   RQ1                  DATE,
   RQ2                  DATE,
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(20)                    not null,
   DJSJ                 DATE                            not null,
   SHR                  NUMBER(10),
   SHRMC                VARCHAR2(20),
   SHSJ                 DATE,
   QDR                  NUMBER(10),
   QDRMC                VARCHAR2(20),
   QDSJ                 DATE,
   STATUS               NUMBER(5)                       not null,
   ZZRQ                 DATE,
   ZZR                  NUMBER(10),
   ZZRMC                VARCHAR2(20),
   LJFS                 NUMBER(5)                      default 0,
   constraint PK_JSZLD primary key (JLBH)
)
/

/*==============================================================*/
/* Table: JSZLDJ                                                */
/*==============================================================*/
create table BFCRM10.JSZLDJ  (
   CXHDBH               NUMBER(10)                      not null,
   SHDM                 VARCHAR2(6)                     not null,
   SHBMDM               VARCHAR2(20)                    not null,
   ZXJLBH_PT            NUMBER(10),
   LJFS_PT              NUMBER(5),
   ZXJLBH_YX            NUMBER(10),
   LJFS_YX              NUMBER(5),
   constraint PK_JSZLDJ primary key (CXHDBH, SHDM, SHBMDM)
)
/

/*==============================================================*/
/* Table: JSZLD_FD                                              */
/*==============================================================*/
create table BFCRM10.JSZLD_FD  (
   JLBH                 NUMBER(10)                      not null,
   INX                  NUMBER(5)                       not null,
   SD                   RAW(48),
   constraint PK_JSZLD_FD primary key (JLBH, INX)
)
/

/*==============================================================*/
/* Table: JSZLD_FDSJ                                            */
/*==============================================================*/
create table BFCRM10.JSZLD_FDSJ  (
   JLBH                 NUMBER(10)                      not null,
   INX                  NUMBER(5)                       not null,
   DAY_WEEK             NUMBER(5)                       not null,
   MIN_DAY1             NUMBER(5)                       not null,
   MIN_DAY2             NUMBER(5),
   constraint PK_JSZLD_FDSJ primary key (JLBH, INX, DAY_WEEK, MIN_DAY1)
)
/

/*==============================================================*/
/* Table: JSZLD_GZITEM                                          */
/*==============================================================*/
create table BFCRM10.JSZLD_GZITEM  (
   JLBH                 NUMBER(10)                      not null,
   INX                  NUMBER(5)                       not null,
   GZBH                 NUMBER(5)                       not null,
   SJLX                 NUMBER(5)                       not null,
   SJNR                 NUMBER(10)                      not null,
   XH                   NUMBER(10),
   constraint PK_JSZLD_GZITEM primary key (JLBH, INX, GZBH, SJLX, SJNR)
)
/

/*==============================================================*/
/* Table: JSZLD_GZSD                                            */
/*==============================================================*/
create table BFCRM10.JSZLD_GZSD  (
   JLBH                 NUMBER(10)                      not null,
   INX                  NUMBER(5)                       not null,
   GZBH                 NUMBER(5)                       not null,
   CLFS_BM              NUMBER(5)                      default 0 not null,
   CLFS_SPFL            NUMBER(5)                      default 0 not null,
   CLFS_SPSB            NUMBER(5)                      default 0 not null,
   CLFS_HT              NUMBER(5)                      default 0 not null,
   CLFS_HYKLX           NUMBER(5),
   BJ_CJ                NUMBER(1)                      default 1 not null,
   GZID                 NUMBER(10),
   CLFS_SP              NUMBER(5)                      default 0 not null,
   CLFS_FXDW            NUMBER(5),
   CLFS_HYZ             NUMBER(5),
   constraint PK_JSZLD_GZSD primary key (JLBH, INX, GZBH)
)
/

/*==============================================================*/
/* Table: JSZLD_SP                                              */
/*==============================================================*/
create table BFCRM10.JSZLD_SP  (
   JLBH                 NUMBER(10)                      not null,
   INX                  NUMBER(5)                       not null,
   SHSPID               NUMBER(10)                      not null,
   GZID                 NUMBER(10)                      not null,
   XH                   NUMBER(10),
   BJ_CJ                NUMBER(5),
   constraint PK_JSZLD_SP primary key (JLBH, INX, SHSPID)
)
/

/*==============================================================*/
/* Table: JXCDB                                                 */
/*==============================================================*/
create sequence BFCRM10.S_JXCDB
/
create table BFCRM10.JXCDB  (
   MDID                 NUMBER(10)                      not null,
   MDDM                 VARCHAR2(10)                    not null,
   IP                   VARCHAR2(20)                    not null,
   PORT                 VARCHAR2(10)                    not null,
   DBNAME               VARCHAR2(10)                    not null,
   USERNAME             VARCHAR2(20)                    not null,
   PASSWORD             VARCHAR2(20),
   MDLX                 NUMBER(5),
   TM                   NUMBER(5),
   BJ_BH                NUMBER(5),
   constraint PK_JXCDB primary key (MDID)
)
/

/*==============================================================*/
/* Table: JXCRHZ                                                */
/*==============================================================*/
create table BFCRM10.JXCRHZ  (
   RQ                   DATE                            not null,
   MDID                 NUMBER(10)                      not null,
   XFJE_JXC             NUMBER(14,2),
   constraint PK_JXCRHZ primary key (RQ, MDID)
)
/

/*==============================================================*/
/* Table: KCK_XFBM                                              */
/*==============================================================*/
create table BFCRM10.KCK_XFBM  (
   CZKHM                VARCHAR2(20)                    not null,
   KSRQ                 DATE                            not null,
   JSRQ                 DATE                            not null,
   SHBMID               NUMBER(10)                      not null,
   SHDM                 VARCHAR2(6),
   CXHDBH               NUMBER(10),
   DJR                  NUMBER(10),
   DJRMC                VARCHAR2(10),
   DJRQ                 DATE,
   constraint PK_KCK_XFBM primary key (CZKHM, KSRQ, JSRQ, SHBMID)
)
/

/*==============================================================*/
/* Table: KCK_YXQBDJL                                           */
/*==============================================================*/
create table BFCRM10.KCK_YXQBDJL  (
   JLBH                 NUMBER(10)                      not null,
   HYKTYPE              NUMBER(5),
   ZY                   VARCHAR2(40),
   XYXQ                 DATE                            not null,
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16)                    not null,
   DJSJ                 DATE                            not null,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(16),
   ZXRQ                 DATE,
   CZDD                 VARCHAR2(20),
   constraint PK_KCK_YXQBDJL primary key (JLBH)
)
/

/*==============================================================*/
/* Table: KCK_YXQBDJLITEM                                       */
/*==============================================================*/
create table BFCRM10.KCK_YXQBDJLITEM  (
   JLBH                 NUMBER(10)                      not null,
   CZKHM                VARCHAR2(13)                    not null,
   YYXQ                 DATE,
   constraint PK_KCK_YXQBDJLITEM primary key (JLBH, CZKHM)
)
/

/*==============================================================*/
/* Table: KHDZTB                                                */
/*==============================================================*/
create table BFCRM10.KHDZTB  (
   KSKH                 VARCHAR2(16)                    not null,
   JSKH                 VARCHAR2(16),
   ZS                   NUMBER(10),
   constraint PK_KHDZTB primary key (KSKH)
)
/

/*==============================================================*/
/* Table: KQJL                                                  */
/*==============================================================*/
create table BFCRM10.KQJL  (
   RQ                   DATE                            not null,
   PERSON_ID            NUMBER(10)                      not null,
   NXH                  VARCHAR2(6)                     not null,
   INX                  NUMBER(5)                       not null,
   DD                   DATE                            not null,
   LK                   DATE,
   constraint PK_KQJL primary key (RQ, PERSON_ID, NXH, INX)
)
/

/*==============================================================*/
/* Index: KQJL_PK                                               */
/*==============================================================*/
create index BFCRM10.KQJL_PK on BFCRM10.KQJL (
   PERSON_ID ASC,
   RQ ASC
)
/

/*==============================================================*/
/* Index: RELATION_993_FK                                       */
/*==============================================================*/
create index BFCRM10.RELATION_993_FK on BFCRM10.KQJL (
   PERSON_ID ASC
)
/

/*==============================================================*/
/* Table: LABEL_BASE_GZ                                         */
/*==============================================================*/
create table BFCRM10.LABEL_BASE_GZ  (
   LABELID              NUMBER(10)                      not null,
   STATUS               NUMBER(10),
   YXYF                 NUMBER(10),
   NEW_LABELID          NUMBER(10),
   DJR                  NUMBER(10),
   DJRMC                VARCHAR2(20),
   DJSJ                 DATE,
   constraint PK_LABEL_BASE_GZ primary key (LABELID)
)
/

/*==============================================================*/
/* Table: LABEL_GZ                                              */
/*==============================================================*/
create table BFCRM10.LABEL_GZ  (
   LABELID              NUMBER(10)                      not null,
   STATUS               NUMBER(10),
   YXYF                 NUMBER(10),
   NEW_LABELID          NUMBER(10),
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16)                    not null,
   DJSJ                 DATE                            not null,
   TJSL                 NUMBER(10),
   TJYF                 NUMBER(10),
   constraint PK_LABEL_GZ primary key (LABELID)
)
/

/*==============================================================*/
/* Table: LABEL_GZITEM                                          */
/*==============================================================*/
create table BFCRM10.LABEL_GZITEM  (
   LABELID              NUMBER(10)                      not null,
   XH                   NUMBER(10)                      not null,
   SJNR                 NUMBER(10),
   constraint PK_LABEL_GZITEM primary key (LABELID, XH)
)
/

/*==============================================================*/
/* Table: LABEL_LB                                              */
/*==============================================================*/
create table BFCRM10.LABEL_LB  (
   LABELLBID            NUMBER(10)                      not null,
   BQMC                 VARCHAR2(50),
   STATUS               NUMBER(5),
   constraint PK_LABEL_LB primary key (LABELLBID)
)
/

/*==============================================================*/
/* Table: LABEL_XM                                              */
/*==============================================================*/
create table BFCRM10.LABEL_XM  (
   LABELXMID            NUMBER(10)                      not null,
   LABELXMDM            VARCHAR2(50),
   LABELXMMC            VARCHAR2(50),
   LABELLBID            NUMBER(10),
   LABELXMMS            VARCHAR2(100),
   RS                   NUMBER(10),
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16)                    not null,
   DJSJ                 DATE                            not null,
   STATUS               NUMBER(5),
   BJ_WY                NUMBER(5),
   constraint PK_LABEL_XM primary key (LABELXMID)
)
/

/*==============================================================*/
/* Table: LABEL_XMITEM                                          */
/*==============================================================*/
create table BFCRM10.LABEL_XMITEM  (
   LABELID              NUMBER(10)                      not null,
   LABELXMID            NUMBER(10),
   LABEL_VALUEID        NUMBER(10),
   LABEL_VALUE          VARCHAR2(50),
   SJXX                 NUMBER(10),
   SJSX                 NUMBER(10),
   BZ                   VARCHAR2(100),
   LABELLX              NUMBER(10),
   constraint PK_LABEL_XMITEM primary key (LABELID)
)
/

/*==============================================================*/
/* Table: LDJL                                                  */
/*==============================================================*/
create table BFCRM10.LDJL  (
   JLBH                 NUMBER(10)                      not null,
   LDRQ                 DATE                            not null,
   LDHM                 VARCHAR2(15)                    not null,
   JTSJ                 DATE,
   GJSJ                 DATE,
   TDBH                 NUMBER(10),
   YJCDH                NUMBER(10),
   EJCDH                NUMBER(10),
   SJCDH                NUMBER(10),
   constraint PK_LDJL primary key (JLBH)
)
/

/*==============================================================*/
/* Table: LPFLDEF                                               */
/*==============================================================*/
create table BFCRM10.LPFLDEF  (
   LPFLID               NUMBER(10)                      not null,
   LPFLDM               VARCHAR2(20),
   LPFLMC               VARCHAR2(30),
   BJ_MJ                NUMBER(5),
   BJ_TY                NUMBER(5),
   TM                   NUMBER(10),
   constraint PK_LPFLDEF primary key (LPFLID)
)
/

/*==============================================================*/
/* Table: LQ_A_B                                                */
/*==============================================================*/
create table BFCRM10.LQ_A_B  (
   JLBH                 NUMBER(10)                      not null,
   QYID                 NUMBER(5)                       not null,
   A_B_JLBH             NUMBER(10)                      not null,
   BJ_CZK               NUMBER(5),
   IP                   VARCHAR2(20),
   PORT                 VARCHAR2(6),
   DBNAME               VARCHAR2(10),
   USERNAME             VARCHAR2(10),
   PASSWORD             VARCHAR2(40),
   BJ_A_B               NUMBER(5),
   constraint PK_LQ_A_B primary key (JLBH, QYID)
)
/

/*==============================================================*/
/* Table: LYJL                                                  */
/*==============================================================*/
create table BFCRM10.LYJL  (
   JLBH                 NUMBER(10)                      not null,
   LDHM                 VARCHAR2(15)                    not null,
   WXHM                 NUMBER(10)                      not null,
   JLMC                 VARCHAR2(30)                    not null,
   RQ                   DATE                            not null,
   LYLX                 NUMBER(5)                       not null,
   YGDM                 VARCHAR2(8),
   LXDH                 VARCHAR2(15),
   SX                   NUMBER(5)                      default 0,
   constraint PK_LYJL primary key (JLBH)
)
/

/*==============================================================*/
/* Table: MBJSPGXITEM                                           */
/*==============================================================*/
create table BFCRM10.MBJSPGXITEM  (
   JLBH                 NUMBER(10)                      not null,
   SHSPID               NUMBER(10)                      not null,
   SPDM                 VARCHAR2(13)                    not null,
   constraint PK_MBJSPGXITEM primary key (JLBH, SHSPID, SPDM)
)
/

/*==============================================================*/
/* Table: MBJZGZ                                                */
/*==============================================================*/
create table BFCRM10.MBJZGZ  (
   MBJZGZID             NUMBER(10)                      not null,
   MBJZGZMC             VARCHAR2(20),
   QDJE                 NUMBER(14,2),
   FFXE                 NUMBER(14,2)                   default 0,
   BJ_TY                NUMBER(5)                      default 0 not null,
   constraint PK_MBJZGZ primary key (MBJZGZID)
)
/

/*==============================================================*/
/* Table: MBJZGZITEM                                            */
/*==============================================================*/
create table BFCRM10.MBJZGZITEM  (
   MBJZGZID             NUMBER(10)                      not null,
   XSJE                 NUMBER(14,2)                   default 0 not null,
   ZKJE                 NUMBER(14,2)                   default 0 not null,
   constraint PK_MBJZGZITEM primary key (MBJZGZID, XSJE)
)
/

/*==============================================================*/
/* Table: MBJZSPGX                                              */
/*==============================================================*/
create table BFCRM10.MBJZSPGX  (
   JLBH                 NUMBER(10)                      not null,
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16)                    not null,
   DJSJ                 DATE                            not null,
   DJH                  NUMBER(10)                      not null,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(16),
   ZXRQ                 DATE,
   INX                  NUMBER(5)                       not null,
   GZBH                 NUMBER(5)                       not null,
   constraint PK_MBJZSPGX primary key (JLBH)
)
/

/*==============================================================*/
/* Index: MBJZSPGX_DJH                                          */
/*==============================================================*/
create index BFCRM10.MBJZSPGX_DJH on BFCRM10.MBJZSPGX (
   DJH ASC,
   INX ASC,
   GZBH ASC
)
/

/*==============================================================*/
/* Table: MBJZ_CXHD_BMHZ                                        */
/*==============================================================*/
create table BFCRM10.MBJZ_CXHD_BMHZ  (
   RQ                   DATE                            not null,
   SHDM                 VARCHAR2(4)                     not null,
   BMDM                 VARCHAR2(20)                    not null,
   MDID                 NUMBER(10)                      not null,
   CXID                 NUMBER(10)                      not null,
   MBJZGZID             NUMBER(10)                      not null,
   XFLJMJFS             NUMBER(5)                       not null,
   ZXSJE                NUMBER(14,2)                   default 0,
   ZXSJE_MBJZ           NUMBER(14,2)                   default 0,
   ZMBJZJE              NUMBER(14,2)                   default 0,
   constraint PK_MBJZ_CXHD_BMHZ primary key (RQ, SHDM, MDID, CXID, MBJZGZID, BMDM, XFLJMJFS)
)
/

/*==============================================================*/
/* Index: MBJZ_BMHZ_SHBM                                        */
/*==============================================================*/
create index BFCRM10.MBJZ_BMHZ_SHBM on BFCRM10.MBJZ_CXHD_BMHZ (
   SHDM ASC,
   BMDM ASC
)
/

/*==============================================================*/
/* Table: MBJZ_CXHD_HTHZ                                        */
/*==============================================================*/
create table BFCRM10.MBJZ_CXHD_HTHZ  (
   RQ                   DATE                            not null,
   SHDM                 VARCHAR2(4)                     not null,
   BMDM                 VARCHAR2(20)                    not null,
   MDID                 NUMBER(10)                      not null,
   CXID                 NUMBER(10)                      not null,
   MBJZGZID             NUMBER(10)                      not null,
   SHHTID               NUMBER(10)                      not null,
   XFLJMJFS             NUMBER(5)                       not null,
   ZXSJE                NUMBER(14,2)                   default 0,
   ZXSJE_MBJZ           NUMBER(14,2)                   default 0,
   ZMBJZJE              NUMBER(14,2)                   default 0,
   constraint PK_MBJZ_CXHD_HTHZ primary key (RQ, SHDM, MDID, CXID, MBJZGZID, SHHTID, BMDM, XFLJMJFS)
)
/

/*==============================================================*/
/* Index: MBJZ_HTHZ_HT                                          */
/*==============================================================*/
create index BFCRM10.MBJZ_HTHZ_HT on BFCRM10.MBJZ_CXHD_HTHZ (
   SHHTID ASC
)
/

/*==============================================================*/
/* Index: MBJZ_HTHZ_SHBM                                        */
/*==============================================================*/
create index BFCRM10.MBJZ_HTHZ_SHBM on BFCRM10.MBJZ_CXHD_HTHZ (
   SHDM ASC,
   BMDM ASC
)
/

/*==============================================================*/
/* Table: MDDY                                                  */
/*==============================================================*/
create sequence BFCRM10.S_MDDY
/
create table BFCRM10.MDDY  (
   MDID                 NUMBER(10)                      not null,
   SHDM                 VARCHAR2(4)                     not null,
   MDDM                 VARCHAR2(10)                    not null,
   MDMC                 VARCHAR2(30)                    not null,
   GXSHDM               VARCHAR2(4),
   SQID                 NUMBER(5),
   TM                   NUMBER(10),
   FDBH_JXC             NUMBER(10),
   constraint PK_MDDY primary key (MDID)
)
/

/*==============================================================*/
/* Index: MDDY_MDDM                                             */
/*==============================================================*/
create unique index BFCRM10.MDDY_MDDM on BFCRM10.MDDY (
   SHDM ASC,
   MDDM ASC
)
/

/*==============================================================*/
/* Index: MDDY_MDDM_INDEX                                       */
/*==============================================================*/
create unique index MDDY_MDDM_INDEX on BFCRM10.MDDY (
   MDDM ASC
)
/

/*==============================================================*/
/* Table: MDDY_GXSH                                             */
/*==============================================================*/
create sequence BFCRM10.S_MDDY_GXSH
/
create table BFCRM10.MDDY_GXSH  (
   HYKTYPE              NUMBER(10)                      not null,
   MDID                 NUMBER(5)                       not null,
   GXSHDM               VARCHAR2(4)                     not null,
   TM                   NUMBER(10),
   constraint PK_MDDY_GXSH primary key (MDID, HYKTYPE)
)
/

/*==============================================================*/
/* Table: MD_SKT_ZT                                             */
/*==============================================================*/
create table BFCRM10.MD_SKT_ZT  (
   MDID                 NUMBER(10)                      not null,
   SKTNO                VARCHAR2(6)                     not null,
   CURR_SKY             NUMBER(10),
   SYSJ                 DATE,
   constraint PK_MD_SKT_ZT primary key (MDID, SKTNO)
)
/

/*==============================================================*/
/* Table: MD_SQ                                                 */
/*==============================================================*/
create table BFCRM10.MD_SQ  (
   SQID                 NUMBER(10)                      not null,
   MDID                 NUMBER(10)                      not null,
   BZ                   VARCHAR2(50),
   constraint PK_MD_SQ primary key (SQID, MDID)
)
/

/*==============================================================*/
/* Table: MD_SQ_LM                                              */
/*==============================================================*/
create table BFCRM10.MD_SQ_LM  (
   SQID                 NUMBER(10)                      not null,
   MDID                 NUMBER(10)                      not null,
   LMID                 NUMBER(10)                      not null,
   constraint PK_MD_SQ_LM primary key (SQID, MDID, LMID)
)
/

/*==============================================================*/
/* Table: NBBM                                                  */
/*==============================================================*/
create table BFCRM10.NBBM  (
   BMBH                 NUMBER(10)                      not null,
   BMMC                 VARCHAR2(100)                   not null,
   constraint PK_NBBM primary key (BMBH)
)
/

/*==============================================================*/
/* Table: NXDEF                                                 */
/*==============================================================*/
create table BFCRM10.NXDEF  (
   NXDKH                NUMBER(10)                      not null,
   NXH                  VARCHAR2(6)                     not null,
   DQDJH                NUMBER(10),
   STATUS               NUMBER(5),
   JTSJ                 DATE,
   BJ_TY                NUMBER(5)                      default 0 not null,
   PERSON_ID            NUMBER(10),
   constraint PK_NXDEF primary key (NXDKH)
)
/

/*==============================================================*/
/* Index: INDEX_NXH                                             */
/*==============================================================*/
create unique index BFCRM10.INDEX_NXH on BFCRM10.NXDEF (
   NXH ASC
)
/

/*==============================================================*/
/* Table: NXZXJL                                                */
/*==============================================================*/
create table BFCRM10.NXZXJL  (
   JLBH                 NUMBER(10)                      not null,
   LDHM                 VARCHAR2(15),
   NXH                  VARCHAR2(6),
   HJSJ                 DATE,
   JTSJ                 DATE,
   GJSJ                 DATE,
   PERSON_ID            NUMBER(10),
   STATUS               NUMBER(5),
   constraint PK_NXZXJL primary key (JLBH)
)
/

/*==============================================================*/
/* Table: PARK_ACCOUNT                                          */
/*==============================================================*/
create table BFCRM10.PARK_ACCOUNT  (
   HYID                 NUMBER(10)                      not null,
   JSRQ                 DATE                            not null,
   MINUTES              NUMBER(10)                      not null,
   constraint PK_PARK_ACCOUNT primary key (HYID, JSRQ)
)
/

/*==============================================================*/
/* Table: PARK_CLJL                                             */
/*==============================================================*/
create table BFCRM10.PARK_CLJL  (
   JLBH                 NUMBER(10)                      not null,
   HYID                 NUMBER(10)                      not null,
   JSRQ                 DATE                            not null,
   PARK_MINUTES         NUMBER(10)                     default 0 not null,
   MINUTES              NUMBER(10)                      not null,
   CLSJ                 DATE                            not null,
   MDID                 NUMBER(10)                      not null,
   SKTNO                VARCHAR2(6)                     not null,
   SKYDM                VARCHAR2(8),
   ZY                   VARCHAR2(20),
   constraint PK_PARK_CLJL primary key (JLBH)
)
/

/*==============================================================*/
/* Table: PARK_GZ                                               */
/*==============================================================*/
create table BFCRM10.PARK_GZ  (
   HYKTYPE              NUMBER(5)                       not null,
   MINUTES              NUMBER(10)                     default 0 not null,
   XFJE                 NUMBER(14,2)                   default 0 not null,
   constraint PK_PARK_GZ primary key (HYKTYPE)
)
/

/*==============================================================*/
/* Table: POSDATADEF                                            */
/*==============================================================*/
create table BFCRM10.POSDATADEF  (
   TBLMC                VARCHAR2(20)                    not null,
   SQLSTR               VARCHAR2(4000),
   KEYSTR               VARCHAR2(40),
   SYSID                NUMBER(5),
   LIBID                NUMBER(10),
   constraint PK_POSDATADEF primary key (TBLMC)
)
/

/*==============================================================*/
/* Table: POSGZSJ                                               */
/*==============================================================*/
create table BFCRM10.POSGZSJ  (
   SHDM                 VARCHAR2(4)                     not null,
   SJMC                 VARCHAR2(20)                    not null,
   SJID                 NUMBER(10)                      not null,
   SJNR                 BLOB                            not null,
   SJLX                 NUMBER(5)                       not null
      constraint CKC_SJLX_POSGZSJ check (SJLX in (0,1)),
   constraint PK_POSGZSJ primary key (SHDM, SJMC, SJID)
)
/

comment on column BFCRM10.POSGZSJ.SHDM is
'值为''''时表示所有商户用的数据'
/

comment on column BFCRM10.POSGZSJ.SJLX is
'-1: 删除数据
0:  正常数据（完整数据）
1:  增量数据'
/

/*==============================================================*/
/* Table: POSGZSJTM                                             */
/*==============================================================*/
create table BFCRM10.POSGZSJTM  (
   SHDM                 VARCHAR2(4)                     not null,
   SJMC                 VARCHAR2(20)                    not null,
   PREVTM               NUMBER(10),
   THISTM               NUMBER(10)                      not null,
   constraint PK_POSGZSJTM primary key (SHDM, SJMC)
)
/

comment on column BFCRM10.POSGZSJTM.SHDM is
'值为''''时表示所有商户用的数据'
/

/*==============================================================*/
/* Table: PTGL_MODULE_GROUP                                     */
/*==============================================================*/
create table BFCRM10.PTGL_MODULE_GROUP  (
   MGRPID               NUMBER(10)                      not null,
   MGRPMC               varchar2(30)                    not null,
   constraint PK_PTGL_MODULE_GROUP primary key (MGRPID)
)
/

/*==============================================================*/
/* Table: QYDB                                                  */
/*==============================================================*/
create table BFCRM10.QYDB  (
   QYID                 NUMBER(5)                       not null,
   IP                   VARCHAR2(20)                    not null,
   PORT                 VARCHAR2(6)                     not null,
   DBNAME               VARCHAR2(10)                    not null,
   USERNAME             VARCHAR2(10),
   PASSWORD             VARCHAR2(40),
   BJ_A_B               NUMBER(5)                      default 0,
   TM                   NUMBER(10),
   constraint PK_QYDB primary key (QYID)
)
/

/*==============================================================*/
/* Table: QYDEF                                                 */
/*==============================================================*/
create table BFCRM10.QYDEF  (
   QYID                 NUMBER(5)                       not null,
   QYMC                 VARCHAR2(20),
   QYLX                 NUMBER(10),
   FTPBJ                NUMBER(5)                      default 0,
   STATUS               NUMBER(5),
   TM                   NUMBER(10),
   constraint PK_QYDEF primary key (QYID)
)
/

/*==============================================================*/
/* Table: QYDEF_DELETE                                          */
/*==============================================================*/
create table BFCRM10.QYDEF_DELETE  (
   JLBH                 NUMBER(10)                      not null,
   QYID                 NUMBER(10),
   QYMC                 VARCHAR2(20),
   SCR                  NUMBER(10),
   SCRMC                VARCHAR2(20),
   SCSJ                 DATE,
   constraint PK_QYDEF_DELETE primary key (JLBH)
)
/

/*==============================================================*/
/* Table: QYDEF_MD                                              */
/*==============================================================*/
create table BFCRM10.QYDEF_MD  (
   QYID                 NUMBER(10)                      not null,
   MDID                 NUMBER(10)                      not null,
   TM                   NUMBER(10),
   constraint PK_QYDEF_MD primary key (QYID, MDID)
)
/

/*==============================================================*/
/* Table: QYDEF_MD_DELETE                                       */
/*==============================================================*/
create table BFCRM10.QYDEF_MD_DELETE  (
   JLBH                 NUMBER(10)                      not null,
   QYID                 NUMBER(10)                      not null,
   MDID                 NUMBER(10)                      not null,
   constraint PK_QYDEF_MD_DELETE primary key (JLBH, QYID, MDID)
)
/

/*==============================================================*/
/* Table: QZJCSSZ                                               */
/*==============================================================*/
create table BFCRM10.QZJCSSZ  (
   URL                  VARCHAR2(60)                    not null,
   constraint PK_QZJCSSZ primary key (URL)
)
/

/*==============================================================*/
/* Table: RCLLOGITEM                                            */
/*==============================================================*/
create table BFCRM10.RCLLOGITEM  (
   CLSJ                 DATE,
   RCLRQ                DATE,
   MODULE_NAME          varchar2(100),
   LOG                  varchar2(100)
)
/

/*==============================================================*/
/* Index: RCLLOG_RCLRQ                                          */
/*==============================================================*/
create index RCLLOG_RCLRQ on BFCRM10.RCLLOGITEM (
   RCLRQ ASC
)
/

/*==============================================================*/
/* Table: SDDEF                                                 */
/*==============================================================*/
create table BFCRM10.SDDEF  (
   CODE                 NUMBER(10)                      not null,
   KSSJ                 NUMBER(10),
   JSSJ                 NUMBER(10),
   TITLE                VARCHAR2(20),
   constraint PK_SDDEF primary key (CODE)
)
/

/*==============================================================*/
/* Table: SHBM                                                  */
/*==============================================================*/
create table BFCRM10.SHBM  (
   SHBMID               NUMBER(10)                      not null,
   SHDM                 VARCHAR2(4)                     not null,
   BMDM                 VARCHAR2(20)                    not null,
   BMMC                 VARCHAR2(100)                   not null,
   BMQC                 VARCHAR2(50),
   DEPT_TYPE            NUMBER(5)                      default 0,
   QYID                 NUMBER(5),
   constraint PK_SHBM primary key (SHBMID)
)
/

/*==============================================================*/
/* Index: BM_BMDM                                               */
/*==============================================================*/
create unique index BFCRM10.BM_BMDM on BFCRM10.SHBM (
   SHDM ASC,
   BMDM ASC
)
/

/*==============================================================*/
/* Table: SHCXHD                                                */
/*==============================================================*/
create table BFCRM10.SHCXHD  (
   CXHDBH               NUMBER(9)                       not null,
   SHDM                 VARCHAR2(4),
   CXHDDM               VARCHAR2(20),
   CXHDMC               VARCHAR2(30)                    not null,
   RQ1                  DATE                            not null,
   RQ2                  DATE                            not null,
   constraint PK_SHCXHD primary key (CXHDBH)
)
/

/*==============================================================*/
/* Table: SHDY                                                  */
/*==============================================================*/
create table BFCRM10.SHDY  (
   SHDM                 VARCHAR2(4)                     not null,
   SHMC                 VARCHAR2(20),
   TM                   NUMBER(10),
   BMJC                 VARCHAR2(20),
   constraint PK_SHDY primary key (SHDM)
)
/

/*==============================================================*/
/* Table: SHHT                                                  */
/*==============================================================*/
create table BFCRM10.SHHT  (
   SHHTID               NUMBER(10)                      not null,
   SHDM                 VARCHAR2(4),
   HTH                  VARCHAR2(10),
   GHSDM                VARCHAR2(10),
   GSHMC                VARCHAR2(100),
   SHBMID               NUMBER(10),
   BJ_YX                NUMBER(5)                      default 1,
   constraint PK_SHHT primary key (SHHTID)
)
/

/*==============================================================*/
/* Index: HT_SHHTH                                              */
/*==============================================================*/
create unique index BFCRM10.HT_SHHTH on BFCRM10.SHHT (
   HTH ASC,
   SHDM ASC
)
/

/*==============================================================*/
/* Index: HT_GHSDM                                              */
/*==============================================================*/
create index BFCRM10.HT_GHSDM on BFCRM10.SHHT (
   GHSDM ASC,
   SHDM ASC
)
/

/*==============================================================*/
/* Table: SHSPFL                                                */
/*==============================================================*/
create table BFCRM10.SHSPFL  (
   SHSPFLID             NUMBER(10)                      not null,
   SHDM                 VARCHAR2(4),
   SPFLDM               VARCHAR2(12)                    not null,
   SPFLMC               VARCHAR2(30)                    not null,
   PYM                  VARCHAR2(50),
   MJBJ                 NUMBER(1)                       not null
      constraint CKC_MJBJ_SHSPFL check (MJBJ in (0,1)),
   SPFLID               NUMBER(10),
   constraint PK_SHSPFL primary key (SHSPFLID)
)
/

/*==============================================================*/
/* Index: SPFL_SHFLDM                                           */
/*==============================================================*/
create unique index BFCRM10.SPFL_SHFLDM on BFCRM10.SHSPFL (
   SPFLDM ASC,
   SHDM ASC
)
/

/*==============================================================*/
/* Index: SHSPFL_SPFLID                                         */
/*==============================================================*/
create index BFCRM10.SHSPFL_SPFLID on BFCRM10.SHSPFL (
   SPFLID ASC
)
/

/*==============================================================*/
/* Table: SHSPSB                                                */
/*==============================================================*/
create table BFCRM10.SHSPSB  (
   SHSBID               NUMBER(10)                      not null,
   SHDM                 VARCHAR2(4),
   SBDM                 VARCHAR2(10)                    not null,
   SBMC                 VARCHAR2(30)                    not null,
   PYM                  VARCHAR2(30),
   SYZ                  VARCHAR2(50),
   MJBJ                 NUMBER(1)                       not null
      constraint CKC_MJBJ_SHSPSB check (MJBJ in (0,1)),
   SBID                 NUMBER(10),
   constraint PK_SHSPSB primary key (SHSBID)
)
/

/*==============================================================*/
/* Index: SPSB_SHSBDM                                           */
/*==============================================================*/
create unique index BFCRM10.SPSB_SHSBDM on BFCRM10.SHSPSB (
   SHDM ASC,
   SBDM ASC
)
/

/*==============================================================*/
/* Index: SPSHSB_SBID                                           */
/*==============================================================*/
create index BFCRM10.SPSHSB_SBID on BFCRM10.SHSPSB (
   SBID ASC
)
/

/*==============================================================*/
/* Table: SHSPXX                                                */
/*==============================================================*/
create sequence BFCRM10.S_SHSPXX
/
create table BFCRM10.SHSPXX  (
   SHSPID               NUMBER(10)                      not null,
   SHDM                 VARCHAR2(4),
   SPDM                 VARCHAR2(13)                    not null,
   SPMC                 VARCHAR2(70)                    not null,
   SPJC                 VARCHAR2(70)                    not null,
   PYM                  VARCHAR2(6)                     not null,
   UNIT                 VARCHAR2(8),
   SPHS                 VARCHAR2(20),
   SPGG                 VARCHAR2(20),
   HH                   VARCHAR2(16),
   SHSPFLID             NUMBER(10),
   SHSBID               NUMBER(10),
   SHHTID               NUMBER(10),
   TM                   NUMBER(10),
   constraint PK_SHSPXX primary key (SHSPID)
)
/

/*==============================================================*/
/* Index: SPXX_SPFL                                             */
/*==============================================================*/
create index BFCRM10.SPXX_SPFL on BFCRM10.SHSPXX (
   SHSPFLID ASC
)
/

/*==============================================================*/
/* Index: SPXX_SB                                               */
/*==============================================================*/
create index BFCRM10.SPXX_SB on BFCRM10.SHSPXX (
   SHSBID ASC
)
/

/*==============================================================*/
/* Index: SPXX_SHSPDM                                           */
/*==============================================================*/
create unique index BFCRM10.SPXX_SHSPDM on BFCRM10.SHSPXX (
   SPDM ASC,
   SHDM ASC
)
/

/*==============================================================*/
/* Index: SPXX_SHPYM                                            */
/*==============================================================*/
create index BFCRM10.SPXX_SHPYM on BFCRM10.SHSPXX (
   PYM ASC,
   SHDM ASC
)
/

/*==============================================================*/
/* Index: SPXX_SHHT                                             */
/*==============================================================*/
create index BFCRM10.SPXX_SHHT on BFCRM10.SHSPXX (
   SHHTID ASC
)
/

/*==============================================================*/
/* Table: SHSPXX_DM                                             */
/*==============================================================*/
create table BFCRM10.SHSPXX_DM  (
   SHSPID               NUMBER(10)                      not null,
   SHDM                 VARCHAR2(4)                     not null,
   SPDM                 VARCHAR2(13)                    not null,
   constraint PK_SHSPXX_DM primary key (SHDM, SPDM)
)
/

/*==============================================================*/
/* Table: SHZFFS                                                */
/*==============================================================*/
create table BFCRM10.SHZFFS  (
   SHZFFSID             NUMBER(10)                      not null,
   SHDM                 VARCHAR2(4)                     not null,
   ZFFSDM               VARCHAR2(10)                    not null,
   ZFFSMC               VARCHAR2(30)                    not null,
   MJBJ                 NUMBER(1)                       not null
      constraint CKC_MJBJ_SHZFFS check (MJBJ in (0,1)),
   ZFFSID               NUMBER(10),
   BJ_JF                NUMBER(5)                      default 1 not null,
   BJ_FQ                NUMBER(5)                      default 1 not null,
   YHQID                NUMBER(5),
   BJ_MBJZ              NUMBER(5)                      default 1 not null,
   JFBL                 FLOAT                          default 1,
   constraint PK_SHZFFS primary key (SHZFFSID)
)
/

/*==============================================================*/
/* Index: ZSFS_SHDM                                             */
/*==============================================================*/
create unique index BFCRM10.ZSFS_SHDM on BFCRM10.SHZFFS (
   SHDM ASC,
   ZFFSDM ASC
)
/

/*==============================================================*/
/* Table: SHZFFS_MDJFBL                                         */
/*==============================================================*/
create table BFCRM10.SHZFFS_MDJFBL  (
   SHZFFSID             NUMBER(10)                      not null,
   MDID                 NUMBER(10)                      not null,
   JFBL                 FLOAT                          default 1,
   constraint PK_SHZFFS_MDJFBL primary key (SHZFFSID, MDID)
)
/

/*==============================================================*/
/* Table: SKTCRMCFG                                             */
/*==============================================================*/
create table BFCRM10.SKTCRMCFG  (
   SKTNO                VARCHAR2(6)                     not null,
   MDDM                 VARCHAR2(10)                    not null,
   CRMIP                VARCHAR2(15)                    not null,
   CRMPORT              NUMBER(10)                      not null,
   CRMUSER              VARCHAR2(10)                    not null,
   CRMPASSWD            VARCHAR2(10)                    not null,
   constraint PK_SKTCRMCFG primary key (SKTNO)
)
/

/*==============================================================*/
/* Table: SPFL                                                  */
/*==============================================================*/
create sequence BFCRM10.S_SPFL
/
create table BFCRM10.SPFL  (
   SPFLID               NUMBER(10)                      not null,
   SPFLDM               VARCHAR2(12)                    not null,
   SPFLMC               VARCHAR2(30)                    not null,
   SPFLQC               VARCHAR2(50),
   PYM                  VARCHAR2(6),
   MJBJ                 NUMBER(1)                       not null
      constraint CKC_MJBJ_SPFL check (MJBJ in (0,1)),
   TM                   NUMBER(10),
   constraint PK_SPFL primary key (SPFLID)
)
/

/*==============================================================*/
/* Index: CRMSPFL_DM                                            */
/*==============================================================*/
create unique index BFCRM10.CRMSPFL_DM on BFCRM10.SPFL (
   SPFLDM ASC
)
/

/*==============================================================*/
/* Table: SPJGDDY                                               */
/*==============================================================*/
create table BFCRM10.SPJGDDY  (
   JGDID                NUMBER(5)                       not null,
   JGDMC                VARCHAR2(30)                    not null,
   LSDJ1                NUMBER(10,2)                    not null,
   LSDJ2                NUMBER(10,2)                    not null,
   constraint PK_SPJGDDY primary key (JGDID)
)
/

/*==============================================================*/
/* Table: SPSB                                                  */
/*==============================================================*/
create sequence BFCRM10.S_SPSB
/
create table BFCRM10.SPSB  (
   SBID                 NUMBER(10)                      not null,
   SBDM                 VARCHAR2(10)                    not null,
   SBMC                 VARCHAR2(30)                    not null,
   PYM                  VARCHAR2(6),
   SYZ                  VARCHAR2(50),
   MJBJ                 NUMBER(1)                       not null
      constraint CKC_MJBJ_SPSB check (MJBJ in (0,1)),
   TM                   NUMBER(10),
   constraint PK_SPSB primary key (SBID)
)
/

/*==============================================================*/
/* Index: CRMSPSB_PYM                                           */
/*==============================================================*/
create index BFCRM10.CRMSPSB_PYM on BFCRM10.SPSB (
   PYM ASC
)
/

/*==============================================================*/
/* Index: CRMSPSB_NAME                                          */
/*==============================================================*/
create unique index BFCRM10.CRMSPSB_NAME on BFCRM10.SPSB (
   SBMC ASC
)
/

/*==============================================================*/
/* Index: CRMSPSB_SBDM                                          */
/*==============================================================*/
create unique index BFCRM10.CRMSPSB_SBDM on BFCRM10.SPSB (
   SBDM ASC
)
/

/*==============================================================*/
/* Table: SQDY                                                  */
/*==============================================================*/
create table BFCRM10.SQDY  (
   SQID                 NUMBER(10)                      not null,
   SQMC                 VARCHAR2(30),
   SQMS                 VARCHAR2(50),
   MDID                 NUMBER(10),
   constraint PK_SQDY primary key (SQID)
)
/

/*==============================================================*/
/* Table: SRDHJL                                                */
/*==============================================================*/
create table BFCRM10.SRDHJL  (
   DHHM                 VARCHAR2(15)                    not null,
   DYCSRSJ              DATE                            not null,
   ZHYCSRSJ             DATE                            not null,
   SRCS                 NUMBER(10)                      not null,
   SX                   NUMBER(5)                       not null,
   BZ                   VARCHAR2(100),
   DJR                  NUMBER(10),
   DJRMC                VARCHAR2(16),
   DJSJ                 DATE,
   constraint PK_SRDHJL primary key (DHHM)
)
/

/*==============================================================*/
/* Table: SWZL                                                  */
/*==============================================================*/
create table BFCRM10.SWZL  (
   JLBH                 NUMBER(10)                      not null,
   RQ                   DATE,
   WPMC                 VARCHAR2(20),
   ZY                   VARCHAR2(100),
   MDID                 NUMBER(5),
   WZMC                 VARCHAR2(16),
   SFZH                 VARCHAR2(18),
   DJR                  NUMBER(5),
   ZXRQ                 DATE,
   DJRMC                VARCHAR2(16),
   DJSJ                 DATE,
   ZXR                  NUMBER(5),
   ZXRMC                VARCHAR2(16),
   LXFS                 VARCHAR2(15),
   BJ                   NUMBER(5),
   constraint PK_SWZL primary key (JLBH)
)
/

/*==============================================================*/
/* Table: SWZL_LQD                                              */
/*==============================================================*/
create table BFCRM10.SWZL_LQD  (
   JLBH                 NUMBER(10)                      not null,
   RQ                   DATE,
   SHDM                 VARCHAR2(4),
   SHBMID               NUMBER(5),
   JGRMC                VARCHAR2(16),
   WPMC                 VARCHAR2(20),
   WPTZ                 VARCHAR2(50),
   ZY                   VARCHAR2(100),
   JLBH_LR              NUMBER(10),
   LRDDJR               NUMBER(10),
   LRDDJRMC             VARCHAR2(16),
   LRDDJSJ              DATE,
   LRDZXR               NUMBER(10),
   LRDZXRMC             VARCHAR2(16),
   LRDZXRQ              DATE,
   WZMC                 VARCHAR2(16),
   LQRZJ                NUMBER(10),
   LQRZJHM              VARCHAR2(30),
   LQRDW                VARCHAR2(50),
   LXFS                 VARCHAR2(15),
   DJR                  NUMBER(10),
   DJRMC                VARCHAR2(16),
   DJSJ                 DATE,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(16),
   ZXRQ                 DATE,
   JSR                  NUMBER(10),
   JSRMC                VARCHAR2(16),
   LQRQ                 DATE,
   constraint PK_SWZL_LQD primary key (JLBH)
)
/

/*==============================================================*/
/* Table: SWZL_LRD                                              */
/*==============================================================*/
create table BFCRM10.SWZL_LRD  (
   JLBH                 NUMBER(10)                      not null,
   RQ                   DATE                            not null,
   SHDM                 VARCHAR2(4),
   SHBMID               NUMBER(10),
   JGRMC                VARCHAR2(16),
   WPMC                 VARCHAR2(20),
   WPTZ                 VARCHAR2(50),
   ZY                   VARCHAR2(100),
   DJR                  NUMBER(5),
   DJRMC                VARCHAR2(16),
   DJSJ                 DATE,
   ZXR                  NUMBER(5),
   ZXRMC                VARCHAR2(16),
   ZXRQ                 DATE,
   constraint PK_SWZL_LRD primary key (JLBH)
)
/

/*==============================================================*/
/* Table: TMP_CFHY                                              */
/*==============================================================*/
create table BFCRM10.TMP_CFHY  (
   PERSON_ID            NUMBER(10),
   HY_NAME              VARCHAR2(30),
   NAME_SL              NUMBER(16,4),
   SJHM                 VARCHAR2(20),
   SJHM_SL              NUMBER(16,4),
   TXDZ                 VARCHAR2(100),
   TXDZ_SL              NUMBER(16,4),
   SFZBH                VARCHAR2(20),
   SFZBH_SL             NUMBER(16,4)
)
/

/*==============================================================*/
/* Table: TMP_CZK_DZ                                            */
/*==============================================================*/
create table BFCRM10.TMP_CZK_DZ  (
   RQ                   DATE,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(20),
   ZFFSID               NUMBER(10),
   SKJE                 NUMBER(14,2),
   JKJE                 NUMBER(14,2)
)
/

/*==============================================================*/
/* Table: TMP_CZK_RBB                                           */
/*==============================================================*/
create table BFCRM10.TMP_CZK_RBB  (
   HYKTYPE              NUMBER(5),
   MDID                 NUMBER(5),
   JE                   NUMBER(14,2)
)
/

/*==============================================================*/
/* Table: TMP_HYDJSQJL                                          */
/*==============================================================*/
create table BFCRM10.TMP_HYDJSQJL  (
   HYID                 NUMBER(10)                      not null,
   YXQ                  DATE                            not null,
   HYKTYPE              NUMBER(10)                      not null,
   HYKTYPE_NEW          NUMBER(10),
   BQJF                 FLOAT(50),
   SJRQ                 DATE,
   STATUS               NUMBER(5),
   constraint PK_TMP_HYDJSQJL primary key (HYID, YXQ)
)
/

/*==============================================================*/
/* Index: "INX_HYK_DJSQITEM_HYIDbc"                             */
/*==============================================================*/
create index BFCRM10."INX_HYK_DJSQITEM_HYIDbc" on BFCRM10.TMP_HYDJSQJL (
   HYID ASC
)
/

/*==============================================================*/
/* Table: TMP_HYFX_XFJL                                         */
/*==============================================================*/
create table BFCRM10.TMP_HYFX_XFJL  (
   RQ                   DATE                            not null,
   HYKTYPE              NUMBER(5)                       not null,
   HYSXID               NUMBER(10)                      not null,
   HYSXLX               NUMBER(5)                       not null,
   XFJLID               NUMBER(10)                      not null,
   HYID                 NUMBER(10)                      not null,
   JE                   NUMBER(14,2)                    not null,
   ID                   NUMBER(10),
   constraint PK_TMP_HYFX_XFJL primary key (RQ, HYSXLX, HYKTYPE, HYSXID, XFJLID, HYID)
)
/

/*==============================================================*/
/* Table: TMP_HYID_RJSH                                         */
/*==============================================================*/
create table BFCRM10.TMP_HYID_RJSH  (
   HYID                 NUMBER(10)                      not null,
   JLBH                 NUMBER(10)                      not null,
   constraint PK_TMP_HYID_RJSH primary key (HYID, JLBH)
)
/

/*==============================================================*/
/* Table: TMP_HYK_FL                                            */
/*==============================================================*/
create table BFCRM10.TMP_HYK_FL  (
   HYID                 NUMBER(10)                      not null,
   LJXFJE               FLOAT(50)                       not null,
   ZKJE                 NUMBER(14,2),
   WCLJF                FLOAT(50),
   FLJE                 NUMBER(14,2),
   MDID                 NUMBER(10),
   constraint PK_TMP_HYK_FL primary key (HYID)
)
/

/*==============================================================*/
/* Table: TMP_HYK_HTFQJL_XPMX                                   */
/*==============================================================*/
create table BFCRM10.TMP_HYK_HTFQJL_XPMX  (
   XFJLID               NUMBER(10)                      not null,
   YHQFFGZID            NUMBER(5)                       not null,
   ZXFJE                NUMBER(14,2),
   FQJE                 NUMBER(14,2)
)
/

/*==============================================================*/
/* Table: TMP_HYK_HYXX_BG                                       */
/*==============================================================*/
create table BFCRM10.TMP_HYK_HYXX_BG  (
   HYID                 NUMBER(10)                      not null,
   STATUS               NUMBER(5)                       not null,
   GZLX                 NUMBER(5)                       not null,
   constraint PK_TMP_HYK_HYXX_BG primary key (HYID)
)
/

/*==============================================================*/
/* Index: ZTBG_GZLX                                             */
/*==============================================================*/
create index BFCRM10.ZTBG_GZLX on BFCRM10.TMP_HYK_HYXX_BG (
   GZLX ASC
)
/

/*==============================================================*/
/* Table: TMP_HYK_JFBDJLMX                                      */
/*==============================================================*/
create table BFCRM10.TMP_HYK_JFBDJLMX  (
   HYID                 NUMBER(10)                      not null,
   JLBH                 NUMBER(10)                      not null,
   WCLJFBD              FLOAT(50),
   BQJFBD               FLOAT(50),
   BNLJJFBD             FLOAT(50)                      default 0,
   constraint PK_TMP_HYK_JFBDJLMX primary key (HYID, JLBH)
)
/

/*==============================================================*/
/* Table: TMP_HYK_SHJF                                          */
/*==============================================================*/
create table BFCRM10.TMP_HYK_SHJF  (
   HYID                 NUMBER(10)                      not null,
   SHDM                 VARCHAR2(4)                     not null,
   LJXFJE               FLOAT(50)                       not null,
   ZKJE                 NUMBER(14,2),
   WCLJF                FLOAT(50),
   constraint PK_TMP_HYK_SHJF primary key (HYID, SHDM)
)
/

/*==============================================================*/
/* Table: TMP_HYK_SJJL                                          */
/*==============================================================*/
create table BFCRM10.TMP_HYK_SJJL  (
   HYID                 NUMBER(10)                      not null,
   HYKTYPE_OLD          NUMBER(5),
   HYKTYPE_NEW          NUMBER(5),
   HYKHM                VARCHAR2(20),
   WCLJF_OLD            FLOAT(50),
   BQJF_OLD             FLOAT(50),
   YJKRQ                DATE,
   QDJF                 FLOAT(50),
   constraint PK_TMP_HYK_SJJL primary key (HYID)
)
/

/*==============================================================*/
/* Table: TMP_HYK_SJJL_N                                        */
/*==============================================================*/
create table BFCRM10.TMP_HYK_SJJL_N  (
   HYID                 NUMBER(10)                      not null,
   HYKTYPE_OLD          NUMBER(5),
   HYKTYPE_NEW          NUMBER(5),
   HYKHM                VARCHAR2(20),
   WCLJF_OLD            FLOAT(50),
   BQJF_OLD             FLOAT(50),
   YJKRQ                DATE,
   QDJF                 FLOAT(50),
   constraint PK_TMP_HYK_SJJL_N primary key (HYID)
)
/

/*==============================================================*/
/* Table: TMP_HYK_XFJE_JF                                       */
/*==============================================================*/
create table BFCRM10.TMP_HYK_XFJE_JF  (
   HYID                 NUMBER(10)                      not null,
   MDID                 NUMBER(5)                       not null,
   XFJE_JF              NUMBER(14,2),
   constraint PK_TMP_HYK_XFJE_JF primary key (HYID, MDID)
)
/

/*==============================================================*/
/* Table: TMP_HYK_XFJL                                          */
/*==============================================================*/
create table BFCRM10.TMP_HYK_XFJL  (
   HYID                 NUMBER(10)                      not null,
   XFJE                 NUMBER(14,2)                    not null,
   constraint PK_TMP_HYK_XFJL primary key (HYID)
)
/

/*==============================================================*/
/* Table: TMP_HYK_XFJL_HTFQ                                     */
/*==============================================================*/
create table BFCRM10.TMP_HYK_XFJL_HTFQ  (
   XFJLID               NUMBER(10)                      not null,
   SHDM                 VARCHAR2(10)                    not null,
   MDID                 NUMBER(10)                      not null,
   SKTNO                VARCHAR2(6)                     not null,
   JLBH                 NUMBER(10)                      not null,
   DJLX                 NUMBER(10)                      not null,
   XFJLID_OLD           NUMBER(10),
   HYID                 NUMBER(10),
   HYID_FQ              NUMBER(10),
   HYID_TQ              NUMBER(10),
   SKYDM                VARCHAR2(10),
   CRMJZRQ              DATE,
   XFSJ                 DATE,
   JZRQ                 DATE,
   SCSJ                 DATE                            not null,
   JE                   NUMBER(14,2)                   default 0 not null,
   ZK                   NUMBER(14,2)                   default 0 not null,
   ZK_HY                NUMBER(14,2)                   default 0 not null,
   CZJE                 NUMBER(14,2)                   default 0 not null,
   JF                   NUMBER(16,4)                   default 0 not null,
   BJ_HTBSK             NUMBER(5)                      default 0 not null,
   XFRQ_FQ              DATE,
   JFBS                 NUMBER(16,4)                   default 1,
   DJBH                 NUMBER(10),
   constraint PK_TMP_HYK_XFJL_HTFQ primary key (XFJLID)
)
/

/*==============================================================*/
/* Table: TMP_HYK_XFJL_SP_FQ_HTFQ                               */
/*==============================================================*/
create table BFCRM10.TMP_HYK_XFJL_SP_FQ_HTFQ  (
   XFJLID               NUMBER(10)                     default 0 not null,
   INX                  NUMBER(5)                       not null,
   YHQID                NUMBER(5)                       not null,
   BMDM                 VARCHAR2(20),
   SPDM                 VARCHAR2(20),
   SHSPID               NUMBER(10),
   YHQFFDBH             NUMBER(10),
   CXID                 NUMBER(10),
   XFLJFQFS             NUMBER(5),
   YHQFFGZID            NUMBER(10),
   XSSL                 NUMBER(16,4),
   XSJE                 NUMBER(14,2),
   XSJE_FQ              NUMBER(14,2),
   FQJE                 NUMBER(14,2),
   FQJE_SJ              NUMBER(14,2)                   default 0,
   constraint PK_TMP_HYK_XFJL_SP_FQ_HTFQ primary key (XFJLID, INX, YHQID)
)
/

/*==============================================================*/
/* Table: TMP_HYK_XFYBB                                         */
/*==============================================================*/
create table BFCRM10.TMP_HYK_XFYBB  (
   YEARMONTH            NUMBER(10)                      not null,
   HYKTYPE              NUMBER(10)                      not null,
   MDID                 NUMBER(10)                      not null,
   XFJE                 NUMBER(14,2),
   XFHYSL               NUMBER(10),
   constraint PK_TMP_HYK_XFYBB primary key (YEARMONTH, HYKTYPE, MDID)
)
/

/*==============================================================*/
/* Table: TMP_HYXFJL_SP_FQ_DBFT                                 */
/*==============================================================*/
create table BFCRM10.TMP_HYXFJL_SP_FQ_DBFT  (
   YHQID                NUMBER(10),
   INX                  NUMBER(10),
   XSJE_FQ              NUMBER(14,2)
)
/

/*==============================================================*/
/* Table: TMP_HYXFJL_SP_FQ_DBFT_HT                              */
/*==============================================================*/
create table BFCRM10.TMP_HYXFJL_SP_FQ_DBFT_HT  (
   YHQID                NUMBER(10)                      not null,
   SHSPID               NUMBER(10)                      not null,
   BMDM                 VARCHAR2(20),
   XSJE_FQ              NUMBER(14,2),
   constraint PK_TMP_HYXFJL_SP_FQ_DBFT_HT primary key (YHQID, SHSPID)
)
/

/*==============================================================*/
/* Table: TMP_HYXFJL_SP_FQ_HTFQ                                 */
/*==============================================================*/
create table BFCRM10.TMP_HYXFJL_SP_FQ_HTFQ  (
   XFJLID               NUMBER(10)                      not null,
   YHQID                NUMBER(10)                      not null,
   constraint PK_TMP_HYXFJL_SP_FQ_HTFQ primary key (XFJLID, YHQID)
)
/

/*==============================================================*/
/* Table: TMP_HY_JEZCLJL                                        */
/*==============================================================*/
create table BFCRM10.TMP_HY_JEZCLJL  (
   HYID                 NUMBER(10)                      not null,
   MDID                 NUMBER(10)                      not null,
   JYID                 NUMBER(10)                      not null,
   DFJE                 NUMBER(14,2)                   default 0,
   JE_CZ                NUMBER(14,2)                   default 0,
   constraint PK_TMP_HY_JEZCLJL primary key (HYID, MDID, JYID)
)
/

/*==============================================================*/
/* Table: TMP_JFYBB                                             */
/*==============================================================*/
create table BFCRM10.TMP_JFYBB  (
   SJLX                 NUMBER(10)                      not null,
   MDID                 NUMBER(10)                      not null,
   OUTJF                FLOAT(50)                      default 0,
   INPJF                FLOAT(50)                      default 0,
   XFJF                 FLOAT(50)                      default 0,
   DHJF                 FLOAT(50)                      default 0,
   WBLJJF               FLOAT(50)                      default 0,
   JCLJJF               FLOAT(50)                      default 0,
   WCLLJJF              FLOAT(50)                      default 0,
   constraint PK_TMP_JFYBB primary key (SJLX, MDID)
)
/

/*==============================================================*/
/* Table: TMP_JYID_CZKJE                                        */
/*==============================================================*/
create table BFCRM10.TMP_JYID_CZKJE  (
   JYID                 NUMBER(10)                      not null,
   CZKJE                NUMBER(14,2)                   default 0,
   constraint PK_TMP_JYID_CZKJE primary key (JYID)
)
/

/*==============================================================*/
/* Table: TMP_JYID_JE                                           */
/*==============================================================*/
create table BFCRM10.TMP_JYID_JE  (
   JYID                 NUMBER(10)                      not null,
   DFJE                 NUMBER(14,2)                   default 0,
   constraint PK_TMP_JYID_JE primary key (JYID)
)
/

/*==============================================================*/
/* Table: TMP_JYID_ZXSJE                                        */
/*==============================================================*/
create table BFCRM10.TMP_JYID_ZXSJE  (
   JYID                 NUMBER(10)                      not null,
   ZXSJE                NUMBER(14,2)                   default 0,
   constraint PK_TMP_JYID_ZXSJE primary key (JYID)
)
/

/*==============================================================*/
/* Table: TMP_MDDY_JFFT                                         */
/*==============================================================*/
create table BFCRM10.TMP_MDDY_JFFT  (
   MDID                 NUMBER(10)                      not null,
   FTBL                 FLOAT(50),
   constraint PK_TMP_MDDY_JFFT primary key (MDID)
)
/

/*==============================================================*/
/* Table: TMP_SHSPXX_XFJE                                       */
/*==============================================================*/
create table BFCRM10.TMP_SHSPXX_XFJE  (
   JYID                 NUMBER(10)                      not null,
   SHSPID               NUMBER(10)                      not null,
   XSJE                 NUMBER(14,2)                   default 0,
   CZKJE                NUMBER(14,2)                   default 0,
   XSBM                 VARCHAR2(10),
   constraint PK_TMP_SHSPXX_XFJE primary key (JYID, SHSPID)
)
/

/*==============================================================*/
/* Table: TMP_XFMX                                              */
/*==============================================================*/
create table BFCRM10.TMP_XFMX  (
   HY_NAME              VARCHAR2(20),
   SEX                  NUMBER(5),
   TXDZ                 VARCHAR2(100),
   YZBM                 VARCHAR2(6),
   SJHM                 VARCHAR2(20),
   BGDH                 VARCHAR2(20),
   E_MAIL               VARCHAR2(40),
   HYK_NO               VARCHAR2(16),
   HYID                 NUMBER(10),
   QYID                 NUMBER(10),
   HYKTYPE              NUMBER(10),
   QYMC                 VARCHAR2(40),
   SKSL                 NUMBER(10),
   XSJE                 NUMBER(14,2)                   default 0,
   ZKJE                 NUMBER(14,2)                   default 0,
   JF                   FLOAT(50),
   FKRQ                 DATE,
   FXMD                 NUMBER(10),
   CSRQ                 DATE
)
/

/*==============================================================*/
/* Table: TMP_XFMXJF                                            */
/*==============================================================*/
create table BFCRM10.TMP_XFMXJF  (
   HY_NAME              VARCHAR2(20),
   SEX                  NUMBER(5),
   TXDZ                 VARCHAR2(100),
   YZBM                 VARCHAR2(6),
   SJHM                 VARCHAR2(20),
   BGDH                 VARCHAR2(20),
   E_MAIL               VARCHAR2(40),
   HYK_NO               VARCHAR2(16),
   HYID                 NUMBER(10),
   HYKTYPE              NUMBER(10),
   XSJE                 NUMBER(14,2)                   default 0,
   ZKJE                 NUMBER(14,2)                   default 0,
   JF                   FLOAT(50)
)
/

/*==============================================================*/
/* Table: TMP_XF_JXC                                            */
/*==============================================================*/
create table BFCRM10.TMP_XF_JXC  (
   YEARMONTH            NUMBER(10)                      not null,
   MDID                 NUMBER(10)                      not null,
   XFJE_JXC             NUMBER(14,2),
   constraint PK_TMP_XF_JXC primary key (YEARMONTH, MDID)
)
/

/*==============================================================*/
/* Table: TMP_YCXFJL                                            */
/*==============================================================*/
create table BFCRM10.TMP_YCXFJL  (
   HYID                 NUMBER(10),
   MDID                 NUMBER(10),
   SKTNO                VARCHAR2(6),
   SKYDM                VARCHAR2(6),
   SKCS                 NUMBER(10),
   XFJE                 NUMBER(14,2),
   THJE                 NUMBER(14,2),
   THCS                 NUMBER(10),
   JF                   FLOAT(50)
)
/

/*==============================================================*/
/* Index: INX_TMPYCXFJL_HYID                                    */
/*==============================================================*/
create index BFCRM10.INX_TMPYCXFJL_HYID on BFCRM10.TMP_YCXFJL (
   HYID ASC
)
/

/*==============================================================*/
/* Table: TMP_YHQFF_SPFT                                        */
/*==============================================================*/
create table BFCRM10.TMP_YHQFF_SPFT  (
   SHDM                 VARCHAR2(4)                     not null,
   MDID                 NUMBER(10)                      not null,
   CXID                 NUMBER(10)                      not null,
   INX_XFRQ             NUMBER(5)                       not null,
   YHQID                NUMBER(5)                       not null,
   YHQFFGZID            NUMBER(10)                      not null,
   ZXFJE                NUMBER(14,2)                    not null,
   FQJE                 NUMBER(14,2)                    not null,
   BL                   FLOAT(50)                      default 0 not null,
   constraint PK_TMP_YHQFF_SPFT primary key (SHDM, MDID, CXID, INX_XFRQ, YHQID, YHQFFGZID)
)
/

/*==============================================================*/
/* Table: TMP_YHQFF_SPFT_SP                                     */
/*==============================================================*/
create table BFCRM10.TMP_YHQFF_SPFT_SP  (
   SHDM                 VARCHAR2(4)                     not null,
   MDID                 NUMBER(10)                      not null,
   CXID                 NUMBER(10)                      not null,
   INX_XFRQ             NUMBER(5)                       not null,
   YHQID                NUMBER(5)                       not null,
   YHQFFGZID            NUMBER(10)                      not null,
   SHSPID               NUMBER(10)                      not null,
   BMDM                 VARCHAR2(20)                    not null,
   ZXFJE                NUMBER(14,2)                    not null,
   FQJE                 NUMBER(14,2)                    not null,
   constraint PK_TMP_YHQFF_SPFT_SP primary key (SHDM, MDID, CXID, INX_XFRQ, YHQID, YHQFFGZID, SHSPID, BMDM)
)
/

/*==============================================================*/
/* Table: TMP_YHQFF_SPFT_SP1                                    */
/*==============================================================*/
create table BFCRM10.TMP_YHQFF_SPFT_SP1  (
   SHDM                 VARCHAR2(4)                     not null,
   CXHDBH               NUMBER(10)                      not null,
   INX_XFRQ             NUMBER(5)                       not null,
   YHQID                NUMBER(5)                       not null,
   YHQFFGZID            NUMBER(10)                      not null,
   SHSPID               NUMBER(10)                      not null,
   BMDM                 VARCHAR2(20)                    not null,
   ZXFJE                NUMBER(14,2)                    not null,
   FQJE                 NUMBER(14,2)                    not null,
   MDID                 NUMBER(10)                      not null,
   constraint PK_TMP_YHQFF_SPFT_SP1 primary key (SHDM, CXHDBH, INX_XFRQ, YHQID, YHQFFGZID, SHSPID, BMDM)
)
/

/*==============================================================*/
/* Table: TMP_YHQ_CXHD_HZ                                       */
/*==============================================================*/
create table BFCRM10.TMP_YHQ_CXHD_HZ  (
   SHDM                 VARCHAR2(4)                     not null,
   MDID                 NUMBER(10)                      not null,
   CXHDBH               NUMBER(10)                      not null,
   YHQID                NUMBER(5)                       not null,
   SHSPID               NUMBER(10)                      not null,
   BMDM                 VARCHAR2(20)                    not null,
   FQJE                 NUMBER(14,2)                    not null,
   constraint PK_TMP_YHQ_CXHD_HZ primary key (SHDM, SHSPID, MDID, CXHDBH, YHQID, BMDM)
)
/

/*==============================================================*/
/* Table: TSJL                                                  */
/*==============================================================*/
create table BFCRM10.TSJL  (
   JLBH                 NUMBER(10)                      not null,
   LDHM                 VARCHAR2(15),
   JTSJ                 DATE,
   GJSJ                 DATE,
   NXH                  VARCHAR2(6),
   TSRQ                 DATE,
   HYID                 NUMBER(10),
   TSFS                 NUMBER(5),
   TSLX                 NUMBER(5),
   MDID                 NUMBER(5),
   SPMC                 VARCHAR2(20),
   SHSPFLID             NUMBER(10),
   YYYDM                VARCHAR2(8),
   TSNR                 VARCHAR2(200),
   CLYJ                 VARCHAR2(100),
   ZRBM                 NUMBER(5),
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(16),
   ZXRQ                 DATE,
   ZFR                  NUMBER(10),
   ZFRMC                VARCHAR2(16),
   ZFRQ                 DATE,
   SBRQ                 DATE,
   HFR                  NUMBER(10),
   HFRMC                VARCHAR2(16),
   HFRQ                 DATE,
   HFJG                 NUMBER(5),
   DJR                  NUMBER(10),
   DJRMC                VARCHAR2(16),
   DJSJ                 DATE,
   STATUS               NUMBER(5),
   SHSBID               NUMBER(10),
   SPDM                 VARCHAR2(13),
   SHDM                 VARCHAR2(4),
   YYYMC                VARCHAR2(16),
   JE                   NUMBER(14,2)                   default 0,
   GHSDM                VARCHAR2(10),
   GSHMC                VARCHAR2(30),
   constraint PK_TSJL primary key (JLBH)
)
/

comment on column BFCRM10.TSJL.TSFS is
'0—总台 1—电话  2—网页'
/

comment on column BFCRM10.TSJL.TSLX is
'投诉商品/投诉服务/投诉环境/投诉缺货/建议'
/

comment on column BFCRM10.TSJL.ZRBM is
'0—本部门 1—上级  2—采购部门  3—送货部门'
/

comment on column BFCRM10.TSJL.HFJG is
'1—满意  2—基本满意  3—接受   4—不满   5—非常不满'
/

/*==============================================================*/
/* Table: TTT                                                   */
/*==============================================================*/
create table BFCRM10.TTT  (
   F1                   NUMBER(10)                      not null,
   DT                   TIMESTAMP,
   constraint PK_TTT primary key (F1)
)
/

/*==============================================================*/
/* Table: VIP_JT                                                */
/*==============================================================*/
create table BFCRM10.VIP_JT  (
   FDBH                 NUMBER(10)                      not null,
   FDNAME               VARCHAR2(30),
   VIP_NO               VARCHAR2(8)                     not null,
   VIP_NAME             VARCHAR2(20),
   SEX                  NUMBER(5),
   SFZBH                VARCHAR2(18),
   PHONE                VARCHAR2(18),
   POINT                FLOAT(50),
   LJPOINT              FLOAT(50),
   BZ                   NUMBER(5),
   constraint PK_VIP_JT primary key (FDBH, VIP_NO)
)
/

/*==============================================================*/
/* Table: XQDY                                                  */
/*==============================================================*/
create table BFCRM10.XQDY  (
   XQID                 NUMBER(10)                      not null,
   XQDM                 VARCHAR2(10),
   XQMC                 VARCHAR2(30),
   XQHS                 NUMBER(10),
   QYID                 NUMBER(10),
   BJ_TY                NUMBER(5),
   constraint PK_XQDY primary key (XQID)
)
/

/*==============================================================*/
/* Table: XQDYITEM                                              */
/*==============================================================*/
create table BFCRM10.XQDYITEM  (
   XQID                 NUMBER(10)                      not null,
   SQID                 NUMBER(10)                      not null,
   constraint PK_XQDYITEM primary key (SQID, XQID)
)
/

/*==============================================================*/
/* Table: XTCZY_BGDDQX                                          */
/*==============================================================*/
create table BFCRM10.XTCZY_BGDDQX  (
   PERSON_ID            NUMBER(10)                      not null,
   BGDDDM               VARCHAR2(20)                    not null,
   constraint PK_XTCZY_BGDDQX primary key (PERSON_ID, BGDDDM)
)
/

/*==============================================================*/
/* Table: XTCZY_BMCZQX                                          */
/*==============================================================*/
create table BFCRM10.XTCZY_BMCZQX  (
   PERSON_ID            NUMBER(10)                      not null,
   SHDM                 VARCHAR2(4)                     not null,
   BMDM                 VARCHAR2(10)                    not null,
   DM                   VARCHAR2(14),
   constraint PK_XTCZY_BMCZQX primary key (PERSON_ID, SHDM, BMDM)
)
/

/*==============================================================*/
/* Table: XTCZY_FXDWQX                                          */
/*==============================================================*/
create table BFCRM10.XTCZY_FXDWQX  (
   PERSON_ID            NUMBER(10)                      not null,
   FXDWDM               VARCHAR2(10)                    not null,
   constraint PK_XTCZY_FXDWQX primary key (PERSON_ID, FXDWDM)
)
/

/*==============================================================*/
/* Table: XTCZY_HYLXQX                                          */
/*==============================================================*/
create table BFCRM10.XTCZY_HYLXQX  (
   PERSON_ID            NUMBER(10)                      not null,
   HYKTYPE              NUMBER(5)                       not null,
   constraint PK_XTCZY_HYLXQX primary key (PERSON_ID, HYKTYPE)
)
/

/*==============================================================*/
/* Table: XTCZY_MDQX                                            */
/*==============================================================*/
create table BFCRM10.XTCZY_MDQX  (
   PERSON_ID            NUMBER(10)                      not null,
   MDID                 NUMBER(10)                      not null,
   constraint PK_XTCZY_MDQX primary key (PERSON_ID, MDID)
)
/

/*==============================================================*/
/* Table: XXBDJL                                                */
/*==============================================================*/
create table BFCRM10.XXBDJL  (
   JLBH                 NUMBER(10)                      not null,
   MSGID                NUMBER(10),
   XGR                  NUMBER(10),
   XGRDM                VARCHAR2(16),
   XGRMC                VARCHAR2(30),
   XGRQ                 DATE,
   KEYVALUE             VARCHAR2(20),
   constraint PK_XXBDJL primary key (JLBH)
)
/

/*==============================================================*/
/* Table: XXBDJLITEM                                            */
/*==============================================================*/
create table BFCRM10.XXBDJLITEM  (
   JLBH                 NUMBER(10)                      not null,
   TABLENAME            VARCHAR2(20)                    not null,
   FIELDNAME            VARCHAR2(20)                    not null,
   OLDVALUE             VARCHAR2(300),
   NEWVALUE             VARCHAR2(300),
   constraint PK_XXBDJLITEM primary key (JLBH, TABLENAME, FIELDNAME)
)
/

/*==============================================================*/
/* Table: YHBM                                                  */
/*==============================================================*/
create table BFCRM10.YHBM  (
   YHID                 NUMBER(5)                       not null,
   YHMC                 VARCHAR2(30)                    not null,
   HKQ                  NUMBER(5)                      default 0 not null,
   HKLL                 FLOAT(50)                      default 0 not null,
   SCJSZH               VARCHAR2(30),
   YHDKZH               VARCHAR2(30),
   FKBM                 VARCHAR2(30),
   MRSQH                VARCHAR2(30),
   constraint PK_YHBM primary key (YHID)
)
/

/*==============================================================*/
/* Table: YHKFQJE_CARD                                          */
/*==============================================================*/
create table BFCRM10.YHKFQJE_CARD  (
   CXID                 NUMBER(10)                      not null,
   YHQID                NUMBER(5)                       not null,
   BANK_KH              VARCHAR2(20)                    not null,
   MDID                 NUMBER(10)                      not null,
   BANK_ID              NUMBER(5)                       not null,
   JE                   NUMBER(14,2),
   CS                   NUMBER(5),
   constraint PK_YHKFQJE_CARD primary key (CXID, YHQID, BANK_KH, MDID)
)
/

/*==============================================================*/
/* Table: YHKFQJE_CARD_DAY                                      */
/*==============================================================*/
create table BFCRM10.YHKFQJE_CARD_DAY  (
   CXID                 NUMBER(10)                      not null,
   YHQID                NUMBER(5)                       not null,
   BANK_KH              VARCHAR2(20)                    not null,
   MDID                 NUMBER(10)                      not null,
   XFRQ                 VARCHAR2(8)                     not null,
   BANK_ID              NUMBER(5)                       not null,
   JE                   NUMBER(14,2),
   CS                   NUMBER(5),
   constraint PK_YHKFQJE_CARD_DAY primary key (CXID, YHQID, BANK_KH, MDID, XFRQ)
)
/

/*==============================================================*/
/* Table: YHKFQZJE                                              */
/*==============================================================*/
create table BFCRM10.YHKFQZJE  (
   CXID                 NUMBER(10)                      not null,
   YHQID                NUMBER(5)                       not null,
   BANK_ID              NUMBER(5)                       not null,
   MDID                 NUMBER(10)                      not null,
   JE                   NUMBER(14,2),
   constraint PK_YHKFQZJE primary key (CXID, YHQID, BANK_ID, MDID)
)
/

/*==============================================================*/
/* Table: YHKFQZJE_DAY                                          */
/*==============================================================*/
create table BFCRM10.YHKFQZJE_DAY  (
   CXID                 NUMBER(10)                      not null,
   YHQID                NUMBER(5)                       not null,
   BANK_ID              NUMBER(5)                       not null,
   MDID                 NUMBER(10)                      not null,
   XFRQ                 VARCHAR2(8)                     not null,
   JE                   NUMBER(14,2),
   constraint PK_YHKFQZJE_DAY primary key (CXID, YHQID, BANK_ID, MDID, XFRQ)
)
/

/*==============================================================*/
/* Table: YHPOSXX                                               */
/*==============================================================*/
create table BFCRM10.YHPOSXX  (
   MACHINE              VARCHAR2(10)                    not null,
   XLH                  VARCHAR2(20),
   IPADDRESS            VARCHAR2(20),
   MAINKEY              VARCHAR2(20),
   constraint PK_YHPOSXX primary key (MACHINE)
)
/

/*==============================================================*/
/* Table: YHQCKGZ                                               */
/*==============================================================*/
create table BFCRM10.YHQCKGZ  (
   JLBH                 NUMBER(10)                      not null,
   KSRQ                 DATE,
   JSRQ                 DATE,
   YHQID                NUMBER(10)                      not null,
   YHQID_XJ             NUMBER(10),
   YHQSYJSRQ            DATE,
   MDID                 NUMBER(10),
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16)                    not null,
   DJSJ                 DATE                            not null,
   constraint PK_YHQCKGZ primary key (JLBH)
)
/

/*==============================================================*/
/* Table: YHQCKGZITEM                                           */
/*==============================================================*/
create table BFCRM10.YHQCKGZITEM  (
   JLBH                 NUMBER(10)                      not null,
   SKJE                 NUMBER(14,2)                    not null,
   FQJE                 NUMBER(14,2)                    not null,
   constraint PK_YHQCKGZITEM primary key (JLBH, SKJE)
)
/

/*==============================================================*/
/* Table: YHQCODE                                               */
/*==============================================================*/
create table BFCRM10.YHQCODE  (
   YHQCODE              VARCHAR2(20)                    not null,
   YHQID                NUMBER(5)                       not null,
   YHQMZ                NUMBER(14,2)                   default 0 not null,
   BEGINDATE            DATE,
   ENDDATE              DATE                            not null,
   CXID                 NUMBER(10),
   MDID_FQ              NUMBER(10),
   FQSJ                 DATE,
   MDID_YQ              NUMBER(10),
   YQSJ                 DATE,
   STATUS               NUMBER(5),
   HYID                 NUMBER(10),
   constraint PK_YHQCODE primary key (YHQCODE)
)
/

comment on column BFCRM10.YHQCODE.STATUS is
'0 库存
1 可用
-1 顾客已用
-2 退货收回
-3 已作废'
/

/*==============================================================*/
/* Table: YHQDEF                                                */
/*==============================================================*/
create sequence BFCRM10.S_YHQDEF
/
create table BFCRM10.YHQDEF  (
   YHQID                NUMBER(5)                       not null,
   YHQMC                VARCHAR2(30)                    not null,
   BJ_DZYHQ             NUMBER(5)                      default 1 not null,
   BJ_TY                NUMBER(5)                      default 0 not null,
   BJ_FQ                NUMBER(5)                      default 0 not null,
   FQLX                 NUMBER(5)                      default 0 not null,
   FS_YQMDFW            NUMBER(5)                      default 1
      constraint CKC_FS_YQMDFW_YHQDEF check (FS_YQMDFW is null or (FS_YQMDFW in (1,2,3))),
   FS_FQMDFW            NUMBER(5)                      default 1
      constraint CKC_FS_FQMDFW_YHQDEF check (FS_FQMDFW is null or (FS_FQMDFW in (1,2,3))),
   YXQTS                NUMBER(10)                     default 0,
   BJ_CXYHQ             NUMBER(5)                      default 0,
   TM                   NUMBER(10),
   BJ_REMOTE            NUMBER(5),
   ISCODED              NUMBER(1)                      default 0,
   CODELEN              NUMBER(5),
   CODEPRE              VARCHAR2(6),
   CODESUF              VARCHAR2(6),
   BJ_TS                NUMBER(5)                      default 0,
   BJ_BKQ               NUMBER(5)                      default 0,
   IMAGEURL             VARCHAR2(100),
   constraint PK_YHQDEF primary key (YHQID)
)
/

/*==============================================================*/
/* Table: YHQDEF_CXHD                                           */
/*==============================================================*/
create table BFCRM10.YHQDEF_CXHD  (
   YHQID                NUMBER(5)                       not null,
   SHDM                 VARCHAR2(4)                     not null,
   CXID                 NUMBER(10)                      not null,
   YHQSYJSRQ            DATE                            not null,
   ZQDW                 VARCHAR2(10),
   ZQBZ1                VARCHAR2(255),
   ZQBZ2                VARCHAR2(255),
   YXQTS                NUMBER(10),
   CXHDBH               NUMBER(10),
   constraint PK_YHQDEF_CXHD primary key (YHQID, SHDM, CXID)
)
/

/*==============================================================*/
/* Table: YHQFFDYD_FD                                           */
/*==============================================================*/
create table BFCRM10.YHQFFDYD_FD  (
   YHQFFDBH             NUMBER(10)                      not null,
   INX                  NUMBER(5)                       not null,
   SD                   RAW(96)                         not null,
   YXJ                  NUMBER(5)                       not null,
   constraint PK_YHQFFDYD_FD primary key (YHQFFDBH, INX)
)
/

/*==============================================================*/
/* Table: YHQFFDYD_GZSD                                         */
/*==============================================================*/
create table BFCRM10.YHQFFDYD_GZSD  (
   YHQFFDBH             NUMBER(10)                      not null,
   INX                  NUMBER(5)                       not null,
   GZBH                 NUMBER(5)                       not null,
   YHQFFGZID            NUMBER(10)                      not null,
   YXJ                  NUMBER(5)                       not null,
   CLFS_BM              NUMBER(5)                      default 0 not null,
   CLFS_SPFL            NUMBER(5)                      default 0 not null,
   CLFS_SPSB            NUMBER(5)                      default 0 not null,
   CLFS_HT              NUMBER(5)                      default 0 not null,
   constraint PK_YHQFFDYD_GZSD primary key (YHQFFDBH, INX, GZBH)
)
/

comment on column BFCRM10.YHQFFDYD_GZSD.CLFS_BM is
'1 等于
0 不处理
-1 不等于'
/

/*==============================================================*/
/* Table: YHQFFDYD_GZSDITEM                                     */
/*==============================================================*/
create table BFCRM10.YHQFFDYD_GZSDITEM  (
   YHQFFDBH             NUMBER(10)                      not null,
   INX                  NUMBER(5)                       not null,
   GZBH                 NUMBER(5)                       not null,
   SJLX                 NUMBER(5)                       not null,
   SJNR                 NUMBER(10)                      not null,
   XH                   NUMBER(10)                     default 0,
   constraint PK_YHQFFDYD_GZSDITEM primary key (YHQFFDBH, INX, GZBH, SJLX, SJNR)
)
/

comment on column BFCRM10.YHQFFDYD_GZSDITEM.SJLX is
'1 部门
2 合同
3 分类
4 品牌'
/

/*==============================================================*/
/* Table: YHQFFDYD_SP                                           */
/*==============================================================*/
create table BFCRM10.YHQFFDYD_SP  (
   YHQFFDBH             NUMBER(10)                      not null,
   INX                  NUMBER(5)                       not null,
   SHSPID               NUMBER(10)                      not null,
   YHQFFGZID            NUMBER(10)                      not null,
   CLFS_SP              NUMBER(5)                      default 0 not null,
   XH                   NUMBER(10),
   constraint PK_YHQFFDYD_SP primary key (YHQFFDBH, INX, SHSPID)
)
/

/*==============================================================*/
/* Table: YHQFFGZ                                               */
/*==============================================================*/
create table BFCRM10.YHQFFGZ  (
   YHQFFGZID            NUMBER(10)                      not null,
   YHQFFGZMC            VARCHAR2(20),
   FFXE                 NUMBER(14,2)                   default 0,
   BJ_TY                NUMBER(5)                      default 0 not null,
   BJ_BFQGZ             NUMBER(5)                      default 0 not null,
   BJ_SF                NUMBER(5)                      default 0,
   FFQDJE               NUMBER(14,2)                   default 0,
   LX                   NUMBER(5)                      default 0,
   CLFS                 NUMBER(5)                      default 0,
   BJ_TYPE              NUMBER(5),
   MDID                 NUMBER(10),
   constraint PK_YHQFFGZ primary key (YHQFFGZID)
)
/

/*==============================================================*/
/* Table: YHQFFGZITEM                                           */
/*==============================================================*/
create table BFCRM10.YHQFFGZITEM  (
   YHQFFGZID            NUMBER(10)                      not null,
   XSJE                 NUMBER(14,2)                   default 0 not null,
   FQJE                 NUMBER(14,2)                   default 0 not null,
   LPDM                 VARCHAR2(6),
   constraint PK_YHQFFGZITEM primary key (YHQFFGZID, XSJE)
)
/

/*==============================================================*/
/* Table: YHQFFGZITEM_DELETE                                    */
/*==============================================================*/
create table BFCRM10.YHQFFGZITEM_DELETE  (
   YHQFFGZID            NUMBER(10)                      not null,
   XSJE                 NUMBER(14,2)                   default 0 not null,
   FQJE                 NUMBER(14,2)                   default 0 not null,
   constraint PK_YHQFFGZITEM_DELETE primary key (YHQFFGZID, XSJE)
)
/

/*==============================================================*/
/* Table: YHQFFGZ_DELETE                                        */
/*==============================================================*/
create table BFCRM10.YHQFFGZ_DELETE  (
   YHQFFGZID            NUMBER(10)                      not null,
   YHQFFGZMC            VARCHAR2(20),
   FFXE                 NUMBER(14,2)                   default 0,
   BJ_TY                NUMBER(5)                      default 0 not null,
   BJ_BFQGZ             NUMBER(5)                      default 0 not null,
   SCR                  NUMBER(10)                      not null,
   SCRMC                VARCHAR2(10)                    not null,
   SCSJ                 DATE                            not null,
   CLLX                 NUMBER(5)                       not null,
   constraint PK_YHQFFGZ_DELETE primary key (YHQFFGZID)
)
/

/*==============================================================*/
/* Table: YHQFFJL_YHQJE                                         */
/*==============================================================*/
create table BFCRM10.YHQFFJL_YHQJE  (
   YHQFFJLID            NUMBER(9,0)                     not null,
   YHQID                NUMBER(5)                       not null,
   YHQSYJSRQ            DATE                            not null,
   YHQFFGZID            NUMBER(10)                      not null,
   YHQJE                NUMBER(14,2)                    not null,
   constraint PK_YHQFFJL_YHQJE primary key (YHQFFJLID, YHQID, YHQSYJSRQ, YHQFFGZID)
)
/

/*==============================================================*/
/* Table: YHQFF_GZSJ_DELSP                                      */
/*==============================================================*/
create table BFCRM10.YHQFF_GZSJ_DELSP  (
   YHQFFDBH             NUMBER(10)                      not null,
   SHSPID               NUMBER(10)                      not null,
   SPDM                 VARCHAR2(13),
   constraint PK_YHQFF_GZSJ_DELSP primary key (YHQFFDBH, SHSPID)
)
/

/*==============================================================*/
/* Table: YHQFF_GZSJ_HT                                         */
/*==============================================================*/
create table BFCRM10.YHQFF_GZSJ_HT  (
   YHQFFDBH             NUMBER(10)                      not null,
   SHHTID               NUMBER(10)                      not null,
   HTH                  VARCHAR2(10)                    not null,
   GHSDM                VARCHAR2(10),
   GHSMC                VARCHAR2(30),
   constraint PK_YHQFF_GZSJ_HT primary key (YHQFFDBH, SHHTID)
)
/

/*==============================================================*/
/* Table: YHQFF_SPFTRJL                                         */
/*==============================================================*/
create table BFCRM10.YHQFF_SPFTRJL  (
   RQ                   DATE                            not null,
   SHDM                 VARCHAR2(4)                     not null,
   MDID                 NUMBER(10)                      not null,
   CXID                 NUMBER(10)                      not null,
   INX_XFRQ             NUMBER(5)                       not null,
   YHQID                NUMBER(5)                       not null,
   YHQFFGZID            NUMBER(10)                      not null,
   SHSPID               NUMBER(10)                      not null,
   BMDM                 VARCHAR2(20)                    not null,
   ZXFJE                NUMBER(14,2)                    not null,
   FQJE                 NUMBER(14,2)                    not null,
   constraint PK_YHQFF_SPFTRJL primary key (RQ, SHDM, MDID, CXID, INX_XFRQ, YHQID, YHQFFGZID, SHSPID, BMDM)
)
/

/*==============================================================*/
/* Table: YHQMZDEF                                              */
/*==============================================================*/
create table BFCRM10.YHQMZDEF  (
   YHQMZID              NUMBER(10)                      not null,
   NAME                 VARCHAR2(10)                    not null,
   YHQID                NUMBER(10)                      not null,
   JE                   NUMBER(14,2)                    not null,
   BJ_TY                NUMBER(5)                      default 0 not null,
   constraint PK_YHQMZDEF primary key (YHQMZID)
)
/

/*==============================================================*/
/* Table: YHQSYDYD_FD                                           */
/*==============================================================*/
create table BFCRM10.YHQSYDYD_FD  (
   YHQSYDBH             NUMBER(10)                      not null,
   INX                  NUMBER(5)                       not null,
   SD                   RAW(96)                         not null,
   YXJ                  NUMBER(5)                       not null,
   constraint PK_YHQSYDYD_FD primary key (YHQSYDBH, INX)
)
/

/*==============================================================*/
/* Table: YHQSYDYD_GZSD                                         */
/*==============================================================*/
create table BFCRM10.YHQSYDYD_GZSD  (
   YHQSYDBH             NUMBER(10)                      not null,
   INX                  NUMBER(5)                       not null,
   GZBH                 NUMBER(5)                       not null,
   YHQSYGZID            NUMBER(10)                      not null,
   YXJ                  NUMBER(5)                       not null,
   CLFS_BM              NUMBER(5)                      default 0 not null,
   CLFS_SPFL            NUMBER(5)                      default 0 not null,
   CLFS_SPSB            NUMBER(5)                      default 0 not null,
   CLFS_HT              NUMBER(5)                      default 0 not null,
   constraint PK_YHQSYDYD_GZSD primary key (YHQSYDBH, INX, GZBH)
)
/

comment on column BFCRM10.YHQSYDYD_GZSD.CLFS_BM is
'1 等于
0 不处理
-1 不等于'
/

/*==============================================================*/
/* Table: YHQSYDYD_GZSDITEM                                     */
/*==============================================================*/
create table BFCRM10.YHQSYDYD_GZSDITEM  (
   YHQSYDBH             NUMBER(10)                      not null,
   INX                  NUMBER(5)                       not null,
   GZBH                 NUMBER(5)                       not null,
   SJLX                 NUMBER(5)                       not null,
   SJNR                 NUMBER(10)                      not null,
   XH                   NUMBER(10)                     default 0,
   constraint PK_YHQSYDYD_GZSDITEM primary key (YHQSYDBH, INX, GZBH, SJLX, SJNR)
)
/

comment on column BFCRM10.YHQSYDYD_GZSDITEM.SJLX is
'1 部门
2 合同
3 分类
4 品牌'
/

/*==============================================================*/
/* Table: YHQSYDYD_SP                                           */
/*==============================================================*/
create table BFCRM10.YHQSYDYD_SP  (
   YHQSYDBH             NUMBER(10)                      not null,
   INX                  NUMBER(5)                       not null,
   SHSPID               NUMBER(10)                      not null,
   YHQSYGZID            NUMBER(10)                      not null,
   CLFS_SP              NUMBER(5)                      default 0 not null,
   XH                   NUMBER(10),
   constraint PK_YHQSYDYD_SP primary key (YHQSYDBH, INX, SHSPID)
)
/

/*==============================================================*/
/* Index: ZKDITEM_SPID                                          */
/*==============================================================*/
create index BFCRM10.ZKDITEM_SPID on BFCRM10.YHQSYDYD_SP (
   SHSPID ASC
)
/

/*==============================================================*/
/* Table: YHQSYGZ                                               */
/*==============================================================*/
create table BFCRM10.YHQSYGZ  (
   YHQSYGZID            NUMBER(10)                      not null,
   YHQSYGZMC            VARCHAR2(20)                    not null,
   YQBL_XFJE            NUMBER(14,2)                   default 0 not null,
   YQBL_YHQJE           NUMBER(14,2)                   default 0 not null,
   BJ_TY                NUMBER(5)                      default 0 not null,
   BJ_PB                NUMBER(5)                      default 0,
   ZDYQJE               NUMBER(14,2)                   default 0,
   MDID                 NUMBER(10),
   constraint PK_YHQSYGZ primary key (YHQSYGZID)
)
/

/*==============================================================*/
/* Table: YHQSYGZITEM                                           */
/*==============================================================*/
create table BFCRM10.YHQSYGZITEM  (
   YHQSYGZID            INT                             not null,
   YQBL_XFJE            NUMBER(14,2)                   default 0 not null,
   YQBL_YHQJE           NUMBER(14,2)                   default 0 not null,
   constraint PK_YHQSYGZITEM primary key (YHQSYGZID, YQBL_XFJE)
)
/

/*==============================================================*/
/* Table: YHQSYSH                                               */
/*==============================================================*/
create table BFCRM10.YHQSYSH  (
   SHDM                 VARCHAR2(4)                     not null,
   YHQID                NUMBER(5)                       not null,
   BJ_SYD               NUMBER(5)                       not null,
   constraint PK_YHQSYSH primary key (SHDM, YHQID)
)
/

/*==============================================================*/
/* Table: YHQ_CXHD_BMHZ                                         */
/*==============================================================*/
create table BFCRM10.YHQ_CXHD_BMHZ  (
   RQ                   DATE                            not null,
   SHDM                 VARCHAR2(4)                     not null,
   MDID                 NUMBER(10)                      not null,
   CXID                 NUMBER(10)                      not null,
   YHQID                NUMBER(5)                       not null,
   BMDM                 VARCHAR2(20)                    not null,
   YQJE                 NUMBER(14,2)                    not null,
   FQJE                 NUMBER(14,2)                    not null,
   ZXFJE                NUMBER(14,2)                   default 0,
   constraint PK_YHQ_CXHD_BMHZ primary key (RQ, SHDM, MDID, CXID, YHQID, BMDM)
)
/

/*==============================================================*/
/* Table: YHQ_CXHD_HTHZ                                         */
/*==============================================================*/
create table BFCRM10.YHQ_CXHD_HTHZ  (
   RQ                   DATE                            not null,
   SHDM                 VARCHAR2(4)                     not null,
   MDID                 NUMBER(10)                      not null,
   CXID                 NUMBER(10)                      not null,
   YHQID                NUMBER(5)                       not null,
   HTH                  NUMBER(10)                      not null,
   BMDM                 VARCHAR2(20)                    not null,
   YQJE                 NUMBER(14,2)                    not null,
   FQJE                 NUMBER(14,2)                    not null,
   ZXFJE                NUMBER(14,2)                   default 0,
   constraint PK_YHQ_CXHD_HTHZ primary key (RQ, SHDM, MDID, CXID, YHQID, HTH, BMDM)
)
/

/*==============================================================*/
/* Table: YHQ_CXHD_HZ                                           */
/*==============================================================*/
create table BFCRM10.YHQ_CXHD_HZ  (
   RQ                   DATE                            not null,
   SHDM                 VARCHAR2(4)                     not null,
   MDID                 NUMBER(10)                      not null,
   CXID                 NUMBER(10)                      not null,
   YHQID                NUMBER(5)                       not null,
   SHSPID               NUMBER(10)                      not null,
   BMDM                 VARCHAR2(20)                    not null,
   YQJE                 NUMBER(14,2)                    not null,
   FQJE                 NUMBER(14,2)                    not null,
   ZXFJE                NUMBER(14,2)                   default 0,
   constraint PK_YHQ_CXHD_HZ primary key (RQ, SHDM, SHSPID, MDID, CXID, YHQID, BMDM)
)
/

/*==============================================================*/
/* Table: YHQ_YEQLTZD                                           */
/*==============================================================*/
create table BFCRM10.YHQ_YEQLTZD  (
   JLBH                 NUMBER(10)                      not null,
   HYKTYPE              NUMBER(10)                      not null,
   CZDD                 VARCHAR2(10),
   YHQID                NUMBER(10)                      not null,
   TZSL                 NUMBER(10),
   TZJE                 NUMBER(14,2),
   ZY                   VARCHAR2(40),
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16)                    not null,
   DJSJ                 DATE                            not null,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(16),
   ZXRQ                 DATE,
   CXID                 NUMBER(10),
   constraint PK_YHQ_YEQLTZD primary key (JLBH)
)
/

/*==============================================================*/
/* Table: YHQ_YEQLTZD_ITEM                                      */
/*==============================================================*/
create table BFCRM10.YHQ_YEQLTZD_ITEM  (
   JLBH                 NUMBER(10)                      not null,
   HYID                 NUMBER(10)                      not null,
   CXID                 NUMBER(10)                      not null,
   JSRQ                 DATE                            not null,
   MDFWDM               VARCHAR2(10)                    not null,
   YYE                  NUMBER(14,2),
   TZJE                 NUMBER(14,2),
   constraint PK_YHQ_YEQLTZD_ITEM primary key (JLBH, HYID, CXID, JSRQ, MDFWDM)
)
/

/*==============================================================*/
/* Table: YHQ_YXQBDJL                                           */
/*==============================================================*/
create table BFCRM10.YHQ_YXQBDJL  (
   JLBH                 NUMBER(10)                      not null,
   XYXQ                 DATE,
   CXID                 NUMBER(10)                      not null,
   MDID                 NUMBER(5)                       not null,
   DJR                  NUMBER(10)                      not null,
   DJRMC                varchar2(16)                    not null,
   DJSJ                 DATE                            not null,
   ZXR                  NUMBER(10),
   ZXRMC                varchar2(16),
   ZXRQ                 DATE,
   ZY                   varchar2(50),
   constraint PK_YHQ_YXQBDJL primary key (JLBH)
)
/

/*==============================================================*/
/* Table: YHQ_YXQBDJLITEM                                       */
/*==============================================================*/
create table BFCRM10.YHQ_YXQBDJLITEM  (
   JLBH                 NUMBER(10)                      not null,
   HYID                 NUMBER(10)                      not null,
   YHQID                NUMBER(10)                      not null,
   MDFWDM               VARCHAR2(20)                    not null,
   YYXQ                 DATE                            not null,
   JE                   NUMBER(14,2),
   constraint PK_YHQ_YXQBDJLITEM primary key (JLBH, HYID, YHQID, YYXQ, MDFWDM)
)
/

/*==============================================================*/
/* Table: YHXX                                                  */
/*==============================================================*/
create table BFCRM10.YHXX  (
   YHID                 NUMBER(10)                      not null,
   YHMC                 VARCHAR2(30)                    not null,
   constraint PK_YHXX primary key (YHID)
)
/

/*==============================================================*/
/* Table: YHXXITEM                                              */
/*==============================================================*/
create table BFCRM10.YHXXITEM  (
   ID                   NUMBER(10)                      not null,
   YHID                 NUMBER(10)                      not null,
   INX                  NUMBER(5)                       not null,
   CODE1                VARCHAR2(20)                    not null,
   CODE2                VARCHAR2(20)                    not null,
   NAME                 VARCHAR2(100)                   not null,
   BJ_TY                NUMBER(5)                      default 0,
   SHDM                 VARCHAR2(10),
   CXID                 NUMBER(10)                     default 0,
   constraint PK_YHXXITEM primary key (ID)
)
/

/*==============================================================*/
/* Table: YWY                                                   */
/*==============================================================*/
create table BFCRM10.YWY  (
   YWYID                NUMBER(10)                      not null,
   YWYDM                VARCHAR2(10),
   NAME                 VARCHAR2(20),
   MDID                 NUMBER(10),
   BZ                   VARCHAR2(50),
   BJ_TY                NUMBER(5),
   constraint PK_YWY primary key (YWYID)
)
/

/*==============================================================*/
/* Table: YWYPS                                                 */
/*==============================================================*/
create table BFCRM10.YWYPS  (
   JLBH                 NUMBER(10)                      not null,
   HYID                 NUMBER(10)                      not null,
   PSNR                 varchar2(100),
   PSJG                 NUMBER(10),
   DJR                  NUMBER(10)                      not null,
   DJRMC                varchar2(16)                    not null,
   DJSJ                 DATE                            not null,
   constraint PK_YWYPS primary key (JLBH)
)
/

/*==============================================================*/
/* Table: ZFFS                                                  */
/*==============================================================*/
create table BFCRM10.ZFFS  (
   ZFFSID               NUMBER(10)                      not null,
   ZFFSDM               VARCHAR2(10)                    not null,
   ZFFSMC               VARCHAR2(30)                    not null,
   BJ_MJ                NUMBER(1)                       not null
      constraint CKC_BJ_MJ_ZFFS check (BJ_MJ in (0,1)),
   TYPE                 NUMBER(5),
   BJ_DZQDCZK           NUMBER(5),
   constraint PK_ZFFS primary key (ZFFSID)
)
/

/*==============================================================*/
/* Index: ZSFS_DM                                               */
/*==============================================================*/
create unique index BFCRM10.ZSFS_DM on BFCRM10.ZFFS (
   ZFFSDM ASC
)
/

/*==============================================================*/
/* Table: ZQMZDY                                                */
/*==============================================================*/
create table BFCRM10.ZQMZDY  (
   YHQID                NUMBER(10)                      not null,
   MZJE                 NUMBER(14,2)                    not null,
   constraint PK_ZQMZDY primary key (YHQID, MZJE)
)
/

/*==============================================================*/
/* View: HYK_GRXX                                               */
/*==============================================================*/
create or replace view BFCRM10.HYK_GRXX as
select H.HYID,G.* from HYK_HYXX H,HYK_GKDA G
where H.GKID=G.GKID

with read only
/


--  Before insert trigger "BFCRM10."tib_cxhddef"" for table "BFCRM10.CXHDDEF"
select 'create trigger BFCRM10."tib_cxhddef"' from dual
/
create or replace trigger BFCRM10."tib_cxhddef" before insert
on BFCRM10.CXHDDEF for each row
begin
    select BFCRM10.S_CXHDDEF.NextVal into :NEW.TM from dual;
end;
/


--  Before update trigger "BFCRM10."tiu_cxhddef"" for table "BFCRM10.CXHDDEF"
select 'create trigger BFCRM10."tiu_cxhddef"' from dual
/
create or replace trigger BFCRM10."tiu_cxhddef" before update
on BFCRM10.CXHDDEF for each row
begin
    select BFCRM10.S_CXHDDEF.NextVal into :NEW.TM from dual;
end;
/


--  Before insert trigger "BFCRM10.TIB_FXDWDEF" for table "BFCRM10.FXDWDEF"
create trigger BFCRM10.TIB_FXDWDEF before insert
on BFCRM10.FXDWDEF for each row
declare
    integrity_error  exception;
    errno            integer;
    errmsg           char(200);
    dummy            integer;
    found            boolean;

begin
        
    select BFCRM10.S_FXDWDEF.NextVal into :NEW.TM from dual;
    
--  Errors handling
exception
    when integrity_error then
       raise_application_error(errno, errmsg);
end;
/


--  Before update trigger "BFCRM10.TIU_FXDWDEF" for table "BFCRM10.FXDWDEF"
select 'create trigger BFCRM10.TIU_FXDWDEF' from dual
/
create or replace trigger BFCRM10.TIU_FXDWDEF before update
on BFCRM10.FXDWDEF for each row
begin
    select BFCRM10.S_FXDWDEF.NextVal into :NEW.TM from dual;
end;
/


--  Before insert trigger "BFCRM10.TIB_GXSHXXDY" for table "BFCRM10.GXSHXXDY"
select 'create trigger BFCRM10.TIB_GXSHXXDY' from dual
/
create or replace trigger BFCRM10.TIB_GXSHXXDY before insert
on BFCRM10.GXSHXXDY for each row
begin
    select BFCRM10.S_GXSHXXDY.NextVal into :NEW.TM from dual;
end;
/


--  Before update trigger "BFCRM10.TIU_GXSHXXDY" for table "BFCRM10.GXSHXXDY"
select 'create trigger BFCRM10.TIU_GXSHXXDY' from dual
/
create or replace trigger BFCRM10.TIU_GXSHXXDY before update
on BFCRM10.GXSHXXDY for each row
begin
    select BFCRM10.S_GXSHXXDY.NextVal into :NEW.TM from dual;
end;
/


--  Before insert trigger "BFCRM10.TIB_GZXFJFR" for table "BFCRM10.GZXFJFR_CRM"
select 'create trigger BFCRM10.TIB_GZXFJFR' from dual
/
create or replace trigger BFCRM10.TIB_GZXFJFR before insert
on BFCRM10.GZXFJFR_CRM for each row
begin
    select BFCRM10.S_GZXFJFR_CRM.NextVal into :NEW.TM from dual;
end;
/


--  Before update trigger "BFCRM10.TIU_GZXFJFR" for table "BFCRM10.GZXFJFR_CRM"
select 'create trigger BFCRM10.TIU_GZXFJFR' from dual
/
create or replace trigger BFCRM10.TIU_GZXFJFR before update
on BFCRM10.GZXFJFR_CRM for each row
begin
    select BFCRM10.S_GZXFJFR_CRM.NextVal into :NEW.TM from dual;
end;
/


--  Before insert trigger "BFCRM10.TIB_HYKDEF" for table "BFCRM10.HYKDEF"
select 'create trigger BFCRM10.TIB_HYKDEF' from dual
/
create or replace trigger BFCRM10.TIB_HYKDEF before insert
on BFCRM10.HYKDEF for each row
begin
    select BFCRM10.S_HYKDEF.NextVal into :NEW.TM from dual;
end;
/


--  Before update trigger "BFCRM10.TIU_HYKDEF" for table "BFCRM10.HYKDEF"
select 'create trigger BFCRM10.TIU_HYKDEF' from dual
/
create or replace trigger BFCRM10.TIU_HYKDEF before update
on BFCRM10.HYKDEF for each row
begin
    select BFCRM10.S_HYKDEF.NextVal into :NEW.TM from dual;
end;
/


--  Before insert trigger "BFCRM10.TIB_HYKJKJL" for table "BFCRM10.HYKJKJL"
create or replace trigger BFCRM10.TIB_HYKJKJL before insert
on BFCRM10.HYKJKJL for each row
declare
    integrity_error  exception;
    errno            integer;
    errmsg           char(200);
    vcount           integer;
    found            boolean;

begin

    /*
    IF (SELECT COUNT(BFCRM.HYKJKJLITEM.CZKHM) FROM BFCRM.HYKJKJLITEM,inserted
	  where BFCRM.HYKJKJLITEM.CZKHM between inserted.CZKHM_BEGIN and inserted.CZKHM_END and inserted.JKFS=0 and datalength(inserted.CZKHM_BEGIN)=datalength(BFCRM.HYKJKJLITEM.CZKHM) ) >0
    begin
       select @errno  = 30001,
              @errmsg = '重复卡号，不能执行!'
       goto error
    end
    */
    if :NEW.JKFS=0 then
      select count(*) into vCount 
        from BFCRM10.HYKJKJLITEM
        where CZKHM>=:NEW.CZKHM_BEGIN
          and CZKHM<=:NEW.CZKHM_END
          and length(CZKHM)=length(:NEW.CZKHM_BEGIN);
      if vCount>0 then
        errno  := -20001;
        errmsg := '重复卡号，不能执行!';
        raise integrity_error;
      end if;
    end if;
    
--  Errors handling
exception
    when integrity_error then
       raise_application_error(errno, errmsg);
end;
/


--  Before delete trigger "BFCRM10.TDB_HYKJKJL" for table "BFCRM10.HYKJKJL"
create or replace trigger BFCRM10.TDB_HYKJKJL before delete
on BFCRM10.HYKJKJL for each row
declare
    integrity_error  exception;
    errno            integer;
    errmsg           char(200);
    dummy            integer;
    found            boolean;

begin

    if :OLD.ZXR is not null then
      errno  := -20001;
      errmsg := '已审核的记录，不能删除!';
      raise integrity_error;
    end if;

--  Errors handling
exception
    when integrity_error then
       raise_application_error(errno, errmsg);
end;
/


--  Before insert trigger "BFCRM10."ti_hykkbkhdmx"" for table "BFCRM10.HYKKBKHDMX"
create trigger BFCRM10."ti_hykkbkhdmx" before insert
on BFCRM10.HYKKBKHDMX for each row
declare
    integrity_error  exception;
    errno            integer;
    errmsg           char(200);
    dummy            integer;
    found            boolean;

begin

    select BFCRM10.S_HYKKBKHDMX.NextVal into :NEW.XH from dual;
    
--  Errors handling
exception
    when integrity_error then
       raise_application_error(errno, errmsg);
end;
/


--  Before insert trigger "BFCRM10.TIB_HYKKZDEF" for table "BFCRM10.HYKKZDEF"
select 'create trigger BFCRM10.TIB_HYKKZDEF' from dual
/
create or replace trigger BFCRM10.TIB_HYKKZDEF before insert
on BFCRM10.HYKKZDEF for each row
begin
    select BFCRM10.S_HYKKZDEF.NextVal into :NEW.TM from dual;
end;
/


--  Before update trigger "BFCRM10.TIU_HYKKZDEF" for table "BFCRM10.HYKKZDEF"
select 'create trigger BFCRM10.TIU_HYKKZDEF' from dual
/
create or replace trigger BFCRM10.TIU_HYKKZDEF before update
on BFCRM10.HYKKZDEF for each row
begin
    select BFCRM10.S_HYKKZDEF.NextVal into :NEW.TM from dual;
end;
/


--  Before insert trigger "BFCRM10.TIB_HYKSBXPMX" for table "BFCRM10.HYKSBXPMX"
select 'create trigger BFCRM10.TIB_HYKSBXPMX' from dual
/
create or replace trigger BFCRM10.TIB_HYKSBXPMX before insert
on BFCRM10.HYKSBXPMX for each row
begin
    select BFCRM10.S_HYKSBXPMX.NextVal into :NEW.TM from dual;
end;
/


--  Before update trigger "BFCRM10.TIU_HYKSBXPMX" for table "BFCRM10.HYKSBXPMX"
select 'create trigger BFCRM10.TIU_HYKSBXPMX' from dual
/
create or replace trigger BFCRM10.TIU_HYKSBXPMX before update
on BFCRM10.HYKSBXPMX for each row
begin
    select BFCRM10.S_HYKSBXPMX.NextVal into :NEW.TM from dual;
end;
/


--  Before insert trigger "BFCRM10.TIB_HYK_CHILD_JL" for table "BFCRM10.HYK_CHILD_JL"
select 'create trigger BFCRM10.TIB_HYK_CHILD_JL' from dual
/
create or replace trigger BFCRM10.TIB_HYK_CHILD_JL before insert
on BFCRM10.HYK_CHILD_JL for each row
begin
    select BFCRM10.S_HYK_CHILD_JL.NextVal into :NEW.TM from dual;
end;
/


--  Before update trigger "BFCRM10.TIU_HYK_CHILD_JL" for table "BFCRM10.HYK_CHILD_JL"
select 'create trigger BFCRM10.TIU_HYK_CHILD_JL' from dual
/
create or replace trigger BFCRM10.TIU_HYK_CHILD_JL before update
on BFCRM10.HYK_CHILD_JL for each row
begin
    select BFCRM10.S_HYK_CHILD_JL.NextVal into :NEW.TM from dual;
end;
/


--  Before delete trigger "BFCRM10.TDB_HYK_CZKSKJL" for table "BFCRM10.HYK_CZKSKJL"
create trigger BFCRM10.TDB_HYK_CZKSKJL before delete
on BFCRM10.HYK_CZKSKJL for each row
declare
    integrity_error  exception;
    errno            integer;
    errmsg           char(200);
    dummy            integer;
    found            boolean;

begin

    if :OLD.ZXR is not null then
              errno  := -20001;
              errmsg := '不能删除已审核的售卡记录!';
              raise integrity_error;
    end if;
    
--  Errors handling
exception
    when integrity_error then
       raise_application_error(errno, errmsg);
end;
/


--  Before insert trigger "BFCRM10.TIB_HYK_CZK_RBB" for table "BFCRM10.HYK_CZK_RBB"
create trigger BFCRM10.TIB_HYK_CZK_RBB before insert
on BFCRM10.HYK_CZK_RBB for each row
declare
    integrity_error  exception;
    errno            integer;
    errmsg           char(200);
    dummy            integer;
    found            boolean;

begin
        
    select BFCRM10.S_HYK_CZK_RBB.NextVal into :NEW.TM from dual;
    
--  Errors handling
exception
    when integrity_error then
       raise_application_error(errno, errmsg);
end;
/


--  Before update trigger "BFCRM10.TIU_HYK_CZK_RBB" for table "BFCRM10.HYK_CZK_RBB"
select 'create trigger BFCRM10.TIU_HYK_CZK_RBB' from dual
/
create or replace trigger BFCRM10.TIU_HYK_CZK_RBB before update
on BFCRM10.HYK_CZK_RBB for each row
begin
    select BFCRM10.S_HYK_CZK_RBB.NextVal into :NEW.TM from dual;
end;
/


--  Before insert trigger "BFCRM10.TIB_HYK_GRXX" for table "BFCRM10.HYK_GKDA"
select 'create trigger BFCRM10.TIB_HYK_GRXX' from dual
/
create or replace trigger BFCRM10.TIB_HYK_GRXX before insert
on BFCRM10.HYK_GKDA for each row
begin
    select BFCRM10.S_HYK_GKDA.NextVal into :NEW.TM from dual;
end;
/


--  Before update trigger "BFCRM10.TIU_HYK_GRXX" for table "BFCRM10.HYK_GKDA"
select 'create trigger BFCRM10.TIU_HYK_GRXX' from dual
/
create or replace trigger BFCRM10.TIU_HYK_GRXX before update
on BFCRM10.HYK_GKDA for each row
begin
    select BFCRM10.S_HYK_GKDA.NextVal into :NEW.TM from dual;
end;
/


--  Before insert trigger "BFCRM10.TIB_HYK_GRXX_CLD" for table "BFCRM10.HYK_GRXX_CHILD_JL"
select 'create trigger BFCRM10.TIB_HYK_GRXX_CLD' from dual
/
create or replace trigger BFCRM10.TIB_HYK_GRXX_CLD before insert
on BFCRM10.HYK_GRXX_CHILD_JL for each row
begin
    select BFCRM10.S_HYK_GRXX_CHILD_JL.NextVal into :NEW.TM from dual;
end;
/


--  Before update trigger "BFCRM10.TIU_HYK_GRXX_CLD" for table "BFCRM10.HYK_GRXX_CHILD_JL"
select 'create trigger BFCRM10.TIU_HYK_GRXX_CLD' from dual
/
create or replace trigger BFCRM10.TIU_HYK_GRXX_CLD before update
on BFCRM10.HYK_GRXX_CHILD_JL for each row
begin
    select BFCRM10.S_HYK_GRXX_CHILD_JL.NextVal into :NEW.TM from dual;
end;
/


--  Before insert trigger "BFCRM10.TIB_HYK_HYXX" for table "BFCRM10.HYK_HYXX"
select 'create trigger BFCRM10.TIB_HYK_HYXX' from dual
/
create or replace trigger BFCRM10.TIB_HYK_HYXX before insert
on BFCRM10.HYK_HYXX for each row
begin
    select BFCRM10.S_HYK_HYXX.NextVal into :NEW.TM from dual;
end;
/


--  Before update trigger "BFCRM10.TIU_HYK_HYXX" for table "BFCRM10.HYK_HYXX"
select 'create trigger BFCRM10.TIU_HYK_HYXX' from dual
/
create or replace trigger BFCRM10.TIU_HYK_HYXX before update
on BFCRM10.HYK_HYXX for each row
begin
    select BFCRM10.S_HYK_HYXX.NextVal into :NEW.TM from dual;
end;
/


--  Before insert trigger "BFCRM10.TIB_HYK_HYXX_ZERO" for table "BFCRM10.HYK_HYXX_ZERO"
select 'create trigger BFCRM10.TIB_HYK_HYXX_ZERO' from dual
/
create or replace trigger BFCRM10.TIB_HYK_HYXX_ZERO before insert
on BFCRM10.HYK_HYXX_ZERO for each row
begin
    select BFCRM10.S_HYK_HYXX_ZERO.NextVal into :NEW.TM from dual;
end;
/


--  Before update trigger "BFCRM10.TIU_HYK_HYXX_ZEO" for table "BFCRM10.HYK_HYXX_ZERO"
select 'create trigger BFCRM10.TIU_HYK_HYXX_ZEO' from dual
/
create or replace trigger BFCRM10.TIU_HYK_HYXX_ZEO before update
on BFCRM10.HYK_HYXX_ZERO for each row
begin
    select BFCRM10.S_HYK_HYXX_ZERO.NextVal into :NEW.TM from dual;
end;
/


--  Before insert trigger "BFCRM10.TIB_HYK_JEZCLJL" for table "BFCRM10.HYK_JEZCLJL"
select 'create trigger BFCRM10.TIB_HYK_JEZCLJL' from dual
/
create or replace trigger BFCRM10.TIB_HYK_JEZCLJL before insert
on BFCRM10.HYK_JEZCLJL for each row
begin
    select BFCRM10.S_HYK_JEZCLJL.NextVal into :NEW.TM from dual;
    select BFCRM10.S_JEZCLJL_JYBH.Nextval into :NEW.JYBH from dual; 
end;
/


--  Before update trigger "BFCRM10.TIU_HYK_JEZCLJL" for table "BFCRM10.HYK_JEZCLJL"
select 'create trigger BFCRM10.TIU_HYK_JEZCLJL' from dual
/
create or replace trigger BFCRM10.TIU_HYK_JEZCLJL before update
on BFCRM10.HYK_JEZCLJL for each row
begin
    select BFCRM10.S_HYK_JEZCLJL.NextVal into :NEW.TM from dual;
end;
/


--  Before insert trigger "BFCRM10.TIB_HYK_JEZCLJL_ZERO" for table "BFCRM10.HYK_JEZCLJL_ZERO"
select 'create trigger BFCRM10.TIB_HYK_JEZCLJL_ZERO' from dual
/
create or replace trigger BFCRM10.TIB_HYK_JEZCLJL_ZERO before insert
on BFCRM10.HYK_JEZCLJL_ZERO for each row
begin
    select BFCRM10.S_HYK_JEZCLJL_ZERO.NextVal into :NEW.TM from dual;
end;
/


--  Before update trigger "BFCRM10.TIU_HYK_JEZCLJL_ZERO" for table "BFCRM10.HYK_JEZCLJL_ZERO"
select 'create trigger BFCRM10.TIU_HYK_JEZCLJL_ZERO' from dual
/
create or replace trigger BFCRM10.TIU_HYK_JEZCLJL_ZERO before update
on BFCRM10.HYK_JEZCLJL_ZERO for each row
begin
    select BFCRM10.S_HYK_JEZCLJL_ZERO.NextVal into :NEW.TM from dual;
end;
/


--  Before insert trigger "BFCRM10.TIB_HYK_JEZH" for table "BFCRM10.HYK_JEZH"
select 'create trigger BFCRM10.TIB_HYK_JEZH' from dual
/
create or replace trigger BFCRM10.TIB_HYK_JEZH before insert
on BFCRM10.HYK_JEZH for each row
begin
    select BFCRM10.S_HYK_JEZH.NextVal into :NEW.TM from dual;
end;
/


--  Before update trigger "BFCRM10.TIU_HYK_JEZH" for table "BFCRM10.HYK_JEZH"
select 'create trigger BFCRM10.TIU_HYK_JEZH' from dual
/
create or replace trigger BFCRM10.TIU_HYK_JEZH before update
of YE
on BFCRM10.HYK_JEZH for each row
begin
    select BFCRM10.S_HYK_JEZH.NextVal into :NEW.TM from dual;
end;
/


--  Before update trigger "BFCRM10.TIU_HYK_JEZ_XFRBB" for table "BFCRM10.HYK_JEZ_XFRBB"
select 'create trigger BFCRM10.TIU_HYK_JEZ_XFRBB' from dual
/
create or replace trigger BFCRM10.TIU_HYK_JEZ_XFRBB before update
on BFCRM10.HYK_JEZ_XFRBB for each row
begin
    select BFCRM10.S_HYK_JEZ_XFRBB.NextVal into :NEW.TM from dual;
end;
/


--  Before insert trigger "BFCRM10.TIB_HYK_JEZ_XFRBB" for table "BFCRM10.HYK_JEZ_XFRBB"
select 'create trigger BFCRM10.TIB_HYK_JEZ_XFRBB' from dual
/
create or replace trigger BFCRM10.TIB_HYK_JEZ_XFRBB before insert
on BFCRM10.HYK_JEZ_XFRBB for each row
begin
    select BFCRM10.S_HYK_JEZ_XFRBB.NextVal into :NEW.TM from dual;
end;
/


--  Before insert trigger "BFCRM10.TIB_HYK_JFBDJL" for table "BFCRM10.HYK_JFBDJL"
select 'create trigger BFCRM10.TIB_HYK_JFBDJL' from dual
/
create or replace trigger BFCRM10.TIB_HYK_JFBDJL before insert
on BFCRM10.HYK_JFBDJL for each row
begin
    select BFCRM10.S_HYK_JFBDJL.NextVal into :NEW.TM from dual;
end;
/


--  Before update trigger "BFCRM10.TIU_HYK_JFBDJL" for table "BFCRM10.HYK_JFBDJL"
select 'create trigger BFCRM10.TIU_HYK_JFBDJL' from dual
/
create or replace trigger BFCRM10.TIU_HYK_JFBDJL before update
on BFCRM10.HYK_JFBDJL for each row
begin
    select BFCRM10.S_HYK_JFBDJL.NextVal into :NEW.TM from dual;
end;
/


--  Before insert trigger "BFCRM10.TIB_HYK_JFYEAR" for table "BFCRM10.HYK_JFYEAR"
select 'create trigger BFCRM10.TIB_HYK_JFYEAR' from dual
/
create or replace trigger BFCRM10.TIB_HYK_JFYEAR before insert
on BFCRM10.HYK_JFYEAR for each row
begin
    select BFCRM10.S_HYK_JFYEAR.NextVal into :NEW.TM from dual;
end;
/


--  Before update trigger "BFCRM10.TIU_HYK_JFYEAR" for table "BFCRM10.HYK_JFYEAR"
select 'create trigger BFCRM10.TIU_HYK_JFYEAR' from dual
/
create or replace trigger BFCRM10.TIU_HYK_JFYEAR before update
on BFCRM10.HYK_JFYEAR for each row
begin
    select BFCRM10.S_HYK_JFYEAR.NextVal into :NEW.TM from dual;
end;
/


--  Before insert trigger "BFCRM10.TIB_HYK_JFZH" for table "BFCRM10.HYK_JFZH"
select 'create trigger BFCRM10.TIB_HYK_JFZH' from dual
/
create or replace trigger BFCRM10.TIB_HYK_JFZH before insert
on BFCRM10.HYK_JFZH for each row
begin
    select BFCRM10.S_HYK_JFZH.NextVal into :NEW.TM from dual;
end;
/


--  Before update trigger "BFCRM10.TIU_HYK_JFZH" for table "BFCRM10.HYK_JFZH"
select 'create trigger BFCRM10.TIU_HYK_JFZH' from dual
/
create or replace trigger BFCRM10.TIU_HYK_JFZH before update
on BFCRM10.HYK_JFZH for each row
begin
    select BFCRM10.S_HYK_JFZH.NextVal into :NEW.TM from dual;
end;
/


create trigger BFCRM10.TIB_HYK_KCCZKBGZ before insert
on BFCRM10.HYK_KCCZKBGZ for each row
declare
    integrity_error  exception;
    errno            integer;
    errmsg           char(200);
    dummy            integer;
    found            boolean;

begin
        
    select BFCRM10.S_HYK_KCCZKBGZ.NextVal into :NEW.TM from dual;
    
--  Errors handling
exception
    when integrity_error then
       raise_application_error(errno, errmsg);
end;
/


--  Before update trigger "BFCRM10.TIU_HYK_KCCZKBGZ" for table "BFCRM10.HYK_KCCZKBGZ"
select 'create trigger BFCRM10.TIU_HYK_KCCZKBGZ' from dual
/
create or replace trigger BFCRM10.TIU_HYK_KCCZKBGZ before update
on BFCRM10.HYK_KCCZKBGZ for each row
begin
    select BFCRM10.S_HYK_KCCZKBGZ.NextVal into :NEW.TM from dual;
end;
/


--  Before insert trigger "BFCRM10.TIB_HYK_MDJF" for table "BFCRM10.HYK_MDJF"
select 'create trigger BFCRM10.TIB_HYK_MDJF' from dual
/
create or replace trigger BFCRM10.TIB_HYK_MDJF before insert
on BFCRM10.HYK_MDJF for each row
begin
    select BFCRM10.S_HYK_MDJF.NextVal into :NEW.TM from dual;
end;
/


--  Before update trigger "BFCRM10.TIU_HYK_MDJF" for table "BFCRM10.HYK_MDJF"
select 'create trigger BFCRM10.TIU_HYK_MDJF' from dual
/
create or replace trigger BFCRM10.TIU_HYK_MDJF before update
on BFCRM10.HYK_MDJF for each row
begin
    select BFCRM10.S_HYK_MDJF.NextVal into :NEW.TM from dual;
end;
/


--  Before insert trigger "BFCRM10.TIB_HYK_MDJFYEAR" for table "BFCRM10.HYK_MDJFYEAR"
select 'create trigger BFCRM10.TIB_HYK_MDJFYEAR' from dual
/
create or replace trigger BFCRM10.TIB_HYK_MDJFYEAR before insert
on BFCRM10.HYK_MDJFYEAR for each row
begin
    select BFCRM10.S_HYK_MDJFYEAR.NextVal into :NEW.TM from dual;
end;
/


--  Before update trigger "BFCRM10.TIU_HYK_MDJFYEAR" for table "BFCRM10.HYK_MDJFYEAR"
select 'create trigger BFCRM10.TIU_HYK_MDJFYEAR' from dual
/
create or replace trigger BFCRM10.TIU_HYK_MDJFYEAR before update
on BFCRM10.HYK_MDJFYEAR for each row
begin
    select BFCRM10.S_HYK_MDJFYEAR.NextVal into :NEW.TM from dual;
end;
/


--  Before insert trigger "BFCRM10.TIB_HYK_WCLJF_RBB" for table "BFCRM10.HYK_WCLJF_RBB"
select 'create trigger BFCRM10.TIB_HYK_WCLJF_RBB' from dual
/
create or replace trigger BFCRM10.TIB_HYK_WCLJF_RBB before insert
on BFCRM10.HYK_WCLJF_RBB for each row
begin
    select BFCRM10.S_HYK_WCLJF_RBB.NextVal into :NEW.TM from dual;
end;
/


--  Before update trigger "BFCRM10.TIU_HYK_WCLJF_RBB" for table "BFCRM10.HYK_WCLJF_RBB"
select 'create trigger BFCRM10.TIU_HYK_WCLJF_RBB' from dual
/
create or replace trigger BFCRM10.TIU_HYK_WCLJF_RBB before update
on BFCRM10.HYK_WCLJF_RBB for each row
begin
    select BFCRM10.S_HYK_WCLJF_RBB.NextVal into :NEW.TM from dual;
end;
/


--  Before insert trigger "BFCRM10.TIB_HYK_XFJL" for table "BFCRM10.HYK_XFJL"
select 'create trigger BFCRM10.TIB_HYK_XFJL' from dual
/
create or replace trigger BFCRM10.TIB_HYK_XFJL before insert
on BFCRM10.HYK_XFJL for each row
begin
    select BFCRM10.S_HYK_XFJL.NextVal into :NEW.TM from dual;
end;
/


--  Before update trigger "BFCRM10.TIU_HYK_XFJL" for table "BFCRM10.HYK_XFJL"
select 'create trigger BFCRM10.TIU_HYK_XFJL' from dual
/
create or replace trigger BFCRM10.TIU_HYK_XFJL before update
on BFCRM10.HYK_XFJL for each row
begin
    select BFCRM10.S_HYK_XFJL.NextVal into :NEW.TM from dual;
end;
/


--  Before insert trigger "BFCRM10.TIB_HYK_XFJL_SP" for table "BFCRM10.HYK_XFJL_SP"
select 'create trigger BFCRM10.TIB_HYK_XFJL_SP' from dual
/
create or replace trigger BFCRM10.TIB_HYK_XFJL_SP before insert
on BFCRM10.HYK_XFJL_SP for each row
begin
    select BFCRM10.S_HYK_XFJL_SP.NextVal into :NEW.TM from dual;
end;
/


--  Before update trigger "BFCRM10.TIU_HYK_XFJL_SP" for table "BFCRM10.HYK_XFJL_SP"
select 'create trigger BFCRM10.TIU_HYK_XFJL_SP' from dual
/
create or replace trigger BFCRM10.TIU_HYK_XFJL_SP before update
on BFCRM10.HYK_XFJL_SP for each row
begin
    select BFCRM10.S_HYK_XFJL_SP.NextVal into :NEW.TM from dual;
end;
/


--  Before insert trigger "BFCRM10.TIB_HYK_XFMX" for table "BFCRM10.HYK_XFMX"
select 'create trigger BFCRM10.TIB_HYK_XFMX' from dual
/
create or replace trigger BFCRM10.TIB_HYK_XFMX before insert
on BFCRM10.HYK_XFMX for each row
begin
    select BFCRM10.S_HYK_XFMX.NextVal into :NEW.TM from dual;
end;
/


--  Before update trigger "BFCRM10.TIU_HYK_XFMX" for table "BFCRM10.HYK_XFMX"
select 'create trigger BFCRM10.TIU_HYK_XFMX' from dual
/
create or replace trigger BFCRM10.TIU_HYK_XFMX before update
on BFCRM10.HYK_XFMX for each row
begin
    select BFCRM10.S_HYK_XFMX.NextVal into :NEW.TM from dual;
end;
/


--  Before update trigger "BFCRM10.TIU_HYK_YHQCLJL" for table "BFCRM10.HYK_YHQCLJL"
select 'create trigger BFCRM10.TIU_HYK_YHQCLJL' from dual
/
create or replace trigger BFCRM10.TIU_HYK_YHQCLJL before update
on BFCRM10.HYK_YHQCLJL for each row
begin
    select BFCRM10.S_HYK_YHQCLJL.NextVal into :NEW.TM from dual;
end;
/


--  Before insert trigger "BFCRM10.TIB_HYK_YHQCLJL" for table "BFCRM10.HYK_YHQCLJL"
select 'create trigger BFCRM10.TIB_HYK_YHQCLJL' from dual
/
create or replace trigger BFCRM10.TIB_HYK_YHQCLJL before insert
on BFCRM10.HYK_YHQCLJL for each row
begin
    select BFCRM10.S_HYK_YHQCLJL.NextVal into :NEW.TM from dual;
    select BFCRM10.S_YHQCLJL_JYBH.NextVal into :NEW.JYBH from dual;
end;
/


--  Before insert trigger "BFCRM10.TIB_HYK_YHQZH" for table "BFCRM10.HYK_YHQZH"
select 'create trigger BFCRM10.TIB_HYK_YHQZH' from dual
/
create or replace trigger BFCRM10.TIB_HYK_YHQZH before insert
on BFCRM10.HYK_YHQZH for each row
begin
    select BFCRM10.S_HYK_YHQZH.NextVal into :NEW.TM from dual;
end;
/


--  Before update trigger "BFCRM10.TIU_HYK_YHQZH" for table "BFCRM10.HYK_YHQZH"
select 'create trigger BFCRM10.TIU_HYK_YHQZH' from dual
/
create or replace trigger BFCRM10.TIU_HYK_YHQZH before update
on BFCRM10.HYK_YHQZH for each row
begin
    select BFCRM10.S_HYK_YHQZH.NextVal into :NEW.TM from dual;
end;
/


--  Before insert trigger "BFCRM10.TIB_HYK_YHQ_RBB" for table "BFCRM10.HYK_YHQ_RBB"
select 'create trigger BFCRM10.TIB_HYK_YHQ_RBB' from dual
/
create or replace trigger BFCRM10.TIB_HYK_YHQ_RBB before insert
on BFCRM10.HYK_YHQ_RBB for each row
begin
    select BFCRM10.S_HYK_YHQ_RBB.NextVal into :NEW.TM from dual;
end;
/


--  Before update trigger "BFCRM10.TIU_HYK_YHQ_RBB" for table "BFCRM10.HYK_YHQ_RBB"
select 'create trigger BFCRM10.TIU_HYK_YHQ_RBB' from dual
/
create or replace trigger BFCRM10.TIU_HYK_YHQ_RBB before update
on BFCRM10.HYK_YHQ_RBB for each row
begin
    select BFCRM10.S_HYK_YHQ_RBB.NextVal into :NEW.TM from dual;
end;
/


--  Before insert trigger "BFCRM10.TIB_HYK_YHQ_XFRBB" for table "BFCRM10.HYK_YHQ_XFRBB"
select 'create trigger BFCRM10.TIB_HYK_YHQ_XFRBB' from dual
/
create or replace trigger BFCRM10.TIB_HYK_YHQ_XFRBB before insert
on BFCRM10.HYK_YHQ_XFRBB for each row
begin
    select BFCRM10.S_HYK_YHQ_XFRBB.NextVal into :NEW.TM from dual;
end;
/


--  Before update trigger "BFCRM10.TIU_HYK_YHQ_XFRBB" for table "BFCRM10.HYK_YHQ_XFRBB"
select 'create trigger BFCRM10.TIU_HYK_YHQ_XFRBB' from dual
/
create or replace trigger BFCRM10.TIU_HYK_YHQ_XFRBB before update
on BFCRM10.HYK_YHQ_XFRBB for each row
begin
    select BFCRM10.S_HYK_YHQ_XFRBB.NextVal into :NEW.TM from dual;
end;
/


--  Before insert trigger "BFCRM10.TIB_HYK_ZKXXJL" for table "BFCRM10.HYK_ZKXXJL"
select 'create trigger BFCRM10.TIB_HYK_ZKXXJL' from dual
/
create or replace trigger BFCRM10.TIB_HYK_ZKXXJL before insert
on BFCRM10.HYK_ZKXXJL for each row
begin
    select BFCRM10.S_HYK_ZKXXJL.NextVal into :NEW.XH from dual;
end;
/


--  Before insert trigger "BFCRM10.TIB_HYXFJL" for table "BFCRM10.HYXFJL"
select 'create trigger BFCRM10.TIB_HYXFJL' from dual
/
create or replace trigger BFCRM10.TIB_HYXFJL before insert
on BFCRM10.HYXFJL for each row
begin
    select BFCRM10.S_HYXFJL.NextVal into :NEW.TM from dual;
end;
/


--  Before update trigger "BFCRM10.TIU_HYXFJL" for table "BFCRM10.HYXFJL"
select 'create trigger BFCRM10.TIU_HYXFJL' from dual
/
create or replace trigger BFCRM10.TIU_HYXFJL before update
on BFCRM10.HYXFJL for each row
begin
    select BFCRM10.S_HYXFJL.NextVal into :NEW.TM from dual;
end;
/


--  Before insert trigger "BFCRM10.TIB_HYXFJL_SP" for table "BFCRM10.HYXFJL_SP"
select 'create trigger BFCRM10.TIB_HYXFJL_SP' from dual
/
create or replace trigger BFCRM10.TIB_HYXFJL_SP before insert
on BFCRM10.HYXFJL_SP for each row
begin
    select BFCRM10.S_HYXFJL_SP.NextVal into :NEW.TM from dual;
end;
/


--  Before update trigger "BFCRM10.TIU_HYXFJL_SP" for table "BFCRM10.HYXFJL_SP"
select 'create trigger BFCRM10.TIU_HYXFJL_SP' from dual
/
create or replace trigger BFCRM10.TIU_HYXFJL_SP before update
on BFCRM10.HYXFJL_SP for each row
begin
    select BFCRM10.S_HYXFJL_SP.NextVal into :NEW.TM from dual;
end;
/


--  Before insert trigger "BFCRM10.TIB_HYXXXMDEF" for table "BFCRM10.HYXXXMDEF"
select 'create trigger BFCRM10.TIB_HYXXXMDEF' from dual
/
create or replace trigger BFCRM10.TIB_HYXXXMDEF before insert
on BFCRM10.HYXXXMDEF for each row
begin
    select BFCRM10.S_HYXXXMDEF.NextVal into :NEW.TM from dual;
end;
/


--  Before update trigger "BFCRM10.TIU_HYXXXMDEF" for table "BFCRM10.HYXXXMDEF"
select 'create trigger BFCRM10.TIU_HYXXXMDEF' from dual
/
create or replace trigger BFCRM10.TIU_HYXXXMDEF before update
on BFCRM10.HYXXXMDEF for each row
begin
    select BFCRM10.S_HYXXXMDEF.NextVal into :NEW.TM from dual;
end;
/


--  Before insert trigger "BFCRM10.TIB_JXCDB" for table "BFCRM10.JXCDB"
create trigger BFCRM10.TIB_JXCDB before insert
on BFCRM10.JXCDB for each row
declare
    integrity_error  exception;
    errno            integer;
    errmsg           char(200);
    dummy            integer;
    found            boolean;

begin
        
    select BFCRM10.S_JXCDB.NextVal into :NEW.TM from dual;
    
--  Errors handling
exception
    when integrity_error then
       raise_application_error(errno, errmsg);
end;
/


--  Before update trigger "BFCRM10.TIU_JXCDB" for table "BFCRM10.JXCDB"
select 'create trigger BFCRM10.TIU_JXCDB' from dual
/
create or replace trigger BFCRM10.TIU_JXCDB before update
on BFCRM10.JXCDB for each row
begin
    select BFCRM10.S_JXCDB.NextVal into :NEW.TM from dual;
end;
/


--  Before insert trigger "BFCRM10.TIB_MDDY" for table "BFCRM10.MDDY"
select 'create trigger BFCRM10.TIB_MDDY' from dual
/
create or replace trigger BFCRM10.TIB_MDDY before insert
on BFCRM10.MDDY for each row
begin
    select BFCRM10.S_MDDY.NextVal into :NEW.TM from dual;
end;
/


--  Before update trigger "BFCRM10.TIU_MDDY" for table "BFCRM10.MDDY"
select 'create trigger BFCRM10.TIU_MDDY' from dual
/
create or replace trigger BFCRM10.TIU_MDDY before update
on BFCRM10.MDDY for each row
begin
    select BFCRM10.S_MDDY.NextVal into :NEW.TM from dual;
end;
/


--  Before insert trigger "BFCRM10.TIB_MDDY_GXSH" for table "BFCRM10.MDDY_GXSH"
select 'create trigger BFCRM10.TIB_MDDY_GXSH' from dual
/
create or replace trigger BFCRM10.TIB_MDDY_GXSH before insert
on BFCRM10.MDDY_GXSH for each row
begin
    select BFCRM10.S_MDDY_GXSH.NextVal into :NEW.TM from dual;
end;
/


--  Before update trigger "BFCRM10.TIU_MDDY_GXSH" for table "BFCRM10.MDDY_GXSH"
select 'create trigger BFCRM10.TIU_MDDY_GXSH' from dual
/
create or replace trigger BFCRM10.TIU_MDDY_GXSH before update
on BFCRM10.MDDY_GXSH for each row
begin
    select BFCRM10.S_MDDY_GXSH.NextVal into :NEW.TM from dual;
end;
/


--  Before insert trigger "BFCRM10.TIB_QYDEF" for table "BFCRM10.QYDEF"
select 'create trigger BFCRM10.TIB_QYDEF' from dual
/
create or replace trigger BFCRM10.TIB_QYDEF before insert
on BFCRM10.QYDEF for each row
begin
    select BFCRM10.S_QYDEF.NextVal into :NEW.TM from dual;
end;
/


--  Before update trigger "BFCRM10.TIU_QYDEF" for table "BFCRM10.QYDEF"
select 'create trigger BFCRM10.TIU_QYDEF' from dual
/
create or replace trigger BFCRM10.TIU_QYDEF before update
on BFCRM10.QYDEF for each row
begin
    select BFCRM10.S_QYDEF.NextVal into :NEW.TM from dual;
end;
/


--  Before insert trigger "BFCRM10.TIB_QYDEF_MD" for table "BFCRM10.QYDEF_MD"
select 'create trigger BFCRM10.TIB_QYDEF_MD' from dual
/
create or replace trigger BFCRM10.TIB_QYDEF_MD before insert
on BFCRM10.QYDEF_MD for each row
begin
    select BFCRM10.S_QYDEF_MD.NextVal into :NEW.TM from dual;
end;
/


--  Before update trigger "BFCRM10.TIU_QYDEF_MD" for table "BFCRM10.QYDEF_MD"
select 'create trigger BFCRM10.TIU_QYDEF_MD' from dual
/
create or replace trigger BFCRM10.TIU_QYDEF_MD before update
on BFCRM10.QYDEF_MD for each row
begin
    select BFCRM10.S_QYDEF_MD.NextVal into :NEW.TM from dual;
end;
/


--  Before insert trigger "BFCRM10.TIB_SHSPXX" for table "BFCRM10.SHSPXX"
select 'create trigger BFCRM10.TIB_SHSPXX' from dual
/
create or replace trigger BFCRM10.TIB_SHSPXX before insert
on BFCRM10.SHSPXX for each row
begin
    select BFCRM10.S_SHSPXX.NextVal into :NEW.TM from dual;
end;
/


--  Before update trigger "BFCRM10.TIU_SHSPXX" for table "BFCRM10.SHSPXX"
select 'create trigger BFCRM10.TIU_SHSPXX' from dual
/
create or replace trigger BFCRM10.TIU_SHSPXX before update
on BFCRM10.SHSPXX for each row
begin
    select BFCRM10.S_SHSPXX.NextVal into :NEW.TM from dual;
end;
/


--  Before insert trigger "BFCRM10.TIB_SPFL" for table "BFCRM10.SPFL"
select 'create trigger BFCRM10.TIB_SPFL' from dual
/
create or replace trigger BFCRM10.TIB_SPFL before insert
on BFCRM10.SPFL for each row
begin
    select BFCRM10.S_SPFL.NextVal into :NEW.TM from dual;
end;
/


--  Before update trigger "BFCRM10.TIU_SPFL" for table "BFCRM10.SPFL"
select 'create trigger BFCRM10.TIU_SPFL' from dual
/
create or replace trigger BFCRM10.TIU_SPFL before update
on BFCRM10.SPFL for each row
begin
    select BFCRM10.S_SPFL.NextVal into :NEW.TM from dual;
end;
/


--  Before insert trigger "BFCRM10.TIB_SPSB" for table "BFCRM10.SPSB"
select 'create trigger BFCRM10.TIB_SPSB' from dual
/
create or replace trigger BFCRM10.TIB_SPSB before insert
on BFCRM10.SPSB for each row
begin
    select BFCRM10.S_SPSB.NextVal into :NEW.TM from dual;
end;
/


--  Before update trigger "BFCRM10.TIU_SPSB" for table "BFCRM10.SPSB"
select 'create trigger BFCRM10.TIU_SPSB' from dual
/
create or replace trigger BFCRM10.TIU_SPSB before update
on BFCRM10.SPSB for each row
begin
    select BFCRM10.S_SPSB.NextVal into :NEW.TM from dual;
end;
/


--  Before insert trigger "BFCRM10.TIB_YHQDEF" for table "BFCRM10.YHQDEF"
select 'create trigger BFCRM10.TIB_YHQDEF' from dual
/
create or replace trigger BFCRM10.TIB_YHQDEF before insert
on BFCRM10.YHQDEF for each row
begin
    select BFCRM10.S_YHQDEF.NextVal into :NEW.TM from dual;
end;
/


--  Before update trigger "BFCRM10.TIU_YHQDEF" for table "BFCRM10.YHQDEF"
select 'create trigger BFCRM10.TIU_YHQDEF' from dual
/
create or replace trigger BFCRM10.TIU_YHQDEF before update
on BFCRM10.YHQDEF for each row
begin
    select BFCRM10.S_YHQDEF.NextVal into :NEW.TM from dual;
end;
/

