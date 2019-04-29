using Neo.IO.Data.LevelDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBHelper
{
    public class LevelDBHelper
    {
        private readonly DB leveldb;

        public LevelDBHelper(string path)
        {
            leveldb = DB.Open(path, new Options { CreateIfMissing = true });
        }

        public void Put(string key, string value)
        {
            var bkey = Encoding.UTF8.GetBytes(key);
            var bvalue = Encoding.UTF8.GetBytes(value);
            leveldb.Put(WriteOptions.Default, bkey, bvalue);
        }

        public string Get(string key)
        {
            var bkey = Encoding.UTF8.GetBytes(key);
            var bvalue = leveldb.Get(ReadOptions.Default, bkey).ToArray();
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
            leveldb.Write(WriteOptions.Default, batch);
        }


    }
}
