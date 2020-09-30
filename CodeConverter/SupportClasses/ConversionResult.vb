﻿' Licensed to the .NET Foundation under one or more agreements.
' The .NET Foundation licenses this file to you under the MIT license.
' See the LICENSE file in the project root for more information.

Imports Microsoft.CodeAnalysis
Imports Microsoft.CodeAnalysis.Formatting
Imports Microsoft.CodeAnalysis.Options
Imports Microsoft.CodeAnalysis.Text

Imports VB = Microsoft.CodeAnalysis.VisualBasic

Namespace CSharpToVBConverter

    Public Class ConversionResult

        Private _filteredListOfFailures As List(Of Diagnostic)

        Friend Sub New(ConvertedTree As SyntaxNode, InputLanguage As String, OutputLanguage As String, VBPreprocessorSymbols As List(Of KeyValuePair(Of String, Object)))
            Me.Exceptions = New List(Of Exception)
            Me.SourceLanguage = InputLanguage
            Me.ResultStatus = ResultTriState.Success
            Me.TargetLanguage = OutputLanguage
            Using Workspace As New AdhocWorkspace()
                Dim project As Project = Workspace.CurrentSolution.AddProject("Project", "Project.dll", OutputLanguage)

                Dim VBParseOptions As VB.VisualBasicParseOptions = GetVBParseOptions(VBPreprocessorSymbols)

                project = project.WithParseOptions(VBParseOptions)

                Dim _Document As Document = project.AddDocument("Document", ConvertedTree)
                Dim _SyntaxTree As SyntaxTree = _Document.GetSyntaxTreeAsync().Result

                Dim Root As SyntaxNode = _SyntaxTree.GetRootAsync().GetAwaiter.GetResult
                Try
                    Me.ConvertedCode = WorkspaceFormat(Workspace, Root, spans:=Nothing, Workspace.Options, _Document.GetTextAsync().GetAwaiter.GetResult)
                    Me.ConvertedTree = DirectCast(Root, VB.VisualBasicSyntaxNode)
                    Exit Sub
                Catch ex As Exception
                    Stop
                End Try

                Dim tree As SyntaxTree = VB.VisualBasicSyntaxTree.ParseText(Root.NormalizeWhitespaceEx(useDefaultCasing:=True).ToFullString)
                Dim Root1 As SyntaxNode = tree.GetRootAsync().GetAwaiter.GetResult
                Try
                    Dim ConvertedCode1 As String = WorkspaceFormat(Workspace, Root1, spans:=Nothing, Workspace.Options, _Document.GetTextAsync().GetAwaiter.GetResult)
                    Me.ConvertedCode = ConvertedCode1
                    Me.ConvertedTree = DirectCast(Root1, VB.VisualBasicSyntaxNode)
                Catch ex As Exception
                    Me.ConvertedCode = DirectCast(Root, VB.VisualBasicSyntaxNode).ToFullString
                End Try
                Me.ConvertedTree = DirectCast(Root1, VB.VisualBasicSyntaxNode)
            End Using
        End Sub

        Friend Sub New(ParamArray exceptions() As Exception)
            Me.ResultStatus = If(exceptions.Any, ResultTriState.Failure, ResultTriState.Ignore)
            Me.Exceptions = exceptions
        End Sub

        ''' <summary>
        ''' Indicates File Conversion succeeded, failed or wasn't attempted (ignored)
        ''' </summary>
        Public Enum ResultTriState

            '     This file was ignored setting.
            Ignore = -2

            '     Conversion succeeded.
            Success = -1

            '     Conversion failed.
            Failure = 0

        End Enum

        Public ReadOnly Property ConvertedCode As String

        Public Property ConvertedTree As VB.VisualBasicSyntaxNode

        Public Property Exceptions As IReadOnlyList(Of Exception)
        Public Property ResultStatus As ResultTriState

        Public Property SourceLanguage As String

        Public Property TargetLanguage As String

        Protected Shared Function WorkspaceFormat(workspace As Workspace, root As SyntaxNode, spans As IEnumerable(Of TextSpan), pOptionSet As OptionSet, pSourceText As SourceText) As String
            Dim result As IList(Of TextChange) = Formatter.GetFormattedTextChanges(root, spans, workspace, pOptionSet)
            Return pSourceText?.WithChanges(result).ToString()
        End Function

        Public Function GetFilteredListOfFailures() As List(Of Diagnostic)
            If _filteredListOfFailures IsNot Nothing Then
                For Each d As Diagnostic In _filteredListOfFailures
                    If d.Id = "BC30689" Then
                        Return New List(Of Diagnostic)
                    End If
                Next
            End If
            Return _filteredListOfFailures
        End Function

        Public Sub SetFilteredListOfFailures(AutoPropertyValue As List(Of Diagnostic))
            _filteredListOfFailures = AutoPropertyValue
        End Sub

    End Class

End Namespace
