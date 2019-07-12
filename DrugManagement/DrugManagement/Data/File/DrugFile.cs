using AYam.Common.Data.File;
using AYam.Common.Data.List;
using DrugManagement.Data.Info;
using DrugManagement.Data.Path;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DrugManagement.Data.File
{

    /// <summary>
    /// 薬ファイル
    /// </summary>
    public class DrugFile : XmlDataFile
    {

        /// <summary>
        /// 薬一覧
        /// </summary>
        public ObservableCollection<Drug> Drugs { get; set; }

        /// <summary>
        /// 薬関連のXML要素名
        /// </summary>
        private const string _ElementName = "Drug";

        /// <summary>
        /// 薬ファイル
        /// </summary>
        public DrugFile() : base(new PathInfo().Files.Drugs, "Drugs")
        { }

        /// <summary>
        /// 終了処理
        /// </summary>
        public override void Dispose()
        {

            Drugs.Clear();
            Drugs = null;

        }

        /// <summary>
        /// ファイル読込
        /// </summary>
        public override async void Read()
        {

            Drugs = new ObservableCollection<Drug>();

            await Task.Run(() =>
            {

                if (Element != null)
                {

                    foreach (var element in Element.Elements(_ElementName))
                    {

                        var drug = new Drug();

                        drug.CreateDateTime = GetAttribute(element, nameof(Drug.CreateDateTime), drug.CreateDateTime);
                        drug.Name = GetValue(element, nameof(Drug.Name), drug.Name);

                        var child = element.Element(nameof(Drug.Breakfast));
                        drug.Breakfast.IsDrug = GetValue(child, nameof(Timing.IsDrug), drug.Breakfast.IsDrug);
                        drug.Breakfast.Kind = GetValue(child, nameof(Timing.Kind), drug.Breakfast.Kind);
                        drug.Breakfast.Volume = GetValue(child, nameof(Timing.Volume), drug.Breakfast.Volume);

                        child = element.Element(nameof(Drug.Lunch));
                        drug.Lunch.IsDrug = GetValue(child, nameof(Timing.IsDrug), drug.Lunch.IsDrug);
                        drug.Lunch.Kind = GetValue(child, nameof(Timing.Kind), drug.Lunch.Kind);
                        drug.Lunch.Volume = GetValue(child, nameof(Timing.Volume), drug.Lunch.Volume);

                        child = element.Element(nameof(Drug.Dinner));
                        drug.Dinner.IsDrug = GetValue(child, nameof(Timing.IsDrug), drug.Dinner.IsDrug);
                        drug.Dinner.Kind = GetValue(child, nameof(Timing.Kind), drug.Dinner.Kind);
                        drug.Dinner.Volume = GetValue(child, nameof(Timing.Volume), drug.Dinner.Volume);

                        child = element.Element(nameof(Drug.Sleep));
                        drug.Sleep.IsDrug = GetValue(child, nameof(Timing.IsDrug), drug.Sleep.IsDrug);
                        drug.Sleep.Kind = GetValue(child, nameof(Timing.Kind), drug.Sleep.Kind);
                        drug.Sleep.Volume = GetValue(child, nameof(Timing.Volume), drug.Sleep.Volume);

                        child = element.Element(nameof(Drug.ToBeTaken));
                        drug.ToBeTaken.IsDrug = GetValue(child, nameof(Timing.IsDrug), drug.ToBeTaken.IsDrug);
                        drug.ToBeTaken.Kind = GetValue(child, nameof(Timing.Kind), drug.ToBeTaken.Kind);
                        drug.ToBeTaken.Volume = GetValue(child, nameof(Timing.Volume), drug.ToBeTaken.Volume);

                        child = element.Element(nameof(Drug.Appoint));
                        drug.Appoint.IsDrug = GetValue(child, nameof(Timing.IsDrug), drug.Appoint.IsDrug);
                        drug.Appoint.Kind = GetValue(child, nameof(Timing.Kind), drug.Appoint.Kind);
                        drug.Appoint.Volume = GetValue(child, nameof(Timing.Volume), drug.Appoint.Volume);
                        drug.AppointTime = GetAttribute(child, nameof(Drug.AppointTime), drug.AppointTime);

                        child = element.Element(nameof(Drug.HourEach));
                        drug.HourEach.IsDrug = GetValue(child, nameof(Timing.IsDrug), drug.HourEach.IsDrug);
                        drug.HourEach.Kind = GetValue(child, nameof(Timing.Kind), drug.HourEach.Kind);
                        drug.HourEach.Volume = GetValue(child, nameof(Timing.Volume), drug.HourEach.Volume);
                        drug.HourEachTime = GetAttribute(child, nameof(Drug.HourEachTime), drug.HourEachTime);
                        drug.HourEachNextTime = GetAttribute(child, nameof(Drug.HourEachNextTime), drug.HourEachNextTime);

                        drug.TotalVolume = GetValue(element, nameof(Drug.TotalVolume), drug.TotalVolume);
                        drug.PrescriptionAlarmVolume = GetValue(element, nameof(Drug.PrescriptionAlarmVolume), drug.PrescriptionAlarmVolume);
                        drug.Remarks = GetValue(element, nameof(Drug.Remarks), drug.Remarks);

                        drug.MakeDrugTiming();

                        Drugs.Add(drug);

                    }

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

                using (var elements = new List<XElement>())
                {

                    for (int iLoop = 0; iLoop < Drugs.Count; iLoop++)
                    {

                        var drug = Drugs[iLoop];
                        var element = new XElement(_ElementName);

                        AddAttribute(ref element, new XAttribute(nameof(Drug.CreateDateTime), drug.CreateDateTime));
                        AddElement(ref element, new XElement(nameof(Drug.Name), drug.Name));

                        var child = new XElement(nameof(Drug.Breakfast));
                        AddElement(ref child, new XElement(nameof(Timing.IsDrug), drug.Breakfast.IsDrug));
                        AddElement(ref child, new XElement(nameof(Timing.Kind), drug.Breakfast.Kind));
                        AddElement(ref child, new XElement(nameof(Timing.Volume), drug.Breakfast.Volume));
                        AddElement(ref element, child);

                        child = new XElement(nameof(Drug.Lunch));
                        AddElement(ref child, new XElement(nameof(Timing.IsDrug), drug.Lunch.IsDrug));
                        AddElement(ref child, new XElement(nameof(Timing.Kind), drug.Lunch.Kind));
                        AddElement(ref child, new XElement(nameof(Timing.Volume), drug.Lunch.Volume));
                        AddElement(ref element, child);

                        child = new XElement(nameof(Drug.Dinner));
                        AddElement(ref child, new XElement(nameof(Timing.IsDrug), drug.Dinner.IsDrug));
                        AddElement(ref child, new XElement(nameof(Timing.Kind), drug.Dinner.Kind));
                        AddElement(ref child, new XElement(nameof(Timing.Volume), drug.Dinner.Volume));
                        AddElement(ref element, child);

                        child = new XElement(nameof(Drug.Sleep));
                        AddElement(ref child, new XElement(nameof(Timing.IsDrug), drug.Sleep.IsDrug));
                        AddElement(ref child, new XElement(nameof(Timing.Kind), drug.Sleep.Kind));
                        AddElement(ref child, new XElement(nameof(Timing.Volume), drug.Sleep.Volume));
                        AddElement(ref element, child);

                        child = new XElement(nameof(Drug.ToBeTaken));
                        AddElement(ref child, new XElement(nameof(Timing.IsDrug), drug.ToBeTaken.IsDrug));
                        AddElement(ref child, new XElement(nameof(Timing.Kind), drug.ToBeTaken.Kind));
                        AddElement(ref child, new XElement(nameof(Timing.Volume), drug.ToBeTaken.Volume));
                        AddElement(ref element, child);

                        child = new XElement(nameof(Drug.Appoint));
                        AddElement(ref child, new XElement(nameof(Timing.IsDrug), drug.Appoint.IsDrug));
                        AddElement(ref child, new XElement(nameof(Timing.Kind), drug.Appoint.Kind));
                        AddElement(ref child, new XElement(nameof(Timing.Volume), drug.Appoint.Volume));
                        AddAttribute(ref child, new XAttribute(nameof(Drug.AppointTime), drug.AppointTime));
                        AddElement(ref element, child);

                        child = new XElement(nameof(Drug.HourEach));
                        AddElement(ref child, new XElement(nameof(Timing.IsDrug), drug.HourEach.IsDrug));
                        AddElement(ref child, new XElement(nameof(Timing.Kind), drug.HourEach.Kind));
                        AddElement(ref child, new XElement(nameof(Timing.Volume), drug.HourEach.Volume));
                        AddAttribute(ref child, new XAttribute(nameof(Drug.HourEachTime), drug.HourEachTime));
                        AddAttribute(ref child, new XAttribute(nameof(Drug.HourEachNextTime), drug.HourEachNextTime));
                        AddElement(ref element, child);

                        AddElement(ref element, new XElement(nameof(Drug.TotalVolume), drug.TotalVolume));
                        AddElement(ref element, new XElement(nameof(Drug.PrescriptionAlarmVolume), drug.PrescriptionAlarmVolume));
                        AddElement(ref element, new XElement(nameof(Drug.Remarks), drug.Remarks));

                        elements.Add(element);

                    }

                    WriteFile(elements);

                }

            });

        }

    }

}
