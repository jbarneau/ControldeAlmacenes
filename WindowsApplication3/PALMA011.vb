Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports System.IO

Public Class PALMA011
#Region "VARIABLES"

    Private CONFIRMACION As Boolean = False
    Private TIPO As String
    Private NSERIE As String
    Private tipoMaq As TIPO_DE_MAQUINA
    Private TipoCert As Integer
#End Region
#Region "FUNCIONES PUBLICAS"
    Public Sub TOMAR(ByVal MITIPO As String, ByVal MISERIE As String, ByVal tipocert As Integer)
        TIPO = MITIPO
        NSERIE = MISERIE
        tipoMaq = New TIPO_DE_MAQUINA(MITIPO)
        Me.TipoCert = tipocert
    End Sub
    Public ReadOnly Property LEERCONFIRMACION
        Get
            Return CONFIRMACION
        End Get
    End Property
#End Region
#Region "FUNCIONES PRIVADAS"


    Private Sub Grabar(ByVal NUMERO As String, ByVal CALIBRACION As String, ByVal FECHA As Date, ByVal VTO As Date, ByVal OBS As String, ByVal TIPO As Integer, ByVal RESP As String)
        Dim cnn As New SqlConnection(conexion)
        Try
            cnn.Open()
            Dim adt As New SqlCommand("INSERT INTO T_CALIBRACION_118 (NSERIE_118,NCALI_118,FCALI_118,VTO_118,OBS_118,TIPO_118,RESPON_118) VALUES (@D1,@D2,@D3,@D4,@D5,@D6,@D7)", cnn)
            adt.Parameters.Add(New SqlParameter("D1", NUMERO))
            adt.Parameters.Add(New SqlParameter("D2", CALIBRACION))
            adt.Parameters.Add(New SqlParameter("D3", FECHA))
            adt.Parameters.Add(New SqlParameter("D4", VTO))
            adt.Parameters.Add(New SqlParameter("D5", OBS))
            adt.Parameters.Add(New SqlParameter("D6", TIPO))
            adt.Parameters.Add(New SqlParameter("D7", RESP))
            adt.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn.Close()
        End Try
    End Sub
    Private Sub Actualizar(ByVal numero As String, ByVal FECHA As Date, ByVal TIPOCERT As Integer)
        Dim cnn As New SqlConnection(conexion)
        Try
            cnn.Open()
            Dim adt As New SqlCommand("UPDATE T_CALIBRACION_118 SET FBAJA_118=@E1 WHERE NSERIE_118=@D1 AND FBAJA_118 IS NULL AND TIPO_118= @D2", cnn)
            adt.Parameters.Add(New SqlParameter("E1", FECHA))
            adt.Parameters.Add(New SqlParameter("D1", numero))
            adt.Parameters.Add(New SqlParameter("D2", TipoCert))
            adt.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn.Close()
        End Try
    End Sub
