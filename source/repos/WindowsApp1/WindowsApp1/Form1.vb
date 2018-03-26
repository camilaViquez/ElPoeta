Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim a As Integer
        a = 5
        Dim b As String
        b = TextBox1.Text()



        a = CInt(b)
        Console.WriteLine(a)




    End Sub
End Class
