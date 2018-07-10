/*==============================================================*/
/* DBMS name:      ORACLE Version 9i                            */
/* Created on:     2017-12-05 11:12:05                          */
/*==============================================================*/



-- Type package declaration
create or replace package PDTypes  
as
    TYPE ref_cursor IS REF CURSOR;
end;

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


drop index BFCRM10.MZKCARD_SKJLBH
/

drop index BFCRM10.MZKCARD_BGDDDM
/

drop index BFCRM10.MZKCARD_BGDDDM_QCYE
/

drop index BFCRM10.MZKCARD_CDNR
/

drop index BFCRM10.MZKCARD_BAK_BGDDDM
/

drop index BFCRM10.MZKCARD_BAK_BGDDDM_QCYE
/

drop index BFCRM10.MZKCARD_BAK_CDNR
/

drop index BFCRM10.MZKXX_STATUS
/

drop index BFCRM10.MZKXX_HYKTYPE
/

drop index BFCRM10.MZKXX_YXQ
/

drop index BFCRM10.MZKXX_TM
/

drop index BFCRM10.MZKXX_CDNR
/

drop index BFCRM10.MZKXX_VIPNO
/

drop index BFCRM10.MZK_CKJL_ZXRQ
/

drop index BFCRM10.MZK_CLJL_TM
/

drop index BFCRM10.MZK_CLJL_MD_SKT
/

drop index BFCRM10.MZK_CLJL_JZRQ
/

drop index BFCRM10.MZK_CLJL_XFJL
/

drop index BFCRM10.MZK_CLJL_HYID
/

drop index BFCRM10.MZK_CLJL_RQ
/

drop index BFCRM10.INDEX_HYK_CZK_JEZ_ZC_ZXRQ
/

drop index BFCRM10.HYK_CZK_JEZ_ZC_HYID
/

drop index BFCRM10.HYK_CZK_JZE_ZCITEM_HYID
/

drop index BFCRM10.MZKJKJLITEM_CZKHM
/

drop index BFCRM10.MZKJKJLITEM_CDNR
/

drop index MZK_JYCL_JLBH
/

drop index BFCRM10.MZK_JYCL_MX
/

drop index BFCRM10.MZK_JYCL_XFJLID
/

drop index BFCRM10.MZK_KHDA_KHMC
/

drop index BFCRM10.MZK_PLCKJL_ZXRQ
/

drop index BFCRM10.MZK_PLQKJL_ZXRQ
/

drop index BFCRM10.MZK_QKJL_ZXRQ
/

drop index BFCRM10.MZK_QKJL_HYID
/

drop index BFCRM10.MZK_CZKSKJL_ZXRQ
/

drop index BFCRM10.MZK_ZFJLITEM_HYID
/

drop index BFCRM10.MZK_ZTBDJLITEM_HYID
/

/*==============================================================*/
/* Table: CARD_BILL                                             */
/*==============================================================*/
create table BFCRM10.CARD_BILL  (
   BILLNO               CHAR(10),
   TYPE                 CHAR(10),
   AMOUNT               CHAR(10),
   NOTE                 CHAR(10),
   CHECKER              CHAR(10),
   POSTTIEM             CHAR(10),
   B2CNO                CHAR(10),
   CHECKTIME            CHAR(10),
   CANCELTIME           CHAR(10),
   STATUS               CHAR(10)
)
/

/*==============================================================*/
/* Table: CARD_BILLITEM                                         */
/*==============================================================*/
create table BFCRM10.CARD_BILLITEM  (
   单号                   CHAR(10),
   CARDTYPE             CHAR(10),
   CARDID               CHAR(10),
   CARDNO               CHAR(10),
   AMOUNT               CHAR(10),
   BALANCE              CHAR(10)
)
/

/*==============================================================*/
/* Table: CARD_ORDER                                            */
/*==============================================================*/
create table BFCRM10.CARD_ORDER  (
   ORDERNO              CHAR(10),
   AMOUNT               CHAR(10),
   ORDERDATE            CHAR(10),
   COUNT                CHAR(10),
   CHECKER              CHAR(10),
   POSTTIEM             CHAR(10),
   NOTE                 CHAR(10),
   ACCOUNTDATE          CHAR(10),
   LOCALE               CHAR(10),
   STATUS               CHAR(10),
   ACTIVETIME           CHAR(10)
)
/

/*==============================================================*/
/* Table: CARD_ORDERITEM                                        */
/*==============================================================*/
create table BFCRM10.CARD_ORDERITEM  (
   ORDERNO              CHAR(10),
   CARDTYPE             CHAR(10),
   "VALUES"             CHAR(10),
   COUNT                CHAR(10)
)
/

/*==============================================================*/
/* Table: CARD_PROCREC                                          */
/*==============================================================*/
create table BFCRM10.CARD_PROCREC  (
   ID                   CHAR(10),
   PROCTIME             CHAR(10),
   TYPE                 CHAR(10),
   MC                   CHAR(10),
   MD                   CHAR(10),
   AMOUNT               CHAR(10),
   INO                  CHAR(10),
   POSTDATE             CHAR(10),
   NOTE                 CHAR(10),
   B2CINO               CHAR(10),
   ACCOUNTDATE          CHAR(10)
)
/

/*==============================================================*/
/* Table: CARD_TRACK                                            */
/*==============================================================*/
create table BFCRM10.CARD_TRACK  (
   JLBH                 NUMBER(10)                      not null,
   HYID                 NUMBER(10)                      not null,
   HM                   VARCHAR2(20)                    not null,
   RQ                   DATE,
   CLLX                 NUMBER(10),
   VALUE                VARCHAR2(200),
   constraint PK_CARD_TRACK primary key (JLBH)
)
/

/*==============================================================*/
/* Table: CARD_VIR                                              */
/*==============================================================*/
create table BFCRM10.CARD_VIR  (
   CARDID               NUMBER(10)                      not null,
   CARDNO               VARCHAR2(30)                    not null,
   CARDTYPE             NUMBER(5)                       not null,
   "VALUES"             NUMBER(14,2)                    not null,
   AMOUNT               NUMBER(14,2)                    not null,
   STATUS               NUMBER(5)                       not null,
   VALIDTIME            DATE,
   CARDDATE             DATE,
   CHECKER              VARCHAR2(30),
   PROCTIME             DATE,
   ACTIVETIME           DATE,
   constraint PK_CARD_VIR primary key (CARDID)
)
/

/*==============================================================*/
/* Table: HHC                                                   */
/*==============================================================*/
create table BFCRM10.HHC  (
   HYID                 NUMBER(10)                      not null,
   HHC                  VARCHAR2(200)                   not null,
   constraint PK_HHC primary key (HYID)
)
/

/*==============================================================*/
/* Table: MD_QY                                                 */
/*==============================================================*/
create table BFCRM10.MD_QY  (
   MDID                 NUMBER(10)                      not null,
   QY                   VARCHAR2(30)                    not null,
   YT                   VARCHAR2(30)                    not null,
   BZ                   VARCHAR2(30),
   constraint PK_MD_QY primary key (MDID)
)
/

/*==============================================================*/
/* Table: MZKCARD                                               */
/*==============================================================*/
create table BFCRM10.MZKCARD  (
   CZKHM                VARCHAR2(30)                    not null,
   CDNR                 VARCHAR2(60)                    not null,
   HYKTYPE              NUMBER(5),
   QCYE                 NUMBER(14,2)                    not null,
   YXTZJE               NUMBER(14,2)                    not null,
   PDJE                 NUMBER(14,2)                   default 0 not null,
   BGDDDM               VARCHAR2(10),
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
   constraint PK_MZKCARD primary key (CZKHM)
)
/

/*==============================================================*/
/* Index: MZKCARD_CDNR                                          */
/*==============================================================*/
create unique index BFCRM10.MZKCARD_CDNR on BFCRM10.MZKCARD (
   CDNR ASC
)
/

/*==============================================================*/
/* Index: MZKCARD_BGDDDM_QCYE                                   */
/*==============================================================*/
create index BFCRM10.MZKCARD_BGDDDM_QCYE on BFCRM10.MZKCARD (
   HYKTYPE ASC,
   QCYE ASC,
   BGDDDM ASC,
   BGR ASC
)
/

/*==============================================================*/
/* Index: MZKCARD_BGDDDM                                        */
/*==============================================================*/
create index BFCRM10.MZKCARD_BGDDDM on BFCRM10.MZKCARD (
   HYKTYPE ASC,
   BGDDDM ASC,
   BGR ASC
)
/

/*==============================================================*/
/* Index: MZKCARD_SKJLBH                                        */
/*==============================================================*/
create index BFCRM10.MZKCARD_SKJLBH on BFCRM10.MZKCARD (
   SKJLBH ASC
)
/

/*==============================================================*/
/* Table: MZKCARD_BAK                                           */
/*==============================================================*/
create table BFCRM10.MZKCARD_BAK  (
   CZKHM                VARCHAR2(30)                    not null,
   CDNR                 VARCHAR2(60)                    not null,
   HYKTYPE              NUMBER(5),
   QCYE                 NUMBER(14,2)                    not null,
   YXTZJE               NUMBER(14,2)                    not null,
   PDJE                 NUMBER(14,2)                   default 0 not null,
   BGDDDM               VARCHAR2(10),
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
   constraint PK_MZKCARD_BAK primary key (CZKHM)
)
/

/*==============================================================*/
/* Index: MZKCARD_BAK_CDNR                                      */
/*==============================================================*/
create unique index BFCRM10.MZKCARD_BAK_CDNR on BFCRM10.MZKCARD_BAK (
   CDNR ASC
)
/

/*==============================================================*/
/* Index: MZKCARD_BAK_BGDDDM_QCYE                               */
/*==============================================================*/
create index BFCRM10.MZKCARD_BAK_BGDDDM_QCYE on BFCRM10.MZKCARD_BAK (
   HYKTYPE ASC,
   QCYE ASC,
   BGDDDM ASC,
   BGR ASC
)
/

/*==============================================================*/
/* Index: MZKCARD_BAK_BGDDDM                                    */
/*==============================================================*/
create index BFCRM10.MZKCARD_BAK_BGDDDM on BFCRM10.MZKCARD_BAK (
   HYKTYPE ASC,
   BGDDDM ASC,
   BGR ASC
)
/

/*==============================================================*/
/* Table: MZKXX                                                 */
/*==============================================================*/
create sequence BFCRM10.S_MZKXX
/
create table BFCRM10.MZKXX  (
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
   ZHDLSJ               DATE,
   SJJRQ                DATE,
   QY                   number(10),
   constraint PK_MZKXX primary key (HYID)
)
/

/*==============================================================*/
/* Index: MZKXX_VIPNO                                           */
/*==============================================================*/
create index BFCRM10.MZKXX_VIPNO on BFCRM10.MZKXX (
   HYK_NO ASC
)
/

/*==============================================================*/
/* Index: MZKXX_CDNR                                            */
/*==============================================================*/
create unique index BFCRM10.MZKXX_CDNR on BFCRM10.MZKXX (
   CDNR ASC
)
/

/*==============================================================*/
/* Index: MZKXX_TM                                              */
/*==============================================================*/
create index BFCRM10.MZKXX_TM on BFCRM10.MZKXX (
   TM ASC
)
/

/*==============================================================*/
/* Index: MZKXX_YXQ                                             */
/*==============================================================*/
create index BFCRM10.MZKXX_YXQ on BFCRM10.MZKXX (
   YXQ ASC
)
/

/*==============================================================*/
/* Index: MZKXX_HYKTYPE                                         */
/*==============================================================*/
create index BFCRM10.MZKXX_HYKTYPE on BFCRM10.MZKXX (
   HYKTYPE ASC
)
/

/*==============================================================*/
/* Index: MZKXX_STATUS                                          */
/*==============================================================*/
create index BFCRM10.MZKXX_STATUS on BFCRM10.MZKXX (
   STATUS ASC
)
/

/*==============================================================*/
/* Table: MZK_BKCK                                              */
/*==============================================================*/
create table BFCRM10.MZK_BKCK  (
   JLBH                 NUMBER(10)                      not null,
   ZY                   VARCHAR2(50)                    not null,
   CZDD                 VARCHAR2(10)                    not null,
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16)                    not null,
   DJSJ                 DATE                            not null,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(16),
   ZXRQ                 DATE,
   constraint PK_MZK_BKCK primary key (JLBH)
)
/

