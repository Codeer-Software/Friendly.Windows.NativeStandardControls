#pragma once
#include "CommandAndNotifyCheckDlg.h"

class CDateTimePickerTestDlg : public CCommandAndNotifyCheckDlg
{
	DECLARE_DYNAMIC(CDateTimePickerTestDlg)

public:
	CDateTimePickerTestDlg(CWnd* pParent = NULL);
	virtual ~CDateTimePickerTestDlg();
	enum { IDD = IDD_DATETIME_PICKER };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);
	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnDtnDatetimechangeDatetimepickerSync(NMHDR *pNMHDR, LRESULT *pResult);
	void ClearCodeInfo();
	std::vector<NMDATETIMECHANGE> m_notify;
};
