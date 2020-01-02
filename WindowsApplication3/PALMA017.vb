Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.Data.Sql
Imports System.IO
Imports System
Imports System.Collections
Imports Microsoft.VisualBasic


Public Class PALMA017

    Private metodos As New Clas_Almacen
    Private mensaje As New Clase_mensaje
    Private medidor As New Clas_Medidor

    
    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        Button3.Enabled = True
    End Sub
    'boton salir
    'boton buscar
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Refresh()
        Dim pantalla As New OpenFileDialog
        pantalla.DereferenceLinks = True
        pantalla.Filter = "csv files (*.csv)|*.csv"
        pantalla.FilterIndex = 1
        pantalla.RestoreDirectory = False
        pantalla.ShowDialog()
        If Windows.Forms.DialogResult.OK Then
            TextBox1.Text = pantalla.FileName
        End If
        If DataGridView1.RowCount <> 0 Then
            DataGridView1.Rows.Clear()
            llenar_DW1("1")
        End If
    End Sub

    Private Sub PALMA017_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        medidor.ELIMINAR_MED_SA(1, 0)
        Label3.Text = "USUARIO: " & _usr.Obt_Nombre_y_Apellido
        Label5.Text = "Fecha: " & DateTime.Now().ToShortDateString
        llenar_DW1(11)
        Ult_actualizacion()
        If DataGridView2.Rows.Count <> 0 Then
            Button1.Enabled = True

        Else
            Button1.Enabled = False
            Button3.Enabled = False
            Button4.Enabled = False

        End If
    End Sub


    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        'Dim cont As Integer = 0
        Dim DT_Medidores2 As New DataTable
        Dim nmedidor As String
        Dim codmate As String
        Dim cant As Integer = 0
        Dim estado As String = ""
        If TextBox1.Text <> Nothing Then
            Try
                DT_Medidores2 = metodos.ConstruirDatatable(",", TextBox1.Text)
                If DT_Medidores2.Rows.Count <> 0 Then

                    ProgressBar1.Visible = True
                    ProgressBar1.Minimum = 0
                    ProgressBar1.Value = 0
                    ProgressBar1.Maximum = DT_Medidores2.Rows.Count
                    For i = 0 To DT_Medidores2.Rows.Count - 1
                        ProgressBar1.Value = ProgressBar1.Value + 1
                        nmedidor = DT_Medidores2.Rows(i).Item(7)
                        codmate = DT_Medidores2.Rows(i).Item(3)
                        estado = DT_Medidores2.Rows(i).Item(4)
                        If estado = "DISPONIBLE" Then
                            If medidor.Exite_Medi(nmedidor) = False Then
                                DataGridView1.Rows.Add(codmate, nmedidor.PadLeft(8, "0"))
                                'cont += 1
                                cant = DataGridView2.Rows.Count - 1
                                Dim indice As Integer = 0
                                Dim encontro As Boolean = False
                                For j = 0 To cant
                                    If DataGridView2.Item(0, j).Value = codmate Then
                                        encontro = True
                                        indice = j
                                    End If
                                Next
                                If encontro = False Then
                                    DataGridView2.Rows.Add(codmate, metodos.detalle_material(codmate), -1)
                                Else
                                    DataGridView2.Item(2, indice).Value = DataGridView2.Item(2, indice).Value - 1

                                End If
                            End If
                        End If
                    Next
                    ProgressBar1.Visible = False
                    If Me.DataGridView1.Rows.Count <> 0 Then
                        Button4.Enabled = True
                    Else
                        mensaje.MERRO011()
                        TextBox1.Text = Nothing
                    End If

                Else
                    mensaje.MERRO011()
                End If
            Catch ex As Exception
                mensaje.MERRO001()
            End Try
        Else
            mensaje.MERRO016()
            Button1.Focus()
        End If
    End Sub


    'BOTON DE CONFIRMAR
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Try
            For I = 0 To Me.DataGridView1.Rows.Count - 1
                medidor.Grabar_medidor(Me.DataGridView1.Item(1, I).Value, Me.DataGridView1.Item(0, I).Value, "11", Date.Now, 1, _usr.Obt_Usr)
            Next
            'actualizo los medidores si 
            For i = 0 To Me.DataGridView2.Rows.Count - 1
                If medidor.Serializados_sin_asignar(Me.DataGridView2.Item(0, i).Value, "11") = True Then
                    medidor.MODIFICAR_MED_SA(Me.DataGridView2.Item(0, i).Value, "11", Me.DataGridView2.Item(2, i).Value)
                Else
                    medidor.Grabar_Med_Sin_Asignar("11", Me.DataGridView2.Item(0, i).Value, Me.DataGridView2.Item(2, i).Value, 0, Date.Now, 0)

                End If
            Next
            'elimino lo que esta en 0

            modificar_ultimo_actualizacion()
            Ult_actualizacion()
            medidor.ELIMINAR_MED_SA("11", 0)
            Me.DataGridView1.Rows.Clear()
            Me.DataGridView2.Rows.Clear()
            Button4.Enabled = False
            TextBox1.Text = Nothing
            If Me.DataGridView2.RowCount = 0 Then
                Button1.Enabled = False
                Button3.Enabled = False

            End If
            llenar_DW1("11")

            mensaje.MADVE001()
        Catch ex As Exception
            mensaje.MERRO001()
        End Try
    End Sub
    '########################################FUNCIONES#################################################
    Private Sub llenar_DW1(ByVal a As String)
        DataGridView2.Rows.Clear()
        Dim dataset1 As New DataSet
        Dim mat As Decimal
        Dim des As String
        Dim CANT As Decimal
        Dim remi As Decimal
        Dim fec As Date
        'CONECTO LA BASE
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("SELECT CMATE_109,CANT_109, NREMI_109,F_REMI_109 FROM T_MED_SA_109 WHERE CDEPO_109 =@D1", cnn2)
        adaptadaor.SelectCommand.Parameters.Add(New SqlParameter("D1", a))
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.Fill(dataset1, "T_MED_SA_109")
        For i = 0 To dataset1.Tables("T_MED_SA_109").Rows.Count - 1
            mat = dataset1.Tables("T_MED_SA_109").Rows(i).Item(0)
            des = metodos.detalle_material(mat)
            CANT = CDec(dataset1.Tables("T_MED_SA_109").Rows(i).Item(1))
            remi = dataset1.Tables("T_MED_SA_109").Rows(i).Item(2)
            fec = dataset1.Tables("T_MED_SA_109").Rows(i).Item(3)
            DataGridView2.Rows.Add(mat, des, CANT, remi, fec)
        Next
        cnn2.Close()
    End Sub
    Private Sub Ult_actualizacion()
        Dim cnn1 As SqlConnection = New SqlConnection(conexion)
        'Abrimos la conexion
        cnn1.Open()
        'Creamos el comando para crear la consulta
        Dim Comando As New SqlClient.SqlCommand("select F_Proceso from ULT_PROCESOS WHERE Proceso = 1", cnn1)
        'Ejecutamos el commnad y le pasamos el parametro
        Comando.ExecuteNonQuery()
        Dim Dusrs As SqlDataReader = Comando.ExecuteReader
        If Dusrs.Read.ToString Then
            Label7.Text = "Ultima actualzación: " + Dusrs.GetDateTime(0).ToShortDateString
        End If
        cnn1.Close()
    End Sub
    Private Sub modificar_ultimo_actualizacion()
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("Update ULT_PROCESOS set F_Proceso = @D1, usuario = @D2 WHERE Proceso = 1", con1)
        comando1.Parameters.Add(New SqlParameter("D1", Date.Now))
        comando1.Parameters.Add(New SqlParameter("D2", _usr.Obt_Usr))
        'creo el lector de parametros
        comando1.ExecuteNonQuery()
        con1.Close()
    End Sub

End Class