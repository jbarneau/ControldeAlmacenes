Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Public Class PDEPO001


    Private Sub PDEPO001_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label3.Text = "USUARIO: " & _usr.Obt_Nombre_y_Apellido
        Label5.Text = "Fecha: " & DateTime.Now().ToShortDateString
        llenarCBequipo()
        llenarCBmaterial()
    End Sub
#Region "LLENADO DE COMBOBOX"
    Private Sub llenarCBequipo()
        Dim cnn1 As SqlConnection = New SqlConnection(conexion)
        Dim tabla As New DataTable
        Try
            cnn1.Open()
            Dim consulta1 As New SqlDataAdapter("select APELL_003+' '+ NOMB_003 AS NOMBRE, NDOC_003  from M_PERS_003 where DEPO_003 = 1 and F_BAJA_003 is NULL ORDER BY NOMBRE", cnn1)
            consulta1.Fill(tabla)
            If tabla.Rows.Count <> 0 Then
                cbOperario.DataSource = tabla
                cbOperario.DisplayMember = "NOMBRE"
                cbOperario.ValueMember = "NDOC_003"
                cbOperario.Text = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn1.Close()
        End Try
    End Sub
    Private Sub llenarCBmaterial()
        'TRAIGO MATERIALES
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        Dim TABLA As New DataTable
        Try
            cnn2.Open()
            Dim consulta2 As New SqlDataAdapter("select CMATE_002, DESC_002 from M_MATE_002 where F_BAJA_002 is NULL ORDER BY DESC_002", cnn2)
            consulta2.Fill(TABLA)
            If TABLA.Rows.Count <> 0 Then
                cbMateriales.DataSource = TABLA
                cbMateriales.DisplayMember = "DESC_002"
                cbMateriales.ValueMember = "CMATE_002"
                cbMateriales.Text = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn2.Close()
        End Try
        cbMateriales.Enabled = False
    End Sub
#End Region
#Region "FUNCIONES DATAGRID"
    Private Sub llenardatagriv()
        Dim cnn As New SqlConnection(conexion)
        Try
            cnn.Open()
            Dim adt As New SqlCommand("SELECT T_SCRI_108.C_MATE_108, M_MATE_002.DESC_002, T_SCRI_108.CANT_108 FROM M_MATE_002 INNER JOIN T_SCRI_108 ON M_MATE_002.CMATE_002 = T_SCRI_108.C_MATE_108 WHERE (T_SCRI_108.C_DEPO_108 = @D1) ORDER BY M_MATE_002.DESC_002", cnn)
            adt.Parameters.Add(New SqlParameter("D1", cbOperario.SelectedValue))
            Dim lector As SqlDataReader = adt.ExecuteReader
            Do While lector.Read
                DataGridView2.Rows.Add(lector.GetValue(0), lector.GetValue(1), lector.GetValue(2))
            Loop
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn.Close()
        End Try
        cbMateriales.Enabled = True
        cant.Enabled = True
        btaceptar.Enabled = True
        If DataGridView2.Rows.Count <> 0 Then
            btConfirmar.Enabled = True
        Else
            btConfirmar.Enabled = False
        End If
    End Sub
