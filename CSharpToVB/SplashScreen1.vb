﻿' Licensed to the .NET Foundation under one or more agreements.
' The .NET Foundation licenses this file to you under the MIT license.
' See the LICENSE file in the project root for more information.

Imports System.Globalization

Public NotInheritable Class SplashScreen1

    'TODO: This form can easily be set as the splash screen for the application by going to the "Application" tab
    '  of the Project Designer ("Properties" under the "Project" menu).

    Private Sub SplashScreen1_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Set up the dialog text at runtime according to the application's assembly information.

        'TODO: Customize the application's assembly information in the "Application" pane of the project
        '  properties dialog (under the "Project" menu).

        'Application title
        ApplicationTitle.Text = If(String.IsNullOrWhiteSpace(My.Info.Title), IO.Path.GetFileNameWithoutExtension(My.Info.AssemblyName), My.Info.Title)

        'Format the version information using the text set into the Version control at design time as the
        '  formatting string.  This allows for effective localization if desired.
        '  Build and revision information could be included by using the following code and changing the
        '  Version control's design-time text to "Version {0}.{1:00}.{2}.{3}" or something similar.  See
        '  String.Format() in Help for more information.
        '
        Version.Text = String.Format(CultureInfo.InvariantCulture, Version.Text, My.Info.Version.Major, My.Info.Version.Minor, My.Info.Version.Build, My.Info.Version.Revision)

        'Copyright info
        Copyright.Text = My.Info.Copyright
    End Sub

End Class
