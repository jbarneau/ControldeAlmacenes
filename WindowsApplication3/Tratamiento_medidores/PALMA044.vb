Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.Data.Sql
Imports System.IO
Imports System
Imports System.Collections
Imports Microsoft.VisualBasic
Public Class PALMA044
    Private nmedidor As String
    Private fcargo As String
    Private userCargo As String
    Private almacen As String
    Private ot As String
    Private codmate As String
    Private poliza As String
    Private contrato As String
    Private fretirado As String
    Private finfo As String
    Private Familia As String
    Private cajon As String
    Private UserCajon As String
    Private estado As Integer
    Private fremito As String
    Private nremitoD As String
    Private UserRemito As String
    Private mensaje As New Clase_mensaje
    Private METODOS As New Clas_Almacen

    Private Sub PALMA044_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label3.Text = "USUARIO: " & _usr.Obt_Nombre_y_Apellido
        Label5.Text = "Fecha: " & DateTime.Now().ToShortDateString
        
    End Sub
    Private Sub txtmedidor_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtmedidor.KeyPress
        If Asc(e.KeyChar) = 13 Then
            llenar_datos()

        End If
    End Sub

#Region "funciones"
    Private Sub LLENAR_DW1(ByVal D1 As String)
        DataGridView2.Rows.Clear()
        Dim cnn1 As SqlConnection = New SqlConnection(conexion)
        'Abrimos la conexion
        Try
            cnn1.Open()
            'Creamos el comando para crear la consulta
            Dim Comando As New SqlClient.SqlCommand("select FMOVI_114,NMOVI_114,TMOV_114,USERM_114 FROM T_MOV_MED_DEVO_114 WHERE NSERI_114 = @D1 order by NMOVI_114", cnn1)
            'Ejecutamos el commnad y le pasamos el parametro
            Comando.Parameters.Add(New SqlParameter("D1", D1))
            Comando.ExecuteNonQuery()
            Dim LECTOR As SqlDataReader = Comando.ExecuteReader
            While LECTOR.Read
                DataGridView2.Rows.Add(LECTOR.GetDateTime(0).ToShortDateString, LECTOR.GetValue(1), desMOVI(LECTOR.GetValue(2)), OBT_NOM_USER(LECTOR.GetValue(3)))
            End While



        Catch ex As Exception
            MessageBox.Show(ex.Message + " llenardw1")
        Finally
            cnn1.Close()
        End Try



    End Sub

    Private Function buscar_medidor(ByVal medidor As String) As Boolean


        Dim resp As Boolean = False

        Dim cnn1 As SqlConnection = New SqlConnection(conexion)
        'Abrimos la conexion
        Try
            cnn1.Open()
            'Creamos el comando para crear la consulta
            Dim Comando As New SqlClient.SqlCommand("select NSERI_113,FCARGO_113,USER_C_113,DEPOSI_113,OT_113,CMATE_113,POLIZA_113,CONTRATO_113,FRETIRO_113,FINFO_113,FAMILIA_113,CAJON_113,USER_AC_113,ESTADO_113,FREMITO_113,NREMITO_113,USER_REM_113,OPERA_113,PROVE_113 FROM T_MED_DEVO_113 WHERE NSERI_113= @D1", cnn1)
            'Ejecutamos el commnad y le pasamos el parametro
            Comando.Parameters.Add(New SqlParameter("D1", medidor))
            Comando.ExecuteNonQuery()
            Dim Dusrs As SqlDataReader = Comando.ExecuteReader
            If Dusrs.Read.ToString Then
                '  Try
                nmedidor = medidor
                fcargo = CDate(Dusrs.GetValue(1)).ToShortDateString
                userCargo = OBT_NOM_USER(Dusrs.GetValue(2).ToString)
                almacen = Dusrs.GetValue(3).ToString
                ot = Dusrs.GetValue(4).ToString
                codmate = Dusrs.GetValue(5)
                poliza = Dusrs.GetValue(6)
                contrato = ContratoMed(Dusrs.GetValue(7).ToString)
                If IsDBNull(Dusrs.GetValue(8)) Then
                    fretirado = ""
                Else
                    fretirado = Dusrs.GetValue(8)
                End If
                If IsDBNull(Dusrs.GetValue(9)) Then
                    finfo = "SIN INFO"
                Else
                    finfo = Dusrs.GetValue(9)
                End If
                Familia = Dusrs.GetValue(10)
                If IsDBNull(Dusrs.GetValue(11)) Then
                    cajon = "SIN CAJON"
                    UserCajon = ""
                Else
                    cajon = Dusrs.GetValue(11)
                    UserCajon = OBT_NOM_USER(Dusrs.GetValue(12))
                End If
                estado = Dusrs.GetValue(13)
                If IsDBNull(Dusrs.GetValue(14)) = False Then
                    fremito = CDate(Dusrs.GetValue(14)).ToShortDateString
                    nremitoD = Dusrs.GetValue(15)
                    UserRemito = OBT_NOM_USER(Dusrs.GetValue(16))
                Else
                    fremito = ""
                    nremitoD = ""
                    UserRemito = ""
                End If
                'Catch ex As Exception
                '  MessageBox.Show(ex.Message)
                'End Try

                resp = True
            Else
                resp = False

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message + " en buscar medidor")
        Finally
            cnn1.Close()

        End Try


        Return resp
    End Function
    Private Sub llenar_datos()
        If txtmedidor.Text <> Nothing Then
            If buscar_medidor(txtmedidor.Text) = True Then
                txtFcargo.Text = fcargo
                TXTAlmacen.Text = METODOS.NOMBRE_DEPOSITO(almacen)
                txtOt.Text = ot
                Lcod.Text = codmate.ToString
                TXTDescMaterial.Text = METODOS.detalle_material(codmate)
                TXTPoliza.Text = poliza
                txtContrato.Text = contrato
                txtFretiro.Text = fretirado
                txtInformado.Text = finfo
                txtFamilia.Text = desfamilia(Familia)
                txtCajon.Text = cajon
                txtUserCajon.Text = UserCajon
                lbestado.Text = Estado_medidor(estado)
                txtFRemito.Text = fremito
                txtNremito.Text = nremitoD
                txtUsrRemito.Text = UserRemito
                LLENAR_DW1(txtmedidor.Text)
            Else
                If IsNumeric(txtmedidor.Text) = True Then
                    med_no_encontrado()
                End If

                txtmedidor.Text = Nothing
                mensaje.MERRO011()
                txtFcargo.Text = Nothing
                TXTAlmacen.Text = Nothing
                Lcod.Text = Nothing
                txtOt.Text = Nothing
                TXTDescMaterial.Text = Nothing
                TXTPoliza.Text = Nothing
                txtContrato.Text = Nothing
                txtFretiro.Text = Nothing
                txtInformado.Text = Nothing
                txtFamilia.Text = Nothing
                txtCajon.Text = Nothing
                txtUserCajon.Text = Nothing
                lbestado.Text = "-------ESTADO-------"
                txtFRemito.Text = Nothing
                txtNremito.Text = Nothing
                txtUsrRemito.Text = Nothing
                DataGridView2.Rows.Clear()
            End If

        Else
            mensaje.MERRO006()
            txtFcargo.Text = Nothing
            Lcod.Text = Nothing
            TXTAlmacen.Text = Nothing
            txtOt.Text = Nothing
            TXTDescMaterial.Text = Nothing
            TXTPoliza.Text = Nothing
            txtContrato.Text = Nothing
            txtFretiro.Text = Nothing
            txtInformado.Text = Nothing
            txtFamilia.Text = Nothing
            txtCajon.Text = Nothing
            txtUserCajon.Text = Nothing
            lbestado.Text = "-------ESTADO-------"
            txtFRemito.Text = Nothing
            txtNremito.Text = Nothing
            txtUsrRemito.Text = Nothing
            DataGridView2.Rows.Clear()
        End If
    End Sub
    Private Function medidorDetectado(medidor As String) As Boolean
        Dim resp As Boolean = False
        Dim cnn As New SqlConnection(conexion)
        Try
            cnn.Open()
            Dim adt As New SqlCommand("SELECT ALMA_117,FECHA_117, NCAJON_117 FROM T_MED_DETEC_117 WHERE NSERIE_117=@D1", cnn)
            adt.Parameters.Add(New SqlParameter("D1", medidor))
            Dim lector As SqlDataReader = adt.ExecuteReader
            If lector.Read Then
                MessageBox.Show("EL MEDIDOR " + medidor.ToString + vbCrLf + "FUE ENCONTRADO EL DIA " + CDate(lector.GetValue(1)).ToShortDateString + vbCrLf + "ESTA EN " + METODOS.NOMBRE_DEPOSITO(lector.GetValue(0)) + vbCrLf + "CAJON Nº" + lector.GetValue(2), "INFORMACION IMPORTANTE", MessageBoxButtons.OK, MessageBoxIcon.Information)
                resp = True
            Else
                resp = False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn.Close()
        End Try
        Return resp
    End Function
    Private Sub med_no_encontrado()
        Dim cnn As New SqlConnection(conexion)
        Try
            cnn.Open()
            Dim adt As New SqlCommand("SELECT NMEDIDOR, ESTADO, FECHA,LECTURA FROM MED_NO_ENCONTRADO WHERE NMEDIDOR=@D1", cnn)
            adt.Parameters.AddWithValue("D1", txtmedidor.Text)
            Dim LECTOR As SqlDataReader = adt.ExecuteReader
            If LECTOR.Read Then
                MessageBox.Show("EL MEDIDOR INGRESO AL PAÑOL EL DIA: " + CDate(LECTOR.GetValue(2)).ToShortDateString + vbCrLf + "LECTURA:" + LECTOR.GetValue(3).ToString, "MEDIDOR " + LECTOR.GetValue(0).ToString, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn.Close()
        End Try
    End Sub

    Private Function desMOVI(ByVal D1 As String) As String
        Dim resp As String = ""
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("SELECT DESC_802 FROM DET_PARAMETRO_802 WHERE C_TABLA_802=16 AND C_PARA_802 = @D1", con1)
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

    Private Function Estado_medidor(ByVal D1 As String) As String
        Dim resp As String = ""
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("SELECT DESC_802 FROM DET_PARAMETRO_802 WHERE C_TABLA_802=14 AND C_PARA_802 = @D1", con1)
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
#End Region

   
  
   
   

    Private Sub Lcod_DoubleClick(sender As Object, e As System.EventArgs) Handles Lcod.DoubleClick
        If Lcod.Text <> Nothing Then
            If _usr.Activar_BT("PALMA035") = True Then
                Dim pantalla As New PALMA035
                pantalla.ShowDialog()
                If pantalla.LEERERPUESTA = True Then
                    Lcod.Text = pantalla.LEERCOD.ToString
                    TXTDescMaterial.Text = pantalla.LEERDES
                    ACTUALIZAR_MEDIDOR(txtmedidor.Text, pantalla.LEERCOD)
                End If
            End If
        End If
    End Sub
    Public Sub ACTUALIZAR_MEDIDOR(MEDIDOR As String, COD As String)
        'creo la cadena de conexion
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        Try
            con1.Open()
            'creo el comando para pasarle los parametros
            Dim comando1 As New SqlClient.SqlCommand("Update T_MED_DEVO_113 set CMATE_113=@D1 WHERE NSERI_113=@E1", con1)
            'creo el lector de parametros
            comando1.Parameters.Add(New SqlParameter("D1", COD))
            comando1.Parameters.Add(New SqlParameter("E1", MEDIDOR))
            comando1.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            con1.Close()
        End Try

        'esta todo bien
    End Sub
    Public Sub ACTUALIZAR_MEDIDOR(MEDIDOR As String)
        'creo la cadena de conexion
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        Try
            con1.Open()
            'creo el comando para pasarle los parametros
            Dim comando1 As New SqlClient.SqlCommand("Update T_MED_DEVO_113 set FINFO_113=@D1 WHERE NSERI_113=@E1", con1)
            'creo el lector de parametros
            comando1.Parameters.Add(New SqlParameter("D1", Date.Now))
            comando1.Parameters.Add(New SqlParameter("E1", MEDIDOR))
            comando1.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            con1.Close()
        End Try

        'esta todo bien
    End Sub

    Private Sub txtInformado_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtInformado.KeyPress
        If _usr.Activar_BT("PALMA035") Then
            If txtInformado.Text = "" Then
                Dim consulta As MsgBoxResult = MessageBox.Show("Esta por colocarle una fecha de informado al medidor." + vbCrLf + "¿Desea Continuar?", "CONSULTA", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                If consulta = MsgBoxResult.Yes Then
                    'TENGO QUE ACTUALIZAR
                    Dim CLASMEDIDOR As New Clase_med_retirar
                    ACTUALIZAR_MEDIDOR(txtmedidor.Text)
                    mensaje.MADVE001()
                End If
            End If
        End If

    End Sub


End Class