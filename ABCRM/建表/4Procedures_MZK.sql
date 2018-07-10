prompt
prompt Creating procedure HYK_PROC_MZK_CZK_RBB
prompt =======================================
prompt
create or replace procedure bfcrm10.HYK_PROC_MZK_CZK_RBB (pProcDate in date) /* 日期 */
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
      from  BFCRM10.HYKDEF D,BFCRM10.MZK_JEZH J,BFCRM10.MZKXX X
      where D.HYKTYPE=X.HYKTYPE
        and J.HYID=X.HYID
        and D.BJ_CZZH=1
      group by D.HYKTYPE,X.MDID;
begin
  vNextDD := pProcDate+1;
  vPrevDD := pProcDate-1;

 update BFCRM10.MZK_JEZCLJL
   set CRMJZRQ=pProcDate
   where CRMJZRQ is null;
   COMMIT ;

  open curLX;
  fetch curLX into vHYKTYPE,vMDID;
  while (curLX%FOUND)
  LOOP
    /*上期余额*/
    vSQYE := 0;
    select avg(QMYE) into vSQYE
      from BFCRM10.MZK_RBB
      where RQ=vPrevDD
        and HYKTYPE=vHYKTYPE
        and MDID=vMDID;
    vSQYE := nvl(vSQYE,0);

    /*售卡金额*/
    vJKJE := 0;
    select sum(I.QCYE) into vJKJE
      from BFCRM10.MZK_SKJL L,BFCRM10.MZK_SKJLITEM I,BFCRM10.HYKDEF H,BFCRM10.HYK_BGDD D
      where L.JLBH=I.JLBH
        and ZXRQ >= pProcDate
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
      from BFCRM10.MZK_SKJL L,BFCRM10.MZK_SKJLITEM I,BFCRM10.HYKDEF H,BFCRM10.HYK_BGDD D
      where L.JLBH=I.JLBH
        and L.QDSJ >=pProcDate
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
      from BFCRM10.MZK_CKJL A,BFCRM10.MZKXX B,BFCRM10.HYK_BGDD D
      where A.HYID=B.HYID
        and A.CZDD=D.BGDDDM
        and A.ZXRQ >= pProcDate
        and A.ZXRQ <  vNextDD
        and B.HYKTYPE=vHYKTYPE
        and B.MDID=vMDID
        --and nvl(A.ZXR,0)>0;
        and A.ZXR is not null;
    vCKJE := nvl(vCKJE,0);


    vTmpJE := 0;
    select sum(I.CKJE) into vTmpJE
      from BFCRM10.MZK_PLCK A,BFCRM10.MZK_PLCKITEM I,BFCRM10.MZKXX B,BFCRM10.HYK_BGDD D
      where A.JLBH=I.CZJPJ_JLBH
        and B.HYID=I.HYID
        and A.CZDD=D.BGDDDM
        and A.ZXRQ >= pProcDate
        and A.ZXRQ <  vNextDD
        and B.HYKTYPE=vHYKTYPE
        and B.MDID=vMDID
        --and nvl(A.ZXR,0)>0;
        and A.ZXR is not null;

    vCKJE := vCKJE+nvl(vTmpJE,0);

     /*取款金额*/
    vQKJE := 0;
    select sum(A.QKJE) into vQKJE
      from BFCRM10.MZK_QKJL A,BFCRM10.MZKXX B,BFCRM10.HYK_BGDD D
      where A.HYID=B.HYID
        and A.CZDD=D.BGDDDM
        and A.ZXRQ >= pProcDate
        and A.ZXRQ <  vNextDD
        and B.HYKTYPE=vHYKTYPE
        and B.MDID=vMDID
        --and nvl(A.ZXR,0)>0
        and A.ZXR is not null;
    vQKJE := nvl(vQKJE,0);

    vTmpJE := 0;
    select sum(I.QKJE) into vTmpJE
      from BFCRM10.MZK_PLQK A,BFCRM10.MZK_PLQKITEM I,BFCRM10.MZKXX B,BFCRM10.HYK_BGDD D
      where A.JLBH=I.JLBH
        and B.HYID=I.HYID
        and A.CZDD=D.BGDDDM
        and A.ZXRQ >= pProcDate
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
      from BFCRM10.MZK_JEZ_ZC A,BFCRM10.MZKXX B,BFCRM10.HYK_BGDD D
      where A.HYID_ZR=B.HYID
        and A.CZDD=D.BGDDDM
        and A.ZXRQ >= pProcDate
        and A.ZXRQ <  vNextDD
        and B.HYKTYPE=vHYKTYPE
        and B.MDID=vMDID
        --and nvl(A.ZXR,0)>0
        and A.ZXR is not null;
    vBKJE := vBKJE+nvl(vTmpJE,0);


    vTmpJE := 0;
    select sum(A.ZCJE) into vTmpJE
      from BFCRM10.MZK_JEZ_ZCITEM A,BFCRM10.MZKXX B,BFCRM10.MZK_JEZ_ZC C,BFCRM10.HYK_BGDD D
      where A.CZJPJ_JLBH=C.CZJPJ_JLBH
        and A.HYID_ZC=B.HYID
        and C.CZDD=D.BGDDDM
        and C.ZXRQ >= pProcDate
        and C.ZXRQ <  vNextDD
        and B.HYKTYPE=vHYKTYPE
        and B.MDID=vMDID
        and C.ZXR is not null;
        --and nvl(C.ZXR,0)>0
    vBKJE := vBKJE - nvl(vTmpJE,0);


    /*消费金额*/
    vXFJE := 0;
    select sum(A.DFJE) into vXFJE
      from BFCRM10.MZK_JEZCLJL A,BFCRM10.MZKXX B
      where B.HYID=A.HYID
        and CRMJZRQ =pProcDate
        and B.HYKTYPE=vHYKTYPE
        and B.MDID=vMDID
        and A.CLLX=7;
    vXFJE  := nvl(vXFJE,0);

    /*退卡金额*/
    vTKJE := 0;
    select sum(A.DFJE) into vTKJE
      from BFCRM10.MZK_JEZCLJL A,BFCRM10.MZKXX B
      where B.HYID=A.HYID
        and CRMJZRQ =pProcDate
        and B.HYKTYPE=vHYKTYPE
        and B.MDID=vMDID
        and A.CLLX=6;
    vTKJE  := nvl(vTKJE,0);

    /*调整金额*/
    vTZJE := 0;
    select sum(A.DFJE) into vTZJE
      from BFCRM10.MZK_JEZCLJL A,BFCRM10.MZKXX B
      where A.HYID=B.HYID
        and CRMJZRQ =pProcDate
        and B.HYKTYPE=vHYKTYPE
        and B.MDID=vMDID
        and (A.CLLX=11  or  A.CLLX=12 );
    vTZJE  := nvl(vTZJE,0);

    vTmpJE := 0;
    select sum(B.YE) into vTmpJE
      from BFCRM10.HYK_GHKLX A,BFCRM10.MZK_JEZH B,BFCRM10.MZKXX X
      where A.HYID=B.HYID
        and A.HYID=X.HYID
        and A.DJSJ >=pProcDate
        and A.DJSJ <vNextDD
        and A.HYKTYPE_OLD=vHYKTYPE
        and X.MDID=vMDID;
    vTZJE := vTZJE-nvl(vTmpJE,0);

    vTmpJE := 0;
    select sum(B.YE) into vTmpJE
      from BFCRM10.HYK_GHKLX A,BFCRM10.MZK_JEZH B,BFCRM10.MZKXX X
      where A.HYID=B.HYID
        and A.HYID=X.HYID
        and A.DJSJ >=pProcDate
        and A.DJSJ <vNextDD
        and A.HYKTYPE_NEW=vHYKTYPE
        and X.MDID=vMDID;
    vTZJE := vTZJE+nvl(vTmpJE,0);

    vTmpJE := 0;
    select sum(B.YE) into vTmpJE
      from BFCRM10.HYK_SJJL A,BFCRM10.MZK_JEZH B,BFCRM10.MZKXX X
      where A.HYID=B.HYID
        and A.HYID=X.HYID
        and A.ZXRQ >=pProcDate
        and A.ZXRQ <vNextDD
        and A.HYKTYPE_OLD=vHYKTYPE
        and X.MDID=vMDID;
    vTZJE := vTZJE-nvl(vTmpJE,0);

    vTmpJE := 0;
    select sum(B.YE) into vTmpJE
     from BFCRM10.HYK_SJJL A,BFCRM10.MZK_JEZH B,BFCRM10.MZKXX X
      where A.HYID=B.HYID
        and A.HYID=X.HYID
        and A.ZXRQ >=pProcDate
        and A.ZXRQ <vNextDD
        and A.HYKTYPE_NEW=vHYKTYPE
        and X.MDID=vMDID;
    vTZJE := vTZJE+nvl(vTmpJE,0);


    /*期末余额*/
    vQMYE := 0;
    select sum(A.YE) into vQMYE
      from BFCRM10.MZK_JEZH A,BFCRM10.MZKXX B ,BFCRM10.HYKDEF F
      where A.HYID=B.HYID and B.HYKTYPE=F.HYKTYPE
        and B.HYKTYPE=vHYKTYPE
        and B.MDID=vMDID;
    vQMYE := nvl(vQMYE,0);

    insert into BFCRM10.MZK_RBB(RQ,HYKTYPE,MDID,SQYE,JKJE,CKJE,QKJE,XFJE,TKJE,QMYE,TZJE,BKJE)
       values(pProcDate,vHYKTYPE,vMDID,vSQYE,vJKJE-vTSKJE,vCKJE,vQKJE,vXFJE,vTKJE,vQMYE,vTZJE,vBKJE);

    fetch curLX into vHYKTYPE,vMDID;
  end LOOP;
  close curLX;
