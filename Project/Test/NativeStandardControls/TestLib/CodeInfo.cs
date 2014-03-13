using System;
using System.Runtime.InteropServices;

namespace NativeStandardControls.TestLib
{
    /// <summary>
    /// 通知コード情報。
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    struct CodeInfo
    {
        /// <summary>
        /// ダイアログ。
        /// </summary>
        internal int dialogId;

        /// <summary>
        /// メッセージ。
        /// </summary>
        internal int message;

        /// <summary>
        /// 通知コード。
        /// </summary>
        internal int code;

        //スクロールバーコード
        internal int nSBCode;

        //スクロール位置
        internal int nPos;

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="dialogId_">ダイアログID。</param>
        /// <param name="message_">メッセージ。</param>
        /// <param name="code_">通知コード。</param>
        internal CodeInfo(int dialogId_, int message_, int code_)
        {
            dialogId = dialogId_;
            message = message_;
            code = code_;
            nSBCode = 0;
            nPos = 0;
        }

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="dialogId_">ダイアログID。</param>
        /// <param name="message_">メッセージ。</param>
        /// <param name="nSBCode_">スクロールバーコード。</param>
        /// <param name="nPos_">スクロール位置。</param>
        internal CodeInfo(int dialogId_, int message_, int nSBCode_, int nPos_)
        {
            dialogId = dialogId_;
            message = message_;
            code = 0;
            nSBCode = nSBCode_;
            nPos = nPos_;
        }

        /// <summary>
        /// 等価比較。
        /// </summary>
        /// <param name="obj">比較対象。</param>
        /// <returns>比較結果。</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is CodeInfo))
            {
                return false;
            }
            CodeInfo target = (CodeInfo)obj;
            return dialogId == target.dialogId &&
                message == target.message &&
                code == target.code;
        }

        /// <summary>
        /// ハッシュコード取得。
        /// </summary>
        /// <returns>ハッシュコード。</returns>
        public override int GetHashCode()
        {
            return dialogId + (int)message + code;
        }
    }
}
