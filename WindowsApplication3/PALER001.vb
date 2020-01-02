Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.Data.Sql
Public Class PALER001
    Private metodos As New Clas_Almacen
    Private mensaje As New Clase_mensaje
    Private Sub B_SALIR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B_SALIR.Click
        Me.Close()

    End Sub

    Private Sub PALER001_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        llenardw()
    End Sub

    Private Sub llenardw()
        Dim contador As Integer = 0
        Dim STOCK As Decimal
        Dim CRITICO As Decimal
        Dim CODMATE As String

        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("select C_MATE_108, CANT_108 From T_SCRI_108 where C_DEPO_108=@D1", con1)
        comando1.Parameters.Add(New SqlParameter("D1", _usr.Obt_Almacen))
        'creo un lector
        comando1.ExecuteNonQuery()
        Dim lector1 As SqlDataReader = comando1.ExecuteReader
        While lector1.Read
            CODMATE = lector1.GetValue(0)
            CRITICO = lector1.GetValue(1)
            STOCK = metodos.Saldo(lector1.GetValue(0), _usr.Obt_Almacen, 1)

            If CRITICO > STOCK Then
                Me.DataGridView1.Rows.Add(CODMATE, metodos.detalle_material(CODMATE), STOCK, CRITICO, CRITICO - STOCK)
            End If
        End While
    End Sub

    Public Sub Archivo()
        Dim fichero As String = "C:\Archivo\Stock_Critico_Deposito_" + metodos.NOMBRE_DEPOSITO(_usr.Obt_Almacen).ToString + "_" + Date.Now.Day.ToString.PadLeft(2, "0") + Date.Now.Month.ToString.PadLeft(2, "0") + Date.Now.Year.ToString + ".csv"
        Dim a As New System.IO.StreamWriter(fichero)
        a.WriteLine("CODIGP; DESCRIPCION;STOCK;CRITICO;SOLICITAR")
        For I = 0 To DataGridView1.RowCount - 1
            a.WriteLine(DataGridView1.Item(0, I).Value.ToString + ";" + DataGridView1.Item(1, I).Value.ToString + ";" + DataGridView1.Item(2, I).Value.ToString + ";" + DataGridView1.Item(3, I).Value.ToString + ";" + DataGridView1.Item(4, I).Value.ToString)
        Next
        a.Close()
        mensaje.MADVE002(fichero)
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Archivo()
        Me.Close()
    End Sub
End Class