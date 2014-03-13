#include "stdafx.h"
#include "NativeControls.h"
#include "SliderTestDlg.h"
#include "afxdialogex.h"

IMPLEMENT_DYNAMIC(CSliderTestDlg, CCommandAndNotifyCheckDlg)

CSliderTestDlg::CSliderTestDlg(CWnd* pParent /*=NULL*/)
: CCommandAndNotifyCheckDlg(CSliderTestDlg::IDD, pParent)
{
	m_msgBoxHScroll.insert(IDC_SLIDER_ASYNC);
}

CSliderTestDlg::~CSliderTestDlg(){}

void CSliderTestDlg::DoDataExchange(CDataExchange* pDX)
{
	CCommandAndNotifyCheckDlg::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_SLIDER_SYNC, m_sync);
	DDX_Control(pDX, IDC_SLIDER_ASYNC, m_async);
	DDX_Control(pDX, IDC_SLIDER_V, m_v);
}

BEGIN_MESSAGE_MAP(CSliderTestDlg, CCommandAndNotifyCheckDlg)
	ON_NOTIFY(TRBN_THUMBPOSCHANGING, IDC_SLIDER_SYNC, &CSliderTestDlg::OnTRBNThumbPosChangingSliderSync)
END_MESSAGE_MAP()

BOOL CSliderTestDlg::OnInitDialog()
{
	__super::OnInitDialog();
	m_sync.SetRange(10, 1000);
	m_v.SetRange(0, 1000);
	m_async.SetRange(0, 1000);
	return TRUE;
}

void CSliderTestDlg::OnTRBNThumbPosChangingSliderSync(NMHDR *pNMHDR, LRESULT *pResult)
{
	NMTRBTHUMBPOSCHANGING *pNMTPC = reinterpret_cast<NMTRBTHUMBPOSCHANGING *>(pNMHDR);
	m_notify.push_back(*pNMTPC);
	*pResult = 0;
}

void CSliderTestDlg::ClearCodeInfo()
{
	__super::ClearCodeInfo();
	m_notify.clear();
}

extern "C"
{
	__declspec(dllexport) int __cdecl GetNMTRBTHUMBPOSCHANGING(HWND hDlg, int arrayCount, NMTRBTHUMBPOSCHANGING* pArray)
	{
		CSliderTestDlg* pDlg = dynamic_cast<CSliderTestDlg*>(CWnd::FromHandle(hDlg));
		if (pDlg == NULL || arrayCount < (int)pDlg->m_notify.size()) {
			return -1;
		}
		for (size_t i = 0; i < pDlg->m_notify.size(); i++) {
			pArray[i] = pDlg->m_notify[i];
		}
		return (int)pDlg->m_notify.size();
	}
}
