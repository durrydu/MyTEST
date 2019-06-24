using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movit.Sys.Api
{
    /// <summary>
    /// 返回值的基类。
    /// </summary>    
    public class Resp
    {
        private string _errorCode = "0000";

        /// <summary>
        /// 错误代码
        /// </summary>
        /// <returns></returns>
        public string code
        {
            get { return _errorCode; }
            set { _errorCode = value; }
        }

        //private bool _state = true;

        /// <summary>
        /// 判断执行结果正确与否, =true表示执行正确, =false表示执行失败
        /// </summary>
        //public bool state
        //{
        //    get
        //    {
        //        return _state;
        //    }
        //    set
        //    {
        //        if (value == true)
        //        {
        //            _errorCode = "0000";
        //        }
        //        _state = value;
        //    }
        //}

        /// <summary>
        /// 执行失败时的错误消息
        /// </summary>
        public string msg { get; set; }

        /// <summary>
        /// 初始化一个产生业务逻辑异常时的返回值
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public static Resp BusinessError(string errorMessage)
        {
            return new Resp()
            {
                //state = false,
                code = "-1",
                msg = errorMessage,
            };
        }


        public static Resp BusinessError(string errorMessage, string errorCode = "-1")
        {
            return new Resp()
            {
                //state = false,
                code = errorCode,
                msg = errorMessage,
            };
        }

        /// <summary>
        /// 初始化一个产生业务逻辑异常的返回值，希望带上一部分数据。
        /// </summary>
        /// <typeparam name="T">需要带上数据的类型</typeparam>
        /// <param name="errorMessage">错误消息</param>
        /// <param name="data">需要带上的数据</param>
        /// <returns>返回给移动端的对象</returns>
        public static Resp<T> BusinessError<T>(string errorMessage, T data, string errorCode = "-1")
        {
            return new Resp<T>()
            {
                //state = false,
                code = errorCode,
                msg = errorMessage,
                data = data,
            };
        }


        /// <summary>
        /// 初始化一个产生非业务逻辑异常时的返回值
        /// </summary>
        /// <returns>返回给移动端的对象</returns>
        public static Resp ServerError(Exception ex)
        {
            return new Resp()
            {
                //state = false,
                code = "500",
                msg = "服务器执行异常"
            };
        }
        /// <summary>
        /// 初始化一个产生非业务逻辑异常时的返回值
        /// </summary>
        /// <returns>返回给移动端的对象</returns>
        public static Resp ServerError(string msg)
        {
            return new Resp()
            {
                //state = false,
                code = "0001",
                msg = msg
            };
        }
        /// <summary>
        /// 初始化一个成功执行的返回值。执行成功，并返回obj。
        /// </summary>
        /// <typeparam name="T">需要返回的数据类型</typeparam>
        /// <param name="obj">需要返回的数据对象</param>
        /// <returns>返回给移动端的对象</returns>
        public static Resp<T> Success<T>(T obj)
        {
            return new Resp<T>()
            {
                //state = true,
                code = "0000",
                msg = string.Empty,
                data = obj,
            };
        }

        /// <summary>
        /// 执行成功，无返回值
        /// </summary>
        /// <returns></returns>
        public static Resp Success()
        {
            return new Resp()
            {
                //state = true,
                code = "0000",
                msg = null,
            };
        }

    }

    /// <summary>
    /// 返回给移动端的值的扩展类，可以带数据返回。
    /// </summary>
    /// <typeparam name="T">需要返回的数据类型</typeparam>    
    public class Resp<T> : Resp
    {
        /// <summary>
        /// 需要返回给移动端的数据
        /// </summary>
        public T data { get; set; }

        public Resp()
        {
            //this.state = true;
        }

        public Resp(T data)
        {
            //this.state = true;
            this.data = data;
        }
    }
}
