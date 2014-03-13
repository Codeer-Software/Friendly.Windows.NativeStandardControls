#pragma once
#include "afxcmn.h"
#include "CommandAndNotifyCheckDlg.h"

class CListCtrlTestDlg : public CCommandAndNotifyCheckDlg
{
	DECLARE_DYNAMIC(CListCtrlTestDlg)

public:
	CListCtrlTestDlg(CWnd* pParent = NULL);
	virtual ~CListCtrlTestDlg();
	enum { IDD = IDD_DIALOG_LISTVIEW };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);
	DECLARE_MESSAGE_MAP()
		
public:
	BOOL OnInitDialog();

public:
	CListCtrl m_sync;
	CListCtrl m_async;
	afx_msg void OnBnClickedButtonChangeView();
	afx_msg void OnLvnEndlabeleditListSync(NMHDR *pNMHDR, LRESULT *pResult);
};
