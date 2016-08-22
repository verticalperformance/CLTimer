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
'26/6/16 Major refactoring to remove and considate duplicate code to do with the DoLane events.

Imports System.Text
Imports System
Imports System.IO.Ports
Imports Microsoft.VisualBasic.FileIO

Public Class CLTimer

    'Public CalValue As Double = 0 '0.2     'Cal value. 
    Public RunState As String
    Public StartState As String
    Public IncTime As Boolean
    'Public Xmit_str As String
    'Public d_no As String
    Public y As Integer
    Public SaveIt As Boolean = False
    'Public raceStartTime As Double

    Dim UpDisplay As Boolean
    Dim DataOk As Boolean

    Public numberOfLanes As Integer = 3

    Dim lnCurrTime(numberOfLanes - 1) As Integer   ' Convert original 3 variables to array
    Dim lnCurrLap(numberOfLanes - 1) As Integer
    Dim lnState(numberOfLanes - 1) As String
    Dim lnUpdateDisplay(numberOfLanes - 1) As Boolean
    Dim lnDisplay(numberOfLanes - 1) As String

    Dim DataIn As String = ""
    Dim NoInHeat As Integer

    Dim Done As Boolean = False
    Dim RunLoop As Boolean = False
    Dim DataOut As String

    Dim SwTest As Boolean = False
 
    Dim PortNo As String = "COM3"  '"COM5"
    Dim D_Level As Integer = 6  ' Display Brightness Level

    Dim StartCount As Integer = 120 ' This is later re-calculated from config data
    Dim CountDownTimerDisplay As String = "A"

    Dim LaneTimerDisplay As String = CountDownTimerDisplay

    Dim elapsedSpan As TimeSpan

    Public ticPreviousSecond As Long = Now.Ticks  ' Use for the second counter
    Public ticRaceStart As Long = Now.Ticks  ' Use for overall race timing

    Dim ClockData As String

    Public HeatLaps As Integer = 100  ' Defaults are overridden by config file
    Public FinalLaps As Integer = 200

    Public RaceDistance As Integer = 0  ' How many laps, 0=unlimited
    Public RaceDuration As Integer = 0 ' How long can the race go for, 0=unlimited

    Public MaxHeatTime As Integer = 6000
    Public MaxFinalTime As Integer = 9000

    Public RaceType As String = "Laps"

    Public RaceClassesFileName As String = "RaceClasses.csv"
    Public myRaces As New Races 'Race Data model

    Dim SerialPort1 As IO.Ports.SerialPort '= New IO.Ports.SerialPort(PortNo, 1200, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One)
    Public Enum WhatToDisplay
        LapsAndTime
        Laps
    End Enum
    Private Sub InitialiseVariables()
        For i = 0 To numberOfLanes - 1
            lnCurrTime(i) = 0
            lnCurrLap(i) = 0
            lnState(i) = ""
            lnUpdateDisplay(i) = False
            lnDisplay(i) = ""
        Next i
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        GetSettings()

        SerialPort1 = New IO.Ports.SerialPort(PortNo, 1200, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One)


        InitialiseVariables()

        StateLn1.BackColor = Color.LightYellow
        StateLn2.BackColor = Color.LightYellow
        StateLn3.BackColor = Color.LightYellow
        RcvData.BackColor = Color.Red

        radHeat.Checked = True  ' Default to heat distance on startup
        bnStartRace.Enabled = False
        bnNextRace.Enabled = False



        Clear_Display()
        tmrCommsOK.Start()
        RunState = "Idle"

        CreateComPortStripMenu()
        SelectComPortStripMenuByName(PortNo)

        On Error Resume Next
        With SerialPort1
            .Open()
            .DiscardOutBuffer()
            .DiscardInBuffer()
        End With
        With SerialPort1
            If .IsOpen = False Then
                MsgBox("Com Port " + PortNo + " Not Found" + vbNewLine + "Select another Com Port from the menu")
            End If
        End With

        Dim AppPath As String = Application.StartupPath

        RaceClassesFileName = AppPath & "\" & RaceClassesFileName  'Add the excecutable path

        myRaces.ReadRaceClassesFromFile(RaceClassesFileName) 'Read the race class file into the data model

        PopulateRaceListBox() ' Display Race classes
        DisplayControlsForStartType() ' Setup buttons based on manual or auto start
        EnableRaceNotCompleteControls(False)
        bnSetupRace.Focus()

    End Sub

    Private Sub PopulateRaceListBox()
        Dim myRaceName As String
        For i As Integer = 0 To myRaces.Count - 1
            Dim myRace As New Race
            myRace = myRaces.Item(i)
            myRaceName = myRace.Name
            ClassName.Items.Add(myRaceName)
        Next

        If My.Settings.LastEvent < myRaces.Count Then
            ClassName.SelectedIndex = My.Settings.LastEvent ' Show the last event run
        Else
            ClassName.SelectedIndex = 0 'Show item 1, and hence populate race data model
        End If

    End Sub

    Private Sub GetSettings()

        StartState = My.Settings.StartState
        PortNo = My.Settings.COMPort


    End Sub

    Private Sub SetSettings()

        My.Settings.StartState = StartState
        My.Settings.COMPort = PortNo
        My.Settings.LastEvent = ClassName.SelectedIndex


        My.Settings.Save()

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
        tmrSecondsCounter.Stop()
        tmrConsXmitDelay.Stop()
        With SerialPort1
            If .IsOpen Then
                .Close()
            End If
        End With

        SetSettings()
        My.Application.DoEvents()       'allow other events

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
                    TimerDisplayCount(CountDownTimerDisplay, -1, True) ' one second or more has elapsed, decrement start counter time
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
                    bnStartRace.Enabled = False
                    bnNextRace.Enabled = True
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

    Sub TimerDisplayCount(DisplayToUse As String, deltaSeconds As Integer, updateMaster As Boolean)
        ' Increment/decrement the display and master clock

        StartCount = StartCount + deltaSeconds

        If updateMaster Then tbClock.Text = FormatSecondsToMinutes(StartCount) 'Update the form master clock if required

        Select Case DisplayToUse.Substring(0, 1)
            Case "A" To "C"
                If StartCount > 0 Then
                    DataOut = DisplayToUse & FormatSecondsToMinutesInDisplayFormat(StartCount)
                    With SerialPort1
                        .Write(DataOut)
                    End With
                    DataOut = ""
                Else
                    StartRace()
                End If
        End Select

    End Sub
    Sub ShowSecondsOnDisplay(DisplayToUse As String, Seconds As Integer)
        tbClock.Text = FormatSecondsToMinutes(Seconds) ' Show on PC
        Select Case DisplayToUse.Substring(0, 1)
            Case "A" To "C"
                DataOut = DisplayToUse & FormatSecondsToMinutesInDisplayFormat(Seconds) ' Show on Display
                With SerialPort1
                    .Write(DataOut)
                End With
                DataOut = ""
        End Select
    End Sub

    Function FormatSecondsToMinutesInDisplayFormat(Seconds As Integer) As String
        Dim tsSeconds = TimeSpan.FromSeconds(Seconds)
        Dim tmpMinutes As String = tsSeconds.ToString("%m")
        Dim tmpSeconds As String = tsSeconds.ToString("ss")
        tmpMinutes = tmpMinutes.PadLeft(2, "/") 'Remove the leading zero on minutes
        FormatSecondsToMinutesInDisplayFormat = "///" & tmpMinutes & tmpSeconds & "/"
    End Function
    Function FormatSecondsToMinutes(Seconds As Integer) As String
        FormatSecondsToMinutes = FormatSecondsToString(CDbl(Seconds), "%m\:ss")
    End Function
    Function FormatSecondsToString(Seconds As Double, Format As String) As String
        Dim tsSeconds = TimeSpan.FromSeconds(Seconds)
        FormatSecondsToString = tsSeconds.ToString(Format)
    End Function
    Function FormatLapsAndSecondsInDisplayFormat(Laps As Integer, Seconds As Double) As String
        Dim tsSeconds = TimeSpan.FromSeconds(Seconds)
        Dim tmpMinutes As String = tsSeconds.ToString("%m")
        Dim tmpSeconds As String = tsSeconds.ToString("ss")
        Dim tmpTenths As String = tsSeconds.ToString("%f")
        tmpMinutes = tmpMinutes.PadLeft(2, "/") 'Remove the leading zero on minutes

        Dim tmpLaps As String = Laps.ToString("##0").PadLeft(3, "/")

        FormatLapsAndSecondsInDisplayFormat = tmpLaps & tmpMinutes & tmpSeconds & tmpTenths
    End Function
    Function FormatLapsInDisplayFormat(Laps As Integer) As String
        Dim tmpLaps As String = Laps.ToString("##0").PadLeft(3, "/")
        FormatLapsInDisplayFormat = tmpLaps & "/////"
    End Function
    Sub StartRace()

        RunState = "Racing"
        ResetLane()
        SetLaneLapTextToZero()
        SetLaneTimeTextToZero()

        If RaceType = "Laps" Then
            SetDisplayToZeroLapsAndTime()
        Else
            SetDisplayToZeroLapsWithTimer(LaneTimerDisplay)  ' Rat race
        End If

        DataOut = "S"           'turn start sounder on
        With SerialPort1
            .Write(DataOut)
        End With

        ReadIn1()
        DataIn = ""                 'clear timer inputs

        ticPreviousSecond = Now.Ticks

        ticRaceStart = Now.Ticks    'Race start time in ticks
        ShowSecondsOnDisplay("X", 0)

        For i = 0 To numberOfLanes - 1
            lnState(i) = "Racing"
        Next

        StateLn1.Text = lnState(0)
        StateLn1.BackColor = Color.LightGreen
        StateLn2.Text = lnState(1)
        StateLn2.BackColor = Color.LightGreen
        StateLn3.Text = lnState(2)
        StateLn3.BackColor = Color.LightGreen
        tbClock.BackColor = Color.White
        bnStartRace.BackColor = Color.Silver


        bnStartRace.Enabled = False
        bnNextRace.Enabled = False
        radHeat.Enabled = False
        radFinal.Enabled = False
        ClassName.Enabled = False

        EnableRaceNotCompleteControls(True)


        If StartState = "Auto" Then
            bnStartRace.Text = "Racing"

        Else
            bnStartRace.Text = "Racing"      'StartState = "Manual"
            lbReady.Visible = False
        End If

        ClkLabel.Visible = True
        tbClock.Visible = True

        tmrCommsOK.Start()
        tmrConsXmitDelay.Start()          'Trigger display update every 0.5 secs
        UpDisplay = False

    End Sub
    Sub Running()
        Dim X, y As Integer
        Dim char1 As String
        Dim elapsedTicks As Long = Now.Ticks - ticRaceStart
        'Dim elapsedSpan As New TimeSpan(elapsedTicks)

        elapsedSpan = New TimeSpan(elapsedTicks)

        ReadIn1()               'read serial port 1 (Timers)
        X = DataIn.Length
        y = 0
        While y < X             ' handle all char's received seperately
            char1 = DataIn.Substring(y, 1)
            If InStr("ABCDX", char1) Then
                'CurrTime = My.Computer.Clock.TickCount.ToString
                'CurrTime = CurrTime / 1000
                Select Case char1
                    Case "A" To "C"
                        DoLane(char1)
                        tmrCommsOK.Stop()                 'any valid char resets timeout
                        tmrCommsOK.Start()
                End Select
            Else
                tbError.Text = ("Data Error")
                tbError.BackColor = Color.Red
            End If
            y = y + 1
        End While


        If RaceDuration > 0 And elapsedSpan.TotalSeconds >= RaceDuration Then 'stop counting after race duration complete
            RaceTimeExceeded()
        End If

        ' Update the master Clock
        If IncTime Then

            Dim currentTime As Double = Int(elapsedSpan.TotalSeconds) ' truncate the decimal seconds for the master clock

            ShowSecondsOnDisplay("X", currentTime)
            ' if race type is time based
            If RaceType = "Mins" Then
                Dim lnNumber = Asc(LaneTimerDisplay) - 65 'A == 0
                Dim lnStatus As Windows.Forms.TextBox = Nothing
                Dim lnLaps As Windows.Forms.TextBox = Nothing
                Dim lnTime As Windows.Forms.TextBox = Nothing

                Select Case LaneTimerDisplay
                    Case "A"
                        lnStatus = StateLn1
                        lnLaps = Lane1Laps
                        lnTime = Ln1Time
                    Case "B"
                        lnStatus = StateLn2
                        lnLaps = Lane2laps
                        lnTime = Ln2Time
                    Case "C"
                        lnStatus = StateLn3
                        lnLaps = Lane3Laps
                        lnTime = Ln3Time
                    Case Else
                        'Ignore, should never get here, trap error
                End Select
                lnUpdateDisplay(lnNumber) = UpdateLaneDisplay(LaneTimerDisplay, currentTime, lnCurrLap(lnNumber), lnDisplay(lnNumber), lnStatus, lnLaps, lnTime, WhatToDisplay.LapsAndTime, False)

            End If

            IncTime = False
        End If

        If UpDisplay Then           'Timer 4 not still running from last xmit to clock (delay between consecutive messages)
            UpdateAllDisplays()
            tmrConsXmitDelay.Start()            'set UpDisplay true after timeout
        End If


    End Sub

    Private Sub RaceTimeExceeded()
        ShowSecondsOnDisplay("X", RaceDuration)
        'CurrTime = My.Computer.Clock.TickCount.ToString
        ' CurrTime = CurrTime / 1000
        IncTime = False
        For i As Integer = Asc("A") To Asc("C")
            DoLane(Chr(i))
        Next

        tmrSecondsCounter.Stop()
        AllDone()

    End Sub

    Private Sub UpdateAllDisplays()

        For i = 0 To numberOfLanes - 1
            If lnUpdateDisplay(i) Then ClockData = ClockData + Chr(65 + i) + lnDisplay(i)
            lnUpdateDisplay(i) = False
        Next

        If ClockData <> "" Then
            With SerialPort1
                .Write(ClockData)   'send data to clock
            End With
        End If

        ClockData = ""
        UpDisplay = False

    End Sub

    Sub DoLane(lane As String)

        Dim laneCurrentLap As Integer = 0
        Dim laneCurrentTimeInSeconds As Double = 0

        laneCurrentTimeInSeconds = elapsedSpan.TotalSeconds '(CurrTime - raceStartTime)

        Dim showData As Integer = WhatToDisplay.LapsAndTime

        If RaceType = "Laps" Then
            showData = WhatToDisplay.LapsAndTime
        Else
            showData = WhatToDisplay.Laps
        End If

        Dim lnNumber = Asc(lane) - 65 'A == 0

        Dim lnStatus As Windows.Forms.TextBox = Nothing
        Dim lnLaps As Windows.Forms.TextBox = Nothing
        Dim lnTime As Windows.Forms.TextBox = Nothing

        Select Case lane
            Case "A"
                lnStatus = StateLn1
                lnLaps = Lane1Laps
                lnTime = Ln1Time
            Case "B"
                lnStatus = StateLn2
                lnLaps = Lane2laps
                lnTime = Ln2Time
            Case "C"
                lnStatus = StateLn3
                lnLaps = Lane3Laps
                lnTime = Ln3Time
            Case Else
                'Ignore, should never get here, trap error
        End Select

        lnUpdateDisplay(lnNumber) = UpdateLaneDisplay(lane, laneCurrentTimeInSeconds, lnCurrLap(lnNumber), lnDisplay(lnNumber), lnStatus, lnLaps, lnTime, showData, True)
        lnState(lnNumber) = lnStatus.Text

        AllDone()               'check if all have finished 


    End Sub

    Function UpdateLaneDisplay(lane As String, laneCurrentTimeInSeconds As Double, ByRef laneCurrentLap As Integer, ByRef DisplayLane As String, laneState As TextBox, laneLaps As TextBox, laneTime As TextBox, show As WhatToDisplay, Optional incrementLaps As Boolean = True) As Boolean

        Dim laneCurrentTimeAsString As String = ""
        UpdateLaneDisplay = False

        If laneState.Text = "Racing" Then
            If RaceDuration > 0 And laneCurrentTimeInSeconds >= RaceDuration Then 'stop counting after race duration complete
                laneCurrentTimeInSeconds = RaceDuration
                show = WhatToDisplay.LapsAndTime ' display times at race end
                laneState.Text = "Finished"
                laneState.BackColor = Color.LightSkyBlue
            Else
                If incrementLaps Then laneCurrentLap = laneCurrentLap + 1 'In racetype laps, when the increment time is calling this don't add to laps
            End If

            laneCurrentTimeAsString = FormatSecondsToString(laneCurrentTimeInSeconds, "%m\:ss\.ff")

            If show = WhatToDisplay.LapsAndTime Then
                DisplayLane = lane & FormatLapsAndSecondsInDisplayFormat(laneCurrentLap, laneCurrentTimeInSeconds)
                laneTime.Text = laneCurrentTimeAsString               'display on screen
            Else
                DisplayLane = lane & FormatLapsInDisplayFormat(laneCurrentLap)
            End If
            laneLaps.Text = laneCurrentLap

            UpdateLaneDisplay = True                  'Flag to update the display
            If RaceDistance > 0 And laneCurrentLap = RaceDistance Then     'stop after race distance complete
                laneState.Text = "Finished"
                laneState.BackColor = Color.LightSkyBlue
            End If
        End If


    End Function

    Sub AllDone() ' check if all have finished
        Select Case NoInHeat
            Case 1
                If lnState(0) <> "Racing" Then Done = True
            Case 2
                If lnState(0) <> "Racing" Then
                    If lnState(1) <> "Racing" Then Done = True
                End If
            Case 3
                If lnState(0) <> "Racing" Then
                    If lnState(1) <> "Racing" Then
                        If lnState(2) <> "Racing" Then Done = True
                    End If
                End If
        End Select

        If Done Then
            'Need to make sure clock gets updated before stopping
            UpdateAllDisplays()
            bnNextRace.Focus()
        End If

    End Sub

    Private Sub ResetLane()
        For i = 0 To numberOfLanes - 1
            lnCurrLap(i) = 0
            lnCurrTime(i) = 0
            lnState(i) = ""
        Next
    End Sub

    Private Sub bnSetupRace_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bnSetupRace.Click
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
                bnStartRace.Enabled = True
                bnSetupRace.Enabled = False
                bnNextRace.Enabled = False

                'SetupToolStripMenuItem.Enabled = False

                bnStartRace.Focus()

                RunState = "WaitStart"

                ' Reset the warmup/cooldown times
                StartCount = myRaces.Item(ClassName.SelectedIndex).WarmUpTime + myRaces.Item(ClassName.SelectedIndex).CoolDownTime

                LaneTimerDisplay = "A"

                ClkLabel.Visible = True
                tbClock.Visible = True

                If StartState = "Auto" Then
                    SetDisplayAsTimer(StartCount, CountDownTimerDisplay)
                    tbClock.Text = FormatSecondsToMinutes(StartCount)
                    lbReady.Visible = False
                Else
                    'ClkLabel.Visible = False
                    'tbStart.Visible = False
                    tbClock.Text = FormatSecondsToMinutes(0)

                End If



                MainLoop()

            Else : MessageBox.Show("Not Receiving Data from Timers")
            End If
        End If

    End Sub
    Private Sub bnStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bnStartRace.Click
        'Dim RaceGo As Boolean = False

        If SwTest Then
            ' Cancel switch testing first
            EndSwitchTest()
        End If

        ' Reset the warmup/cooldown times
        StartCount = myRaces.Item(ClassName.SelectedIndex).WarmUpTime + myRaces.Item(ClassName.SelectedIndex).CoolDownTime

        If DataOk Then
            Select Case RunState
                Case "WaitStart"    '
                    SetFormControlsToInRaceState(True)
                    If StartState = "Auto" Then
                        SetDisplayAsTimer(StartCount, CountDownTimerDisplay)
                        ticPreviousSecond = Now.Ticks
                        tmrSecondsCounter.Start()
                        tmrCommsOK.Stop()
                        RunState = "AutoStart"
                        bnStartRace.Text = "Stop CountDown"
                        ClkLabel.Visible = True
                        tbClock.Visible = True
                        bnStartRace.BackColor = Color.LightGreen
                        ResetLane()
                        Done = False
                    ElseIf StartState = "Manual" Then
                        StartCount = 0
                        ticPreviousSecond = Now.Ticks
                        tmrSecondsCounter.Start()
                        '                       SetDisplayToZeroLapsAndTime()               'Set displays to 00 for manual start

                        RunState = "WaitStartPress"
                        bnStartRace.Text = "Clear and Reset"

                        ResetLane()
                        SetLaneLapTextToZero()
                        SetLaneTimeTextToZero()
                        If RaceType = "Laps" Then
                            SetDisplayToZeroLapsAndTime()
                        Else
                             SetDisplayToZeroLapsWithTimer(LaneTimerDisplay)  ' Rat race
                        End If

                        Done = False
                        lbReady.Visible = True
                        lbReady.BackColor = Color.LightGreen
                    End If

                Case "AutoStart"        'Cancel auto start count down

                    SetFormControlsToInRaceState(False)

                    tmrSecondsCounter.Stop()
                    tmrCommsOK.Start()
                    RunState = "WaitStart"

                    bnStartRace.Text = "Start Countdown"
                    bnStartRace.BackColor = Color.Silver

                    Dim pauseDisplayTime As Double = 3
                    WaitX(pauseDisplayTime) ' Pause the clock display a little before reset

                    tbClock.Text = FormatSecondsToMinutes(StartCount)
                    SetDisplayAsTimer(StartCount, CountDownTimerDisplay)

                Case "WaitStartPress"       'quit waiting for starter

                    SetFormControlsToInRaceState(False)
                    tmrSecondsCounter.Stop()

                    Clear_Display()
                    bnStartRace.Text = "Prepare for Start"
                    lbReady.Visible = False
                    RunState = "WaitStart"
            End Select
        Else
            MessageBox.Show("Not Receiving Data from Timers")
        End If
    End Sub
    Private Sub bnNextRace_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bnNextRace.Click

        Dim Str1 As String = ""
        Dim Str2 As String = ""
        Dim Str3 As String = ""
        Dim Str4 As String = ""
        Dim Str5 As String = ""
        Dim results As String = ""

        Dim RaceFinishTime As String = Format(Now, "f")

        Dim LineID As String = RaceFinishTime + Chr(9) + ClassName.SelectedItem + Chr(9)

        If radHeat.Checked = True Then
            LineID = LineID + "Heat" + Chr(9)
        Else
            LineID = LineID + "Final" + Chr(9)
        End If


        Str1 = LineID + "Red" + Chr(9) + StateLn1.Text + Chr(9) + Lane1Laps.Text + Chr(9) + Ln1Time.Text + Chr(9) + vbNewLine
        Str2 = LineID + "Green" + Chr(9) + StateLn2.Text + Chr(9) + Lane2laps.Text + Chr(9) + Ln2Time.Text + Chr(9) + vbNewLine
        Str3 = LineID + "Yellow" + Chr(9) + StateLn3.Text + Chr(9) + Lane3Laps.Text + Chr(9) + Ln3Time.Text + Chr(9) + vbNewLine

        results = Str1 + Str2 + Str3
        Clipboard.Clear()
        Clipboard.SetText(results)

        SaveIt = False
        Clear_Display()
        RunState = "Idle"

        SetFormControlsToInRaceState(False)
        EnableRaceNotCompleteControls(False)

        bnNextRace.Enabled = False
        bnSetupRace.Enabled = True
        bnSetupRace.Focus()
        DisplayControlsForStartType()
        'bnExit.Enabled = True
 
    End Sub

    Private Sub SetFormControlsToInRaceState(Racing As Boolean)
        ' Disable all non race controls when racing = true
        ' Enable when false

        If Racing Then
            TestDisplay.Enabled = False
            ClearDisplayBoardOnly.Enabled = False
            TestHorn.Enabled = False
            TestSwitches.Enabled = False
            bnExit.Enabled = False
        Else
            TestDisplay.Enabled = True
            ClearDisplayBoardOnly.Enabled = True
            TestHorn.Enabled = True
            TestSwitches.Enabled = True
            bnExit.Enabled = True
        End If


    End Sub

    Private Sub EnableRaceNotCompleteControls(Enable As Boolean)

        Rerun1.Enabled = Enable
        Rerun2.Enabled = Enable
        Rerun3.Enabled = Enable
        DNF1.Enabled = Enable
        DNF2.Enabled = Enable
        DNF3.Enabled = Enable
        DQ1.Enabled = Enable
        DQ2.Enabled = Enable
        DQ3.Enabled = Enable

    End Sub

    Sub SetDisplayAsTimer(InitialTime As Integer, DisplayToUse As String)
        'Setup display A,B or C to InitialTimeValue
        DataOut = DisplayToUse & FormatSecondsToMinutesInDisplayFormat(InitialTime)
        With SerialPort1
            .Write(DataOut)
        End With
    End Sub
    Sub SetDisplayToZeroLapsAndTime()           'Manual start Laps = 0, Time = 0:00.0

        Dim tmpDisplay As String
        If RaceType = "Laps" Then
            tmpDisplay = "//0/0000"
        Else
            tmpDisplay = "//0/////"
        End If

        For i As Integer = 0 To 2
            DataOut = Chr(i + 65) & tmpDisplay
            With SerialPort1
                .Write(DataOut)
            End With
        Next

    End Sub
    Sub SetDisplayToZeroLapsWithTimer(DisplayToUse As String)           'Used to setup display with zero laps, and a single timer (eg for ratrace)
        Dim timerString As String

        For i As Integer = 0 To 2
            If i + 65 = Asc(DisplayToUse) Then
                timerString = "000/"
            Else
                timerString = "////"
            End If
            DataOut = Chr(i + 65) & "//0/" & timerString
            With SerialPort1
                .Write(DataOut)
            End With
        Next
    End Sub

    Sub WaitX(Delay As Double) 'wait time in "Delay" in seconds

        Dim ticStart As Long = Now.Ticks
        Dim waitTime As Double = 0.3

        Dim elapsedSecondsTicks As Long = 0
        Dim elapsedSecondsSpan As TimeSpan

        tmrCommsOK.Stop()
        RcvData.BackColor = Color.LightGreen 'don't monitor data in for now

        Do
            My.Application.DoEvents()
            elapsedSecondsTicks = Now.Ticks - ticStart
            elapsedSecondsSpan = New TimeSpan(elapsedSecondsTicks)
        Loop While elapsedSecondsSpan.TotalSeconds <= Delay

        tmrCommsOK.Start()

        'Dim t_stop As Integer
        'Dim tim As Integer
        'tmrCommsOK.Stop()
        'CurrTime = My.Computer.Clock.TickCount.ToString
        'CurrTime = CurrTime / 1000
        't_stop = CurrTime + Delay

        'RcvData.BackColor = Color.LightGreen 'don't monitor data in for now

        'Do While t_stop > CurrTime       'wait here
        '    My.Application.DoEvents()
        '    tim = t_stop - CurrTime
        '    tim = Format(tim, "##00")
        '    'tbClock.Text = tim
        '    CurrTime = My.Computer.Clock.TickCount.ToString
        '    CurrTime = CurrTime / 1000
        'Loop
        'tmrCommsOK.Start()
    End Sub
    Sub WaitY() 'wait 0.3 secs

        'Dim t_stop As Integer
        'Dim tim As Integer

        Dim ticStart As Long = Now.Ticks
        Dim waitTime As Double = 0.3

        Dim elapsedSecondsTicks As Long = 0
        Dim elapsedSecondsSpan As TimeSpan

        Do
            elapsedSecondsTicks = Now.Ticks - ticStart
            elapsedSecondsSpan = New TimeSpan(elapsedSecondsTicks)
        Loop While elapsedSecondsSpan.TotalSeconds <= waitTime

        '        CurrTime = My.Computer.Clock.TickCount.ToString
        '        CurrTime = CurrTime / 100
        '       t_stop = CurrTime + 3

        '        Do While t_stop > CurrTime       'wait here
        '  My.Application.DoEvents()   'allow "other stuff"
        'tim = t_stop - CurrTime
        'tbClock.Text = tim
        '       CurrTime = My.Computer.Clock.TickCount.ToString
        '      CurrTime = CurrTime / 100
        '      Loop


    End Sub

    Sub ReadIn1() '(ByVal datain As String)
        DataIn = ""
        With SerialPort1
            If .IsOpen Then DataIn = .ReadExisting()
        End With
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
    Private Sub tmrSecondsCounter_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrSecondsCounter.Tick
        '1 Sec timer, IncTime is True once every second

        Dim elapsedSecondsTicks As Long = Now.Ticks - ticPreviousSecond
        Dim elapsedSecondsSpan As New TimeSpan(elapsedSecondsTicks)

        If elapsedSecondsSpan.TotalSeconds >= 1 Then
            IncTime = True
            ticPreviousSecond = Now.Ticks
        Else
            IncTime = False
        End If


    End Sub
    Private Sub tmrConsXmitDelay_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrConsXmitDelay.Tick
        ' Used to delay xmit of consecutive updates to display
        UpDisplay = True
        tmrConsXmitDelay.Stop()

    End Sub

    Sub SetNoInHeat()

        NoInHeat = 3            'was used when reading names in, for now just set to 3

        '  If NoInHeat > 0 Then
        StateLn1.Text = "Ready"
        StateLn1.BackColor = Color.LightYellow
        lnState(0) = "Ready"

        ' End If
        ' If NoInHeat > 1 Then
        StateLn2.Text = "Ready"
        StateLn2.BackColor = Color.LightYellow
        lnState(1) = "Ready"

        'End If
        'If NoInHeat > 2 Then
        StateLn3.Text = "Ready"
        StateLn3.BackColor = Color.LightYellow
        lnState(2) = "Ready"

        ' End If


    End Sub
    Sub ClearFm()

        StateLn1.Clear()
        StateLn2.Clear()
        StateLn3.Clear()

        Ln1Time.Clear()
        Ln2Time.Clear()
        Ln3Time.Clear()
        tbClock.Clear()
        Lane1Laps.Clear()
        Lane2laps.Clear()
        Lane3Laps.Clear()
        tbError.Clear()
        tbError.BackColor = Color.LightGray

        StateLn1.BackColor = Color.White
        StateLn2.BackColor = Color.White
        StateLn3.BackColor = Color.White

    End Sub

    Private Sub SetLaneLapTextToZero()
        Lane1Laps.Text = "0"
        Lane2laps.Text = "0"
        Lane3Laps.Text = "0"
    End Sub
    Private Sub SetLaneTimeTextToZero()
        Ln1Time.Text = "0:00.00"
        Ln2Time.Text = "0:00.00"
        Ln3Time.Text = "0:00.00"
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

                Dim leaveDisplayOnTime As Double = 3

                'Delay = 3
                WaitX(leaveDisplayOnTime)
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
        tbGrnSw.Clear()
        tbAmbSw.Clear()
        tbstarter.Clear()
        TestSwitches.Text = "Test"
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


    Private Sub CreateComPortStripMenu()

        SetPortNoToolStripMenuItem.DropDownItems.Clear()

        Dim tsMenuItem As ToolStripMenuItem

        For Each sp As String In My.Computer.Ports.SerialPortNames

            tsMenuItem = SetPortNoToolStripMenuItem.DropDownItems.Add(sp)
            tsMenuItem.Tag = sp.Substring(3, sp.Length - 3)
            tsMenuItem.Name = sp
            AddHandler tsMenuItem.Click, AddressOf ComToolStripMenuItem_Click

        Next


        tsMenuItem = SetPortNoToolStripMenuItem.DropDownItems.Add("HELP")
        tsMenuItem.Name = "HELPCOMToolStripMenuItem"
        AddHandler tsMenuItem.Click, AddressOf HELPToolStripMenuItem1_Click



    End Sub

    Private Sub SelectComPortStripMenuByName(Name As String)
        For Each tsMenuItem As ToolStripMenuItem In SetPortNoToolStripMenuItem.DropDownItems
            If tsMenuItem.Name = Name Then
                tsMenuItem.Checked = True
            Else
                tsMenuItem.Checked = False
            End If
        Next
    End Sub

    Private Sub ComToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

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

    Private Sub HELPToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
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
                StartCount = myRaces.Item(ClassName.SelectedIndex).WarmUpTime + myRaces.Item(ClassName.SelectedIndex).CoolDownTime
                CLOCKSTARTToolStripMenuItem.Checked = True
                ManualStartToolStripMenuItem1.Checked = False
                bnStartRace.Text = ("Start Countdown")
                lbReady.Visible = False
                ClkLabel.Visible = True
                tbClock.Visible = True
                tbClock.Text = FormatSecondsToMinutes(StartCount)

            Case "Manual"
                StartCount = 0
                ManualStartToolStripMenuItem1.Checked = True
                CLOCKSTARTToolStripMenuItem.Checked = False
                bnStartRace.Text = ("Prepare for Start")
                ClkLabel.Visible = True
                tbClock.Visible = True
                tbClock.Text = FormatSecondsToMinutes(StartCount)
        End Select

    End Sub

    Private Sub HELPToolStripMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HELPToolStripMenuItem3.Click
        'clock start or manual start? 
        MsgBox("Select Manual if you want the starter to start the race with the switch instead of with the display countdown")
    End Sub

    Private Sub DNF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DNF1.Click, DNF2.Click, DNF3.Click
        If sender Is DNF1 Then
            lnState(0) = "Finished"
            StateLn1.BackColor = Color.LightSkyBlue
            StateLn1.Text = "DNF"
            If lnState(1) <> "Finished" Then
                LaneTimerDisplay = "B"
            Else
                LaneTimerDisplay = "C"
            End If
        ElseIf sender Is DNF2 Then
            lnState(1) = "Finished"
            StateLn2.BackColor = Color.LightSkyBlue
            StateLn2.Text = "DNF"
            If lnState(2) <> "Finished" Then
                LaneTimerDisplay = "C"
            End If
        ElseIf sender Is DNF3 Then
            lnState(2) = "Finished"
            StateLn3.BackColor = Color.LightSkyBlue
            StateLn3.Text = "DNF"
        End If
        AllDone()   'check if all have finished
    End Sub
    Private Sub Rerun_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Rerun1.Click, Rerun2.Click, Rerun3.Click
        If sender Is Rerun1 Then
            lnState(0) = "Finished"
            StateLn1.BackColor = Color.LightSkyBlue
            StateLn1.Text = "RR"
            If lnState(1) <> "Finished" Then
                LaneTimerDisplay = "B"
            Else
                LaneTimerDisplay = "C"
            End If
        ElseIf sender Is Rerun2 Then
            lnState(1) = "Finished"
            StateLn2.BackColor = Color.LightSkyBlue
            StateLn2.Text = "RR"
            If lnState(2) <> "Finished" Then
                LaneTimerDisplay = "C"
            End If
        ElseIf sender Is Rerun3 Then
            lnState(2) = "Finished"
            StateLn3.BackColor = Color.LightSkyBlue
            StateLn3.Text = "RR"
        End If
        AllDone()   'check if all have finished
    End Sub
    Private Sub DQ_Click(sender As System.Object, e As System.EventArgs) Handles DQ1.Click, DQ2.Click, DQ3.Click
        If sender Is DQ1 Then
            lnState(0) = "Finished"
            StateLn1.BackColor = Color.LightSkyBlue
            StateLn1.Text = "DQ"
            If lnState(1) <> "Finished" Then
                LaneTimerDisplay = "B"
            Else
                LaneTimerDisplay = "C"
            End If
        ElseIf sender Is DQ2 Then
            lnState(1) = "Finished"
            StateLn2.BackColor = Color.LightSkyBlue
            StateLn2.Text = "DQ"
            If lnState(2) <> "Finished" Then
                LaneTimerDisplay = "C"
            End If
        ElseIf sender Is DQ3 Then
            lnState(2) = "Finished"
            StateLn3.BackColor = Color.LightSkyBlue
            StateLn3.Text = "DQ"
        End If
        AllDone()   'check if all have finished
    End Sub

    Private Sub radHeatFinal_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles radHeat.CheckedChanged, radFinal.CheckedChanged
        If sender Is radHeat Then
            'RaceIsAFinal = False
            RaceLength.BackColor = Color.LightGray
            RaceDuration = MaxHeatTime
            RaceDistance = HeatLaps
        Else
            'RaceIsAFinal = True
            RaceLength.BackColor = Color.Salmon
            RaceDuration = MaxFinalTime
            RaceDistance = FinalLaps
        End If
        SetupRaceLengthForRaceType()

    End Sub
    Private Sub ClassName_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ClassName.SelectedIndexChanged

        HeatLaps = myRaces.Item(sender.SelectedIndex).HeatDistance
        FinalLaps = myRaces.Item(sender.SelectedIndex).FinalDistance
        MaxHeatTime = myRaces.Item(sender.SelectedIndex).MaxHeatTime
        MaxFinalTime = myRaces.Item(sender.SelectedIndex).MaxFinalTime

        If StartState = "Manual" Then
            StartCount = 0
        Else
            StartCount = myRaces.Item(sender.SelectedIndex).WarmUpTime + myRaces.Item(sender.SelectedIndex).CoolDownTime
        End If

        tbClock.Text = FormatSecondsToMinutes(StartCount)

        RaceType = myRaces.Item(sender.SelectedIndex).Type

        lblTimeOrDistance.Text = RaceType

        If radHeat.Checked Then
            RaceDistance = HeatLaps
            RaceDuration = MaxHeatTime
        Else
            RaceDistance = FinalLaps
            RaceDuration = MaxFinalTime
        End If
        SetupRaceLengthForRaceType()

    End Sub
    Private Sub SetupRaceLengthForRaceType()
        If RaceType = "Laps" Then
            RaceLength.Text = RaceDistance.ToString
        Else
            RaceDistance = 0
            RaceLength.Text = FormatSecondsToMinutes(RaceDuration)
        End If
    End Sub
    Private Sub bnClearDisplayBoardOnly_Click(sender As System.Object, e As System.EventArgs) Handles ClearDisplayBoardOnly.Click
        Clear_Display()
    End Sub


End Class




