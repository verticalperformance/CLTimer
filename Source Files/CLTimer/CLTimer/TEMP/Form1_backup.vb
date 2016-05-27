'**** CLTimer for timing Control Line races *****

'12/8 Saved as Form1_backup within VB.  DONT DO THAT! totally stuffed up Vb environment - reverted to backed up version...
'21/10 tested with displays 6 - 8.
'7/11/15 backup to word doc
' 8/11 main progam loop need fixing. Rewrote all
'12/11 Tested OK. All displays lane 1 look OK.
'       Function of screen buttons OK?
'       Aux O/P (sounder) OK.

'26/1/16 Copied to Form1_backup


'FIX
'elapsed time display at 1:00. Now OK ?
' copy DoLane1 Min -> sec to DoLane 2/3
' At sw_test, turn display 0xx xxxxx on, turn off at timerX tick



Imports System.Text
Imports System
Imports System.IO.Ports
Public Class CLTimer

    Public CalValue As Double = 0.2     'Cal value. 
    Public RunState As String
    Public StartState As String
    Public IncTime As Boolean
    Public Xmit_str As String
    Public d_no As String
    Public y As Integer
    Public SaveIt As Boolean = False
    Public Lane1Time As Double      'Start Time
    Public Lane2Time As Double
    Public Lane3Time As Double
    Public L1FinalMin As String 'Double
    Public L2FinalMin As String 'Double
    Public L3FinalMin As String 'Double
    Public L1FinalSec As String 'Double
    Public L2FinalSec As String 'Double
    Public L3FinalSec As String 'Double

    Dim UpDisplay As Boolean
    Dim DataOk As Boolean
    Dim Ln1CurrTime As Integer = 0
    Dim Ln2CurrTime As Integer = 0
    Dim Ln3CurrTime As Integer = 0
    Dim Ln4CurrTime As Integer = 0
    'Dim Ln1Cuts, Ln2Cuts, Ln3Cuts, Ln4Cuts As Integer
    Dim Ln1CurrLap, Ln2CurrLap, Ln3CurrLap, Ln4CurrLap As Integer
    Dim DispLap1 As String = "" 'Red curr laps to display
    Dim DispLap2 As String = "" 'Grn curr laps to display
    Dim DispLap3 As String = "" 'Amb curr laps to display

    Dim DataIn As String = ""
    Dim CurrTime As Double
    Dim NoInHeat As Integer
    Dim Ln1State, Ln2State, Ln3State, Ln4State As String
    'Dim Results As String
    Dim Done As Boolean = False
    'Dim IsCut As Boolean = False
    ' Dim Cuts As Integer
    ' Dim ScoreD As Double
    Dim Watch As Integer = 0
    Dim RunLoop As Boolean = False
    Dim Delay As Integer
    Dim DataOut As String
    Dim Lap200 As Boolean = False       'default is 100 laps
    Dim NoClock As Boolean = False
    Dim SwTest As Boolean = False
    Dim Update1 As Boolean = False
    Dim Update2 As Boolean = False
    Dim Update3 As Boolean = False
    'Dim FName As String
    Dim PortNo As String = "COM5"
    Dim tim8 As Boolean
    Dim x_count As Integer
    ' Dim x_count2 As Integer
    ' Dim PollOne As Boolean
    ' Dim PollAll As Boolean
    ' Dim Waiting1 As Boolean
    ' Dim Waiting2 As Boolean
    'Dim Waiting3 As Boolean
    ' Dim R1 As Boolean = False
    'Dim R2 As Boolean = False
    ' Dim R3 As Boolean = False
    ' Dim R4 As Boolean = False
    'Dim WaitTime As Double

    'Dim L1C1, L1C2, L1C3 As Integer
    'Dim L2C1, L2C2, L2C3 As Integer
    'Dim L3C1, L3C2, L3C3 As Integer
    'Dim L4C1, L4C2, L4C3 As Integer

    'Public C1, C2, C3 As Integer

    Dim StartCount As Integer
    'Dim CharCount As Integer
    Dim ClockData As String
    ' Dim ChkCuts As Boolean
    'Dim DataIn2 As String

    ' Dim FileName As String
    ' Dim ScoreType As String
    ' Dim StartPos As Integer

    Public sec1 As Double
    Public sec2 As Double
    Public temp4 As String
    Public FinMinutes As Integer
    Public minint As Integer
    Public result As String
    Public resultm As String
    Public results As String
    Public SecSize As Integer





    Dim SerialPort1 As IO.Ports.SerialPort = New IO.Ports.SerialPort(PortNo, 1200, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One)


    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        StateLn1.BackColor = Color.LightYellow
        StateLn2.BackColor = Color.LightYellow
        StateLn3.BackColor = Color.LightYellow
        RcvData.BackColor = Color.Red

        bnStart.Enabled = False
        bnSave.Enabled = False

        On Error Resume Next
        With SerialPort1
            .Open()
            .DiscardOutBuffer()
            .DiscardInBuffer()
        End With
        With SerialPort1
            If .IsOpen = False Then
                MsgBox("Com Port " + PortNo + " Not Found")
            End If
        End With

        Delay = 1
        Clear_Display()
        Timer3.Start()
        RunState = "Idle"
        StartState = "Auto"
    End Sub
    Sub MainLoop()
        Dim char1 As String
        Dim x, y As Integer

