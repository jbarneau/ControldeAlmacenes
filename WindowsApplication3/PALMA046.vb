Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.Data.Sql
Public Class PALMA046
    Private MENSAJE As New Clase_mensaje
    Private Clas_Metodos As New Clas_Almacen
    Private Clas_Medidor As New Clas_Medidor
    Private med_rettirar As New Clase_med_retirar
    Private DT_medidores As New DataTable
    Private _DEPOSITO As String
    Private cantmed As Integer
    Private nomfamilia As String
    Private UserCajon As String
    Private estado As Integer
    Private fremito As String
    Private nremitoD As String
    Private UserRemito As String
    Private familia As Integer
    Private cajon As String
    Private fecha As Date
    Private tablacust As New DataTable
    Private medidor As Decimal
    Private listaNoEncontrados As New List(Of String)
    Private listaNoEncontradosESTADOS As New List(Of String)
    Private listaOC As New List(Of String)
    Private cont As Integer = 0
    Private Archivo As String
    Dim a As String = ""
    Dim b As String = ""
    Dim listaMedsAlCajon As New List(Of Clase_med_retirar)
    Private Sub PALMA046_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Label3.Text = "USUARIO: " & _usr.Obt_Nombre_y_Apellido
        Label5.Text = "Fecha: " & DateTime.Now().ToShortDateString
        llenar_DS_OPERARIO()
        If _usr.Obt_Almacen <> "0" Then ' SI ES DISTINTO DE CERO PONGO EL COMBOBOS CON EL NOMBRE Y GUAROD EL DEPOSITO
            ComboBox3.DropDownStyle = ComboBoxStyle.DropDown
            _DEPOSITO = _usr.Obt_Almacen
            ComboBox3.Text = NOMBRE_DEPOSITO(_DEPOSITO)
            CBEQUIPO.Enabled = True
        Else
            ComboBox3.Enabled = True
            ComboBox3.Focus()
            llenar_DS_DEPOSITO()
            ComboBox3.Focus()
            CBEQUIPO.Enabled = False
        End If
    End Sub

#Region "FUNCIONES DE INICIO"
    Private Sub llenar_DS_OPERARIO()
        'CONECTO LA BASE 
        Dim DS_deposito As New DataTable
        DS_deposito.Columns.Add("CODIGO")
        DS_deposito.Columns.Add("DESC")
        DS_deposito.Rows.Add(0, "--TODOS--")
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlCommand("SELECT OPERARIO, APELL_003 + ' ' + NOMB_003 AS Expr1 FROM TEMP_MED_UBICAR INNER JOIN M_PERS_003 ON OPERARIO = NDOC_003 GROUP BY OPERARIO, APELL_003 + ' ' + NOMB_003 ORDER BY Expr1", cnn2)
        'LLENO EL ADAPTADOR CON EL DATASET
        Dim LECTOR As SqlDataReader = adaptadaor.ExecuteReader
        Do While LECTOR.Read
            DS_deposito.Rows.Add(LECTOR.GetValue(0), LECTOR.GetValue(1))
        Loop
        cnn2.Close()
        CBEQUIPO.DataSource = DS_deposito
        CBEQUIPO.DisplayMember = "DESC"
        CBEQUIPO.ValueMember = "CODIGO"
        CBEQUIPO.Text = Nothing
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


    Private Sub llenar_table_base(ByVal DEPO As String)
        DT_medidores.Clear()
        ListBox1.Items.Clear()
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        Try
            cnn2.Open()
            Dim adaptadaor As New SqlDataAdapter("SELECT NSERI_113, CMATE_113, POLIZA_113, OT_113, CONTRATO_113, FCARGO_113, OPERA_113 FROM T_MED_DEVO_113 WHERE ESTADO_113=0 AND FAMILIA_113=0 AND OPERA_113=@D1 ORDER BY NSERI_113", cnn2)
            adaptadaor.SelectCommand.Parameters.Add(New SqlParameter("D1", DEPO))
            adaptadaor.Fill(DT_medidores)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn2.Close()
        End Try
        For i = 0 To DT_medidores.Rows.Count - 1
            ListBox1.Items.Add(DT_medidores.Rows(i).Item(0).ToString.PadLeft(8, "0"))
        Next
        TextBox3.Text = ListBox1.Items.Count
    End Sub
    Private Sub llenar_table_base()
        DT_medidores.Clear()
        ListBox1.Items.Clear()
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        Try
            cnn2.Open()
            Dim adaptadaor As New SqlDataAdapter("SELECT NSERI_113, CMATE_113, POLIZA_113, OT_113, CONTRATO_113, FCARGO_113, OPERA_113 FROM T_MED_DEVO_113 WHERE ESTADO_113=0 AND FAMILIA_113=0 ORDER BY NSERI_113", cnn2)
            adaptadaor.Fill(DT_medidores)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn2.Close()
        End Try
        For i = 0 To DT_medidores.Rows.Count - 1
            ListBox1.Items.Add(DT_medidores.Rows(i).Item(0).ToString.PadLeft(8, "0"))
        Next
        TextBox3.Text = ListBox1.Items.Count
    End Sub
