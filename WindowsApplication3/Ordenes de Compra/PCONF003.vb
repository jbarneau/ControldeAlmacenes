Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.Data.Sql
Imports System.IO
Public Class PCONF003
    Private MATERIAL As String = ""

    Private Sub PCONF003_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label5.Text = EnDepo(MATERIAL).ToString
        Label6.Text = Enoperario(MATERIAL).ToString
        Label7.Text = (EnDepo(MATERIAL) + Enoperario(MATERIAL)).ToString
    End Sub
    Public Sub GRABARDATO(ByVal MATE As String)
        MATERIAL = MATE
    End Sub
    Private Function EnDepo(ByVal codmate As String) As Decimal
        Dim cant As Decimal = 0
        'creo la cadena de conexion
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("SELECT T_ALMA_103.N_CANT_103 FROM T_ALMA_103 INNER JOIN M_PERS_003 ON T_ALMA_103.C_ALMA_103 = M_PERS_003.NDOC_003 WHERE (T_ALMA_103.C_MATE_103 = @D1) AND (T_ALMA_103.ESTA_103 = 1) AND (M_PERS_003.DEPO_003 = 1)", con1)
        comando1.Parameters.Add(New SqlParameter("D1", codmate))
        'creo el lector de parametros
        comando1.ExecuteNonQuery()
        'genero un lector
        Dim lector1 As SqlDataReader = comando1.ExecuteReader
        While lector1.Read
            cant += lector1.GetValue(0)
        End While
        Return cant
    End Function
    Private Function Enoperario(ByVal codmate As String) As Decimal
        Dim cant As Decimal = 0
        'creo la cadena de conexion
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("SELECT T_ALMA_103.N_CANT_103 FROM T_ALMA_103 INNER JOIN M_PERS_003 ON T_ALMA_103.C_ALMA_103 = M_PERS_003.NDOC_003 WHERE (T_ALMA_103.C_MATE_103 = @D1) AND (T_ALMA_103.ESTA_103 = 1) AND (M_PERS_003.ALMA_003 = 1)", con1)
        comando1.Parameters.Add(New SqlParameter("D1", codmate))
        'creo el lector de parametros
        comando1.ExecuteNonQuery()
        'genero un lector
        Dim lector1 As SqlDataReader = comando1.ExecuteReader
        While lector1.Read
            cant += lector1.GetValue(0)
        End While
        Return cant
    End Function


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()

    End Sub
End Class