namespace Codeer.Friendly.Windows.NativeStandardControls
{
#if ENG
    /// <summary>
    /// Represents the display style of a calendar. 
    /// </summary>
#else
    /// <summary>
    /// SysMonthCal32の表示タイプ
    /// </summary>
#endif
    public enum MonthCalendarView
    {
#if ENG
        /// <summary>
        /// Month
        /// </summary>
#else
        /// <summary>
        /// 月
        /// </summary>
#endif
        Month = 0,

#if ENG
        /// <summary>
        /// Year
        /// </summary>
#else
        /// <summary>
        /// 年
        /// </summary>
#endif
        Year = 1,

#if ENG
        /// <summary>
        /// Decade
        /// </summary>
#else
        /// <summary>
        /// 10年
        /// </summary>
#endif
        Decade = 2,

#if ENG
        /// <summary>
        /// Century
        /// </summary>
#else
        /// <summary>
        /// 世紀
        /// </summary>
#endif
        Century = 3,
    }
}
