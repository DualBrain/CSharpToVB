﻿' Licensed to the .NET Foundation under one or more agreements.
' The .NET Foundation licenses this file to you under the MIT license.

Friend Enum vbErrors
        None = 0
        ReturnWOGoSub = 3
        IllegalFuncCall = 5
        Overflow = 6
        OutOfMemory = 7
        OutOfBounds = 9
        ArrayLocked = 10
        DivByZero = 11
        TypeMismatch = 13
        OutOfStrSpace = 14
        ExprTooComplex = 16
        CantContinue = 17
        UserInterrupt = 18
        ResumeWOErr = 20
        OutOfStack = 28
        UNDONE = 29
        UndefinedProc = 35
        TooManyClients = 47
        DLLLoadErr = 48
        DLLBadCallingConv = 49
        InternalError = 51
        BadFileNameOrNumber = 52
        FileNotFound = 53
        BadFileMode = 54
        FileAlreadyOpen = 55
        IOError = 57
        FileAlreadyExists = 58
        BadRecordLen = 59
        DiskFull = 61
        EndOfFile = 62
        BadRecordNum = 63
        TooManyFiles = 67
        DevUnavailable = 68
        PermissionDenied = 70
        DiskNotReady = 71
        DifferentDrive = 74
        PathFileAccess = 75
        PathNotFound = 76
        ObjNotSet = 91
        IllegalFor = 92
        BadPatStr = 93
        CantUseNull = 94
        UserDefined = 95
        AdviseLimit = 96
        BadCallToFriendFunction = 97
        CantPassPrivateObject = 98
        DLLCallException = 99
        DoesntImplementICollection = 100
        Abort = 287
        InvalidFileFormat = 321
        CantCreateTmpFile = 322
        InvalidResourceFormat = 325
        InvalidPropertyValue = 380
        InvalidPropertyArrayIndex = 381
        SetNotSupportedAtRuntime = 382
        SetNotSupported = 383
        NeedPropertyArrayIndex = 385
        SetNotPermitted = 387
        GetNotSupportedAtRuntime = 393
        GetNotSupported = 394
        PropertyNotFound = 422
        NoSuchControlOrProperty = 423
        NotObject = 424
        CantCreateObject = 429
        OLENotSupported = 430
        OLEFileNotFound = 432
        OLENoPropOrMethod = 438
        OLEAutomationError = 440
        LostTLB = 442
        OLENoDefault = 443
        ActionNotSupported = 445
        NamedArgsNotSupported = 446
        LocaleSettingNotSupported = 447
        NamedParamNotFound = 448
        ParameterNotOptional = 449
        FuncArityMismatch = 450
        NotEnum = 451
        InvalidOrdinal = 452
        InvalidDllFunctionName = 453
        CodeResourceNotFound = 454
        CodeResourceLockError = 455
        DuplicateKey = 457
        InvalidTypeLibVariable = 458
        ObjDoesNotSupportEvents = 459
        InvalidClipboardFormat = 460
        IdentNotMember = 461
        ServerNotFound = 462
        ObjNotRegistered = 463
        InvalidPicture = 481
        PrinterError = 482
        CantSaveFileToTemp = 735
        SearchTextNotFound = 744
        ReplacementsTooLong = 746

        NotYetImplemented = 32768
        FileNotFoundWithName = 40243
        CantFindDllEntryPoint = 59201

        SeekErr = 32771
        ReadFault = 32772
        WriteFault = 32773
        BadFunctionId = 32774
        FileLockViolation = 32775
        ShareRequired = 32789
        BufferTooSmall = 32790
        InvDataRead = 32792
        UnsupFormat = 32793
        RegistryAccess = 32796
        LibNotRegistered = 32797
        Usage = 32799
        UndefinedType = 32807
        QualifiedNameDisallowed = 32808
        InvalidState = 32809
        WrongTypeKind = 32810
        ElementNotFound = 32811
        AmbiguousName = 32812
        ModNameConflict = 32813
        UnknownLcid = 32814
        BadModuleKind = 35005
        NoContainingLib = 35009
        BadTypeId = 35010
        BadLibId = 35011
        Eof = 35012
        SizeTooBig = 35013
        ExpectedFuncNotModule = 35015
        ExpectedFuncNotRecord = 35016
        ExpectedFuncNotProject = 35017
        ExpectedFuncNotVar = 35018
        ExpectedTypeNotProj = 35019
        UnsuitableFuncPropMatch = 35020
        BrokenLibRef = 35021
        UnsupportedTypeLibFeature = 35022
        ModuleAsType = 35024
        InvalidTypeInfoKind = 35025
        InvalidTypeLibFunction = 35026
        OperationNotAllowedInDll = 40035
        CompileError = 40036
        CantEvalWatch = 40037
        MissingVbaTypeLib = 40038
        UserReset = 40040
        MissingEndBrack = 40041
        IncorrectTypeChar = 40042
        InvalidNumLit = 40043
        IllegalChar = 40044
        IdTooLong = 40045
        StatementTooComplex = 40046
        ExpectedTokens = 40047
        InconsistentPropFuncs = 40067
        CircularType = 40068
        AccessViolation = &H80004003 'This is E_POINTER.  This is what VB6 returns from err.Number when calling into a .NET assembly that throws an AccessViolation
        LastTrappable = ReplacementsTooLong
    End Enum

    ' Implements error utilities for Basic
    Friend NotInheritable Class ExceptionUtils

        ' Prevent creation.
        Private Sub New()
        End Sub

        Friend Const DISP_E_UNKNOWNNAME As Integer = &H80020006I
        Friend Const DISP_E_NOTACOLLECTION As Integer = &H80020011I

        Friend Shared Function VbMakeExceptionEx(number As Integer, sMsg As String) As Exception
            Dim vBDefinedError As Boolean

            VbMakeExceptionEx = BuildException(number, sMsg, vBDefinedError)

            If vBDefinedError Then
            End If

        End Function

        Friend Shared Function BuildException(Number As Integer, Description As String, ByRef VBDefinedError As Boolean) As Exception

            VBDefinedError = True

            Select Case Number

                Case vbErrors.None

                Case vbErrors.ReturnWOGoSub,
                    vbErrors.ResumeWOErr,
                    vbErrors.CantUseNull,
                    vbErrors.DoesntImplementICollection
                    Return New InvalidOperationException(Description)

                Case vbErrors.IllegalFuncCall,
                    vbErrors.NamedParamNotFound,
                    vbErrors.NamedArgsNotSupported,
                    vbErrors.ParameterNotOptional
                    Return New ArgumentException(Description)

                Case vbErrors.OLENoPropOrMethod
                    Return New MissingMemberException(Description)

                Case vbErrors.Overflow
                    Return New OverflowException(Description)

                Case vbErrors.OutOfMemory, vbErrors.OutOfStrSpace
                    Return New OutOfMemoryException(Description)

                Case vbErrors.OutOfBounds
                    Return New IndexOutOfRangeException(Description)

                Case vbErrors.DivByZero
                    Return New DivideByZeroException(Description)

                Case vbErrors.TypeMismatch
                    Return New InvalidCastException(Description)

                Case vbErrors.OutOfStack
                    Return New StackOverflowException(Description)

                Case vbErrors.DLLLoadErr
                    Return New TypeLoadException(Description)

                Case vbErrors.FileNotFound
                    Return New IO.FileNotFoundException(Description)

                Case vbErrors.EndOfFile
                    Return New IO.EndOfStreamException(Description)

                Case vbErrors.IOError,
                    vbErrors.BadFileNameOrNumber,
                    vbErrors.BadFileMode,
                    vbErrors.FileAlreadyOpen,
                    vbErrors.FileAlreadyExists,
                    vbErrors.BadRecordLen,
                    vbErrors.DiskFull,
                    vbErrors.BadRecordNum,
                    vbErrors.TooManyFiles,
                    vbErrors.DevUnavailable,
                    vbErrors.PermissionDenied,
                    vbErrors.DiskNotReady,
                    vbErrors.DifferentDrive,
                    vbErrors.PathFileAccess
                    Return New IO.IOException(Description)

                Case vbErrors.PathNotFound,
                    vbErrors.OLEFileNotFound
                    Return New IO.FileNotFoundException(Description)

                Case vbErrors.ObjNotSet
                    Return New NullReferenceException(Description)

                Case vbErrors.PropertyNotFound
                    Return New MissingFieldException(Description)

                Case vbErrors.CantCreateObject,
                    vbErrors.ServerNotFound
                    Return New Exception(Description)

                Case vbErrors.AccessViolation
                    Return New AccessViolationException() 'We never want a custom description here.  Use the localized message that comes for free inside the exception

                Case Else
                    'Fall below to default
                    VBDefinedError = False
                    Return New Exception(Description)
            End Select

            VBDefinedError = False
            Return New Exception(Description)

        End Function

        ''' <summary>
        ''' Return a new instance of ArgumentException with the message from resource file and the Exception.ArgumentName property set.
        ''' </summary>
        ''' <param name="ArgumentName">The name of the argument (parameter). Not localized.</param>
        ''' <param name="ResourceID">The resource ID. Use CompilerServices.ResID.xxx</param>
        ''' <param name="PlaceHolders">Strings that will replace place holders in the resource string, if any.</param>
        ''' <returns>A new instance of ArgumentException.</returns>
        ''' <remarks>This is the preferred way to construct an argument exception.</remarks>
        Friend Shared Function GetArgumentExceptionWithArgName(ArgumentName As String,
                                                               ResourceID As String,
                                                               ParamArray PlaceHolders() As String) As ArgumentException

        Return New ArgumentException(String.Format(ResourceID, PlaceHolders), ArgumentName)
    End Function

        ''' <summary>
        ''' Return a new instance of ArgumentNullException with message: "Argument cannot be Nothing."
        ''' </summary>
        ''' <param name="ArgumentName">The name of the argument (parameter). Not localized.</param>
        ''' <returns>A new instance of ArgumentNullException.</returns>
        Friend Shared Function GetArgumentNullException(ArgumentName As String) As ArgumentNullException

            Return New ArgumentNullException(ArgumentName, "Argument Null Exception")
        End Function

        ''' <summary>
        ''' Return a new instance of ArgumentNullException with the message from resource file.
        ''' </summary>
        ''' <param name="ArgumentName">The name of the argument (parameter). Not localized.</param>
        ''' <param name="ResourceID">The resource ID. Use CompilerServices.ResID.xxx</param>
        ''' <param name="PlaceHolders">Strings that will replace place holders in the resource string, if any.</param>
        ''' <returns>A new instance of ArgumentNullException.</returns>
        Friend Shared Function GetArgumentNullException(ArgumentName As String,
                                                        ResourceID As String,
                                                        ParamArray PlaceHolders() As String) As ArgumentNullException

        Return New ArgumentNullException(ArgumentName, String.Format(ResourceID, PlaceHolders))
    End Function

        ''' <summary>
        ''' Return a new instance of IO.DirectoryNotFoundException with the message from resource file.
        ''' </summary>
        ''' <param name="ResourceID">The resource ID. Use CompilerServices.ResID.xxx</param>
        ''' <param name="PlaceHolders">Strings that will replace place holders in the resource string, if any.</param>
        ''' <returns>A new instance of IO.DirectoryNotFoundException.</returns>
        Friend Shared Function GetDirectoryNotFoundException(ResourceID As String,
                                                             ParamArray PlaceHolders() As String) As IO.DirectoryNotFoundException

        Return New IO.DirectoryNotFoundException(String.Format(ResourceID,
                                                               PlaceHolders))
    End Function

        ''' <summary>
        ''' Return a new instance of IO.FileNotFoundException with the message from resource file.
        ''' </summary>
        ''' <param name="FileName">The file name (path) of the not found file.</param>
        ''' <param name="ResourceID">The resource ID. Use CompilerServices.ResID.xxx</param>
        ''' <param name="PlaceHolders">Strings that will replace place holders in the resource string, if any.</param>
        ''' <returns>A new instance of IO.FileNotFoundException.</returns>
        Friend Shared Function GetFileNotFoundException(FileName As String,
                                                        ResourceID As String,
                                                        ParamArray PlaceHolders() As String) As IO.FileNotFoundException

        Return New IO.FileNotFoundException(String.Format(ResourceID, PlaceHolders), FileName)
    End Function

        ''' <summary>
        ''' Return a new instance of InvalidOperationException with the message from resource file.
        ''' </summary>
        ''' <param name="ResourceID">The resource ID. Use CompilerServices.ResID.xxx</param>
        ''' <param name="PlaceHolders">Strings that will replace place holders in the resource string, if any.</param>
        ''' <returns>A new instance of InvalidOperationException.</returns>
        Friend Shared Function GetInvalidOperationException(ResourceID As String,
                                                            ParamArray PlaceHolders() As String) As InvalidOperationException

        Return New InvalidOperationException(String.Format(ResourceID, PlaceHolders))
    End Function

        ''' <summary>
        ''' Return a new instance of IO.IOException with the message from resource file.
        ''' </summary>
        ''' <param name="ResourceID">The resource ID. Use CompilerServices.ResID.xxx</param>
        ''' <param name="PlaceHolders">Strings that will replace place holders in the resource string, if any.</param>
        ''' <returns>A new instance of IO.IOException.</returns>
        Friend Shared Function GetIOException(ResourceID As String, ParamArray PlaceHolders() As String) As IO.IOException

        Return New IO.IOException(String.Format(ResourceID, PlaceHolders))
    End Function

        ''' <summary>
        ''' Return a new instance of Win32Exception with the message from resource file and the last Win32 error.
        ''' </summary>
        ''' <param name="ResourceID">The resource ID. Use CompilerServices.ResID.xxx</param>
        ''' <param name="PlaceHolders">Strings that will replace place holders in the resource string, if any.</param>
        ''' <returns>A new instance of Win32Exception.</returns>
        ''' <remarks>There is no way to exclude the Win32 error so this function will call Marshal.GetLastWin32Error all the time.</remarks>

        Friend Shared Function GetWin32Exception(ResourceID As String, ParamArray PlaceHolders() As String) As ComponentModel.Win32Exception

        Return New ComponentModel.Win32Exception(Runtime.InteropServices.Marshal.GetLastWin32Error(), String.Format(ResourceID, PlaceHolders))
    End Function

    End Class