#End Region
#Region "CONFIRMACION"
    Private Sub BORRAR_TABLA()
        Dim CNN As New SqlConnection(conexion)
        Try
            CNN.Open()
            Dim ADT As New SqlCommand("DELETE FROM T_SCRI_108 WHERE (C_DEPO_108 = @D1)", CNN)
            ADT.Parameters.Add(New SqlParameter("D1", cbOperario.SelectedValue))
            ADT.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            CNN.Close()
        End Try
    End Sub
    Private Sub LLENAR_TABLA(DEPO As String, MATE As String, MAX As Decimal)
        Dim CNN As New SqlConnection(conexion)
        Try
            CNN.Open()
            Dim ADT As New SqlCommand("INSERT INTO T_SCRI_108 (C_DEPO_108,C_MATE_108,CANT_108,USER_108,MAX_108) VALUES (@D1,@D2,@D3,@D4,@D5)", CNN)
            ADT.Parameters.Add(New SqlParameter("D1", DEPO))
            ADT.Parameters.Add(New SqlParameter("D2", MATE))
            ADT.Parameters.Add(New SqlParameter("D3", MAX))
            ADT.Parameters.Add(New SqlParameter("D4", _usr.Obt_Usr))
            ADT.Parameters.Add(New SqlParameter("D5", 0))
            ADT.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            CNN.Close()
        End Try
    End Sub
    Private Sub borrar()
        cbOperario.Enabled = True
        cbOperario.Text = Nothing
        cbMateriales.Enabled = False
        cant.Enabled = False
        btaceptar.Enabled = False
        btConfirmar.Enabled = False
        DataGridView2.Rows.Clear()
        cbMateriales.Text = Nothing
        cant.Value = 0
    End Sub
#End Region
    Private Sub PDEPO003_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Label3.Text = "USUARIO: " & _usr.Obt_Nombre_y_Apellido
        Label5.Text = "Fecha: " & DateTime.Now().ToShortDateString
        llenarCBequipo()
        llenarCBmaterial()
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        If cbOperario.Text <> "" Then
            llenardatagriv()
            cbOperario.Enabled = False

        End If
    End Sub

    Private Sub btaceptar_Click(sender As System.Object, e As System.EventArgs) Handles btaceptar.Click
        If cant.Value <> 0 Then
            Dim resp As Boolean = False
            For i = 0 To DataGridView2.Rows.Count - 1
                If DataGridView2.Item(0, i).Value = cbMateriales.SelectedValue Then
                    DataGridView2.Item(2, i).Value = cant.Value
                    resp = True
                End If
            Next
            If resp = False Then
                DataGridView2.Rows.Add(cbMateriales.SelectedValue, cbMateriales.Text, cant.Value)
            End If
        End If
        If DataGridView2.Rows.Count <> 0 Then
            btConfirmar.Enabled = True
        Else
            btConfirmar.Enabled = False
        End If
        cbMateriales.Text = Nothing
        cant.Value = 0
        cbMateriales.Focus()
    End Sub




    Private Sub ELIMINARToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ELIMINARToolStripMenuItem.Click
        If DataGridView2.Rows.Count <> 0 Then
            Dim resp As MsgBoxResult = MessageBox.Show("Esta seguro que desea eliminar el item" + vbCrLf + DataGridView2.Item(1, DataGridView2.CurrentRow.Index).Value, "CONFIRMACION", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If resp = MsgBoxResult.Yes Then
                DataGridView2.Rows.RemoveAt(DataGridView2.CurrentRow.Index)
            End If
            If DataGridView2.Rows.Count <> 0 Then
                btConfirmar.Enabled = True
            Else
                btConfirmar.Enabled = False
            End If
        End If

    End Sub

    Private Sub MODIFICARCANTIDADToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles MODIFICARCANTIDADToolStripMenuItem.Click
        If DataGridView2.Rows.Count <> 0 Then
            Dim PANTALLA As New PDEPO003BIS
            PANTALLA.TOOMAR(CDec(DataGridView2.Item(2, DataGridView2.CurrentRow.Index).Value))
            PANTALLA.ShowDialog()
            If PANTALLA.LLERRESP = True Then
                DataGridView2.Item(2, DataGridView2.CurrentRow.Index).Value = PANTALLA.LLERCANT
            End If

        End If
    End Sub



    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        borrar()
    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles btConfirmar.Click
        If DataGridView2.Rows.Count <> 0 Then
            BORRAR_TABLA()
            For I = 0 To DataGridView2.Rows.Count - 1
                LLENAR_TABLA(cbOperario.SelectedValue, DataGridView2.Item(0, I).Value, CDec(DataGridView2.Item(2, I).Value))
            Next
            borrar()
        End If
    End Sub

   
   
End Class