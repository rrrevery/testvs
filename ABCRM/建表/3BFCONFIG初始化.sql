insert into BFPUB8.BFCONFIG (JLBH, SYSID, LIBID, YXBDBJ, DATATYPE, LX, DEF_VAL, CUR_VAL, MAX_VAL, MIN_VAL, YY)
values (0, 510, 510000, 1, 1, '当前数据库ID', '0', '0', '99', '0', '');

insert into BFPUB8.BFCONFIG (JLBH, SYSID, LIBID, YXBDBJ, DATATYPE, LX, DEF_VAL, CUR_VAL, MAX_VAL, MIN_VAL, YY)
values (1, 510, 0, 1, 1, '日处理最早时间', '2100', '2100', '2359', '0', '');

insert into BFPUB8.BFCONFIG (JLBH, SYSID, LIBID, YXBDBJ, DATATYPE, LX, DEF_VAL, CUR_VAL, MAX_VAL, MIN_VAL, YY)
values (11, 0, 30, 1, 1, '人员编码方式', '1', '1', '1', '0', '0:流水码，1:自定义码');

insert into BFPUB8.BFCONFIG (JLBH, SYSID, LIBID, YXBDBJ, DATATYPE, LX, DEF_VAL, CUR_VAL, MAX_VAL, MIN_VAL, YY)
values (12, 0, 30, 1, 1, '人员代码长度', '5', '6', '8', '1', '设定人员代码的长度');

insert into BFPUB8.BFCONFIG (JLBH, SYSID, LIBID, YXBDBJ, DATATYPE, LX, DEF_VAL, CUR_VAL, MAX_VAL, MIN_VAL, YY)
values (13, 0, 30, 1, 1, '人员代码前导码长度', '0', '0', '2', '0', '人员代码可以在前导码范围内流水');

insert into BFPUB8.BFCONFIG (JLBH, SYSID, LIBID, YXBDBJ, DATATYPE, LX, DEF_VAL, CUR_VAL, MAX_VAL, MIN_VAL, YY)
values (510000100, 510, 510000, 1, 1, '会员地区代码编码规则', '22222', '22222', '1111111111', '1', '每一位表示一级代码的长度,总位数是代码的总级数');

insert into BFPUB8.BFCONFIG (JLBH, SYSID, LIBID, YXBDBJ, DATATYPE, LX, DEF_VAL, CUR_VAL, MAX_VAL, MIN_VAL, YY)
values (510000101, 510, 510000, 1, 1, '支付方式代码编码规则', '22222', '22222', '1111111111', '1', '每一位表示一级代码的长度,总位数是代码的总级数');

insert into BFPUB8.BFCONFIG (JLBH, SYSID, LIBID, YXBDBJ, DATATYPE, LX, DEF_VAL, CUR_VAL, MAX_VAL, MIN_VAL, YY)
values (510000102, 510, 510000, 1, 1, '商品分类代码编码规则', '22222', '22222', '1111111111', '1', '每一位表示一级代码的长度,总位数是代码的总级数');

insert into BFPUB8.BFCONFIG (JLBH, SYSID, LIBID, YXBDBJ, DATATYPE, LX, DEF_VAL, CUR_VAL, MAX_VAL, MIN_VAL, YY)
values (520000100, 510, 510000, 1, 1, '系统会员卡使用介质', '0', '0', '2', '0', '0:磁卡;1:IC卡；2:磁卡，IC卡并用');

insert into BFPUB8.BFCONFIG (JLBH, SYSID, LIBID, YXBDBJ, DATATYPE, LX, DEF_VAL, CUR_VAL, MAX_VAL, MIN_VAL, YY)
values (520000101, 510, 510000, 1, 1, '储值优惠券对应优惠券ID', '0', '0', '10', '0', '此优惠券ID应存在于优惠券定义中');

insert into BFPUB8.BFCONFIG (JLBH, SYSID, LIBID, YXBDBJ, DATATYPE, LX, DEF_VAL, CUR_VAL, MAX_VAL, MIN_VAL, YY)
values (520000102, 510, 510000, 1, 1, '会员卡积分不能小于零', '0', '1', '1', '0', '0:会员卡积分可小于零;1:会员卡积分不能小于零');

