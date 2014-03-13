#include "stdafx.h"
#include "NativeControls.h"
#include "SpinTestDlg.h"
#include "afxdialogex.h"

IMPLEMENT_DYNAMIC(CSpinTestDlg, CCommandAndNotifyCheckDlg)

CSpinTestDlg::CSpinTestDlg(CWnd* pParent /*=NULL*/)
	: CCommandAndNotifyCheckDlg(CSpinTestDlg::IDD, pParent)
{
	m_msgBoxIdAndNotify[IDC_SPIN_ASYNC].insert(UDN_DELTAPOS);
}

CSpinTestDlg::~CSpinTestDlg()
{
}

void CSpinTestDlg::DoDataExchange(CDataExchange* pDX)
{
	CCommandAndNotifyCheckDlg::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_SPIN_SYNC, m_sync);
	DDX_Control(pDX, IDC_SPIN_ASYNC, m_async);
}

BEGIN_MESSAGE_MAP(CSpinTestDlg, CCommandAndNotifyCheckDlg)
	ON_NOTIFY(UDN_DELTAPOS, IDC_SPIN_SYNC, &CSpinTestDlg::OnDeltaposSpinSync)
END_MESSAGE_MAP()

BOOL CSpinTestDlg::OnInitDialog()
{
	__super::OnInitDialog();
	m_sync.SetRange32(1, 1000);
	m_async.SetRange32(0, 1000);
	return TRUE;
}

void CSpinTestDlg::OnDeltaposSpinSync(NMHDR *pNMHDR, LRESULT *pResult)
{
	LPNMUPDOWN pNMUpDown = reinterpret_cast<LPNMUPDOWN>(pNMHDR);
	m_notify.push_back(*pNMUpDown);
	*pResult = 0;
}

void CSpinTestDlg::ClearCodeInfo()
{
	__super::ClearCodeInfo();
	m_notify.clear();
}

extern "C"
{
	__declspec(dllexport) int __cdecl GetNMUPDOWN(HWND hDlg, int arrayCount, NMUPDOWN* pArray)
	{
		CSpinTestDlg* pDlg = dynamic_cast<CSpinTestDlg*>(CWnd::FromHandle(hDlg));
		if (pDlg == NULL || arrayCount < (int)pDlg->m_notify.size()) {
			return -1;
		}
		for (size_t i = 0; i < pDlg->m_notify.size(); i++) {
			pArray[i] = pDlg->m_notify[i];
		}
		return (int)pDlg->m_notify.size();
	}
}
