#include "stdafx.h"
#include "NativeControls.h"
#include "ProgressTestDlg.h"
#include "afxdialogex.h"

IMPLEMENT_DYNAMIC(CProgressTestDlg, CDialogEx)

CProgressTestDlg::CProgressTestDlg(CWnd* pParent /*=NULL*/)
: CDialogEx(CProgressTestDlg::IDD, pParent){}

CProgressTestDlg::~CProgressTestDlg(){}

void CProgressTestDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_PROGRESS, m_progress);
}

BEGIN_MESSAGE_MAP(CProgressTestDlg, CDialogEx)
END_MESSAGE_MAP()

BOOL CProgressTestDlg::OnInitDialog()
{
	__super::OnInitDialog();
	m_progress.SetRange(10, 100);
	m_progress.SetPos(30);
	return TRUE;
}
