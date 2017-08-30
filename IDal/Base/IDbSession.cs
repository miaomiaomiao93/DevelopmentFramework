using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDal
{
    public interface IDbSession
    {
        DbContext entity { get; }
        /// <summary>
        /// 范例
        /// </summary>
        //IProductAdminDal ProductDal { get; set; }       
        int ExcuteSql(string sql, object[] parameters);
        bool SaveChanges();
    }
}
