
using RocksDbSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBHelper
{
    public class RocksDBHelper
    {
        private readonly RocksDb rocksdb;

        public RocksDBHelper(string path)
        {

            rocksdb = RocksDb.Open(new DbOptions()
    .SetCreateIfMissing(true), path);

        }

        public void Put(string key, string value)
        {
            var bkey = Encoding.UTF8.GetBytes(key);
            var bvalue = Encoding.UTF8.GetBytes(value);
            rocksdb.Put(bkey, bvalue);
        }

        public string Get(string key)
        {
            var bkey = Encoding.UTF8.GetBytes(key);
            var bvalue = rocksdb.Get(bkey);
            return Encoding.UTF8.GetString(bvalue);
        }

        public void Write(KeyValuePair<string, string>[] kvs)
        {
            var batch = new WriteBatch();
            foreach (var item in kvs)
            {
                var bkey = Encoding.UTF8.GetBytes(item.Key);
                var bvalue = Encoding.UTF8.GetBytes(item.Value);
                batch.Put(bkey, bvalue);
            }
            rocksdb.Write(batch);
        }


    }
}
