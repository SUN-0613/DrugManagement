using AYam.Common.Data.File;
using AYam.Common.Data.List;
using DrugManagement.Data.Info;
using DrugManagement.Data.Path;
using System;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DrugManagement.Data.File
{

    /// <summary>
    /// 設定ファイル
    /// </summary>
    public class SettingFile : XmlDataFile
    {

        /// <summary>
        /// 朝食時間範囲
        /// </summary>
        public BetweenTime Breakfast;

        /// <summary>
        /// 昼食時間範囲
        /// </summary>
        public BetweenTime Lunch;

        /// <summary>
        /// 夕食時間範囲
        /// </summary>
        public BetweenTime Dinner;

        /// <summary>
        /// 就寝時間
        /// </summary>
        public TimeSpan Sleep;

        /// <summary>
        /// 食前時間
        /// </summary>
        public TimeSpan BeforeMeals;

        /// <summary>
        /// 食後時間
        /// </summary>
        public TimeSpan AfterMeals;

        /// <summary>
        /// 就寝前時間
        /// </summary>
        public TimeSpan BeforeSleep;

        /// <summary>
        /// 再通知時間
        /// </summary>
        public TimeSpan Realarm;

        /// <summary>
        /// 設定ファイル
        /// </summary>
        public SettingFile() : base(new PathInfo().Files.Setting, "Setting")
        { }

        /// <summary>
        /// 終了処理
        /// </summary>
        public override void Dispose()
        { }

        /// <summary>
        /// ファイル読込
        /// </summary>
        public override async void Read()
        {

            await Task.Run(() => 
            {

                if (Element != null)
                {

                    var element = Element.Element(nameof(Breakfast));

                    Breakfast = new BetweenTime()
                    {
                        Start = GetValue(element, nameof(BetweenTime.Start), Breakfast.Start),
                        Finish = GetValue(element, nameof(BetweenTime.Finish), Breakfast.Finish)
                    };

                    element = Element.Element(nameof(Lunch));

                    Lunch = new BetweenTime()
                    {
                        Start = GetValue(element, nameof(BetweenTime.Start), Lunch.Start),
                        Finish = GetValue(element, nameof(BetweenTime.Finish), Lunch.Finish)
                    };

                    element = Element.Element(nameof(Dinner));

                    Dinner = new BetweenTime()
                    {
                        Start = GetValue(element, nameof(BetweenTime.Start), Dinner.Start),
                        Finish = GetValue(element, nameof(BetweenTime.Finish), Dinner.Finish)
                    };

                    Sleep = GetValue(nameof(Sleep), Sleep);
                    BeforeMeals = GetValue(nameof(BeforeMeals), BeforeMeals);
                    AfterMeals = GetValue(nameof(AfterMeals), AfterMeals);
                    BeforeSleep = GetValue(nameof(BeforeSleep), BeforeSleep);
                    Realarm = GetValue(nameof(Realarm), Realarm);

                }
                else
                {

                    Breakfast = new BetweenTime()
                    {
                        Start = new TimeSpan(6, 0, 0),
                        Finish = new TimeSpan(6, 30, 0)
                    };

                    Lunch = new BetweenTime()
                    {
                        Start = new TimeSpan(12, 0, 0),
                        Finish = new TimeSpan(12, 30, 0)
                    };

                    Dinner = new BetweenTime()
                    {
                        Start = new TimeSpan(18, 0, 0),
                        Finish = new TimeSpan(18, 30, 0)
                    };

                    Sleep = new TimeSpan(23, 0, 0);

                    BeforeMeals = new TimeSpan(0, 30, 0);
                    AfterMeals = new TimeSpan(0, 30, 0);
                    BeforeSleep = new TimeSpan(0, 30, 0);
                    Realarm = new TimeSpan(0, 30, 0);

                }

            });

        }

        /// <summary>
        /// ファイル保存
        /// </summary>
        public override async void Save()
        {

            await Task.Run(() => 
            {

                var breakfast = new XElement(nameof(Breakfast));
                AddElement(ref breakfast, new XElement(nameof(BetweenTime.Start), Breakfast.Start));
                AddElement(ref breakfast, new XElement(nameof(BetweenTime.Finish), Breakfast.Finish));

                var lunch = new XElement(nameof(Lunch));
                AddElement(ref lunch, new XElement(nameof(BetweenTime.Start), Lunch.Start));
                AddElement(ref lunch, new XElement(nameof(BetweenTime.Finish), Lunch.Finish));

                var dinner = new XElement(nameof(Dinner));
                AddElement(ref dinner, new XElement(nameof(BetweenTime.Start), Dinner.Start));
                AddElement(ref dinner, new XElement(nameof(BetweenTime.Finish), Dinner.Finish));

                using (var elements = new List<XElement>
            {
                breakfast,
                lunch,
                dinner,
                new XElement(nameof(Sleep), Sleep),
                new XElement(nameof(BeforeMeals), BeforeMeals),
                new XElement(nameof(AfterMeals), AfterMeals),
                new XElement(nameof(BeforeSleep), BeforeSleep),
                new XElement(nameof(Realarm), Realarm)
            })
                {

                    WriteFile(elements);

                }

            });

        }
    }

}
