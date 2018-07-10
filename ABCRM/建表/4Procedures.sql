------------------------------------------------------
-- Export file for user BFCRM10@ZSLHCRM             --
-- Created by Administrator on 2017-11-03, 11:35:03 --
------------------------------------------------------

set define off
spool 5Procedures.log

prompt
prompt Creating function UPDATE_BHZT
prompt =============================
prompt
create or replace function bfcrm10.Update_BHZT (pTBLNAME in varchar2) return int
as
  vJLBH number(10);
  vDBID number(10);
begin
  update BFCRM10.BHZT set REC_NUM = REC_NUM+1 where TBLNAME = pTBLNAME;
  if SQL%NOTFOUND then
    insert into BFCRM10.BHZT (TBLNAME,REC_NUM) values (pTBLNAME, 1);
  end if;
  vDBID := 0;
  select to_number(CUR_VAL) into vDBID from BFCRM10.BFCONFIG where JLBH=0;
  vDBID := nvl(vDBID,0);
  select REC_NUM into vJLBH from BFCRM10.BHZT where TBLNAME=pTBLNAME;
  vJLBH := vJLBH + vDBID * 100000000;
  return vJLBH;
end;
/

prompt
prompt Creating procedure CALC_SP_FQJE_DJFQ
prompt ====================================
prompt
create or replace procedure bfcrm10.Calc_SP_FQJE_DJFQ (
                              pRclDate in date
                            )
is
  vSHDM      varchar2(4);
  vCXID    integer;
  vYHQID     integer;
  vYHQFFGZID integer;
  vFQJE      number(14,2);
  vSPID      integer;
  vMDID      integer;
  vBMDM      varchar2(10);
  vZXFJE     number(14,2);
  vINX       integer;
  vXFJLID    integer;
  vFQJE_SP   number(14,2);
  vXFJLID_OLD integer;
  vHYID       integer;

  cursor crFQ1 is
    select S.XFJLID,S.YHQID,S.CXID,YHQFFGZID, FQJE
     from BFCRM10.HYK_XFJL_FQ S,BFCRM10.HYK_XFJL H
     where S.XFJLID=H.XFJLID
       and H.CRMJZRQ =pRclDate
       and H.STATUS=1
       and H.HYID_FQ >0
       and XFLJFQFS=3
       and S.FQJE<0
       and H.DJLX in(1,2);

  cursor crSPFQ1 is
    select S.INX, FQJE
      from  BFCRM10.HYTH_DJFQ_SPFTR  S
      where S.XFJLID=vXFJLID
        and S.CXID=vCXID
        and YHQID=vYHQID
        and YHQFFGZID=vYHQFFGZID;

  /* 将单件返券中发券金额为负,单据类型为(取消钱返券)的商品返券记录插入HYTH_DJFQ_SPFTR  */
  cursor crFQ2 is
    select S.XFJLID,S.YHQID,S.CXID,YHQFFGZID, FQJE,HYID_FQ,SHDM,MDID
      from BFCRM10.HYK_XFJL_FQ S,BFCRM10.HYK_XFJL H
      where S.XFJLID=H.XFJLID
        and H.CRMJZRQ =pRclDate
        and H.STATUS=1
        and H.HYID_FQ >0
        and XFLJFQFS=3
        and S.FQJE<0
        and (H.DJLX=3);

  cursor crSPFQ2 is
    select S.XFJLID_OLD,S.INX,S.SHSPID,S.BMDM, sum(FQJE - FQJE_SJ)
      from  BFCRM10.HYTH_DJFQ_SPFTR  S
      where S.CXID=vCXID
        and YHQID=vYHQID
        and YHQFFGZID=vYHQFFGZID
        and HYID=vHYID
      group by S.XFJLID_OLD,S.INX,S.SHSPID,S.BMDM
      having  sum(FQJE - FQJE_SJ) <>0
      order by  S.XFJLID_OLD,S.INX;

  cursor Cur_YHQ is
    select SHDM,MDID,CXID,YHQID,YHQFFGZID,SHSPID,BMDM, sum(XSJE_FQ), sum(FQJE_SJ)
      from  BFCRM10.HYTH_DJFQ_SPFTR
      where CRMJZRQ=pRclDate
      group by  SHDM,MDID,CXID,YHQID,YHQFFGZID,SHSPID,BMDM;

begin

  delete from BFCRM10.HYTH_DJFQ_SPFTR  where CRMJZRQ=pRclDate;

  /* 将单件返券中发券金额为正，单据类型为销售的商品返券记录插入临时表中  */
  insert into BFCRM10.TMP_YHQFF_SPFT_SP(SHDM,MDID,CXID,INX_XFRQ,YHQID,YHQFFGZID,SHSPID,BMDM, ZXFJE, FQJE)
    select H.SHDM,H.MDID,S.CXID,XFLJFQFS,S.YHQID,YHQFFGZID,SHSPID,BMDM,sum(XSJE_FQ) ZXFJE, sum(FQJE)
      from BFCRM10.HYK_XFJL_SP_FQ S,BFCRM10.HYK_XFJL H ,BFCRM10.YHQDEF F
      where S.XFJLID=H.XFJLID
        and S.YHQID=F.YHQID
        and H.CRMJZRQ =pRclDate
        and H.STATUS=1
        and ((H.HYID_FQ >0 and F.BJ_DZYHQ=1) or (F.BJ_DZYHQ=0))
        and XFLJFQFS=3
        and FQJE>=0
        and H.DJLX=0
      group by H.SHDM,H.MDID,S.CXID,XFLJFQFS,S.YHQID,YHQFFGZID,SHSPID,BMDM;


  /* 将单件返券中发券金额为负,单据类型为(退货，换货)的商品返券记录插入HYTH_DJFQ_SPFTR  */
  insert into BFCRM10.HYTH_DJFQ_SPFTR(XFJLID,XFJLID_OLD,INX,YHQID,SHSPID,
         CXID,YHQFFGZID,XSJE_FQ,FQJE,FQJE_SJ,SHDM,MDID,HYID,CRMJZRQ,BMDM)
    select F.XFJLID,F.XFJLID,F.INX,F.YHQID,F.SHSPID,F.CXID,F.YHQFFGZID,F.XSJE_FQ,F.FQJE,0,
          X.SHDM,X.MDID,X.HYID_FQ,X.CRMJZRQ ,BMDM
      from BFCRM10.HYK_XFJL X,BFCRM10.HYK_XFJL_SP_FQ F
      where X.XFJLID=F.XFJLID
        and X.STATUS=1
        and X.HYID_FQ>0
        and F.FQJE<0
        and F.XFLJFQFS=3
        and X.CRMJZRQ=pRclDate
        and X.DJLX in (1,2);


  open crFQ1;
  fetch crFQ1 into vXFJLID,vYHQID,vCXID,vYHQFFGZID ,vFQJE;
  while (crFQ1%FOUND)
  loop
    open crSPFQ1;
    fetch crSPFQ1 into vINX,vFQJE_SP;
    while (crSPFQ1%FOUND) and (vFQJE<>0)
    loop
      if abs(vFQJE) <= abs(vFQJE_SP) then
        update BFCRM10.HYTH_DJFQ_SPFTR
           set FQJE_SJ=vFQJE
          where XFJLID=vXFJLID
            and INX=vINX
            and YHQID=vYHQID;
        vFQJE := 0;
      else
        update BFCRM10.HYTH_DJFQ_SPFTR
           set FQJE_SJ=vFQJE_SP
          where XFJLID=vXFJLID
            and INX=vINX
            and YHQID=vYHQID;
        vFQJE := vFQJE - vFQJE_SP;
      end if;
      fetch crSPFQ1 into vINX,vFQJE_SP;
    end loop;
    close crSPFQ1;
    fetch crFQ1 into vXFJLID,vYHQID,vCXID,vYHQFFGZID ,vFQJE;
  end loop;
  close crFQ1;


  /* 将单件返券中发券金额为负,单据类型为(取消钱返券)的商品返券记录插入HYTH_DJFQ_SPFTR  */

  open crFQ2;
  fetch crFQ2 into vXFJLID,vYHQID,vCXID,vYHQFFGZID ,vFQJE ,vHYID,vSHDM,vMDID;
  while (crFQ2%FOUND)
  loop
    open crSPFQ2;
    fetch crSPFQ2 into vXFJLID_OLD,vINX,vSPID,vBMDM,vFQJE_SP;
    while (crSPFQ2%FOUND) and(vFQJE<>0)
    loop
      if abs(vFQJE) <= abs(vFQJE_SP) then
        insert into BFCRM10.HYTH_DJFQ_SPFTR(XFJLID,XFJLID_OLD,INX,YHQID,SHSPID,
         CXID,YHQFFGZID,XSJE_FQ,FQJE,FQJE_SJ,SHDM,MDID,HYID,CRMJZRQ,BMDM)
        values( vXFJLID,vXFJLID_OLD,vINX,vYHQID,vSPID,vCXID,vYHQFFGZID,0,0,vFQJE,
               vSHDM,vMDID,vHYID,pRclDate,vBMDM);
        vFQJE := 0;
      else
       insert into BFCRM10.HYTH_DJFQ_SPFTR(XFJLID,XFJLID_OLD,INX,YHQID,SHSPID,
         CXID,YHQFFGZID,XSJE_FQ,FQJE,FQJE_SJ,SHDM,MDID,HYID,CRMJZRQ,BMDM)
        values( vXFJLID,vXFJLID_OLD,vINX,vYHQID,vSPID,vCXID,vYHQFFGZID,0,0,vFQJE_SP,
               vSHDM,vMDID,vHYID,pRclDate,vBMDM);
        vFQJE := vFQJE - vFQJE_SP;
      end if;
      fetch crSPFQ2 into vXFJLID_OLD,vINX,vSPID,vBMDM,vFQJE_SP;
    end loop;
    close crSPFQ2;
    fetch crFQ2 into vXFJLID,vYHQID,vCXID,vYHQFFGZID ,vFQJE ,vHYID,vSHDM,vMDID;
  end loop;
  close crFQ2;


  /*将单件返券中发券金额为负的商品返券记录插入临时表TMP_YHQFF_SPFT_SP（将HYTH_DJFQ_SPFTR 中的FQJE_SJ金额汇总）中 */
  open Cur_YHQ;
  fetch Cur_YHQ into vSHDM,vMDID,vCXID,vYHQID,vYHQFFGZID,vSPID,vBMDM,vZXFJE, vFQJE;
  while (Cur_YHQ%FOUND)
  loop
    update BFCRM10.TMP_YHQFF_SPFT_SP
       set ZXFJE=ZXFJE + vZXFJE,
           FQJE=FQJE + nvl(vFQJE,0)
     where SHDM=vSHDM and
           MDID=vMDID and
           CXID=vCXID and
           INX_XFRQ=3 and
           YHQID=vYHQID and
           YHQFFGZID=vYHQFFGZID and
           SHSPID=vSPID and
           BMDM=vBMDM;
    if SQL%NOTFOUND then
      insert into BFCRM10.TMP_YHQFF_SPFT_SP(SHDM,MDID,CXID,INX_XFRQ,YHQID,YHQFFGZID,SHSPID,BMDM, ZXFJE, FQJE)
        values(vSHDM,vMDID,vCXID,3,vYHQID,vYHQFFGZID,vSPID,vBMDM,vZXFJE, nvl(vFQJE,0));
    end if;
    fetch Cur_YHQ into vSHDM,vMDID,vCXID,vYHQID,vYHQFFGZID,vSPID,vBMDM,vZXFJE, vFQJE;
  end loop;
  close Cur_YHQ;
end;
/

prompt
prompt Creating procedure CALC_SP_FQJE_WC_WGZ
prompt ======================================
prompt
create or replace procedure bfcrm10.Calc_SP_FQJE_WC_WGZ (
                              pRclDate date
                            )
is
  vSHDM      varchar2(4);
  vCXID    integer;
  vINX_XFRQ  integer;
  vYHQID     integer;
  vYHQFFGZID integer;
  vFQJE      number(14,2);
  vREAL_FQJE number(14,2);
  vSPID      integer;
  vMDID      integer;

  cursor Cur_YHQ is
    select H.SHDM,H.MDID,S.CXID, S.YHQID, sum(S.FQJE) FQJE
      from BFCRM10.HYK_XFJL_FQ S,BFCRM10.HYK_XFJL H,BFCRM10.YHQDEF F
      where S.XFJLID=H.XFJLID
        and S.YHQID=F.YHQID
        and H.CRMJZRQ =pRclDate
        and H.STATUS=1
        and ((HYID_FQ >0 and F.BJ_DZYHQ=1) or (F.BJ_DZYHQ=0))
      group by H.SHDM,H.MDID,S.CXID,S.YHQID;
begin
    /*处理无规则的发券金额分摊*/

  open Cur_YHQ;
  fetch Cur_YHQ into vSHDM,vMDID,vCXID,vYHQID,vFQJE;
  while (Cur_YHQ%FOUND)
  loop
    select sum(FQJE) into vREAL_FQJE
      from BFCRM10.TMP_YHQFF_SPFT_SP
      where SHDM=vSHDM
        and MDID=vMDID
        and CXID=vCXID
        and YHQID=vYHQID;

    if (vREAL_FQJE<>vFQJE) then
      select max(SHSPID) into vSPID
        from BFCRM10.TMP_YHQFF_SPFT_SP
        where FQJE=(select MAX(FQJE)
                      from BFCRM10.TMP_YHQFF_SPFT_SP
                      where SHDM=vSHDM
                        and MDID=vMDID
                        and CXID=vCXID
                        and YHQID=vYHQID)
          and SHDM=vSHDM
          and MDID=vMDID
          and CXID=vCXID
          and YHQID=vYHQID;


      select max(INX_XFRQ),max(YHQFFGZID)
        into vINX_XFRQ,vYHQFFGZID
        from BFCRM10.TMP_YHQFF_SPFT_SP
        where SHDM=vSHDM
          and MDID=vMDID
          and CXID=vCXID
          and YHQID=vYHQID
          and SHSPID=vSPID;

      update BFCRM10.TMP_YHQFF_SPFT_SP
         set FQJE = FQJE + nvl((vFQJE - vREAL_FQJE),0)
        where SHDM=vSHDM
          and MDID=vMDID
          and CXID=vCXID
          and INX_XFRQ=vINX_XFRQ
          and YHQID=vYHQID
          and YHQFFGZID=vYHQFFGZID
          and SHSPID=vSPID;
    end if;

    fetch Cur_YHQ into vSHDM,vMDID,vCXID,vYHQID,vFQJE;
  end loop;
  close Cur_YHQ;

end;
/

prompt
prompt Creating procedure CALC_SP_FQJE
prompt ===============================
prompt
create or replace procedure bfcrm10.Calc_SP_FQJE (
                              pRclDate in date
                            )
is
  vSHDM      varchar2(4);
  vCXID    integer;
  vINX_XFRQ  integer;
  vYHQID     integer;
  vYHQFFGZID integer;
  vFQJE      number(14,2);
  vREAL_FQJE number(14,2);
  vSPID      integer;
  vMDID      integer;
  vBMDM      varchar2(10);
  vZXFJE     number(14,2);
  vINX       integer;
  vXFJLID    integer;
  vFQJE_SP   number(14,2);
  vXFJLID_OLD integer;
  vHYID       integer;
  vMXXSJE    number(14,2);
  vFQJE_J    number(14,2);
  vXSJE      number(14,2);
  vTMPJE     number(14,2);

  cursor Cur_YHQ is
    select H.SHDM,H.MDID,S.CXID, XFLJFQFS,S.YHQID,YHQFFGZID,sum(S.FQJE) FQJE
      from BFCRM10.HYK_XFJL_FQ S,BFCRM10.HYK_XFJL H,BFCRM10.YHQDEF F
      where S.XFJLID=H.XFJLID
        and S.YHQID=F.YHQID
        and H.CRMJZRQ =pRclDate
        and H.STATUS=1
        and not (XFLJFQFS in (0,3,4,5,6))
        and ((H.HYID_FQ >0 and F.BJ_DZYHQ=1) or (F.BJ_DZYHQ=0))
      group by H.SHDM,H.MDID,S.CXID,XFLJFQFS,S.YHQID,YHQFFGZID;

  cursor crTmpYHQ is
     select SHDM,MDID,CXID,INX_XFRQ,YHQID,YHQFFGZID,sum(FQJE)
       from  BFCRM10.TMP_YHQFF_SPFT_SP
       group by  SHDM,MDID,CXID,INX_XFRQ,YHQID,YHQFFGZID;

  cursor Cur_SPFQMX is
    select YHQID,INX,XSJE_FQ
      from BFCRM10.TMP_HYXFJL_SP_FQ_DBFT;

   cursor Cur_SPFQ is
    select A.XFJLID,A.YHQFFGZID,sum(A.FQJE)
        from BFCRM10.HYK_XFJL_FQ A,
             BFCRM10.HYK_XFJL B,
             BFCRM10.YHQDEF F
       where A.XFJLID=B.XFJLID
         and A.YHQID=F.YHQID
         and B.CRMJZRQ=pRclDate
         and B.STATUS=1
         and A.XFLJFQFS in (0,4,5,6)
         and ((B.HYID_FQ >0 and F.BJ_DZYHQ=1) or (F.BJ_DZYHQ=0))
    group by A.XFJLID,A.YHQFFGZID;
begin
  delete from BFCRM10.TMP_YHQFF_SPFT_SP;
  delete from BFCRM10.TMP_YHQFF_SPFT;
  delete from BFCRM10.YHQFF_SPFTRJL  where RQ=pRclDate;

  /*处理单笔返券的发券金额分摊 */
  open Cur_SPFQ;
  fetch Cur_SPFQ into vXFJLID,vYHQFFGZID,vFQJE;
  while (Cur_SPFQ%FOUND)
  loop
    vFQJE_J := vFQJE;
    delete from BFCRM10.TMP_HYXFJL_SP_FQ_DBFT;

    insert into BFCRM10.TMP_HYXFJL_SP_FQ_DBFT(YHQID,INX,XSJE_FQ)
    select F.YHQID,F.INX,sum(F.XSJE_FQ)
      from BFCRM10.HYK_XFJL_SP_FQ F
    where F.XFJLID=vXFJLID
      and F.XFLJFQFS in (0,4,5,6)
      and F.YHQFFGZID=vYHQFFGZID
    group by F.YHQID,F.INX;

    vXSJE := 0;
    select sum(XSJE_FQ) into vXSJE
      from TMP_HYXFJL_SP_FQ_DBFT;
    vXSJE := nvl(vXSJE,0);

    open Cur_SPFQMX;
    fetch Cur_SPFQMX into vYHQID,vINX,vMXXSJE;
    while (Cur_SPFQMX%FOUND)
    loop
      vTmpJE := round(nvl(vFQJE,0) * (nvl(vMXXSJE,0)/nvl(vXSJE,0)),2);
      update BFCRM10.HYK_XFJL_SP_FQ set FQJE=vTmpJE
       where XFJLID=vXFJLID
         and YHQID=vYHQID
         and INX=vINX
         and YHQFFGZID=vYHQFFGZID;

      vFQJE_J := round(vFQJE_J - vTmpJE,2);
      fetch Cur_SPFQMX into vYHQID,vINX,vMXXSJE;
    end loop;
    close Cur_SPFQMX;
    /*处理各规则分摊后的尾差，挤入最后一笔的发券金额*/
    if vFQJE_J<>0 then
      update BFCRM10.HYK_XFJL_SP_FQ set FQJE =FQJE + vFQJE_J
       where XFJLID=vXFJLID
         and YHQID=vYHQID
         and INX=vINX
         and YHQFFGZID=vYHQFFGZID;
    end if;

    fetch Cur_SPFQ into vXFJLID,vYHQFFGZID,vFQJE;
  end loop;
  close Cur_SPFQ;

  /*处理非单件且非单笔返券的发券金额分摊   */
  /*按商户代码，门店,促销活动编号，消费累计发券方式，优惠券，发放规则，商品，部门汇总HYK_XFJL_SP_FQ中的总销售金额*/
  insert into BFCRM10.TMP_YHQFF_SPFT_SP(SHDM,MDID,CXID,INX_XFRQ,YHQID,YHQFFGZID,SHSPID,BMDM,ZXFJE, FQJE)
    select H.SHDM,H.MDID,S.CXID,XFLJFQFS,S.YHQID,YHQFFGZID,SHSPID,BMDM,sum(XSJE_FQ) ZXFJE,0
      from BFCRM10.HYK_XFJL_SP_FQ S,BFCRM10.HYK_XFJL H ,BFCRM10.YHQDEF F
      where S.XFJLID=H.XFJLID
        and S.YHQID=F.YHQID
        and H.CRMJZRQ =pRclDate
        and H.STATUS=1
        and ((H.HYID_FQ >0 AND F.BJ_DZYHQ=1) or (F.BJ_DZYHQ=0))
        and not (XFLJFQFS in(0,3,4,5,6))
      group by H.SHDM,H.MDID,S.CXID,XFLJFQFS,S.YHQID,YHQFFGZID,SHSPID,BMDM;


  /*按商户代码，门店,促销活动编号，消费累计发券方式，优惠券，发放规则，汇总HYK_XFJL_SP_FQ中

的总销售金额*/
  insert into BFCRM10.TMP_YHQFF_SPFT
    select SHDM,MDID,CXID,INX_XFRQ,YHQID,YHQFFGZID,SUM(ZXFJE),0 ,0
      from BFCRM10.TMP_YHQFF_SPFT_SP
    group by  SHDM,MDID,CXID,INX_XFRQ,YHQID,YHQFFGZID
    having SUM(ZXFJE)<>0;

  open Cur_YHQ;
  fetch Cur_YHQ into vSHDM,vMDID,vCXID,vINX_XFRQ,vYHQID,vYHQFFGZID ,vFQJE;
  while (Cur_YHQ%FOUND)
  loop
    update BFCRM10.TMP_YHQFF_SPFT
      set FQJE=vFQJE
      where SHDM=vSHDM
        and MDID=vMDID
        and CXID=vCXID
        and INX_XFRQ=vINX_XFRQ
        and YHQID=vYHQID
        and YHQFFGZID=vYHQFFGZID;
    fetch Cur_YHQ into vSHDM,vMDID,vCXID,vINX_XFRQ,vYHQID,vYHQFFGZID ,vFQJE;
  end loop;
  close Cur_YHQ;

  /*按商户代码，门店,促销活动编号，消费累计发券方式，优惠券，发放规则汇总总销售金额，发券金额，比例*/
  update BFCRM10.TMP_YHQFF_SPFT
    --set BL=CONVERT(FLOAT,FQJE)/ZXFJE
    set BL=FQJE/ZXFJE
    where ZXFJE<>0;

  update BFCRM10.TMP_YHQFF_SPFT_SP Y
    set FQJE=nvl(round((select S.BL from BFCRM10.TMP_YHQFF_SPFT S
                          where Y.SHDM=S.SHDM
                            and Y.MDID=S.MDID
                            and Y.CXID=S.CXID
                            and Y.INX_XFRQ=S.INX_XFRQ
                            and Y.YHQID=S.YHQID
                            and Y.YHQFFGZID=S.YHQFFGZID) * Y.ZXFJE,2),0)
    where exists(
            select 1 from BFCRM10.TMP_YHQFF_SPFT S
              where Y.SHDM=S.SHDM
                and Y.MDID=S.MDID
                and Y.CXID=S.CXID
                and Y.INX_XFRQ=S.INX_XFRQ
                and Y.YHQID=S.YHQID
                and Y.YHQFFGZID=S.YHQFFGZID
                and S.ZXFJE<>0);

  /*处理各规则分摊后的尾差，顺次挤入最大的发券金额*/
  open crTmpYHQ;
  fetch crTmpYHQ into vSHDM,vMDID,vCXID,vINX_XFRQ,vYHQID,vYHQFFGZID ,vFQJE;
  while (crTmpYHQ%FOUND)
  loop
    select sum(FQJE) into vREAL_FQJE
      from BFCRM10.TMP_YHQFF_SPFT
      where SHDM=vSHDM
        and MDID=vMDID
        and CXID=vCXID
        and INX_XFRQ=vINX_XFRQ
        and YHQID=vYHQID
        and YHQFFGZID=vYHQFFGZID;

    if (vREAL_FQJE<>vFQJE) then
      select max(SHSPID) into vSPID
        from BFCRM10.TMP_YHQFF_SPFT_SP
        where FQJE=(select MAX(FQJE)
                      from BFCRM10.TMP_YHQFF_SPFT_SP
                      where SHDM=vSHDM
                        and MDID=vMDID
                        and CXID=vCXID
                        and INX_XFRQ=vINX_XFRQ
                        and YHQID=vYHQID
                        and YHQFFGZID=vYHQFFGZID);

      update BFCRM10.TMP_YHQFF_SPFT_SP
        set FQJE = FQJE + nvl((vREAL_FQJE - vFQJE),0)
        where SHDM=vSHDM
          and MDID=vMDID
          and CXID=vCXID
          and INX_XFRQ=vINX_XFRQ
          and YHQID=vYHQID
          and YHQFFGZID=vYHQFFGZID
          and SHSPID=vSPID;
    end if;

    fetch crTmpYHQ into vSHDM,vMDID,vCXID,vINX_XFRQ,vYHQID,vYHQFFGZID ,vFQJE;
  end loop;
  close crTmpYHQ;

  /*处理单件返券的发券金额分摊*/
  BFCRM10.Calc_SP_FQJE_DJFQ(pRclDate);

  /*处理无规则的发券金额分摊*/
  BFCRM10.Calc_SP_FQJE_WC_WGZ(pRclDate);

  insert into BFCRM10.TMP_YHQFF_SPFT_SP(SHDM,MDID,CXID,INX_XFRQ,YHQID,YHQFFGZID,SHSPID,BMDM,ZXFJE, FQJE)
    select H.SHDM,H.MDID,S.CXID,S.XFLJFQFS,S.YHQID,S.YHQFFGZID,S.SHSPID,S.BMDM,sum(S.XSJE_FQ) ZXFJE,sum(S.FQJE)
      from BFCRM10.HYK_XFJL_SP_FQ S,BFCRM10.HYK_XFJL H ,BFCRM10.YHQDEF F
      where S.XFJLID=H.XFJLID
        and S.YHQID=F.YHQID
        and H.CRMJZRQ =pRclDate
        and H.STATUS=1
        and ((H.HYID_FQ >0 AND F.BJ_DZYHQ=1) or (F.BJ_DZYHQ=0))
        and S.XFLJFQFS in(0,4,5,6)
   group by H.SHDM,H.MDID,S.CXID,S.XFLJFQFS,S.YHQID,S.YHQFFGZID,S.SHSPID,S.BMDM;

  insert into BFCRM10.YHQFF_SPFTRJL(RQ,SHDM,CXID,INX_XFRQ,YHQID,YHQFFGZID,SHSPID,BMDM,ZXFJE,FQJE,MDID)
    select pRclDate ,SHDM,CXID,INX_XFRQ,YHQID,YHQFFGZID,SHSPID,BMDM,ZXFJE,FQJE,MDID
      from BFCRM10.TMP_YHQFF_SPFT_SP;

  insert into BFCRM10.YHQ_CXHD_HZ(RQ,SHDM,MDID,CXID,YHQID,SHSPID,BMDM,YQJE,FQJE,ZXFJE)
    select pRclDate,L.SHDM,L.MDID,L.CXID,L.YHQID,L.SHSPID,L.BMDM,0,sum(FQJE),sum(ZXFJE)
      from BFCRM10.TMP_YHQFF_SPFT_SP  L
      group by  L.SHDM,L.MDID,L.CXID,L.YHQID,L.SHSPID,L.BMDM;
end;
/

prompt
prompt Creating procedure PRO_MOVE_XFJLHTFQ
prompt ====================================
prompt
CREATE OR REPLACE Procedure BFCRM10.PRO_MOVE_XFJLHTFQ(pRclDate in date)
is
  vXFJLID   number(10);
  vYHQID    number(10);
  cursor curFQ is
   select XFJLID,YHQID
     from BFCRM10.TMP_HYXFJL_SP_FQ_HTFQ
   order by XFJLID,YHQID;
begin

 delete from BFCRM10.TMP_HYXFJL_SP_FQ_HTFQ;

 insert into BFCRM10.TMP_HYXFJL_SP_FQ_HTFQ(XFJLID,YHQID)
 select distinct F.XFJLID,F.YHQID
   from BFCRM10.HYK_HTFQJL H,
        BFCRM10.HYK_HTFQJL_XPMX X,
        BFCRM10.HYK_XFJL_SP_FQ F,
        BFCRM10.HYK_HTFQJL_FQ Q
  where H.JLBH=X.JLBH
    and X.XFJLID=F.XFJLID
    and F.YHQID=Q.YHQID
    and H.ZXRQ=pRclDate;

 open curFQ;
 fetch curFQ into vXFJLID,vYHQID;
 while (curFQ%FOUND)
 loop
   insert into BFCRM10.HYK_XFJL_SP_FQ_DELETE(XFJLID,INX,YHQID,BMDM,SPDM,SHSPID,YHQFFDBH,
               CXID,XFLJFQFS,YHQFFGZID,XSSL,XSJE,XSJE_FQ,FQJE,FQJE_SJ,CLLX)
    select distinct F.XFJLID,F.INX,F.YHQID,F.BMDM,F.SPDM,F.SHSPID,F.YHQFFDBH,
           F.CXID,F.XFLJFQFS,F.YHQFFGZID,F.XSSL,F.XSJE,F.XSJE_FQ,F.FQJE,F.FQJE_SJ,2
      from BFCRM10.HYXFJL_SP_FQ F
     where F.XFJLID=vXFJLID
       and F.YHQID=vYHQID;

   delete from BFCRM10.HYXFJL_SP_FQ where XFJLID=vXFJLID and YHQID=vYHQID;

   fetch curFQ into vXFJLID,vYHQID;
 end loop;
 close curFQ;

 insert into BFCRM10.HYXFJL_SP_FQ
 select distinct F.*
   from BFCRM10.HYK_HTFQJL H,
        BFCRM10.HYK_HTFQJL_XPMX X,
        BFCRM10.HYK_XFJL_SP_FQ F,
        BFCRM10.HYK_HTFQJL_FQ Q
  where H.JLBH=X.JLBH
    and X.XFJLID=F.XFJLID
    and F.YHQID=Q.YHQID
    and H.ZXRQ>=pRclDate
    and H.ZXRQ<pRclDate + 1;

 delete BFCRM10.HYK_XFJL_SP_FQ F
  where exists(select 1 from BFCRM10.HYK_HTFQJL H,
         BFCRM10.HYK_HTFQJL_XPMX X,
         BFCRM10.HYK_HTFQJL_FQ Q
   where H.JLBH=X.JLBH
     and X.XFJLID=F.XFJLID
     and F.YHQID=Q.YHQID
     and H.ZXRQ>=pRclDate
     and H.ZXRQ<pRclDate + 1);

end;
/

prompt
prompt Creating procedure PRO_WRITE_XFJLHTFQ_TMP
prompt =========================================
prompt
CREATE OR REPLACE Procedure BFCRM10.PRO_Write_XFJLHTFQ_TMP (pRclDate in date)
is
  vJLBH  number(10);

  cursor curFQJL is
    select JLBH
      from BFCRM10.HYK_HTFQJL
     where ZXRQ is null;
begin

  delete from BFCRM10.TMP_HYK_XFJL_HTFQ;
  delete from BFCRM10.TMP_HYK_XFJL_SP_FQ_HTFQ;
  Open curFQJL;
  fetch curFQJL into vJLBH;
  while (curFQJL%FOUND)
  loop
    insert into BFCRM10.HYK_HTFQJL_DELETE(JLBH,MDID,XFJE,XFJE_FQ,FQJE,STATUS,DJR,DJRMC,DJSJ,ZXR,ZXRMC,ZXRQ)
      select JLBH,MDID,XFJE,XFJE_FQ,FQJE,STATUS,DJR,DJRMC,DJSJ,ZXR,ZXRMC,ZXRQ
        from BFCRM10.HYK_HTFQJL
       where JLBH=vJLBH;

    insert into BFCRM10.HYK_HTFQJL_XPMX_DELETE(JLBH,XFJLID,SKTNO,XPH,XH)
      select JLBH,XFJLID,SKTNO,XPH,XH
        from BFCRM10.HYK_HTFQJL_XPMX
       where JLBH=vJLBH;

    insert into BFCRM10.HYK_HTFQJL_FQ_DELETE(JLBH,YHQID,FQJE)
      select JLBH,YHQID,FQJE
        from BFCRM10.HYK_HTFQJL_FQ
       where JLBH=vJLBH;

    insert into BFCRM10.HYK_XFJL_FQ_ITEM_DELETE(JLBH,CXID,YHQID,YHQFFGZID,XFLJFQFS,ZXFJE,SYXFJE,FQJE,BMDM)
      select JLBH,CXID,YHQID,YHQFFGZID,XFLJFQFS,ZXFJE,SYXFJE,FQJE,BMDM
        from BFCRM10.HYK_XFJL_FQ_ITEM
       where JLBH=vJLBH;

    insert into BFCRM10.HYK_XFJL_SP_FQ_DELETE(XFJLID,INX,YHQID,BMDM,SPDM,SHSPID,YHQFFDBH,
                 CXID,XFLJFQFS,YHQFFGZID,XSSL,XSJE,XSJE_FQ,FQJE,FQJE_SJ,CLLX)
      select distinct F.XFJLID,F.INX,F.YHQID,F.BMDM,F.SPDM,F.SHSPID,F.YHQFFDBH,
             F.CXID,F.XFLJFQFS,F.YHQFFGZID,F.XSSL,F.XSJE,F.XSJE_FQ,F.FQJE,F.FQJE_SJ,1
        from BFCRM10.HYK_HTFQJL H,
             BFCRM10.HYK_HTFQJL_XPMX X,
             BFCRM10.HYK_XFJL_SP_FQ F,
             BFCRM10.HYK_HTFQJL_FQ Q
       where H.JLBH=X.JLBH
         and X.XFJLID=F.XFJLID
         and F.YHQID=Q.YHQID
         and H.JLBH=vJLBH;

    delete from BFCRM10.HYK_XFJL_SP_FQ F
     where exists(select 1 from BFCRM10.HYK_HTFQJL H,
         BFCRM10.HYK_HTFQJL_XPMX X,
         BFCRM10.HYK_HTFQJL_FQ Q
    where H.JLBH=X.JLBH
      and X.XFJLID=F.XFJLID
      and F.YHQID=Q.YHQID
      and H.JLBH=vJLBH);

    delete from BFCRM10.HYK_XFJL_FQ_ITEM  where JLBH=vJLBH;
    delete from BFCRM10.HYK_HTFQJL_FQ where JLBH=vJLBH;
    delete from BFCRM10.HYK_HTFQJL_XPMX where JLBH=vJLBH;
    delete from BFCRM10.HYK_HTFQJL  where JLBH=vJLBH;

    fetch curFQJL into vJLBH;
  end loop;
  close curFQJL;

 insert into BFCRM10.TMP_HYK_XFJL_HTFQ(XFJLID,SHDM,MDID,SKTNO,JLBH,DJLX,XFJLID_OLD,HYID,HYID_FQ,HYID_TQ,SKYDM,
         CRMJZRQ,XFSJ,JZRQ,SCSJ,JE,ZK,ZK_HY,CZJE,JF,BJ_HTBSK,XFRQ_FQ,JFBS)
 select distinct F.XFJLID,F.SHDM,F.MDID,F.SKTNO,F.JLBH,F.DJLX,F.XFJLID_OLD,F.HYID,F.HYID_FQ,F.HYID_TQ,F.SKYDM,
        pRclDate,F.XFSJ,F.JZRQ,F.SCSJ,F.JE,F.ZK,F.ZK_HY,F.CZJE,F.JF,F.BJ_HTBSK,F.XFRQ_FQ,F.JFBS
   from BFCRM10.HYK_HTFQJL H,
        BFCRM10.HYK_HTFQJL_XPMX X,
        BFCRM10.HYK_XFJL F
  where H.JLBH=X.JLBH
    and X.XFJLID=F.XFJLID
    and H.ZXRQ>=pRclDate
    and H.ZXRQ< pRclDate + 1
 union
 select distinct F.XFJLID,F.SHDM,F.MDID,F.SKTNO,F.JLBH,F.DJLX,F.XFJLID_OLD,F.HYID,F.HYID_FQ,F.HYID_TQ,F.SKYDM,
        pRclDate,F.XFSJ,F.JZRQ,F.SCSJ,F.JE,F.ZK,F.ZK_HY,F.CZJE,F.JF,F.BJ_HTBSK,F.XFRQ_FQ,F.JFBS
   from BFCRM10.HYK_HTFQJL H,
        BFCRM10.HYK_HTFQJL_XPMX X,
        BFCRM10.HYXFJL F
  where H.JLBH=X.JLBH
    and X.XFJLID=F.XFJLID
    and H.ZXRQ>=pRclDate
    and H.ZXRQ< pRclDate + 1;

