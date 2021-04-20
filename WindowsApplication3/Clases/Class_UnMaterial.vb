Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports System.IO

Public Class Class_UnMaterial
    Private codmates As String
    Private calt As String
    Private desc As String
    Private tipo As Integer = 1
    Private seri As Boolean = False
    Private cons As Boolean = False
    Private deci As Boolean = False
    Private falta As Date
    Private fbaja As Date
    Private unidad As String
    Public Function Existe_material(ByVal codmate As String) As Boolean
        Dim resp As Boolean = False
        Try
            Dim cnn1 As SqlConnection = New SqlConnection(conexion)
            'Abrimos la conexion
            cnn1.Open()
            'Creamos el comando para crear la consulta
            Dim Comando As SqlCommand = cnn1.CreateCommand
            Comando.CommandText = "select CALTE_002, DESC_002, TIPO_002, SERI_002, CONS_002, DECI_002, F_ALTA_002, F_BAJA_002, UNID_002 from M_MATE_002 where CMATE_002 = @COD_MATE"
            Comando.Parameters.Add(New SqlParameter("COD_MATE", codmate))
            Comando.ExecuteNonQuery()
            Dim Dusrs As SqlDataReader = Comando.ExecuteReader
            If Dusrs.Read.ToString Then
                resp = True
                codmates = codmate
                calt = Dusrs.GetValue(0)
                desc = Dusrs.GetValue(1)
                tipo = Dusrs.GetInt32(2)
                seri = Dusrs.GetBoolean(3)
                cons = Dusrs.GetBoolean(4)
                deci = Dusrs.GetBoolean(5)
                falta = Dusrs.GetDateTime(6)
                If IsDBNull(Dusrs.GetValue(7)) Then
                    fbaja = Nothing
                Else
                    fbaja = Dusrs.GetDateTime(7)
                End If
                unidad = Dusrs.GetValue(8)
            End If
                cnn1.Close()
        Catch ex As Exception
            MessageBox.Show("aca esta el error")
        End Try
        Return resp
    End Function

    Public ReadOnly Property alternativo
        Get
            Return calt

        End Get
    End Property
    Public ReadOnly Property descripcion
        Get
            Return desc

        End Get
    End Property
    Public ReadOnly Property codtipo
        Get
            Return tipo

        End Get
    End Property
    Public ReadOnly Property serializado
        Get
            Return seri

        End Get
    End Property
    Public ReadOnly Property consumible
        Get
            Return cons

        End Get
    End Property
    Public ReadOnly Property tienedecimal
        Get
            Return deci

        End Get

    End Property
    Public ReadOnly Property fechadealta
        Get
            Return falta

        End Get
    End Property
    Public ReadOnly Property fechadebaja
        Get
            Return fbaja

        End Get
    End Property
    Public ReadOnly Property unidadmedida
        Get
            Return unidad

        End Get
    End Property
    Public ReadOnly Property codsap
        Get
            Return codmates

        End Get
    End Property
End Class
