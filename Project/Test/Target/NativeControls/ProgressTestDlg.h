#pragma once
#include "afxcmn.h"

class CProgressTestDlg : public CDialogEx
{
	DECLARE_DYNAMIC(CProgressTestDlg)

public:
	CProgressTestDlg(CWnd* pParent = NULL);
	virtual ~CProgressTestDlg();
	enum { IDD = IDD_DIALOG_PROGRESS };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);
	DECLARE_MESSAGE_MAP()

public:
	BOOL OnInitDialog();
	CProgressCtrl m_progress;
};
