Public Class Races
    Inherits System.Collections.CollectionBase

    Public ReadOnly Property Item(index As Integer) As Race
        Get
            Return CType(List.Item(index), Race)
        End Get
    End Property

    Public Sub Add(aRace As Race)
        List.Add(aRace)
    End Sub

    Public Sub Remove(index As Integer)
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

    Public ReadOnly Property Item(index As Integer) As Laps
        Get
            Return CType(List.Item(index), Laps)
        End Get
    End Property

    Public Sub Add(aLaps As Laps, Lane As String)
        List.Add(aLaps)
        aLaps.Lane = Lane
    End Sub

    Public Sub Remove(index As Integer)
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
    Private raceStartTimeValue As Date
    Private raceEndTimeValue As Date
    Private raceLapResultsValue As RaceLapResults
    Private raceTypeValue As String
    Private raceResultsValue As RaceResults

    Public Property Name As String
        Get
            Return nameValue
        End Get
        Set(value As String)
            nameValue = value
        End Set
    End Property
    Public Property RaceType As String
        Get
            Return raceTypeValue
        End Get
        Set(value As String)
            raceTypeValue = value
        End Set
    End Property

    Public Property RaceResults As RaceResults
        Get
            Return raceResultsValue
        End Get
        Set(value As RaceResults)
            raceResultsValue = value
        End Set
    End Property


    Public Property RaceStartTime As Date
        Get
            Return raceStartTimeValue
        End Get
        Set(value As Date)
            raceStartTimeValue = value
        End Set
    End Property
    Public Property RaceEndTime As Date
        Get
            Return raceEndTimeValue
        End Get
        Set(value As Date)
            raceEndTimeValue = value
        End Set
    End Property
    Public Property RaceLapResults As RaceLapResults
        Get
            Return raceLapResultsValue
        End Get
        Set(value As RaceLapResults)
            raceLapResultsValue = value
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
    Public ReadOnly Property ResultsFileName As String
        Get
            Return FormatDateForFileName(raceStartTimeValue) & "_" & raceClassValue.Name & "_" & raceTypeValue & ".json"
        End Get

    End Property

    Public Function FormatRaceTimeToDate(TicksToFormat As Long) As Date
        ' Convert race start/finish times from Ticks back to a date
        Dim myRaceStartTime As New DateTime(TicksToFormat)
        FormatRaceTimeToDate = myRaceStartTime

    End Function

    Private Function FormatDateForFileName(DateToFormat As Date) As String
        ' Format a date/time into a filename friendly format
        ' 
        FormatDateForFileName = DateToFormat.ToString("s").Replace(":", "")
    End Function

    Public Function FormatRaceTimeToFullDateTime(TicksToFormat As Long) As Date
        ' Convert race start/finish times from Ticks back to a date
        Dim myRaceTime As New DateTime(TicksToFormat)
        FormatRaceTimeToFullDateTime = myRaceTime.ToString("F")
    End Function

End Class

Public Class RaceResults
    Inherits System.Collections.CollectionBase
    Private aRaceResult As RaceResult


    Public ReadOnly Property Item(index As Integer) As RaceResult
        Get
            Return CType(List.Item(index), RaceResult)
        End Get
    End Property

    Public Sub Add(aRaceResult As RaceResult, Name As String)
        List.Add(aRaceResult)
        aRaceResult.Name = Name
    End Sub

    Public Sub Remove(index As Integer)
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

Public Class RaceResult

    Private nameValue As String
    Private competitorValue As String
    Private lapsValue As Integer
    Private timeValue As String
    Private resultValue As String
    Private colourValue As String

    Public Property Name As String
        Get
            Return nameValue
        End Get
        Set(value As String)
            nameValue = value
        End Set
    End Property
    Public Property Colour As String
        Get
            Return colourValue
        End Get
        Set(value As String)
            colourValue = value
        End Set
    End Property
    Public Property Competitor As String
        Get
            Return competitorValue
        End Get
        Set(value As String)
            competitorValue = value
        End Set
    End Property

    Public Property Laps As Integer
        Get
            Return lapsValue
        End Get
        Set(value As Integer)
            lapsValue = value
        End Set
    End Property

    Public Property Time As String
        Get
            Return timeValue
        End Get
        Set(value As String)
            timeValue = value
        End Set
    End Property

    Public Property Result As String
        Get
            Return resultValue
        End Get
        Set(value As String)
            resultValue = value
        End Set
    End Property


End Class