/*==============================================================*/
/* Table: MZK_BKCKITEM                                          */
/*==============================================================*/
create table BFCRM10.MZK_BKCKITEM  (
   JLBH                 NUMBER(10)                      not null,
   HYID                 NUMBER(10),
   HYK_NO               VARCHAR2(30)                    not null,
   HYKTYPE              NUMBER(10)                      not null,
   JE                   NUMBER(14,2)                   default 0,
   BJ_ZR                NUMBER(5),
   constraint PK_MZK_BKCKITEM primary key (JLBH, HYK_NO)
)
/

/*==============================================================*/
/* Table: MZK_CKJL                                              */
/*==============================================================*/
create table BFCRM10.MZK_CKJL  (
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
   ZFFSID               NUMBER(5),
   SKYMC                VARCHAR2(20),
   SQRYID               NUMBER(10),
   CZMD                 NUMBER(10),
   constraint PK_MZK_CKJL primary key (CZJPJ_JLBH)
)
/

/*==============================================================*/
/* Index: MZK_CKJL_ZXRQ                                         */
/*==============================================================*/
create index BFCRM10.MZK_CKJL_ZXRQ on BFCRM10.MZK_CKJL (
   ZXRQ ASC
)
/

/*==============================================================*/
/* Table: MZK_CKJLSKITEM                                        */
/*==============================================================*/
create table BFCRM10.MZK_CKJLSKITEM  (
   CZJPJ_JLBH           NUMBER(10)                      not null,
   ZFFSID               NUMBER(10)                      not null,
   JE                   NUMBER(14,2)                    not null,
   constraint PK_MZK_CKJLSKITEM primary key (CZJPJ_JLBH, ZFFSID)
)
/

/*==============================================================*/
/* Table: MZK_CKSJFGZ                                           */
/*==============================================================*/
create table BFCRM10.MZK_CKSJFGZ  (
   CKJE_BEGIN           NUMBER(14,2)                   default 0 not null,
   CKJE_END             NUMBER(14,2)                   default 0,
   ZSBL                 NUMBER(16,4),
   ZSJF                 NUMBER(16,4),
   constraint PK_MZK_CKSJFGZ primary key (CKJE_BEGIN)
)
/

/*==============================================================*/
/* Table: MZK_GS                                                */
/*==============================================================*/
create table BFCRM10.MZK_GS  (
   CZJPJ_JLBH           NUMBER(10)                      not null,
   VIPID                NUMBER(10),
   HYKTYPE              NUMBER(5),
   ZY                   VARCHAR2(40),
   JF                   FLOAT(50)                       not null,
   JE                   NUMBER(14,2)                    not null,
   JLJE                 NUMBER(14,2)                    not null,
   LX                   NUMBER(1)                       not null
      constraint CKC_LX_MZK_GS check (LX in (0,1)),
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
   CZMD                 NUMBER(10),
   constraint PK_MZK_GS primary key (CZJPJ_JLBH)
)
/

/*==============================================================*/
/* Table: MZK_HK                                                */
/*==============================================================*/
create table BFCRM10.MZK_HK  (
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
   BGDDDM               VARCHAR2(10),
   QCYE                 NUMBER(14,2),
   BJ_CZK               NUMBER(5)                      default 0,
   BJ_CHILD             NUMBER(5),
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(20),
   ZXRQ                 DATE,
   BJ_LMK               NUMBER(5)                      default 0,
   CZMD                 NUMBER(10),
   constraint PK_MZK_HK primary key (CZJPJ_JLBH)
)
/

/*==============================================================*/
/* Table: MZK_JEZCLJL                                           */
/*==============================================================*/
create sequence BFCRM10.S_MZK_JEZCLJL
/
create table BFCRM10.MZK_JEZCLJL  (
   JYBH                 NUMBER(9)                       not null,
   HYID                 NUMBER(10)                      not null,
   CLSJ                 DATE                            not null,
   CLLX                 NUMBER(5)                       not null,
   JLBH                 NUMBER(10),
   MDID                 NUMBER(10),
   SKTNO                VARCHAR2(6),
   ZY                   VARCHAR2(100),
   JFJE                 NUMBER(14,2)                   default 0 not null
      constraint CKC_JFJE_MZK_JEZC check (JFJE between -200000 and 200000),
   DFJE                 NUMBER(14,2)                   default 0 not null
      constraint CKC_DFJE_MZK_JEZC check (DFJE between -200000 and 200000),
   YE                   NUMBER(14,2)                   default 0 not null,
   JZRQ                 DATE,
   CRMJZRQ              DATE,
   SKYDM                VARCHAR2(8),
   SKYMC                VARCHAR2(16),
   JYID                 FLOAT(50),
   TM                   NUMBER(10),
   MDID_CZ              NUMBER(10),
   HYK_NO               VARCHAR2(20),
   constraint PK_MZK_JEZCLJL primary key (JYBH)
)
/

/*==============================================================*/
/* Index: MZK_CLJL_RQ                                           */
/*==============================================================*/
create index BFCRM10.MZK_CLJL_RQ on BFCRM10.MZK_JEZCLJL (
   CLSJ ASC
)
/

/*==============================================================*/
/* Index: MZK_CLJL_HYID                                         */
/*==============================================================*/
create index BFCRM10.MZK_CLJL_HYID on BFCRM10.MZK_JEZCLJL (
   HYID ASC
)
/

/*==============================================================*/
/* Index: MZK_CLJL_XFJL                                         */
/*==============================================================*/
create index BFCRM10.MZK_CLJL_XFJL on BFCRM10.MZK_JEZCLJL (
   MDID ASC,
   SKTNO ASC,
   JLBH ASC
)
/

/*==============================================================*/
/* Index: MZK_CLJL_JZRQ                                         */
/*==============================================================*/
create index BFCRM10.MZK_CLJL_JZRQ on BFCRM10.MZK_JEZCLJL (
   MDID ASC,
   SKTNO ASC,
   CRMJZRQ ASC
)
/

/*==============================================================*/
/* Index: MZK_CLJL_MD_SKT                                       */
/*==============================================================*/
create index BFCRM10.MZK_CLJL_MD_SKT on BFCRM10.MZK_JEZCLJL (
   CLSJ ASC,
   MDID ASC,
   SKTNO ASC
)
/

/*==============================================================*/
/* Index: MZK_CLJL_TM                                           */
/*==============================================================*/
create index BFCRM10.MZK_CLJL_TM on BFCRM10.MZK_JEZCLJL (
   TM ASC
)
/

/*==============================================================*/
/* Table: MZK_JEZH                                              */
/*==============================================================*/
create sequence BFCRM10.S_MZK_JEZH;
create table BFCRM10.MZK_JEZH  (
   HYID                 NUMBER(10)                      not null,
   QCYE                 NUMBER(14,2)                   default 0 not null,
   YE                   NUMBER(14,2)                   default 0 not null
      constraint CKC_YE_MZK_JEZH check (YE between 0 and 500000),
   YXTZJE               NUMBER(14,2)                   default 0 not null,
   TM                   NUMBER(10)                      not null,
   PDJE                 NUMBER(14,2)                   default 0 not null,
   JYDJJE               NUMBER(14,2)                   default 0
      constraint CKC_JYDJJE_MZK_JEZH check (JYDJJE is null or (JYDJJE >= 0)),
   FPJE                 NUMBER(14,2)                   default 0,
   constraint PK_MZK_JEZH primary key (HYID)
)
rowdependencies
/

