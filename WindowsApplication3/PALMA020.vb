Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.Data.Sql
Imports System.IO
Imports System
Imports System.Collections
Imports Microsoft.VisualBasic
Public Class PALMA020
    Private nmedidor As String
    Private almacen As String
    Private estado As Integer
    Private codmate As String
    Private poliza As String
    Private mensaje As New Clase_mensaje
    Private METODOS As New Clas_Almacen
    Private METODO_MED As New Clas_Medidor
    Private falta As String
    Private finfo As String
    Private futil As String
    Private fremito As String
    Private nremitoD As String
    Private usrresol As String
    Private fresul As String
    Private motivo As String
    Private poliza_error As String
    Private motivo_error As String
    Private Sub PALMA020_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label3.Text = "USUARIO: " & _usr.Obt_Nombre_y_Apellido
        Label5.Text = "Fecha: " & DateTime.Now().ToShortDateString
    End Sub
    Private Function buscar_medidor(ByVal medidor As Decimal) As Boolean
        Dim resp As Boolean = False
        Dim cnn1 As SqlConnection = New SqlConnection(conexion)
        'Abrimos la conexion
        cnn1.Open()
        'Creamos el comando para crear la consulta
        Dim Comando As New SqlClient.SqlCommand("select NSERIE_102, CMATE_102, F_ALTA_102,ESTADO_102,F_INFO_102,F_UTIL_102, POLIZA_102,CALMA_102, USER_R_102, F_RESU_102, RDEVO_102, FDEVO_102, C_RESU_102, POLIZA_E_102, MOTIVO_102,FREV_102 FROM T_MEDI_102 WHERE NSERIE_102= @D1", cnn1)
        'Ejecutamos el commnad y le pasamos el parametro
        Comando.Parameters.Add(New SqlParameter("D1", medidor))
        Comando.ExecuteNonQuery()
        Dim Dusrs As SqlDataReader = Comando.ExecuteReader
        If Dusrs.Read.ToString Then
            nmedidor = Dusrs.GetValue(0)
            codmate = Dusrs.GetString(1)
            almacen = Dusrs.GetString(7)
            If IsDBNull(Dusrs.GetValue(2)) = False Then
                falta = CDate(Dusrs.GetValue(2)).ToShortDateString
            Else
                falta = ""
            End If
            estado = Dusrs.GetInt32(3)
            If IsDBNull(Dusrs.GetValue(4)) = False Then
                finfo = CDate(Dusrs.GetValue(4)).ToShortDateString
            Else
                finfo = ""
            End If
            If IsDBNull(Dusrs.GetValue(5)) = False Then
                futil = CDate(Dusrs.GetValue(5)).ToShortDateString
            Else
                futil = ""
            End If
            If IsDBNull(Dusrs.GetValue(6)) = False Then
                poliza = Dusrs.GetValue(6)
            Else
                poliza = ""
            End If
            If IsDBNull(Dusrs.GetValue(8)) = False Then
                usrresol = MAIN.OBT_NOM_USER(Dusrs.GetValue(8))
            Else
                usrresol = ""
            End If
            If IsDBNull(Dusrs.GetValue(9)) = False Then
                fresul = Dusrs.GetDateTime(9).ToShortDateString
            Else
                fresul = ""
            End If
            If IsDBNull(Dusrs.GetValue(10)) = False Then
                nremitoD = Dusrs.GetValue(10).ToString.PadLeft(8, "0")
            Else
                nremitoD = ""
            End If
            If IsDBNull(Dusrs.GetValue(11)) = False Then
                fremito = Dusrs.GetDateTime(11).ToShortDateString
            Else
                fremito = ""
            End If
            If IsDBNull(Dusrs.GetValue(12)) = False Then
                motivo = desc_motivo(Dusrs.GetValue(12))
            Else
                motivo = ""
            End If

            If IsDBNull(Dusrs.GetValue(13)) = False Then
                poliza_error = Dusrs.GetValue(13)
            Else
                poliza_error = ""
            End If
            If IsDBNull(Dusrs.GetValue(14)) = False Then
                motivo_error = Dusrs.GetValue(14)
            Else
                motivo_error = ""
            End If
            If IsDBNull(Dusrs.GetValue(15)) = False Then
                lvrev.Text = CDate(Dusrs.GetValue(15)).ToShortDateString
            Else
                lvrev.Text = ""
            End If

            resp = True

        Else
            mensaje.MERRO011()
        End If
        cnn1.Close()
        Return resp
    End Function
   
   
    Private Function Estado_medidor(ByVal D1 As Integer) As String
        Dim resp As String = ""
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("SELECT DESC_802 FROM DET_PARAMETRO_802 WHERE C_TABLA_802=10 AND C_PARA_802 = @D1", con1)
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
    Private Sub LLENAR_DW1(ByVal D1 As Decimal)
        DataGridView2.Rows.Clear()
        Dim cnn1 As SqlConnection = New SqlConnection(conexion)
        'Abrimos la conexion
        cnn1.Open()
        'Creamos el comando para crear la consulta
        Dim Comando As New SqlClient.SqlCommand("select FREMI_110, REMITO_110, ALMAE_110, ALMAR_110 FROM T_MOV_REMI_110 WHERE NSERI_110 = @D1 order by FREMI_110", cnn1)
        'Ejecutamos el commnad y le pasamos el parametro
        Comando.Parameters.Add(New SqlParameter("D1", D1))
        Comando.ExecuteNonQuery()
        Dim LECTOR As SqlDataReader = Comando.ExecuteReader
        While LECTOR.Read
            DataGridView2.Rows.Add(LECTOR.GetDateTime(0).ToShortDateString, LECTOR.GetValue(1), METODOS.NOMBRE_DEPOSITO(LECTOR.GetValue(3)), METODOS.NOMBRE_DEPOSITO(LECTOR.GetValue(2)))
        End While
        cnn1.Close()
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        llenar_datos()
    End Sub
    Private Function desc_motivo(ByVal D1 As Integer) As String
        Dim resp As String = ""
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("SELECT DESC_802 FROM DET_PARAMETRO_802 WHERE C_TABLA_802=13 AND C_PARA_802 = @D1", con1)
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




    
    
    

    Private Sub lbestado_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbestado.DoubleClick
        If estado = 7 Then
            Dim PANTALLA As New PALMA032
            PANTALLA.lleer_datos(motivo_error, poliza_error, estado, nmedidor)
            PANTALLA.Text = "VER ERROR EN MEDIDOR - PALMA032"
            PANTALLA.ShowDialog()

        End If
        If estado = 2 Then
            Dim PANTALLA As New PALMA032
            PANTALLA.lleer_datos(motivo_error, poliza_error, estado, nmedidor)
            PANTALLA.Text = "CARGAR ERROR EN MEDIDOR - PALMA032"
            PANTALLA.ShowDialog()
            If PANTALLA.confir = True Then
                llenar_datos()
            End If
        End If
    End Sub
    Private Sub llenar_datos()
        If IsNumeric(TextBox1.Text) And TextBox1.Text <> Nothing Then
            If buscar_medidor(TextBox1.Text) = True Then
                lbestado.ForeColor = Color.Black
                TXTAlmacen.Text = METODOS.NOMBRE_DEPOSITO(almacen)
                lbestado.Text = Estado_medidor(estado)
                TXTF_ingreso.Text = falta
                TXTF_informado.Text = finfo
                TXTF_utilizado.Text = futil
                TXTDescMaterial.Text = METODOS.detalle_material(codmate)
                TXTPoliza.Text = poliza
                LLENAR_DW1(TextBox1.Text)
                TextBox3.Text = usrresol
                TextBox2.Text = fresul
                TextBox5.Text = nremitoD
                TextBox4.Text = fremito
                TextBox6.Text = motivo

                Select Case estado
                    Case Is = 1
                        lbestado.ForeColor = Color.Green
                    Case Is = 2
                        lbestado.ForeColor = Color.Black
                    Case Is = 0
                        lbestado.ForeColor = Color.Blue
                    Case Is = 9
                        lbestado.ForeColor = Color.DarkBlue
                    Case Is = 5
                        lbestado.ForeColor = Color.DarkOrange
                    Case Is = 6
                        lbestado.ForeColor = Color.DarkRed
                End Select
            Else
                TextBox1.Text = Nothing
                TXTAlmacen.Text = Nothing
                lbestado.ForeColor = Color.Black
                lbestado.Text = "-------ESTADO-------"
                TXTF_ingreso.Text = Nothing
                TXTF_informado.Text = Nothing
                TXTF_utilizado.Text = Nothing
                TXTDescMaterial.Text = Nothing
                TXTPoliza.Text = Nothing
                TextBox3.Text = Nothing
                TextBox2.Text = Nothing
                TextBox5.Text = Nothing
                TextBox4.Text = Nothing
                TextBox6.Text = Nothing
                DataGridView2.Rows.Clear()
            End If

        Else
            mensaje.MERRO006()
            TextBox1.Text = Nothing
            TXTAlmacen.Text = Nothing
            lbestado.ForeColor = Color.Black
            lbestado.Text = "-------ESTADO-------"
            TXTF_ingreso.Text = Nothing
            TXTF_informado.Text = Nothing
            TXTF_utilizado.Text = Nothing
            TXTDescMaterial.Text = Nothing
            TXTPoliza.Text = Nothing
            TextBox3.Text = Nothing
            TextBox2.Text = Nothing
            TextBox5.Text = Nothing
            TextBox4.Text = Nothing
            TextBox6.Text = Nothing
            DataGridView2.Rows.Clear()
        End If
    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        If Asc(e.KeyChar) = 13 Then
            llenar_datos()
        End If
    End Sub

    Private Sub txtRev_DoubleClick(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label19_DoubleClick(sender As Object, e As EventArgs) Handles lvrev.DoubleClick
        If lvrev.Text <> "" Then

            If CDate(lvrev.Text) <= Date.Today Then
                Dim msm As DialogResult = MessageBox.Show("ESTA POR ACTUALIZAR EL MEDIDOR " + TextBox1.Text.ToString + vbCrLf + "¿ESTA SEGURO?", "ACTUALIZAR MEDIDOR", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If msm = DialogResult.Yes Then
                    METODO_MED.MODIFICAR_MEDIDOR_REVISADO(TextBox1.Text)
                    buscar_medidor(TextBox1.Text)
                End If

            End If
        End If
    End Sub
End Class