#pragma once
#include "afxwin.h"
#include "CommandAndNotifyCheckDlg.h"

class CScrollTestDlg : public CCommandAndNotifyCheckDlg
{
	DECLARE_DYNAMIC(CScrollTestDlg)

public:
	CScrollTestDlg(CWnd* pParent = NULL);
	virtual ~CScrollTestDlg();
	enum { IDD = IDD_DIALOG_SCROLL };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);
	DECLARE_MESSAGE_MAP()

public:
	BOOL OnInitDialog();
	void OnHScroll(UINT nSBCode, UINT nPos, CScrollBar* pScrollBar);
	void OnVScroll(UINT nSBCode, UINT nPos, CScrollBar* pScrollBar);

	CScrollBar m_HAsync;
	CScrollBar m_HSync;
	CScrollBar m_VSync;
};
