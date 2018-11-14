using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChangYi.Crm.Server
{
    public class Payment
    {
        public int Id = 0;
        public string Code = string.Empty;
        public string NewCode = string.Empty;
        public string Name = string.Empty;
        public int CouponType = -1;
        public bool IsLeaf = false;
        public bool JoinPromDecMoney = false;
        public bool IsExist = false;
    }
  
    public class ArticleCategory
    {
        public int Id = 0;
        public string Code = string.Empty;
        public string NewCode = string.Empty;
        public string Name = string.Empty;
        public string Spell = string.Empty;
        public bool IsLeaf = false;
        public bool IsExist = false;
    }
    public class ArticleBrand
    {
        public int Id = 0;
        public string Code = string.Empty;
        public string NewCode = string.Empty;
        public string Name = string.Empty;
        public string Spell = string.Empty;
        public string Owner = string.Empty;
        public bool IsLeaf = false;
        public bool IsExist = false;
    }
    public class Article
    {
        public int Id = 0;
        public string Code = string.Empty;
        public string NewCode = string.Empty;
        public string Name = string.Empty;
        public string ShortName = string.Empty;
        public string Spell = string.Empty;
        public string Unit = string.Empty;
        public string Color = string.Empty;
        public string Spec = string.Empty;
        public string ModelCode = string.Empty;
        public string BrandCode = string.Empty;
        public string CategoryCode = string.Empty;
        public string ContractCode = string.Empty;
        public int BrandId = 0;
        public int CategoryId = 0;
        public int ContractId = 0;
        public bool IsExist = false;
    }
    public class Dept
    {
        public int Id = 0;
        public string Code = string.Empty;
        public string NewCode = string.Empty;
        public string Name = string.Empty;
        public string FullName = string.Empty;
        public string DeptType = string.Empty;
        public bool IsExist = false;
    }
    public class Contract
    {
        public int Id = 0; 
        public string Code = string.Empty;
        public string NewCode = string.Empty;
        public string SuppName = string.Empty;
        public string SuppCode = string.Empty;
        public string DeptCode = string.Empty;
        public int DeptId = 0;
        public bool IsValid = false;
        public bool IsExist = false;
    }
    public class ArticleSaleCode
    {
        public string SaleCode = string.Empty;
        public string NewSaleCode = string.Empty;
        public string ArticleCode = string.Empty;
        public int ArticleId = 0;
    }
    public class DeptArticle
    {
        public string DeptCode = string.Empty;
        public string ArticleCode = string.Empty;
        public int DeptId = 0;
        public int ArticleId = 0;
        public bool IsExist = false;
    }
    public class Promotion
    {
        public int Id = 0;
        public int Year = 0;
        public int PeriodNum = 0;
        public string Subject = string.Empty;
        public string Content = string.Empty;
        public DateTime BeginTime = DateTime.MinValue;
        public DateTime EndTime = DateTime.MinValue;
        public bool IsExist = false;
    }
}
