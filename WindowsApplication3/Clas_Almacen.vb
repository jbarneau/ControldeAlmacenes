Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Public Class Clas_Almacen
    Public Sub Increpmentar_Stock_Material(ByVal Cod_Mat As String, ByVal Cod_Equipo As String, ByVal Cantidad As Decimal, ByVal estado As Integer)
        'creo la cadena de conexion
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("select C_ALMA_103, C_MATE_103 from T_ALMA_103 WHERE C_MATE_103=@MATE AND C_ALMA_103=@EQUI AND ESTA_103=@std", con1)
        'creo el lector de parametros
        comando1.Parameters.Add(New SqlParameter("MATE", Cod_Mat))
        comando1.Parameters.Add(New SqlParameter("EQUI", Cod_Equipo))
        comando1.Parameters.Add(New SqlParameter("std", estado))
        comando1.ExecuteNonQuery()
        'genero un lector
        Dim lector1 As SqlDataReader = comando1.ExecuteReader
        'pregunto si encontro
        If lector1.Read.ToString Then
            Dim con3 As SqlConnection = New SqlConnection(conexion)
            con3.Open()
            'si leeo el lector incremento el stock
            Dim comando2 As New SqlClient.SqlCommand("Update T_ALMA_103 set N_CANT_103=N_CANT_103+@Cantidad WHERE C_MATE_103=@MATE AND C_ALMA_103=@EQUI AND ESTA_103=@std", con3)
            'creo el lector de parametros
            comando2.Parameters.Add(New SqlParameter("MATE", Cod_Mat))
            comando2.Parameters.Add(New SqlParameter("EQUI", Cod_Equipo))
            comando2.Parameters.Add(New SqlParameter("Cantidad", Cantidad))
            comando2.Parameters.Add(New SqlParameter("std", estado))
            comando2.ExecuteNonQuery()
            con3.Close()
        Else
            Dim con2 As SqlConnection = New SqlConnection(conexion)
            con2.Open()
            'si no obtiene datos se genera un stock nuevo
            Dim comando3 As New SqlClient.SqlCommand("insert INTO T_ALMA_103  (N_CANT_103,C_MATE_103,C_ALMA_103,ESTA_103) values(@Cantidad,@MATE,@EQUI,@std)", con2)
            'creo el lector de parametros
            comando3.Parameters.Add(New SqlParameter("MATE", Cod_Mat))
            comando3.Parameters.Add(New SqlParameter("EQUI", Cod_Equipo))
            comando3.Parameters.Add(New SqlParameter("Cantidad", Cantidad))
            comando3.Parameters.Add(New SqlParameter("std", estado))
            comando3.ExecuteNonQuery()
            con2.Close()
        End If
        'ciero la conexion
        con1.Close()
    End Sub
    Public Sub Descontar_Stock_Material(ByVal Cod_Mat As String, ByVal Cod_Equipo As String, ByVal Cantidad As Decimal, ByVal estado As Integer)
        'creo la cadena de conexion
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("Update T_ALMA_103 set N_CANT_103=N_CANT_103-@Cantidad WHERE C_MATE_103=@MATE AND C_ALMA_103=@EQUI AND ESTA_103=@std", con1)
        'creo el lector de parametros
        comando1.Parameters.Add(New SqlParameter("MATE", Cod_Mat))
        comando1.Parameters.Add(New SqlParameter("EQUI", Cod_Equipo))
        comando1.Parameters.Add(New SqlParameter("Cantidad", Cantidad))
        comando1.Parameters.Add(New SqlParameter("std", estado))
        comando1.ExecuteNonQuery()
        con1.Close()
    End Sub
    Public Function Obtener_Numero_Remito() As Decimal
        'lee el ultimo numero de remito
        Dim Numero_Remito As Decimal
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("select NUMERO From NUMERACION where C_NUM=1", con1)
        'creo un lector
        comando1.ExecuteNonQuery()
        Dim lector1 As SqlDataReader = comando1.ExecuteReader
        If lector1.Read.ToString() Then
            Numero_Remito = CDec(lector1.GetValue(0))
        End If
        con1.Close()
        Return Numero_Remito
    End Function
    Public Sub Sumar_Num_Remito()
        'incrementa el numero de remito
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("Update NUMERACION set NUMERO = NUMERO+1 WHERE C_NUM=1", con1)
        comando1.ExecuteReader()
        con1.Close()
    End Sub
    Public Function Saldo(ByVal Cod_Mat As String, ByVal Cod_Equipo As String, ByVal estado As Integer) As Decimal
        'esta funcion tan solo dice si tiene stock de materiales
        Dim resp As Decimal = 0
        'creo la cadena de conexion
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("select  N_CANT_103 FROM T_ALMA_103 WHERE C_MATE_103=@MATE AND C_ALMA_103=@EQUI AND ESTA_103=@std", con1)
        'creo el lector de parametros
        comando1.Parameters.Add(New SqlParameter("MATE", Cod_Mat))
        comando1.Parameters.Add(New SqlParameter("EQUI", Cod_Equipo))
        comando1.Parameters.Add(New SqlParameter("std", estado))
        comando1.ExecuteNonQuery()
        'genero un lector
        Dim lector1 As SqlDataReader = comando1.ExecuteReader
        'pregunto si encontro
        If lector1.Read.ToString Then
            resp = CDec(lector1.GetValue(0))
        End If
        'ciero la conexion
        con1.Close()
        'envio la respuesta
        Return resp
    End Function
    Public Function Tiene_Decimal(ByVal _CodMate As String) As Boolean
        Dim _resp As Boolean = False
        Dim cnn4 As SqlConnection = New SqlConnection(conexion)
        cnn4.Open()
        'Consulta SQL...
        Dim comando1 As New SqlClient.SqlCommand("select CMATE_002 FROM M_MATE_002 WHERE CMATE_002=@D1 AND DECI_002=1", cnn4)
        comando1.Parameters.Add(New SqlParameter("D1", _CodMate))
        comando1.ExecuteNonQuery()
        Dim lector1 As SqlDataReader = comando1.ExecuteReader
        'pregunto si encontro
        If lector1.Read.ToString Then
            _resp = True
        End If
        cnn4.Close()
        Return _resp
    End Function
    Public Function validarMaterial(ByVal Cmate As String) As Boolean
        Dim resp As Boolean
        'creo la cadena de conexion
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("SELECT CMATE_002 FROM M_MATE_002 WHERE CMATE_002=@mate", con1)
        'creo el lector de parametros
        comando1.Parameters.Add(New SqlParameter("mate", Cmate))
        comando1.ExecuteNonQuery()
        'genero un lector
        Dim lector1 As SqlDataReader = comando1.ExecuteReader
        'pregunto si encontro
        If lector1.Read.ToString Then
            resp = True
        Else
            resp = False
        End If
        'ciero la conexion
        con1.Close()
        Return resp
    End Function
    Public Function validarEquipo(ByVal cequipo As String) As Boolean
        Dim resp As Boolean
        'creo la cadena de conexion
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("SELECT NDOC_003 FROM M_PERS_003 WHERE NDOC_003=@EQUI", con1)
        'creo el lector de parametros
        comando1.Parameters.Add(New SqlParameter("EQUI", cequipo))
        comando1.ExecuteNonQuery()
        'genero un lector
        Dim lector1 As SqlDataReader = comando1.ExecuteReader
        'pregunto si encontro
        If lector1.Read.ToString Then
            resp = True
        Else
            resp = False
        End If
        'ciero la conexion
        con1.Close()
        Return resp
    End Function
    Public Function ContDecimales(ByVal Dato As String) As Integer
        Dim Cadena As String
        Cadena = CDec(Dato) - CInt(Dato)
        Return (Cadena.Count - 2)
    End Function
    Public Function Rempl_Punto_Coma(ByVal cadena As String) As String
        If cadena.LastIndexOf(".") <> -1 Then
            cadena = Replace(cadena, ".", ",")
        End If
        Return cadena
    End Function
    Public Function Contar_Coma_Punto(ByVal Cadena As String) As Integer
        Dim Contador As Integer = 0
        For i = 0 To Cadena.Count - 1
            If Cadena(i) = "," Or Cadena(i) = "." Then
                Contador = Contador + 1
            End If
        Next
        Return Contador
    End Function
    Public Sub Grabar_Trans(ByVal NREMI As Decimal, ByVal FALTA As Date, ByVal CMATE As String, ByVal ALMAE As String, ByVal ALMAR As String, ByVal TMOTI As Integer, ByVal FINFO As Date, ByVal PETI As Decimal, ByVal OBSE As String, ByVal MOTI As Integer, ByVal CANT As Decimal, ByVal OC As Decimal, ByVal USER As String, ByVal CONT As String, ByVal RECIBE As String)
        Dim cnn4 As SqlConnection = New SqlConnection(conexion)
        cnn4.Open()
        'Consulta SQL...
        Dim comando4 As New SqlClient.SqlCommand("INSERT INTO T_REMI_104 (N_REMI_104, F_ALTA_104, C_MATE_104, ALMAE_104, ALMAR_104, T_MOV_104, F_INFO_104, N_PETI_104, OBSE_104, MOTI_104, CANT_104, OC_104, USER_104, CONT_104, RECIBE_104) VALUES (@D1,@D2,@D3,@D4,@D5,@D6,@D7,@D8,@D9,@D10,@D11, @D12, @D13, @D14,@D15) ", cnn4)
        comando4.Parameters.Add(New SqlParameter("D1", NREMI))
        comando4.Parameters.Add(New SqlParameter("D2", FALTA))
        comando4.Parameters.Add(New SqlParameter("D3", CMATE))
        comando4.Parameters.Add(New SqlParameter("D4", ALMAE))
        comando4.Parameters.Add(New SqlParameter("D5", ALMAR))
        comando4.Parameters.Add(New SqlParameter("D6", TMOTI))
        comando4.Parameters.Add(New SqlParameter("D7", FINFO))
        comando4.Parameters.Add(New SqlParameter("D8", PETI))
        comando4.Parameters.Add(New SqlParameter("D9", OBSE))
        comando4.Parameters.Add(New SqlParameter("D10", MOTI))
        comando4.Parameters.Add(New SqlParameter("D11", CANT))
        comando4.Parameters.Add(New SqlParameter("D12", OC))
        comando4.Parameters.Add(New SqlParameter("D13", USER))
        comando4.Parameters.Add(New SqlParameter("D14", CONT))
        comando4.Parameters.Add(New SqlParameter("D15", RECIBE))
        comando4.ExecuteNonQuery()
        cnn4.Close()
    End Sub
    Public Sub Descontar_Stock_Contrato(ByVal Cod_Mat As String, ByVal CONTRATO As String, ByVal Cantidad As Decimal)
        'creo la cadena de conexion
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("Update T_SCONT_107 set CANT_107=CANT_107-@D1 WHERE C_MATE_107=@D2 AND CONT_107=@D3", con1)
        'creo el lector de parametros
        comando1.Parameters.Add(New SqlParameter("D1", Cantidad))
        comando1.Parameters.Add(New SqlParameter("D2", Cod_Mat))
        comando1.Parameters.Add(New SqlParameter("D3", CONTRATO))
        comando1.ExecuteNonQuery()
        con1.Close()
    End Sub
    Public Sub Grabar_Trans_Temp(ByVal NREMI As Decimal, ByVal CMATE As String, ByVal CANT As Decimal, ByVal USER As String)
        Dim cnn4 As SqlConnection = New SqlConnection(conexion)
        cnn4.Open()
        'Consulta SQL...
        Dim comando4 As New SqlClient.SqlCommand("INSERT INTO TEMP_TRANSFERENCIA (N_REMI_110, C_MATE_110, CANT_110, USER_110) VALUES (@D1,@D2,@D3,@D4) ", cnn4)
        comando4.Parameters.Add(New SqlParameter("D1", NREMI))
        comando4.Parameters.Add(New SqlParameter("D2", CMATE))
        comando4.Parameters.Add(New SqlParameter("D3", CANT))
        comando4.Parameters.Add(New SqlParameter("D4", USER))
        comando4.ExecuteNonQuery()
        cnn4.Close()
    End Sub
    Public Sub Grabar_Cabezera_Tranf(ByVal nremi As Decimal, ByVal fecha As Date, ByVal almae As String, ByVal almar As String)
        Dim cnn4 As SqlConnection = New SqlConnection(conexion)
        cnn4.Open()
        'Consulta SQL...
        Dim comando4 As New SqlClient.SqlCommand("INSERT INTO C_TRANF_111 (NTRANF111,FECH111,ALMAE111,ALMAR111) VALUES (@D1,@D2,@D3,@D4) ", cnn4)
        comando4.Parameters.Add(New SqlParameter("D1", nremi))
        comando4.Parameters.Add(New SqlParameter("D2", fecha))
        comando4.Parameters.Add(New SqlParameter("D3", almae))
        comando4.Parameters.Add(New SqlParameter("D4", almar))
        comando4.ExecuteNonQuery()
        cnn4.Close()
    End Sub
    Public Sub Eliminar_med_transf(ByVal nremito As Decimal)
        'ELIMINO LOS MEDIDORES
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("DELETE FROM TEMP_TRANS_MED WHERE N_REMI=@D1", con1)
        'creo el lector de parametros
        comando1.Parameters.Add(New SqlParameter("D1", nremito))
        comando1.ExecuteNonQuery()
        con1.Close()
    End Sub
    Public Sub Elimino_C_Trans(ByVal nremito As Decimal)
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("DELETE FROM C_TRANF_111 WHERE NTRANF111=@D1", con1)
        'creo el lector de parametros
        comando1.Parameters.Add(New SqlParameter("D1", nremito))
        comando1.ExecuteNonQuery()
        con1.Close()
    End Sub
    Public Sub Eliminar_D_Trans(ByVal nremito As Decimal)
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("DELETE FROM TEMP_TRANSFERENCIA WHERE N_REMI_110=@D1", con1)
        'creo el lector de parametros
        comando1.Parameters.Add(New SqlParameter("D1", nremito))
        comando1.ExecuteNonQuery()
        con1.Close()
    End Sub
    Public Function tiene_decimal(ByVal cmate As String, ByVal cant As String) As Boolean
        Dim resp As Boolean = False
        If Tiene_Decimal(cmate) = False Then
            If Contar_Coma_Punto(cant) = 0 Then
                resp = True
            End If
        Else
            resp = True
        End If
        Return resp
    End Function
    Public Function detalle_material(ByVal codmat As String) As String
        Dim resp As String = ""
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("SELECT DESC_002 FROM M_MATE_002 WHERE CMATE_002 = @D1", con1)
        'creo el lector de parametros
        comando1.Parameters.Add(New SqlParameter("D1", codmat))
        comando1.ExecuteNonQuery()
        'genero un lector
        Dim lector1 As SqlDataReader = comando1.ExecuteReader
        If lector1.Read.ToString Then
            resp = lector1.GetValue(0)
        End If
        con1.Close()
        Return resp
    End Function

    Public Function ConstruirDatatable(ByVal Separador As Char, ByVal direccion As String) As DataTable
        'declaramos la Tabla donde añadiremos los datos y la fila correspondiente
        Dim MiTabla As DataTable = New DataTable("MyTable")
        Dim MiFila As DataRow
        'declaramos el resto de variables que nos harán falta
        Dim pos As Boolean = False
        Dim i As Integer
        Dim fieldValues As String()
        Dim mireader As New System.IO.StreamReader(direccion)
        Try
            'Abrimos el fichero y leemos la primera linea con el fin de determinar cuantos campos tenemos
            fieldValues = mireader.ReadLine().Split(Separador)
            'Creamos las columnas de la cabecera
            For i = 0 To fieldValues.Length() - 1
                MiTabla.Columns.Add(New DataColumn(fieldValues(i).ToString()))
            Next
            'Continuamos leyendo el resto de filas y añadiendolas a la tabla
            While mireader.Peek() <> -1
                fieldValues = mireader.ReadLine().Split(Separador)
                MiFila = MiTabla.NewRow
                For i = 0 To fieldValues.Length() - 1
                    MiFila.Item(i) = fieldValues(i).ToString
                Next
                MiTabla.Rows.Add(MiFila)
            End While
            'Cerramos el reader
            mireader.Close()
        Catch ex As Exception
            'Gestionamos las excepciones, Aqui cada uno puede hacer lo que crea conveniente: Mostrar un error en Javascript en este caso y devolver el Datatable vacío
            Return New DataTable("Empty")
        Finally
            'Si queremos ejecutar algo exista excepción o no
        End Try
        'Devolvemos el DataTable si todo ha ido bien
        Return MiTabla
    End Function

    Public Function ConstruirDatatableValdes(ByVal Separador As Char, ByVal direccion As String) As DataTable
        'declaramos la Tabla donde añadiremos los datos y la fila correspondiente
        Dim MiTabla As DataTable = New DataTable("MyTable")
        Dim MiFila As DataRow
        'declaramos el resto de variables que nos harán falta
        Dim pos As Boolean = False
        Dim i As Integer
        Dim fieldValues As String()
        Dim mireader As New System.IO.StreamReader(direccion)
        Try
            'DE ESTA MANERA, HARCODEANDO LAS COLUMNAS, NO SE SALTEA LA PRIMERA LÌNEA POR DEFECTO EN LOS CSV
            For i = 0 To 73
                MiTabla.Columns.Add(New DataColumn("columna" + i.ToString))
            Next
            'Continuamos leyendo el resto de filas y añadiendolas a la tabla
            While mireader.Peek() <> -1
                fieldValues = mireader.ReadLine().Split(Separador)
                MiFila = MiTabla.NewRow
                For i = 0 To fieldValues.Length() - 1
                    MiFila.Item(i) = fieldValues(i).ToString
                Next
                MiTabla.Rows.Add(MiFila)
            End While
            'Cerramos el reader
            mireader.Close()
        Catch ex As Exception
            Throw New Exception(ex.Message)
            'Gestionamos las excepciones, Aqui cada uno puede hacer lo que crea conveniente: Mostrar un error en Javascript en este caso y devolver el Datatable vacío
            'Return New DataTable("Empty")
        Finally
            'Si queremos ejecutar algo exista excepción o no
        End Try
        'Devolvemos el DataTable si todo ha ido bien
        Return MiTabla
    End Function

    Public Function ConstruirDatatableSGC(ByVal Separador As Char, ByVal direccion As String) As DataTable
        'declaramos la Tabla donde añadiremos los datos y la fila correspondiente
        Dim MiTabla As DataTable = New DataTable("MyTable")
        Dim MiFila As DataRow
        'declaramos el resto de variables que nos harán falta
        Dim pos As Boolean = False
        Dim i As Integer
        Dim fieldValues As String()
        Dim mireader As New System.IO.StreamReader(direccion)
        Try
            'Abrimos el fichero y leemos la primera linea con el fin de determinar cuantos campos tenemos
            fieldValues = mireader.ReadLine().Split(Separador)
            'Creamos las columnas de la cabecera
            For i = 0 To fieldValues.Length() - 1
                MiTabla.Columns.Add(New DataColumn("columna" + i.ToString))
            Next
            'Continuamos leyendo el resto de filas y añadiendolas a la tabla
            While mireader.Peek() <> -1
                fieldValues = mireader.ReadLine().Split(Separador)
                MiFila = MiTabla.NewRow
                For i = 0 To fieldValues.Length() - 1
                    MiFila.Item(i) = fieldValues(i).ToString
                Next
                MiTabla.Rows.Add(MiFila)
            End While
            'Cerramos el reader
            mireader.Close()
        Catch ex As Exception
            'Gestionamos las excepciones, Aqui cada uno puede hacer lo que crea conveniente: Mostrar un error en Javascript en este caso y devolver el Datatable vacío
            Return New DataTable("Empty")
        Finally
            'Si queremos ejecutar algo exista excepción o no
        End Try
        'Devolvemos el DataTable si todo ha ido bien
        Return MiTabla
    End Function
    Public Function ConstruirDatatableSinColumnas(ByVal Separador As Char, ByVal direccion As String, columnas As Integer) As DataTable
        'declaramos la Tabla donde añadiremos los datos y la fila correspondiente
        Dim MiTabla As DataTable = New DataTable("MyTable")
        Dim MiFila As DataRow
        'declaramos el resto de variables que nos harán falta
        Dim pos As Boolean = False
        Dim i As Integer
        Dim fieldValues As String()
        Dim mireader As New System.IO.StreamReader(direccion)

        Try
            'Continuamos leyendo el resto de filas y añadiendolas a la tabla
            For i = 0 To columnas
                MiTabla.Columns.Add("columna" + i.ToString)
            Next
            While mireader.Peek() <> -1
                fieldValues = mireader.ReadLine().Split(Separador)
                MiFila = MiTabla.NewRow
                For i = 0 To fieldValues.Length() - 1
                    MiFila.Item(i) = fieldValues(i).ToString
                Next
                MiTabla.Rows.Add(MiFila)
            End While
            'Cerramos el reader
            mireader.Close()
        Catch ex As Exception
            'Gestionamos las excepciones, Aqui cada uno puede hacer lo que crea conveniente: Mostrar un error en Javascript en este caso y devolver el Datatable vacío
            Return New DataTable("Empty")
        Finally
            'Si queremos ejecutar algo exista excepción o no
        End Try
        'Devolvemos el DataTable si todo ha ido bien
        Return MiTabla
    End Function

    Public Function NOMBRE_DEPOSITO(ByVal str As String) As String
        Dim resp As String = "error"
        Dim cnn1 As SqlConnection = New SqlConnection(conexion)
        'Abrimos la conexion
        cnn1.Open()
        'Creamos el comando para crear la consulta
        Dim Comando As New SqlClient.SqlCommand("select (APELL_003+ ' ' +NOMB_003) AS NOMBRE FROM M_PERS_003 WHERE NDOC_003 = @D1", cnn1)
        'Ejecutamos el commnad y le pasamos el parametro
        Comando.Parameters.Add(New SqlParameter("D1", str))
        Dim Dusrs As SqlDataReader = Comando.ExecuteReader
        If Dusrs.Read.ToString Then
            resp = Dusrs.GetString(0)
        End If
        cnn1.Close()
        Return resp
    End Function
    Public Function Usuario(ByVal dni As String) As String
        Dim resp As String = ""
        Dim cnn1 As SqlConnection = New SqlConnection(conexion)
        'Abrimos la conexion
        cnn1.Open()
        'Creamos el comando para crear la consulta
        Dim Comando As SqlCommand = cnn1.CreateCommand
        Comando.CommandText = "select (APELL_001+' '+NOMB_001) as nomb from M_USRS_001 WHERE NDOC_001= @USRS"
        'Ejecutamos el commnad y le pasamos el parametro
        Comando.Parameters.Add(New SqlParameter("USRS", dni))
        Comando.ExecuteNonQuery()
        Dim Dusrs As SqlDataReader = Comando.ExecuteReader
        If Dusrs.Read Then
            resp = Dusrs.GetValue(0)
        End If
        cnn1.Close()
        Return resp
    End Function
    Public Sub Incrementar_Stock_Contrato(ByVal Cod_Mat As String, ByVal CONTRATO As String, ByVal Cantidad As Decimal)
        'creo la cadena de conexion
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("Update T_SCONT_107 set CANT_107=CANT_107+@D1 WHERE C_MATE_107=@D2 AND CONT_107=@D3", con1)
        'creo el lector de parametros
        comando1.Parameters.Add(New SqlParameter("D1", Cantidad))
        comando1.Parameters.Add(New SqlParameter("D2", Cod_Mat))
        comando1.Parameters.Add(New SqlParameter("D3", CONTRATO))
        comando1.ExecuteNonQuery()
        con1.Close()
    End Sub
    Public Function Unidad(ByVal str As String) As String
        Dim resp As String = "ERR"
        Dim cnn1 As SqlConnection = New SqlConnection(conexion)
        'Abrimos la conexion
        cnn1.Open()
        'Creamos el comando para crear la consulta
        Dim Comando As New SqlClient.SqlCommand("select UNID_002 FROM M_MATE_002 WHERE CMATE_002 = @D1", cnn1)
        'Ejecutamos el commnad y le pasamos el parametro
        Comando.Parameters.Add(New SqlParameter("D1", str))
        Comando.ExecuteNonQuery()
        Dim Dusrs As SqlDataReader = Comando.ExecuteReader
        If Dusrs.Read.ToString Then
            resp = Dusrs.GetString(0)
        End If
        cnn1.Close()
        Return resp
    End Function
    Public Function validarMaterial(ByVal Cmate As String, ByVal DES As String) As Boolean
        Dim resp As Boolean
        'creo la cadena de conexion
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("SELECT DESC_002 FROM M_MATE_002 WHERE CMATE_002=@mate", con1)
        'creo el lector de parametros
        comando1.Parameters.Add(New SqlParameter("mate", Cmate))
        comando1.ExecuteNonQuery()
        'genero un lector
        Dim lector1 As SqlDataReader = comando1.ExecuteReader
        'pregunto si encontro
        If lector1.Read.ToString Then
            If lector1.GetValue(0) = DES Then
                resp = True
            Else
                resp = False
            End If
        Else
            resp = False
        End If
        'ciero la conexion
        con1.Close()
        Return resp
    End Function

    Public Sub AgregarAlertaStockMax(ByVal listaitems As List(Of Clase_mensaje))
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        con1.Open()
        For index = 0 To listaitems.Count - 1
            Dim comando1 As New SqlClient.SqlCommand("INSERT INTO TEMP_STOCK_MAX (DNIOPE,NOMAPE,CODMATE,DESCMATE,STOCKMAX,SOLICITA,STOCKACTUAL) VALUES (@D1,@D2,@D3,@D4,@D5,@D6,@D7) ", con1)
            comando1.Parameters.Add(New SqlParameter("D1", listaitems(index).dniope))
            comando1.Parameters.Add(New SqlParameter("D2", listaitems(index).nomope))
            comando1.Parameters.Add(New SqlParameter("D3", listaitems(index).material))
            comando1.Parameters.Add(New SqlParameter("D4", listaitems(index).descmate))
            comando1.Parameters.Add(New SqlParameter("D5", listaitems(index).stockmax))
            comando1.Parameters.Add(New SqlParameter("D6", listaitems(index).apedir))
            comando1.Parameters.Add(New SqlParameter("D7", listaitems(index).stockope))
            comando1.ExecuteNonQuery()
        Next
        con1.Close()
    End Sub
End Class

