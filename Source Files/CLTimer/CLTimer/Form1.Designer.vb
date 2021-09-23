<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CLTimer
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CLTimer))
        Me.Label4 = New System.Windows.Forms.Label()
        Me.ManualStartToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.Ln2Time = New System.Windows.Forms.TextBox()
        Me.CLOCKSTARTToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ManualStartToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HELPToolStripMenuItem3 = New System.Windows.Forms.ToolStripMenuItem()
        Me.Lane2laps = New System.Windows.Forms.TextBox()
        Me.Lane1Laps = New System.Windows.Forms.TextBox()
        Me.SetPortNoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Lane1 = New System.Windows.Forms.GroupBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Ln1Speed = New System.Windows.Forms.TextBox()
        Me.DQ1 = New System.Windows.Forms.Button()
        Me.Rerun1 = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Ln1Time = New System.Windows.Forms.TextBox()
        Me.DNF1 = New System.Windows.Forms.Button()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.StateLn1 = New System.Windows.Forms.TextBox()
        Me.Rerun2 = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Ln3Speed = New System.Windows.Forms.TextBox()
        Me.DQ3 = New System.Windows.Forms.Button()
        Me.Rerun3 = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Ln3Time = New System.Windows.Forms.TextBox()
        Me.Lane3Laps = New System.Windows.Forms.TextBox()
        Me.DNF3 = New System.Windows.Forms.Button()
        Me.StateLn3 = New System.Windows.Forms.TextBox()
        Me.tmrSecondsCounter = New System.Windows.Forms.Timer(Me.components)
        Me.tbError = New System.Windows.Forms.TextBox()
        Me.SetupToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DNF2 = New System.Windows.Forms.Button()
        Me.StateLn2 = New System.Windows.Forms.TextBox()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Ln2Speed = New System.Windows.Forms.TextBox()
        Me.DQ2 = New System.Windows.Forms.Button()
        Me.tmrCommsOK = New System.Windows.Forms.Timer(Me.components)
        Me.tmrConsXmitDelay = New System.Windows.Forms.Timer(Me.components)
        Me.bnExit = New System.Windows.Forms.Button()
        Me.tmrRedSw = New System.Windows.Forms.Timer(Me.components)
        Me.tmrGrnSw = New System.Windows.Forms.Timer(Me.components)
        Me.tmrAmbSw = New System.Windows.Forms.Timer(Me.components)
        Me.tmrStarterSw = New System.Windows.Forms.Timer(Me.components)
        Me.gbSystem = New System.Windows.Forms.GroupBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.tbstarter = New System.Windows.Forms.TextBox()
        Me.tbAmbSw = New System.Windows.Forms.TextBox()
        Me.tbGrnSw = New System.Windows.Forms.TextBox()
        Me.TestSwitches = New System.Windows.Forms.Button()
        Me.tbRedSw = New System.Windows.Forms.TextBox()
        Me.ClearDisplayBoardOnly = New System.Windows.Forms.Button()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.DecDisplay = New System.Windows.Forms.Button()
        Me.IncDisplay = New System.Windows.Forms.Button()
        Me.Bar6 = New System.Windows.Forms.TextBox()
        Me.Bar5 = New System.Windows.Forms.TextBox()
        Me.Bar4 = New System.Windows.Forms.TextBox()
        Me.Bar3 = New System.Windows.Forms.TextBox()
        Me.Bar2 = New System.Windows.Forms.TextBox()
        Me.Bar1 = New System.Windows.Forms.TextBox()
        Me.TestHorn = New System.Windows.Forms.Button()
        Me.TestDisplay = New System.Windows.Forms.Button()
        Me.RcvData = New System.Windows.Forms.TextBox()
        Me.tbClock = New System.Windows.Forms.TextBox()
        Me.lbReady = New System.Windows.Forms.Label()
        Me.bnNextRace = New System.Windows.Forms.Button()
        Me.bnStartRace = New System.Windows.Forms.Button()
        Me.bnSetupRace = New System.Windows.Forms.Button()
        Me.ClkLabel = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.RaceLength = New System.Windows.Forms.TextBox()
        Me.lblTimeOrDistance = New System.Windows.Forms.Label()
        Me.ClassName = New System.Windows.Forms.ComboBox()
        Me.radHeat = New System.Windows.Forms.RadioButton()
        Me.radFinal = New System.Windows.Forms.RadioButton()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.Ln1SpeedUnit = New System.Windows.Forms.Label()
        Me.Ln2SpeedUnit = New System.Windows.Forms.Label()
        Me.Ln3SpeedUnit = New System.Windows.Forms.Label()
        Me.SpeedLimitText = New System.Windows.Forms.Label()
        Me.SpeedLimit = New System.Windows.Forms.TextBox()
        Me.Lane1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.gbSystem.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label4.Location = New System.Drawing.Point(98, 12)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(44, 20)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "Laps"
        '
        'ManualStartToolStripMenuItem1
        '
        Me.ManualStartToolStripMenuItem1.Name = "ManualStartToolStripMenuItem1"
        Me.ManualStartToolStripMenuItem1.Size = New System.Drawing.Size(181, 22)
        Me.ManualStartToolStripMenuItem1.Text = "Manual Start"
        '
        'Ln2Time
        '
        Me.Ln2Time.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Ln2Time.Location = New System.Drawing.Point(163, 49)
        Me.Ln2Time.Name = "Ln2Time"
        Me.Ln2Time.Size = New System.Drawing.Size(106, 31)
        Me.Ln2Time.TabIndex = 12
        Me.Ln2Time.TabStop = False
        Me.Ln2Time.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'CLOCKSTARTToolStripMenuItem
        '
        Me.CLOCKSTARTToolStripMenuItem.Name = "CLOCKSTARTToolStripMenuItem"
        Me.CLOCKSTARTToolStripMenuItem.Size = New System.Drawing.Size(181, 22)
        Me.CLOCKSTARTToolStripMenuItem.Text = "Display Countdown"
        '
        'ManualStartToolStripMenuItem
        '
        Me.ManualStartToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CLOCKSTARTToolStripMenuItem, Me.ManualStartToolStripMenuItem1, Me.HELPToolStripMenuItem3})
        Me.ManualStartToolStripMenuItem.Name = "ManualStartToolStripMenuItem"
        Me.ManualStartToolStripMenuItem.Size = New System.Drawing.Size(150, 22)
        Me.ManualStartToolStripMenuItem.Text = "Start Type"
        '
        'HELPToolStripMenuItem3
        '
        Me.HELPToolStripMenuItem3.Name = "HELPToolStripMenuItem3"
        Me.HELPToolStripMenuItem3.Size = New System.Drawing.Size(181, 22)
        Me.HELPToolStripMenuItem3.Text = "HELP"
        '
        'Lane2laps
        '
        Me.Lane2laps.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lane2laps.Location = New System.Drawing.Point(163, 12)
        Me.Lane2laps.Name = "Lane2laps"
        Me.Lane2laps.Size = New System.Drawing.Size(60, 31)
        Me.Lane2laps.TabIndex = 11
        Me.Lane2laps.TabStop = False
        Me.Lane2laps.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Lane1Laps
        '
        Me.Lane1Laps.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lane1Laps.Location = New System.Drawing.Point(164, 16)
        Me.Lane1Laps.Name = "Lane1Laps"
        Me.Lane1Laps.Size = New System.Drawing.Size(60, 31)
        Me.Lane1Laps.TabIndex = 10
        Me.Lane1Laps.TabStop = False
        Me.Lane1Laps.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'SetPortNoToolStripMenuItem
        '
        Me.SetPortNoToolStripMenuItem.Name = "SetPortNoToolStripMenuItem"
        Me.SetPortNoToolStripMenuItem.Size = New System.Drawing.Size(150, 22)
        Me.SetPortNoToolStripMenuItem.Text = "Set Port 1 No."
        '
        'Lane1
        '
        Me.Lane1.BackColor = System.Drawing.Color.Firebrick
        Me.Lane1.Controls.Add(Me.Ln1SpeedUnit)
        Me.Lane1.Controls.Add(Me.Label5)
        Me.Lane1.Controls.Add(Me.Ln1Speed)
        Me.Lane1.Controls.Add(Me.DQ1)
        Me.Lane1.Controls.Add(Me.Rerun1)
        Me.Lane1.Controls.Add(Me.Label2)
        Me.Lane1.Controls.Add(Me.Ln1Time)
        Me.Lane1.Controls.Add(Me.DNF1)
        Me.Lane1.Controls.Add(Me.Label24)
        Me.Lane1.Controls.Add(Me.StateLn1)
        Me.Lane1.Controls.Add(Me.Lane1Laps)
        Me.Lane1.Location = New System.Drawing.Point(299, 27)
        Me.Lane1.Name = "Lane1"
        Me.Lane1.Size = New System.Drawing.Size(286, 123)
        Me.Lane1.TabIndex = 109
        Me.Lane1.TabStop = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label5.Location = New System.Drawing.Point(99, 88)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(56, 20)
        Me.Label5.TabIndex = 26
        Me.Label5.Text = "Speed"
        '
        'Ln1Speed
        '
        Me.Ln1Speed.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Ln1Speed.Location = New System.Drawing.Point(164, 86)
        Me.Ln1Speed.Name = "Ln1Speed"
        Me.Ln1Speed.Size = New System.Drawing.Size(60, 31)
        Me.Ln1Speed.TabIndex = 25
        Me.Ln1Speed.TabStop = False
        Me.Ln1Speed.Text = "--- "
        Me.Ln1Speed.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'DQ1
        '
        Me.DQ1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.DQ1.Location = New System.Drawing.Point(15, 94)
        Me.DQ1.Name = "DQ1"
        Me.DQ1.Size = New System.Drawing.Size(45, 23)
        Me.DQ1.TabIndex = 6
        Me.DQ1.Text = "DQ"
        Me.DQ1.UseVisualStyleBackColor = True
        '
        'Rerun1
        '
        Me.Rerun1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Rerun1.Location = New System.Drawing.Point(15, 70)
        Me.Rerun1.Name = "Rerun1"
        Me.Rerun1.Size = New System.Drawing.Size(45, 23)
        Me.Rerun1.TabIndex = 5
        Me.Rerun1.Text = "RR"
        Me.Rerun1.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label2.Location = New System.Drawing.Point(99, 52)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(43, 20)
        Me.Label2.TabIndex = 14
        Me.Label2.Text = "Time"
        '
        'Ln1Time
        '
        Me.Ln1Time.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Ln1Time.Location = New System.Drawing.Point(164, 52)
        Me.Ln1Time.Name = "Ln1Time"
        Me.Ln1Time.Size = New System.Drawing.Size(106, 31)
        Me.Ln1Time.TabIndex = 12
        Me.Ln1Time.TabStop = False
        Me.Ln1Time.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'DNF1
        '
        Me.DNF1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.DNF1.Location = New System.Drawing.Point(15, 46)
        Me.DNF1.Name = "DNF1"
        Me.DNF1.Size = New System.Drawing.Size(45, 23)
        Me.DNF1.TabIndex = 4
        Me.DNF1.Text = "DNF"
        Me.DNF1.UseVisualStyleBackColor = True
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label24.Location = New System.Drawing.Point(101, 16)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(44, 20)
        Me.Label24.TabIndex = 24
        Me.Label24.Text = "Laps"
        '
        'StateLn1
        '
        Me.StateLn1.BackColor = System.Drawing.SystemColors.Control
        Me.StateLn1.Location = New System.Drawing.Point(6, 19)
        Me.StateLn1.Name = "StateLn1"
        Me.StateLn1.ReadOnly = True
        Me.StateLn1.Size = New System.Drawing.Size(68, 20)
        Me.StateLn1.TabIndex = 0
        Me.StateLn1.TabStop = False
        Me.StateLn1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Rerun2
        '
        Me.Rerun2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Rerun2.Location = New System.Drawing.Point(15, 69)
        Me.Rerun2.Name = "Rerun2"
        Me.Rerun2.Size = New System.Drawing.Size(45, 23)
        Me.Rerun2.TabIndex = 8
        Me.Rerun2.Text = "RR"
        Me.Rerun2.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label3.Location = New System.Drawing.Point(99, 49)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(43, 20)
        Me.Label3.TabIndex = 14
        Me.Label3.Text = "Time"
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.Goldenrod
        Me.GroupBox2.Controls.Add(Me.Ln3SpeedUnit)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.Ln3Speed)
        Me.GroupBox2.Controls.Add(Me.DQ3)
        Me.GroupBox2.Controls.Add(Me.Rerun3)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.Ln3Time)
        Me.GroupBox2.Controls.Add(Me.Lane3Laps)
        Me.GroupBox2.Controls.Add(Me.DNF3)
        Me.GroupBox2.Controls.Add(Me.StateLn3)
        Me.GroupBox2.Location = New System.Drawing.Point(299, 297)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(286, 123)
        Me.GroupBox2.TabIndex = 112
        Me.GroupBox2.TabStop = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label9.Location = New System.Drawing.Point(98, 88)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(56, 20)
        Me.Label9.TabIndex = 28
        Me.Label9.Text = "Speed"
        '
        'Ln3Speed
        '
        Me.Ln3Speed.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Ln3Speed.Location = New System.Drawing.Point(163, 86)
        Me.Ln3Speed.Name = "Ln3Speed"
        Me.Ln3Speed.Size = New System.Drawing.Size(60, 31)
        Me.Ln3Speed.TabIndex = 27
        Me.Ln3Speed.TabStop = False
        Me.Ln3Speed.Text = "--- "
        Me.Ln3Speed.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'DQ3
        '
        Me.DQ3.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.DQ3.Location = New System.Drawing.Point(15, 93)
        Me.DQ3.Name = "DQ3"
        Me.DQ3.Size = New System.Drawing.Size(45, 23)
        Me.DQ3.TabIndex = 12
        Me.DQ3.Text = "DQ"
        Me.DQ3.UseVisualStyleBackColor = True
        '
        'Rerun3
        '
        Me.Rerun3.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Rerun3.Location = New System.Drawing.Point(15, 69)
        Me.Rerun3.Name = "Rerun3"
        Me.Rerun3.Size = New System.Drawing.Size(45, 23)
        Me.Rerun3.TabIndex = 11
        Me.Rerun3.Text = "RR"
        Me.Rerun3.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label6.Location = New System.Drawing.Point(100, 51)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(43, 20)
        Me.Label6.TabIndex = 14
        Me.Label6.Text = "Time"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label7.Location = New System.Drawing.Point(100, 13)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(44, 20)
        Me.Label7.TabIndex = 13
        Me.Label7.Text = "Laps"
        '
        'Ln3Time
        '
        Me.Ln3Time.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Ln3Time.Location = New System.Drawing.Point(163, 51)
        Me.Ln3Time.Name = "Ln3Time"
        Me.Ln3Time.Size = New System.Drawing.Size(106, 31)
        Me.Ln3Time.TabIndex = 12
        Me.Ln3Time.TabStop = False
        Me.Ln3Time.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Lane3Laps
        '
        Me.Lane3Laps.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lane3Laps.Location = New System.Drawing.Point(163, 13)
        Me.Lane3Laps.Name = "Lane3Laps"
        Me.Lane3Laps.Size = New System.Drawing.Size(60, 31)
        Me.Lane3Laps.TabIndex = 11
        Me.Lane3Laps.TabStop = False
        Me.Lane3Laps.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'DNF3
        '
        Me.DNF3.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.DNF3.Location = New System.Drawing.Point(15, 45)
        Me.DNF3.Name = "DNF3"
        Me.DNF3.Size = New System.Drawing.Size(45, 23)
        Me.DNF3.TabIndex = 10
        Me.DNF3.Text = "DNF"
        Me.DNF3.UseVisualStyleBackColor = True
        '
        'StateLn3
        '
        Me.StateLn3.Location = New System.Drawing.Point(6, 19)
        Me.StateLn3.Name = "StateLn3"
        Me.StateLn3.ReadOnly = True
        Me.StateLn3.Size = New System.Drawing.Size(68, 20)
        Me.StateLn3.TabIndex = 0
        Me.StateLn3.TabStop = False
        Me.StateLn3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tmrSecondsCounter
        '
        Me.tmrSecondsCounter.Interval = 10
        '
        'tbError
        '
        Me.tbError.BackColor = System.Drawing.Color.LightGray
        Me.tbError.Location = New System.Drawing.Point(6, 42)
        Me.tbError.Name = "tbError"
        Me.tbError.Size = New System.Drawing.Size(269, 20)
        Me.tbError.TabIndex = 120
        Me.tbError.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.tbError.Visible = False
        '
        'SetupToolStripMenuItem
        '
        Me.SetupToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ManualStartToolStripMenuItem, Me.SetPortNoToolStripMenuItem})
        Me.SetupToolStripMenuItem.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.SetupToolStripMenuItem.Name = "SetupToolStripMenuItem"
        Me.SetupToolStripMenuItem.Size = New System.Drawing.Size(52, 20)
        Me.SetupToolStripMenuItem.Text = "Setup"
        '
        'DNF2
        '
        Me.DNF2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.DNF2.Location = New System.Drawing.Point(15, 45)
        Me.DNF2.Name = "DNF2"
        Me.DNF2.Size = New System.Drawing.Size(45, 23)
        Me.DNF2.TabIndex = 7
        Me.DNF2.Text = "DNF"
        Me.DNF2.UseVisualStyleBackColor = True
        '
        'StateLn2
        '
        Me.StateLn2.Location = New System.Drawing.Point(6, 19)
        Me.StateLn2.Name = "StateLn2"
        Me.StateLn2.ReadOnly = True
        Me.StateLn2.Size = New System.Drawing.Size(68, 20)
        Me.StateLn2.TabIndex = 0
        Me.StateLn2.TabStop = False
        Me.StateLn2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SetupToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
        Me.MenuStrip1.Size = New System.Drawing.Size(594, 24)
        Me.MenuStrip1.TabIndex = 125
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.OliveDrab
        Me.GroupBox1.Controls.Add(Me.Ln2SpeedUnit)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Ln2Speed)
        Me.GroupBox1.Controls.Add(Me.DQ2)
        Me.GroupBox1.Controls.Add(Me.Rerun2)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Ln2Time)
        Me.GroupBox1.Controls.Add(Me.Lane2laps)
        Me.GroupBox1.Controls.Add(Me.DNF2)
        Me.GroupBox1.Controls.Add(Me.StateLn2)
        Me.GroupBox1.Location = New System.Drawing.Point(299, 162)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(286, 123)
        Me.GroupBox1.TabIndex = 111
        Me.GroupBox1.TabStop = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label8.Location = New System.Drawing.Point(98, 88)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(56, 20)
        Me.Label8.TabIndex = 28
        Me.Label8.Text = "Speed"
        '
        'Ln2Speed
        '
        Me.Ln2Speed.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Ln2Speed.Location = New System.Drawing.Point(163, 86)
        Me.Ln2Speed.Name = "Ln2Speed"
        Me.Ln2Speed.Size = New System.Drawing.Size(61, 31)
        Me.Ln2Speed.TabIndex = 27
        Me.Ln2Speed.TabStop = False
        Me.Ln2Speed.Text = "--- "
        Me.Ln2Speed.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'DQ2
        '
        Me.DQ2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.DQ2.Location = New System.Drawing.Point(15, 93)
        Me.DQ2.Name = "DQ2"
        Me.DQ2.Size = New System.Drawing.Size(45, 23)
        Me.DQ2.TabIndex = 9
        Me.DQ2.Text = "DQ"
        Me.DQ2.UseVisualStyleBackColor = True
        '
        'tmrCommsOK
        '
        Me.tmrCommsOK.Interval = 3000
        '
        'tmrConsXmitDelay
        '
        Me.tmrConsXmitDelay.Interval = 125
        '
        'bnExit
        '
        Me.bnExit.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.bnExit.Location = New System.Drawing.Point(211, 70)
        Me.bnExit.Name = "bnExit"
        Me.bnExit.Size = New System.Drawing.Size(64, 50)
        Me.bnExit.TabIndex = 114
        Me.bnExit.Text = "Exit"
        Me.bnExit.UseVisualStyleBackColor = True
        '
        'tmrRedSw
        '
        Me.tmrRedSw.Interval = 1000
        '
        'tmrGrnSw
        '
        Me.tmrGrnSw.Interval = 1000
        '
        'tmrAmbSw
        '
        Me.tmrAmbSw.Interval = 1000
        '
        'tmrStarterSw
        '
        Me.tmrStarterSw.Interval = 1000
        '
        'gbSystem
        '
        Me.gbSystem.Controls.Add(Me.GroupBox3)
        Me.gbSystem.Controls.Add(Me.ClearDisplayBoardOnly)
        Me.gbSystem.Controls.Add(Me.GroupBox4)
        Me.gbSystem.Controls.Add(Me.bnExit)
        Me.gbSystem.Controls.Add(Me.TestHorn)
        Me.gbSystem.Controls.Add(Me.TestDisplay)
        Me.gbSystem.Controls.Add(Me.tbError)
        Me.gbSystem.Controls.Add(Me.RcvData)
        Me.gbSystem.Location = New System.Drawing.Point(12, 27)
        Me.gbSystem.Name = "gbSystem"
        Me.gbSystem.Size = New System.Drawing.Size(281, 211)
        Me.gbSystem.TabIndex = 139
        Me.gbSystem.TabStop = False
        Me.gbSystem.Text = "System"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.tbstarter)
        Me.GroupBox3.Controls.Add(Me.tbAmbSw)
        Me.GroupBox3.Controls.Add(Me.tbGrnSw)
        Me.GroupBox3.Controls.Add(Me.TestSwitches)
        Me.GroupBox3.Controls.Add(Me.tbRedSw)
        Me.GroupBox3.Location = New System.Drawing.Point(5, 158)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(267, 49)
        Me.GroupBox3.TabIndex = 142
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Test Switches"
        '
        'tbstarter
        '
        Me.tbstarter.BackColor = System.Drawing.SystemColors.Control
        Me.tbstarter.Location = New System.Drawing.Point(216, 21)
        Me.tbstarter.Name = "tbstarter"
        Me.tbstarter.ReadOnly = True
        Me.tbstarter.Size = New System.Drawing.Size(45, 20)
        Me.tbstarter.TabIndex = 139
        Me.tbstarter.TabStop = False
        Me.tbstarter.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tbAmbSw
        '
        Me.tbAmbSw.Location = New System.Drawing.Point(167, 21)
        Me.tbAmbSw.Name = "tbAmbSw"
        Me.tbAmbSw.ReadOnly = True
        Me.tbAmbSw.Size = New System.Drawing.Size(45, 20)
        Me.tbAmbSw.TabIndex = 138
        Me.tbAmbSw.TabStop = False
        Me.tbAmbSw.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tbGrnSw
        '
        Me.tbGrnSw.Location = New System.Drawing.Point(118, 21)
        Me.tbGrnSw.Name = "tbGrnSw"
        Me.tbGrnSw.ReadOnly = True
        Me.tbGrnSw.Size = New System.Drawing.Size(45, 20)
        Me.tbGrnSw.TabIndex = 137
        Me.tbGrnSw.TabStop = False
        Me.tbGrnSw.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TestSwitches
        '
        Me.TestSwitches.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.TestSwitches.Location = New System.Drawing.Point(8, 19)
        Me.TestSwitches.Name = "TestSwitches"
        Me.TestSwitches.Size = New System.Drawing.Size(55, 23)
        Me.TestSwitches.TabIndex = 135
        Me.TestSwitches.Text = "Test"
        Me.TestSwitches.UseVisualStyleBackColor = True
        '
        'tbRedSw
        '
        Me.tbRedSw.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbRedSw.Location = New System.Drawing.Point(69, 21)
        Me.tbRedSw.Name = "tbRedSw"
        Me.tbRedSw.ReadOnly = True
        Me.tbRedSw.Size = New System.Drawing.Size(45, 20)
        Me.tbRedSw.TabIndex = 136
        Me.tbRedSw.TabStop = False
        Me.tbRedSw.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'ClearDisplayBoardOnly
        '
        Me.ClearDisplayBoardOnly.Location = New System.Drawing.Point(99, 127)
        Me.ClearDisplayBoardOnly.Name = "ClearDisplayBoardOnly"
        Me.ClearDisplayBoardOnly.Size = New System.Drawing.Size(82, 25)
        Me.ClearDisplayBoardOnly.TabIndex = 141
        Me.ClearDisplayBoardOnly.Text = "Clear Display"
        Me.ClearDisplayBoardOnly.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.DecDisplay)
        Me.GroupBox4.Controls.Add(Me.IncDisplay)
        Me.GroupBox4.Controls.Add(Me.Bar6)
        Me.GroupBox4.Controls.Add(Me.Bar5)
        Me.GroupBox4.Controls.Add(Me.Bar4)
        Me.GroupBox4.Controls.Add(Me.Bar3)
        Me.GroupBox4.Controls.Add(Me.Bar2)
        Me.GroupBox4.Controls.Add(Me.Bar1)
        Me.GroupBox4.Location = New System.Drawing.Point(7, 68)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(198, 53)
        Me.GroupBox4.TabIndex = 140
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Display Brightness"
        '
        'DecDisplay
        '
        Me.DecDisplay.Location = New System.Drawing.Point(6, 19)
        Me.DecDisplay.Name = "DecDisplay"
        Me.DecDisplay.Size = New System.Drawing.Size(21, 25)
        Me.DecDisplay.TabIndex = 146
        Me.DecDisplay.Text = "-"
        Me.DecDisplay.UseVisualStyleBackColor = True
        '
        'IncDisplay
        '
        Me.IncDisplay.Location = New System.Drawing.Point(159, 19)
        Me.IncDisplay.Name = "IncDisplay"
        Me.IncDisplay.Size = New System.Drawing.Size(21, 25)
        Me.IncDisplay.TabIndex = 145
        Me.IncDisplay.Text = "+"
        Me.IncDisplay.UseVisualStyleBackColor = True
        '
        'Bar6
        '
        Me.Bar6.BackColor = System.Drawing.Color.Cyan
        Me.Bar6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Bar6.Location = New System.Drawing.Point(133, 22)
        Me.Bar6.Name = "Bar6"
        Me.Bar6.Size = New System.Drawing.Size(20, 20)
        Me.Bar6.TabIndex = 144
        '
        'Bar5
        '
        Me.Bar5.BackColor = System.Drawing.Color.Cyan
        Me.Bar5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Bar5.Location = New System.Drawing.Point(113, 22)
        Me.Bar5.Name = "Bar5"
        Me.Bar5.Size = New System.Drawing.Size(20, 20)
        Me.Bar5.TabIndex = 143
        '
        'Bar4
        '
        Me.Bar4.BackColor = System.Drawing.Color.Cyan
        Me.Bar4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Bar4.Location = New System.Drawing.Point(93, 22)
        Me.Bar4.Name = "Bar4"
        Me.Bar4.Size = New System.Drawing.Size(20, 20)
        Me.Bar4.TabIndex = 142
        '
        'Bar3
        '
        Me.Bar3.BackColor = System.Drawing.Color.Cyan
        Me.Bar3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Bar3.Location = New System.Drawing.Point(73, 22)
        Me.Bar3.Name = "Bar3"
        Me.Bar3.Size = New System.Drawing.Size(20, 20)
        Me.Bar3.TabIndex = 141
        '
        'Bar2
        '
        Me.Bar2.BackColor = System.Drawing.Color.Cyan
        Me.Bar2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Bar2.Location = New System.Drawing.Point(53, 22)
        Me.Bar2.Name = "Bar2"
        Me.Bar2.Size = New System.Drawing.Size(20, 20)
        Me.Bar2.TabIndex = 140
        '
        'Bar1
        '
        Me.Bar1.BackColor = System.Drawing.Color.Cyan
        Me.Bar1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Bar1.Location = New System.Drawing.Point(33, 22)
        Me.Bar1.Name = "Bar1"
        Me.Bar1.Size = New System.Drawing.Size(20, 20)
        Me.Bar1.TabIndex = 139
        '
        'TestHorn
        '
        Me.TestHorn.Location = New System.Drawing.Point(193, 127)
        Me.TestHorn.Name = "TestHorn"
        Me.TestHorn.Size = New System.Drawing.Size(82, 25)
        Me.TestHorn.TabIndex = 134
        Me.TestHorn.Text = "Test Horn"
        Me.TestHorn.UseVisualStyleBackColor = True
        '
        'TestDisplay
        '
        Me.TestDisplay.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.TestDisplay.Location = New System.Drawing.Point(6, 127)
        Me.TestDisplay.Name = "TestDisplay"
        Me.TestDisplay.Size = New System.Drawing.Size(82, 25)
        Me.TestDisplay.TabIndex = 133
        Me.TestDisplay.TabStop = False
        Me.TestDisplay.Text = "Test Display"
        Me.TestDisplay.UseVisualStyleBackColor = True
        '
        'RcvData
        '
        Me.RcvData.Location = New System.Drawing.Point(6, 16)
        Me.RcvData.Name = "RcvData"
        Me.RcvData.ReadOnly = True
        Me.RcvData.Size = New System.Drawing.Size(269, 20)
        Me.RcvData.TabIndex = 118
        Me.RcvData.TabStop = False
        Me.RcvData.Text = "Lap Counter Communication Status"
        Me.RcvData.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tbClock
        '
        Me.tbClock.Location = New System.Drawing.Point(217, 151)
        Me.tbClock.Name = "tbClock"
        Me.tbClock.Size = New System.Drawing.Size(55, 20)
        Me.tbClock.TabIndex = 118
        Me.tbClock.TabStop = False
        Me.tbClock.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.tbClock.Visible = False
        '
        'lbReady
        '
        Me.lbReady.AutoSize = True
        Me.lbReady.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lbReady.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lbReady.Location = New System.Drawing.Point(137, 115)
        Me.lbReady.Name = "lbReady"
        Me.lbReady.Size = New System.Drawing.Size(119, 15)
        Me.lbReady.TabIndex = 124
        Me.lbReady.Text = "Ready For Starter"
        Me.lbReady.Visible = False
        '
        'bnNextRace
        '
        Me.bnNextRace.BackColor = System.Drawing.Color.Silver
        Me.bnNextRace.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.bnNextRace.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.bnNextRace.Location = New System.Drawing.Point(9, 145)
        Me.bnNextRace.Name = "bnNextRace"
        Me.bnNextRace.Size = New System.Drawing.Size(121, 30)
        Me.bnNextRace.TabIndex = 3
        Me.bnNextRace.Text = "Next Race"
        Me.bnNextRace.UseVisualStyleBackColor = False
        '
        'bnStartRace
        '
        Me.bnStartRace.BackColor = System.Drawing.Color.Silver
        Me.bnStartRace.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.bnStartRace.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.bnStartRace.Location = New System.Drawing.Point(9, 109)
        Me.bnStartRace.Name = "bnStartRace"
        Me.bnStartRace.Size = New System.Drawing.Size(121, 30)
        Me.bnStartRace.TabIndex = 2
        Me.bnStartRace.Text = "Start Countdown"
        Me.bnStartRace.UseVisualStyleBackColor = False
        '
        'bnSetupRace
        '
        Me.bnSetupRace.BackColor = System.Drawing.Color.Silver
        Me.bnSetupRace.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.bnSetupRace.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.bnSetupRace.Location = New System.Drawing.Point(9, 73)
        Me.bnSetupRace.Name = "bnSetupRace"
        Me.bnSetupRace.Size = New System.Drawing.Size(121, 30)
        Me.bnSetupRace.TabIndex = 1
        Me.bnSetupRace.Text = "Setup Race"
        Me.bnSetupRace.UseVisualStyleBackColor = False
        '
        'ClkLabel
        '
        Me.ClkLabel.AutoSize = True
        Me.ClkLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.ClkLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.ClkLabel.Location = New System.Drawing.Point(137, 154)
        Me.ClkLabel.Name = "ClkLabel"
        Me.ClkLabel.Size = New System.Drawing.Size(70, 13)
        Me.ClkLabel.TabIndex = 121
        Me.ClkLabel.Text = "Clock Time"
        Me.ClkLabel.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(9, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(48, 20)
        Me.Label1.TabIndex = 30
        Me.Label1.Text = "Class"
        '
        'RaceLength
        '
        Me.RaceLength.BackColor = System.Drawing.Color.LightGray
        Me.RaceLength.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RaceLength.Location = New System.Drawing.Point(161, 45)
        Me.RaceLength.Name = "RaceLength"
        Me.RaceLength.Size = New System.Drawing.Size(67, 26)
        Me.RaceLength.TabIndex = 126
        Me.RaceLength.TabStop = False
        Me.RaceLength.Text = "xxx"
        Me.RaceLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblTimeOrDistance
        '
        Me.lblTimeOrDistance.AutoSize = True
        Me.lblTimeOrDistance.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTimeOrDistance.Location = New System.Drawing.Point(231, 48)
        Me.lblTimeOrDistance.Name = "lblTimeOrDistance"
        Me.lblTimeOrDistance.Size = New System.Drawing.Size(44, 20)
        Me.lblTimeOrDistance.TabIndex = 127
        Me.lblTimeOrDistance.Text = "Laps"
        '
        'ClassName
        '
        Me.ClassName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ClassName.FormattingEnabled = True
        Me.ClassName.Location = New System.Drawing.Point(78, 18)
        Me.ClassName.Name = "ClassName"
        Me.ClassName.Size = New System.Drawing.Size(197, 21)
        Me.ClassName.TabIndex = 128
        '
        'radHeat
        '
        Me.radHeat.AutoSize = True
        Me.radHeat.Checked = True
        Me.radHeat.Location = New System.Drawing.Point(13, 48)
        Me.radHeat.Name = "radHeat"
        Me.radHeat.Size = New System.Drawing.Size(48, 17)
        Me.radHeat.TabIndex = 129
        Me.radHeat.TabStop = True
        Me.radHeat.Text = "Heat"
        Me.radHeat.UseVisualStyleBackColor = True
        '
        'radFinal
        '
        Me.radFinal.AutoSize = True
        Me.radFinal.Location = New System.Drawing.Point(80, 48)
        Me.radFinal.Name = "radFinal"
        Me.radFinal.Size = New System.Drawing.Size(47, 17)
        Me.radFinal.TabIndex = 130
        Me.radFinal.TabStop = True
        Me.radFinal.Text = "Final"
        Me.radFinal.UseVisualStyleBackColor = True
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.SpeedLimitText)
        Me.GroupBox5.Controls.Add(Me.SpeedLimit)
        Me.GroupBox5.Controls.Add(Me.radFinal)
        Me.GroupBox5.Controls.Add(Me.radHeat)
        Me.GroupBox5.Controls.Add(Me.ClassName)
        Me.GroupBox5.Controls.Add(Me.lblTimeOrDistance)
        Me.GroupBox5.Controls.Add(Me.RaceLength)
        Me.GroupBox5.Controls.Add(Me.Label1)
        Me.GroupBox5.Controls.Add(Me.ClkLabel)
        Me.GroupBox5.Controls.Add(Me.bnSetupRace)
        Me.GroupBox5.Controls.Add(Me.bnStartRace)
        Me.GroupBox5.Controls.Add(Me.bnNextRace)
        Me.GroupBox5.Controls.Add(Me.lbReady)
        Me.GroupBox5.Controls.Add(Me.tbClock)
        Me.GroupBox5.Location = New System.Drawing.Point(12, 240)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(281, 181)
        Me.GroupBox5.TabIndex = 140
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Race Management"
        '
        'Ln1SpeedUnit
        '
        Me.Ln1SpeedUnit.AutoSize = True
        Me.Ln1SpeedUnit.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Ln1SpeedUnit.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Ln1SpeedUnit.Location = New System.Drawing.Point(231, 88)
        Me.Ln1SpeedUnit.Name = "Ln1SpeedUnit"
        Me.Ln1SpeedUnit.Size = New System.Drawing.Size(39, 20)
        Me.Ln1SpeedUnit.TabIndex = 27
        Me.Ln1SpeedUnit.Text = "s/10"
        '
        'Ln2SpeedUnit
        '
        Me.Ln2SpeedUnit.AutoSize = True
        Me.Ln2SpeedUnit.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Ln2SpeedUnit.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Ln2SpeedUnit.Location = New System.Drawing.Point(230, 88)
        Me.Ln2SpeedUnit.Name = "Ln2SpeedUnit"
        Me.Ln2SpeedUnit.Size = New System.Drawing.Size(39, 20)
        Me.Ln2SpeedUnit.TabIndex = 29
        Me.Ln2SpeedUnit.Text = "s/10"
        '
        'Ln3SpeedUnit
        '
        Me.Ln3SpeedUnit.AutoSize = True
        Me.Ln3SpeedUnit.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Ln3SpeedUnit.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Ln3SpeedUnit.Location = New System.Drawing.Point(230, 88)
        Me.Ln3SpeedUnit.Name = "Ln3SpeedUnit"
        Me.Ln3SpeedUnit.Size = New System.Drawing.Size(39, 20)
        Me.Ln3SpeedUnit.TabIndex = 29
        Me.Ln3SpeedUnit.Text = "s/10"
        '
        'SpeedLimitText
        '
        Me.SpeedLimitText.AutoSize = True
        Me.SpeedLimitText.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SpeedLimitText.Location = New System.Drawing.Point(231, 80)
        Me.SpeedLimitText.Name = "SpeedLimitText"
        Me.SpeedLimitText.Size = New System.Drawing.Size(39, 20)
        Me.SpeedLimitText.TabIndex = 132
        Me.SpeedLimitText.Text = "s/10"
        Me.SpeedLimitText.Visible = False
        '
        'SpeedLimit
        '
        Me.SpeedLimit.BackColor = System.Drawing.Color.LightGray
        Me.SpeedLimit.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SpeedLimit.Location = New System.Drawing.Point(161, 77)
        Me.SpeedLimit.Name = "SpeedLimit"
        Me.SpeedLimit.Size = New System.Drawing.Size(67, 26)
        Me.SpeedLimit.TabIndex = 131
        Me.SpeedLimit.TabStop = False
        Me.SpeedLimit.Text = "00.0"
        Me.SpeedLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.SpeedLimit.Visible = False
        '
        'CLTimer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(594, 431)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.gbSystem)
        Me.Controls.Add(Me.Lane1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "CLTimer"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Control Line Timer"
        Me.Lane1.ResumeLayout(False)
        Me.Lane1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.gbSystem.ResumeLayout(False)
        Me.gbSystem.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents ManualStartToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Ln2Time As System.Windows.Forms.TextBox
    Friend WithEvents CLOCKSTARTToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ManualStartToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HELPToolStripMenuItem3 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Lane2laps As System.Windows.Forms.TextBox
    Friend WithEvents Lane1Laps As System.Windows.Forms.TextBox
    Friend WithEvents SetPortNoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Lane1 As System.Windows.Forms.GroupBox
    Friend WithEvents Rerun1 As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Ln1Time As System.Windows.Forms.TextBox
    Friend WithEvents DNF1 As System.Windows.Forms.Button
    Friend WithEvents StateLn1 As System.Windows.Forms.TextBox
    Friend WithEvents Rerun2 As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Rerun3 As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Ln3Time As System.Windows.Forms.TextBox
    Friend WithEvents Lane3Laps As System.Windows.Forms.TextBox
    Friend WithEvents DNF3 As System.Windows.Forms.Button
    Friend WithEvents StateLn3 As System.Windows.Forms.TextBox
    Friend WithEvents tmrSecondsCounter As System.Windows.Forms.Timer
    Friend WithEvents tbError As System.Windows.Forms.TextBox
    Friend WithEvents SetupToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DNF2 As System.Windows.Forms.Button
    Friend WithEvents StateLn2 As System.Windows.Forms.TextBox
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents tmrCommsOK As System.Windows.Forms.Timer
    Friend WithEvents tmrConsXmitDelay As System.Windows.Forms.Timer
    Friend WithEvents bnExit As System.Windows.Forms.Button
    Friend WithEvents tmrRedSw As System.Windows.Forms.Timer
    Friend WithEvents tmrGrnSw As System.Windows.Forms.Timer
    Friend WithEvents tmrAmbSw As System.Windows.Forms.Timer
    Friend WithEvents tmrStarterSw As System.Windows.Forms.Timer
    Friend WithEvents gbSystem As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents DecDisplay As System.Windows.Forms.Button
    Friend WithEvents IncDisplay As System.Windows.Forms.Button
    Friend WithEvents Bar6 As System.Windows.Forms.TextBox
    Friend WithEvents Bar5 As System.Windows.Forms.TextBox
    Friend WithEvents Bar4 As System.Windows.Forms.TextBox
    Friend WithEvents Bar3 As System.Windows.Forms.TextBox
    Friend WithEvents Bar2 As System.Windows.Forms.TextBox
    Friend WithEvents Bar1 As System.Windows.Forms.TextBox
    Friend WithEvents tbstarter As System.Windows.Forms.TextBox
    Friend WithEvents tbAmbSw As System.Windows.Forms.TextBox
    Friend WithEvents tbGrnSw As System.Windows.Forms.TextBox
    Friend WithEvents tbRedSw As System.Windows.Forms.TextBox
    Friend WithEvents TestSwitches As System.Windows.Forms.Button
    Friend WithEvents TestHorn As System.Windows.Forms.Button
    Friend WithEvents TestDisplay As System.Windows.Forms.Button
    Friend WithEvents RcvData As System.Windows.Forms.TextBox
    Friend WithEvents ClearDisplayBoardOnly As System.Windows.Forms.Button
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents DQ1 As System.Windows.Forms.Button
    Friend WithEvents DQ3 As System.Windows.Forms.Button
    Friend WithEvents DQ2 As System.Windows.Forms.Button
    Friend WithEvents tbClock As System.Windows.Forms.TextBox
    Friend WithEvents lbReady As System.Windows.Forms.Label
    Friend WithEvents bnNextRace As System.Windows.Forms.Button
    Friend WithEvents bnStartRace As System.Windows.Forms.Button
    Friend WithEvents bnSetupRace As System.Windows.Forms.Button
    Friend WithEvents ClkLabel As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents RaceLength As System.Windows.Forms.TextBox
    Friend WithEvents lblTimeOrDistance As System.Windows.Forms.Label
    Friend WithEvents ClassName As System.Windows.Forms.ComboBox
    Friend WithEvents radHeat As System.Windows.Forms.RadioButton
    Friend WithEvents radFinal As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Ln1Speed As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Ln3Speed As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Ln2Speed As System.Windows.Forms.TextBox
    Friend WithEvents Ln1SpeedUnit As Label
    Friend WithEvents Ln3SpeedUnit As Label
    Friend WithEvents Ln2SpeedUnit As Label
    Friend WithEvents SpeedLimitText As Label
    Friend WithEvents SpeedLimit As TextBox
End Class
