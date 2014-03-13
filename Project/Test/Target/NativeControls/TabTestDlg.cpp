#include "stdafx.h"
#include "NativeControls.h"
#include "TabTestDlg.h"
#include "afxdialogex.h"

IMPLEMENT_DYNAMIC(CTabTestDlg, CCommandAndNotifyCheckDlg)

CTabTestDlg::CTabTestDlg(CWnd* pParent /*=NULL*/)
: CCommandAndNotifyCheckDlg(CTabTestDlg::IDD, pParent)
{
	m_msgBoxIdAndNotify[IDC_TAB_ASYNC].insert(TCN_SELCHANGING);
}

CTabTestDlg::~CTabTestDlg(){}

void CTabTestDlg::DoDataExchange(CDataExchange* pDX)
{
	CCommandAndNotifyCheckDlg::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_TAB_SYNC, m_sync);
	DDX_Control(pDX, IDC_TAB_ASYNC, m_async);
}

BEGIN_MESSAGE_MAP(CTabTestDlg, CCommandAndNotifyCheckDlg)
END_MESSAGE_MAP()

BOOL CTabTestDlg::OnInitDialog()
{
	__super::OnInitDialog();

	m_image.Create(IDB_BITMAP1, 16, 2, RGB(255, 255, 255));
	m_sync.SetImageList(&m_image);
	m_sync.InsertItem(TCIF_TEXT | TCIF_IMAGE | TCIF_PARAM, 0, _T("a"), 0, (LPARAM)10, 3, 3);
	m_sync.InsertItem(TCIF_TEXT | TCIF_IMAGE | TCIF_PARAM, 1, _T("あ"), 1, (LPARAM)11, 0, 0);

	CString strLongText;
	for (int i = 0; i < 259; i++) {
		strLongText += "a";
	}
	m_sync.InsertItem(2, strLongText);
#if UNICODE
	m_sync.InsertItem(3, _T("𩸽"));
#endif
	m_sync.SetCurSel(0);

	m_async.InsertItem(0, _T("a"));
	m_async.InsertItem(1, _T("あ"));
	m_async.SetCurSel(0);
	return TRUE;
}
