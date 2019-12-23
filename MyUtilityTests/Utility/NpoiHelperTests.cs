using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyUtility.Models;
using MyUtility.Utility;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyUtility.Utility.Tests
{

    [TestClass()]
    public class NpoiHelperTests
    {
        public const string testFolder = "TestData";


        [TestMethod()]
        public void ExportListTest()
        {

            var players = new List<Player>();
            var img = Path.Combine(testFolder, "123.jpg");

            for (int i = 0; i < 1000; i++)
            {
                players.Add(new Player
                {
                    Id = 1,
                    Birthday = DateTime.Now,
                    Comments = "",
                    PlayerImg = File.ReadAllBytes(img)
                });
            }

            ExcelTemplate template = new ExcelTemplate
            {
                Columns = new List<ExcelTemplateColumn>
                {
                    new ExcelTemplateColumn{
                        ColumnName ="Id",
                        ColumnTitle ="Id",
                    },
                    new ExcelTemplateColumn{
                        ColumnName ="Birthday",
                        ColumnTitle ="生日",
                        Width = 20
                    },
                    new ExcelTemplateColumn{
                        ColumnName ="Comments",
                        ColumnTitle ="注释",
                    },
                    new ExcelTemplateColumn{
                        ColumnName ="PlayerImg",
                        ColumnTitle ="图片",
                        Format =PictureType.JPEG.ToString(),
                        //Height = 50,
                        Width = 10
                    },
                },
                TemplateID = "Player",
                TemplateName = "M1"
            };

            string file = Path.Combine(testFolder, "123.xlsx");
            IWorkbook workbook = new XSSFWorkbook(); //WorkbookFactory.Create();
            ISheet sheet = workbook.CreateSheet("Test");

            NpoiHelper.ExportList(players, sheet, template);

            //保存为Excel文件  
            using (FileStream fs = new FileStream(file, FileMode.Create, FileAccess.Write))
            {
                workbook.Write(fs);
            }

            //Assert.Fail();
        }

    }
}