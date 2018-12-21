﻿using CoSales.Model;
using CoSales.Model.NodeType;
using CoSales.Model.PO;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoSales.DAL
{
    public class ProductDAO
    {
        public static readonly ProductDAO DAO = new ProductDAO();

        /// <summary>
        /// 分页查询产品信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ResultPager<Product> GetProductInfo(Product param)
        {
            string sql = @"
SELECT  ROW_NUMBER() OVER ( ORDER BY A.ID ) AS RN ,
        a.ID ,
        ProductName ,
        Price ,
        ProductStockNumber ,
        ProductSellNumber ,
        State ,
        nd.NodeName StateText
FROM    T_PRODUCT a
        LEFT JOIN dbo.T_DIC_NODE nd ON a.state = nd.NodeID
        /**where**/ 
";

            // Dapper 扩展 SqlBuilder
            SqlBuilder builder = new SqlBuilder();

            // 产品名称模糊匹配
            if (!string.IsNullOrEmpty(param.ProductName))
            {
                builder.Where("ProductName LIKE @ProductName", new { ProductName = string.Format($"%{param.ProductName}%") });
            }
            // 产品状态筛选
            if (param.StateList != null && param.StateList.Count > 0)
            {
                builder.Where("State in @StateList", new { StateList = param.StateList });
            }

            ResultPager<Product> resPager = DapperHelper.GetResultPager<Product>(builder, sql, param);

            return resPager;
        }

        /// <summary>
        /// 根据id获取产品实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Product GetProduct(int id)
        {
            return DapperHelper.Get<Product>(id);
        }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int? RemoveProduct(int id)
        {
            string sql = @"UPDATE T_PRODUCT SET State=@State WHERE ID=@ID";
            object pams = new { State = (int)EnumProductState.已删除, ID = id };
            return DapperHelper.Excute(sql, pams);
        }
    }
}