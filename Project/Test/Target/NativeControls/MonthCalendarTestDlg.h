#pragma once
#include "CommandAndNotifyCheckDlg.h"

class CMonthCalendarTestDlg : public CCommandAndNotifyCheckDlg
{
	DECLARE_DYNAMIC(CMonthCalendarTestDlg)

public:
	CMonthCalendarTestDlg(CWnd* pParent = NULL);
	virtual ~CMonthCalendarTestDlg();
	enum { IDD = IDD_DIALOG_MONTHCALENDER };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);
	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnMcnSelchangeMonthcalendar1(NMHDR *pNMHDR, LRESULT *pResult);
	afx_msg void OnMcnSelectMonthcalendar1(NMHDR *pNMHDR, LRESULT *pResult);
	void ClearCodeInfo();
	std::vector<NMSELCHANGE> m_notify;
	afx_msg void OnMcnSelchangeMonthcalendarSyncMulti(NMHDR *pNMHDR, LRESULT *pResult);
	afx_msg void OnMcnSelectMonthcalendarSyncMulti(NMHDR *pNMHDR, LRESULT *pResult);
};
