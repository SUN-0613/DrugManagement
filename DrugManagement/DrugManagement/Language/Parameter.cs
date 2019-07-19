namespace DrugManagement.Language
{

    /// <summary>
    /// 対応言語のテキストをリソースファイルから取得
    /// パラメータ用
    /// </summary>
    public class Parameter : Base.TranslateExtension
    {

        /// <summary>
        /// 対応言語のテキストをリソースファイルから取得
        /// パラメータ用
        /// </summary>
        public Parameter() : base(nameof(DrugManagement) + "." + nameof(Resx) + "." + nameof(Parameter))
        { }

    }
}
