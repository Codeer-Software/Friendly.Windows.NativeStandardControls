#pragma once
#include "afxcmn.h"
#include "CommandAndNotifyCheckDlg.h"

class CSpinTestDlg : public CCommandAndNotifyCheckDlg
{
	DECLARE_DYNAMIC(CSpinTestDlg)

public:
	CSpinTestDlg(CWnd* pParent = NULL);
	virtual ~CSpinTestDlg();
	enum { IDD = IDD_DIALOG_SPIN };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);  
	DECLARE_MESSAGE_MAP()

public:
	BOOL OnInitDialog();

public:
	CSpinButtonCtrl m_sync;
	CSpinButtonCtrl m_async;
	afx_msg void OnDeltaposSpinSync(NMHDR *pNMHDR, LRESULT *pResult);
	void ClearCodeInfo();
	std::vector<NMUPDOWN> m_notify;
};
