#pragma once

/**
	@brief	通知コード情報。
*/
struct CodeInfo
{
	//ダイアログID。
	int dialogId;

	//メッセージ
	int msg;

	//通知コード。
	int code;

	//スクロールバーコード
	int nSBCode;
	
	//スクロール位置
	int nPos;

	CodeInfo() : dialogId(), msg(), code(), nSBCode(), nPos() {}
	CodeInfo(int dialogId_, int msg_, int code_, int nSBCode_, int nPos_) : dialogId(dialogId_), msg(msg_), code(code_), nSBCode(nSBCode_), nPos(nPos_)  {}
};