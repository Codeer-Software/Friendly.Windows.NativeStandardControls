#pragma once
#include "CommandAndNotifyCheckDlg.h"
#include "afxwin.h"
#include "afxcmn.h"

class CComboTestDlg : public CCommandAndNotifyCheckDlg
{
	DECLARE_DYNAMIC(CComboTestDlg)

public:
	CComboTestDlg(CWnd* pParent = NULL);
	virtual ~CComboTestDlg();
	enum { IDD = IDD_COMBO_TEST };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);

	DECLARE_MESSAGE_MAP()
	
public:
	BOOL OnInitDialog();

private:
	CComboBox m_sync;
	CComboBox m_async;
	CComboBoxEx m_exSync;
	CComboBoxEx m_exAsync;
public:
	CComboBoxEx m_dropDownList;
};
