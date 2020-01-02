Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.Data.Sql
Public Class PALMA043
    Private MENSAJE As New Clase_mensaje
    Private Clas_Metodos As New Clas_Almacen
    Private Clas_Medidor As New Clas_Medidor
    Private med_rettirar As New Clase_med_retirar
    Private DT_medidores2 As New DataTable
    Private _DEPOSITO As String
    Private Sub PALMA043_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Label3.Text = "USUARIO: " & _usr.Obt_Nombre_y_Apellido
        Label5.Text = "Fecha: " & DateTime.Now().ToShortDateString
        Button3.Enabled = False
        Button1.Enabled = False
        LLENAR_CONTRATO()
    End Sub
    Private Sub LLENAR_CONTRATO()
        Dim TABLA As New DataTable
        Dim CNN As New SqlConnection(conexion)
        Try
            CNN.Open()
            Dim ADT As New SqlDataAdapter("SELECT CCONT807, DESC807 FROM P806_CONTRATO_MED WHERE (CCONT807 <> N'06') ORDER BY DESC807", CNN)
            ADT.Fill(TABLA)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            CNN.Close()
        End Try
        If TABLA.Rows.Count <> 0 Then
            ComboBox1.DataSource = TABLA
            ComboBox1.ValueMember = "CCONT807"
            ComboBox1.DisplayMember = "DESC807"
            ComboBox1.Text = Nothing
        End If
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Refresh()
        Dim pantalla As New OpenFileDialog
        pantalla.DereferenceLinks = True
        pantalla.Filter = "xls files (*.xls)|*.xls"
        pantalla.FilterIndex = 1
        pantalla.Title = "Seleccione el archivo"
        pantalla.RestoreDirectory = False
        pantalla.ShowDialog()
        If Windows.Forms.DialogResult.OK Then
            TextBox1.Text = pantalla.FileName
        End If
        If TextBox1.Text <> Nothing Then
            Button3.Enabled = True
        End If
    End Sub
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim CONTRATO As String = ComboBox1.SelectedValue
        Dim MEDIDOR As Decimal
        Dim OT As String = "SO"
        Dim FECHACARGA As Date = Date.Now
        Dim FECHAREALIZADO As Date = Date.Now
        Dim POLIZA As String = "SP"
        Dim CANTIDAD As Decimal = 0
        Dim Capacidad As String = "400015"
        Dim total As Integer
        Dim operario As String = "SO"
        DT_medidores2 = CargarExcel(TextBox1.Text, "Hoja1")
        total = DT_medidores2.Rows.Count
        ProgressBar1.Visible = True
        ProgressBar1.Minimum = 0
        ProgressBar1.Maximum = total
        ProgressBar1.Value = 0
        Try
            If DT_medidores2.Rows.Count <> 0 Then
                For I = 0 To total - 1
                    ' MessageBox.Show(I.ToString)
                    'TENGO QUE VERIFICAR SI ESTA REALIZADO
                    If IsNumeric(DT_medidores2.Rows(I).Item(0)) Then
                        Dim PASA As Boolean = True
                        MEDIDOR = DT_medidores2.Rows(I).Item(0)
                        Capacidad = DT_medidores2.Rows(I).Item(1)
                        med_rettirar.Exite_Medi_2(MEDIDOR)
                        med_rettirar.LeerContratoMed(MEDIDOR)
                        Dim med_nuevo As New UnMedidor(MEDIDOR)
                        If med_nuevo.LeerExiste Then
                            If med_nuevo.LeerEstado = 5 Or med_nuevo.LeerEstado = 2 Or med_nuevo.LeerEstado = 7 Then
                                PASA = True
                            Else
                                PASA = False
                            End If
                        End If
                        If PASA = True Then
                            If med_rettirar.GETSETCONTRATO() <> "00" Then
                                If med_rettirar.LEERFINFO = "NO" Then
                                    If CONTRATO = "07" Then
                                        med_rettirar.GRABAR_MEDIDOR(MEDIDOR, FECHACARGA, _usr.Obt_Usr, "0", Capacidad, POLIZA, CONTRATO, FECHAREALIZADO, 7, 0, OT, operario)
                                        'TENGO QUE VERIFICAR SI ES UNA OT QUE SE PERMITE CARGAR MEDRET
                                        CANTIDAD += 1
                                    Else
                                        med_rettirar.GRABAR_MEDIDOR(MEDIDOR, FECHACARGA, _usr.Obt_Usr, "0", Capacidad, POLIZA, CONTRATO, FECHAREALIZADO, 0, 0, OT, operario)
                                        ' med_rettirar.Grabar_Temp_posible(MEDIDOR, Capacidad, POLIZA, OT, operario, FECHACARGA, 0, CONTRATO)
                                        'TENGO QUE VERIFICAR SI ES UNA OT QUE SE PERMITE CARGAR MEDRET
                                        CANTIDAD += 1
                                    End If
                                Else
                                    If CONTRATO = "04" Then
                                        med_rettirar.ACTUALIZAR_MEDIDOR_GUARDIA(MEDIDOR, CONTRATO)
                                        CANTIDAD += 1
                                    End If
                                End If
                            End If
                        End If
                    Else
                        MessageBox.Show("no numerico")
                    End If
                    ProgressBar1.Value = ProgressBar1.Value + 1
                Next
            End If
            MENSAJE.MADVE005(CANTIDAD, "MEDIDORES")
            ProgressBar1.Visible = False
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Function ValidarOT(ByVal ot As String) As Boolean
        Dim resp As Boolean = False
        Dim cnn As New SqlConnection(conexion)
        cnn.Open()
        Dim adt As New SqlCommand("select OT from OT where OT=@D1", cnn)
        adt.Parameters.Add(New SqlParameter("D1", ot))
        Dim lector As SqlDataReader = adt.ExecuteReader
        If lector.Read Then
            resp = True
        End If
        cnn.Close()
        Return resp
    End Function
    Private Function OperarioDNI(ByVal nom As String) As String
        Dim resp As String = ""
        Dim cnn As New SqlConnection(conexion)
        cnn.Open()
        Dim adt As New SqlCommand("select DNI from P_COLABORAR_805 where COLABORAR=@D1", cnn)
        adt.Parameters.Add(New SqlParameter("D1", nom))
        Dim lector As SqlDataReader = adt.ExecuteReader
        If lector.Read Then
            resp = lector.GetValue(0)
        End If
        cnn.Close()
        Return resp
    End Function
    

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.Text <> Nothing Then
            Button1.Enabled = True
        End If
    End Sub
End Class