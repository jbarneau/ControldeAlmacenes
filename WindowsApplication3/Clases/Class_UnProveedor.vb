Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports System.IO
Public Class Class_UnProveedor
    Private vcuit As String
    Private vrazon As String
    Private vdir As String
    Private vloca As Integer
    Private vcontac As String
    Private vtel As String
    Private VFALTA As Date
    Private VFBAJA As Date
    Private cod_partido As Integer
    Private cod_PROVINCIA As Integer
    Private peti As Integer


    Public Function Existe_proveedor(ByVal dni As String) As Boolean
        Dim resp As Boolean = False
        Dim cnn1 As SqlConnection = New SqlConnection(conexion)
        Try

            'Abrimos la conexion
            cnn1.Open()
            'Creamos el comando para crear la consulta
            Dim Comando As SqlCommand = cnn1.CreateCommand
            Comando.CommandText = "select RAZO_005, DIRE_005,LOCA_005, CONT_005,TELE_005, F_ALTA_005,F_BAJA_005, PROV_005, PART_005, SPETI_005 from M_PROV_005 where CUIT_005 = @DU "
            'Ejecutamos el commnad y le pasamos el parametro
            Comando.Parameters.Add(New SqlParameter("DU", dni))
            Comando.ExecuteNonQuery()
            Dim LECTOR As SqlDataReader = Comando.ExecuteReader
            If LECTOR.Read.ToString = True Then
                resp = True
                vcuit = dni
                vrazon = LECTOR.GetValue(0)
                vdir = LECTOR.GetValue(1)
                vloca = LECTOR.GetInt32(2)
                vcontac = LECTOR.GetValue(3)
                vtel = LECTOR.GetValue(4)
                VFALTA = LECTOR.GetDateTime(5)
                If IsDBNull(LECTOR.GetValue(6)) Then
                    VFBAJA = Nothing
                Else
                    VFBAJA = LECTOR.GetDateTime(6)
                End If
                cod_PROVINCIA = LECTOR.GetInt32(7)
                cod_partido = LECTOR.GetInt32(8)
                peti = LECTOR.GetValue(9)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn1.Close()
        End Try

        Return resp
    End Function

    Public ReadOnly Property OBT_RAZON
        Get
            Return vrazon
        End Get
            End Property
    Public ReadOnly Property OBT_DIRECCION
        Get
            Return vdir
        End Get
    End Property
    Public ReadOnly Property OBT_CONTACTO
        Get
            Return vcontac
        End Get
    End Property
    Public ReadOnly Property OBT_TELEFONO
        Get
            Return vtel
        End Get
    End Property
    Public ReadOnly Property OBT_FALTA
        Get
            Return VFALTA
        End Get
    End Property
    Public ReadOnly Property OBT_FBAJA
        Get
            Return VFBAJA
        End Get
    End Property
    Public ReadOnly Property OBT_LOCALIDAD
        Get
            Return vloca
        End Get
    End Property
    Public ReadOnly Property OBT_PARTIDO
        Get
            Return cod_partido
        End Get
    End Property
    Public ReadOnly Property OBT_PROVINCIA
        Get
            Return cod_PROVINCIA
        End Get
    End Property
    Public ReadOnly Property OBT_PETICION
        Get
            Return peti
        End Get
    End Property
End Class
