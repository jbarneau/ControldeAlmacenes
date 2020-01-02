Imports System.Data.SqlClient

Public Class PALMA036BIS2
    Private cajon As String
    Private Data As New DataTable
    Private nrem As String

    Public Sub New(ByVal DATA As String, ByVal Nrem As String)

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()
        Prop() = DATA
        Proprem() = Nrem
        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
    End Sub

    Public Property Proprem() As String
        Get
            Return nrem
        End Get
        Set(ByVal value As String)
            nrem = value
        End Set
    End Property

    Public Property Prop() As String
        Get
            Return cajon
        End Get
        Set(ByVal value As String)
            cajon = value
        End Set
    End Property
    Private Sub PALMA036BIS2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MostrarMedidores(Proprem)
    End Sub

    Private Sub MostrarMedidores(ByVal nremi As String)

        ListView1.Items.Clear()
        Data.Clear()
        Dim contrato As String
        Dim linea As New ListViewItem
        Dim cnn As SqlConnection = New SqlConnection(conexion)
        cnn.Open()
        Dim cmd As New SqlCommand("SELECT NSERI_113, CMATE_113, CONTRATO_113 FROM T_MED_DEVO_113 WHERE (NREMITO_113 = @param)", cnn)
        cmd.Parameters.AddWithValue("param", nremi)
        Dim adaptador As New SqlDataAdapter(cmd)
        adaptador.Fill(Data)
        cnn.Close()
        For i = 0 To Data.Rows.Count - 1
            If (Data.Rows(i).Item(2).ToString = "00") Then
                contrato = "TRATAMIENTO DE MEDIDORES"
            End If
            If (Data.Rows(i).Item(2).ToString = "01") Then
                contrato = "UTILIZACION"
            End If
            If (Data.Rows(i).Item(2).ToString = "02") Then
                contrato = "SEGUIMIENTO DE DEUDA"
            End If
            If (Data.Rows(i).Item(2).ToString = "03") Then
                contrato = "GUARDIA - RETEN"
            End If
            If (Data.Rows(i).Item(2).ToString = "04") Then
                contrato = "LABORATORIO"
            End If
            If (Data.Rows(i).Item(2).ToString = "05") Then
                contrato = "MEDICION"
            End If
            linea = New ListViewItem(Data.Rows(i).Item(0).ToString)
            linea.SubItems.Add(Data.Rows(i).Item(1).ToString)
            linea.SubItems.Add(contrato)
            ListView1.Items.Add(linea)
        Next

    End Sub

    Private Sub ListView1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles ListView1.MouseDoubleClick
        For index = 0 To Data.Rows.Count - 1
            If ListView1.Items(index).Selected = True Then
                Dim pantalla As New PALMA036BIS3(Convert.ToDecimal(ListView1.Items(index).SubItems.Item(0).Text), ListView1.Items(index).SubItems.Item(1).Text, Proprem(), ListView1.Items(index).SubItems.Item(2).Text)
                pantalla.ShowDialog()
            End If
        Next
    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged

    End Sub
End Class