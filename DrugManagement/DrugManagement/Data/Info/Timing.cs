using DrugManagement.Data.Format;

namespace DrugManagement.Data.Info
{

    /// <summary>
    /// 服用タイミング情報
    /// </summary>
    public class Timing
    {

        /// <summary>
        /// 服用するか
        /// </summary>
        public bool IsDrug { get; set; } = false;

        /// <summary>
        /// 服用時間
        /// </summary>
        public Kind Kind { get; set; } = Kind.None;

        /// <summary>
        /// 服用錠数
        /// </summary>
        public int Volume { get; set; } = 0;

        /// <summary>
        /// 服用タイミング情報
        /// </summary>
        public Timing()
        { }

    }

}
