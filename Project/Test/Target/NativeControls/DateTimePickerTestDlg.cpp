#include "stdafx.h"
#include "NativeControls.h"
#include "DateTimePickerTestDlg.h"
#include "afxdialogex.h"

IMPLEMENT_DYNAMIC(CDateTimePickerTestDlg, CCommandAndNotifyCheckDlg)

CDateTimePickerTestDlg::CDateTimePickerTestDlg(CWnd* pParent /*=NULL*/)
	: CCommandAndNotifyCheckDlg(CDateTimePickerTestDlg::IDD, pParent)
{
	m_msgBoxIdAndNotify[IDC_DATETIMEPICKER_ASYNC].insert(DTN_DATETIMECHANGE);
}

CDateTimePickerTestDlg::~CDateTimePickerTestDlg()
{
}

void CDateTimePickerTestDlg::DoDataExchange(CDataExchange* pDX)
{
	CCommandAndNotifyCheckDlg::DoDataExchange(pDX);
}

BEGIN_MESSAGE_MAP(CDateTimePickerTestDlg, CCommandAndNotifyCheckDlg)
	ON_NOTIFY(DTN_DATETIMECHANGE, IDC_DATETIMEPICKER_SYNC, &CDateTimePickerTestDlg::OnDtnDatetimechangeDatetimepickerSync)
END_MESSAGE_MAP()


void CDateTimePickerTestDlg::OnDtnDatetimechangeDatetimepickerSync(NMHDR *pNMHDR, LRESULT *pResult)
{
	LPNMDATETIMECHANGE pDTChange = reinterpret_cast<LPNMDATETIMECHANGE>(pNMHDR);
	m_notify.push_back(*pDTChange);
	*pResult = 0;
}

void CDateTimePickerTestDlg::ClearCodeInfo()
{
	__super::ClearCodeInfo();
	m_notify.clear();
}

extern "C"
{
	__declspec(dllexport) int __cdecl GetNMDATETIMECHANGE(HWND hDlg, int arrayCount, NMDATETIMECHANGE* pArray)
	{
		CDateTimePickerTestDlg* pDlg = dynamic_cast<CDateTimePickerTestDlg*>(CWnd::FromHandle(hDlg));
		if (pDlg == NULL || arrayCount < (int)pDlg->m_notify.size()) {
			return -1;
		}
		for (size_t i = 0; i < pDlg->m_notify.size(); i++) {
			pArray[i] = pDlg->m_notify[i];
		}
		return (int)pDlg->m_notify.size();
	}
}