/*==============================================================*/
/* Table: MZK_JEZ_ZC                                            */
/*==============================================================*/
create table BFCRM10.MZK_JEZ_ZC  (
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
create index BFCRM10.HYK_CZK_JEZ_ZC_HYID on BFCRM10.MZK_JEZ_ZC (
   HYID_ZR ASC
)
/

/*==============================================================*/
/* Index: INDEX_HYK_CZK_JEZ_ZC_ZXRQ                             */
/*==============================================================*/
create index BFCRM10.INDEX_HYK_CZK_JEZ_ZC_ZXRQ on BFCRM10.MZK_JEZ_ZC (
   ZXRQ ASC
)
/

/*==============================================================*/
/* Table: MZK_JEZ_ZCITEM                                        */
/*==============================================================*/
create table BFCRM10.MZK_JEZ_ZCITEM  (
   CZJPJ_JLBH           NUMBER(10)                      not null,
   HYID_ZC              NUMBER(10)                      not null,
   ZCJE                 NUMBER(14,2)                    not null,
   BJ_CHILD             NUMBER(5),
   HYKNO                VARCHAR2(30),
   constraint PK_MZK_JEZ_ZCITEM primary key (CZJPJ_JLBH, HYID_ZC)
)
/

/*==============================================================*/
/* Index: HYK_CZK_JZE_ZCITEM_HYID                               */
/*==============================================================*/
create index BFCRM10.HYK_CZK_JZE_ZCITEM_HYID on BFCRM10.MZK_JEZ_ZCITEM (
   HYID_ZC ASC
)
/

/*==============================================================*/
/* Table: MZK_JKD                                               */
/*==============================================================*/
create table BFCRM10.MZK_JKD  (
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
   constraint PK_MZK_JKD primary key (JLBH)
)
/

/*==============================================================*/
/* Table: MZK_JKDMX                                             */
/*==============================================================*/
create table BFCRM10.MZK_JKDMX  (
   JLBH                 NUMBER(10)                      not null,
   ZFFSID               NUMBER(10)                      not null,
   JE                   NUMBER(14,2)                    not null,
   constraint PK_MZK_JKDMX primary key (JLBH, ZFFSID)
)
/

/*==============================================================*/
/* Table: MZK_JKJL                                              */
/*==============================================================*/
create table BFCRM10.MZK_JKJL  (
   JLBH                 NUMBER(10)                      not null,
   JKFS                 NUMBER(5)                      default 0 not null,
   HYKTYPE              NUMBER(5)                       not null,
   BGDDDM               VARCHAR2(10),
   ZY                   VARCHAR2(40),
   CZKHM_BEGIN          VARCHAR2(30)                    not null,
   CZKHM_END            VARCHAR2(30)                    not null,
   JKSL                 NUMBER(10)                      not null
      constraint CKC_JKSL_MZK_JKJL check (JKSL between 0 and 500000),
   QCYE                 NUMBER(14,2)                   default 0 not null,
   YXTZJE               NUMBER(14,2)                   default 0 not null,
   JKWJCS               NUMBER(5)                      default 0 not null,
   BGR                  NUMBER(10)                      not null,
   BGRMC                VARCHAR2(16)                    not null,
   PDJE                 NUMBER(14,2)                   default 0 not null,
   BJ_CZK               NUMBER(1)                       not null
      constraint CKC_BJ_CZK_MZK_JKJL check (BJ_CZK in (0,1)),
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
   constraint PK_MZK_JKJL primary key (JLBH)
)
/

/*==============================================================*/
/* Table: MZK_JKJLITEM                                          */
/*==============================================================*/
create table BFCRM10.MZK_JKJLITEM  (
   JLBH                 NUMBER(10)                      not null,
   CZKHM                VARCHAR2(30)                    not null,
   CDNR                 VARCHAR2(60)                    not null,
   JE                   NUMBER(14,2)                   default 0,
   BJ_ZK                NUMBER(5)                      default 0,
   XKRQ                 DATE,
   constraint PK_MZK_JKJLITEM primary key (JLBH, CZKHM)
)
/

/*==============================================================*/
/* Index: MZKJKJLITEM_CDNR                                      */
/*==============================================================*/
create unique index BFCRM10.MZKJKJLITEM_CDNR on BFCRM10.MZK_JKJLITEM (
   CDNR ASC
)
/

/*==============================================================*/
/* Index: MZKJKJLITEM_CZKHM                                     */
/*==============================================================*/
create unique index BFCRM10.MZKJKJLITEM_CZKHM on BFCRM10.MZK_JKJLITEM (
   CZKHM ASC
)
/

/*==============================================================*/
/* Table: MZK_JYCL                                              */
/*==============================================================*/
create table BFCRM10.MZK_JYCL  (
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
   constraint PK_MZK_JYCL primary key (JYID)
)
/

/*==============================================================*/
/* Index: MZK_JYCL_XFJLID                                       */
/*==============================================================*/
create index BFCRM10.MZK_JYCL_XFJLID on BFCRM10.MZK_JYCL (
   XFJLID ASC
)
/

/*==============================================================*/
/* Index: MZK_JYCL_MX                                           */
/*==============================================================*/
create index BFCRM10.MZK_JYCL_MX on BFCRM10.MZK_JYCL (
   XFJLID ASC,
   JYZT ASC,
   BJ_CZK ASC
)
/

/*==============================================================*/
/* Index: MZK_JYCL_JLBH                                         */
/*==============================================================*/
create index MZK_JYCL_JLBH on BFCRM10.MZK_JYCL (
   MDID ASC,
   SKTNO ASC,
   JLBH ASC
)
/

/*==============================================================*/
/* Table: MZK_JYCLITEM_CZK                                      */
/*==============================================================*/
create table BFCRM10.MZK_JYCLITEM_CZK  (
   JYID                 NUMBER(10)                      not null,
   HYID                 NUMBER(10)                      not null,
   JE                   NUMBER(14,2)                    not null,
   BJ_HSK               NUMBER(5)                      default 0 not null,
   constraint PK_MZK_JYCLITEM_CZK primary key (JYID, HYID)
)
/

/*==============================================================*/
/* Table: MZK_JYCL_ZBZT                                         */
/*==============================================================*/
create table BFCRM10.MZK_JYCL_ZBZT  (
   JYID                 NUMBER(10)                      not null,
   ZBSJ                 DATE,
   BJ_DJ                NUMBER(5),
   JYLX                 NUMBER(1),
   constraint PK_MZK_JYCL_ZBZT primary key (JYID)
)
/

/*==============================================================*/
/* Table: MZK_KCBGZ                                             */
/*==============================================================*/
create sequence BFCRM10.S_MZK_KCBGZ
/
create table BFCRM10.MZK_KCBGZ  (
   RQ                   DATE                            not null,
   BGDDDM               VARCHAR2(10)                    not null,
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
   constraint PK_MZK_KCBGZ primary key (RQ, BGDDDM, HYKTYPE, MZJE)
)
/

/*==============================================================*/
/* Table: MZK_KCKPDHZ                                           */
/*==============================================================*/
create table BFCRM10.MZK_KCKPDHZ  (
   JLBH                 NUMBER(10)                      not null,
   JLBH_PC              NUMBER(10)                      not null,
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(30)                    not null,
   DJSJ                 DATE                            not null,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(30),
   ZXRQ                 DATE,
   constraint PK_MZK_KCKPDHZ primary key (JLBH)
)
/

/*==============================================================*/
/* Table: MZK_KCKPDHZMX                                         */
/*==============================================================*/
create table BFCRM10.MZK_KCKPDHZMX  (
   JLBH                 NUMBER(10)                      not null,
   CZKHM                VARCHAR2(30)                    not null,
   BGR                  NUMBER(10)                      not null,
   MZJE                 NUMBER(14,2)                    not null,
   BJ                   NUMBER(5)                       not null,
   constraint PK_MZK_KCKPDHZMX primary key (JLBH, CZKHM)
)
/

/*==============================================================*/
/* Table: MZK_KCKPDLR                                           */
/*==============================================================*/
create table BFCRM10.MZK_KCKPDLR  (
   JLBH                 NUMBER(10)                      not null,
   JLBH_PC              NUMBER(10)                      not null,
   HYKTYPE              NUMBER(10)                      not null,
   BGR                  NUMBER(10)                      not null,
   PDSL                 NUMBER(10)                      not null,
   CZDD                 VARCHAR2(10)                    not null,
   ZY                   VARCHAR2(60),
   DJR                  NUMBER(10),
   DJRMC                VARCHAR2(20),
   DJSJ                 DATE,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(20),
   ZXRQ                 DATE,
   constraint PK_MZK_KCKPDLR primary key (JLBH)
)
/

/*==============================================================*/
/* Table: MZK_KCKPDLRITEM                                       */
/*==============================================================*/
create table BFCRM10.MZK_KCKPDLRITEM  (
   JLBH                 NUMBER(10)                      not null,
   CZKHM                VARCHAR2(30)                    not null,
   constraint PK_MZK_KCKPDLRITEM primary key (JLBH, CZKHM)
)
/

/*==============================================================*/
/* Table: MZK_KCKPDLRKDITEM                                     */
/*==============================================================*/
create table BFCRM10.MZK_KCKPDLRKDITEM  (
   JLBH                 NUMBER(10)                      not null,
   CZKHM_BEGIN          VARCHAR2(30)                    not null,
   CZKHM_END            VARCHAR2(30)                    not null,
   MZJE                 NUMBER(14,2),
   SL                   NUMBER(10),
   constraint PK_MZK_KCKPDLRKDITEM primary key (JLBH, CZKHM_BEGIN)
)
/

/*==============================================================*/
/* Table: MZK_KCKPDPC                                           */
/*==============================================================*/
create table BFCRM10.MZK_KCKPDPC  (
   JLBH                 NUMBER(10)                      not null,
   HYKTYPE              NUMBER(10)                      not null,
   CZDD                 VARCHAR2(30),
   ZY                   VARCHAR2(60),
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(30)                    not null,
   DJSJ                 DATE                            not null,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(30),
   ZXRQ                 DATE,
   constraint PK_MZK_KCKPDPC primary key (JLBH)
)
/

/*==============================================================*/
/* Table: MZK_KCKPDPCITEM                                       */
/*==============================================================*/
create table BFCRM10.MZK_KCKPDPCITEM  (
   JLBH                 NUMBER(10)                      not null,
   CZKHM                VARCHAR2(30)                    not null,
   BGR                  NUMBER(10),
   MZJE                 NUMBER(14,2),
   constraint PK_MZK_KCKPDPCITEM primary key (JLBH, CZKHM)
)
/

/*==============================================================*/
/* Table: MZK_KCKPDPCKDITEM                                     */
/*==============================================================*/
create table BFCRM10.MZK_KCKPDPCKDITEM  (
   JLBH                 NUMBER(10)                      not null,
   HM_MIN               VARCHAR2(20)                    not null,
   HM_MAX               VARCHAR2(20),
   SL                   NUMBER(10),
   JE                   NUMBER(14,2),
   BGR                  NUMBER(10),
   constraint PK_MZK_KCKPDPCKDITEM primary key (JLBH, HM_MIN)
)
/

/*==============================================================*/
/* Table: MZK_KCKPDSY                                           */
/*==============================================================*/
create table BFCRM10.MZK_KCKPDSY  (
   JLBH                 NUMBER(10)                      not null,
   JLBH_PC              NUMBER(10)                      not null,
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(30)                    not null,
   DJSJ                 DATE                            not null,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(30),
   ZXRQ                 DATE,
   constraint PK_MZK_KCKPDSY primary key (JLBH)
)
/

/*==============================================================*/
/* Table: MZK_KCKPDSYMX                                         */
/*==============================================================*/
create table BFCRM10.MZK_KCKPDSYMX  (
   JLBH                 NUMBER(10)                      not null,
   CZKHM                VARCHAR2(30)                    not null,
   BGR                  NUMBER(10)                      not null,
   MZJE                 NUMBER(14,2)                    not null,
   BJ                   NUMBER(5)                       not null,
   constraint PK_MZK_KCKPDSYMX primary key (JLBH, CZKHM)
)
/

/*==============================================================*/
/* Table: MZK_KCKYXQBDJL                                        */
/*==============================================================*/
create table BFCRM10.MZK_KCKYXQBDJL  (
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
   constraint PK_MZK_KCKYXQBDJL primary key (JLBH)
)
/

/*==============================================================*/
/* Table: MZK_KCKYXQBDJLITEM                                    */
/*==============================================================*/
create table BFCRM10.MZK_KCKYXQBDJLITEM  (
   JLBH                 NUMBER(10)                      not null,
   CZKHM                VARCHAR2(13)                    not null,
   YYXQ                 DATE,
   constraint PK_MZK_KCKYXQBDJLITEM primary key (JLBH, CZKHM)
)
/

/*==============================================================*/
/* Table: MZK_KCKZFJL                                           */
/*==============================================================*/
create table BFCRM10.MZK_KCKZFJL  (
   JLBH                 NUMBER(10)                      not null,
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
   BGDDDM               VARCHAR2(10),
   constraint PK_MZK_KCKZFJL primary key (JLBH)
)
/

/*==============================================================*/
/* Table: MZK_KCKZFJLITEM                                       */
/*==============================================================*/
create table BFCRM10.MZK_KCKZFJLITEM  (
   JLBH                 NUMBER(10)                      not null,
   CZKHM                VARCHAR2(16)                    not null,
   QCYE                 NUMBER(14,2)                   default 0 not null,
   constraint PK_MZK_KCKZFJLITEM primary key (JLBH, CZKHM)
)
/

/*==============================================================*/
/* Table: MZK_KCYETZD                                           */
/*==============================================================*/
create table BFCRM10.MZK_KCYETZD  (
   JLBH                 NUMBER(10)                      not null,
   HYKTYPE              NUMBER(5)                       not null,
   BGDDDM               VARCHAR2(10)                    not null,
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16)                    not null,
   DJSJ                 DATE                            not null,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(16),
   ZXRQ                 DATE,
   constraint PK_MZK_KCYETZD primary key (JLBH)
)
/

/*==============================================================*/
/* Table: MZK_KCYETZDITEM                                       */
/*==============================================================*/
create table BFCRM10.MZK_KCYETZDITEM  (
   JLBH                 NUMBER(10)                      not null,
   HM                   VARCHAR2(16)                    not null,
   QCYE_OLD             NUMBER(14,2),
   TZJE                 NUMBER(14,2)                   default 0,
   QCYE_NEW             NUMBER(14,2),
   constraint PK_MZK_KCYETZDITEM primary key (JLBH, HM)
)
/

/*==============================================================*/
/* Table: MZK_KHDA                                              */
/*==============================================================*/
create table BFCRM10.MZK_KHDA  (
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
   constraint PK_MZK_KHDA primary key (KHID)
)
/

/*==============================================================*/
/* Index: MZK_KHDA_KHMC                                         */
/*==============================================================*/
create unique index BFCRM10.MZK_KHDA_KHMC on BFCRM10.MZK_KHDA (
   KHMC ASC
)
/

/*==============================================================*/
/* Table: MZK_LQJL                                              */
/*==============================================================*/
create table BFCRM10.MZK_LQJL  (
   JLBH                 NUMBER(10)                      not null,
   BGDDDM_BC            VARCHAR2(10)                    not null,
   BGDDDM_BR            VARCHAR2(10)                    not null,
   HYKKH1               VARCHAR2(30),
   HYKKH2               VARCHAR2(30),
   HYKSL                NUMBER(5)                       not null,
   LQR                  NUMBER(10),
   LQRMC                VARCHAR2(16),
   BJ_CZK               NUMBER(1)                       not null
      constraint CKC_BJ_CZK_MZK_LQJL check (BJ_CZK in (0,1)),
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
   constraint PK_MZK_LQJL primary key (JLBH)
)
/

/*==============================================================*/
/* Table: MZK_LQJLITEM                                          */
/*==============================================================*/
create table BFCRM10.MZK_LQJLITEM  (
   JLBH                 NUMBER(10)                      not null,
   HM                   VARCHAR2(30)                    not null,
   HYKTYPE              NUMBER(10)                      not null,
   JE                   NUMBER(14,2)                   default 0,
   LQRMC                VARCHAR2(16),
   constraint PK_MZK_LQJLITEM primary key (JLBH, HM)
)
/

/*==============================================================*/
/* Table: MZK_LQJLKDITEM                                        */
/*==============================================================*/
create table BFCRM10.MZK_LQJLKDITEM  (
   JLBH                 NUMBER(10)                      not null,
   CZKHM_BEGIN          VARCHAR2(30)                    not null,
   CZKHM_END            VARCHAR2(30)                    not null,
   HYKTYPE              NUMBER(10)                      not null,
   SKSL                 NUMBER(10),
   MZJE                 NUMBER(14,2),
   constraint PK_MZK_LQJLKDITEM primary key (JLBH, CZKHM_BEGIN)
)
/

