Imports System.Net.Http
Imports System.Text
Imports Newtonsoft.Json

Public Class CreateSubmissionForm

    Private WithEvents btnStartStop As Button
    Private WithEvents btnSubmit As Button
    Private stopwatch As Stopwatch

    Public Sub New()
        InitializeComponent()
        stopwatch = New Stopwatch()
    End Sub

    Private Sub CreateSubmissionForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Initialize buttons
        btnStartStop = New Button() With {
            .Text = "Start",
            .Location = New Point(10, 200),
            .Size = New Size(100, 30)
        }
        AddHandler btnStartStop.Click, AddressOf btnStartStop_Click

        btnSubmit = New Button() With {
            .Text = "Submit",
            .Location = New Point(120, 200),
            .Size = New Size(100, 30)
        }
        AddHandler btnSubmit.Click, AddressOf btnSubmit_Click

        ' Add buttons to the form
        Me.Controls.Add(btnStartStop)
        Me.Controls.Add(btnSubmit)
    End Sub

    Private Sub btnStartStop_Click(sender As Object, e As EventArgs)
        If stopwatch.IsRunning Then
            stopwatch.Stop()
            btnStartStop.Text = "Start"
        Else
            stopwatch.Start()
            btnStartStop.Text = "Stop"
        End If
    End Sub

    Private Async Sub btnSubmit_Click(sender As Object, e As EventArgs)
        Dim client As New HttpClient()
        client.BaseAddress = New Uri("http://localhost:3000/")

        Dim submission = New With {
            .name = txtName.Text,
            .email = txtEmail.Text,
            .phone = txtPhone.Text,
            .github_link = txtGitHubLink.Text,
            .stopwatch_time = stopwatch.Elapsed.ToString()
        }

        Dim content = New StringContent(JsonConvert.SerializeObject(submission), Encoding.UTF8, "application/json")
        Dim response = Await client.PostAsync("submit", content)

        If response.IsSuccessStatusCode Then
            MsgBox("Form submitted successfully!")
        Else
            MsgBox("Failed to submit form.")
        End If
    End Sub

    ' Handling key press event for Ctrl + S to submit form
    Protected Overrides Sub OnKeyDown(e As KeyEventArgs)
        MyBase.OnKeyDown(e)
        If e.Control AndAlso e.KeyCode = Keys.S Then
            btnSubmit.PerformClick()
        End If
    End Sub

End Class