insert into BFPUB8.BFCONFIG (JLBH, SYSID, LIBID, YXBDBJ, DATATYPE, LX, DEF_VAL, CUR_VAL, MAX_VAL, MIN_VAL, YY)
values (520000103, 510, 510000, 1, 1, '单商户标记', '1', '0', '1', '0', '0:非单户;1:单商户(单商户的分类和商标可自动对照)');

insert into BFPUB8.BFCONFIG (JLBH, SYSID, LIBID, YXBDBJ, DATATYPE, LX, DEF_VAL, CUR_VAL, MAX_VAL, MIN_VAL, YY)
values (520000104, 510, 510000, 1, 1, '核算', '1', '0', '1', '0', '0:总部核算;1:发行门店核算');

insert into BFPUB8.BFCONFIG (JLBH, SYSID, LIBID, YXBDBJ, DATATYPE, LX, DEF_VAL, CUR_VAL, MAX_VAL, MIN_VAL, YY)
values (520000105, 510, 510000, 1, 1, '写磁方式', '0', '0', '2', '0', '0:只写二磁道;1:二磁道写磁道内容,三磁道写卡号;2:二磁道写卡号,三磁道写磁道内容');

insert into BFPUB8.BFCONFIG (JLBH, SYSID, LIBID, YXBDBJ, DATATYPE, LX, DEF_VAL, CUR_VAL, MAX_VAL, MIN_VAL, YY)
values (520000106, 510, 510000, 1, 1, '加密算法', '0', '0', '1', '0', '0:公司加密算法;1:银联加密算法 ');

insert into BFPUB8.BFCONFIG (JLBH, SYSID, LIBID, YXBDBJ, DATATYPE, LX, DEF_VAL, CUR_VAL, MAX_VAL, MIN_VAL, YY)
values (520000107, 510, 510000, 1, 1, '备份数据库最早时间', '2130', '2200', '2359', '0800', '备份数据库最早时间 ');

insert into BFPUB8.BFCONFIG (JLBH, SYSID, LIBID, YXBDBJ, DATATYPE, LX, DEF_VAL, CUR_VAL, MAX_VAL, MIN_VAL, YY)
values (520000108, 510, 510000, 1, 1, '会员卡制卡文件格式', '1', '1', '1', '0', '0:磁道内容,1:卡号+磁道内容');

insert into BFPUB8.BFCONFIG (JLBH, SYSID, LIBID, YXBDBJ, DATATYPE, LX, DEF_VAL, CUR_VAL, MAX_VAL, MIN_VAL, YY)
values (520000109, 510, 510000, 1, 1, '面值卡制卡文件格式', '1', '1', '2', '0', '0:磁道内容,1:卡号+磁道内容,2:卡号嵌面额+磁道内容');

insert into BFPUB8.BFCONFIG (JLBH, SYSID, LIBID, YXBDBJ, DATATYPE, LX, DEF_VAL, CUR_VAL, MAX_VAL, MIN_VAL, YY)
values (520000110, 510, 510000, 1, 1, '面值卡制卡文件格式面额开始位置', '1', '1', '30', '1', '面值卡制卡文件格式选卡号嵌面额+磁道内容时有效');

insert into BFPUB8.BFCONFIG (JLBH, SYSID, LIBID, YXBDBJ, DATATYPE, LX, DEF_VAL, CUR_VAL, MAX_VAL, MIN_VAL, YY)
values (520000111, 510, 510000, 1, 1, '面值卡制卡文件格式面额长度', '1', '1', '6', '1', '面值卡制卡文件格式选卡号嵌面额+磁道内容时有效');

insert into BFPUB8.BFCONFIG (JLBH, SYSID, LIBID, YXBDBJ, DATATYPE, LX, DEF_VAL, CUR_VAL, MAX_VAL, MIN_VAL, YY)
values (520000112, 510, 510000, 1, 1, '储值账户消费卡号验证码长度', '0', '3', '10', '0', '储值账户支付卡面号码验证码长度');

