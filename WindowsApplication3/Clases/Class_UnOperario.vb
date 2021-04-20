Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports System.IO
Public Class Class_UnOperario
    Private Vdni As String
    Private VNombre As String
    Private VApellido As String
    Private Vlegajo As String
    Private VDomicilio As String
    Private Vesalma As Boolean
    Private VFalta As Date
    Private VFbaja As Date
    Private Vlocal As String
    Private Vpart As String
    Private Vprov As String
    

    Public Function Existe_USR(ByVal dni As String) As Boolean
        Dim resp As Boolean = False
        Dim cnn1 As SqlConnection = New SqlConnection(conexion)
        'Abrimos la conexion
        cnn1.Open()
        'Creamos el comando para crear la consulta
        Dim Comando As SqlCommand = cnn1.CreateCommand
        Comando.CommandText = "select NDOC_003, NOMB_003, APELL_003, DOMI_003, PROV_003 , PART_003, LOCAL_003, LEGA_003, ALMA_003,F_ALTA_003, F_BAJA_003, F_MODI_003 from M_PERS_003 where NDOC_003 = @DU "
        'Ejecutamos el commnad y le pasamos el parametro
        Comando.Parameters.Add(New SqlParameter("DU", dni))
        Comando.ExecuteNonQuery()
        Dim Dusrs As SqlDataReader = Comando.ExecuteReader
        If Dusrs.Read.ToString = True Then
            resp = True
            Vdni = dni
            VNombre = Dusrs.GetValue(1)
            VApellido = Dusrs.GetValue(2)
            VDomicilio = Dusrs.GetValue(3)
            Vprov = Dusrs.GetInt32(4)
            Vpart = Dusrs.GetInt32(5)
            Vlocal = Dusrs.GetInt32(6)
            Vlegajo = Dusrs.GetValue(7)
            Vesalma = Dusrs.GetBoolean(8)
            VFalta = Dusrs.GetDateTime(9)
            If IsDBNull(Dusrs.GetValue(10)) Then
                VFbaja = Nothing
            Else
                VFbaja = Dusrs.GetDateTime(10)
            End If
            
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
    Public ReadOnly Property direccion
        Get
            Return VDomicilio
        End Get
    End Property
    Public ReadOnly Property provincia
        Get
            Return Vprov
        End Get
    End Property
    Public ReadOnly Property partido
        Get
            Return Vpart
        End Get
    End Property
    Public ReadOnly Property localidad
        Get
            Return Vlocal
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
    Public ReadOnly Property legajo
        Get
            Return Vlegajo
        End Get
    End Property
    Public ReadOnly Property esalmacen
        Get
            Return Vesalma
        End Get
    End Property

End Class
