#include "stdafx.h"
#include "NativeControls.h"
#include "CommandAndNotifyCheckDlg.h"
#include "afxdialogex.h"
#include "CodeInfo.h"
#include <map>
#include "LogDispDlg.h"

//調査用にWM_COMMANDとWM_NOTIFYをログ表示するための仕組み。
namespace
{
	std::map<CStringA, std::map<int, CStringA>> s_commandMap;
	std::map<CStringA, std::map<int, CStringA>> s_notifyMap;
	#define ENTRY_COMMAND(windowClass, code) s_commandMap[#windowClass][code] = #code;
	#define ENTRY_COMMAND2(windowClass1, windowClass2, code) s_commandMap[#windowClass1][code] = #code; s_commandMap[#windowClass2][code] = #code;
	#define ENTRY_COMMAND3(windowClass1, windowClass2, windowClass3, code) s_commandMap[#windowClass1][code] = #code; s_commandMap[#windowClass2][code] = #code; s_commandMap[#windowClass3][code] = #code;
	#define ENTRY_NOTIFY(windowClass, code) s_notifyMap[#windowClass][code] = #code;
	#define ENTRY_NOTIFY2(windowClass1, windowClass2, code) s_notifyMap[#windowClass1][code] = #code; s_notifyMap[#windowClass2][code] = #code;
	#define ENTRY_NOTIFY_ALL(code) \
		s_notifyMap["Button"][code] = #code;\
		s_notifyMap["ComboBox"][code] = #code;\
		s_notifyMap["ComboBoxEx32"][code] = #code;\
		s_notifyMap["SysDateTimePick32"][code] = #code;\
		s_notifyMap["SysIPAddress32"][code] = #code;\
		s_notifyMap["msctls_trackbar32"][code] = #code;\
		s_notifyMap["msctls_updown32"][code] = #code;\
		s_notifyMap["SysMonthCal32"][code] = #code;\
		s_notifyMap["ListBox"][code] = #code;\
		s_notifyMap["Edit"][code] = #code;\
		s_notifyMap["RichEdit20A"][code] = #code;\
		s_notifyMap["RichEdit20W"][code] = #code;\
		s_notifyMap["SysListView32"][code] = #code;\
		s_notifyMap["SysTreeView32"][code] = #code;\
		s_notifyMap["SysTabControl32"][code] = #code;
	
