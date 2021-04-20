Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.Data.Sql

Public Class CodMedidor
    Dim tablaagregar As New DataTable
    Dim respuesta As Boolean = False
    Dim cod As String = "EXGA09"
    Dim DESC As String = "MEDIDORES DESGUA"
    Private Sub PALAMA040_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        llenarContrato()
        llenarMEDIDOR()
        tablaagregar.Columns.Add("COD")
        tablaagregar.Columns.Add("CONT")
        tablaagregar.Columns.Add("CANT")
        tablaagregar.Columns.Add("OBS")
    End Sub
   
    Private Sub llenarContrato()
        Dim cnn As New SqlConnection(conexion)
        Dim tabla As New DataTable
        Try
            cnn.Open()
            Dim adt As New SqlDataAdapter("SELECT CCONT807, DESC807 FROM P806_CONTRATO_MED ORDER BY DESC807", cnn)
            adt.Fill(tabla)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn.Close()
        End Try
        If tabla.Rows.Count <> 0 Then
            cbContrato.DataSource = tabla
            cbContrato.DisplayMember = "DESC807"
            cbContrato.ValueMember = "CCONT807"
            cbContrato.Text = Nothing
        End If
    End Sub
    Private Sub llenarMEDIDOR()
        Dim cnn As New SqlConnection(conexion)
        Dim tabla As New DataTable
        Try
            cnn.Open()
            Dim adt As New SqlDataAdapter("SELECT CMATE_002, DESC_002 FROM M_MATE_002 WHERE (CMATE_002 = '400005' OR CMATE_002 = '400015' OR CMATE_002 = '400025' OR CMATE_002 = '400115' OR CMATE_002 = '400125' OR CMATE_002 = '400125' OR CMATE_002 = '400135' OR CMATE_002 = '400145' OR  CMATE_002 = '400165' OR CMATE_002 = '400185' OR CMATE_002 = '400305' OR CMATE_002 = '400315' OR CMATE_002 = '400325' OR CMATE_002 = 'EXGA09') ORDER BY DESC_002", cnn)
            adt.Fill(tabla)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn.Close()
        End Try
        If tabla.Rows.Count <> 0 Then
            cbCapacidad.DataSource = tabla
            cbCapacidad.DisplayMember = "DESC_002"
            cbCapacidad.ValueMember = "CMATE_002"
            cbCapacidad.Text = Nothing
        End If
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        If cbContrato.SelectedValue <> Nothing And cbContrato.Text <> Nothing And txtcant.Value <> 0 And cbCapacidad.Text <> Nothing Then
            Dim OBS As String
            If txtObservaciones.Text = "" Then
                OBS = ""
            Else
                OBS = txtObservaciones.Text
            End If
            Dim existe As Boolean = False
            For i = 0 To DataGridView1.RowCount - 1
                If DataGridView1.Item(0, i).Value = cbCapacidad.SelectedValue And DataGridView1.Item(2, i).Value = cbContrato.SelectedValue Then
                    existe = True
                    DataGridView1.Item(4, i).Value = CInt(DataGridView1.Item(4, i).Value) + CInt(txtcant.Value)
                End If
            Next
            If existe = False Then
                DataGridView1.Rows.Add(cbCapacidad.SelectedValue, cbCapacidad.Text, cbContrato.SelectedValue, cbContrato.Text, txtcant.Value, OBS)
            End If
            cbContrato.Text = Nothing
            txtcant.Value = 0
            cbCapacidad.Text = Nothing
            txtObservaciones.Text = Nothing
            cbContrato.Focus()
        End If
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        If DataGridView1.Rows.Count <> 0 Then
            respuesta = True
            For I = 0 To DataGridView1.Rows.Count - 1
                tablaagregar.Rows.Add(DataGridView1.Item(0, I).Value, DataGridView1.Item(2, I).Value, DataGridView1.Item(4, I).Value, DataGridView1.Item(5, I).Value)
            Next
            Me.Close()
        Else
            Me.Close()
            respuesta = False
        End If
    End Sub
    Public ReadOnly Property LEER_RESPUESTA
        Get
            Return respuesta
        End Get
    End Property
    Public ReadOnly Property LEER_TABLA
        Get
            Return tablaagregar
        End Get
    End Property
End Class