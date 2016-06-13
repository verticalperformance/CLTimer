'**** CLTimer for timing Control Line races *****

'12/8 Saved as Form1_backup within VB.  DONT DO THAT! totally stuffed up Vb environment - reverted to backed up version...
'21/10 tested with displays 6 - 8.
'7/11/15 backup to word doc
' 8/11 main progam loop need fixing. Rewrote all
'12/11 Tested OK. All displays lane 1 look OK.
'       Function of screen buttons OK?
'       Aux O/P (sounder) OK.

'15/3/16 Copied from Dev PC as working version
'----------------------------------------------
'Keith
'16/5/16 Made variables for heat and final laps
'17/6/16 Refactored Rerun clicks into single event

Imports System.Text
Imports System
Imports System.IO.Ports
Imports Microsoft.VisualBasic.FileIO

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
    Dim Ln1CurrLap, Ln2CurrLap, Ln3CurrLap, Ln4CurrLap As Integer
    Dim DispLap1 As String = "" 'Red curr laps to display
    Dim DispLap2 As String = "" 'Grn curr laps to display
    Dim DispLap3 As String = "" 'Amb curr laps to display

    Dim DataIn As String = ""
    Dim CurrTime As Double
    Dim NoInHeat As Integer
    Dim Ln1State, Ln2State, Ln3State, Ln4State As String

    Dim Done As Boolean = False
    Dim Watch As Integer = 0
    Dim RunLoop As Boolean = False
    Dim Delay As Integer
    Dim DataOut As String
    Dim RaceIsAFinal As Boolean = False       'default is 100 laps
    Dim NoClock As Boolean = False
    Dim SwTest As Boolean = False
    Dim Update1 As Boolean = False
    Dim Update2 As Boolean = False
    Dim Update3 As Boolean = False
    Dim PortNo As String = "COM5"
    Dim tim8 As Boolean
    Dim x_count As Integer
    Dim D_Level As Integer = 6
    Dim StartCount As Integer
    Dim ClockData As String

    Public sec1 As Double
    Public sec2 As Double
    Public temp4 As String
    Public FinMinutes As Integer
    Public minint As Integer
    Public result As String
    Public resultm As String
    Public results As String
    Public SecSize As Integer

    Public HeatLaps As Integer = 100  ' Defaults overridden by config file
    Public FinalLaps As Integer = 200

    'Public RaceClassesFileName As String = "D:\keith\Documents\Visual Studio 2010\Projects\CLTimer\Source Files\CLTimer\CLTimer\RaceClasses.csv"  ' need to get this from install dir
    Public RaceClassesFileName As String = "RaceClasses.csv"
    Public myRaces As New Races 'Race Data model

    Dim SerialPort1 As IO.Ports.SerialPort = New IO.Ports.SerialPort(PortNo, 1200, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One)



    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        StateLn1.BackColor = Color.LightYellow
        StateLn2.BackColor = Color.LightYellow
        StateLn3.BackColor = Color.LightYellow
        RcvData.BackColor = Color.Red

        radHeat.Checked = True  ' Default to heat distance on startup
        bnStart.Enabled = False
        bnClearDisplay.Enabled = False

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
        tmrCommsOK.Start()
        RunState = "Idle"
        'StartState = "Auto"
        StartState = "Manual"

        DisplayControlsForStartType()
        Com3ToolStripMenuItem.Checked = True  ' Need to fix

        Dim AppPath As String = Application.StartupPath

        RaceClassesFileName = AppPath & "\" & RaceClassesFileName  'Add the excecutable path

        myRaces.ReadRaceClassesFromFile(RaceClassesFileName) 'Read the race class file into the data model

        PopulateRaceListBox()


    End Sub

    Private Sub PopulateRaceListBox()
        Dim myRaceName As String
        For i As Integer = 0 To myRaces.Count - 1
            Dim myRace As New Race
            myRace = myRaces.Item(i)
            myRaceName = myRace.Name
            ClassName.Items.Add(myRaceName)
        Next
        ClassName.SelectedIndex = 0 'Show item 1
    End Sub


    Private Sub CLTimer_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        CloseCLTimer()
    End Sub
    Private Sub bnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bnExit.Click
        MyBase.Close() ' Tell the form to close
    End Sub
    Private Sub CloseCLTimer()
        ' Shutdown events
        tmrCommsOK.Stop()
        tmrCountDown.Stop()
        tmrConsXmitDelay.Stop()
        With SerialPort1
            If .IsOpen Then
                .Close()
            End If
        End With
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
                    Countdown()       'tmrCountDown ticked, decrement start time
                    IncTime = False
                End If
            Case "WaitStartPress"   'Waiting for manual starter switch press
                ReadIn1()               'read serial port 1 (Timers)
                x = DataIn.Length
                y = 0
                While y < x             ' handle all char's received seperately
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
                    tmrConsXmitDelay.Stop()
                    RunState = "Finished"
                    bnStart.Enabled = False
                    bnClearDisplay.Enabled = True
                    SetupToolStripMenuItem.Enabled = True
                    radHeat.Enabled = True
                    radFinal.Enabled = True
                    ClassName.Enabled = True
                Else
                    Running()
                End If


            Case "Finished"
                'wait for save clicked

        End Select
        GoTo Loop1

    End Sub
    Sub Countdown()           'Called by tmrCountDown every second

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

        tmrCountDown.Stop()
        RunState = "Racing"
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
        bnClearDisplay.Enabled = False
        radHeat.Enabled = False
        radFinal.Enabled = False
        ClassName.Enabled = False


        If StartState = "Auto" Then
            bnStart.Text = "Start Countdown"
        Else
            bnStart.Text = "Setup Display"      'StartState = "Manual"
        End If
        lbReady.Visible = False
        tbStart.Visible = False
        tbStart.Text = ("")

        tmrCommsOK.Start()
        tmrConsXmitDelay.Start()          'Trigger display update every 0.5 secs
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
                        tmrCommsOK.Stop()                 'any valid char resets timeout
                        tmrCommsOK.Start()
                    Case "B"
                        DoLane2()
                        tmrCommsOK.Stop()
                        tmrCommsOK.Start()
                    Case "C"
                        DoLane3()
                        tmrCommsOK.Stop()
                        tmrCommsOK.Start()
                    Case "D"
                        'starter, ignore
                End Select

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
            tmrConsXmitDelay.Start()            'set UpDisplay true after timeout
            Update1 = False
            Update2 = False
            Update3 = False
        End If



    End Sub

    Sub DoLane1()

        If StateLn1.Text = "Racing" Then
            Ln1CurrLap = Ln1CurrLap + 1
            sec1 = CurrTime - Lane1Time     'Lane1time saved at start

            'convert seconds to Minutes / Seconds 
            SecSize = Int(sec1)
            FinMinutes = SecSize \ 60              'minutes
            sec2 = sec1 - (FinMinutes * 60)     'seconds as double
            resultm = Format(FinMinutes, "0")      'minutes as string
            results = Format(sec2, "00.00")     'seconds as string
            result = resultm & ":" & results
            Ln1Time.Text = result               'display on screen

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
            If resultm < 10 Then
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

            If RaceIsAFinal Then
                If Ln1CurrLap = FinalLaps Then     'if final stop at 200 laps
                    Ln1State = "Finished"
                    StateLn1.Text = "Finished"
                    StateLn1.BackColor = Color.LightSkyBlue
                    AllDone()               'check if all have finished 
                End If
            ElseIf Ln1CurrLap = HeatLaps Then    'if not final and done 100 laps
                Ln1State = "Finished"
                StateLn1.Text = "Finished"
                StateLn1.BackColor = Color.LightSkyBlue
                AllDone()                   'check if all have finished 
            End If
        End If

    End Sub
    Sub DoLane2()

        If StateLn2.Text = "Racing" Then
            Ln2CurrLap = Ln2CurrLap + 1
            sec1 = CurrTime - Lane2Time     'Lane1time saved at start

            'convert seconds to Minutes / Seconds 
            SecSize = Int(sec1)
            FinMinutes = SecSize \ 60              'minutes
            sec2 = sec1 - (FinMinutes * 60)     'seconds as double
            resultm = Format(FinMinutes, "0")      'minutes as string
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
            If resultm < 10 Then
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

            If RaceIsAFinal Then
                If Ln2CurrLap = FinalLaps Then       'if final stop at 200 laps
                    Ln2State = "Finished"
                    StateLn2.Text = "Finished"
                    StateLn2.BackColor = Color.LightSkyBlue
                    AllDone() 'check if all have finished 
                End If
            ElseIf Ln2CurrLap = HeatLaps Then    'if not final and done 100 laps
                Ln2State = "Finished"
                StateLn2.Text = "Finished"
                StateLn2.BackColor = Color.LightSkyBlue
                AllDone() 'check if all have finished 
            End If
        End If

    End Sub
    Sub DoLane3()


        If StateLn3.Text = "Racing" Then
            Ln3CurrLap = Ln3CurrLap + 1
            sec1 = CurrTime - Lane3Time     'Lane1time saved at start

            'convert seconds to Minutes / Seconds 
            SecSize = Int(sec1)
            FinMinutes = SecSize \ 60              'minutes
            sec2 = sec1 - (FinMinutes * 60)     'seconds as double
            resultm = Format(FinMinutes, "0")      'minutes as string
            results = Format(sec2, "00.00")     'seconds as string
            result = resultm & ":" & results
            Ln3Time.Text = result               'display on screen

            temp4 = Ln3CurrLap
            Lane3Laps.Text = temp4
            If Ln3CurrLap < 10 Then
                DispLap3 = "//" + temp4         'DispLap1 used to send current lap count to display    
            ElseIf Ln3CurrLap < 100 Then
                DispLap3 = "/" + temp4
            Else
                DispLap3 = temp4
            End If

            L3FinalMin = resultm            'Used to send to displays
            If resultm < 10 Then
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

            If RaceIsAFinal Then
                If Ln3CurrLap = FinalLaps Then       'if final stop at 200 laps
                    Ln3State = "Finished"
                    StateLn3.Text = "Finished"
                    StateLn3.BackColor = Color.LightSkyBlue
                    AllDone() 'check if all have finished 
                End If
            ElseIf Ln3CurrLap = HeatLaps Then    'if not final and done 100 laps
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

        If Done Then
            'Need to make sure clock gets updated before stopping
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
            Update1 = False
            Update2 = False
            Update3 = False

            bnClearDisplay.Focus()

        End If

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
                bnClearDisplay.Enabled = False

                'SetupToolStripMenuItem.Enabled = False

                bnStart.Focus()

                RunState = "WaitStart"

                MainLoop()

            Else : MessageBox.Show("Not Receiving Data from Timers")
            End If
        End If

    End Sub
    Private Sub tmrCountDown_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrCountDown.Tick
        '1 Sec timer used during 60sec start
        IncTime = True
    End Sub

    Private Sub tmrCommsOK_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrCommsOK.Tick
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

    Private Sub InRaceControlState(Racing As Boolean)
        ' Disable all non race controls when racing = true
        ' Enable when false

        If Racing Then
            TestDisplay.Enabled = False
            TestHorn.Enabled = False
            TestSwitches.Enabled = False
            bnExit.Enabled = False
        Else
            TestDisplay.Enabled = True
            TestHorn.Enabled = True
            TestSwitches.Enabled = True
            bnExit.Enabled = True
        End If


    End Sub


    Private Sub bnStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bnStart.Click
        Dim RaceGo As Boolean = False

        If SwTest Then
            ' Cancel switch testing first
            EndSwitchTest()
        End If

        ' Disable all 

        If DataOk Then
            Select Case RunState
                Case "WaitStart"    '

                    InRaceControlState(True)

                    If StartState = "Auto" Then
                        SetDisplay2()               'setup display 1 for 2:00
                        tmrCountDown.Start()
                        tmrCommsOK.Stop()
                        RunState = "AutoStart"
                        bnStart.Text = "Stop CountDown"
                        ClkLabel.Visible = True
                        tbStart.Visible = True
                        bnStart.BackColor = Color.LightGreen
                        'bnExit.Enabled = False
                        Ln1CurrLap = 0
                        Ln2CurrLap = 0
                        Ln3CurrLap = 0
                        Done = False
                        StartCount = 120        '2 minute start time

                    ElseIf StartState = "Manual" Then
                        SetDisplay0()               'Set displays to 00 for manual start
                        RunState = "WaitStartPress"
                        bnStart.Text = "Reset Display"
                        Ln1CurrLap = 0
                        Ln2CurrLap = 0
                        Ln3CurrLap = 0
                        Done = False
                        lbReady.Visible = True
                        lbReady.BackColor = Color.LightGreen
                        'bnExit.Enabled = False
                    End If

                Case "AutoStart"        'Cancel auto start count down

                    InRaceControlState(False)

                    tmrCountDown.Stop()
                    tmrCommsOK.Start()
                    RunState = "WaitStart"

                    bnStart.Text = "Start Countdown"
                    bnStart.BackColor = Color.Silver
                    tbStart.BackColor = Color.Silver
                    ClkLabel.Visible = False
                    tbStart.Text = ""
                    tbStart.Visible = False
                    'bnExit.Enabled = True
                    Clear_Display()

                Case "WaitStartPress"       'quit waiting for starter

                    InRaceControlState(False)

                    Clear_Display()
                    bnStart.Text = "Setup Display"
                    lbReady.Visible = False
                    'bnExit.Enabled = True
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


    Sub Start()

        tbStart.BackColor = Color.LightGreen
        ClkLabel.Visible = True
        tbStart.Visible = True
        bnStart.Text = "Stop Clock"
        StartCount = 65 '** temp, set to 120

        ' Set up display 1 to 2:00
        DataOut = "A////200/"     'send to display 1, _ _ _ _ 2:00.
        With SerialPort1
            .Write(DataOut)
        End With

        tmrCommsOK.Stop()
        tmrCountDown.Start()      '1 second timer, calls Countdown

    End Sub


    Sub bnClearDisplay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bnClearDisplay.Click

        Dim Str1 As String = ""
        Dim Str2 As String = ""
        Dim Str3 As String = ""
        Dim Str4 As String = ""
        Dim Str5 As String = ""
        Dim results As String = ""

        If Done Then

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

            InRaceControlState(False)

            bnClearDisplay.Enabled = False
            bnSetup.Enabled = True
            bnSetup.Focus()
            'bnExit.Enabled = True

        Else
            MessageBox.Show("Not All Finished")
        End If

    End Sub
    Sub ManStart()
        If Ln1State = "Ready" Then
            StartRace()
        End If

    End Sub

    Sub WaitX() 'wait time in "Delay" in seconds
        Dim t_stop As Integer
        Dim tim As Integer
        tmrCommsOK.Stop()
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
        tmrCommsOK.Start()
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
    'Sub DoRcv()     'Received an "Z" from the remote transmitter

    '    If Watch < 3 Then     'watch gets decremented by timer3 every 200mS
    '        Watch = Watch + 12   '  
    '    End If
    'End Sub

    Sub ReadIn1() '(ByVal datain As String)
        DataIn = ""
        With SerialPort1
            If .IsOpen Then DataIn = .ReadExisting()
        End With
    End Sub

    Sub SetNoInHeat()

        NoInHeat = 3            'was used when reading names in, for now just set to 3

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

        StateLn1.Clear()
        StateLn2.Clear()
        StateLn3.Clear()

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

    End Sub


    Private Sub tmrConsXmitDelay_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrConsXmitDelay.Tick
        ' Used to delay xmit of consecutive updates to display
        UpDisplay = True
        tmrConsXmitDelay.Stop()

    End Sub

    Private Sub SwitchTestTimerTick(sender As System.Object, e As System.EventArgs) Handles tmrRedSw.Tick, tmrGrnSw.Tick, tmrAmbSw.Tick, tmrStarterSw.Tick
        If sender Is tmrRedSw Then
            tbRedSw.BackColor = SystemColors.Control
            tbRedSw.Text = "<->"
        ElseIf sender Is tmrGrnSw Then
            tbGrnSw.BackColor = SystemColors.Control
            tbGrnSw.Text = "<->"
        ElseIf sender Is tmrAmbSw Then
            tbAmbSw.BackColor = SystemColors.Control
            tbAmbSw.Text = "<->"
        ElseIf sender Is tmrStarterSw Then
            tbstarter.BackColor = SystemColors.Control
            tbstarter.Text = "<->"
        End If
        sender.Stop() ' Stop the timer

    End Sub

    Private Sub TestDisplay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TestDisplay.Click
        With SerialPort1
            If .IsOpen Then
                tmrCommsOK.Stop()               'Stop polling radios till done
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
                tmrCommsOK.Start()
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

    Private Sub AdjustDisplayBrightness_Click(sender As System.Object, e As System.EventArgs) Handles IncDisplay.Click, DecDisplay.Click

        If sender Is IncDisplay Then
            If D_Level < 6 Then
                D_Level = D_Level + 1
                DataOut = "I"           'Increase display intensity
            Else
                DataOut = ""
            End If

        Else
            If D_Level > 1 Then
                D_Level = D_Level - 1
                DataOut = "D"           'Decrease display intensity
            Else
                DataOut = ""
            End If
        End If

        If DataOut <> "" Then  ' If brightness needs to change
            With SerialPort1
                If .IsOpen Then
                    With SerialPort1
                        .Write(DataOut)
                    End With
                End If
            End With
            Set_Bar()
        End If

    End Sub
    Sub Set_Bar()
        Select Case D_Level
            Case 1
                Bar1.BackColor = Color.Cyan
                Bar2.BackColor = Color.AntiqueWhite
                Bar3.BackColor = Color.AntiqueWhite
                Bar4.BackColor = Color.AntiqueWhite
                Bar5.BackColor = Color.AntiqueWhite
                Bar6.BackColor = Color.AntiqueWhite
            Case 2
                Bar1.BackColor = Color.Cyan
                Bar2.BackColor = Color.Cyan
                Bar3.BackColor = Color.AntiqueWhite
                Bar4.BackColor = Color.AntiqueWhite
                Bar5.BackColor = Color.AntiqueWhite
                Bar6.BackColor = Color.AntiqueWhite
            Case 3
                Bar1.BackColor = Color.Cyan
                Bar2.BackColor = Color.Cyan
                Bar3.BackColor = Color.Cyan
                Bar4.BackColor = Color.AntiqueWhite
                Bar5.BackColor = Color.AntiqueWhite
                Bar6.BackColor = Color.AntiqueWhite
            Case 4
                Bar1.BackColor = Color.Cyan
                Bar2.BackColor = Color.Cyan
                Bar3.BackColor = Color.Cyan
                Bar4.BackColor = Color.Cyan
                Bar5.BackColor = Color.AntiqueWhite
                Bar6.BackColor = Color.AntiqueWhite
            Case 5
                Bar1.BackColor = Color.Cyan
                Bar2.BackColor = Color.Cyan
                Bar3.BackColor = Color.Cyan
                Bar4.BackColor = Color.Cyan
                Bar5.BackColor = Color.Cyan
                Bar6.BackColor = Color.AntiqueWhite
            Case 6
                Bar1.BackColor = Color.Cyan
                Bar2.BackColor = Color.Cyan
                Bar3.BackColor = Color.Cyan
                Bar4.BackColor = Color.Cyan
                Bar5.BackColor = Color.Cyan
                Bar6.BackColor = Color.Cyan

        End Select

    End Sub


    Private Sub tbError_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbError.Click
        tbError.Text = ""           'received a wrong bit of data at the receiver, just clear it...
        tbError.BackColor = Color.LightGray
    End Sub

    Private Sub TestSwitches_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TestSwitches.Click
        With SerialPort1
            If .IsOpen Then
                If SwTest = False Then  'Do Switch test
                    SwTest = True
                    tmrCommsOK.Stop()
                    tbRedSw.Visible = True
                    tbGrnSw.Visible = True
                    tbAmbSw.Visible = True
                    tbstarter.Visible = True
                    TestSwitches.Text = "Done"
                    TestSwitches.BackColor = Color.CadetBlue

                    tbRedSw.Text = ""
                    tbGrnSw.Text = ""
                    tbAmbSw.Text = ""
                    tbstarter.Text = ""


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
                                        tbRedSw.Text = "Red"
                                        tmrRedSw.Start()
                                    Case "B"
                                        tbGrnSw.BackColor = Color.OliveDrab
                                        tbGrnSw.Text = "Green"
                                        tmrGrnSw.Start()
                                    Case "C"
                                        tbAmbSw.BackColor = Color.Goldenrod
                                        tbAmbSw.Text = "Yellow"
                                        tmrAmbSw.Start()
                                    Case "D"
                                        tbstarter.BackColor = Color.SkyBlue
                                        tbstarter.Text = "Starter"
                                        tmrStarterSw.Start()
                                End Select
                            End If
                            y = y + 1
                        End While
                    Loop

                Else
                    EndSwitchTest()
                End If
            End If
        End With

    End Sub

    Private Sub EndSwitchTest()

        SwTest = False
        tbRedSw.Clear()
        tbRedSw.Visible = False
        tbGrnSw.Visible = False
        tbAmbSw.Visible = False
        tbstarter.Visible = False
        TestSwitches.Text = "Test Switches"
        TestSwitches.BackColor = Color.LightGray
        tmrCommsOK.Start()


    End Sub


    Private Sub TestHorn_Click(sender As System.Object, e As System.EventArgs) Handles TestHorn.Click
        With SerialPort1
            If .IsOpen Then
                DataOut = "S"           'Test Aux out
                With SerialPort1
                    .Write(DataOut)
                End With
            End If
        End With
    End Sub

    Private Sub ComToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Com1ToolStripMenuItem.Click, Com2ToolStripMenuItem.Click, Com3ToolStripMenuItem.Click, Com4ToolStripMenuItem.Click, Com5ToolStripMenuItem.Click, Com6ToolStripMenuItem.Click

        UnCheckComportMenuItems()
        sender.checked = True

        If PortNo <> sender.Text Then
            PortNo = sender.Text
            tmrCommsOK.Stop()

            With SerialPort1
                If .IsOpen Then
                    DataOk = False
                    RcvData.BackColor = Color.Red
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
                tmrCommsOK.Start()
            End With
        End If
        GoTo endsub
