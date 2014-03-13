#include "stdafx.h"
#include "NativeControls.h"
#include "LogDispDlg.h"
#include "afxdialogex.h"

static CLogDispDlg* s_pDlg = NULL;

IMPLEMENT_DYNAMIC(CLogDispDlg, CDialogEx)

CLogDispDlg::CLogDispDlg(CWnd* pParent /*=NULL*/)
	: CDialogEx(CLogDispDlg::IDD, pParent){}

CLogDispDlg::~CLogDispDlg(){}

void CLogDispDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_EDIT_LOG, m_edit);
}

BEGIN_MESSAGE_MAP(CLogDispDlg, CDialogEx)
	ON_BN_CLICKED(IDC_BUTTON_CLEAR, &CLogDispDlg::OnBnClickedButtonClear)
END_MESSAGE_MAP()

void CLogDispDlg::OnBnClickedButtonClear()
{
	GetDlgItem(IDC_EDIT_LOG)->SetWindowText(_T(""));
}

void CLogDispDlg::Trace(LPCSTR szTrace)
{
	if (s_pDlg) {
		CString s;
		s_pDlg->m_edit.GetWindowText(s);
		s_pDlg->m_edit.SetWindowText(s + szTrace + _T("\r\n"));
		::SendMessage(s_pDlg->m_edit.m_hWnd, WM_VSCROLL,SB_BOTTOM,NULL);
	} else {
		TRACE(CStringA(szTrace) + szTrace);
	}
}

void CLogDispDlg::PostNcDestroy()
{
	delete this;
	s_pDlg = NULL;
}

void CLogDispDlg::ShowSingleton()
{
	if (s_pDlg) {
		return;
	}
	s_pDlg = new CLogDispDlg();
	s_pDlg->Create(CLogDispDlg::IDD);
	s_pDlg->ShowWindow(SW_SHOW);
}

void CLogDispDlg::OnCancel()
{
	__super::OnCancel();
	DestroyWindow();
}