#End Region
    Private Function Obtener_Numero_Verificacion() As String
        'lee el ultimo numero de remito
        Dim Numero_Remito As String = ""
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("select NUMERO From NUMERACION where C_NUM=8", con1)
        'creo un lector
        comando1.ExecuteNonQuery()
        Dim lector1 As SqlDataReader = comando1.ExecuteReader
        If lector1.Read.ToString() Then
            Numero_Remito = Date.Today.Year.ToString + CDec(lector1.GetValue(0)).ToString.PadLeft(8, "0")
        End If
        con1.Close()
        Return Numero_Remito
    End Function
    Private Sub Sumar_Num_Verificacion()
        'incrementa el numero de remito
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("Update NUMERACION set NUMERO = NUMERO+1 WHERE C_NUM=8", con1)
        comando1.ExecuteReader()
        con1.Close()
    End Sub
    Private Sub PALMA011_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        TXTSERIE.Text = NSERIE
        If TipoCert = 2 Then
            txtCalibracion.Text = Obtener_Numero_Verificacion()
            txtCalibracion.Enabled = False
        End If
    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As System.Object, e As System.EventArgs) Handles DateTimePicker1.ValueChanged
        If TipoCert = 1 Then
            DateTimePicker2.Value = DateAdd(DateInterval.Month, tipoMaq.leerPlazoCalibracion, DateTimePicker1.Value)
        End If
        If TipoCert = 2 Then
            DateTimePicker2.Value = DateAdd(DateInterval.Month, tipoMaq.leerPlazoVerif, DateTimePicker1.Value)
        End If

    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        If TipoCert = 1 Then
            If txtCalibracion.Text <> Nothing Then
                Actualizar(TXTSERIE.Text, DateAdd(DateInterval.Day, -1, DateTimePicker1.Value), TipoCert)
                Grabar(TXTSERIE.Text, txtCalibracion.Text, DateTimePicker1.Value, DateTimePicker2.Value, txtOBS.Text, TipoCert, _usr.Obt_Nombre_y_Apellido)
                If tipoMaq.leerVerificacion = 1 Then
                    Dim NCERT = Obtener_Numero_Verificacion()
                    Sumar_Num_Verificacion()
                    Actualizar(NCERT, DateAdd(DateInterval.Day, -1, DateTimePicker1.Value), TipoCert)
                    Grabar(TXTSERIE.Text, NCERT, DateTimePicker1.Value, DateAdd(DateInterval.Month, tipoMaq.leerPlazoVerif, DateTimePicker1.Value), txtOBS.Text, 2, _usr.Obt_Nombre_y_Apellido)
                End If
                Dim pantalla As New OpenFileDialog
                Dim pantalla2 As New SaveFileDialog
                pantalla.DereferenceLinks = True
                pantalla.Filter = "pdf files (*.pdf)|*.pdf"
                pantalla.FilterIndex = 1
                pantalla.RestoreDirectory = False
                pantalla.ShowDialog()
                If Windows.Forms.DialogResult.OK Then
                    'Dim direccion As String = CarpetaCalibracion.ToString + TXTSERIE.Text.ToString + "-" + txtCalibracion.Text.ToString + "-" + DateTimePicker1.Value.ToShortDateString.Replace("/", "-") + ".pdf"
                    Dim ruta As String = pantalla.FileName
                    'Dim archivo As String = pantalla.FileName.ToString
                    'Dim direccion As String = Path.GetDirectoryName(pantalla.FileName)
                    'Dim archivo As String = Path.GetFileName(pantalla.FileName)
                    Try
                        ' File.Open(CarpetaCalibracion, FileMode.Open, FileAccess.ReadWrite)
                        ' File.Move(archivo, direccion)
                        'File.Create("\\SERVER1\CertificadosCalibracion\" + TXTSERIE.Text.ToString + "-" + txtCalibracion.Text.ToString + "-" + DateTimePicker1.Value.ToShortDateString.Replace("/", "-") + ".pdf")
                        File.Copy(ruta, "\\SERVER1\CertificadosCalibracion\" + TXTSERIE.Text.ToString + "-" + txtCalibracion.Text.ToString + "-" + DateTimePicker1.Value.ToShortDateString.Replace("/", "-") + ".pdf")
                    Catch ex As Exception
                        MessageBox.Show(ex.Message)
                    End Try

                End If
                CONFIRMACION = True
                Me.Close()
            End If
        End If
        If TipoCert = 2 Then
            Dim NCERT = Obtener_Numero_Verificacion()
            Sumar_Num_Verificacion()
            Actualizar(NCERT, DateAdd(DateInterval.Day, -1, DateTimePicker1.Value), TipoCert)
            Grabar(TXTSERIE.Text, NCERT, DateTimePicker1.Value, DateTimePicker2.Value, txtOBS.Text, TipoCert, _usr.Obt_Nombre_y_Apellido)
            CONFIRMACION = True
            Me.Close()
        End If

    End Sub
End Class