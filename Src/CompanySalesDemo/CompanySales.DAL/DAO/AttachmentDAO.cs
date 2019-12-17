﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanySales.Model.Entity;

namespace CompanySales.DAL
{
    public class AttachmentDAO
    {
        /// <summary>
        /// 按照附件主键id进行删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool DeleteById(int id)
        {
            using (SaleContext db = new SaleContext())
            {
                var entity = db.Attachment.Find(id);
                db.Attachment.Remove(entity);
                db.SaveChanges();
                return true;
            }
        }

        /// <summary>
        /// 根据关联资源的主键id获取所有附件信息
        /// </summary>
        /// <param name="relatedID"></param>
        /// <returns></returns>
        public static List<Attachment> GetListByRelatedId(int relatedID)
        {
            using (SaleContext db = new SaleContext())
            {
                List<Attachment> list = db.Attachment.Where(t => t.RelatedID == relatedID).ToList();
                return list;
            }
        }

        /// <summary>
        /// 添加附件资源
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static bool Add(Attachment entity)
        {
            using (SaleContext db = new SaleContext())
            {
                db.Attachment.Add(entity);
                db.SaveChanges();
                return true;
            }
        }

        /// <summary>
        /// 根据主键id获取实体对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Attachment Get(int id)
        {
            using (SaleContext db = new SaleContext())
            {
                var res = db.Attachment.Find(id);
                return res;
            }
        }
    }
}
