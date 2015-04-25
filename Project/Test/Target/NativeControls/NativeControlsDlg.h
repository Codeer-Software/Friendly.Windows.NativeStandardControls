#pragma once

class CNativeControlsDlg : public CDialogEx
{
public:
	CNativeControlsDlg(CWnd* pParent = NULL);
	enum { IDD = IDD_MAIN_DLG };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);

protected:
	HICON m_hIcon;

	virtual BOOL OnInitDialog();
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnBnClickedButtonButtonTest();
	afx_msg void OnBnClickedButtonComboTest();
	afx_msg void OnBnClickedButtonLog();
	afx_msg void OnBnClickedButtonDatetimePicker();
	afx_msg void OnBnClickedButtonIpadress();
	afx_msg void OnBnClickedButtonTab();
	afx_msg void OnBnClickedButtonSlider();
	afx_msg void OnBnClickedButtonSpin();
	afx_msg void OnBnClickedButtonScroll();
	afx_msg void OnBnClickedButtonMonth();
	afx_msg void OnBnClickedButtonProgress();
	afx_msg void OnBnClickedButtonListbox();
	afx_msg void OnBnClickedButtonEdit();
	afx_msg void OnBnClickedButtonListctrl();
	afx_msg void OnBnClickedButtonTree();
};
