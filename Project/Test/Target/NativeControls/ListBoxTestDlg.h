#pragma once
#include "afxwin.h"
#include "CommandAndNotifyCheckDlg.h"

class CListBoxTestDlg : public CCommandAndNotifyCheckDlg
{
	DECLARE_DYNAMIC(CListBoxTestDlg)

public:
	CListBoxTestDlg(CWnd* pParent = NULL);
	virtual ~CListBoxTestDlg();
	enum { IDD = IDD_DIALOG_LIST };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);
	DECLARE_MESSAGE_MAP()

public:
	BOOL OnInitDialog();

public:
	CListBox m_sync;
	CListBox m_syncMulti;
	CListBox m_async;
	CListBox m_asyncMulti;
};
