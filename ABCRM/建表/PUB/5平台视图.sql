--注意BFPUB和BFCRM在同一个库的话按本文件建视图，不是同一个库建物化视图
--需要BFCONFIG、RYXX、XTCZY、XTCZYGRP、XTXX、RCL、SYSLIB表的视图，平台用户是BFPUB8则按照下边语句即可，平台用户是BFPUB10则需要更新成10
/*==============================================================*/
/* View: BFCONFIG                                               */
/*==============================================================*/
grant all on BFPUB8.BFCONFIG to BFCRM10
/
create or replace view BFCRM10.BFCONFIG as
select * from BFPUB8.BFCONFIG where SYSID=510
/

/*==============================================================*/
/* View: RYXX                                                   */
/*==============================================================*/
grant select on BFPUB8.RYXX to BFCRM10
/
create or replace view BFCRM10.RYXX as
select * from BFPUB8.RYXX
/

/*==============================================================*/
/* View: XTCZY                                                  */
/*==============================================================*/
grant select on BFPUB8.XTCZY to BFCRM10
/
create or replace view BFCRM10.XTCZY as
select * from BFPUB8.XTCZY
/

/*==============================================================*/
/* View: XTCZYGRP                                               */
/*==============================================================*/
grant select on BFPUB8.XTCZYGRP to BFCRM10
/
create or replace view BFCRM10.XTCZYGRP as
select * from BFPUB8.XTCZYGRP
/

/*==============================================================*/
/* View: XTXX                                               */
/*==============================================================*/
grant select on BFPUB8.XTXX to BFCRM10
/
create or replace view BFCRM10.XTXX as
select * from BFPUB8.XTXX
/

/*==============================================================*/
/* View: RCL                                               */
/*==============================================================*/
grant select on BFPUB8.RCL to BFCRM10
/
create or replace view BFCRM10.RCL as
select * from BFPUB8.RCL
/

/*==============================================================*/
/* View: SYSLIB                                               */
/*==============================================================*/
grant select on BFPUB8.SYSLIB to BFCRM10
/
create or replace view BFCRM10.SYSLIB as
select * from BFPUB8.SYSLIB
/