insert into BFCRM10.TMP_HYK_XFJL_SP_FQ_HTFQ(XFJLID,INX,YHQID,BMDM,SPDM,SHSPID,YHQFFDBH,CXID,
            XFLJFQFS,YHQFFGZID,XSSL,XSJE,XSJE_FQ,FQJE,FQJE_SJ)
 select distinct F.XFJLID,F.INX,F.YHQID,F.BMDM,F.SPDM,F.SHSPID,F.YHQFFDBH,F.CXID,
            F.XFLJFQFS,F.YHQFFGZID,F.XSSL,F.XSJE,F.XSJE_FQ,F.FQJE,F.FQJE_SJ
   from BFCRM10.HYK_HTFQJL H,
        BFCRM10.HYK_HTFQJL_XPMX X,
        BFCRM10.HYK_XFJL_SP_FQ F,
        BFCRM10.HYK_HTFQJL_FQ Q
  where H.JLBH=X.JLBH
    and X.XFJLID=F.XFJLID
    and H.JLBH=Q.JLBH
    and F.YHQID=Q.YHQID
    and H.ZXRQ>=pRclDate
    and H.ZXRQ< pRclDate + 1;

end;
/

prompt
prompt Creating procedure CALC_SP_FQJE_HTFQ
prompt ====================================
prompt
CREATE OR REPLACE Procedure BFCRM10.Calc_SP_FQJE_HTFQ (pRclDate date)
is
  vSHDM       varchar2(10);
  vCXID       number(10);
  vINX_XFRQ   number(10);
  vYHQID      number(10);
  vYHQFFGZID  number(10);
  vFQJE       number(14,2);
  vREAL_FQJE  number(14,2);
  vSPID       number(10);
  vMDID       number(10);
  vBMDM       varchar2(20);
  vZXFJE      number(14,2);
  vINX        number(10);
  vXFJLID     number(10);
  vFQJE_SP    number(14,2);
  vXFJLID_OLD number(10);
  vHYID       number(10);
  vXSJE       number(14,2);
  vMXXSJE     number(14,2);
  vTmpJE      number(14,2);
  vFQJE_J     number(14,2);
  vBMDM_TMP   varchar2(20);
  vJLBH       number(10);

   cursor Cur_SPFQMXHT is
    select YHQID,SHSPID,XSJE_FQ
      from BFCRM10.TMP_HYXFJL_SP_FQ_DBFT_HT;

   cursor Cur_SPFQHT is
    select H.JLBH,YHQFFGZID,sum(S.ZXFJE),sum(S.FQJE)
    from BFCRM10.HYK_HTFQJL H,
         BFCRM10.HYK_XFJL_FQ_ITEM S
    where H.JLBH=S.JLBH
      and H.ZXRQ=pRclDate
      and S.XFLJFQFS in (0,4,5,6)
      and S.ZXFJE<>0
    group by H.JLBH,YHQFFGZID;

   cursor Cur_SPFQMX is
    select YHQID,SHSPID,XSJE_FQ
      from BFCRM10.TMP_HYXFJL_SP_FQ_DBFT_HT;

   cursor Cur_SPFQ is
    select XFJLID,YHQFFGZID,sum(ZXFJE),sum(FQJE)
        from BFCRM10.TMP_HYK_HTFQJL_XPMX
    group by XFJLID,YHQFFGZID;

begin

  delete from BFCRM10.TMP_YHQFF_SPFT_SP;
  delete from BFCRM10.TMP_YHQFF_SPFT;

  BFCRM10.PRO_Write_XFJLHTFQ_TMP(pRclDate);

 open Cur_SPFQHT;
 fetch Cur_SPFQHT into vJLBH,vYHQFFGZID,vXSJE,vFQJE;
 while (Cur_SPFQHT%FOUND)
 loop
    vFQJE_J := vFQJE;
    delete BFCRM10.TMP_HYXFJL_SP_FQ_DBFT_HT;

    insert into BFCRM10.TMP_HYXFJL_SP_FQ_DBFT_HT(YHQID,SHSPID,XSJE_FQ)
    select L.XFJLID,F.YHQFFGZID,sum(F.XSJE_FQ)
      from BFCRM10.TMP_HYK_XFJL_HTFQ L,
           BFCRM10.TMP_HYK_XFJL_SP_FQ_HTFQ  F,
           BFCRM10.HYK_HTFQJL_XPMX X,
           BFCRM10.HYK_HTFQJL H
     where L.XFJLID=X.XFJLID
       and F.XFJLID=L.XFJLID
       and X.JLBH=H.JLBH
       and H.JLBH=vJLBH
       and F.YHQFFGZID=vYHQFFGZID
    group by L.XFJLID,F.YHQFFGZID;

    open Cur_SPFQMXHT;
    fetch Cur_SPFQMXHT into vXFJLID,vYHQFFGZID,vMXXSJE;
    while (Cur_SPFQMXHT%FOUND)
    loop
      vTmpJE := round(nvl(vFQJE,0) * (nvl(vMXXSJE,0) / nvl(vXSJE,0)),2);

      insert into BFCRM10.TMP_HYK_HTFQJL_XPMX(XFJLID,YHQFFGZID,ZXFJE,FQJE)
       values(vXFJLID,vYHQFFGZID,vMXXSJE,vTmpJE);

      vFQJE_J := round(vFQJE_J - vTmpJE,2);

      fetch Cur_SPFQMXHT into vXFJLID,vYHQFFGZID,vMXXSJE;
    end loop;
    close Cur_SPFQMXHT;
    /*处理各规则分摊后的尾差，挤入最后一笔的发券金额*/
    if vFQJE_J<>0 then
      update BFCRM10.TMP_HYK_HTFQJL_XPMX set FQJE=FQJE + vFQJE_J
       where XFJLID=vXFJLID
        and YHQFFGZID=vYHQFFGZID;
    end if;

    fetch Cur_SPFQHT into vJLBH,vYHQFFGZID,vXSJE,vFQJE;
  end loop;
  close Cur_SPFQHT;

   /*处理单笔返券的发券金额分摊 */
  open Cur_SPFQ;
  fetch Cur_SPFQ into vXFJLID,vYHQFFGZID,vXSJE,vFQJE;
  while (Cur_SPFQ%FOUND)
  loop
    vFQJE_J := vFQJE;
    delete from BFCRM10.TMP_HYXFJL_SP_FQ_DBFT_HT;

    insert into BFCRM10.TMP_HYXFJL_SP_FQ_DBFT_HT(YHQID,SHSPID,XSJE_FQ)
    select F.YHQID,F.INX,sum(F.XSJE_FQ)
      from BFCRM10.TMP_HYK_XFJL_SP_FQ_HTFQ F,
           BFCRM10.TMP_HYK_XFJL_HTFQ L
    where F.XFJLID=vXFJLID
      and F.XFJLID=L.XFJLID
      and F.XFLJFQFS in (0,4,5,6)
      and F.YHQFFGZID=vYHQFFGZID
    group by F.YHQID,F.INX;

    open Cur_SPFQMX;
    fetch Cur_SPFQMX into vYHQID,vSPID,vMXXSJE;
    while (Cur_SPFQMX%FOUND)
    loop
      vTmpJE := round(nvl(vFQJE,0) * (nvl(vMXXSJE,0)/nvl(vXSJE,0)),2);
      update BFCRM10.TMP_HYK_XFJL_SP_FQ_HTFQ set FQJE=vTmpJE
       where XFJLID=vXFJLID
         and YHQID=vYHQID
         and INX=vSPID
         and YHQFFGZID=vYHQFFGZID;

      vFQJE_J := vFQJE_J - vTmpJE;
      fetch Cur_SPFQMX into vYHQID,vSPID,vMXXSJE;
    end loop;
    close Cur_SPFQMX;
    /*处理各规则分摊后的尾差，挤入最后一笔的发券金额*/
    if vFQJE_J<>0 then
      update BFCRM10.TMP_HYK_XFJL_SP_FQ_HTFQ set FQJE =FQJE + vFQJE_J
       where XFJLID=vXFJLID
         and YHQID=vYHQID
         and INX=vSPID;
    end if;

    fetch Cur_SPFQ into vXFJLID,vYHQFFGZID,vXSJE,vFQJE;
  end loop;
  close Cur_SPFQ;

  insert into BFCRM10.TMP_YHQFF_SPFT_SP(SHDM,MDID,CXID,INX_XFRQ,YHQID,YHQFFGZID,SHSPID,BMDM, ZXFJE, FQJE)
    select H.SHDM,H.MDID,S.CXID,XFLJFQFS,S.YHQID,YHQFFGZID,SHSPID,BMDM,sum(XSJE_FQ) ZXFJE,SUM(S.FQJE) FQJE
    from BFCRM10.TMP_HYK_XFJL_SP_FQ_HTFQ S,BFCRM10.TMP_HYK_XFJL_HTFQ H,BFCRM10.YHQDEF F
    where S.XFJLID=H.XFJLID and
          S.YHQID=F.YHQID and
          H.CRMJZRQ =pRclDate and
          S.XFLJFQFS in (0,4,5,6)
    group by H.SHDM,H.MDID,S.CXID,XFLJFQFS,S.YHQID,YHQFFGZID,SHSPID,BMDM;

    insert into BFCRM10.YHQFF_SPFTRJL(RQ,SHDM,CXID,INX_XFRQ,YHQID,YHQFFGZID,SHSPID,BMDM,ZXFJE,FQJE,MDID)
     select pRclDate ,SHDM,CXID,INX_XFRQ,YHQID,YHQFFGZID,SHSPID,BMDM,ZXFJE,FQJE,MDID
       from BFCRM10.TMP_YHQFF_SPFT_SP;

    insert into BFCRM10.YHQ_CXHD_HZ(RQ,SHDM,MDID,CXID,YHQID,SHSPID,BMDM,YQJE,FQJE,ZXFJE)
    select pRclDate,L.SHDM,L.MDID,L.CXID,L.YHQID,L.SHSPID,L.BMDM,0,sum(FQJE),sum(ZXFJE)
     from BFCRM10.TMP_YHQFF_SPFT_SP  L
     group by  L.SHDM,L.MDID,L.CXID,L.YHQID,L.SHSPID,L.BMDM;

    BFCRM10.PRO_MOVE_XFJLHTFQ(pRclDate);

end;
/

prompt
prompt Creating procedure CHECKRCLSTATUS
prompt =================================
prompt
CREATE OR REPLACE PROCEDURE BFCRM10.CHECKRCLSTATUS(vLIBID in integer,
                                                   pRclDate in date)
is
  vBJ    number(10);
begin
  vBJ := 0;
  if vLIBID=510030 then
    select count(*) into vBJ from BFCRM10.RCL where RQ=pRclDate and LIBID=510020;
    vBJ := nvl(vBJ,0);
    if vBJ=0 then
      raise_application_error(-20001,'会员卡管理模块日处理未做，不能做会员消费模块的日处理');
      return;
    end if;

    select count(*) into vBJ from BFCRM10.RCL where RQ=pRclDate and  LIBID=510020 and STATUS<>0;
    vBJ := nvl(vBJ,0);
    if vBJ<>0 then
      raise_application_error(-20001,'会员卡管理模块日处理未完成，不能做会员消费模块的日处理');
      return;
    end if;
  end if;

  if vLIBID= 510040 then
    select count(*) into vBJ from BFCRM10.RCL where RQ=pRclDate and LIBID in (510020,510030);
    vBJ := nvl(vBJ,0);
    if vBJ=0 then
      raise_application_error(-20001,'会员卡管理或会员消费模块日处理未做，不能做会员卡优惠券模块的日处理');
      return;
    end if;

    select count(*) into vBJ from BFCRM10.RCL where RQ=pRclDate and LIBID in (510020,510030) and STATUS<>0;
    vBJ := nvl(vBJ,0);
    if vBJ<>0 then
      raise_application_error(-20001,'会员卡管理或会员消费模块日处理未做，不能做会员卡优惠券模块的日处理');
      return;
    end if;
  end if;

  if vLIBID= 510200 then
    select count(*) into vBJ from BFCRM10.RCL where RQ=pRclDate and LIBID in (510020,510030,510040);
    vBJ := nvl(vBJ,0);
    if vBJ=0 then
      raise_application_error(-20001,'会员卡管理,会员消费或会员卡优惠券模块日处理未做，不能做顾客分析模块的日处理');
      return;
    end if;

    select count(*) into vBJ from BFCRM10.RCL where RQ=pRclDate and LIBID in (510020,510030,510040) and STATUS<>0;
    vBJ := nvl(vBJ,0);
    if vBJ<>0 then
      raise_application_error(-20001,'会员卡管理,会员消费或会员卡优惠券模块日处理未做，不能做顾客分析模块的日处理');
      return;
    end if;
  end if;

  if vLIBID= 510210 then
    select count(*) into vBJ from BFCRM10.RCL where RQ=pRclDate and LIBID in (510020,510030,510040,510200);
    vBJ := nvl(vBJ,0);
    if vBJ=0 then
      raise_application_error(-20001,'会员卡管理,会员消费或会员卡优惠券模块或者顾客分析模块日处理未做，不能做CRM分析模块的日处理');
      return;
    end if;

    select count(*) into vBJ from BFCRM10.RCL where RQ=pRclDate and LIBID in (510020,510030,510040,510200) and STATUS<>0;
    vBJ := nvl(vBJ,0);
    if vBJ<>0 then
      raise_application_error(-20001,'会员卡管理,会员消费或会员卡优惠券模块或者顾客分析模块日处理未做，不能做CRM分析模块的日处理');
      return;
    end if;
  end if;
end;
/

prompt
prompt Creating procedure CXMBJZDYD_ZZ
prompt ===============================
prompt
create or replace procedure bfcrm10.CXMBJZDYD_ZZ (
                              pRYID in integer,
                              pRYMC in varchar2,
                              pJLBH in integer
                            )
is
  vBMDM  varchar2(10);
  vSHDM  varchar2(4);
begin
  select SHBMDM,SHDM
    into vBMDM, vSHDM
    from BFCRM10.CXMBJZDYD
    where JLBH=pJLBH;

  update BFCRM10.CXMBJZDYD
    set ZZR=pRYID,
        ZZRMC=pRYMC,
        ZZRQ=sysdate,
        STATUS=3     --提前终止状态
    where JLBH=pJLBH;

  delete from BFCRM10.GZSJ_MBJZ where JLBH=pJLBH;
end;
/

prompt
prompt Creating procedure HYFQDYD_ZZ
prompt =============================
prompt
create or replace procedure bfcrm10.HYFQDYD_ZZ (
                              pRYID in integer,
                              pRYMC in varchar2,
                              pJLBH in integer
                            )
is
  vBMDM varchar2(10);
  vSHDM varchar2(4);
  vYHQID integer;
begin
  select SHBMDM,SHDM, YHQID
    into vBMDM, vSHDM,vYHQID
    from BFCRM10.HYKFQDYD
    where JLBH=pJLBH;

  update BFCRM10.HYKFQDYD
    set ZZR=pRYID,ZZRMC=pRYMC,ZZRQ=sysdate,STATUS=3     --提前终止状态
    where JLBH=pJLBH;
  delete from BFCRM10.GZSJ_HYFQ where JLBH=pJLBH;
end;
/

prompt
prompt Creating procedure HYJFDYD_ZZ
prompt =============================
prompt
create or replace procedure bfcrm10.HYJFDYD_ZZ (
                              pRYID in integer,
                              pRYMC in varchar2,
                              pJLBH in integer
                            )
is
  vBMDM varchar2(10);
  vSHDM varchar2(4);
  vHYKTYPE integer;
begin
  select SHBMDM,SHDM, HYKTYPE
    into vBMDM, vSHDM,vHYKTYPE
    from BFCRM10.HYKJFDYD
    where JLBH=pJLBH;

  update BFCRM10.HYKJFDYD
    set ZZR=pRYID,ZZRMC=pRYMC,ZZRQ=sysdate,STATUS=3     --提前终止状态
    where JLBH=pJLBH;

  delete from BFCRM10.GZSJ_HYJF where JLBH=pJLBH;
end;
/

prompt
prompt Creating procedure HYKGL_ZX_HYK_JGDJGZ
prompt ======================================
prompt
create or replace procedure bfcrm10.HYKGL_ZX_HYK_JGDJGZ (
                              pRCLRQ in date
                            )
is
  vJLBH         integer;
  vHYKTYPE      number(5);
  vJGID         number(5);
  vFS           number(5);
  vQDMTXFCS     integer;
  vZZMTXFCS     integer;
  vQDMTTHCS     integer;
  vZZMTTHCS     integer;
  vLastDD30     date;
  vJGID_OLD     number(5);
  vHYID         integer;
  vXFCS30       float;
  vTHCS30       float;
  vCount        integer;

  cursor Cur_HYK_HYXX is
      select HYID,JGID
        from BFCRM10.HYK_HYXX
        where HYKTYPE=vHYKTYPE
          and STATUS>=0   --有效卡
          and JGID<vJGID;  --JGID较大者警告较严重

  cursor Cur_HYK_JGDJGZ is
      select J.HYKTYPE,J.JGID,J.FS,J.QDMTXFCS,J.ZZMTXFCS,J.QDMTTHCS,J.ZZMTTHCS
        from BFCRM10.HYK_JGDJGZ J,BFCRM10.HYKDEF H
        where H.HYKTYPE=J.HYKTYPE
          and H.BJ_CZK=0
        order by J.HYKTYPE,J.JGID  DESC;  --JGID较大者警告较严重
begin
  vLastDD30 := trunc(sysdate)-30;

  open Cur_HYK_JGDJGZ;
  fetch Cur_HYK_JGDJGZ into vHYKTYPE,vJGID,vFS,vQDMTXFCS,vZZMTXFCS,vQDMTTHCS,vZZMTTHCS;
  while (Cur_HYK_JGDJGZ%FOUND)
  loop
    vJLBH := BFCRM10.Update_BHZT('HYK_JGDJBDJL');

    insert into BFCRM10.HYK_JGDJBDJL(JLBH,RQ,HYKTYPE,JGID,FS,QDMTXFCS,ZZMTXFCS,QDMTTHCS,ZZMTTHCS)
      values(vJLBH,pRCLRQ,vHYKTYPE,vJGID,vFS,vQDMTXFCS,vZZMTXFCS,vQDMTTHCS,vZZMTTHCS);

    open Cur_HYK_HYXX;
    fetch Cur_HYK_HYXX into vHYID,vJGID_OLD;
    while (Cur_HYK_HYXX%FOUND)
    loop
      vXFCS30 := 0;
      select count(*) into vXFCS30      --=sum(1)
        from  BFCRM10.HYXFJL
        where XFSJ>=vLastDD30
          and HYID=vHYID
          and JE>0;

        --select vXFCS30 := nvl(vXFCS30,0)
        vXFCS30 := round(vXFCS30/30,5);

        vTHCS30 := 0;
        --select vTHCS30=sum(1) from   BFCRM10.HYXFJL where XFSJ>=vLastDD30 and HYID=vHYID and JE<0
        --select vTHCS30 := nvl(vXFCS30,0)
        select count(*) into vTHCS30
          from BFCRM10.HYXFJL
          where XFSJ>=vLastDD30
            and HYID=vHYID
            and JE<0;

        vTHCS30 := round(vTHCS30/30,5);

        if (vFS=0) --每天消费次数且每天退货次数
           and (vXFCS30>=vQDMTXFCS)  and (vXFCS30<=vZZMTXFCS)
           and (vTHCS30>=vQDMTTHCS)  and (vTHCS30<=vZZMTTHCS) then

          insert into BFCRM10.HYK_JGDJBDJLITEM(JLBH,HYID,JGID_OLD,JGID_NEW)
            values(vJLBH,vHYID,vJGID_OLD,vJGID);

          update BFCRM10.HYK_HYXX
            set JGID=vJGID
            where HYID=vHYID;
        end if;

        if (vFS=1)  --每天消费次数或每天退货次数
           and ( (vXFCS30>=vQDMTXFCS)  and (vXFCS30<=vZZMTXFCS)
               or (vTHCS30>=vQDMTTHCS) and (vTHCS30<=vZZMTTHCS)) then
          insert into BFCRM10.HYK_JGDJBDJLITEM(JLBH,HYID,JGID_OLD,JGID_NEW)
            values(vJLBH,vHYID,vJGID_OLD,vJGID);

          update BFCRM10.HYK_HYXX
            set JGID=vJGID
            where HYID=vHYID;
        end if;

        fetch Cur_HYK_HYXX into vHYID,vJGID_OLD;
     end loop;
     close Cur_HYK_HYXX;

     select count(*) into vCount from BFCRM10.HYK_JGDJBDJLITEM where JLBH=vJLBH;
     if vCount=0 then
       delete from BFCRM10.HYK_JGDJBDJL  where JLBH=vJLBH;
     end if;

   fetch Cur_HYK_JGDJGZ into vHYKTYPE,vJGID,vFS,vQDMTXFCS,vZZMTXFCS,vQDMTTHCS,vZZMTTHCS;
  end loop;
  close Cur_HYK_JGDJGZ;
end;
/

prompt
prompt Creating procedure HYKGL_ZX_HYK_SMZTGZ
prompt ======================================
prompt
create or replace procedure bfcrm10.HYKGL_ZX_HYK_SMZTGZ (
                              pRCLRQ in date
                            )
is
  vJLBH     integer;
  vHYKTYPE  number(5);
  vFS       number(5);
  vQDZJXFTS number(5);
  vZZZJXFTS number(5);
  vQDLJXFJE number(14,2);
  vZZLJXFJE number(14,2);
  vQDCZYE   number(14,2);
  vZZCZYE   number(14,2);
  vRQ1      date;
  vRQ2      date;
  vCount    integer;

  cursor Cur_HYK_SMZTGZ is
      select S.HYKTYPE,S.FS,S.QDZJXFTS,S.ZZZJXFTS,S.QDLJXFJE,S.ZZLJXFJE,S.QDCZYE,S.ZZCZYE
        from BFCRM10.HYK_SMZTGZ S,BFCRM10.HYKDEF H
        where H.BJ_CZK=0
          and S.HYKTYPE=H.HYKTYPE
        order by S.HYKTYPE;
begin
  open Cur_HYK_SMZTGZ;
  fetch Cur_HYK_SMZTGZ into  vHYKTYPE,vFS,vQDZJXFTS,vZZZJXFTS,vQDLJXFJE,vZZLJXFJE,vQDCZYE,vZZCZYE;
  while (Cur_HYK_SMZTGZ%FOUND)
  loop
    vJLBH := BFCRM10.Update_BHZT('HYK_SMZTBDJL');

    insert into BFCRM10.HYK_SMZTBDJL(JLBH,RQ,HYKTYPE,FS,QDZJXFTS,ZZZJXFTS,QDLJXFJE,ZZLJXFJE,QDCZYE,ZZCZYE)
      values(vJLBH,pRCLRQ,vHYKTYPE,vFS,vQDZJXFTS,vZZZJXFTS,vQDLJXFJE,vZZLJXFJE,vQDCZYE,vZZCZYE);

    vRQ1 := sysdate-vZZZJXFTS;
    vRQ2 := sysdate-vQDZJXFTS;
    case vFS
      when 0 then --消费时间且消费金额且储值余额
      begin
        insert into BFCRM10.HYK_SMZTBDJLITEM(JLBH,HYID,STATUS_OLD)
          select vJLBH,A.HYID,A.STATUS
            from BFCRM10.HYK_HYXX A,BFCRM10.HYK_JFZH B,BFCRM10.HYK_JEZH C
            where A.HYID=B.HYID and A.HYID=C.HYID
              and A.HYKTYPE=vHYKTYPE
              and A.STATUS>=0   --有效卡
              and A.STATUS<>3
              --and DATEDIFF(day,A.ZHXFRQ,sysdate) >=vQDZJXFTS  and  DATEDIFF(day,A.ZHXFRQ,sysdate) <=vZZZJXFTS
              and A.ZHXFRQ>=vRQ1 and A.ZHXFRQ<=vRQ2
              and B.LJXFJE>=vQDLJXFJE    and B.LJXFJE<=vZZLJXFJE
              and C.YE>=vQDCZYE          and C.YE<=vZZCZYE;
      end;

      when 1 then --消费时间或消费金额或储值余额
      begin
        insert into BFCRM10.HYK_SMZTBDJLITEM(JLBH,HYID,STATUS_OLD)
          select vJLBH,A.HYID,A.STATUS
            from BFCRM10.HYK_HYXX A,BFCRM10.HYK_JFZH B,BFCRM10.HYK_JEZH C
            where A.HYID=B.HYID and A.HYID=C.HYID
              and A.HYKTYPE=vHYKTYPE
              and A.STATUS>=0   --有效卡
              and A.STATUS<>3
              and (
                      --(DATEDIFF(day,A.ZHXFRQ,sysdate) >=vQDZJXFTS  and  DATEDIFF(day,A.ZHXFRQ,sysdate) <=vZZZJXFTS)
                      (A.ZHXFRQ>=vRQ1 and A.ZHXFRQ<=vRQ2)
                 or   (B.LJXFJE>=vQDLJXFJE    and B.LJXFJE<=vZZLJXFJE)
                 or   (C.YE>=vQDCZYE          and C.YE<=vZZCZYE)
               );
      end;

      when 2 then --消费时间且(消费金额或储值余额)
      begin
        insert into BFCRM10.HYK_SMZTBDJLITEM(JLBH,HYID,STATUS_OLD)
          select vJLBH,A.HYID,A.STATUS
            from BFCRM10.HYK_HYXX A,BFCRM10.HYK_JFZH B,BFCRM10.HYK_JEZH C
            where A.HYID=B.HYID and A.HYID=C.HYID
              and A.HYKTYPE=vHYKTYPE
              and A.STATUS>=0   --有效卡
              and A.STATUS<>3
              and (
                      --(DATEDIFF(day,A.ZHXFRQ,sysdate) >=vQDZJXFTS  and  DATEDIFF(day,A.ZHXFRQ,sysdate) <=vZZZJXFTS)
                      A.ZHXFRQ>=vRQ1 and A.ZHXFRQ<=vRQ2
                 and  ((B.LJXFJE>=vQDLJXFJE    and B.LJXFJE<=vZZLJXFJE)
                    or (C.YE>=vQDCZYE          and C.YE<=vZZCZYE))
               );
      end;

      when 3 then --(消费时间或消费金额)且储值余额
      begin
        insert into BFCRM10.HYK_SMZTBDJLITEM(JLBH,HYID,STATUS_OLD)
          select vJLBH,A.HYID,A.STATUS
            from BFCRM10.HYK_HYXX A,BFCRM10.HYK_JFZH B,BFCRM10.HYK_JEZH C
            where A.HYID=B.HYID and A.HYID=C.HYID
              and A.HYKTYPE=vHYKTYPE
              and A.STATUS>=0   --有效卡
              and A.STATUS<>3
              and (
                 ( --(DATEDIFF(day,A.ZHXFRQ,sysdate) >=vQDZJXFTS  and  DATEDIFF(day,A.ZHXFRQ,sysdate) <=vZZZJXFTS)
                   (A.ZHXFRQ>=vRQ1 and A.ZHXFRQ<=vRQ2)
                   or  (B.LJXFJE>=vQDLJXFJE    and B.LJXFJE<=vZZLJXFJE)
                 )
                 and (C.YE>=vQDCZYE          and C.YE<=vZZCZYE)
               );
      end;

      when 4 then  --(消费时间或储值余额)且消费金额
      begin
        insert into BFCRM10.HYK_SMZTBDJLITEM(JLBH,HYID,STATUS_OLD)
          select vJLBH,A.HYID,A.STATUS
            from BFCRM10.HYK_HYXX A,BFCRM10.HYK_JFZH B,BFCRM10.HYK_JEZH C
            where A.HYID=B.HYID and A.HYID=C.HYID
              and A.HYKTYPE=vHYKTYPE
              and A.STATUS>=0   --有效卡
              and A.STATUS<>3
              and (
                    (--(DATEDIFF(day,A.ZHXFRQ,sysdate) >=vQDZJXFTS  and  DATEDIFF(day,A.ZHXFRQ,sysdate) <=vZZZJXFTS)
                     (A.ZHXFRQ>=vRQ1 and A.ZHXFRQ<=vRQ2)
                 or (C.YE>=vQDCZYE          and C.YE<=vZZCZYE))
               and    (B.LJXFJE>=vQDLJXFJE    and B.LJXFJE<=vZZLJXFJE)
               );
      end;
    end case;

    select count(*) into vCount from BFCRM10.HYK_SMZTBDJLITEM where JLBH=vJLBH;
    if vCount=0 then
      delete from BFCRM10.HYK_SMZTBDJL  where JLBH=vJLBH;
    else
      update BFCRM10.HYK_HYXX
        set STATUS=3
        where HYID in (select HYID from BFCRM10.HYK_SMZTBDJLITEM where JLBH=vJLBH);
    end if;

    fetch Cur_HYK_SMZTGZ into  vHYKTYPE,vFS,vQDZJXFTS,vZZZJXFTS,vQDLJXFJE,vZZLJXFJE,vQDCZYE,vZZCZYE;
  end loop;
  close Cur_HYK_SMZTGZ;
end;
/

prompt
prompt Creating procedure HYKGL_ZX_HYK_YXQDQDJ
prompt =======================================
prompt
create or replace procedure bfcrm10.HYKGL_ZX_HYK_YXQDQDJ (
                              pRCLRQ in date
                            )
is
  vJLBH     integer;
  vDJRMC    varchar2(16);
  vDJR      integer;
  vSL       integer;
begin
  select min(PERSON_ID) into vDJR from BFCRM10.RYXX;
  select PERSON_NAME into vDJRMC from BFCRM10.RYXX  where PERSON_ID=vDJR;

  vSL := 0;
  select count(*) into vSL
    from BFCRM10.HYK_HYXX H, BFCRM10.HYKDEF K
    where H.HYKTYPE=K.HYKTYPE
      and YXQ<= pRCLRQ
      and STATUS>=0
      and K.BJ_CZK=1;

  if vSL > 0 then
    vJLBH := BFCRM10.Update_BHZT('HYK_ZTBDJL');

    insert into BFCRM10.HYK_ZTBDJL(JLBH,NEW_STATUS,ZY,DJR,DJRMC,DJSJ,ZXR,ZXRMC,ZXRQ)
      values(vJLBH,-4,'有效期到期',vDJR,vDJRMC,pRCLRQ,vDJR,vDJRMC,pRCLRQ);

    insert into BFCRM10.HYK_ZTBDJLITEM(JLBH,HYID,OLD_STATUS)
      select vJLBH,HYID,STATUS
        from BFCRM10.HYK_HYXX H,
             BFCRM10.HYKDEF K
        where H.HYKTYPE=K.HYKTYPE
          and YXQ<= pRCLRQ
          and STATUS>=0
          and K.BJ_CZK=1;

    update BFCRM10.HYK_HYXX
       set STATUS=-4
      where HYKTYPE in (select HYKTYPE from BFCRM10.HYKDEF where BJ_CZK=1)
        and YXQ<= pRCLRQ
        and STATUS>=0;
  end if;

  select count(*) into vSL
    from BFCRM10.HYK_HYXX H ,
         BFCRM10.HYKDEF K
    where H.HYKTYPE=K.HYKTYPE
      and YXQ<= pRCLRQ
      and STATUS>=0
      and K.BJ_CZK = 0;

  if vSL <=0 then
    return;
  end if;

  vJLBH := BFCRM10.Update_BHZT('HYK_ZTBDJL');

  insert into BFCRM10.HYK_ZTBDJL(JLBH,NEW_STATUS,ZY,DJR,DJRMC,DJSJ,ZXR,ZXRMC,ZXRQ)
     values(vJLBH,5,'有效期到期休眠',vDJR,vDJRMC,pRCLRQ,vDJR,vDJRMC,pRCLRQ);

  insert into BFCRM10.HYK_ZTBDJLITEM(JLBH,HYID,OLD_STATUS)
    select vJLBH,HYID,STATUS
      from BFCRM10.HYK_HYXX H,
           BFCRM10.HYKDEF K
      where H.HYKTYPE=K.HYKTYPE and
            YXQ<= pRCLRQ and
            STATUS>=0  and
            K.BJ_CZK=0;

 -- insert into BFCRM10.HYK_JFBDJLMX(HYID,RQ,CLLX,JLBH,WCLJFBD,BQJFBD,BNLJJFBD)
    --select H.HYID,pRCLRQ,6,vJLBH,0,H.BQJF*(-1) as BQJF,0
     -- from BFCRM10.HYK_JFZH H,
       --    BFCRM10.HYK_HYXX X,
         --  BFCRM10.HYKDEF K
      --where H.HYID=X.HYID and
        --    X.HYKTYPE=K.HYKTYPE and
          --  X.YXQ<= pRCLRQ and
            --X.STATUS>=0  and
            --K.BJ_CZK=0;

  update BFCRM10.HYK_HYXX
    set STATUS=5
    where HYKTYPE in (select HYKTYPE from BFCRM10.HYKDEF where BJ_CZK=0)
      and YXQ<= pRCLRQ
      and STATUS>=0;

  update BFCRM10.HYK_JFZH
     set BQJF=0
    where HYID in (select X.HYID
                    from BFCRM10.HYK_HYXX X,BFCRM10.HYKDEF K
                    where X.HYKTYPE=K.HYKTYPE
                      and X.YXQ<= pRCLRQ
                      and X.STATUS=5
                      and K.BJ_CZK=0);

  update BFCRM10.HYK_MDJF
    set  BQJF=0
    where HYID in (select X.HYID
                    from BFCRM10.HYK_HYXX X,BFCRM10.HYKDEF K
                    where X.HYKTYPE=K.HYKTYPE
                      and X.YXQ<= pRCLRQ
                      and X.STATUS=5
                      and K.BJ_CZK=0);

  update BFCRM10.HYK_HYXX
     set STATUS=-1
   where HYKTYPE in (select HYKTYPE from BFCRM10.HYKDEF where BJ_CZK=0)
     and STATUS=5
     and YXQ<=pRCLRQ-3;
end;
/

prompt
prompt Creating procedure HYK_CHECKXP
prompt ==============================
prompt
create or replace procedure bfcrm10.HYK_CHECKXP (
                              pRclDate in date
                            )
is
  vTS         integer;
  vPreDay     date;
  vNextDay    date;
  --vHYID       integer;
  vDJRMC      varchar2(16);
  vDJR        integer;
  vJLBH       integer;
  vBJ         integer;
  vCount      integer;
begin
  select min(PERSON_ID) into vDJR from BFCRM10.RYXX;
  select PERSON_NAME into vDJRMC from BFCRM10.RYXX  where PERSON_ID=vDJR;

  select count(*) into vCount from BFCRM10.BFCONFIG where JLBH=520000115;
  if vCount=0 then
      raise_application_error(-20001,'没有设置会员卡未录入小票天数选择');
      return;
  end if;

  select to_number(CUR_VAL) into vTS
    from BFCRM10.BFCONFIG
    where JLBH=520000115;

  if vTS>0 then
    vPreDay := pRclDate - vTS;
    vNextDay:= vPreDay+2;

    for item in (
         select HYID
           from BFCRM10.HYK_HYXX X,BFCRM10.HYKDEF F
           where X.HYKTYPE=F.HYKTYPE
             and F.BJ_CZK=0
             and X.STATUS>=0
             and X.FFSX=0
             and X.JKRQ >vPreDay
             and X.JKRQ <vNextDay
           order by HYID)
    loop
      vBJ := 1;
      select count(*) into vCount from BFCRM10.HYXLKHGH where HYID=item.HYID and ZXRQ is not null;
      if vCount>0 then
        vBJ := 0;
      else
        select count(*) into vCount from BFCRM10.HYKSBXPJL where HYID=item.HYID;
        if vCount>0 then
          vBJ := 0;
        end if;
      end if;

      if vBJ >=1 then
        vJLBH := BFCRM10.Update_BHZT('HYK_ZTBDJL');

        insert into BFCRM10.HYK_ZTBDJL(JLBH,NEW_STATUS,ZY,DJR,DJRMC,DJSJ,ZXR,ZXRMC,ZXRQ)
            values(vJLBH,-4,'未录入小票',vDJR,vDJRMC,pRclDate,vDJR,vDJRMC,pRclDate);

        insert into BFCRM10.HYK_ZTBDJLITEM(JLBH,HYID,OLD_STATUS)
          select vJLBH,HYID,STATUS
            from BFCRM10.HYK_HYXX
            where HYID=item.HYID;

        update BFCRM10.HYK_HYXX
          set STATUS=-4
          where HYID=item.HYID;
      end if;
    end loop;
  end if;
end;
/

