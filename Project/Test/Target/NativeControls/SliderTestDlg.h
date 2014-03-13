#pragma once
#include "afxcmn.h"
#include "CommandAndNotifyCheckDlg.h"

class CSliderTestDlg : public CCommandAndNotifyCheckDlg
{
	DECLARE_DYNAMIC(CSliderTestDlg)

public:
	CSliderTestDlg(CWnd* pParent = NULL);
	virtual ~CSliderTestDlg();
	enum { IDD = IDD_DIALOG_SLIDER };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);
	DECLARE_MESSAGE_MAP()
	
public:
	BOOL OnInitDialog();

public:
	CSliderCtrl m_sync;
	CSliderCtrl m_async;
	CSliderCtrl m_v;
	afx_msg void OnTRBNThumbPosChangingSliderSync(NMHDR *pNMHDR, LRESULT *pResult);
	void ClearCodeInfo();
	std::vector<NMTRBTHUMBPOSCHANGING> m_notify;
};