	void InitMap()
	{
		if (s_commandMap.size() != 0) {
			return;
		}

		//共通
		ENTRY_NOTIFY_ALL(NM_OUTOFMEMORY)
		ENTRY_NOTIFY_ALL(NM_CLICK)
		ENTRY_NOTIFY_ALL(NM_DBLCLK)
		ENTRY_NOTIFY_ALL(NM_RETURN)
		ENTRY_NOTIFY_ALL(NM_RCLICK)
		ENTRY_NOTIFY_ALL(NM_RDBLCLK)
		ENTRY_NOTIFY_ALL(NM_SETFOCUS)
		ENTRY_NOTIFY_ALL(NM_KILLFOCUS)
		ENTRY_NOTIFY_ALL(NM_CUSTOMDRAW)
		ENTRY_NOTIFY_ALL(NM_HOVER)
		ENTRY_NOTIFY_ALL(NM_NCHITTEST)
		ENTRY_NOTIFY_ALL(NM_KEYDOWN)
		ENTRY_NOTIFY_ALL(NM_RELEASEDCAPTURE)
		ENTRY_NOTIFY_ALL(NM_SETCURSOR)
		ENTRY_NOTIFY_ALL(NM_CHAR)
		ENTRY_NOTIFY_ALL(NM_TOOLTIPSCREATED)
		ENTRY_NOTIFY_ALL(NM_LDOWN)
		ENTRY_NOTIFY_ALL(NM_RDOWN)
		ENTRY_NOTIFY_ALL(NM_THEMECHANGED)
		ENTRY_NOTIFY_ALL(NM_FONTCHANGED)
		ENTRY_NOTIFY_ALL(NM_CUSTOMTEXT)
		ENTRY_NOTIFY_ALL(NM_TVSTATEIMAGECHANGING)

		//ボタン
		ENTRY_COMMAND(Button, BN_CLICKED)
		ENTRY_COMMAND(Button, BN_PAINT)
		ENTRY_COMMAND(Button, BN_HILITE)
		ENTRY_COMMAND(Button, BN_UNHILITE)
		ENTRY_COMMAND(Button, BN_DISABLE)
		ENTRY_COMMAND(Button, BN_DOUBLECLICKED)
		ENTRY_COMMAND(Button, BN_PUSHED)
		ENTRY_COMMAND(Button, BN_UNPUSHED)
		ENTRY_COMMAND(Button, BN_DBLCLK)
		ENTRY_COMMAND(Button, BN_SETFOCUS)
		ENTRY_COMMAND(Button, BN_KILLFOCUS)
		ENTRY_NOTIFY(Button, NM_GETCUSTOMSPLITRECT)
		ENTRY_NOTIFY(Button, BCN_HOTITEMCHANGE)
		ENTRY_NOTIFY(Button, BCN_DROPDOWN)

		//コンボ
		ENTRY_COMMAND2(ComboBox, ComboBoxEx32, CBN_ERRSPACE)
		ENTRY_COMMAND2(ComboBox, ComboBoxEx32, CBN_SELCHANGE)
		ENTRY_COMMAND2(ComboBox, ComboBoxEx32, CBN_DBLCLK)
		ENTRY_COMMAND2(ComboBox, ComboBoxEx32, CBN_SETFOCUS)
		ENTRY_COMMAND2(ComboBox, ComboBoxEx32, CBN_KILLFOCUS)
		ENTRY_COMMAND2(ComboBox, ComboBoxEx32, CBN_EDITCHANGE)
		ENTRY_COMMAND2(ComboBox, ComboBoxEx32, CBN_EDITUPDATE)
		ENTRY_COMMAND2(ComboBox, ComboBoxEx32, CBN_DROPDOWN)
		ENTRY_COMMAND2(ComboBox, ComboBoxEx32, CBN_CLOSEUP)
		ENTRY_COMMAND2(ComboBox, ComboBoxEx32, CBN_SELENDOK)
		ENTRY_COMMAND2(ComboBox, ComboBoxEx32, CBN_SELENDCANCEL)
		ENTRY_NOTIFY(ComboBoxEx32, CBEN_GETDISPINFOA)
		ENTRY_NOTIFY(ComboBoxEx32, CBEN_INSERTITEM)
		ENTRY_NOTIFY(ComboBoxEx32, CBEN_DELETEITEM)
		ENTRY_NOTIFY(ComboBoxEx32, CBEN_BEGINEDIT)
		ENTRY_NOTIFY(ComboBoxEx32, CBEN_ENDEDITA)
		ENTRY_NOTIFY(ComboBoxEx32, CBEN_ENDEDITW)
		ENTRY_NOTIFY(ComboBoxEx32, CBEN_GETDISPINFOW)
		ENTRY_NOTIFY(ComboBoxEx32, CBEN_DRAGBEGINA)
		ENTRY_NOTIFY(ComboBoxEx32, CBEN_DRAGBEGINW)

		//DateTimePicker
		ENTRY_NOTIFY(SysDateTimePick32, DTN_DATETIMECHANGE)
		ENTRY_NOTIFY(SysDateTimePick32, DTN_USERSTRINGA)
		ENTRY_NOTIFY(SysDateTimePick32, DTN_USERSTRINGW)
		ENTRY_NOTIFY(SysDateTimePick32, DTN_WMKEYDOWNA)
		ENTRY_NOTIFY(SysDateTimePick32, DTN_WMKEYDOWNW)
		ENTRY_NOTIFY(SysDateTimePick32, DTN_FORMATA)
		ENTRY_NOTIFY(SysDateTimePick32, DTN_FORMATW)
		ENTRY_NOTIFY(SysDateTimePick32, DTN_FORMATQUERYA)
		ENTRY_NOTIFY(SysDateTimePick32, DTN_FORMATQUERYW)
		ENTRY_NOTIFY(SysDateTimePick32, DTN_DROPDOWN)
		ENTRY_NOTIFY(SysDateTimePick32, DTN_CLOSEUP)

		//IPAdress
		ENTRY_NOTIFY(SysIPAddress32, IPN_FIELDCHANGED)

		//Tab
		ENTRY_NOTIFY(SysTabControl32, TCN_KEYDOWN)
		ENTRY_NOTIFY(SysTabControl32, TCN_SELCHANGE)
		ENTRY_NOTIFY(SysTabControl32, TCN_SELCHANGING)
		ENTRY_NOTIFY(SysTabControl32, TCN_GETOBJECT)
		ENTRY_NOTIFY(SysTabControl32, TCN_FOCUSCHANGE)

		//Slider
		ENTRY_NOTIFY(msctls_trackbar32, TRBN_THUMBPOSCHANGING)

		//Spin
		ENTRY_NOTIFY(msctls_updown32, UDN_DELTAPOS)

		//MonthCal
		ENTRY_NOTIFY(SysMonthCal32, MCN_SELCHANGE)
		ENTRY_NOTIFY(SysMonthCal32, MCN_GETDAYSTATE)
		ENTRY_NOTIFY(SysMonthCal32, MCN_SELECT)
		ENTRY_NOTIFY(SysMonthCal32, MCN_VIEWCHANGE)

		//ListBox
		ENTRY_COMMAND(ListBox, LBN_ERRSPACE)
		ENTRY_COMMAND(ListBox, LBN_SELCHANGE)
		ENTRY_COMMAND(ListBox, LBN_DBLCLK)
		ENTRY_COMMAND(ListBox, LBN_SELCANCEL)
		ENTRY_COMMAND(ListBox, LBN_SETFOCUS)
		ENTRY_COMMAND(ListBox, LBN_KILLFOCUS)

		//Edit, RichEdit20A
		ENTRY_COMMAND3(Edit, RichEdit20A, RichEdit20W, EN_SETFOCUS)
		ENTRY_COMMAND3(Edit, RichEdit20A, RichEdit20W, EN_KILLFOCUS)
		ENTRY_COMMAND3(Edit, RichEdit20A, RichEdit20W, EN_CHANGE)
		ENTRY_COMMAND3(Edit, RichEdit20A, RichEdit20W, EN_UPDATE)
		ENTRY_COMMAND3(Edit, RichEdit20A, RichEdit20W, EN_ERRSPACE)
		ENTRY_COMMAND3(Edit, RichEdit20A, RichEdit20W, EN_MAXTEXT)
		ENTRY_COMMAND3(Edit, RichEdit20A, RichEdit20W, EN_HSCROLL)
		ENTRY_COMMAND3(Edit, RichEdit20A, RichEdit20W, EN_VSCROLL)
		ENTRY_COMMAND3(Edit, RichEdit20A, RichEdit20W, EN_ALIGN_LTR_EC)
		ENTRY_COMMAND3(Edit, RichEdit20A, RichEdit20W, EN_ALIGN_RTL_EC)
		ENTRY_NOTIFY2(RichEdit20A, RichEdit20W, EN_MSGFILTER)
		ENTRY_NOTIFY2(RichEdit20A, RichEdit20W, EN_REQUESTRESIZE)
		ENTRY_NOTIFY2(RichEdit20A, RichEdit20W, EN_SELCHANGE)
		ENTRY_NOTIFY2(RichEdit20A, RichEdit20W, EN_DROPFILES)
		ENTRY_NOTIFY2(RichEdit20A, RichEdit20W, EN_PROTECTED)
		ENTRY_NOTIFY2(RichEdit20A, RichEdit20W, EN_CORRECTTEXT)
		ENTRY_NOTIFY2(RichEdit20A, RichEdit20W, EN_STOPNOUNDO)
		ENTRY_NOTIFY2(RichEdit20A, RichEdit20W, EN_IMECHANGE)
		ENTRY_NOTIFY2(RichEdit20A, RichEdit20W, EN_SAVECLIPBOARD)
		ENTRY_NOTIFY2(RichEdit20A, RichEdit20W, EN_OLEOPFAILED)
		ENTRY_NOTIFY2(RichEdit20A, RichEdit20W, EN_OBJECTPOSITIONS)
		ENTRY_NOTIFY2(RichEdit20A, RichEdit20W, EN_LINK)
		ENTRY_NOTIFY2(RichEdit20A, RichEdit20W, EN_DRAGDROPDONE)
		ENTRY_NOTIFY2(RichEdit20A, RichEdit20W, EN_PARAGRAPHEXPANDED)
		ENTRY_NOTIFY2(RichEdit20A, RichEdit20W, EN_PAGECHANGE)
		ENTRY_NOTIFY2(RichEdit20A, RichEdit20W, EN_LOWFIRTF)
		ENTRY_NOTIFY2(RichEdit20A, RichEdit20W, EN_ALIGNLTR)
		ENTRY_NOTIFY2(RichEdit20A, RichEdit20W, EN_ALIGNRTL)

		//SysListView32
		ENTRY_NOTIFY(SysListView32, LVN_ITEMCHANGING)
		ENTRY_NOTIFY(SysListView32, LVN_ITEMCHANGED)
		ENTRY_NOTIFY(SysListView32, LVN_INSERTITEM)
		ENTRY_NOTIFY(SysListView32, LVN_DELETEITEM)
		ENTRY_NOTIFY(SysListView32, LVN_DELETEALLITEMS)
		ENTRY_NOTIFY(SysListView32, LVN_BEGINLABELEDITA)
		ENTRY_NOTIFY(SysListView32, LVN_BEGINLABELEDITW)
		ENTRY_NOTIFY(SysListView32, LVN_ENDLABELEDITA)
		ENTRY_NOTIFY(SysListView32, LVN_ENDLABELEDITW)
		ENTRY_NOTIFY(SysListView32, LVN_COLUMNCLICK)
		ENTRY_NOTIFY(SysListView32, LVN_BEGINDRAG)
		ENTRY_NOTIFY(SysListView32, LVN_BEGINRDRAG)
		ENTRY_NOTIFY(SysListView32, LVN_ODCACHEHINT)
		ENTRY_NOTIFY(SysListView32, LVN_ODFINDITEMA)
		ENTRY_NOTIFY(SysListView32, LVN_ODFINDITEMW)
		ENTRY_NOTIFY(SysListView32, LVN_ITEMACTIVATE)
		ENTRY_NOTIFY(SysListView32, LVN_ODSTATECHANGED)
		ENTRY_NOTIFY(SysListView32, LVN_HOTTRACK)
		ENTRY_NOTIFY(SysListView32, LVN_GETDISPINFOA)
		ENTRY_NOTIFY(SysListView32, LVN_GETDISPINFOW)
		ENTRY_NOTIFY(SysListView32, LVN_SETDISPINFOA)
		ENTRY_NOTIFY(SysListView32, LVN_SETDISPINFOW)
		ENTRY_NOTIFY(SysListView32, LVN_KEYDOWN)
		ENTRY_NOTIFY(SysListView32, LVN_MARQUEEBEGIN)
		ENTRY_NOTIFY(SysListView32, LVN_GETINFOTIPA)
		ENTRY_NOTIFY(SysListView32, LVN_GETINFOTIPW)
		ENTRY_NOTIFY(SysListView32, LVN_INCREMENTALSEARCHA)
		ENTRY_NOTIFY(SysListView32, LVN_INCREMENTALSEARCHW)
		ENTRY_NOTIFY(SysListView32, LVN_COLUMNDROPDOWN )
		ENTRY_NOTIFY(SysListView32, LVN_COLUMNOVERFLOWCLICK)
		ENTRY_NOTIFY(SysListView32, LVN_BEGINSCROLL)
		ENTRY_NOTIFY(SysListView32, LVN_ENDSCROLL)
		ENTRY_NOTIFY(SysListView32, LVN_LINKCLICK)
		ENTRY_NOTIFY(SysListView32, LVN_GETEMPTYMARKUP)

		//SysTreeView32
		ENTRY_NOTIFY(SysTreeView32, TVN_SELCHANGINGA)
		ENTRY_NOTIFY(SysTreeView32, TVN_SELCHANGINGW)
		ENTRY_NOTIFY(SysTreeView32, TVN_SELCHANGEDA)
		ENTRY_NOTIFY(SysTreeView32, TVN_SELCHANGEDW)
		ENTRY_NOTIFY(SysTreeView32, TVN_GETDISPINFOA)
		ENTRY_NOTIFY(SysTreeView32, TVN_GETDISPINFOW)
		ENTRY_NOTIFY(SysTreeView32, TVN_SETDISPINFOA)
		ENTRY_NOTIFY(SysTreeView32, TVN_SETDISPINFOW)
		ENTRY_NOTIFY(SysTreeView32, TVN_ITEMEXPANDINGA)
		ENTRY_NOTIFY(SysTreeView32, TVN_ITEMEXPANDINGW)
		ENTRY_NOTIFY(SysTreeView32, TVN_ITEMEXPANDEDA)
		ENTRY_NOTIFY(SysTreeView32, TVN_ITEMEXPANDEDW)
		ENTRY_NOTIFY(SysTreeView32, TVN_BEGINDRAGA)
		ENTRY_NOTIFY(SysTreeView32, TVN_BEGINDRAGW)
		ENTRY_NOTIFY(SysTreeView32, TVN_BEGINRDRAGA)
		ENTRY_NOTIFY(SysTreeView32, TVN_BEGINRDRAGW)
		ENTRY_NOTIFY(SysTreeView32, TVN_DELETEITEMA)
		ENTRY_NOTIFY(SysTreeView32, TVN_DELETEITEMW)
		ENTRY_NOTIFY(SysTreeView32, TVN_BEGINLABELEDITA)
		ENTRY_NOTIFY(SysTreeView32, TVN_BEGINLABELEDITW)
		ENTRY_NOTIFY(SysTreeView32, TVN_ENDLABELEDITA)
		ENTRY_NOTIFY(SysTreeView32, TVN_ENDLABELEDITW)
		ENTRY_NOTIFY(SysTreeView32, TVN_KEYDOWN)
		ENTRY_NOTIFY(SysTreeView32, TVN_GETINFOTIPA)
		ENTRY_NOTIFY(SysTreeView32, TVN_GETINFOTIPW)
		ENTRY_NOTIFY(SysTreeView32, TVN_SINGLEEXPAND)
		ENTRY_NOTIFY(SysTreeView32, TVN_ITEMCHANGINGA)
		ENTRY_NOTIFY(SysTreeView32, TVN_ITEMCHANGINGW)
		ENTRY_NOTIFY(SysTreeView32, TVN_ITEMCHANGEDA)
		ENTRY_NOTIFY(SysTreeView32, TVN_ITEMCHANGEDW)
		ENTRY_NOTIFY(SysTreeView32, TVN_ASYNCDRAW)
	}