prompt
prompt Creating procedure HYK_HSK
prompt ==========================
prompt
create or replace procedure bfcrm10.HYK_HSK (
                              pCDNR in varchar2,
                              pCZY  in integer,
                              vFS   in integer,
                              pRetStr out varchar2,
                              pRetFlag out Integer
                            )
is
  vCZKHM       varchar2(16);
  vBGDDDM      varchar2(10);
  vHYKTYPE     integer;
  vHYID        integer;
  vCZYNAME     varchar2(16);
  vTKJLBH      integer;
  vKF          number(14,2);
  vYXQ         date;
  vNEW_HYK_NO  varchar2(16);
  vZMJE        number(14,2);
  vTKJE        number(14,2);
  vBJ_CZK      integer;
begin
  pRetFlag := 1;
  vCZKHM   := null;

  begin
    select A.HYK_NO,A.YBGDD,A.HYKTYPE,A.HYID,nvl(B.KFJE,0),BJ_CZK, A.YXQ
      into vCZKHM,  vBGDDDM,vHYKTYPE, vHYID, vKF,          vBJ_CZK,vYXQ
      from BFCRM10.HYK_HYXX A ,BFCRM10.HYKDEF B
      where A.HYKTYPE=B.HYKTYPE
        and A.CDNR=pCDNR
        and A.STATUS>=0
        and B.TKBJ=1;
  exception
    when others then
      pRetStr  := '此卡不是有效的可退顾客卡，不能回收!';
      pRetFlag := 1;
      return;
    end;

  if vFS=2 then
    insert into BFCRM10.HYKCARD(CZKHM,CDNR,HYKTYPE,QCYE,YXTZJE,PDJE,BGDDDM,BGR,JKFS,SKJLBH,STATUS,YXQ,XKRQ)
       values (vCZKHM,pCDNR,vHYKTYPE,0,0,0,vBGDDDM,pCZY,2,null,1,vYXQ,sysdate);

    --vHYLEN := length(rtrim(to_char(vHYID)));

    vNEW_HYK_NO := LPAD(to_char(vHYID),15,'0')||'X';

    update BFCRM10.HYK_HYXX
      set OLD_HYKNO=HYK_NO,
          HYK_NO=vNEW_HYK_NO,
          CDNR=vNEW_HYK_NO,
          STATUS=-2
      where HYID=vHYID;
  else
    update BFCRM10.HYK_HYXX set STATUS=-2 where  HYID=vHYID;
  end if;

  select PERSON_NAME into vCZYNAME
    from BFCRM10.RYXX
    where PERSON_ID=pCZY;

  vTKJE := 0;
  vZMJE := 0;
  vKF := 0;
  if vBJ_CZK=1 then
    select PDJE,YE
      into vTKJE,vZMJE
      from BFCRM10.HYK_JEZH
      where HYID=vHYID;

    if vTKJE >vZMJE then
      pRetStr := '余额小于铺底金额，不能退卡!';
      pRetFlag := 1;
      return;
    end if;

    if vTKJE<>0 then
      update BFCRM10.HYK_JEZH
        set YE=YE - vTKJE
        where HYID=vHYID;

      insert into BFCRM10.HYK_JEZCLJL(HYID,CLSJ,CLLX,JLBH,ZY,JFJE,DFJE,YE)
        values(vHYID,sysdate,6,vTKJLBH,'后台退卡',0,vTKJE,vZMJE-vTKJE);
    end if;
  end if;

  vTKJLBH := BFCRM10.Update_BHZT('HYK_TK');

  insert into BFCRM10.HYK_TK(JLBH,HYKTYPE,ZY,TKSL,JE,TKJE,TKFS,DJR,DJRMC,DJSJ,ZXR,ZXRMC,ZXRQ)
    values(vTKJLBH,vHYKTYPE,'顾客卡回收',1,vZMJE,vTKJE,0,pCZY,vCZYNAME,sysdate,pCZY,vCZYNAME,trunc(sysdate));

  insert into BFCRM10.HYK_TK_ITEM(JLBH,HYID,INX,JE,ZKL,TKJE,KFJE)
    values(vTKJLBH,vHYID,1,vZMJE,1,vTKJE,vKF);

  pRetFlag := 0;
  pRetStr  := '';
end;
/

prompt
prompt Creating procedure HYK_HSK_HT
prompt =============================
prompt
create or replace procedure bfcrm10.HYK_HSK_HT (
                              pCDNR in varchar2,
                              pCZY  in integer,
                              vFS   in integer,
                              pRetStr out varchar2,
                         pRetFlag out integer
                            )
is
  vCZKHM       varchar2(16);
  vBGDDDM      varchar2(10);
  vHYKTYPE     integer;
  vHYID        integer;
  vCZYNAME     varchar2(16);
  vTKJLBH      integer;
  vKF          number(14,2);
  vYXQ         date;
  vNEW_HYK_NO  varchar2(16);
  --vHYLEN     integer;
  vZMJE        number(14,2);
  vTKJE        number(14,2);
  vBJ_CZK      integer;
begin

  pRetFlag := 1;
  vCZKHM   := null;

  begin
    select A.HYK_NO,A.YBGDD,A.HYKTYPE,A.HYID,nvl(B.KFJE,0),BJ_CZK,A.YXQ
      into vCZKHM,  vBGDDDM,vHYKTYPE, vHYID, vKF,          vBJ_CZK,vYXQ
      from BFCRM10.HYK_HYXX A ,BFCRM10.HYKDEF B
      where A.HYKTYPE=B.HYKTYPE
        and A.CDNR=pCDNR
        --and A.STATUS>=0
        and B.TKBJ=1;
  exception
    when others then
      pRetStr := '此卡不是有效的可退顾客卡，不能回收!';
      pRetFlag := 1;
      return;
  end;

  if vFS=2 then
    insert into BFCRM10.HYKCARD(CZKHM,CDNR,HYKTYPE,QCYE,YXTZJE,PDJE,BGDDDM,BGR,JKFS,SKJLBH,STATUS,YXQ,XKRQ)
      values (vCZKHM,pCDNR,vHYKTYPE,0,0,0,vBGDDDM,pCZY,2,null,1,vYXQ,sysdate);
    --select vHYLEN = length(rtrim(convert(varchar2(15),vHYID)))

    vNEW_HYK_NO := LPAD(to_char(vHYID),15,'0')||'X';

    update BFCRM10.HYK_HYXX
      set OLD_HYKNO=HYK_NO,
          HYK_NO=vNEW_HYK_NO,
          CDNR=vNEW_HYK_NO,
          STATUS=-1
      where HYID=vHYID;
  else
    update BFCRM10.HYK_HYXX set STATUS=-1 where  HYID=vHYID;
  end if;

  select PERSON_NAME into vCZYNAME
    from BFCRM10.RYXX
    where PERSON_ID=pCZY;

  vTKJE := 0;
  vZMJE := 0;
  vKF   := 0;
  if vBJ_CZK=1 then
    select PDJE,YE
      into vTKJE,vZMJE
      from BFCRM10.HYK_JEZH
      where HYID=vHYID;
    if vTKJE >vZMJE then
      pRetStr := '余额小于铺底金额，不能退卡!';
      pRetFlag := 1;
      return;
    end if;
    if vTKJE<>0 then
      update BFCRM10.HYK_JEZH
        set YE=YE - vTKJE
        where HYID=vHYID;

      insert into BFCRM10.HYK_JEZCLJL(HYID,CLSJ,CLLX,JLBH,ZY,JFJE,DFJE,YE)
        values(vHYID,sysdate,6,vTKJLBH,'后台退卡',0,vTKJE,vZMJE-vTKJE);
    end if;
  end if;

  pRetFlag := 0;
  pRetStr  := '';
end;
/

prompt
prompt Creating procedure HYK_MOVE_HYXF_BSK
prompt ====================================
prompt
create or replace procedure bfcrm10.HYK_MOVE_HYXF_BSK (
                              pRCLRQ in date
                            )
is
  vNextDay date;
  vSKTNO varchar2(10);
  vXPH  integer;
  vSL   integer;

  cursor CurBSKJL is
    select SKTNO,XPH
      from BFCRM10.CRMBSKJL
      where DJSJ>=pRCLRQ
        and DJSJ<vNextDay;
begin

  vNextDay := pRCLRQ+1;

  open CurBSKJL;
  fetch CurBSKJL into vSKTNO,vXPH;
  while (CurBSKJL%FOUND)
  loop
    /*移补刷卡历史记录到消费记录*/
    vSL := 0;

    select count(*) into vSL
      from BFCRM10.HYK_XFJL
      where SKTNO=vSKTNO
        and JLBH=vXPH
        and BJ_HTBSK=1;

    if vSL=0 then
      insert into BFCRM10.HYK_XFJL(XFJLID,SHDM,MDID,SKTNO,JLBH,DJLX,XFJLID_OLD,HYID,HYID_FQ,HYID_TQ,SKYDM,XFSJ,JZRQ,CRMJZRQ,SCSJ,JE,ZK,ZK_HY,CZJE,JF,BJ_HTBSK,XFRQ_FQ,JFBS,STATUS)
        select XFJLID,SHDM,MDID,SKTNO,JLBH,DJLX,XFJLID_OLD,HYID,HYID_FQ,HYID_TQ,SKYDM,XFSJ,JZRQ,pRCLRQ,SCSJ,JE,ZK,ZK_HY,CZJE,JF,BJ_HTBSK,XFRQ_FQ,JFBS,1
          from  BFCRM10.HYXFJL
          where SKTNO=vSKTNO
            and JLBH=vXPH
            and BJ_HTBSK=1;

      insert into BFCRM10.HYK_XFJL_SP(XFJLID,INX,SHSPID,BMDM,SPDM,XSSL,XSJE,ZKJE,ZKJE_HY,THJE,BJ_JF,XSJE_JF,XSJE_FQ,JFDYDBH,JF,JFJS,ZKL,ZKDYDBH,HTH,BJ_JFBS)
        select A.XFJLID,A.INX,A.SHSPID,A.BMDM,A.SPDM,A.XSSL,A.XSJE,A.ZKJE,A.ZKJE_HY,A.THJE,A.BJ_JF,A.XSJE_JF,
               A.XSJE_FQ,A.JFDYDBH,A.JF,A.JFJS,A.ZKL,A.ZKDYDBH,A.HTH,A.BJ_JFBS
          from BFCRM10.HYXFJL_SP A,BFCRM10.HYXFJL B
          where A.XFJLID=B.XFJLID
            and B.SKTNO=vSKTNO
            and B.JLBH=vXPH
            and B.BJ_HTBSK=1;

      /*删除补刷卡历史记录*/
      delete from BFCRM10.HYXFJL_SP
        where XFJLID in (select XFJLID
                           from BFCRM10.HYXFJL
                           where SKTNO=vSKTNO
                             and JLBH=vXPH
                             and BJ_HTBSK=1);

      delete from BFCRM10.HYXFJL
        where SKTNO=vSKTNO
          and JLBH=vXPH
          and BJ_HTBSK=1;
    end if;

    fetch CurBSKJL into vSKTNO,vXPH;
  end loop;
  close CurBSKJL;
end;
/

prompt
prompt Creating procedure HYK_PROC_ADD_JF
prompt ==================================
prompt
create or replace procedure bfcrm10.HYK_PROC_ADD_JF (
                              pHYID   in integer,
                              pMDID   in integer,
                              pWCLJF  in float,
                              pBNLJJF in float,
                              pDJBH   in integer
                            )
is
begin
  update BFCRM10.HYK_JFZH
     set WCLJF=WCLJF + pWCLJF,
         BQJF = BQJF +pWCLJF,
         BNLJJF=BNLJJF + pBNLJJF,
         LJJF=LJJF +  pWCLJF
    where HYID=pHYID;

  if SQL%NOTFOUND then
    insert into BFCRM10.HYK_JFZH(HYID,WCLJF,XFJE,ZKJE,BQJF,LJJF,LJXFJE,LJZKJE,XFCS,THCS,BNLJJF)
      values(pHYID,pWCLJF,0,0,pWCLJF,pWCLJF,0,0,0,0,pBNLJJF);
  end if;

  update BFCRM10.HYK_MDJF
     set WCLJF=WCLJF + pWCLJF,
         BQJF = BQJF +pWCLJF,
         BNLJJF=BNLJJF + pBNLJJF,
         LJJF=LJJF +  pWCLJF
    where HYID=pHYID
      and MDID=pMDID;

  if SQL%NOTFOUND then
    insert into BFCRM10.HYK_MDJF(HYID,MDID,WCLJF,XFJE,ZKJE,BQJF,LJJF,LJXFJE,LJZKJE,XFCS,THCS,BNLJJF)
      values(pHYID,
           pMDID,
           pWCLJF,
           0,
           0,
           pWCLJF,
           pWCLJF,
           0,
           0,
           0,
           0,
           pBNLJJF);
  end if;


  /*insert into BFCRM10.HYK_JFBDJLMX(HYID,RQ,CLLX,JLBH,WCLJFBD,BQJFBD,BNLJJFBD)
  select pHYID,
         convert(date,convert(varchar2(20),sysdate,102)),
         11,
         pDJBH,
         round(pWCLJF,4),
         round(pWCLJF,4),
         round(pBNLJJF,4)*/

end;
/

prompt
prompt Creating procedure HYK_PROC_CZKFS_RJSH
prompt ======================================
prompt
create or replace procedure bfcrm10.HYK_PROC_CZKFS_RJSH(
                            pRclDate in date
                           )
is
  vHYID  integer;
  vHYKTYPE integer;
  vHYK_NO varchar2(30);
  vCDNR  varchar2(60);
  vYXQ  date;
  vDJR  integer;
  vDJRMC varchar2(16);
  vMDID  integer;
  vBGDD  varchar2(10);
  vYBGDD varchar2(10);
  vKHID  integer;
  vFXDW  integer;
  vYZM   varchar2(20);
  vQCYE  number(14,2);
  vYXTZJE number(14,2);
  vPDJE   number(14,2);
  --vJYBH   integer;
  vJLBH   integer;
  vZY     varchar2(40);
  vCZKHM  varchar2(30);
  vSTATUS integer;
  vCount     integer;

  integrity_error  exception;
    errno            integer;
    errmsg           char(200);

  cursor curLX is
    select A.JLBH,B.HYKTYPE,B.CZKHM,B.YXQ,A.DJR,A.DJRMC,A.BGDDDM,A.KHID,B.QCYE,B.YXTZJE,B.PDJE,A.ZY
      from BFCRM10.HYK_CZKSKJL A,
           BFCRM10.HYK_CZKSKJLITEM B
     where A.JLBH=B.JLBH
       and A.BJ_RJSH=1
       and A.ZXRQ >= pRclDate
       and A.ZXRQ < pRclDate + 1
       and A.SXSJ is null
      order by A.JLBH,B.CZKHM;

begin

  delete from BFCRM10.TMP_HYID_RJSH;

  open curLX;
  fetch curLX into vJLBH,vHYKTYPE,vHYK_NO,vYXQ,vDJR,vDJRMC,vYBGDD,vKHID,vQCYE,vYXTZJE,vPDJE,vZY;
  while (curLX%FOUND)
  loop
    vCount := 0;
    select count(*) into vCount
      from BFCRM10.HYKCARD
     where CZKHM=vHYK_NO;
    vCount := nvl(vCount,0);

    if vCount <= 0 then
      errno  := -20001;
      errmsg := '库存卡'||vHYK_NO||'已销售或卡号不存在！';
      raise integrity_error;
    end if;

    select CZKHM,CDNR,nvl(FXDWID,1),YZM,BGDDDM into vCZKHM,vCDNR,vFXDW,vYZM,vBGDD
      from BFCRM10.HYKCARD
     where CZKHM=vHYK_NO;

    if vBGDD <> vYBGDD then
      errno  := -20001;
      errmsg := '库存卡'||vHYK_NO||'的保管地点和售卡记录的保管地点不一致！';
      raise integrity_error;
    end if;

    vCount := 0;
    select count(*) into vCount
      from BFCRM10.ZFFS A,
           BFCRM10.HYK_CZKSKJLSKMX X
     where X.ZFFSID=A.ZFFSID
       and X.JLBH=vJLBH;
    vCount := nvl(vCount,0);

    if vCount <= 0 then
      errno  := -20001;
      errmsg := '没有售卡明细记录或者支付方式不正确！';
      raise integrity_error;
    end if;

    vCount := 0;
    select count(*) into vCount
      from BFCRM10.ZFFS A,
           BFCRM10.HYK_CZKSKJLSKMX X
      where X.ZFFSID=A.ZFFSID
        and A.TYPE in(6,10,11)
        and X.JLBH=vJLBH;
    vCount := nvl(vCount,0);
    vSTATUS := 0;
    if vCount > 0 then
      vSTATUS := -4;
    end if;

    vMDID := 0;
    select MDID into vMDID
      from BFCRM10.HYK_BGDD
     where BGDDDM=vYBGDD;
    vMDID := nvl(vMDID,0);

    if vMDID <=0 then
      errno  := -20001;
      errmsg := '保管地点 '||vYBGDD||' 未指定门店';
      raise integrity_error;
    end if;

    vHYID := BFCRM10.Update_BHZT('HYK_HYXX');
    --vJYBH := BFCRM10.Update_BHZT('HYK_JEZCLJL');
    insert into BFCRM10.HYK_HYXX(HYID,HYKTYPE,HYK_NO,CDNR,JKRQ,YXQ,DJR,DJRMC,DJSJ,STATUS,YBGDD,KHID,MDID,FXDW,YZM,BJ_PSW,PASSWORD)
    values(vHYID ,vHYKTYPE,vHYK_NO,vCDNR,Trunc(sysdate),vYXQ,vDJR,vDJRMC,sysdate,vSTATUS,vYBGDD,vKHID,vMDID,vFXDW,vYZM,0,vYZM);

    insert into BFCRM10.HYK_JEZH(HYID,QCYE,YE,YXTZJE,PDJE)
    values(vHYID,round(vQCYE,2),round(vQCYE,2),round(vYXTZJE,2),round(vPDJE,2));

    insert into BFCRM10.HYK_JEZCLJL(HYID,MDID,CLSJ,CLLX,JLBH,ZY,JFJE,DFJE,YE)
    values(vHYID,vMDID,sysdate,0,vJLBH,vZY,round(vQCYE,2),0,round(vQCYE,2));

    update BFCRM10.HYK_CZKSKJLITEM set HYID=vHYID where JLBH=vJLBH and CZKHM=vHYK_NO;

    insert into BFCRM10.TMP_HYID_RJSH(HYID,JLBH)
     values(vHYID,vJLBH);

    delete from BFCRM10.HYKCARD where CZKHM=vHYK_NO;

    fetch curLX into vJLBH,vHYKTYPE,vHYK_NO,vYXQ,vDJR,vDJRMC,vYBGDD,vKHID,vQCYE,vYXTZJE,vPDJE,vZY;
  end loop;
  close curLX;

  update BFCRM10.HYK_CZKSKJL A set SXSJ=sysdate
  where exists(select 1 from BFCRM10.TMP_HYID_RJSH where A.JLBH=JLBH);

  exception
    when integrity_error then
       raise_application_error(errno, errmsg);
end;
/

prompt
prompt Creating procedure HYK_PROC_CZKFS_SH
prompt ====================================
prompt
CREATE OR REPLACE PROCEDURE BFCRM10.HYK_PROC_CZKFS_SH (
                          pHYID in integer,
                          --pJYBH in integer,
                          pJLBH in  integer,
                          pStr  out varchar2
                           )
is
  vHYID    integer;
  vHYKTYPE integer;
  vHYK_NO  varchar2(30);
  vCDNR    varchar2(60);
  vYXQ     date;
  vDJR     integer;
  vDJRMC   varchar2(16);
  vMDID    integer;
  vYBGDD   varchar2(10);
  vKHID    integer;
  vFXDW    integer;
  vYZM     varchar2(20);
  vQCYE    number(14,2);
  vYXTZJE  number(14,2);
  vPDJE    number(14,2);
  --vJYBH    integer;
  vJLBH    integer;
  vZY      varchar2(40);
  vCZKHM   varchar2(30);
  vSTATUS  integer;
  vSL      integer;
  vBGDD    varchar2(10);
  vYSZE    number(14,2);

  cursor curLX is
    select B.HYKTYPE,B.CZKHM,B.YXQ,A.DJR,A.DJRMC,A.BGDDDM,A.KHID,B.QCYE,B.YXTZJE,B.PDJE,A.JLBH,A.ZY
      from BFCRM10.HYK_CZKSKJL A,
           BFCRM10.HYK_CZKSKJLITEM B
     where A.JLBH=B.JLBH
       and A.JLBH=pJLBH
  order by B.CZKHM;

begin
  --判断支付方式是否启动
  vSTATUS := 0;
  select sum(YSZE) into vYSZE from BFCRM10.HYK_CZKSKJL where JLBH=pJLBH;
  vYSZE := nvl(vYSZE,0);
  if vYSZE <> 0 then
    select count(*) into vSL
      from BFCRM10.ZFFS A,
           BFCRM10.HYK_CZKSKJLSKMX X
     where X.ZFFSID=A.ZFFSID
       and X.JLBH=pJLBH;
    vSL := nvl(vSL,0);

    if vSL=0 then
      pStr:='不存在支付明细！';
      return;
    else
      vSL := 0;
      select count(*) into vSL
        from BFCRM10.ZFFS A,
             BFCRM10.HYK_CZKSKJLSKMX X
        where X.ZFFSID=A.ZFFSID
          and A.TYPE in(6,10,11)
          and X.JLBH=pJLBH;
      vSL := nvl(vSL,0);
      if vSL > 0 then
        vSTATUS := -4;
      end if;
    end if;
  end if;

  vHYID:=pHYID;
  --vJYBH:=pJYBH;
  open curLX;
  fetch curLX into vHYKTYPE,vHYK_NO,vYXQ,vDJR,vDJRMC,vYBGDD,vKHID,vQCYE,vYXTZJE,vPDJE,vJLBH,vZY;
  while (curLX%FOUND)
  loop
   ---判断卡信息
    select count(*)
      into vSL
      from HYKCARD
     where CZKHM=vHYK_NO;

    if vSL=0 then
      pStr:='库存卡'+vHYK_NO+'已销售或卡号不存在！';
      return;
    else
      select CZKHM,CDNR,FXDWID,YZM,BGDDDM
        into vCZKHM,vCDNR,vFXDW,vYZM,vBGDD
        from HYKCARD
       where CZKHM=vHYK_NO;
    end if;

    --判断YBGDD和HYKCARD中的是否一致
    if vBGDD<>vYBGDD then
      pStr:='库存卡'+vHYK_NO+'的保管地点和售卡记录的保管地点不一致！';
      return;
    end if;

    vMDID := 0;
    select MDID into vMDID
      from BFCRM10.HYK_BGDD
     where BGDDDM=vYBGDD;
    vMDID := nvl(vMDID,0);

    if vMDID <=0 then
      pStr:='保管地点 '+vYBGDD+' 未指定门店';
      return;
    end if;

    ----写会员信息(支票等方式付款的状态置为停用)
    insert into BFCRM10.HYK_HYXX(HYID,HYKTYPE,HYK_NO,CDNR,JKRQ,YXQ,DJR,DJRMC,DJSJ,STATUS,YBGDD,KHID,MDID,FXDW,YZM,BJ_PSW,PASSWORD)
    values(vHYID ,vHYKTYPE,vHYK_NO,vCDNR,Trunc(sysdate),vYXQ,vDJR,vDJRMC,sysdate,vSTATUS,vYBGDD,vKHID,vMDID,vFXDW,vYZM,0,vYZM);

    ----写金额账户
    insert into BFCRM10.HYK_JEZH(HYID,QCYE,YE,YXTZJE,PDJE)
    values(vHYID,round(vQCYE,2),round(vQCYE,2),round(vYXTZJE,2),round(vPDJE,2));

    ----写金额帐处理记录
    insert into BFCRM10.HYK_JEZCLJL(HYID,MDID,CLSJ,CLLX,JLBH,ZY,JFJE,DFJE,YE)
    values(vHYID,vMDID,sysdate,0,pJLBH,vZY,round(vQCYE,2),0,round(vQCYE,2));

    ----删除HYKCARD
    delete BFCRM10.HYKCARD where CZKHM=vHYK_NO;

    update BFCRM10.HYK_CZKSKJLITEM set HYID=vHYID where JLBH=vJLBH and CZKHM=vHYK_NO;

    vHYID:=vHYID+1;
    --vJYBH:=vJYBH+1;
    fetch curLX into vHYKTYPE,vHYK_NO,vYXQ,vDJR,vDJRMC,vYBGDD,vKHID,vQCYE,vYXTZJE,vPDJE,vJLBH,vZY;
  end loop;
  close curLX;

  ----反写HYK_CZKSKJL的SXSJ
  update BFCRM10.HYK_CZKSKJL set SXSJ=sysdate where JLBH=pJLBH;
end;
/

prompt
prompt Creating procedure HYK_PROC_CZK_XFHZ
prompt ====================================
prompt
create or replace procedure bfcrm10.HYK_PROC_CZK_XFHZ (
                              pRCLRQ in date
                            )
is
  vJYID integer;
  vHYID integer;
  vSHSPID integer;
  vCZKJE number(14,2);
  vJE number(14,2);
  vXSJE number(14,2);
  vXSBM varchar2(10);
  vMDID integer;
  --vCE number(14,2);

  cursor Cur_CE is
    select  L.JYID  ,P.SHSPID,sum(P.XSJE)
      from BFCRM10.HYXFJL J,BFCRM10.HYXFJL_SP P ,BFCRM10.HYK_JYCL L
      where J.XFJLID=P.XFJLID
        and J.XFJLID=L.XFJLID
        and L.JYZT=2
        and L.BJ_CZK=1
        and J.CRMJZRQ=pRCLRQ
      group by  L.JYID ,P.SHSPID;

  cursor curCZK is
    select HYID,MDID,DFJE - JE_CZ
      from BFCRM10.TMP_HY_JEZCLJL
      where JYID=vJYID
        and abs(DFJE)>abs(JE_CZ)
      order by JYID asc,DFJE desc;

  cursor curSP is
    select JYID,SHSPID,XSJE,CZKJE,XSBM
      from BFCRM10.TMP_SHSPXX_XFJE
      order by JYID asc,CZKJE desc;
begin

  delete from BFCRM10.TMP_HY_JEZCLJL;

  delete from BFCRM10.TMP_JYID_JE;

  delete from BFCRM10.TMP_SHSPXX_XFJE;

  delete from BFCRM10.TMP_JYID_ZXSJE;

  delete from BFCRM10.TMP_JYID_CZKJE;

  update BFCRM10.HYK_XFJL
     set CRMJZRQ=pRCLRQ
    where STATUS=1
      and CRMJZRQ is null;

  insert into  BFCRM10.TMP_HY_JEZCLJL(HYID,MDID,JYID,DFJE,JE_CZ)
    select  J.HYID,J.MDID,J.JYID,sum(J.DFJE),0
      from  BFCRM10.HYK_JEZCLJL  J
      where J.CRMJZRQ=pRCLRQ
        and J.CLLX=7
      group by  J.HYID,J.MDID,J.JYID;

  insert into  BFCRM10.TMP_JYID_JE(JYID,DFJE)
    select  JYID,sum(DFJE)
      from  BFCRM10.TMP_HY_JEZCLJL
      group by JYID;

  insert into  BFCRM10.TMP_SHSPXX_XFJE(JYID,SHSPID,XSJE,CZKJE)
    select  L.JYID  ,P.SHSPID,sum(P.XSJE) ,0
      from  BFCRM10.HYK_XFJL J,BFCRM10.HYK_XFJL_SP P ,BFCRM10.HYK_JYCL L
      where L.XFJLID=P.XFJLID
        and J.XFJLID=L.XFJLID
        and J.STATUS=1
        and L.JYZT=2
        and L.BJ_CZK=1
        and J.CRMJZRQ=pRCLRQ
      group by  L.JYID ,P.SHSPID;

  open Cur_CE;
  fetch Cur_CE into vJYID,vSHSPID,vXSJE;
  while (Cur_CE%FOUND)
  loop
    update  BFCRM10.TMP_SHSPXX_XFJE
      set   XSJE=XSJE + vXSJE
      where JYID=vJYID
        and SHSPID=vSHSPID;

    if SQL%NOTFOUND then
      insert into  BFCRM10.TMP_SHSPXX_XFJE(JYID,SHSPID,XSJE,CZKJE)
        values(vJYID,vSHSPID,vXSJE,0);
    end if;

    fetch Cur_CE into vJYID,vSHSPID,vXSJE;
  end loop;
  close Cur_CE;

  insert into BFCRM10.TMP_JYID_ZXSJE(JYID,ZXSJE)
    select JYID,sum(XSJE) ZXSJE
      from BFCRM10.TMP_SHSPXX_XFJE
      group by JYID;

  update BFCRM10.TMP_SHSPXX_XFJE C
    set CZKJE=round((select B.DFJE/D.ZXSJE
                       from BFCRM10.TMP_JYID_ZXSJE D,BFCRM10.TMP_JYID_JE B
                       where D.JYID=B.JYID
                         and D.JYID=C.JYID
                     ) * C.XSJE,2);
    /*
    where exists(select 1
                   from BFCRM10.TMP_JYID_ZXSJE D,BFCRM10.TMP_JYID_JE B
                   where D.JYID=C.JYID
                     and D.JYID=B.JYID)
    */

  insert into BFCRM10.TMP_JYID_CZKJE(JYID,CZKJE)
   select JYID,sum(CZKJE) CZKJE
     from BFCRM10.TMP_SHSPXX_XFJE
     group by JYID;

  for item in (
     select E.JYID,sum(E.CZKJE - B.DFJE) as CE
      from  BFCRM10.TMP_JYID_CZKJE  E,BFCRM10.TMP_JYID_JE B
      where E.JYID=B.JYID
        and abs(E.CZKJE - B.DFJE)>0
      group by  E.JYID)
  loop
    select max(SHSPID) into vSHSPID
      from BFCRM10.TMP_SHSPXX_XFJE
      where CZKJE=(select MAX(CZKJE)
                     from BFCRM10.TMP_SHSPXX_XFJE
                     where JYID=item.JYID)
        and JYID=item.JYID;

    update BFCRM10.TMP_SHSPXX_XFJE
      set CZKJE = CZKJE - item.CE
      where JYID=item.JYID
        and SHSPID=vSHSPID;
  end loop;

  open curSP;
  fetch curSP into vJYID,vSHSPID,vXSJE,vCZKJE,vXSBM;
  while (curSP%FOUND)
  loop
    open curCZK;
    fetch curCZK into vHYID,vMDID,vJE;
    while (curCZK%FOUND) and (vJE<=vCZKJE)
    loop
      update BFCRM10.CZK_XFMX_HZ
        set CZK_XFJE=CZK_XFJE+vJE
        where RQ=pRCLRQ
          and HYID=vHYID
          and SHSPID=vSHSPID;
      if SQL%NOTFOUND then
        insert into BFCRM10.CZK_XFMX_HZ(RQ,HYID,SHSPID,CZK_XFJE)
          values(pRCLRQ,vHYID,vSHSPID, vJE);
      end if;

      update BFCRM10.TMP_HY_JEZCLJL
        set JE_CZ=JE_CZ + vJE
        where HYID=vHYID
          and JYID=vJYID;

      vCZKJE := vCZKJE - vJE;

      fetch curCZK into vHYID,vMDID,vJE;
    end loop;
    close curCZK;

    if vJE>vCZKJE then
      update BFCRM10.CZK_XFMX_HZ
         set CZK_XFJE=CZK_XFJE+vCZKJE
        where RQ=pRCLRQ
          and HYID=vHYID
          and SHSPID=vSHSPID;
      if SQL%NOTFOUND then
        insert into BFCRM10.CZK_XFMX_HZ(RQ,HYID,SHSPID,CZK_XFJE)
          values(pRCLRQ,vHYID,vSHSPID, vCZKJE);
      end if;

      update BFCRM10.TMP_HY_JEZCLJL
        set JE_CZ=JE_CZ + vCZKJE
        where HYID=vHYID
          and JYID=vJYID;
      vCZKJE :=0;
    end if;
    fetch curSP into vJYID,vSHSPID,vXSJE,vCZKJE,vXSBM;
  end loop;
  close curSP;

  /*
  update BFCRM10.CZK_XFMX_HZ
     set KHID=X.KHID,
         SHSPFLID=S.SHSPFLID,
         SHSBID=S.SHSBID
    from BFCRM10.CZK_XFMX_HZ  H,
         BFCRM10.HYK_HYXX X,
         BFCRM10.SHSPXX S
   where H.RQ=pRCLRQ and
         H.HYID=X.HYID and
         H.SHSPID=S.SHSPID
  */
  update BFCRM10.CZK_XFMX_HZ H
     set KHID=(select X.KHID from BFCRM10.HYK_HYXX X where X.HYID=H.HYID),
         (SHSPFLID,SHSBID)=(select S.SHSPFLID,S.SHSBID from BFCRM10.SHSPXX S where S.SHSPID=H.SHSPID)
   where H.RQ=pRCLRQ;
     --and H.HYID=X.HYID
     --and H.SHSPID=S.SHSPID
end;
/

prompt
prompt Creating procedure HYK_PROC_DEL_XFJL
prompt ====================================
prompt
create or replace procedure bfcrm10.HYK_PROC_DEL_XFJL (
                              pRCLRQ in date
                            )
is
  TYPE TID is table of BFCRM10.HYK_XFJL.XFJLID%TYPE
    index by binary_integer;

  listID TID;
  vNextDay   date;
  vCount     integer;
begin
  vNextDay := pRCLRQ+1;

  select XFJLID bulk collect into listID
    from BFCRM10.HYK_XFJL
    where CRMJZRQ=pRCLRQ
      and STATUS=1;

  forall vID in listID.First..listID.Last
    delete from BFCRM10.HYK_XFJL_SP where XFJLID=listID(vID);

  forall vID in listID.First..listID.Last
    delete from BFCRM10.HYK_XFJL_ZFFS where XFJLID=listID(vID);

  forall vID in listID.First..listID.Last
    delete from BFCRM10.HYK_XFJL_SP_ZFFS where XFJLID=listID(vID);

  forall vID in listID.First..listID.Last
    delete from BFCRM10.HYK_XFJL_SP_FQ where XFJLID=listID(vID);

  forall vID in listID.First..listID.Last
    delete from BFCRM10.HYK_XFJL_SP_YQJC where XFJLID=listID(vID);
  forall vID in listID.First..listID.Last
    delete from BFCRM10.HYK_XFJL_SP_YQFT where XFJLID=listID(vID);

  forall vID in listID.First..listID.Last
    delete from BFCRM10.HYK_XFJL_FQ where XFJLID=listID(vID);

  forall vID in listID.First..listID.Last
    delete from BFCRM10.HYK_XFJL_FQDM where XFJLID=listID(vID);

  forall vID in listID.First..listID.Last
    delete from BFCRM10.HYK_XFJL_SP_ZFFS_FQ where XFJLID=listID(vID);

   forall vID in listID.First..listID.Last
    delete from BFCRM10.HYK_XFJL_ZFFS_FQ where XFJLID=listID(vID);

   forall vID in listID.First..listID.Last
    delete from BFCRM10.HYK_XFJL_YHKZF where XFJLID=listID(vID);

   forall vID in listID.First..listID.Last
    delete from BFCRM10.HYK_XFJL_ZSLP where XFJLID=listID(vID);

   forall vID in listID.First..listID.Last
    delete from BFCRM10.HYK_XFJL_ZFFS_ZSLP where XFJLID=listID(vID);

   forall vID in listID.First..listID.Last
    delete from BFCRM10.HYK_XFJL_SP_MBJZ where XFJLID=listID(vID);

   forall vID in listID.First..listID.Last
    delete from BFCRM10.HYK_XFJL_SP_ZFFS_MBJZ where XFJLID=listID(vID);

  forall vID in listID.First..listID.Last
    delete from BFCRM10.HYK_XFJL where XFJLID=listID(vID);
end;
/

prompt
prompt Creating procedure HYK_PROC_HYK_CZK_RBB
prompt =======================================
prompt
create or replace procedure bfcrm10.HYK_PROC_HYK_CZK_RBB (pRCLRQ in date) /* 日期 */
as
  vPrevDD      Date;
  vNextDD      Date;
  vHYKTYPE     number;
  vMDID        number;
  vTmpJE       number(14,2);
  vSQYE        number(14,2);
  vJKJE        number(14,2);
  vTSKJE       number(14,2);
  vCKJE        number(14,2);
  vQKJE        number(14,2);
  vXFJE        number(14,2);
  vTKJE        number(14,2);
  vQMYE        number(14,2);
  vTZJE        number(14,2);
  vBKJE        number(14,2);

  cursor curLX is
    select  D.HYKTYPE,X.MDID
      from  BFCRM10.HYKDEF D,BFCRM10.HYK_JEZH J,BFCRM10.HYK_HYXX X
      where D.HYKTYPE=X.HYKTYPE
        and J.HYID=X.HYID
        and D.BJ_CZZH=1
      group by D.HYKTYPE,X.MDID;
