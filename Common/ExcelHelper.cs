using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Reflection;
using System.IO;


namespace Common
{

    /// <summary>
    /// 输出Excel类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ExcelHelper<T>
    {
        /// <summary>
        /// 输出Excel的方法
        /// </summary>
        /// <param name="t">列表</param>
        /// <param name="path">存放地址（地址尾部加“/”,不包括文件名）</param>
        /// <param name="columnName">Excel的列名</param>
        /// <returns>返回true/false</returns>
        public bool ToExcel(List<T> t, string path, string[] columnName)
        {
            bool res = false;
            StringBuilder strBu = new StringBuilder();

            string fourCode = "";
            //随机生成类
            Random rNum = new Random();
            //返回指定范围内的随机数
            int num1 = rNum.Next(0, 9);
            int num2 = rNum.Next(0, 9);
            int num3 = rNum.Next(0, 9);
            int num4 = rNum.Next(0, 9);

            int[] nums = new int[4] { num1, num2, num3, num4 };
            //循环添加四个随机生成数
            for (int i = 0; i < nums.Length; i++)
            {
                fourCode += nums[i].ToString();
            }

            //输出的地址加文件名
            var fileNamePath = path + "报名人信息表" + DateTime.Parse(DateTime.Now.ToString()).ToString("yyyy.MM.dd") + "_" + fourCode + ".xls";
            //输出的规则
            for (int i = -1; i < t.Count; i++)
            {
                var columnCount = columnName.Length;
                if (i == -1)
                {
                    for (int j = 0; j < columnCount; j++)
                    {
                        strBu.Append(columnName[i + 1 + j] + "\t");
                    }
                    strBu.Append("\n");
                }
                else
                {
                    //反射获取属性值
                    var tc = typeof(T).GetProperties();
                    foreach (PropertyInfo pi in tc)
                    {
                        object value = t[i].GetType().GetProperty(pi.Name).GetValue(t[i], null);
                        strBu.Append(value.ToString() + "\t");

                    }
                    strBu.Append("\n");
                }

            }
            //将str写入Excel表格
            var str = strBu.ToString();
            //判断文件是否存在
            if (!File.Exists(path))
            {
                FileStream fs1 = new FileStream(fileNamePath, FileMode.Create, FileAccess.Write);//创建写入文件   
                StreamWriter sw = new StreamWriter(fs1);
                sw.Write(str);
                sw.Close();
                fs1.Close();
                res = true;
            }
            else
            {
                StreamWriter strmsave = new StreamWriter(fileNamePath, false, System.Text.Encoding.Default);
                strmsave.Write(str);
                strmsave.Close();
                res = true;
            }
            return res;

        }
    }
}



