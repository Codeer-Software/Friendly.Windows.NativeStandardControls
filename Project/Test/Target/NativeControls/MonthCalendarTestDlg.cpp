#include "stdafx.h"
#include "NativeControls.h"
#include "MonthCalendarTestDlg.h"
#include "afxdialogex.h"

IMPLEMENT_DYNAMIC(CMonthCalendarTestDlg, CCommandAndNotifyCheckDlg)

CMonthCalendarTestDlg::CMonthCalendarTestDlg(CWnd* pParent /*=NULL*/)
	: CCommandAndNotifyCheckDlg(CMonthCalendarTestDlg::IDD, pParent)
{
	m_msgBoxIdAndNotify[IDC_MONTHCALENDAR_ASYNC].insert(MCN_SELCHANGE);
	m_msgBoxIdAndNotify[IDC_MONTHCALENDAR_ASYNC].insert(MCN_VIEWCHANGE);
	m_msgBoxIdAndNotify[IDC_MONTHCALENDAR_ASYNC_MULTI].insert(MCN_SELCHANGE);
	m_msgBoxIdAndNotify[IDC_MONTHCALENDAR_ASYNC_MULTI].insert(MCN_VIEWCHANGE);
}

CMonthCalendarTestDlg::~CMonthCalendarTestDlg(){}

void CMonthCalendarTestDlg::DoDataExchange(CDataExchange* pDX)
{
	__super::DoDataExchange(pDX);
}

BEGIN_MESSAGE_MAP(CMonthCalendarTestDlg, CDialogEx)
	ON_NOTIFY(MCN_SELCHANGE, IDC_MONTHCALENDAR_SYNC, &CMonthCalendarTestDlg::OnMcnSelchangeMonthcalendar1)
	ON_NOTIFY(MCN_SELECT, IDC_MONTHCALENDAR_SYNC, &CMonthCalendarTestDlg::OnMcnSelectMonthcalendar1)
	ON_NOTIFY(MCN_SELCHANGE, IDC_MONTHCALENDAR_SYNC_MULTI, &CMonthCalendarTestDlg::OnMcnSelchangeMonthcalendarSyncMulti)
	ON_NOTIFY(MCN_SELECT, IDC_MONTHCALENDAR_SYNC_MULTI, &CMonthCalendarTestDlg::OnMcnSelectMonthcalendarSyncMulti)
END_MESSAGE_MAP()


void CMonthCalendarTestDlg::OnMcnSelchangeMonthcalendar1(NMHDR *pNMHDR, LRESULT *pResult)
{
	LPNMSELCHANGE pSelChange = reinterpret_cast<LPNMSELCHANGE>(pNMHDR);
	m_notify.push_back(*pSelChange);
	*pResult = 0;
}

void CMonthCalendarTestDlg::OnMcnSelectMonthcalendar1(NMHDR *pNMHDR, LRESULT *pResult)
{
	LPNMSELCHANGE pSelChange = reinterpret_cast<LPNMSELCHANGE>(pNMHDR);
	m_notify.push_back(*pSelChange);
	*pResult = 0;
}

void CMonthCalendarTestDlg::OnMcnSelchangeMonthcalendarSyncMulti(NMHDR *pNMHDR, LRESULT *pResult)
{
	LPNMSELCHANGE pSelChange = reinterpret_cast<LPNMSELCHANGE>(pNMHDR);
	m_notify.push_back(*pSelChange);
	*pResult = 0;
}

void CMonthCalendarTestDlg::OnMcnSelectMonthcalendarSyncMulti(NMHDR *pNMHDR, LRESULT *pResult)
{
	LPNMSELCHANGE pSelChange = reinterpret_cast<LPNMSELCHANGE>(pNMHDR);
	m_notify.push_back(*pSelChange);
	*pResult = 0;
}

void CMonthCalendarTestDlg::ClearCodeInfo()
{
	__super::ClearCodeInfo();
	m_notify.clear();
}

extern "C"
{
	__declspec(dllexport) int __cdecl GetNMSELCHANGE(HWND hDlg, int arrayCount, NMSELCHANGE* pArray)
	{
		CMonthCalendarTestDlg* pDlg = dynamic_cast<CMonthCalendarTestDlg*>(CWnd::FromHandle(hDlg));
		if (pDlg == NULL || arrayCount < (int)pDlg->m_notify.size()) {
			return -1;
		}
		for (size_t i = 0; i < pDlg->m_notify.size(); i++) {
			pArray[i] = pDlg->m_notify[i];
		}
		return (int)pDlg->m_notify.size();
	}
}