begin
  vNextDD := pRCLRQ+1;
  vPrevDD := pRCLRQ-1;

 update BFCRM10.HYK_JEZCLJL
   set CRMJZRQ=pRCLRQ
   where CRMJZRQ is null;
   COMMIT ;

  open curLX;
  fetch curLX into vHYKTYPE,vMDID;
  while (curLX%FOUND)
  LOOP
    /*上期余额*/
    vSQYE := 0;
    select avg(QMYE) into vSQYE
      from BFCRM10.HYK_CZK_RBB
      where RQ=vPrevDD
        and HYKTYPE=vHYKTYPE
        and MDID=vMDID;
    vSQYE := nvl(vSQYE,0);

    /*售卡金额*/
    vJKJE := 0;
    select sum(I.QCYE) into vJKJE
      from BFCRM10.HYK_CZKSKJL L,BFCRM10.HYK_CZKSKJLITEM I,BFCRM10.HYKDEF H,BFCRM10.HYK_BGDD D
      where L.JLBH=I.JLBH
        and ZXRQ >= pRCLRQ
        and ZXRQ <  vNextDD
        and L.STATUS>=1
        and I.HYKTYPE=H.HYKTYPE
        and L.BGDDDM=D.BGDDDM
        and I.HYKTYPE=vHYKTYPE
        and D.MDID=vMDID
        and L.FS<=1
        and H.BJ_CZYHQ=0;

    vJKJE := nvl(vJKJE,0);

    /*退售卡金额*/
    vTSKJE := 0;
    select sum(I.QCYE) into vTSKJE
      from BFCRM10.HYK_CZKSKJL L,BFCRM10.HYK_CZKSKJLITEM I,BFCRM10.HYKDEF H,BFCRM10.HYK_BGDD D
      where L.JLBH=I.JLBH
        and L.QDSJ >=pRCLRQ
        and L.QDSJ < vNextDD
        and L.STATUS>1
        and I.HYKTYPE=H.HYKTYPE
        and L.BGDDDM=D.BGDDDM
        and I.HYKTYPE=vHYKTYPE
        and D.MDID=vMDID
        and FS=2
        and H.BJ_CZYHQ=0;

    vTSKJE:=nvl(vTSKJE,0);

    /*存款金额*/
    vCKJE := 0;
    select sum(A.CKJE) into vCKJE
      from BFCRM10.HYK_CZK_CKJL A,BFCRM10.HYK_HYXX B,BFCRM10.HYK_BGDD D
      where A.HYID=B.HYID
        and A.CZDD=D.BGDDDM
        and A.ZXRQ >= pRCLRQ
        and A.ZXRQ <  vNextDD
        and B.HYKTYPE=vHYKTYPE
        and B.MDID=vMDID
        --and nvl(A.ZXR,0)>0;
        and A.ZXR is not null;
    vCKJE := nvl(vCKJE,0);


    vTmpJE := 0;
    select sum(I.CKJE) into vTmpJE
      from BFCRM10.HYK_CZK_PLCKJL A,BFCRM10.HYK_CZK_PLCKJLITEM I,BFCRM10.HYK_HYXX B,BFCRM10.HYK_BGDD D
      where A.JLBH=I.CZJPJ_JLBH
        and B.HYID=I.HYID
        and A.CZDD=D.BGDDDM
        and A.ZXRQ >= pRCLRQ
        and A.ZXRQ <  vNextDD
        and B.HYKTYPE=vHYKTYPE
        and B.MDID=vMDID
        --and nvl(A.ZXR,0)>0;
        and A.ZXR is not null;

    vCKJE := vCKJE+nvl(vTmpJE,0);

     /*取款金额*/
    vQKJE := 0;
    select sum(A.QKJE) into vQKJE
      from BFCRM10.HYK_CZK_QKJL A,BFCRM10.HYK_HYXX B,BFCRM10.HYK_BGDD D
      where A.HYID=B.HYID
        and A.CZDD=D.BGDDDM
        and A.ZXRQ >= pRCLRQ
        and A.ZXRQ <  vNextDD
        and B.HYKTYPE=vHYKTYPE
        and B.MDID=vMDID
        --and nvl(A.ZXR,0)>0
        and A.ZXR is not null;
    vQKJE := nvl(vQKJE,0);

    vTmpJE := 0;
    select sum(I.QKJE) into vTmpJE
      from BFCRM10.HYK_CZK_PLQKJL A,BFCRM10.HYK_CZK_PLQKJLITEM I,BFCRM10.HYK_HYXX B,BFCRM10.HYK_BGDD D
      where A.JLBH=I.CZJPJ_JLBH
        and B.HYID=I.HYID
        and A.CZDD=D.BGDDDM
        and A.ZXRQ >= pRCLRQ
        and A.ZXRQ <  vNextDD
        and B.HYKTYPE=vHYKTYPE
        and B.MDID=vMDID
        --and nvl(A.ZXR,0)>0
        and A.ZXR is not null;
    vQKJE := vQKJE+nvl(vTmpJE,0);

    /*并卡金额   */

    vBKJE := 0;
    vTmpJE := 0;
    select sum(A.ZRJE) into vTmpJE
      from BFCRM10.HYK_CZK_JEZ_ZC A,BFCRM10.HYK_HYXX B,BFCRM10.HYK_BGDD D
      where A.HYID_ZR=B.HYID
        and A.CZDD=D.BGDDDM
        and A.ZXRQ >= pRCLRQ
        and A.ZXRQ <  vNextDD
        and B.HYKTYPE=vHYKTYPE
        and B.MDID=vMDID
        --and nvl(A.ZXR,0)>0
        and A.ZXR is not null;
    vBKJE := vBKJE+nvl(vTmpJE,0);


    vTmpJE := 0;
    select sum(A.ZCJE) into vTmpJE
      from BFCRM10.HYK_CZK_JZE_ZCITEM A,BFCRM10.HYK_HYXX B,BFCRM10.HYK_CZK_JEZ_ZC C,BFCRM10.HYK_BGDD D
      where A.CZJPJ_JLBH=C.CZJPJ_JLBH
        and A.HYID_ZC=B.HYID
        and C.CZDD=D.BGDDDM
        and C.ZXRQ >= pRCLRQ
        and C.ZXRQ <  vNextDD
        and B.HYKTYPE=vHYKTYPE
        and B.MDID=vMDID
        and C.ZXR is not null;
        --and nvl(C.ZXR,0)>0
    vBKJE := vBKJE - nvl(vTmpJE,0);


    /*消费金额*/
    vXFJE := 0;
    select sum(A.DFJE) into vXFJE
      from BFCRM10.HYK_JEZCLJL A,BFCRM10.HYK_HYXX B
      where B.HYID=A.HYID
        and CRMJZRQ =pRCLRQ
        and B.HYKTYPE=vHYKTYPE
        and B.MDID=vMDID
        and A.CLLX=7;
    vXFJE  := nvl(vXFJE,0);

    /*退卡金额*/
    vTKJE := 0;
    select sum(A.DFJE) into vTKJE
      from BFCRM10.HYK_JEZCLJL A,BFCRM10.HYK_HYXX B
      where B.HYID=A.HYID
        and CRMJZRQ =pRCLRQ
        and B.HYKTYPE=vHYKTYPE
        and B.MDID=vMDID
        and A.CLLX=6;
    vTKJE  := nvl(vTKJE,0);

    /*调整金额*/
    vTZJE := 0;
    select sum(A.DFJE) into vTZJE
      from BFCRM10.HYK_JEZCLJL A,BFCRM10.HYK_HYXX B
      where A.HYID=B.HYID
        and CRMJZRQ =pRCLRQ
        and B.HYKTYPE=vHYKTYPE
        and B.MDID=vMDID
        and (A.CLLX=11  or  A.CLLX=12 );
    vTZJE  := nvl(vTZJE,0);

    vTmpJE := 0;
    select sum(B.YE) into vTmpJE
      from BFCRM10.HYK_GHKLX A,BFCRM10.HYK_JEZH B,BFCRM10.HYK_HYXX X
      where A.HYID=B.HYID
        and A.HYID=X.HYID
        and A.DJSJ >=pRCLRQ
        and A.DJSJ <vNextDD
        and A.HYKTYPE_OLD=vHYKTYPE
        and X.MDID=vMDID;
    vTZJE := vTZJE-nvl(vTmpJE,0);

    vTmpJE := 0;
    select sum(B.YE) into vTmpJE
      from BFCRM10.HYK_GHKLX A,BFCRM10.HYK_JEZH B,BFCRM10.HYK_HYXX X
      where A.HYID=B.HYID
        and A.HYID=X.HYID
        and A.DJSJ >=pRCLRQ
        and A.DJSJ <vNextDD
        and A.HYKTYPE_NEW=vHYKTYPE
        and X.MDID=vMDID;
    vTZJE := vTZJE+nvl(vTmpJE,0);

    vTmpJE := 0;
    select sum(B.YE) into vTmpJE
      from BFCRM10.HYK_SJJL A,BFCRM10.HYK_JEZH B,BFCRM10.HYK_HYXX X
      where A.HYID=B.HYID
        and A.HYID=X.HYID
        and A.ZXRQ >=pRCLRQ
        and A.ZXRQ <vNextDD
        and A.HYKTYPE_OLD=vHYKTYPE
        and X.MDID=vMDID;
    vTZJE := vTZJE-nvl(vTmpJE,0);

    vTmpJE := 0;
    select sum(B.YE) into vTmpJE
     from BFCRM10.HYK_SJJL A,BFCRM10.HYK_JEZH B,BFCRM10.HYK_HYXX X
      where A.HYID=B.HYID
        and A.HYID=X.HYID
        and A.ZXRQ >=pRCLRQ
        and A.ZXRQ <vNextDD
        and A.HYKTYPE_NEW=vHYKTYPE
        and X.MDID=vMDID;
    vTZJE := vTZJE+nvl(vTmpJE,0);


    /*期末余额*/
    vQMYE := 0;
    select sum(A.YE) into vQMYE
      from BFCRM10.HYK_JEZH A,BFCRM10.HYK_HYXX B ,BFCRM10.HYKDEF F
      where A.HYID=B.HYID and B.HYKTYPE=F.HYKTYPE
        and B.HYKTYPE=vHYKTYPE
        and B.MDID=vMDID;
    vQMYE := nvl(vQMYE,0);

    insert into BFCRM10.HYK_CZK_RBB(RQ,HYKTYPE,MDID,SQYE,JKJE,CKJE,QKJE,XFJE,TKJE,QMYE,TZJE,BKJE)
       values(pRCLRQ,vHYKTYPE,vMDID,vSQYE,vJKJE-vTSKJE,vCKJE,vQKJE,vXFJE,vTKJE,vQMYE,vTZJE,vBKJE);

    fetch curLX into vHYKTYPE,vMDID;
  end LOOP;
  close curLX;
end;
/

prompt
prompt Creating procedure HYK_PROC_HYK_HYGRPZZ
prompt =======================================
prompt
create or replace procedure bfcrm10.HYK_PROC_HYK_HYGRPZZ (
                              pZZR   in integer,
                              pProcDate in date
                            )
is
  vZZRMC   varchar2(16);
  vNextDay date;
begin
  vNextDay := pProcDate+1;

  begin
    select PERSON_NAME into vZZRMC
      from BFCRM10.RYXX
      where PERSON_ID=pZZR;
  exception
    when others then
      vZZRMC := to_char(pZZR);
  end;

  for item in (
    select GRPID
      from BFCRM10.HYK_HYGRP
      where JSSJ< vNextDay
        and ZXR is not null
        and ZZR is null)
  loop
    update BFCRM10.HYK_HYGRP
      set ZZR=pZZR,
          ZZRMC=vZZRMC,
          ZZRQ=sysdate,
          STATUS=3
      where GRPID=item.grpid;

    if SQL%NOTFOUND then
      raise_application_error(-20001,'没有可终止的会员组!');
      return;
    end if;
  end loop;
end;
/

prompt
prompt Creating procedure HYK_PROC_HYK_JFFHLPJXC
prompt =========================================
prompt
CREATE OR REPLACE PROCEDURE BFCRM10.HYK_PROC_HYK_JFFHLPJXC (
                              pRCLRQ in date
                            )
is
  vCount   integer;
  vNextDay date;
begin
  select count(*) into vCount from BFCRM10.HYK_JFFHLPJXC  where RQ=pRCLRQ;
  if vCount>0 then
    return;
  end if;

  vNextDay := pRCLRQ+1;
  /*期初库存*/
  insert into HYK_JFFHLPJXC (RQ, MDID, LPID, JHSL, BRSL, BCSL, FFSL, JCSL, ZFSL, SYSL, THSL, ZTSL, QCSL)
    SELECT pRCLRQ,MDID,LPID,0,0,0,0,0,0,0,0,0,NVL(JCSL,0)
    FROM HYK_JFFHLPJXC
    WHERE RQ=pRCLRQ-1;

  /*进货*/
  for item in (
    select B.LPID,D.MDID,sum(B.JHSL) SL
      from BFCRM10.HYK_JFFHLPJHD A,BFCRM10.HYK_JFFHLPJHDMX B,BFCRM10.HYK_BGDD D
      where A.JLBH=B.JLBH
        and A.BGDDDM=D.BGDDDM
        and A.ZXRQ>=pRCLRQ
        and A.ZXRQ< vNextDay
        and A.ZXR is not null
        AND A.DJLX=0
      group by B.LPID,D.MDID)
  loop
    update BFCRM10.HYK_JFFHLPJXC
      set JHSL=JHSL+item.SL
      where RQ=pRCLRQ
        and LPID = item.LPID
        and MDID = item.MDID;

    if SQL%NOTFOUND then
     insert into BFCRM10.HYK_JFFHLPJXC(RQ,LPID,MDID,JHSL,BRSL,BCSL,FFSL,JCSL,ZFSL,SYSL,THSL,ZTSL) --增加作废数量、损益数量和在途数量
        values(pRCLRQ,item.LPID,item.MDID,item.SL,0,0,0,0,0,0,0,0);
    end if;
  end loop;


  /*退货*/
  for item in (
    select B.LPID,D.MDID,sum(B.JHSL) SL
      from BFCRM10.HYK_JFFHLPJHD A,BFCRM10.HYK_JFFHLPJHDMX B,BFCRM10.HYK_BGDD D
      where A.JLBH=B.JLBH
        and A.BGDDDM=D.BGDDDM
        and A.ZXRQ>=pRCLRQ
        and A.ZXRQ< vNextDay
        and A.ZXR is not null
        AND A.DJLX=1
      group by B.LPID,D.MDID)
  loop
    update BFCRM10.HYK_JFFHLPJXC
      set THSL=THSL+item.SL
      where RQ=pRCLRQ
        and LPID = item.LPID
        and MDID = item.MDID;

    if SQL%NOTFOUND then
     insert into BFCRM10.HYK_JFFHLPJXC(RQ,LPID,MDID,JHSL,BRSL,BCSL,FFSL,JCSL,ZFSL,SYSL,THSL,ZTSL) --增加作废数量、损益数量和在途数量
        values(pRCLRQ,item.LPID,item.MDID,0,0,0,0,0,0,0,item.SL,0);
    end if;
  end loop;
  /*拨入*/
  for item in (
    select B.LPID,D.MDID,sum(B.LQSL) SL
      from BFCRM10.HYK_LPLQJL A,BFCRM10.HYK_LPLQJLITEM B,BFCRM10.HYK_BGDD D
      where A.JLBH=B.JLBH
        and A.BGDDDM_BR=D.BGDDDM
        and A.LQSJ>=pRCLRQ
        and A.LQSJ< vNextDay
        and A.LQR is not null
      group by B.LPID,D.MDID)
  loop
    update BFCRM10.HYK_JFFHLPJXC
      set BRSL=BRSL+item.SL
      where RQ=pRCLRQ
        and  LPID = item.LPID
        and  MDID = item.MDID;
    if SQL%NOTFOUND then
      insert into BFCRM10.HYK_JFFHLPJXC(RQ,LPID,MDID,JHSL,BRSL,BCSL,FFSL,JCSL,ZFSL,SYSL,THSL,ZTSL)--增加作废数量、损益数量和在途数量
        values(pRCLRQ,item.LPID,item.MDID,0,item.SL,0,0,0,0,0,0,0);
    end if;
  end loop;

  /*拨出*/
  for item in (
    select B.LPID,D.MDID,sum(B.LQSL) SL
      from BFCRM10.HYK_LPLQJL A,BFCRM10.HYK_LPLQJLITEM B,BFCRM10.HYK_BGDD D
      where A.JLBH=B.JLBH
      and A.BGDDDM_BC=D.BGDDDM
        and A.ZXRQ>=pRCLRQ
        and A.ZXRQ< vNextDay
        and A.ZXR is not null
      group by B.LPID,D.MDID)
  loop
    update BFCRM10.HYK_JFFHLPJXC
      set BCSL=BCSL+item.SL
      where RQ=pRCLRQ
        and  LPID = item.LPID
        and  MDID = item.MDID;

    if SQL%NOTFOUND then
      insert into BFCRM10.HYK_JFFHLPJXC(RQ,LPID,MDID,JHSL,BRSL,BCSL,FFSL,JCSL,ZFSL,SYSL,THSL,ZTSL)--增加作废数量、损益数量和在途数量
        values(pRCLRQ,item.LPID,item.MDID,0,0,item.SL,0,0,0,0,0,0);
    end if;
  end loop;

  /*在途量*/
  for item in (
    select B.LPID,D.MDID,sum(B.LQSL) SL
      from BFCRM10.HYK_LPLQJL A,BFCRM10.HYK_LPLQJLITEM B,BFCRM10.HYK_BGDD D
      where A.JLBH=B.JLBH
      and A.BGDDDM_BC=D.BGDDDM
        and A.ZXRQ>=pRCLRQ
        and A.ZXRQ< vNextDay
        and A.ZXR is not null
        and A.LQR is null
      group by B.LPID,D.MDID)
  loop
    update BFCRM10.HYK_JFFHLPJXC
      set ZTSL=ZTSL+item.SL
      where RQ=pRCLRQ
        and  LPID = item.LPID
        and  MDID = item.MDID;

    if SQL%NOTFOUND then
      insert into BFCRM10.HYK_JFFHLPJXC(RQ,LPID,MDID,JHSL,BRSL,BCSL,FFSL,JCSL,ZFSL,SYSL,THSL,ZTSL)--增加作废数量、损益数量和在途数量
        values(pRCLRQ,item.LPID,item.MDID,0,0,0,0,0,0,0,0,item.SL);
    end if;
  end loop;

  /*发放*/
  for item in (
    select B.LPID,C.MDID,sum(B.SL) SL
      from BFCRM10.HYK_JFFLJL A,BFCRM10.HYK_JFFLJL_LP B,BFCRM10.HYK_BGDD C
      where A.JLBH=B.JLBH
        and A.SHRQ>=pRCLRQ
        and A.SHRQ< vNextDay
        and A.SHR is not null
        AND A.CZDD=C.BGDDDM
        AND A.HBFS=0
      group by B.LPID,C.MDID)       --2012.10.19,发放应从此表取数
  loop
    update BFCRM10.HYK_JFFHLPJXC
      set FFSL=FFSL+item.SL
      where RQ=pRCLRQ
        and  LPID = item.LPID
        and  MDID = item.MDID;

    if SQL%NOTFOUND then
      insert into BFCRM10.HYK_JFFHLPJXC(RQ,LPID,MDID,JHSL,FFSL,JCSL,ZFSL,SYSL,THSL,ZTSL) --增加作废数量、损益数量和在途数量
        values(pRCLRQ,item.LPID,item.MDID,0,item.SL,0,0,0,0,0);
    end if;
  end loop;


  /*作废*/
   for item in (
     select B.LPID,C.MDID,sum(B.BFSL) SL
      from BFCRM10.HYK_JFFHLPBFJL A,BFCRM10.HYK_JFFHLPBFJLITEM B,BFCRM10.HYK_BGDD C
      where A.JLBH=B.JLBH
        and A.ZXRQ>=pRCLRQ
        and A.ZXRQ< vNextDay
        and A.ZXR is not null
        AND A.BGDDDM=C.BGDDDM
      group by B.LPID,C.MDID
    )
  loop
    update BFCRM10.HYK_JFFHLPJXC
      set ZFSL=ZFSL+item.SL
      where RQ=pRCLRQ
        and  LPID = item.LPID
        and  MDID = item.MDID;

    if SQL%NOTFOUND then
      insert into BFCRM10.HYK_JFFHLPJXC(RQ,LPID,MDID,JHSL,FFSL,JCSL,ZFSL,SYSL,THSL,ZTSL) --增加作废数量、损益数量和在途数量
        values(pRCLRQ,item.LPID,item.MDID,0,0,0,item.SL,0,0,0);
    end if;
  end loop;



   /*损益*/
   for item in (
     select B.LPID,C.MDID,sum(B.PKSL) SL
      from BFCRM10.HYK_LPPDJL A,BFCRM10.HYK_LPPDJLITEM B,BFCRM10.HYK_BGDD C
      where A.JLBH=B.JLBH
        and A.ZXRQ>=pRCLRQ
        and A.ZXRQ< vNextDay
        and A.ZXR is not null
        AND A.BGDDDM=C.BGDDDM
      group by B.LPID,C.MDID
    )
  loop
    update BFCRM10.HYK_JFFHLPJXC
      set SYSL=SYSL+item.SL
      where RQ=pRCLRQ
        and  LPID = item.LPID
        and  MDID = item.MDID;

    if SQL%NOTFOUND then
      insert into BFCRM10.HYK_JFFHLPJXC(RQ,LPID,MDID,JHSL,FFSL,JCSL,ZFSL,SYSL,THSL,ZTSL) --增加作废数量、损益数量和在途数量
        values(pRCLRQ,item.LPID,item.MDID,0,0,0,0,item.SL,0,0);
    end if;
  end loop;


  /*库存*/
  for item in (
    select C.LPID,D.MDID,SUM(C.KCSL) SL
      from BFCRM10.HYK_JFFHLPKC C ,BFCRM10.HYK_BGDD D
      where C.BGDDDM=D.BGDDDM
        and KCSL<>0
      group by C.LPID,D.MDID)
  loop
    update BFCRM10.HYK_JFFHLPJXC
      set JCSL=JCSL+item.SL
      where RQ=pRCLRQ
        and  LPID = item.LPID
        and  MDID = item.MDID;

    if SQL%NOTFOUND then
      insert into BFCRM10.HYK_JFFHLPJXC(RQ,LPID,MDID,JHSL,FFSL,JCSL,ZFSL,SYSL,THSL,ZTSL) --增加作废数量、损益数量和在途数量
        values(pRCLRQ,item.LPID,item.MDID,0,0,item.SL,0,0,0,0);
    end if;
  end loop;
end;
/

prompt
prompt Creating procedure HYK_PROC_HYK_JFFLGZ
prompt ======================================
prompt
create or replace procedure bfcrm10.HYK_PROC_HYK_JFFLGZ (
                              pRCLRQ date
                            )
is
  vFLGZBH           number(10);
  vSHDM varchar2(10);
  vHYKTYPE          number(5);
  vCLRC             number(5);
  vCLFS             number(5);
  vYHQID             number(10);
  vYHQJSRQ date;
  vJLBH           number(10);
  vFLJLBH           number(10);
  vYEARMONTH        integer;
  vRMBJ             number(5);   /*0每日末*/
  vYMBJ             number(5);   /*1每月末*/
  vZMBJ             number(5);   /*2每季末*/
  vBNMBJ            number(5);   /*3每半年*/
  vNMBJ             number(5);   /*4每年末*/

  cursor curJFCLGZ is
    select JLBH,SHDM,HYKTYPE,CLRC,CLFS,YHQID,
    case when YHQJSRQ is null then case YHQDW when 1 then pRCLRQ+YHQSL else add_months(pRCLRQ,YHQSL) end else YHQJSRQ end
    from BFCRM10.HYK_JFFLGZ where CLFS=2 order by HYKTYPE desc;
  cursor curFLHY is select HYID,WCLJF,FLJE,MDID from TMP_HYK_FL order by HYID;
begin

  vYEARMONTH := to_char(pRCLRQ,'yyyy')*100+to_char(pRCLRQ,'mm');

  /*每月末*/
  if to_char(pRCLRQ+1,'dd')=1 then
    vYMBJ := 1;
  else
    vYMBJ := 0;
  end if;

  /*每季末*/
  if (vYMBJ=1) and (to_char(pRCLRQ,'mm') in (3,6,9,12)) then
    vZMBJ := 1;
  else
    vZMBJ := 0;
  end if;

  /*每半年*/
  if (vYMBJ=1) and (to_char(pRCLRQ,'mm') in (6,12)) then
    vBNMBJ := 1;
  else
    vBNMBJ := 0;
  end if;

  /*每年末*/
  if (vYMBJ=1) and (to_char(pRCLRQ,'mm')=12) then
    vNMBJ :=1;
  else
    vNMBJ  := 0;
  end if;

  open curJFCLGZ;
  fetch curJFCLGZ into vFLGZBH,vSHDM,vHYKTYPE,vCLRC,vCLFS,vYHQID,vYHQJSRQ;
  while (curJFCLGZ%FOUND)
  loop
    if ((vCLRC=0) and (vRMBJ=1)) /*0每日末处理*/
      or ((vCLRC=1) and (vYMBJ=1)) /*1每月末处理*/
      or ((vCLRC=2) and (vZMBJ=1))     /*2每季末*/
      or ((vCLRC=3) and (vBNMBJ=1))    /*3每半年*/
      or ((vCLRC=4) and (vNMBJ=1))     /*4每年末*/
    then
      if vCLFS=2 then
       --BFCRM10.HYK_PROC_HYK_JFFL_A(pRCLRQ,vYEARMONTH,vHYKTYPE,vCLFW,vQZQD);
        delete from TMP_HYK_FL;
        insert into BFCRM10.TMP_HYK_FL(HYID,WCLJF,FLJE,MDID)
          select H.HYID,F.WCLJF,Round(F.WCLJF*I.FLBL,2),MDID from HYK_HYXX H,HYK_JFZH F,HYK_JFFLGZITEM I
          where JLBH=vFLGZBH and H.HYKTYPE=vHYKTYPE and H.HYID=F.HYID and F.WCLJF>=I.JFXX and F.WCLJF<I.JFSX;

        for item in(select HYID,WCLJF,FLJE,MDID from TMP_HYK_FL) loop
          vFLJLBH := BFCRM10.Update_BHZT('HYK_JFFLJL');
          /*insert into BFCRM10.HYK_YHQZH(HYID,YHQID,JSRQ,MDFWDM,JE,CXID)
            select HYID,vYHQID,vYHQJSRQ,' ',FLJE,0 from BFCRM10.TMP_HYK_FL F where not exists(select * from BFCRM10.HYK_YHQZH Y
            where Y.HYID=F.HYID and Y.YHQID=vYHQID and Y.JSRQ=vYHQJSRQ and MDFWDM=' ' and CXID=0);
          update HYK_YHQZH Y set JE=(select JE+FLJE from TMP_HYK_FL F where F.HYID=Y.HYID)
            where exists(select 1 from TMP_HYK_FL F where F.HYID=Y.HYID and Y.YHQID=0 and Y.CXID=0 and Y.MDFWDM=' ' and JSRQ=vYHQJSRQ);*/

          update HYK_YHQZH set JE=JE+round(item.FLJE,2) WHERE HYID=item.HYID AND YHQID=vYHQID AND JSRQ=vYHQJSRQ and MDFWDM=' ' and CXID=0;
          if SQL%NOTFOUND then
            insert into HYK_YHQZH(HYID,YHQID,JSRQ,MDFWDM,JE,CXID)
              values(item.HYID,vYHQID,vYHQJSRQ,' ',round(item.FLJE,2),0);
          end if;

          --vJLBH := BFCRM10.Update_BHZT('HYK_YHQCLJL');
          insert into HYK_YHQCLJL(JYBH,HYID,CLSJ,CLLX,JLBH,YHQID,JSRQ,MDFWDM,MDID,ZY,JFJE,DFJE,YE,CXID)
            select vJLBH,Y.HYID,sysdate,1,vFLJLBH,vYHQID,vYHQJSRQ,' ',item.MDID,'自动积分返利',item.FLJE,0,JE,0
            from HYK_YHQZH Y where Y.HYID=item.HYID and YHQID=vYHQID and JSRQ=vYHQJSRQ and CXID=0 and Y.MDFWDM=' ';

          vJLBH := BFCRM10.Update_BHZT('HYK_JFBDJLMX');
          insert into HYK_JFBDJLMX(JYBH,CZMD,JLBH,HYID,CLSJ,CLLX,WCLJFBD,WCLJF,CZYDM,CZYMC)
            values(vJLBH,item.MDID,vFLJLBH,item.HYID,sysdate,7,-item.WCLJF,0,'','自动积分返利');
          insert into HYK_JFBDJLMX_MD(JYBH,MDID,WCLJFBD,WCLJF)
            select vJLBH,M.MDID,-M.WCLJF,0 from HYK_MDJF M where M.HYID=item.HYID;
        end loop;
      end if;
    end if;

    fetch curJFCLGZ into vFLGZBH,vSHDM,vHYKTYPE,vCLFS,vCLRC,vYHQID,vYHQJSRQ;
  end loop;
  close curJFCLGZ;
end;
/

prompt
prompt Creating procedure HYK_PROC_HYK_JFRBB
prompt =====================================
prompt
create or replace procedure bfcrm10.HYK_PROC_HYK_JFRBB (
                              pRCLRQ in date
                            )
is
  vYEARMONTH integer;
  vPrevDD    date;
  vNextDD    date;
  vQMWCLJF   float;
  vSQWCLJF   float;
  --vHYKTYPE   number(5);
  vTmpWCLJF  float;
  vTZJF_WCLJF float;
begin
  vYEARMONTH := to_char(pRCLRQ,'yyyy')*100+to_char(pRCLRQ,'mm');
  vNextDD := pRCLRQ+1;
  vPrevDD  := pRCLRQ-1;

  update BFCRM10.HYK_JFBDJLMX set CRMJZRQ=pRCLRQ where CRMJZRQ is null; --lwb 2013/3/7

  /*未处理积分日报表*/
  insert into BFCRM10.HYK_WCLJF_RBB(RQ,HYKTYPE,SQWCLJF,WCLJFBD_QTXF,WCLJFBD_BDD,WCLJFBD_TZD,WCLJFBD_JFZC,WCLJFBD_FL_CZ
     ,WCLJFBD_YQMJF,WCLJFBD_JFQL,WCLJFBD_GHKLX,WCLJFBD_SJ,WCLJFBD_JJ,WCLJFBD_CZKSJF,WCLJFBD_WEB,WCLJFBD_JFDX,QMWCLJF,YEARMONTH)
    select pRCLRQ,H.HYKTYPE,0,
           sum((1-abs(sign(CLLX-31))) * (nvl(WCLJFBD,0))) WCLJFBD_QTXF,
           sum((1-abs(sign(CLLX-32))) * (nvl(WCLJFBD,0))) WCLJFBD_BDD,
           sum((1-abs(sign(CLLX-33))) * (nvl(WCLJFBD,0))) WCLJFBD_TZD,
           sum((1-abs(sign(CLLX-34))) * (nvl(WCLJFBD,0))) WCLJFBD_JFZC,
           sum((1-abs(sign(CLLX-35))) * (nvl(WCLJFBD,0))) WCLJFBD_FL_CZ,
           sum((1-abs(sign(CLLX-36))) * (nvl(WCLJFBD,0))) WCLJFBD_YQMJF,
           sum((1-abs(sign(CLLX-37))) * (nvl(WCLJFBD,0))) WCLJFBD_JFQL,
           sum((1-abs(sign(CLLX-38))) * (nvl(WCLJFBD,0))) WCLJFBD_GHKLX,
           sum((1-abs(sign(CLLX-39))) * (nvl(WCLJFBD,0))) WCLJFBD_SJ,
           sum((1-abs(sign(CLLX-40))) * (nvl(WCLJFBD,0))) WCLJFBD_JJ,
           sum((1-abs(sign(CLLX-41))) * (nvl(WCLJFBD,0))) WCLJFBD_CZKSJF,
           sum((1-abs(sign(CLLX-42))) * (nvl(WCLJFBD,0))) WCLJFBD_WEB,
           sum((1-abs(sign(CLLX-43))) * (nvl(WCLJFBD,0))) WCLJFBD_JFDX,
           0,vYEARMONTH
      from BFCRM10.HYK_JFBDJLMX L,BFCRM10.HYK_HYXX H
      where L.HYID=H.HYID
        and L.CRMJZRQ=pRCLRQ
      group by H.HYKTYPE;


  for item in (
    select  D.HYKTYPE
      from  BFCRM10.HYKDEF D,BFCRM10.HYK_HYXX X
      where D.HYKTYPE=X.HYKTYPE
        and D.BJ_JF=1
      group by D.HYKTYPE)
  loop
    /*上期未处理积分*/
    vSQWCLJF := 0;
    begin
      select nvl(QMWCLJF,0) into vSQWCLJF
        from BFCRM10.HYK_WCLJF_RBB
        where RQ=vPrevDD
          and HYKTYPE=item.HYKTYPE;
    exception
      when others then
        vSQWCLJF := 0;
    end;

    /*期末未处理积分*/
    vQMWCLJF := 0;

    select sum(A.WCLJF)
      into vQMWCLJF
      from BFCRM10.HYK_JFZH A,BFCRM10.HYK_HYXX B
      where A.HYID=B.HYID
        and B.HYKTYPE=item.HYKTYPE;

    vQMWCLJF := nvl(vQMWCLJF,0);

    /* -----------更换卡类型调整积分------------------------------------------*/
    vTmpWCLJF := 0;
    vTZJF_WCLJF := 0;

    select sum(B.WCLJF)
      into vTmpWCLJF
      from BFCRM10.HYK_GHKLX A,BFCRM10.HYK_JFZH B
      where A.HYID=B.HYID
        and A.DJSJ >=pRCLRQ
        and A.DJSJ <vNextDD
        and A.HYKTYPE_OLD=item.HYKTYPE;

    vTZJF_WCLJF := vTZJF_WCLJF-nvl(vTmpWCLJF,0);

    vTmpWCLJF := 0;

    select sum(B.WCLJF)
      into vTmpWCLJF
      from BFCRM10.HYK_GHKLX A,BFCRM10.HYK_JFZH B
      where A.HYID=B.HYID
        and A.DJSJ >=pRCLRQ
        and A.DJSJ <vNextDD
        and A.HYKTYPE_NEW=item.HYKTYPE;

    vTZJF_WCLJF := vTZJF_WCLJF+nvl(vTmpWCLJF,0);

    update BFCRM10.HYK_WCLJF_RBB
       set SQWCLJF=vSQWCLJF,
           QMWCLJF=vQMWCLJF
        --   WCLJFBD_GHKLX=WCLJFBD_GHKLX+nvl(vTZJF_WCLJF,0)
      where RQ=pRCLRQ
        and HYKTYPE=item.HYKTYPE;

    if SQL%NOTFOUND then
      insert into BFCRM10.HYK_WCLJF_RBB(RQ,HYKTYPE,SQWCLJF,WCLJFBD_GHKLX,WCLJFBD_QTXF,WCLJFBD_BDD,WCLJFBD_TZD,WCLJFBD_JFZC,WCLJFBD_FL_CZ
     ,WCLJFBD_YQMJF,WCLJFBD_JFQL,WCLJFBD_SJ,WCLJFBD_JJ,WCLJFBD_CZKSJF,WCLJFBD_WEB,WCLJFBD_JFDX,QMWCLJF,YEARMONTH)
        values(pRCLRQ,item.HYKTYPE,vSQWCLJF,nvl(vTZJF_WCLJF,0),0,0,0,0,0,0,0,0,0,0,0,0,vQMWCLJF,vYEARMONTH);
    end if;

     /* -----------会员卡升级调整积分------------------------------------------*/
    vTmpWCLJF := 0;
    vTZJF_WCLJF := 0;

    select sum(B.WCLJF)
      into vTmpWCLJF
      from BFCRM10.HYK_SJJL A,BFCRM10.HYK_JFZH B
      where A.HYID=B.HYID
        and A.ZXRQ >=pRCLRQ
        and A.ZXRQ <vNextDD
        and A.HYKTYPE_OLD=item.HYKTYPE;

    vTZJF_WCLJF := vTZJF_WCLJF-nvl(vTmpWCLJF,0);

    select sum(B.WCLJF)
      into vTmpWCLJF
      from BFCRM10.HYK_SJJL A,BFCRM10.HYK_JFZH B
      where A.HYID=B.HYID
        and A.ZXRQ >=pRCLRQ
        and A.ZXRQ <vNextDD
        and A.HYKTYPE_NEW=item.HYKTYPE;

    vTZJF_WCLJF := vTZJF_WCLJF+nvl(vTmpWCLJF,0);

    update BFCRM10.HYK_WCLJF_RBB
       set SQWCLJF=vSQWCLJF,
           QMWCLJF=vQMWCLJF
          -- WCLJFBD_GHKLX=WCLJFBD_GHKLX+nvl(vTZJF_WCLJF,0)
      where RQ=pRCLRQ
        and HYKTYPE=item.HYKTYPE;

    if SQL%NOTFOUND then
      insert into BFCRM10.HYK_WCLJF_RBB(RQ,HYKTYPE,SQWCLJF,WCLJFBD_GHKLX,WCLJFBD_QTXF,WCLJFBD_BDD,WCLJFBD_TZD,WCLJFBD_JFZC,WCLJFBD_FL_CZ
     ,WCLJFBD_YQMJF,WCLJFBD_JFQL,WCLJFBD_SJ,WCLJFBD_JJ,WCLJFBD_CZKSJF,WCLJFBD_WEB,WCLJFBD_JFDX,QMWCLJF,YEARMONTH)
        values(pRCLRQ,item.HYKTYPE,vSQWCLJF,nvl(vTZJF_WCLJF,0),0,0,0,0,0,0,0,0,0,0,0,0,vQMWCLJF,vYEARMONTH);
    end if;

  end loop;
