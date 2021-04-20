Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports CrystalDecisions.[Shared]

Public Class PALMA038
    Private MENSAJES As New Clase_mensaje
    Private ALMACEN As New Clas_Almacen
    Private _DEPOSITO As String
    Private cantmed As Integer
    Private nomfamilia As String
    Private med_rettirar As New Clase_med_retirar
    Private familia As Integer
    Private cajon As String
    Private fecha As Date
    Private tablacust As New DataTable
    Private medidor As Decimal
    Private Sub PALMA038_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        If _usr.Obt_Almacen <> "0" Then ' SI ES DISTINTO DE CERO PONGO EL COMBOBOS CON EL NOMBRE Y GUAROD EL DEPOSITO
            ComboBox3.DropDownStyle = ComboBoxStyle.DropDown
            _DEPOSITO = _usr.Obt_Almacen
            ComboBox3.Text = NOMBRE_DEPOSITO(_DEPOSITO)
        Else
            ComboBox3.Enabled = True
            ComboBox3.Focus()
            llenar_DS_DEPOSITO()
            ComboBox3.Focus()

        End If
        LLENAR()
    End Sub
    Private Sub LLENAR()
        DGV1.Rows.Clear()
        Dim MEDIDOR As String
        Dim POLIZA As String
        Dim CAJON As String
        Dim FCUSTODIA As String
        Dim cnn As New SqlConnection(MAIN.conexion)
        Try
            cnn.Open()
            Dim adt As New SqlCommand("select NSERI_113, POLIZA_113,CAJON_113, FCUSTODIA_113 FROM T_MED_DEVO_113 WHERE ESTADO_113=9 And FCUSTODIA_113 <= @D1 ORDER BY CAJON_113, NSERI_113", cnn)
            adt.Parameters.Add(New SqlParameter("D1", DateAdd(DateInterval.Month, -6, Date.Now)))
            Dim lector As SqlDataReader = adt.ExecuteReader
            Do While lector.Read
                MEDIDOR = lector.GetValue(0)
                POLIZA = lector.GetValue(1)
                CAJON = lector.GetValue(2)
                FCUSTODIA = CDate(lector.GetValue(3)).ToShortDateString
                DGV1.Rows.Add(MEDIDOR, POLIZA, CAJON, FCUSTODIA)
            Loop
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn.Close()
        End Try
        If DGV1.Rows.Count <> 0 Then
            BTCONFIRMAR.Enabled = True
            BTEXPORTAR.Enabled = True
        Else
            BTCONFIRMAR.Enabled = False
            BTEXPORTAR.Enabled = False
        End If
    End Sub


    Private Sub CheckBox1_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles CheckBox1.CheckedChanged
        For i = 0 To Me.DGV1.RowCount - 1
            If Me.DGV1.Item(4, i).Value <> CheckBox1.Checked Then
                Me.DGV1.Item(4, i).Value = CheckBox1.Checked
            End If
        Next
    End Sub

    Private Sub BTEXPORTAR_Click(sender As System.Object, e As System.EventArgs) Handles BTEXPORTAR.Click
        If My.Computer.FileSystem.DirectoryExists("C:\ARCHIVO") = False Then
            My.Computer.FileSystem.CreateDirectory("C:\ARCHIVO")
        End If

        Dim fichero As String = "C:\Archivo\Medidores_Custodia_Para_Entregar" + "_" + Date.Now.Day.ToString.PadLeft(2, "0") + Date.Now.Month.ToString.PadLeft(2, "0") + Date.Now.Year.ToString + ".csv"
        Dim a As New System.IO.StreamWriter(fichero)
        a.WriteLine("N_SERIE;POLIZA; CAJON;FEC_CUSTODIA")
        For I = 0 To Me.DGV1.RowCount - 1
            a.WriteLine(Me.DGV1.Item(0, I).Value.ToString + ";" + Me.DGV1.Item(1, I).Value.ToString + ";" + Me.DGV1.Item(2, I).Value.ToString + ";" + Me.DGV1.Item(3, I).Value.ToString)
        Next
        a.Close()
        MENSAJES.MADVE002(fichero)
    End Sub

    Private Sub BTCONFIRMAR_Click(sender As System.Object, e As System.EventArgs) Handles BTCONFIRMAR.Click
        Dim cant_liberados As Integer = 0
        For i = 0 To DGV1.Rows.Count - 1
            If DGV1.Item(4, i).Value = True Then
                cant_liberados += 1
            End If
        Next
        If cant_liberados <> 0 Then

            Dim mensaje As DialogResult = MessageBox.Show("DESEA LIBERAR " + cant_liberados.ToString + " MEDIDORES", "PREGUNTA", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If mensaje = DialogResult.Yes Then
                Dim DS As New DSListado
                For I = 0 To DGV1.RowCount - 1
                    If DGV1.Item(4, I).Value = True Then
                        DS.Tables("ListadoMedLiberados").Rows.Add(DGV1.Item(0, I).Value, DGV1.Item(1, I).Value, DGV1.Item(2, I).Value, DGV1.Item(3, I).Value)

                        med_rettirar.ACTUALIZAR_MEDIDOR_GUARDIA_NUEVO(DGV1.Item(0, I).Value)
                    End If
                Next
                Dim informe As New ListadoMedLiberados
                informe.SetDataSource(DS)
                Dim archivo As String = "C:\ARCHIVO\MEDIDORES_LIBERADOS_" + Date.Now.ToString("dd_MM_yyyy") + ".PDF"

                informe.ExportToDisk(ExportFormatType.PortableDocFormat, archivo)
                Dim CONSULTA As DialogResult = MessageBox.Show("EL ARCHIVO SE ENCUENTRA EN " + vbCrLf + archivo + vbCrLf + "¿DESEA ABRIRLO?", "ABRIR", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If CONSULTA = DialogResult.Yes Then
                    Process.Start(archivo)
                End If


            End If

        Else
            MessageBox.Show("NO TIENE MEDIDORES SELECCIONADOS", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End If







        'Dim pantalla As New PALMA037
        'Dim fecha As Date = Date.Now
        'Dim nmov As Decimal
        'If ComboBox3.Text <> Nothing Then
        '    If _usr.Obt_Almacen = 0 Then
        '        _DEPOSITO = ComboBox3.SelectedValue
        '    End If
        '    Dim cant As Integer = 0
        '    For i = 0 To DGV1.Rows.Count - 1
        '        If Me.DGV1.Item(4, i).Value = True Then
        '            cant += 1
        '        End If
        '    Next
        '    If cant <> 0 Then
        '        pantalla.TOMAR(_DEPOSITO,)
        '        pantalla.ShowDialog()
        '        If pantalla.LEERRESPUESTA = True Then
        '            nmov = med_rettirar.Obtener_Numero_Mov
        '            familia = pantalla.LEERFAMILIA
        '            nomfamilia = pantalla.LEERNOMBREFAMILIA
        '            If pantalla.LEERCAJON = "--NUEVO CAJON--" Then
        '                cajon = Date.Now.Year.ToString + med_rettirar.Obtener_Numero_lote.ToString.PadLeft(8, "0")
        '            Else
        '                cajon = pantalla.LEERCAJON
        '            End If
        '            For I = 0 To DGV1.Rows.Count - 1
        '                If Me.DGV1.Item(4, I).Value = True Then
        '                    medidor = Me.DGV1.Item(0, I).Value
        '                    med_rettirar.MODIFICAR_MEDIDOR(_DEPOSITO, cajon, familia, _usr.Obt_Usr, medidor, 1)
        '                    med_rettirar.GRABAR_TRANS(medidor, nmov, fecha, _usr.Obt_Usr, 1)
        '                End If
        '            Next
        '        End If
        '        cantmed = ContMedidores(cajon)
        '        Dim RESPUESTA As MsgBoxResult = MessageBox.Show("DESEA IMPRIMIR EL PAPEL DEL LOTE: " + cajon.ToString, "CONSULTA", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        '        If RESPUESTA = MsgBoxResult.Yes Then
        '            PrintDocument1.Print()
        '        End If
        '        MENSAJES.MADVE001()

        '    Else
        '        MessageBox.Show("DEBE TILDAR ALMENOS UN MEDIDOR VERIFIQUE", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    End If
        '    LLENAR()
        'End If

    End Sub
    Private Function ContMedidores(ncajon As String) As Decimal
        Dim resp As Decimal = 0
        Dim cnn As New SqlConnection(conexion)
        Dim tabla As New DataTable
        Try
            cnn.Open()
            Dim adt As New SqlDataAdapter("SELECT NSERI_113 FROM T_MED_DEVO_113 WHERE CAJON_113=@D1 ", cnn)
            adt.SelectCommand.Parameters.Add(New SqlParameter("D1", ncajon))
            adt.Fill(tabla)
            resp = tabla.Rows.Count
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn.Close()
        End Try
        Return resp
    End Function
    Private Sub llenar_DS_DEPOSITO()
        'CONECTO LA BASE 
        Dim DS_deposito As New DataSet
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("SELECT NDOC_003, NOMB_003 FROM M_PERS_003 WHERE DEPO_003 = 1 And F_BAJA_003 Is NULL ORDER BY NOMB_003", cnn2)
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.Fill(DS_deposito, "M_PERS_003")
        cnn2.Close()
        ComboBox3.DataSource = DS_deposito.Tables("M_PERS_003")
        ComboBox3.DisplayMember = "NOMB_003"
        ComboBox3.ValueMember = "NDOC_003"
        ComboBox3.Text = Nothing
    End Sub
    Private Function NOMBRE_DEPOSITO(ByVal str As String) As String
        Dim resp As String = "error"
        Dim cnn1 As SqlConnection = New SqlConnection(conexion)
        'Abrimos la conexion
        cnn1.Open()
        'Creamos el comando para crear la consulta
        Dim Comando As New SqlClient.SqlCommand("select APELL_003+' ' +NOMB_003 AS APEYNOM FROM M_PERS_003 WHERE NDOC_003 = @D1", cnn1)
        'Ejecutamos el commnad y le pasamos el parametro
        Comando.Parameters.Add(New SqlParameter("D1", str))
        Comando.ExecuteNonQuery()
        Dim Dusrs As SqlDataReader = Comando.ExecuteReader
        If Dusrs.Read.ToString Then
            resp = Dusrs.GetString(0)
        End If
        cnn1.Close()
        Return resp
    End Function
    Private Sub LLENAR_CAJON(MICAJON As String)
        Dim CNN As New SqlConnection(conexion)
        Try
            CNN.Open()
            Dim ADT As New SqlDataAdapter("SELECT NSERI_113, POLIZA_113 FROM T_MED_DEVO_113 WHERE CAJON_113=@D1 ORDER BY NSERI_113", CNN)
            ADT.SelectCommand.Parameters.Add(New SqlParameter("D1", MICAJON))
            ADT.Fill(tablacust)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            CNN.Close()
        End Try
    End Sub
    

    Private Sub PrintDocument1_PrintPage(sender As System.Object, e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        e.Graphics.DrawString("LOTE Nº: " + cajon.ToString, New Font("ARIAL", 48, FontStyle.Bold), Brushes.Black, 20, 70)
        e.Graphics.DrawString("FECHA LOTE: " + Date.Now.ToShortDateString, New Font("ARIAL", 30, FontStyle.Bold), Brushes.Black, 30, 135)
        e.Graphics.DrawString("FAMILIA: " + nomfamilia.ToString, New Font("ARIAL", 30, FontStyle.Bold), Brushes.Black, 30, 180)
        e.Graphics.DrawString("CANTIDAD: " + cantmed.ToString, New Font("ARIAL", 30, FontStyle.Bold), Brushes.Black, 30, 230)
        e.Graphics.DrawString(MAIN.obtenerbarras(cajon).ToString, New Font("Code 2 of 5 Interleaved", 48, FontStyle.Bold), Brushes.Black, 150, 270)
        '      e.Graphics.DrawString(MAIN.obtenerbarras(cajon).ToString, New Font("ARIAL", 35, FontStyle.Bold), Brushes.Black, 740, 170)

    End Sub


End Class