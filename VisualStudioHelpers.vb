Imports System
Imports EnvDTE
Imports EnvDTE80
Imports EnvDTE90
Imports EnvDTE90a
Imports EnvDTE100
Imports System.Diagnostics
Imports System.Runtime.InteropServices

Public Module Helpers
    Sub ExcludeExceptions()
        Dim dbg As EnvDTE100.Debugger5 = DTE.Debugger

        Dim exSettings As EnvDTE90.ExceptionSettings = dbg.ExceptionGroups.Item("Common Language Runtime Exceptions")

        Dim exSetting As EnvDTE90.ExceptionSetting = exSettings.Item("Common Language Runtime Exceptions")

        If Not exSetting.BreakWhenThrown Then
            exSettings.SetBreakWhenThrown(True, exSetting)
        End If


        Dim xmlException = exSettings.Item("System.Xml.XmlException")
        exSettings.SetBreakWhenThrown(False, xmlException)


        Dim exceptionName As String = "System.Configuration.ConfigurationErrorsException"
        Dim configurationException
        Try
            configurationException = exSettings.Item(Name)
        Catch ex As COMException When ex.ErrorCode = -2147352565 ' DISP_E_BADINDEX
            configurationException = exSettings.NewException(Name, 0)
        End Try

        exSettings.SetBreakWhenThrown(False, configurationException)


    End Sub

    Sub CollapseSummaryAndExpandCode()
        DTE.ExecuteCommand("Edit.CollapsetoDefinitions")
        DTE.SuppressUI = True
        Dim objSelection As TextSelection = DTE.ActiveDocument.Selection
        objSelection.StartOfDocument()
        Do While objSelection.FindText("{", vsFindOptions.vsFindOptionsMatchInHiddenText)
        Loop
        objSelection.StartOfDocument()
        DTE.SuppressUI = False
    End Sub
End Module