Loop1:

        My.Application.DoEvents()       'allow other events
        Select Case RunState
            Case "Idle"
                'do nothing
            Case "WaitStart"
                'do nothing, waiting for start button
            Case "AutoStart"
                If IncTime Then
                    Countdown()       'Timer 2 ticked, decrement start time
                    IncTime = False
                End If
            Case "WaitStartPress"   'Waiting for manual starter switch press
                ReadIn1()               'read serial port 1 (Timers)
                X = DataIn.Length
                y = 0
                While y < X             ' handle all char's received seperately
                    char1 = DataIn.Substring(y, 1)
                    If InStr("DX", char1) Then
                        If char1 = "D" Then
                            x = 0       'ignore anything else received
                            StartRace()
                        End If
                        DataOk = True
                    Else
                        tbError.Text = ("Data Error")
                        tbError.BackColor = Color.Red
                    End If
                    y = y + 1
                End While

            Case "Racing"
                If Done Then                'end of race
                    Timer4.Stop()
                    RunState = "Finished"
                    bnSave.Enabled = True
                Else
                    Running()
                End If


            Case "Finished"
                'wait for save clicked

        End Select
        GoTo Loop1

    End Sub
    Sub Countdown()           'Called by Timer2 every second

        Dim ch1 As String
        Dim tempsecs As Integer

        StartCount = StartCount - 1
        tbStart.Text = StartCount

        If StartCount > 0 Then
            tempsecs = StartCount


            If tempsecs > 59 Then
                If tempsecs < 70 Then
                    DataOut = "A////10"
                Else
                    DataOut = "A////1"     'minutes = 1    (/ = blank)
                End If
                tempsecs = tempsecs - 60
            ElseIf StartCount < 10 Then
                DataOut = "A//////"     'minutes = 0, sec 10's = 0    (/ = blank)
            Else
                DataOut = "A/////"     'minutes = 0, seconds = 59 to 10
            End If

            ch1 = tempsecs
            DataOut = DataOut + ch1 + "/"   'display from _ _ _ _ 2:00._ to _ _ _ _ _:_1._    _ = blank
            With SerialPort1
                .Write(DataOut)
            End With
            DataOut = ""
        Else
            StartRace()
        End If  'End of decrement 1 second count
    End Sub
    Sub StartRace()

        Timer2.Stop()
        RunState = "Racing"
        'Setup Displays
        DataOut = "A//0/0000"
        With SerialPort1
            .Write(DataOut)
        End With

        DataOut = "B//0/0000"
        With SerialPort1
            .Write(DataOut)
        End With

        DataOut = "C//0/0000"
        With SerialPort1
            .Write(DataOut)
        End With

        DataOut = "S"           'turn start sounder on
        With SerialPort1
            .Write(DataOut)
        End With

        ReadIn1()
        DataIn = ""                 'clear timer inputs

        CurrTime = My.Computer.Clock.TickCount.ToString
        CurrTime = CurrTime / 1000
        CurrTime = CurrTime + CalValue  'Add cal value so final time is correct
        Lane1Time = CurrTime      'save start time
        Lane2Time = CurrTime
        Lane3Time = CurrTime

        StateLn1.Text = "Racing"
        StateLn1.BackColor = Color.LightGreen
        StateLn2.Text = "Racing"
        StateLn2.BackColor = Color.LightGreen
        StateLn3.Text = "Racing"
        StateLn3.BackColor = Color.LightGreen
        tbStart.BackColor = Color.White
        ClkLabel.Visible = False
        bnStart.BackColor = Color.Silver
        bnStart.Enabled = False
        bnSave.Enabled = False
        If StartState = "Auto" Then
            bnStart.Text = "Start Countdown"
        Else
            bnStart.Text = "Setup Display"      'StartState = "Manual"
        End If
        lbReady.Visible = False
        tbStart.Visible = False
        tbStart.Text = ("")

        Timer3.Start()
        Timer4.Start()          'Trigger display update every 0.5 secs
        UpDisplay = False

    

    End Sub

    Sub Running()
        Dim X, y As Integer
        Dim char1 As String

            ReadIn1()               'read serial port 1 (Timers)
            X = DataIn.Length
            y = 0
            While y < X             ' handle all char's received seperately
                char1 = DataIn.Substring(y, 1)
                If InStr("ABCDX", char1) Then
                    CurrTime = My.Computer.Clock.TickCount.ToString
                    CurrTime = CurrTime / 1000
                    Select Case char1
                        Case "A"
                        DoLane1()
                        Timer3.Stop()                 'any valid char resets timeout
                        Timer3.Start()
                        Case "B"
                        DoLane2()
                        Timer3.Stop()
                        Timer3.Start()
                        Case "C"
                        DoLane3()
                        Timer3.Stop()
                        Timer3.Start()
                        Case "D"
                        'starter, ignore
                    End Select

                '             Timer3.Stop()                 'any valid char resets timeout
                '            Timer3.Start()
                Else
                    tbError.Text = ("Data Error")
                    tbError.BackColor = Color.Red
                End If
                y = y + 1
            End While

            If UpDisplay Then           'Timer 4 not still running from last xmit to clock (delay between consecutive messages)
                If Update1 Then ClockData = ClockData + "A" + DispLap1 + L1FinalMin + L1FinalSec
                If Update2 Then ClockData = ClockData + "B" + DispLap2 + L2FinalMin + L2FinalSec
                If Update3 Then ClockData = ClockData + "C" + DispLap3 + L3FinalMin + L3FinalSec
                If ClockData = "" Then
                Else
                    With SerialPort1
                        .Write(ClockData)   'send data to clock
                    End With
                End If
                ClockData = ""
                UpDisplay = False
                Timer4.Start()            'set UpDisplay true after timeout
                Update1 = False
                Update2 = False
                Update3 = False
        End If



    End Sub

    Sub DoLane1()
        '     Dim sec1 As Double
        '    Dim sec2 As Double
        '   Dim temp4 As String
        '  Dim FinMinutes As Integer
        ' Dim minint As Integer
        'Dim result As String
        ' Dim resultm As String
        ' Dim results As String
        'Dim SecSize As Integer


        If StateLn1.Text = "Racing" Then
            Ln1CurrLap = Ln1CurrLap + 1
            sec1 = CurrTime - Lane1Time     'Lane1time saved at start
            'convert seconds to Minutes / Seconds
            '          FinMinutes = sec1 \ 60              'minutes as double
            '         sec2 = sec1 - (FinMinutes * 60)     'seconds as double
            '        minint = Int(FinMinutes)             'minutes as integer
            '       resultm = Format(minint, "0")      'minutes as string
            '      results = Format(sec2, "00.00")     'seconds as string
            '     result = resultm & ":" & results
            '    Ln1Time.Text = result               'display on screen

            'convert seconds to Minutes / Seconds TAKE TWO
            SecSize = Int(sec1)
            FinMinutes = SecSize \ 60              'minutes
            sec2 = sec1 - (FinMinutes * 60)     'seconds as double
            '  minint = Int(FinMinutes)             'minutes as integer
            resultm = Format(FinMinutes, "0")      'minutes as string
            results = Format(sec2, "00.00")     'seconds as string
            result = resultm & ":" & results
            Ln1Time.Text = result               'display on screen





            'TEMP
            tbError.Text = sec1

            'TEMP
            'If sec1 > 59 Then
            'sec2 = 1    'just to put a break point. Debugging error at 1:00 displays a 1:-00 ????
            'End If

            temp4 = Ln1CurrLap
            Lane1Laps.Text = temp4
            If Ln1CurrLap < 10 Then
                DispLap1 = "//" + temp4         'DispLap1 used to send current lap count to display    
            ElseIf Ln1CurrLap < 100 Then
                DispLap1 = "/" + temp4
            Else
                DispLap1 = temp4
            End If

            L1FinalMin = resultm            'Used to send to displays
            If minint < 10 Then
                L1FinalMin = "/" + L1FinalMin 'if < 10 minutes add a blank for minute 10's
            End If


            temp4 = Format(sec2, "00.0") 'rounds value up if "00"!   
            results = Mid(temp4, 1, 2)   'get the first 2 char's   
            L1FinalSec = results
            temp4 = Format(sec2, "0.0")     'get seconds decimal
            SecSize = Len(temp4)
            results = Mid(temp4, SecSize, 1) ' get the last char
            L1FinalSec = L1FinalSec + results 'add seconds + decimal without the "." for the display


            Update1 = True                  'Flag to update the display

            If Lap200 Then
                If Ln1CurrLap = 10 Then    '** change to 200    'if final stop at 200 laps
                    Ln1State = "Finished"
                    StateLn1.Text = "Finished"
                    StateLn1.BackColor = Color.LightSkyBlue
                    AllDone()               'check if all have finished 
                End If
            ElseIf Ln1CurrLap = 100 Then    'if not final and done 100 laps
                Ln1State = "Finished"
                StateLn1.Text = "Finished"
                StateLn1.BackColor = Color.LightSkyBlue
                AllDone()                   'check if all have finished 
            End If
        End If

    End Sub
    Sub DoLane2()
        '  Dim sec1 As Double
        ' Dim sec2 As Double
        'Dim temp4 As String
        'Dim FinMinutes As Integer
        'Dim minint As Integer
        'Dim result As String
        'Dim resultm As String
        'Dim results As String
        'Dim SecSize As Integer

        If StateLn2.Text = "Racing" Then
            Ln2CurrLap = Ln2CurrLap + 1
            sec1 = CurrTime - Lane2Time     'Lane1time saved at start

            FinMinutes = sec1 \ 60              'minutes as double
            sec2 = sec1 - (FinMinutes * 60)     'seconds as double
            minint = Int(FinMinutes)             'minutes as integer
            resultm = Format(minint, "0")      'minutes as string
            results = Format(sec2, "00.00")     'seconds as string
            result = resultm & ":" & results
            Ln2Time.Text = result               'display on screen

            temp4 = Ln2CurrLap
            Lane2laps.Text = temp4
            If Ln2CurrLap < 10 Then
                DispLap2 = "//" + temp4         'DispLap2 used to send current lap count to display    
            ElseIf Ln2CurrLap < 100 Then
                DispLap2 = "/" + temp4
            Else
                DispLap2 = temp4
            End If

            L2FinalMin = resultm            'Used to send to displays
            If minint < 10 Then
                L2FinalMin = "/" + L2FinalMin 'if < 10 minutes add a blank for minute 10's
            End If


            temp4 = Format(sec2, "00.0") 'rounds value up if "00"!   
            results = Mid(temp4, 1, 2)   'get the first 2 char's   
            L2FinalSec = results
            temp4 = Format(sec2, "0.0")     'get seconds decimal
            SecSize = Len(temp4)
            results = Mid(temp4, SecSize, 1) ' get the last char
            L2FinalSec = L2FinalSec + results 'add seconds + decimal without the "." for the display

            Update2 = True                  'Flag to update the display

            If Lap200 Then
                If Ln2CurrLap = 20 Then    '** change to 200    'if final stop at 200 laps
                    'Timer5.Stop()
                    Ln2State = "Finished"
                    StateLn2.Text = "Finished"
                    StateLn2.BackColor = Color.LightSkyBlue
                    AllDone() 'check if all have finished 
                End If
            ElseIf Ln2CurrLap = 10 Then '** change to 100   'if not final and done 100 laps
                'Timer5.Stop()
                Ln2State = "Finished"
                StateLn2.Text = "Finished"
                StateLn2.BackColor = Color.LightSkyBlue
                AllDone() 'check if all have finished 
            End If
        End If

    End Sub
    Sub DoLane3()
        ' Dim sec1 As Double
        ' Dim sec2 As Double
        'Dim temp4 As String
        'Dim FinMinutes As Integer
        'Dim minint As Integer
        'Dim result As String
        'Dim resultm As String
        'Dim results As String
        'Dim SecSize As Integer


        If StateLn3.Text = "Racing" Then
            Ln3CurrLap = Ln3CurrLap + 1
            sec1 = CurrTime - Lane3Time     'Lane1time saved at start

            FinMinutes = sec1 \ 60              'minutes as double
            sec2 = sec1 - (FinMinutes * 60)     'seconds as double
            minint = Int(FinMinutes)             'minutes as integer
            resultm = Format(minint, "0")      'minutes as string
            results = Format(sec2, "00.00")     'seconds as string
            result = resultm & ":" & results
            Ln3Time.Text = result               'display on screen

            temp4 = Ln3CurrLap
            Lane3Laps.Text = temp4
            If Ln1CurrLap < 10 Then
                DispLap3 = "//" + temp4         'DispLap1 used to send current lap count to display    
            ElseIf Ln3CurrLap < 100 Then
                DispLap3 = "/" + temp4
            Else
                DispLap3 = temp4
            End If

            L3FinalMin = resultm            'Used to send to displays
            If minint < 10 Then
                L3FinalMin = "/" + L3FinalMin 'if < 10 minutes add a blank for minute 10's
            End If


            temp4 = Format(sec2, "00.0") 'rounds value up if "00"!   
            results = Mid(temp4, 1, 2)   'get the first 2 char's   
            L3FinalSec = results
            temp4 = Format(sec2, "0.0")     'get seconds decimal
            SecSize = Len(temp4)
            results = Mid(temp4, SecSize, 1) ' get the last char
            L3FinalSec = L3FinalSec + results 'add seconds + decimal without the "." for the display

            Update3 = True                  'Flag to update the display

            If Lap200 Then
                If Ln3CurrLap = 20 Then    '** change to 200    'if final stop at 200 laps
                    ' Timer6.Stop()
                    Ln3State = "Finished"
                    StateLn3.Text = "Finished"
                    StateLn3.BackColor = Color.LightSkyBlue
                    AllDone() 'check if all have finished 
                End If
            ElseIf Ln3CurrLap = 10 Then '** change to 100   'if not final and done 100 laps
                ' Timer6.Stop()
                Ln3State = "Finished"
                StateLn3.Text = "Finished"
                StateLn3.BackColor = Color.LightSkyBlue
                AllDone() 'check if all have finished 
            End If
        End If
    End Sub
    Sub AllDone() ' check if all have finished
        Select Case NoInHeat
            Case 1
                If Ln1State = "Finished" Then Done = True
            Case 2
                If Ln1State = "Finished" Then
                    If Ln2State = "Finished" Then Done = True
                End If
            Case 3
                If Ln1State = "Finished" Then
                    If Ln2State = "Finished" Then
                        If Ln3State = "Finished" Then Done = True
                    End If
                End If
        End Select

        'If Done Then
        'SaveIt = True
        'Timer4.Stop()   'Stop 0.5 sec display update
        '; If NoClock Then bnStart.BackColor = Color.White
        'End If

    End Sub
    Sub bnSetup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bnSetup.Click
        Dim Doit As Boolean = True

        If SaveIt = True Then
            If MessageBox.Show("Do You Want to Save the Last Race First?", "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                Doit = False
            End If
        End If

        If Doit Then
            If DataOk Then           ' if receiving data
                ClearFm()               'clear all text boxes from previous race
                Clear_Display()             'clear the last results
                SetNoInHeat()               'now just sets up screen
                bnStart.Enabled = True
                bnSetup.Enabled = False
                bnSave.Enabled = False
                RunState = "WaitStart"

                MainLoop()

                ' ReadNamesIn()           'Read names from clipboard
                'Name1.Text = NameNo1 '    and put into name boxes
                'Name2.Text = NameNo2
                'Name3.Text = NameNo3


                'If NameNo1 Is "" Then
                'Else : NoInHeat = 1   ' if there is a name....
                'End If
                'If NameNo2 Is "" Then
                'Else : NoInHeat = 2
                'End If
                'If NameNo3 Is "" Then
                'Else : NoInHeat = 3
                'End If

            Else : MessageBox.Show("Not Receiving Data from Timers")
            End If
        End If

    End Sub
    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        '1 Sec timer used during 60sec start
        IncTime = True
    End Sub

    Private Sub Timer3_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer3.Tick
        '3 secs time, used for monitoring data receive from timers. Gets reset while racing if other data received.
        ' While idle, handle "X" in from timers and send "X" out to displays for keep alive

        Dim char1 As String
        Dim x As Integer        'length of string
        Dim y As Integer = 0    'current char in string

        If RunState = "Racing" Then 'don't read serial port
        Else
            ReadIn1() 'read serial port
            x = DataIn.Length
            If x = 0 Then
                DataOk = False
            End If
            While y < x             ' handle all char's received seperately
                char1 = DataIn.Substring(y, 1)
                If InStr("X", char1) Then
                    DataOk = True
                End If
                y = y + 1
            End While
        End If

        If DataOk Then           'During race, Watch should get incremented every second
            RcvData.BackColor = Color.LightGreen
        End If
      
        ClockData = "X"
        With SerialPort1
            If .IsOpen Then .Write(ClockData)
        End With

    End Sub
    Private Sub bnStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bnStart.Click
        Dim RaceGo As Boolean = False

        If DataOk Then
            Select Case RunState
                Case "WaitStart"    '
                    If StartState = "Auto" Then
                        SetDisplay2()               'setup display 1 for 2:00
                        Timer2.Start()
                        Timer3.Stop()
                        RunState = "AutoStart"
                        bnStart.Text = "Stop CountDown"
                        ClkLabel.Visible = True
                        tbStart.Visible = True
                        bnStart.BackColor = Color.LightGreen
                        bnExit.Enabled = False
                        Ln1CurrLap = 0
                        Ln2CurrLap = 0
                        Ln3CurrLap = 0
                        Done = False
                        StartCount = 10 '** temp, set to 120

                    ElseIf StartState = "Manual" Then
                        SetDisplay0()               'Set displays to 00 for manual start
                        RunState = "WaitStartPress"
                        bnStart.Text = "Clear Display"
                        Ln1CurrLap = 0
                        Ln2CurrLap = 0
                        Ln3CurrLap = 0
                        Done = False
                        ' bnStart.BackColor = Color.LightGreen
                        lbReady.Visible = True
                        lbReady.BackColor = Color.LightGreen
                        bnExit.Enabled = False
                    End If

                Case "AutoStart"        'Cancel auto start count down
                    Timer2.Stop()
                    Timer3.Start()
                    RunState = "WaitStart"

                    bnStart.Text = "Start Countdown"
                    bnStart.BackColor = Color.Silver
                    tbStart.BackColor = Color.Silver
                    ClkLabel.Visible = False
                    tbStart.Text = ""
                    tbStart.Visible = False
                    bnExit.Enabled = True
                    Clear_Display()

                Case "WaitStartPress"       'quit waiting for starter
                    Clear_Display()
                    bnStart.Text = "Setup Display"
                    ' bnStart.BackColor = Color.Silver
                    lbReady.Visible = False
                    bnExit.Enabled = True
                    RunState = "WaitStart"
            End Select
        Else
            MessageBox.Show("Not Receiving Data from Timers")
        End If
    End Sub

    Sub SetDisplay2()
        'Setup display 1 to 2:00 ready for auto count down
        DataOut = "A////200/"     'send to display 1, _ _ _ _ 2:00.
        With SerialPort1
            .Write(DataOut)
        End With

    End Sub
    Sub SetDisplay0()           'Manual start Laps = 0, Time = 0:00.0
        DataOut = "A//0/0000"
        With SerialPort1
            .Write(DataOut)
        End With
        DataOut = "B//0/0000"
        With SerialPort1
            .Write(DataOut)
        End With
        DataOut = "C//0/0000"
        With SerialPort1
            .Write(DataOut)
        End With
    End Sub
    Private Sub bnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bnExit.Click
        Timer3.Stop()
        Timer2.Stop()
        Timer4.Stop()
        With SerialPort1
            If .IsOpen Then
                .Close()
            End If
        End With
        Me.Close()
    End Sub





    'All below here is old version
    '******************************************************************************************************************************








    '   Private Sub bnStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bnStart.Click
    'Dim RaceGo As Boolean = False
    '
    '   If bnStart.Text = "Stop Clock" Then 'Stop start countdown
    '      bnStart.Text = "Start Clock"
    '     Timer2.Stop()
    '    Timer3.Start()
    '   Clear_Display()
    '      tbStart.BackColor = Color.White
    '  ClkLabel.Visible = False
    ' tbStart.Text = ""
    ''tbStart.Visible = False
    ' Else
    '    If StateLn1.Text = "Ready" Then
    '       If Watch > 0 Then           ' if receiving data
    '          Ln1CurrLap = 0
    '         Ln2CurrLap = 0
    '        Ln3CurrLap = 0
    '       Done = False
    '      If NoClock Then
    ' bnStart.BackColor = Color.LightGreen
    '         lbReady.Visible = True
    '    End If
    '   RaceGo = True
    'Else : MessageBox.Show("Not Receiving Data from Timers")
    'End If
    ' Else : MessageBox.Show("Lane 1 Not Ready")
    'End If
    'End If
    'If RaceGo Then
    '   If NoClock = False Then
    '      Start()           'Go do a 2 minute countdown
    ' Else
    '** set the displays up to all 0 00:00.0
    '     DataOut = "A//000000"
    '    With SerialPort1
    '       .Write(DataOut)
    '  End With
    '           WaitY()         4/11 took out
    ' DataOut = "B//000000"
    'With SerialPort1
    '   .Write(DataOut)
    'End With
    '           WaitY()
    'DataOut = "C//000000"
    'With SerialPort1
    '   .Write(DataOut)
    '      End With
    '     Running()     'Go do a manual start
    'End If
    'End If
    ' End Sub

    Sub Start()

        tbStart.BackColor = Color.LightGreen
        ClkLabel.Visible = True
        tbStart.Visible = True
        bnStart.Text = "Stop Clock"
        StartCount = 10 '** temp, set to 120

        ' Set up display 1 to 2:00
        DataOut = "A////200/"     'send to display 1, _ _ _ _ 2:00.
        With SerialPort1
            .Write(DataOut)
        End With

        Timer3.Stop()
        Timer2.Start()      '1 second timer, calls Countdown

    End Sub


    Sub Countdown_old()           'Called by Timer2 every second

        Dim ch1 As String
        Dim tempsecs As Integer

        StartCount = StartCount - 1

        If StartCount > 0 Then
            tempsecs = StartCount
            tbStart.Text = StartCount

            If tempsecs > 60 Then
                If tempsecs < 70 Then
                    DataOut = "A////10"
                Else
                    DataOut = "A////1"     'minutes = 1    (/ = blank)
                End If
                tempsecs = tempsecs - 60
            ElseIf StartCount < 10 Then
                DataOut = "A//////"     'minutes = 0, sec 10's = 0    (/ = blank)
            Else
                DataOut = "A/////"     'minutes = 0, seconds = 59 to 10
            End If

            ch1 = tempsecs
            DataOut = DataOut + ch1 + "/"   'display from _ _ _ _ 2:00._ to _ _ _ _ _:_1._    _ = blank
            With SerialPort1
                .Write(DataOut)
            End With
            DataOut = ""
        Else

            StartRace()
            Running() 'Go run the race
        End If  'End of decrement 1 second count
    End Sub
    Sub StartRace_old()

        If NoClock = False Then         'if manual start, displays are already set up
            DataOut = "A/0000000"
            With SerialPort1
                .Write(DataOut)
            End With
            '        WaitY()
            DataOut = "B/0000000"
            With SerialPort1
                .Write(DataOut)
            End With
            '       WaitY()
            DataOut = "C/0000000"
            With SerialPort1
                .Write(DataOut)
            End With

        End If

        If NoClock Then
            bnStart.Text = ""
            lbReady.Visible = False
        Else
            bnStart.Text = "Start Clock"
        End If


        DataOut = "S"           'turn start sounder on
        With SerialPort1
            .Write(DataOut)
        End With


        ClkLabel.Visible = False
        tbStart.Visible = False
        tbStart.Text = ("")
        tbStart.BackColor = Color.White

        CurrTime = My.Computer.Clock.TickCount.ToString
        CurrTime = CurrTime / 1000
        CurrTime = CurrTime + CalValue  'Add cal value so final time is correct
        Lane1Time = CurrTime      'save start time
        Lane2Time = CurrTime
        Lane3Time = CurrTime
        StateLn1.Text = "Racing"
        StateLn1.BackColor = Color.LightGreen
        StateLn2.Text = "Racing"
        StateLn2.BackColor = Color.LightGreen
        StateLn3.Text = "Racing"
        StateLn3.BackColor = Color.LightGreen

        'Timer5.Start()
        'Timer6.Start()
        Timer2.Stop()
        Timer3.Start()
        Timer4.Start()          'Trigger display update every 0.5 secs
        UpDisplay = False
        RunLoop = True              'stop timer3_tick handling "X" input
    End Sub




    

    Private Sub DNF1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DNF1.Click
        Ln1State = "Finished"
        StateLn1.BackColor = Color.LightSkyBlue
        StateLn1.Text = "DNF"
        '     DataOut = "LAE"
        '    With SerialPort1
        '.Write(DataOut)
        'End With
        AllDone()   'check if all have finished
    End Sub
    Private Sub DNF2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DNF2.Click
        Ln2State = "Finished"
        StateLn2.BackColor = Color.LightSkyBlue
        StateLn2.Text = "DNF"
        'DataOut = "LBE"
        'With SerialPort1
        '.Write(DataOut)
        'End With
        AllDone()   'check if all have finished
    End Sub
    Private Sub DNF3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DNF3.Click
        Ln3State = "Finished"
        StateLn3.BackColor = Color.LightSkyBlue
        StateLn3.Text = "DNF"
        'DataOut = "LCE"
        'With SerialPort1
        ' .Write(DataOut)
        'End With
        AllDone()   'check if all have finished
    End Sub

    Sub bnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bnSave.Click

        Dim Str1 As String = ""
        Dim Str2 As String = ""
        Dim Str3 As String = ""
        Dim Str4 As String = ""
        Dim Str5 As String = ""
        Dim results As String = ""

        If Done Then
            ' If Ln1State = "Finished" Then
            'Str1 = Ln1Rd.Text + Chr(9) + Name1.Text + Chr(9) + Chr(9) + FileName + vbCr + vbLf
            'End If
            'If Ln2State = "Finished" Then
            'Str2 = Ln2Rd.Text + Chr(9) + Name2.Text + Chr(9) + Chr(9) + vbCr + vbLf
            'End If
            'If Ln3State = "Finished" Then
            'Str3 = Ln3Rd.Text + Chr(9) + Name3.Text + Chr(9) + Chr(9) + vbCr + vbLf
            'End If
            If Ln1State = "Finished" Then
                Str1 = StateLn1.Text + Chr(9) + Lane1Laps.Text + Chr(9) + Ln1Time.Text + Chr(9) + vbCr + vbLf
            End If
            If Ln2State = "Finished" Then
                Str2 = StateLn2.Text + Chr(9) + Lane2laps.Text + Chr(9) + Ln2Time.Text + Chr(9) + vbCr + vbLf
            End If
            If Ln3State = "Finished" Then
                Str3 = StateLn3.Text + Chr(9) + Lane3Laps.Text + Chr(9) + Ln3Time.Text + Chr(9) + vbCr + vbLf
            End If


            results = Str1 + Str2 + Str3
            Clipboard.Clear()
            Clipboard.SetText(results)

            SaveIt = False
            Clear_Display()
            RunState = "Idle"
            ' bnSave.Visible = False
            bnSetup.Enabled = True
            bnExit.Enabled = True

        Else
            MessageBox.Show("Not All Finished")
        End If

    End Sub





    Sub Running_old()
        Dim X, y As Integer
        Dim char1 As String
        ' ClockData = ""              'clear it

        Timer3.Start()              '11/9/15 put in for manual start
        ReadIn1()
        DataIn = ""                 'clear timer inputs

        Do While Done = False       'Done is true when all are finished
            My.Application.DoEvents()
            ReadIn1()               'read serial port 1 (Timers)
            X = DataIn.Length
            y = 0
            While y < X             ' handle all char's received seperately
                char1 = DataIn.Substring(y, 1)
                If InStr("ABCDX", char1) Then
                    CurrTime = My.Computer.Clock.TickCount.ToString
                    CurrTime = CurrTime / 1000
                    Select Case char1
                        Case "A"
                            DoLane1()
                        Case "B"
                            DoLane2()
                        Case "C"
                            DoLane3()
                        Case "D"
                            If NoClock Then ManStart() 'If manual start is set, start race on switch press by the starter
                    End Select
                    Watch = 12          'any valid char resets timeout
                Else
                    tbError.Text = ("Data Error")
                    tbError.BackColor = Color.Red
                End If
                y = y + 1
            End While

            If UpDisplay Then           'Timer 4 not still running from last xmit to clock (delay between consecutive messages)
                If Update1 Then ClockData = ClockData + "A" + DispLap1 + L1FinalMin + L1FinalSec
                If Update2 Then ClockData = ClockData + "B" + DispLap2 + L2FinalMin + L2FinalSec
                If Update3 Then ClockData = ClockData + "C" + DispLap3 + L3FinalMin + L3FinalSec
                If ClockData = "" Then
                Else
                    With SerialPort1
                        .Write(ClockData)   'send data to clock
                    End With
                End If
                ClockData = ""
                UpDisplay = False
                Timer4.Start()            'set UpDisplay true after timeout
                Update1 = False
                Update2 = False
                Update3 = False
            End If
        Loop
        RunLoop = False

    End Sub

    Sub ManStart()
        If Ln1State = "Ready" Then
            StartRace()
        End If

    End Sub



    Sub WaitX() 'wait time in "Delay" in seconds
        Dim t_stop As Integer
        Dim tim As Integer
        Timer3.Stop()
        CurrTime = My.Computer.Clock.TickCount.ToString
        CurrTime = CurrTime / 1000
        t_stop = CurrTime + Delay

        RcvData.BackColor = Color.LightGreen 'don't monitor data in for now

        Do While t_stop > CurrTime       'wait here
            My.Application.DoEvents()
            tim = t_stop - CurrTime
            tim = Format(tim, "##00")
            tbStart.Text = tim
            CurrTime = My.Computer.Clock.TickCount.ToString
            CurrTime = CurrTime / 1000
        Loop
        Timer3.Start()
    End Sub

    Sub WaitY() 'wait 0.3 secs

        Dim t_stop As Integer
        Dim tim As Integer

        CurrTime = My.Computer.Clock.TickCount.ToString
        CurrTime = CurrTime / 100
        t_stop = CurrTime + 3

        Do While t_stop > CurrTime       'wait here
            '  My.Application.DoEvents()   'allow "other stuff"
            tim = t_stop - CurrTime
            tbStart.Text = tim
            CurrTime = My.Computer.Clock.TickCount.ToString
            CurrTime = CurrTime / 100
        Loop
    End Sub
    Sub DoRcv()     'Received an "Z" from the remote transmitter

        If Watch < 3 Then     'watch gets decremented by timer3 every 200mS
            Watch = Watch + 12   '  
        End If
    End Sub



    Sub ReadIn1() '(ByVal datain As String)
        DataIn = ""
        With SerialPort1
            If .IsOpen Then DataIn = .ReadExisting()
        End With
    End Sub

    Sub SetNoInHeat()

        NoInHeat = 3            'was used when reading names in, for now just set to 3
        ' StateLn1.Text = ""
        ' StateLn1.BackColor = Color.White
        ' Ln1State = ""
        ' StateLn2.Text = ""
        '  StateLn2.BackColor = Color.White
        ' Ln2State = ""
        ' StateLn3.Text = ""
        ' StateLn3.BackColor = Color.White
        'Ln3State = ""

        '  If NoInHeat > 0 Then
        StateLn1.Text = "Ready"
        StateLn1.BackColor = Color.LightYellow
        Ln1State = "Ready"

        ' End If
        ' If NoInHeat > 1 Then
        StateLn2.Text = "Ready"
        StateLn2.BackColor = Color.LightYellow
        Ln2State = "Ready"

        'End If
        'If NoInHeat > 2 Then
        StateLn3.Text = "Ready"
        StateLn3.BackColor = Color.LightYellow
        Ln3State = "Ready"

        ' End If


    End Sub




    Sub ClearFm()
        ' NameNo1 = ""
        'NameNo2 = ""
        'NameNo3 = ""
        'NameNo4 = ""
   

        StateLn1.Clear()
        StateLn2.Clear()
        StateLn3.Clear()

        '  Ln1Rd.Clear()
        ' Ln2Rd.Clear()
        'Ln3Rd.Clear()

        

        Ln1Time.Clear()
        Ln2Time.Clear()
        Ln3Time.Clear()
        tbStart.Clear()
        Lane1Laps.Clear()
        Lane2laps.Clear()
        Lane3Laps.Clear()
        tbError.Clear()
        tbError.BackColor = Color.LightGray

        StateLn1.BackColor = Color.White
        StateLn2.BackColor = Color.White
        StateLn3.BackColor = Color.White
        Name1.Clear()
        Name2.Clear()
        Name3.Clear()

    End Sub





    Private Sub Timer4_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer4.Tick
        ' Used to delay xmit of consecutive updates to display
        UpDisplay = True
        Timer4.Stop()

    End Sub

    Private Sub Timer5_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer5.Tick
        tbRedSw.BackColor = Color.White
        tbRedSw.Text = ""
        Timer5.Stop()
    End Sub
    Private Sub Timer6_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer6.Tick
        tbGrnSw.BackColor = Color.White
        tbGrnSw.Text = ""
        Timer6.Stop()
    End Sub
    Private Sub Timer7_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer7.Tick
        tbAmbSw.BackColor = Color.White
        tbAmbSw.Text = ""
        Timer7.Stop()
    End Sub
    Private Sub Timer8_Tick(sender As System.Object, e As System.EventArgs) Handles Timer8.Tick
        tbstarter.BackColor = Color.White
        tbstarter.Text = ""
        Timer8.Stop()
    End Sub

    Private Sub ClkTest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClkTest.Click
        With SerialPort1
            If .IsOpen Then
                Timer3.Stop()               'Stop polling radios till done
                Clear_Display()
                WaitY()

                DataOut = "A00000000"           'Display 1 all zero's
                With SerialPort1
                    .Write(DataOut)
                End With
                WaitY()

                DataOut = "B00000000"           'Display 2 all zero's
                With SerialPort1
                    .Write(DataOut)
                End With
                WaitY()
                DataOut = "C00000000"           'Display 3 all zero's
                With SerialPort1
                    .Write(DataOut)
                End With
                WaitY()

                Delay = 3
                WaitX()
                Clear_Display()

                RcvData.BackColor = Color.Red
                Timer3.Start()
            End If
        End With
    End Sub

    Sub Clear_Display()
        With SerialPort1
            If .IsOpen Then
                DataOut = "Z"           'Clear all displays
                With SerialPort1
                    .Write(DataOut)
                End With
            End If
        End With
    End Sub
    Private Sub IncDisplay_Click(sender As System.Object, e As System.EventArgs) Handles IncDisplay.Click
        With SerialPort1
            If .IsOpen Then
                DataOut = "I"           'Increase display intensity
                With SerialPort1
                    .Write(DataOut)
                End With
            End If
        End With
    End Sub
    Private Sub DecDisplay_Click(sender As System.Object, e As System.EventArgs) Handles DecDisplay.Click
        With SerialPort1
            If .IsOpen Then
                DataOut = "D"           'Decrease display intensity
                With SerialPort1
                    .Write(DataOut)
                End With
            End If
        End With
    End Sub



    Private Sub tbError_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbError.Click
        tbError.Text = ""           'received a wrong bit of data at the receiver, just clear it...
        tbError.BackColor = Color.LightGray
    End Sub

    Private Sub Sw_test_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Sw_test.Click
        With SerialPort1
            If .IsOpen Then
                If SwTest = False Then  'Do Switch test
                    SwTest = True
                    Timer3.Stop()
                    tbRedSw.Visible = True
                    tbGrnSw.Visible = True
                    tbAmbSw.Visible = True
                    tbstarter.Visible = True
                    Sw_test.Text = "Done"
                    Sw_test.BackColor = Color.CadetBlue

                    Do While SwTest = True
                        My.Application.DoEvents()
                        ReadIn1() 'read serial port
                        Dim char1 As String
                        Dim x As Integer        'length of string
                        Dim y As Integer = 0    'current char in string
                        x = DataIn.Length

                        While y < x             ' handle all char's received seperately
                            char1 = DataIn.Substring(y, 1)
                            If InStr("ABCD", char1) Then
                                Select Case char1
                                    Case "A"
                                        tbRedSw.BackColor = Color.Firebrick
                                        '  tbRedSw.Text = "Red"
                                        Timer5.Start()
                                    Case "B"
                                        tbGrnSw.BackColor = Color.OliveDrab
                                        ' tbGrnSw.Text = "Green"
                                        Timer6.Start()
                                    Case "C"
                                        tbAmbSw.BackColor = Color.Goldenrod
                                        'tbAmbSw.Text = "Amber"
                                        Timer7.Start()
                                    Case "D"
                                        tbstarter.BackColor = Color.SkyBlue
                                        tbstarter.Text = "Starter"
                                        Timer8.Start()
                                End Select
                            End If
                            y = y + 1
                        End While
                    Loop

                Else : SwTest = False
                    tbRedSw.Clear()
                    tbRedSw.Visible = False
                    tbGrnSw.Visible = False
                    tbAmbSw.Visible = False
                    tbstarter.Visible = False
                    Sw_test.Text = "Test Switches"
                    Sw_test.BackColor = Color.LightGray
                    Timer3.Start()
                End If
            End If
        End With

    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        With SerialPort1
            If .IsOpen Then
                DataOut = "S"           'Test Aux out
                With SerialPort1
                    .Write(DataOut)
                End With
            End If
        End With
    End Sub

    Private Sub Com1ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Com1ToolStripMenuItem.Click
        Timer3.Stop()
        PortNo = "COM1"
        With SerialPort1
            If .IsOpen Then
                .Close()
            End If
        End With

        Dim NewSerialPort As IO.Ports.SerialPort = New IO.Ports.SerialPort(PortNo, 1200, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One)
        SerialPort1 = NewSerialPort
        On Error GoTo err1
        With SerialPort1
            .Open()
            .DiscardOutBuffer()
            .DiscardInBuffer()
            Timer3.Start()
        End With
        GoTo endsub
err1:
        MsgBox("   Com1 Not Found   ")
endsub:
    End Sub


    Private Sub Com2ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Com2ToolStripMenuItem.Click

        Timer3.Stop()
        PortNo = "COM2"
        With SerialPort1
            If .IsOpen Then
                .Close()
            End If
        End With

        Dim NewSerialPort As IO.Ports.SerialPort = New IO.Ports.SerialPort(PortNo, 1200, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One)
        SerialPort1 = NewSerialPort
        On Error GoTo err1
        With SerialPort1
            .Open()
            .DiscardOutBuffer()
            .DiscardInBuffer()
            Timer3.Start()
        End With
        GoTo endsub
err1:
        MsgBox("  Com2 Not Found   ")
endsub:
    End Sub

    Private Sub Com3ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Com3ToolStripMenuItem.Click
        Timer3.Stop()
        PortNo = "COM3"
        With SerialPort1
            If .IsOpen Then
                .Close()
            End If
        End With

        Dim NewSerialPort As IO.Ports.SerialPort = New IO.Ports.SerialPort(PortNo, 1200, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One)
        SerialPort1 = NewSerialPort
        On Error GoTo err1
        With SerialPort1
            .Open()
            .DiscardOutBuffer()
            .DiscardInBuffer()
            Timer3.Start()
        End With
        GoTo endsub
err1:
        MsgBox("  Com3 Not Found  ")
endsub:
    End Sub

    Private Sub Com4ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Com4ToolStripMenuItem.Click
        Timer3.Stop()
        PortNo = "COM4"
        With SerialPort1
            If .IsOpen Then
                .Close()
            End If
        End With

        Dim NewSerialPort As IO.Ports.SerialPort = New IO.Ports.SerialPort(PortNo, 1200, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One)
        SerialPort1 = NewSerialPort
        On Error GoTo err1
        With SerialPort1
            .Open()
            .DiscardOutBuffer()
            .DiscardInBuffer()
            Timer3.Start()
        End With
        GoTo endsub
err1:
        MsgBox("  Com4 Not Found  ")
endsub:
    End Sub
    Private Sub Com5ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Com5ToolStripMenuItem.Click
        'port 1, setup com 5
        Timer3.Stop()
        PortNo = "COM5"
        With SerialPort1
            If .IsOpen Then
                .Close()
            End If
        End With

        Dim NewSerialPort As IO.Ports.SerialPort = New IO.Ports.SerialPort(PortNo, 1200, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One)
        SerialPort1 = NewSerialPort
        On Error GoTo err1
        With SerialPort1
            .Open()
            .DiscardOutBuffer()
            .DiscardInBuffer()
            Timer3.Start()
        End With
        GoTo endsub
err1:
        MsgBox("  Com5 Not Found  ")
endsub:
    End Sub

    Private Sub Com6ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Com6ToolStripMenuItem.Click
        'port 1, setup com 6
        Timer3.Stop()
        PortNo = "COM6"
        With SerialPort1
            If .IsOpen Then
                .Close()
            End If
        End With

        Dim NewSerialPort As IO.Ports.SerialPort = New IO.Ports.SerialPort(PortNo, 1200, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One)
        SerialPort1 = NewSerialPort
        On Error GoTo err1
        With SerialPort1
            .Open()
            .DiscardOutBuffer()
            .DiscardInBuffer()
            Timer3.Start()
            GoTo endsub
        End With
err1:
        MsgBox("   Com6 Not Found   ")
endsub:
    End Sub

    Private Sub HELPToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HELPToolStripMenuItem1.Click
        MsgBox("Port 1 is the Receive port, used for communicating with the Timers and to send UHF data to the clock")
    End Sub

    Private Sub Com1ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Com2ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Com3ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Com4ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Com5ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub HELPToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    Private Sub AllPylonsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)


    End Sub

    Private Sub No1ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        ' SetupP1Polling()
    End Sub

    Private Sub DISABLEToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'NoCutJudges()
    End Sub

    Private Sub RUN10LAPSToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RUN100LAPSToolStripMenuItem.Click
        Lap200 = False

    End Sub

    Private Sub RUN1012LAPSToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RUN200LAPSToolStripMenuItem.Click
        Lap200 = True
        RaceFormat.Text = "200 Lap Final"
    End Sub

    Private Sub CLOCKSTARTToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CLOCKSTARTToolStripMenuItem.Click
        'Start using computer and display    
        ' NoClock = False
        bnStart.Text = ("Start Countdown")
        ' tbStart.Visible = True
        lbReady.Visible = False
        StartState = "Auto"
    End Sub

    Private Sub ManualStartToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ManualStartToolStripMenuItem1.Click
        'start using flag fall
        ' NoClock = True
        bnStart.Text = ("Setup Displays")
        bnSetup.Text = ("Setup Screen")
        StartState = "Manual"
        ' tbStart.Visible = False
    End Sub

    Private Sub HELPToolStripMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HELPToolStripMenuItem3.Click
        'clock start or manual start? 
        MsgBox("Select Manual if you want the starter to start the race with the switch instead of with the display countdown")
    End Sub

    Private Sub HELPToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    ' Sub TestWrite()
    'Dim f_str As String
    'Dim f_name As String
    '   f_name = "C:\temp\testfile.txt"
    '  f_str = ClockData + vbCr + vbLf
    ' My.Computer.FileSystem.WriteAllText( _
    '                                 file:=f_name, _
    '                              text:=f_str, _
    '                            append:=True, _
    '                          encoding:=System.Text.Encoding.ASCII)
    'End Sub

    ' Sub NoCutJudges()
    '    PollOne = False
    '   PollAll = False

    '    End Sub

    Private Sub Rerun1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Rerun1.Click
        Ln1State = "Finished"
        StateLn1.BackColor = Color.LightSkyBlue
        StateLn1.Text = "RR"
        ' DataOut = "LAE"
        'With SerialPort1
        '.Write(DataOut)
        'End With
        AllDone()   'check if all have finished

    End Sub

    Private Sub Rerun2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Rerun2.Click
        Ln2State = "Finished"
        StateLn2.BackColor = Color.LightSkyBlue
        StateLn2.Text = "RR"
        'DataOut = "LBE"
        'With SerialPort1
        ' .Write(DataOut)
        'End With
        AllDone()   'check if all have finished

    End Sub

    Private Sub Rerun3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Rerun3.Click
        Ln3State = "Finished"
        StateLn3.BackColor = Color.LightSkyBlue
        StateLn3.Text = "RR"
        'DataOut = "LCE"
        'With SerialPort1
        '.Write(DataOut)
        'End With
        AllDone()   'check if all have finished

    End Sub

End Class



