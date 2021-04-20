Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Public Class PINFO004
    Dim C_depo As String
    Dim codigo_material As String
    Dim material_seleccionado As String
    Dim consulta As Integer
    Dim material As String
    Dim mensaje As New Clase_mensaje

   

    Private Sub PINFO004_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label3.Text = "USUARIO: " & _usr.Obt_Nombre_y_Apellido
        Label5.Text = "Fecha: " & DateTime.Now().ToShortDateString
        llenar_depositos()
        'CARGO ALMACEN A COMBOBOX
        LLENAR_MATERIAL()
    End Sub

#Region "FUNCIONES"
    Private Sub llenar_depositos()
        Try

            ListView1.Items.Clear()
            Dim renglon As New ListViewItem
            Dim cnn2 As SqlConnection = New SqlConnection(conexion)
            'ABRO LA BASE
            cnn2.Open()
            'GENERO UN ADAPTADOR
            Dim adaptadaor As New SqlClient.SqlCommand("SELECT NDOC_003, NOMB_003 FROM M_PERS_003 WHERE DEPO_003 = 1 ORDER BY NOMB_003", cnn2)
            adaptadaor.ExecuteNonQuery() 'LLENO EL ADAPTADOR CON EL DATASET
            Dim lector As SqlDataReader = adaptadaor.ExecuteReader
            While lector.Read
                renglon = New ListViewItem(lector.GetValue(0).ToString)
                renglon.SubItems.Add(lector.GetValue(1).ToString.ToString)
                ListView1.Items.Add(renglon)
            End While
            cnn2.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub LLENAR_MATERIAL()
        Try
            Dim variable As String = TextBox1.Text
            ListView2.Items.Clear()
            Dim renglon As New ListViewItem
            Dim cnn2 As SqlConnection = New SqlConnection(conexion)
            'ABRO LA BASE
            cnn2.Open()
            'GENERO UN ADAPTADOR
            Dim adaptadaor As New SqlClient.SqlCommand("SELECT CMATE_002, DESC_002 FROM M_MATE_002 WHERE DESC_002 LIKE @D1+'%' ORDER BY DESC_002", cnn2)
            adaptadaor.Parameters.Add(New SqlParameter("D1", variable))
            adaptadaor.ExecuteNonQuery() 'LLENO EL ADAPTADOR CON EL DATASET
            Dim lector As SqlDataReader = adaptadaor.ExecuteReader
            While lector.Read
                renglon = New ListViewItem(lector.GetValue(0).ToString)
                renglon.SubItems.Add(lector.GetValue(1).ToString.ToString)
                ListView2.Items.Add(renglon)
            End While
            cnn2.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub llenar_data_todo(ByVal DEPO As String, ByVal MATE As String, ByVal ndepo As String)
        Dim cnn As New SqlConnection(conexion)
        Try
            cnn.Open()
            Dim adaptador As New SqlCommand("select C_MATE_103, DESC_002,UNID_002,N_CANT_103 FROM T_ALMA_103 INNER JOIN M_MATE_002 ON (CMATE_002 =C_MATE_103) WHERE C_ALMA_103 = @D1 AND C_MATE_103 = @D2 AND ESTA_103 = 1 and N_CANT_103<>0", cnn)
            adaptador.Parameters.Add(New SqlParameter("D1", DEPO))
            adaptador.Parameters.Add(New SqlParameter("D2", MATE))
            adaptador.ExecuteNonQuery()
            Dim lector As SqlDataReader = adaptador.ExecuteReader
            Do While lector.Read
                Me.DataGridView1.Rows.Add(ndepo, lector.GetValue(0), lector.GetValue(1), lector.GetValue(2), lector.GetValue(3))
            Loop

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn.Close()
        End Try
    End Sub
#End Region



    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        LLENAR_MATERIAL()
    End Sub

    Private Sub B_Agregar_Item_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B_Agregar_Item.Click
        Me.DataGridView1.Rows.Clear()
        If ListView1.SelectedIndices.Count <> 0 Then
            If ListView2.SelectedIndices.Count = 0 Then
                'lleno los depositos pero de cada item traigo todos los materiales activos
                For j = 0 To ListView2.Items.Count - 1
                    For i = 0 To ListView1.Items.Count - 1
                        If ListView1.Items(i).Selected Then
                            llenar_data_todo(ListView1.Items(i).Text, ListView2.Items(j).Text, ListView1.Items(i).SubItems(1).Text)
                        End If
                    Next
                Next
                If DataGridView1.RowCount <> 0 Then
                    Button1.Enabled = True
                Else
                    mensaje.MERRO011()
                End If
                

            Else
                For I = 0 To ListView2.Items.Count - 1
                    If ListView2.Items(I).Selected Then
                        For J = 0 To ListView1.Items.Count - 1
                            If ListView1.Items(J).Selected Then
                                llenar_data_todo(ListView1.Items(J).Text, ListView2.Items(I).Text, ListView1.Items(J).SubItems(1).Text)
                            End If
                        Next
                    End If
                Next
                If DataGridView1.RowCount <> 0 Then
                    Button1.Enabled = True
                Else
                    mensaje.MERRO011()
                End If
            End If
        Else
            mensaje.MERRO030()
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'EXPORTO A EXCEL................
        If My.Computer.FileSystem.DirectoryExists("C:\ARCHIVO") = False Then
            My.Computer.FileSystem.CreateDirectory("C:\ARCHIVO")
        End If
        If DataGridView1.RowCount <> 0 Then
            Dim fichero As String = "C:\Archivo\Stock_de_depositos_" + DateTime.Now.ToString("dd-MM-yyyy") + ".csv"
            Dim a As New System.IO.StreamWriter(fichero)
            a.WriteLine("DEPOSITO;COD_MATERIAL;DESC_MATERIAL;UNIDAD;CANTIDAD")
            For i = 0 To DataGridView1.RowCount - 1
                a.WriteLine(DataGridView1.Item(0, i).Value.ToString + ";" + DataGridView1.Item(1, i).Value.ToString + ";" + DataGridView1.Item(2, i).Value.ToString + ";" + DataGridView1.Item(3, i).Value.ToString + ";" + DataGridView1.Item(4, i).Value.ToString)
            Next
            a.Close()
            mensaje.MADVE002(fichero)
            Me.DataGridView1.Rows.Clear()
            TextBox1.Text = Nothing
            LLENAR_MATERIAL()
            Button1.Enabled = False



        End If
    End Sub
End Class