/*==============================================================*/
/* Table: MZK_LQSQJL                                            */
/*==============================================================*/
create table BFCRM10.MZK_LQSQJL  (
   JLBH                 NUMBER(10)                      not null,
   HYKTYPE              NUMBER(5)                       not null,
   BGDDDM_BC            VARCHAR2(10)                    not null,
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
   constraint PK_MZK_LQSQJL primary key (JLBH)
)
/

/*==============================================================*/
/* Table: MZK_MZ                                                */
/*==============================================================*/
create table BFCRM10.MZK_MZ  (
   CODE                 NUMBER(5)                       not null,
   JE                   NUMBER(14,2)                    not null,
   constraint PK_MZK_MZ primary key (CODE)
)
/

/*==============================================================*/
/* Table: MZK_PLCK                                              */
/*==============================================================*/
create table BFCRM10.MZK_PLCK  (
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
      constraint CKC_STATUS_MZK_PLCK check (STATUS in (0,1,2)),
   CZDD                 VARCHAR2(10),
   MDFWDM               VARCHAR2(10),
   BJ_NB                NUMBER(5),
   MDID_CZ              NUMBER(5),
   YZSJF                NUMBER(14,2),
   SJZSJF               NUMBER(14,2),
   constraint PK_MZK_PLCK primary key (JLBH)
)
/

/*==============================================================*/
/* Index: MZK_PLCKJL_ZXRQ                                       */
/*==============================================================*/
create index BFCRM10.MZK_PLCKJL_ZXRQ on BFCRM10.MZK_PLCK (
   ZXRQ ASC
)
/

/*==============================================================*/
/* Table: MZK_PLCKITEM                                          */
/*==============================================================*/
create table BFCRM10.MZK_PLCKITEM  (
   CZJPJ_JLBH           NUMBER(10)                      not null,
   HYID                 NUMBER(10)                      not null,
   YJE                  NUMBER(14,2)                    not null,
   CKJE                 NUMBER(14,2)                    not null,
   CZKHM                VARCHAR2(16),
   BZ                   VARCHAR2(40),
   constraint PK_MZK_PLCKITEM primary key (CZJPJ_JLBH, HYID)
)
/

/*==============================================================*/
/* Table: MZK_PLCKKDITEM                                        */
/*==============================================================*/
create table BFCRM10.MZK_PLCKKDITEM  (
   JLBH                 NUMBER(10)                      not null,
   CZKHM_BEGIN          VARCHAR2(16)                    not null,
   CZKHM_END            VARCHAR2(16)                    not null,
   CKSL                 NUMBER(10),
   CKJE                 NUMBER(14,2),
   constraint PK_MZK_PLCKKDITEM primary key (JLBH, CZKHM_BEGIN)
)
/

/*==============================================================*/
/* Table: MZK_PLCKSKJL                                          */
/*==============================================================*/
create table BFCRM10.MZK_PLCKSKJL  (
   JLBH                 NUMBER(10)                      not null,
   ZFFSID               NUMBER(10)                      not null,
   JE                   NUMBER(14,2)                    not null,
   FKRQ                 DATE,
   constraint PK_MZK_PLCKSKJL primary key (JLBH, ZFFSID)
)
/

/*==============================================================*/
/* Table: MZK_PLCKZJFITEM                                       */
/*==============================================================*/
create table BFCRM10.MZK_PLCKZJFITEM  (
   JLBH                 NUMBER(10)                      not null,
   HYK_NO               VARCHAR2(16)                    not null,
   HYID                 NUMBER(10),
   ZSJF                 NUMBER(16,4),
   constraint PK_MZK_PLCKZJFITEM primary key (JLBH, HYK_NO)
)
/

/*==============================================================*/
/* Table: MZK_PLCKZKITEM                                        */
/*==============================================================*/
create table BFCRM10.MZK_PLCKZKITEM  (
   JLBH                 NUMBER(10)                      not null,
   HYK_NO               VARCHAR2(16)                    not null,
   YHQLX                NUMBER(10),
   ZKJE                 NUMBER(14,2),
   YXQTS                NUMBER(10),
   constraint PK_MZK_PLCKZKITEM primary key (JLBH, HYK_NO)
)
/

/*==============================================================*/
/* Table: MZK_PLCKZPMX                                          */
/*==============================================================*/
create table BFCRM10.MZK_PLCKZPMX  (
   JLBH                 NUMBER(10)                      not null,
   ZFFSID               NUMBER(10)                      not null,
   ZPHM                 VARCHAR2(30)                    not null,
   JE                   NUMBER(14,2)                    not null,
   YHID                 NUMBER(5),
   DW                   VARCHAR2(60),
   constraint PK_MZK_PLCKZPMX primary key (JLBH, ZFFSID, ZPHM)
)
/

/*==============================================================*/
/* Table: MZK_PLQK                                              */
/*==============================================================*/
create table BFCRM10.MZK_PLQK  (
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
   MDID_CZ              NUMBER(10),
   constraint PK_MZK_PLQK primary key (JLBH)
)
/

/*==============================================================*/
/* Index: MZK_PLQKJL_ZXRQ                                       */
/*==============================================================*/
create index BFCRM10.MZK_PLQKJL_ZXRQ on BFCRM10.MZK_PLQK (
   ZXRQ ASC
)
/

/*==============================================================*/
/* Table: MZK_PLQKITEM                                          */
/*==============================================================*/
create table BFCRM10.MZK_PLQKITEM  (
   JLBH                 NUMBER(10)                      not null,
   HYID                 NUMBER(10)                      not null,
   YJE                  NUMBER(14,2)                    not null,
   QKJE                 NUMBER(14,2)                    not null,
   CZKHM                VARCHAR2(16),
   constraint PK_MZK_PLQKITEM primary key (JLBH, HYID)
)
/

/*==============================================================*/
/* Table: MZK_PLQKKDITEM                                        */
/*==============================================================*/
create table BFCRM10.MZK_PLQKKDITEM  (
   JLBH                 NUMBER(10)                      not null,
   CZKHM_BEGIN          VARCHAR2(16)                    not null,
   CZKHM_END            VARCHAR2(16)                    not null,
   QKSL                 NUMBER(10),
   QKJE                 NUMBER(14,2),
   constraint PK_MZK_PLQKKDITEM primary key (JLBH, CZKHM_BEGIN)
)
/

/*==============================================================*/
/* Table: MZK_PLQKSKJL                                          */
/*==============================================================*/
create table BFCRM10.MZK_PLQKSKJL  (
   JLBH                 NUMBER(10)                      not null,
   ZFFSID               NUMBER(10)                      not null,
   JE                   NUMBER(14,2)                    not null,
   FKRQ                 DATE,
   constraint PK_MZK_PLQKSKJL primary key (JLBH, ZFFSID)
)
/

/*==============================================================*/
/* Table: MZK_PLQKZPMX                                          */
/*==============================================================*/
create table BFCRM10.MZK_PLQKZPMX  (
   JLBH                 NUMBER(10)                      not null,
   ZFFSID               NUMBER(10)                      not null,
   ZPHM                 VARCHAR2(30)                    not null,
   JE                   NUMBER(14,2)                    not null,
   YHID                 NUMBER(5),
   DW                   VARCHAR2(60),
   constraint PK_MZK_PLQKZPMX primary key (JLBH, ZFFSID, ZPHM)
)
/

/*==============================================================*/
/* Table: MZK_PLZC                                              */
/*==============================================================*/
create table BFCRM10.MZK_PLZC  (
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
   CZMD                 NUMBER(10),
   constraint PK_MZK_PLZC primary key (JLBH)
)
/

/*==============================================================*/
/* Table: MZK_PLZCITEM                                          */
/*==============================================================*/
create table BFCRM10.MZK_PLZCITEM  (
   JLBH                 NUMBER(10)                      not null,
   HYID_ZC              NUMBER(10)                      not null,
   ZCJE                 NUMBER(14,2)                    not null,
   constraint PK_MZK_PLZCITEM primary key (JLBH, HYID_ZC)
)
/

/*==============================================================*/
/* Table: MZK_PLZRITEM                                          */
/*==============================================================*/
create table BFCRM10.MZK_PLZRITEM  (
   JLBH                 NUMBER(10)                      not null,
   HYID_ZR              NUMBER(10)                      not null,
   ZRJE                 NUMBER(14,2)                    not null,
   constraint PK_MZK_PLZRITEM primary key (JLBH, HYID_ZR)
)
/

/*==============================================================*/
/* Table: MZK_QKGZJL                                            */
/*==============================================================*/
create table BFCRM10.MZK_QKGZJL  (
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
   constraint PK_MZK_QKGZJL primary key (JLBH)
)
/

/*==============================================================*/
/* Table: MZK_QKGZJLMX                                          */
/*==============================================================*/
create table BFCRM10.MZK_QKGZJLMX  (
   JLBH                 NUMBER(10)                      not null,
   CZKHM                VARCHAR2(30)                    not null,
   SKJLBH               NUMBER(10)                      not null,
   QCYE                 NUMBER(14,2)                    not null,
   HYID                 NUMBER(10)                      not null,
   constraint PK_MZK_QKGZJLMX primary key (JLBH, CZKHM)
)
/

/*==============================================================*/
/* Table: MZK_QKJL                                              */
/*==============================================================*/
create table BFCRM10.MZK_QKJL  (
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
   CZMD                 NUMBER(10),
   constraint PK_MZK_QKJL primary key (CZJPJ_JLBH)
)
/

/*==============================================================*/
/* Index: MZK_QKJL_HYID                                         */
/*==============================================================*/
create index BFCRM10.MZK_QKJL_HYID on BFCRM10.MZK_QKJL (
   HYID ASC
)
/

/*==============================================================*/
/* Index: MZK_QKJL_ZXRQ                                         */
/*==============================================================*/
create index BFCRM10.MZK_QKJL_ZXRQ on BFCRM10.MZK_QKJL (
   ZXRQ ASC
)
/

/*==============================================================*/
/* Table: MZK_RBB                                               */
/*==============================================================*/
create sequence BFCRM10.S_MZK_RBB
/
create table BFCRM10.MZK_RBB  (
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
   constraint PK_MZK_RBB primary key (RQ, HYKTYPE, MDID)
)
/

/*==============================================================*/
/* Table: MZK_SKJL                                              */
/*==============================================================*/
create table BFCRM10.MZK_SKJL  (
   JLBH                 NUMBER(10)                      not null,
   FS                   NUMBER(5)                       not null,
   SKSL                 NUMBER(10)                      not null,
   YSZE                 NUMBER(14,2)                   default 0 not null,
   ZKL                  NUMBER(16,4)                   default 1 not null,
   ZKJE                 NUMBER(14,2)                   default 0 not null,
   SSJE                 NUMBER(14,2)                   default 0 not null,
   YXQ                  DATE                            not null,
   BGDDDM               VARCHAR2(10)                    not null,
   ZY                   VARCHAR2(40),
   TK_FLAG              NUMBER(5)                      default 0 not null
      constraint CKC_TK_FLAG_MZK_SKJL check (TK_FLAG in (0,1)),
   DJR                  NUMBER(10)                      not null,
   DJRMC                VARCHAR2(16)                    not null,
   DJSJ                 DATE                            not null,
   ZXR                  NUMBER(10),
   ZXRMC                VARCHAR2(16),
   ZXRQ                 DATE,
   STATUS               NUMBER(5)                      default 0 not null
      constraint CKC_STATUS_MZK_SKJL check (STATUS in (0,1,2)),
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
   MDID_CZ              NUMBER(10),
   SXSJ                 DATE,
   HYKTYPE              NUMBER(10),
   BJ_JKSH              NUMBER(5),
   constraint PK_MZK_SKJL primary key (JLBH)
)
/

/*==============================================================*/
/* Index: MZK_CZKSKJL_ZXRQ                                      */
/*==============================================================*/
create index BFCRM10.MZK_CZKSKJL_ZXRQ on BFCRM10.MZK_SKJL (
   ZXRQ ASC
)
/

