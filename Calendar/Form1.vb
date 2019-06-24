Imports System.Reflection
Imports System.IO

Public Class Form1
    Inherits System.Windows.Forms.Form

    Public Year As Integer

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        MonthCalendar1.SetDate(Now.AddMonths(-1))
    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents MonthCalendar1 As System.Windows.Forms.MonthCalendar
    Friend WithEvents Label1 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.MonthCalendar1 = New System.Windows.Forms.MonthCalendar()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'MonthCalendar1
        '
        Me.MonthCalendar1.CalendarDimensions = New System.Drawing.Size(3, 1)
        Me.MonthCalendar1.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MonthCalendar1.Location = New System.Drawing.Point(5, 5)
        Me.MonthCalendar1.MaxSelectionCount = 1
        Me.MonthCalendar1.Name = "MonthCalendar1"
        Me.MonthCalendar1.ShowToday = False
        Me.MonthCalendar1.ShowWeekNumbers = True
        Me.MonthCalendar1.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(0, 315)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(1204, 26)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "G: Ir para /  Shift+Up : - 1 mês / Shift+Down : + 1 mês / Control+Up : - 1 ano / " &
    "Control+Down : + 1 ano / H : Hoje / Y: Ver ano inteiro / X : Fechar"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(1204, 341)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.MonthCalendar1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Calendário 3 meses"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        MonthCalendar1.SelectionStart = Now()
        MonthCalendar1.SelectionEnd = Now()

        Year = MonthCalendar1.SelectionStart.Year
    End Sub

    Private Sub DrawVerticalText()
        Dim formGraphics As System.Drawing.Graphics = Me.CreateGraphics()
        Dim drawString As String = "Sample Text"
        Dim drawFont As System.Drawing.Font = New System.Drawing.Font("Arial", 16)
        Dim drawBrush As System.Drawing.SolidBrush = New System.Drawing.SolidBrush(System.Drawing.Color.Black)
        Dim x As Double = 150.0F
        Dim y As Double = 50.0F
        Dim drawFormat As System.Drawing.StringFormat = New System.Drawing.StringFormat(StringFormatFlags.DirectionVertical)
        formGraphics.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat)
        drawFont.Dispose()
        drawBrush.Dispose()
        formGraphics.Dispose()
    End Sub


    Private Sub MonthCalendar1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MonthCalendar1.KeyDown
        Dim d As DateTime
        If e.KeyCode = System.Windows.Forms.Keys.G Then
            Dim res As String = InputBox("Ir para data (formato: DD/MM/YYYY)", "Ir para")
            Dim data As Date
            If (Date.TryParseExact(res, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentUICulture, Globalization.DateTimeStyles.AssumeLocal, data)) Then
                MonthCalendar1.SelectionStart = data
                MonthCalendar1.SelectionEnd = data
            End If
        ElseIf e.Shift And e.KeyCode = System.Windows.Forms.Keys.Down Then
            d = MonthCalendar1.SelectionRange.Start.AddMonths(1)
            MonthCalendar1.SelectionStart = d
            MonthCalendar1.SelectionEnd = d
            Year = MonthCalendar1.SelectionStart.Year
            e.Handled = True
        ElseIf e.Shift And e.KeyCode = System.Windows.Forms.Keys.Up Then
            d = MonthCalendar1.SelectionRange.Start.AddMonths(-1)
            MonthCalendar1.SelectionStart = d
            MonthCalendar1.SelectionEnd = d
            Year = MonthCalendar1.SelectionStart.Year
            e.Handled = True
        ElseIf e.Control And e.KeyCode = System.Windows.Forms.Keys.Down Then
            d = MonthCalendar1.SelectionRange.Start.AddYears(1)
            MonthCalendar1.SelectionStart = d
            MonthCalendar1.SelectionEnd = d
            Year = MonthCalendar1.SelectionStart.Year
            e.Handled = True
        ElseIf e.Control And e.KeyCode = System.Windows.Forms.Keys.Up Then
            d = MonthCalendar1.SelectionRange.Start.AddYears(-1)
            MonthCalendar1.SelectionStart = d
            MonthCalendar1.SelectionEnd = d
            Year = MonthCalendar1.SelectionStart.Year
            e.Handled = True
        ElseIf e.KeyCode = System.Windows.Forms.Keys.H Then
            d = Now()
            MonthCalendar1.SelectionStart = d
            MonthCalendar1.SelectionEnd = d
            Year = MonthCalendar1.SelectionStart.Year
            e.Handled = True
        ElseIf e.KeyCode = System.Windows.Forms.Keys.Y Then
            Me.Hide()
            Form2.Show()
            e.Handled = True
        ElseIf e.KeyCode = System.Windows.Forms.Keys.X Then
            Environment.Exit(0)
        Else
            e.Handled = False
        End If
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub
End Class