	void Trace(const CodeInfo& info, HWND handle)
	{
		char name[1024];
		::ZeroMemory(name, sizeof(name));
		::GetClassNameA(handle, name, sizeof(name) - 1);

		std::map<CStringA, std::map<int, CStringA>> empty;
		const std::map<CStringA, std::map<int, CStringA>>* codeMap = &empty;
		CStringA codeType;
		switch(info.msg)
		{
		case WM_COMMAND:
			codeType = "WM_COMMAND";
			codeMap = &s_commandMap;
			break;
		case WM_NOTIFY:
			codeType = "WM_NOTIFY";
			codeMap = &s_notifyMap;
			break;
		case WM_VSCROLL:
			codeType.Format("WM_VSCROLL nSBCode = %d, nPos = %d, ", info.nSBCode, info.nPos);
			break;
		case WM_HSCROLL:
			codeType.Format("WM_HSCROLL nSBCode = %d, nPos = %d, ", info.nSBCode, info.nPos);
			break;
		}
		//登録されているコードであれば文字列でトレース。
		BOOL isTrace = FALSE;
		std::map<CStringA, std::map<int, CStringA>>::const_iterator iteClass = codeMap->find(name);
		if (iteClass != codeMap->end()) {
			std::map<int, CStringA>::const_iterator iteCode = iteClass->second.find(info.code);
			if (iteCode != iteClass->second.end()) {
				CStringA strLog; 
				strLog.Format("%s %d, %s", codeType, info.dialogId, iteCode->second); 
				CLogDispDlg::Trace(strLog);
				isTrace = TRUE;
			}
		}

		//それ以外は数値でトレース。
		if (!isTrace) {
			CStringA strLog; 
			strLog.Format("%s %d, %d", codeType, info.dialogId, info.code); 
			CLogDispDlg::Trace(strLog);
		}
	}
}

