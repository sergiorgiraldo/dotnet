Public Class Form2

    Private Sub MonthCalendar1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MonthCalendar1.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.M Then
            Me.Hide()
            Form1.Show()
            e.Handled = True
        ElseIf e.KeyCode = System.Windows.Forms.Keys.R Then
            Process.Start("C:\Users\segiral\Documents\datasRestritas.pdf")
        ElseIf e.KeyCode = System.Windows.Forms.Keys.G Then
            Dim res As String = InputBox("Ir para data (formato: DD/MM/YYYY)", "Ir para")
            Dim data As Date
            If (Date.TryParseExact(res, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentUICulture, Globalization.DateTimeStyles.AssumeLocal, data)) Then
                MonthCalendar1.SelectionStart = data
                MonthCalendar1.SelectionEnd = data
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.X Then
            Environment.Exit(0)
        Else
            e.Handled = False
        End If
    End Sub

    Private Sub Form2_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Environment.Exit(0)
    End Sub

    Private Sub Form2_Activated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        MonthCalendar1.SelectionStart = New Date(Form1.Year, 1, 1)
        MonthCalendar1.SelectionEnd = New Date(Form1.Year, 12, 31)
    End Sub
End Class