/*==============================================================*/
/* Table: MZK_SKJLITEM                                          */
/*==============================================================*/
create table BFCRM10.MZK_SKJLITEM  (
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
   constraint PK_MZK_SKJLITEM primary key (JLBH, CZKHM)
)
/

/*==============================================================*/
/* Table: MZK_SKJLKDITEM                                        */
/*==============================================================*/
create table BFCRM10.MZK_SKJLKDITEM  (
   JLBH                 NUMBER(10)                      not null,
   CZKHM_BEGIN          VARCHAR2(30)                    not null,
   CZKHM_END            VARCHAR2(30)                    not null,
   SKSL                 NUMBER(10),
   MZJE                 NUMBER(14,2),
   HYKTYPE              NUMBER(10),
   constraint PK_MZK_SKJLKDITEM primary key (JLBH, CZKHM_BEGIN)
)
/

/*==============================================================*/
/* Table: MZK_SKJLSKMX                                          */
/*==============================================================*/
create table BFCRM10.MZK_SKJLSKMX  (
   JLBH                 NUMBER(10)                      not null,
   ZFFSID               NUMBER(10)                      not null,
   JE                   NUMBER(14,2)                    not null,
   FKRQ                 DATE,
   constraint PK_MZK_SKJLSKMX primary key (JLBH, ZFFSID)
)
/

/*==============================================================*/
/* Table: MZK_SKJLZJFITEM                                       */
/*==============================================================*/
create table BFCRM10.MZK_SKJLZJFITEM  (
   JLBH                 NUMBER(10)                      not null,
   HYK_NO               VARCHAR2(16)                    not null,
   ZSJF                 NUMBER(16,4),
   constraint PK_MZK_SKJLZJFITEM primary key (JLBH, HYK_NO)
)
/

/*==============================================================*/
/* Table: MZK_SKJLZKITEM                                        */
/*==============================================================*/
create table BFCRM10.MZK_SKJLZKITEM  (
   JLBH                 NUMBER(10)                      not null,
   HYK_NO               VARCHAR2(16)                    not null,
   YHQLX                NUMBER(10),
   ZKJE                 NUMBER(14,2),
   YXQTS                NUMBER(10),
   constraint PK_MZK_SKJLZKITEM primary key (JLBH, HYK_NO)
)
/

/*==============================================================*/
/* Table: MZK_SKJLZPMX                                          */
/*==============================================================*/
create table BFCRM10.MZK_SKJLZPMX  (
   JLBH                 NUMBER(10)                      not null,
   ZFFSID               NUMBER(10)                      not null,
   ZPHM                 VARCHAR2(30)                    not null,
   JE                   NUMBER(14,2)                    not null,
   YHID                 NUMBER(5),
   DW                   VARCHAR2(60),
   constraint PK_MZK_SKJLZPMX primary key (JLBH, ZFFSID, ZPHM)
)
/

/*==============================================================*/
/* Table: MZK_TK                                                */
/*==============================================================*/
create table BFCRM10.MZK_TK  (
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
   BGDDDM               VARCHAR2(16),
   LYR                  NUMBER(5),
   LYRMC                VARCHAR2(16),
   FXDWID               NUMBER(5),
   YXQ_NEW              DATE,
   CZMD                 NUMBER(5),
   constraint PK_MZK_TK primary key (JLBH)
)
/

/*==============================================================*/
/* Table: MZK_TKITEM                                            */
/*==============================================================*/
create table BFCRM10.MZK_TKITEM  (
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
   constraint PK_MZK_TKITEM primary key (JLBH, HYID)
)
/

/*==============================================================*/
/* Table: MZK_XFRBB                                             */
/*==============================================================*/
create sequence BFCRM10.S_MZK_XFRBB
/
create table BFCRM10.MZK_XFRBB  (
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
   constraint PK_MZK_XFRBB primary key (RQ, HYKTYPE, MDID, CZMD, SKTNO)
)
/

/*==============================================================*/
/* Table: MZK_YEBD                                              */
/*==============================================================*/
create table BFCRM10.MZK_YEBD  (
   HYID                 NUMBER(10)                      not null,
   BDSJ                 DATE,
   DAC                  RAW(8),
   constraint PK_MZK_YEBD primary key (HYID)
)
/

/*==============================================================*/
/* Table: MZK_YEQLTZD                                           */
/*==============================================================*/
create table BFCRM10.MZK_YEQLTZD  (
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
   CZMD                 NUMBER(10),
   constraint PK_MZK_YEQLTZD primary key (JLBH)
)
/

/*==============================================================*/
/* Table: MZK_YEQLTZDITEM                                       */
/*==============================================================*/
create table BFCRM10.MZK_YEQLTZDITEM  (
   JLBH                 NUMBER(10)                      not null,
   HYID                 NUMBER(10)                      not null,
   YYE                  NUMBER(14,2),
   TZJE                 NUMBER(14,2),
   constraint PK_MZK_YEQLTZDITEM primary key (JLBH, HYID)
)
/

/*==============================================================*/
/* Table: MZK_YXQBDJL                                           */
/*==============================================================*/
create table BFCRM10.MZK_YXQBDJL  (
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
   CZMD                 NUMBER(10),
   constraint PK_MZK_YXQBDJL primary key (CZJPJ_JLBH)
)
/

/*==============================================================*/
/* Table: MZK_YXQBDJLITEM                                       */
/*==============================================================*/
create table BFCRM10.MZK_YXQBDJLITEM  (
   CZJPJ_JLBH           NUMBER(10)                      not null,
   HYID                 NUMBER(10)                      not null,
   YYXQ                 DATE,
   HYKNO                VARCHAR2(20),
   BJ_CHILD             NUMBER(5),
   constraint PK_MZK_YXQBDJLITEM primary key (CZJPJ_JLBH, HYID)
)
/

/*==============================================================*/
/* Table: MZK_ZFJL                                              */
/*==============================================================*/
create table BFCRM10.MZK_ZFJL  (
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
   BGDDDM               VARCHAR2(16),
   MDID                 NUMBER(10),
   constraint PK_MZK_ZFJL primary key (JLBH)
)
/

/*==============================================================*/
/* Table: MZK_ZFJLITEM                                          */
/*==============================================================*/
create table BFCRM10.MZK_ZFJLITEM  (
   JLBH                 NUMBER(10)                      not null,
   HYID                 NUMBER(10)                      not null,
   LJXFJE               NUMBER(14,2),
   LJJF                 FLOAT(50),
   WCLJF                FLOAT(50),
   YE                   NUMBER(14,2)                   default 0,
   JLJYE                NUMBER(14,2)                   default 0,
   HYKNO                VARCHAR2(20),
   BJ_CHILD             NUMBER(10),
   constraint PK_MZK_ZFJLITEM primary key (JLBH, HYID)
)
/

/*==============================================================*/
/* Index: MZK_ZFJLITEM_HYID                                     */
/*==============================================================*/
create index BFCRM10.MZK_ZFJLITEM_HYID on BFCRM10.MZK_ZFJLITEM (
   HYID ASC
)
/

/*==============================================================*/
/* Table: MZK_ZKGZ                                              */
/*==============================================================*/
create table BFCRM10.MZK_ZKGZ  (
   XSJE_BEGIN           NUMBER(14,2)                   default 0 not null,
   XSJE_END             NUMBER(14,2)                   default 0,
   YHQTYPE              NUMBER(5),
   YXQTS                NUMBER(10),
   ZSBL                 FLOAT(50)
)
/

/*==============================================================*/
/* Table: MZK_ZTBDJL                                            */
/*==============================================================*/
create table BFCRM10.MZK_ZTBDJL  (
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
   BGDDDM               VARCHAR2(10),
   MDID                 NUMBER(10),
   constraint PK_MZK_ZTBDJL primary key (JLBH)
)
/

/*==============================================================*/
/* Table: MZK_ZTBDJLITEM                                        */
/*==============================================================*/
create table BFCRM10.MZK_ZTBDJLITEM  (
   JLBH                 NUMBER(10)                      not null,
   HYID                 NUMBER(10)                      not null,
   OLD_STATUS           NUMBER(5)                      default 0,
   HYKNO                VARCHAR2(20),
   BJ_CHILD             NUMBER(5),
   constraint PK_MZK_ZTBDJLITEM primary key (JLBH, HYID)
)
/

/*==============================================================*/
/* Index: MZK_ZTBDJLITEM_HYID                                   */
/*==============================================================*/
create index BFCRM10.MZK_ZTBDJLITEM_HYID on BFCRM10.MZK_ZTBDJLITEM (
   HYID ASC
)
/

/*==============================================================*/
/* Table: PASSMZKJEZCLJL                                        */
/*==============================================================*/
create table BFCRM10.PASSMZKJEZCLJL  (
   JYBH                 NUMBER(9),
   HYID                 NUMBER(10),
   CLSJ                 DATE,
   CLLX                 NUMBER(5),
   JLBH                 NUMBER(10),
   MDID                 NUMBER(10),
   SKTNO                VARCHAR2(6),
   ZY                   VARCHAR2(100),
   JFJE                 NUMBER(14,2),
   DFJE                 NUMBER(14,2),
   YE                   NUMBER(14,2)                   default 0,
   JZRQ                 DATE,
   CRMJZRQ              DATE,
   SKYDM                VARCHAR2(8),
   SKYMC                VARCHAR2(16),
   TM                   DATE,
   JYID                 FLOAT(50),
   MDID_CZ              NUMBER(10),
   HOST_IP              VARCHAR2(50),
   USERNAME             VARCHAR2(50),
   USER_IP              VARCHAR2(50)
)
/

/*==============================================================*/
/* Table: PASSMZKRBB                                            */
/*==============================================================*/
create table BFCRM10.PASSMZKRBB  (
   RQ                   DATE,
   HYKTYPE              NUMBER(5),
   MDID                 NUMBER(10),
   SQYE                 NUMBER(14,2)                   default 0,
   JKJE                 NUMBER(14,2)                   default 0,
   CKJE                 NUMBER(14,2)                   default 0,
   QKJE                 NUMBER(14,2)                   default 0,
   XFJE                 NUMBER(14,2)                   default 0,
   TKJE                 NUMBER(14,2)                   default 0,
   TZJE                 NUMBER(14,2)                   default 0,
   QMYE                 NUMBER(14,2)                   default 0,
   BKJE                 NUMBER(14,2)                   default 0,
   TM                   DATE
)
/


create or replace trigger BFCRM10.TDB_MZKXX before delete
on BFCRM10.MZKXX for each row
declare
    integrity_error  exception;
    errno            integer;
    errmsg           char(200);
    dummy            integer;
    found            boolean;
    vDel             integer;
begin
    if :OLD.HYID is not null then 
       
       vDel := 0;
       select count(*) into vDel from MZK_JEZH where HYID= :OLD.HYID;
       vDel := nvl(vDel,0);
       if vDel>0 then 
         errno  := -20001;
         errmsg := '该"卡信息"还存在金额账户,不能删除';
         raise integrity_error;
       end if;  
       
       vDel := 0;
       select count(*) into vDel from MZK_YEBD where HYID= :OLD.HYID;
       vDel := nvl(vDel,0);
       if vDel>0 then 
         errno  := -20001;
         errmsg := '该"卡信息"还存在余额变动中,不能删除';
         raise integrity_error;
       end if; 
       
       vDel := 0;
       select count(*) into vDel from MZK_JEZCLJL where HYID= :OLD.HYID;
       vDel := nvl(vDel,0);
       if vDel>0 then 
         errno  := -20001;
         errmsg := '该"卡信息"还存在金额账处理记录,不能删除';
         raise integrity_error;
       end if;   
           
     end if;
--  Errors handling
exception
    when integrity_error then
       raise_application_error(errno, errmsg);
end;
/


--  Before insert trigger "BFCRM10.TIB_MZKXX" for table "BFCRM10.MZKXX"
select 'create trigger BFCRM10.TIB_MZKXX' from dual
/
create or replace trigger BFCRM10.TIB_MZKXX before insert
on BFCRM10.MZKXX for each row
begin
    select BFCRM10.S_MZKXX.NextVal into :NEW.TM from dual;
end;
/


