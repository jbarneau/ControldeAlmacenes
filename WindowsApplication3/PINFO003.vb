Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.Data.Sql
Imports System.IO
Imports System
Imports System.Collections
Imports Microsoft.VisualBasic
Public Class PINFO003
    Private MEDIDOR As New Clas_Medidor
    Private MENSAJE As New Clase_mensaje
    Private METODOS As New Clas_Almacen

   

    Private Sub PINFO003_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label3.Text = "USUARIO: " & _usr.Obt_Nombre_y_Apellido
        Label5.Text = "Fecha: " & DateTime.Now().ToShortDateString
        LLENAR_dw()
        LLENAR_MOTIVO()
    End Sub
    Private Sub LLENAR_MOTIVO()
        Dim DATASET1 As New DataSet
        'CONECTO LA BASE
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("SELECT C_PARA_802, DESC_802 FROM DET_PARAMETRO_802 WHERE C_TABLA_802 = 13 order by DESC_802", cnn2)
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.Fill(DATASET1, "DET_PARAMETRO_802")
        cnn2.Close()
        MOTIVO.DataSource = DATASET1.Tables("DET_PARAMETRO_802")
        MOTIVO.DisplayMember = "DESC_802"
        MOTIVO.ValueMember = "C_PARA_802"
        MOTIVO.Text = Nothing

    End Sub
    Private Sub LLENAR_dw()
        Button1.Enabled = False
        Button2.Enabled = False
        Me.DataGridView1.Rows.Clear()
        Dim nserie As String
        Dim MATE As String
        Dim ALMACEN As String
        Dim POLIZA As String
        Dim cnn4 As SqlConnection = New SqlConnection(conexion)
        cnn4.Open()
        'Consulta SQL...
        Dim comando4 As New SqlClient.SqlCommand("SELECT NSERIE_102, CMATE_102, CALMA_102,POLIZA_102 FROM T_MEDI_102 WHERE ESTADO_102=6 AND F_RESU_102 IS NULL", cnn4)
        comando4.ExecuteNonQuery()
        Dim LECTOR1 As SqlDataReader = comando4.ExecuteReader()
        While LECTOR1.Read
            nserie = LECTOR1.GetValue(0).ToString.PadLeft(8, "0")
            MATE = LECTOR1.GetValue(1)
            ALMACEN = LECTOR1.GetValue(2)
            POLIZA = LECTOR1.GetValue(3)
            DataGridView1.Rows.Add(nserie, MATE, ALMACEN, METODOS.NOMBRE_DEPOSITO(ALMACEN), POLIZA)
        End While
        cnn4.Close()
        If Me.DataGridView1.RowCount <> 0 Then
            Button1.Enabled = True
            Button2.Enabled = True
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim conf As Boolean = False
        For i = 0 To DataGridView1.RowCount - 1
            If Me.DataGridView1.Item(5, i).Value = True Then
                conf = True
            End If
        Next
        If conf = True Then
            For i = 0 To Me.DataGridView1.RowCount - 1
                If Me.DataGridView1.Item(5, i).Value = True Then

                    If MOTIVO.SelectedValue = 2 Then
                        If CInt(Me.DataGridView1.Item(2, i).Value) > 10 Then
                            METODOS.Descontar_Stock_Material(Me.DataGridView1.Item(1, i).Value, Me.DataGridView1.Item(2, i).Value, 1, 1)
                            MEDIDOR.RESOLVER_final(Me.DataGridView1.Item(0, i).Value, _usr.Obt_Usr, Date.Now, MOTIVO.SelectedValue)
                        End If
                    Else
                        MEDIDOR.RESOLVER(Me.DataGridView1.Item(0, i).Value, _usr.Obt_Usr, Date.Now, MOTIVO.SelectedValue)
                    End If
                End If


            Next
        Else
            MENSAJE.MERRO019()
        End If
        LLENAR_dw()
        MOTIVO.Text = Nothing
    End Sub

    

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If My.Computer.FileSystem.DirectoryExists("C:\ARCHIVO") = False Then
            My.Computer.FileSystem.CreateDirectory("C:\ARCHIVO")
        End If

        Dim fichero As String = "C:\Archivo\Error_en_medidores" + "_" + Date.Now.Day.ToString.PadLeft(2, "0") + Date.Now.Month.ToString.PadLeft(2, "0") + Date.Now.Year.ToString + ".csv"
        Dim a As New System.IO.StreamWriter(fichero)
        a.WriteLine("N_SERIE;COD_MATERIAL; COD_ALMACEN;ALMACEN;POLIZA")
        For I = 0 To Me.DataGridView1.RowCount - 1
            a.WriteLine(Me.DataGridView1.Item(0, I).Value.ToString + ";" + Me.DataGridView1.Item(1, I).Value.ToString + ";" + Me.DataGridView1.Item(2, I).Value.ToString + ";" + Me.DataGridView1.Item(3, I).Value.ToString + ";" + Me.DataGridView1.Item(4, I).Value.ToString)
        Next
        a.Close()
        MENSAJE.MADVE002(fichero)

    End Sub

    Private Function traer_codigo(ByVal a As String) As Integer
        Dim resp As Integer = 0
        'CONECTO LA BASE
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlClient.SqlCommand("SELECT C_PARA_802 FROM DET_PARAMETRO_802 WHERE C_TABLA_802 = 13  and DESC_802 = @D1", cnn2)
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.Parameters.Add(New SqlParameter("D1", a))
        adaptadaor.ExecuteNonQuery()
        Dim lector As SqlDataReader = adaptadaor.ExecuteReader
        If lector.Read Then
            resp = lector.GetInt32(0)
        End If
        cnn2.Close()
        Return resp
    End Function
    'selecciona todos los items
    Private Sub CheckBox2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        For i = 0 To Me.DataGridView1.RowCount - 1
            If Me.DataGridView1.Item(5, i).Value <> CheckBox1.Checked Then
                Me.DataGridView1.Item(5, i).Value = CheckBox1.Checked
            End If
        Next
    End Sub

   
End Class