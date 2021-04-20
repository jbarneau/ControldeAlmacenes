
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Module ADM
    Public partidos_obtenidos As New ArrayList
    Public prov_cb As New ArrayList
    Public localidades_obtenidas As New ArrayList
    Public var_partido As String
    Public var_provincia As String
    Public desc_localidad As String
    Public desc_partido As String
    Public desc_provincia As String
    Public tipo_materiales As New ArrayList
    Public material_obtenido As String
    Public material_critico As String
    Public almacen As New ArrayList
    Public cod_almacen As String
    Public depositos_encontrados As New ArrayList

    '.................................................................................................
    'USADO PARA ADMINISTRADOR
    Public consulta_provincias As String = "SELECT C_PARA_802,DESC_802 FROM DET_PARAMETRO_802 where C_TABLA_802 = 6 order by DESC_802"
    Public consulta_cod_provincias As String = "SELECT C_PARA_802 FROM DET_PARAMETRO_802 WHERE (C_TABLA_802 = 6) AND (DESC_802 = @C1) "
    Public consulta_users As String = "select DESC_802 from DET_PARAMETRO_802 where (C_TABLA_802 = 3) and (F_BAJA_802 IS NULL)"
    Public partidos As String = "select CPARA_802, DESC_802 from DET_PARAMETRO_802 where (C_TABLA_802 =7)  and (CODVINC_802 = @C1) and (CTVINC_802 = 6) and (F_BAJA_802 IS NULL)ORDER BY DESC_802 "
    Dim localidades As String = "select C_PARA_802, DESC_802 from DET_PARAMETRO_802 where (C_TABLA_802 = 8) and (CTVINC_802 = 7) and (CODVINC_802 = @C1) and (F_BAJA_802 IS NULL) ORDER BY DESC_802 "
    Dim localidad As String = "select DESC_802 from DET_PARAMETRO_802 where (C_TABLA_802 = 8) and (CTVINC_802 = 7) and (C_PARA_802= @C1) and (F_BAJA_802 IS NULL)"
    Dim partido As String = "select DESC_802 from DET_PARAMETRO_802 where (C_TABLA_802 =7)  and (C_PARA_802 = @C1) and (CTVINC_802 = 6) and (F_BAJA_802 IS NULL) "
    Dim provincia As String = "select DESC_802 from DET_PARAMETRO_802 where (C_TABLA_802 =6)  and (C_PARA_802 = @C1) "
    Dim tipo_material As String = "select DESC_802 from DET_PARAMETRO_802 where C_TABLA_802 =2 "
    Dim material As String = "select DESC_802 from DET_PARAMETRO_802 where (C_TABLA_802 =2) and (C_PARA_802 =@C1) "
    Dim C_material As String = "select CMATE_002 from M_MATE_002 where DESC_002 =@C1 "
    '...................................................................

    'MODULOS ADMINISTRADOR
    'TRAER PROVINCIAS

    Public Sub traer_provincias()
        prov_cb.Clear()
        Dim cnn1 As SqlConnection = New SqlConnection(conexion)
        cnn1.Open()
        Dim consulta As New SqlClient.SqlCommand(consulta_provincias, cnn1)
        consulta.ExecuteNonQuery()
        Dim provincias As SqlDataReader = consulta.ExecuteReader()
        While (provincias.Read())
            prov_cb.Add(provincias.GetValue(1))

        End While
        cnn1.Close()
    End Sub

    '..........................................................................................
    'TRAER PARTIDOS
    Public Sub traer_partidos(ByVal codigo_provincia As Integer)

        'TRAIGO LOS PARTIDOS
        partidos_obtenidos.Clear()
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        cnn2.Open()
        Dim traer_partidos As New SqlClient.SqlCommand(partidos, cnn2)
        traer_partidos.Parameters.Add(New SqlParameter("C1", codigo_provincia))
        traer_partidos.ExecuteNonQuery()
        Dim descripcion_partidos As SqlDataReader = traer_partidos.ExecuteReader()
        While descripcion_partidos.Read()
            partidos_obtenidos.Add(descripcion_partidos.GetValue(0))
        End While
        cnn2.Close()

    End Sub
    'TRAER LOCALIDADES
    Public Sub traer_localidades(ByVal cod_partido As Integer)
        localidades_obtenidas.Clear()
        Dim cnn3 As SqlConnection = New SqlConnection(conexion)
        cnn3.Open()
        Dim traer_localidades As New SqlClient.SqlCommand(localidades, cnn3)
        traer_localidades.Parameters.Add(New SqlParameter("C1", cod_partido))
        traer_localidades.ExecuteNonQuery()
        Dim descripcion_localidades As SqlDataReader = traer_localidades.ExecuteReader()
        While descripcion_localidades.Read()
            localidades_obtenidas.Add(descripcion_localidades.GetValue(1))
        End While
        cnn3.Close()

    End Sub

    'TRAER LOCALIDAD
    Public Sub traer_localidad(ByVal d_localidad As Integer)

        Dim cnn4 As SqlConnection = New SqlConnection(conexion)
        cnn4.Open()
        Dim traer_localidad As New SqlClient.SqlCommand(localidad, cnn4)
        traer_localidad.Parameters.Add(New SqlParameter("C1", d_localidad))
        traer_localidad.ExecuteNonQuery()
        Dim descripcion_localidad As SqlDataReader = traer_localidad.ExecuteReader()
        If descripcion_localidad.Read() Then
            desc_localidad = descripcion_localidad.GetString(0)
        End If
        cnn4.Close()

        'FIN 
    End Sub

    'TRAER PARTIDO

    Public Sub traer_partido(ByVal d_partido As Integer)

        Dim cnn5 As SqlConnection = New SqlConnection(conexion)
        cnn5.Open()
        Dim traer_partido As New SqlClient.SqlCommand(partido, cnn5)
        traer_partido.Parameters.Add(New SqlParameter("C1", d_partido))
        traer_partido.ExecuteNonQuery()
        Dim descripcion_partido As SqlDataReader = traer_partido.ExecuteReader()
        If descripcion_partido.Read() Then
            desc_partido = descripcion_partido.GetString(0)
        End If
        cnn5.Close()

        'FIN 
    End Sub

    Public Sub traer_provincia(ByVal d_provincia As Integer)

        Dim cnn6 As SqlConnection = New SqlConnection(conexion)
        cnn6.Open()
        Dim traer_provincia As New SqlClient.SqlCommand(provincia, cnn6)
        traer_provincia.Parameters.Add(New SqlParameter("C1", d_provincia))
        traer_provincia.ExecuteNonQuery()
        Dim descripcion_provincia As SqlDataReader = traer_provincia.ExecuteReader()
        If descripcion_provincia.Read() Then
            desc_provincia = descripcion_provincia.GetString(0)
        End If
        cnn6.Close()

        'FIN 
    End Sub

    Public Sub traer_tipo_material()

        Dim cnn7 As SqlConnection = New SqlConnection(conexion)
        cnn7.Open()
        Dim traer_material As New SqlClient.SqlCommand(tipo_material, cnn7)
        traer_material.ExecuteNonQuery()
        Dim descripcion_material As SqlDataReader = traer_material.ExecuteReader()
        While descripcion_material.Read()
            tipo_materiales.Add(descripcion_material.GetString(0))
        End While
        cnn7.Close()
        'FIN 
    End Sub

    Public Sub traer_material(ByVal tipo_material As Integer)

        Dim cnn7 As SqlConnection = New SqlConnection(conexion)
        cnn7.Open()
        Dim traer_material As New SqlClient.SqlCommand(material, cnn7)
        traer_material.Parameters.Add(New SqlParameter("C1", tipo_material))
        traer_material.ExecuteNonQuery()
        Dim descripcion_material As SqlDataReader = traer_material.ExecuteReader()
        If descripcion_material.Read() Then
            material_obtenido = descripcion_material.GetString(0)
        End If
        cnn7.Close()
        'FIN 
    End Sub

    Public Sub traer_codigo_material(ByVal material_seleccionado As String)
        Dim cnn7 As SqlConnection = New SqlConnection(conexion)
        cnn7.Open()
        Dim traer_material As New SqlClient.SqlCommand(C_material, cnn7)
        traer_material.Parameters.Add(New SqlParameter("C1", material_seleccionado))
        traer_material.ExecuteNonQuery()
        Dim descripcion_material As SqlDataReader = traer_material.ExecuteReader()
        If descripcion_material.Read() Then
            material_critico = descripcion_material.GetString(0)
        End If
        cnn7.Close()
        'FIN 
    End Sub
    Public Sub traer_almacenes()
        almacen.Clear()
        Dim cnn8 As SqlConnection = New SqlConnection(conexion)
        cnn8.Open()
        Dim traer_almacen As New SqlClient.SqlCommand("select NOMB_003 from M_PERS_003 where (F_BAJA_003 is NULL) and (ALMA_003 = 1)  ", cnn8)
        traer_almacen.ExecuteNonQuery()
        Dim almacenes As SqlDataReader = traer_almacen.ExecuteReader()
        While almacenes.Read()
            almacen.Add(almacenes.GetString(0))
        End While
        cnn8.Close()
        'FIN 
    End Sub

    Public Sub traer_codigo_almacen(ByVal alma As String)
        cod_almacen = Nothing
        Dim cnn9 As SqlConnection = New SqlConnection(conexion)
        cnn9.Open()
        Dim traer_C_almacen As New SqlClient.SqlCommand("select NDOC_003 from M_PERS_003 where NOMB_003 =@C1  ", cnn9)
        traer_C_almacen.Parameters.Add(New SqlParameter("C1", alma))
        traer_C_almacen.ExecuteNonQuery()
        Dim C_almacenes As SqlDataReader = traer_C_almacen.ExecuteReader()
        While C_almacenes.Read()
            cod_almacen = C_almacenes.GetString(0)
        End While
        cnn9.Close()
        'FIN 
    End Sub

    Public Sub traer_depositos()
        depositos_encontrados.Clear()
        Dim cnn10 As SqlConnection = New SqlConnection(conexion)
        cnn10.Open()
        Dim traer_almacen As New SqlClient.SqlCommand("select NOMB_003 from M_PERS_003 where (F_BAJA_003 is NULL) and (DEPO_003 = 1)  ", cnn10)
        traer_almacen.ExecuteNonQuery()
        Dim almacenes As SqlDataReader = traer_almacen.ExecuteReader()
        While almacenes.Read()
            depositos_encontrados.Add(almacenes.GetString(0))
        End While
        cnn10.Close()
        'FIN 
    End Sub
End Module
