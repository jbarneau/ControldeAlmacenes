Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.Data.Sql
Public Class PALMA036
    Private MENSAJE As New Clase_mensaje
    Private DATA As New DataTable
    Private Clas_Metodos As New Clas_Almacen
    Private Clas_Medidor As New Clas_Medidor
    Private med_rettirar As New Clase_med_retirar
    Private _DEPOSITO As String
    Private cant As Integer = 0
    Private familia As Integer = 0
    Private DT_remito As New DataTable
    Private remito As Decimal
    Private fecha As Date
    Private resumen As New DataTable
    Private BODY As String
    Private TABLABODY As String
    Private CAJON As String
    Private DESFAMILA As String
    Private ADJUNTO As String
    Private ADJUNTOPDF As String
    Dim cant_medidores As Decimal
    Private CANTMED As String
    Dim fichero As String
    Private Sub PALMA036_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label3.Text = "USUARIO: " & _usr.Obt_Nombre_y_Apellido
        Label5.Text = "Fecha: " & DateTime.Now().ToShortDateString
        llenar_DS_Familia()
        llenar_DS_Proveedor()
        If _usr.Obt_Almacen <> "0" Then ' SI ES DISTINTO DE CERO PONGO EL COMBOBOS CON EL NOMBRE Y GUAROD EL DEPOSITO
            ComboBox3.DropDownStyle = ComboBoxStyle.DropDown
            _DEPOSITO = _usr.Obt_Almacen
            ComboBox3.Text = NOMBRE_DEPOSITO(_DEPOSITO)
            ComboBox1.Enabled = True
            ComboBox1.Focus()
            'Button1.Enabled = True
            ' Button3.Enabled = False

        Else
            ComboBox3.Enabled = True
            ComboBox3.Focus()
            llenar_DS_DEPOSITO()
            ComboBox3.Focus()
            ComboBox1.Enabled = False
            'Button1.Enabled = False
            'Button3.Enabled = False
        End If

    End Sub
    Private Sub Grabar_cab_Remito(nremi As Decimal, fec As Date, prov As String, cant As Decimal, flia As Integer)
        Dim cnn As New SqlConnection(conexion)
        Try
            cnn.Open()
            Dim adt As New SqlCommand("insert into C_REMITO_DEV_115 (NREMITO_115,CPROV_115,FECHA_115,CANT_115,FAMILIA_115) VALUES (@D1,@D2,@D3,@D4,@D5)", cnn)
            adt.Parameters.Add(New SqlParameter("D1", nremi))
            adt.Parameters.Add(New SqlParameter("D2", prov))
            adt.Parameters.Add(New SqlParameter("D3", fec))
            adt.Parameters.Add(New SqlParameter("D4", cant))
            adt.Parameters.Add(New SqlParameter("D5", flia))
            adt.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn.Close()
        End Try
    End Sub

    Private Sub llenar_DS_DEPOSITO()
        'CONECTO LA BASE 
        Dim DS_deposito As New DataSet
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("SELECT NDOC_003, NOMB_003 FROM M_PERS_003 WHERE DEPO_003 = 1 and F_BAJA_003 IS NULL ORDER BY NOMB_003", cnn2)
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
        Dim Comando As New SqlClient.SqlCommand("select NOMB_003 FROM M_PERS_003 WHERE NDOC_003 = @D1", cnn1)
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
    Private Sub LLENAR(ByVal DEPOSITO As String, ByVal flia As Integer)
        DATA.Clear()
        ListView1.Items.Clear()
        'Dim DATA As New DataTable
        Dim renglon As New ListViewItem
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("SELECT CAJON_113, COUNT(NSERI_113) AS CANTIDAD FROM T_MED_DEVO_113 WHERE (DEPOSI_113=@D1) AND (FAMILIA_113=@D2) AND (ESTADO_113 = 1)  GROUP BY CAJON_113", cnn2)
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.SelectCommand.Parameters.Add(New SqlParameter("D1", DEPOSITO))
        adaptadaor.SelectCommand.Parameters.Add(New SqlParameter("D2", flia))
        adaptadaor.Fill(DATA)
        cnn2.Close()
        For i = 0 To DATA.Rows.Count - 1
            'MessageBox.Show(DATA.Rows(i).Item(0).ToString + " : " + DATA.Rows(i).Item(1).ToString)
            renglon = New ListViewItem(DATA.Rows(i).Item(0).ToString)
            renglon.SubItems.Add(DATA.Rows(i).Item(1).ToString)
            ListView1.Items.Add(renglon)
        Next
    End Sub
    Public Function OBTPROVE(ByVal DNI As String) As String
        Dim RESP As String = ""
        Dim cnn1 As SqlConnection = New SqlConnection(conexion)
        'Abrimos la conexion
        cnn1.Open()
        'Creamos el comando para crear la consulta
        Dim Comando As SqlCommand = cnn1.CreateCommand
        Comando.CommandText = "SELECT RAZON_007 FROM M_PROV_MED_RET_007 where MCUIT_007= @USRS"
        'Ejecutamos el commnad y le pasamos el parametro
        Comando.Parameters.Add(New SqlParameter("USRS", DNI))
        Comando.ExecuteNonQuery()
        Dim Dusrs As SqlDataReader = Comando.ExecuteReader
        If Dusrs.Read Then
            RESP = Dusrs.GetValue(0)
        End If
        cnn1.Close()
        Return RESP
    End Function
    Private Sub ListView1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListView1.SelectedIndexChanged
        Dim CAN As Integer = 0
        For I = 0 To ListView1.Items.Count - 1
            If ListView1.Items(I).Selected = True Then
                'MessageBox.Show()
                CAN = CAN + CInt(ListView1.Items(I).SubItems(1).Text)
            End If
        Next
        TextBox1.Text = CAN
        If CAN <> 0 Then
            Button1.Enabled = True
        Else
            Button1.Enabled = False
        End If
    End Sub
    Private Sub ComboBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox3.SelectedIndexChanged
        If ComboBox3.ValueMember <> Nothing And ComboBox3.Text <> Nothing Then
            _DEPOSITO = ComboBox3.SelectedValue
            ComboBox1.Enabled = True
            TextBox1.Text = Nothing
            If ComboBox1.ValueMember <> Nothing And ComboBox1.Text <> Nothing Then
                familia = ComboBox1.SelectedValue
                ComboBox2.Enabled = True
                TextBox1.Text = 0
                LLENAR(_DEPOSITO, familia)
            End If
        End If
    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim medidor As String
        Dim nmov As Decimal
        Dim prov As String

        If ComboBox3.Text <> Nothing And CInt(TextBox1.Text) > 0 And ComboBox2.Text <> Nothing Then
            DT_remito.Rows.Clear()
            DT_remito.Clear()
            remito = med_rettirar.Obtener_Numero_Remito
            fecha = Date.Now.ToShortDateString
            prov = ComboBox2.SelectedValue
            For I = 0 To ListView1.Items.Count - 1
                If ListView1.Items(I).Selected = True Then
                    llenar_dt_remito(_DEPOSITO, ListView1.Items(I).Text)
                    'llenar_dt_remito2(remito)
                End If
            Next
            nmov = med_rettirar.Obtener_Numero_Mov
            For I = 0 To DT_remito.Rows.Count - 1
                medidor = DT_remito.Rows(I).Item(0).ToString
                med_rettirar.GRABAR_TRANS(medidor, nmov, fecha, _usr.Obt_Usr, 2)
                med_rettirar.actualizar_remito(remito, fecha, 2, medidor, _usr.Obt_Usr, prov)
            Next
            resumen.Clear()
            cant_medidores = DT_remito.Rows.Count
            llenar_DT_resumen(remito)
            'grabo la cantidad 
            If ComboBox1.SelectedValue = "6" Then
                Dim respuesta As MsgBoxResult = MessageBox.Show("DESEA AGREGAR ITEMS AL REMITO", "PREGUNTA", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If respuesta = MsgBoxResult.Yes Then
                    Dim PANTALLA As New CodMedidor
                    PANTALLA.ShowDialog()
                    If PANTALLA.LEER_RESPUESTA = True Then
                        Dim TABLA_AGREGAR As New DataTable
                        TABLA_AGREGAR = PANTALLA.LEER_TABLA
                        For I = 0 To TABLA_AGREGAR.Rows.Count - 1
                            cant_medidores = cant_medidores + CDec(TABLA_AGREGAR.Rows(I).Item(2))
                            Dim encontro = False
                            For k = 0 To resumen.Rows.Count - 1
                                If resumen.Rows(k).Item(0) = TABLA_AGREGAR.Rows(I).Item(1) And resumen.Rows(k).Item(1) = TABLA_AGREGAR.Rows(I).Item(0) Then
                                    resumen.Rows(k).Item(2) = CInt(resumen.Rows(k).Item(2)) + CInt(TABLA_AGREGAR.Rows(I).Item(2))
                                    encontro = True
                                End If
                            Next
                            If encontro = False Then
                                resumen.Rows.Add(TABLA_AGREGAR.Rows(I).Item(1), TABLA_AGREGAR.Rows(I).Item(0), TABLA_AGREGAR.Rows(I).Item(2), TABLA_AGREGAR.Rows(I).Item(3))
                            End If
                        Next
                    End If
                End If
            End If
            Grabar_cab_Remito(remito, fecha, prov, cant_medidores, ComboBox1.SelectedValue)
            For k = 0 To resumen.Rows.Count - 1
                Grabar_detalle_remito(remito, fecha, resumen.Rows(k).Item(1), resumen.Rows(k).Item(0), resumen.Rows(k).Item(2), "")
            Next
            PrintDocument1.Print()
            PrintDocument1.Print()

            ''''''''''''''''''''''''''''''''''''''''ANTES''''''''''''''''
            If ComboBox1.SelectedValue = 6 Then
                Dim pantalla As New OpenFileDialog
                pantalla.DereferenceLinks = True
                pantalla.Filter = "JPG files (*.JPG)|*.JPG"
                pantalla.FilterIndex = 1
                pantalla.Title = "Seleccione el archivo"
                pantalla.RestoreDirectory = False
                pantalla.ShowDialog()
                pantalla.AddExtension = False
                pantalla.Multiselect = False
                If pantalla.FileName = Nothing Then
                    ADJUNTO = "NO"
                Else
                    ADJUNTO = pantalla.FileName
                End If
            End If
            If ComboBox1.SelectedValue <> 1 Then
                Dim pantalla As New OpenFileDialog
                pantalla.DereferenceLinks = True
                pantalla.Filter = "PDF files (*.PDF)|*.PDF"
                pantalla.FilterIndex = 1
                pantalla.Title = "Seleccione el archivo"
                pantalla.RestoreDirectory = False
                pantalla.ShowDialog()
                pantalla.AddExtension = False
                pantalla.Multiselect = False
                If pantalla.FileName = Nothing Then
                    ADJUNTOPDF = "NO"
                Else
                    ADJUNTOPDF = pantalla.FileName
                End If
                armar_archivo(remito, fecha)
                ArmarBodyMail(ADJUNTO, ADJUNTOPDF)
                RemitoAlta(remito)
            End If
            borrar()
        Else
        End If
    End Sub

    Private Sub RemitoAlta(ByVal nrem As String)
        Dim cnn As SqlConnection = New SqlConnection(conexion)
        cnn.Open()
        Dim cmd As New SqlCommand("UPDATE T_MED_DEVO_113 SET REMCOM_113 = 0 WHERE NREMITO_113 = @P1", cnn)
        cmd.Parameters.AddWithValue("P1", nrem)
        cmd.ExecuteNonQuery()
        cnn.Close()
    End Sub

    'Private Sub llenar_dt_remito2(ByVal remito As String)
    '    DT_remito.Rows.Clear()
    '    DT_remito.Clear()
    '    Dim cnn2 As SqlConnection = New SqlConnection(conexion)
    '    'ABRO LA BASE
    '    cnn2.Open()
    '    'GENERO UN ADAPTADOR
    '    Dim adaptadaor As New SqlDataAdapter("SELECT NSERI_113, CMATE_113,CONTRATO_113 FROM T_MED_DEVO_113 WHERE (ESTADO_113 = 2) AND (NREMITO_113=@D2)", cnn2)
    '    'LLENO EL ADAPTADOR CON EL DATASET
    '    adaptadaor.SelectCommand.Parameters.Add(New SqlParameter("D1", remito))
    '    adaptadaor.Fill(DT_remito)
    '    cnn2.Close()
    'End Sub
    Private Sub llenar_dt_remito(ByVal deposito As String, ByVal cajon As String)
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        'Dim adaptadaor As New SqlDataAdapter("SELECT NSERI_113, CMATE_113,CONTRATO_113 FROM T_MED_DEVO_113 WHERE (DEPOSI_113=@D1) AND (ESTADO_113 = 2) AND (CAJON_113=@D2)", cnn2)
        Dim adaptadaor As New SqlDataAdapter("SELECT NSERI_113, CMATE_113,CONTRATO_113 FROM T_MED_DEVO_113 WHERE (DEPOSI_113=@D1) AND (CAJON_113=@D2) AND (NREMITO_113 IS NULL)", cnn2)
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.SelectCommand.Parameters.Add(New SqlParameter("D1", deposito))
        adaptadaor.SelectCommand.Parameters.Add(New SqlParameter("D2", cajon))
        adaptadaor.Fill(DT_remito)
        cnn2.Close()
    End Sub
    Private Sub armar_archivo(ByVal remito As Decimal, ByVal fecha As Date)
        Try
            If My.Computer.FileSystem.DirectoryExists("C:\ARCHIVO") = False Then
                My.Computer.FileSystem.CreateDirectory("C:\ARCHIVO")
            End If
            fichero = "C:\Archivo\Remito_Devolucion_" + remito.ToString.PadLeft(8, "0") + ".csv"
            Dim a As New System.IO.StreamWriter(fichero)
            a.WriteLine("N_SERIE;CAPACIDAD;DESCRIPCION;CONTRATO;REMITO;FECHA_REMITO")
            Dim NMEDIDOR As String
            Dim CAPADIDAD As String
            Dim CONTRATO As String
            For I = 0 To DT_remito.Rows.Count - 1
                NMEDIDOR = DT_remito.Rows(I).Item(0)
                CAPADIDAD = DT_remito.Rows(I).Item(1)
                CONTRATO = ContratoMed(DT_remito.Rows(I).Item(2))
                If NMEDIDOR = Nothing Then
                    NMEDIDOR = ""
                End If
                If CAPADIDAD = Nothing Then
                    CAPADIDAD = ""
                End If
                If CONTRATO = Nothing Then
                    CONTRATO = ""
                End If
                a.WriteLine(NMEDIDOR.PadLeft(8, "0") + ";" + CAPADIDAD.ToString() + ";" + DETATIPO(CAPADIDAD) + ";" + CONTRATO.ToString + ";" + remito.ToString + ";" + fecha.ToShortDateString)
            Next
            a.Close()
            MENSAJE.MADVE007(fichero)
        Catch ex As Exception
        End Try
    End Sub
    Private Sub llenar_DT_resumen(ByVal remito As Decimal)
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        Try
            cnn2.Open()
            'GENERO UN ADAPTADOR
            Dim adaptadaor As New SqlDataAdapter("SELECT CONTRATO_113, CMATE_113, COUNT(NSERI_113) AS CANT FROM T_MED_DEVO_113 WHERE (NREMITO_113 = @D1) GROUP BY CONTRATO_113, CMATE_113 ORDER BY  CONTRATO_113, CMATE_113", cnn2)
            'LLENO EL ADAPTADOR CON EL DATASET
            adaptadaor.SelectCommand.Parameters.Add(New SqlParameter("D1", remito))
            adaptadaor.Fill(resumen)
            If resumen.Columns.Contains("OBS") = False Then
                resumen.Columns.Add("OBS")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn2.Close()

        End Try
    End Sub

    Private Sub llenar_DT_resumenARCHIVO(ByVal remito As Decimal)
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        Try
            cnn2.Open()
            'GENERO UN ADAPTADOR
            'Dim adaptadaor As New SqlDataAdapter("SELECT CONTRATO_113, CMATE_113, COUNT(NSERI_113) AS CANT FROM T_MED_DEVO_113 WHERE (NREMITO_113 = @D1) GROUP BY CONTRATO_113, CMATE_113 ORDER BY  CONTRATO_113, CMATE_113", cnn2)
            Dim adaptadaor As New SqlDataAdapter("SELECT NSERI_113, CMATE_113, CONTRATO_113 FROM T_MED_DEVO_113 WHERE (NREMITO_113 = @D1) GROUP BY CONTRATO_113, CMATE_113, NSERI_113 ORDER BY CONTRATO_113, CMATE_113", cnn2)
            'LLENO EL ADAPTADOR CON EL DATASET
            adaptadaor.SelectCommand.Parameters.Add(New SqlParameter("D1", remito))
            adaptadaor.Fill(resumen)
            If resumen.Columns.Contains("OBS") = False Then
                resumen.Columns.Add("OBS")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn2.Close()

        End Try
    End Sub
    Private Sub Grabar_detalle_remito(nremito As Decimal, fec As Date, COD As String, CONTR As String, CANT As Decimal, obs As String)
        Dim cnn As New SqlConnection(conexion)
        Try
            cnn.Open()
            Dim adt As New SqlCommand("insert into D_REMITO_DEV_116 (NREMITO_116,FECHA_116,CODMATE_116,CONTRA_116, CANT_116, OBS_116) VALUES (@D1,@D2,@D3,@D4,@D5,@D6)", cnn)
            adt.Parameters.Add(New SqlParameter("D1", nremito))
            adt.Parameters.Add(New SqlParameter("D2", fec))
            adt.Parameters.Add(New SqlParameter("D3", COD))
            adt.Parameters.Add(New SqlParameter("D4", CONTR))
            adt.Parameters.Add(New SqlParameter("D5", CANT))
            adt.Parameters.Add(New SqlParameter("D6", obs))
            adt.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn.Close()
        End Try
    End Sub

    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Dim R1_T1 As String = ""
        Dim R1_T2 As String = ""
        Dim R2_T1 As String = ""
        Dim R2_T2 As String = ""
        Dim R3_T1 As String = ""
        Dim R3_T2 As String = ""

        Dim n_REMITO As String
        If _usr.Obt_Almacen = 0 Then
            n_REMITO = "0100" + "-" + remito.ToString.PadLeft(8, "0")
        Else
            n_REMITO = _usr.Obt_Almacen.ToString.PadLeft(4, "0") + "-" + remito.ToString.PadLeft(8, "0")
        End If        'DEFINO LAS VARIABLES
        R1_T1 = "DEVOLUCION DE MEDIDORES"
        R1_T2 = "PROVEEDOR: " + ComboBox2.Text + "(" + ComboBox2.SelectedValue.ToString + ")"
        R2_T1 = "DEPOSITO: " + ComboBox3.Text
        R2_T2 = "TIPO DE MEDIDORE:" + ComboBox1.Text
        R3_T1 = "CONFECCIONO: " + _usr.Obt_Nombre_y_Apellido
        R3_T2 = "TOTAL DE MEDIDORES: " + cant_medidores.ToString

        'DEFINO LA LINEA DEL REMITO Y EL SALTO
        Dim LINEA As Integer = 356
        Dim SALTO As Integer = 24
        'IMAGEN ######################################
        e.Graphics.DrawImage(MAIN.REMITO_IMAGEN, 0, 0, 800, 1140)
        'ESCRIBO EL REMITO Y LA FECHA
        e.Graphics.DrawString(n_REMITO.ToString, New Font("ARIAL", 16, FontStyle.Bold), Brushes.Black, 435, 73)
        e.Graphics.DrawString(fecha.ToString, New Font("ARIAL", 12, FontStyle.Regular), Brushes.Black, 435, 101)
        'ESCRIBO LOS RENGLONES
        e.Graphics.DrawString(R1_T1.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 15, 215)
        e.Graphics.DrawString(R2_T1.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 15, 247)
        e.Graphics.DrawString(R3_T1.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 15, 280)

        e.Graphics.DrawString(R1_T2.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 450, 215)
        e.Graphics.DrawString(R2_T2.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 450, 247)
        e.Graphics.DrawString(R3_T2.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 450, 280)
        Dim CONTRATO As String
        'ESCRIBO EL ENCABEZADO DEL DETALLE
        e.Graphics.DrawString("CAPACIDAD", New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 15, 325)
        e.Graphics.DrawString("CONTRATO", New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 350, 325)
        e.Graphics.DrawString("CANTIDAD", New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 690, 325)
        'RECORRO EL DATA
        For I = 0 To resumen.Rows.Count - 1
            CONTRATO = ContratoMed(resumen.Rows(I).Item(0))
            e.Graphics.DrawString(resumen.Rows(I).Item(1).ToString + "-" + DETATIPO(resumen.Rows(I).Item(1).ToString), New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 15, LINEA)
            e.Graphics.DrawString(CONTRATO.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 400, LINEA)
            e.Graphics.DrawString(resumen.Rows(I).Item(2).ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 710, LINEA)
            LINEA += SALTO
        Next


    End Sub
    Private Sub ArmarBodyMail(ByVal archivo As String, ByVal PDF As String)
        Dim n_REMITO As String
        If _usr.Obt_Almacen = 0 Then
            n_REMITO = "0100" + "-" + remito.ToString.PadLeft(8, "0")
        Else
            n_REMITO = _usr.Obt_Almacen.ToString.PadLeft(4, "0") + "-" + remito.ToString.PadLeft(8, "0")
        End If
        BODY = "<body><table width='897' height='1017' border='0'> <tr><td height='103' bordercolor='#F0F0F0'><img src='http://www.exgadetsa.com.ar/png/exgadet sin DS.png' width='143' height='111'/></td><td colspan='2'><div align='center' class='Estilo1'>DEVOLUCION DE MEDIDORES EXGADET</div></td></tr><tr><td width='112' height='31' bordercolor='#F0F0F0'>&nbsp;</td><td colspan='2'><div align='right'>REMITO N&ordm; - " + n_REMITO + "</div></td></tr> <tr><td height='31' colspan='2' bordercolor='#F0F0F0'><div align='right' class='Estilo4'>PROVEEDOR</div></td> <td width='637' bordercolor='#F0F0F0'><span class='Estilo4'><blockquote>" + ComboBox2.Text + "(" + ComboBox2.SelectedValue.ToString + ")" + "</blockquote></span></td></tr><tr><td height='31' colspan='2' bordercolor='#F0F0F0'><div align='right' class='Estilo4'>TIPO</div></td><td width='637' bordercolor='#F0F0F0'><span class='Estilo4'><blockquote>" + ComboBox1.Text + "</blockquote></span></td></tr><tr><td height='31' colspan='2' bordercolor='#F0F0F0'><div align='right' class='Estilo4'>TOTAL</div></td><td width='637' bordercolor='#F0F0F0'><span class='Estilo4'><blockquote>" + DT_remito.Rows.Count.ToString + "</blockquote></span></td></tr><tr><td height='776' colspan='3' valign='top'><div align='center'> <table width='886' border='1'><tr><td width='506'><div align='center'>CAPACIDAD</div></td><td width='238'><div align='center'>CONTRATO</div></td><td width='120'><div align='center'>CANTIDAD</div></td></tr>"
        Dim contrato As String
        For I = 0 To resumen.Rows.Count - 1
            contrato = ContratoMed(resumen.Rows(I).Item(0))
            TABLABODY = TABLABODY + "<tr><td>" + resumen.Rows(I).Item(1).ToString + "-" + DETATIPO(resumen.Rows(I).Item(1).ToString) + "</td><td><div align='center'>" + contrato + "</div></td><td><div align='center'>" + resumen.Rows(I).Item(2).ToString + "</div></td></tr>"
        Next
        BODY = BODY + TABLABODY + "</table></div></td></tr></table></body>"
        ENVIAR_MENSAJE(Direcciones_mail(1).ToString, n_REMITO.ToString, BODY.ToString, archivo, PDF)
    End Sub
    Private Sub borrar()
        DT_remito.Clear()
        resumen.Clear()
        ListView1.Items.Clear()
        ComboBox2.Enabled = False
        ComboBox2.Text = Nothing
        ComboBox1.Text = Nothing
        If _usr.Obt_Almacen <> "0" Then ' SI ES DISTINTO DE CERO PONGO EL COMBOBOS CON EL NOMBRE Y GUAROD EL DEPOSITO
            TextBox1.Text = 0
        Else
            ComboBox3.Text = Nothing
            ComboBox3.Focus()
            TextBox1.Text = 0
        
        End If
    End Sub
    Private Function DETATIPO(ByVal codmat As String) As String
        Dim resp As String = ""
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("SELECT DESC_002 FROM M_MATE_002 WHERE CMATE_002 = @D1", con1)
        'creo el lector de parametros
        comando1.Parameters.Add(New SqlParameter("D1", codmat))
        comando1.ExecuteNonQuery()
        'genero un lector
        Dim lector1 As SqlDataReader = comando1.ExecuteReader
        If lector1.Read.ToString Then
            resp = lector1.GetValue(0)
        End If
        con1.Close()
        Return resp
    End Function
    Private Function QUEFAMILIA(COD As String) As String
        Dim RESP As String = ""
        Dim cnn As New SqlConnection(conexion)
        Try
            cnn.Open()
            Dim adt As New SqlCommand("select C_PARA_802,DESC_802 FROM DET_PARAMETRO_802 WHERE C_TABLA_802 = 15 and F_BAJA_802 IS NULL and C_PARA_802 = @D1", cnn)
            adt.Parameters.Add(New SqlParameter("D1", COD))
            Dim lector As SqlDataReader = adt.ExecuteReader
            If lector.Read Then
                RESP = lector.GetValue(1)
            End If
        Catch ex As Exception

        End Try
        Return RESP
    End Function

    Private Sub llenar_DS_Familia()
        'CONECTO LA BASE 
        Dim DS_deposito As New DataSet
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("SELECT C_PARA_802,DESC_802 FROM DET_PARAMETRO_802 WHERE (C_TABLA_802 = 15) and (F_BAJA_802 IS NULL) and (C_PARA_802 <>0) and (C_PARA_802 <>9)  ORDER BY DESC_802", cnn2)
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.Fill(DS_deposito, " DET_PARAMETRO_802")
        cnn2.Close()
        ComboBox1.DataSource = DS_deposito.Tables(" DET_PARAMETRO_802")
        ComboBox1.DisplayMember = "DESC_802"
        ComboBox1.ValueMember = "C_PARA_802"
        ComboBox1.Text = Nothing
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.ValueMember <> Nothing And ComboBox1.Text <> Nothing Then
            familia = ComboBox1.SelectedValue
            ComboBox2.Enabled = True
            TextBox1.Text = 0
            LLENAR(_DEPOSITO, familia)
        End If
    End Sub

    Private Sub llenar_DS_Proveedor()
        'CONECTO LA BASE 
        Dim DS_deposito As New DataSet
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("SELECT MCUIT_007, RAZON_007 FROM M_PROV_MED_RET_007 order by RAZON_007", cnn2)
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.Fill(DS_deposito, "M_PROV_MED_RET_007")
        cnn2.Close()
        ComboBox2.DataSource = DS_deposito.Tables("M_PROV_MED_RET_007")
        ComboBox2.DisplayMember = "RAZON_007"
        ComboBox2.ValueMember = "MCUIT_007"
        ComboBox2.Text = Nothing
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        remito = CDec(InputBox("ingrese el numero"))
        Dim dr As New DialogResult
        dr = PrintDialog1.ShowDialog()
        If dr = DialogResult.OK Then
            PrintDocument2.PrinterSettings = PrintDialog1.PrinterSettings
            PrintDocument2.Print()
            MessageBox.Show("SE HA IMPRIMIDO CORRECTAMENTE", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub PrintDocument2_PrintPage(sender As System.Object, e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument2.PrintPage
        Dim fecha As Date
        Dim deposito As String = ""
        Dim texto_familia As String = ""
        Dim confecciono As String = ""
        Dim cant As Decimal = 0
        Dim proveedor As String = ""
        Dim tabla As New DataTable
        Dim cnn As New SqlConnection(conexion)
        Dim R1_T1 As String = ""
        Dim R1_T2 As String = ""
        Dim R2_T1 As String = ""
        Dim R2_T2 As String = ""
        Dim R3_T1 As String = ""
        Dim R3_T2 As String = ""

        Try
            cnn.Open()
            Dim adt As New SqlCommand("select CPROV_115, FECHA_115, CANT_115, FAMILIA_115 FROM C_REMITO_DEV_115 WHERE NREMITO_115=@D1", cnn)
            adt.Parameters.Add(New SqlParameter("D1", remito))
            Dim lecor As SqlDataReader = adt.ExecuteReader
            If lecor.Read Then
                fecha = lecor.GetDateTime(1)
                cant = lecor.GetValue(2)
                deposito = "GENERAL THAMES"
                confecciono = _usr.Obt_Nombre_y_Apellido
                texto_familia = QUEFAMILIA(lecor.GetValue(3))
                proveedor = OBTPROVE(lecor.GetValue(0)) + "(" + lecor.GetValue(0) + ")"
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn.Close()
        End Try
        Try
            cnn.Open()
            Dim adt2 As New SqlDataAdapter("select CODMATE_116,CONTRA_116,CANT_116 FROM D_REMITO_DEV_116 WHERE NREMITO_116=@D1", cnn)
            adt2.SelectCommand.Parameters.Add(New SqlParameter("D1", remito))
            adt2.Fill(tabla)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn.Close()
        End Try
        Dim n_REMITO As String
        If _usr.Obt_Almacen = 0 Then
            n_REMITO = "0100" + "-" + remito.ToString.PadLeft(8, "0")
        Else
            n_REMITO = _usr.Obt_Almacen.ToString.PadLeft(4, "0") + "-" + remito.ToString.PadLeft(8, "0")
        End If        'DEFINO LAS VARIABLES
        R1_T1 = "DEVOLUCION DE MEDIDORES"
        R1_T2 = "PROVEEDOR: " + proveedor
        R2_T1 = "DEPOSITO: " + deposito
        R2_T2 = "TIPO DE MEDIDORE:" + texto_familia
        R3_T1 = "CONFECCIONO: " + _usr.Obt_Nombre_y_Apellido
        R3_T2 = "TOTAL DE MEDIDORES: " + cant.ToString

        'DEFINO LA LINEA DEL REMITO Y EL SALTO
        Dim LINEA As Integer = 356
        Dim SALTO As Integer = 24
        'IMAGEN ######################################
        e.Graphics.DrawImage(MAIN.REMITO_IMAGEN, 0, 0, 800, 1140)
        'ESCRIBO EL REMITO Y LA FECHA
        e.Graphics.DrawString(n_REMITO.ToString, New Font("ARIAL", 16, FontStyle.Bold), Brushes.Black, 435, 73)
        e.Graphics.DrawString(fecha.ToString, New Font("ARIAL", 12, FontStyle.Regular), Brushes.Black, 435, 101)
        'ESCRIBO LOS RENGLONES
        e.Graphics.DrawString(R1_T1.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 15, 215)
        e.Graphics.DrawString(R2_T1.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 15, 247)
        e.Graphics.DrawString(R3_T1.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 15, 280)

        e.Graphics.DrawString(R1_T2.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 450, 215)
        e.Graphics.DrawString(R2_T2.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 450, 247)
        e.Graphics.DrawString(R3_T2.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 450, 280)
        Dim CONTRATO As String
        'ESCRIBO EL ENCABEZADO DEL DETALLE
        e.Graphics.DrawString("CAPACIDAD", New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 15, 325)
        e.Graphics.DrawString("CONTRATO", New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 350, 325)
        e.Graphics.DrawString("CANTIDAD", New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 690, 325)
        'RECORRO EL DATA
        Dim cont As Integer = tabla.Rows.Count - 1
        For I = 0 To cont
            CONTRATO = ContratoMed(tabla.Rows(I).Item(1))
            e.Graphics.DrawString(tabla.Rows(I).Item(0).ToString + "-" + DETATIPO(tabla.Rows(I).Item(0).ToString), New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 15, LINEA)
            e.Graphics.DrawString(CONTRATO.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 400, LINEA)
            e.Graphics.DrawString(tabla.Rows(I).Item(2).ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 710, LINEA)
            LINEA += SALTO
        Next

    End Sub
    Public Sub ENVIAR_MENSAJE(ByVal DIRECCION As String, ByVal NREMITO As String, ByVal _BODY As String, ByVal Archivo As String, ByVal PDF As String)

        Dim _mensage As New System.Net.Mail.MailMessage
        Dim _Smtp As New System.Net.Mail.SmtpClient
        _Smtp.Credentials = New System.Net.NetworkCredential(MAIN.mail, MAIN.passmail)
        _Smtp.Host = MAIN.smtpmail
        _Smtp.Port = MAIN.puertomail
        _Smtp.EnableSsl = False
        'configuracion del mensaje
        _mensage.To.Add(DIRECCION.ToString)
        _mensage.From = New System.Net.Mail.MailAddress("almacensis@exgadetsa.com.ar", "REMITO MEDIDORES DEVOLUCION N" + NREMITO, System.Text.Encoding.UTF8)
        _mensage.Subject = "REMITO DE ENTREGA DE MEDIDORES N" + NREMITO
        _mensage.SubjectEncoding = System.Text.Encoding.UTF8
        _mensage.Body = _BODY.ToString
        If Archivo <> "NO" Then
            If Archivo <> Nothing Then
                _mensage.Attachments.Add(New System.Net.Mail.Attachment(Archivo))
            End If
        End If
        If PDF <> "NO" Then
            If PDF <> Nothing Then
                _mensage.Attachments.Add(New System.Net.Mail.Attachment(PDF))
            End If
        End If
        If fichero <> Nothing Then
            _mensage.Attachments.Add(New System.Net.Mail.Attachment(fichero))
        End If
        _mensage.BodyEncoding = System.Text.Encoding.UTF8
        _mensage.Priority = System.Net.Mail.MailPriority.High
        _mensage.IsBodyHtml = True
        'enviar
        Try
            _Smtp.Send(_mensage)
        Catch ex As System.Net.Mail.SmtpException
            MessageBox.Show("NO SE PUDO ENVIAR EL MENSAJE ERROR")
        End Try
    End Sub

    Private Sub EXPORTARCSVToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles EXPORTARCSVToolStripMenuItem.Click
        For I = 0 To ListView1.Items.Count - 1
            If ListView1.Items(I).Selected = True Then
                CAJON = ListView1.Items(I).Text
                Dim cnn As New SqlConnection(conexion)
                Dim tabla As New DataTable
                Try
                    cnn.Open()
                    Dim adt As New SqlDataAdapter("SELECT NSERI_113,CMATE_113, FRETIRO_113, POLIZA_113 FROM T_MED_DEVO_113 WHERE CAJON_113 = @D1 ORDER BY NSERI_113 ", cnn)
                    adt.SelectCommand.Parameters.Add(New SqlParameter("D1", CAJON))
                    adt.Fill(tabla)
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                Finally
                    cnn.Close()
                End Try
                If My.Computer.FileSystem.DirectoryExists("C:\ARCHIVO") = False Then
                    My.Computer.FileSystem.CreateDirectory("C:\ARCHIVO")
                End If
                Dim fichero As String = "C:\Archivo\CAJON_" + CAJON.ToString + ".csv"
                Dim a As New System.IO.StreamWriter(fichero)
                a.WriteLine("N_SERIE;CAPACIDAD;FECH_ALTA;POLIZA")
                Dim NMEDIDOR As String
                Dim fecha As String
                Dim CAPADIDAD As String
                Dim ESTADO As String
                For J = 0 To tabla.Rows.Count - 1
                    NMEDIDOR = tabla.Rows(J).Item(0)
                    CAPADIDAD = tabla.Rows(J).Item(1)
                    fecha = CDate(tabla.Rows(J).Item(2)).ToShortDateString
                    ESTADO = tabla.Rows(J).Item(3)
                    a.WriteLine(NMEDIDOR.PadLeft(8, "0") + ";" + CAPADIDAD + "-" + DETATIPO(CAPADIDAD) + ";" + fecha + ";" + ESTADO)
                Next
                a.Close()
                MENSAJE.MADVE002(fichero)
            End If
        Next
    End Sub

    Private Sub REIMPRIMIRLISTADOToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles REIMPRIMIRLISTADOToolStripMenuItem.Click
        For I = 0 To ListView1.Items.Count - 1
            If ListView1.Items(I).Selected = True Then
                CAJON = ListView1.Items(I).Text
                CANTMED = ListView1.Items(I).SubItems(1).Text
                DESFAMILA = ComboBox1.Text
                PrintDocument3.Print()
            End If
        Next
    End Sub

    Private Sub PrintDocument3_PrintPage(sender As System.Object, e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument3.PrintPage
        e.Graphics.DrawString("LOTE Nº: " + CAJON.ToString, New Font("ARIAL", 48, FontStyle.Bold), Brushes.Black, 20, 70)
        e.Graphics.DrawString("FECHA LOTE: " + Date.Now.ToShortDateString, New Font("ARIAL", 30, FontStyle.Bold), Brushes.Black, 30, 135)
        e.Graphics.DrawString("FAMILIA: " + DESFAMILA.ToString, New Font("ARIAL", 30, FontStyle.Bold), Brushes.Black, 30, 180)
        e.Graphics.DrawString("CANTIDAD: " + CANTMED.ToString, New Font("ARIAL", 30, FontStyle.Bold), Brushes.Black, 30, 230)
        e.Graphics.DrawString(MAIN.obtenerbarras(CAJON).ToString, New Font("Code 2 of 5 Interleaved", 48, FontStyle.Bold), Brushes.Black, 150, 270)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim pantallita As New PALMA036BIS(ComboBox1.SelectedValue, ComboBox3.SelectedValue, ComboBox2.SelectedValue)
        pantallita.ShowDialog()
    End Sub

End Class