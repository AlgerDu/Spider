using D.Util.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Util.Interface
{
    /// <summary>
    /// ajax 请求类型
    /// </summary>
    public enum AjaxRequestTypes
    {
        GET,
        POST
    }

    public enum AjaxContenTypes
    {
        /// <summary>
        /// application/json
        /// </summary>
        JSON,

        /// <summary>
        /// text/plain
        /// </summary>
        Text,

        /// <summary>
        /// application/x-www-form-urlencoded
        /// </summary>
        x_www_form_urlencoded
    }

    /// <summary>
    /// C# 版的 JQuery
    /// 主要实现 ajax 功能
    /// </summary>
    public interface IjQuery
    {
        /// <summary>
        /// ajax 请求
        /// </summary>
        /// <typeparam name="T">返回数据的类型</typeparam>
        /// <param name="type">请求类型：GET、POST</param>
        /// <param name="url">发送请求的 URL</param>
        /// <param name="data">要发送到服务器的数据</param>
        /// <param name="success">当请求成功时运行的函数</param>
        /// <param name="error">请求失败要运行的函数 默认不设置</param>
        /// <param name="timeout">本地的请求超时时间（以毫秒计） 默认值为 -1 表示使用默认的超时时间</param>
        Task Ajax<T>(
            AjaxRequestTypes type,
            string url,
            object data,
            AjaxContenTypes contentType,
            EventHandler<jQuerySuccessEventArgs<T>> success,
            EventHandler<jQueryErrorEventArgs> error = null,
            int timeout = -1)
            where T : class;

        /// <summary>
        /// ajax Get 请求
        /// </summary>
        /// <typeparam name="T">返回数据的类型</typeparam>
        /// <param name="url">发送请求的 URL</param>
        /// <param name="success">当请求成功时运行的函数</param>
        Task Get<T>(
            string url,
            EventHandler<jQuerySuccessEventArgs<T>> success
            ) where T : class;

        /// <summary>
        /// ajax Post 请求
        /// </summary>
        /// <typeparam name="T">返回数据的类型</typeparam>
        /// <param name="url">发送请求的 URL</param>
        /// <param name="data">要发送到服务器的数据</param>
        /// <param name="success">当请求成功时运行的函数</param>
        Task Post<T>(
            string url,
            object data,
            EventHandler<jQuerySuccessEventArgs<T>> success
            ) where T : class;
    }
}
