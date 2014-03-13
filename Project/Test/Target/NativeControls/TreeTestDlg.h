#pragma once
#include "afxcmn.h"
#include "CommandAndNotifyCheckDlg.h"

class CTreeTestDlg : public CCommandAndNotifyCheckDlg
{
	DECLARE_DYNAMIC(CTreeTestDlg)

public:
	CTreeTestDlg(CWnd* pParent = NULL);
	virtual ~CTreeTestDlg();
	enum { IDD = IDD_DIALOG_TREE };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);
	DECLARE_MESSAGE_MAP()
		
public:
	BOOL OnInitDialog();

public:
	CTreeCtrl m_sync;
	CTreeCtrl m_async;
	CImageList m_image;
	afx_msg void OnTvnEndlabeleditTreeSync(NMHDR *pNMHDR, LRESULT *pResult);
	afx_msg void OnTvnItemexpandedTreeSync(NMHDR *pNMHDR, LRESULT *pResult);
	afx_msg void OnTvnItemexpandingTreeSync(NMHDR *pNMHDR, LRESULT *pResult);
	void ClearCodeInfo();
	std::vector<NMTREEVIEW> m_notify;
};