--  Before update trigger "BFCRM10.TIU_MZKXX" for table "BFCRM10.MZKXX"
select 'create trigger BFCRM10.TIU_MZKXX' from dual
/
create or replace trigger BFCRM10.TIU_MZKXX before update
on BFCRM10.MZKXX for each row
begin
    select BFCRM10.S_MZKXX.NextVal into :NEW.TM from dual;
end;
/


create or replace trigger BFCRM10.TDB_MZKLQJL before delete
on BFCRM10.MZK_BKCK for each row
declare
    integrity_error  exception;
    errno            integer;
    errmsg           char(200);
    dummy            integer;
    found            boolean;

begin
    if :OLD.ZXR is not null then
              errno  := -20001;
              errmsg := '不能删除已审核的记录!';
              raise integrity_error;
    end if;
--  Errors handling
exception
    when integrity_error then
       raise_application_error(errno, errmsg);
end;
/


create or replace trigger BFCRM10.TIB_MZKLQJL before insert
on BFCRM10.MZK_BKCK for each row
declare
    integrity_error  exception;
    errno            integer;
    errmsg           char(200);
    dummy            integer;
    found            boolean;

begin
    if :NEW.JLBH=0 then
              errno  := -20001;
              errmsg := '记录编号不能为0!';
              raise integrity_error;
    end if;

--  Errors handling
exception
    when integrity_error then
       raise_application_error(errno, errmsg);
end;
/


create or replace trigger BFCRM10.TUB_MZKLQJL before update
of JLBH
on BFCRM10.MZK_BKCK for each row
declare
    integrity_error  exception;
    errno            integer;
    errmsg           char(200);
    dummy            integer;
    found            boolean;
    seq NUMBER;

begin
    seq := IntegrityPackage.GetNestLevel;
    if :OLD.ZXRQ is not null then
      errno  := -20001;
      errmsg := '已审核的记录不能再审核!';
      raise integrity_error;
    end if;
--  Errors handling
exception
    when integrity_error then
       raise_application_error(errno, errmsg);
end;
/


create or replace trigger BFCRM10.TDB_MZK_CZK_CKJL before delete
on BFCRM10.MZK_CKJL for each row
declare
    integrity_error  exception;
    errno            integer;
    errmsg           char(200);
    dummy            integer;
    found            boolean;

begin
    if :OLD.ZXR is not null then
              errno  := -20001;
              errmsg := '不能删除已审核的记录!';
              raise integrity_error;
    end if;

--  Errors handling
exception
    when integrity_error then
       raise_application_error(errno, errmsg);
end;
/


create or replace trigger BFCRM10.TIB_MZK_CZK_CKJL before insert
on BFCRM10.MZK_CKJL for each row
declare
    integrity_error  exception;
    errno            integer;
    errmsg           char(200);
    dummy            integer;
    found            boolean;

begin
    if :NEW.CZJPJ_JLBH=0 then
              errno  := -20001;
              errmsg := '记录编号不能为0!';
              raise integrity_error;
    end if;
--  Errors handling
exception
    when integrity_error then
       raise_application_error(errno, errmsg);
end;
/


create or replace trigger BFCRM10.TUB_MZK_CZK_CKJL before update
of CZJPJ_JLBH
on BFCRM10.MZK_CKJL for each row
declare
    integrity_error  exception;
    errno            integer;
    errmsg           char(200);
    dummy            integer;
    found            boolean;
    seq NUMBER;

begin
    seq := IntegrityPackage.GetNestLevel;
    if :OLD.ZXRQ is not null then
      errno  := -20001;
      errmsg := '已审核的记录不能再审核!';
      raise integrity_error;
    end if;
--  Errors handling
exception
    when integrity_error then
       raise_application_error(errno, errmsg);
end;
/


create or replace trigger BFCRM10.TDB_MZK_KX before delete
on BFCRM10.MZK_GS for each row
declare
    integrity_error  exception;
    errno            integer;
    errmsg           char(200);
    dummy            integer;
    found            boolean;

begin
    if :OLD.ZXR is not null then
              errno  := -20001;
              errmsg := '不能删除已审核的记录!';
              raise integrity_error;
    end if;
--  Errors handling
exception
    when integrity_error then
       raise_application_error(errno, errmsg);
end;
/


create or replace trigger BFCRM10.TIB_MZK_KX before insert
on BFCRM10.MZK_GS for each row
declare
    integrity_error  exception;
    errno            integer;
    errmsg           char(200);
    dummy            integer;
    found            boolean;

begin
    if :NEW.CZJPJ_JLBH=0 then
              errno  := -20001;
              errmsg := '记录编号不能为0!';
              raise integrity_error;
    end if;
--  Errors handling
exception
    when integrity_error then
       raise_application_error(errno, errmsg);
end;
/


create or replace trigger BFCRM10.TUB_MZK_KX before update
of CZJPJ_JLBH
on BFCRM10.MZK_GS for each row
declare
    integrity_error  exception;
    errno            integer;
    errmsg           char(200);
    dummy            integer;
    found            boolean;
    seq NUMBER;

begin
    seq := IntegrityPackage.GetNestLevel;
    if :OLD.ZXRQ is not null then
      errno  := -20001;
      errmsg := '已审核的记录不能再审核!';
      raise integrity_error;
    end if;
--  Errors handling
exception
    when integrity_error then
       raise_application_error(errno, errmsg);
end;
/


create or replace trigger BFCRM10.TDB_MZK_CZK_WK before delete
on BFCRM10.MZK_HK for each row
declare
    integrity_error  exception;
    errno            integer;
    errmsg           char(200);
    dummy            integer;
    found            boolean;

begin
    if :OLD.ZXR is not null then
              errno  := -20001;
              errmsg := '不能删除已审核的记录!';
              raise integrity_error;
    end if;
--  Errors handling
exception
    when integrity_error then
       raise_application_error(errno, errmsg);
end;
/


create or replace trigger BFCRM10.TIB_MZK_CZK_WK before insert
on BFCRM10.MZK_HK for each row
declare
    integrity_error  exception;
    errno            integer;
    errmsg           char(200);
    dummy            integer;
    found            boolean;

begin
    if :NEW.CZJPJ_JLBH=0 then
              errno  := -20001;
              errmsg := '记录编号不能为0!';
              raise integrity_error;
    end if;
--  Errors handling
exception
    when integrity_error then
       raise_application_error(errno, errmsg);
end;
/


create or replace trigger BFCRM10.TUB_MZK_CZK_WK before update
of CZJPJ_JLBH
on BFCRM10.MZK_HK for each row
declare
    integrity_error  exception;
    errno            integer;
    errmsg           char(200);
    dummy            integer;
    found            boolean;
    seq NUMBER;

begin
    seq := IntegrityPackage.GetNestLevel;
    if :OLD.ZXRQ is not null then
      errno  := -20001;
      errmsg := '已审核的记录不能再审核!';
      raise integrity_error;
    end if;
--  Errors handling
exception
    when integrity_error then
       raise_application_error(errno, errmsg);
end;
/


--  Before insert trigger "BFCRM10.TIB_MZK_JEZCLJL" for table "BFCRM10.MZK_JEZCLJL"
select 'create trigger BFCRM10.TIB_MZK_JEZCLJL' from dual
/
create or replace trigger BFCRM10.TIB_MZK_JEZCLJL before insert
on BFCRM10.MZK_JEZCLJL for each row
begin
    select BFCRM10.S_MZK_JEZCLJL.NextVal into :NEW.TM from dual;
end;
/


--  Before update trigger "BFCRM10.TIU_MZK_JEZCLJL" for table "BFCRM10.MZK_JEZCLJL"
select 'create trigger BFCRM10.TIU_MZK_JEZCLJL' from dual
/
create or replace trigger BFCRM10.TIU_MZK_JEZCLJL before update
on BFCRM10.MZK_JEZCLJL for each row
begin
    select BFCRM10.S_MZK_JEZCLJL.NextVal into :NEW.TM from dual;
end;
/


create or replace trigger BFCRM10.TI_PASSMZKJEZCLJL before delete
on BFCRM10.MZK_JEZCLJL for each row
declare
    integrity_error  exception;
    errno            integer;
    errmsg           char(200);
    dummy            integer;
    found            boolean;

begin
    if (:OLD.CLLX>0) then
      insert into PASSMZKJEZCLJL(JYBH, HYID, CLSJ, CLLX, JLBH, MDID, SKTNO, ZY,
      JFJE, DFJE, YE, JZRQ, CRMJZRQ, SKYDM, SKYMC, JYID, TM, MDID_CZ,HOST_IP,USERNAME,USER_IP)
      select :OLD.JYBH, :OLD.HYID, :OLD.CLSJ, :OLD.CLLX, :OLD.JLBH, :OLD.MDID, :OLD.SKTNO, :OLD.ZY,
      :OLD.JFJE, :OLD.DFJE, :OLD.YE, :OLD.JZRQ, :OLD.CRMJZRQ, :OLD.SKYDM, :OLD.SKYMC, :OLD.JYID, sysdate, :OLD.MDID_CZ,
      utl_inaddr.get_host_address,SYS_CONTEXT('USERENV','HOST'),SYS_CONTEXT('USERENV','IP_ADDRESS') from dual;
    end if;
--  Errors handling
exception
    when integrity_error then
       raise_application_error(errno, errmsg);
end;
/


create or replace trigger BFCRM10.TDB_MZK_JEZH before delete
on BFCRM10.MZK_JEZH for each row
declare
    integrity_error  exception;
    errno            integer;
    errmsg           char(200);
    dummy            integer;
    found            boolean;
    vDel             integer;
begin
    if :OLD.HYID is not null then 
       vDel := 0;
       select count(*) into vDel from HYK_JEZCLJL where HYID= :OLD.HYID;
       vDel := nvl(vDel,0);
       if vDel>0 then 
         errno  := -20001;
         errmsg := '该"卡金额账"还存在金额账处理记录中,不能删除';
         raise integrity_error;
       end if;   
       
       vDel := 0;
       select count(*) into vDel from HYK_YEBD where HYID= :OLD.HYID;
       vDel := nvl(vDel,0);
       if vDel>0 then 
         errno  := -20001;
         errmsg := '该"卡金额账"还存在金额账余额变动中,不能删除';
         raise integrity_error;
       end if;       
     end if;
--  Errors handling
exception
    when integrity_error then
       raise_application_error(errno, errmsg);
end;
/


--  Before insert trigger "BFCRM10.TIB_MZK_JEZH" for table "BFCRM10.MZK_JEZH"
select 'create trigger BFCRM10.TIB_MZK_JEZH' from dual
/
create or replace trigger BFCRM10.TIB_MZK_JEZH before insert
on BFCRM10.MZK_JEZH for each row
begin
    select BFCRM10.S_MZK_JEZH.NextVal into :NEW.TM from dual;
end;
/


--  Before update trigger "BFCRM10.TIU_MZK_JEZH" for table "BFCRM10.MZK_JEZH"
select 'create trigger BFCRM10.TIU_MZK_JEZH' from dual
/
create or replace trigger BFCRM10.TIU_MZK_JEZH before update
on BFCRM10.MZK_JEZH for each row
begin
    select BFCRM10.S_MZK_JEZH.NextVal into :NEW.TM from dual;
end;
/


create or replace trigger BFCRM10.TDB_MZKJKJL before delete
on BFCRM10.MZK_JKJL for each row
declare
    integrity_error  exception;
    errno            integer;
    errmsg           char(200);
    dummy            integer;
    found            boolean;

begin
    if :OLD.ZXR is not null then
              errno  := -20001;
              errmsg := '不能删除已审核的记录!';
              raise integrity_error;
    end if;
--  Errors handling
exception
    when integrity_error then
       raise_application_error(errno, errmsg);
end;
/


--  Before insert trigger "BFCRM10.TIB_MZKJKJL" for table "BFCRM10.MZK_JKJL"
create or replace trigger BFCRM10.TIB_MZKJKJL before insert
on BFCRM10.MZK_JKJL for each row
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
    if :NEW.JLBH=0 then
      errno  := -20001;
      errmsg := '记录编号不能为0!';
      raise integrity_error;
    end if;
    
    if :NEW.JKSL <=0 then
      errno  := -20001;
      errmsg := '建卡数量不能小于或等于0!';
      raise integrity_error;  
    end if;
    
--  Errors handling
exception
    when integrity_error then
       raise_application_error(errno, errmsg);
