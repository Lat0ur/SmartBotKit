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


' ReSharper disable once CheckNamespace

Namespace My

    
    <Runtime.CompilerServices.CompilerGeneratedAttribute(),  _
     CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.7.0.0"),  _
     ComponentModel.EditorBrowsableAttribute(ComponentModel.EditorBrowsableState.Advanced)>  _
    Partial Friend NotInheritable Class MySettings
        Inherits Configuration.ApplicationSettingsBase
        
        Private Shared defaultInstance As MySettings = CType(Configuration.ApplicationSettingsBase.Synchronized(New MySettings()),MySettings)
        
#Region "My.Settings Auto-Save Functionality"
#If _MyType = "WindowsForms" Then
    Private Shared addedHandler As Boolean

    Private Shared addedHandlerLockObject As New Object

    <Diagnostics.DebuggerNonUserCodeAttribute(), ComponentModel.EditorBrowsableAttribute(ComponentModel.EditorBrowsableState.Advanced)> _
    Private Shared Sub AutoSaveSettings(sender As Object, e As EventArgs)
        If My.Application.SaveMySettingsOnExit Then
            My.Settings.Save()
        End If
    End Sub
#End If
#End Region
        
        Public Shared ReadOnly Property [Default]() As MySettings
            Get
                
#If _MyType = "WindowsForms" Then
               If Not addedHandler Then
                    SyncLock addedHandlerLockObject
                        If Not addedHandler Then
                            AddHandler My.Application.Shutdown, AddressOf AutoSaveSettings
                            addedHandler = True
                        End If
                    End SyncLock
                End If
#End If
                Return defaultInstance
            End Get
        End Property
        
        <Configuration.UserScopedSettingAttribute(),  _
         Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Configuration.DefaultSettingValueAttribute("")>  _
        Public Property Setting() As String
            Get
                Return CType(Me("Setting"),String)
            End Get
            Set
                Me("Setting") = value
            End Set
        End Property
    End Class
End Namespace

' ReSharper disable once CheckNamespace

Namespace My

    
    <Global.Microsoft.VisualBasic.HideModuleNameAttribute(),  _
     Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Runtime.CompilerServices.CompilerGeneratedAttribute()>  _
    Friend Module MySettingsProperty
        
        <ComponentModel.Design.HelpKeywordAttribute("My.Settings")>  _
        Friend ReadOnly Property Settings() As Global.SmartBotKit.Plugins.My.MySettings
            Get
                Return Global.SmartBotKit.Plugins.My.MySettings.Default
            End Get
        End Property
    End Module
End Namespace