end;
/

prompt
prompt Creating procedure HYK_PROC_HYK_JFZHMONTH
prompt =========================================
prompt
create or replace procedure bfcrm10.HYK_PROC_HYK_JFZHMONTH (pProcDate in date)      /* 日期 */
as
begin
  update BFCRM10.HYK_XFJL
    set CRMJZRQ=pProcDate
    where STATUS=1
      and CRMJZRQ is null;

  -- 消费记录中的积分变动
  delete from BFCRM10.TMP_HYK_JFBDJLMX;

  insert into BFCRM10.TMP_HYK_JFBDJLMX(HYID,JLBH,WCLJFBD,BQJFBD,BNLJJFBD)
    select L.HYID,1 AS JLBH,sum(round(L.JF,4)) WCLJFBD,sum(round(L.JF,4)) BQJFBD,sum(round(L.JF,4)) BNLJJFBD
      from BFCRM10.HYK_XFJL L
      where L.CRMJZRQ=pProcDate
        and nvl(L.JF,0) <> 0
        and (L.HYID >0)
        and L.STATUS=1
      group by L.HYID;

  /*insert into BFCRM10.HYK_JFBDJLMX(HYID,JYBH,CLLX,JLBH,WCLJFBD)
    select HYID,10,JLBH,round(WCLJFBD,4)
      from BFCRM10.TMP_HYK_JFBDJLMX;*/

  /*--------------汇用钱买积分－－－－－－－－－－－*/

  insert into BFCRM10.HYK_JFBDJLMX(HYID,CLLX,JLBH,WCLJFBD)
    select HYID,11,JLBH,round(SMJJF,4)
      from BFCRM10.CRMTHMJFJL
      where ZXRQ=pProcDate;

/********汇总积分变动******************/
 /* select @YEARMONTH=datepart(YEAR,pProcDate)*100+datePart(MONTH,pProcDate)
  delete BFCRM10.TMP_HYK_JFBDJLMX

  insert into BFCRM10.TMP_HYK_JFBDJLMX(HYID,JLBH,WCLJFBD,BQJFBD,BNLJJFBD)
  select X.HYID,1,
         sum(nvl(round(X.WCLJFBD,4),0)) WCLJFBD,
         sum(nvl(round(X.BQJFBD,4),0)) BQJFBD,
         sum(nvl(round(X.BNLJJFBD,4),0)) BNLJJFBD
    from BFCRM10.HYK_JFBDJLMX X
   where X.RQ=pProcDate
  group by X.HYID


  insert into BFCRM10.HYK_JFBDJL(HYID,RQ,YEARMONTH,WCLJFBD,BQJFBD,WCLJF,BQJF,BNLJJF)
  select X.HYID,
         pProcDate,
         @YEARMONTH,
         sum(nvl(round(X.WCLJFBD,4),0)) WCLJFBD,
         sum(nvl(round(X.BQJFBD,4),0)) BQJFBD,
         sum(nvl(H.WCLJF,0)) WCLJF,
         sum(nvl(H.BQJF,0)) BQJF ,
         sum(nvl(H.BNLJJF,0)) BNLJJF
    from BFCRM10.TMP_HYK_JFBDJLMX X,
         BFCRM10.HYK_JFZH H
   where X.HYID=H.HYID
  group by X.HYID

  delete BFCRM10.TMP_HYK_JFBDJLMX */
end;
/

prompt
prompt Creating procedure HYK_PROC_HYK_KCCZKBGZ
prompt ========================================
prompt
create or replace procedure bfcrm10.HYK_PROC_HYK_KCCZKBGZ (
                               pRCLRQ  in date,
                               pNY in number
                            )
as
  vNextDD      date;
  vCount       integer;
begin
  select count(*) into vCount from BFCRM10.HYK_KCCZKBGZ where RQ=pRCLRQ;
  if vCount>0 then
    return;
  end if;

  vNextDD := pRCLRQ+1;

  /*上期余额*/
  insert into BFCRM10.HYK_KCCZKBGZ(RQ,BGDDDM,HYKTYPE,MZJE,QCSL,QCJE,JKSL,JKJE,BRSL,BRJE,BCSL,
        BCJE,FSSL,FSJE,FSTSSL,FSTSJE,XFTSSL,XFTSJE,ZFSL,ZFJE,JCSL,JCJE,TZSL,TZJE,YEARMONTH)
    select pRCLRQ,BGDDDM,HYKTYPE,MZJE,JCSL,JCJE,0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,pNY
      from BFCRM10.HYK_KCCZKBGZ
      where RQ=pRCLRQ-1;

  /*建卡*/
  for item in  (
    select J.BGDDDM,J.HYKTYPE,J.QCYE,count(I.JLBH) SL,sum(nvl(I.JE,0)) JE
      from BFCRM10.HYKJKJL  J,BFCRM10.HYKJKJLITEM I
      where J.JLBH=I.JLBH
        and J.ZXRQ>=pRCLRQ
        and J.ZXRQ <vNextDD
      group by J.BGDDDM,J.HYKTYPE,J.QCYE)
  LOOP
    update BFCRM10.HYK_KCCZKBGZ
      set JKSL=JKSL+item.SL,
          JKJE=JKJE+item.JE
      where RQ=pRCLRQ
       and  BGDDDM = item.BGDDDM
       and  HYKTYPE = item.HYKTYPE
       and  MZJE = item.QCYE;

    if SQL%NOTFOUND then
      insert into BFCRM10.HYK_KCCZKBGZ(RQ,BGDDDM,HYKTYPE,MZJE,JKSL,JKJE,YEARMONTH)
        values(pRCLRQ,item.BGDDDM,item.HYKTYPE,item.QCYE,item.SL,item.JE,pNY);
    end if;
  end LOOP;

  /*写卡*/
  for item in (
      select J.BGDDDM,J.HYKTYPE,J.QCYE,count(I.JLBH) SL,sum(nvl(I.JE,0)) JE
        from BFCRM10.HYKJKJL  J,BFCRM10.HYKJKJLITEM I
        where J.JLBH=I.JLBH
          and I.XKRQ>=pRCLRQ
          and I.XKRQ <vNextDD
        group by J.BGDDDM,J.HYKTYPE,J.QCYE)
  LOOP
    update BFCRM10.HYK_KCCZKBGZ
      set XKSL=XKSL+item.SL,
          XKJE=XKJE+item.JE
      where RQ=pRCLRQ
        and BGDDDM = item.BGDDDM
        and HYKTYPE = item.HYKTYPE
        and MZJE = item.QCYE;
    if SQL%NOTFOUND then
      insert into BFCRM10.HYK_KCCZKBGZ(RQ,BGDDDM,HYKTYPE,MZJE,XKSL,XKJE,YEARMONTH)
        values(pRCLRQ,item.BGDDDM,item.HYKTYPE,item.QCYE,item.SL,item.JE,pNY);
    end if;
  end LOOP;

  /*拨入*/
  for item in (
      select J.BGDDDM_BR as BGDDDM,I.HYKTYPE,I.JE QCYE,count(I.JLBH) SL,sum(I.JE) JE
        from BFCRM10.CARDLQJL J,BFCRM10.CARDLQJLITEM I
        where J.JLBH=I.JLBH
          and J.ZXRQ>=pRCLRQ
          and J.ZXRQ <vNextDD
        group by  J.BGDDDM_BR,I.HYKTYPE,I.JE)
  LOOP
    update BFCRM10.HYK_KCCZKBGZ
      set BRSL=BRSL+item.SL,BRJE=BRJE+item.JE
      where RQ=pRCLRQ
        and  BGDDDM  = item.BGDDDM
        and  HYKTYPE = item.HYKTYPE
        and  MZJE    = item.QCYE;
    if SQL%NOTFOUND then
      insert into BFCRM10.HYK_KCCZKBGZ(RQ,BGDDDM,HYKTYPE,MZJE,BRSL,BRJE,YEARMONTH)
        values(pRCLRQ,item.BGDDDM,item.HYKTYPE,item.QCYE,item.SL,item.JE,pNY);
    end if;
  end LOOP;

  /*拨出*/
  for item in (
      select J.BGDDDM_BC as BGDDDM,I.HYKTYPE,I.JE QCYE,count(I.JLBH) SL,sum(I.JE) JE
        from BFCRM10.CARDLQJL J,BFCRM10.CARDLQJLITEM I
        where J.JLBH=I.JLBH
          and J.ZXRQ>=pRCLRQ
          and J.ZXRQ <vNextDD
        group by  J.BGDDDM_BC,I.HYKTYPE,I.JE)
  LOOP
    update BFCRM10.HYK_KCCZKBGZ
      set BCSL=BCSL+item.SL,
          BCJE=BCJE+item.JE
      where RQ=pRCLRQ
        and  BGDDDM = item.BGDDDM
        and  HYKTYPE = item.HYKTYPE
        and  MZJE = item.QCYE;

    if SQL%NOTFOUND then
      insert into BFCRM10.HYK_KCCZKBGZ(RQ,BGDDDM,HYKTYPE,MZJE,BCSL,BCJE,YEARMONTH)
        values(pRCLRQ,item.BGDDDM,item.HYKTYPE,item.QCYE,item.SL,item.JE,pNY);
    end if;
  end LOOP;

    /*(换卡)*/
  for item in (
      select J.BGDDDM,J.HYKTYPE,nvl(J.QCYE,0) QCYE,count(J.CZJPJ_JLBH ) SL,sum(nvl(J.QCYE,0)) JE
        from BFCRM10.HYK_CZK_WK J
        where J.DJSJ >=pRCLRQ
          and J.DJSJ  <vNextDD
        group by  J.BGDDDM,J.HYKTYPE,J.QCYE)
  LOOP
    update BFCRM10.HYK_KCCZKBGZ
      set HKSL=HKSL+item.SL,
          HKJE=HKJE+item.JE
      where RQ=pRCLRQ
        and BGDDDM = item.BGDDDM
        and HYKTYPE = item.HYKTYPE
        and MZJE = item.QCYE;
    if SQL%NOTFOUND then
      insert into BFCRM10.HYK_KCCZKBGZ(RQ,BGDDDM,HYKTYPE,MZJE,HKSL,HKJE,YEARMONTH)
        values(pRCLRQ,item.BGDDDM,item.HYKTYPE,item.QCYE,item.SL,item.JE,pNY);
    end if;
  end LOOP;

  for item in (
      select J.BGDDDM,J.HYKTYPE_NEW as HYKTYPE,nvl(J.QCYE,0) QCYE,count(J.JLBH ) SL,sum(nvl(J.QCYE,0)) JE
        from BFCRM10.HYK_GHKLX J
        where J.DJSJ>=pRCLRQ
          and J.DJSJ< vNextDD
        group by J.BGDDDM,J.HYKTYPE_NEW,nvl(J.QCYE,0))
  LOOP
    update BFCRM10.HYK_KCCZKBGZ
      set HKSL=HKSL+item.SL,HKJE=HKJE+item.JE
      where RQ=pRCLRQ
        and  BGDDDM = item.BGDDDM
        and  HYKTYPE = item.HYKTYPE
        and  MZJE = item.QCYE;
    if SQL%NOTFOUND then
     insert into BFCRM10.HYK_KCCZKBGZ(RQ,BGDDDM,HYKTYPE,MZJE,HKSL,HKJE,YEARMONTH)
      values(pRCLRQ,item.BGDDDM,item.HYKTYPE,item.QCYE,item.SL,item.JE,pNY);
    end if;
  end LOOP;

  /*升级换卡  */
  for item in (
      select J.BGDDDM,J.HYKTYPE_NEW as HYKTYPE, 0 QCYE,count(J.JLBH ) SL
        from BFCRM10.HYK_SJJL J
        where J.ZXRQ >=pRCLRQ
          and J.ZXRQ  <vNextDD
        group by  J.BGDDDM,J.HYKTYPE_NEW)
  loop
    update BFCRM10.HYK_KCCZKBGZ
      set HKSL=HKSL+item.SL
      where RQ=pRCLRQ
        and  BGDDDM = item.BGDDDM
        and  HYKTYPE = item.HYKTYPE
        and  MZJE = item.QCYE;
    if SQL%NOTFOUND then
     insert into BFCRM10.HYK_KCCZKBGZ(RQ,BGDDDM,HYKTYPE,MZJE,HKSL,HKJE,YEARMONTH)
      values(pRCLRQ,item.BGDDDM,item.HYKTYPE,0,item.SL,0,pNY);
    end if;
  end loop;



   /*售卡*/
  for item in (
    select A.BGDDDM,B.HYKTYPE,nvl(B.QCYE,0) QCYE,sum(1) SL, sum(B.QCYE) JE
      from BFCRM10.HYK_CZKSKJL A,BFCRM10.HYK_CZKSKJLITEM B
      where A.JLBH=B.JLBH
        and ZXRQ >=pRCLRQ
        and ZXRQ <vNextDD
        and STATUS>=1
        and A.FS in (0,1)
      group by A.BGDDDM,B.HYKTYPE,nvl(B.QCYE,0))
  loop
    update BFCRM10.HYK_KCCZKBGZ
      set FSSL=FSSL+item.SL,
          FSJE=FSJE+item.JE
      where RQ=pRCLRQ
        and  HYKTYPE = item.HYKTYPE
        and  BGDDDM = item.BGDDDM
        and  MZJE=item.QCYE;

    if SQL%NOTFOUND then
     insert into BFCRM10.HYK_KCCZKBGZ(RQ,HYKTYPE,BGDDDM,MZJE,FSSL,FSJE,YEARMONTH)
      values(pRCLRQ,item.HYKTYPE,item.BGDDDM,item.QCYE,item.SL,item.JE,pNY);
    end if;
  end loop;

  /*退售卡*/
  for item in (
    select B.HYKTYPE,A.BGDDDM,nvl(B.QCYE,0) QCYE,sum(1) SL,sum(B.QCYE) JE
    from BFCRM10.HYK_CZKSKJL A,BFCRM10.HYK_CZKSKJLITEM B
    where A.JLBH=B.JLBH
      and QDSJ>= pRCLRQ
      and QDSJ<vNextDD
      and STATUS>1
      and A.FS=2
    group by B.HYKTYPE,A.BGDDDM,nvl(B.QCYE,0))
  loop
     update BFCRM10.HYK_KCCZKBGZ
     set FSTSSL=FSTSSL+item.SL,
         FSTSJE=FSTSJE+item.JE
     where RQ=pRCLRQ
      and  HYKTYPE = item.HYKTYPE
      and  BGDDDM = item.BGDDDM
      and  MZJE=item.QCYE;

    if SQL%NOTFOUND then
      insert into BFCRM10.HYK_KCCZKBGZ(RQ,HYKTYPE,BGDDDM,MZJE,YEARMONTH,FSTSSL,FSTSJE)
      values(pRCLRQ,item.HYKTYPE,item.BGDDDM,item.QCYE,pNY,item.SL,item.JE);
    end if;

  end loop;

  /*回收卡*/
  for item in (
    select A.HYKTYPE,X.YBGDD BGDDDM,0 QCYE,sum(1) SL,0 JE
    from BFCRM10.HYK_TK A,BFCRM10.HYK_TK_ITEM B,BFCRM10.HYK_HYXX X
    where A.JLBH=B.JLBH
      and B.HYID=X.HYID
      and ZXRQ>=pRCLRQ
      and ZXRQ< vNextDD
      and A.TKFS=2
    group by A.HYKTYPE,X.YBGDD)
  loop
     update BFCRM10.HYK_KCCZKBGZ
     set XFTSSL=XFTSSL+item.SL,
         XFTSJE=XFTSJE+item.JE
     where RQ=pRCLRQ
      and  HYKTYPE = item.HYKTYPE
      and  BGDDDM = item.BGDDDM
      and  MZJE=item.QCYE;

    if SQL%NOTFOUND then
      insert into BFCRM10.HYK_KCCZKBGZ(RQ,HYKTYPE,BGDDDM,MZJE,YEARMONTH,XFTSSL,XFTSJE)
      values(pRCLRQ,item.HYKTYPE,item.BGDDDM,item.QCYE,pNY,item.SL,item.JE);
    end if;
  end loop;


  /*作废卡*/
  for item in (
    select A.HYKTYPE,A.BGDDDM,nvl(B.QCYE,0) QCYE,sum(1) SL,sum(B.QCYE) JE
    from BFCRM10.HYK_KCKZFJL A, BFCRM10.HYK_KCKZFJLITEM B
    where A.ZXRQ>=pRCLRQ
      and A.ZXRQ <vNextDD
      and nvl(A.ZXR,0)>0
      and A.JLBH=B.JLBH
      and A.BJ_HF=0
    group by A.HYKTYPE,BGDDDM,nvl(B.QCYE,0))

  loop
     update BFCRM10.HYK_KCCZKBGZ
     set ZFSL=ZFSL+item.SL,
         ZFJE=ZFJE+item.JE
     where RQ=pRCLRQ
      and  HYKTYPE = item.HYKTYPE
      and  BGDDDM = item.BGDDDM
      and  MZJE=item.QCYE;

    if SQL%NOTFOUND then
      insert into BFCRM10.HYK_KCCZKBGZ(RQ,HYKTYPE,BGDDDM,MZJE,YEARMONTH,ZFSL,ZFJE)
      values(pRCLRQ,item.HYKTYPE,item.BGDDDM,item.QCYE,pNY,item.SL,item.JE);
    end if;
  end loop;

    /*作废卡恢复*/
  for item in (
    select A.HYKTYPE,A.BGDDDM,nvl(B.QCYE,0) QCYE,sum(-1) SL,sum(B.QCYE *(-1)) JE
    from BFCRM10.HYK_KCKZFJL A, BFCRM10.HYK_KCKZFJLITEM B
    where A.ZXRQ>=pRCLRQ
      and A.ZXRQ< vNextDD
      and nvl(A.ZXR,0)>0
      and A.JLBH=B.JLBH
      and A.BJ_HF=1
    group by A.HYKTYPE,BGDDDM,nvl(B.QCYE,0))
  loop
    update BFCRM10.HYK_KCCZKBGZ
     set ZFSL=ZFSL+item.SL,
         ZFJE=ZFJE+item.JE
     where RQ=pRCLRQ
      and  HYKTYPE = item.HYKTYPE
      and  BGDDDM = item.BGDDDM
      and  MZJE=item.QCYE;

    if SQL%NOTFOUND then
      insert into BFCRM10.HYK_KCCZKBGZ(RQ,HYKTYPE,BGDDDM,MZJE,YEARMONTH,ZFSL,ZFJE)
      values(pRCLRQ,item.HYKTYPE,item.BGDDDM,item.QCYE,pNY,item.SL,item.JE);
    end if;
  end loop;

/*调整金额*/
  for item in (
    select J.BGDDDM,J.HYKTYPE,nvl(I.QCYE_OLD,0) QCYE,count(I.JLBH) SL,sum(nvl(I.QCYE_OLD,0)) JE
      from BFCRM10.CARD_YETZD  J,BFCRM10.CARD_YETZDITEM I
      where J.JLBH=I.JLBH
        and J.ZXRQ>=pRCLRQ
        and J.ZXRQ< vNextDD
    group by J.BGDDDM,J.HYKTYPE,nvl(I.QCYE_OLD,0))
  loop
     update BFCRM10.HYK_KCCZKBGZ
     set TZSL=TZSL+item.SL*(-1),TZJE=TZJE+item.JE*(-1)
     where RQ=pRCLRQ
      and  BGDDDM = item.BGDDDM
      and  HYKTYPE = item.HYKTYPE
      and  MZJE = item.QCYE;

    if SQL%NOTFOUND then
     insert into BFCRM10.HYK_KCCZKBGZ(RQ,BGDDDM,HYKTYPE,MZJE,TZSL,TZJE,YEARMONTH)
      values(pRCLRQ,item.BGDDDM,item.HYKTYPE,item.QCYE,item.SL * (-1),item.JE * (-1),pNY);
    end if;
  end loop;

  for item in (
    select J.BGDDDM,J.HYKTYPE,nvl(I.QCYE_NEW,0) QCYE,count(I.JLBH) SL,sum(nvl(I.QCYE_NEW,0)) JE
    from BFCRM10.CARD_YETZD  J,BFCRM10.CARD_YETZDITEM I
      where J.JLBH=I.JLBH
        and J.ZXRQ>=pRCLRQ
        and J.ZXRQ< vNextDD
    group by J.BGDDDM,J.HYKTYPE,nvl(I.QCYE_NEW,0))
  loop
     update BFCRM10.HYK_KCCZKBGZ
     set TZSL=TZSL+item.SL,TZJE=TZJE+item.JE
     where RQ=pRCLRQ
      and  BGDDDM = item.BGDDDM
      and  HYKTYPE = item.HYKTYPE
      and  MZJE = item.QCYE;

    if SQL%NOTFOUND then
     insert into BFCRM10.HYK_KCCZKBGZ(RQ,BGDDDM,HYKTYPE,MZJE,TZSL,TZJE,YEARMONTH)
      values(pRCLRQ,item.BGDDDM,item.HYKTYPE,item.QCYE,item.SL,item.JE,pNY);
    end if;
  end loop;


  /*    结存卡*/
  for item in (
    select HYKTYPE,BGDDDM,nvl(QCYE,0) QCYE,sum(1) SL ,sum(QCYE) JE
    from BFCRM10.HYKCARD
    --where XKRQ is not null
    group by HYKTYPE,BGDDDM,nvl(QCYE,0))
  loop
     update BFCRM10.HYK_KCCZKBGZ
     set JCSL=JCSL+item.SL,
         JCJE=JCJE+item.JE
     where RQ=pRCLRQ
      and  HYKTYPE = item.HYKTYPE
      and  BGDDDM = item.BGDDDM
      and  MZJE=item.QCYE;

    if SQL%NOTFOUND then
      insert into BFCRM10.HYK_KCCZKBGZ(RQ,HYKTYPE,BGDDDM,MZJE,YEARMONTH,JCSL,JCJE)
      values(pRCLRQ,item.HYKTYPE,item.BGDDDM,item.QCYE,pNY,item.SL,item.JE);
    end if;
  end loop;
end;
/

prompt
prompt Creating procedure HYK_PROC_HYK_XDKPOSXFJL
prompt ==========================================
prompt
create or replace procedure bfcrm10.HYK_PROC_HYK_XDKPOSXFJL (
                              vSKTNO in varchar2,     --收款台号
                              vJLBH  in integer       --交易号
                            )
is
  vCount integer;
  vNIAN        integer;
  vNIAN_next   integer;
  vTmpNIAN     integer;
  vYUE         number(5);
  vYUE_next    number(5);
  vTmpYUE      number(5);
  vXDKID       integer;
  vXDKXFJE     number(14,2);
  vOLD_SKTNO   varchar2(3);
  vOLD_JLBH    integer;
  vJYSJ        date;
  vHKQ         number(5);  /*还款期(月个数)*/
  vYHDJE       number(14,2);       /*月还贷金额*/
  vSumYHDJE    number(14,2);
  vTmpHKQ      number(5);
  vYHID        number(5);
  vTmpSumI     number(5);
  vSumRec      number(5);

  cursor curYMX is
      select NIAN,YUE
        from BFCRM10.HYK_XDKSKTSJ_YDFJ
        where SKTNO=vOLD_SKTNO
          and JLBH=vOLD_JLBH
      order by NIAN,YUE;
begin
  /*此记录已处理或不存在*/
  select count(*) into vCount
    from HYK_XDKPOSXFJL
    where SKTNO=vSKTNO
      and JLBH=vJLBH
      and nvl(CLBJ,0)=0;
  if vCount>0 then
    raise_application_error(-20001,'此记录已处理或不存在');
    return;
  end if;


  select  XDKID, XDKXFJE, OLD_SKTNO, OLD_JLBH, JYSJ
    into  vXDKID,vXDKXFJE,vOLD_SKTNO,vOLD_JLBH,vJYSJ
    from  BFCRM10.HYK_XDKPOSXFJL
    where SKTNO=vSKTNO
      and JLBH=vJLBH
      and nvl(CLBJ,0)=0;

  vNIAN := to_char(vJYSJ,'yyyy');
  vYUE  := to_char(vJYSJ,'mm');

  if vYUE=12 then
    vNIAN_next := vNIAN+1;
    vYUE_next  := 1;
  else
    vNIAN_next := vNIAN;
    vYUE_next  := vYUE+1;
  end if;

  select YHID into vYHID from BFCRM10.HYK_XDKXX  where XDKID=vXDKID;

  select HKQ into vHKQ from BFCRM10.YHBM where YHID=vYHID;

  vHKQ := nvl(vHKQ,12);
  if vHKQ<=0 then
    vHKQ := 12;
  end if;

  vYHDJE := round(vXDKXFJE/vHKQ,2);

  /*正消费*/
  if vXDKXFJE>0 then
    update BFCRM10.HYK_XDKXX
      set XFJE=XFJE+vXDKXFJE
      where XDKID=vXDKID;

    insert into BFCRM10.HYK_XDKSKTSJ(SKTNO,JLBH,XDKID,XFJE,RQ)
      values(vSKTNO,vJLBH,vXDKID,vXDKXFJE,vJYSJ);

    vSumYHDJE := 0;
    vTmpHKQ := 1;
    vTmpNIAN := vNIAN_next;
    vTmpYUE := vYUE_next;
    while (vTmpHKQ<=vHKQ)
    loop
      if vTmpHKQ<vHKQ then
        insert into BFCRM10.HYK_XDKSKTSJ_YDFJ(SKTNO,JLBH,NIAN,YUE,YHDJE)
          values(vSKTNO,vJLBH,vTmpNIAN,vTmpYUE,vYHDJE);
        vSumYHDJE := vSumYHDJE+vYHDJE;
      else
        vYHDJE := vXDKXFJE-vSumYHDJE;
        insert into BFCRM10.HYK_XDKSKTSJ_YDFJ(SKTNO,JLBH,NIAN,YUE,YHDJE)
          values(vSKTNO,vJLBH,vTmpNIAN,vTmpYUE,vYHDJE);
      end if;
      if vTmpYUE=12 then
        vTmpNIAN := vTmpNIAN+1;
        vTmpYUE := 1;
      else
        vTmpYUE := vTmpYUE+1;
      end if;
      vTmpHKQ := vTmpHKQ+1;
    end loop;
  end if;  /*完成正消费*/

  /*负消费（POS退货）*/
  if vXDKXFJE<0 then
    select count(*) into vSumRec
      from BFCRM10.HYK_XDKSKTSJ_YDFJ
      where SKTNO=vOLD_SKTNO
        and JLBH=vOLD_JLBH;

    vSumYHDJE := 0;
    vTmpSumI  := 0;
    vTmpHKQ  := 0;

    Open curYMX;
    fetch curYMX into vTmpNIAN,vTmpYUE;
    while (curYMX%FOUND)
    loop
      vTmpHKQ := vTmpHKQ+1;

      if (vTmpNIAN*100+vTmpYUE)<=(vNIAN*100+vYUE) then
        vTmpSumI  := vTmpSumI+1;
        vSumYHDJE := vSumYHDJE+vYHDJE;
      elsif vTmpHKQ<vSumRec then
        update BFCRM10.HYK_XDKSKTSJ_YDFJ
          set YHDJE = YHDJE+vYHDJE  /*注意: vYHDJE<0*/
          where SKTNO=vOLD_SKTNO
            and JLBH=vOLD_JLBH
            and NIAN=vTmpNIAN
            and YUE=vTmpYUE;

        vSumYHDJE := vSumYHDJE+vYHDJE;
      else
        update BFCRM10.HYK_XDKSKTSJ_YDFJ
          set YHDJE=YHDJE+vXDKXFJE-vSumYHDJE  /*注意: vXDKXFJE<0,vSumYHDJE<0*/
          where SKTNO=vOLD_SKTNO
            and JLBH=vOLD_JLBH
            and NIAN=vTmpNIAN
            and YUE=vTmpYUE;
      end if;
      fetch curYMX into vTmpNIAN,vTmpYUE;
    end loop;
    close curYMX;

    /*退返金额*/
    update BFCRM10.HYK_XDKSKTSJ_YDFJ
      set TFJE=TFJE-(vYHDJE*vTmpSumI)  /*注意: vYHDJE<0*/
      where SKTNO=vOLD_SKTNO
        and JLBH=vOLD_JLBH
        and NIAN=vNIAN_next
        and YUE=vYUE_next;

     update BFCRM10.HYK_XDKSKTSJ
       set THJE=THJE-vXDKXFJE,TFJE=TFJE-(vYHDJE*vTmpSumI)
       where SKTNO=vOLD_SKTNO
         and JLBH=vOLD_JLBH;

     update BFCRM10.HYK_XDKXX
       set XFJE=XFJE+vXDKXFJE     /*vXDKXFJE<0*/
       where XDKID=vXDKID;

  end if;  /*完成负消费（POS退货）*/

  update  BFCRM10.HYK_XDKPOSXFJL
    set CLBJ=1
    where SKTNO=vSKTNO
      and JLBH=vJLBH;
end;
/

prompt
prompt Creating procedure HYK_PROC_HYK_XDKYBB
prompt ======================================
prompt
create or replace procedure bfcrm10.HYK_PROC_HYK_XDKYBB (
                              pProcDate in date,
                              vDJR      in integer
                            )
as
  vCount     integer;
  vIsFirst   integer;
  vNIAN      integer;
  vYUE       integer;
  vYHID      integer;   /*银行ID*/
  vXDKID     integer;   /*信贷卡ID*/
  vYHZH      varchar2(30);
  vKKDJLBH   integer;
  vLLHDJE    number(14,2);
  vDJRMC     varchar2(16);
  vsumJFJE   number(14,2);
  vsumDFJE   number(14,2);
  vYE        number(14,2);
  vHDJE      number(14,2);
  vMYHDJE    number(14,2);

  cursor curKKD is
    select distinct B.XDKID,C.YHZH,(A.YHDJE-A.LLHDJE) LLHDJE
      from BFCRM10.HYK_XDKSKTSJ_YDFJ A, BFCRM10.HYK_XDKSKTSJ B,BFCRM10.HYK_XDKXX C
      where A.NIAN=vNIAN and A.YUE=vYUE  and A.YHDJE>A.LLHDJE
        and A.SKTNO=B.SKTNO and A.JLBH=B.JLBH
        and B.XDKID=C.XDKID and C.YHID=vYHID;

  cursor curYHID is
    select distinct C.YHID
      from BFCRM10.HYK_XDKSKTSJ_YDFJ A, BFCRM10.HYK_XDKSKTSJ B,BFCRM10.HYK_XDKXX C
      where A.NIAN=vNIAN and A.YUE=vYUE
        and A.SKTNO=B.SKTNO and A.JLBH=B.JLBH
        and B.XDKID=C.XDKID;
begin
  vNIAN := to_char(pProcDate,'yyyy');
  vYUE  := to_char(pProcDate,'mm');

  --不是月末,退岀
  if to_char(sysdate+1,'dd')<>1 then
    return;
  end if;

  /*本月已生成扣款单*/
  select count(*) into vCount from BFCRM10.HYK_XDKKKD where DJLX=0 and NIAN=vNIAN and YUE=vYUE;
  if vCount>0 then
    return;
  end if;

  begin
    select PERSON_NAME into vDJRMC
      from BFCRM10.RYXX
      where PERSON_ID=vDJR;
  exception
    when others then
      vDJRMC := ' ';
  end;



  open curYHID;
  fetch curYHID into vYHID;
  while (curYHID%FOUND)
  loop
    /*扣款单*/
    vIsFirst := 1;
    vsumJFJE := 0;
    vsumDFJE := 0;

    open curKKD;
    fetch curKKD into vXDKID,vYHZH,vLLHDJE;
    while (curKKD%FOUND)
    loop
      if vIsFirst=1 then
        vIsFirst := 0;

        vKKDJLBH := BFCRM10.Update_BHZT('HYK_XDKKKD');

        insert into BFCRM10.HYK_XDKKKD(JLBH,DJLX,NIAN,YUE,YHID,JFJE,DFJE,DJR,DJRMC,DJSJ)
          values(vKKDJLBH,0,vNIAN,vYUE,vYHID,0,0,vDJR,vDJRMC,sysdate);
      end if;

      insert into BFCRM10.HYK_XDKKKDMX(JLBH,XDKID,YHZH,NIAN,YUE,JFJE,DFJE,STATUS,DZID,DZSJ)
         values(vKKDJLBH,vXDKID,vYHZH,vNIAN,vYUE,0,vLLHDJE,0,null,null);

      vsumDFJE := vsumDFJE+vLLHDJE;

      fetch curKKD into vXDKID,vYHZH,vLLHDJE;
    end loop;
    close curKKD;

    update BFCRM10.HYK_XDKKKD
      set DFJE=vsumDFJE
      where JLBH=vKKDJLBH;

    fetch curYHID into vYHID;
  end loop;
  close curYHID;
end;
/

prompt
prompt Creating procedure HYK_PROC_HYK_YHQ_QTCLRBB
prompt ===========================================
prompt
create or replace procedure bfcrm10.HYK_PROC_HYK_YHQ_QTCLRBB (
                              pProcDate in date
                            )
as
  vYEARMONTH integer;
  vNextDay   date;
begin
  vYEARMONTH := to_char(pProcDate,'yyyy')*100+to_char(pProcDate,'mm');
  vNextDay   := pProcDate+1;

  update BFCRM10.HYK_YHQCLJL
    set CRMJZRQ=pProcDate
    where CRMJZRQ is null;

  insert into BFCRM10.HYK_YHQ_QTCLRBB(RQ,HYKTYPE,YHQID,MDFWDM,MDID,SKTNO,QTFQJE,BKJE,QKJE,YEARMONTH)
    select pProcDate,H.HYKTYPE,
           L.YHQID,
           L.MDFWDM,
           L.MDID,
           L.SKTNO,
           SUM((1-abs(sign(L.CLLX-10)))*(nvl(JFJE,0) - nvl(DFJE,0))) QTFQJE ,
           0 BKJE ,
           0,
           vYEARMONTH
      from BFCRM10.HYK_YHQCLJL L,BFCRM10.HYK_HYXX H
      where L.HYID=H.HYID
        and CLLX in (10)
        and L.MDID>0
        and CRMJZRQ=pProcDate
      group by H.HYKTYPE,L.YHQID,MDFWDM,L.MDID,L.SKTNO;

end;
/

prompt
prompt Creating procedure HYK_PROC_HYK_YHQ_RBB
prompt =======================================
prompt
create or replace procedure bfcrm10.HYK_PROC_HYK_YHQ_RBB (
                              pRCLRQ in date
                            )
as
  vPrevDD      date;
  vNextDay     date;
  vHYKTYPE     number(5);
  vYHQID       number(5);
  vTmpJE       number(14,2);
  vSQYE        number(14,2);
  vCKJE        number(14,2);
  vQKJE        number(14,2);
  vBKJE        number(14,2);
  vXFJE        number(14,2);
  vQMYE        number(14,2);
  vTZJE        number(14,2);
  vMDFWDM      varchar2(10);
  --vtmpTZJE     number(14,2);
  cursor curYE is
    select B.HYKTYPE,A.YHQID,MDFWDM,sum(A.JE) YE
      from BFCRM10.HYK_YHQZH A,BFCRM10.HYK_HYXX B
      where A.HYID=B.HYID
      group by B.HYKTYPE,A.YHQID,MDFWDM;
