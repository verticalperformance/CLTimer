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
        Me.Label4 = New System.Windows.Forms.Label()
        Me.ManualStartToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.Ln2Time = New System.Windows.Forms.TextBox()
        Me.CLOCKSTARTToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HELPToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ManualStartToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HELPToolStripMenuItem3 = New System.Windows.Forms.ToolStripMenuItem()
        Me.Com6ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Com5ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Lane2laps = New System.Windows.Forms.TextBox()
        Me.Lane1Laps = New System.Windows.Forms.TextBox()
        Me.SetPortNoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Com1ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Com2ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Com3ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Com4ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ClkLabel = New System.Windows.Forms.Label()
        Me.ClkTest = New System.Windows.Forms.Button()
        Me.Sw_test = New System.Windows.Forms.Button()
        Me.RaceformatToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RUN100LAPSToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RUN200LAPSToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.bnStart = New System.Windows.Forms.Button()
        Me.Lane1 = New System.Windows.Forms.GroupBox()
        Me.Rerun1 = New System.Windows.Forms.Button()
        Me.Name1 = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Ln1Time = New System.Windows.Forms.TextBox()
        Me.DNF1 = New System.Windows.Forms.Button()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.StateLn1 = New System.Windows.Forms.TextBox()
        Me.Rerun2 = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Rerun3 = New System.Windows.Forms.Button()
        Me.Name3 = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Ln3Time = New System.Windows.Forms.TextBox()
        Me.Lane3Laps = New System.Windows.Forms.TextBox()
        Me.DNF3 = New System.Windows.Forms.Button()
        Me.StateLn3 = New System.Windows.Forms.TextBox()
        Me.lbReady = New System.Windows.Forms.Label()
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.tbError = New System.Windows.Forms.TextBox()
        Me.tbRedSw = New System.Windows.Forms.TextBox()
        Me.SetupToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RcvData = New System.Windows.Forms.TextBox()
        Me.DNF2 = New System.Windows.Forms.Button()
        Me.StateLn2 = New System.Windows.Forms.TextBox()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.bnSave = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Name2 = New System.Windows.Forms.TextBox()
        Me.bnSetup = New System.Windows.Forms.Button()
        Me.tbStart = New System.Windows.Forms.TextBox()
        Me.Timer3 = New System.Windows.Forms.Timer(Me.components)
        Me.Timer4 = New System.Windows.Forms.Timer(Me.components)
        Me.bnExit = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.RaceFormat = New System.Windows.Forms.TextBox()
        Me.tbGrnSw = New System.Windows.Forms.TextBox()
        Me.tbAmbSw = New System.Windows.Forms.TextBox()
        Me.Timer5 = New System.Windows.Forms.Timer(Me.components)
        Me.Timer6 = New System.Windows.Forms.Timer(Me.components)
        Me.Timer7 = New System.Windows.Forms.Timer(Me.components)
        Me.tbstarter = New System.Windows.Forms.TextBox()
        Me.Timer8 = New System.Windows.Forms.Timer(Me.components)
        Me.IncDisplay = New System.Windows.Forms.Button()
        Me.DecDisplay = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Bar1 = New System.Windows.Forms.TextBox()
        Me.Bar2 = New System.Windows.Forms.TextBox()
        Me.Bar3 = New System.Windows.Forms.TextBox()
        Me.Bar4 = New System.Windows.Forms.TextBox()
        Me.Bar5 = New System.Windows.Forms.TextBox()
        Me.Bar6 = New System.Windows.Forms.TextBox()
        Me.Lane1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label4.Location = New System.Drawing.Point(391, 27)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(44, 20)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "Laps"
        '
        'ManualStartToolStripMenuItem1
        '
        Me.ManualStartToolStripMenuItem1.Name = "ManualStartToolStripMenuItem1"
        Me.ManualStartToolStripMenuItem1.Size = New System.Drawing.Size(184, 22)
        Me.ManualStartToolStripMenuItem1.Text = "Manual Start"
        '
        'Ln2Time
        '
        Me.Ln2Time.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Ln2Time.Location = New System.Drawing.Point(457, 69)
        Me.Ln2Time.Name = "Ln2Time"
        Me.Ln2Time.Size = New System.Drawing.Size(106, 31)
        Me.Ln2Time.TabIndex = 12
        Me.Ln2Time.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'CLOCKSTARTToolStripMenuItem
        '
        Me.CLOCKSTARTToolStripMenuItem.Name = "CLOCKSTARTToolStripMenuItem"
        Me.CLOCKSTARTToolStripMenuItem.Size = New System.Drawing.Size(184, 22)
        Me.CLOCKSTARTToolStripMenuItem.Text = " Display Countdown"
        '
        'HELPToolStripMenuItem1
        '
        Me.HELPToolStripMenuItem1.Name = "HELPToolStripMenuItem1"
        Me.HELPToolStripMenuItem1.Size = New System.Drawing.Size(109, 22)
        Me.HELPToolStripMenuItem1.Text = "HELP"
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
        Me.HELPToolStripMenuItem3.Size = New System.Drawing.Size(184, 22)
        Me.HELPToolStripMenuItem3.Text = "HELP"
        '
        'Com6ToolStripMenuItem
        '
        Me.Com6ToolStripMenuItem.Name = "Com6ToolStripMenuItem"
        Me.Com6ToolStripMenuItem.Size = New System.Drawing.Size(109, 22)
        Me.Com6ToolStripMenuItem.Text = "Com 6"
        '
        'Com5ToolStripMenuItem
        '
        Me.Com5ToolStripMenuItem.Name = "Com5ToolStripMenuItem"
        Me.Com5ToolStripMenuItem.Size = New System.Drawing.Size(109, 22)
        Me.Com5ToolStripMenuItem.Text = "Com 5"
        '
        'Lane2laps
        '
        Me.Lane2laps.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lane2laps.Location = New System.Drawing.Point(457, 20)
        Me.Lane2laps.Name = "Lane2laps"
        Me.Lane2laps.Size = New System.Drawing.Size(60, 31)
        Me.Lane2laps.TabIndex = 11
        Me.Lane2laps.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Lane1Laps
        '
        Me.Lane1Laps.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lane1Laps.Location = New System.Drawing.Point(457, 22)
        Me.Lane1Laps.Name = "Lane1Laps"
        Me.Lane1Laps.Size = New System.Drawing.Size(60, 31)
        Me.Lane1Laps.TabIndex = 10
        Me.Lane1Laps.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'SetPortNoToolStripMenuItem
        '
        Me.SetPortNoToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Com1ToolStripMenuItem, Me.Com2ToolStripMenuItem, Me.Com3ToolStripMenuItem, Me.Com4ToolStripMenuItem, Me.Com5ToolStripMenuItem, Me.Com6ToolStripMenuItem, Me.HELPToolStripMenuItem1})
        Me.SetPortNoToolStripMenuItem.Name = "SetPortNoToolStripMenuItem"
        Me.SetPortNoToolStripMenuItem.Size = New System.Drawing.Size(150, 22)
        Me.SetPortNoToolStripMenuItem.Text = "Set Port 1 No."
        '
        'Com1ToolStripMenuItem
        '
        Me.Com1ToolStripMenuItem.Name = "Com1ToolStripMenuItem"
        Me.Com1ToolStripMenuItem.ShowShortcutKeys = False
        Me.Com1ToolStripMenuItem.Size = New System.Drawing.Size(109, 22)
        Me.Com1ToolStripMenuItem.Text = "Com 1"
        '
        'Com2ToolStripMenuItem
        '
        Me.Com2ToolStripMenuItem.Name = "Com2ToolStripMenuItem"
        Me.Com2ToolStripMenuItem.ShowShortcutKeys = False
        Me.Com2ToolStripMenuItem.Size = New System.Drawing.Size(109, 22)
        Me.Com2ToolStripMenuItem.Text = "Com 2"
        '
        'Com3ToolStripMenuItem
        '
        Me.Com3ToolStripMenuItem.Name = "Com3ToolStripMenuItem"
        Me.Com3ToolStripMenuItem.ShowShortcutKeys = False
        Me.Com3ToolStripMenuItem.Size = New System.Drawing.Size(109, 22)
        Me.Com3ToolStripMenuItem.Text = "Com 3"
        '
        'Com4ToolStripMenuItem
        '
        Me.Com4ToolStripMenuItem.Name = "Com4ToolStripMenuItem"
        Me.Com4ToolStripMenuItem.ShowShortcutKeys = False
        Me.Com4ToolStripMenuItem.Size = New System.Drawing.Size(109, 22)
        Me.Com4ToolStripMenuItem.Text = "Com 4"
        '
        'ClkLabel
        '
        Me.ClkLabel.AutoSize = True
        Me.ClkLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.ClkLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.ClkLabel.Location = New System.Drawing.Point(573, 571)
        Me.ClkLabel.Name = "ClkLabel"
        Me.ClkLabel.Size = New System.Drawing.Size(70, 13)
        Me.ClkLabel.TabIndex = 121
        Me.ClkLabel.Text = "Clock Time"
        Me.ClkLabel.Visible = False
        '
        'ClkTest
        '
        Me.ClkTest.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.ClkTest.Location = New System.Drawing.Point(20, 405)
        Me.ClkTest.Name = "ClkTest"
        Me.ClkTest.Size = New System.Drawing.Size(80, 25)
        Me.ClkTest.TabIndex = 119
        Me.ClkTest.TabStop = False
        Me.ClkTest.Text = "Test Display"
        Me.ClkTest.UseVisualStyleBackColor = True
        '
        'Sw_test
        '
        Me.Sw_test.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Sw_test.Location = New System.Drawing.Point(20, 567)
        Me.Sw_test.Name = "Sw_test"
        Me.Sw_test.Size = New System.Drawing.Size(80, 35)
        Me.Sw_test.TabIndex = 122
        Me.Sw_test.Text = "Test Timer Switches"
        Me.Sw_test.UseVisualStyleBackColor = True
        '
        'RaceformatToolStripMenuItem
        '
        Me.RaceformatToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RUN100LAPSToolStripMenuItem, Me.RUN200LAPSToolStripMenuItem})
        Me.RaceformatToolStripMenuItem.Name = "RaceformatToolStripMenuItem"
        Me.RaceformatToolStripMenuItem.Size = New System.Drawing.Size(150, 22)
        Me.RaceformatToolStripMenuItem.Text = "Raceformat"
        '
        'RUN100LAPSToolStripMenuItem
        '
        Me.RUN100LAPSToolStripMenuItem.Name = "RUN100LAPSToolStripMenuItem"
        Me.RUN100LAPSToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.RUN100LAPSToolStripMenuItem.Text = "RUN 100 LAPS"
        '
        'RUN200LAPSToolStripMenuItem
        '
        Me.RUN200LAPSToolStripMenuItem.Name = "RUN200LAPSToolStripMenuItem"
        Me.RUN200LAPSToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.RUN200LAPSToolStripMenuItem.Text = "RUN 200 LAPS"
        '
        'bnStart
        '
        Me.bnStart.BackColor = System.Drawing.Color.Silver
        Me.bnStart.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.bnStart.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.bnStart.Location = New System.Drawing.Point(562, 507)
        Me.bnStart.Name = "bnStart"
        Me.bnStart.Size = New System.Drawing.Size(129, 30)
        Me.bnStart.TabIndex = 115
        Me.bnStart.Text = "Start Countdown"
        Me.bnStart.UseVisualStyleBackColor = False
        '
        'Lane1
        '
        Me.Lane1.BackColor = System.Drawing.Color.Firebrick
        Me.Lane1.Controls.Add(Me.Rerun1)
        Me.Lane1.Controls.Add(Me.Name1)
        Me.Lane1.Controls.Add(Me.Label2)
        Me.Lane1.Controls.Add(Me.Ln1Time)
        Me.Lane1.Controls.Add(Me.DNF1)
        Me.Lane1.Controls.Add(Me.Label24)
        Me.Lane1.Controls.Add(Me.StateLn1)
        Me.Lane1.Controls.Add(Me.Lane1Laps)
        Me.Lane1.Location = New System.Drawing.Point(141, 27)
        Me.Lane1.Name = "Lane1"
        Me.Lane1.Size = New System.Drawing.Size(677, 123)
        Me.Lane1.TabIndex = 109
        Me.Lane1.TabStop = False
        '
        'Rerun1
        '
        Me.Rerun1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Rerun1.Location = New System.Drawing.Point(158, 16)
        Me.Rerun1.Name = "Rerun1"
        Me.Rerun1.Size = New System.Drawing.Size(45, 23)
        Me.Rerun1.TabIndex = 29
        Me.Rerun1.Text = "RR"
        Me.Rerun1.UseVisualStyleBackColor = True
        '
        'Name1
        '
        Me.Name1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Name1.Location = New System.Drawing.Point(23, 59)
        Me.Name1.Name = "Name1"
        Me.Name1.Size = New System.Drawing.Size(162, 20)
        Me.Name1.TabIndex = 17
        Me.Name1.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label2.Location = New System.Drawing.Point(393, 73)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(43, 20)
        Me.Label2.TabIndex = 14
        Me.Label2.Text = "Time"
        '
        'Ln1Time
        '
        Me.Ln1Time.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Ln1Time.Location = New System.Drawing.Point(457, 73)
        Me.Ln1Time.Name = "Ln1Time"
        Me.Ln1Time.Size = New System.Drawing.Size(106, 31)
        Me.Ln1Time.TabIndex = 12
        Me.Ln1Time.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'DNF1
        '
        Me.DNF1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.DNF1.Location = New System.Drawing.Point(98, 16)
        Me.DNF1.Name = "DNF1"
        Me.DNF1.Size = New System.Drawing.Size(45, 23)
        Me.DNF1.TabIndex = 1
        Me.DNF1.Text = "DNF"
        Me.DNF1.UseVisualStyleBackColor = True
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label24.Location = New System.Drawing.Point(392, 29)
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
        Me.StateLn1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Rerun2
        '
        Me.Rerun2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Rerun2.Location = New System.Drawing.Point(158, 17)
        Me.Rerun2.Name = "Rerun2"
        Me.Rerun2.Size = New System.Drawing.Size(45, 23)
        Me.Rerun2.TabIndex = 31
        Me.Rerun2.Text = "RR"
        Me.Rerun2.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label3.Location = New System.Drawing.Point(392, 76)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(43, 20)
        Me.Label3.TabIndex = 14
        Me.Label3.Text = "Time"
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.Goldenrod
        Me.GroupBox2.Controls.Add(Me.Rerun3)
        Me.GroupBox2.Controls.Add(Me.Name3)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.Ln3Time)
        Me.GroupBox2.Controls.Add(Me.Lane3Laps)
        Me.GroupBox2.Controls.Add(Me.DNF3)
        Me.GroupBox2.Controls.Add(Me.StateLn3)
        Me.GroupBox2.Location = New System.Drawing.Point(141, 328)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(684, 130)
        Me.GroupBox2.TabIndex = 112
        Me.GroupBox2.TabStop = False
        '
        'Rerun3
        '
        Me.Rerun3.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Rerun3.Location = New System.Drawing.Point(158, 16)
        Me.Rerun3.Name = "Rerun3"
        Me.Rerun3.Size = New System.Drawing.Size(45, 23)
        Me.Rerun3.TabIndex = 32
        Me.Rerun3.Text = "RR"
        Me.Rerun3.UseVisualStyleBackColor = True
        '
        'Name3
        '
        Me.Name3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Name3.Location = New System.Drawing.Point(23, 58)
        Me.Name3.Name = "Name3"
        Me.Name3.Size = New System.Drawing.Size(162, 20)
        Me.Name3.TabIndex = 16
        Me.Name3.Visible = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label6.Location = New System.Drawing.Point(391, 77)
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
        Me.Label7.Location = New System.Drawing.Point(393, 29)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(44, 20)
        Me.Label7.TabIndex = 13
        Me.Label7.Text = "Laps"
        '
        'Ln3Time
        '
        Me.Ln3Time.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Ln3Time.Location = New System.Drawing.Point(457, 70)
        Me.Ln3Time.Name = "Ln3Time"
        Me.Ln3Time.Size = New System.Drawing.Size(106, 31)
        Me.Ln3Time.TabIndex = 12
        Me.Ln3Time.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Lane3Laps
        '
        Me.Lane3Laps.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lane3Laps.Location = New System.Drawing.Point(457, 22)
        Me.Lane3Laps.Name = "Lane3Laps"
        Me.Lane3Laps.Size = New System.Drawing.Size(60, 31)
        Me.Lane3Laps.TabIndex = 11
        Me.Lane3Laps.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'DNF3
        '
        Me.DNF3.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.DNF3.Location = New System.Drawing.Point(98, 16)
        Me.DNF3.Name = "DNF3"
        Me.DNF3.Size = New System.Drawing.Size(45, 23)
        Me.DNF3.TabIndex = 1
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
        Me.StateLn3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lbReady
        '
        Me.lbReady.AutoSize = True
        Me.lbReady.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lbReady.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lbReady.Location = New System.Drawing.Point(563, 543)
        Me.lbReady.Name = "lbReady"
        Me.lbReady.Size = New System.Drawing.Size(119, 15)
        Me.lbReady.TabIndex = 124
        Me.lbReady.Text = "Ready For Starter"
        Me.lbReady.Visible = False
        '
        'Timer2
        '
        Me.Timer2.Interval = 1000
        '
        'tbError
        '
        Me.tbError.BackColor = System.Drawing.Color.LightGray
        Me.tbError.Location = New System.Drawing.Point(455, 582)
        Me.tbError.Name = "tbError"
        Me.tbError.Size = New System.Drawing.Size(59, 20)
        Me.tbError.TabIndex = 120
        Me.tbError.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.tbError.Visible = False
        '
        'tbRedSw
        '
        Me.tbRedSw.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbRedSw.Location = New System.Drawing.Point(116, 573)
        Me.tbRedSw.Name = "tbRedSw"
        Me.tbRedSw.Size = New System.Drawing.Size(55, 20)
        Me.tbRedSw.TabIndex = 123
        Me.tbRedSw.Visible = False
        '
        'SetupToolStripMenuItem
        '
        Me.SetupToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RaceformatToolStripMenuItem, Me.ManualStartToolStripMenuItem, Me.SetPortNoToolStripMenuItem})
        Me.SetupToolStripMenuItem.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.SetupToolStripMenuItem.Name = "SetupToolStripMenuItem"
        Me.SetupToolStripMenuItem.Size = New System.Drawing.Size(52, 20)
        Me.SetupToolStripMenuItem.Text = "Setup"
        '
        'RcvData
        '
        Me.RcvData.Location = New System.Drawing.Point(20, 361)
        Me.RcvData.Name = "RcvData"
        Me.RcvData.Size = New System.Drawing.Size(74, 20)
        Me.RcvData.TabIndex = 116
        Me.RcvData.Text = "Rcv Timers"
        Me.RcvData.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'DNF2
        '
        Me.DNF2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.DNF2.Location = New System.Drawing.Point(98, 16)
        Me.DNF2.Name = "DNF2"
        Me.DNF2.Size = New System.Drawing.Size(45, 23)
        Me.DNF2.TabIndex = 1
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
        Me.StateLn2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SetupToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
        Me.MenuStrip1.Size = New System.Drawing.Size(974, 24)
        Me.MenuStrip1.TabIndex = 125
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'bnSave
        '
        Me.bnSave.BackColor = System.Drawing.Color.Silver
        Me.bnSave.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.bnSave.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.bnSave.Location = New System.Drawing.Point(725, 507)
        Me.bnSave.Name = "bnSave"
        Me.bnSave.Size = New System.Drawing.Size(100, 30)
        Me.bnSave.TabIndex = 110
        Me.bnSave.Text = "Clear Display"
        Me.bnSave.UseVisualStyleBackColor = False
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.OliveDrab
        Me.GroupBox1.Controls.Add(Me.Rerun2)
        Me.GroupBox1.Controls.Add(Me.Name2)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Ln2Time)
        Me.GroupBox1.Controls.Add(Me.Lane2laps)
        Me.GroupBox1.Controls.Add(Me.DNF2)
        Me.GroupBox1.Controls.Add(Me.StateLn2)
        Me.GroupBox1.Location = New System.Drawing.Point(141, 174)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(677, 130)
        Me.GroupBox1.TabIndex = 111
        Me.GroupBox1.TabStop = False
        '
        'Name2
        '
        Me.Name2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Name2.Location = New System.Drawing.Point(23, 59)
        Me.Name2.Name = "Name2"
        Me.Name2.Size = New System.Drawing.Size(162, 20)
        Me.Name2.TabIndex = 16
        Me.Name2.Visible = False
        '
        'bnSetup
        '
        Me.bnSetup.BackColor = System.Drawing.Color.Silver
        Me.bnSetup.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.bnSetup.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.bnSetup.Location = New System.Drawing.Point(411, 507)
        Me.bnSetup.Name = "bnSetup"
        Me.bnSetup.Size = New System.Drawing.Size(121, 30)
        Me.bnSetup.TabIndex = 117
        Me.bnSetup.Text = "Setup Race"
        Me.bnSetup.UseVisualStyleBackColor = False
        '
        'tbStart
        '
        Me.tbStart.Location = New System.Drawing.Point(649, 568)
        Me.tbStart.Name = "tbStart"
        Me.tbStart.Size = New System.Drawing.Size(30, 20)
        Me.tbStart.TabIndex = 118
        Me.tbStart.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.tbStart.Visible = False
        '
        'Timer3
        '
        Me.Timer3.Interval = 3000
        '
        'Timer4
        '
        Me.Timer4.Interval = 125
        '
        'bnExit
        '
        Me.bnExit.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.bnExit.Location = New System.Drawing.Point(906, 590)
        Me.bnExit.Name = "bnExit"
        Me.bnExit.Size = New System.Drawing.Size(56, 23)
        Me.bnExit.TabIndex = 114
        Me.bnExit.Text = "Exit"
        Me.bnExit.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(144, 471)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(93, 20)
        Me.Label1.TabIndex = 30
        Me.Label1.Text = "Raceformat"
        '
        'RaceFormat
        '
        Me.RaceFormat.BackColor = System.Drawing.Color.LightGray
        Me.RaceFormat.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RaceFormat.Location = New System.Drawing.Point(239, 468)
        Me.RaceFormat.Name = "RaceFormat"
        Me.RaceFormat.Size = New System.Drawing.Size(168, 26)
        Me.RaceFormat.TabIndex = 126
        Me.RaceFormat.Text = "100 Laps"
        '
        'tbGrnSw
        '
        Me.tbGrnSw.Location = New System.Drawing.Point(177, 575)
        Me.tbGrnSw.Name = "tbGrnSw"
        Me.tbGrnSw.Size = New System.Drawing.Size(55, 20)
        Me.tbGrnSw.TabIndex = 127
        Me.tbGrnSw.Visible = False
        '
        'tbAmbSw
        '
        Me.tbAmbSw.Location = New System.Drawing.Point(239, 575)
        Me.tbAmbSw.Name = "tbAmbSw"
        Me.tbAmbSw.Size = New System.Drawing.Size(55, 20)
        Me.tbAmbSw.TabIndex = 128
        Me.tbAmbSw.Visible = False
        '
        'Timer5
        '
        Me.Timer5.Interval = 1000
        '
        'Timer6
        '
        Me.Timer6.Interval = 1000
        '
        'Timer7
        '
        Me.Timer7.Interval = 1000
        '
        'tbstarter
        '
        Me.tbstarter.BackColor = System.Drawing.SystemColors.Window
        Me.tbstarter.Location = New System.Drawing.Point(299, 575)
        Me.tbstarter.Name = "tbstarter"
        Me.tbstarter.Size = New System.Drawing.Size(55, 20)
        Me.tbstarter.TabIndex = 129
        Me.tbstarter.Visible = False
        '
        'Timer8
        '
        Me.Timer8.Interval = 1000
        '
        'IncDisplay
        '
        Me.IncDisplay.Location = New System.Drawing.Point(20, 489)
        Me.IncDisplay.Name = "IncDisplay"
        Me.IncDisplay.Size = New System.Drawing.Size(80, 25)
        Me.IncDisplay.TabIndex = 130
        Me.IncDisplay.Text = "Inc Display"
        Me.IncDisplay.UseVisualStyleBackColor = True
        '
        'DecDisplay
        '
        Me.DecDisplay.Location = New System.Drawing.Point(20, 521)
        Me.DecDisplay.Name = "DecDisplay"
        Me.DecDisplay.Size = New System.Drawing.Size(80, 25)
        Me.DecDisplay.TabIndex = 131
        Me.DecDisplay.Text = "Dec Display"
        Me.DecDisplay.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(20, 437)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(80, 25)
        Me.Button1.TabIndex = 132
        Me.Button1.Text = "Test Aux"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Bar1
        '
        Me.Bar1.BackColor = System.Drawing.Color.Cyan
        Me.Bar1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Bar1.Location = New System.Drawing.Point(116, 509)
        Me.Bar1.Name = "Bar1"
        Me.Bar1.Size = New System.Drawing.Size(20, 20)
        Me.Bar1.TabIndex = 133
        '
        'Bar2
        '
        Me.Bar2.BackColor = System.Drawing.Color.Cyan
        Me.Bar2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Bar2.Location = New System.Drawing.Point(135, 509)
        Me.Bar2.Name = "Bar2"
        Me.Bar2.Size = New System.Drawing.Size(20, 20)
        Me.Bar2.TabIndex = 134
        '
        'Bar3
        '
        Me.Bar3.BackColor = System.Drawing.Color.Cyan
        Me.Bar3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Bar3.Location = New System.Drawing.Point(154, 509)
        Me.Bar3.Name = "Bar3"
        Me.Bar3.Size = New System.Drawing.Size(20, 20)
        Me.Bar3.TabIndex = 135
        '
        'Bar4
        '
        Me.Bar4.BackColor = System.Drawing.Color.Cyan
        Me.Bar4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Bar4.Location = New System.Drawing.Point(174, 509)
        Me.Bar4.Name = "Bar4"
        Me.Bar4.Size = New System.Drawing.Size(20, 20)
        Me.Bar4.TabIndex = 136
        '
        'Bar5
        '
        Me.Bar5.BackColor = System.Drawing.Color.Cyan
        Me.Bar5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Bar5.Location = New System.Drawing.Point(194, 509)
        Me.Bar5.Name = "Bar5"
        Me.Bar5.Size = New System.Drawing.Size(20, 20)
        Me.Bar5.TabIndex = 137
        '
        'Bar6
        '
        Me.Bar6.BackColor = System.Drawing.Color.Cyan
        Me.Bar6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Bar6.Location = New System.Drawing.Point(214, 509)
        Me.Bar6.Name = "Bar6"
        Me.Bar6.Size = New System.Drawing.Size(20, 20)
        Me.Bar6.TabIndex = 138
        '
        'CLTimer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(974, 625)
        Me.Controls.Add(Me.Bar6)
        Me.Controls.Add(Me.Bar5)
        Me.Controls.Add(Me.Bar4)
        Me.Controls.Add(Me.Bar3)
        Me.Controls.Add(Me.Bar2)
        Me.Controls.Add(Me.Bar1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.DecDisplay)
        Me.Controls.Add(Me.IncDisplay)
        Me.Controls.Add(Me.tbstarter)
        Me.Controls.Add(Me.tbAmbSw)
        Me.Controls.Add(Me.tbGrnSw)
        Me.Controls.Add(Me.RaceFormat)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ClkLabel)
        Me.Controls.Add(Me.ClkTest)
        Me.Controls.Add(Me.Sw_test)
        Me.Controls.Add(Me.bnStart)
        Me.Controls.Add(Me.Lane1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.lbReady)
        Me.Controls.Add(Me.tbError)
        Me.Controls.Add(Me.tbRedSw)
        Me.Controls.Add(Me.RcvData)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.bnSave)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.bnSetup)
        Me.Controls.Add(Me.tbStart)
        Me.Controls.Add(Me.bnExit)
        Me.Name = "CLTimer"
        Me.Text = "Control Line Timer"
        Me.Lane1.ResumeLayout(False)
        Me.Lane1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents ManualStartToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Ln2Time As System.Windows.Forms.TextBox
    Friend WithEvents CLOCKSTARTToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HELPToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ManualStartToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HELPToolStripMenuItem3 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Com6ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Com5ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Lane2laps As System.Windows.Forms.TextBox
    Friend WithEvents Lane1Laps As System.Windows.Forms.TextBox
    Friend WithEvents SetPortNoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Com1ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Com2ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Com3ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Com4ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ClkLabel As System.Windows.Forms.Label
    Friend WithEvents ClkTest As System.Windows.Forms.Button
    Friend WithEvents Sw_test As System.Windows.Forms.Button
    Friend WithEvents RaceformatToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RUN100LAPSToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RUN200LAPSToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents bnStart As System.Windows.Forms.Button
    Friend WithEvents Lane1 As System.Windows.Forms.GroupBox
    Friend WithEvents Rerun1 As System.Windows.Forms.Button
    Friend WithEvents Name1 As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Ln1Time As System.Windows.Forms.TextBox
    Friend WithEvents DNF1 As System.Windows.Forms.Button
    Friend WithEvents StateLn1 As System.Windows.Forms.TextBox
    Friend WithEvents Rerun2 As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Rerun3 As System.Windows.Forms.Button
    Friend WithEvents Name3 As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Ln3Time As System.Windows.Forms.TextBox
    Friend WithEvents Lane3Laps As System.Windows.Forms.TextBox
    Friend WithEvents DNF3 As System.Windows.Forms.Button
    Friend WithEvents StateLn3 As System.Windows.Forms.TextBox
    Friend WithEvents lbReady As System.Windows.Forms.Label
    Friend WithEvents Timer2 As System.Windows.Forms.Timer
    Friend WithEvents tbError As System.Windows.Forms.TextBox
    Friend WithEvents tbRedSw As System.Windows.Forms.TextBox
    Friend WithEvents SetupToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RcvData As System.Windows.Forms.TextBox
    Friend WithEvents DNF2 As System.Windows.Forms.Button
    Friend WithEvents StateLn2 As System.Windows.Forms.TextBox
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents bnSave As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Name2 As System.Windows.Forms.TextBox
    Friend WithEvents bnSetup As System.Windows.Forms.Button
    Friend WithEvents tbStart As System.Windows.Forms.TextBox
    Friend WithEvents Timer3 As System.Windows.Forms.Timer
    Friend WithEvents Timer4 As System.Windows.Forms.Timer
    Friend WithEvents bnExit As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents RaceFormat As System.Windows.Forms.TextBox
    Friend WithEvents tbGrnSw As System.Windows.Forms.TextBox
    Friend WithEvents tbAmbSw As System.Windows.Forms.TextBox
    Friend WithEvents Timer5 As System.Windows.Forms.Timer
    Friend WithEvents Timer6 As System.Windows.Forms.Timer
    Friend WithEvents Timer7 As System.Windows.Forms.Timer
    Friend WithEvents tbstarter As System.Windows.Forms.TextBox
    Friend WithEvents Timer8 As System.Windows.Forms.Timer
    Friend WithEvents IncDisplay As System.Windows.Forms.Button
    Friend WithEvents DecDisplay As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Bar1 As System.Windows.Forms.TextBox
    Friend WithEvents Bar2 As System.Windows.Forms.TextBox
    Friend WithEvents Bar3 As System.Windows.Forms.TextBox
    Friend WithEvents Bar4 As System.Windows.Forms.TextBox
    Friend WithEvents Bar5 As System.Windows.Forms.TextBox
    Friend WithEvents Bar6 As System.Windows.Forms.TextBox

End Class
