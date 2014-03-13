#pragma once
#include "CommandAndNotifyCheckDlg.h"

class CIPAdressTestDlg : public CCommandAndNotifyCheckDlg
{
	DECLARE_DYNAMIC(CIPAdressTestDlg)

public:
	CIPAdressTestDlg(CWnd* pParent = NULL);
	virtual ~CIPAdressTestDlg();
	enum { IDD = IDD_IPADRESS };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);
	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnIpnFieldchangedIpaddressSync(NMHDR *pNMHDR, LRESULT *pResult);
	void ClearCodeInfo();
	std::vector<NMIPADDRESS> m_notify;
};