begin

  vPrevDD := pRCLRQ-1;
  vNextDay := pRCLRQ+1;

  update BFCRM10.HYK_YHQCLJL
    set CRMJZRQ=pRCLRQ
    where CRMJZRQ is null;

  open curYE;
  fetch curYE into vHYKTYPE,vYHQID,vMDFWDM,vQMYE;
  while (curYE%FOUND)
  loop
    /*上期余额*/
    vSQYE := 0;
    select avg(QMYE) into vSQYE
      from BFCRM10.HYK_YHQ_RBB
      where RQ=vPrevDD
        and HYKTYPE=vHYKTYPE
        and YHQID=vYHQID
        and MDFWDM=vMDFWDM;
    vSQYE := nvl(vSQYE,0);

    /* 存款金额*/
    vCKJE := 0;
    select sum(JFJE-DFJE) into vCKJE
      from BFCRM10.HYK_YHQCLJL A,BFCRM10.HYK_HYXX B
      where A.HYID=B.HYID
        and A.CRMJZRQ=pRCLRQ
        and B.HYKTYPE=vHYKTYPE
        and (A.CLLX=1 or A.CLLX=10)
        and A.YHQID=vYHQID
        and MDFWDM=vMDFWDM;

    vCKJE := nvl(vCKJE,0);

    /* 取款金额*/
    vQKJE := 0;
    select sum(DFJE - JFJE) into vQKJE
      from BFCRM10.HYK_YHQCLJL A,BFCRM10.HYK_HYXX B
      where A.HYID=B.HYID
        and A.CRMJZRQ=pRCLRQ
        and B.HYKTYPE=vHYKTYPE
        and A.CLLX=2
        and A.YHQID=vYHQID
        and MDFWDM=vMDFWDM;
    vQKJE := nvl(vQKJE,0);

    /* 并卡金额*/
    vBKJE := 0;
    select sum(JFJE-DFJE) into vBKJE
      from BFCRM10.HYK_YHQCLJL A,BFCRM10.HYK_HYXX B
      where A.HYID=B.HYID
        and A.CRMJZRQ=pRCLRQ
        and B.HYKTYPE=vHYKTYPE
        and A.CLLX=5
        and A.YHQID=vYHQID
        and MDFWDM=vMDFWDM;
    vBKJE := nvl(vBKJE,0);

    /*消费金额*/
    vXFJE := 0;
    select sum(DFJE - JFJE) into vXFJE
      from BFCRM10.HYK_YHQCLJL A,BFCRM10.HYK_HYXX B
      where A.HYID=B.HYID
        and A.CRMJZRQ=pRCLRQ
        and B.HYKTYPE=vHYKTYPE
        and A.CLLX=7
        and A.YHQID=vYHQID
        and MDFWDM=vMDFWDM;
    vXFJE := nvl(vXFJE,0);


    /*调整金额*/
    vTZJE := 0;
    select sum(DFJE - JFJE) into vTZJE
      from BFCRM10.HYK_YHQCLJL A,BFCRM10.HYK_HYXX B
      where A.HYID=B.HYID
        and A.CRMJZRQ=pRCLRQ
        and B.HYKTYPE=vHYKTYPE
        and (A.CLLX=11 or A.CLLX=12 )
        and A.YHQID=vYHQID
        and MDFWDM=vMDFWDM;
    vTZJE := nvl(vTZJE,0);

    /*GHKLX*/
    vTmpJE := 0;
    select sum(H.TZJE) into vTmpJE
      from BFCRM10.HYK_GHKLX A,BFCRM10.HYK_GHKLX_YHQZH H
      where A.JLBH=H.JLBH
        and A.DJSJ<=pRCLRQ
        and A.DJSJ>vNextDay
        and A.HYKTYPE_OLD=vHYKTYPE
        and H.YHQID=vYHQID
        and H.MDFWDM=vMDFWDM;
    vTZJE := vTZJE - nvl(vTmpJE,0);

    vTmpJE := 0;
    select sum(H.TZJE) into vTmpJE
      from BFCRM10.HYK_GHKLX A,BFCRM10.HYK_GHKLX_YHQZH H
      where A.JLBH=H.JLBH
        and A.DJSJ<=pRCLRQ
        and A.DJSJ> vNextDay
        and A.HYKTYPE_NEW=vHYKTYPE
        and H.YHQID=vYHQID
        and H.MDFWDM=vMDFWDM;
    vTZJE :=vTZJE + nvl(vTmpJE,0);

   /*--调整金额应减掉更换卡类型之前的发生金额-*/
    vTmpJE := 0;
    select sum(JFJE-DFJE) into vTmpJE
      from BFCRM10.HYK_YHQCLJL A,BFCRM10.HYK_GHKLX B
      where A.HYID=B.HYID
        and A.CRMJZRQ=pRCLRQ
        and B.DJSJ<=pRCLRQ
        and B.DJSJ> vNextDay
        and B.HYKTYPE_OLD=vHYKTYPE
        and A.CLSJ<B.DJSJ
        and A.YHQID=vYHQID
        and MDFWDM=vMDFWDM;
    vTZJE :=vTZJE + nvl(vTmpJE,0);

    vTmpJE := 0;
    select sum(JFJE-DFJE) into vTmpJE
      from BFCRM10.HYK_YHQCLJL A,BFCRM10.HYK_GHKLX B
      where A.HYID=B.HYID
        and A.CRMJZRQ=pRCLRQ
        and B.DJSJ<=pRCLRQ
        and B.DJSJ> vNextDay
        and B.HYKTYPE_NEW=vHYKTYPE
        and A.CLSJ<B.DJSJ
        and A.YHQID=vYHQID
        and MDFWDM=vMDFWDM;
    vTZJE := vTZJE - nvl(vTmpJE,0);

    /*升级记录  */
    vTmpJE := 0;
    select sum(H.TZJE) into vTmpJE
      from BFCRM10.HYK_SJJL A,BFCRM10.HYK_SJJL_YHQZH H
      where A.JLBH=H.JLBH
        and A.ZXRQ >= pRCLRQ
        and A.ZXRQ <vNextDay
        and A.HYKTYPE_OLD=vHYKTYPE
        and H.YHQID=vYHQID
        and H.MDFWDM=vMDFWDM;
    vTZJE := vTZJE - nvl(vTmpJE,0);

    vTmpJE := 0;
    select sum(H.TZJE) into vTmpJE
      from BFCRM10.HYK_SJJL A,BFCRM10.HYK_SJJL_YHQZH H
      where A.JLBH=H.JLBH
        and A.ZXRQ >= pRCLRQ
        and A.ZXRQ <vNextDay
        and A.HYKTYPE_NEW=vHYKTYPE
        and H.YHQID=vYHQID
        and H.MDFWDM=vMDFWDM;
    vTZJE := vTZJE + nvl(vTmpJE,0);

   /*--调整金额应减掉升级记录之前的发生金额---*/
    vTmpJE := 0;
    select sum(JFJE-DFJE) into vTmpJE
      from BFCRM10.HYK_YHQCLJL A,BFCRM10.HYK_SJJL B
      where A.HYID=B.HYID
        and A.CRMJZRQ=pRCLRQ
        and B.ZXRQ<=pRCLRQ
        and B.ZXRQ> vNextDay
        and B.HYKTYPE_OLD=vHYKTYPE
        and A.CLSJ<B.ZXRQ  /*--ZXRQ必须有Time--*/
        and A.YHQID=vYHQID
        and MDFWDM=vMDFWDM;
    vTZJE := vTZJE + nvl(vTmpJE,0);

    vTmpJE := 0;
    select sum(JFJE-DFJE) into vTmpJE
      from BFCRM10.HYK_YHQCLJL A,BFCRM10.HYK_SJJL B
      where A.HYID=B.HYID
        and A.CRMJZRQ=pRCLRQ
        and B.ZXRQ<=pRCLRQ
        and B.ZXRQ> vNextDay
        and B.HYKTYPE_NEW=vHYKTYPE
        and A.CLSJ<B.ZXRQ  /*--ZXRQ必须有Time--*/
        and A.YHQID=vYHQID
        and MDFWDM=vMDFWDM;
    vTZJE := vTZJE - nvl(vTmpJE,0);

    insert into BFCRM10.HYK_YHQ_RBB(RQ,HYKTYPE,YHQID,MDFWDM,SQYE,CKJE,QKJE,BKJE,XFJE,TZJE,QMYE)
     values(pRCLRQ,vHYKTYPE,vYHQID,vMDFWDM,vSQYE,vCKJE,vQKJE,vBKJE,vXFJE,vTZJE,vQMYE);

    fetch curYE into vHYKTYPE,vYHQID,vMDFWDM,vQMYE;
  end loop;
  close curYE;
end;
/

prompt
prompt Creating procedure HYK_PROC_HYK_YHQ_RHZ
prompt =======================================
prompt
create or replace procedure bfcrm10.HYK_PROC_HYK_YHQ_RHZ (
                              pProcDate in date
                            )
as
  vPrevDD      date;
  vNextDay     date;
  vHYID        integer;
  vYHQID       number(5);
  vSQYE        number(14,2);
  vCKJE        number(14,2);
  vQKJE        number(14,2);
  vBKJE        number(14,2);
  vXFJE        number(14,2);
  vTZJE        number(14,2);
  vQMYE        number(14,2);
  cursor curYE is
    select A.HYID,A.YHQID,sum(A.JE) YE
      from BFCRM10.HYK_YHQZH A
      group by A.HYID,A.YHQID;
begin

  vPrevDD := pProcDate-1;
  vNextDay := pProcDate+1;

  open curYE;
  fetch curYE into vHYID,vYHQID,vQMYE;
  while (curYE%FOUND)
  loop
    /*上期余额*/
    vSQYE := 0;
    select avg(QMYE) into vSQYE
      from BFCRM10.HYK_YHQ_RHZ
      where RQ=vPrevDD
        and HYID=vHYID
        and YHQID=vYHQID;
    vSQYE := nvl(vSQYE,0);

    /* 存款金额*/
    vCKJE := 0;
    select sum(JFJE-DFJE) into vCKJE
      from BFCRM10.HYK_YHQCLJL A
      where A.HYID=vHYID
        and A.CRMJZRQ=pProcDate
        and A.CLLX=1
        and A.YHQID=vYHQID;
    vCKJE := nvl(vCKJE,0);

    /* 取款金额*/
    vQKJE := 0;
    select sum(DFJE - JFJE) into vQKJE
      from BFCRM10.HYK_YHQCLJL A
      where A.HYID=vHYID
        and A.CRMJZRQ=pProcDate
        and A.CLLX=2
        and A.YHQID=vYHQID;
    vQKJE := nvl(vQKJE,0);

    /* 并卡金额*/
    vBKJE := 0;
    select sum(JFJE-DFJE) into vBKJE
      from BFCRM10.HYK_YHQCLJL A
      where A.HYID=vHYID
        and A.CRMJZRQ=pProcDate
        and A.CLLX=5
        and A.YHQID=vYHQID;
    vBKJE := nvl(vBKJE,0);

    /*消费金额*/
    vXFJE := 0;
    select sum(DFJE - JFJE) into vXFJE
      from BFCRM10.HYK_YHQCLJL A
      where A.HYID=vHYID
        and A.CRMJZRQ=pProcDate
        and A.CLLX=7
        and A.YHQID=vYHQID;
    vXFJE := nvl(vXFJE,0);

    /*调整金额*/
    vTZJE := 0;
    select sum(DFJE - JFJE) into vTZJE
      from BFCRM10.HYK_YHQCLJL A
      where A.HYID=vHYID
        and A.CRMJZRQ=pProcDate
        and A.CLLX=11
        and A.YHQID=vYHQID;
    vTZJE := nvl(vTZJE,0);

    insert into BFCRM10.HYK_YHQ_RHZ(RQ,HYID,YHQID,SQYE,CKJE,QKJE,BKJE,XFJE,TZJE,QMYE)
      values(pProcDate,vHYID,vYHQID,vSQYE,vCKJE,vQKJE,vBKJE,vXFJE,vTZJE,vQMYE);

    fetch curYE into vHYID,vYHQID,vQMYE;
  end loop;
  close curYE;
end;
/

prompt
prompt Creating procedure HYK_PROC_HYK_YHQ_XFRBB
prompt =========================================
prompt
create or replace procedure bfcrm10.HYK_PROC_HYK_YHQ_XFRBB (
                              pRCLRQ in date
                            )
as
  vYEARMONTH integer;
  vNextDay   date;
begin
  vYEARMONTH := to_char(pRCLRQ,'yyyy')*100+to_char(pRCLRQ,'mm');
  vNextDay   := pRCLRQ+1;

  update BFCRM10.HYK_YHQCLJL
    set CRMJZRQ=pRCLRQ
    where CRMJZRQ is null;

  insert into BFCRM10.HYK_YHQ_XFRBB(RQ,HYKTYPE,YHQID,MDFWDM,MDID,SKTNO,XFJE,TZJE,YEARMONTH)
    select pRCLRQ,H.HYKTYPE,
           L.YHQID,
           L.MDFWDM,
           L.MDID,
           L.SKTNO,
           SUM((1-abs(sign(CLLX-7))) * (nvl(DFJE,0) - nvl(JFJE,0))) XFJE ,
           SUM((1-abs(sign(CLLX-11))) * (nvl(DFJE,0) - nvl(JFJE,0))) TZJE ,
           vYEARMONTH
      from BFCRM10.HYK_YHQCLJL L,BFCRM10.HYK_HYXX H
      where L.HYID=H.HYID
        and CLLX in(7,11)
        and CRMJZRQ =pRCLRQ
      group by H.HYKTYPE,L.YHQID,L.MDFWDM,L.MDID,L.SKTNO;

  for item in (
      select B.HYKTYPE_OLD HYKTYPE,A.YHQID,A.MDFWDM,A.MDID,A.SKTNO,
             sum((1-abs(sign(CLLX-7))) * (nvl(DFJE,0) - nvl(JFJE,0))) XFJE ,
             sum((1-abs(sign(CLLX-11))) * (nvl(DFJE,0) - nvl(JFJE,0))) TZJE
        from BFCRM10.HYK_YHQCLJL A,BFCRM10.HYK_GHKLX B
        where A.HYID=B.HYID AND A.CLLX in (7,11)
          and A.CRMJZRQ=pRCLRQ
          and B.DJSJ>=pRCLRQ
          and B.DJSJ< vNextDay
        group by B.HYKTYPE_OLD,A.YHQID,A.MDFWDM,A.MDID,A.SKTNO)
  loop
    update BFCRM10.HYK_YHQ_XFRBB
      set XFJE = XFJE + item.XFJE,
           TZJE = TZJE + item.TZJE
      where HYKTYPE = item.HYKTYPE
        and YHQID = item.YHQID
        and MDFWDM = item.MDFWDM
        and MDID = item.MDID
        and SKTNO = item.SKTNO
        and RQ=pRCLRQ;
    if SQL%NOTFOUND then
      insert into BFCRM10.HYK_YHQ_XFRBB(RQ,HYKTYPE,YHQID,MDFWDM,MDID,SKTNO,XFJE,TZJE)
        values(pRCLRQ,item.HYKTYPE,item.YHQID,item.MDFWDM,item.MDID,item.SKTNO,item.XFJE,item.TZJE);
    end if;
  end loop;

  for item in (
    select B.HYKTYPE_NEW HYKTYPE,A.YHQID,A.MDFWDM,A.MDID,A.SKTNO,
           sum((1-abs(sign(CLLX-7))) * (nvl(DFJE,0) - nvl(JFJE,0))) XFJE ,
           sum((1-abs(sign(CLLX-11))) * (nvl(DFJE,0) - nvl(JFJE,0))) TZJE
      from BFCRM10.HYK_YHQCLJL A,BFCRM10.HYK_GHKLX B
      where A.HYID=B.HYID  and  A.CLLX in (7,11)
        and A.CRMJZRQ=pRCLRQ
        and B.DJSJ>=pRCLRQ
        and B.DJSJ< vNextDay
      group by B.HYKTYPE_NEW,A.YHQID,A.MDFWDM,A.MDID,A.SKTNO)
  loop
    update BFCRM10.HYK_YHQ_XFRBB
       set XFJE = XFJE - item.XFJE,
           TZJE = TZJE - item.TZJE
      where HYKTYPE = item.HYKTYPE
        and YHQID = item.YHQID
        and MDFWDM = item.MDFWDM
        and MDID = item.MDID
        and SKTNO = item.SKTNO
        and RQ=pRCLRQ;
    if SQL%NOTFOUND then
      insert into BFCRM10.HYK_YHQ_XFRBB(RQ,HYKTYPE,YHQID,MDFWDM,MDID,SKTNO,XFJE,TZJE)
        values(pRCLRQ,item.HYKTYPE,item.YHQID,item.MDFWDM,item.MDID,item.SKTNO,item.XFJE*(-1),item.TZJE*(-1));
    end if;
  end loop;
end;
/

prompt
prompt Creating procedure HYK_PROC_MOVE_XFJL
prompt =====================================
prompt
create or replace procedure bfcrm10.HYK_PROC_MOVE_XFJL (
                              pRCLRQ in date
                            )
as
  vNextDay date;
begin

  vNextDay := pRCLRQ+1;

  /*移消费记录*/
  insert into BFCRM10.HYXFJL (XFJLID,SHDM,MDID,SKTNO,JLBH,DJLX,XFJLID_OLD,HYID,HYID_FQ,HYID_TQ,SKYDM,XFSJ,JZRQ,CRMJZRQ,SCSJ,XSSL,
JE,ZK,ZK_HY,CZJE,JF,BJ_HTBSK,XFRQ_FQ,JFBS,TDJFDYDBH,BSFS,HYKNO,HYKNO_FQ,FXDW,BJ_CHILD,PGRYID,VIPTYPE,BJ_SL)
    select XFJLID,SHDM,MDID,SKTNO,JLBH,DJLX,XFJLID_OLD,HYID,HYID_FQ,HYID_TQ,SKYDM,XFSJ,JZRQ,CRMJZRQ,SCSJ,XSSL,
JE,ZK,ZK_HY,CZJE,JF,BJ_HTBSK,XFRQ_FQ,JFBS,TDJFDYDBH,BSFS,HYKNO,HYKNO_FQ,FXDW,BJ_CHILD,PGRYID,VIPTYPE,BJ_SL
     from  BFCRM10.HYK_XFJL
     where CRMJZRQ=pRCLRQ
       and STATUS=1;

  insert into BFCRM10.HYXFJL_SP(XFJLID,INX,SHSPID,BMDM,SPDM,XSSL,XSJE,ZKJE,ZKJE_HY,THJE,BJ_JF,XSJE_JF,XSJE_FQ,JFDYDBH,JF,JFJS,ZKL,ZKDYDBH,HTH,BJ_JFBS,
BJ_BCJHD,BS,XSJE_SYJE,FTBL,JFFBGZ,THJE_JF,THJF,INX_OLD)
    select
A.XFJLID,A.INX,A.SHSPID,A.BMDM,A.SPDM,A.XSSL,A.XSJE,A.ZKJE,A.ZKJE_HY,A.THJE,A.BJ_JF,A.XSJE_JF,A.XSJE_FQ,A.JFDYDBH,A.JF,A.JFJS,A.ZKL,A.ZKDYDBH,A.HTH,A.BJ_JFBS,
A.BJ_BCJHD,A.BS,A.XSJE_SYJE,A.FTBL,A.JFFBGZ,A.THJE_JF,A.THJF,A.INX_OLD
      from BFCRM10.HYK_XFJL_SP A,BFCRM10.HYK_XFJL B
      where A.XFJLID=B.XFJLID
        and B.STATUS =1
        and B.CRMJZRQ=pRCLRQ;

  insert into BFCRM10.HYXFJL_ZFFS
    select A.*
      from BFCRM10.HYK_XFJL_ZFFS A,BFCRM10.HYK_XFJL B
      where A.XFJLID=B.XFJLID
        and B.STATUS=1
        and B.CRMJZRQ=pRCLRQ;

  insert into BFCRM10.HYXFJL_SP_ZFFS
    select A.*
      from BFCRM10.HYK_XFJL_SP_ZFFS A,BFCRM10.HYK_XFJL B
     where A.XFJLID=B.XFJLID
        and B.STATUS=1
       and B.CRMJZRQ=pRCLRQ;

  insert into BFCRM10.HYXFJL_SP_FQ
    select A.*
      from BFCRM10.HYK_XFJL_SP_FQ A,BFCRM10.HYK_XFJL B
     where A.XFJLID=B.XFJLID
        and B.STATUS=1
       and B.CRMJZRQ=pRCLRQ;

  insert into BFCRM10.HYXFJL_SP_YQFT
    select A.*
      from BFCRM10.HYK_XFJL_SP_YQFT A,BFCRM10.HYK_XFJL B
     where A.XFJLID=B.XFJLID
       and B.STATUS=1
       and B.CRMJZRQ=pRCLRQ;

  insert into BFCRM10.HYXFJL_FQ
    select A.*
      from BFCRM10.HYK_XFJL_FQ A,BFCRM10.HYK_XFJL B
     where A.XFJLID=B.XFJLID
       and B.STATUS=1
       and B.CRMJZRQ=pRCLRQ;

  insert into BFCRM10.HYXFJL_YHKZF
 (XFJLID,BANK_KH,BANK_ID,SHZFFSID,JE,INX)
    select A.XFJLID,A.BANK_KH,A.BANK_ID,A.SHZFFSID,A.JE,A.INX
      from BFCRM10.HYK_XFJL_YHKZF A,BFCRM10.HYK_XFJL B
     where A.XFJLID=B.XFJLID
       and B.STATUS=1
       and B.CRMJZRQ=pRCLRQ;

  insert into BFCRM10.HYXFJL_FQDM(XFJLID,YHQCODE,YHQID,YHQMZ,CXID)
    select A.XFJLID,YHQCODE,YHQID,YHQMZ,CXID
      from BFCRM10.HYK_XFJL_FQDM A,BFCRM10.HYK_XFJL B
     where A.XFJLID=B.XFJLID
       and B.STATUS=1
       and B.CRMJZRQ=pRCLRQ;

  insert into BFCRM10.HYXFJL_SP_MBJZ
    select A.*
      from BFCRM10.HYK_XFJL_SP_MBJZ A,BFCRM10.HYK_XFJL B
     where A.XFJLID=B.XFJLID
       and B.STATUS=1
       and B.CRMJZRQ=pRCLRQ;

  insert into BFCRM10.HYXFJL_SP_ZFFS_MBJZ
    select A.*
      from BFCRM10.HYK_XFJL_SP_ZFFS_MBJZ A,BFCRM10.HYK_XFJL B
     where A.XFJLID=B.XFJLID
       and B.STATUS=1
       and B.CRMJZRQ=pRCLRQ;

  insert into BFCRM10.HYXFJL_ZFFS_FQ
    select A.*
      from BFCRM10.HYK_XFJL_ZFFS_FQ A,BFCRM10.HYK_XFJL B
     where A.XFJLID=B.XFJLID
       and B.STATUS=1
       and B.CRMJZRQ=pRCLRQ;

  insert into BFCRM10.HYXFJL_SP_ZFFS_FQ
    select A.*
      from BFCRM10.HYK_XFJL_SP_ZFFS_FQ A,BFCRM10.HYK_XFJL B
     where A.XFJLID=B.XFJLID
       and B.STATUS=1
       and B.CRMJZRQ=pRCLRQ;

  insert into BFCRM10.HYXFJL_ZFFS_ZSLP
    select A.*
      from BFCRM10.HYK_XFJL_ZFFS_ZSLP A,BFCRM10.HYK_XFJL B
     where A.XFJLID=B.XFJLID
       and B.STATUS=1
       and B.CRMJZRQ=pRCLRQ;

  insert into BFCRM10.HYXFJL_ZSLP
    select A.*
      from BFCRM10.HYK_XFJL_ZSLP A,BFCRM10.HYK_XFJL B
     where A.XFJLID=B.XFJLID
       and B.STATUS=1
       and B.CRMJZRQ=pRCLRQ;

end;
/

prompt
prompt Creating procedure HYK_PROC_HYK_XFRBB
prompt =====================================
prompt
create or replace procedure bfcrm10.HYK_PROC_HYK_XFRBB (
                              pProcDate in date
                            )
as
  vYEARMONTH integer;
  vNextDay   date;
begin

  vYEARMONTH := to_char(pProcDate,'yyyy')*100+to_char(pProcDate,'mm');
  vNextDay := pProcDate+1;

  insert into BFCRM10.HYK_JEZ_XFRBB(RQ,HYKTYPE,MDID,CZMD,SKTNO,XFJE,TKJE,TZJE,YEARMONTH)
    select pProcDate,H.HYKTYPE,
           H.MDID ,
           L.MDID CZMD,
           L.SKTNO,
           SUM((1-abs(sign(CLLX-7))) * (nvl(DFJE,0) - nvl(JFJE,0))) XFJE ,
           SUM((1-abs(sign(CLLX-6))) * (nvl(DFJE,0) - nvl(JFJE,0))) TKJE ,
           SUM((1-abs(sign(CLLX-11))) * (nvl(DFJE,0) - nvl(JFJE,0))) TZJE ,
           vYEARMONTH
      from BFCRM10.HYK_JEZCLJL L,BFCRM10.HYK_HYXX H
      where L.HYID=H.HYID
        and CLLX in(6,7,11)
        and CRMJZRQ=pProcDate
      group by H.HYKTYPE ,H.MDID,L.MDID,L.SKTNO;
end;
/

prompt
prompt Creating procedure HYK_PROC_QK_CZK_HY
prompt =====================================
prompt
create or replace procedure bfcrm10.HYK_PROC_QK_CZK_HY (
                              pHYID  in integer,
                              pCZKJE in number,
                              pCZDD  in varchar2,
                              pDJR   in integer,
                              pZY    in varchar2,
                              pERROR out varchar2
                            )
is
  vJLBH integer;
  vDJRMC varchar2(16);
  vYE number(14,2);
  vZ_CZKJE number(14,2);
begin

  select sum(YE) into vZ_CZKJE
    from  BFCRM10.HYK_JEZH
    where  HYID=pHYID;

  vZ_CZKJE := nvl(vZ_CZKJE,0);

  pERROR := '';

  if vZ_CZKJE <pCZKJE then
    pERROR := '储值卡余额不足，操作中止';
    return; --- 2007.12.10
  end if;


  select PERSON_NAME into vDJRMC from BFCRM10.RYXX  where PERSON_ID=pDJR;

  vJLBH := BFCRM10.Update_BHZT('HYK_CZK_QKJL');

  insert into BFCRM10.HYK_CZK_QKJL(CZJPJ_JLBH,HYID,YJE,QKJE,CZDD,ZY,DJR,DJRMC,DJSJ,ZXR,ZXRMC,ZXRQ)
    values(vJLBH,
           pHYID,
           pCZKJE,
           pCZKJE,
           pCZDD,
           pZY,
           pDJR,
           vDJRMC,
           sysdate,
           pDJR,
           vDJRMC,
           trunc(sysdate));

  update BFCRM10.HYK_JEZH
     set YE=YE - pCZKJE
   where HYID=pHYID;

  select YE into vYE
    from BFCRM10.HYK_JEZH
   where HYID=pHYID;

  insert into BFCRM10.HYK_JEZCLJL(HYID,CLSJ,CLLX,JLBH,ZY,JFJE,DFJE,YE)
    values(pHYID,sysdate,2,vJLBH,pZY,0,pCZKJE,vYE);
end;
/

prompt
prompt Creating procedure HYK_PROC_QK_HYFW
prompt ===================================
prompt
create or replace procedure bfcrm10.HYK_PROC_QK_HYFW (
                              pHYID   in integer,
                              pFWNRID in integer,
                              pFWSL   in float,
                              pDJR    in integer,
                              pBJ_XZFWSL in integer,
                              vJLBH      in integer,
                              pZY        in varchar2,
                              pERROR     out varchar2
                            )
is
  vT_FWNRID integer;
  vJSRQ     date;
  vDJRMC    varchar2(16);
  vSYFWSL   float;
  vZ_FWSL   float;
  vJYSL     float;
  vSL       float;
  vSYSL     float;

  cursor CurHYFW is
    select FWNRID,JSRQ,FWSL
      from BFCRM10.HYK_FWZH
      where HYID=pHYID
        and FWNRID=pFWNRID
        and JSRQ>=trunc(sysdate)
      order by JSRQ,FWNRID;
begin

  select  sum(FWSL) into vZ_FWSL
    from  BFCRM10.HYK_FWZH
    where HYID=pHYID
      and FWNRID=pFWNRID
      and JSRQ>=trunc(sysdate);

  vZ_FWSL := nvl(vZ_FWSL,0);

  vSYSL := pFWSL;
  if pBJ_XZFWSL=0 then
    vSYSL := 0;
  end if;

  pERROR := '';

  if (vZ_FWSL <pFWSL) and (pBJ_XZFWSL=1) then
    pERROR := '剩余服务数量不足，操作中止';
    return;
  end if;

  select PERSON_NAME into vDJRMC from BFCRM10.RYXX  where PERSON_ID=pDJR;

  open CurHYFW;
  fetch CurHYFW into vT_FWNRID,vJSRQ,vSL;
  while (CurHYFW%FOUND)
  loop
    vJYSL := 0;
    if vSL<vSYSL then
      vJYSL := vSL;
    else
      vJYSL := vSYSL;
    end if;

    update BFCRM10.HYK_FWZH
       set FWSL=FWSL - vJYSL
      where HYID=pHYID
        and FWNRID=vT_FWNRID
        and JSRQ=vJSRQ;

    select FWSL into vSYFWSL
      from BFCRM10.HYK_FWZH
      where HYID=pHYID
        and FWNRID=vT_FWNRID
        and JSRQ=vJSRQ;

    insert into BFCRM10.HYK_HYFWCLJL(HYID,FWNRID,JSRQ,CLSJ,CLLX,JLBH,JFSL,DFSL,SYSL,ZY)
      values(pHYID,
           vT_FWNRID,
           vJSRQ,
           sysdate,
           2,
           vJLBH,
           0,
           vJYSL,
           vSYFWSL,
           pZY);

    fetch CurHYFW into vT_FWNRID,vJSRQ,vSL;
  end loop;
  close CurHYFW;
end;
/

prompt
prompt Creating procedure HYK_PROC_QK_YHQ_HY
prompt =====================================
prompt
create or replace procedure bfcrm10.HYK_PROC_QK_YHQ_HY (
                              pHYID  in integer,
                              pYHQJE in number,
                              pCZDD  in varchar2,
                              pDJR   in integer,
                              pZY    in varchar2,
                              pERROR out varchar2
                            )
is
  vYHQID   integer;
  vYHQJE   number(14,2);
  vJSRQ date;
  vMDFWDM varchar2(10);
  vJE number(14,2);
  vJYJE number(14,2);
  vJLBH integer;
  vDJRMC varchar2(16);
  vYE number(14,2);
  vZ_YHQJE number(14,2);

  cursor CurYHQ is
    select H.YHQID,
           JSRQ,
           MDFWDM,
           JE
      from BFCRM10.HYK_YHQZH H,YHQDEF F
      where H.YHQID=F.YHQID
        and HYID=pHYID
        and nvl(F.BJ_CXYHQ,0)=0
        and JSRQ>=trunc(sysdate)
      order by JSRQ,H.YHQID;
begin
  vYHQJE := pYHQJE;

  select  sum(JE) into vZ_YHQJE
    from  BFCRM10.HYK_YHQZH H,BFCRM10.YHQDEF F
    where  H.YHQID=F.YHQID
      and H.HYID=pHYID
      and nvl(F.BJ_CXYHQ,0)=0
      and JSRQ>=trunc(sysdate);
      --datediff(day,sysdate,JSRQ) >=0

  vZ_YHQJE := nvl(vZ_YHQJE,0);

  pERROR := '';

  if vZ_YHQJE <vYHQJE then
    pERROR := '优惠券余额不足，操作中止';
    return;  -- 周智钢于2007.12.10加入
  end if;

  select PERSON_NAME into vDJRMC from BFCRM10.RYXX  where PERSON_ID=pDJR;


  open CurYHQ;
  fetch CurYHQ into vYHQID,vJSRQ,vMDFWDM,vJE;
  while (CurYHQ%FOUND)
  loop
    vJYJE := 0;
    if vJE<vYHQJE then
      vJYJE := vJE;
    else
      vJYJE := vYHQJE;
    end if;

    vJLBH := BFCRM10.Update_BHZT('HYK_YHQ_QKJL');

    insert into BFCRM10.HYK_CZK_YHQ_QKJL(CZJPJ_JLBH,CZDD,HYID,YHQID,JSRQ,MDFWDM,YJE,QKJE,ZY,DJR,DJRMC,DJSJ,ZXR,ZXRMC,ZXRQ)
     values(vJLBH,
           pCZDD,
           pHYID,
           vYHQID,
           vJSRQ,
           vMDFWDM,
           vJE,
           vJYJE,
           pZY,
           pDJR,
           vDJRMC,
           sysdate,
           pDJR,
           vDJRMC,
           trunc(sysdate));

    update BFCRM10.HYK_YHQZH
       set JE=JE - vJYJE
     where HYID=pHYID and
           YHQID=vYHQID and
           JSRQ=vJSRQ and
           MDFWDM=vMDFWDM;

    select JE into vYE
      from BFCRM10.HYK_YHQZH
     where HYID=pHYID and
           YHQID=vYHQID and
           JSRQ=vJSRQ and
           MDFWDM=vMDFWDM;

    insert into BFCRM10.HYK_YHQCLJL(HYID,CLSJ,CLLX,JLBH,YHQID,JSRQ,MDFWDM,ZY,JFJE,DFJE,YE)
      values(pHYID,
           sysdate,
           2,
           vJLBH,
           vYHQID,
           vJSRQ,
           vMDFWDM,
           pZY,
           0,
           vJYJE,
           vYE);

    vYHQJE := vYHQJE - vJYJE;
    fetch CurYHQ into vYHQID,vJSRQ,vMDFWDM,vJE;
  end loop;
  close CurYHQ;
end;
/

prompt
prompt Creating procedure HYK_PROC_SHSPSB
prompt ==================================
prompt
create or replace procedure bfcrm10.HYK_PROC_SHSPSB
is
  vSBDM varchar2(10);
  vSBMC varchar2(30);
  vPYM  varchar2(6);
  vSYZ  varchar2(50);
  vMJBJ    integer;
  vSHSBID  integer;
  vMAX_SBID integer;
  vSHDM varchar2(4);

  cursor Cur_insert is
    select SBDM,SBMC,PYM,SYZ,MJBJ,SHDM,SHSBID
      from BFCRM10.SHSPSB A
      where A.SBID is null;
begin
  open Cur_insert;
  fetch Cur_insert into vSBDM,vSBMC,vPYM,vSYZ,vMJBJ,vSHDM,vSHSBID;
  while (Cur_insert%FOUND)
  loop
    vMAX_SBID := 0;
    select max(SBID)+1 into vMAX_SBID
      from BFCRM10.SPSB;

    insert into BFCRM10.SPSB(SBID,SBDM,SBMC,PYM,SYZ,MJBJ)
      values(vMAX_SBID,vSBDM,vSBMC,vPYM,vSYZ,vMJBJ);

    update BFCRM10.SHSPSB
      set SBID=vMAX_SBID
      where SHSBID=vSHSBID;

    vMAX_SBID:=0;
    fetch Cur_insert into vSBDM,vSBMC,vPYM,vSYZ,vMJBJ,vSHDM,vSHSBID;
  end loop;
  close Cur_insert;
end;
/

prompt
prompt Creating procedure HYK_PROC_SP_CXHZ
prompt ===================================
prompt
create or replace procedure bfcrm10.HYK_PROC_SP_CXHZ (
                              pRCLRQ in date
                            )