#End Region

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs)
        Dim reg As DataRow()
        Dim filtro As String = "NMEDIDOR=" + CDec(TextBox1.Text).ToString
        reg = DT_medidores.Select(filtro)
        MessageBox.Show(reg(0)(2).ToString)
    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ComboBox3.SelectedIndexChanged
        If ComboBox3.ValueMember <> Nothing And IsNothing(ComboBox3.Text) = False Then
            CBEQUIPO.Enabled = True
        End If
    End Sub

    Private Sub CBEQUIPO_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles CBEQUIPO.SelectedIndexChanged
        If CBEQUIPO.ValueMember <> Nothing And CBEQUIPO.Text <> Nothing Then
            If CBEQUIPO.SelectedValue = "0" Then
                llenar_table_base()
            Else
                llenar_table_base(CBEQUIPO.SelectedValue)
            End If

        End If
    End Sub
    Private Sub TextBox1_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        Dim nmed As String = TextBox1.Text.PadLeft(8, "0")
        If Asc(e.KeyChar) = 13 Then
            If IsNothing(TextBox1.Text) Then
                MessageBox.Show("NO SE PERMITEN CAMPOS VACIOS", "VERIFICAR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                If val_medi_data(nmed) = True Then
                    'NUEVO
                    ListBox2.Items.Add(CStr(nmed).PadLeft(8, "0"))
                    For i = 0 To ListBox2.Items.Count - 1
                        For g = ListBox1.Items.Count - 1 To 0 Step -1
                            If ListBox1.Items(g) = ListBox2.Items(i) Then
                                ListBox1.Items.RemoveAt(g)
                            End If
                        Next
                    Next
                    contadores()
                Else
                    If val_medi_data_encontrado(nmed) = True Then
                        MessageBox.Show("EL MEDIDOR YA FUE PASADO" + vbCrLf + "SE ENCUENTRA EN EL RECUADRO DE ENCONTRADOS", "YA SE LO PASASTE", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Else
                        If buscar_medidor(nmed) = True Then
                            MessageBox.Show("EL MEDIDOR Nº: " + nmed + vbCrLf + "SE ENCUENTRA EN EL CAJON: " + cajon + vbCrLf + "EATA EN EL REMITO Nº: " + nremitoD, "MEDIDOR YA ENCONTRADO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                        Else
                            If verificarOCmed(nmed) = True Then
                                MessageBox.Show("EL MEDIDOR Nº: " + nmed + vbCrLf + "####  PROVIENE DE UNA ÒRDEN DE CIERRE (OC)  ####  INFORMAR URGENTE!!!!!!.", "MEDIDOR DE OC", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                listaOC.Add(nmed)
                            Else
                                MessageBox.Show("VERIFIQUE LOS DATOS, EL MEDIDOR NO SE ENCUENTRA EN EL RECUADRO", "VERIFICAR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                a = TextBox1.Text
                                If a = b Then
                                    cont += 1
                                    If cont < 2 Then

                                    Else
                                        listaNoEncontrados.Add(nmed)
                                        Dim pantalla As New PALMA046BIS(nmed)
                                        pantalla.ShowDialog()
                                        cont = 0
                                    End If
                                Else
                                    b = TextBox1.Text
                                End If

                            End If

                        End If
                    End If
                End If
            End If
            TextBox1.Text = Nothing
            TextBox1.Focus()
        End If
    End Sub
    Private Function val_medi_data(ByVal med As String) As Boolean
        med = med.PadLeft(8, "0")
        Dim resp As Boolean = False
        For i = 0 To ListBox1.Items.Count - 1
            If med = ListBox1.Items(i) Then
                resp = True
            End If
        Next
        Return resp
    End Function
    Private Function val_medi_data_encontrado(ByVal med As String) As Boolean
        med = med.PadLeft(8, "0")
        Dim resp As Boolean = False
        For i = 0 To ListBox2.Items.Count - 1
            If med = ListBox2.Items(i) Then
                resp = True
            End If
        Next
        Return resp
    End Function
    Private Sub contadores()
        TextBox2.Text = ListBox2.Items.Count
        TextBox3.Text = ListBox1.Items.Count
        If ListBox2.Items.Count <> 0 Then
            Button5.Enabled = True
            Button6.Enabled = True
        Else
            Button5.Enabled = False
            Button6.Enabled = False

        End If
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        For I = 0 To ListBox2.Items.Count - 1
            If ListBox2.GetSelected(I) = True Then
                ListBox1.Items.Add(ListBox2.Items(I))
            End If
        Next
        For i = 0 To ListBox1.Items.Count - 1
            For g = ListBox2.Items.Count - 1 To 0 Step -1
                If ListBox2.Items(g) = ListBox1.Items(i) Then
                    ListBox2.Items.RemoveAt(g)
                End If
            Next
        Next
        ListBox1.Sorted = True
        ListBox2.Sorted = True
        contadores()
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Dim contador45 As Integer = 0
        Dim pantalla As New PALMA037()
        fecha = Date.Now
        Dim nmov As Decimal
        If ComboBox3.Text <> Nothing Then
            If _usr.Obt_Almacen = 0 Then
                _DEPOSITO = ComboBox3.SelectedValue
            End If
            pantalla.TOMAR(_DEPOSITO)
            pantalla.ShowDialog()
            If pantalla.LEERRESPUESTA = True Then
                nmov = med_rettirar.Obtener_Numero_Mov
                familia = pantalla.LEERFAMILIA
                nomfamilia = pantalla.LEERNOMBREFAMILIA
                If pantalla.LEERCAJON = "--NUEVO CAJON--" Then
                    cajon = Date.Now.Year.ToString + med_rettirar.Obtener_Numero_lote.ToString.PadLeft(8, "0")
                Else
                    cajon = pantalla.LEERCAJON
                End If
                If familia = 9 Then
                    For i = 0 To ListBox2.Items.Count - 1
                        medidor = ListBox2.Items(i)
                        med_rettirar.MODIFICAR_MEDIDOR_CUSTODIA(_DEPOSITO, cajon, familia, _usr.Obt_Usr, fecha, medidor, 9)
                        med_rettirar.GRABAR_TRANS(medidor, nmov, fecha, _usr.Obt_Usr, 1)
                    Next
                    cantmed = ContMedidores(cajon)
                    LLENAR_CAJON(cajon)
                    Dim RESPUESTA As MsgBoxResult = MessageBox.Show("DESEA IMPRIMIR EL PAPEL DEL LOTE: " + cajon.ToString, "CONSULTA", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    If RESPUESTA = MsgBoxResult.Yes Then
                        PrintDocument2.DefaultPageSettings.Landscape = True
                        PrintDocument2.Print()
                    End If
                    MENSAJE.MADVE001()
                Else
                    For i = 0 To ListBox2.Items.Count - 1
                        Dim item As New Clase_med_retirar
                        medidor = ListBox2.Items(i)
                        Dim codsap As String = buscar_sap(medidor)
                        med_rettirar.MODIFICAR_MEDIDOR(_DEPOSITO, cajon, familia, _usr.Obt_Usr, medidor, 1)
                        med_rettirar.GRABAR_TRANS(medidor, nmov, fecha, _usr.Obt_Usr, 1)

                    Next
                    cantmed = ContMedidores(cajon)
                    Dim RESPUESTA As MsgBoxResult = MessageBox.Show("DESEA IMPRIMIR EL PAPEL DEL LOTE: " + cajon.ToString, "CONSULTA", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    If RESPUESTA = MsgBoxResult.Yes Then
                        PrintDocument1.Print()
                    End If

                    If listaMedsAlCajon.Count > 0 Then
                        Dim p As New PALMA046BIS_3(listaMedsAlCajon)
                        p.ShowDialog()
                        p.Dispose()
                        EnviarPorMail(listaMedsAlCajon)
                        MENSAJE.MADVE001()
                        listaMedsAlCajon.Clear()
                    End If
                End If
                'borro todo
                borrar()
            End If
        Else
            MENSAJE.MERRO006()
            ComboBox3.Focus()
        End If
    End Sub
    Private Sub LLENAR_CAJON(ByVal MICAJON As String)
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
    Private Sub borrar()
        DT_medidores.Rows.Clear()
        ListBox2.Items.Clear()
        ListBox1.Items.Clear()
        Button6.Enabled = False
        CBEQUIPO.Text = Nothing
        TextBox2.Text = Nothing
        TextBox3.Text = Nothing
        If _usr.Obt_Almacen <> "0" Then ' SI ES DISTINTO DE CERO PONGO EL COMBOBOS CON EL NOMBRE Y GUAROD EL DEPOSITO
            ComboBox3.DropDownStyle = ComboBoxStyle.DropDown
            _DEPOSITO = _usr.Obt_Almacen
            ComboBox3.Text = NOMBRE_DEPOSITO(_DEPOSITO)
            CBEQUIPO.Enabled = True
        Else
            ComboBox3.Enabled = True
            ComboBox3.Focus()
            llenar_DS_DEPOSITO()
            ComboBox3.Focus()
            CBEQUIPO.Enabled = False
        End If
    End Sub

    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        e.Graphics.DrawString("LOTE Nº: " + cajon.ToString, New Font("ARIAL", 48, FontStyle.Bold), Brushes.Black, 20, 70)
        e.Graphics.DrawString("FECHA LOTE: " + Date.Now.ToShortDateString, New Font("ARIAL", 30, FontStyle.Bold), Brushes.Black, 30, 135)
        e.Graphics.DrawString("FAMILIA: " + nomfamilia.ToString, New Font("ARIAL", 30, FontStyle.Bold), Brushes.Black, 30, 180)
        e.Graphics.DrawString("CANTIDAD: " + cantmed.ToString, New Font("ARIAL", 30, FontStyle.Bold), Brushes.Black, 30, 230)
        e.Graphics.DrawString(MAIN.obtenerbarras(cajon).ToString, New Font("Code 2 of 5 Interleaved", 48, FontStyle.Bold), Brushes.Black, 150, 270)
        '      e.Graphics.DrawString(MAIN.obtenerbarras(cajon).ToString, New Font("ARIAL", 35, FontStyle.Bold), Brushes.Black, 740, 170)

    End Sub



    Private Sub PrintDocument2_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument2.PrintPage
        e.Graphics.DrawString("LOTE Nº: " + cajon.ToString, New Font("ARIAL", 48, FontStyle.Bold), Brushes.Black, 20, 70)
        e.Graphics.DrawString("FECHA LOTE: " + Date.Now.ToShortDateString, New Font("ARIAL", 30, FontStyle.Bold), Brushes.Black, 30, 135)
        e.Graphics.DrawString("CANTIDAD: " + cantmed.ToString, New Font("ARIAL", 30, FontStyle.Bold), Brushes.Black, 30, 180)
        e.Graphics.DrawString("MEDIDORES", New Font("ARIAL", 20, FontStyle.Bold), Brushes.Black, 50, 240)
        Dim RENGLON As Integer = 270
        For I = 0 To tablacust.Rows.Count - 1
            e.Graphics.DrawString((I + 1).ToString.PadLeft(2, "0") + "-" + tablacust.Rows(I).Item(0).ToString + " - " + tablacust.Rows(I).Item(1).ToString, New Font("ARIAL", 20, FontStyle.Bold), Brushes.Black, 50, RENGLON)
            RENGLON += 30
        Next
    End Sub
    Private Function buscar_medidor(ByVal medidor As Decimal) As Boolean
        Dim resp As Boolean = False
        Dim cnn1 As SqlConnection = New SqlConnection(conexion)
        'Abrimos la conexion
        cnn1.Open()
        'Creamos el comando para crear la consulta
        Dim Comando As New SqlClient.SqlCommand("select NSERI_113,FCARGO_113,USER_C_113,DEPOSI_113,OT_113,CMATE_113,POLIZA_113,CONTRATO_113,FRETIRO_113,FINFO_113,FAMILIA_113,CAJON_113,USER_AC_113,ESTADO_113,FREMITO_113,NREMITO_113,USER_REM_113,OPERA_113,PROVE_113 FROM T_MED_DEVO_113 WHERE NSERI_113= @D1", cnn1)
        'Ejecutamos el commnad y le pasamos el parametro
        Comando.Parameters.Add(New SqlParameter("D1", medidor))
        Comando.ExecuteNonQuery()
        Dim Dusrs As SqlDataReader = Comando.ExecuteReader
        If Dusrs.Read.ToString Then
            Try
                familia = Dusrs.GetValue(10)
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
                    nremitoD = "SIN REMITO"
                    UserRemito = ""
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            resp = True
        Else
            resp = False

        End If
        cnn1.Close()
        Return resp
    End Function





    Private Function verificarOCmed(ByVal medidor As Decimal) As Boolean
        Dim resp As Boolean = False
        Dim res As Object
        Dim cnn1 As SqlConnection = New SqlConnection(conexionIntegral)
        'Abrimos la conexion
        cnn1.Open()
        'Creamos el comando para crear la consulta
        Dim Comando As New SqlClient.SqlCommand("SELECT TIPO_TRAB900, CCONT900, NPARTE900, NMEDIDOR900 FROM C900_TAREAS WHERE NMEDIDOR900 = @D1", cnn1)
        'Ejecutamos el commnad y le pasamos el parametro
        Comando.Parameters.Add(New SqlParameter("D1", medidor.ToString()))
        res = Convert.ToString(Comando.ExecuteScalar())
        Try
            If res = "OC" Then
                resp = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        cnn1.Close()
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

    Private Sub PictureBox2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'If (listaNoEncontrados.Count <> 0) Then
        '    listaNoEncontrados = listaNoEncontrados.Distinct().ToList()
        '    ArmarExcel1(listaNoEncontrados, SaveFileDialog1)
        '    listaNoEncontrados.Clear()
        'End If
    End Sub

    Private Sub ArmarExcel1(ByRef listamedsnoencontrados As List(Of String), ByRef listaNoEncontradosESTADOS As List(Of String))
        If My.Computer.FileSystem.DirectoryExists("C:\ARCHIVO") = False Then
            My.Computer.FileSystem.CreateDirectory("C:\ARCHIVO")
        End If
        Dim fichero As String = "C:\Archivo\ListaMedidoresNoEncontrados" + ".csv"
        Archivo = fichero
        'Dim dialog As DialogResult = ruta.ShowDialog
        'If (dialog = DialogResult.OK) Then
        Dim a As New System.IO.StreamWriter(fichero)
        a.WriteLine("NUMERO DE MEDIDOR" + ";" + "ESTADO" + ";" + "FECHA INGRESO")
        Dim NMED As String
        Dim FECHA As Date
        Dim EST As String
        For i = 0 To listamedsnoencontrados.Count - 1
            NMED = listamedsnoencontrados(i)
            EST = listaNoEncontradosESTADOS(i)
            FECHA = DateTime.Now
            a.WriteLine(NMED + ";" + EST + ";" + FECHA.ToString())
        Next
        a.Close()
        MessageBox.Show("DATOS EXPORTADOS CORRECTAMENTE", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information)
        'End If
    End Sub

    Private Sub ArmarExcel2(ByRef listaOC As List(Of String), ByRef ruta As SaveFileDialog)
        If My.Computer.FileSystem.DirectoryExists("C:\ARCHIVO") = False Then
            My.Computer.FileSystem.CreateDirectory("C:\ARCHIVO")
        End If
        Dim fichero As String = "C:\Archivo\ListaMedidoresOC" + ".csv"
        Archivo = fichero
        'Dim dialog As DialogResult = ruta.ShowDialog
        'If (dialog = DialogResult.OK) Then
        Dim a As New System.IO.StreamWriter(fichero)
        a.WriteLine("NUMERO DE MEDIDOR" + ";" + "FECHA INGRESO")
        Dim NMED As String
        Dim FECHA As Date
        For i = 0 To listaOC.Count - 1
            NMED = listaOC(i)
            FECHA = DateTime.Now
            a.WriteLine(NMED + ";" + FECHA.ToString())
        Next
        a.Close()
        MessageBox.Show("DATOS EXPORTADOS CORRECTAMENTE", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information)
        'End If
    End Sub

    Private Sub PALMA046_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        If listaOC.Count > 0 Then
            Me.Cursor = Cursors.WaitCursor
            If DialogResult.Yes = MessageBox.Show("Desea Enviar el Mail con Medidores de OC?", "ATENCION", MessageBoxButtons.YesNo, MessageBoxIcon.Information) Then
                listaOC = listaOC.Distinct().ToList()
                ArmarBodyMailOC()
                listaOC.Clear()
            Else
                listaOC.Clear()
            End If
        End If
        If listaNoEncontrados.Count > 0 Then
            Me.Cursor = Cursors.WaitCursor
            listaNoEncontrados = listaNoEncontrados.Distinct().ToList()
            ArmarBodyMail()
            listaNoEncontrados.Clear()
            listaNoEncontradosESTADOS.Clear()
        End If
    End Sub

    Private Sub ArmarBodyMail()
        Dim TABLABODY As String = ""
        Dim BODY As String = "<body><table width='897' height='1017' border='0'> <tr><td height='103' bordercolor='#F0F0F0'><img src='http://exgadetsa.com.ar/wp-content/uploads/2018/08/logo-color-amarillo-celeste.png' width='143' height='111'/></td><td colspan='2'><div align='center' class='Estilo1'>LISTADO DE MEDIDORES NO ENCONTRADOS</div></td></tr><tr><td width='112' height='31' bordercolor='#F0F0F0'>&nbsp;</td><td colspan='2'><div align='left'>---MEDIDORES---</div></td></tr>"
        For I = 0 To listaNoEncontrados.Count - 1
            TABLABODY = TABLABODY + "<tr><td><div align='left'>" + listaNoEncontrados(I) + "</div></td></tr>"
        Next
        BODY = BODY + TABLABODY + "</table></div></td></tr></table></body>"
        ENVIAR_MENSAJE(BODY)
    End Sub

    Private Sub ArmarBodyMailOC()
        Dim TABLABODY As String = ""
        Dim BODY As String = "<body><table width='897' height='1017' border='0'> <tr><td height='103' bordercolor='#F0F0F0'><img src='http://exgadetsa.com.ar/wp-content/uploads/2018/08/logo-color-amarillo-celeste.png' width='143' height='111'/></td><td colspan='2'><div align='center' class='Estilo1'>LISTADO DE MEDIDORES PROVENIENTES DE OC</div></td></tr><tr><td width='112' height='31' bordercolor='#F0F0F0'>&nbsp;</td><td colspan='2'><div align='left'>---MEDIDORES---</div></td></tr>"
        For I = 0 To listaOC.Count - 1
            TABLABODY = TABLABODY + "<tr><td><div align='left'>" + listaOC(I) + "</div></td></tr>"
        Next
        BODY = BODY + TABLABODY + "</table></div></td></tr></table></body>"
        ENVIAR_MENSAJEOC(BODY)
    End Sub

    Public Sub ENVIAR_MENSAJE(ByVal BODY As String)

        ArmarExcel1(listaNoEncontrados, listaNoEncontradosESTADOS)
        Dim _mensage As New System.Net.Mail.MailMessage
        Dim _Smtp As New System.Net.Mail.SmtpClient
        _Smtp.Credentials = New System.Net.NetworkCredential("automatico@exgadetsa.com.ar", "dni26226264b")
        _Smtp.Host = MAIN.smtpmail
        _Smtp.Port = MAIN.puertomail
        _Smtp.EnableSsl = False
        'configuracion del mensaje
        _mensage.To.Add("frozenmuter@exgadetsa.com.ar" + "," + "rfernandez@exgadetsa.com.ar" + "," + "jvalencia@exgadetsa.com.ar" + "," + "jleuce@exgadetsa.com.ar" + "," + "dambres@exgadetsa.com.ar")
        '_mensage.To.Add("frozenmuter@exgadetsa.com.ar")
        _mensage.From = New System.Net.Mail.MailAddress("automatico@exgadetsa.com.ar", "LISTADO MEDIDORES NO ENCONTRADOS", System.Text.Encoding.UTF8)
        _mensage.Subject = "LISTADO DE MEDIDORES NO ENCONTRADOS"
        _mensage.SubjectEncoding = System.Text.Encoding.UTF8
        _mensage.Body = BODY
        If Archivo <> Nothing Then
            _mensage.Attachments.Add(New System.Net.Mail.Attachment(Archivo))
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

    Public Sub ENVIAR_MENSAJEOC(ByVal BODY As String)

        ArmarExcel2(listaOC, SaveFileDialog1)
        Dim _mensage As New System.Net.Mail.MailMessage
        Dim _Smtp As New System.Net.Mail.SmtpClient
        _Smtp.Credentials = New System.Net.NetworkCredential("automatico@exgadetsa.com.ar", "dni26226264b")
        _Smtp.Host = MAIN.smtpmail
        _Smtp.Port = MAIN.puertomail
        _Smtp.EnableSsl = False
        'configuracion del mensaje
        _mensage.To.Add("frozenmuter@exgadetsa.com.ar" + "," + "rfernandez@exgadetsa.com.ar" + "," + "jleuce@exgadetsa.com.ar" + "," + "dambres@exgadetsa.com.ar")
        '_mensage.To.Add("frozenmuter@exgadetsa.com.ar")
        _mensage.From = New System.Net.Mail.MailAddress("automatico@exgadetsa.com.ar", "LISTADO MEDIDORES PROVENIENTES DE OC", System.Text.Encoding.UTF8)
        _mensage.Subject = "LISTADO MEDIDORES PROVENIENTES DE OC"
        _mensage.SubjectEncoding = System.Text.Encoding.UTF8
        _mensage.Body = BODY
        If Archivo <> Nothing Then
            _mensage.Attachments.Add(New System.Net.Mail.Attachment(Archivo))
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

    Private Function buscar_sap(ByVal medidor As Decimal)
        Dim resp As String = ""
        Dim cnn1 As SqlConnection = New SqlConnection(conexion)
        'Abrimos la conexion
        cnn1.Open()
        'Creamos el comando para crear la consulta
        Dim Comando As New SqlClient.SqlCommand("select CMATE_113 FROM T_MED_DEVO_113 WHERE NSERI_113= @D1", cnn1)
        'Ejecutamos el commnad y le pasamos el parametro
        Comando.Parameters.Add(New SqlParameter("D1", medidor))
        Comando.ExecuteNonQuery()
        Dim Dusrs As SqlDataReader = Comando.ExecuteReader
        If Dusrs.Read.ToString Then
            Try
                resp = Dusrs.GetValue(0)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
        cnn1.Close()
        Return resp
    End Function

    Private Sub EnviarPorMail(ByVal lista As List(Of Clase_med_retirar))
        Dim archivo As String
        archivo = armar_archivo(cajon, lista)
        Dim _mensage As New System.Net.Mail.MailMessage
        Dim _Smtp As New System.Net.Mail.SmtpClient
        _Smtp.Credentials = New System.Net.NetworkCredential(MAIN.mail, MAIN.passmail)
        _Smtp.Host = MAIN.smtpmail
        _Smtp.Port = MAIN.puertomail
        _Smtp.EnableSsl = False
        'configuracion del mensaje
        _mensage.To.Add("almacenes@exgadetsa.com.ar")
        _mensage.From = New System.Net.Mail.MailAddress("almacensis@exgadetsa.com.ar", "MEDIDORES DE GEA MAL TIPEADOS", System.Text.Encoding.UTF8)
        _mensage.Subject = "MEDIDORES DE GEA MAL TIPEADOS"
        _mensage.SubjectEncoding = System.Text.Encoding.UTF8
        _mensage.Body = "CAJON Nº" + cajon
        If archivo <> Nothing Then
            _mensage.Attachments.Add(New System.Net.Mail.Attachment(archivo))
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

    Private Function armar_archivo(ByVal cajon As String, ByVal lista As List(Of Clase_med_retirar))
        Dim fichero As String = ""
        Try
            If My.Computer.FileSystem.DirectoryExists("C:\ARCHIVO") = False Then
                My.Computer.FileSystem.CreateDirectory("C:\ARCHIVO")
            End If
            fichero = "C:\Archivo\MedidoresAGeaMalTipeados_" + cajon + ".csv"
            Dim a As New System.IO.StreamWriter(fichero)
            a.WriteLine("N_SERIE;CAPACIDAD;DESCRIPCION")
            Dim NMEDIDOR As String
            Dim CAPADIDAD As String
            Dim DESCRIPCION As String
            For I = 0 To lista.Count - 1
                NMEDIDOR = lista(I).GETSETNMED
                CAPADIDAD = lista(I).GETSETSAP
                DESCRIPCION = lista(I).GETSETNOMFAMILIA
                a.WriteLine(NMEDIDOR.PadLeft(8, "0") + ";" + CAPADIDAD.ToString() + ";" + DESCRIPCION.ToString())
            Next
            a.Close()
        Catch ex As Exception
        End Try
        Return fichero
    End Function
End Class
