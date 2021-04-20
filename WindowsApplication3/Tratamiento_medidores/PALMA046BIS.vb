Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.Data.Sql

Public Class PALMA046BIS

    Private med_rettirar As New Clase_med_retirar
    Dim CONTRATO As String = ""
    Dim OT As String = "SO"
    Dim FECHACARGA As Date = Date.Now
    Dim FECHAREALIZADO As Date = Date.Now
    Dim POLIZA As String = "SP"
    Dim CANTIDAD As Decimal = 0
    Dim Capacidad As String = "400015"
    Dim total As Integer
    Dim operario As String = "SO"
    Dim nmed As String
    Dim listaestados As List(Of String)

    Private Sub PALMA046BIS_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'LLENAR_CONTRATO()
        'CONTRATO = cmbfamilia.SelectedValue()
    End Sub

    Public Sub New(ByVal Nmed As String)
        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()
        txtnmed.Text = Nmed

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
    End Sub



    Private Sub btnguardar_Click(sender As Object, e As EventArgs) Handles btnguardar.Click
        If txtnmed.Text <> Nothing And txtestado.Text <> Nothing Then
            Dim CNN As New SqlConnection(conexion)
            Try
                CNN.Open()
                Dim ADT As New SqlCommand("INSERT INTO MED_NO_ENCONTRADO (NMEDIDOR,ESTADO,FECHA,USUARIO,LECTURA) VALUES (@D1,@D2,@D3,@D4,@D5)", CNN)
                ADT.Parameters.AddWithValue("D1", txtnmed.Text)
                ADT.Parameters.AddWithValue("D2", 0)
                ADT.Parameters.AddWithValue("D3", Date.Today)
                ADT.Parameters.AddWithValue("D4", _usr.Obt_Usr)
                ADT.Parameters.AddWithValue("D5", txtestado.Text)
                ADT.ExecuteNonQuery()


            Catch ex As Exception
                MessageBox.Show(ex.Message)
            Finally
                CNN.Close()
            End Try
            Me.Close()
        Else
            MessageBox.Show("CAMPOS VACIOS", "OK", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub



End Class