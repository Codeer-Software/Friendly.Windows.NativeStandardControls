#include "stdafx.h"
#include "NativeControls.h"
#include "ListCtrlTestDlg.h"
#include "afxdialogex.h"

IMPLEMENT_DYNAMIC(CListCtrlTestDlg, CCommandAndNotifyCheckDlg)

CListCtrlTestDlg::CListCtrlTestDlg(CWnd* pParent /*=NULL*/)
	: CCommandAndNotifyCheckDlg(CListCtrlTestDlg::IDD, pParent)
{
}

CListCtrlTestDlg::~CListCtrlTestDlg()
{
}

void CListCtrlTestDlg::DoDataExchange(CDataExchange* pDX)
{
	__super::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_LIST_SYNC, m_sync);
	DDX_Control(pDX, IDC_LIST_ASYNC, m_async);
}

BEGIN_MESSAGE_MAP(CListCtrlTestDlg, CCommandAndNotifyCheckDlg)
	ON_BN_CLICKED(IDC_BUTTON_CHANGE_VIEW, &CListCtrlTestDlg::OnBnClickedButtonChangeView)
	ON_NOTIFY(LVN_ENDLABELEDIT, IDC_LIST_SYNC, &CListCtrlTestDlg::OnLvnEndlabeleditListSync)
END_MESSAGE_MAP()

BOOL CListCtrlTestDlg::OnInitDialog()
{
	__super::OnInitDialog();

	CListCtrl* pList[] = {&m_sync, &m_async};
	for (int i = 0; i < 2; i++)
	{
		pList[i]->InsertColumn(0, _T("Col0"), 0, 100, 0);
		LVCOLUMN col;
		col.mask = LVCF_FMT | LVCF_WIDTH | LVCF_TEXT | LVCF_SUBITEM | LVCF_IMAGE | LVCF_ORDER | LVCF_MINWIDTH | LVCF_DEFAULTWIDTH | LVCF_IDEALWIDTH;
		col.fmt = 1;
		col.cx = 101;
		col.pszText = _T("Col1");
		col.cchTextMax = 0;
		col.iSubItem = 1;
		col.iImage = -1;
		col.iOrder = 1;
		col.cxMin = 10;
		col.cxDefault = 101;
		col.cxIdeal = 101;
		pList[i]->InsertColumn(1, &col);
		for (int j = 0; j < 100; j++)
		{	
			CString strIndex;
			strIndex.Format(_T("%d"), j);
			pList[i]->InsertItem(j, _T(""));
			pList[i]->SetItemText(j, 0, strIndex + _T("-0"));
			pList[i]->SetItemText(j, 1, strIndex + _T("-1"));
			pList[i]->SetItemData(j, j);
			RECT rc;
			pList[i]->GetItemRect(j, &rc, 2);
			CRect crc;
			pList[i]->GetSubItemRect(j, 0, 2, crc);
			pList[i]->GetSubItemRect(j, 1, 2, crc);
		}
	}

	m_msgBoxIdAndNotify[IDC_LIST_ASYNC].insert(LVN_ITEMCHANGING);
	m_msgBoxIdAndNotify[IDC_LIST_ASYNC].insert(LVN_BEGINLABELEDITW);
	m_msgBoxIdAndNotify[IDC_LIST_ASYNC].insert(LVN_BEGINLABELEDITA);
	return TRUE;
}

void CListCtrlTestDlg::OnBnClickedButtonChangeView()
{
	int view = m_sync.GetView() + 1;
	if (LV_VIEW_MAX < view) {
		view = 0;
	}
	m_sync.SetView(view);
}

void CListCtrlTestDlg::OnLvnEndlabeleditListSync(NMHDR *pNMHDR, LRESULT *pResult)
{
	NMLVDISPINFO *pDispInfo = reinterpret_cast<NMLVDISPINFO*>(pNMHDR);
	*pResult = TRUE;
}