is
  vSHDM   varchar2(4);
  vMDID   integer;
  vCXID integer;
  vYHQID  integer;
  vSHSPID integer;
  vBMDM   varchar2(20);
  vYQJE   number(14,2);

  cursor Cur_YHQ is
     select X.SHDM,X.MDID,nvl(Y.CXID,-1) CXID,Y.YHQID,Y.SHSPID,Y.BMDM,sum(YQJE) YQJE
       from BFCRM10.HYK_XFJL X,BFCRM10.HYK_XFJL_SP_YQFT Y
       where X.XFJLID=Y.XFJLID
         and X.STATUS=1
         and X.CRMJZRQ=pRCLRQ
       group by X.SHDM,X.MDID,nvl(Y.CXID,-1),Y.YHQID,Y.SHSPID,Y.BMDM;begin
  update BFCRM10.HYK_XFJL
     set CRMJZRQ=pRCLRQ
    where STATUS=1
      and CRMJZRQ is null;

  BFCRM10.Calc_SP_FQJE_HTFQ(pRCLRQ);
  BFCRM10.Calc_SP_FQJE(pRCLRQ);

  open Cur_YHQ;
  fetch Cur_YHQ into vSHDM,vMDID,vCXID,vYHQID,vSHSPID,vBMDM,vYQJE;
  while (Cur_YHQ%FOUND)
  loop
    vCXID := nvl(vCXID,0);
    vBMDM := nvl(vBMDM,'');

    update BFCRM10.YHQ_CXHD_HZ
      set YQJE=YQJE + vYQJE
      where RQ= pRCLRQ
        and SHDM=vSHDM
        and MDID=vMDID
        and CXID=vCXID
        and YHQID=vYHQID
        and SHSPID=vSHSPID
        and BMDM=vBMDM;
    if SQL%NOTFOUND then
      insert into BFCRM10.YHQ_CXHD_HZ(RQ, SHDM,  MDID, CXID, YHQID, SHSPID,BMDM,FQJE,YQJE)
        values(pRCLRQ,vSHDM,vMDID,vCXID,vYHQID,vSHSPID,vBMDM,0,vYQJE);
    end if;

    fetch Cur_YHQ into vSHDM,vMDID,vCXID,vYHQID,vSHSPID,vBMDM,vYQJE;
  end loop;
  close Cur_YHQ;

end;
/

prompt
prompt Creating procedure HYK_PROC_UPDATECRMJZRQ
prompt =========================================
prompt
CREATE OR REPLACE Procedure BFCRM10.HYK_PROC_UPDATECRMJZRQ(pRCLRQ in date)
is
begin
  update BFCRM10.HYK_XFJL
     set CRMJZRQ=pRCLRQ
   where STATUS=1 and
         CRMJZRQ is null;

  update BFCRM10.HYK_JEZCLJL
     set CRMJZRQ=pRCLRQ
   where CRMJZRQ is null;

  update BFCRM10.HYK_YHQCLJL
     set CRMJZRQ=pRCLRQ
   where CRMJZRQ is null;
end;
/

prompt
prompt Creating procedure YHQ_QD_MBJZDYD
prompt =================================
prompt
create or replace procedure bfcrm10.YHQ_QD_MBJZDYD (
                              vJLBH  in integer,
                              pQDR   in integer
                            )
is
  vBMID       integer;
  vBMDM       varchar2(20);
  vSHDM       varchar2(4);
  vQDRMC      varchar2(16);
  vXFLJMJFS   number(5);
  vCXHDBH     integer;
  vDJLX       number(5);
  vcurJLBH_PT integer;
  vcurJLBH_YX integer;
begin

  begin
    select PERSON_NAME into vQDRMC
      from BFCRM10.RYXX
      where PERSON_ID=pQDR;
  exception
    when others then
      vQDRMC := to_char(pQDR);
  end;

  update BFCRM10.CXMBJZDYD
    set QDR=pQDR,
        QDRMC=vQDRMC,
        QDSJ=sysdate,
        STATUS=2
    where JLBH=vJLBH
      and SHR is not null
      and QDR is null
      and STATUS=1;
  if SQL%NOTFOUND then
    raise_application_error(-20001,'本单已启动或不存在!');
    return;
  end if;

end;
/

prompt
prompt Creating procedure YHQ_QD_YHQFFDYD
prompt ==================================
prompt
create or replace procedure bfcrm10.YHQ_QD_YHQFFDYD (
                              vJLBH in integer,
                              pQDR  in integer
                            )
is
  vBMID     integer;
  vBMDM     varchar2(20);
  vSHDM     varchar2(4);
  vYHQID    integer;
  vCXHDBH   integer;
  vXFLJFQFS number(5);
  vDJLX           number(5);
  vcurJLBH_PT     integer;
  vcurJLBH_YX     integer;
  vQDRMC          varchar2(16);
begin

  begin
    select PERSON_NAME into vQDRMC
      from BFCRM10.RYXX
      where PERSON_ID=pQDR;
  exception
    when others then
      vQDRMC := ' ';
  end;

  update BFCRM10.HYKFQDYD
     set QDR=pQDR,
         QDRMC=vQDRMC,
         QDSJ=sysdate,
         STATUS=2
    where JLBH=vJLBH
      and SHR is not null
      and QDR is null
      and STATUS=1;

  if (SQL%NOTFOUND) then
    raise_application_error(-20001,'本单已启动或不存在!');
    return;
  end if;

end;
/

prompt
prompt Creating procedure YHQ_QD_YHQSYDYD
prompt ==================================
prompt
create or replace procedure bfcrm10.YHQ_QD_YHQSYDYD (
                              vJLBH  in integer,
                              pQDR   in integer
                            )
is
  vBMID       integer;
  vBMDM       varchar2(20);
  vSHDM       varchar2(4);
  vYHQID      integer;
  vQDRMC      varchar2(16);
  vBJ_FQ      number(5);
  vCXHDBH     integer;
  vDJLX       number(5);
  vcurJLBH_PT integer;
  vcurJLBH_YX integer;
begin

  begin
    select PERSON_NAME into vQDRMC
      from BFCRM10.RYXX
      where PERSON_ID=pQDR;
  exception
    when others then
      vQDRMC := to_char(pQDR);
  end;

  update BFCRM10.HYKYQDYD
    set QDR=pQDR,
        QDRMC=vQDRMC,
        QDSJ=sysdate,
        STATUS=2
    where JLBH=vJLBH
      and SHR is not null
      and QDR is null
      and STATUS=1;
  if SQL%NOTFOUND then
    raise_application_error(-20001,'本单已启动或不存在!');
    return;
  end if;

end;
/

prompt
prompt Creating procedure HYK_YHQ_QD_CXDJ
prompt ==================================
prompt
CREATE OR REPLACE PROCEDURE BFCRM10."HYK_YHQ_QD_CXDJ" (
                              pRCLRQ in date,
                              pZXR     in integer
                            )
is
  vQDRMC varchar2(12);
  vNextDay date;
  vCount number(10);
begin
  begin
    select PERSON_NAME into vQDRMC
      from BFCRM10.RYXX
      where PERSON_ID=pZXR;
  exception
    when others then
      vQDRMC := to_char(pZXR);
  end;

  --declare vJLBH   integer;

  vNextDay := pRCLRQ+1;
  --优惠券发放定义单
  for item in (
        select JLBH
          from BFCRM10.HYKFQDYD
          where SHR is not null
            and QDR is null
            and STATUS=1
            and RQ1=vNextDay
            and RQ2>sysdate
          order by JLBH)
  loop
    BFCRM10.YHQ_QD_YHQFFDYD(item.JLBH,pZXR);
  end loop;

  --优惠券使用定义单
  for item in (
        select  JLBH
          from BFCRM10.HYKYQDYD
          where SHR is not null
            and QDR is null
            and STATUS=1
            and RQ1=vNextDay
            and RQ2>sysdate
          order by JLBH)
  loop
    BFCRM10.YHQ_QD_YHQSYDYD(item.JLBH,pZXR);
  end loop;

  --促销满百减折定义单
  for item in (
        select  JLBH
          from BFCRM10.CXMBJZDYD
          where SHR is not null
            and QDR is null
            and STATUS=1
            and RQ1=vNextDay
            and RQ2>sysdate
          order by JLBH)
  loop
    BFCRM10.YHQ_QD_MBJZDYD(item.JLBH,pZXR);
  end loop;

     /*FQ*/

   update BFCRM10.HYKFQDYD
     set STATUS=4,
         ZZRQ=sysdate  ,
         ZZR=pZXR,
         ZZRMC=vQDRMC
     where RQ2<to_date(to_char(vNextDay,'yyyy-mm-dd')||' 08:00:00','yyyy-mm-dd hh24:mi:ss')
       and STATUS<>3 and STATUS<>4;

     /*YQ*/
   update BFCRM10.HYKYQDYD
     set STATUS= 4,
         ZZRQ=sysdate ,
         ZZR=pZXR,
         ZZRMC=vQDRMC
     where RQ2<to_date(to_char(vNextDay,'yyyy-mm-dd')||' 08:00:00','yyyy-mm-dd hh24:mi:ss')
       and STATUS<>3 and STATUS <>4;

   /*MJ*/
   update BFCRM10.CXMBJZDYD
     set STATUS= 4,
         ZZRQ=sysdate,
         ZZR=pZXR,
         ZZRMC=vQDRMC
     where RQ2<to_date(to_char(vNextDay,'yyyy-mm-dd')||' 08:00:00','yyyy-mm-dd hh24:mi:ss')
       and STATUS<>3 and STATUS <>4;

   /*------------停用没有启动单据的银行号段-------------*/
   for item in (
    select distinct M.ID
      from BFCRM10.YHXXITEM M
      where nvl(BJ_TY,0)=0
        and exists(select 1 from BFCRM10.HYKFQDYD where M.SHDM=SHDM and M.CXID=CXID and STATUS=3 or STATUS=4)
        and not exists(select 1 from BFCRM10.HYKFQDYD where M.SHDM=SHDM and M.CXID=CXID and STATUS<>3 and STATUS<>4))
  loop
    update BFCRM10.YHXXITEM
       set BJ_TY=1
      where ID = item.ID;
  end loop;

  /*终止发券限额单据*/
  vCount:=0;
  select count(*) into vCount
    from CXHD_FQXEDYGZD A
   where A.STATUS=2
     and exists(select 1
                  from CXHDDEF
                 where CXID=A.CXID
                   and JSSJ<pRCLRQ+1);
  vCount:=nvl(vCount,0);

  if vCount>0 then
    update CXHD_FQXEDYGZD A set A.STATUS=3,A.ZZR=pZXR,A.ZZRMC=vQDRMC,A.ZZRQ=sysdate
     where A.STATUS=2
       and exists(select 1
                    from CXHDDEF
                   where CXID=A.CXID
                     and JSSJ<pRCLRQ+1);

    delete from CXHD_FQXEDEF A
     where exists(select 1
                    from CXHDDEF
                   where CXID=A.CXID
                     and JSSJ<pRCLRQ+1);
  end if;
end;
/

prompt
prompt Creating procedure HYXF_QD_HYJFDYD
prompt ==================================
prompt
create or replace procedure bfcrm10.HYXF_QD_HYJFDYD (
                              vJLBH in integer,
                              vQDR  in integer
                            )
is
  vBMID           integer;
  vBMDM           varchar2(20);
  vSHDM           varchar2(4);
  vHYKTYPE        number(5);
  vDJLX           number(5);
  vcurJLBH_PT     integer;
  vcurJLBH_YX     integer;
begin
  update BFCRM10.HYKJFDYD
     set QDR=vQDR,
         QDRMC=(select PERSON_NAME from BFCRM10.RYXX  where PERSON_ID=vQDR),
         QDSJ=sysdate,
         STATUS=2
    where JLBH=vJLBH
      and SHR is not null
      and QDR is null
      and STATUS=1;
  if SQL%NOTFOUND then
    raise_application_error(-20001,'本单已启动或不存在!');
    return;
  end if;
end;
/

prompt
prompt Creating procedure HYXF_QD_HYZKDYD
prompt ==================================
prompt
create or replace procedure bfcrm10.HYXF_QD_HYZKDYD (
                              vJLBH  in integer,
                              vQDR   in integer
                            )
is
  vBMID           integer;
  vBMDM           varchar2(20);
  vSHDM           varchar2(4);
  vHYKTYPE        number(5);
  vDJLX           number(5);
  vcurJLBH_PT     integer;
  vcurJLBH_YX     integer;
  vCount          integer;
begin
  update BFCRM10.HYKZKDYD
    set QDR=vQDR,
        QDRMC=(select PERSON_NAME from BFCRM10.RYXX  where PERSON_ID=vQDR),
        QDSJ=sysdate,
        STATUS=2
    where JLBH=vJLBH
      and SHR is not null
      and QDR is null
      and STATUS=1;
  if SQL%NOTFOUND then
    raise_application_error(-20001,'本单已启动或不存在!');
    return;
  end if;
end;
/

prompt
prompt Creating procedure HYXF_QD_HYCXDJ
prompt =================================
prompt
create or replace procedure bfcrm10.HYXF_QD_HYCXDJ (
                              pRCLRQ in date,
                              pZXR     in integer
                            )
is
  TYPE TJLBH is table of number(10) --BFCRM10.HYKJFDYD.JLBH%TYPE
    index by binary_integer;


  listJLBH TJLBH;
  vNextDay date;
  vCurDay  date;
begin
    --declare vJLBH   integer;
  vNextDay := pRCLRQ+1;
  vCurDay  := sysdate;

  --会员卡折扣定义单
  for item in (
      select JLBH
        from BFCRM10.HYKZKDYD
        where SHR is not null
          and QDR is null
          and STATUS=1
          and RQ1<=vNextDay
          and RQ2> vCurDay
        order by JLBH)
  loop
    BFCRM10.HYXF_QD_HYZKDYD(item.JLBH, pZXR);
  end loop;

  --会员卡积分定义单
  for item in (
      select JLBH
        from BFCRM10.HYKJFDYD
        where SHR is not null
          and QDR is null
          and STATUS=1
          and RQ1<=vNextDay
          and RQ2> vCurDay
        order by JLBH)
  loop
    BFCRM10.HYXF_QD_HYJFDYD(item.JLBH,pZXR);
  end loop;

     /*JF*/
  select JLBH bulk collect into listJLBH
    from BFCRM10.HYKJFDYD
    where RQ2<to_date(to_char(vNextDay,'yyyy-mm-dd')||' 08:00:00','yyyy-mm-dd hh24:mi:ss')
      and STATUS<>3 and STATUS<>4;

  forall vJLBH in listJLBH.First..listJLBH.Last
    update BFCRM10.HYKJFDYD
      set STATUS= 4,ZZRQ=sysdate  --过期单据
      where JLBH=listJLBH(vJLBH);

     /*****ZK*/
  select JLBH bulk collect into listJLBH
    from BFCRM10.HYKZKDYD
    where RQ2<to_date(to_char(vNextDay,'yyyy-mm-dd')||' 08:00:00','yyyy-mm-dd hh24:mi:ss')
      and STATUS<>3 and STATUS<>4;

  forall vJLBH in listJLBH.First..listJLBH.Last
    update BFCRM10.HYKZKDYD
      set STATUS= 4,ZZRQ=sysdate
      where JLBH=listJLBH(vJLBH);

end;
/

prompt
prompt Creating procedure HYXF_QD_HYCXDJ_BAK20170921
prompt =============================================
prompt
create or replace procedure bfcrm10.HYXF_QD_HYCXDJ_BAK20170921 (
                              pRclDate in date,
                              pQDR     in integer
                            )
is
  TYPE TJLBH is table of number(10) --BFCRM10.HYKJFDYD.JLBH%TYPE
    index by binary_integer;


  listJLBH TJLBH;
  vNextDay date;
  vCurDay  date;
begin
    --declare vJLBH   integer;
  vNextDay := pRclDate+1;
  vCurDay  := sysdate;

  --会员卡折扣定义单
  for item in (
      select JLBH
        from BFCRM10.HYKZKDYD
        where SHR is not null
          and QDR is null
          and STATUS=1
          and RQ1<=vNextDay
          and RQ2> vCurDay
        order by JLBH)
  loop
    BFCRM10.HYXF_QD_HYZKDYD(item.JLBH, pQDR);
  end loop;

  --会员卡积分定义单
  for item in (
      select JLBH
        from BFCRM10.HYKJFDYD
        where SHR is not null
          and QDR is null
          and STATUS=1
          and RQ1<=vNextDay
          and RQ2> vCurDay
        order by JLBH)
  loop
    BFCRM10.HYXF_QD_HYJFDYD(item.JLBH,pQDR);
  end loop;

     /*JF*/
  select JLBH bulk collect into listJLBH
    from BFCRM10.HYKJFDYD
    where RQ2<to_date(to_char(vNextDay,'yyyy-mm-dd')||' 08:00:00','yyyy-mm-dd hh24:mi:ss')
      and STATUS<>3 and STATUS<>4;

  forall vJLBH in listJLBH.First..listJLBH.Last
    update BFCRM10.HYKJFDYD
      set STATUS= 4,ZZRQ=sysdate  --过期单据
      where JLBH=listJLBH(vJLBH);

     /*****ZK*/
  select JLBH bulk collect into listJLBH
    from BFCRM10.HYKZKDYD
    where RQ2<to_date(to_char(vNextDay,'yyyy-mm-dd')||' 08:00:00','yyyy-mm-dd hh24:mi:ss')
      and STATUS<>3 and STATUS<>4;

  forall vJLBH in listJLBH.First..listJLBH.Last
    update BFCRM10.HYKZKDYD
      set STATUS= 4,ZZRQ=sysdate
      where JLBH=listJLBH(vJLBH);

end;
/

prompt
prompt Creating procedure HYXF_ZX_HYK_DJSQGZ
prompt =====================================
prompt
create or replace procedure bfcrm10.HYXF_ZX_HYK_DJSQGZ (
                              pRCLRQ in date
                            )
is
  vJLBH                 integer;
  vHYKTYPE              number(5);
  vHYKTYPE_NEW          number(5);
  vQDJF                 float;
  vHYID                 integer;
  vBQJF                 number(14,2);
  vSJRQ                 date;
  vYXQ                  date;
  vHYKTYPE_PREV         number(5);
  vQDJF_PREV            float;

  cursor crDJSQGZ is
     select HYKTYPE,HYKTYPE_NEW, QDJF
       from BFCRM10.HYK_DJSQGZ
     order by HYKTYPE asc,QDJF desc;

  cursor crTmpDJSQGZ is
     select HYID,HYKTYPE,HYKTYPE_NEW,BQJF,SJRQ,YXQ
       from BFCRM10.TMP_HYDJSQJL;
begin
  delete from BFCRM10.HYDJSQJL
    where months_between(pRCLRQ,YXQ)>=12;

  delete from BFCRM10.TMP_HYDJSQJL;


  vHYKTYPE_PREV := -1;
  vQDJF_PREV :=  0;
  open crDJSQGZ;
  fetch crDJSQGZ into vHYKTYPE,vHYKTYPE_NEW,vQDJF;
  while (crDJSQGZ%FOUND)
  loop
    if vHYKTYPE=vHYKTYPE_PREV then
      insert into BFCRM10.TMP_HYDJSQJL(HYID,HYKTYPE,HYKTYPE_NEW,BQJF,SJRQ,YXQ)
        select A.HYID,A.HYKTYPE,vHYKTYPE_NEW,B.WCLJF,pRCLRQ,A.YXQ
          from BFCRM10.HYK_HYXX A,BFCRM10.HYK_JFZH B
          where A.HYID=B.HYID
            and A.HYKTYPE=vHYKTYPE
            and A.STATUS>=0   --有效卡
            and B.WCLJF>=vQDJF
            and B.WCLJF<vQDJF_PREV;
    else
      insert into BFCRM10.TMP_HYDJSQJL(HYID,HYKTYPE,HYKTYPE_NEW,BQJF,SJRQ,YXQ)
        select A.HYID,A.HYKTYPE,vHYKTYPE_NEW,B.WCLJF,pRCLRQ,A.YXQ
          from BFCRM10.HYK_HYXX A,BFCRM10.HYK_JFZH B
          where A.HYID=B.HYID
            and A.HYKTYPE=vHYKTYPE
            and A.STATUS>=0   --有效卡
            and B.WCLJF>=vQDJF;
    end if;
    vHYKTYPE_PREV := vHYKTYPE;
    vQDJF_PREV :=  vQDJF;

    fetch crDJSQGZ into vHYKTYPE,vHYKTYPE_NEW,vQDJF;
  end loop;
  close crDJSQGZ;

   /*保留已升级的时间*/
  /*
  update BFCRM10.TMP_HYDJSQJL
    set SJRQ=H.SJRQ
    from BFCRM10.TMP_HYDJSQJL T,
          BFCRM10.HYDJSQJL H
    where T.HYID=H.HYID and
          T.HYKTYPE_NEW=H.HYKTYPE_NEW and
          T.YXQ=H.YXQ
  */
  update BFCRM10.TMP_HYDJSQJL L
    set SJRQ=(select H.SJRQ
                from BFCRM10.HYDJSQJL H
                where H.HYID=L.HYID
                  and H.HYKTYPE_NEW=HYKTYPE_NEW
                  and H.YXQ=YXQ)
    where exists(select 1
                   from BFCRM10.HYDJSQJL H
                   where H.HYID=L.HYID
                     and H.HYKTYPE_NEW=HYKTYPE_NEW
                     and H.YXQ=YXQ);

  delete from BFCRM10.HYDJSQJL
    where YXQ>=pRCLRQ;


  open crTmpDJSQGZ;
  fetch crTmpDJSQGZ into vHYID,vHYKTYPE,vHYKTYPE_NEW,vBQJF,vSJRQ,vYXQ;
  while (crTmpDJSQGZ%FOUND)
  loop
    update BFCRM10.HYDJSQJL
       set HYKTYPE=vHYKTYPE ,
           HYKTYPE_NEW=vHYKTYPE_NEW,
           BQJF=vBQJF,
           SJRQ=vSJRQ,
           YXQ=vYXQ
      where HYID=vHYID
        and YXQ=vYXQ;
    if SQL%NOTFOUND then
      insert into BFCRM10.HYDJSQJL(HYID,HYKTYPE,HYKTYPE_NEW,BQJF,SJRQ,YXQ)
        values(vHYID,vHYKTYPE,vHYKTYPE_NEW,vBQJF,vSJRQ,vYXQ);
    end if;
    fetch crTmpDJSQGZ into vHYID,vHYKTYPE,vHYKTYPE_NEW,vBQJF,vSJRQ,vYXQ;
  end loop;
  close crTmpDJSQGZ;
end;
/

prompt
prompt Creating procedure HYXF_ZX_HYK_JJCL
prompt ===================================
prompt
create or replace procedure bfcrm10.HYXF_ZX_HYK_JJCL (
                              pRclDate in date
                            )
is
  vHYKTYPE              number(5);
  vHYKTYPE_NEW          number(5);
  vQDJF                 float;
  vXFSJ                 number(5);
  vHYID                 integer;
  vHYKHM                varchar2(20);
  vWCLJF_OLD            float;
  vBQJF_OLD             float;
  vYJKRQ                date;
  vHYKTYPE_PREV         number(5);
  vQDJF_PREV            float;
  vSJJLBH               integer;
  vDJR                  integer;
  vDJRMC                varchar2(16);

  cursor Cur_HYK_DJSQGZ is
     select HYKTYPE,HYKTYPE_NEW,QDJF
       from BFCRM10.HYK_DJSQGZ
       where BJ_SJ=0
         and XFSJ=1
       order by HYKTYPE DESC,QDJF ASC;

  cursor Cur_HYK_SJJL is
     select HYID,HYKTYPE_OLD,HYKTYPE_NEW,HYKHM,WCLJF_OLD,BQJF_OLD,YJKRQ
       from BFCRM10.TMP_HYK_SJJL;
begin
  select min(PERSON_ID) into vDJR
    from BFCRM10.RYXX;
  select PERSON_NAME into vDJRMC
    from BFCRM10.RYXX
    where PERSON_ID=vDJR;

  delete from BFCRM10.TMP_HYK_SJJL;

  vHYKTYPE_PREV := -1;
  vQDJF_PREV :=  0;
  open Cur_HYK_DJSQGZ;
  fetch Cur_HYK_DJSQGZ into vHYKTYPE,vHYKTYPE_NEW,vQDJF;
  while (Cur_HYK_DJSQGZ%FOUND)
  loop
    vSJJLBH := BFCRM10.Update_BHZT('HYK_SJJL');
    if vHYKTYPE=vHYKTYPE_PREV then
      insert into BFCRM10.TMP_HYK_SJJL(HYID,HYKTYPE_OLD,HYKTYPE_NEW,HYKHM,WCLJF_OLD,BQJF_OLD,YJKRQ)
        select A.HYID,A.HYKTYPE,vHYKTYPE_NEW,A.HYK_NO,B.WCLJF,B.BQJF,A.JKRQ
          from BFCRM10.HYK_HYXX A,BFCRM10.HYK_JFZH B
          where A.HYID=B.HYID
            and A.HYKTYPE=vHYKTYPE
            and A.STATUS>=0   --有效卡
            and A.YXQ<=pRclDate
            and B.BQJF<=vQDJF
            and B.BQJF>vQDJF_PREV;
    else
      insert into BFCRM10.TMP_HYK_SJJL(HYID,HYKTYPE_OLD,HYKTYPE_NEW,HYKHM,WCLJF_OLD,BQJF_OLD,YJKRQ)
        select A.HYID,A.HYKTYPE,vHYKTYPE_NEW,A.HYK_NO,B.WCLJF,B.BQJF,A.JKRQ
          from BFCRM10.HYK_HYXX A,BFCRM10.HYK_JFZH B
          where A.HYID=B.HYID
            and A.HYKTYPE=vHYKTYPE
            and A.STATUS>=0   --有效卡
            and A.YXQ<=pRclDate
            and B.BQJF<=vQDJF;
    end if;
    vHYKTYPE_PREV := vHYKTYPE;
    vQDJF_PREV    :=  vQDJF;

    fetch Cur_HYK_DJSQGZ into vHYKTYPE,vHYKTYPE_NEW,vQDJF;
  end loop;
  close Cur_HYK_DJSQGZ;

  /*open Cur_HYK_SJJL;
  fetch Cur_HYK_SJJL into vHYID,vHYKTYPE,vHYKTYPE_NEW,vHYKHM,vWCLJF_OLD,vBQJF_OLD,vYJKRQ;
  while (Cur_HYK_SJJL%FOUND)
  loop

    vSJJLBH := BFCRM10.Update_BHZT('HYK_SJJL');

    insert into BFCRM10.HYK_SJJL(JLBH,HYID,HYKTYPE_OLD,HYKTYPE_NEW,HYKHM_OLD,HYKHM_NEW,WCLJF_OLD,BQJF_OLD,SJJF,ZY,DJR,DJRMC,DJSJ,ZXR,ZXRMC,ZXRQ,BJ_SJ,YJKRQ)
      values(vSJJLBH,vHYID,vHYKTYPE,vHYKTYPE_NEW,vHYKHM,vHYKHM,vWCLJF_OLD,vBQJF_OLD,vBQJF_OLD,'会员降级（日处理）',vDJR,vDJRMC,sysdate,vDJR,vDJRMC,pRclDate,0,vYJKRQ);

    update BFCRM10.HYK_HYXX
      set HYKTYPE=vHYKTYPE_NEW,
          YXQ    =add_months(trunc(pRclDate),12),
          JKRQ   =pRclDate
      where HYID=vHYID;

    update BFCRM10.HYK_JFBDJLMX
      set BQJFBD=BQJFBD+nvl(vBQJF_OLD*(-1),0)
      where HYID=vHYID
        and RQ=pRclDate
        and CLLX=13
        and JLBH=vSJJLBH;
    if SQL%NOTFOUND then
      insert into BFCRM10.HYK_JFBDJLMX(HYID,RQ,CLLX,JLBH,BQJFBD)
        values(vHYID,pRclDate,13,vSJJLBH,nvl(vBQJF_OLD*(-1),0));
    end if;

    update BFCRM10.HYK_JFZH
      set BQJF=0
      where HYID=vHYID;

    update BFCRM10.HYK_MDJF
      set BQJF=0
      where HYID=vHYID;

    fetch Cur_HYK_SJJL into vHYID,vHYKTYPE,vHYKTYPE_NEW,vHYKHM,vWCLJF_OLD,vBQJF_OLD,vYJKRQ;
  end loop;
  close Cur_HYK_SJJL;*/
end;
/

prompt
prompt Creating procedure HYXF_ZX_HYK_YXQYCGZ
prompt ======================================
prompt
create or replace procedure bfcrm10.HYXF_ZX_HYK_YXQYCGZ (
                              pRCLRQ in date
                            )
is
  TYPE THYID is table of BFCRM10.HYK_YXQBDJLITEM.HYID%TYPE
    index by binary_integer;
  listHYID THYID;

  vJLBH           integer;
  vHYKTYPE        number(5);
  vXMJF_SX        float;
  vBJ             varchar2(1);
  vSJ             integer;
  vSTR_YXQ        varchar2(4);
  vYXQ            date;
  vRCL_DATE_NEXT  date;
  vDJR            integer;
  vDJRMC          varchar2(16);

  cursor Cur_HYK_GZ is
     select HYKTYPE, QDXFJE
       from BFCRM10.HYK_YXQYCGZ;
begin
  vRCL_DATE_NEXT := pRCLRQ+1;
  select min(PERSON_ID) into vDJR
    from BFCRM10.RYXX;
  select PERSON_NAME into vDJRMC
    from BFCRM10.RYXX
    where PERSON_ID=vDJR;

  open Cur_HYK_GZ;
  fetch Cur_HYK_GZ into vHYKTYPE,vXMJF_SX;
  while (Cur_HYK_GZ%FOUND)
  loop
    select YXQCD into vSTR_YXQ from BFCRM10.HYKDEF where HYKTYPE=vHYKTYPE;

    vBJ := substr(vSTR_YXQ,length(vSTR_YXQ),1);
    vSJ := to_number(substr(vSTR_YXQ,1,length(vSTR_YXQ)-1));
    if vBJ='Y' then
      vYXQ := add_months(pRCLRQ,vSJ*12);
    elsif vBJ='M' then
      vYXQ := add_months(pRCLRQ,vSJ);
    elsif vBJ='D' then
      vYXQ := pRCLRQ+vSJ;
    end if;

    vJLBH := BFCRM10.Update_BHZT('HYK_YXQBDJL');
    insert into  BFCRM10.HYK_YXQBDJL(CZJPJ_JLBH,HYKTYPE,ZY,XYXQ,DJR,DJRMC,DJSJ,ZXR,ZXRMC,ZXRQ)
      values(vJLBH,vHYKTYPE,'系统自动延期',vYXQ,vDJR,vDJRMC,sysdate,vDJR,vDJRMC,pRCLRQ);

    insert into  BFCRM10.HYK_YXQBDJLITEM(CZJPJ_JLBH,HYID,YYXQ)
      select  vJLBH,H.HYID,YXQ
        from  BFCRM10.HYK_HYXX H,
              BFCRM10.HYK_JFZH J
       where  H.HYID=J.HYID and
              H.HYKTYPE=vHYKTYPE and
              YXQ>=pRCLRQ and
              YXQ< vRCL_DATE_NEXT and
              J.XFJE>=vXMJF_SX;

    select HYID bulk collect into listHYID
      from BFCRM10.HYK_YXQBDJLITEM
      where CZJPJ_JLBH=vJLBH;

    forall vHYID in listHYID.First..listHYID.Last
      update BFCRM10.HYK_HYXX
        set YXQ=vYXQ
        where HYID=listHYID(vHYID);

    forall vHYID in listHYID.First..listHYID.Last
      update BFCRM10.HYK_JFZH
        set XFJE=0
        where HYID=listHYID(vHYID);

    forall vHYID in listHYID.First..listHYID.Last
      update BFCRM10.HYK_MDJF
        set XFJE=0
        where HYID=listHYID(vHYID);

    fetch Cur_HYK_GZ into vHYKTYPE,vXMJF_SX;
  end loop;
  close Cur_HYK_GZ;
end;
/

prompt
prompt Creating procedure HYXF_ZX_HYK_ZTBG
prompt ===================================
prompt
create or replace procedure bfcrm10.HYXF_ZX_HYK_ZTBG (
                              pRclDate in date
                            )
is
  vHYKTYPE              integer;
  vHYKTYPE_PREV         integer;
  vWSYSJ                number(5);
  vWSYSJ_PREV           number(5);
  vGZLX                 number(5);
  vSJJLBH               integer;
  vDJR                  integer;
  vDJRMC                varchar2(16);
  vHYID                 integer;
  vNEW_STATUS           number(5);
  vSTATUS               number(5);
  vChkRQ1               date;
  vChkRQ2               date;

  cursor crZTBGGZ is
     select HYKTYPE,WSYSJ,GZLX
       from BFCRM10.HYK_ZTBGGZ
       order by HYKTYPE ASC,GZLX ASC;

  cursor crTmpHYXXBG is
    select HYID,STATUS,GZLX
      from BFCRM10.TMP_HYK_HYXX_BG
      order by HYID;

begin
  select min(PERSON_ID) into vDJR
    from BFCRM10.RYXX;
  select PERSON_NAME into vDJRMC
    from BFCRM10.RYXX
    where PERSON_ID=vDJR;

  update BFCRM10.HYK_XFJL
    set CRMJZRQ=pRclDate
    where STATUS=1
      and CRMJZRQ is null;

  delete from BFCRM10.TMP_HYK_HYXX_BG;

  vHYKTYPE_PREV := -1;
  vWSYSJ_PREV   :=  0;
  open crZTBGGZ;
  fetch crZTBGGZ into vHYKTYPE,vWSYSJ,vGZLX;
  while (crZTBGGZ%FOUND)
  loop
    if vHYKTYPE=vHYKTYPE_PREV then
      vWSYSJ_PREV := vWSYSJ_PREV+vWSYSJ;

      vChkRQ1 := add_months(pRclDate,vWSYSJ_PREV*(-1));
      vChkRQ2 := vChkRQ2+1;
      if vGZLX=1 then
        insert into BFCRM10.TMP_HYK_HYXX_BG(HYID,STATUS,GZLX)
          select HYID,STATUS,vGZLX
            from BFCRM10.HYK_HYXX
            where JKRQ>=vChkRQ1
              and JKRQ< vChkRQ2
              and HYKTYPE=vHYKTYPE
              and STATUS>=0
              and STATUS<>3
              and STATUS<>6;
      elsif vGZLX=2 then
        insert into BFCRM10.TMP_HYK_HYXX_BG(HYID,STATUS,GZLX)
          select HYID,STATUS,vGZLX
            from BFCRM10.HYK_HYXX
            where JKRQ>=vChkRQ1
              and JKRQ<vChkRQ2
              and HYKTYPE=vHYKTYPE
              and STATUS=3;
      elsif vGZLX=3 then
        insert into BFCRM10.TMP_HYK_HYXX_BG(HYID,STATUS,GZLX)
          select HYID,STATUS,vGZLX
            from BFCRM10.HYK_HYXX
            where JKRQ>=vChkRQ1
              and JKRQ<vChkRQ2
              and HYKTYPE=vHYKTYPE
              and STATUS=6;
      elsif vGZLX=4 then
        insert into BFCRM10.TMP_HYK_HYXX_BG(HYID,STATUS,GZLX)
          select HYID,STATUS,vGZLX
            from BFCRM10.HYK_HYXX
            where JKRQ>=vChkRQ1
              and JKRQ< vChkRQ2
              and HYKTYPE=vHYKTYPE
              and STATUS=-6;
      end if;
    else
      vWSYSJ_PREV := vWSYSJ;
      vChkRQ1 := add_months(pRclDate,vWSYSJ_PREV*(-1));
      vChkRQ2 := vChkRQ2+1;

      if vGZLX=1 then
        insert into BFCRM10.TMP_HYK_HYXX_BG(HYID,STATUS,GZLX)
          select HYID,STATUS,vGZLX
            from BFCRM10.HYK_HYXX
            where JKRQ>=vChkRQ1
              and JKRQ< vChkRQ2
              and HYKTYPE=vHYKTYPE
              and STATUS>=0
              and STATUS<>3
              and STATUS<>6;
      elsif vGZLX=2 then
        insert into BFCRM10.TMP_HYK_HYXX_BG(HYID,STATUS,GZLX)
          select HYID,STATUS,vGZLX
            from BFCRM10.HYK_HYXX
            where JKRQ>=vChkRQ1
              and JKRQ< vChkRQ2
              and HYKTYPE=vHYKTYPE
              and STATUS=3;
      elsif vGZLX=3 then
        insert into BFCRM10.TMP_HYK_HYXX_BG(HYID,STATUS,GZLX)
          select HYID,STATUS,vGZLX
            from BFCRM10.HYK_HYXX
            where JKRQ>=vChkRQ1
              and JKRQ< vChkRQ2
              and HYKTYPE=vHYKTYPE
              and STATUS=6;
      elsif vGZLX=4 then
        insert into BFCRM10.TMP_HYK_HYXX_BG(HYID,STATUS,GZLX)
          select HYID,STATUS,vGZLX
            from BFCRM10.HYK_HYXX
            where JKRQ>=vChkRQ1
              and JKRQ< vChkRQ2
              and HYKTYPE=vHYKTYPE
              and STATUS=-6;
      end if;
    end if;

    delete from BFCRM10.TMP_HYK_HYXX_BG M
     where M.GZLX=vGZLX
       and M.HYID in (select L.HYID from BFCRM10.HYK_XFJL L where L.HYID=L.HYID and L.STATUS=1);

    delete from BFCRM10.TMP_HYK_HYXX_BG M
      where M.GZLX=vGZLX
        and M.HYID in (select L.HYID
                         from BFCRM10.HYXFJL L
                         where L.CRMJZRQ<=pRclDate
                           and L.CRMJZRQ>=add_months(pRclDate,vWSYSJ_PREV*(-1)));

    vHYKTYPE_PREV := vHYKTYPE;
    fetch crZTBGGZ into vHYKTYPE,vWSYSJ,vGZLX;
  end loop;
  close crZTBGGZ;

  open crTmpHYXXBG;
  fetch crTmpHYXXBG into vHYID,vSTATUS,vGZLX;
  while (crTmpHYXXBG%FOUND)
  loop
    case vGZLX
      when 1 then vNEW_STATUS := 3;
      when 2 then vNEW_STATUS := 6;
      when 3 then vNEW_STATUS := -6;
      when 4 then vNEW_STATUS := -1;
    end case;

    vSJJLBH := BFCRM10.Update_BHZT('HYK_ZTBDJL');

    insert into BFCRM10.HYK_ZTBDJL(JLBH,NEW_STATUS,ZY,DJR,DJRMC,DJSJ,ZXR,ZXRMC,ZXRQ)
        values(vSJJLBH,vNEW_STATUS,'状态变动',vDJR,vDJRMC,sysdate,vDJR,vDJRMC,pRclDate);

    insert into BFCRM10.HYK_ZTBDJLITEM(JLBH,HYID,OLD_STATUS)
        values(vSJJLBH,vHYID,vSTATUS);

    update BFCRM10.HYK_HYXX
      set STATUS=vNEW_STATUS
      where HYID=vHYID;

    fetch crTmpHYXXBG into vHYID,vSTATUS,vGZLX;
  end loop;
  close crTmpHYXXBG;
