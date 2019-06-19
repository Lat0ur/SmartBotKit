﻿
#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " Imports "

Imports Microsoft.VisualBasic.FileIO

Imports System.Collections.Generic
Imports System.IO
Imports System.Linq

Imports SmartBot.Plugins
Imports SmartBot.Plugins.API

Imports SmartBotKit.Interop
Imports SmartBotKit.ReservedUse

#End Region

#Region " GarbageCleanerPlugin "

' ReSharper disable once CheckNamespace

Namespace GarbageCleaner


    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' A plugin that cleans temporary files generated by SmartBot and its plugins.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <seealso cref="Plugin"/>
    ''' ----------------------------------------------------------------------------------------------------
    Public NotInheritable Class GarbageCleanerPlugin : Inherits Plugin

#Region " Properties "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the plugin's data container.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' The plugin's data container.
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        Public Shadows ReadOnly Property DataContainer As GarbageCleanerPluginData
            Get
                Return DirectCast(MyBase.DataContainer, GarbageCleanerPluginData)
            End Get
        End Property

#End Region

#Region " Private Fields "

        ' ReSharper disable InconsistentNaming

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Keeps track of the last <see cref="GarbageCleanerPluginData.Enabled"/> value.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private lastEnabled As Boolean

        ' ReSharper restore InconsistentNaming

#End Region

#Region " Constructors "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Initializes a new instance of the <see cref="GarbageCleanerPlugin"/> class.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Public Sub New()
            UpdateUtil.RunUpdaterExecutable()
            Me.IsDll = True
        End Sub

#End Region

#Region " Public Methods "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Called when this <see cref="GarbageCleanerPlugin"/> is created by the SmartBot plugin manager.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Public Overrides Sub OnPluginCreated()
            Me.lastEnabled = Me.DataContainer.Enabled
            If (Me.lastEnabled) Then
                Bot.Log("[Garbage Cleaner] -> Plugin initialized.")
                If (Me.DataContainer.CleanerEvent = SmartBotEvent.Startup) Then
                    Me.CleanGarbage()
                End If
            End If
            MyBase.OnPluginCreated()
        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Called when the properties of <see cref="GarbageCleanerPluginData"/> are updated.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Public Overrides Sub OnDataContainerUpdated()
            Dim enabled As Boolean = Me.DataContainer.Enabled
            If (enabled <> Me.lastEnabled) Then
                If (enabled) Then
                    Bot.Log("[Garbage Cleaner] -> Plugin enabled.")
                Else
                    Bot.Log("[Garbage Cleaner] -> Plugin disabled.")
                End If
                Me.lastEnabled = enabled
            End If
            MyBase.OnDataContainerUpdated()
        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Called when the bot is started.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Public Overrides Sub OnStarted()
            If (Me.DataContainer.Enabled) AndAlso (Me.DataContainer.CleanerEvent = SmartBotEvent.BotStart) Then
                Me.CleanGarbage()
            End If
            MyBase.OnStarted()
        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Called when the bot is stopped.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Public Overrides Sub OnStopped()
            If (Me.DataContainer.Enabled) AndAlso (Me.DataContainer.CleanerEvent = SmartBotEvent.BotStop) Then
                Me.CleanGarbage()
            End If
            MyBase.OnStopped()
        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Releases all the Global.System.Resources.used by this <see cref="GarbageCleanerPlugin"/> instance.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Public Overrides Sub Dispose()
            If (Me.DataContainer.Enabled) AndAlso (Me.DataContainer.CleanerEvent = SmartBotEvent.Exit) Then
                Me.CleanGarbage()
            End If
            MyBase.Dispose()
        End Sub

#End Region

#Region " Private Methods "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Cleans the temporary files and folders.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private Sub CleanGarbage()

            If Not File.Exists(Path.Combine(SmartBotUtil.SmartBotDir.FullName, "SBAPI.dll")) Then
                Throw New DirectoryNotFoundException("SmartBot's root directory cannot be found.")
            End If

            Dim verboseMode As Boolean = Me.DataContainer.VerboseMode
            Dim minDaysDiff As Integer = Me.DataContainer.OlderThanDays

            Dim recycleOption As RecycleOption
            If Me.DataContainer.SendFilesToRecycleBin Then
                recycleOption = RecycleOption.SendToRecycleBin
            Else
                recycleOption = RecycleOption.DeletePermanently
            End If

            ' Delete seeds
            If (Me.DataContainer.DeleteSeeds) Then

                Dim seeds As New List(Of DirectoryInfo)
                ' Dim lastSeedFile As FileInfo = Nothing

                If (SmartBotUtil.SeedsDir.Exists) Then
                    seeds.AddRange(SmartBotUtil.SeedsDir.EnumerateDirectories("*", System.IO.SearchOption.TopDirectoryOnly))

                    If (Me.DataContainer.CleanerEvent = SmartBotEvent.BotStart) OrElse (Me.DataContainer.CleanerEvent = SmartBotEvent.BotStop) Then

                        Dim seedDirToKeep As DirectoryInfo = (From seedDir As DirectoryInfo In seeds
                                                              Order By seedDir.LastWriteTime Descending).FirstOrDefault()

                        If (seedDirToKeep IsNot Nothing) Then
                            seeds.Remove(seedDirToKeep)
                        End If

                    End If

                End If

                For Each seed As DirectoryInfo In seeds

                    Dim daysDiff As Integer = CInt((Date.Now - seed.CreationTime).TotalDays)
                    If (daysDiff >= minDaysDiff) Then
                        Try
                            My.Computer.FileSystem.DeleteDirectory(seed.FullName, UIOption.OnlyErrorDialogs, recycleOption)
                            If (verboseMode) Then
                                Bot.Log(
                                    $"[Garbage Cleaner] -> Seed directory deleted: '{seed.Name}'. Older than {daysDiff _
                                           } days.")
                            End If
                        Catch ex As Exception
                            ' Ignore all.
                        End Try
                    End If
                Next seed
            End If

            ' Delete SmartBot temporary files
            If (Me.DataContainer.DeleteSmartBotLogs) Then
                Dim logs As New List(Of FileInfo)
                If (SmartBotUtil.CrashesDir.Exists) Then
                    logs.AddRange(SmartBotUtil.CrashesDir.EnumerateFiles("*.txt", System.IO.SearchOption.TopDirectoryOnly))
                End If
                If (SmartBotUtil.LogsDir.Exists) Then
                    logs.AddRange(SmartBotUtil.LogsDir.EnumerateFiles("*.txt", System.IO.SearchOption.TopDirectoryOnly))
                End If
                logs.Add(New FileInfo(Path.Combine(SmartBotUtil.SmartBotDir.FullName, "UpdaterLog.txt")))
                logs.Add(New FileInfo(Path.Combine(SmartBotUtil.SmartBotDir.FullName, "CompileErrorsDiscoverCC.txt")))
                logs.Add(New FileInfo(Path.Combine(SmartBotUtil.SmartBotDir.FullName, "CompileErrorsProfile.txt")))
                logs.Add(New FileInfo(Path.Combine(SmartBotUtil.SmartBotDir.FullName, "SmartBotDiagTool_report.txt")))
                logs.Add(New FileInfo(Path.Combine(SmartBotUtil.SmartBotDir.FullName, "Stats.ini")))

                For Each log As FileInfo In logs
                    Dim daysDiff As Integer = CInt((Date.Now - log.CreationTime).TotalDays)
                    If (daysDiff >= minDaysDiff) Then
                        Try
                            My.Computer.FileSystem.DeleteFile(log.FullName, UIOption.OnlyErrorDialogs, recycleOption)
                            If (verboseMode) Then
                                Bot.Log(
                                    $"[Garbage Cleaner] -> Log file deleted: '{log.Name}'. Older than {daysDiff} days.")
                            End If
                        Catch ex As Exception
                            ' Ignore all.
                        End Try
                    End If
                Next log
            End If

            ' Delete Soviet Mulligan Kit (SMK) temporary files
            If (Me.DataContainer.DeleteSovietLogs) Then
                Dim logs As New List(Of FileInfo)
                Dim sovietDir As New DirectoryInfo(Path.Combine(SmartBotUtil.LogsDir.FullName, "Soviet"))
                If (sovietDir.Exists) Then
                    logs.AddRange(sovietDir.EnumerateFiles("*.log", System.IO.SearchOption.AllDirectories))
                End If
                logs.Add(New FileInfo(Path.Combine(SmartBotUtil.SmartBotDir.FullName, "UpdateInfo.ini")))
                For Each log As FileInfo In logs
                    Dim daysDiff As Integer = CInt((Date.Now - log.CreationTime).TotalDays)
                    If (daysDiff >= minDaysDiff) Then
                        Try
                            My.Computer.FileSystem.DeleteFile(log.FullName, UIOption.OnlyErrorDialogs, recycleOption)
                            If (verboseMode) Then
                                Bot.Log(
                                    $"[Garbage Cleaner] -> Log file deleted: '{log.Name}'. Older than {daysDiff} days.")
                            End If
                        Catch ex As Exception
                            ' Ignore all.
                        End Try
                    End If
                Next log
            End If

            ' Delete BattleTag Crawler temporary files
            If (Me.DataContainer.DeleteBattleTagCrawlertLogs) Then
                Dim logs As New List(Of FileInfo)
                Dim battleTagDir As New DirectoryInfo(Path.Combine(SmartBotUtil.LogsDir.FullName, "BattleTag Crawler"))
                If (battleTagDir.Exists) Then
                    logs.AddRange(battleTagDir.EnumerateFiles("*.csv", System.IO.SearchOption.TopDirectoryOnly))
                End If

                For Each log As FileInfo In logs
                    Dim daysDiff As Integer = CInt((Date.Now - log.CreationTime).TotalDays)
                    If (daysDiff >= minDaysDiff) Then
                        Try
                            My.Computer.FileSystem.DeleteFile(log.FullName, UIOption.OnlyErrorDialogs, recycleOption)
                            If (verboseMode) Then
                                Bot.Log(
                                    $"[Garbage Cleaner] -> Log file deleted: '{log.Name}'. Older than {daysDiff} days.")
                            End If
                        Catch ex As Exception
                            ' Ignore all.
                        End Try
                    End If
                Next log
            End If

            ' Delete screenshots
            If (Me.DataContainer.DeleteScreenshots) Then
                Dim screenshots As New List(Of FileInfo)
                If (SmartBotUtil.ScreenshotsDir.Exists) Then
                    screenshots.AddRange(SmartBotUtil.ScreenshotsDir.EnumerateFiles("*.png", System.IO.SearchOption.TopDirectoryOnly))
                End If

                For Each screenshot As FileInfo In screenshots
                    Dim daysDiff As Integer = CInt((Date.Now - screenshot.CreationTime).TotalDays)
                    If (daysDiff >= minDaysDiff) Then
                        Try
                            My.Computer.FileSystem.DeleteFile(screenshot.FullName, UIOption.OnlyErrorDialogs, recycleOption)
                            If (verboseMode) Then
                                Bot.Log(
                                    $"[Garbage Cleaner] -> Screenshot deleted: '{screenshot.Name}'. Older than {daysDiff _
                                           } days.")
                            End If
                        Catch ex As Exception
                            ' Ignore all.
                        End Try
                    End If
                Next screenshot
            End If

        End Sub

#End Region

    End Class

End Namespace

#End Region
