#include "stdafx.h"
#include "NativeControls.h"
#include "NativeControlsDlg.h"
#include "afxdialogex.h"

CNativeControlsDlg::CNativeControlsDlg(CWnd* pParent /*=NULL*/)
	: CDialogEx(CNativeControlsDlg::IDD, pParent)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CNativeControlsDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
}

BEGIN_MESSAGE_MAP(CNativeControlsDlg, CDialogEx)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_BN_CLICKED(IDC_BUTTON_BUTTON_TEST, &CNativeControlsDlg::OnBnClickedButtonButtonTest)
	ON_BN_CLICKED(IDC_BUTTON_COMBO_TEST, &CNativeControlsDlg::OnBnClickedButtonComboTest)
	ON_BN_CLICKED(IDC_BUTTON_LOG, &CNativeControlsDlg::OnBnClickedButtonLog)
	ON_BN_CLICKED(IDC_BUTTON_DATETIME_PICKER, &CNativeControlsDlg::OnBnClickedButtonDatetimePicker)
	ON_BN_CLICKED(IDC_BUTTON_IPADRESS, &CNativeControlsDlg::OnBnClickedButtonIpadress)
	ON_BN_CLICKED(IDC_BUTTON_TAB, &CNativeControlsDlg::OnBnClickedButtonTab)
	ON_BN_CLICKED(IDC_BUTTON_SLIDER, &CNativeControlsDlg::OnBnClickedButtonSlider)
	ON_BN_CLICKED(IDC_BUTTON_SPIN, &CNativeControlsDlg::OnBnClickedButtonSpin)
	ON_BN_CLICKED(IDC_BUTTON_SCROLL, &CNativeControlsDlg::OnBnClickedButtonScroll)
	ON_BN_CLICKED(IDC_BUTTON_MONTH, &CNativeControlsDlg::OnBnClickedButtonMonth)
	ON_BN_CLICKED(IDC_BUTTON_PROGRESS, &CNativeControlsDlg::OnBnClickedButtonProgress)
	ON_BN_CLICKED(IDC_BUTTON_LISTBOX, &CNativeControlsDlg::OnBnClickedButtonListbox)
	ON_BN_CLICKED(IDC_BUTTON_EDIT, &CNativeControlsDlg::OnBnClickedButtonEdit)
	ON_BN_CLICKED(IDC_BUTTON_LISTCTRL, &CNativeControlsDlg::OnBnClickedButtonListctrl)
	ON_BN_CLICKED(IDC_BUTTON_TREE, &CNativeControlsDlg::OnBnClickedButtonTree)
END_MESSAGE_MAP()

BOOL CNativeControlsDlg::OnInitDialog()
{
	CDialogEx::OnInitDialog();
	SetIcon(m_hIcon, TRUE);
	SetIcon(m_hIcon, FALSE);
	return TRUE;
}

void CNativeControlsDlg::OnPaint()
{
	if (IsIconic())
	{
		CPaintDC dc(this); 
		SendMessage(WM_ICONERASEBKGND, reinterpret_cast<WPARAM>(dc.GetSafeHdc()), 0);
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CDialogEx::OnPaint();
	}
}

HCURSOR CNativeControlsDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}

#include "LogDispDlg.h"
void CNativeControlsDlg::OnBnClickedButtonLog()
{
	CLogDispDlg::ShowSingleton();
}

#include "ButtonTestDlg.h"
void CNativeControlsDlg::OnBnClickedButtonButtonTest()
{
	CButtonTestDlg dlg;
	dlg.DoModal();
}

#include "ComboTestDlg.h"
void CNativeControlsDlg::OnBnClickedButtonComboTest()
{
	CComboTestDlg dlg;
	dlg.DoModal();
}

#include "DateTimePickerTestDlg.h"
void CNativeControlsDlg::OnBnClickedButtonDatetimePicker()
{
	CDateTimePickerTestDlg dlg;
	dlg.DoModal();
}

#include "IPAdressTestDlg.h"
void CNativeControlsDlg::OnBnClickedButtonIpadress()
{
	CIPAdressTestDlg dlg;
	dlg.DoModal();
}

#include "TabTestDlg.h"
void CNativeControlsDlg::OnBnClickedButtonTab()
{
	CTabTestDlg dlg;
	dlg.DoModal();
}

#include "SliderTestDlg.h"
void CNativeControlsDlg::OnBnClickedButtonSlider()
{
	CSliderTestDlg dlg;
	dlg.DoModal();
}

#include "SpinTestDlg.h"
void CNativeControlsDlg::OnBnClickedButtonSpin()
{
	CSpinTestDlg dlg;
	dlg.DoModal();
}

#include "ScrollTestDlg.h"
void CNativeControlsDlg::OnBnClickedButtonScroll()
{
	CScrollTestDlg dlg;
	dlg.DoModal();
}

#include "MonthCalendarTestDlg.h"
void CNativeControlsDlg::OnBnClickedButtonMonth()
{
	CMonthCalendarTestDlg dlg;
	dlg.DoModal();
}

#include "ProgressTestDlg.h"
void CNativeControlsDlg::OnBnClickedButtonProgress()
{
	CProgressTestDlg dlg;
	dlg.DoModal();
}

#include "ListBoxTestDlg.h"
void CNativeControlsDlg::OnBnClickedButtonListbox()
{
	CListBoxTestDlg dlg;
	dlg.DoModal();
}

#include "EditTestDlg.h"
void CNativeControlsDlg::OnBnClickedButtonEdit()
{
	CEditTestDlg dlg;
	dlg.DoModal();
}

#include "ListCtrlTestDlg.h"
void CNativeControlsDlg::OnBnClickedButtonListctrl()
{
	CListCtrlTestDlg dlg;
	dlg.DoModal();}

#include "TreeTestDlg.h"
void CNativeControlsDlg::OnBnClickedButtonTree()
{
	CTreeTestDlg dlg;
	dlg.DoModal();
}
