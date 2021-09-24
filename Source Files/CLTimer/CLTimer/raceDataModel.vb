Public Class Races
    Inherits System.Collections.CollectionBase

    Public ReadOnly Property Item(ByVal index As Integer) As Race
        Get
            Return CType(List.Item(index), Race)
        End Get
    End Property

    Public Sub Add(ByVal aRace As Race)
        List.Add(aRace)
    End Sub

    Public Sub Remove(ByVal index As Integer)
        ' Check to see if there is a race  at the supplied index.
        If index > Count - 1 Or index < 0 Then
            ' If no race exists at index, .....
        Else
            ' Invokes the RemoveAt method of the List object.
            List.RemoveAt(index)
        End If
    End Sub

    Public Overloads Sub Clear()
        List.Clear()
    End Sub



End Class

Public Class RaceLapResults
    Inherits System.Collections.CollectionBase

    Public ReadOnly Property Item(ByVal index As Integer) As Laps
        Get
            Return CType(List.Item(index), Laps)
        End Get
    End Property

    Public Sub Add(ByVal aLaps As Laps)
        List.Add(aLaps)
    End Sub

    Public Sub Remove(ByVal index As Integer)
        If index > Count - 1 Or index < 0 Then

        Else
            ' Invokes the RemoveAt method of the List object.
            List.RemoveAt(index)
        End If
    End Sub

    Public Overloads Sub Clear()
        List.Clear()
    End Sub


End Class

Public Class Race
    Private nameValue As String
    Private raceClassValue As RaceClass
    Private raceStartTimeValue As Integer

    Public Property Name As String
        Get
            Return nameValue
        End Get
        Set(value As String)
            nameValue = value
        End Set
    End Property

    Public Property RaceClass As RaceClass
        Get
            Return raceClassValue
        End Get
        Set(value As RaceClass)
            raceClassValue = value
        End Set
    End Property

    Public Property RaceStartTime As Integer
        Get
            Return Nothing
        End Get
        Set(value As Integer)
        End Set
    End Property
End Class
