﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.42000
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Namespace Global.My

    'NOTE: This file is auto-generated; do not modify it directly.  To make changes,
    ' or if you encounter build errors in this file, go to the Project Designer
    ' (go to Project Properties or double-click the My Project node in
    ' Solution Explorer), and make changes on the Application tab.
    '
    Partial Friend Class MyApplication
#If Not (NET5_0 OrElse Net4_8) Then
        Inherits Global.Microsoft.VisualBasic.ApplicationServices.WindowsFormsApplicationBase
#Else
        Inherits ApplicationServices.WindowsFormsApplicationBase
#End If

        <Global.System.Diagnostics.DebuggerStepThroughAttribute()>
        Public Sub New()
#If NET5_0 Then
            MyBase.New(ApplicationServices.AuthenticationMode.Windows)
#Else
            MyBase.New()
#End If
            Me.EnableVisualStyles = True
            Me.IsSingleInstance = True
            Me.MinimumSplashScreenDisplayTime = 5000
            Me.SaveMySettingsOnExit = True
#If Not (NET5_0 OrElse Net4_8) Then
            Me.ShutdownStyle = Global.Microsoft.VisualBasic.ApplicationServices.ShutdownMode.AfterMainFormCloses
#Else
            Me.ShutdownStyle = ApplicationServices.ShutdownMode.AfterMainFormCloses
#End If
        End Sub

        <Global.System.Diagnostics.DebuggerStepThroughAttribute()>
        Protected Overrides Sub OnCreateMainForm()
#If Not (NET5_0 OrElse Net4_8) Then
            Me.MainForm = New Form1
#Else
            Me.MainForm = New CSharpToVBApp.Form1
#End If
        End Sub

        <Global.System.Diagnostics.DebuggerStepThroughAttribute()>
        Protected Overrides Sub OnCreateSplashScreen()
#If Not (NET5_0 OrElse Net4_8) Then
            Me.SplashScreen = New SplashScreen1
#Else
            Me.SplashScreen = New CSharpToVBApp.SplashScreen1
#End If
        End Sub
    End Class
End Namespace
