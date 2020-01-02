Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Public Class PINFO009
    Private mensaje As New Clase_mensaje
    Private METODOS As New Clas_Almacen

  

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        DataGridView1.Rows.Clear()
        lbCantidad.Text = 0
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Dim Fdesde As Date = DateTimePicker1.Value
        Dim Fhasta As Date = DateTimePicker2.Value
        Fdesde = Fdesde.ToShortDateString & " 0:00:00"
        Fhasta = Fhasta.ToShortDateString & " 23:59:59"
        If My.Computer.FileSystem.DirectoryExists("C:\ARCHIVO") = False Then
            My.Computer.FileSystem.CreateDirectory("C:\ARCHIVO")
        End If
        'EXPORTO, VOY A LA BASE Y TRAIGO TODO....
        Try
            Dim fichero As String = "C:\Archivo\DEVOLUCION DE MEDIDORES_" + DateTimePicker1.Value.ToString("dd-MM-yyyy") + "_hasta_" + DateTimePicker2.Value.ToString("dd-MM-yyyy") + ".csv"
            Dim a As New System.IO.StreamWriter(fichero)
            Dim NREMITO As String
            Dim FECHA As String
            Dim CODMATE As String
            Dim CONTRATO As String = ""
            Dim FAMILIA As String
            Dim CANT As String

            a.WriteLine("Nº REMITO;FECHA;COD_MATERIAL;DESC_MATERIAL;CONTRATO;FAMILIA;CANTIDAD")

            Dim cnn2 As SqlConnection = New SqlConnection(conexion)
            cnn2.Open()
            Dim consulta2 As New SqlClient.SqlCommand("SELECT D_REMITO_DEV_116.NREMITO_116, D_REMITO_DEV_116.FECHA_116, D_REMITO_DEV_116.CODMATE_116, D_REMITO_DEV_116.CONTRA_116, C_REMITO_DEV_115.FAMILIA_115, D_REMITO_DEV_116.CANT_116 FROM D_REMITO_DEV_116 INNER JOIN C_REMITO_DEV_115 ON D_REMITO_DEV_116.NREMITO_116 = C_REMITO_DEV_115.NREMITO_115 AND D_REMITO_DEV_116.FECHA_116 = C_REMITO_DEV_115.FECHA_115 WHERE (D_REMITO_DEV_116.FECHA_116 BETWEEN @D1 AND @D2) ORDER BY D_REMITO_DEV_116.NREMITO_116, D_REMITO_DEV_116.CODMATE_116", cnn2)
            consulta2.Parameters.Add(New SqlParameter("D1", Fdesde))
            consulta2.Parameters.Add(New SqlParameter("D2", Fhasta))
            consulta2.ExecuteNonQuery()
            Dim datos2 As SqlDataReader = consulta2.ExecuteReader()
            While datos2.Read()
                NREMITO = datos2.GetValue(0)
                FECHA = datos2.GetDateTime(1).ToShortDateString
                CODMATE = datos2.GetValue(2)
                Select Case datos2.GetValue(3)
                    Case Is = "01"
                        CONTRATO = "UTILIZACION"
                    Case Is = "02"
                        CONTRATO = "SEGUIMIENTO DE DEUDA"
                    Case Is = "03"
                        CONTRATO = "GUARDIA - RETEN"
                End Select
                FAMILIA = desfamilia(datos2.GetValue(4))
                CANT = datos2.GetValue(5)

                a.WriteLine(NREMITO.ToString + ";" + FECHA + ";" + CODMATE + ";" + METODOS.detalle_material(CODMATE) + ";" + CONTRATO + ";" + FAMILIA + ";" + CANT)
            End While
            cnn2.Close()
            a.Close()
            mensaje.MADVE002(fichero)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub B_Agregar_Item_Click(sender As System.Object, e As System.EventArgs) Handles B_Agregar_Item.Click
        Me.DataGridView1.Rows.Clear()
        Dim Fdesde As Date = DateTimePicker1.Value
        Dim Fhasta As Date = DateTimePicker2.Value
        Fdesde = Fdesde.ToShortDateString & " 0:00:00"
        Fhasta = Fhasta.ToShortDateString & " 23:59:59"
        llenarData(Fdesde, Fhasta)
        If DataGridView1.RowCount = 0 Then
            mensaje.MERRO011()
        Else
            Button1.Enabled = True
        End If
    End Sub

    Private Sub llenarData(desde As Date, hasta As Date)
        Dim cnn As New SqlConnection(conexion)
        Try
            cnn.Open()
            Dim adt As New SqlCommand("SELECT FECHA_115,NREMITO_115,FAMILIA_115, CANT_115 FROM C_REMITO_DEV_115 WHERE (FECHA_115 BETWEEN @D1 AND @D2) ORDER BY NREMITO_115", cnn)
            adt.Parameters.Add(New SqlParameter("D1", desde))
            adt.Parameters.Add(New SqlParameter("D2", hasta))
            Dim LECTOR As SqlDataReader = adt.ExecuteReader
            Do While LECTOR.Read
                DataGridView1.Rows.Add(CDate(LECTOR.GetValue(0)).ToShortDateString, LECTOR.GetValue(1), desfamilia(LECTOR.GetValue(2)), LECTOR.GetValue(3))
            Loop
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn.Close()
        End Try
        Dim CANT As Integer = 0
        For I = 0 To DataGridView1.Rows.Count - 1
            CANT = CANT + CInt(DataGridView1.Item(3, I).Value)
        Next
        lbCantidad.Text = CANT.ToString
    End Sub
    Private Function desfamilia(ByVal D1 As String) As String
        Dim resp As String = ""
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("SELECT DESC_802 FROM DET_PARAMETRO_802 WHERE C_TABLA_802=15 AND C_PARA_802 = @D1", con1)
        'creo el lector de parametros
        comando1.Parameters.Add(New SqlParameter("D1", D1))
        comando1.ExecuteNonQuery()
        'genero un lector
        Dim lector1 As SqlDataReader = comando1.ExecuteReader
        If lector1.Read.ToString Then
            resp = lector1.GetValue(0)
        End If
        con1.Close()
        Return resp
    End Function
    Private Sub PINFO009_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Label3.Text = "USUARIO: " & _usr.Obt_Nombre_y_Apellido
        Label5.Text = "Fecha: " & DateTime.Now().ToShortDateString
    End Sub
End Class