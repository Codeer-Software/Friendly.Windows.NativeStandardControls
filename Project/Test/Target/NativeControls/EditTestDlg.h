#pragma once
#include "CommandAndNotifyCheckDlg.h"
#include "afxcmn.h"

class CEditTestDlg : public CCommandAndNotifyCheckDlg
{
	DECLARE_DYNAMIC(CEditTestDlg)

public:
	CEditTestDlg(CWnd* pParent = NULL);
	virtual ~CEditTestDlg();
	enum { IDD = IDD_DIALOG_EDIT };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);
	DECLARE_MESSAGE_MAP()

public:
	BOOL OnInitDialog();
	CRichEditCtrl m_richSync;
	CRichEditCtrl m_richAsync;
};
