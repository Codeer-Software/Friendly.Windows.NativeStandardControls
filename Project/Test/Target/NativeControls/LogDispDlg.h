#pragma once
#include "afxwin.h"

/**
	@brief	調査用ログ表示ダイアログ。
*/
class CLogDispDlg : public CDialogEx
{
	DECLARE_DYNAMIC(CLogDispDlg)

public:
	CLogDispDlg(CWnd* pParent = NULL);
	virtual ~CLogDispDlg();
	enum { IDD = IDD_LOG_DISP };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);

	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnBnClickedButtonClear();
	void PostNcDestroy();
	static void Trace(LPCSTR szStr);
	static void ShowSingleton();
	void OnCancel();
	CEdit m_edit;
};
