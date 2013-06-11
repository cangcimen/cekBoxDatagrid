Imports System.Data.SqlClient

Public Class Form1
    Dim cnstring As String = "Data source = localhost;uid = sa;pwd = biofree;initial catalog = northwind"
    Dim dt As New DataTable

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Using cn As New SqlConnection(cnstring)
            cn.Open()
            Dim cmd As New SqlCommand("select * from products", cn)

            Dim reader As SqlDataReader = cmd.ExecuteReader
            dt.Load(reader)

            DataGridView1.AutoGenerateColumns = False
            DataGridView1.DataSource = dt
        End Using
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim _productID As New List(Of Integer)
        For Each r As DataGridViewRow In DataGridView1.Rows
            If r.Cells(0).Value = True Then
                _productID.Add(r.Cells(1).Value)

                Dim cell() As Object = {r.Cells(1).Value, r.Cells(2).Value}
                DataGridView2.Rows.Add(cell)
            End If
        Next

        Dim _s As String = ""

        For Each i As Integer In _productID
            _s &= i & ","
        Next

        Dim sql As String = "SELECT * FROM Products WHERE ProductID NOT IN (" & _s.TrimEnd(",") & ")"

        Using cn As New SqlConnection(cnstring)
            cn.Open()
            Dim cmd As New SqlCommand(sql, cn)

            Dim reader As SqlDataReader = cmd.ExecuteReader
            dt = New DataTable
            dt.Load(reader)

            DataGridView1.DataSource = dt
        End Using
    End Sub
End Class
