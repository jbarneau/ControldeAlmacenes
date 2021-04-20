Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.Data.Sql
Imports System.IO
Public Class PINFO008
    Private MENSAJE As New Clase_mensaje
    Private Metodos As New Clas_Almacen
    Private oc As New Class_OC
    Private NOC As Decimal
    Private _proveedor As String
    Private _material As String
    Private _contrato As String
    Private _cant As Decimal
    Private _CantItem As Integer = 0
    Private Sub PINFO008_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label3.Text = "USUARIO: " & _usr.Obt_Nombre_y_Apellido
        Label5.Text = "Fecha: " & DateTime.Now().ToShortDateString
        llenar_CB_proveedor()
        llenar_CB_materiales()
    End Sub
    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        If DataGridView1.RowCount <> 0 Then
            NOC = CDec(Me.DataGridView1.Item(0, DataGridView1.CurrentRow.Index).Value)
            llenarDW2_estado2y3(NOC)
        End If
    End Sub
    Private Sub DataGridView2_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView2.DoubleClick
        If Me.DataGridView2.Item(4, Me.DataGridView2.CurrentRow.Index).Value <> 0 Then
            Dim PANTALLA As New PINFO008BIS
            PANTALLA.GRABAR(NOC, Me.DataGridView2.Item(0, DataGridView2.CurrentRow.Index).Value)
            PANTALLA.ShowDialog()
        Else
            MENSAJE.MERRO029()
        End If
    End Sub
