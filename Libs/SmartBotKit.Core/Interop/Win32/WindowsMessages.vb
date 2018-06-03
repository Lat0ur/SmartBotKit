﻿
#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " Windows Messages "

Namespace SmartBotKit.Interop.Win32

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' The system sends or posts a system-defined message when it communicates with an application. 
    ''' <para></para>
    ''' It uses these messages to control the operations of applications and to provide input and other information for applications to process. 
    ''' <para></para>
    ''' An application can also send or post system-defined messages.
    ''' <para></para>
    ''' Applications generally use these messages to control the operation of control windows created by using preregistered window classes.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <remarks>
    ''' <see href="http://msdn.microsoft.com/en-us/library/windows/desktop/ms644927%28v=vs.85%29.aspx"/>
    ''' </remarks>
    ''' ----------------------------------------------------------------------------------------------------
    Public Enum WindowsMessages As Integer

        ' *****************************************************************************
        '                            WARNING!, NEED TO KNOW...
        '
        '  THIS ENUMERATION IS PARTIALLY DEFINED TO MEET THE PURPOSES OF THIS API
        ' *****************************************************************************

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' The <see cref="Null"/> message performs no operation.
        ''' <para></para>
        ''' An application sends the <see cref="Null"/> message if it wants to 
        ''' post a message that the recipient window will ignore.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Null = &H0

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Posted when the user presses a hot key registered by the <see cref="NativeMethods.RegisterHotKey"/> function. 
        ''' <para></para>
        ''' The message is placed at the top of the message queue associated with the thread that registered the hot key.
        ''' <para></para>
        ''' 
        ''' <c>wParam</c> 
        ''' The identifier of the hot key that generated the message.
        ''' If the message was generated by a system-defined hot key.
        ''' <para></para>
        ''' 
        ''' <c>lParam</c> 
        ''' The low-order word specifies the keys that were to be pressed in 
        ''' combination with the key specified by the high-order word to generate the 
        ''' <see cref="WindowsMessages.WM_Hotkey"/> message.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <remarks>
        ''' <see href="http://msdn.microsoft.com/en-us/library/windows/desktop/ms646279%28v=vs.85%29.aspx"/>
        ''' </remarks>
        ''' ----------------------------------------------------------------------------------------------------
        WM_Hotkey = &H312

    End Enum

End Namespace

#End Region