﻿' Instat-R
' Copyright (C) 2015
'
' This program is free software: you can redistribute it and/or modify
' it under the terms of the GNU General Public License as published by
' the Free Software Foundation, either version 3 of the License, or
' (at your option) any later version.
'
' This program is distributed in the hope that it will be useful,
' but WITHOUT ANY WARRANTY; without even the implied warranty of
' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
' GNU General Public License for more details.
'
' You should have received a copy of the GNU General Public License k
' along with this program.  If not, see <http://www.gnu.org/licenses/>.

Imports System.ComponentModel
Imports System.Globalization
Imports System.Threading
Imports instat.Translations

Public Class dlgOptions
    Dim clsInstatOptions As New InstatOptions
    Public strComment As String = "code generated by the dialog"
    Public strCurrLanguage As String
    Private Panels As New List(Of Panel)()
    Private VisiblePanel As Panel = Nothing
    'Define the Fonts dialog (only one)
    Dim dlgFont As New FontDialog
    Dim bFirstLoad As Boolean = True

    Private Sub dlgOptions_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        autoTranslate(Me)
        If bFirstLoad Then
            setView()
            setDefaults()
            bFirstLoad = False
        End If
    End Sub

    Private Sub setView()
        ' Expand all tree nodes.
        trOptions.ExpandAll()

        ' Move the Panels out of the TabControl.
        tbcOptions.Visible = False
        For Each page As TabPage In tbcOptions.TabPages
            ' Add the Panel to the list.
            Dim the_panel As Panel = DirectCast(page.Controls(0), Panel)
            Panels.Add(the_panel)

            ' Reparent and move the Panel.
            the_panel.Parent = tbcOptions.Parent
            the_panel.Location = tbcOptions.Location
            the_panel.Visible = False
        Next page

        ' Display the first panel.
        DisplayPanel(0)
    End Sub

    ' Display the appropriate Panel.
    Private Sub DisplayPanel(ByVal index As Integer)
        If (Panels.Count < 1) Then Return

        ' If this is the same Panel, do nothing.
        If (VisiblePanel Is Panels(index)) Then Return

        ' Hide the previously visible Panel.
        If (VisiblePanel IsNot Nothing) Then VisiblePanel.Visible = False

        ' Display the appropriate Panel.
        Panels(index).Visible = True
        VisiblePanel = Panels(index)
    End Sub

    'Handles click of the tree node
    Private Sub trOptions_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles trOptions.AfterSelect
        Dim index As Integer = Integer.Parse(e.Node.Tag.ToString())
        DisplayPanel(index)
    End Sub

    Private Sub setDefaults()
        ' Load saved values.
        'clsInstatOption.LoadAllSettings(Application.ProductName, Me)
        txtComment.Text = strComment
        strCurrLanguage = Thread.CurrentThread.CurrentCulture.ThreeLetterISOLanguageName
        rdoEnglish.Checked = False
        rdoFrench.Checked = False
        rdoKiswahili.Checked = False

        Select Case strCurrLanguage
            Case "eng"
                rdoEnglish.Checked = True
            Case "fra"
                rdoFrench.Checked = True
            Case "swa"
                rdoKiswahili.Checked = True
        End Select
        cmdApply.Enabled = False
    End Sub

    Private Sub cmdCancel_Click(sender As Object, e As EventArgs) Handles cmdCancel.Click
        Me.Close()
    End Sub

    Private Sub cmdOk_Click(sender As Object, e As EventArgs) Handles cmdOk.Click
        cmdApply_Click(sender, e)
        Me.Close()
    End Sub

    Private Sub cmdApply_Click(sender As Object, e As EventArgs) Handles cmdApply.Click
        strComment = txtComment.Text
        If rdoEnglish.Checked Then
            Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")
            Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
            strCurrLanguage = "eng"
        End If
        'Sets the lines to be read
        dlgImportDataset.setLinesToRead(nudNoLines.Value)
        If rdoFrench.Checked Then
            Thread.CurrentThread.CurrentCulture = New CultureInfo("fr-FR")
            Thread.CurrentThread.CurrentUICulture = New CultureInfo("fr-FR")
            strCurrLanguage = "fra"
        End If

        If rdoKiswahili.Checked Then
            Thread.CurrentThread.CurrentCulture = New CultureInfo("sw-KE")
            Thread.CurrentThread.CurrentUICulture = New CultureInfo("sw-KE")
            strCurrLanguage = "swa"
        End If
        cmdApply.Enabled = False
        'cmdOk.Enabled = False
        autoTranslate(Me)

        If frmMain.Visible Then
            autoTranslate(frmMain)
        End If

        If frmCommand.Visible Then
            autoTranslate(frmCommand)
        End If

        If frmEditor.Visible Then
            autoTranslate(frmEditor)
        End If

        If frmLog.Visible Then
            autoTranslate(frmLog)
        End If

        If frmMetaData.Visible Then
            autoTranslate(frmMetaData)
        End If

        If frmScript.Visible Then
            autoTranslate(frmScript)
        End If

        If frmVariables.Visible Then
            autoTranslate(frmVariables)
        End If
        'disables the command after running it
        cmdApply.Enabled = False
    End Sub

    Private Sub rdoKiswahili_CheckedChanged(sender As Object, e As EventArgs) Handles rdoKiswahili.CheckedChanged
        cmdApply.Enabled = True
    End Sub

    Private Sub rdoFrench_CheckedChanged(sender As Object, e As EventArgs) Handles rdoFrench.CheckedChanged
        cmdApply.Enabled = True
    End Sub

    Private Sub rdoEnglish_CheckedChanged(sender As Object, e As EventArgs) Handles rdoEnglish.CheckedChanged
        cmdApply.Enabled = True
    End Sub

    Private Sub txtComment_TextChanged(sender As Object, e As EventArgs) Handles txtComment.TextChanged
        cmdApply.Enabled = True
    End Sub

    Private Sub cmdScriptChange_Click(sender As Object, e As EventArgs) Handles cmdScriptChange.Click
        dlgFont.ShowColor = True
        dlgFont.MaxSize = 15
        dlgFont.MinSize = 8
        dlgFont.Font = frmMain.clsRLink.fScript
        dlgFont.Color = frmMain.clsRLink.clrScript
        If dlgFont.ShowDialog = DialogResult.OK Then
            cmdApply.Enabled = True
            frmMain.clsRLink.setFormatScript(dlgFont.Font, dlgFont.Color)
            dlgFont.Reset()
        End If
    End Sub

    Private Sub cmdOutputChange_Click(sender As Object, e As EventArgs) Handles cmdOutputChange.Click
        dlgFont.ShowColor = True
        dlgFont.MaxSize = 15
        dlgFont.MinSize = 8
        dlgFont.Font = frmMain.clsRLink.fOutput
        dlgFont.Color = frmMain.clsRLink.clrOutput
        If dlgFont.ShowDialog = DialogResult.OK Then
            frmMain.clsRLink.setFormatOutput(dlgFont.Font, dlgFont.Color)
            dlgFont.Reset()
        End If
    End Sub

    Private Sub cmdCommentsChange_Click(sender As Object, e As EventArgs) Handles cmdCommentsChange.Click
        dlgFont.ShowColor = True
        dlgFont.MaxSize = 15
        dlgFont.MinSize = 8
        dlgFont.Font = frmMain.clsRLink.fComments
        dlgFont.Color = frmMain.clsRLink.clrComments
        If dlgFont.ShowDialog = DialogResult.OK Then
            frmMain.clsRLink.setFormatComment(dlgFont.Font, dlgFont.Color)
            dlgFont.Reset()
        End If
    End Sub

    Private Sub nudNoLines_ValueChanged(sender As Object, e As EventArgs) Handles nudNoLines.ValueChanged
        cmdApply.Enabled = True
    End Sub

    Private Sub chkIncludeDefaultParams_CheckedChanged(sender As Object, e As EventArgs) Handles chkIncludeDefaultParams.CheckedChanged
        frmMain.clsInstatOptions.bIncludeRDefaultParameters = chkIncludeDefaultParams.Checked
    End Sub

    Private Sub dlgOptions_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        'to save the values
        'clsInstatOptions.SaveAllSettings(Application.ProductName, Me)
    End Sub

    Private Sub cmdOutput_Click(sender As Object, e As EventArgs) Handles cmdOutput.Click
        cmdOutputChange_Click(sender, e)
    End Sub

    Private Sub cmdRComments_Click(sender As Object, e As EventArgs) Handles cmdRComments.Click
        cmdCommentsChange_Click(sender, e)
    End Sub

    Private Sub cmdScript_Click(sender As Object, e As EventArgs) Handles cmdScript.Click
        cmdScriptChange_Click(sender, e)
    End Sub
End Class