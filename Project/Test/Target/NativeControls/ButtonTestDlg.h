#pragma once
#include "CommandAndNotifyCheckDlg.h"
#include "afxwin.h"

class CButtonTestDlg : public CCommandAndNotifyCheckDlg
{
	DECLARE_DYNAMIC(CButtonTestDlg)

public:
	CButtonTestDlg(CWnd* pParent = NULL);
	virtual ~CButtonTestDlg();
	enum { IDD = IDD_BUTTON_TEST };
	BOOL OnInitDialog();

protected:
	virtual void DoDataExchange(CDataExchange* pDX);

	DECLARE_MESSAGE_MAP()
public:
	CButton m_radio1;
};
