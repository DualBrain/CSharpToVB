﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.42000
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On


Namespace CSharpToVBApp.My_Project
    
    <Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(),  _
     Global.System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "16.6.0.0")>  _
    Partial Friend NotInheritable Class Settings
        Inherits Global.System.Configuration.ApplicationSettingsBase
        
        Private Shared defaultInstance As Settings = CType(Global.System.Configuration.ApplicationSettingsBase.Synchronized(New Settings()),Settings)
        
        Public Shared ReadOnly Property [Default]() As Settings
            Get
                Return defaultInstance
            End Get
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property ColorizeInput() As Boolean
            Get
                Return CType(Me("ColorizeInput"),Boolean)
            End Get
            Set
                Me("ColorizeInput") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property ColorizeOutput() As Boolean
            Get
                Return CType(Me("ColorizeOutput"),Boolean)
            End Get
            Set
                Me("ColorizeOutput") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0")>  _
        Public Property ConversionDelay() As Integer
            Get
                Return CType(Me("ConversionDelay"),Integer)
            End Get
            Set
                Me("ConversionDelay") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("")>  _
        Public Property DefaultProjectDirectory() As String
            Get
                Return CType(Me("DefaultProjectDirectory"),String)
            End Get
            Set
                Me("DefaultProjectDirectory") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("NETCOREAPP")>  _
        Public Property Framework() As String
            Get
                Return CType(Me("Framework"),String)
            End Get
            Set
                Me("Framework") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Property IgnoreFileList() As Global.System.Collections.Specialized.StringCollection
            Get
                Return CType(Me("IgnoreFileList"),Global.System.Collections.Specialized.StringCollection)
            End Get
            Set
                Me("IgnoreFileList") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("")>  _
        Public Property LastPath() As String
            Get
                Return CType(Me("LastPath"),String)
            End Get
            Set
                Me("LastPath") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(),
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),
         Global.System.Configuration.DefaultSettingValueAttribute("")>
        Public Property LastProject() As String
            Get
                Return CType(Me("LastProject"), String)
            End Get
            Set
                Me("LastProject") = Value
            End Set
        End Property


        <Global.System.Configuration.UserScopedSettingAttribute(),
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),
         Global.System.Configuration.DefaultSettingValueAttribute("")>
        Public Property LastSolution() As String
            Get
                Return CType(Me("LastSolution"), String)
            End Get
            Set
                Me("LastSolution") = Value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Property MRU_Data() As Global.System.Collections.Specialized.StringCollection
            Get
                Return CType(Me("MRU_Data"),Global.System.Collections.Specialized.StringCollection)
            End Get
            Set
                Me("MRU_Data") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(),
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),
         Global.System.Configuration.DefaultSettingValueAttribute("Text")>
        Public Property OptionCompare() As String
            Get
                Return CType(Me("OptionCompare"), String)
            End Get
            Set
                Me("OptionCompare") = Value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(),
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),
         Global.System.Configuration.DefaultSettingValueAttribute("True")>
        Public Property OptionCompareIncludeInCode() As Boolean
            Get
                Return CType(Me("OptionCompareIncludeInCode"), Boolean)
            End Get
            Set
                Me("OptionCompareIncludeInCode") = Value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(),
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),
         Global.System.Configuration.DefaultSettingValueAttribute("On")>
        Public Property OptionExplicit() As String
            Get
                Return CType(Me("OptionExplicit"), String)
            End Get
            Set
                Me("OptionExplicit") = Value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(),
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),
         Global.System.Configuration.DefaultSettingValueAttribute("True")>
        Public Property OptionExplicitIncludeInCode() As Boolean
            Get
                Return CType(Me("OptionExplicitIncludeInCode"), Boolean)
            End Get
            Set
                Me("OptionExplicitIncludeInCode") = Value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(),
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),
         Global.System.Configuration.DefaultSettingValueAttribute("Off")>
        Public Property OptionInfer() As String
            Get
                Return CType(Me("OptionInfer"), String)
            End Get
            Set
                Me("OptionInfer") = Value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(),
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),
         Global.System.Configuration.DefaultSettingValueAttribute("True")>
        Public Property OptionInferIncludeInCode() As Boolean
            Get
                Return CType(Me("OptionInferIncludeInCode"), Boolean)
            End Get
            Set
                Me("OptionInferIncludeInCode") = Value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(),
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),
         Global.System.Configuration.DefaultSettingValueAttribute("On")>
        Public Property OptionStrict() As String
            Get
                Return CType(Me("OptionStrict"), String)
            End Get
            Set
                Me("OptionStrict") = Value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(),
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),
         Global.System.Configuration.DefaultSettingValueAttribute("True")>
        Public Property OptionStrictIncludeInCode() As Boolean
            Get
                Return CType(Me("OptionStrictIncludeInCode"), Boolean)
            End Get
            Set
                Me("OptionStrictIncludeInCode") = Value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property PauseConvertOnSuccess() As Boolean
            Get
                Return CType(Me("PauseConvertOnSuccess"),Boolean)
            End Get
            Set
                Me("PauseConvertOnSuccess") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property ShowDestinationLineNumbers() As Boolean
            Get
                Return CType(Me("ShowDestinationLineNumbers"),Boolean)
            End Get
            Set
                Me("ShowDestinationLineNumbers") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property ShowSourceLineNumbers() As Boolean
            Get
                Return CType(Me("ShowSourceLineNumbers"),Boolean)
            End Get
            Set
                Me("ShowSourceLineNumbers") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property SkipAutoGenerated() As Boolean
            Get
                Return CType(Me("SkipAutoGenerated"),Boolean)
            End Get
            Set
                Me("SkipAutoGenerated") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property SkipBinAndObjFolders() As Boolean
            Get
                Return CType(Me("SkipBinAndObjFolders"),Boolean)
            End Get
            Set
                Me("SkipBinAndObjFolders") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property SkipTestResourceFiles() As Boolean
            Get
                Return CType(Me("SkipTestResourceFiles"),Boolean)
            End Get
            Set
                Me("SkipTestResourceFiles") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property StartFolderConvertFromLastFile() As Boolean
            Get
                Return CType(Me("StartFolderConvertFromLastFile"),Boolean)
            End Get
            Set
                Me("StartFolderConvertFromLastFile") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property UpgradeRequired() As Boolean
            Get
                Return CType(Me("UpgradeRequired"),Boolean)
            End Get
            Set
                Me("UpgradeRequired") = value
            End Set
        End Property
    End Class
End Namespace
