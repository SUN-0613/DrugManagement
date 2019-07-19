namespace DrugManagement.Resx.Language
{

    /// <summary>
    /// 対応言語のテキストをリソースファイルから取得
    /// 服用ページ用
    /// </summary>
    public class Drug : Base.TranslateExtension
    {

        /// <summary>
        /// 対応言語のテキストをリソースファイルから取得
        /// メインメニュー用
        /// </summary>
        public Drug() : base(nameof(DrugManagement) + "." + nameof(Resx) + "." + nameof(Drug))
        { }

    }

}
