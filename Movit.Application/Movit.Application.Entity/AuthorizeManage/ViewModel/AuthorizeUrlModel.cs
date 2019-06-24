﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movit.Application.Entity.AuthorizeManage.ViewModel
{
    /// <summary>
    ///  
    /// Copyright (c) 2018-2016  
    /// 创建人： 
    /// 日 期：2015.11.27
    /// 描 述：授权功能Url、操作Url
    /// </summary
    public class AuthorizeUrlModel
    {
        /// <summary>
        /// 授权主键
        /// </summary>		
        public string AuthorizeId { get; set; }
        /// <summary>
        /// 功能主键
        /// </summary>
        public string ModuleId { set; get; }
        /// <summary>
        /// Url地址
        /// </summary>
        public string UrlAddress { set; get; }
        /// <summary>
        /// 名称
        /// </summary>
        public string FullName { set; get; }
    }
}