IMPLEMENT_DYNAMIC(CCommandAndNotifyCheckDlg, CDialogEx)

CCommandAndNotifyCheckDlg::CCommandAndNotifyCheckDlg(UINT id, CWnd* pParent) : CDialogEx(id, pParent)
{
	InitMap();
}

void CCommandAndNotifyCheckDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
}

CCommandAndNotifyCheckDlg::~CCommandAndNotifyCheckDlg(){}

BEGIN_MESSAGE_MAP(CCommandAndNotifyCheckDlg, CDialogEx)
	ON_WM_HSCROLL()
	ON_WM_VSCROLL()
END_MESSAGE_MAP()

void CCommandAndNotifyCheckDlg::OnVScroll(UINT nSBCode, UINT nPos, CScrollBar* pScrollBar)
{
	int dialogId = pScrollBar ? ::GetDlgCtrlID(pScrollBar->m_hWnd) : 0;
	m_codeInfo.push_back(CodeInfo(dialogId, WM_VSCROLL, 0, nSBCode, nPos));
	const CodeInfo& info = m_codeInfo[m_codeInfo.size() -1];
	Trace(info, (pScrollBar ? pScrollBar->m_hWnd : NULL));
	
	//非同期テスト用にメッセージボックス表示
	if (m_msgBoxVScroll.find(info.dialogId) != m_msgBoxVScroll.end()) {
		::AfxMessageBox(_T(""));
	}
}

