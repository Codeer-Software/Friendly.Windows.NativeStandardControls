#include "stdafx.h"
#include "NativeControls.h"
#include "ComboTestDlg.h"
#include "afxdialogex.h"

IMPLEMENT_DYNAMIC(CComboTestDlg, CCommandAndNotifyCheckDlg)

CComboTestDlg::CComboTestDlg(CWnd* pParent /*=NULL*/)
	: CCommandAndNotifyCheckDlg(CComboTestDlg::IDD, pParent)
{
	m_msgBoxIdAndCommand[IDC_COMBO_ASYNC].insert(CBN_SELCHANGE);
	m_msgBoxIdAndCommand[IDC_COMBOBOXEX_ASYNC].insert(CBN_SELCHANGE);

	m_msgBoxIdAndCommand[IDC_COMBO_ASYNC].insert(CBN_EDITCHANGE);
	m_msgBoxIdAndCommand[IDC_COMBOBOXEX_ASYNC].insert(CBN_EDITCHANGE);

	m_msgBoxIdAndCommand[IDC_COMBO_ASYNC].insert(CBN_DROPDOWN);
	m_msgBoxIdAndCommand[IDC_COMBOBOXEX_ASYNC].insert(CBN_DROPDOWN);
}

CComboTestDlg::~CComboTestDlg()
{
}

BOOL CComboTestDlg::OnInitDialog()
{
	__super::OnInitDialog();
	
	m_sync.AddString(_T("あ"));
	m_sync.AddString(_T("b"));
	m_sync.AddString(_T("c"));

#if UNICODE
	m_sync.AddString(_T("𩸽"));
#endif

	m_async.AddString(_T("あ"));
	m_async.AddString(_T("b"));
	m_async.AddString(_T("c"));

	COMBOBOXEXITEM citem;
	citem.mask = CBEIF_TEXT | CBEIF_INDENT | CBEIF_IMAGE | CBEIF_SELECTEDIMAGE;
    citem.iItem = 0;
    citem.iIndent = 0;
    citem.iImage = -1;
    citem.iSelectedImage = -1;


    citem.iItem = 0;
	citem.pszText = _T("あ");
	m_exSync.InsertItem(&citem);
	m_dropDownList.InsertItem(&citem);
	m_exAsync.InsertItem(&citem);

	citem.iItem = 1;
    citem.pszText = _T("b");
	m_exSync.InsertItem(&citem);
	m_dropDownList.InsertItem(&citem);
	m_exAsync.InsertItem(&citem);

	citem.iItem = 2;
	citem.pszText = _T("c");
	m_exSync.InsertItem(&citem);
	m_dropDownList.InsertItem(&citem);
	m_exAsync.InsertItem(&citem);

#if UNICODE
	citem.iItem = 3;
    citem.pszText = _T("𩸽");
	m_exSync.InsertItem(&citem);
#endif

	m_dropDownList.SetCurSel(0);

	m_sync.SetItemData(0, 0);
	m_sync.SetItemData(1, 1);
	m_sync.SetItemData(2, 2);
	m_exSync.SetItemData(0, 0);
	m_exSync.SetItemData(1, 1);
	m_exSync.SetItemData(2, 2);
	return TRUE;
}

void CComboTestDlg::DoDataExchange(CDataExchange* pDX)
{
	CCommandAndNotifyCheckDlg::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_COMBO_SYNC, m_sync);
	DDX_Control(pDX, IDC_COMBO_ASYNC, m_async);
	DDX_Control(pDX, IDC_COMBOBOXEX_SYNC, m_exSync);
	DDX_Control(pDX, IDC_COMBOBOXEX_ASYNC, m_exAsync);
	DDX_Control(pDX, IDC_COMBOBOXEX_SYNC_DROPDOWNLIST, m_dropDownList);
}

BEGIN_MESSAGE_MAP(CComboTestDlg, CCommandAndNotifyCheckDlg)
END_MESSAGE_MAP()