end;
/

prompt
prompt Creating procedure HYXF_ZX_MDJF_SJFT
prompt ====================================
prompt
create or replace procedure bfcrm10.HYXF_ZX_MDJF_SJFT (
                              pHYID in integer,
                              pQDJF in float
                            )
is
  vJFFTBL   float;
  vSumJF    float;

begin

  select sum(WCLJF) into vSumJF
    from BFCRM10.HYK_MDJF
    where HYID=pHYID;
  if vSumJF > 0 then
    vJFFTBL := pQDJF / vSumJF;
  end if;

  update BFCRM10.HYK_MDJF
    set WCLJF=round(WCLJF*(1-vJFFTBL),3)
    where HYID=pHYID;

end;
/

prompt
prompt Creating procedure HYYQDYD_ZZ
prompt =============================
prompt
create or replace procedure bfcrm10.HYYQDYD_ZZ (
                              pRYID in integer,
                              pRYMC in varchar2,
                              pJLBH in integer
                            )
is
  vBMDM varchar2(10);
  vSHDM varchar2(4);
  vYHQID integer;
begin
  select SHBMDM,SHDM, YHQID
    into vBMDM, vSHDM,vYHQID
    from BFCRM10.HYKYQDYD
    where JLBH=pJLBH;

  update BFCRM10.HYKYQDYD
    set ZZR=pRYID,ZZRMC=pRYMC,ZZRQ=sysdate,STATUS=3     --提前终止状态
    where JLBH=pJLBH;

  delete from BFCRM10.GZSJ_HYYQ where JLBH=pJLBH;
end;
/

prompt
prompt Creating procedure HYZKDYD_ZZ
prompt =============================
prompt
create or replace procedure bfcrm10.HYZKDYD_ZZ (
                              pRYID in integer,
                              pRYMC in varchar2,
                              pJLBH in integer
                            )
is
  vBMDM varchar2(10);
  vSHDM varchar2(4);
  vHYKTYPE integer;
begin
  select SHBMDM,SHDM, HYKTYPE
    into vBMDM, vSHDM,vHYKTYPE
    from BFCRM10.HYKZKDYD
    where JLBH=pJLBH;

  update BFCRM10.HYKZKDYD
    set ZZR=pRYID,ZZRMC=pRYMC,ZZRQ=sysdate,STATUS=3     --提前终止状态
    where JLBH=pJLBH;
  delete from BFCRM10.GZSJ_HYZKD where JLBH=pJLBH;
end;
/

prompt
prompt Creating procedure HZ_MBJZ_CXHDSJ
prompt =================================
prompt
create or replace procedure bfcrm10.HZ_MBJZ_CXHDSJ (
                              pRCLRQ in date
                            )
is
begin
  delete from  BFCRM10.CXHDMBJZ_SPFTRJL where RQ=pRCLRQ;
  delete from  BFCRM10.MBJZ_CXHD_BMHZ where RQ=pRCLRQ;
  delete from  BFCRM10.MBJZ_CXHD_HTHZ where RQ=pRCLRQ;

   /*********************按商品汇总**********************************/
  insert into BFCRM10.CXHDMBJZ_SPFTRJL(RQ,SHDM,MDID,BMDM,SHSPID,CXID,MBJZGZID,XFLJMJFS,ZXSJE,ZXSJE_MBJZ,ZMBJZJE)
    select pRCLRQ,L.SHDM,L.MDID,Z.BMDM,nvl(Z.SHSPID,-1),Z.CXID,Z.MBJZGZID,Z.XFLJMJFS,sum(Z.XSJE),sum(Z.XSJE_MBJZ),sum(Z.MBJZJE)
      from BFCRM10.HYK_XFJL_SP_MBJZ Z,
           BFCRM10.HYK_XFJL L
      where L.XFJLID=Z.XFJLID
        and L.STATUS=1
        and L.CRMJZRQ=pRCLRQ
      group by L.SHDM,L.MDID,Z.BMDM,Z.SHSPID,Z.CXID,Z.MBJZGZID,Z.XFLJMJFS;

  /**************************按合同汇总*****************************************/
  insert into BFCRM10.MBJZ_CXHD_HTHZ(RQ,SHDM,BMDM,MDID,CXID,MBJZGZID,SHHTID,XFLJMJFS,ZXSJE,ZXSJE_MBJZ,ZMBJZJE)
    select pRCLRQ,L.SHDM,Z.BMDM,L.MDID,Z.CXID,Z.MBJZGZID,nvl(Z.SHHTID,-1),Z.XFLJMJFS,sum(Z.XSJE),sum(Z.XSJE_MBJZ),sum(Z.MBJZJE)
      from BFCRM10.HYK_XFJL_SP_MBJZ Z,
           BFCRM10.HYK_XFJL L
      where L.XFJLID=Z.XFJLID
        and L.STATUS=1
        and L.CRMJZRQ=pRCLRQ
      group by L.SHDM,Z.BMDM,L.MDID,Z.CXID,Z.MBJZGZID,Z.SHHTID,Z.XFLJMJFS;

  /****************************按部门汇总**********************************************/
  insert into BFCRM10.MBJZ_CXHD_BMHZ(RQ,SHDM,BMDM,MDID,CXID,MBJZGZID,XFLJMJFS,ZXSJE,ZXSJE_MBJZ,ZMBJZJE)
    select pRCLRQ,SHDM,BMDM,MDID,CXID,MBJZGZID,XFLJMJFS,sum(ZXSJE),sum(ZXSJE_MBJZ),sum(ZMBJZJE)
      from BFCRM10.MBJZ_CXHD_HTHZ
      where RQ=pRCLRQ
      group by SHDM,BMDM,MDID,CXID,MBJZGZID,XFLJMJFS;
end;
/

prompt
prompt Creating procedure HZ_YHQ_CXHDBYBM
prompt ==================================
prompt
create or replace procedure bfcrm10.HZ_YHQ_CXHDByBM (
                              pRCLRQ in date
                            )
is
begin
  delete from  BFCRM10.YHQ_CXHD_BMHZ where RQ=pRCLRQ;

  insert into BFCRM10.YHQ_CXHD_BMHZ(RQ, SHDM,  MDID, CXID, YHQID, BMDM,FQJE,YQJE,ZXFJE)
    select RQ,SHDM,MDID,CXID,YHQID,BMDM ,sum(FQJE),sum(YQJE),sum(ZXFJE)
      from BFCRM10.YHQ_CXHD_HZ
      where RQ=pRCLRQ
      group by RQ,SHDM,MDID,CXID,YHQID,BMDM;
end;
/

prompt
prompt Creating procedure HZ_YHQ_CXHDBYHT
prompt ==================================
prompt
create or replace procedure bfcrm10.HZ_YHQ_CXHDByHT (
                              pRCLRQ in date
                            )
is
begin
  delete from  BFCRM10.YHQ_CXHD_HTHZ where RQ=pRCLRQ;

  insert into BFCRM10.YHQ_CXHD_HTHZ(RQ, SHDM,  MDID, CXID, YHQID,HTH, BMDM,FQJE,YQJE,ZXFJE)
    select C.RQ,C.SHDM,C.MDID,C.CXID,C.YHQID,to_number(H.HTH),C.BMDM ,sum(FQJE),sum(YQJE),sum(ZXFJE)
      from BFCRM10.YHQ_CXHD_HZ C,BFCRM10.SHSPXX X,BFCRM10.SHHT H
      where X.SHSPID=C.SHSPID
        and X.SHHTID=H.SHHTID(+)
        and RQ=pRCLRQ
      group by C.RQ,C.SHDM,C.MDID,C.CXID,C.YHQID,to_number(H.HTH),C.BMDM;
end;
/

prompt
prompt Creating procedure TMP_CALC_SP_FQJE
prompt ===================================
prompt
create or replace procedure bfcrm10.TMP_Calc_SP_FQJE (
                              pRclDate in date
                            )
is
  vSHDM      varchar2(4);
  vCXID    integer;
  vINX_XFRQ  integer;
  vYHQID     integer;
  vYHQFFGZID integer;
  vFQJE      number(14,2);
  vREAL_FQJE number(14,2);
  vSPID      integer;
  vMDID      integer;
  vBMDM      varchar2(10);
  vZXFJE     number(14,2);
  vINX       integer;
  vXFJLID    integer;
  vFQJE_SP   number(14,2);
  vXFJLID_OLD integer;
  vHYID       integer;
  vMXXSJE    number(14,2);
  vFQJE_J    number(14,2);
  vXSJE      number(14,2);
  vTMPJE     number(14,2);

  cursor Cur_YHQ is
    select H.SHDM,H.MDID,S.CXID, XFLJFQFS,S.YHQID,YHQFFGZID,sum(S.FQJE) FQJE
      from BFCRM10.HYXFJL_FQ S,BFCRM10.HYXFJL H,BFCRM10.YHQDEF F
      where S.XFJLID=H.XFJLID
        and S.YHQID=F.YHQID
        and H.CRMJZRQ =pRclDate
        and not (XFLJFQFS in (0,3,4,5,6))
        and ((H.HYID_FQ >0 and F.BJ_DZYHQ=1) or (F.BJ_DZYHQ=0))
      group by H.SHDM,H.MDID,S.CXID,XFLJFQFS,S.YHQID,YHQFFGZID;

  cursor crTmpYHQ is
     select SHDM,MDID,CXID,INX_XFRQ,YHQID,YHQFFGZID,sum(FQJE)
       from  BFCRM10.TMP_YHQFF_SPFT_SP
       group by  SHDM,MDID,CXID,INX_XFRQ,YHQID,YHQFFGZID;

  cursor Cur_SPFQMX is
    select YHQID,INX,XSJE_FQ
      from BFCRM10.TMP_HYXFJL_SP_FQ_DBFT;

   cursor Cur_SPFQ is
    select A.XFJLID,A.YHQFFGZID,sum(A.FQJE)
        from BFCRM10.HYXFJL_FQ A,
             BFCRM10.HYXFJL B,
             BFCRM10.YHQDEF F
       where A.XFJLID=B.XFJLID
         and A.YHQID=F.YHQID
         and B.CRMJZRQ=pRclDate
         and A.XFLJFQFS in (0,4,5,6)
         and ((B.HYID_FQ >0 and F.BJ_DZYHQ=1) or (F.BJ_DZYHQ=0))
    group by A.XFJLID,A.YHQFFGZID;
begin
  delete from BFCRM10.TMP_YHQFF_SPFT_SP;
  delete from BFCRM10.TMP_YHQFF_SPFT;
  delete from BFCRM10.YHQFF_SPFTRJL  where RQ=pRclDate;
  delete from BFCRM10.YHQ_CXHD_HZ  where RQ=pRclDate;

  /*处理单笔返券的发券金额分摊 */
  open Cur_SPFQ;
  fetch Cur_SPFQ into vXFJLID,vYHQFFGZID,vFQJE;
  while (Cur_SPFQ%FOUND)
  loop
    vFQJE_J := vFQJE;
    delete from BFCRM10.TMP_HYXFJL_SP_FQ_DBFT;

    insert into BFCRM10.TMP_HYXFJL_SP_FQ_DBFT(YHQID,INX,XSJE_FQ)
    select F.YHQID,F.INX,sum(F.XSJE_FQ)
      from BFCRM10.HYXFJL_SP_FQ F
    where F.XFJLID=vXFJLID
      and F.XFLJFQFS in (0,4,5,6)
      and F.YHQFFGZID=vYHQFFGZID
    group by F.YHQID,F.INX;

    vXSJE := 0;
    select sum(XSJE_FQ) into vXSJE
      from TMP_HYXFJL_SP_FQ_DBFT;
    vXSJE := nvl(vXSJE,0);

    open Cur_SPFQMX;
    fetch Cur_SPFQMX into vYHQID,vINX,vMXXSJE;
    while (Cur_SPFQMX%FOUND)
    loop
      vTmpJE := round(nvl(vFQJE,0) * (nvl(vMXXSJE,0)/nvl(vXSJE,0)),2);
      update BFCRM10.HYXFJL_SP_FQ set FQJE=vTmpJE
       where XFJLID=vXFJLID
         and YHQID=vYHQID
         and INX=vINX
         and YHQFFGZID=vYHQFFGZID;

      vFQJE_J := round(vFQJE_J - vTmpJE,2);
      fetch Cur_SPFQMX into vYHQID,vINX,vMXXSJE;
    end loop;
    close Cur_SPFQMX;
    /*处理各规则分摊后的尾差，挤入最后一笔的发券金额*/
    if vFQJE_J<>0 then
      update BFCRM10.HYXFJL_SP_FQ set FQJE =FQJE + vFQJE_J
       where XFJLID=vXFJLID
         and YHQID=vYHQID
         and INX=vINX
         and YHQFFGZID=vYHQFFGZID;
    end if;

    fetch Cur_SPFQ into vXFJLID,vYHQFFGZID,vFQJE;
  end loop;
  close Cur_SPFQ;

  /*处理非单件且非单笔返券的发券金额分摊   */
  /*按商户代码，门店,促销活动编号，消费累计发券方式，优惠券，发放规则，商品，部门汇总HYK_XFJL_SP_FQ中的总销售金额*/
  insert into BFCRM10.TMP_YHQFF_SPFT_SP(SHDM,MDID,CXID,INX_XFRQ,YHQID,YHQFFGZID,SHSPID,BMDM,ZXFJE, FQJE)
    select H.SHDM,H.MDID,S.CXID,XFLJFQFS,S.YHQID,YHQFFGZID,SHSPID,BMDM,sum(XSJE_FQ) ZXFJE,0
      from BFCRM10.HYXFJL_SP_FQ S,BFCRM10.HYXFJL H ,BFCRM10.YHQDEF F
      where S.XFJLID=H.XFJLID
        and S.YHQID=F.YHQID
        and H.CRMJZRQ =pRclDate
        and ((H.HYID_FQ >0 AND F.BJ_DZYHQ=1) or (F.BJ_DZYHQ=0))
        and not (XFLJFQFS in(0,3,4,5,6))
      group by H.SHDM,H.MDID,S.CXID,XFLJFQFS,S.YHQID,YHQFFGZID,SHSPID,BMDM;


  /*按商户代码，门店,促销活动编号，消费累计发券方式，优惠券，发放规则，汇总HYK_XFJL_SP_FQ中

的总销售金额*/
  insert into BFCRM10.TMP_YHQFF_SPFT
    select SHDM,MDID,CXID,INX_XFRQ,YHQID,YHQFFGZID,SUM(ZXFJE),0 ,0
      from BFCRM10.TMP_YHQFF_SPFT_SP
    group by  SHDM,MDID,CXID,INX_XFRQ,YHQID,YHQFFGZID
    having SUM(ZXFJE)<>0;

  open Cur_YHQ;
  fetch Cur_YHQ into vSHDM,vMDID,vCXID,vINX_XFRQ,vYHQID,vYHQFFGZID ,vFQJE;
  while (Cur_YHQ%FOUND)
  loop
    update BFCRM10.TMP_YHQFF_SPFT
      set FQJE=vFQJE
      where SHDM=vSHDM
        and MDID=vMDID
        and CXID=vCXID
        and INX_XFRQ=vINX_XFRQ
        and YHQID=vYHQID
        and YHQFFGZID=vYHQFFGZID;
    fetch Cur_YHQ into vSHDM,vMDID,vCXID,vINX_XFRQ,vYHQID,vYHQFFGZID ,vFQJE;
  end loop;
  close Cur_YHQ;

  /*按商户代码，门店,促销活动编号，消费累计发券方式，优惠券，发放规则汇总总销售金额，发券金额，比例*/
  update BFCRM10.TMP_YHQFF_SPFT
    --set BL=CONVERT(FLOAT,FQJE)/ZXFJE
    set BL=FQJE/ZXFJE
    where ZXFJE<>0;

  update BFCRM10.TMP_YHQFF_SPFT_SP Y
    set FQJE=nvl(round((select S.BL from BFCRM10.TMP_YHQFF_SPFT S
                          where Y.SHDM=S.SHDM
                            and Y.MDID=S.MDID
                            and Y.CXID=S.CXID
                            and Y.INX_XFRQ=S.INX_XFRQ
                            and Y.YHQID=S.YHQID
                            and Y.YHQFFGZID=S.YHQFFGZID) * Y.ZXFJE,2),0)
    where exists(
            select 1 from BFCRM10.TMP_YHQFF_SPFT S
              where Y.SHDM=S.SHDM
                and Y.MDID=S.MDID
                and Y.CXID=S.CXID
                and Y.INX_XFRQ=S.INX_XFRQ
                and Y.YHQID=S.YHQID
                and Y.YHQFFGZID=S.YHQFFGZID
                and S.ZXFJE<>0);

  /*处理各规则分摊后的尾差，顺次挤入最大的发券金额*/
  open crTmpYHQ;
  fetch crTmpYHQ into vSHDM,vMDID,vCXID,vINX_XFRQ,vYHQID,vYHQFFGZID ,vFQJE;
  while (crTmpYHQ%FOUND)
  loop
    select sum(FQJE) into vREAL_FQJE
      from BFCRM10.TMP_YHQFF_SPFT
      where SHDM=vSHDM
        and MDID=vMDID
        and CXID=vCXID
        and INX_XFRQ=vINX_XFRQ
        and YHQID=vYHQID
        and YHQFFGZID=vYHQFFGZID;

    if (vREAL_FQJE<>vFQJE) then
      select max(SHSPID) into vSPID
        from BFCRM10.TMP_YHQFF_SPFT_SP
        where FQJE=(select MAX(FQJE)
                      from BFCRM10.TMP_YHQFF_SPFT_SP
                      where SHDM=vSHDM
                        and MDID=vMDID
                        and CXID=vCXID
                        and INX_XFRQ=vINX_XFRQ
                        and YHQID=vYHQID
                        and YHQFFGZID=vYHQFFGZID);

      update BFCRM10.TMP_YHQFF_SPFT_SP
        set FQJE = FQJE + nvl((vREAL_FQJE - vFQJE),0)
        where SHDM=vSHDM
          and MDID=vMDID
          and CXID=vCXID
          and INX_XFRQ=vINX_XFRQ
          and YHQID=vYHQID
          and YHQFFGZID=vYHQFFGZID
          and SHSPID=vSPID;
    end if;

    fetch crTmpYHQ into vSHDM,vMDID,vCXID,vINX_XFRQ,vYHQID,vYHQFFGZID ,vFQJE;
  end loop;
  close crTmpYHQ;

  /*处理单件返券的发券金额分摊*/
  --BFCRM10.Calc_SP_FQJE_DJFQ(pRclDate);

  /*处理无规则的发券金额分摊*/
  --BFCRM10.Calc_SP_FQJE_WC_WGZ(pRclDate);

  insert into BFCRM10.TMP_YHQFF_SPFT_SP(SHDM,MDID,CXID,INX_XFRQ,YHQID,YHQFFGZID,SHSPID,BMDM,ZXFJE, FQJE)
    select H.SHDM,H.MDID,S.CXID,S.XFLJFQFS,S.YHQID,S.YHQFFGZID,S.SHSPID,S.BMDM,sum(S.XSJE_FQ) ZXFJE,sum(S.FQJE)
      from BFCRM10.HYXFJL_SP_FQ S,BFCRM10.HYXFJL H ,BFCRM10.YHQDEF F
      where S.XFJLID=H.XFJLID
        and S.YHQID=F.YHQID
        and H.CRMJZRQ =pRclDate
        and ((H.HYID_FQ >0 AND F.BJ_DZYHQ=1) or (F.BJ_DZYHQ=0))
        and S.XFLJFQFS in(0,4,5,6)
   group by H.SHDM,H.MDID,S.CXID,S.XFLJFQFS,S.YHQID,S.YHQFFGZID,S.SHSPID,S.BMDM;

  insert into BFCRM10.YHQFF_SPFTRJL(RQ,SHDM,CXID,INX_XFRQ,YHQID,YHQFFGZID,SHSPID,BMDM,ZXFJE,FQJE,MDID)
    select pRclDate ,SHDM,CXID,INX_XFRQ,YHQID,YHQFFGZID,SHSPID,BMDM,ZXFJE,FQJE,MDID
      from BFCRM10.TMP_YHQFF_SPFT_SP;

  insert into BFCRM10.YHQ_CXHD_HZ(RQ,SHDM,MDID,CXID,YHQID,SHSPID,BMDM,YQJE,FQJE,ZXFJE)
    select pRclDate,L.SHDM,L.MDID,L.CXID,L.YHQID,L.SHSPID,L.BMDM,0,sum(FQJE),sum(ZXFJE)
      from BFCRM10.TMP_YHQFF_SPFT_SP  L
      group by  L.SHDM,L.MDID,L.CXID,L.YHQID,L.SHSPID,L.BMDM;
end;
/

prompt
prompt Creating procedure TMP_HYK_PROC_SP_CXHZ
prompt =======================================
prompt
create or replace procedure bfcrm10.TMP_HYK_PROC_SP_CXHZ (
                              pRclDate in date
                            )
is
  vSHDM   varchar2(4);
  vMDID   integer;
  vCXID integer;
  vYHQID  integer;
  vSHSPID integer;
  vBMDM   varchar2(20);
  vYQJE   number(14,2);

  cursor Cur_YHQ is
     select X.SHDM,X.MDID,nvl(Y.CXID,-1) CXID,Y.YHQID,Y.SHSPID,Y.BMDM,sum(YQJE) YQJE
       from BFCRM10.HYXFJL X,BFCRM10.HYXFJL_SP_YQFT Y
       where X.XFJLID=Y.XFJLID
         and X.CRMJZRQ=pRclDate
       group by X.SHDM,X.MDID,nvl(Y.CXID,-1),Y.YHQID,Y.SHSPID,Y.BMDM;
begin

  --BFCRM.Calc_SP_FQJE_HTFQ(pRclDate);
  BFCRM10.TMP_Calc_SP_FQJE(pRclDate);

  open Cur_YHQ;
  fetch Cur_YHQ into vSHDM,vMDID,vCXID,vYHQID,vSHSPID,vBMDM,vYQJE;
  while (Cur_YHQ%FOUND)
  loop
    vCXID := nvl(vCXID,0);
    vBMDM := nvl(vBMDM,'');

    update BFCRM10.YHQ_CXHD_HZ
      set YQJE=YQJE + vYQJE
      where RQ= pRclDate
        and SHDM=vSHDM
        and MDID=vMDID
        and CXID=vCXID
        and YHQID=vYHQID
        and SHSPID=vSHSPID
        and BMDM=vBMDM;
    if SQL%NOTFOUND then
      insert into BFCRM10.YHQ_CXHD_HZ(RQ, SHDM,  MDID, CXID, YHQID, SHSPID,BMDM,FQJE,YQJE)
        values(pRclDate,vSHDM,vMDID,vCXID,vYHQID,vSHSPID,vBMDM,0,vYQJE);
    end if;

    fetch Cur_YHQ into vSHDM,vMDID,vCXID,vYHQID,vSHSPID,vBMDM,vYQJE;
  end loop;
  close Cur_YHQ;

end;
/

prompt
prompt Creating procedure WRITE_HYK_GZXFJFR
prompt ====================================
prompt
create or replace procedure bfcrm10.Write_HYK_GZXFJFR (
                              pRCLRQ in date
                            )
is
begin
  update BFCRM10.HYK_XFJL
    set CRMJZRQ=pRCLRQ
    where STATUS=1
      and CRMJZRQ is null;

  insert into BFCRM10.GZXFJFR_CRM(JZRQ,FDBH,DEPTID,WLDW,HTH,VIPTYPE,LX,JE,JF)
    select pRCLRQ,H.MDID,S.BMDM,null as WLDW,nvl(S.HTH,-1),Y.HYKTYPE,0 as LX,sum(S.XSJE) as XSJE,sum(nvl(S.JF,0)) as JF
      from BFCRM10.HYK_XFJL H,
           BFCRM10.HYK_XFJL_SP S,
           BFCRM10.HYK_HYXX Y
      where H.XFJLID =S.XFJLID
        and H.HYID=Y.HYID
        and H.CRMJZRQ = pRCLRQ
        and nvl(H.HYID,0)>0
        and H.STATUS=1
      group by H.MDID,S.BMDM,S.HTH,Y.HYKTYPE;

end;
/

prompt
prompt Creating procedure WRITE_HYK_XFMX
prompt =================================
prompt
create or replace procedure bfcrm10.Write_HYK_XFMX (
                              pRCLRQ in date
                            )
is
  vYEARMONTH  integer;
  vMDID       integer;
  vSKTNO      varchar2(5);
  vJLBH       integer;
  vSHSPID     integer;
  vHYID       integer;
  vHYKTYPE    integer;
  vJYSJ       date;
  vXSBM       varchar2(10);
  vSHHTID     integer;
  vSHSPFLID   integer;
  vSHSBID     integer;
  vJF         float;

  cursor Cur_YHQ is
    select J.MDID,J.SKTNO,J.XSJYBH,I.SHSPID,J.HYID,Y.HYKTYPE,J.ZXRQ,I.XSBM,X.SHHTID,
            X.SHSPFLID,X.SHSBID,SUM(nvl(I.TZJF,0))
      from BFCRM10.HYK_JFTZJL J,BFCRM10.HYK_JFTZJLITEM I,BFCRM10.SHSPXX X ,BFCRM10.HYK_HYXX Y
      where J.JLBH=I.JLBH
        and J.HYID=Y.HYID
        and X.SHSPID=I.SHSPID
        and J.ZXRQ = pRCLRQ
      group by J.MDID,J.SKTNO,J.XSJYBH,I.SHSPID,J.HYID,Y.HYKTYPE,J.ZXRQ,I.XSBM,X.SHHTID,X.SHSPFLID,X.SHSBID;
begin
  vYEARMONTH := to_char(pRCLRQ,'yyyy')*100+to_char(pRCLRQ,'mm');

  update BFCRM10.HYK_XFJL
     set CRMJZRQ=pRCLRQ
    where STATUS=1
      and CRMJZRQ is null;

  insert into BFCRM10.HYK_XFMX(RQ,MDID,SKTNO,JLBH,SHSPID,HYID,HYKTYPE,JYSJ,DEPTID,SHHTID,SHSPFLID,SHSBID,
          XSSL,XSJE,ZKJE,ZKJE_HY,JF,YEARMONTH)
    select  pRCLRQ,H.MDID,H.SKTNO,H.JLBH,S.SHSPID,H.HYID,H.VIPTYPE HYKTYPE,H.XFSJ,S.BMDM,X.SHHTID,
            X.SHSPFLID,X.SHSBID,SUM(S.XSSL),SUM(S.XSJE),SUM(S.ZKJE),SUM(S.ZKJE_HY),SUM(S.JF),vYEARMONTH
      from BFCRM10.HYK_XFJL H,BFCRM10.HYK_XFJL_SP S ,BFCRM10.SHSPXX X ,BFCRM10.HYK_HYXX Y
      where H.XFJLID =S.XFJLID
        and H.HYID=Y.HYID
        and X.SHSPID=S.SHSPID
        and H.CRMJZRQ=pRCLRQ
        and H.HYID>0
        and H.STATUS=1
        and H.HYID is not null
      group by H.MDID,H.SKTNO,H.JLBH,S.SHSPID,H.HYID,H.VIPTYPE,H.XFSJ,S.BMDM,X.SHHTID,X.SHSPFLID,X.SHSBID;

  insert into BFCRM10.HYK_XFMX(RQ,MDID,SKTNO,JLBH,SHSPID,HYID,HYKTYPE,JYSJ,DEPTID,SHHTID,SHSPFLID,SHSBID,
        XSSL,XSJE,ZKJE,ZKJE_HY,JF,YEARMONTH)
    select pRCLRQ,B.MDID,'000',J.JLBH,-1,M.HYID,X.HYKTYPE,ZXRQ,' ',NULL,1,-1,0,0,0,0,SUM(M.TZJF),vYEARMONTH
      from BFCRM10.HYK_JFBDD J,BFCRM10.HYK_JFBDDITEM M,BFCRM10.HYK_BGDD B,BFCRM10.HYK_HYXX X
      where J.CZDD=B.BGDDDM
        and M.HYID=X.HYID
        and J.JLBH=M.JLBH
        and J.ZXRQ=pRCLRQ
      group by B.MDID,J.JLBH,M.HYID,X.HYKTYPE,ZXRQ;        --LWB 2013.3.7

  open Cur_YHQ;
  fetch Cur_YHQ into vMDID,vSKTNO,vJLBH,vSHSPID,vHYID,vHYKTYPE,vJYSJ,vXSBM,vSHHTID,vSHSPFLID,vSHSBID,vJF;
  while (Cur_YHQ%FOUND)
  loop
    update BFCRM10.HYK_XFMX
       set JF=JF+vJF
      where RQ=pRCLRQ
        and MDID=vMDID
        and SKTNO=vSKTNO
        and JLBH=vJLBH
        and SHSPID=vSHSPID;

    if SQL%NOTFOUND then
      insert into BFCRM10.HYK_XFMX(RQ,MDID,SKTNO,JLBH,SHSPID,HYID,HYKTYPE,JYSJ,DEPTID,SHHTID,SHSPFLID,SHSBID,
              XSSL,XSJE,ZKJE,ZKJE_HY,JF,YEARMONTH)
        values(pRCLRQ,vMDID,vSKTNO,vJLBH,vSHSPID,vHYID,vHYKTYPE,vJYSJ,vXSBM,vSHHTID,vSHSPFLID,vSHSBID,
              0,0,0,0,vJF,vYEARMONTH);
    end if;
    fetch Cur_YHQ into vMDID,vSKTNO,vJLBH,vSHSPID,vHYID,vHYKTYPE,vJYSJ,vXSBM,vSHHTID,vSHSPFLID,vSHSBID,vJF;
  end loop;
  close Cur_YHQ;
end;
/

prompt
prompt Creating procedure WRITE_HYK_XFMX_TMP
prompt =====================================
prompt
create or replace procedure bfcrm10.Write_HYK_XFMX_TMP (
                              pRclDate in date
                            )
is
  vYEARMONTH  integer;
  vMDID       integer;
  vSKTNO      varchar2(5);
  vJLBH       integer;
  vSHSPID     integer;
  vHYID       integer;
  vHYKTYPE    integer;
  vJYSJ       date;
  vXSBM       varchar2(10);
  vSHHTID     integer;
  vSHSPFLID   integer;
  vSHSBID     integer;
  vJF         float;

  cursor Cur_YHQ is
    select J.MDID,J.SKTNO,J.XSJYBH,I.SHSPID,J.HYID,Y.HYKTYPE,J.ZXRQ,I.XSBM,X.SHHTID,
            X.SHSPFLID,X.SHSBID,SUM(nvl(I.TZJF,0))
      from BFCRM10.HYK_JFTZJL J,BFCRM10.HYK_JFTZJLITEM I,BFCRM10.SHSPXX X ,BFCRM10.HYK_HYXX Y
      where J.JLBH=I.JLBH
        and J.HYID=Y.HYID
        and X.SHSPID=I.SHSPID
        and J.ZXRQ = pRclDate
      group by J.MDID,J.SKTNO,J.XSJYBH,I.SHSPID,J.HYID,Y.HYKTYPE,J.ZXRQ,I.XSBM,X.SHHTID,X.SHSPFLID,X.SHSBID;
begin
  vYEARMONTH := to_char(pRclDate,'yyyy')*100+to_char(pRclDate,'mm');

  insert into BFCRM10.HYK_XFMX(RQ,MDID,SKTNO,JLBH,SHSPID,HYID,HYKTYPE,JYSJ,DEPTID,SHHTID,SHSPFLID,SHSBID,
          XSSL,XSJE,ZKJE,ZKJE_HY,JF,YEARMONTH)
    select  pRclDate,H.MDID,H.SKTNO,H.JLBH,S.SHSPID,H.HYID,Y.HYKTYPE,H.XFSJ,S.BMDM,X.SHHTID,
            X.SHSPFLID,X.SHSBID,SUM(S.XSSL),SUM(S.XSJE),SUM(S.ZKJE),SUM(S.ZKJE_HY),SUM(S.JF),vYEARMONTH
      from BFCRM10.HYXFJL H,BFCRM10.HYXFJL_SP S ,BFCRM10.SHSPXX X ,BFCRM10.HYK_HYXX Y
      where H.XFJLID =S.XFJLID
        and H.HYID=Y.HYID
        and X.SHSPID=S.SHSPID
        and H.CRMJZRQ=pRclDate
        and H.HYID>0
        and H.HYID is not null
      group by H.MDID,H.SKTNO,H.JLBH,S.SHSPID,H.HYID,Y.HYKTYPE,H.XFSJ,S.BMDM,X.SHHTID,X.SHSPFLID,X.SHSBID;

  insert into BFCRM10.HYK_XFMX(RQ,MDID,SKTNO,JLBH,SHSPID,HYID,HYKTYPE,JYSJ,DEPTID,SHHTID,SHSPFLID,SHSBID,
        XSSL,XSJE,ZKJE,ZKJE_HY,JF,YEARMONTH)
    select pRclDate,B.MDID,'000',J.JLBH,-1,M.HYID,X.HYKTYPE,ZXRQ,' ',NULL,1,-1,0,0,0,0,SUM(M.TZJF),vYEARMONTH
      from BFCRM10.HYK_JFBDD J,BFCRM10.HYK_JFBDDITEM M,BFCRM10.HYK_BGDD B,BFCRM10.HYK_HYXX X
      where J.CZDD=B.BGDDDM
        and M.HYID=X.HYID
        and J.JLBH=M.JLBH
        and J.ZXRQ=pRclDate
      group by B.MDID,J.JLBH,M.HYID,X.HYKTYPE,ZXRQ;        --LWB 2013.3.7

  open Cur_YHQ;
  fetch Cur_YHQ into vMDID,vSKTNO,vJLBH,vSHSPID,vHYID,vHYKTYPE,vJYSJ,vXSBM,vSHHTID,vSHSPFLID,vSHSBID,vJF;
  while (Cur_YHQ%FOUND)
  loop
    update BFCRM10.HYK_XFMX
       set JF=JF+vJF
      where RQ=pRclDate
        and MDID=vMDID
        and SKTNO=vSKTNO
        and JLBH=vJLBH
        and SHSPID=vSHSPID;

    if SQL%NOTFOUND then
      insert into BFCRM10.HYK_XFMX(RQ,MDID,SKTNO,JLBH,SHSPID,HYID,HYKTYPE,JYSJ,DEPTID,SHHTID,SHSPFLID,SHSBID,
              XSSL,XSJE,ZKJE,ZKJE_HY,JF,YEARMONTH)
        values(pRclDate,vMDID,vSKTNO,vJLBH,vSHSPID,vHYID,vHYKTYPE,vJYSJ,vXSBM,vSHHTID,vSHSPFLID,vSHSBID,
              0,0,0,0,vJF,vYEARMONTH);
    end if;
    fetch Cur_YHQ into vMDID,vSKTNO,vJLBH,vSHSPID,vHYID,vHYKTYPE,vJYSJ,vXSBM,vSHHTID,vSHSPFLID,vSHSBID,vJF;
  end loop;
  close Cur_YHQ;
end;
/


spool off