void CCommandAndNotifyCheckDlg::OnHScroll(UINT nSBCode, UINT nPos, CScrollBar* pScrollBar) 
{
	int dialogId = pScrollBar ? ::GetDlgCtrlID(pScrollBar->m_hWnd) : 0;
	m_codeInfo.push_back(CodeInfo(dialogId, WM_HSCROLL, 0, nSBCode, nPos));
	const CodeInfo& info = m_codeInfo[m_codeInfo.size() -1];
	Trace(info, (pScrollBar ? pScrollBar->m_hWnd : NULL));
	
	//非同期テスト用にメッセージボックス表示
	if (m_msgBoxHScroll.find(info.dialogId) != m_msgBoxHScroll.end()) {
		::AfxMessageBox(_T(""));
	}
}

BOOL CCommandAndNotifyCheckDlg::OnCommand(WPARAM wParam, LPARAM lParam)
{
	BOOL ret = __super::OnCommand(wParam, lParam);
	m_codeInfo.push_back(CodeInfo(LOWORD(wParam), WM_COMMAND, HIWORD(wParam), 0, 0));
	const CodeInfo& info = m_codeInfo[m_codeInfo.size() -1];
	Trace(info, (HWND)lParam);
	
	//非同期テスト用にメッセージボックス表示
	std::map<UINT, std::set<UINT>>::const_iterator iteId = m_msgBoxIdAndCommand.find(info.dialogId);
	if (iteId != m_msgBoxIdAndCommand.end() && iteId->second.find(info.code) != iteId->second.end()) {
		::AfxMessageBox(_T(""));
	}
	return ret;
}