#Region "FUNCIONES"
    ' PRINT DOCUMENT

    'LLENAR DATASVIEW
    Private Sub llenarDW1(ByVal desde As Date, ByVal hasta As Date, ByVal pro As String, ByVal codmat As String)
        DataGridView2.Rows.Clear()
        Dim d1 As String = ""
        Dim d2 As String = ""
        Dim d3 As String = ""
        Dim d4 As String = ""
        Dim d5 As String = ""
        Dim d6 As String = ""
        Dim d7 As String = ""
        DataGridView1.Rows.Clear()
        'creo la cadena de conexion
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        If codmat <> "-" Then
            If pro <> "-" Then
                Dim comando1 As New SqlClient.SqlCommand("SELECT dbo.T_C_OC_105.N_OC_105, dbo.T_C_OC_105.F_ALTA_105, dbo.T_C_OC_105.FAPRO_105, dbo.M_PROV_005.RAZO_005, dbo.T_C_OC_105.USERG_105, dbo.T_C_OC_105.USERR_105, dbo.T_C_OC_105.CONT_105 FROM  dbo.T_C_OC_105 INNER JOIN  dbo.M_PROV_005 ON dbo.T_C_OC_105.C_PROV_105 = dbo.M_PROV_005.CUIT_005 INNER JOIN dbo.T_D_OC_106 ON dbo.T_C_OC_105.N_OC_105 = dbo.T_D_OC_106.N_OC_106 WHERE (dbo.T_C_OC_105.F_ALTA_105 BETWEEN @D1 AND @D2) AND (dbo.T_C_OC_105.TIPO_OC_105 = 1) AND (dbo.T_C_OC_105.C_PROV_105 = @D3) AND (dbo.T_C_OC_105.ESTA_105 = 3 OR dbo.T_C_OC_105.ESTA_105 = 4) AND (dbo.T_D_OC_106.C_MATE_106 = @D4)", con1)
                'creo el lector de parametros
                comando1.Parameters.Add(New SqlParameter("D1", desde))
                comando1.Parameters.Add(New SqlParameter("D2", hasta))
                comando1.Parameters.Add(New SqlParameter("D3", pro))
                comando1.Parameters.Add(New SqlParameter("D4", codmat))
                comando1.ExecuteNonQuery()
                'genero un lector
                Dim lector1 As SqlDataReader = comando1.ExecuteReader
                While lector1.Read
                    d1 = lector1.GetValue(0).ToString.PadLeft(8, "0")
                    d2 = lector1.GetDateTime(1).ToShortDateString
                    d3 = MAIN.OBT_NOM_USER(lector1.GetValue(4))
                    If IsDBNull(lector1.GetValue(2)) = False Then
                        d4 = lector1.GetDateTime(2).ToShortDateString
                    End If
                    If IsDBNull(lector1.GetValue(5)) = False Then
                        d5 = MAIN.OBT_NOM_USER(lector1.GetValue(5))
                    Else
                        d5 = "NO APROBADA"
                    End If
                    d6 = lector1.GetValue(3)
                    If lector1.GetValue(6) = "0" Then
                        d7 = "SIN CONTRATO"
                    Else
                        d7 = oc.que_contrato(lector1.GetValue(6))
                    End If

                    Me.DataGridView1.Rows.Add(d1, d2, d3, d4, d5, d6, d7)
                End While

                'ciero la conexion
                con1.Close()
            Else
                Dim comando1 As New SqlClient.SqlCommand("SELECT dbo.T_C_OC_105.N_OC_105, dbo.T_C_OC_105.F_ALTA_105, dbo.T_C_OC_105.FAPRO_105, dbo.M_PROV_005.RAZO_005, dbo.T_C_OC_105.USERG_105, dbo.T_C_OC_105.USERR_105, dbo.T_C_OC_105.CONT_105 FROM  dbo.T_C_OC_105 INNER JOIN  dbo.M_PROV_005 ON dbo.T_C_OC_105.C_PROV_105 = dbo.M_PROV_005.CUIT_005 INNER JOIN dbo.T_D_OC_106 ON dbo.T_C_OC_105.N_OC_105 = dbo.T_D_OC_106.N_OC_106 WHERE (dbo.T_C_OC_105.F_ALTA_105 BETWEEN @D1 AND @D2) AND (dbo.T_C_OC_105.TIPO_OC_105 = 1) AND (dbo.T_C_OC_105.ESTA_105 = 3 OR dbo.T_C_OC_105.ESTA_105 = 4) AND (dbo.T_D_OC_106.C_MATE_106 = @D4)", con1)
                'creo el lector de parametros
                comando1.Parameters.Add(New SqlParameter("D1", desde))
                comando1.Parameters.Add(New SqlParameter("D2", hasta))
                comando1.Parameters.Add(New SqlParameter("D4", codmat))
                comando1.ExecuteNonQuery()
                'genero un lector
                Dim lector1 As SqlDataReader = comando1.ExecuteReader
                While lector1.Read
                    d1 = lector1.GetValue(0).ToString.PadLeft(8, "0")
                    d2 = lector1.GetDateTime(1).ToShortDateString
                    d3 = MAIN.OBT_NOM_USER(lector1.GetValue(4))
                    If IsDBNull(lector1.GetValue(2)) = False Then
                        d4 = lector1.GetDateTime(2).ToShortDateString
                    End If
                    If IsDBNull(lector1.GetValue(5)) = False Then
                        d5 = MAIN.OBT_NOM_USER(lector1.GetValue(5))
                    Else
                        d5 = "NO APROBADA"
                    End If
                    d6 = lector1.GetValue(3)
                    If lector1.GetValue(6) = "0" Then
                        d7 = "SIN CONTRATO"
                    Else
                        d7 = oc.que_contrato(lector1.GetValue(6))
                    End If

                    Me.DataGridView1.Rows.Add(d1, d2, d3, d4, d5, d6, d7)
                End While

                'ciero la conexion
                con1.Close()
            End If
        Else
            Dim comando1 As New SqlClient.SqlCommand("SELECT dbo.T_C_OC_105.N_OC_105, dbo.T_C_OC_105.F_ALTA_105, dbo.T_C_OC_105.FAPRO_105, dbo.M_PROV_005.RAZO_005, dbo.T_C_OC_105.USERG_105, dbo.T_C_OC_105.USERR_105, dbo.T_C_OC_105.CONT_105 FROM dbo.T_C_OC_105 INNER JOIN dbo.M_PROV_005 ON dbo.T_C_OC_105.C_PROV_105 = dbo.M_PROV_005.CUIT_005 WHERE (dbo.T_C_OC_105.F_ALTA_105 between @D1 and @D2) and (dbo.T_C_OC_105.TIPO_OC_105 = 1) and (dbo.T_C_OC_105.C_PROV_105 = @D3)AND (dbo.T_C_OC_105.ESTA_105 = 3 or dbo.T_C_OC_105.ESTA_105 = 4 ) ", con1)
            'creo el lector de parametros
            comando1.Parameters.Add(New SqlParameter("D1", desde))
            comando1.Parameters.Add(New SqlParameter("D2", hasta))
            comando1.Parameters.Add(New SqlParameter("D3", pro))
            comando1.ExecuteNonQuery()
            'genero un lector
            Dim lector1 As SqlDataReader = comando1.ExecuteReader
            While lector1.Read
                d1 = lector1.GetValue(0).ToString.PadLeft(8, "0")
                d2 = lector1.GetDateTime(1).ToShortDateString
                d3 = MAIN.OBT_NOM_USER(lector1.GetValue(4))
                If IsDBNull(lector1.GetValue(2)) = False Then
                    d4 = lector1.GetDateTime(2).ToShortDateString
                End If
                If IsDBNull(lector1.GetValue(5)) = False Then
                    d5 = MAIN.OBT_NOM_USER(lector1.GetValue(5))
                Else
                    d5 = "NO APROBADA"
                End If
                d6 = lector1.GetValue(3)
                If lector1.GetValue(6) = "0" Then
                    d7 = "SIN CONTRATO"
                Else
                    d7 = oc.que_contrato(lector1.GetValue(6))
                End If

                Me.DataGridView1.Rows.Add(d1, d2, d3, d4, d5, d6, d7)
            End While

            'ciero la conexion
            con1.Close()
        End If

    End Sub
    Private Sub llenarDW2_estado2y3(ByVal nremi As Decimal)
        Dim mate As String = ""
        Dim desc As String = ""
        Dim soli As Decimal = 0
        Dim ent As Decimal = 0
        Dim unid As String = ""
        DataGridView2.Rows.Clear()
        'creo la cadena de conexion
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("SELECT dbo.T_D_OC_106.C_MATE_106, dbo.M_MATE_002.DESC_002, dbo.T_D_OC_106.CANT_106, dbo.T_D_OC_106.CANTE_106, dbo.M_MATE_002.UNID_002 FROM dbo.T_D_OC_106 INNER JOIN dbo.M_MATE_002 ON dbo.T_D_OC_106.C_MATE_106 = dbo.M_MATE_002.CMATE_002 WHERE (dbo.T_D_OC_106.N_OC_106 = @D1) and (dbo.T_D_OC_106.CONF_106 = 1) ", con1)
        'creo el lector de parametros
        comando1.Parameters.Add(New SqlParameter("D1", nremi))
        comando1.ExecuteNonQuery()
        'genero un lector
        Dim lector1 As SqlDataReader = comando1.ExecuteReader
        'pregunto si encontro
        While lector1.Read
            mate = lector1.GetValue(0)
            desc = lector1.GetValue(1)
            soli = lector1.GetValue(2)
            ent = lector1.GetValue(3)
            unid = lector1.GetValue(4)
            Me.DataGridView2.Rows.Add(mate, desc, unid, soli, ent, soli - ent)
        End While
        'ciero la conexion
        con1.Close()
        If DataGridView2.Rows.Count <> 0 Then
            Button3.Enabled = True
        Else
            Button3.Enabled = False
        End If
    End Sub
    Private Sub llenar_CB_proveedor()
        Dim DS_contrato As New DataSet
        'CONECTO LA BASE
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("SELECT CUIT_005,RAZO_005 FROM M_PROV_005 where F_BAJA_005 is NULL and SPETI_005 = 0 order by RAZO_005", cnn2)
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.Fill(DS_contrato, "M_PROV_005")
        cnn2.Close()
        cbProveedor.DataSource = DS_contrato.Tables("M_PROV_005")
        cbProveedor.DisplayMember = "RAZO_005"
        cbProveedor.ValueMember = "CUIT_005"
        cbProveedor.Text = Nothing
    End Sub

    Private Sub llenar_CB_materiales()
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        Dim dt As New DataTable()
        cnn2.Open()
        Dim adaptadaor As New SqlDataAdapter("SELECT CMATE_002, DESC_002 FROM M_MATE_002 where SERI_002 = 0 AND F_BAJA_002 IS NULL order by DESC_002", cnn2)
        adaptadaor.Fill(dt)
        cnn2.Close()
        cbMaterial.DisplayMember = "DESC_002"
        cbMaterial.ValueMember = "CMATE_002"
        cbMaterial.DataSource = dt
        cbMaterial.Text = Nothing
    End Sub