err1:
        MsgBox("   " & PortNo & " Not Found   ")
endsub:
    End Sub

    Private Sub UnCheckComportMenuItems()
        For Each ToolStripMenuItem In SetPortNoToolStripMenuItem.DropDownItems
            ToolStripMenuItem.Checked = False
        Next
    End Sub

    Private Sub HELPToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HELPToolStripMenuItem1.Click
        MsgBox("Port number of the serial port, used for communicating with the Lap Counter switches and to send UHF data to the clock. The default is 5")
    End Sub


    Private Sub CLOCKSTARTToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CLOCKSTARTToolStripMenuItem.Click
        'Start using computer and display    
        StartState = "Auto"
        DisplayControlsForStartType()
    End Sub

    Private Sub ManualStartToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ManualStartToolStripMenuItem1.Click
        'start using flag fall
        StartState = "Manual"
        DisplayControlsForStartType()
    End Sub


    Private Sub DisplayControlsForStartType()
        ' Populate the buttons and labels for a particular start type
        Select Case StartState
            Case "Auto"
                CLOCKSTARTToolStripMenuItem.Checked = True
                ManualStartToolStripMenuItem1.Checked = False
                bnStart.Text = ("Start Countdown")
                lbReady.Visible = False
            Case "Manual"
                ManualStartToolStripMenuItem1.Checked = True
                CLOCKSTARTToolStripMenuItem.Checked = False
                bnStart.Text = ("Setup Displays")
                bnSetup.Text = ("Setup Screen")
        End Select
    End Sub

    Private Sub HELPToolStripMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HELPToolStripMenuItem3.Click
        'clock start or manual start? 
        MsgBox("Select Manual if you want the starter to start the race with the switch instead of with the display countdown")
    End Sub

    Private Sub DNF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DNF1.Click, DNF2.Click, DNF3.Click
        If sender Is DNF1 Then
            Ln1State = "Finished"
            StateLn1.BackColor = Color.LightSkyBlue
            StateLn1.Text = "DNF"
        ElseIf sender Is DNF2 Then
            Ln2State = "Finished"
            StateLn2.BackColor = Color.LightSkyBlue
            StateLn2.Text = "DNF"
        ElseIf sender Is DNF3 Then
            Ln3State = "Finished"
            StateLn3.BackColor = Color.LightSkyBlue
            StateLn3.Text = "DNF"
        End If
        AllDone()   'check if all have finished
    End Sub
    Private Sub Rerun_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Rerun1.Click, Rerun2.Click, Rerun3.Click
        If sender Is Rerun1 Then
            Ln1State = "Finished"
            StateLn1.BackColor = Color.LightSkyBlue
            StateLn1.Text = "RR"
        ElseIf sender Is Rerun2 Then
            Ln2State = "Finished"
            StateLn2.BackColor = Color.LightSkyBlue
            StateLn2.Text = "RR"
        ElseIf sender Is Rerun3 Then
            Ln3State = "Finished"
            StateLn3.BackColor = Color.LightSkyBlue
            StateLn3.Text = "RR"
        End If
        AllDone()   'check if all have finished
    End Sub

    Private Sub radHeatFinal_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles radHeat.CheckedChanged, radFinal.CheckedChanged
        If sender Is radHeat Then
            RaceIsAFinal = False
            RaceFormat.Text = HeatLaps.ToString
            RaceFormat.BackColor = Color.LightGray
        Else
            RaceIsAFinal = True
            RaceFormat.Text = FinalLaps.ToString
            RaceFormat.BackColor = Color.Salmon
        End If
    End Sub

    Private Sub ClassName_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ClassName.SelectedIndexChanged
        HeatLaps = myRaces.Item(sender.SelectedIndex).HeatDistance
        FinalLaps = myRaces.Item(sender.SelectedIndex).FinalDistance
        If radHeat.Checked Then
            RaceFormat.Text = HeatLaps.ToString
        Else
            RaceFormat.Text = FinalLaps.ToString
        End If
    End Sub
End Class




