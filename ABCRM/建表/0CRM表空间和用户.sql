CREATE TABLESPACE "CRM" 
    LOGGING 
    DATAFILE 'd:\oradata\CRM.ORA' SIZE 300M REUSE EXTENT 
    MANAGEMENT LOCAL; --SEGMENT SPACE MANAGEMENT AUTO 

CREATE USER "BFCRM10"  PROFILE "DEFAULT" 
    IDENTIFIED BY "DHHZDHHZ" DEFAULT TABLESPACE "CRM" 
    QUOTA UNLIMITED 
    ON "CRM" 
    ACCOUNT UNLOCK; 
grant connect,resource to BFCRM10;
grant create database link to BFCRM10;
grant create materialized view to BFCRM10;
grant create view to BFCRM10;
grant unlimited tablespace to BFCRM10;

CREATE or replace DIRECTORY  DPUMP_DIR  AS 'd:\backup';