#End Region
#Region "BOTONES"
    'boton imprimir
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If DataGridView2.Rows.Count <> 0 Then
            PrintDocument1.Print()
        End If
    End Sub
    'boton salir

    'BOTON DE FILTRAR POR FECHA Y PROVEEDRO

    'boton del excle
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim desde As String = dtpDesde.Value.Day & dtpDesde.Value.Month & dtpDesde.Value.Year
        Dim hasta As String = dtpHasta.Value.Day & dtpHasta.Value.Month & dtpHasta.Value.Year
        Dim desdelargo As String = dtpDesde.Value.Day & "/" & dtpDesde.Value.Month & "/" & dtpDesde.Value.Year
        Dim hastalargo As String = dtpHasta.Value.Day & "/" & dtpHasta.Value.Month & "/" & dtpHasta.Value.Year
        Dim mate As String = ""
        Dim desc As String = ""
        Dim soli As Decimal = 0
        Dim ent As Decimal = 0
        Dim unid As String = ""
        If My.Computer.FileSystem.DirectoryExists("C:\ARCHIVO") = False Then
            My.Computer.FileSystem.CreateDirectory("C:\ARCHIVO")
        End If
        Dim fichero As String = "C:\Archivo\Informe_OC_" + cbProveedor.Text + "_" + desde.ToString + "_" + hasta.ToString + ".csv"
        Dim a As New System.IO.StreamWriter(fichero)
        a.WriteLine("PROVEEDOR:;" + cbProveedor.SelectedValue + "-" + cbProveedor.Text)
        a.WriteLine("Desde:;" + desdelargo.ToString + "; hasta:;" + hastalargo.ToString)
        a.WriteLine()
        a.WriteLine("NOC;FGENERADO; GENERO;FAPROBADO; APROBO;CONTRATO;CODMATERIAL;DETALLE;UNIDAD;SOLICITADO;ENTREGADO;SALDO")
        For I = 0 To Me.DataGridView1.RowCount - 1
            ' a.WriteLine(Me.DataGridView1.Item(0, I).Value.ToString + ";" + Me.DataGridView1.Item(1, I).Value.ToString + ";" + Me.DataGridView1.Item(2, I).Value.ToString + ";" + Me.DataGridView1.Item(3, I).Value.ToString + ";" + Me.DataGridView1.Item(4, I).Value.ToString + ";" + Me.DataGridView1.Item(6, I).Value.ToString)
            ' a.WriteLine(";;CODMATERIAL;DETALLE;UNIDAD;SOLICITADO;ENTREGADO;SALDO")
            Dim con1 As SqlConnection = New SqlConnection(conexion)
            Try
                con1.Open()
                Dim comando1 As New SqlClient.SqlCommand("SELECT dbo.T_D_OC_106.C_MATE_106, dbo.M_MATE_002.DESC_002, dbo.T_D_OC_106.CANT_106, dbo.T_D_OC_106.CANTE_106, dbo.M_MATE_002.UNID_002 FROM dbo.T_D_OC_106 INNER JOIN dbo.M_MATE_002 ON dbo.T_D_OC_106.C_MATE_106 = dbo.M_MATE_002.CMATE_002 WHERE (dbo.T_D_OC_106.N_OC_106 = @D1) and (dbo.T_D_OC_106.CONF_106 = 1) ", con1)
                comando1.Parameters.Add(New SqlParameter("D1", CDec(Me.DataGridView1.Item(0, I).Value)))
                Dim lector1 As SqlDataReader = comando1.ExecuteReader
                While lector1.Read
                    mate = lector1.GetValue(0)
                    desc = lector1.GetValue(1)
                    soli = lector1.GetValue(2)
                    ent = lector1.GetValue(3)
                    unid = lector1.GetValue(4)
                    a.WriteLine(Me.DataGridView1.Item(0, I).Value.ToString + ";" + Me.DataGridView1.Item(1, I).Value.ToString + ";" + Me.DataGridView1.Item(2, I).Value.ToString + ";" + Me.DataGridView1.Item(3, I).Value.ToString + ";" + Me.DataGridView1.Item(4, I).Value.ToString + ";" + Me.DataGridView1.Item(6, I).Value.ToString + ";" + mate.ToString + ";" + desc.ToString + ";" + unid.ToString + ";" + soli.ToString + ";" + ent.ToString + ";" + (soli - ent).ToString)
                End While

            Catch ex As Exception
                MessageBox.Show(ex.Message)
            Finally
                con1.Close()
            End Try
        Next
        a.Close()
        MENSAJE.MADVE002(fichero)
    End Sub
    'BOTON DE FILTRAR POR NUMERO DE REMITO

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        DataGridView1.Rows.Clear()
        DataGridView2.Rows.Clear()

        Dim FGENERADO As Date
        Dim USERG As String
        Dim CONTRATO2 As String
        Dim USERA As String
        Dim FAPRO As Date

        If cbProveedor.ValueMember <> Nothing And nRemito.Text <> Nothing Then
            'PRIMERO TENGO QUE BUSCAR CON EL PROVEEDOR CUAL ES LA OC PARA LLENAR LAS COSAS
            Dim band As Boolean = False
            Dim CNN As New SqlConnection(conexion)
            Dim remito As String
            Try
                CNN.Open()
                Dim ADAPTADOR As New SqlCommand("SELECT N_OC_105, F_ALTA_105, USERG_105, C_PROV_105, USERR_105, FAPRO_105, CONT_105,N_PETI_104 FROM T_C_OC_105 INNER JOIN T_REMI_104 ON N_OC_105 = OC_104 WHERE (C_PROV_105 = @D1) AND (TIPO_OC_105 = 1)", CNN)
                ADAPTADOR.Parameters.Add(New SqlParameter("D1", cbProveedor.SelectedValue))
                Dim LECTOR As SqlDataReader = ADAPTADOR.ExecuteReader
                Do While LECTOR.Read And band = False
                    remito = LECTOR.GetValue(7)
                    If remito = nRemito.Text Then
                        NOC = LECTOR.GetValue(0)
                        FGENERADO = LECTOR.GetDateTime(1)
                        USERG = LECTOR.GetValue(2)
                        USERA = LECTOR.GetValue(4)
                        FAPRO = LECTOR.GetDateTime(5)
                        CONTRATO2 = LECTOR.GetValue(6)
                        Me.DataGridView1.Rows.Add(NOC.ToString, FGENERADO.ToShortDateString, OBT_NOM_USER(USERG), FAPRO.ToShortDateString, OBT_NOM_USER(USERA), cbProveedor.Text, oc.que_contrato(CONTRATO2))
                        llenarDW2_estado2y3(CDec(NOC))
                        band = True
                    End If
                Loop
                If DataGridView1.RowCount = 0 Then
                    MENSAJE.MERRO011()
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            Finally
                CNN.Close()
            End Try
        End If
    End Sub



#End Region

    Private Sub llenar_dgv1()
        Me.Cursor = Cursors.WaitCursor
        'primero tengo que asignar fechas
        'Dim fdesde ad Date = 





        Me.Cursor = Cursors.Arrow
    End Sub

End Class