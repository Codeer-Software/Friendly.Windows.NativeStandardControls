#include "stdafx.h"
#include "NativeControls.h"
#include "TreeTestDlg.h"
#include "afxdialogex.h"

IMPLEMENT_DYNAMIC(CTreeTestDlg, CCommandAndNotifyCheckDlg)

CTreeTestDlg::CTreeTestDlg(CWnd* pParent /*=NULL*/)
	: CCommandAndNotifyCheckDlg(CTreeTestDlg::IDD, pParent)
{
}

CTreeTestDlg::~CTreeTestDlg()
{
}

void CTreeTestDlg::DoDataExchange(CDataExchange* pDX)
{
	__super::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_TREE_SYNC, m_sync);
	DDX_Control(pDX, IDC_TREE_ASYNC, m_async);
}

BEGIN_MESSAGE_MAP(CTreeTestDlg, CCommandAndNotifyCheckDlg)
	ON_NOTIFY(TVN_ENDLABELEDIT, IDC_TREE_SYNC, &CTreeTestDlg::OnTvnEndlabeleditTreeSync)
	ON_NOTIFY(TVN_ITEMEXPANDED, IDC_TREE_SYNC, &CTreeTestDlg::OnTvnItemexpandedTreeSync)
	ON_NOTIFY(TVN_ITEMEXPANDING, IDC_TREE_SYNC, &CTreeTestDlg::OnTvnItemexpandingTreeSync)
END_MESSAGE_MAP()

BOOL CTreeTestDlg::OnInitDialog()
{
	__super::OnInitDialog();

	
	m_image.Create(IDB_BITMAP1, 16, 2, RGB(255, 255, 255));

	{
		m_sync.SetImageList(&m_image, TVSIL_NORMAL);
		HTREEITEM root1 = m_sync.InsertItem(_T("0"), 0, 4);
		m_sync.SetItemData(root1, 100);
		m_sync.InsertItem(_T("1"), 1, 4, root1);
		m_sync.InsertItem(_T("2"),2, 4, root1);
		HTREEITEM root2 = m_sync.InsertItem(_T("10"), 3, 4);
		m_sync.SetItemData(root2, 101);
		for (int i = 0; i < 100; i++) {
			CString str;
			str.Format(_T("%d"), i);
			m_sync.InsertItem(str, 0, 4, root2);
		}
		
	}
	{
		m_async.SetImageList(&m_image, TVSIL_NORMAL);
		HTREEITEM i = m_async.InsertItem(_T("0"), 0, 0);
		m_async.InsertItem(_T("1"), 1, 1, i);
		m_async.InsertItem(_T("2"),2, 2, i);
	}

	
	m_msgBoxIdAndNotify[IDC_TREE_ASYNC].insert(TVN_BEGINLABELEDIT);
	m_msgBoxIdAndNotify[IDC_TREE_ASYNC].insert(TVN_SELCHANGING);
	m_msgBoxIdAndNotify[IDC_TREE_ASYNC].insert(TVN_ITEMCHANGING);
	m_msgBoxIdAndNotify[IDC_TREE_ASYNC].insert(TVN_ITEMEXPANDING);
	return TRUE;
}

void CTreeTestDlg::OnTvnEndlabeleditTreeSync(NMHDR *pNMHDR, LRESULT *pResult)
{
	LPNMTVDISPINFO pTVDispInfo = reinterpret_cast<LPNMTVDISPINFO>(pNMHDR);
	*pResult = TRUE;//Ç±ÇÍÇTRUEÇ…Ç∑ÇÈÇ±Ç∆Ç…ÇÊÇ¡Çƒï“èWÇ™ämíËÇ≥ÇÍÇÈÅB
}

void CTreeTestDlg::OnTvnItemexpandedTreeSync(NMHDR *pNMHDR, LRESULT *pResult)
{
	LPNMTREEVIEW pNMTreeView = reinterpret_cast<LPNMTREEVIEW>(pNMHDR);
	m_notify.push_back(*pNMTreeView);
	*pResult = 0;
}

void CTreeTestDlg::OnTvnItemexpandingTreeSync(NMHDR *pNMHDR, LRESULT *pResult)
{
	LPNMTREEVIEW pNMTreeView = reinterpret_cast<LPNMTREEVIEW>(pNMHDR);
	m_notify.push_back(*pNMTreeView);
	*pResult = 0;
}

void CTreeTestDlg::ClearCodeInfo()
{
	__super::ClearCodeInfo();
	m_notify.clear();
}

extern "C"
{
	__declspec(dllexport) int __cdecl GetNMTREEVIEW(HWND hDlg, int arrayCount, NMTREEVIEW* pArray)
	{
		CTreeTestDlg* pDlg = dynamic_cast<CTreeTestDlg*>(CWnd::FromHandle(hDlg));
		if (pDlg == NULL || arrayCount < (int)pDlg->m_notify.size()) {
			return -1;
		}
		for (size_t i = 0; i < pDlg->m_notify.size(); i++) {
			pArray[i] = pDlg->m_notify[i];
		}
		return (int)pDlg->m_notify.size();
	}
}
