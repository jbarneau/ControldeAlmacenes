
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.Data.Sql
Imports System.IO
Public Class PINFO008BIS
    Private MENSAJE As New Clase_mensaje
    Private Metodos As New Clas_Almacen
    Private oc As New Class_OC
    Private NOC As Decimal
    Private MAT As String
    Public Sub GRABAR(ByVal NOC2 As Decimal, ByVal MAT2 As String)
        Me.MAT = MAT2
        Me.NOC = NOC2
    End Sub

    Private Sub PINFO008BIS_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LLENAR()
    End Sub
    Private Sub LLENAR()
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        
        Try
            con1.Open()
            'creo el comando para pasarle los parametros
            'Dim comando1 As New SqlClient.SqlCommand("SELECT T_REMI_104.N_REMI_104, T_REMI_104.F_ALTA_104, T_REMI_104.C_MATE_104, M_MATE_002.DESC_002, T_REMI_104.ALMAE_104, M_PERS_003.NOMB_003, T_REMI_104.CANT_104, T_REMI_104.USER_104, T_REMI_104.N_PETI_104 FROM T_REMI_104 INNER JOIN M_PERS_003 ON T_REMI_104.ALMAE_104 = M_PERS_003.NDOC_003 INNER JOIN M_MATE_002 ON T_REMI_104.C_MATE_104 = M_MATE_002.CMATE_002 WHERE (T_REMI_104.T_MOV_104 = 1) AND (T_REMI_104.OC_104 = @D1) AND (T_REMI_104.C_MATE_104 = @D2)", con1)
            Dim comando1 As New SqlClient.SqlCommand("SELECT T_REMI_104.N_REMI_104, T_REMI_104.F_ALTA_104, T_REMI_104.C_MATE_104, M_MATE_002.DESC_002, T_REMI_104.ALMAE_104, M_PERS_003.NOMB_003, T_REMI_104.CANT_104, T_REMI_104.USER_104, T_REMI_104.N_PETI_104, M_PERS_003_1.NOMB_003 + ' ' + M_PERS_003_1.APELL_003 AS NOMAPE FROM T_REMI_104 INNER JOIN M_PERS_003 ON T_REMI_104.ALMAE_104 = M_PERS_003.NDOC_003 INNER JOIN M_MATE_002 ON T_REMI_104.C_MATE_104 = M_MATE_002.CMATE_002 INNER JOIN M_PERS_003 AS M_PERS_003_1 ON T_REMI_104.USER_104 = M_PERS_003_1.NDOC_003 WHERE (T_REMI_104.T_MOV_104 = 1) AND (T_REMI_104.OC_104 = @D1) AND (T_REMI_104.C_MATE_104 = @D2)", con1)
            'creo el lector de parametros
            comando1.Parameters.Add(New SqlParameter("D1", NOC))
            comando1.Parameters.Add(New SqlParameter("D2", MAT))
            comando1.ExecuteNonQuery()
            'genero un lector
            Dim lector1 As SqlDataReader = comando1.ExecuteReader
            While lector1.Read
                Me.DataGridView1.Rows.Add(lector1.GetValue(0), lector1.GetDateTime(1).ToShortDateString, lector1.GetValue(5), lector1.GetValue(8), lector1.GetValue(6), lector1.GetValue(9))
            End While
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            con1.Close()
        End Try



    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
End Class
