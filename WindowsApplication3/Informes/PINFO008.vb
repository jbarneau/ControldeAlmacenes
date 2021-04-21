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
            Dim P As New PCONS008BIS2
            If cbMaterial.Text <> Nothing Then
                P.tomar(NOC, cbMaterial.SelectedValue)
            Else
                P.tomar(NOC, "NO")
            End If

            P.ShowDialog()
        End If
    End Sub

#Region "FUNCIONES"
    ' PRINT DOCUMENT

    'LLENAR DATASVIEW
    Private Sub llenarDW12(ByVal desde As Date, ByVal hasta As Date, ByVal pro As String, ByVal codmat As String)

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


    'BOTON DE FILTRAR POR FECHA Y PROVEEDRO

    'boton del excle
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btEcxel.Click
        Me.Cursor = Cursors.WaitCursor
        DataGridView1.Rows.Clear()
        'primero tengo que asignar fechas
        Dim fdesde As Date = dtpDesde.Value.ToShortDateString + " 00:00"
        Dim fhasta As Date = dtpHasta.Value.ToShortDateString + " 23:50"
        Dim whereProveedor As String = ""
        Dim whereMaterial As String = ""
        Dim tabla As New DataTable
        If cbMaterial.Text <> Nothing Then
            whereMaterial = " AND (T_D_OC_106.C_MATE_106 = '" + cbMaterial.SelectedValue + "')"
        End If
        If cbProveedor.Text <> Nothing Then
            whereProveedor = " AND (T_C_OC_105.C_PROV_105 = '" + cbProveedor.SelectedValue + "')"
        End If
        Dim consulta As String = “SELECT T_C_OC_105.N_OC_105 AS NOC, T_C_OC_105.F_ALTA_105 AS FALTA, UsrG.APELL_001 + ' ' + UsrG.NOMB_001 AS GENERO, DET_PARAMETRO_802.DESC_802 AS ESTADO, T_C_OC_105.FAPRO_105 AS FAPROBO, UsrA.APELL_001 + ' ' + UsrA.NOMB_001 AS APROBO, M_PROV_005.RAZO_005 AS PROVEEDOR,IIF(T_C_OC_105.CONPRECIO_105=1,'CON PRECIO','SIN PRECIO') AS VALORIZADA, T_C_OC_105.MONTO_105, T_D_OC_106.C_MATE_106 AS CODMATERIAL, M_MATE_002.DESC_002 AS DESC_MAERIAL, M_MATE_002.UNID_002 AS UNIDAD, T_D_OC_106.CANT_106 AS CANT_SOLICITADA, T_D_OC_106.CANTE_106 AS CANT_ENTREGADA, T_D_OC_106.CANT_106 - T_D_OC_106.CANTE_106 AS SALDO, T_D_OC_106.PRECIO_C_106 AS PRECIO_U, T_D_OC_106.PRECIO_C_106 * T_D_OC_106.CANT_106 AS SUBTOTAL FROM T_C_OC_105 INNER JOIN M_USRS_001 AS UsrG ON T_C_OC_105.USERG_105 = UsrG.NDOC_001 INNER JOIN M_USRS_001 AS UsrA ON T_C_OC_105.USERR_105 = UsrA.NDOC_001 INNER JOIN DET_PARAMETRO_802 ON T_C_OC_105.ESTA_105 = DET_PARAMETRO_802.C_PARA_802 INNER JOIN M_PROV_005 ON T_C_OC_105.C_PROV_105 = M_PROV_005.CUIT_005 INNER JOIN T_D_OC_106 ON T_C_OC_105.N_OC_105 = T_D_OC_106.N_OC_106 INNER JOIN M_MATE_002 ON T_D_OC_106.C_MATE_106 = M_MATE_002.CMATE_002 WHERE (DET_PARAMETRO_802.C_TABLA_802 = 9) AND (T_C_OC_105.TIPO_OC_105 = 1) AND (T_C_OC_105.F_ALTA_105 BETWEEN @D1 AND @D2)” + whereMaterial + whereProveedor + “ ORDER BY NOC DESC”

        Dim CNN As New SqlConnection(conexion)
        Try
            CNN.Open()
            Dim ADAPTADOR As New SqlDataAdapter(consulta, CNN)
            ADAPTADOR.SelectCommand.Parameters.Add(New SqlParameter("D1", fdesde))
            ADAPTADOR.SelectCommand.Parameters.Add(New SqlParameter("D2", fhasta))
            ADAPTADOR.Fill(tabla)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            CNN.Close()
        End Try
        If tabla.Rows.Count <> 0 Then
            If My.Computer.FileSystem.DirectoryExists("C:\ARCHIVO") = False Then
                My.Computer.FileSystem.CreateDirectory("C:\ARCHIVO")
            End If
            Dim fichero As String = "C:\Archivo\Informe_OC_" + cbProveedor.Text + "_" + fdesde.ToString("dd-MM-yyyy") + "_" + fhasta.ToString("dd-MM-yyyy") + "_" + Date.Now.ToString("ddMMyyyyHHmmss") + ".csv"
            Dim a As New System.IO.StreamWriter(fichero)
            a.WriteLine("Desde:;" + fdesde.ToShortDateString + "; hasta:;" + fhasta.ToShortDateString)
            Dim linea As String = ""
            For I = 0 To tabla.Columns.Count - 1
                If I = 0 Then
                    linea = tabla.Columns(I).ColumnName.ToString
                Else
                    linea = linea + ";" + tabla.Columns(I).ColumnName.ToString
                End If
            Next
            a.WriteLine(linea)
            For I = 0 To tabla.Rows.Count - 1
                For j = 0 To tabla.Columns.Count - 1
                    If j = 0 Then
                        linea = tabla.Rows(I).Item(j).ToString
                    Else
                        linea = linea + ";" + tabla.Rows(I).Item(j).ToString
                    End If
                Next
                a.WriteLine(linea)
            Next
            a.Close()
            MENSAJE.MADVE002(fichero)

        End If
        Me.Cursor = Cursors.Arrow


    End Sub
    'BOTON DE FILTRAR POR NUMERO DE REMITO

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        DataGridView1.Rows.Clear()

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
        DataGridView1.Rows.Clear()
        'primero tengo que asignar fechas
        Dim fdesde As Date = dtpDesde.Value.ToShortDateString + " 00:00"
        Dim fhasta As Date = dtpHasta.Value.ToShortDateString + " 23:50"
        Dim whereProveedor As String = ""
        Dim whereMaterial As String = ""
        If cbMaterial.Text <> Nothing Then
            whereMaterial = "AND (T_D_OC_106.C_MATE_106 = '" + cbMaterial.SelectedValue + "')"
        End If
        If cbProveedor.Text <> Nothing Then
            whereProveedor = "AND (T_C_OC_105.C_PROV_105 = '" + cbProveedor.SelectedValue + "')"
        End If
        Dim consulta As String = “SELECT T_C_OC_105.N_OC_105 AS NOC, T_C_OC_105.F_ALTA_105 AS FALTA, UsrG.APELL_001 + ' ' + UsrG.NOMB_001 AS GENERO, DET_PARAMETRO_802.DESC_802 AS ESTADO, T_C_OC_105.FAPRO_105 AS FAPROBO, UsrA.APELL_001 + ' ' + UsrA.NOMB_001 AS APROBO, M_PROV_005.RAZO_005 AS PROVEEDOR, IIF(T_C_OC_105.CONPRECIO_105=1,'CON PRECIO','SIN PRECIO') AS VALORIZADA, T_C_OC_105.MONTO_105, SUM(T_D_OC_106.CANT_106) AS CANTIDAD FROM T_C_OC_105 INNER JOIN M_USRS_001 AS UsrG ON T_C_OC_105.USERG_105 = UsrG.NDOC_001 INNER JOIN M_USRS_001 AS UsrA ON T_C_OC_105.USERR_105 = UsrA.NDOC_001 INNER JOIN DET_PARAMETRO_802 ON T_C_OC_105.ESTA_105 = DET_PARAMETRO_802.C_PARA_802 INNER JOIN M_PROV_005 ON T_C_OC_105.C_PROV_105 = M_PROV_005.CUIT_005 INNER JOIN T_D_OC_106 ON T_C_OC_105.N_OC_105 = T_D_OC_106.N_OC_106 WHERE (DET_PARAMETRO_802.C_TABLA_802 = 9) AND (T_C_OC_105.TIPO_OC_105 = 1)” + whereMaterial + whereProveedor + ” GROUP BY T_C_OC_105.N_OC_105, T_C_OC_105.F_ALTA_105, UsrG.APELL_001 + ' ' + UsrG.NOMB_001, DET_PARAMETRO_802.DESC_802, T_C_OC_105.FAPRO_105, UsrA.APELL_001 + ' ' + UsrA.NOMB_001, M_PROV_005.RAZO_005, T_C_OC_105.CONPRECIO_105, T_C_OC_105.MONTO_105 HAVING (T_C_OC_105.F_ALTA_105 BETWEEN @D1 AND @D2) ORDER BY NOC”

        Dim CNN As New SqlConnection(conexion)

        Try
            CNN.Open()
            Dim ADAPTADOR As New SqlCommand(consulta, CNN)
            ADAPTADOR.Parameters.Add(New SqlParameter("D1", fdesde))
            ADAPTADOR.Parameters.Add(New SqlParameter("D2", fhasta))
            Dim LECTOR As SqlDataReader = ADAPTADOR.ExecuteReader
            Do While LECTOR.Read
                Me.DataGridView1.Rows.Add(LECTOR.GetValue(0).ToString, LECTOR.GetValue(1).ToString, LECTOR.GetValue(2), LECTOR.GetValue(3), LECTOR.GetValue(4), LECTOR.GetValue(5), LECTOR.GetValue(6), LECTOR.GetValue(7), LECTOR.GetValue(8))
            Loop
            If DataGridView1.RowCount = 0 Then
                MENSAJE.MERRO011()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            CNN.Close()
        End Try
        If DataGridView1.RowCount <> 0 Then
            btEcxel.Enabled = True
        Else
            btEcxel.Enabled = False
        End If

        Me.Cursor = Cursors.Arrow
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        llenar_dgv1()



    End Sub
End Class