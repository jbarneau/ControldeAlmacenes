
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.Data.Sql
Public Class PALMA022
    Private METODO_MED As New Clas_Medidor

    Private Sub PALMA022_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label3.Text = "USUARIO: " & _usr.Obt_Nombre_y_Apellido
        Label5.Text = "Fecha: " & DateTime.Now().ToShortDateString
        LLENAROPERARIO()
    End Sub


    Private Sub LLENAROPERARIO()
        Dim CNN As New SqlConnection(conexion)
        Dim TABLA As New DataTable
        Try
            CNN.Open()
            Dim ADT As New SqlDataAdapter("SELECT T_MEDI_102.CALMA_102 AS DNI, M_PERS_003.APELL_003 + N' ' + M_PERS_003.NOMB_003 AS NOMBRE, COUNT(T_MEDI_102.NSERIE_102) AS CANTIDAD FROM T_MEDI_102 INNER JOIN M_PERS_003 ON T_MEDI_102.CALMA_102 = M_PERS_003.NDOC_003 WHERE (T_MEDI_102.FREV_102 < GETDATE()) AND (T_MEDI_102.ESTADO_102 = 1) AND (M_PERS_003.DEPO_003 = 0) GROUP BY T_MEDI_102.CALMA_102, M_PERS_003.APELL_003 + N' ' + M_PERS_003.NOMB_003 ORDER BY NOMBRE", CNN)
            ADT.Fill(TABLA)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            CNN.Close()
        End Try
        If TABLA.Rows.Count <> 0 Then
            cbEquipo.DataSource = TABLA
            cbEquipo.DisplayMember = "NOMBRE"
            cbEquipo.ValueMember = "DNI"
            cbEquipo.Text = Nothing

        End If
    End Sub
    Private Sub llenar_dw(dni As String)
        DataGridView1.Rows.Clear()
        Dim cnn As New SqlConnection(conexion)
        Dim tabla As New DataTable
        Try
            cnn.Open()
            Dim adt As New SqlDataAdapter("SELECT NSERIE_102, FREV_102 FROM T_MEDI_102 WHERE (FREV_102 < GETDATE()) AND (CALMA_102 = @D1) ORDER BY NSERIE_102", cnn)
            adt.SelectCommand.Parameters.AddWithValue("D1", dni)
            adt.Fill(tabla)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn.Close()
        End Try
        If tabla.Rows.Count Then
            For i = 0 To tabla.Rows.Count - 1
                DataGridView1.Rows.Add(tabla.Rows(i).Item(0), tabla.Rows(i).Item(1))
            Next
            Button1.Enabled = True
        Else
            Button1.Enabled = False
        End If



    End Sub


    Private Sub cbEquipo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbEquipo.SelectedIndexChanged
        If cbEquipo.ValueMember <> Nothing And cbEquipo.Text <> Nothing Then
            llenar_dw(cbEquipo.SelectedValue)
        End If


    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click


        If DataGridView1.SelectedRows.Count <> 0 Then
            Dim msm As DialogResult = MessageBox.Show("ESTA POR ACTUALIZAR " + DataGridView1.SelectedRows.Count.ToString + " MEDIDORES" + vbCrLf + "¿ESTA SEGURI?", "PREGUNTA", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If msm = DialogResult.Yes Then
                For I = 0 To DataGridView1.Rows.Count - 1
                    If DataGridView1.Rows(I).Selected Then
                        METODO_MED.MODIFICAR_MEDIDOR_REVISADO(DataGridView1.Item(0, I).Value)
                    End If

                Next
                LLENAROPERARIO()
                DataGridView1.Rows.Clear()
                Button1.Enabled = False
            End If

        End If
    End Sub
End Class