Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.Data.Sql
Imports System.IO

Public Class PPETI002
    Private oc As New Class_OC
    Private mensaje As New Clase_mensaje
    Private metodos As New Clas_Almacen
    Private NESTADO As Integer

   

    Private Sub PPETI002_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label3.Text = "USUARIO: " & _usr.Obt_Nombre_y_Apellido
        Label5.Text = "Fecha: " & DateTime.Now().ToShortDateString
        llenar_estado()
    End Sub
    Private Sub llenar_estado()
        Dim DS_contrato As New DataSet
        'CONECTO LA BASE
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("SELECT C_PARA_802, DESC_802 FROM DET_PARAMETRO_802 where F_BAJA_802 is NULL AND C_TABLA_802 = 9 order by DESC_802", cnn2)
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.Fill(DS_contrato, "DET_PARAMETRO_802")
        cnn2.Close()
        CB_estado.DataSource = DS_contrato.Tables("DET_PARAMETRO_802")
        CB_estado.DisplayMember = "DESC_802"
        CB_estado.ValueMember = "C_PARA_802"
        CB_estado.Text = Nothing
    End Sub

    Private Sub CB_estado_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CB_estado.SelectedIndexChanged
        If CB_estado.ValueMember <> Nothing Then
            If CB_estado.Text <> Nothing Then
                NESTADO = CB_estado.SelectedValue
                llenarDW1()
                Button1.Enabled = False
                Button3.Enabled = False
                Button4.Enabled = False
            End If
        End If
    End Sub
    Private Sub llenarDW1()
        Dim d1 As String = ""
        Dim d2 As String = ""
        Dim d3 As String = ""
        Dim d4 As String = ""
        Dim d5 As String = ""
        DataGridView1.Rows.Clear()
        'creo la cadena de conexion
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("SELECT dbo.T_C_OC_105.N_OC_105, dbo.T_C_OC_105.F_ALTA_105, dbo.T_C_OC_105.N_PETI_105, dbo.T_C_OC_105.FAPRO_105, dbo.M_CONT_004.DESC_004 FROM dbo.T_C_OC_105 INNER JOIN dbo.M_CONT_004 ON dbo.T_C_OC_105.CONT_105 = dbo.M_CONT_004.NCONT_004 WHERE (dbo.T_C_OC_105.TIPO_OC_105 = 2) AND (dbo.T_C_OC_105.ESTA_105 = @D1)", con1)
        'creo el lector de parametros
        comando1.Parameters.Add(New SqlParameter("D1", NESTADO))
        comando1.ExecuteNonQuery()
        'genero un lector
        Dim lector1 As SqlDataReader = comando1.ExecuteReader
        While lector1.Read
            d1 = lector1.GetValue(0).ToString.PadLeft(8, "0")
            d2 = lector1.GetDateTime(1).ToShortDateString
            If IsDBNull(lector1.GetValue(2)) = False Then
                d3 = lector1.GetValue(2)
            End If
            If IsDBNull(lector1.GetValue(3)) = False Then
                d4 = lector1.GetDateTime(3).ToShortDateString
            End If
            d5 = lector1.GetValue(4)
            Me.DataGridView1.Rows.Add(d1, d2, d3, d4, d5)
        End While
        Me.DataGridView2.Rows.Clear()
        'ciero la conexion
        con1.Close()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        PrintDocument1.Print()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        If DataGridView1.RowCount <> 0 And DataGridView2.RowCount <> 0 Then
            Dim pantallas As New PPETI002BIS
            pantallas.ShowDialog()
            If pantallas.respuesta = True Then
                oc.Grabar_N_peticion_gnf(CDec(Me.DataGridView1.Item(0, DataGridView1.CurrentRow.Index).Value),3,Date.Now,_usr.Obt_Usr, pantallas.NUMERO_PETICION)
                mensaje.MADVE003()
                Button4.Enabled = False
                Button1.Enabled = False
                Button3.Enabled = False
                llenarDW1()
            End If
        Else
            mensaje.MERRO011()
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If DataGridView1.RowCount <> 0 And DataGridView2.RowCount <> 0 Then
            Dim PANTALLA As New PPETI002BIS_2
            PANTALLA.ShowDialog()
            If PANTALLA.respuesta = True Then
                oc.cerrar_peticion(CDec(Me.DataGridView1.Item(0, DataGridView1.CurrentRow.Index).Value), 4, Date.Now, _usr.Obt_Usr, PANTALLA.tipo_cierre)
                mensaje.MADVE003()
                llenarDW1()
                Button1.Enabled = False
                Button3.Enabled = False
                Button4.Enabled = False

            Else
                mensaje.MERRO011()
            End If
        End If
    End Sub
    Private Sub llenarDW2_estado1(ByVal nremi As Decimal)
        Dim mate As String = ""
        Dim desc As String = ""
        Dim soli As Decimal = 0
        Dim ent As Decimal = 0
        Dim UNID As String = ""
        DataGridView2.Rows.Clear()
        'creo la cadena de conexion
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("SELECT dbo.T_D_OC_106.C_MATE_106, dbo.M_MATE_002.DESC_002, dbo.T_D_OC_106.CANT_106, dbo.T_D_OC_106.CANTE_106, dbo.M_MATE_002.UNID_002 FROM dbo.T_D_OC_106 INNER JOIN dbo.M_MATE_002 ON dbo.T_D_OC_106.C_MATE_106 = dbo.M_MATE_002.CMATE_002 WHERE (dbo.T_D_OC_106.N_OC_106 = @D1)", con1)
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
            UNID = lector1.GetValue(4)
            Me.DataGridView2.Rows.Add(mate, desc, UNID, soli, ent, soli - ent)
        End While
        'ciero la conexion
        con1.Close()
    End Sub
    Private Sub llenarDW2_estado2y3(ByVal nremi As Decimal)
        Dim mate As String = ""
        Dim desc As String = ""
        Dim soli As Decimal = 0
        Dim ent As Decimal = 0
        Dim UNID As String = ""
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
            UNID = lector1.GetValue(4)
            Me.DataGridView2.Rows.Add(mate, desc, UNID, soli, ent, soli - ent)
        End While
        'ciero la conexion
        con1.Close()
    End Sub
    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        If DataGridView1.RowCount <> 0 Then
            If NESTADO = 1 Then

                llenarDW2_estado1(CDec(Me.DataGridView1.Item(0, DataGridView1.CurrentRow.Index).Value))
            Else

                llenarDW2_estado2y3(CDec(Me.DataGridView1.Item(0, DataGridView1.CurrentRow.Index).Value))

            End If
            If DataGridView1.RowCount <> 0 And DataGridView2.RowCount <> 0 Then
                If NESTADO = 2 Then
                    Button4.Enabled = True
                    Button1.Enabled = True
                ElseIf NESTADO = 1 Then
                    Button4.Enabled = False
                    Button1.Enabled = False
                ElseIf NESTADO = 3 Then
                    Button4.Enabled = False
                    Button1.Enabled = True
                ElseIf NESTADO = 4 Then
                    Button1.Enabled = False
                    Button4.Enabled = False

                End If

                Button3.Enabled = True
            End If
        End If
    End Sub

    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Dim LINEA As Integer
        Dim N_OC As String = Me.DataGridView1.Item(0, DataGridView1.CurrentRow.Index).Value
        Dim F_alta As String = Me.DataGridView1.Item(1, DataGridView1.CurrentRow.Index).Value
        Dim contrato As String = Me.DataGridView1.Item(4, DataGridView1.CurrentRow.Index).Value
        Dim D_estado As String = Me.CB_estado.Text
        Dim Cod_estado As String = CB_estado.SelectedValue
        Dim usr_genero As String = ""
        Dim Cod_Proveedor As String = ""
        Dim Det_Proveedor As String = ""
        Dim tipo As String = "PETICION DE MATERIALES"
        Dim N_Peticion As String = Me.DataGridView1.Item(2, DataGridView1.CurrentRow.Index).Value
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
            usr_aprobo = "LA PETICION NO ESTA APROBADA"
        Else
            usr_aprobo = usr_aprobo + " Fecha: " + F_Aprobo
        End If

        con1.Close()


        e.Graphics.DrawString("CONSULTA DE PETICION", New Font("Arial", 14, FontStyle.Bold), Brushes.Black, 50, 50)
        e.Graphics.DrawLine(Pens.Black, 0, 90, 1000, 90)
        e.Graphics.DrawString("PETICION: " + N_Peticion.ToString, New Font("Arial", 11, FontStyle.Regular), Brushes.Black, 50, 100)
        e.Graphics.DrawString("FECHA: " + F_alta.ToString, New Font("Arial", 11, FontStyle.Regular), Brushes.Black, 500, 100)
        e.Graphics.DrawString("USUARIO GENERADA: " + usr_genero.ToString, New Font("Arial", 11, FontStyle.Regular), Brushes.Black, 50, 130)
        e.Graphics.DrawString("CONTRATO: " + contrato.ToString, New Font("Arial", 11, FontStyle.Regular), Brushes.Black, 500, 130)
        e.Graphics.DrawString("PROVEEDOR: " + Det_Proveedor.ToString, New Font("Arial", 11, FontStyle.Regular), Brushes.Black, 50, 160)
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
        For i = 0 To Me.DataGridView2.RowCount - 1
            e.Graphics.DrawString(DataGridView2.Item(0, i).Value + "  -  " + DataGridView2.Item(1, i).Value, New Font("Arial", 10, FontStyle.Regular), Brushes.Black, 10, LINEA)
            'e.Graphics.DrawString(DataGridView2.Item(1, i).Value, New Font("Arial", 10, FontStyle.Regular), Brushes.Black, 100, LINEA)
            e.Graphics.DrawString(DataGridView2.Item(2, i).Value, New Font("Arial", 10, FontStyle.Regular), Brushes.Black, 593, LINEA)
            e.Graphics.DrawString(DataGridView2.Item(3, i).Value, New Font("Arial", 10, FontStyle.Regular), Brushes.Black, 650, LINEA)
            e.Graphics.DrawString(DataGridView2.Item(5, i).Value, New Font("Arial", 10, FontStyle.Regular), Brushes.Black, 750, LINEA)
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

    
End Class