using System.Collections.Generic;

namespace Codeer.Friendly.Windows.NativeStandardControls.Generator.Hook
{
    /// <summary>
    /// フック管理。
    /// </summary>
    public static class ThreadMessageHookManager<T> where T : HookBase, new()
    {
        static Dictionary<int, T> threadAndHookHandle = new Dictionary<int, T>();

        /// <summary>
        /// 登録。
        /// </summary>
        /// <param name="threadId">スレッドID。</param>
        /// <param name="proc">メッセージ解析メソッド。</param>
        public static void Entry(int threadId, AnalyzeMessage proc)
        {
            lock (threadAndHookHandle)
            {
                T hook;
                if (!threadAndHookHandle.TryGetValue(threadId, out hook))
                {
                    hook = new T();
                    hook.Init(threadId);
                    threadAndHookHandle.Add(threadId, hook);
                }
                hook.Entry(proc);
            }
        }

        /// <summary>
        /// 削除。
        /// </summary>
        /// <param name="threadId">スレッドID。</param>
        /// <param name="proc">メッセージ解析メソッド。</param>
        public static void Remove(int threadId, AnalyzeMessage proc)
        {
            lock (threadAndHookHandle)
            {
                T hook;
                if (threadAndHookHandle.TryGetValue(threadId, out hook))
                {
                    hook.Remove(proc);
                    if (hook.Empty)
                    {
                        threadAndHookHandle.Remove(threadId);
                    }
                }
            }
        }
    }
}
