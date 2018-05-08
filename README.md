Friendly.Windows.NativeStandardControls
============================

This library is a layer on top of
Friendly, so you must learn that first.
But it is very easy to learn.

https://github.com/Codeer-Software/Friendly.Windows

## Getting Started
Install Friendly.Windows from NuGet

    PM> Install-Package Codeer.Friendly.Windows.NativeStandardControls
https://www.nuget.org/packages/Codeer.Friendly.Windows.NativeStandardControls/

***
Friendly.Windows.NativeStandardControls defines the following classes.   
They can operate Win32/MFC control easily from a separate process.  

* NativeButton
* NativeComboBox
* NativeDateTimePicker
* NativeEdit
* NativeIPAddress
* NativeListBox
* NativeListControl
* NativeMenuItem
* NativeMessageBox
* NativeMonthCalendar
* NativeProgress
* NativeScrollBar
* NativeSlider
* NativeSpinButton
* NativeTab
* NativeTree

***
```cs  
//sample  
var process = Process.GetProcessesByName("NativeTarget")[0];  
using (var app = new WindowsAppFriend(process))  
{  
    var testDlg = WindowControl.FromZTop(app);

    //Button
    var button = new NativeButton(testDlg.IdentifyFromDialogId(1004));
    button.EmulateClick();

    //Edit
    var edit = new NativeEdit(testDlg.IdentifyFromDialogId(1020));
    edit.EmulateChangeText("abc");
    
    //Tree
    var tree = new NativeTree(testDlg.IdentifyFromDialogId(1041));
    tree.EmulateEdit(tree.Nodes[0], "new text"); 
}  
```
### More samples
https://github.com/Codeer-Software/Friendly.Windows.NativeStandardControls/tree/master/Project/Test

***
For other GUI types, use the following libraries:

* For WPF.  
https://www.nuget.org/packages/RM.Friendly.WPFStandardControls/

* For WinForms.  
https://www.nuget.org/packages/Ong.Friendly.FormsStandardControls/  

* For getting the target window.  
https://www.nuget.org/packages/Codeer.Friendly.Windows.Grasp/  

***
If you use PinInterface, you map control simple.  
https://www.nuget.org/packages/VSHTC.Friendly.PinInterface/



