using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyCSharp.DAL;
using MyCSharp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCSharp.DAL.Tests
{
    [TestClass()]
    public class DALBaseTests
    {
        [TestMethod()]
        public void BatchInsertTest()
        {
            var players = new List<Player>();

            players.Add(new Player
            {
                Birthday = null,//new DateTime(1993, 1, 24),
                Comments = null,
                Gender = null,
                IsSingle = null,
                Name = null,
                RoleType = null
            });

            for (int i = 0; i < 10000; i++)
            {
                players.Add(new Player
                {
                    Birthday = new DateTime(1993, 1, 24),
                    Comments = "自然拥有无限可能",
                    Gender = "男",
                    IsSingle = true,
                    Name = "小明",
                    RoleType = RoleType.Type2
                });
            }

            var dal = new DALBase();

            Stopwatch sw = new Stopwatch();
            sw.Start();
            dal.BatchInsert(players);
            sw.Stop();
            Console.WriteLine($"Insert Time: {sw.ElapsedMilliseconds} ms");

        }

        [TestMethod()]
        public void BatchUpdateTest()
        {
            var dal = new DALBase();
            var players = dal.context.Player.Take(10000).ToList();
            foreach (var item in players)
            {
                item.Name = "小月";
            }

            Stopwatch sw = new Stopwatch();
            sw.Start();
            dal.context.SaveChanges();
            //dal.BatchUpdate(players);
            sw.Stop();
            Console.WriteLine($"Update Time: {sw.ElapsedMilliseconds} ms");
        }

        [TestMethod()]
        public void BatchDeleteTest()
        {
            //var dal = new DALBase();
            //var players = dal.context.Player.Take(2).ToList();
            //dal.BatchDelete(players);
        }
    }
}