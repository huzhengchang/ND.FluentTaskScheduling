﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

//**********************************************************************
//
// 文件名称(File Name)：EnumHelper.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-26 11:14:56         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-26 11:14:56          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Helper
{
    public class EnumHelper<T> where T : struct
                                
    {
        /// <summary>
        /// 将指定枚举转为列表控件的数据源
        /// </summary>
        /// <returns>Text:枚举的描述，value：枚举的值</returns>
        public static Dictionary<string, string> GetAllEnumsOfList()
        {
            Type t = typeof(T);
            FieldInfo[] fieldInfoList = t.GetFields();
            IList<SelectListItem> dt = new List<SelectListItem>();

            var query = from q in fieldInfoList
                        where q.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false).Length > 0
                        select new SelectListItem()
                        {
                            Value = q.Name,
                            Text = (q.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false)[0] as System.ComponentModel.DescriptionAttribute).Description ?? q.Name
                        };
            Dictionary<string, string> dic = new Dictionary<string, string>();
            query.ToList().ForEach(x =>
            {
              
                if(!dic.ContainsKey(x.Value))
                {
                   
                    dic.Add(x.Value, x.Text);
                }
            });
            return dic;
        }

       
        /// <summary>
        /// 将指定枚举转为列表控件的数据源
        /// </summary>
        /// <returns>Text:枚举的描述，value：枚举的值</returns>
        public static IEnumerable<SelectListItem> GetAllEnumsOfList2()
        {
            Type t = typeof(T);
            FieldInfo[] fieldInfoList = t.GetFields();
            IList<SelectListItem> dt = new List<SelectListItem>();

            var query = from q in fieldInfoList
                        where q.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false).Length > 0
                        select new SelectListItem()
                        {
                            Value = ((int)q.GetValue(null)).ToString(),
                            Text = (q.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false)[0] as System.ComponentModel.DescriptionAttribute).Description ?? q.Name
                        };
            return query;
        }

        /// <summary>
        /// 获得指定枚举的描述信息
        /// </summary>
        /// <param name="enumInstance"></param>
        /// <returns></returns>
        public static string GetText(T enumInstance)
        {
            Type t = typeof(T);
            FieldInfo[] fieldInfoList = t.GetFields();

            var query = from q in fieldInfoList
                        where q.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false).Length > 0
                        && string.Equals(q.Name, enumInstance.ToString(), StringComparison.CurrentCultureIgnoreCase)
                        select ((System.ComponentModel.DescriptionAttribute)q.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false)[0]).Description;
            if (query != null)
            {
                return query.First<string>();
            }
            return null;
            /*
            string strReturn = string.Empty;


            foreach (FieldInfo tField in fieldInfoList)
            {
                if (!tField.IsSpecialName && tField.Name.ToLower() == enumInstance.ToString().ToLower())
                {
                    DescribtionAtrtribute[] enumAttributelist = (DescribtionAtrtribute[])tField.GetCustomAttributes(typeof(DescribtionAtrtribute), false);
                    if (enumAttributelist != null && enumAttributelist.Length > 0)
                    {
                        strReturn = enumAttributelist[0].Description;
                        break;
                    }
                }
            }
            return strReturn;
             * */
        }

        /// <summary>
        /// 通过枚举，获得其枚举值
        /// </summary>
        /// <param name="enumInstance"></param>
        /// <returns></returns>
        public static string GetValue(T enumInstance)
        {
            return enumInstance.ToString();
            /*
            Type t = typeof(T);
            FieldInfo[] fieldInfoList = t.GetFields();
            string strReturn = string.Empty;
            foreach (FieldInfo tField in fieldInfoList)
            {
                if (!tField.IsSpecialName && tField.Name.ToLower() == enumInstance.ToString().ToLower())
                {
                    strReturn = tField.Name;
                    break;
                }
            }
            return strReturn;*/
        }

        /// <summary>
        /// 将字符串转换为枚举
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static T GetEnum(string Value)
        {
            Type t = typeof(T);
            FieldInfo field = t.GetField(Value);
            T e = default(T);
            System.Enum.TryParse<T>(Value, out e);
            return e;
        }

    }
}
