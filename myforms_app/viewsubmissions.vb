Imports System.Net.Http
Imports Newtonsoft.Json

Public Class ViewSubmissionsForm

    Private WithEvents btnPrevious As Button
    Private WithEvents btnNext As Button

    Private submissions As List(Of Submission)
    Private currentIndex As Integer = 0

    Private Async Sub ViewSubmissionsForm_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        ' Initialize buttons
        btnPrevious = New Button() With {
            .Text = "Previous",
            .Location = New Point(10, 200),
            .Size = New Size(100, 30)
        }
        AddHandler btnPrevious.Click, AddressOf btnPrevious_Click

        btnNext = New Button() With {
            .Text = "Next",
            .Location = New Point(120, 200),
            .Size = New Size(100, 30)
        }
        AddHandler btnNext.Click, AddressOf btnNext_Click

        ' Add buttons to the form
        Me.Controls.Add(btnPrevious)
        Me.Controls.Add(btnNext)

        ' Load the submissions from the backend
        Await LoadSubmissions()
        DisplaySubmission()
    End Sub

    Private Async Function LoadSubmissions() As Task
        Using client As New HttpClient()
            Dim response As HttpResponseMessage = Await client.GetAsync("http://localhost:3000/read?index=0")
            If response.IsSuccessStatusCode Then
                Dim responseData As String = Await response.Content.ReadAsStringAsync()
                submissions = JsonConvert.DeserializeObject(Of List(Of Submission))(responseData)
            End If
        End Using
    End Function

    Private Sub DisplaySubmission()
        If submissions IsNot Nothing AndAlso submissions.Count > 0 Then
            Dim submission As Submission = submissions(currentIndex)
            ' Assuming you have TextBoxes or Labels to display submission details
            txtName.Text = submission.name
            txtEmail.Text = submission.email
            txtPhone.Text = submission.phone
            txtGitHubLink.Text = submission.github_link
            txtStopwatchTime.Text = submission.stopwatch_time
        End If
    End Sub

    Private Sub btnPrevious_Click(sender As Object, e As EventArgs)
        If currentIndex > 0 Then
            currentIndex -= 1
            DisplaySubmission()
        End If
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs)
        If currentIndex < submissions.Count - 1 Then
            currentIndex += 1
            DisplaySubmission()
        End If
    End Sub

End Class

Public Class Submission
    Public Property name As String
    Public Property email As String
    Public Property phone As String
    Public Property github_link As String
    Public Property stopwatch_time As String
End Class
