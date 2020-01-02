Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Public Class PALMA048
    Private mensaje As New Clase_mensaje
    Private UNA_MAQUINA As New Clase_maquina
    Private Tabla_Calibracion As New DataTable
    Private Sub PALMA048_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label3.Text = "USUARIO: " & _usr.Obt_Nombre_y_Apellido
        Label5.Text = "Fecha: " & DateTime.Now().ToShortDateString
    End Sub
   
    Private Sub TXTSERIE_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TXTSERIE.DoubleClick
        Dim PANTALLA As New PALMA010BIS
        PANTALLA.ShowDialog()
        If PANTALLA.llerrespuesta = True Then
            TXTSERIE.Text = PANTALLA.leernserie
            LLENAR(TXTSERIE.Text)
        End If
    End Sub
    Private Sub btBorrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btBorrar.Click
        BORRAR()
    End Sub
#Region "FUNCIONES"
   
  
    Private Sub LLENAR_tabla_CALIBRACION(ByVal cod As String)
        Tabla_Calibracion.Clear()
        Dim CNN As New SqlConnection(conexion)
        Try
            CNN.Open()
            Dim ADT As New SqlDataAdapter("SELECT NSERIE_118, TIPO_118, FCALI_118, NCALI_118, VTO_118, FBAJA_118, OBS_118, RESPON_118 FROM T_CALIBRACION_118 WHERE (NSERIE_118 = @D1) ORDER BY FCALI_118, TIPO_118", CNN)
            ADT.SelectCommand.Parameters.Add(New SqlParameter("D1", cod))
            ADT.Fill(Tabla_Calibracion)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            CNN.Close()
        End Try
    End Sub
    Private Sub LLENAR(ByVal NSERIE As String)
        UNA_MAQUINA.TOMAR(NSERIE)
        If UNA_MAQUINA.LEEREXISTE = True Then
            TXTSERIE.Enabled = False
            lbModelo.Text = UNA_MAQUINA.LEERMODELO
            lbChapa.Text = UNA_MAQUINA.LEERCHAPA
            lbMarca.Text = UNA_MAQUINA.LEERMARCA
            lbFalta.Text = CDate(UNA_MAQUINA.LEEFALTA).ToShortDateString
            lbOBS.Text = UNA_MAQUINA.LEEOBS
            lbFbaja.Text = UNA_MAQUINA.LEERFBAJA
            lbTipo.Text = DESC_TIPO(UNA_MAQUINA.LEERTIPO)
            lbEstado.Text = DESC_Estado(UNA_MAQUINA.LEERESTADO)
            lbDeposito.Text = NOM_DEPOSITOS2(UNA_MAQUINA.LEERALMACEN)
            If ValidarCalibracion(UNA_MAQUINA.LEERTIPO) = 1 Then
                'tengo que verificar si el estado del instrumento esta activo
                Dim FECHA As Date = ValidarFechaCalibracion(UNA_MAQUINA.LEERNSERIE)
                If FECHA <= Date.Today Then
                    LinkLabel1.Visible = True
                    LinkLabel1.Text = "CERTIFICADO SE VENCIO EL DIA " + FECHA.ToShortDateString
                Else
                    LinkLabel1.Visible = True
                    LinkLabel1.Text = "CERTIFICADO SE VENCE EL DIA " + FECHA.ToShortDateString

                End If
            End If
        Else
            MessageBox.Show("EL NUMERO DE SERIE NO EXISTE", "CONSULTA", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        End If
    End Sub
    Private Function NOM_DEPOSITOS2(ByVal cod As String) As String
        Dim resp As String = ""
        'CONECTO LA BASE
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        Try
            cnn2.Open()
            Dim adaptador As New SqlCommand("SELECT (APELL_003+ ' ' +NOMB_003) AS NOMBRE FROM M_PERS_003 WHERE NDOC_003=@D1", cnn2)
            adaptador.Parameters.Add(New SqlParameter("D1", cod))
            'LLENO EL ADAPTADOR CON EL DATASET
            Dim lector As SqlDataReader = adaptador.ExecuteReader
            If lector.Read Then
                resp = lector.GetValue(0)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn2.Close()
        End Try
        Return resp
    End Function
    Private Function ValidarFechaCalibracion(ByVal numero As String) As Date
        Dim fecha As Date
        Dim cnn As New SqlConnection(conexion)
        Try
            cnn.Open()
            Dim adt As New SqlCommand("SELECT VTO_118 FROM T_CALIBRACION_118 WHERE NSERIE_118 = @D1 AND FBAJA_118 IS NULL", cnn)
            adt.Parameters.Add(New SqlParameter("D1", numero))
            Dim LECTOR As SqlDataReader = adt.ExecuteReader
            If LECTOR.Read Then
                fecha = LECTOR.GetValue(0)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn.Close()
        End Try
        Return fecha
    End Function
    Private Function ValidarCalibracion(ByVal codigo As String) As Integer
        Dim resp As Integer = 0
        Dim cnn As New SqlConnection(conexion)
        Try
            cnn.Open()
            Dim adt As New SqlCommand("SELECT CALI_806 FROM P_MAQUINA_806 WHERE TIPO_806 =@D1", cnn)
            adt.Parameters.Add(New SqlParameter("D1", codigo))
            Dim LECTOR As SqlDataReader = adt.ExecuteReader
            If LECTOR.Read Then
                resp = LECTOR.GetValue(0)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn.Close()
        End Try
        Return resp
    End Function
    Private Sub BORRAR()
        LinkLabel1.Visible = False
        lbOBS.Text = Nothing
        lbEstado.Text = Nothing
        lbTipo.Text = Nothing
        TXTSERIE.Text = Nothing
        TXTSERIE.Enabled = True
        lbModelo.Text = Nothing
        lbChapa.Text = Nothing
        lbFalta.Text = Nothing
        lbMarca.Text = Nothing
        lbFbaja.Text = Nothing
        lbChapa.Enabled = False
        lbOBS.Enabled = False
        lbMarca.Enabled = False
    End Sub
    Private Function DESC_TIPO(ByVal COD As String) As String
        Dim RESP As String = ""
        Dim CNN As New SqlConnection(conexion)
        Try
            CNN.Open()
            Dim ADT As New SqlCommand("SELECT DESC_806 FROM P_MAQUINA_806 WHERE TIPO_806=" + COD.ToString, CNN)
            Dim LECTOR As SqlDataReader = ADT.ExecuteReader
            If LECTOR.Read Then
                RESP = LECTOR.GetValue(0)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            CNN.Close()
        End Try
        Return RESP
    End Function
    Private Function DESC_Estado(ByVal COD As String) As String
        Dim RESP As String = ""
        Dim CNN As New SqlConnection(conexion)
        Try
            CNN.Open()
            Dim ADT As New SqlCommand("SELECT DESC_802 FROM DET_PARAMETRO_802 WHERE C_TABLA_802 = 17 and C_PARA_802=" + COD.ToString, CNN)
            Dim LECTOR As SqlDataReader = ADT.ExecuteReader
            If LECTOR.Read Then
                RESP = LECTOR.GetValue(0)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            CNN.Close()
        End Try
        Return RESP
    End Function
#End Region
    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        'Dim p As New PALMA011
        'p.TOMAR(UNA_MAQUINA.LEERTIPO, TXTSERIE.Text)
        'p.ShowDialog()
        'If p.LEERCONFIRMACION = True Then
        '    mensaje.MADVE001()
        '    BORRAR()
        '    LLENAR(UNA_MAQUINA.LEERNSERIE)
        'Else
        '    MessageBox.Show("NO SE CARGO EL VENCIMIENTO DE LA CALIBRACION")
        'End If
    End Sub
    

  
    Private Sub TXTSERIE_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTSERIE.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If IsNothing(TXTSERIE.Text) = False Then
                LLENAR(TXTSERIE.Text)
            Else
                mensaje.MERRO008()
            End If
        End If
    End Sub
End Class