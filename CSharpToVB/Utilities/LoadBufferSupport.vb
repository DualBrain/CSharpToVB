﻿' Licensed to the .NET Foundation under one or more agreements.
' The .NET Foundation licenses this file to you under the MIT license.
' See the LICENSE file in the project root for more information.

Imports System.IO
Imports CSharpToVBConverter
Imports Microsoft.CodeAnalysis

Module LoadBufferSupport

    Friend Function LoadInputBufferFromStream(MainForm As Form1, Path As String) As Integer
        MainForm.ConversionInput.Visible = False
        LocalUseWaitCursor(MainForm, WaitCursorEnable:=True)
        Dim sourceText As String
        Using myFileStream As FileStream = File.OpenRead(Path)
            sourceText = myFileStream.GetFileTextFromStream()
        End Using

        Dim lines() As String = sourceText.SplitLines
        If MainForm.mnuOptionsColorizeSource.Checked Then
            Colorize(MainForm, GetClassifiedRanges(lines.Join(vbCrLf), LanguageNames.CSharp), MainForm.ConversionInput, lines.Length)
        Else
            MainForm.ConversionInput.Text = lines.Join(vbCrLf)
        End If
        MainForm.ConversionInput.Visible = True
        If MainForm.mnuViewShowSourceLineNumbers.Checked Then
            MainForm.LineNumbersForConversionInput.Visible = False
            Application.DoEvents()
            MainForm.LineNumbersForConversionInput.Visible = True
            Application.DoEvents()
        End If
        LocalUseWaitCursor(MainForm, WaitCursorEnable:=False)
        Return lines.Length
    End Function

    Friend Sub LoadOutputBufferFromStream(MainForm As Form1, Path As String)
        LocalUseWaitCursor(MainForm, WaitCursorEnable:=True)
        Dim sourceText As String
        Using myFileStream As FileStream = File.OpenRead(Path)
            sourceText = myFileStream.GetFileTextFromStream()
        End Using

        Dim lines() As String = sourceText.SplitLines
        If MainForm.mnuOptionsColorizeSource.Checked Then
            Colorize(MainForm, GetClassifiedRanges(lines.Join(vbCrLf), LanguageNames.VisualBasic), MainForm.ConversionOutput, lines.Length)
        Else
            MainForm.ConversionOutput.Text = lines.Join(vbCrLf)
        End If
        LocalUseWaitCursor(MainForm, WaitCursorEnable:=False)
    End Sub

    Friend Sub OpenSourceFile(MainForm As Form1, Path As String)
        MainForm.mnuConvertConvertSnippet.Enabled = LoadInputBufferFromStream(MainForm, Path) <> 0
        My.Settings.MRU_Data.mnuAddToMRU(Path)
        MainForm.mnuFile.DropDownItems.FileMenuMRUUpdateUI(AddressOf MainForm.mnu_MRUList_Click)
        MainForm.UpdateLastFileMenu()
    End Sub

End Module
