#include "stdafx.h"
#include "NativeControls.h"
#include "EditTestDlg.h"
#include "afxdialogex.h"

IMPLEMENT_DYNAMIC(CEditTestDlg, CCommandAndNotifyCheckDlg)

CEditTestDlg::CEditTestDlg(CWnd* pParent /*=NULL*/)
: CCommandAndNotifyCheckDlg(CEditTestDlg::IDD, pParent)
{
	m_msgBoxIdAndCommand[IDC_EDIT_ASYNC].insert(EN_CHANGE);
	m_msgBoxIdAndCommand[IDC_EDIT_ASYNC].insert(EN_HSCROLL);
	m_msgBoxIdAndCommand[IDC_EDIT_ASYNC].insert(EN_VSCROLL);
	m_msgBoxIdAndCommand[IDC_RICHEDIT_ASYNC].insert(EN_CHANGE);
	m_msgBoxIdAndCommand[IDC_RICHEDIT_ASYNC].insert(EN_HSCROLL);
	m_msgBoxIdAndCommand[IDC_RICHEDIT_ASYNC].insert(EN_VSCROLL);
	m_msgBoxIdAndNotify[IDC_RICHEDIT_ASYNC].insert(EN_SELCHANGE);
}

CEditTestDlg::~CEditTestDlg(){}

void CEditTestDlg::DoDataExchange(CDataExchange* pDX)
{
	__super::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_RICHEDIT_SYNC, m_richSync);
	DDX_Control(pDX, IDC_RICHEDIT_ASYNC, m_richAsync);
}

BEGIN_MESSAGE_MAP(CEditTestDlg, CCommandAndNotifyCheckDlg)
END_MESSAGE_MAP()


BOOL CEditTestDlg::OnInitDialog()
{
	__super::OnInitDialog();

	UINT mask =
		ENM_CHANGE|
		ENM_UPDATE|
		ENM_SCROLL|
		ENM_SCROLLEVENTS|
		ENM_DRAGDROPDONE|
		ENM_PARAGRAPHEXPANDED|
		ENM_PAGECHANGE|
	//	ENM_KEYEVENTS|
	//	ENM_MOUSEEVENTS|
	//	ENM_REQUESTRESIZE|
		ENM_SELCHANGE|
		ENM_DROPFILES|
		ENM_PROTECTED|
		ENM_CORRECTTEXT|
		ENM_IMECHANGE|
		ENM_LANGCHANGE|
		ENM_OBJECTPOSITIONS|
		ENM_LINK|
		ENM_LOWFIRTF;

	m_richSync.SetEventMask(mask);
	m_richAsync.SetEventMask(mask);
	return TRUE;
}
