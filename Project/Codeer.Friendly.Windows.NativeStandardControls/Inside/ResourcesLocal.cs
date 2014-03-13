using System;
using Codeer.Friendly.Windows.NativeStandardControls.Properties;

namespace Codeer.Friendly.Windows.NativeStandardControls.Inside
{
    /// <summary>
    /// ローカライズ済みリソース。
    /// </summary>
    [Serializable]
    class ResourcesLocal
    {
        static internal ResourcesLocal Instance;

        internal string CheckStateIsNotSupported;
        internal string SpinButtonHasNoBuddy;

        /// <summary>
        /// 初期化。
        /// </summary>
        /// <param name="app">アプリケーション操作クラス。</param>
        internal static void Initialize(WindowsAppFriend app)
        {
            Instance = new ResourcesLocal();
            Instance.Initialize();
            app[typeof(ResourcesLocal), "Instance"](Instance);
        }

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        void Initialize()
        {
            CheckStateIsNotSupported = Resources.CheckStateIsNotSupported;
            SpinButtonHasNoBuddy = Resources.SpinButtonHasNoBuddy;
        }
    }
}
