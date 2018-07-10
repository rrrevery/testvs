--Oracle
grant all on BFBHDD8.CRMSTAMP to TRANS_USER8;
grant select on BFBHDD8.BM to TRANS_USER8;
grant select on BFBHDD8.HT to TRANS_USER8;
grant select on BFBHDD8.WLDW to TRANS_USER8;
grant select on BFBHDD8.SPFL to TRANS_USER8;
grant select on BFBHDD8.SPSB to TRANS_USER8;
grant select on BFBHDD8.SPXX to TRANS_USER8;
grant select on BFBHDD8.GTSP to TRANS_USER8;
grant select on BFBHDD8.SKFS to TRANS_USER8;
grant select on BFBHDD8.CXHDDEF to TRANS_USER8;
--如果没有CRMSTAMP表
create table TRANS_USER8.CRMSTAMP  (
   TBL_NAME             VARCHAR2(20)                    not null,
   STAMP_OLD            NUMBER(10),
   constraint PK_CRMSTAMP primary key (TBL_NAME)
)

create or replace view TRANS_USER8.BM as select * from BFBHDD8.BM where length(BMDM)>0;
create or replace view TRANS_USER8.HT as select HTH,B.BMDM DEPTID,GHDWDM,(case H.STATUS when 3 then 1 else 0 end) BJ_YX,H.TM,W.NAME from BFBHDD8.HT H,BFBHDD8.WLDW W,TRANS_USER8.BM B where H.DEPTID=B.DEPTID and H.GHDWDM=W.CODE;
create or replace view TRANS_USER8.SPFL as select * from BFBHDD8.SPFL where length(SPFL)>0;
create or replace view TRANS_USER8.SPSB as select * from BFBHDD8.SPSB;
create or replace view TRANS_USER8.SPXX as select X.* from BFBHDD8.SPXX X,TRANS_USER8.SPSB B,TRANS_USER8.SPFL F,TRANS_USER8.HT H where X.SB=B.SBID and X.SPFL=F.SPFL and X.HTH=H.HTH and X.PACKED=0;
create or replace view TRANS_USER8.GTSP as select G.*,X.SPCODE,B.BMDM from BFBHDD8.GTSP G,TRANS_USER8.SPXX X,TRANS_USER8.BM B where G.SP_ID=X.SP_ID and G.DEPTID=B.DEPTID;
create or replace view TRANS_USER8.SKFS as select S.*,1 BJ_ZK,1 BJ_MBJZ from BFBHDD8.SKFS S;
create or replace view TRANS_USER8.CXHDDEF as select * from BFBHDD8.CXHDDEF;


--Sybase
grant all on BFBHDD.CRMSTAMP to TRANS_USER;
grant select on BFBHDD.BM to TRANS_USER
grant select on BFBHDD.HT to TRANS_USER
grant select on BFBHDD.WLDW to TRANS_USER
grant select on BFBHDD.SPFL to TRANS_USER
grant select on BFBHDD.SPSB to TRANS_USER
grant select on BFBHDD.SPXX to TRANS_USER
grant select on BFBHDD.GTSP to TRANS_USER
grant select on BFBHDD.SKFS to TRANS_USER
grant select on BFBHDD.CXHDDEF to TRANS_USER

create view TRANS_USER.BM as select * from BFBHDD.BM where BMDM<>''
create view TRANS_USER.HT as select HTH,B.BMDM DEPTID,GHDWDM,(case H.STATUS when 3 then 1 else 0 end) BJ_YX,H.TM,W.NAME from BFBHDD.HT H,BFBHDD.WLDW W,TRANS_USER.BM B where H.DEPTID=B.DEPTID and H.GHDWDM=W.CODE
create view TRANS_USER.SPFL as select * from BFBHDD.SPFL where SPFL<>''
create view TRANS_USER.SPSB as select * from BFBHDD.SPSB
create view TRANS_USER.SPXX as select X.* from BFBHDD.SPXX X,TRANS_USER.SPSB B,TRANS_USER.SPFL F,TRANS_USER.HT H where X.SB=B.SBID and X.SPFL=F.SPFL and X.HTH=H.HTH and X.PACKED=0
create view TRANS_USER.GTSP as select G.*,X.SPCODE,B.BMDM from BFBHDD.GTSP G,TRANS_USER.SPXX X,TRANS_USER.BM B where G.SP_ID=X.SP_ID and G.DEPTID=B.DEPTID
create view TRANS_USER.SKFS as select S.*,1 BJ_ZK,1 BJ_MBJZ from BFBHDD.SKFS S
create view TRANS_USER.CXHDDEF as select * from BFBHDD.CXHDDEF

--如果没有CRMSTAMP表
create table TRANS_USER.CRMSTAMP  (
   TBL_NAME             varchar(20)                    not null,
   STAMP_OLD            timestamp                      null,
   constraint PK_CRMSTAMP primary key (TBL_NAME)
)