end;
/


create or replace trigger BFCRM10.TUB_MZKJKJL before update
of JLBH
on BFCRM10.MZK_JKJL for each row
declare
    integrity_error  exception;
    errno            integer;
    errmsg           char(200);
    dummy            integer;
    found            boolean;
    seq NUMBER;

begin
    seq := IntegrityPackage.GetNestLevel;
    if :OLD.ZXRQ is not null then
      errno  := -20001;
      errmsg := '已审核的记录不能再审核!';
      raise integrity_error;
    end if;
--  Errors handling
exception
    when integrity_error then
       raise_application_error(errno, errmsg);
end;
/


--  Before insert trigger "BFCRM10.TIB_MZK_KCCZKBGZ" for table "BFCRM10.MZK_KCBGZ"
select 'create trigger BFCRM10.TIB_MZK_KCCZKBGZ' from dual
/
create or replace trigger BFCRM10.TIB_MZK_KCCZKBGZ before insert
on BFCRM10.MZK_KCBGZ for each row
begin
    select BFCRM10.S_MZK_KCBGZ.NextVal into :NEW.TM from dual;
end;
/


--  Before update trigger "BFCRM10.TIU_MZK_KCCZKBGZ" for table "BFCRM10.MZK_KCBGZ"
select 'create trigger BFCRM10.TIU_MZK_KCCZKBGZ' from dual
/
create or replace trigger BFCRM10.TIU_MZK_KCCZKBGZ before update
on BFCRM10.MZK_KCBGZ for each row
begin
    select BFCRM10.S_MZK_KCBGZ.NextVal into :NEW.TM from dual;
end;
/


create or replace trigger BFCRM10.TDB_KCK_YXQBDJL before delete
on BFCRM10.MZK_KCKYXQBDJL for each row
declare
    integrity_error  exception;
    errno            integer;
    errmsg           char(200);
    dummy            integer;
    found            boolean;

begin
    if :OLD.ZXR is not null then
              errno  := -20001;
              errmsg := '不能删除已审核的记录!';
              raise integrity_error;
    end if;
--  Errors handling
exception
    when integrity_error then
       raise_application_error(errno, errmsg);
end;
/


create or replace trigger BFCRM10.TIB_KCK_YXQBDJL before insert
on BFCRM10.MZK_KCKYXQBDJL for each row
declare
    integrity_error  exception;
    errno            integer;
    errmsg           char(200);
    dummy            integer;
    found            boolean;

begin
    if :NEW.JLBH=0 then
              errno  := -20001;
              errmsg := '记录编号不能为0!';
              raise integrity_error;
    end if;
--  Errors handling
exception
    when integrity_error then
       raise_application_error(errno, errmsg);
end;
/


select 'create trigger BFCRM10.TUB_KCK_YXQBDJL' from dual
/
create or replace trigger BFCRM10.TUB_KCK_YXQBDJL before update
of JLBH
on BFCRM10.MZK_KCKYXQBDJL for each row
declare
    integrity_error  exception;
    errno            integer;
    errmsg           char(200);
    dummy            integer;
    found            boolean;
    seq NUMBER;

begin
    seq := IntegrityPackage.GetNestLevel;
    if :OLD.ZXRQ is not null then
      errno  := -20001;
      errmsg := '已审核的记录不能再审核!';
      raise integrity_error;
    end if;
--  Errors handling
exception
    when integrity_error then
       raise_application_error(errno, errmsg);
end;
/


create or replace trigger BFCRM10.TDB_MZK_KCKZFJL before delete
on BFCRM10.MZK_KCKZFJL for each row
declare
    integrity_error  exception;
    errno            integer;
    errmsg           char(200);
    dummy            integer;
    found            boolean;

begin
    if :OLD.ZXR is not null then
              errno  := -20001;
              errmsg := '不能删除已审核的记录!';
              raise integrity_error;
    end if;
--  Errors handling
exception
    when integrity_error then
       raise_application_error(errno, errmsg);
end;
/


create or replace trigger BFCRM10.TIB_MZK_KCKZFJL before insert
on BFCRM10.MZK_KCKZFJL for each row
declare
    integrity_error  exception;
    errno            integer;
    errmsg           char(200);
    dummy            integer;
    found            boolean;

begin
    if :NEW.JLBH=0 then
              errno  := -20001;
              errmsg := '记录编号不能为0!';
              raise integrity_error;
    end if;
--  Errors handling
exception
    when integrity_error then
       raise_application_error(errno, errmsg);
end;
/


create or replace trigger BFCRM10.TUB_MZK_KCKZFJL before update
of JLBH
on BFCRM10.MZK_KCKZFJL for each row
declare
    integrity_error  exception;
    errno            integer;
    errmsg           char(200);
    dummy            integer;
    found            boolean;
    seq NUMBER;

begin
    seq := IntegrityPackage.GetNestLevel;
    if :OLD.ZXRQ is not null then
      errno  := -20001;
      errmsg := '已审核的记录不能再审核!';
      raise integrity_error;
    end if;
--  Errors handling
exception
    when integrity_error then
       raise_application_error(errno, errmsg);
end;
/


create or replace trigger BFCRM10.TDB_MZK_KHDA before delete
on BFCRM10.MZK_KHDA for each row
declare
    integrity_error  exception;
    errno            integer;
    errmsg           char(200);
    dummy            integer;
    found            boolean;
    vDel             integer;
begin
    if :OLD.KHID is not null then 
       vDel := 0;
       select count(*) into vDel from MZKXX where KHID= :OLD.KHID;
       vDel := nvl(vDel,0);
       if vDel>0 then 
         errno  := -20001;
         errmsg := '该"客户档案"还存在于卡信息中,不能删除';
         raise integrity_error;
       end if;   
       
       vDel := 0;
       select count(*) into vDel from MZK_SKJL where KHID= :OLD.KHID;
       vDel := nvl(vDel,0);
       if vDel>0 then 
         errno  := -20001;
         errmsg := '该"客户档案"还存在于售卡记录中,不能删除';
         raise integrity_error;
       end if;  
       
       vDel := 0;
       select count(*) into vDel from MZK_PLCK where KHID= :OLD.KHID;
       vDel := nvl(vDel,0);
       if vDel>0 then 
         errno  := -20001;
         errmsg := '该"客户档案"还存在于批量存款记录中,不能删除';
         raise integrity_error;
       end if;  
          
     end if;
--  Errors handling
exception
    when integrity_error then
       raise_application_error(errno, errmsg);
end;
/


create or replace trigger BFCRM10.TDB_MZKLQJL before delete
on BFCRM10.MZK_LQJL for each row
declare
    integrity_error  exception;
    errno            integer;
    errmsg           char(200);
    dummy            integer;
    found            boolean;

begin
    if :OLD.ZXR is not null then
              errno  := -20001;
              errmsg := '不能删除已审核的记录!';
              raise integrity_error;
    end if;
--  Errors handling
exception
    when integrity_error then
       raise_application_error(errno, errmsg);
end;
/


create or replace trigger BFCRM10.TIB_MZKLQJL before insert
on BFCRM10.MZK_LQJL for each row
declare
    integrity_error  exception;
    errno            integer;
    errmsg           char(200);
    dummy            integer;
    found            boolean;

begin
    if :NEW.JLBH=0 then
              errno  := -20001;
              errmsg := '记录编号不能为0!';
              raise integrity_error;
    end if;

--  Errors handling
exception
    when integrity_error then
       raise_application_error(errno, errmsg);
end;
/


create or replace trigger BFCRM10.TUB_MZKLQJL before update
of JLBH
on BFCRM10.MZK_LQJL for each row
declare
    integrity_error  exception;
    errno            integer;
    errmsg           char(200);
    dummy            integer;
    found            boolean;
    seq NUMBER;

begin
    seq := IntegrityPackage.GetNestLevel;
    if :OLD.ZXRQ is not null then
      errno  := -20001;
      errmsg := '已审核的记录不能再审核!';
      raise integrity_error;
    end if;
--  Errors handling
exception
    when integrity_error then
       raise_application_error(errno, errmsg);
end;
/


create or replace trigger BFCRM10.TDB_MZK_PLCKJL before delete
on BFCRM10.MZK_PLCK for each row
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


create or replace trigger BFCRM10.TIB_MZK_PLCKJL before insert
on BFCRM10.MZK_PLCK for each row
declare
    integrity_error  exception;
    errno            integer;
    errmsg           char(200);
    dummy            integer;
    found            boolean;

begin
    if :NEW.JLBH=0 then
              errno  := -20001;
              errmsg := '记录编号不能为0!';
              raise integrity_error;
    end if;
--  Errors handling
exception
    when integrity_error then
       raise_application_error(errno, errmsg);
end;
/


create or replace trigger BFCRM10.TUB_MZK_PLCKJL before update
of JLBH
on BFCRM10.MZK_PLCK for each row
declare
    integrity_error  exception;
    errno            integer;
    errmsg           char(200);
    dummy            integer;
    found            boolean;
    seq NUMBER;

begin
    seq := IntegrityPackage.GetNestLevel;
    if :OLD.ZXRQ is not null then
      errno  := -20001;
      errmsg := '已审核的记录不能再审核!';
      raise integrity_error;
    end if;
--  Errors handling
exception
    when integrity_error then
       raise_application_error(errno, errmsg);
end;
/


create or replace trigger BFCRM10.TDB_MZK_PLQKJL before delete
on BFCRM10.MZK_PLQK for each row
declare
    integrity_error  exception;
    errno            integer;
    errmsg           char(200);
    dummy            integer;
    found            boolean;

begin
    if :OLD.ZXR is not null then
              errno  := -20001;
              errmsg := '不能删除已审核的取款记录!';
              raise integrity_error;
    end if;
--  Errors handling
exception
    when integrity_error then
       raise_application_error(errno, errmsg);
end;
/


create or replace trigger BFCRM10.TIB_MZK_PLQKJL before insert
on BFCRM10.MZK_PLQK for each row
declare
    integrity_error  exception;
    errno            integer;
    errmsg           char(200);
    dummy            integer;
    found            boolean;

begin
    if :NEW.JLBH=0 then
              errno  := -20001;
              errmsg := '记录编号不能为0!';
              raise integrity_error;
    end if;
--  Errors handling
exception
    when integrity_error then
       raise_application_error(errno, errmsg);
end;
/


create or replace trigger BFCRM10.TUB_MZK_PLQKJL before update
of JLBH
on BFCRM10.MZK_PLQK for each row
declare
    integrity_error  exception;
    errno            integer;
    errmsg           char(200);
    dummy            integer;
    found            boolean;
    seq NUMBER;

begin
    seq := IntegrityPackage.GetNestLevel;
    if :OLD.ZXRQ is not null then
      errno  := -20001;
      errmsg := '已审核的记录不能再审核!';
      raise integrity_error;
    end if;
--  Errors handling
exception
    when integrity_error then
       raise_application_error(errno, errmsg);
end;
/


create or replace trigger BFCRM10.TDB_MZK_PLZCJL before delete
on BFCRM10.MZK_PLZC for each row
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


create or replace trigger BFCRM10.TIB_MZK_PLZCJL before insert
on BFCRM10.MZK_PLZC for each row
declare
    integrity_error  exception;
    errno            integer;
    errmsg           char(200);
    dummy            integer;
    found            boolean;

begin
    if :NEW.JLBH=0 then
              errno  := -20001;
              errmsg := '记录编号不能为0!';
              raise integrity_error;
    end if;
--  Errors handling
exception
    when integrity_error then
       raise_application_error(errno, errmsg);
end;
/


create or replace trigger BFCRM10.TUB_MZK_PLZCJL before update
of JLBH
on BFCRM10.MZK_PLZC for each row
declare
    integrity_error  exception;
    errno            integer;
    errmsg           char(200);
    dummy            integer;
    found            boolean;
    seq NUMBER;

begin
    seq := IntegrityPackage.GetNestLevel;
    if :OLD.ZXRQ is not null then
      errno  := -20001;
      errmsg := '已审核的记录不能再审核!';
      raise integrity_error;
    end if;
--  Errors handling
exception
    when integrity_error then
       raise_application_error(errno, errmsg);
end;
/


