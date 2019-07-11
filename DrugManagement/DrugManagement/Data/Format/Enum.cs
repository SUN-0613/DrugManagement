namespace DrugManagement.Data.Format
{

    /// <summary>
    /// 服用タイミング
    /// </summary>
    public enum Kind
    {
        /// <summary>
        /// 指定なし
        /// </summary>
        None,
        /// <summary>
        /// 食前
        /// </summary>
        Before,
        /// <summary>
        /// 食間
        /// </summary>
        Between,
        /// <summary>
        /// 食後
        /// </summary>
        After,
        /// <summary>
        /// 指定日時
        /// </summary>
        Appoint,
        /// <summary>
        /// 時間ごと
        /// </summary>
        TimeEach
    }

}