end;
/

prompt
prompt Creating procedure HYK_PROC_MZK_KCCZKBGZ
prompt ========================================
prompt
create or replace procedure bfcrm10.HYK_PROC_MZK_KCCZKBGZ (
                               pRCLRQ  in date,
                               pNY in number
                            )
as
  vNextDD      date;
  vCount       integer;
begin
  select count(*) into vCount from BFCRM10.MZK_KCBGZ where RQ=pRCLRQ;
  if vCount>0 then
    return;
  end if;

  vNextDD := pRCLRQ+1;

  /*上期余额*/
  insert into BFCRM10.MZK_KCBGZ(RQ,BGDDDM,HYKTYPE,MZJE,QCSL,QCJE,JKSL,JKJE,BRSL,BRJE,BCSL,
        BCJE,FSSL,FSJE,FSTSSL,FSTSJE,XFTSSL,XFTSJE,ZFSL,ZFJE,JCSL,JCJE,TZSL,TZJE,YEARMONTH)
    select pRCLRQ,BGDDDM,HYKTYPE,MZJE,JCSL,JCJE,0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,pNY
      from BFCRM10.MZK_KCBGZ
      where RQ=pRCLRQ-1;

  /*建卡*/
  for item in  (
    select J.BGDDDM,J.HYKTYPE,J.QCYE,count(I.JLBH) SL,sum(nvl(I.JE,0)) JE
      from BFCRM10.MZK_JKJL J,BFCRM10.MZK_JKJLITEM I
      where J.JLBH=I.JLBH
        and J.ZXRQ>=pRCLRQ
        and J.ZXRQ <vNextDD
      group by J.BGDDDM,J.HYKTYPE,J.QCYE)
  LOOP
    update BFCRM10.MZK_KCBGZ
      set JKSL=JKSL+item.SL,
          JKJE=JKJE+item.JE
      where RQ=pRCLRQ
       and  BGDDDM = item.BGDDDM
       and  HYKTYPE = item.HYKTYPE
       and  MZJE = item.QCYE;

    if SQL%NOTFOUND then
      insert into BFCRM10.MZK_KCBGZ(RQ,BGDDDM,HYKTYPE,MZJE,JKSL,JKJE,YEARMONTH)
        values(pRCLRQ,item.BGDDDM,item.HYKTYPE,item.QCYE,item.SL,item.JE,pNY);
    end if;
  end LOOP;

  /*写卡*/
  for item in (
      select J.BGDDDM,J.HYKTYPE,J.QCYE,count(I.JLBH) SL,sum(nvl(I.JE,0)) JE
        from BFCRM10.MZK_JKJL  J,BFCRM10.MZK_JKJLITEM I
        where J.JLBH=I.JLBH
          and I.XKRQ>=pRCLRQ
          and I.XKRQ <vNextDD
        group by J.BGDDDM,J.HYKTYPE,J.QCYE)
  LOOP
    update BFCRM10.MZK_KCBGZ
      set XKSL=XKSL+item.SL,
          XKJE=XKJE+item.JE
      where RQ=pRCLRQ
        and BGDDDM = item.BGDDDM
        and HYKTYPE = item.HYKTYPE
        and MZJE = item.QCYE;
    if SQL%NOTFOUND then
      insert into BFCRM10.MZK_KCBGZ(RQ,BGDDDM,HYKTYPE,MZJE,XKSL,XKJE,YEARMONTH)
        values(pRCLRQ,item.BGDDDM,item.HYKTYPE,item.QCYE,item.SL,item.JE,pNY);
    end if;
  end LOOP;

  /*拨入*/
  for item in (
      select J.BGDDDM_BR as BGDDDM,I.HYKTYPE,I.JE QCYE,count(I.JLBH) SL,sum(I.JE) JE
        from BFCRM10.MZK_LQJL J,BFCRM10.MZK_LQJLITEM I
        where J.JLBH=I.JLBH
          and J.ZXRQ>=pRCLRQ
          and J.ZXRQ <vNextDD
        group by  J.BGDDDM_BR,I.HYKTYPE,I.JE)
  LOOP
    update BFCRM10.MZK_KCBGZ
      set BRSL=BRSL+item.SL,BRJE=BRJE+item.JE
      where RQ=pRCLRQ
        and  BGDDDM  = item.BGDDDM
        and  HYKTYPE = item.HYKTYPE
        and  MZJE    = item.QCYE;
    if SQL%NOTFOUND then
      insert into BFCRM10.MZK_KCBGZ(RQ,BGDDDM,HYKTYPE,MZJE,BRSL,BRJE,YEARMONTH)
        values(pRCLRQ,item.BGDDDM,item.HYKTYPE,item.QCYE,item.SL,item.JE,pNY);
    end if;
  end LOOP;

  /*拨出*/
  for item in (
      select J.BGDDDM_BC as BGDDDM,I.HYKTYPE,I.JE QCYE,count(I.JLBH) SL,sum(I.JE) JE
        from BFCRM10.MZK_LQJL J,BFCRM10.MZK_LQJLITEM I
        where J.JLBH=I.JLBH
          and J.ZXRQ>=pRCLRQ
          and J.ZXRQ <vNextDD
        group by  J.BGDDDM_BC,I.HYKTYPE,I.JE)
  LOOP
    update BFCRM10.MZK_KCBGZ
      set BCSL=BCSL+item.SL,
          BCJE=BCJE+item.JE
      where RQ=pRCLRQ
        and  BGDDDM = item.BGDDDM
        and  HYKTYPE = item.HYKTYPE
        and  MZJE = item.QCYE;

    if SQL%NOTFOUND then
      insert into BFCRM10.MZK_KCBGZ(RQ,BGDDDM,HYKTYPE,MZJE,BCSL,BCJE,YEARMONTH)
        values(pRCLRQ,item.BGDDDM,item.HYKTYPE,item.QCYE,item.SL,item.JE,pNY);
    end if;
  end LOOP;

    /*(换卡)*/
  for item in (
      select J.BGDDDM,J.HYKTYPE,nvl(J.QCYE,0) QCYE,count(J.CZJPJ_JLBH ) SL,sum(nvl(J.QCYE,0)) JE
        from BFCRM10.MZK_HK J
        where J.DJSJ >=pRCLRQ
          and J.DJSJ  <vNextDD
        group by  J.BGDDDM,J.HYKTYPE,J.QCYE)
  LOOP
    update BFCRM10.MZK_KCBGZ
      set HKSL=HKSL+item.SL,
          HKJE=HKJE+item.JE
      where RQ=pRCLRQ
        and BGDDDM = item.BGDDDM
        and HYKTYPE = item.HYKTYPE
        and MZJE = item.QCYE;
    if SQL%NOTFOUND then
      insert into BFCRM10.MZK_KCBGZ(RQ,BGDDDM,HYKTYPE,MZJE,HKSL,HKJE,YEARMONTH)
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
    update BFCRM10.MZK_KCBGZ
      set HKSL=HKSL+item.SL,HKJE=HKJE+item.JE
      where RQ=pRCLRQ
        and  BGDDDM = item.BGDDDM
        and  HYKTYPE = item.HYKTYPE
        and  MZJE = item.QCYE;
    if SQL%NOTFOUND then
     insert into BFCRM10.MZK_KCBGZ(RQ,BGDDDM,HYKTYPE,MZJE,HKSL,HKJE,YEARMONTH)
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
    update BFCRM10.MZK_KCBGZ
      set HKSL=HKSL+item.SL
      where RQ=pRCLRQ
        and  BGDDDM = item.BGDDDM
        and  HYKTYPE = item.HYKTYPE
        and  MZJE = item.QCYE;
    if SQL%NOTFOUND then
     insert into BFCRM10.MZK_KCBGZ(RQ,BGDDDM,HYKTYPE,MZJE,HKSL,HKJE,YEARMONTH)
      values(pRCLRQ,item.BGDDDM,item.HYKTYPE,0,item.SL,0,pNY);
    end if;
  end loop;



   /*售卡*/
  for item in (
    select A.BGDDDM,nvl(B.HYKTYPE,0) HYKTYPE,nvl(B.QCYE,0) QCYE,sum(1) SL, sum(B.QCYE) JE
      from BFCRM10.MZK_SKJL A,BFCRM10.MZK_SKJLITEM B
      where A.JLBH=B.JLBH
        and ZXRQ >=pRCLRQ
        and ZXRQ <vNextDD
        and STATUS>=1
        and A.FS in (0,1)
      group by A.BGDDDM,B.HYKTYPE,nvl(B.QCYE,0))
  loop
    update BFCRM10.MZK_KCBGZ
      set FSSL=FSSL+item.SL,
          FSJE=FSJE+item.JE
      where RQ=pRCLRQ
        and  HYKTYPE = item.HYKTYPE
        and  BGDDDM = item.BGDDDM
        and  MZJE=item.QCYE;

    if SQL%NOTFOUND then
     insert into BFCRM10.MZK_KCBGZ(RQ,HYKTYPE,BGDDDM,MZJE,FSSL,FSJE,YEARMONTH)
      values(pRCLRQ,item.HYKTYPE,item.BGDDDM,item.QCYE,item.SL,item.JE,pNY);
    end if;
  end loop;

  /*退售卡*/
  for item in (
    select B.HYKTYPE,A.BGDDDM,nvl(B.QCYE,0) QCYE,sum(1) SL,sum(B.QCYE) JE
    from BFCRM10.MZK_SKJL A,BFCRM10.MZK_SKJLITEM B
    where A.JLBH=B.JLBH
      and QDSJ>= pRCLRQ
      and QDSJ<vNextDD
      and STATUS>1
      and A.FS=2
    group by B.HYKTYPE,A.BGDDDM,nvl(B.QCYE,0))
  loop
     update BFCRM10.MZK_KCBGZ
     set FSTSSL=FSTSSL+item.SL,
         FSTSJE=FSTSJE+item.JE
     where RQ=pRCLRQ
      and  HYKTYPE = item.HYKTYPE
      and  BGDDDM = item.BGDDDM
      and  MZJE=item.QCYE;

    if SQL%NOTFOUND then
      insert into BFCRM10.MZK_KCBGZ(RQ,HYKTYPE,BGDDDM,MZJE,YEARMONTH,FSTSSL,FSTSJE)
      values(pRCLRQ,item.HYKTYPE,item.BGDDDM,item.QCYE,pNY,item.SL,item.JE);
    end if;

  end loop;

  /*回收卡*/
  for item in (
    select A.HYKTYPE,X.YBGDD BGDDDM,0 QCYE,sum(1) SL,0 JE
    from BFCRM10.HYK_TK A,BFCRM10.HYK_TK_ITEM B,BFCRM10.MZKXX X
    where A.JLBH=B.JLBH
      and B.HYID=X.HYID
      and ZXRQ>=pRCLRQ
      and ZXRQ< vNextDD
      and A.TKFS=2
    group by A.HYKTYPE,X.YBGDD)
  loop
     update BFCRM10.MZK_KCBGZ
     set XFTSSL=XFTSSL+item.SL,
         XFTSJE=XFTSJE+item.JE
     where RQ=pRCLRQ
      and  HYKTYPE = item.HYKTYPE
      and  BGDDDM = item.BGDDDM
      and  MZJE=item.QCYE;

    if SQL%NOTFOUND then
      insert into BFCRM10.MZK_KCBGZ(RQ,HYKTYPE,BGDDDM,MZJE,YEARMONTH,XFTSSL,XFTSJE)
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
     update BFCRM10.MZK_KCBGZ
     set ZFSL=ZFSL+item.SL,
         ZFJE=ZFJE+item.JE
     where RQ=pRCLRQ
      and  HYKTYPE = item.HYKTYPE
      and  BGDDDM = item.BGDDDM
      and  MZJE=item.QCYE;

    if SQL%NOTFOUND then
      insert into BFCRM10.MZK_KCBGZ(RQ,HYKTYPE,BGDDDM,MZJE,YEARMONTH,ZFSL,ZFJE)
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
    update BFCRM10.MZK_KCBGZ
     set ZFSL=ZFSL+item.SL,
         ZFJE=ZFJE+item.JE
     where RQ=pRCLRQ
      and  HYKTYPE = item.HYKTYPE
      and  BGDDDM = item.BGDDDM
      and  MZJE=item.QCYE;

    if SQL%NOTFOUND then
      insert into BFCRM10.MZK_KCBGZ(RQ,HYKTYPE,BGDDDM,MZJE,YEARMONTH,ZFSL,ZFJE)
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
     update BFCRM10.MZK_KCBGZ
     set TZSL=TZSL+item.SL*(-1),TZJE=TZJE+item.JE*(-1)
     where RQ=pRCLRQ
      and  BGDDDM = item.BGDDDM
      and  HYKTYPE = item.HYKTYPE
      and  MZJE = item.QCYE;

    if SQL%NOTFOUND then
     insert into BFCRM10.MZK_KCBGZ(RQ,BGDDDM,HYKTYPE,MZJE,TZSL,TZJE,YEARMONTH)
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
     update BFCRM10.MZK_KCBGZ
     set TZSL=TZSL+item.SL,TZJE=TZJE+item.JE
     where RQ=pRCLRQ
      and  BGDDDM = item.BGDDDM
      and  HYKTYPE = item.HYKTYPE
      and  MZJE = item.QCYE;

    if SQL%NOTFOUND then
     insert into BFCRM10.MZK_KCBGZ(RQ,BGDDDM,HYKTYPE,MZJE,TZSL,TZJE,YEARMONTH)
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
     update BFCRM10.MZK_KCBGZ
     set JCSL=JCSL+item.SL,
         JCJE=JCJE+item.JE
     where RQ=pRCLRQ
      and  HYKTYPE = item.HYKTYPE
      and  BGDDDM = item.BGDDDM
      and  MZJE=item.QCYE;

    if SQL%NOTFOUND then
      insert into BFCRM10.MZK_KCBGZ(RQ,HYKTYPE,BGDDDM,MZJE,YEARMONTH,JCSL,JCJE)
      values(pRCLRQ,item.HYKTYPE,item.BGDDDM,item.QCYE,pNY,item.SL,item.JE);
    end if;
  end loop;