BOOL CCommandAndNotifyCheckDlg::OnNotify(WPARAM wParam, LPARAM lParam, LRESULT* pResult)
{
	BOOL ret = __super::OnNotify(wParam, lParam, pResult);
	NMHDR* pHeader = (NMHDR*)lParam;
	if (pHeader) {
		//邪魔なので無視。
		switch (pHeader->code) {
		case NM_CUSTOMDRAW:
		case NM_KILLFOCUS:
		case NM_SETFOCUS:
		case NM_SETCURSOR:
			return ret;
		}

		m_codeInfo.push_back(CodeInfo((int)wParam, WM_NOTIFY, pHeader->code, 0, 0));
		const CodeInfo& info = m_codeInfo[m_codeInfo.size() -1];
		Trace(info, pHeader->hwndFrom);

		//非同期テスト用にメッセージボックス表示
		std::map<UINT, std::set<UINT>>::const_iterator iteId = m_msgBoxIdAndNotify.find(info.dialogId);
		if (iteId != m_msgBoxIdAndNotify.end() && iteId->second.find(info.code) != iteId->second.end()) {
			::AfxMessageBox(_T(""));
		}
	}
	return ret;
}

void CCommandAndNotifyCheckDlg::ClearCodeInfo()
{
	m_codeInfo.clear();
}

