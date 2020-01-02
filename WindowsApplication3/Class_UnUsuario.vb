
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.Data.Sql
Imports System.IO
Public Class Class_UnUsuario
    Private VNombre As String
    Private VApellido As String
    Private VUser As String
    Private VPass As String
    Private VAlmacen As Integer
    Private VAcceso As Integer
    Private VFalta As Date
    Private VFbaja As Date
    Private VFmodi As Date
    Private VEmail As String
    Private Vsend As Integer
    Private Vdni As String
    Public Function Existe_USR(ByVal dni As String) As Boolean
        Dim resp As Boolean = False
        Dim cnn1 As SqlConnection = New SqlConnection(conexion)
        'Abrimos la conexion
        cnn1.Open()
        'Creamos el comando para crear la consulta
        Dim Comando As SqlCommand = cnn1.CreateCommand
        Comando.CommandText = "select NDOC_001, NOMB_001, APELL_001, ISUR_001, PASS_001, CALM_001, CACC_001,F_ALTA_001,F_BAJA_001,F_MODI_001,EMAIL_001,SEND_001 from M_USRS_001 WHERE NDOC_001= @USRS"
        'Ejecutamos el commnad y le pasamos el parametro
        Comando.Parameters.Add(New SqlParameter("USRS", dni))
        Comando.ExecuteNonQuery()
        Dim Dusrs As SqlDataReader = Comando.ExecuteReader
        If Dusrs.Read.ToString = True Then
            resp = True
            Vdni = dni
            VNombre = Dusrs.GetValue(1)
            VApellido = Dusrs.GetValue(2)
            VUser = Dusrs.GetValue(3)
            VPass = Dusrs.GetValue(4)
            VAlmacen = Dusrs.GetValue(5)
            VAcceso = Dusrs.GetInt32(6)
            VFalta = Dusrs.GetDateTime(7)
            If IsDBNull(Dusrs.GetValue(8)) Then
                VFbaja = Nothing
            Else
                VFbaja = Dusrs.GetDateTime(8)
            End If
            If IsDBNull(Dusrs.GetValue(9)) Then
                VFmodi = Nothing
            Else
                VFmodi = Dusrs.GetDateTime(9)
            End If
            VEmail = Dusrs.GetValue(10)
            Vsend = Dusrs.GetInt32(11)
        End If
        cnn1.Close()

        Return resp
    End Function

    Public ReadOnly Property nombre
        Get
            Return VNombre
        End Get
    End Property
    Public ReadOnly Property apellido
        Get
            Return VApellido
        End Get
    End Property
    Public ReadOnly Property id
        Get
            Return VUser
        End Get
    End Property
    Public ReadOnly Property alta
        Get
            Return VFalta
        End Get
    End Property
    Public ReadOnly Property baja
        Get
            Return VFbaja
        End Get
    End Property
    Public ReadOnly Property clave
        Get
            Return VPass
        End Get
    End Property
    Public ReadOnly Property almacen
        Get
            Return VAlmacen
        End Get
    End Property
    Public ReadOnly Property nivel
        Get
            Return VAcceso
        End Get
    End Property
    Public ReadOnly Property email
        Get
            Return VEmail
        End Get
    End Property
    Public ReadOnly Property send
        Get
            Return Vsend
        End Get
    End Property
End Class
