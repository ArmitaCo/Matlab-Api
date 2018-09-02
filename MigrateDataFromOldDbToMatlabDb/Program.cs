using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Matlab.DataModel;
using MigrateDataFromOldDbToMatlabDb.OldDb;

namespace MigrateDataFromOldDbToMatlabDb
{
    class Program
    {
        static Matlab.DataModel.MatlabDb _matlabDb = new MatlabDb();
        static HealthMessageEntities _healthMessageEntities = new HealthMessageEntities();
        private static string[] _answerStrings = new[] {"الف", "ب", "ج", "د"};
        static void Main(string[] args)
        {
            var catergories = _matlabDb.Catergories;
            var zones = _healthMessageEntities.tblZones;
            var catergoriesNew = zones.Select(x => new Catergory()
            {
                Description = x.structureMessage,
                Title = x.title,
            }).ToArray();
            catergories.AddOrUpdate(x => x.Title, catergoriesNew);
            _matlabDb.SaveChanges();
            var classes = _healthMessageEntities.tblClasses;
            var zoneToCategory = (from categ in _matlabDb.Catergories.ToList()
                                  join zo in _healthMessageEntities.tblZones.ToList() on categ.Title equals zo.title
                                  join cls in classes on zo.ID equals cls.zoneID
                                  select new tblClass()
                                  {
                                      title = cls.title,
                                      zoneID = (byte)categ.Id
                                  }
                );


            var packages = _matlabDb.Packages;

            //var t = (from cls in classes join zcateg in zoneToCategory on cls.zoneID equals zcateg.zo.ID
            //        select new tblClass()
            //        {
            //            title = cls.title,
            //            zoneID = (byte) zcateg.categ.Id
            //        }
            //        ).ToList();


            var packagesNew = zoneToCategory.Select(x => new Package()
            {
                Title = x.title,
                CategoryId = x.zoneID
            }).ToArray();




            packages.AddOrUpdate(x => x.Title, packagesNew);
            _matlabDb.SaveChanges();
            var classToPackageDictionary = (from pack in _matlabDb.Packages.ToList()
                                            join cls in _healthMessageEntities.tblClasses.ToList() on pack.Title equals cls.title
                                            join zo in _healthMessageEntities.tblZones.ToList() on cls.zoneID equals zo.ID
                                            join categ in _matlabDb.Catergories.ToList() on pack.CategoryId equals categ.Id
                                            select new { pack, cls, zo, categ }).ToList();


            var content = _healthMessageEntities.tblContents;
            foreach (var itemContent in content)
            {
                int packageid = classToPackageDictionary.First(x => x.cls.ID == itemContent.classID).pack.Id;
                string baseQuestion = itemContent.question.Replace("- ", "").Replace("\r\n", "");
                string[] questionParts = baseQuestion.Split(new String[] { "11", "12", "13", "14" },
                    StringSplitOptions.RemoveEmptyEntries);
                string questionText = questionParts[0];

                string answerString = "";

                var answersList = questionParts.Skip(1).Select((x, y) => new Answer()
                {
                    Title = x,
                    ChoiceLable = (ChoiceLable)y + 11,
                    IsCorrect = y.ToString() == itemContent.answer,
                }).ToList();

                for (int i = 0; i < answersList.Count; i++)
                {
                    answerString += $"{_answerStrings[i]}. {answersList[i]}\n";
                }

                Debug.Assert(itemContent.optionCount != null, "itemContent.optionCount != null");
                Article article = new Article()
                {
                    Title = itemContent.context,
                    PackageId = packageid,
                    Order = itemContent.code,
                    Question = new Question()
                    {
                        Title = questionText,
                        CorrectChoiceLable = (ChoiceLable)(int.Parse(itemContent.answer)-11),
                        Answers = answersList,
                        AnswersCount = (int) itemContent.optionCount,
                        AnswersString = answerString,
                    }

                };

                _matlabDb.Articles.Add(article);

            }

            _matlabDb.SaveChanges();
        }
    }
}
