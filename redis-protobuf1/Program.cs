using ProtoBuf;
using RedisHelper;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace redis_protobuf1
{    

    class Program
    {
        static void Main(string[] args)
        {
            RedisClient myRedisClient = null;
            Parallel.For(0, 1, (i) =>
            {
                myRedisClient = new RedisClient(RedisSingletonConnection.Instance);
            });
            myRedisClient.Key.KeyDelete("tww");
            myRedisClient.String.Incr("tww");
            string str = myRedisClient.String.Get("tww");
            //HashTest();
            return;
            //SetAndGet();            
            //TestInteger();
            //TestDouble();
            //TestBool();
            //TestSerializedDate();
            //TestProtobufNet();
            //TestSerializedArray();
            //TestProtobufAndRedis();
            //TestProtobufAndRedis_List();
            //TestProtobufAndRedis_IEnumerable();
            //TestDeleteKey();
            //TestSync();
            //TestAsync();
            Console.ReadKey();
        }

        //public static void HashTest()
        //{            
        //    RedisManager.UnitTestSingleton();
        //    return;
        //    myRedisClient.Key.KeyDelete("201904102002");
        //    Parallel.For(0, 1000, (i) =>
        //      {
        //          myRedisClient.Hash.HashIncrement("201904102002", "fiedname", 1);
        //          Thread.Sleep(20);
        //      });
        //    var list = myRedisClient.Hash.HashGetAll("201904102002");
        //    string s = string.Empty;

        //    Console.ReadKey();
        //}

        //public static void SetAndGet()
        //{
        //    const string value = "abcdefg";
        //    myRedisClient..StringSet("mykey", value);
        //    var val = myRedisClient.Key.StringGet("mykey");
        //    Console.WriteLine(val);
        //}


        //public static void TestInteger()
        //{
        //    const int num = 5;
        //    myRedisClient.Key.StringSet("StackExchangeRedis_TestInteger", num);
        //    var val = myRedisClient.Key.StringGet("StackExchangeRedis_TestInteger");            
        //    Console.WriteLine(val);
        //}


        //public static void TestDouble()
        //{
        //    const double num = 5.34567;
        //    myRedisClient.Key.StringSet("StackExchangeRedis_TestDouble", num);
        //    var val = myRedisClient.Key.StringGet("StackExchangeRedis_TestDouble");
        //    Console.WriteLine(val);
        //}


        //public static void TestBool()
        //{
        //    const bool b = true;
        //    myRedisClient.Key.StringSet("StackExchangeRedis_TestBoolT", b);
        //    var val = myRedisClient.Key.StringGet("StackExchangeRedis_TestBoolT");
        //    Console.WriteLine(val);
        //}


        //public static void TestSerializedDate()
        //{
        //    DateTime now = DateTime.Now;
        //    SetCache<DateTime>("StackExchangeRedis_TestSerializedDate", now);
        //    var val = GetCache<DateTime>("StackExchangeRedis_TestSerializedDate");
        //    Console.WriteLine(now);
        //    Console.WriteLine(val);
        //}


        public static void TestProtobufNet()
        {
            var ppl = new People()
            {
                ID = 1,
                FirstName = "John",
                LastName = "Doe",
                Address = new AddressModel()
                {
                    AptNumber = 56,
                    StreetAdress = "123 Main Street",
                    City = "Toronto",
                    State = "Ontario",
                    Country = "Canada"
                }
            };
            using (var file = File.Create("person.bin"))
            {
                Serializer.Serialize<People>(file, ppl);
            }

            People newPerson;
            using (var file = File.OpenRead("person.bin"))
            {
                newPerson = Serializer.Deserialize<People>(file);
            }
            Console.WriteLine(newPerson.Address.StreetAdress);
            Console.WriteLine(newPerson.Address.Country + "==" + ppl.Address.Country);
        }


        //public static void TestSerializedArray()
        //{
        //    int[] arr = new int[4] { 5, 7, 11, 17 };
        //    SetCache<int[]>("StackExchangeRedis_TestSerializedArray", arr);
        //    Console.WriteLine("Array length = " + arr.Length);
        //    arr = GetCache<int[]>("StackExchangeRedis_TestSerializedArray");
        //    Console.WriteLine("Deserialized array length = " + arr.Length);
        //    Console.WriteLine(arr[2]);
        //}


        //public static void TestProtobufAndRedis()
        //{
        //    var ppl = new PeopleNoAttr()
        //    {
        //        ID = 2,
        //        FirstName = "Jane",
        //        LastName = "Smith",
        //        Address = new AddressNoAttr()
        //        {
        //            AptNumber = 56,
        //            StreetAdress = "123 Main Street",
        //            City = "Toronto",
        //            State = "Ontario",
        //            Country = "Canada"
        //        }
        //    };
        //    SetCacheNoAttr<PeopleNoAttr>("StackExchangeRedis_TestProtobufAndRedis_NoAttr", ppl);
        //    var val2 = GetCache<PeopleNoAttr>("StackExchangeRedis_TestProtobufAndRedis_NoAttr");
        //    Console.WriteLine(val2.Address.AptNumber);
        //}


        //public static void TestProtobufAndRedis_List()
        //{
        //    var cachekey = "StackExchangeRedis_TestProtobufAndRedisList";
        //    List<People> ppl = GenerateList();
        //    SetCache<List<People>>(cachekey, ppl);
        //    var val2 = GetCache<List<People>>(cachekey);
        //    Console.WriteLine(val2[1].Address.StreetAdress);
        //}


        //public static void TestProtobufAndRedis_IEnumerable()
        //{
        //    var cachekey = "StackExchangeRedis_TestProtobufAndRedisIEnumerable";
        //    List<People> ppl = GenerateList();
        //    IEnumerable<People> Ippl = (IEnumerable<People>)ppl;
        //    SetCache<IEnumerable<People>>(cachekey, ppl);
        //    var val2 = GetCache<IEnumerable<People>>(cachekey);
        //    var el = val2.ElementAt(1);
        //    Console.WriteLine(el.Address.StreetAdress);
        //}


        //public static void TestDeleteKey()
        //{
        //    DeleteFromCache("StackExchangeRedis_TestProtobufAndRedis");
        //}

        //// TO DO:
        //// =====
        //// no attributes
        //// twemproxy
        //// compare to old redis: C:\workspace\CareerCruising_Core\CC.Data_Tests\RemoteCacheProvider_Test.cs


        ////******************************

        //public static void TestSync()
        //{
        //    var aSync = myRedisClient.Key.StringGet("StackExchangeRedis_TestDouble");
        //    var bSync = myRedisClient.Key.StringGet("StackExchangeRedis_TestInteger");
        //}

        //public static void TestAsync()
        //{
        //    var aPending = myRedisClient.Key.StringGetAsync("StackExchangeRedis_TestDouble");
        //    var bPending = myRedisClient.Key.StringGetAsync("StackExchangeRedis_TestInteger");
        //    var a = myRedisClient.Key.Wait(aPending);
        //    var b = myRedisClient.Key.Wait(bPending);
        //}

        ////******************************

        ///*
        //public static void TestExpirationDate()
        //{
        //    var cachekey = "StackExchangeRedis_TestExpirationDate";
        //    RedisStore.RedisCache.StringSet(cachekey, "testing expiration date");
        //    RedisStore.RedisCache.KeyExpire(cachekey, TimeSpan.FromMinutes(30));
        //    var ttl = RedisStore.RedisCache.KeyTimeToLive(cachekey);
        //    Console.Write(ttl);
        //}


        //public static void TestDeleteKeysByPartOfName()
        //{
        //    DeleteKeysByPartOfName("StackExchangeRedis_");
        //}*/

        ///*
        //public static void TestDeleteAllKeys()
        //{
        //    ClearCache();
        //}*/

        //#region non-test methods


        //private static byte[] DataToBytes<T>(T data)
        //{
        //    MemoryStream stream = new MemoryStream();
        //    //ProtoBuf序列化开始
        //    Serializer.Serialize(stream, data);
        //    byte[] bytes = stream.ToArray();
        //    stream.Close();
        //    return bytes;
        //}

        //private static List<People> GenerateList()
        //{
        //    List<People> ppl = new List<People>();
        //    var person1 = new People()
        //    {
        //        ID = 1,
        //        FirstName = "Jane",
        //        LastName = "Smith",
        //        Address =
        //            new AddressModel()
        //            {
        //                AptNumber = 51,
        //                StreetAdress = "123 Main Street",
        //                City = "Toronto",
        //                State = "Ontario",
        //                Country = "Canada"
        //            }
        //    };
        //    var person2 = new People()
        //    {
        //        ID = 2,
        //        FirstName = "John",
        //        LastName = "Doe",
        //        Address =
        //            new AddressModel()
        //            {
        //                AptNumber = 52,
        //                StreetAdress = "678 Main Street",
        //                City = "Toronto1",
        //                State = "Ontario1",
        //                Country = "Canada1"
        //            }
        //    };
        //    ppl.Add(person1);
        //    ppl.Add(person2);
        //    return ppl;
        //}

        //// Serialization/deserialization and caching:
        //public static bool SetCache<T>(string key, T value)
        //{
        //    if (!string.IsNullOrWhiteSpace(key))
        //    {
        //        return myRedisClient.Key.StringSet(key, DataToBytes<T>(value));
        //    }
        //    return false;
        //}

        //public static bool SetCacheNoAttr<T>(string key, T value)
        //{
        //    if (!string.IsNullOrWhiteSpace(key))
        //    {

        //        Console.WriteLine(DataToBytes<T>(value));
        //        return myRedisClient.Key.StringSet(key, DataToBytes<T>(value));
        //    }
        //    return false;
        //}


        //public static T GetCache<T>(string key)
        //{
        //    byte[] val = myRedisClient.Key.StringGet(key);
        //    MemoryStream stream = new MemoryStream(val, false);
        //    return Serializer.Deserialize<T>(stream);
        //}

        //public static bool DeleteFromCache(string key)
        //{
        //    return myRedisClient.Key.KeyDelete(key);
        //}

        //public bool DeleteKeysByPartOfName(string pattern)
        //{
        //    bool result = true;
        //    var keysPattern = string.Format("*{0}*", pattern);
        //    /*foreach (var key in server.Keys(pattern: keysPattern))
        //    {
        //        if (!RedisStore.RedisCache.KeyDelete(key))
        //            result = false;
        //    }*/
        //    return result;
        //}

        /*public static void ClearCache()
        {
            server.FlushDatabase();
        }*/


    }
}
