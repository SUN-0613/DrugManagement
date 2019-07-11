using ComMethod = AYam.Common.Method;
using System;

namespace DrugManagement.Data.Path
{

    /// <summary>
    /// パス情報
    /// </summary>
    public class PathInfo
    {

        /// <summary>
        /// ファイル一覧
        /// </summary>
        public readonly FileInfo Files;

        /// <summary>
        /// パス情報
        /// </summary>
        public PathInfo()
        {
            Files = new FileInfo();
        }

        #region Class

        /// <summary>
        /// ファイル情報
        /// </summary>
        public class FileInfo
        {

            /// <summary>
            /// フォルダ一覧
            /// </summary>
            private readonly DirectoryInfo _Directories;

            /// <summary>
            /// 設定
            /// </summary>
            public readonly string Setting;

            /// <summary>
            /// 薬一覧
            /// </summary>
            public readonly string Drugs;

            /// <summary>
            /// ファイル情報
            /// </summary>
            public FileInfo()
            {

                _Directories = new DirectoryInfo();

                Setting = ComMethod::Path.GetFullPath(_Directories.RootPath, "Setting.xml");
                Drugs = ComMethod::Path.GetFullPath(_Directories.RootPath, "Drugs.xml");

            }

        }

        /// <summary>
        /// フォルダ情報
        /// </summary>
        public class DirectoryInfo
        {

            /// <summary>
            /// ルートフォルダ
            /// </summary>
            public readonly string RootPath;

            /// <summary>
            /// フォルダ情報
            /// </summary>
            public DirectoryInfo()
            {

                RootPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            }

        }

        #endregion

    }

}
