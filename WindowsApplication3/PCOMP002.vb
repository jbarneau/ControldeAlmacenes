Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.Data.Sql
Imports System.IO
Public Class PCOMP002
    Private oc As New Class_OC
    Private mensaje As New Clase_mensaje
    Private metodos As New Clas_Almacen

    Private NESTADO As Integer


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
        Dim comando1 As New SqlClient.SqlCommand("SELECT dbo.T_C_OC_105.N_OC_105, dbo.T_C_OC_105.F_ALTA_105, dbo.T_C_OC_105.FAPRO_105, dbo.M_PROV_005.RAZO_005, dbo.T_C_OC_105.USERG_105, dbo.T_C_OC_105.USERR_105, dbo.T_C_OC_105.CONT_105 FROM dbo.T_C_OC_105 INNER JOIN dbo.M_PROV_005 ON dbo.T_C_OC_105.C_PROV_105 = dbo.M_PROV_005.CUIT_005 WHERE (dbo.T_C_OC_105.TIPO_OC_105 = 1) AND (dbo.T_C_OC_105.ESTA_105 = @D1) AND (dbo.T_C_OC_105.F_ALTA_105 BETWEEN @D2 AND @D3)", con1)
        'creo el lector de parametros
        comando1.Parameters.Add(New SqlParameter("D1", NESTADO))
        comando1.Parameters.Add(New SqlParameter("D2", dtpdesde.Value.ToShortDateString + " " + "00:00:00"))
        comando1.Parameters.Add(New SqlParameter("D3", dtphasta.Value.ToShortDateString + " " + "23:59:00"))
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
        Me.DataGridView2.Rows.Clear()
        'ciero la conexion
        con1.Close()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        PrintDocument1.Print()
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

            End If
        Else
            mensaje.MERRO011()
        End If
    End Sub
    Private Sub llenarDW2_estado1(ByVal nremi As Decimal)
        Dim mate As String = ""
        Dim desc As String = ""
        Dim soli As Decimal = 0
        Dim ent As Decimal = 0
        Dim U As String
        DataGridView2.Rows.Clear()
        'creo la cadena de conexion
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("SELECT T_D_OC_106.C_MATE_106, M_MATE_002.DESC_002,M_MATE_002.UNID_002, T_D_OC_106.CANT_106, T_D_OC_106.CANTE_106 FROM T_D_OC_106 INNER JOIN M_MATE_002 ON T_D_OC_106.C_MATE_106 = M_MATE_002.CMATE_002 WHERE (T_D_OC_106.N_OC_106 = @D1)", con1)
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
            Me.DataGridView2.Rows.Add(mate, desc, U, soli, ent, soli - ent)
        End While
        'ciero la conexion
        con1.Close()
    End Sub
    Private Sub llenarDW2_estado2y3(ByVal nremi As Decimal)
        Dim mate As String = ""
        Dim desc As String = ""
        Dim soli As Decimal = 0
        Dim ent As Decimal = 0
        Dim u As String
        DataGridView2.Rows.Clear()
        'creo la cadena de conexion
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("SELECT T_D_OC_106.C_MATE_106, M_MATE_002.DESC_002,M_MATE_002.UNID_002, T_D_OC_106.CANT_106, T_D_OC_106.CANTE_106 FROM T_D_OC_106 INNER JOIN M_MATE_002 ON T_D_OC_106.C_MATE_106 = M_MATE_002.CMATE_002 WHERE (T_D_OC_106.N_OC_106 = @D1) and (T_D_OC_106.CONF_106 = 1) ", con1)
        'creo el lector de parametros
        comando1.Parameters.Add(New SqlParameter("D1", nremi))
        comando1.ExecuteNonQuery()
        'genero un lector
        Dim lector1 As SqlDataReader = comando1.ExecuteReader
        'pregunto si encontro
        While lector1.Read
            mate = lector1.GetValue(0)
            desc = lector1.GetValue(1)
            u = lector1.GetValue(2)
            soli = lector1.GetValue(3)
            ent = lector1.GetValue(4)

            Me.DataGridView2.Rows.Add(mate, desc, u, soli, ent, soli - ent)
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
                If NESTADO = 1 Then
                    Button1.Enabled = False
                ElseIf NESTADO = 3 Then
                    Button1.Enabled = True
                ElseIf NESTADO = 4 Then
                    Button1.Enabled = False
                End If
                Button3.Enabled = True
            End If
        End If
    End Sub

    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Dim LINEA As Integer
        Dim N_OC As String = Me.DataGridView1.Item(0, DataGridView1.CurrentRow.Index).Value
        Dim F_alta As String = Me.DataGridView1.Item(1, DataGridView1.CurrentRow.Index).Value
        Dim Contrato As String = Me.DataGridView1.Item(6, DataGridView1.CurrentRow.Index).Value
        Dim D_estado As String = Me.ComboBox1.Text
        Dim Cod_estado As String = ComboBox1.SelectedValue
        Dim usr_genero As String = ""
        Dim Cod_Proveedor As String = ""
        Dim Det_Proveedor As String = ""
        Dim tipo As String = "ORDEN DE COMPRA"
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
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged

    End Sub

    Private Sub PCOMP002_Load_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label3.Text = "USUARIO: " & _usr.Obt_Nombre_y_Apellido
        Label5.Text = "Fecha: " & DateTime.Now().ToShortDateString
        llenar_estado()
    End Sub



    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        llenarDW1()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If ComboBox1.ValueMember <> Nothing Then
            If ComboBox1.Text <> Nothing Then
                NESTADO = ComboBox1.SelectedValue
                llenarDW1()
                Button1.Enabled = False
                Button3.Enabled = False

            End If
        End If
    End Sub
End Class