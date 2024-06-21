Public Class Form1

    Private WithEvents btnViewSubmissions As Button
    Private WithEvents btnCreateNewSubmission As Button

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Initialize buttons
        btnViewSubmissions = New Button() With {
            .Text = "View Submissions",
            .Location = New Point(10, 10),
            .Size = New Size(150, 30)
        }
        AddHandler btnViewSubmissions.Click, AddressOf btnViewSubmissions_Click

        btnCreateNewSubmission = New Button() With {
            .Text = "Create New Submission",
            .Location = New Point(10, 50),
            .Size = New Size(150, 30)
        }
        AddHandler btnCreateNewSubmission.Click, AddressOf btnCreateNewSubmission_Click

        ' Add buttons to the form
        Me.Controls.Add(btnViewSubmissions)
        Me.Controls.Add(btnCreateNewSubmission)
    End Sub

    Private Sub btnViewSubmissions_Click(sender As Object, e As EventArgs)
        Dim viewSubmissionsForm As New ViewSubmissionsForm()
        viewSubmissionsForm.Show()
    End Sub

    Private Sub btnCreateNewSubmission_Click(sender As Object, e As EventArgs)
        Dim createSubmissionForm As New CreateSubmissionForm()
        createSubmissionForm.Show()
    End Sub

End Class
