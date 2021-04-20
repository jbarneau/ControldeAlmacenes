Imports System.Data.SqlClient
Imports System.Data.Sql
Imports System.Data.SqlTypes

Public Class PALMA016
    Private NSERIE As String
    Private TIPOMQ As TIPO_DE_MAQUINA
    Private tipo As String
    Public Sub TOMAR(ByVal NSERIE As String, ByVal TIPO As String)
        Me.NSERIE = NSERIE
        TIPOMQ = New TIPO_DE_MAQUINA(TIPO)
        Me.tipo = TIPO
    End Sub

    Private Sub PALMA016_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label1.Text = NSERIE
        LLENAR()
    End Sub
    Private Sub LLENAR()
        DataGridView1.Rows.Clear()
        ComboBox1.Text = Nothing
        Dim CNN As New SqlConnection(conexion)
        Try
            CNN.Open()
            Dim ADT As New SqlCommand("SELECT NCALI_118, TIPO_118, FCALI_118, VTO_118, FBAJA_118, NSERIE_118 FROM T_CALIBRACION_118 WHERE (NSERIE_118 = @D1) ORDER BY FCALI_118, TIPO_118", CNN)
            ADT.Parameters.AddWithValue("D1", NSERIE)
            Dim LECTOR As SqlDataReader = ADT.ExecuteReader
            Dim TIPO As String
            Dim BAJA As String
            Do While LECTOR.Read
                If IsDBNull(LECTOR.GetValue(4)) Then
                    BAJA = "NO"
                Else
                    BAJA = "SI"
                End If
                If LECTOR.GetValue(1) = "1" Then
                    TIPO = "CALIBRACION"
                Else
                    TIPO = "VERIFICACION"
                End If
                DataGridView1.Rows.Add(LECTOR.GetValue(0), TIPO, CDate(LECTOR.GetValue(2)).ToShortDateString, CDate(LECTOR.GetValue(3)).ToShortDateString, LECTOR.GetValue(4), BAJA)
            Loop
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            CNN.Close()
        End Try
        Dim TABLA As New DataTable
        TABLA.Columns.Add("TIPO")
        TABLA.Columns.Add("DESC")
        If TIPOMQ.leerCalibracion = 1 Then
            TABLA.Rows.Add(1, "CALIBRACION")
        End If
        If TIPOMQ.leerVerificacion = 1 Then
            TABLA.Rows.Add(2, "VERIFICACION")
        End If
        ComboBox1.DataSource = TABLA
        ComboBox1.DisplayMember = "DESC"
        ComboBox1.ValueMember = "TIPO"
        ComboBox1.Text = Nothing
    End Sub
   
    Private Sub BTCARGAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTCARGAR.Click
        Dim pantalla As New PALMA011
        pantalla.TOMAR(tipo, NSERIE, ComboBox1.SelectedValue)
        pantalla.ShowDialog()
        If pantalla.LEERCONFIRMACION = True Then
            LLENAR()
        End If
    End Sub
End Class