insert into BFPUB8.BFCONFIG (JLBH, SYSID, LIBID, YXBDBJ, DATATYPE, LX, DEF_VAL, CUR_VAL, MAX_VAL, MIN_VAL, YY)
values (520000113, 510, 510000, 1, 1, '优惠券账户消费卡号验证码长度', '0', '0', '10', '0', '优惠券账户支付卡面号码验证长度');

insert into BFPUB8.BFCONFIG (JLBH, SYSID, LIBID, YXBDBJ, DATATYPE, LX, DEF_VAL, CUR_VAL, MAX_VAL, MIN_VAL, YY)
values (520000114, 510, 510000, 1, 1, '面值卡有效期延长天数', '0', '720', '6000', '0', '面值卡有效期延长天数,为零时不控制，大于零每次只能延长此天数');

insert into BFPUB8.BFCONFIG (JLBH, SYSID, LIBID, YXBDBJ, DATATYPE, LX, DEF_VAL, CUR_VAL, MAX_VAL, MIN_VAL, YY)
values (520000115, 510, 510000, 1, 1, '会员卡本年累计积分不能小于零', '0', '0', '1', '0', '0:可小于零;1:不能小于零(该积分年终被清零，本参数适用于有年度总积分双重返利的客户)');

insert into BFPUB8.BFCONFIG (JLBH, SYSID, LIBID, YXBDBJ, DATATYPE, LX, DEF_VAL, CUR_VAL, MAX_VAL, MIN_VAL, YY)
values (520000120, 510, 510000, 1, 1, '存在用券的消费小票不再积分', '0', '0', '1', '0', '0:可积分;1:不积分');

insert into BFPUB8.BFCONFIG (JLBH, SYSID, LIBID, YXBDBJ, DATATYPE, LX, DEF_VAL, CUR_VAL, MAX_VAL, MIN_VAL, YY)
values (520000121, 510, 510000, 1, 1, '存在用券的消费小票不再发券', '0', '0', '1', '0', '0:可发券;1:不发券');

insert into BFPUB8.BFCONFIG (JLBH, SYSID, LIBID, YXBDBJ, DATATYPE, LX, DEF_VAL, CUR_VAL, MAX_VAL, MIN_VAL, YY)
values (520000123, 510, 510000, 1, 1, '储值卡交易保存商品明细', '0', '1', '1', '0', '0:不保存;1:保存');

insert into BFPUB8.BFCONFIG (JLBH, SYSID, LIBID, YXBDBJ, DATATYPE, LX, DEF_VAL, CUR_VAL, MAX_VAL, MIN_VAL, YY)
values (520000125, 510, 510000, 1, 1, '礼品进货单形式', '1', '1', '1', '0', '0可直接选择礼品进货;1只能选择礼品订单进货');

insert into BFPUB8.BFCONFIG (JLBH, SYSID, LIBID, YXBDBJ, DATATYPE, LX, DEF_VAL, CUR_VAL, MAX_VAL, MIN_VAL, YY)
values (520000126, 510, 510000, 1, 1, '售卡强制日处理审核张数', '0', '0', '99999999', '0', '0不强制;大于0的为强制审核的张数');

insert into BFPUB8.BFCONFIG (JLBH, SYSID, LIBID, YXBDBJ, DATATYPE, LX, DEF_VAL, CUR_VAL, MAX_VAL, MIN_VAL, YY)
values (520000127, 510, 510000, 1, 1, '售卡金额授权限制', '0', '0', '10000000', '0', '0:不需要授权;大于0:达到该金额时需要授权');

insert into BFPUB8.BFCONFIG (JLBH, SYSID, LIBID, YXBDBJ, DATATYPE, LX, DEF_VAL, CUR_VAL, MAX_VAL, MIN_VAL, YY)
values (520000128, 510, 510000, 1, 1, '磁道是否二次加密', '0', '1', '1', '0', '0:不需要二次加密;大于0:需要二次加密');

insert into BFPUB8.BFCONFIG (JLBH,SYSID,LIBID,YXBDBJ,DATATYPE,LX,DEF_VAL,CUR_VAL,MAX_VAL,MIN_VAL,YY)
values (520000129 ,510,510000,1,1,'是否CRM升级','0','0','1','0','0:新项目;1:升级项目,升级项目新款台消费只能在新款台退货'); 


