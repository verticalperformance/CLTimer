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
    Private laneValue As String

    Public Property Lane As String
        Get
            Return laneValue
        End Get
        Set(value As String)
            laneValue = value
        End Set
    End Property

    Public ReadOnly Property Item(index As Integer) As Lap
        Get
            Return CType(List.Item(index), Lap)
        End Get
    End Property

    Public Sub Add(aLap As Lap, Name As String, Elapsed As TimeSpan)
        List.Add(aLap)
        aLap.Name = Name
        aLap.Elapsed = Elapsed
    End Sub

    Public Sub Remove(index As Integer)
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

    Public Function GetAverageTime(NumberOfLapsToAverageOver As Integer) As Double
        'get an average time over the last x laps

        If NumberOfLapsToAverageOver > 0 Then
            If Count < NumberOfLapsToAverageOver + 1 Then
                GetAverageTime = 0
            Else
                Dim TotalTimeSpan = Item(Count - 1).Elapsed - Item(Count - NumberOfLapsToAverageOver - 1).Elapsed
                GetAverageTime = TotalTimeSpan.TotalSeconds / NumberOfLapsToAverageOver
            End If
        Else
            GetAverageTime = 0
        End If

    End Function

End Class
