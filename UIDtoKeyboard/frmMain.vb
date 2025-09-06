Imports PCSC
Imports PCSC.Iso7816
Imports PCSC.Monitoring

Public Class frmMain

    Private Shared ReadOnly _contextFactory As IContextFactory = ContextFactory.Instance
    Private _hContext As ISCardContext
    Private shouldExit As Boolean = False

    Dim readerName As String
    Dim readingMode As String
    Dim isstart As Boolean = False


    Function loadReaderList()
        Dim readerList As String()
        Try
            cbxReaderList.DataSource = Nothing

            _hContext = _contextFactory.Establish(SCardScope.System)
            readerList = _hContext.GetReaders()
            _hContext.Release()

            If readerList.Length > 0 Then
                cbxReaderList.DataSource = readerList
            Else
                MessageBox.Show("No card reader detected!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If

            Return True
        Catch ex As Exceptions.PCSCException
            MessageBox.Show("Error: getReaderList() : " & ex.Message & " (" & ex.SCardError.ToString() & ")")
            Return False
        End Try
    End Function

    Dim monitor

    Private Sub startMonitor()
        Dim monitorFactory As MonitorFactory = MonitorFactory.Instance
        monitor = monitorFactory.Create(SCardScope.System)
        AttachToAllEvents(monitor)
        monitor.Start(cbxReaderList.Text)

        readerName = cbxReaderList.Text
        readingMode = txtReadingMode.Text
    End Sub

    Private Sub AttachToAllEvents(monitor As ISCardMonitor)
        AddHandler monitor.CardInserted, AddressOf cardInit
    End Sub

    Sub cardInit(eventName As SCardMonitor, unknown As CardStatusEventArgs)
        If readingMode = 1 OrElse readingMode = 2 Then
            SendUID4Byte()
        ElseIf readingMode = 3 OrElse readingMode = 4 Then
            SendUID7Byte()
        ElseIf readingMode = 5 OrElse readingMode = 6 Then
            SendUID8H10D()
        End If
    End Sub

    ' Als het formulier via het kruisje wordt afgesloten, verberg het opnieuw in de taakbalk
    Private Sub frmMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If e.CloseReason = CloseReason.UserClosing AndAlso Not shouldExit Then
            e.Cancel = True
            Me.ShowInTaskbar = False
            Me.Hide()
        End If
    End Sub

    Public Function checkForUpdates()
        'Doe een call naar https://assets.deboeck.dev/vba/meloflare/versie.txt
        Dim client As New Net.WebClient()
        Dim latestVersion As String = client.DownloadString("https://assets.deboeck.dev/vba/meloflare/versie.txt").Trim()
        Dim currentVersion As String = Application.ProductVersion
        If latestVersion <> currentVersion Then
            Dim result As DialogResult = MessageBox.Show("A new version is available. Do you want to download it?", "Update available", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
            If result = DialogResult.Yes Then
                'Open de downloadpagina
                Process.Start("https://www.deboeck.dev/melo")
            Else
                stopApp()
            End If
        Else


        End If
    End Function

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        checkForUpdates()
        loadReaderList()
        versionlabel.Text = "v " & Application.ProductVersion

        If My.Computer.FileSystem.FileExists("C:\deboeck\config.txt") Then
            Dim lines() As String = IO.File.ReadAllLines("C:\deboeck\config.txt")
            If lines.Length >= 2 Then
                Dim reader As String = lines(0).Trim()
                Dim mode As String = lines(1).Trim()
                If cbxReaderList.Items.Contains(reader) Then
                    cbxReaderList.SelectedItem = reader
                End If
                If mode = "1" OrElse mode = "2" OrElse mode = "3" OrElse mode = "4" OrElse mode = "5" OrElse mode = "6" Then
                    txtReadingMode.Text = mode
                End If
            End If
        Else
            If Not My.Computer.FileSystem.DirectoryExists("C:\deboeck") Then
                My.Computer.FileSystem.CreateDirectory("C:\deboeck")
            End If
            Dim defaultLines() As String = {"GHI NC001 0", "4"}
            IO.File.WriteAllLines("C:\deboeck\config.txt", defaultLines)
        End If

        'als textreading mode leeg is val terug op 4
        If txtReadingMode.Text = "" Then
            txtReadingMode.Text = "4"
        End If

        NotifyIcon1.BalloonTipTitle = "Melo is running"
        NotifyIcon1.BalloonTipText = "Melo is running in the background. Double click the icon to open the application."
        NotifyIcon1.ShowBalloonTip(3000)




        startMonitor()
        Me.Hide()
        Me.Visible = False


    End Sub

    Private Sub btnRefreshReader_Click(sender As Object, e As EventArgs) Handles btnRefreshReader.Click
        loadReaderList()
    End Sub

    Private Sub btnStartMonitor_Click(sender As Object, e As EventArgs) Handles btnStartMonitor.Click
        If txtReadingMode.Text <> 1 AndAlso txtReadingMode.Text <> 2 AndAlso txtReadingMode.Text <> 3 AndAlso txtReadingMode.Text <> 4 AndAlso txtReadingMode.Text <> 5 AndAlso txtReadingMode.Text <> 6 Then
            MessageBox.Show("Error: Reading mode not match the preset.")
        Else
            If isstart = True Then
                monitor.Cancel()
            End If
            startMonitor()
            isstart = True
        End If
    End Sub

    Public Function stopApp()
        shouldExit = True
        Me.Close()
    End Function
    Private Sub btnStopMonitor_Click(sender As Object, e As EventArgs) Handles btnStopMonitor.Click
        stopApp()
    End Sub

    Function SendUID4Byte()
        Try
            Using context = _contextFactory.Establish(SCardScope.System)
                Using rfidReader = context.ConnectReader(readerName, SCardShareMode.Shared, SCardProtocol.Any)
                    Using rfidReader.Transaction(SCardReaderDisposition.Leave)

                        Dim apdu As Byte() = {&HFF, &HCA, &H0, &H0, &H4}
                        Dim sendPci = SCardPCI.GetPci(rfidReader.Protocol)
                        Dim receivePci = New SCardPCI()

                        Dim receiveBuffer = New Byte(255) {}
                        Dim command = apdu.ToArray()
                        Dim bytesReceived = rfidReader.Transmit(sendPci, command, command.Length, receivePci, receiveBuffer, receiveBuffer.Length)
                        Dim responseApdu = New ResponseApdu(receiveBuffer, bytesReceived, IsoCase.Case2Short, rfidReader.Protocol)

                        If readingMode = 1 Then
                            Dim uid As String = BitConverter.ToString(responseApdu.GetData())
                            uid = uid.Replace("-", "")

                            SendKeys.SendWait(uid + "{ENTER}")
                        ElseIf readingMode = 2 Then
                            Dim uid As Byte() = New Byte(3) {}
                            Dim revuid As Byte() = New Byte(3) {}
                            Array.Copy(responseApdu.GetData(), uid, 4)
                            Array.Copy(uid, revuid, 4)
                            Array.Reverse(revuid, 0, 4)

                            Dim uid2 As String = BitConverter.ToString(revuid)
                            uid2 = uid2.Replace("-", "")

                            SendKeys.SendWait(uid2 + "{ENTER}")
                        End If
                    End Using
                End Using
            End Using
        Catch
            'Error Handling should be developed
        End Try

        Return True
    End Function

    Function SendUID8H10D()
        Try
            Using context = _contextFactory.Establish(SCardScope.System)
                Using rfidReader = context.ConnectReader(readerName, SCardShareMode.Shared, SCardProtocol.Any)
                    Using rfidReader.Transaction(SCardReaderDisposition.Leave)

                        Dim apdu As Byte() = {&HFF, &HCA, &H0, &H0, &H4}
                        Dim sendPci = SCardPCI.GetPci(rfidReader.Protocol)
                        Dim receivePci = New SCardPCI()

                        Dim receiveBuffer = New Byte(255) {}
                        Dim command = apdu.ToArray()
                        Dim bytesReceived = rfidReader.Transmit(sendPci, command, command.Length, receivePci, receiveBuffer, receiveBuffer.Length)
                        Dim responseApdu = New ResponseApdu(receiveBuffer, bytesReceived, IsoCase.Case2Short, rfidReader.Protocol)


                        Dim uid As String
                        If readingMode = 6 Then

                            uid = BitConverter.ToUInt32(responseApdu.GetData(), 0)

                        ElseIf readingMode = 5 Then
                            Dim revuid As Byte() = New Byte(4) {}

                            Array.Copy(responseApdu.GetData(), revuid, 4)
                            Array.Reverse(revuid, 0, 4)

                            uid = BitConverter.ToUInt32(revuid, 0)

                        End If
                        SendKeys.SendWait(uid + "{ENTER}")
                    End Using
                End Using
            End Using
        Catch
            Console.WriteLine("Erreur 8H10D")
            'Error Handling should be developed
        End Try

        Return True
    End Function





    Function SendUID7Byte()
        Try
            Using context = _contextFactory.Establish(SCardScope.System)
                Using rfidReader = context.ConnectReader(readerName, SCardShareMode.Shared, SCardProtocol.Any)
                    Using rfidReader.Transaction(SCardReaderDisposition.Leave)

                        Dim apdu As Byte() = {&HFF, &HCA, &H0, &H0, &H7}
                        Dim sendPci = SCardPCI.GetPci(rfidReader.Protocol)
                        Dim receivePci = New SCardPCI()

                        Dim receiveBuffer = New Byte(255) {}
                        Dim command = apdu.ToArray()
                        Dim bytesReceived = rfidReader.Transmit(sendPci, command, command.Length, receivePci, receiveBuffer, receiveBuffer.Length)
                        Dim responseApdu = New ResponseApdu(receiveBuffer, bytesReceived, IsoCase.Case2Short, rfidReader.Protocol)

                        If readingMode = 3 Then
                            Dim uid As String = BitConverter.ToString(responseApdu.GetData())
                            uid = uid.Replace("-", "")

                            SendKeys.SendWait(uid + "{ENTER}")
                        ElseIf readingMode = 4 Then
                            Dim uid As Byte() = New Byte(6) {}
                            Dim revuid As Byte() = New Byte(6) {}
                            Array.Copy(responseApdu.GetData(), uid, 7)
                            Array.Copy(uid, revuid, 7)
                            Array.Reverse(revuid, 0, 7)

                            Dim uid2 As String = BitConverter.ToString(revuid)
                            uid2 = uid2.Replace("-", "")
                            SendKeys.SendWait(uid2 + "{ENTER}")
                        End If
                    End Using
                End Using
            End Using
        Catch
            'Error Handling should be developed
        End Try

        Return True
    End Function

    Private Sub NotifyIcon1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles NotifyIcon1.Click

        Me.ShowInTaskbar = True
        Me.Focus()
        Me.Show()
    End Sub



    Private Sub txtReadingMode_TextChanged(sender As Object, e As EventArgs) Handles txtReadingMode.TextChanged
        'sla de waarde op in het tekstbestand
        If Not My.Computer.FileSystem.DirectoryExists("C:\deboeck") Then
            My.Computer.FileSystem.CreateDirectory("C:\deboeck")
        End If
        Dim lines() As String = {cbxReaderList.Text, txtReadingMode.Text}
        IO.File.WriteAllLines("C:\deboeck\config.txt", lines)
        'als de monitor loopt, herstarten
        If isstart = True Then
            monitor.Cancel()
            startMonitor()
        End If
    End Sub


End Class
