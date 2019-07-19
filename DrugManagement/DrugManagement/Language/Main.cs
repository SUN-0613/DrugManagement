namespace DrugManagement.Language
{

    /// <summary>
    /// 対応言語のテキストをリソースファイルから取得
    /// メインメニュー用
    /// </summary>
    public class Main : Base.TranslateExtension
    {

        /// <summary>
        /// 対応言語のテキストをリソースファイルから取得
        /// メインメニュー用
        /// </summary>
        public Main() : base(nameof(DrugManagement) + "." + nameof(Resx) + "." + nameof(Main))
        { }

    }
}
