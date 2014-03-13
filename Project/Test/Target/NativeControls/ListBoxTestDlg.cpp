#include "stdafx.h"
#include "NativeControls.h"
#include "ListBoxTestDlg.h"
#include "afxdialogex.h"

IMPLEMENT_DYNAMIC(CListBoxTestDlg, CCommandAndNotifyCheckDlg)

CListBoxTestDlg::CListBoxTestDlg(CWnd* pParent /*=NULL*/)
: CCommandAndNotifyCheckDlg(CListBoxTestDlg::IDD, pParent)
{
	m_msgBoxIdAndCommand[IDC_LIST_ASYNC].insert(LBN_SELCHANGE);
	m_msgBoxIdAndCommand[IDC_LIST_ASYNC].insert(LBN_DBLCLK);
	m_msgBoxIdAndCommand[IDC_LIST_ASYNC_MULTI].insert(LBN_SELCHANGE);
	m_msgBoxIdAndCommand[IDC_LIST_ASYNC_MULTI].insert(LBN_DBLCLK);
}

CListBoxTestDlg::~CListBoxTestDlg(){}

void CListBoxTestDlg::DoDataExchange(CDataExchange* pDX)
{
	__super::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_LIST_SYNC, m_sync);
	DDX_Control(pDX, IDC_LIST_SYNC_MULTI, m_syncMulti);
	DDX_Control(pDX, IDC_LIST_ASYNC, m_async);
	DDX_Control(pDX, IDC_LIST_ASYNC_MULTI, m_asyncMulti);
}

BEGIN_MESSAGE_MAP(CListBoxTestDlg, CCommandAndNotifyCheckDlg)
END_MESSAGE_MAP()

BOOL CListBoxTestDlg::OnInitDialog()
{
	__super::OnInitDialog();

	CListBox* lists[] = { &m_sync, &m_syncMulti, &m_async, &m_asyncMulti};
	for (int i = 0; i < sizeof(lists) / sizeof(CListBox*); i++)
	{
		for (int j = 0; j < 100; j++)
		{
			CString str;
			str.Format(_T("%d"), j);
			lists[i]->AddString(str);
			lists[i]->SetItemData(j, j);
		}
#if UNICODE
		lists[i]->AddString(_T("𩸽"));
		lists[i]->SetItemData(100, 100);
#endif
	}
	return TRUE;
}
