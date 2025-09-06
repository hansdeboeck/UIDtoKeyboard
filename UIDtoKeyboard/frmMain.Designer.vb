<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.cbxReaderList = New System.Windows.Forms.ComboBox()
        Me.btnRefreshReader = New System.Windows.Forms.Button()
        Me.btnStartMonitor = New System.Windows.Forms.Button()
        Me.txtInputSpace = New System.Windows.Forms.TextBox()
        Me.btnStopMonitor = New System.Windows.Forms.Button()
        Me.txtReadingMode = New System.Windows.Forms.TextBox()
        Me.lblReadingMode = New System.Windows.Forms.Label()
        Me.NotifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.Label1 = New System.Windows.Forms.Label()
        Me.versionlabel = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'cbxReaderList
        '
        Me.cbxReaderList.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.cbxReaderList.FormattingEnabled = True
        Me.cbxReaderList.Location = New System.Drawing.Point(84, 22)
        Me.cbxReaderList.Name = "cbxReaderList"
        Me.cbxReaderList.Size = New System.Drawing.Size(351, 26)
        Me.cbxReaderList.TabIndex = 0
        '
        'btnRefreshReader
        '
        Me.btnRefreshReader.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnRefreshReader.Location = New System.Drawing.Point(441, 21)
        Me.btnRefreshReader.Name = "btnRefreshReader"
        Me.btnRefreshReader.Size = New System.Drawing.Size(87, 27)
        Me.btnRefreshReader.TabIndex = 2
        Me.btnRefreshReader.Text = "Refresh"
        Me.btnRefreshReader.UseVisualStyleBackColor = True
        '
        'btnStartMonitor
        '
        Me.btnStartMonitor.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnStartMonitor.Location = New System.Drawing.Point(22, 281)
        Me.btnStartMonitor.Name = "btnStartMonitor"
        Me.btnStartMonitor.Size = New System.Drawing.Size(99, 44)
        Me.btnStartMonitor.TabIndex = 3
        Me.btnStartMonitor.Text = "Herstart"
        Me.btnStartMonitor.UseVisualStyleBackColor = True
        '
        'txtInputSpace
        '
        Me.txtInputSpace.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.txtInputSpace.Location = New System.Drawing.Point(22, 88)
        Me.txtInputSpace.Multiline = True
        Me.txtInputSpace.Name = "txtInputSpace"
        Me.txtInputSpace.Size = New System.Drawing.Size(506, 187)
        Me.txtInputSpace.TabIndex = 4
        '
        'btnStopMonitor
        '
        Me.btnStopMonitor.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnStopMonitor.Location = New System.Drawing.Point(127, 281)
        Me.btnStopMonitor.Name = "btnStopMonitor"
        Me.btnStopMonitor.Size = New System.Drawing.Size(401, 44)
        Me.btnStopMonitor.TabIndex = 5
        Me.btnStopMonitor.Text = "Stop Melo"
        Me.btnStopMonitor.UseVisualStyleBackColor = True
        '
        'txtReadingMode
        '
        Me.txtReadingMode.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.txtReadingMode.Location = New System.Drawing.Point(84, 55)
        Me.txtReadingMode.Name = "txtReadingMode"
        Me.txtReadingMode.Size = New System.Drawing.Size(444, 24)
        Me.txtReadingMode.TabIndex = 8
        '
        'lblReadingMode
        '
        Me.lblReadingMode.AutoSize = True
        Me.lblReadingMode.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblReadingMode.Location = New System.Drawing.Point(22, 59)
        Me.lblReadingMode.Name = "lblReadingMode"
        Me.lblReadingMode.Size = New System.Drawing.Size(46, 18)
        Me.lblReadingMode.TabIndex = 9
        Me.lblReadingMode.Text = "Mode"
        '
        'NotifyIcon1
        '
        Me.NotifyIcon1.Icon = CType(resources.GetObject("NotifyIcon1.Icon"), System.Drawing.Icon)
        Me.NotifyIcon1.Text = "MeloFare"
        Me.NotifyIcon1.Visible = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label1.Location = New System.Drawing.Point(22, 26)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(56, 18)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "Reader"
        '
        'versionlabel
        '
        Me.versionlabel.AutoSize = True
        Me.versionlabel.Location = New System.Drawing.Point(22, 328)
        Me.versionlabel.Name = "versionlabel"
        Me.versionlabel.Size = New System.Drawing.Size(41, 13)
        Me.versionlabel.TabIndex = 11
        Me.versionlabel.Text = "version"
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(538, 350)
        Me.Controls.Add(Me.versionlabel)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblReadingMode)
        Me.Controls.Add(Me.txtReadingMode)
        Me.Controls.Add(Me.btnStopMonitor)
        Me.Controls.Add(Me.txtInputSpace)
        Me.Controls.Add(Me.btnStartMonitor)
        Me.Controls.Add(Me.btnRefreshReader)
        Me.Controls.Add(Me.cbxReaderList)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmMain"
        Me.ShowInTaskbar = False
        Me.Text = "MeloFare"
        Me.WindowState = System.Windows.Forms.FormWindowState.Minimized
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents cbxReaderList As ComboBox
    Friend WithEvents btnRefreshReader As Button
    Friend WithEvents btnStartMonitor As Button
    Friend WithEvents txtInputSpace As TextBox
    Friend WithEvents btnStopMonitor As Button
    Friend WithEvents txtReadingMode As TextBox
    Friend WithEvents lblReadingMode As Label
    Friend WithEvents NotifyIcon1 As NotifyIcon
    Friend WithEvents Label1 As Label
    Friend WithEvents versionlabel As Label
End Class
