#include "stdafx.h"
#include "NativeControls.h"
#include "ScrollTestDlg.h"
#include "afxdialogex.h"

namespace {
	void SetNextPos(UINT nSBCode, UINT nPos, CScrollBar* pScrollBar)
	{
		if (!pScrollBar) {
			return;
		}

		int nextPos = pScrollBar->GetScrollPos();
		switch(nSBCode){
		case SB_LINEDOWN:
			nextPos++; break;
		case SB_LINEUP:
			nextPos--; break;
		case SB_PAGEDOWN:
			nextPos+=10; break;
		case SB_PAGEUP:
			nextPos-=10; break;
		case SB_THUMBTRACK:
			nextPos = nPos; break;
		default:
			return;
		}
		int minPos = 0, maxPos = 0;
		pScrollBar->GetScrollRange(&minPos, &maxPos);
		if( nextPos < minPos) {
			nextPos = minPos;
		} else if( nextPos > maxPos) {
			nextPos = maxPos;
		}
		pScrollBar->SetScrollPos(nextPos);
	}

}

IMPLEMENT_DYNAMIC(CScrollTestDlg, CCommandAndNotifyCheckDlg)

CScrollTestDlg::CScrollTestDlg(CWnd* pParent /*=NULL*/)
	: CCommandAndNotifyCheckDlg(CScrollTestDlg::IDD, pParent)
{
	m_msgBoxHScroll.insert(IDC_SCROLLBAR_H_ASYNC);
}

CScrollTestDlg::~CScrollTestDlg(){}

void CScrollTestDlg::DoDataExchange(CDataExchange* pDX)
{
	__super::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_SCROLLBAR_H_ASYNC, m_HAsync);
	DDX_Control(pDX, IDC_SCROLLBAR_H_SYNC, m_HSync);
	DDX_Control(pDX, IDC_SCROLLBAR_V_SYNC, m_VSync);
}

BEGIN_MESSAGE_MAP(CScrollTestDlg, CCommandAndNotifyCheckDlg)
	ON_WM_HSCROLL()
	ON_WM_VSCROLL()
END_MESSAGE_MAP()

BOOL CScrollTestDlg::OnInitDialog()
{
	__super::OnInitDialog();
	m_HSync.SetScrollRange(1, 100);
	m_HSync.SetScrollPos(50);
	m_HAsync.SetScrollRange(1, 100);
	m_HAsync.SetScrollPos(50);
	m_VSync.SetScrollRange(1, 100);
	m_VSync.SetScrollPos(50);
	return TRUE;
}

void CScrollTestDlg::OnVScroll(UINT nSBCode, UINT nPos, CScrollBar* pScrollBar)
{
	__super::OnVScroll(nSBCode, nPos, pScrollBar);
	SetNextPos(nSBCode, nPos, pScrollBar);
}

void CScrollTestDlg::OnHScroll(UINT nSBCode, UINT nPos, CScrollBar* pScrollBar) 
{
	__super::OnHScroll(nSBCode, nPos, pScrollBar);
	SetNextPos(nSBCode, nPos, pScrollBar);
}