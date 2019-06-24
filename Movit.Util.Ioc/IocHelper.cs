using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Movit.Util.Ioc
{
    
    /// <summary>
    /// 自己写的简单的Ioc容器
    /// 作者：姚栋
    /// 日期:20160302
    /// </summary>
    public static class IocHelper
    {           
        /// <summary>
        /// 存放实例类型
        /// </summary>
        private readonly static Dictionary<string, System.Type> typeDict = new Dictionary<string, Type>(1024);

        /// <summary>
        /// 获取注册的键值
        /// </summary>
        /// <param name="intefaceType"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private static string GetFullKey(System.Type intefaceType, string key = null)
        {
            if (key != null)
            {
                return intefaceType.ToString() + "$" + key;
            }
            return intefaceType.ToString() + "$";
        }

        /// <summary>
        /// 注册 类型
        /// </summary>
        /// <param name="fullKey"></param>
        /// <param name="implType"></param>
        private static void RegisterType(string fullKey, System.Type implType)
        {
            try
            {
                typeDict.Add(fullKey, implType);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(
                    string.Format("注入出错: key={0}, type={1}, error={2}", fullKey, implType.FullName, ex.ToString()));
            }
        }       

        /// <summary>
        /// 注入多个类型，获取的时候用键进行区分
        /// </summary>
        /// <typeparam name="TInterface">需要生成实例的接口类型</typeparam>
        /// <typeparam name="TImplementation">实现接口的类型</typeparam>
        /// <param name="key"></param>
        public static void RegisterType<TInterface, TImplementation>(string key)
            where TImplementation : TInterface
        {
            var fullKey = GetFullKey(typeof(TInterface), key);
            //typeDict.Add(fullKey, typeof(TImplementation));
            RegisterType(fullKey, typeof(TImplementation));
        }
        public static void RegisterType<TInterface, TImplementation>()
            where TImplementation : TInterface
        {
            RegisterType<TInterface, TImplementation>(null);
        }
        

        /// <summary>
        /// 注入类型
        /// </summary>
        /// <param name="interfaceType"></param>
        /// <param name="implType"></param>
        public static void RegisterType(Type interfaceType, Type implType, string key)
        {
            var fullKey = GetFullKey(interfaceType, key);
            //typeDict.Add(fullKey, implType);
            RegisterType(fullKey, implType);

        }
        public static void RegisterType(Type interfaceType, Type implType)
        {
            RegisterType(interfaceType, implType, null);
        }


        /// <summary>
        /// 注册一个类型为单例
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <typeparam name="TImplemention"></typeparam>
        private static void RegisterSingleInstanceType<TInterface, TImplemention>()
            where TImplemention : TInterface
        {
            //defaultContainer.RegisterType<TInterface, TImplemention>(
            //   new ContainerControlledLifetimeManager());
            throw new NotImplementedException();
        }

        /// <summary>
        /// 注册一个类型为单例
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <typeparam name="TImplemention"></typeparam>
        private static void RegisterSingleInstanceType(Type from, Type to)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 判断是否为指定的接口类型注册了实现类。
        /// </summary>
        /// <param name="interfaceType"></param>
        /// <returns></returns>
        public static bool IsRegistered(System.Type interfaceType, string key)
        {
            return typeDict.ContainsKey(GetFullKey(interfaceType, key));
        }
        public static bool IsRegistered(System.Type interfaceType)
        {
            return IsRegistered(interfaceType, null);
        }

        /// <summary>
        /// 移除已注册的类
        /// </summary>
        /// <param name="interfaceType"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool Remove(System.Type interfaceType, string key)
        {
            return typeDict.Remove(GetFullKey(interfaceType, key));
        }
        public static bool Remove(System.Type interfaceType)
        {
            return Remove(interfaceType, null);
        }

        /// <summary>
        /// 判断为某个接口类型是否注册实现类
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool IsRegistered<TInterface>(string key)
        {
            return typeDict.ContainsKey(GetFullKey(typeof(TInterface), key));
        }
        public static bool IsRegistered<TInterface>()
        {
            return IsRegistered<TInterface>(null);
        }

        public static bool Remove<TInterface>(string key)
        {
            return typeDict.Remove(GetFullKey(typeof(TInterface), key));
        }
        public static bool Remove<TInterface>()
        {
            return Remove<TInterface>(null);
        }

        /// <summary>
        /// 获取实例
        /// </summary>
        /// <param name="interfaceType">接口类型</param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object GetInstance(Type interfaceType, string key, params object[] args)
        {
            System.Type implType;
            var fullKey = GetFullKey(interfaceType, key);
            if (typeDict.TryGetValue(fullKey, out implType))
            {
                return Activator.CreateInstance(implType, args);
            }
            throw new ApplicationException("未注册类型:" + fullKey);
        }
        public static object GetInstance(Type interfaceType, params object[] args)
        {
            return GetInstance(interfaceType, null, args);
        } 


        /// <summary>
        /// 获取实例
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static TInterface GetInstance<TInterface>(string key, params object[] args)
        {
            return (TInterface)GetInstance(typeof(TInterface), key, args);
        }
        public static TInterface GetInstance<TInterface>(params object[] args)
        {
            return GetInstance<TInterface>(null, args);
        }
                

        /// <summary>
        /// 获取实例
        /// </summary>
        /// <typeparam name="TInterface">接口类型</typeparam>
        /// <param name="key">键值，用于区分多个类型</param>
        /// <returns></returns>
        public static TInterface Resolve<TInterface>(string key, params object[] args)
        {
            return GetInstance<TInterface>(key, args);
        }
        public static TInterface Resolve<TInterface>(params object[] args)
        {
            return Resolve<TInterface>(null, args);
        }

        /// <summary>
        /// 获取实例
        /// </summary>
        /// <param name="interfaceType">接口类型</param>
        /// <param name="key">键值，用于区分多个类型</param>
        /// <returns></returns>
        public static object Resolve(Type interfaceType, string key, params object[] args)
        {
            return GetInstance(interfaceType, key, args);
        }
        public static object Resolve(Type interfaceType,  params object[] args)
        {
            return Resolve(interfaceType, null, args);
        }


        /// <summary>
        /// 获取所有实例
        /// </summary>
        /// <param name="interfaceType"></param>
        /// <returns></returns>
        public static IEnumerable<object> GetAllInstances(Type interfaceType, params object[] args)
        {
            List<object> targetList = new List<object>();
            var prefix = GetFullKey(interfaceType);
            foreach (var item in typeDict)
            {
                if (item.Key.StartsWith(prefix))
                {
                    targetList.Add(Activator.CreateInstance(item.Value,args));
                }
            }
            return targetList;
        }

        /// <summary>
        /// 获取所有实例
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <returns></returns>
        public static IEnumerable<TInterface> GetAllInstances<TInterface>(params object[] args)
        {
            return GetAllInstances(typeof(TInterface), args).Cast<TInterface>();
        }
    }
}
