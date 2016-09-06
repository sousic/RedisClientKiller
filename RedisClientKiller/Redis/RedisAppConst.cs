using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisClientKiller.Redis
{
    public class RedisAppConst
    {
        public static IList<string> ReidsCacheStageServerList
        {
            get
            {
                IList<string> hostList = new List<string>();
                try
                {
                    NameValueCollection collection = (NameValueCollection)ConfigurationManager.GetSection("Redis/RedisCacheStageServer");


                    foreach (var item in collection)
                    {
                        hostList.Add(collection[item.ToString()]);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return hostList;
            }
        }

        public static IList<string> ReidsCacheLiveServerList
        {
            get
            {
                IList<string> hostList = new List<string>();
                try
                {
                    NameValueCollection collection = (NameValueCollection)ConfigurationManager.GetSection("Redis/RedisCacheLiveServer");


                    foreach (var item in collection)
                    {
                        hostList.Add(collection[item.ToString()]);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return hostList;
            }
        }
    }
}