//テスト用公開関数
extern "C"
{
	__declspec(dllexport) void __cdecl ClearCodeInfo(HWND hDlg)
	{
		CCommandAndNotifyCheckDlg* pDlg = dynamic_cast<CCommandAndNotifyCheckDlg*>(CWnd::FromHandle(hDlg));
		if (pDlg != NULL) {
			pDlg->ClearCodeInfo();
		}
	}

	__declspec(dllexport) int __cdecl GetCodeInfo(HWND hDlg, int arrayCount, CodeInfo* pinfo)
	{
		CCommandAndNotifyCheckDlg* pDlg = dynamic_cast<CCommandAndNotifyCheckDlg*>(CWnd::FromHandle(hDlg));
		if (pinfo == NULL || pDlg == NULL || pDlg->m_codeInfo.size() == 0) {
			return 0;
		}
		memcpy(pinfo, &pDlg->m_codeInfo[0], sizeof(CodeInfo) * min(arrayCount, (int)pDlg->m_codeInfo.size()));
		return (int)pDlg->m_codeInfo.size();
	}

	_declspec(dllexport) void __cdecl ClearAsyncMsgBox(HWND hDlg)
	{
		CCommandAndNotifyCheckDlg* pDlg = dynamic_cast<CCommandAndNotifyCheckDlg*>(CWnd::FromHandle(hDlg));
		if (pDlg != NULL) {
			pDlg->m_msgBoxIdAndCommand.clear();
			pDlg->m_msgBoxIdAndNotify.clear();
			pDlg->m_msgBoxVScroll.clear();
			pDlg->m_msgBoxHScroll.clear();
		}
	}

}