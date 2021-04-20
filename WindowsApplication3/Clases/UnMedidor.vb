Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data.SqlTypes

Public Class UnMedidor

    Private Estado As Integer
    Private Cmate As String
    Private Existe As Boolean = False
    Public ReadOnly Property LeerExiste
        Get
            Return Existe
        End Get
    End Property
    Public ReadOnly Property LeerEstado
        Get
            Return Estado
        End Get
    End Property
    Public Sub New(nmedidor As Decimal)
        Dim cnn As New SqlConnection(conexion)
        Try
            cnn.Open()
            Dim adt As New SqlCommand("SELECT CMATE_102,ESTADO_102 FROM T_MEDI_102 WHERE NSERIE_102=@D1", cnn)
            adt.Parameters.AddWithValue("D1", nmedidor)
            Dim LECTOR As SqlDataReader = adt.ExecuteReader
            If LECTOR.Read.ToString Then
                Existe = True
                Estado = LECTOR.GetValue(1)
                Cmate = LECTOR.GetValue(0)
            Else
                Existe = False
            End If

        Catch ex As Exception
            Existe = False
        Finally
            cnn.Close()
        End Try
    End Sub

End Class
