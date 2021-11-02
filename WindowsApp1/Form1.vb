Imports System
Imports System.IO.Ports
Imports System.Threading

Public Class Form1
    Dim comport As String
    'now_port and old_port use to check if device connect to serial port'
    Dim now_port As Integer = 0
    Dim old_port As Integer = 0

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Interval = 1000
        Timer1.Start()
        Button2.Enabled = False
        Button3.Enabled = False

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        now_port = My.Computer.Ports.SerialPortNames.Count
        If now_port <> old_port Then
            ComboBox1.Items.Clear()
            For Each item As String In My.Computer.Ports.SerialPortNames
                ComboBox1.Items.Add(item)
            Next
            old_port = now_port
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.SelectedItem <> "" Then
            comport = ComboBox1.SelectedItem
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If comport = "" Then Return
        If SerialPort1.IsOpen Then SerialPort1.Close()
        If Button1.Text = "Connect" Then
            SerialPort1.PortName = comport
            SerialPort1.BaudRate = 115200
            Try
                SerialPort1.Open()
                Button1.Text = "Disconnect"
                ComboBox1.Enabled = False
                Button2.Enabled = True
                Button3.Enabled = False
                MsgBox("Port " + comport + " is connected to your PC and ready to use", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "COEKKU Spirometry Test")
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Else
            Button1.Text = "Connect"
            ComboBox1.Enabled = True
            Button2.Enabled = False
            Button3.Enabled = False
            MsgBox("Port " + comport + " is Disconnected", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "COEKKU Spirometry Test")
        End If
    End Sub
End Class
