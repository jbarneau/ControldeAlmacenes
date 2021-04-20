Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.Data.Sql
Public Class PALMA004BIS
    Private cantidad As Decimal
    Private material As String
    Private almacen As String
    Private ESTADO As Integer
    Private conf As Boolean = False
    Private MENSAJE As New Clase_mensaje
    Private Metodos As New Clas_Almacen
    Private medidores As DataSet
    Private Sub PALMA004BIS_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'PALMA004.serie
        'PALMA004.material
        llenarLIST()
        TextBox2.Text = 0
    End Sub
    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged
        Dim cont As Integer = 0
        For i = 0 To ListBox1.Items.Count - 1
            If ListBox1.GetSelected(i) = True Then
                cont += 1
            End If
        Next
        TextBox2.Text = cont
        TextBox1.Text = cantidad - TextBox2.Text
    End Sub
    '#####################################FUNCIONES#############################FUNCIONES##########################
    Public Sub grabardatos(ByVal mate As String, ByVal cant As Decimal, ByVal alma As String, EST As UInteger)
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
    '########################BOTONES##########################BOTONES########################################
    Private Sub B_Entregar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B_Entregar.Click
        If TextBox2.Text = cantidad Then
            conf = True
            For i = 0 To ListBox1.Items.Count - 1
                If ListBox1.GetSelected(i) = True Then
                    MAIN.material.Add(material)
                    MAIN.serie.Add(CDec(ListBox1.Items(i)))
                End If
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
End Class