create or replace trigger BFCRM10.TDB_MZK_QKJL before delete
on BFCRM10.MZK_QKJL for each row
declare
    integrity_error  exception;
    errno            integer;
    errmsg           char(200);
    dummy            integer;
    found            boolean;

begin
    if :OLD.ZXR is not null then
              errno  := -20001;
              errmsg := '不能删除已审核的记录!';
              raise integrity_error;
    end if;
--  Errors handling
exception
    when integrity_error then
       raise_application_error(errno, errmsg);
end;
/


create or replace trigger BFCRM10.TIB_MZK_QKJL before insert
on BFCRM10.MZK_QKJL for each row
declare
    integrity_error  exception;
    errno            integer;
    errmsg           char(200);
    dummy            integer;
    found            boolean;

begin
    if :NEW.CZJPJ_JLBH=0 then
              errno  := -20001;
              errmsg := '记录编号不能为0!';
              raise integrity_error;
    end if;
--  Errors handling
exception
    when integrity_error then
       raise_application_error(errno, errmsg);
end;
/


create or replace trigger BFCRM10.TUB_MZK_QKJL before update
of CZJPJ_JLBH
on BFCRM10.MZK_QKJL for each row
declare
    integrity_error  exception;
    errno            integer;
    errmsg           char(200);
    dummy            integer;
    found            boolean;
    seq NUMBER;

begin
    seq := IntegrityPackage.GetNestLevel;
    if :OLD.ZXRQ is not null then
      errno  := -20001;
      errmsg := '已审核的记录不能再审核!';
      raise integrity_error;
    end if;
--  Errors handling
exception
    when integrity_error then
       raise_application_error(errno, errmsg);
end;
/


--  Before insert trigger "BFCRM10.TIB_MZK_RBB" for table "BFCRM10.MZK_RBB"
select 'create trigger BFCRM10.TIB_MZK_RBB' from dual
/
create or replace trigger BFCRM10.TIB_MZK_RBB before insert
on BFCRM10.MZK_RBB for each row
begin
    select BFCRM10.S_MZK_RBB.NextVal into :NEW.TM from dual;
end;
/


--  Before update trigger "BFCRM10.TIU_MZK_RBB" for table "BFCRM10.MZK_RBB"
select 'create trigger BFCRM10.TIU_MZK_RBB' from dual
/
create or replace trigger BFCRM10.TIU_MZK_RBB before update
on BFCRM10.MZK_RBB for each row
begin
    select BFCRM10.S_MZK_RBB.NextVal into :NEW.TM from dual;
end;
/


create or replace trigger BFCRM10.TI_PASSMZKCZKRBB before delete
on BFCRM10.MZK_RBB for each row
declare
    integrity_error  exception;
    errno            integer;
    errmsg           char(200);
    dummy            integer;
    found            boolean;

begin
    insert into PASSMZKRBB(TM,RQ,HYKTYPE,MDID,SQYE,JKJE,CKJE,QKJE,XFJE,TKJE,TZJE,QMYE,BKJE)
    select sysdate,:OLD.RQ,:OLD.HYKTYPE,:OLD.MDID,:OLD.SQYE,:OLD.JKJE,:OLD.CKJE,:OLD.QKJE,:OLD.XFJE,:OLD.TKJE,:OLD.TZJE,:OLD.QMYE,:OLD.BKJE 
    from dual;
--  Errors handling
exception
    when integrity_error then
       raise_application_error(errno, errmsg);
end;
/


--  Before delete trigger "BFCRM10.TDB_MZK_CZKSKJL" for table "BFCRM10.MZK_SKJL"
create or replace trigger BFCRM10.TDB_MZK_CZKSKJL before delete
on BFCRM10.MZK_SKJL for each row
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


create or replace trigger BFCRM10.TUB_MZK_CZKSKJL before update
of JLBH
on BFCRM10.MZK_SKJL for each row
declare
    integrity_error  exception;
    errno            integer;
    errmsg           char(200);
    dummy            integer;
    found            boolean;
    seq NUMBER;

begin
    seq := IntegrityPackage.GetNestLevel;
    if :OLD.ZXRQ is not null then
      errno  := -20001;
      errmsg := '已审核的记录不能再审核!';
      raise integrity_error;
    end if;
--  Errors handling
exception
    when integrity_error then
       raise_application_error(errno, errmsg);
end;
/


create or replace trigger BFCRM10.TIB_MZK_CZKSKJL before insert
on BFCRM10.MZK_SKJL for each row
declare
    integrity_error  exception;
    errno            integer;
    errmsg           char(200);
    dummy            integer;
    found            boolean;

begin
    if :NEW.JLBH=0 then
              errno  := -20001;
              errmsg := '记录编号不能为0!';
              raise integrity_error;
    end if;
--  Errors handling
exception
    when integrity_error then
       raise_application_error(errno, errmsg);
end;
/


create or replace trigger BFCRM10.TDB_MZK_TK before delete
on BFCRM10.MZK_TK for each row
declare
    integrity_error  exception;
    errno            integer;
    errmsg           char(200);
    dummy            integer;
    found            boolean;

begin
    if :OLD.ZXR is not null then
              errno  := -20001;
              errmsg := '不能删除已审核的记录!';
              raise integrity_error;
    end if;
 --  Errors handling
exception
    when integrity_error then
       raise_application_error(errno, errmsg);
end;
/


create or replace trigger BFCRM10.TIB_MZK_TK before insert
on BFCRM10.MZK_TK for each row
declare
    integrity_error  exception;
    errno            integer;
    errmsg           char(200);
    dummy            integer;
    found            boolean;

begin
    if :NEW.JLBH=0 then
              errno  := -20001;
              errmsg := '记录编号不能为0!';
              raise integrity_error;
    end if;
--  Errors handling
exception
    when integrity_error then
       raise_application_error(errno, errmsg);
end;
/


create or replace trigger BFCRM10.TUB_MZK_TK before update
of JLBH
on BFCRM10.MZK_TK for each row
declare
    integrity_error  exception;
    errno            integer;
    errmsg           char(200);
    dummy            integer;
    found            boolean;
    seq NUMBER;

begin
    seq := IntegrityPackage.GetNestLevel;
    if :OLD.ZXRQ is not null then
      errno  := -20001;
      errmsg := '已审核的记录不能再审核!';
      raise integrity_error;
    end if;
--  Errors handling
exception
    when integrity_error then
       raise_application_error(errno, errmsg);
end;
/


--  Before insert trigger "BFCRM10.TIB_MZK_XFRBB" for table "BFCRM10.MZK_XFRBB"
select 'create trigger BFCRM10.TIB_MZK_XFRBB' from dual
/
create or replace trigger BFCRM10.TIB_MZK_XFRBB before insert
on BFCRM10.MZK_XFRBB for each row
begin
    select BFCRM10.S_MZK_XFRBB.NextVal into :NEW.TM from dual;
end;
/


--  Before update trigger "BFCRM10.TIU_MZK_XFRBB" for table "BFCRM10.MZK_XFRBB"
select 'create trigger BFCRM10.TIU_MZK_XFRBB' from dual
/
create or replace trigger BFCRM10.TIU_MZK_XFRBB before update
on BFCRM10.MZK_XFRBB for each row
begin
    select BFCRM10.S_MZK_XFRBB.NextVal into :NEW.TM from dual;
end;
/


create or replace trigger BFCRM10.TDB_MZK_YXQBDJL before delete
on BFCRM10.MZK_YXQBDJL for each row
declare
    integrity_error  exception;
    errno            integer;
    errmsg           char(200);
    dummy            integer;
    found            boolean;

begin
    if :OLD.ZXR is not null then
              errno  := -20001;
              errmsg := '不能删除已审核的记录!';
              raise integrity_error;
    end if;
--  Errors handling
exception
    when integrity_error then
       raise_application_error(errno, errmsg);
end;
/


create or replace trigger BFCRM10.TIB_MZK_YXQBDJL before insert
on BFCRM10.MZK_YXQBDJL for each row
declare
    integrity_error  exception;
    errno            integer;
    errmsg           char(200);
    dummy            integer;
    found            boolean;

begin
    if :NEW.CZJPJ_JLBH=0 then
              errno  := -20001;
              errmsg := '记录编号不能为0!';
              raise integrity_error;
    end if;
--  Errors handling
exception
    when integrity_error then
       raise_application_error(errno, errmsg);
end;
/


create or replace trigger BFCRM10.TUB_MZK_YXQBDJL before update
of CZJPJ_JLBH
on BFCRM10.MZK_YXQBDJL for each row
declare
    integrity_error  exception;
    errno            integer;
    errmsg           char(200);
    dummy            integer;
    found            boolean;
    seq NUMBER;

begin
    seq := IntegrityPackage.GetNestLevel;
    if :OLD.ZXRQ is not null then
      errno  := -20001;
      errmsg := '已审核的记录不能再审核!';
      raise integrity_error;
    end if;
--  Errors handling
exception
    when integrity_error then
       raise_application_error(errno, errmsg);
end;
/


create or replace trigger BFCRM10.TDB_MZK_ZFJL before delete
on BFCRM10.MZK_ZFJL for each row
declare
    integrity_error  exception;
    errno            integer;
    errmsg           char(200);
    dummy            integer;
    found            boolean;

begin
    if :OLD.ZXR is not null then
              errno  := -20001;
              errmsg := '不能删除已审核的记录!';
              raise integrity_error;
    end if;
--  Errors handling
exception
    when integrity_error then
       raise_application_error(errno, errmsg);
end;
/


create or replace trigger BFCRM10.TIB_MZK_ZFJL before insert
on BFCRM10.MZK_ZFJL for each row
declare
    integrity_error  exception;
    errno            integer;
    errmsg           char(200);
    dummy            integer;
    found            boolean;

begin
    if :NEW.JLBH=0 then
              errno  := -20001;
              errmsg := '记录编号不能为0!';
              raise integrity_error;
    end if;
--  Errors handling
exception
    when integrity_error then
       raise_application_error(errno, errmsg);
end;
/


create or replace trigger BFCRM10.TUB_MZK_ZFJL before update
of JLBH
on BFCRM10.MZK_ZFJL for each row
declare
    integrity_error  exception;
    errno            integer;
    errmsg           char(200);
    dummy            integer;
    found            boolean;
    seq NUMBER;

begin
    seq := IntegrityPackage.GetNestLevel;
    if :OLD.ZXRQ is not null then
      errno  := -20001;
      errmsg := '已审核的记录不能再审核!';
      raise integrity_error;
    end if;
--  Errors handling
exception
    when integrity_error then
       raise_application_error(errno, errmsg);
end;
/


create or replace trigger BFCRM10.TDB_MZK_ZTBDJL before delete
on BFCRM10.MZK_ZTBDJL for each row
declare
    integrity_error  exception;
    errno            integer;
    errmsg           char(200);
    dummy            integer;
    found            boolean;

begin
    if :OLD.ZXR is not null then
              errno  := -20001;
              errmsg := '不能删除已审核的记录!';
              raise integrity_error;
    end if;
--  Errors handling
exception
    when integrity_error then
       raise_application_error(errno, errmsg);
end;
/


create or replace trigger BFCRM10.TIB_MZK_ZTBDJL before insert
on BFCRM10.MZK_ZTBDJL for each row
declare
    integrity_error  exception;
    errno            integer;
    errmsg           char(200);
    dummy            integer;
    found            boolean;

begin
    if :NEW.JLBH=0 then
              errno  := -20001;
              errmsg := '记录编号不能为0!';
              raise integrity_error;
    end if;
--  Errors handling
exception
    when integrity_error then
       raise_application_error(errno, errmsg);
end;
/


create or replace trigger BFCRM10.TUB_MZK_ZTBDJL before update
of JLBH
on BFCRM10.MZK_ZTBDJL for each row
declare
    integrity_error  exception;
    errno            integer;
    errmsg           char(200);
    dummy            integer;
    found            boolean;
    seq NUMBER;

begin
    seq := IntegrityPackage.GetNestLevel;
    if :OLD.ZXRQ is not null then
      errno  := -20001;
      errmsg := '已审核的记录不能再审核!';
      raise integrity_error;
    end if;
--  Errors handling
exception
    when integrity_error then
       raise_application_error(errno, errmsg);
end;
/

