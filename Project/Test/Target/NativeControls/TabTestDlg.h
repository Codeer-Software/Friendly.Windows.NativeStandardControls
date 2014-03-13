#pragma once
#include "afxcmn.h"
#include "CommandAndNotifyCheckDlg.h"

class CTabTestDlg : public CCommandAndNotifyCheckDlg
{
	DECLARE_DYNAMIC(CTabTestDlg)

public:
	CTabTestDlg(CWnd* pParent = NULL);
	virtual ~CTabTestDlg();
	enum { IDD = IDD_DIALOG_TAB };

protected:
	virtual void DoDataExchange(CDataExchange* pDX); 
	DECLARE_MESSAGE_MAP()

public:
	BOOL OnInitDialog();

public:
	CTabCtrl m_sync;
	CTabCtrl m_async;
	CImageList m_image;
};
