[DllImport("USER32.DLL")]
private static extern IntPtr SendMessage(IntPtr hWnd, Int32 Msg, Int32 wParam, ref Point lParam);
[DllImport("USER32.DLL")]
private static extern bool GetScrollRange(IntPtr hWnd, int nBar, out int lpMinPos, out int lpMaxPos);

/// <summary>
/// テキストボックスへ文字列を追加します(自動スクロールしない)
/// </summary>
/// <param name="str">追加する文字列</param>
public void AppendLog(String str)
{
    // テキストボックスへフォーカス
    this.textbox.Focus();
	
	// スクロール位置取得
	Point point = new Point(0, 0);
	SendMessage(this.textbox.Handle, 0x04DD, 0, out point);
	
	// ログ出力
	this.textbox.AppendText(str);
	
	// スクロール位置を復元
	SendMessage(this.textbox.Handle, 0x04DE, 0, out point);
}

/// <summary>
/// テキストボックスへ文字列を追加します(最終行が表示されている場合のみ自動スクロール)
/// </summary>
/// <param name="str">追加する文字列</param>
public void AppendLog2(String str)
{
    // テキストボックスへフォーカス
    this.textbox.Focus();
	
	// スクロールバーの最終座標取得
	int min = 0;
	int max = 0;
	GetScrollRange(this.textbox.Handle, 1, out min, out max);
	
	// スクロール位置取得
	Point point = new Point(0, 0);
	SendMessage(this.textbox.Handle, 0x04DD, 0, out point);
	
	// 最終行判定
	bool isLastLine = false;
	if(max - this.textbox.Height - point.Y <= 0)
	{
	    isLastLine = true;
	}
	
	// ログ出力
	this.textbox.AppendText(str);
	
	// スクロール位置を復元
	if(!isLastLine)
	{
	    SendMessage(this.textbox.Handle, 0x04DE, 0, out point);
	}
}