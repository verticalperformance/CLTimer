Imports Microsoft.VisualBasic.FileIO
Public Class Race
    Private nameValue As String
    Public Property Name() As String
        Get
            Return nameValue
        End Get
        Set(ByVal value As String)
            nameValue = value
        End Set
    End Property
    Private heatDistanceValue As Integer
    Public Property HeatDistance() As Integer
        Get
            Return heatDistanceValue
        End Get
        Set(ByVal value As Integer)
            heatDistanceValue = value
        End Set
    End Property
    Private finalDistanceValue As Integer
    Public Property FinalDistance() As Integer
        Get
            Return finalDistanceValue
        End Get
        Set(ByVal value As Integer)
            finalDistanceValue = value
        End Set
    End Property
    Private TypeValue As String
    Public Property Type() As String
        Get
            Return TypeValue
        End Get
        Set(ByVal value As String)
            TypeValue = value
        End Set
    End Property
    Private warmUpTimeValue As Integer
    Public Property WarmUpTime() As Integer
        Get
            Return warmUpTimeValue
        End Get
        Set(ByVal value As Integer)
            warmUpTimeValue = value
        End Set
    End Property
    Private coolDownTimeValue As Integer
    Public Property CoolDownTime() As Integer
        Get
            Return coolDownTimeValue
        End Get
        Set(ByVal value As Integer)
            coolDownTimeValue = value
        End Set
    End Property
    Private maxHeatTimeValue As Integer
    Public Property MaxHeatTime() As Integer
        Get
            Return maxHeatTimeValue
        End Get
        Set(ByVal value As Integer)
            maxHeatTimeValue = value
        End Set
    End Property
    Private maxFinalTimeValue As Integer
    Public Property MaxFinalTime() As Integer
        Get
            Return maxFinalTimeValue
        End Get
        Set(ByVal value As Integer)
            maxFinalTimeValue = value
        End Set
    End Property
    Private maxSpeedValue As Single
    Public Property MaxSpeed() As Single
        Get
            Return maxSpeedValue
        End Get
        Set(ByVal value As Single)
            maxSpeedValue = value
        End Set
    End Property

End Class
Public Class Races
    Inherits System.Collections.CollectionBase
    ' Restricts to Race types, items that can be added to the collection.
    Public Sub Add(ByVal aRace As Race)
        ' Invokes Add method of the List object to add a race.
        List.Add(aRace)
    End Sub

    Public Sub Remove(ByVal index As Integer)
        ' Check to see if there is a race at the supplied index.
        If index > Count - 1 Or index < 0 Then
            ' If no race exists at index, .....
        Else
            ' Invokes the RemoveAt method of the List object.
            List.RemoveAt(index)
        End If
    End Sub

    ' This line declares the Item property as ReadOnly, and 
    ' declares that it will return a Race object.
    Public ReadOnly Property Item(ByVal index As Integer) As Race
        Get
            ' The appropriate item is retrieved from the List object and 
            ' explicitly cast to the Race type, then returned to the 
            ' caller.
            Return CType(List.Item(index), Race)
        End Get
    End Property

    Public Sub ReadRaceClassesFromFile(RaceClassFile As String)

        ' Read a textfile containing the race classes

        Dim myRaceClasses As New Races

        Dim fileExists As Boolean
        Dim headerLine As Boolean = True 'Skip first line containing headers
        fileExists = My.Computer.FileSystem.FileExists(RaceClassFile)

        If fileExists Then
            Dim fields As String()
            Dim delimiter As String = ","
            Using parser As New TextFieldParser(RaceClassFile)
                parser.SetDelimiters(delimiter)
                While Not parser.EndOfData
                    ' Read in the fields for the current line
                    fields = parser.ReadFields()
                    If headerLine Then
                        headerLine = False
                    Else
                        ' Add code here to use data in fields variable.
                        Dim myRace As New Race
                        myRace.Name = fields(0)
                        myRace.HeatDistance = fields(1)
                        myRace.FinalDistance = fields(2)
                        myRace.Type = "Laps"
                        myRace.WarmUpTime = fields(3)
                        myRace.CoolDownTime = fields(4)
                        myRace.MaxHeatTime = fields(5)
                        myRace.MaxFinalTime = fields(6)
                        myRace.MaxSpeed = fields(7)

                        If fields(1) = 0 Then
                            myRace.Type = "Mins"
                        End If

                        Me.Add(myRace)
                    End If
                End While
            End Using

        Else
            ' Return a default races if file not found
            Dim myRace As New Race
            myRace.Name = "F2C"
            myRace.HeatDistance = 100
            myRace.FinalDistance = 200
            myRace.Type = "Laps"
            myRace.WarmUpTime = 90
            myRace.CoolDownTime = 30
            myRace.MaxHeatTime = 6000
            myRace.MaxFinalTime = 9000
            myRace.MaxSpeed = 0.0
            Me.Add(myRace)
        End If


    End Sub

End Class

