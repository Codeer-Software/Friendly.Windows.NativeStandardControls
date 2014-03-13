#include "stdafx.h"
#include "NativeControls.h"
#include "IPAdressTestDlg.h"
#include "afxdialogex.h"

IMPLEMENT_DYNAMIC(CIPAdressTestDlg, CCommandAndNotifyCheckDlg)

CIPAdressTestDlg::CIPAdressTestDlg(CWnd* pParent /*=NULL*/)
	: CCommandAndNotifyCheckDlg(CIPAdressTestDlg::IDD, pParent)
{
	m_msgBoxIdAndNotify[IDC_IPADDRESS_ASYNC].insert(IPN_FIELDCHANGED);
}

CIPAdressTestDlg::~CIPAdressTestDlg()
{
}

void CIPAdressTestDlg::DoDataExchange(CDataExchange* pDX)
{
	CCommandAndNotifyCheckDlg::DoDataExchange(pDX);
}

BEGIN_MESSAGE_MAP(CIPAdressTestDlg, CCommandAndNotifyCheckDlg)
	ON_NOTIFY(IPN_FIELDCHANGED, IDC_IPADDRESS_SYNC, &CIPAdressTestDlg::OnIpnFieldchangedIpaddressSync)
END_MESSAGE_MAP()

void CIPAdressTestDlg::OnIpnFieldchangedIpaddressSync(NMHDR *pNMHDR, LRESULT *pResult)
{
	LPNMIPADDRESS pIPAddr = reinterpret_cast<LPNMIPADDRESS>(pNMHDR);
	m_notify.push_back(*pIPAddr);
	*pResult = 0;
}

void CIPAdressTestDlg::ClearCodeInfo()
{
	__super::ClearCodeInfo();
	m_notify.clear();
}

extern "C"
{
	__declspec(dllexport) int __cdecl GetNMIPADDRESS(HWND hDlg, int arrayCount, NMIPADDRESS* pArray)
	{
		CIPAdressTestDlg* pDlg = dynamic_cast<CIPAdressTestDlg*>(CWnd::FromHandle(hDlg));
		if (pDlg == NULL || arrayCount < (int)pDlg->m_notify.size()) {
			return -1;
		}
		for (size_t i = 0; i < pDlg->m_notify.size(); i++) {
			pArray[i] = pDlg->m_notify[i];
		}
		return (int)pDlg->m_notify.size();
	}
}
