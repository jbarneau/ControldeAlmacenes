Imports System.Data.SqlClient

Public Class PCONS008BIS2
    Private OC As String
    Private material As String

    Public Sub tomar(OC As String, material As String)
        Me.OC = OC
        Me.material = material
    End Sub


    Private Sub PCONS008BIS2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        llenar()
    End Sub


    Private Sub llenar()
        Dim wheremat As String = ""
        If material <> "NO" Then
            wheremat = " AND (dbo.T_D_OC_106.C_MATE_106='" + material + "') "
        End If
        Dim mate As String = ""
        Dim desc As String = ""
        Dim unid As String = ""
        Dim soli As Decimal = 0
        Dim ent As Decimal = 0
        Dim saldo As Decimal = 0
        Dim precio As Decimal = 0
        Dim tot As Decimal = 0
        Dim consulta As String = "SELECT dbo.T_D_OC_106.C_MATE_106, dbo.M_MATE_002.DESC_002, dbo.T_D_OC_106.CANT_106, dbo.T_D_OC_106.CANTE_106, dbo.M_MATE_002.UNID_002,dbo.T_D_OC_106.PRECIO_C_106 FROM dbo.T_D_OC_106 INNER JOIN dbo.M_MATE_002 ON dbo.T_D_OC_106.C_MATE_106 = dbo.M_MATE_002.CMATE_002 WHERE (dbo.T_D_OC_106.N_OC_106 =" + OC + ") and (dbo.T_D_OC_106.CONF_106 = 1) " + wheremat
        DataGridView2.Rows.Clear()
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        Try
            con1.Open()
            Dim comando1 As New SqlClient.SqlCommand(consulta, con1)
            comando1.ExecuteNonQuery()
            Dim lector1 As SqlDataReader = comando1.ExecuteReader
            While lector1.Read
                mate = lector1.GetValue(0)
                desc = lector1.GetValue(1)
                soli = lector1.GetValue(2)
                ent = lector1.GetValue(3)
                unid = lector1.GetValue(4)
                Try
                    precio = lector1.GetValue(5)

                Catch ex As Exception
                    precio = 0
                End Try
                tot = precio * soli
                saldo = soli - ent
                Me.DataGridView2.Rows.Add(mate, desc, unid, soli, ent, saldo, precio, tot)
            End While
            con1.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub


End Class