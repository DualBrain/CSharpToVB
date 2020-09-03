﻿' Licensed to the .NET Foundation under one or more agreements.
' The .NET Foundation licenses this file to you under the MIT license.
' See the LICENSE file in the project root for more information.

Imports Microsoft.CodeAnalysis

Imports CS = Microsoft.CodeAnalysis.CSharp
Imports CSS = Microsoft.CodeAnalysis.CSharp.Syntax
Imports VB = Microsoft.CodeAnalysis.VisualBasic
Imports Factory = Microsoft.CodeAnalysis.VisualBasic.SyntaxFactory
Imports VBS = Microsoft.CodeAnalysis.VisualBasic.Syntax

Namespace CSharpToVBCodeConverter.ToVisualBasic

    Partial Public Class CSharpConverter

        Partial Friend Class NodesVisitor
            Inherits CS.CSharpSyntaxVisitor(Of VB.VisualBasicSyntaxNode)

            Private Function VisitCSArguments(OpenToken As SyntaxToken, csArguments As SeparatedSyntaxList(Of CSS.ArgumentSyntax), CloseToken As SyntaxToken) As VB.VisualBasicSyntaxNode
                If csArguments.Count = 0 Then
                    Return Factory.ArgumentList(Factory.SeparatedList(csArguments.Select(Function(a As CSS.ArgumentSyntax) DirectCast(a.Accept(Me), VBS.ArgumentSyntax))))
                End If
                Dim vbNodeList As New List(Of VBS.ArgumentSyntax)
                Dim separators As New List(Of SyntaxToken)
                For Each e As IndexClass(Of CSS.ArgumentSyntax) In csArguments.WithIndex
                    Dim argument As VBS.ArgumentSyntax = DirectCast(e.Value.Accept(Me), VBS.ArgumentSyntax)
                    Dim csOperation As Operations.IArgumentOperation = CType(_mSemanticModel.GetOperation(e.Value), Operations.IArgumentOperation)
                    If csOperation?.Value.Kind = OperationKind.DelegateCreation Then
                        Dim getExpression As VBS.ExpressionSyntax = argument.GetExpression
                        Select Case getExpression.Kind
                            Case VB.SyntaxKind.MultiLineFunctionLambdaExpression,
                                 VB.SyntaxKind.MultiLineSubLambdaExpression,
                                 VB.SyntaxKind.SingleLineFunctionLambdaExpression,
                                 VB.SyntaxKind.SingleLineSubLambdaExpression
                            Case Else
                                Dim newleadingTrivia As SyntaxTriviaList = getExpression.GetLeadingTrivia
                                If newleadingTrivia.Count >= 2 AndAlso newleadingTrivia(1).IsKind(VB.SyntaxKind.LineContinuationTrivia) Then
                                    argument = Factory.SimpleArgument(Factory.AddressOfExpression(AddressOfKeyword.WithTrailingTrivia(newleadingTrivia.GetRange(0, 1)), getExpression))
                                Else
                                    argument = Factory.SimpleArgument(Factory.AddressOfExpression(getExpression))
                                End If
                        End Select
                    End If
                    vbNodeList.Add(argument.AdjustNodeTrivia(SeparatorFollows:=Not e.IsLast))
                    If Not e.IsLast Then
                        separators.Add(CommaToken.WithConvertedTrailingTriviaFrom(csArguments.GetSeparators()(e.Index)))
                    End If
                Next
                Dim openParenTokenWithTrivia As SyntaxToken = OpenParenToken.WithConvertedTriviaFrom(OpenToken)
                Dim closeParenTokenWithTrivia As SyntaxToken = CloseParenToken.WithConvertedTriviaFrom(CloseToken)
                RestructureNodesAndSeparators(openParenTokenWithTrivia, vbNodeList, separators, closeParenTokenWithTrivia)
                Return Factory.ArgumentList(openParenTokenWithTrivia,
                                              Factory.SeparatedList(vbNodeList, separators),
                                              closeParenTokenWithTrivia
                                             )
            End Function

            Public Overrides Function VisitArgument(node As CSS.ArgumentSyntax) As VB.VisualBasicSyntaxNode
                Dim name As VBS.NameColonEqualsSyntax = Nothing
                Dim csExpression As CSS.ExpressionSyntax = node?.Expression
                Dim argumentWithTrivia As VBS.ExpressionSyntax = Nothing
                Dim newLeadingTrivia As New SyntaxTriviaList
                Dim newTrailingTrivia As New SyntaxTriviaList
                Try
                    If (Not node.RefKindKeyword.IsKind(CS.SyntaxKind.None)) AndAlso node.RefKindKeyword.Text = "ref" Then
                        argumentWithTrivia = DirectCast(node.Expression.Accept(Me), VBS.ExpressionSyntax)
                    ElseIf csExpression.IsKind(CS.SyntaxKind.CoalesceExpression) Then
                        Dim csBinaryExpression As CSS.BinaryExpressionSyntax = DirectCast(csExpression, CSS.BinaryExpressionSyntax)
                        If csBinaryExpression.Right.IsKind(CS.SyntaxKind.ThrowExpression) Then
                            Dim leftExpression As VBS.ExpressionSyntax = DirectCast(csBinaryExpression.Left.Accept(Me).WithConvertedTriviaFrom(csBinaryExpression.Left), VBS.ExpressionSyntax)
                            Dim rightExpression As VBS.ThrowStatementSyntax = DirectCast(csBinaryExpression.Right.Accept(Me).WithConvertedTriviaFrom(csBinaryExpression.Right), VBS.ThrowStatementSyntax)
                            Dim vbStatements As SyntaxList(Of VBS.StatementSyntax) = Factory.SingletonList(Of VBS.StatementSyntax)(rightExpression)

                            Dim condition As VBS.ExpressionSyntax = Factory.IsExpression(leftExpression, NothingExpression)
                            Dim ifBlock As VBS.SingleLineIfStatementSyntax = Factory.SingleLineIfStatement(condition,
                                                                                                              vbStatements,
                                                                                                              elseClause:=Nothing)
                            GetStatementwithIssues(node).AddMarker(ifBlock, StatementHandlingOption.PrependStatement, AllowDuplicates:=False)
                            ' TODO remove conversion of trivia
                            argumentWithTrivia = DirectCast(csBinaryExpression.Left.Accept(Me).WithConvertedTriviaFrom(csBinaryExpression.Left).AdjustNodeTrivia(SeparatorFollows:=True), VBS.ExpressionSyntax)
                        Else
                            argumentWithTrivia = DirectCast(csExpression.Accept(Me).AdjustNodeTrivia(SeparatorFollows:=True), VBS.ExpressionSyntax)
                        End If
                    ElseIf csExpression.IsKind(CS.SyntaxKind.IndexExpression) Then
                        Try
                            Dim elementAccessExpression As CSS.ElementAccessExpressionSyntax = CType(node.Parent.Parent, CSS.ElementAccessExpressionSyntax)
                            Dim offsetFromLength As VBS.ExpressionSyntax = CType(CType(csExpression, CSS.PrefixUnaryExpressionSyntax).Operand.Accept(Me), VBS.ExpressionSyntax)
                            Dim identName As VBS.IdentifierNameSyntax = Factory.IdentifierName(MakeVBSafeName(elementAccessExpression.Expression.ToString))
                            argumentWithTrivia = Factory.BinaryExpression(VB.SyntaxKind.SubtractExpression, identName, MinusToken, right:=offsetFromLength)
                        Catch ex As Exception
                            Stop
                            Throw UnexpectedValue("IndexExpression Parent.Parent not 'ElementAccessExpression'")
                        End Try
                    Else
                        argumentWithTrivia = DirectCast(csExpression.Accept(Me).AdjustNodeTrivia(SeparatorFollows:=True), VBS.ExpressionSyntax)
                    End If

                    If TypeOf node.Parent Is CSS.BracketedArgumentListSyntax Then
                        Dim _Typeinfo As TypeInfo = _mSemanticModel.GetTypeInfo(csExpression)
                        If Not SymbolEqualityComparer.Default.Equals(_Typeinfo.ConvertedType, _Typeinfo.Type) Then
                            If _Typeinfo.Type?.SpecialType = SpecialType.System_Char Then '
                                argumentWithTrivia = Factory.ParseExpression($"ChrW({argumentWithTrivia.WithoutTrivia})").WithTriviaFrom(argumentWithTrivia)
                            End If
                        End If
                    End If

                    If node.NameColon IsNot Nothing Then
                        name = Factory.NameColonEquals(DirectCast(node.NameColon.Name.Accept(Me), VBS.IdentifierNameSyntax))
                        Dim NameWithOutColon As String = name.Name.ToString.RemoveAll(":=")
                        ' TODO Remove comment
                        'If NameWithOutColon.EndsWith("_Renamed", StringComparison.Ordinal) Then
                        '    name = Factory.NameColonEquals(Factory.IdentifierName(NameWithOutColon.Replace("_Renamed", "", StringComparison.Ordinal)))
                        'End If
                    End If

                    If argumentWithTrivia.HasLeadingTrivia Then
                        For Each trivia As SyntaxTrivia In argumentWithTrivia.GetLeadingTrivia
                            Select Case trivia.RawKind
                                Case VB.SyntaxKind.WhitespaceTrivia, VB.SyntaxKind.EndOfLineTrivia,
                                     VB.SyntaxKind.CommentTrivia, VB.SyntaxKind.DocumentationCommentTrivia,
                                     VB.SyntaxKind.IfDirectiveTrivia,
                                     VB.SyntaxKind.DisabledTextTrivia, VB.SyntaxKind.ElseDirectiveTrivia,
                                     VB.SyntaxKind.ElseIfDirectiveTrivia, VB.SyntaxKind.EndIfDirectiveTrivia
                                    newLeadingTrivia = newLeadingTrivia.Add(trivia)
                                Case VB.SyntaxKind.DisableWarningDirectiveTrivia
                                    GetStatementwithIssues(node).AddMarker(Factory.EmptyStatement.WithLeadingTrivia(trivia), StatementHandlingOption.PrependStatement, AllowDuplicates:=True)
                                Case VB.SyntaxKind.EnableWarningDirectiveTrivia
                                    GetStatementwithIssues(node).AddMarker(Factory.EmptyStatement.WithLeadingTrivia(trivia), StatementHandlingOption.AppendEmptyStatement, AllowDuplicates:=True)
                                Case VB.SyntaxKind.LineContinuationTrivia
                                    If newLeadingTrivia.Last.IsKind(VB.SyntaxKind.LineContinuationTrivia) Then
                                        Continue For
                                    End If
                                    newLeadingTrivia = newLeadingTrivia.Add(LineContinuation)
                                Case Else
                                    Stop
                                    Throw UnexpectedValue(trivia.RawKind, "trivia.RawKind")
                            End Select
                        Next
                    End If
                    newTrailingTrivia = newTrailingTrivia.AddRange(argumentWithTrivia.GetTrailingTrivia)
                Catch ex As OperationCanceledException
                    Throw
                Catch ex As Exception
                    Stop
                End Try
                argumentWithTrivia = argumentWithTrivia.WithLeadingTrivia(newLeadingTrivia).WithTrailingTrivia(VBSpaceTrivia)
                If name IsNot Nothing Then
                    Dim tempTrivia As SyntaxTriviaList = node.NameColon.GetLeadingTrivia.ConvertTriviaList()
                    tempTrivia = tempTrivia.AddRange(argumentWithTrivia.GetLeadingTrivia)
                    argumentWithTrivia = argumentWithTrivia.WithLeadingTrivia(VBSpaceTrivia)
                    name = name.WithLeadingTrivia(tempTrivia)
                End If
                Return Factory.SimpleArgument(name, argumentWithTrivia).WithTrailingTrivia(newTrailingTrivia)
            End Function

            Public Overrides Function VisitArgumentList(node As CSS.ArgumentListSyntax) As VB.VisualBasicSyntaxNode
                Return Me.VisitCSArguments(node.OpenParenToken, node.Arguments, node.CloseParenToken)
            End Function

            Public Overrides Function VisitBracketedArgumentList(node As CSS.BracketedArgumentListSyntax) As VB.VisualBasicSyntaxNode
                Return Me.VisitCSArguments(node.OpenBracketToken, node.Arguments, node.CloseBracketToken)
            End Function

            Public Overrides Function VisitOmittedTypeArgument(node As CSS.OmittedTypeArgumentSyntax) As VB.VisualBasicSyntaxNode
                Return Factory.ParseTypeName("").WithConvertedTriviaFrom(node)
            End Function

            Public Overrides Function VisitTypeArgumentList(node As CSS.TypeArgumentListSyntax) As VB.VisualBasicSyntaxNode
                Dim csVisitorArguments As SeparatedSyntaxList(Of CSS.TypeSyntax) = node.Arguments
                Debug.Assert(csVisitorArguments.Any, "VisitTypeArgumentList csVisitorArguments.Count = 0")
                Dim csSeparators As IEnumerable(Of SyntaxToken) = csVisitorArguments.GetSeparators
                Dim nodeList As New List(Of VBS.TypeSyntax)
                Dim separators As New List(Of SyntaxToken)
                For Each e As IndexClass(Of CSS.TypeSyntax) In csVisitorArguments.WithIndex
                    nodeList.Add(DirectCast(e.Value.Accept(Me), VBS.TypeSyntax))
                    If Not e.IsLast Then
                        separators.Add(CommaToken.WithConvertedTrailingTriviaFrom(csSeparators(e.Index)))
                    End If
                Next
                Dim openParenTokenWithTrivia As SyntaxToken = OpenParenToken.WithConvertedTriviaFrom(node.LessThanToken)
                Dim closeParenTokenWithTrivia As SyntaxToken = CloseParenToken.WithConvertedTriviaFrom(node.GreaterThanToken)
                RestructureNodesAndSeparators(openParenTokenWithTrivia, nodeList, separators, closeParenTokenWithTrivia)
                Return Factory.TypeArgumentList(
                        openParenTokenWithTrivia,
                        OfKeyword.WithTrailingTrivia(VBSpaceTrivia),
                        Factory.SeparatedList(nodeList, separators),
                        closeParenTokenWithTrivia)
            End Function

        End Class

    End Class

End Namespace