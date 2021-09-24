Public Class Lap
    Private nameValue As String

    Private elapsedValue As TimeSpan

    Public Property Name As String
        Get
            Return nameValue
        End Get
        Set(value As String)
            nameValue = value
        End Set
    End Property



    Public Property Elapsed As TimeSpan
        Get
            Return elapsedValue
        End Get
        Set(value As TimeSpan)
            elapsedValue = value
        End Set
    End Property
End Class

Public Class Laps
    Inherits System.Collections.CollectionBase
    Private laneValue As Integer

    Public Property Lane As Integer
        Get
            Return laneValue
        End Get
        Set(value As Integer)
            laneValue = value
        End Set
    End Property

    Public ReadOnly Property Item(ByVal index As Integer) As Lap
        Get
            Return CType(List.Item(index), Lap)
        End Get
    End Property

    Public Sub Add(ByVal aLap As Lap)
        List.Add(aLap)
    End Sub

    Public Sub Remove(ByVal index As Integer)
        ' Check to see if there is a lap  at the supplied index.
        If index > Count - 1 Or index < 0 Then
            ' If no lap exists at index, .....
        Else
            ' Invokes the RemoveAt method of the List object.
            List.RemoveAt(index)
        End If
    End Sub

    Public Overloads Sub Clear()
        List.Clear()
    End Sub
End Class
