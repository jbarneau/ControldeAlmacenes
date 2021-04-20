Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.Data.Sql
Imports System.IO
Public Class PCOMP002
    Private oc As New Class_OC
    Private mensaje As New Clase_mensaje
    Private metodos As New Clas_Almacen
    Private SeModifica As Boolean = True
    Private NESTADO As Integer
    Private noc As Decimal = 0

    Private Sub llenar_estado()
        Dim DS_contrato As New DataSet
        'CONECTO LA BASE
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("SELECT C_PARA_802, DESC_802 FROM DET_PARAMETRO_802 where F_BAJA_802 is NULL AND C_TABLA_802 = 9 and C_PARA_802 <> 2 order by DESC_802", cnn2)
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.Fill(DS_contrato, "DET_PARAMETRO_802")
        cnn2.Close()
        ComboBox1.DataSource = DS_contrato.Tables("DET_PARAMETRO_802")
        ComboBox1.DisplayMember = "DESC_802"
        ComboBox1.ValueMember = "C_PARA_802"
        ComboBox1.Text = Nothing
    End Sub


    Private Sub llenarDW1()
        Dim noc As String = ""
        Dim FE As String = ""
        Dim Genero As String = ""
        Dim FA As String = ""
        Dim Aprobo As String = ""
        Dim Proveedor As String = ""
        Dim Valorizada As String = ""
        Dim Valor As String = ""
        dgv1.Rows.Clear()
        'creo la cadena de conexion
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("SELECT dbo.T_C_OC_105.N_OC_105, dbo.T_C_OC_105.F_ALTA_105, dbo.T_C_OC_105.FAPRO_105, dbo.M_PROV_005.RAZO_005, dbo.T_C_OC_105.USERG_105, dbo.T_C_OC_105.USERR_105,dbo.T_C_OC_105.CONPRECIO_105,IIF(dbo.T_C_OC_105.MONTO_105=null,0,dbo.T_C_OC_105.MONTO_105) as valor FROM dbo.T_C_OC_105 INNER JOIN dbo.M_PROV_005 ON dbo.T_C_OC_105.C_PROV_105 = dbo.M_PROV_005.CUIT_005 WHERE (dbo.T_C_OC_105.TIPO_OC_105 = 1) AND (dbo.T_C_OC_105.ESTA_105 = @D1) AND (dbo.T_C_OC_105.F_ALTA_105 BETWEEN @D2 AND @D3)", con1)
        'creo el lector de parametros
        comando1.Parameters.Add(New SqlParameter("D1", NESTADO))
        comando1.Parameters.Add(New SqlParameter("D2", dtpdesde.Value.ToShortDateString + " " + "00:00:00"))
        comando1.Parameters.Add(New SqlParameter("D3", dtphasta.Value.ToShortDateString + " " + "23:59:00"))
        comando1.ExecuteNonQuery()
        'genero un lector
        Dim lector1 As SqlDataReader = comando1.ExecuteReader
        While lector1.Read
            noc = lector1.GetValue(0).ToString.PadLeft(8, "0")
            FE = lector1.GetDateTime(1).ToShortDateString
            Genero = MAIN.OBT_NOM_USER(lector1.GetValue(4))
            If IsDBNull(lector1.GetValue(2)) = False Then
                FA = lector1.GetDateTime(2).ToShortDateString
            End If
            If IsDBNull(lector1.GetValue(5)) = False Then
                Aprobo = MAIN.OBT_NOM_USER(lector1.GetValue(5))
            Else
                Aprobo = "NO APROBADA"
            End If
            Proveedor = lector1.GetValue(3)
            If lector1.GetValue(6) = "0" Then
                Valorizada = "SIN VALOR"
            Else
                Valorizada = "CON VALOR"
            End If
            If IsDBNull(lector1.GetValue(7)) Then
                Valor = "0"
            Else
                Valor = lector1.GetValue(7)
            End If



            Me.dgv1.Rows.Add(noc, FE, Genero, FA, Aprobo, Proveedor, Valorizada, Valor)
        End While
        Me.dgv2.Rows.Clear()
        'ciero la conexion
        con1.Close()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btImprimir.Click
        If dgv1.SelectedRows.Count <> 0 Then
            oc.ImprimirDetalleOC(CDec(dgv1.Item(0, dgv1.CurrentRow.Index).Value))
        End If




        'Dim dr As New DialogResult
        'dr = PrintDialog1.ShowDialog()
        'If dr = DialogResult.OK Then
        '    PrintDocument1.PrinterSettings = PrintDialog1.PrinterSettings
        '    PrintDocument1.Print()
        '    MessageBox.Show("SE HA IMPRIMIDO CORRECTAMENTE", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information)
        'End If
    End Sub



    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btCerrar.Click
        If dgv1.RowCount <> 0 And dgv2.RowCount <> 0 Then
            Dim PANTALLA As New PPETI002BIS_2
            PANTALLA.ShowDialog()
            If PANTALLA.respuesta = True Then
                oc.cerrar_peticion(CDec(Me.dgv1.Item(0, dgv1.CurrentRow.Index).Value), 4, Date.Now, _usr.Obt_Usr, PANTALLA.tipo_cierre)
                mensaje.MADVE003()
                llenarDW1()
                btCerrar.Enabled = False
                btImprimir.Enabled = False

            End If
        Else
            mensaje.MERRO011()
        End If
    End Sub
    Private Sub llenarDW2(ByVal nremi As Decimal)
        Dim mate As String = ""
        Dim desc As String = ""
        Dim soli As Decimal = 0
        Dim ent As Decimal = 0
        Dim U As String
        Dim valorU As Decimal = 0
        Dim total As Decimal = 0
        dgv2.Rows.Clear()
        'creo la cadena de conexion
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("SELECT T_D_OC_106.C_MATE_106, M_MATE_002.DESC_002,M_MATE_002.UNID_002, T_D_OC_106.CANT_106, T_D_OC_106.CANTE_106,T_D_OC_106.PRECIO_C_106 FROM T_D_OC_106 INNER JOIN M_MATE_002 ON T_D_OC_106.C_MATE_106 = M_MATE_002.CMATE_002 WHERE (T_D_OC_106.N_OC_106 = @D1)", con1)
        'creo el lector de parametros
        comando1.Parameters.Add(New SqlParameter("D1", nremi))
        comando1.ExecuteNonQuery()
        'genero un lector
        Dim lector1 As SqlDataReader = comando1.ExecuteReader
        'pregunto si encontro
        While lector1.Read
            mate = lector1.GetValue(0)
            desc = lector1.GetValue(1)
            U = lector1.GetValue(2)
            soli = lector1.GetValue(3)
            ent = lector1.GetValue(4)
            valorU = lector1.GetValue(5)
            total = FormatNumber(valorU * soli, 2)
            Me.dgv2.Rows.Add(mate, desc, U, soli, ent, soli - ent, valorU, total)
        End While
        'ciero la conexion
        con1.Close()
    End Sub

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgv1.DoubleClick
        If dgv1.RowCount <> 0 Then
            noc = CDec(Me.dgv1.Item(0, dgv1.CurrentRow.Index).Value)
            llenarDW2(noc)

            If dgv1.RowCount <> 0 And dgv2.RowCount <> 0 Then


                If NESTADO = 1 Then
                    btCerrar.Enabled = False
                    SeModifica = True
                ElseIf NESTADO = 3 Then
                    btCerrar.Enabled = True
                    For i = 0 To dgv2.RowCount - 1
                        If (CDec(dgv2.Item(4, i).Value) <> 0) Then
                            SeModifica = False
                        End If
                    Next
                ElseIf NESTADO = 4 Then
                    btCerrar.Enabled = False
                    SeModifica = False
                End If
                btImprimir.Enabled = True
                If _usr.Activar_BT("PCONF001") = True Then
                    If SeModifica = True Then
                        btModificar.Enabled = True
                    Else
                        btModificar.Enabled = False
                    End If
                Else
                    btModificar.Enabled = False
                End If


            End If
        End If
    End Sub

    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Dim LINEA As Integer
        Dim N_OC As String = Me.dgv1.Item(0, dgv1.CurrentRow.Index).Value
        Dim F_alta As String = Me.dgv1.Item(1, dgv1.CurrentRow.Index).Value
        Dim Contrato As String = Me.dgv1.Item(6, dgv1.CurrentRow.Index).Value
        Dim D_estado As String = Me.ComboBox1.Text
        Dim Cod_estado As String = ComboBox1.SelectedValue
        Dim usr_genero As String = ""
        Dim Cod_Proveedor As String = ""
        Dim Det_Proveedor As String = ""
        Dim tipo As String = "ORDEN DE COMPRA"
        Dim N_Peticion As String = Me.dgv1.Item(2, dgv1.CurrentRow.Index).Value
        Dim usr_aprobo As String = ""
        Dim F_Aprobo As String = ""
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        Dim comando1 As New SqlClient.SqlCommand("SELECT USERG_105, C_PROV_105, USERR_105, FAPRO_105 FROM T_C_OC_105 WHERE N_OC_105 = @D1", con1)
        'creo el lector de parametros
        comando1.Parameters.Add(New SqlParameter("D1", CDec(N_OC)))
        comando1.ExecuteNonQuery()
        'genero un lector
        Dim lector1 As SqlDataReader = comando1.ExecuteReader
        While lector1.Read
            usr_genero = metodos.Usuario(lector1.GetValue(0).ToString)
            Cod_Proveedor = lector1.GetValue(1)
            Det_Proveedor = DETPROVEEDOR(Cod_Proveedor)
            If IsDBNull(lector1.GetValue(2)) = False Then
                usr_aprobo = metodos.Usuario(lector1.GetValue(2))
            Else
                usr_aprobo = ""
            End If
            If IsDBNull(lector1.GetValue(3)) = False Then
                F_Aprobo = lector1.GetDateTime(3).ToShortDateString
            Else
                F_Aprobo = ""
            End If
        End While
        If usr_aprobo = "" Then
            usr_aprobo = "OC NO APROBADA"
        Else
            usr_aprobo = usr_aprobo + " Fecha: " + F_Aprobo
        End If

        con1.Close()


        e.Graphics.DrawString("CONSULTA DE ORDENES DE COMPRA", New Font("Arial", 14, FontStyle.Bold), Brushes.Black, 50, 50)
        e.Graphics.DrawLine(Pens.Black, 0, 90, 1000, 90)
        e.Graphics.DrawString("NUMERO DE OC: " + N_OC.ToString, New Font("Arial", 11, FontStyle.Regular), Brushes.Black, 50, 100)
        e.Graphics.DrawString("FECHA: " + F_alta.ToString, New Font("Arial", 11, FontStyle.Regular), Brushes.Black, 500, 100)
        e.Graphics.DrawString("USUARIO GENERADA: " + usr_genero.ToString, New Font("Arial", 11, FontStyle.Regular), Brushes.Black, 50, 130)
        e.Graphics.DrawString("CONTARTO: " + Contrato.ToString, New Font("Arial", 11, FontStyle.Regular), Brushes.Black, 500, 130)
        e.Graphics.DrawString("PROVEEDOR: " + " - " + Det_Proveedor.ToString + " (" + Cod_Proveedor.ToString + ")", New Font("Arial", 11, FontStyle.Regular), Brushes.Black, 50, 160)
        e.Graphics.DrawString("ESTADO: " + Cod_estado + " - " + D_estado.ToString, New Font("Arial", 11, FontStyle.Regular), Brushes.Black, 500, 160)
        e.Graphics.DrawString("APROBADA: " + usr_aprobo.ToString, New Font("Arial", 11, FontStyle.Regular), Brushes.Black, 50, 190)
        e.Graphics.DrawString("TIPO: " + tipo.ToString, New Font("Arial", 11, FontStyle.Regular), Brushes.Black, 550, 190)
        e.Graphics.DrawLine(Pens.Black, 0, 220, 1000, 220)
        e.Graphics.DrawString("CODIGO", New Font("Arial", 10, FontStyle.Regular), Brushes.Black, 10, 250)
        e.Graphics.DrawString("DETALLE", New Font("Arial", 10, FontStyle.Regular), Brushes.Black, 150, 250)
        e.Graphics.DrawString("U", New Font("Arial", 10, FontStyle.Regular), Brushes.Black, 595, 250)
        e.Graphics.DrawString("SOLICITADO", New Font("Arial", 10, FontStyle.Regular), Brushes.Black, 630, 250)
        e.Graphics.DrawString("SALDO", New Font("Arial", 10, FontStyle.Regular), Brushes.Black, 750, 250)
        e.Graphics.DrawLine(Pens.Black, 0, 270, 1000, 270)
        LINEA = 290
        For i = 0 To Me.dgv2.RowCount - 1
            e.Graphics.DrawString(dgv2.Item(0, i).Value + "  -  " + dgv2.Item(1, i).Value, New Font("Arial", 10, FontStyle.Regular), Brushes.Black, 10, LINEA)
            'e.Graphics.DrawString(DataGridView2.Item(1, i).Value, New Font("Arial", 10, FontStyle.Regular), Brushes.Black, 100, LINEA)
            e.Graphics.DrawString(dgv2.Item(2, i).Value, New Font("Arial", 10, FontStyle.Regular), Brushes.Black, 593, LINEA)
            e.Graphics.DrawString(dgv2.Item(3, i).Value, New Font("Arial", 10, FontStyle.Regular), Brushes.Black, 650, LINEA)
            e.Graphics.DrawString(dgv2.Item(5, i).Value, New Font("Arial", 10, FontStyle.Regular), Brushes.Black, 750, LINEA)
            LINEA = LINEA + 15
            e.Graphics.DrawLine(Pens.Black, 0, LINEA, 1000, LINEA)
            LINEA = LINEA + 10
        Next
    End Sub

    Private Function DETPROVEEDOR(ByVal A As String) As String
        Dim RESP As String = ""
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        Dim comando1 As New SqlClient.SqlCommand("SELECT RAZO_005 FROM M_PROV_005 WHERE CUIT_005 = @D1", con1)
        'creo el lector de parametros
        comando1.Parameters.Add(New SqlParameter("D1", A))
        comando1.ExecuteNonQuery()
        'genero un lector
        Dim lector1 As SqlDataReader = comando1.ExecuteReader
        If lector1.Read Then
            RESP = lector1.GetValue(0)
        End If
        con1.Close()
        Return RESP
    End Function
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged

    End Sub

    Private Sub PCOMP002_Load_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label3.Text = "USUARIO: " & _usr.Obt_Nombre_y_Apellido
        Label5.Text = "Fecha: " & DateTime.Now().ToShortDateString
        llenar_estado()
    End Sub





    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If ComboBox1.ValueMember <> Nothing Then
            If ComboBox1.Text <> Nothing Then
                NESTADO = ComboBox1.SelectedValue
                llenarDW1()
                btCerrar.Enabled = False
                btImprimir.Enabled = False
                btModificar.Enabled = False
            End If
        End If
    End Sub

    Private Sub btModificar_Click(sender As Object, e As EventArgs) Handles btModificar.Click
        If dgv1.SelectedRows.Count <> 0 Then
            Dim PANTALLA As New PCOMP006

            PANTALLA.Tomar(noc)
            PANTALLA.ShowDialog()
        End If
    End Sub
End Class