#pragma once
#include <vector>
#include <map>
#include <set>
struct CodeInfo;

class CCommandAndNotifyCheckDlg : public CDialogEx
{
	DECLARE_DYNAMIC(CCommandAndNotifyCheckDlg)

public:
	CCommandAndNotifyCheckDlg(UINT id, CWnd* pParent);
	virtual ~CCommandAndNotifyCheckDlg();
	virtual void ClearCodeInfo();

protected:
	virtual void DoDataExchange(CDataExchange* pDX);
	DECLARE_MESSAGE_MAP()
	
public:
	virtual BOOL OnCommand(WPARAM wParam, LPARAM lParam);
	virtual BOOL OnNotify(WPARAM wParam, LPARAM lParam, LRESULT* pResult);
	virtual void OnVScroll(UINT nSBCode, UINT nPos, CScrollBar* pScrollBar);
	virtual void OnHScroll(UINT nSBCode, UINT nPos, CScrollBar* pScrollBar);

	std::vector<CodeInfo> m_codeInfo;

	//メッセージボックスを表示するWM_COMMANDとWM_NOTIFYの通知
	std::map<UINT, std::set<UINT>> m_msgBoxIdAndCommand;
	std::map<UINT, std::set<UINT>> m_msgBoxIdAndNotify;
	std::set<UINT> m_msgBoxVScroll;
	std::set<UINT> m_msgBoxHScroll;
};