end;
/

prompt
prompt Creating procedure HYK_PROC_MZK_XFRBB
prompt =====================================
prompt
create or replace procedure bfcrm10.HYK_PROC_MZK_XFRBB (
                              pProcDate in date
                            )
as
  vYEARMONTH integer;
  vNextDay   date;
begin

  vYEARMONTH := to_char(pProcDate,'yyyy')*100+to_char(pProcDate,'mm');
  vNextDay := pProcDate+1;

  insert into BFCRM10.MZK_XFRBB(RQ,HYKTYPE,MDID,CZMD,SKTNO,XFJE,TKJE,TZJE,YEARMONTH)
    select pProcDate,H.HYKTYPE,
           H.MDID ,
           L.MDID CZMD,
           L.SKTNO,
           SUM((1-abs(sign(CLLX-7))) * (nvl(DFJE,0) - nvl(JFJE,0))) XFJE ,
           SUM((1-abs(sign(CLLX-6))) * (nvl(DFJE,0) - nvl(JFJE,0))) TKJE ,
           SUM((1-abs(sign(CLLX-11))) * (nvl(DFJE,0) - nvl(JFJE,0))) TZJE ,
           vYEARMONTH
      from BFCRM10.MZK_JEZCLJL L,BFCRM10.MZKXX H
      where L.HYID=H.HYID
        and CLLX in(6,7,11)
        and CRMJZRQ=pProcDate
      group by H.HYKTYPE ,H.MDID,L.MDID,L.SKTNO;
end;
/