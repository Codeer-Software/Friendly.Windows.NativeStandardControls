#include "stdafx.h"
#include "NativeControls.h"
#include "ButtonTestDlg.h"
#include "afxdialogex.h"

IMPLEMENT_DYNAMIC(CButtonTestDlg, CCommandAndNotifyCheckDlg)

CButtonTestDlg::CButtonTestDlg(CWnd* pParent /*=NULL*/)
	: CCommandAndNotifyCheckDlg(CButtonTestDlg::IDD, pParent)
{
	m_msgBoxIdAndCommand[IDC_BUTTON_ASYNC].insert(BN_CLICKED);
	m_msgBoxIdAndCommand[IDC_CHECK_ASYNC].insert(BN_CLICKED);
}

CButtonTestDlg::~CButtonTestDlg()
{
}

void CButtonTestDlg::DoDataExchange(CDataExchange* pDX)
{
	CCommandAndNotifyCheckDlg::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_RADIO1, m_radio1);
}

BOOL CButtonTestDlg::OnInitDialog()
{
	__super::OnInitDialog();
	m_radio1.SetCheck(1);
	return TRUE;
}

BEGIN_MESSAGE_MAP(CButtonTestDlg, CCommandAndNotifyCheckDlg)
END_MESSAGE_MAP()
