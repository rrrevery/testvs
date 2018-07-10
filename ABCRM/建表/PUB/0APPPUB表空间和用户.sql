CREATE TABLESPACE "PUB" 
    LOGGING 
    DATAFILE 'd:\oradata\PUB.ORA' SIZE 300M REUSE EXTENT 
    MANAGEMENT LOCAL; --SEGMENT SPACE MANAGEMENT AUTO 

CREATE USER "BFAPP10"  PROFILE "DEFAULT" 
    IDENTIFIED BY "DHHZDHHZ" DEFAULT TABLESPACE "PUB" 
    QUOTA UNLIMITED 
    ON "PUB" 
    ACCOUNT UNLOCK; 
grant connect,resource to BFAPP10;
grant create database link to BFAPP10;
grant create materialized view to BFAPP10;
grant create view to BFAPP10;
grant unlimited tablespace to BFAPP10;

CREATE USER "BFPUB10"  PROFILE "DEFAULT" 
    IDENTIFIED BY "DHHZDHHZ" DEFAULT TABLESPACE "PUB" 
    QUOTA UNLIMITED 
    ON "PUB" 
    ACCOUNT UNLOCK; 
grant connect,resource to BFPUB10;
grant create database link to BFPUB10;
grant create materialized view to BFPUB10;
grant create view to BFPUB10;
grant unlimited tablespace to BFPUB10;

CREATE or replace DIRECTORY  DPUMP_DIR  AS 'd:\backup';