using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using ChangYi.Pub;
using ChangYi.Crm.Rule;

namespace ChangYi.Crm.Server
{
    public class CrmPubUtils
    {
        public static void GetVipCard(DbCommand cmd, StringBuilder sql, CrmVipGroup vipGroup)
        {

        }

        public static void GetVipGroup(DbCommand cmd, StringBuilder sql, CrmVipGroup vipGroup)
        {
            if (vipGroup.GroupId == 0)
                return;
            sql.Length = 0;
            sql.Append("select GRPID,SRFS,GZFS from HYK_HYGRP where GRPID = ").Append(vipGroup.GroupId);
            cmd.CommandText = sql.ToString();
            DbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                vipGroup.BirthdayMode = DbUtils.GetInt(reader, 1);
                vipGroup.OnlyExistVipId = (DbUtils.GetInt(reader, 2) == 0);
            }
            reader.Close();

            sql.Length = 0;
            sql.Append("select SJLX,SJNR from HYK_HYGRP_GZMX where GRPID = ").Append(vipGroup.GroupId);
            sql.Append(" order by SJLX,SJNR");
            cmd.CommandText = sql.ToString();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int dataType = DbUtils.GetInt(reader, 0);
                string dataValue = DbUtils.GetString(reader, 1);
                if (dataValue.Length > 0)
                {
                    switch (dataType)
                    {
                        case 1:
                            if (vipGroup.VipIds == null)
                                vipGroup.VipIds = new HashSet<int>();
                            vipGroup.VipIds.Add(Convert.ToInt32(dataValue));
                            break;
                        case 2:
                            if (vipGroup.IdCardTypes == null)
                                vipGroup.IdCardTypes = new List<int>();
                            vipGroup.IdCardTypes.Add(Convert.ToInt32(dataValue));
                            break;
                        case 3:
                            if (vipGroup.VipTypes == null)
                                vipGroup.VipTypes = new List<int>();
                            vipGroup.VipTypes.Add(Convert.ToInt32(dataValue));
                            break;
                        case 4:
                            if (vipGroup.SexTypes == null)
                                vipGroup.SexTypes = new List<int>();
                            vipGroup.SexTypes.Add(Convert.ToInt32(dataValue));
                            break;
                        case 5:
                            if (vipGroup.JobTypes == null)
                                vipGroup.JobTypes = new List<int>();
                            vipGroup.JobTypes.Add(Convert.ToInt32(dataValue));
                            break;
                        case 6:
                            if (vipGroup.IssueCardCompanyIds == null)
                                vipGroup.IssueCardCompanyIds = new List<int>();
                            vipGroup.IssueCardCompanyIds.Add(Convert.ToInt32(dataValue));
                            break;
                    }
                }
            }
            reader.Close();
        }
        public static void GetVipGroups(DbCommand cmd, StringBuilder sql, List<int> groupIds, List<CrmVipGroup> vipGroups)
        {
            if (groupIds.Count == 0)
                return;
            sql.Length = 0;
            sql.Append("select GRPID,SRFS,GZFS from HYK_HYGRP ");
            if (groupIds.Count == 1)
                sql.Append(" where GRPID = ").Append(groupIds[0]);
            else
            {
                sql.Append(" where GRPID in (").Append(groupIds[0]);
                for (int i = 1; i < groupIds.Count; i++)
                {
                    sql.Append(",").Append(groupIds[i]);
                }
                sql.Append(")");
            }
            cmd.CommandText = sql.ToString();
            DbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                CrmVipGroup vipGroup = new CrmVipGroup();
                vipGroups.Add(vipGroup);
                vipGroup.GroupId = DbUtils.GetInt(reader, 0);
                vipGroup.BirthdayMode = DbUtils.GetInt(reader, 1);
                vipGroup.OnlyExistVipId = (DbUtils.GetInt(reader, 2) == 0);
            }
            reader.Close();

            CrmVipGroup vipGroup2 = null;
            sql.Length = 0;
            sql.Append("select GRPID,SJLX,SJNR from HYK_HYGRP_GZMX ");
            if (groupIds.Count == 1)
                sql.Append(" where GRPID = ").Append(groupIds[0]);
            else
            {
                sql.Append(" where GRPID in (").Append(groupIds[0]);
                for (int i = 1; i < groupIds.Count; i++)
                {
                    sql.Append(",").Append(groupIds[i]);
                }
                sql.Append(")");
            }
            sql.Append(" order by GRPID,SJLX,SJNR");
            cmd.CommandText = sql.ToString();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int groupId = DbUtils.GetInt(reader, 0);
                int dataType = DbUtils.GetInt(reader, 1);
                string dataValue = DbUtils.GetString(reader, 2);
                if (dataValue.Length > 0)
                {
                    if ((vipGroup2 == null) || (vipGroup2.GroupId != groupId))
                    {
                        foreach (CrmVipGroup vipGroup in vipGroups)
                        {
                            if (vipGroup.GroupId == groupId)
                            {
                                vipGroup2 = vipGroup;
                                break;
                            }
                        }
                    }
                    if (vipGroup2 == null)
                    {
                        vipGroup2 = new CrmVipGroup();
                        vipGroup2.GroupId = groupId;
                    }
                }
                switch (dataType)
                {
                    case 1:
                        if (vipGroup2.VipIds == null)
                            vipGroup2.VipIds = new HashSet<int>();
                        vipGroup2.VipIds.Add(Convert.ToInt32(dataValue));
                        break;
                    case 2:
                        if (vipGroup2.IdCardTypes == null)
                            vipGroup2.IdCardTypes = new List<int>();
                        vipGroup2.IdCardTypes.Add(Convert.ToInt32(dataValue));
                        break;
                    case 3:
                        if (vipGroup2.VipTypes == null)
                            vipGroup2.VipTypes = new List<int>();
                        vipGroup2.VipTypes.Add(Convert.ToInt32(dataValue));
                        break;
                    case 4:
                        if (vipGroup2.SexTypes == null)
                            vipGroup2.SexTypes = new List<int>();
                        vipGroup2.SexTypes.Add(Convert.ToInt32(dataValue));
                        break;
                    case 5:
                        if (vipGroup2.JobTypes == null)
                            vipGroup2.JobTypes = new List<int>();
                        vipGroup2.JobTypes.Add(Convert.ToInt32(dataValue));
                        break;
                    case 6:
                        if (vipGroup2.IssueCardCompanyIds == null)
                            vipGroup2.IssueCardCompanyIds = new List<int>();
                        vipGroup2.IssueCardCompanyIds.Add(Convert.ToInt32(dataValue));
                        break;
                }
            }
            reader.Close();
        }
    }

    public class CrmEnCryptUtils
    {
        private const int C1 = 21469;
        private const int C2 = 12347;
        private const int KeyWord = 26493;
        private static string Decrypt(string src)
        {
            int key = KeyWord;
            char[] dest = new char[src.Length];
            for (int i = 0; i < src.Length; i++)
            {
                dest[i] = (char)(((byte)src[i]) ^ (key >> 8));
                key = (((src[i] + key) * C1) + C2) % 65536;
            }
            return new string(dest);
        }
        private static string Encrypt(string src)
        {
            int key = KeyWord;
            char[] dest = new char[src.Length];
            for (int i = 0; i < src.Length; i++)
            {
                dest[i] = (char)((byte)src[i] ^ (key >> 8));
                key = ((dest[i] + key) * C1 + C2) % 65536;
            }
            return new string(dest);
        }
        public static string DesDecryptCardTrack15(string src)
        {
            int len = src.Length;
            if ((len > 0) && ((len % 3) == 0))
            {
                len /= 3;
                try
                {
                    int i;
                    char[] src2 = new char[len];
                    for (i = 0; i < len; i++)
                    {
                        src2[i] = (char)int.Parse(src.Substring(i * 3, 3));
                    }
                    string dest2 = Decrypt(new string(src2));
                    char[] dest = new char[len * 2];
                    for (i = 0; i < len; i++)
                    {
                        dest[2 * i + 1] = (char)((((byte)(dest2[len - (i + 1)]) >> 4) & 15) + 48);
                        dest[2 * i] = (char)(((byte)(dest2[len - (i + 1)]) & 15) + 48);
                    }
                    string Dest = new string(dest);
                    if (Dest.Substring(0, 1).Equals(":"))
                        Dest = Dest.Substring(1, Dest.Length - 1);
                    return Dest;
                }
                catch
                {
                    return string.Empty;
                }
            }
            return string.Empty;
        }

        //public static bool IsNumber(string strInput)
        //{
        //    string strValue = strInput.Trim();
        //    Regex regex = new Regex(@"^[0-9]+\.?[0-9]*$");
        //    Match match = regex.Match(strValue);
        //    return match.Success;
        //}

        public static string DesEncryptCardTrack15(string cardTrack, byte[] key)
        {
            int len1 = cardTrack.Length;
            if (len1 == 0)
                return string.Empty;

            if ((len1 % 2) == 1)
            {
                cardTrack = ":" + cardTrack;
                len1++;
            }
            int len2 = (len1 / 2);

            byte[] bytes1 = Encoding.ASCII.GetBytes(cardTrack);
            byte[] bytes2 = null;
            char[] aa = null;
            int mod = (len2 % 8);
            if (mod == 0)
            {
                bytes2 = new byte[len2];
                aa = new Char[len2];
            }
            else
            {
                bytes2 = new byte[len2 + 8 - mod];
                aa = new Char[len2 + 8 - mod];
            }
            for (int i = 0; i < len2; i++)
            {
                bytes2[i] = (byte)(((bytes1[len1 - 2 * i - 1] - 48) << 4) | (bytes1[len1 - 2 * i - 2] - 48));
                aa[i] = (char)bytes2[i];
            }
            string dest2 = Encrypt(new string(aa));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < len2; i++)
            {
                sb.Append(((byte)dest2[i]).ToString("d3"));
            }
            return sb.ToString();



        }

        public static int ConvertDateTimeInt(System.DateTime time)
        {

            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));

            return (int)(time - startTime).TotalSeconds;

        }

        private static byte[] HexStrToBytes(string s)
        {
            byte[] bytes = new byte[s.Length / 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(s.Substring(i * 2, 2), 16);
            }
            return bytes;
        }

        

        public static string DesDecryptPassword(byte[] key)
        {
            string sPassword = string.Empty;
            try
            {
                for (int i = 0; i <= (key.Length - 1); i++)
                {
                    char ch = Convert.ToChar((long)(key[i]));
                    sPassword = sPassword + ch.ToString();
                }
                return Decrypt(sPassword);
            }
            catch
            {
                return "-1@#3*~!%2";
            }

        }
    }
}
