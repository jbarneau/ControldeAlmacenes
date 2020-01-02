Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.Data.Sql


Public Class PALMA013BIS
    Private cantidad As Decimal
    Private material As String
    Private almacen As String
    Private ESTADO As Integer
    Private conf As Boolean = False
    Private MENSAJE As New Clase_mensaje
    Private Metodos As New Clas_Almacen
    Private medidor As New Clas_Medidor
    Private medidores As DataSet
    Private Sub Label4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub PALMA013BIS_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        llenarLIST()
        ListBox1.Sorted = True
        contadores()

    End Sub
    Public Sub grabardatos(ByVal mate As String, ByVal cant As Decimal, ByVal alma As String, ByVal EST As UInteger)
        cantidad = cant
        material = mate
        almacen = alma
        ESTADO = EST
    End Sub
    Public Function validar() As Boolean
        Return conf
    End Function
    Public Sub llenarLIST()
        Dim numero As String

        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        cnn2.Open()
        'Consulta SQL...
        Dim comando2 As New SqlClient.SqlCommand("select NSERIE_102 FROM T_MEDI_102 WHERE CMATE_102= @D1 AND CALMA_102=@D2 AND ESTADO_102 = @D3", cnn2)
        comando2.Parameters.Add(New SqlParameter("D1", material))
        comando2.Parameters.Add(New SqlParameter("D2", almacen))
        comando2.Parameters.Add(New SqlParameter("D3", ESTADO))
        comando2.ExecuteNonQuery()
        Dim LECTOR As SqlDataReader = comando2.ExecuteReader()
        While (LECTOR.Read())
            numero = LECTOR.GetValue(0)
            ListBox1.Items.Add(numero.PadLeft(8, "0"))
        End While
        cnn2.Close()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If TextBox2.Text = cantidad Then
            conf = True
            For i = 0 To ListBox2.Items.Count - 1
                MAIN.material.Add(material)
                MAIN.serie.Add(CDec(ListBox2.Items(i)))
            Next
            Me.Close()
        Else
            Dim res As DialogResult
            res = MessageBox.Show("La cantidad solicitada no coincide con la seleccionada, ¿Desea Salir?", "MADVE002", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
            If res = System.Windows.Forms.DialogResult.Yes Then
                conf = False
                Me.Close()
            End If
        End If
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        For I = 0 To ListBox1.Items.Count - 1
            If ListBox1.GetSelected(I) = True Then
                ListBox2.Items.Add(ListBox1.Items(I))
            End If
        Next
        For i = 0 To ListBox2.Items.Count - 1
            For g = ListBox1.Items.Count - 1 To 0 Step -1
                If ListBox1.Items(g) = ListBox2.Items(i) Then
                    ListBox1.Items.RemoveAt(g)
                End If
            Next
        Next
        contadores()
        ListBox1.Sorted = True
        ListBox2.Sorted = True
    End Sub

    Private Sub contadores()
        TextBox2.Text = ListBox2.Items.Count
        TextBox3.Text = cantidad - ListBox2.Items.Count
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        For I = 0 To ListBox2.Items.Count - 1
            If ListBox2.GetSelected(I) = True Then
                ListBox1.Items.Add(ListBox2.Items(I))
            End If
        Next
        For i = 0 To ListBox1.Items.Count - 1
            For g = ListBox2.Items.Count - 1 To 0 Step -1
                If ListBox2.Items(g) = ListBox1.Items(i) Then
                    ListBox2.Items.RemoveAt(g)
                End If
            Next
        Next
        contadores()
        ListBox1.Sorted = True
        ListBox2.Sorted = True

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim nmed As Decimal
        If NumericUpDown1.Value <= cantidad And NumericUpDown1.Value > 0 Then
            'VERIFICO QUE SE INGRESO UN NUMERO DE MEDIDOR
            If IsNumeric(TextBox1.Text) = True Then
                'VOY AGREGANDO DE A UN MEDIDOR
                For i = 0 To NumericUpDown1.Value - 1
                    'AL PRIMER MEDIDOR LE SUMO UNO HASTA LLEGAR A LA CANTIDAD
                    nmed = CDec(TextBox1.Text) + i
                    'ME FIJO SI EL DATA DE MEDIDORES ESTA LLENO O ES EL PRIMERO

                    If medidor.Ver_Disp_Medi(material, nmed, almacen, 1) = True Then
                        'agrego el item a la datview
                        If val_medi_data(nmed) = False Then
                            ListBox2.Items.Add(CStr(nmed).PadLeft(8, "0"))
                        End If
                    End If

                Next
                For i = 0 To ListBox2.Items.Count - 1
                    For g = ListBox1.Items.Count - 1 To 0 Step -1
                        If ListBox1.Items(g) = ListBox2.Items(i) Then
                            ListBox1.Items.RemoveAt(g)
                        End If
                    Next
                Next
                contadores()
                TextBox1.Text = Nothing
                NumericUpDown1.Value = 0
            Else
                MENSAJE.MERRO006()
                TextBox1.SelectAll()
                TextBox1.Focus()
            End If
        Else
            MENSAJE.MERRO006()
            NumericUpDown1.Focus()
        End If
    End Sub

    Private Function val_medi_data(ByVal med As String) As Boolean
        med = med.PadLeft(8, "0")
        Dim resp As Boolean = False
        For i = 0 To ListBox2.Items.Count - 1
            If med = ListBox2.Items(i) Then
                resp = True
            End If
        Next
        Return resp
    End Function
End Class