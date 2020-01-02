Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.Data.Sql
Imports System.IO
Public Class PCONS003

    Private Sub PCONS003_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label3.Text = "USUARIO: " & _usr.Obt_Nombre_y_Apellido
        Label5.Text = "Fecha: " & DateTime.Now().ToShortDateString
        llenar_DS_MATERIAL()
        CB_PROVEEDOR.Focus()
    End Sub


    Private Sub llenar_DS_MATERIAL()
        Dim DS_MATERIAL As New DataSet
        'CONECTO LA BASE
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        'Dim adaptadaor As New SqlDataAdapter("SELECT CMATE_002, DESC_002, CALTE_002 FROM M_MATE_002 where M_MATE_002.F_BAJA_002 IS NULL order by DESC_002", cnn2)
        Dim adaptadaor As New SqlDataAdapter("SELECT M_MATE_002.CMATE_002 as CMATE_002, M_MATE_002.DESC_002 as DESC_002, M_MATE_002.CALTE_002, DET_PARAMETRO_802.DESC_802 FROM M_MATE_002 INNER JOIN DET_PARAMETRO_802 ON M_MATE_002.TIPO_002 = DET_PARAMETRO_802.C_PARA_802 WHERE M_MATE_002.F_BAJA_002 IS NULL AND DET_PARAMETRO_802.C_TABLA_802 = 2 ORDER BY M_MATE_002.DESC_002", cnn2)
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.Fill(DS_material, "M_MATE_002")
        cnn2.Close()
        CB_PROVEEDOR.DataSource = DS_MATERIAL.Tables("M_MATE_002")
        CB_PROVEEDOR.DisplayMember = "DESC_002"
        CB_PROVEEDOR.ValueMember = "CMATE_002"
        CB_PROVEEDOR.Text = Nothing


    End Sub
    
    Private Sub Label2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub CB_PROVEEDOR_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CB_PROVEEDOR.SelectedIndexChanged
        If CB_PROVEEDOR.ValueMember <> Nothing And CB_PROVEEDOR.Text <> Nothing Then
            TextBox6.Text = CB_PROVEEDOR.SelectedValue
            LLENAR_DATOS(CB_PROVEEDOR.SelectedValue)

        End If
    End Sub

    Private Function TIPO(ByVal str As Integer) As String
        Dim resp As String = "error"
        Dim cnn1 As SqlConnection = New SqlConnection(conexion)
        'Abrimos la conexion
        cnn1.Open()
        'Creamos el comando para crear la consulta
        Dim Comando As New SqlClient.SqlCommand("select DESC_802 FROM DET_PARAMETRO_802 WHERE C_TABLA_802 = 2 AND C_PARA_802 = @D1", cnn1)
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

    Private Sub LLENAR_DATOS(ByVal DATO As String)
        Dim cnn1 As SqlConnection = New SqlConnection(conexion)
        'Abrimos la conexion
        cnn1.Open()
        'Creamos el comando para crear la consulta
        Dim Comando As New SqlClient.SqlCommand("select CALTE_002,TIPO_002,SERI_002,CONS_002,DECI_002,F_ALTA_002,F_BAJA_002 FROM M_MATE_002 WHERE CMATE_002 = @D1", cnn1)
        'Ejecutamos el commnad y le pasamos el parametro
        Comando.Parameters.Add(New SqlParameter("D1", DATO))
        Comando.ExecuteNonQuery()
        Dim Dusrs As SqlDataReader = Comando.ExecuteReader
        If Dusrs.Read.ToString Then

            If IsDBNull(Dusrs.GetValue(0)) = True Then
                TextBox5.Text = ""
            Else
                TextBox5.Text = Dusrs.GetValue(0)
            End If
            TextBox2.Text = TIPO(Dusrs.GetInt32(1))
            TextBox1.Text = Dusrs.GetDateTime(5).ToShortDateString
            If IsDBNull(Dusrs.GetValue(6)) = True Then
                TextBox3.Text = "----------------"
            Else
                TextBox3.Text = Dusrs.GetDateTime(6).ToShortDateString
            End If
            If Dusrs.GetValue(2) = True Then
                TextBox7.Text = "SERIALIZADO"
            ElseIf Dusrs.GetValue(3) = True Then
                TextBox7.Text = "CONSUMIBLE"
            Else
                TextBox7.Text = "------------------"
            End If
            If Dusrs.GetValue(4) = True Then
                TextBox4.Text = "CON DECIMALES"
            Else
                TextBox4.Text = "SIN DECIMALES"
            End If
            cnn1.Close()
        End If

    End Sub

    Private Sub btnexcel_Click(sender As Object, e As EventArgs) Handles btnexcel.Click
        ArmarExcel1(CB_PROVEEDOR.DataSource)
    End Sub

    Private Sub ArmarExcel1(ByRef dt As DataTable)
        Dim ofd As SaveFileDialog = New SaveFileDialog
        ofd.Filter = ".csv|"
        ofd.DefaultExt = "CSV"
        If ofd.ShowDialog = DialogResult.OK Then
            Dim fichero As String = ofd.FileName
            Dim a As New System.IO.StreamWriter(fichero)
            a.WriteLine("CODMATE;CODALT;DESC;TIPO;")
            Dim CODMATE As String
            Dim CODALT As String
            Dim DESC As String
            Dim TIPO As String
            For i = 0 To dt.Rows.Count - 1
                CODMATE = dt.Rows(i).Item(0).ToString()
                CODALT = dt.Rows(i).Item(2).ToString()
                DESC = dt.Rows(i).Item(1).ToString()
                TIPO = dt.Rows(i).Item(3).ToString()
                a.WriteLine(CODMATE + ";" + CODALT + ";" + DESC + ";" + TIPO)
            Next
            a.Close()
            MessageBox.Show("DATOS EXPORTADOS CORRECTAMENTE", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub
End Class